using System;
using VOL.Entity.DomainModels;

namespace VOL.Entity.MQModel
{
    public  class AttachmentInformationMQ
    {
        public AttachmentInformation GetByAttachmentInformationMQ()
        {
            AttachmentInformation attachmentInformation = new AttachmentInformation();
            //attachmentInformation.AttachmentInformationID = this.AttachmentInformationID;
            attachmentInformation.BusinessTableId2 = this.BusinessTableId2;
            attachmentInformation.BusinessTableId1 = this.BusinessTableId1;
            attachmentInformation.BusinessTableId = this.BusinessTableId;
            attachmentInformation.BusinessType = this.BusinessType;
            attachmentInformation.Remark = this.Remark;
            attachmentInformation.UploadTime = this.UploadTime;
            attachmentInformation.Uploader = this.Uploader;
            attachmentInformation.UploaderID = this.UploaderID;
            attachmentInformation.AttachmentType = this.AttachmentType;
            attachmentInformation.AttachmentPath = this.AttachmentPath;
            attachmentInformation.AttachmentName = this.AttachmentName;
            //attachmentInformation.BucketName = this.BucketName;
            //attachmentInformation.ModuleEnum = this.ModuleEnum;
            attachmentInformation.AttachmentSize = this.AttachmentSize;
            return attachmentInformation;
        }

        /// <summary>
        ///附件信息主键
        /// </summary>      
        public int AttachmentInformationID { get; set; }

        /// <summary>
        ///业务表主键2
        /// </summary>   
        public int? BusinessTableId2 { get; set; }

        /// <summary>
        ///业务表主键1
        /// </summary>      
        public int? BusinessTableId1 { get; set; }

        /// <summary>
        ///业务表主键
        /// </summary>       
        public int? BusinessTableId { get; set; }

        /// <summary>
        ///业务类型（用于区别不同的业务/表）
        /// </summary>       
        public string BusinessType { get; set; }

        /// <summary>
        ///备注
        /// </summary>      
        public string Remark { get; set; }

        /// <summary>
        ///上传时间
        /// </summary>      
        public DateTime? UploadTime { get; set; }

        /// <summary>
        ///上传人
        /// </summary>       
        public string Uploader { get; set; }

        /// <summary>
        ///上传人主键
        /// </summary>      
        public int? UploaderID { get; set; }

        /// <summary>
        ///附件类型
        /// </summary>     
        public string AttachmentType { get; set; }

        /// <summary>
        ///附件路径
        /// </summary>       
        public string AttachmentPath { get; set; }

        /// <summary>
        ///附件名称
        /// </summary>      
        public string AttachmentName { get; set; }

        /// <summary>
        ///Minio桶名称	
        /// </summary>      
        public string BucketName { get; set; }

        /// <summary>
        /// 模块
        /// </summary>      
        public string ModuleEnum { get; set; }

        /// <summary>
        ///附件大小（KB）
        /// </summary>      
        public string AttachmentSize { get; set; }

        public byte[] AttachmentData { get; set; }

        public string ChecksSum { get; set; }
    }
}
