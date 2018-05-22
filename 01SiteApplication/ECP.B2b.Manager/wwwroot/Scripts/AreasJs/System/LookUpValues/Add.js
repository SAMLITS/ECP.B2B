/********************************************
* 模块名称：Add
* 功能说明：新增页面代码操作  
* 创 建 人：LDY
* 创建时间：2017-8-25
* 修 改 人：
* 修改时间：
* ******************************************/
$(function () {
    InnitForm();
})

/*
*表单初始化
*/
function InnitForm() {
    //设置必填字段前端校验
    $('#lookUpValues-add-form').validate({
        rules: {
            //值代码
            LOOKUP_CODE: { required: true },
            //值名称
            LOOKUP_MEANING: { required: true },
            //启用标识
            START_DATE_ACTIVE: { required: true }
        }
    });
}