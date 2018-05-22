using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    public class SingleFormLayout
    {
        public bool IsShowForm { get; set; } = true;
        public string FormId { get; set; } 
        public int colCount { get; set; } = 3;
        public List<ExtendSingleFormControl> Controls { get; set; }
        public bool IsContentClass { get; set; } = false;
        public List<ActionButtonControl> actionButtonControls { get; set; } = new List<ActionButtonControl>();

        /// <summary>
        /// 默认只可为null   因为已加入判断
        /// </summary>
        public TableEntity tableEntity { get; set; }
    }
}
