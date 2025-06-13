using Newtonsoft.Json;
using System.Diagnostics;
using VOL.Core.Utilities;
using VOL.Entity.MQModel;

namespace RabbitMQDemo
{
    public class IPorcessService
    {
        public async Task<List<ProcessMQ>> PushBopProcess(PushProcessParam param)
        {
            List<ProcessMQ> processMQs = new List<ProcessMQ>();
            var datas = GetList<Process>(x => param.ProcessIDs.Contains(x.ProcessId))
                 .Include(x => x.ProcessOperations).ThenInclude(x => x.ProcessTestParas).ToList();
            if (datas != null && datas.Any())
            {
                foreach (var processItem in datas)
                {
                    ProcessMQ processMQ = new ProcessMQ();
                    _mapper.Map(processItem, processMQ);
                    processMQ.ProcessType = param.processType;
                    processMQ.ProcessClass = param.processClass;
                    if (processItem.ProcessOperations != null && processItem.ProcessOperations.Count > 0)
                    {
                        List<ProcessOperationMQ> processOperationMQs = new List<ProcessOperationMQ>();
                        foreach (var processOperationItem in processItem.ProcessOperations)
                        {
                            ProcessOperationMQ processOperationMQ = new ProcessOperationMQ();
                            _mapper.Map(processOperationItem, processOperationMQ);
                            processOperationMQs.Add(processOperationMQ);

                            if (processOperationItem.ProcessTestParas != null && processOperationItem.ProcessTestParas.Count > 0)
                            {
                                List<ProcessTestParaMQ> processTestParaMQs = new List<ProcessTestParaMQ>();
                                foreach (var processTestParaItem in processOperationItem.ProcessTestParas)
                                {
                                    ProcessTestParaMQ processTestParaMQ = new ProcessTestParaMQ();
                                    _mapper.Map(processTestParaItem, processTestParaMQ);
                                    if (!string.IsNullOrEmpty(processTestParaItem.ConditionFileName))
                                    {
                                        //处理附件
                                        AttachmentInformationMQ attachmentInformationMQ = new AttachmentInformationMQ();
                                        AttachmentInformation attachmentInformation = _attachmentInformationService.GetInfo(int.Parse(processTestParaItem.ConditionFileName));
                                        if (attachmentInformation != null)
                                        {
                                            _mapper.Map(attachmentInformation, attachmentInformationMQ);
                                            attachmentInformationMQ.AttachmentData = await _attachmentInformationService.GetAttachmentToBytes(attachmentInformation.BucketName, attachmentInformation.AttachmentPath);
                                            attachmentInformationMQ.ChecksSum = SecurityEncDecrypt.GetFileCheckSum(attachmentInformationMQ.AttachmentData);
                                            processTestParaMQ.AttachmentInformation = attachmentInformationMQ;
                                        }
                                    }
                                    processTestParaMQs.Add(processTestParaMQ);
                                }
                                processOperationMQ.processTestParaMQs = processTestParaMQs;
                            }
                        }
                        processMQ.processOperationMQs = processOperationMQs;
                    }
                    processMQs.Add(processMQ);
                }
            }
            return processMQs;
        }



