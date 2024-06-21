namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    /// <summary>
    /// 提交审核请求更新信息
    /// </summary>
    public class OrderHeaderInfo
    {
        public string updateFlg { get; set; }
        public int orderHeaderID { get; set; }
        public string auditRemark { get; set; }

        public bool IsAuditBySelf { get; set; }
    }

}
