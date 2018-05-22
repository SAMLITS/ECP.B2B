using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    public class TableEntity
    {
        public string ToolbarId { get; set; } = "toolbar";
        public string listdgId { get; set; } = "listdg";

        public List<ActionButtonControl> tableActionButtonControls { get; set; } = new List<ActionButtonControl>
        {
             new ActionButtonControl{ btnType= BtnType.Add },
             new ActionButtonControl{ btnType= BtnType.Modify},
             new ActionButtonControl{ btnType = BtnType.Delete }
        };
    }
}
