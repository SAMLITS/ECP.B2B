using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    ///弹出查询控制属性
    public class QueryControlLayout
    {
        public QueryControlLayout(string bindTextControlId, string bindTextColumnName, string bindValueControlId, string bindValueColumnName)
        {
            BindTextControlId = bindTextControlId;
            BindTextColumnName = bindTextColumnName;
            BindValueControlId = bindValueControlId;
            BindValueColumnName = bindValueColumnName; 
        }

        public QueryControlLayout()
        { 
        }


        public QueryControlLayout(string bindTextControlId ,string bindValueControlId )
        {
            BindTextControlId = bindTextControlId; 
            BindValueControlId = bindValueControlId;  
        } 

        public QueryControlLayout(List<ExtendQueryFormControl> controls)
        {
            Controls = controls;
        }


        public void InitLayout()
        {
            //默认按钮集合
            actionButtonControls = new List<ActionButtonControl>
            {
                 new ActionButtonControl{ btnType= BtnType.Search , Id="btnQuerySearch_"+_bindTextControlId ,Title="搜索", onClickEvent="onQuerySearch_"+_bindTextControlId+"()" },
                 new ActionButtonControl{ btnType= BtnType.Submit,Id = "btnQuerySubmit_"+_bindTextControlId,Title="确定", onClickEvent="onQuerySubmit_"+_bindTextControlId+"()"},
                 new ActionButtonControl{ btnType = BtnType.Delete,Id = "btnQueryClear_"+_bindTextControlId,Title="清空", onClickEvent="onQueryClear_"+_bindTextControlId+"()" }
            };
            this.FormId = "frmQuerySearchWhere_" + _bindTextControlId;
            this.tableEntity = new TableEntity
            {
                listdgId = "query_listdg_" + _bindTextControlId,
                tableActionButtonControls = new List<ActionButtonControl> { },
                ToolbarId = ""
            };
        }

        private string _bindTextControlId;


        public string BindTextControlId {
            get { return _bindTextControlId; }
            set {
                _bindTextControlId = value;
                InitLayout();
            }
        }



        public string BindTextColumnName { get; set; }

        public string BindValueControlId { get; set; }
        public string BindValueColumnName { get; set; }

        public string FormId { get; set; }
    
        public string controlTitle { get; set; } = "查询控件";
        public List<ExtendQueryFormControl> Controls { get; set; }

        public bool IsQueryAvail { get; set; } = true;

        public List<ActionButtonControl> actionButtonControls { get; set; }
        public TableEntity tableEntity { get; set; } = new TableEntity() ;
    }
}
