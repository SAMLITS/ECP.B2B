/********************************************
* 模块名称：LookUpValuesAll/List
* 功能说明：码表 列表页面js代码操作  
* 创 建 人：LDY
* 创建时间：2017-8-8
* 修 改 人：LDY
* 修改时间：2017-8-10
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
        $('#mainListGrid').onExDelete("/System/LookUpValuesAll/Remove");
    });
    //加载页面完成即加载网格数据
    LoadGrid();
})

/*
* 网格加载
*/
function LoadGrid() {
    $('#mainListGrid').onExBootstrapPageQuery({
        url: '/System/LookUpValuesAll/List',        //请求后台的URL（*） 
        columns: [
            { field: 'LOOKUP_TYPE', title: '码表代码', sortable: true, width: 150},
            { field: 'LOOKUP_TYPE_NAME', title: '码表名称', sortable: true, width: 100 },
            { field: 'LOOKUP_DESCRIPTION', title: '说明', sortable: true, width: 150 }
        ]
    });
}

/*
* 新增数据
*/
function Add() {
    $('#mainContentModalLayout').onExActionAdd({
        title: "码表-新增",
        reqUrl: '/System/LookUpValuesAll/Add',
        modalLayout: '#lookUpValuesAll-add-form ',
        reloaddg: '#mainListGrid ',
        modalWidth: $.layoutWidth1,
        onSuccess: function (response) {
            RedictModify(response.Id);
        }
    });
}

/*
* 跳转编辑数据
*/
function RedictModify(Id) {
    var contentUrl = '/System/LookUpValuesAll/Modify?ID=' + Id;
    layerEx.iframeModal(contentUrl, "码表-编辑", {
        area: ['100%', '100%']
    });
}

/*
* 编辑数据
*/
function Modify() {
    var rows = $('#mainListGrid').bootstrapTable('getSelections');
    if (rows.length == 1) {
        var contentUrl = '/System/LookUpValuesAll/Modify?ID=' + rows[0].ID;
        layerEx.iframeModal(contentUrl, "码表-编辑", {
            area: ['100%', '100%']
        });
    }
    else {
        $.alert(2001);
    }
}
