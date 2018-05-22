/********************************************
* 模块名称：Messages/List
* 功能说明：消息 列表页面js代码操作  
* 创 建 人：LDY
* 创建时间：2017-8-10
* 修 改 人：LDY
* 修改时间：2017-8-11
* ******************************************/
$(function () {

    //点击新增按钮
    $("#btnAdd").click(function () {
        Add();
    });
    //点击编辑按钮
    $("#btnModify").click(function () {
        Modify();
    });
    //点击删除按钮
    $("#btnDelete").click(function () {
        Delete();
    });
    //加载页面完成即加载网格数据
    LoadGrid();
})

/*
* 网格加载
*/
function LoadGrid() {
    $('#listdg').onExBootstrapPageQuery({
        url: '/System/Messages/List',        //请求后台的URL（*） 
        columns: [
            { field: 'MESSAGE_NUMBER', title: '消息编号', sortable: true, width: 100 },
            { field: 'MESSAGE_NAME', title: '消息代码', sortable: true, width: 100 },
            { field: 'MESSAGE_TYPE_NAME', title: '消息类型', sortable: true, width: 100 },
            { field: 'MESSAGE_TEXT', title: '消息内容', sortable: true, width: 200 },
            { field: 'MESSAGE_DESCRIPTION', title: '说明', sortable: true, width: 200 }
        ],
        sortName: "MESSAGE_NUMBER",
        sortOrder: "asc"
    });
}
/*
* 新增数据
*/
function Add() {
    $('#contentModalLayout').onExActionAdd({
        title: "消息-新增",
        reqUrl: '/System/Messages/Add',
        modalLayout: "#messages-add-form",
        reloaddg: "#listdg",
        modalWidth: $.layoutWidth2
    });
}

/*
* 编辑数据
*/
function Modify() {
    $("#contentModalLayout").onExActionEdit({
        title: "消息-编辑",
        reqUrl: "/System/Messages/Modify",
        dataUrl: "/System/Messages/FindById",
        modalLayout: "#messages-modify-form",
        reloaddg: "#listdg",
        modalWidth: $.layoutWidth2
    });
}


/*
* 删除数据
*/
function Delete() {
    $('#listdg').onExDelete("/System/Messages/Remove");
}
