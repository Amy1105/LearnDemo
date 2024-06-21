using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    /// <summary>
    /// 附件参数
    /// </summary>
    public class AttachmentsParamters
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public IEnumerable<IFormFile> FileInput { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string AttachmentType { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 业务表主键
        /// </summary>
        public int BusinessTableId { get; set; }

        /// <summary>
        /// 业务表主键
        /// </summary>
        public int BusinessTableId1 { get; set; }

        /// <summary>
        /// 业务表主键
        /// </summary>
        public int BusinessTableId2 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

}
