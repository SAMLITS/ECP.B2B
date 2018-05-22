/********************************************
* 模块名称：List
* 功能说明：页面js代码操作  
* 创 建 人：LTS
* 创建时间：2017-8-7
* 修 改 人：
* 修改时间：
* ******************************************/

$(function () {
    $("#btnAdd_sub").click(function () {
        var rows = $("#listdg").bootstrapTable('getSelections');
        if (rows.length == 1) {
            $("#contentModalLayout_sub").onExActionAdd({
                title: "子菜单-新增",
                reqUrl: "/System/Menu/Add",
                params: {
                    MENU_TYPE: "S",
                    MAIN_MENU_ID: rows[0].ID,
                    MAIN_MENU_NAME: rows[0].MENU_NAME,
                    MENU_SORT: rows[0].MENU_SORT,
                    TERMINAL_TYPE: rows[0].TERMINAL_TYPE
                },
                modalLayout: "#menu-add-form",
                reloaddg: "#listdg_sub",
                modalWidth: $.layoutWidth1
            });
        }
        else {
            $.alert("3041");
        }

    });

    $("#btnModify_sub").click(function () {
        $("#contentModalLayout_sub").onExActionEdit({
            title: "子菜单-编辑",
            reqUrl: "/System/Menu/Modify",
            dataUrl: "/System/Menu/FindExtendById",
            modalLayout: "#menu-modify-form",
            reloaddg: "#listdg_sub",
            modalWidth: $.layoutWidth1
        });
    });

    $("#btnDelete_sub").click(function () {
        $("#listdg_sub").onExDelete("/System/Menu/Remove");
    });

    $("#btnMenuFunction").click(function () {
        var rows = $('#listdg_sub').bootstrapTable('getSelections');
        if (rows.length == 1) {
            var contentUrl = '/System/MenuFunction/List?MENU_ID=' + rows[0].ID;
            layerEx.iframeModal(contentUrl, "子菜单功能", {
                area: ['100%', '100%']
            });
        }
        else {
            $.alert(2001);
        }
    });

    $("#listdg_sub").onExBootstrapPageQuery({
        url: '/System/Menu/List',        //请求后台的URL（*） 
        toolbar: "#toolbar_sub",
        sortName: "ORDER",
        sortOrder: 'asc',
        columns: [
            { field: 'MENU_TYPE_NAME', title: '菜单类型', width: 100 },
            { field: 'MENU_CODE', title: '菜单代码', sortable: true, width: 130 },
            { field: 'MENU_NAME', title: '菜单名称', sortable: true, width: 150 },
            { field: 'ORDER', title: '序号', sortable: true, width: 80 },
            { field: 'MENU_URL', title: '菜单URL', sortable: true, width: 150 },
            { field: 'MENU_SORT_NAME', title: '菜单种类', width: 100 },
            { field: 'MAIN_MENU_NAME', title: '父层菜单', width: 150 },
            { field: 'MENU_PATH', title: '菜单路径', width: 150 },
            { field: 'TERMINAL_TYPE_NAME', title: '适用终端类别', width: 100 },
            { field: 'IS_AVAILABLE_NAME', title: '是否有效', width: 80 },
            { field: 'IS_ALLOCATED_NAME', title: '是否可分配', width: 80 }
        ]
    }, "#subfrmQueryWhere");
}); 