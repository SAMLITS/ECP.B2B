/********************************************
* 模块名称：List
* 功能说明：页面js代码操作  
* 创 建 人：LTS
* 创建时间：2017-8-7
* 修 改 人：
* 修改时间：
* ******************************************/

$(function () {

    $("#btnAdd").click(function () {
        $("#contentModalLayout").onExActionAdd({
            title: "主菜单-新增",
            reqUrl: "/System/Menu/Add",
            params: { MENU_TYPE: "M" },
            modalLayout: "#menu-add-form",
            reloaddg: "#listdg",
            modalWidth: $.layoutWidth1
        });
    });

    $("#btnModify").click(function () {
        $("#contentModalLayout").onExActionEdit({
            title: "主菜单-编辑",
            reqUrl: "/System/Menu/Modify",
            dataUrl: "/System/Menu/FindExtendById",
            modalLayout: "#menu-modify-form",
            reloaddg: "#listdg",
            modalWidth: $.layoutWidth1
        });
    });

    $("#btnDelete").click(function () {
        $("#listdg").onExDelete("/System/Menu/Remove");
    });

    //展示菜单层次结构
    $("#btnShowPaths").click(function () {
        onShowPaths();
    });

    $("#listdg").onExBootstrapPageQuery({
        url: '/System/Menu/List',        //请求后台的URL（*）  
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
        ],
        onCheck: function (row) {
            $("#subfrmQueryWhere input[name='MAIN_MENU_ID']").val(row.ID);
            $("#subfrmQueryWhere input[name='MAIN_MENU_NAME']").val(row.MENU_NAME);
            $("#listdg_sub").bootstrapTable('refresh');
        },
        onUncheck: function () {
            $("#subfrmQueryWhere input[name='MAIN_MENU_ID']").val("");
            $("#subfrmQueryWhere input[name='MAIN_MENU_NAME']").val("");
            $("#listdg_sub").bootstrapTable('refresh');
        },
        onLoadSuccess: function () {
            this.onUncheck();
        }
    });
});

//查看菜单层次结构
function onShowPaths() {
    layerEx.modal('选择菜单种类', "#contentModalLayoutShowPaths", function (index) {
        //刷新页面
        layer.close(index);
        layerEx.iframeModal("/System/Menu/ShowPaths?MENU_SORT=" + $("#txtShowPathsMENU_SORT").val(), "菜单层次结构", {
            area: ["400px", "90%"]
        });
    }, function (index) {
        layer.close(index);
        }, { btn: ['查看', '返回'], width: $.layoutWidth1 });
}