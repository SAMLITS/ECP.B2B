using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.PartialExtend.Entity
{
    #region 后台Manager控件

    #region 基础控件
    /// <summary>
    /// 表单控件基础属性
    /// </summary>
    public class FormControl
    {
        public FormControl(FormControl formControl)
        {
            this.Id = formControl.Id;
            this.Name = formControl.Name;
            this.title = formControl.title;
            this.defaultVal = formControl.defaultVal;
            this.controlType = formControl.controlType;
            this.IsFixed = formControl.IsFixed;

            this.IsPersonDisabled = formControl.IsPersonDisabled;
            this.IsRequired_hint = formControl.IsRequired_hint;
            this.IsDefaultDisabled = formControl.IsDefaultDisabled;



            this.PartialViewName = formControl.PartialViewName;
            this.PartialViewData = formControl.PartialViewData;
            this.ViewComponentName = formControl.ViewComponentName;
            this.ViewComponentData = formControl.ViewComponentData; 
    }

        public FormControl() { }
        public FormControl(string _Name, string _title, ControlType _controlType)
        {
            this.Name = _Name;
            this.title = _title;
            this.controlType = _controlType;
        }
        public FormControl(string _Name,ControlType _controlType)
        {
            this.Name = _Name;
            this.controlType = _controlType;
        }


        public string Id { get; set; }
        public string Name { get; set; }
        public string title { get; set; }
        public string defaultVal { get; set; } = "";
        public ControlType controlType { get; set; }


        /// <summary>
        /// 一般用于存放固定值  目前仅支持hidden
        /// </summary>
        public bool IsFixed { get; set; } = false;

        /// <summary>
        /// 是否需要使用自定义的禁用方法功能
        /// </summary>
        public bool IsPersonDisabled { get; set; } = false;
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired_hint { get; set; } = false;
        /// <summary>
        /// 是否默认禁用
        /// </summary>
        public bool IsDefaultDisabled { get; set; } = false;


        /// <summary>
        ///  相关parial区块   针对 parialcontrol 类型控件
        /// </summary>
        public string PartialViewName { get; set; }
        public object PartialViewData { get; set; }


        /// <summary>
        ///  相关viewcomponent区块   针对 viewcomponent 类型控件
        /// </summary>
        public string ViewComponentName { get; set; }
        public object ViewComponentData { get; set; }


        /// <summary>
        /// 数据格式
        /// </summary>
        public ControlDataFormatter  controlDataFormatter { get; set; }

        /// <summary>
        /// 跨单元格
        /// </summary>
        public int colspanCount { get; set; } = 1;
    }

    /// <summary>
    /// 数据格式
    /// </summary>
    public enum ControlDataFormatter
    {
        /// <summary>
        /// 默认
        /// </summary>
        def,
        money,
        weight,
        volume
    }
    
    /// <summary>
    /// 表单控件类型
    /// </summary>
    public enum ControlType
    {
        textbox,
        password,
        select,
        hidden,
        datetime,
        datetimebet,
        textarea,
        address,
        img,
        parialcontrol,
        viewcomponent
    }
    #endregion


    #region 查询区域表单内的控件
    public class ExtendQueryFormControl : FormControl
    {
        public ExtendQueryFormControl(string _Name, string _title, ControlType _controlType):base(_Name,_title, _controlType)
        { 
        }
        public ExtendQueryFormControl(string _Name,  ControlType _controlType) : base(_Name , _controlType)
        {
        }
        public ExtendQueryFormControl(string _title, ControlType _controlType,List<DateTimeControlFormProperGroup> _dtDateTimeBets) 
        {
            base.title = _title;
            base.controlType = _controlType;
            this.dtDateTimeBets = _dtDateTimeBets;
        }


        /// <summary>
        /// 是否支持模糊查询  用于textbox
        /// </summary>
        public bool IsLike { get; set; } = false;

        public SelectControlDataSourceSet dataSource { get; set; }
        public ExtendSelectControl GetExtendSelectControl(bool _IsAllOption = false)
        {
            return new ExtendSelectControl(this) { IsAllOption = _IsAllOption };
        }


        public List<DateTimeControlFormProperGroup> dtDateTimeBets { get; set; }
        public List<ExtendDateTimeControl> GetExtendDateTimeControls()
        {
            List<ExtendDateTimeControl> datetimeList = new List<ExtendDateTimeControl>();
            for (int i = 0; i < dtDateTimeBets.Count; i++)
            {
                datetimeList.Add(new ExtendDateTimeControl(dtDateTimeBets[i]));
            }
            return datetimeList;
        }
    }
    #endregion


    #region 单个页面表单内控件
    public class ExtendSingleFormControl : FormControl
    {
        public ExtendSingleFormControl(string _Name, string _title, ControlType _controlType) : base(_Name, _title, _controlType)
        {
        }
        public ExtendSingleFormControl(string _Name, ControlType _controlType) : base(_Name, _controlType)
        {
        }
        public ExtendSingleFormControl() : base()
        {
        }

        public SelectControlDataSourceSet dataSource { get; set; }
        public ExtendSelectControl GetExtendSelectControl(bool _IsAllOption = false)
        {
            return new ExtendSelectControl(this) { IsAllOption = _IsAllOption };
        }


        public DateTimeControlProperSet dtProperSet { get; set; }
        public ExtendDateTimeControl GetExtendDateTimeControl()
        {
            return new ExtendDateTimeControl(new DateTimeControlFormProperGroup { formControl = this, dateTimeControlProperSet = dtProperSet });
        }
    }
    #endregion


    #region Select控件
    /// <summary>
    /// Select控件扩展定义
    /// </summary>
    public class ExtendSelectControl : FormControl
    {
        public ExtendSelectControl(ExtendQueryFormControl formControl):base(formControl)
        {
            this.dataSource = formControl.dataSource;
        }
        public ExtendSelectControl(ExtendSingleFormControl formControl) : base(formControl)
        {
            this.dataSource = formControl.dataSource;
        }


        public bool IsAllOption { get; set; } = false;

        public SelectControlDataSourceSet dataSource { get; set; }
    }

    /// <summary>
    /// Select 控件数据来源
    /// </summary>
    public class SelectControlDataSourceSet
    {

        //3选1即可
        public SelectControlDataSourceSet(Dictionary<string, string> _dictionaryData)
        {
            this.DictionaryData = _dictionaryData;
        }

        public SelectControlDataSourceSet(string _lookupName, bool _isShowEmptyOption = false)
        {
            this.LookupName = _lookupName;
            this.IsShowEmptyOption = _isShowEmptyOption; 
        } 


        /// <summary>
        /// 1/初始化数据集合
        /// </summary>
        public Dictionary<string, string> DictionaryData { get; set; } = null;




        /// <summary>
        /// 2/码表
        /// 是否需要空选项
        /// </summary>
        public bool IsShowEmptyOption { get; set; } = false;

        /// <summary>
        /// 码表数据来源
        /// </summary>
        public LookupTypeSource LookupTypeSource { get; set; } = LookupTypeSource.B2B;
        #region 码表条件配置  与 ECP.B2b.ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams 实体相对应
        public string LookupName { get; set; } = null;
        public bool IsBetweenOt { get; set; } = false;
        public List<string> LOOKUP_CODE_List { get; set; } 
        public string ATTIBUTE1 { get; set; }
        public string ATTIBUTE2 { get; set; }
        public string ATTIBUTE3 { get; set; }
        public string ATTIBUTE4 { get; set; } 
        public string ATTIBUTE5 { get; set; }
        public string TAG { get; set; }
        #endregion

    }

    public enum LookupTypeSource
    {
        ECP,
        B2B
    }

    #endregion


    #region datetime 控件
    /// <summary>
    /// DateTime时间控件扩展定义
    /// </summary>
    public class ExtendDateTimeControl : ECP.Util.HtmlHelper.PartialExtend.Entity.FormControl
{
        public ExtendDateTimeControl(DateTimeControlFormProperGroup dateTimeControlFormProperGroup) :base(dateTimeControlFormProperGroup.formControl)
        {
            this.dtProperSet = dateTimeControlFormProperGroup.dateTimeControlProperSet;
        }
         

        public DateTimeControlProperSet dtProperSet { get; set; }
    }

    public class DateTimeControlFormProperGroup {
        public FormControl formControl { get; set; }
        public DateTimeControlProperSet  dateTimeControlProperSet { get; set; }
    }

    /// <summary>
    /// DateTime 控件 属性设置
    /// </summary>
    public class DateTimeControlProperSet
    {
        public string maxControlId { get; set; }
        public string minControlId { get; set; }
        public string formatter { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 最小时间值
        /// </summary>
        public string minDateVal { get; set; }

        /// <summary>
        /// 是否最小为当前时间
        /// </summary>
        public bool IsMinCurrentDate { get; set; } = false;
    }
    #endregion


    #region 按钮控件
    public class ActionButtonControl
    {
        public ActionButtonControl() { }
        public ActionButtonControl(BtnType _btnType)
        {
            this.btnType = _btnType;
        }
        public ActionButtonControl(string _Id , string _Title,BtnType _btnType)
        {
            this.Id = _Id;
            this.Title = _Title;
            this.btnType = _btnType;
        }

        public ActionButtonControl(string _Id, string _Title, BtnType _btnType, string _onClickEvent)
        {
            this.Id = _Id;
            this.Title = _Title;
            this.btnType = _btnType;
            this.onClickEvent = _onClickEvent;
        }

        public string Id { get; set; } = null;
        public string Title { get; set; } = null;
        public BtnType btnType { get; set; }
        public string onClickEvent { get; set; }



        public string CreateBtn()
        { 
                (Id, Title) =
                    btnType == BtnType.Add ? (Id??"btnAdd", Title??"新增")
                    : btnType == BtnType.Modify ? (Id ?? "btnModify", Title ?? "编辑")
                    : btnType == BtnType.Delete ? (Id ?? "btnDelete", Title ?? "删除")

                    : btnType == BtnType.Back ? (Id ?? "btnBack", Title ?? "返回")
                    : btnType == BtnType.Cancel ? (Id ?? "btnCancel", Title ?? "取消")
                    : btnType == BtnType.Submit ? (Id ?? "btnSubmit", Title ?? "提交")
                    : btnType == BtnType.Show ? (Id ?? "btnShow", Title ?? "查看")
                    : btnType == BtnType.Show ? (Id ?? "Search", Title ?? "搜索")
                    : (Id ?? "", Title ?? "");

            return string.Format(BtnArray[Convert.ToInt32(btnType)], Id, Title, onClickEvent);
        }

        private List<string> BtnArray = new List<string>()
        {
            "<a id='{0}' onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-plus'>{1}</a>",
            "<a id='{0}'  onclick='{2}' class='btn btn-primary glyphicon glyphicon-pencil' aria-hidden='true'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-danger glyphicon glyphicon-remove'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-user'>{1}</a>",

            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-circle-arrow-left'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-arrow-down'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-arrow-up'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-zoom-in'>{1}</a>",
            "<a id = '{0}'  onclick='{2}' aria-hidden='true' class='btn btn-primary glyphicon glyphicon-search'>{1}</a>"
    };
    }


    public enum BtnType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Modify = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,
        /// <summary>
        /// 用户
        /// </summary>
        User=3,
        /// <summary>
        /// 返回
        /// </summary>
        Back = 4, 
        /// <summary>
        /// 取消
        /// </summary>
        Cancel=5,
        /// <summary>
        /// 提交
        /// </summary>
        Submit=6,
        /// <summary>
        /// 查看
        /// </summary>
        Show=7,
        ///搜索
        ///
        Search=8
    }
    #endregion



#endregion



     


    #region 客户端控件

    public class ClientSelectControlEntity
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public string defaultVal { get; set; } = "";

        public bool IsAllOption { get; set; } = false;

        /// <summary>
        /// 是否默认禁用
        /// </summary>
        public bool IsDefaultDisabled { get; set; } = false;



        public SelectControlDataSourceSet dataSource { get; set; } 
    }

    #endregion
}
