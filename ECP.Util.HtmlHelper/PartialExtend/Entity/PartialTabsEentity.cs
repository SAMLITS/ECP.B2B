using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    public class PartialTabsEentity
    {
        public PartialTabsEentity(string _TabTitle, string _PartialViewName)
        {
            this.TabTitle = _TabTitle;
            this.PartialViewName = _PartialViewName;
        }

        public string TabTitle { get; set; }
       
        public string PartialViewName { get; set; }
    }
}