        public async Task<string> PopBopMessage(string message)
        {
            try
            {
                ProcessMQ processMQ = JsonConvert.DeserializeObject<ProcessMQ>(message);
                if (processMQ == null)
                {
                    return WebResponseContent.Instance.ErrorAutoTranslate("反序列化失败");
                }
                var result = GetList<Process>().Where(x => x.ProcessName == processMQ.ProcessName).Any();
                if (result)
                {
                    return WebResponseContent.Instance.ErrorAutoTranslate($"{processMQ.ProcessName}bop流程数据已经存在，更新请先删除");
                }
                //获取  processtype，processclass，指定bop存放的地址
                string processTypeName = processMQ.ProcessType;
                string processClassName = processMQ.ProcessClass;
                var data = GetList<ProcessType>().Include(x => x.ProcessClasses).Where(x => x.ProcessTypeName == processTypeName &&
                x.ProcessClasses.Where(y => y.ProcessClassName == processClassName).Any()).FirstOrDefault();
                if (data == null)
                {
                    return WebResponseContent.Instance.ErrorAutoTranslate($"没有找到{processTypeName}下的{processClassName}的类型");
                }

                return await UseTransactionAsync(async () =>
                {
                    Process process = processMQ.GetByProcessMQ(data.ProcessTypeId, data.ProcessClasses.First().ProcessClassId);
                    if (!string.IsNullOrEmpty(process.ProcessName))
                    {
                        process.BopStatus = (int)Bop_Status.待提交;    //确保可以修改                             
                                                                    //更新字典类型                  
                        var projectAttributeDic = dictService.GetEnumDic("projectAttributeDic");
                        var projectAttributeVo = dictService.GetDictValueVoByRemark(projectAttributeDic, process.ProjectAttribute);
                        if (projectAttributeVo != null)
                        {
                            process.ProjectAttribute = projectAttributeVo.key;
                        }
                        var ProjectTypeCodeDic = dictService.GetEnumDic("project_type");
                        var ProjectTypeCodeVo = dictService.GetDictValueVoByRemark(ProjectTypeCodeDic, process.ProjectType);
                        if (ProjectTypeCodeVo != null)
                        {
                            process.ProjectType = ProjectTypeCodeVo.value;
                            process.ProjectTypeCode = ProjectTypeCodeVo.key;
                        }
                        var TestClassEnumDic = dictService.GetEnumDic("TestGenra");
                        var TestClassEnumVo = dictService.GetDictValueVoByRemark(TestClassEnumDic, process.TestClass);
                        if (TestClassEnumVo != null)
                        {
                            process.TestClass = TestClassEnumVo.value;
                            process.TestClassEnum = TestClassEnumVo.key;
                        }
                        var SampleGroupCodeDic = dictService.GetEnumDic("SampleGroup_RB");
                        var SampleGroupCodeVo = dictService.GetDictValueVoByRemark(SampleGroupCodeDic, process.SampleGroup);
                        if (SampleGroupCodeVo != null)
                        {
                            process.TestClass = SampleGroupCodeVo.value;
                            process.TestClassEnum = SampleGroupCodeVo.key;
                        }
                        var StepEnumDic = dictService.GetEnumDic("StepEnum");
                        if (processMQ.processOperationMQs != null && processMQ.processOperationMQs.Count > 0)
                        {
                            List<ProcessOperation> processOperations = new List<ProcessOperation>();
                            foreach (var processOperationItemMQ in processMQ.processOperationMQs)
                            {
                                ProcessOperation processOperation = processOperationItemMQ.GetByProcessOperationMQ();
                                if (processOperationItemMQ.processTestParaMQs != null && processOperationItemMQ.processTestParaMQs.Count > 0)
                                {
                                    List<ProcessTestPara> processTestParas = new List<ProcessTestPara>();
                                    foreach (var processTestParaItemMQ in processOperationItemMQ.processTestParaMQs)
                                    {
                                        ProcessTestPara processTestPara = processTestParaItemMQ.GetByProcessTestParaMQ();
                                        var StepEnumVo = dictService.GetDictValueVoByRemark(StepEnumDic, processTestPara.Name);
                                        if (StepEnumVo != null)
                                        {
                                            processTestPara.Name = TestClassEnumVo.value;
                                            processTestPara.StepEnum = TestClassEnumVo.key;
                                        }
                                        if (processTestParaItemMQ.AttachmentInformation != null)
                                        {
                                            byte[] fileData = processTestParaItemMQ.AttachmentInformation.AttachmentData;
                                            // 计算接收数据的校验和
                                            string calculatedChecksum = SecurityEncDecrypt.GetFileCheckSum(fileData);

                                            if (processTestParaItemMQ.AttachmentInformation.ChecksSum != calculatedChecksum)
                                            {
                                                return WebResponseContent.Instance.ErrorAutoTranslate($"文件{processTestParaItemMQ.AttachmentInformation.AttachmentName} 校验失败，可能已损坏");
                                            }
                                            //获取附件，上传到站点的minIO上，修改新路径
                                            AttachmentInformation attachmentInformation = processTestParaItemMQ.AttachmentInformation.GetByAttachmentInformationMQ();
                                            attachmentInformation.ModuleEnum = "bop";//bop模块，固定死的                                                                                                                      
                                            AttachmentInformation information = await _attachmentInformationService.SaveFileAsync(fileData, attachmentInformation);
                                            processTestPara.ConditionFileName = information.AttachmentInformationID.ToString();
                                        }
                                        processTestParas.Add(processTestPara);
                                    }
                                    processOperation.ProcessTestParas = processTestParas;
                                }
                                processOperations.Add(processOperation);
                            }
                            process.ProcessOperations = processOperations;
                        }
                        Insert<Process>(process, true);
                        return WebResponseContent.Instance.OK();

                    }
                    else
                    {
                        return WebResponseContent.Instance.ErrorAutoTranslate($"{processMQ.ProcessName} 解析失败");
                    }
                });
            }
            catch (Exception ex)
            {
                return WebResponseContent.Instance.Error(LangManager.GetText("拉取异常") + ex.Message + ex.StackTrace);
            }
        }
    }
}
}
