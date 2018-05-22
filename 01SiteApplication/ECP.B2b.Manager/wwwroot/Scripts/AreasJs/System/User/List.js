/********************************************
* 模块名称：List
* 功能说明：用户管理页面js代码操作  
* 创 建 人：ZH
* 创建时间：2017-8-8
* 修 改 人：
* 修改时间：
* ******************************************/

$(function () {

    //新增
    $("#btnAdd").click(function () {
        Add();
    });
    //编辑
    $("#btnModify").click(function () {
        Modify();
    });
    //删除
    $("#btnDelete").click(function () {
        var rows = $('#listdg').bootstrapTable('getSelections');
        //仅当数据行“注册状态”是“待处理”时，“删除”按钮可用
        if (rows.length == 1 && rows[0].REG_STATUS != "0") return;
        $("#listdg").onExDelete("/System/User/Remove");
    });
    //菜单分配
    $("#btnAuthority").click(function () {
        Authority();
    });

    //功能分配
    $("#btnFunction").click(function () {
        Function();
    });
    //加载网格
    onloadgride();
});


function onloadgride() {
    $("#listdg").onExBootstrapPageQuery({
        url: "/System/User/List",        //请求后台的URL（*） 
        columns: [
            { field: "PARTY_NAME", title: "交易方名称", width: 180 },
            { field: "PARTY_TYPE_NAME", title: "交易方类型", width: 100 },
            { field: "USER", title: "用户账号", sortable: true, width: 100 },
            { field: "USER_NAME", title: "真实姓名", sortable: true, width: 100 },
            { field: "IS_MAIN_NAME", title: "是否主账号", width: 100 },
            { field: "REG_STATUS_NAME", title: "注册状态", width: 90 },
            { field: "WX_OPENID", title: "已绑定微信号", sortable: true, width: 115 },
            { field: "IS_WXIN_LOGIN_NAME", title: "是否强制微信登录", width: 140 },
            { field: "START_DATE", title: "生效日期", sortable: true, width: 90, formatter: $.dateFormat },
            { field: "END_DATE", title: "失效日期", sortable: true, width: 90, formatter: $.dateFormat }
        ],
        onCheck: function (row) {
            //选择的数据记录为本交易方下"已生效"且非“主账号”时，"菜单分配"&&"权限"按钮可用,否则灰显不可用
            if (row.REG_STATUS == "1" && row.IS_MAIN != "Y" && row.PARTY_ID == currentUserPartyId && row.PARTY_TYPE!="SYS") {
                $("#btnAuthority").removeAttr("disabled");
                $("#btnFunction").removeAttr("disabled");
            } else {
                $("#btnAuthority").attr("disabled", "disabled");
                $("#btnFunction").attr("disabled", "disabled");
            }
            //仅当数据行“注册状态”是“待处理”时，“删除”按钮可用
            if (row.REG_STATUS != "0") {
                $("#btnDelete").attr("disabled", "disabled");
            } else {
                $("#btnDelete").removeAttr("disabled");
            }
        },
        onUncheck: function () {
            $("#btnAuthority").removeAttr("disabled");
            $("#btnFunction").removeAttr("disabled");
            $("#btnDelete").removeAttr("disabled");
        },
    });
}

/*
* 新增数据
*/
function Add() {
    $("#contentModalLayout").onExActionAdd({
        title: "用户-新增",
        reqUrl: "/System/User/Add",
        modalLayout: "#user-add-form",
        reloaddg: "#listdg",
        modalWidth: $.layoutWidth3,
        onSuccess: function () {
        }
    });
}

/*
* 编辑数据
*/
function Modify() {
    $("#contentModalLayout").onExActionEdit({
        title: "用户-查看编辑",
        reqUrl: "/System/User/Modify",
        dataUrl: "/System/User/FindExtendById",
        submitUrl: "/System/User/ModifyDic",
        modalLayout: "#user-modify-form",
        reloaddg: "#listdg",
        modalWidth: $.layoutWidth3,
        onInitSuccess: function (data) {
            $("#txtModifyCREATION_DATE").val($.dateFormat(data.CREATION_DATE));
            $("#txtModifyPASSWORD_UPDATE_TIME").val($.dateFullFormat(data.PASSWORD_UPDATE_TIME));
            $("#txtModifySTART_DATE").val($.dateFormat(data.START_DATE));
            $("#txtModifyEND_DATE").val($.dateFormat(data.END_DATE));
            $("#pwdModifyPASSWORD").val("");
            if (data.REG_STATUS != "0") {
                //当“注册状态”为“1 - 已生效、2 - 已失效”时，“注册状态”下拉列表只显示“已生效”和“已失效”
                $("#user-modify-form [name=REG_STATUS] option[value!='1'][value!='2']").remove();
            }
        }
    });
}

/*
* 分配菜单
*/
function Authority() {
    var rows = $("#listdg").bootstrapTable("getSelections");
    if (rows.length == 1) {
        if (rows[0].REG_STATUS != "1" || rows[0].IS_MAIN == "Y" || rows[0].PARTY_ID != currentUserPartyId || row.PARTY_TYPE == "SYS") return;
        layerEx.iframeModal("/System/User/Authority?ID=" + rows[0]["ID"], "菜单分配", {
            area: ["400px", "90%"]
        });
    }
    else {
        //请选择一行数据！
        $.alert(2001);
    }
}

/*
* 分配功能
*/
function Function() {
    var rows = $("#listdg").bootstrapTable("getSelections");
    if (rows.length == 1) {
        if (rows[0].REG_STATUS != "1" || rows[0].IS_MAIN == "Y" || rows[0].PARTY_ID != currentUserPartyId || row.PARTY_TYPE == "SYS") return;
        layerEx.iframeModal("/System/User/Function?ID=" + rows[0]["ID"], "功能分配", {
            area: ["400px", "90%"]
        });
    }
    else {
        //请选择一行数据！
        $.alert(2001);
    }
}