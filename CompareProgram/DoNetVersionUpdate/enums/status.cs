using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetVersionUpdate.enums
{

    public enum OUT_ORDER_STATUS
    {
        None = -1,
        [Description("草稿")]
        Draft = 0,
        [Description("待提交")]
        WaitSubmit = 1,
        [Description("已提交")]
        Submitted = 2,
        [Description("审批中")]
        Ongoing = 3,
        [Description("已通过")]
        Pass = 4,
        [Description("已驳回")]
        FailedPass = 5,
        [Description("在测")]
        Checking = 6,
        [Description("完成")]
        Finished = 7,
        [Description("修改申请单")]
        Edit = 8,
    }
}
