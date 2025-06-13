using VOL.Entity.DomainModels;

namespace VOL.Entity.MQModel
{
    public  class ProcessTestParaMQ
    {
        public ProcessTestPara GetByProcessTestParaMQ()
        {
            ProcessTestPara para = new ProcessTestPara();
            //para.OperationTestParaId = this.OperationTestParaId;
            //para.OperationId = this.OperationId;
            para.SequenceNo = this.SequenceNo;
            para.Name = this.Name;
            para.StepEnum = this.StepEnum;
            para.Description = this.Description;
            para.DurationSeconds = this.DurationSeconds;
            para.CutOffVoltage = this.CutOffVoltage;
            para.CutOffCurrent = this.CutOffCurrent;
            para.CutOffCapacity = this.CutOffCapacity;
            para.DelatTime = this.DelatTime;
            para.Remark = this.Remark;
            para.Temperature = this.Temperature;
            para.Voltage = this.Voltage;
            para.Current = this.Current;
            para.EndingCondition = this.EndingCondition;
            para.SampleRate = this.SampleRate;
            para.To = this.To;
            para.From = this.From;
            para.Power = this.Power;
            para.RepeatFromSequenceNo = this.RepeatFromSequenceNo;
            para.RepeatTimes = this.RepeatTimes;
            para.WaterCooler = this.WaterCooler;
            para.VariableJson = this.VariableJson;
            para.ConditionJson = this.ConditionJson;
            para.CutTemperature = this.CutTemperature;
            return para;
        }
        /// <summary>
        ///基础工序参数主键
        /// </summary>      
        public int OperationTestParaId { get; set; }

        /// <summary>
        ///基础工序主键
        /// </summary>      
        public int OperationId { get; set; }

        /// <summary>
        ///工步号
        /// </summary>    
        public int SequenceNo { get; set; }

        /// <summary>
        ///工步名称
        /// </summary>       
        public string Name { get; set; }


        /// <summary>
        ///工步名称枚举值
        /// </summary>      
        public string StepEnum { get; set; }

        /// <summary>
        ///描述
        /// </summary>      
        public string Description { get; set; }

        /// <summary>
        ///截止时间
        /// </summary>      
        public string DurationSeconds { get; set; }

        /// <summary>
        ///截止电压
        /// </summary>    
        public string CutOffVoltage { get; set; }

        /// <summary>
        ///截止电流
        /// </summary>       
        public string CutOffCurrent { get; set; }

        /// <summary>
        ///截止容量
        /// </summary>       
        public string CutOffCapacity { get; set; }

        /// <summary>
        ///采样时间变量
        /// </summary>     
        public decimal? DelatTime { get; set; }

        /// <summary>
        ///备注
        /// </summary>      
        public string Remark { get; set; }

        /// <summary>
        ///温度
        /// </summary>       
        public string Temperature { get; set; }

        /// <summary>
        ///电压
        /// </summary>      
        public string Voltage { get; set; }

        /// <summary>
        ///电流
        /// </summary>     
        public string Current { get; set; }

        /// <summary>
        ///截止条件
        /// </summary>       
        public string EndingCondition { get; set; }

        /// <summary>
        ///采样率
        /// </summary>      
        public decimal? SampleRate { get; set; }

        /// <summary>
        ///终止值
        /// </summary>      
        public decimal? To { get; set; }

        /// <summary>
        ///起始值
        /// </summary>       
        public decimal? From { get; set; }

        /// <summary>
        ///测试功率
        /// </summary>       
        public string Power { get; set; }

        /// <summary>
        ///循环开始序号
        /// </summary>      
        public int? RepeatFromSequenceNo { get; set; }

        /// <summary>
        ///循环次数
        /// </summary>       
        public int? RepeatTimes { get; set; }

        /// <summary>
        ///模拟工况文件路径
        /// </summary>           
        public AttachmentInformationMQ AttachmentInformation { get; set; } = null;

        /// <summary>
        ///水冷机
        /// </summary>      
        public string WaterCooler { get; set; }

        /// <summary>
        ///高级变量参数
        /// </summary>      
        public string VariableJson { get; set; }

        /// <summary>
        ///高级条件参数
        /// </summary>       
        public string ConditionJson { get; set; }

        /// <summary>
        ///温度截止
        /// </summary>      
        public string CutTemperature { get; set; }
    }
}
