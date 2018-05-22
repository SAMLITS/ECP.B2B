/********************************************
* 模块名称：Modify
* 功能说明：编辑页面代码操作  
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
    //表单校验逻辑
    $("#user-modify-form").validate({
        rules: {
            PASSWORD: {  minlength: 8, maxlength: 18  },
            CONFIRM_PASSWORD: { equalTo: "#pwdModifyPASSWORD" },
            USER_NAME: { required: true },
            MOBILE: { required: true, mobile:true},
            MAIL: { required: true, email: true },
            START_DATE: { required: true }
        },
        messages: {
            CONFIRM_PASSWORD: "两次密码输入不一致!",
        }
    });
}