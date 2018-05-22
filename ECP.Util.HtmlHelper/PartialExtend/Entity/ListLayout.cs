using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    public class ListLayout
    {
        /// <summary>
        /// 是否显示查询区域
        /// </summary>
        public bool IsShowQueryLayout { get; set; } = true;


        public string FormId { get; set; } = "frmQueryWhere";
        public string BtnSearchId { get; set; } = "btn_query";
        public int colCount { get; set; } = 3;
        public List<ExtendQueryFormControl> Controls { get; set; }
       
        public bool IsContentClass { get; set; } = true;

        public string contentModalLayoutId { get; set; } = "contentModalLayout";

        public TableEntity tableEntity { get; set; } = new TableEntity();

    }
}
