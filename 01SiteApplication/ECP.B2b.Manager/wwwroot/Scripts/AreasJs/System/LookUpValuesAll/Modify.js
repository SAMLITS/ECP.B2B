/********************************************
* 模块名称：LookUpValuesAll/Modify
* 功能说明：编辑页面js代码操作  
* 创 建 人：LDY
* 创建时间：2017-8-9
* 修 改 人：LDY
* 修改时间：2017-8-10
* ******************************************/
$(function () {
    //表单校验
    $('#lookUpValuesAll-modify-form').validate({
        rules: {
            LOOKUP_TYPE: { required: true },
            LOOKUP_TYPE_NAME: { required: true }
        }
    });
    //绑定主表单数据
    $('#lookUpValuesAll-modify-form').onExInitDataForm({
        dataUrl: '/System/LookUpValuesAll/FindById?',
        params: { ID: $("#hidID").val() }
    });
    //加载明细列表数据
    LoadLookUpValuesGrid();
    $("#btn_save").click(function () {
        $('#lookUpValuesAll-modify-form').onExPostForm({
            submitUrl: '/System/LookUpValuesAll/Modify',
            onSuccess: function () {
                //刷新List页面网格数据
                window.parent.$("#mainListGrid").bootstrapTable("refresh");
            }
        });
    });
    $("#btn_back").click(function () {
        layerEx.closeIframeModal();
    });
    $("#btnAdd").click(function () {
        Add();
    });
    $("#btnModify").click(function () {
        Modify();
    });
    $("#btnDelete").click(function () {
        $('#subListGrid').onExDelete("/System/LookUpValues/Remove");
    });
});

/*
* 加载码表明细网格
*/
function LoadLookUpValuesGrid() {
    $('#subListGrid').onExBootstrapPageQuery({
        url: '/System/LookUpValues/List',      //请求后台的URL（*）
        columns: [
            { field: 'LOOKUP_CODE', title: '值代码', sortable: true, width: 100 },
            { field: 'LOOKUP_MEANING', title: '值名称', sortable: true, width: 120 },
            { field: 'LOOKUP_DESCRIPTION', title: '说明', sortable: true, width: 100 },
            { field: 'TAG', title: '标签', sortable: true, width: 80 },
            { field: 'ENABLED_FLAG_NAME', title: '启用标识', sortable: true, width: 100 },
            { field: 'ATTIBUTE1', title: '属性1', sortable: true, width: 80 },
            { field: 'ATTIBUTE2', title: '属性2', sortable: true, width: 80 },
            { field: 'ATTIBUTE3', title: '属性3', sortable: true, width: 80 },
            { field: 'ATTIBUTE4', title: '属性4', sortable: true, width: 80 },
            { field: 'ATTIBUTE5', title: '属性5', sortable: true, width: 80 },
            { field: 'START_DATE_ACTIVE', title: '生效日期', sortable: true, width: 140, formatter: $.dateFormat },
            { field: 'END_DATE_ACTIVE', title: '失效日期', sortable: true, width: 140, formatter: $.dateFormat }
        ],
        pageSize: 5,                       //每页的记录行数（*）
        pageList: [5, 10, 15],        //可供选择的每页的行数（*）
    });
}

/*
* 新增码表明细数据
*/
function Add() {
    $('#subContentModalLayout').onExActionAdd({
        title: "码表明细-新增",
        reqUrl: '/System/LookUpValues/Add',
        params: { LOOKUP_VALUES_ALL_ID: $("#hidID").val(), LOOKUP_TYPE: $("#txtLOOKUP_TYPE").val() },
        modalLayout: '#lookUpValues-add-form ',
        reloaddg: '#subListGrid ',
        IsResetForm: false,
        modalWidth: $.layoutWidth2
    });
}

/*
* 编辑码表明细数据
*/
function Modify() {
    $("#subContentModalLayout").onExActionEdit({
        title: "码表明细-编辑",
        reqUrl: "/System/LookUpValues/Modify",
        dataUrl: "/System/LookUpValues/FindById",
        modalLayout: "#lookUpValues-modify-form",
        reloaddg: "#subListGrid",
        modalWidth: $.layoutWidth2,
        onInitSuccess: function (data) {
            $("#txtModifySTART_DATE_ACTIVE").val($.dateFormat(data.START_DATE_ACTIVE));
            $("#txtModifyEND_DATE_ACTIVE").val($.dateFormat(data.END_DATE_ACTIVE));
        }
    });
}
