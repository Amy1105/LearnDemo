using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VOL.Entity.DomainModels;

namespace VOL.Entity.MQModel
{
    public  class ProcessOperationMQ
    {
        public ProcessOperationMQ()
        {
          
        }

        public ProcessOperation GetByProcessOperationMQ()
        {
            ProcessOperation processOperation= new ProcessOperation();
            //  ProcessOperationId = this.ProcessOperationId;
          processOperation. ProcessId = this.ProcessId;
          processOperation. OperationId = this.OperationId;
          processOperation. OperationSequenceNo = this.OperationSequenceNo;
          processOperation. OperationType = this.OperationType;
          processOperation. EffectiveDate = this.EffectiveDate;
          processOperation. DiscontinueDate = this.DiscontinueDate;
          processOperation. Description = this.Description;
          processOperation. Remark = this.Remark;
          processOperation. OperationDay = this.OperationDay;
          processOperation. OperationDayOld = this.OperationDayOld;
          processOperation. TestType = this.TestType;
          processOperation. WorkCenter = this.WorkCenter;
          processOperation. OperationCode = this.OperationCode;
          processOperation. OperationName = this.OperationName;
          processOperation. DefaultSamplingTime = this.DefaultSamplingTime;
          processOperation. Description1 = this.Description1;
          processOperation. WaterCooler = this.WaterCooler;
          processOperation. TestCodeSerialTemperature = this.TestCodeSerialTemperature;
          processOperation. TestCodeSerialNumber = this.TestCodeSerialNumber;
          processOperation. OperationAttributeEnum = this.OperationAttributeEnum;
            return processOperation;
        }

        public int ProcessOperationId { get; set; }

        /// <summary>
        ///测试流程
        /// </summary>      
        public int ProcessId { get; set; }  
        
        /// <summary>
        ///工序名称
        /// </summary>      
        public int OperationId { get; set; }

        /// <summary>
        ///序号
        /// </summary>     
        public int OperationSequenceNo { get; set; }

        /// <summary>
        ///工序类型
        /// </summary>      
        public string OperationType { get; set; }

        /// <summary>
        ///生效日期
        /// </summary>       
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        ///失效日期
        /// </summary>     
        public DateTime? DiscontinueDate { get; set; }

        /// <summary>
        ///测试简码
        /// </summary>      
        public string Description { get; set; }

        /// <summary>
        ///备注
        /// </summary>     
        public string Remark { get; set; }

        /// <summary>
        ///工时(天)
        /// </summary>      
        public decimal? OperationDay { get; set; }

        /// <summary>
        ///参考工时(天)
        /// </summary>     
        public decimal? OperationDayOld { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>    
        public string TestType { get; set; }

        /// <summary>
        ///工作中心
        /// </summary>       
        public string WorkCenter { get; set; }

        /// <summary>
        ///基础工序编码
        /// </summary>     
        public string OperationCode { get; set; }

        /// <summary>
        ///工序名称
        /// </summary>     
        public string OperationName { get; set; }

        /// <summary>
        ///全局采样时间
        /// </summary>      
        public decimal? DefaultSamplingTime { get; set; }

        /// <summary>
        ///描述1(上个描述用作了测试简码)
        /// </summary>     
        public string Description1 { get; set; }

        /// <summary>
        ///全局水冷机
        /// </summary>      
        public string WaterCooler { get; set; }

        /// <summary>
        /// 测试简码温度
        /// </summary>       
        public string TestCodeSerialTemperature { get; set; }

        /// <summary>
        /// 测试简码序号
        /// </summary>     
        public string TestCodeSerialNumber { get; set; }

        /// <summary>
        /// 工序属性
        /// </summary>       
        public string OperationAttributeEnum { get; set; }


        [ForeignKey("ProcessOperationId")]
        public ICollection<ProcessTestParaMQ> processTestParaMQs { get; set; }
    }    
}
