using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    public class SingleTabsEntity
    {
        public bool IsShowForm { get; set; } = true;
        public string FormId { get; set; }
        public List<Tabs_SingleCollection> TabsSingleColl { get; set; } = new List<Tabs_SingleCollection>();
        public List<ActionButtonControl> actionButtonControls { get; set; } = new List<ActionButtonControl>();

        /// <summary>
        /// 默认只可为null   因为已加入判断
        /// </summary>
        public TableEntity tableEntity { get; set; }

        public class Tabs_SingleCollection {

            public Tabs_SingleCollection(string TabTitle, SingleFormLayout SingleFormLayout)
            {
                this._TabTitle = TabTitle;
                this._SingleFormLayout = SingleFormLayout;
            }

            public string _TabTitle { get; set; }

            public SingleFormLayout _SingleFormLayout { get; set; }
        }
    }
}
