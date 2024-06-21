using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class OutSampleReceiptHeaderDto
    {      
        public int? OutSampleReceiptHeaderId { get; set; }

        /// <summary>
        ///备注
        /// </summary>    
        public string Remark { get; set; }

        /// <summary>
        ///收样人员
        /// </summary>       
        public string Recipient { get; set; }

        /// <summary>
        ///送样人员
        /// </summary>       
        public string Sender { get; set; }

        /// <summary>
        ///状态
        /// </summary>        
        public string Status { get; set; }
      
        /// <summary>
        ///样品总数量
        /// </summary>      
        public int? SampleQuantity { get; set; }

        /// <summary>
        ///收样时间
        /// </summary>      
        public DateTime? ReceiptTime { get; set; }

        /// <summary>
        ///收样单号
        /// </summary>      
        public string OutSampleReceiptNo { get; set; }
       
        /// <summary>
        ///区域ID
        /// </summary>      
        public int? AreaID { get; set; }

        public List<OutSampleReceiptDetailDto> OutSampleReceiptDetailDtos { get; set; } = new List<OutSampleReceiptDetailDto>();
    }

    public class OutSampleReceiptDetailDto
    {
        /// <summary>
        ///收样单明细主键
        /// </summary>      
        public int? OutSampleReceiptDetailId { get; set; }

        /// <summary>
        ///收样单主键
        /// </summary>
       
        public int? OutSampleReceiptHeaderId { get; set; }
      
        /// <summary>
        ///样品生产条码
        /// </summary>      
        public string BarCode { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
     
        public string SampleSpec { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
      
        public string SampleType { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>
       
        public string TaskType { get; set; }

        /// <summary>
        ///备注
        /// </summary>     
        public string Remark { get; set; }
    
      
        /// <summary>
        ///测试申请人
        /// </summary>       
        public string Applicant { get; set; }
                    

        /// <summary>
        ///来样检备注
        /// </summary>      
        public string ReceiptCheckRemark { get; set; }
           

        /// <summary>
        ///样品系列
        /// </summary>   
        public string SampleSeries { get; set; }

        /// <summary>
        ///样品SOC
        /// </summary>      
        public string SampleSOC { get; set; }

        /// <summary>
        ///样品SOC描述
        /// </summary>     
        public string SampleSOCRemark { get; set; }

        /// <summary>
        ///样品状态
        /// </summary>      
        public string SampleStatus { get; set; }

        /// <summary>
        ///有潜在异常风险
        /// </summary>    
        public bool IsPotentialRisk { get; set; } = false;

        /// <summary>
        ///潜在异常风险描述
        /// </summary>     
        public string PotentialRiskRemark { get; set; }
      
        /// <summary>
        ///夹具
        /// </summary>       
        public string Fixture { get; set; }

        /// <summary>
        ///工装
        /// </summary>     
        public string Workwear { get; set; }
       

        /// <summary>
        ///线束
        /// </summary>    
        public string WireHarness { get; set; }

    }
}
