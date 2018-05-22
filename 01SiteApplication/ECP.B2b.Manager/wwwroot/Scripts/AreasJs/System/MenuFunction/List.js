/********************************************
* 模块名称：List
* 功能说明：子菜单功能/列表页面js代码操作  
* 创 建 人：LDY
* 创建时间：2018-4-20
* 修 改 人：
* 修改时间：
* ******************************************/

$(function () {
    $("#frmQueryWhere").hide();
    $("#btnAdd").click(function () {
        $("#contentModalLayout").onExActionAdd({
            title: "子菜单功能-新增",
            reqUrl: "/System/MenuFunction/Add",
            params: { MENU_ID: $("#hidMENU_ID").val() },
            modalLayout: "#menuFunction-add-form",
            reloaddg: "#listdg",
            modalWidth: $.layoutWidth1
        });
    });

    $("#btnModify").click(function () {
        $("#contentModalLayout").onExActionEdit({
            title: "子菜单功能-编辑",
            reqUrl: "/System/MenuFunction/Modify",
            dataUrl: "/System/MenuFunction/FindExtendById",
            submitUrl: "/System/MenuFunction/ModifyDic",
            modalLayout: "#menuFunction-modify-form",
            reloaddg: "#listdg",
            modalWidth: $.layoutWidth1
        });
    });

    $("#btnDelete").click(function () {
        $("#listdg").onExDelete("/System/MenuFunction/Remove");
    });

    $("#listdg").onExBootstrapPageQuery({
        url: '/System/MenuFunction/List',        //请求后台的URL（*）  
        sortName: "ID",
        sortOrder: 'desc',
        columns: [
            { field: 'MENU_NAME', title: '菜单名称', width: 150 },
            { field: 'FUNCTION_CODE', title: '功能代码', sortable: true, width: 150 },
            { field: 'FUNCTION_DESC', title: '功能说明', sortable: true, width: 150 },
            { field: 'DEFAULT_ASSIGN_NAME', title: '是否默认分配', width: 80 },
            { field: 'REMARK', title: '备注', sortable: true, width: 100 }
        ]
    });
});