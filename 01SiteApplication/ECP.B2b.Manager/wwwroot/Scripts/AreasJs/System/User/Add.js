/********************************************
* 模块名称：Add
* 功能说明：新增页面代码操作  
* 创 建 人：LDY
* 创建时间：2017-8-18
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
    //控件校验
    $("#user-add-form").validate({
        rules: {
            USER: { required: true },
            PASSWORD: { required: true, minlength: 8, maxlength: 18  },
            USER_NAME: { required: true },
            MOBILE: { required: true, mobile: true },
            MAIL: { required: true, email: true },
            START_DATE: { required: true }
        }
    }); 
}