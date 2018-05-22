using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Controls.Address
{
    public class _LayoutSelectCityEntity
    {
        public _LayoutSelectCityEntity(string bindTextControlId, string bindValueControlId)
        {
            BindTextControlId = bindTextControlId;
            BindValueControlId = bindValueControlId;
        }
        public string BindTextControlId { get; set; }
        public string BindValueControlId { get; set; }
    }
}
