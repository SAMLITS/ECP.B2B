/********************************************
* 模块名称：PersonalSet
* 功能说明：用户个人设置页面js代码操作  
* 创 建 人：LDY
* 创建时间：2017-9-20
* 修 改 人：
* 修改时间：
* ******************************************/
$(function () {
    //初始化表单
    InnitForm();
    //加载表单数据
    LoadFormData();
    //密码更改
    $("#btnSetChangePwd").click(function () {
        ChangePwd();
    });

    //保存
    $("#btnSetSave").click(function () {
        $("#user-personalset-form").onExPostForm({
            submitUrl: "/System/User/ModifyDic"
        });
    });
});

/*
* 初始化表单
*/
function InnitForm() {
    //主表单校验规则
    $("#user-personalset-form").validate({
        rules: {
            USER_NAME: { required: true },
            MOBILE: { required: true, mobile: true },
            MAIL: { required: true, email: true },
            PW_QUESTION: { required: true },
            PW_ANSWER: { required: true }
        }
    });

    //密码修改表单校验规则处理
    $("#user-changePwd-form").validate({
        rules: {
            ORIGINAL_PASSWORD: { required: true },
            PASSWORD: { required: true, regexHardPassword: true, minlength: 8, maxlength: 18 },
            CONFIRM_PASSWORD: { required: true, equalTo: '#pwdChangePwd_PASSWORD' },

        },
        messages: {
            CONFIRM_PASSWORD: { equalTo: "新密码前后输入不一致！" }
        }
    });
}

/*
* 加载表单数据
*/
function LoadFormData() {
    $("#user-personalset-form").onExInitDataForm({
        dataUrl: "/System/User/FindExtendById",
        params: { ID: $("#hidSetID").val() },
        onSuccess: function (data) {
            //时间日期格式化
            $("#txtSetPASSWORD_UPDATE_TIME").val($.dateFullFormat(data.PASSWORD_UPDATE_TIME));
            $("#txtSetLAST_LOGIN_DATE").val($.dateFullFormat(data.LAST_LOGIN_DATE));
            //若当前账号未绑定微信，则“已关联微信”灰显为“N-否”且不可更改，“是否强制微信登录”字段灰显为“N-否”且不可更改，按钮显示为“绑定微信”且可用；
            $("#selSetIS_WXIN_LOGIN").val("N");
            if (data.IS_BIND_WXIN == "N") {
                $("#selSetIS_WXIN_LOGIN").rdisabled();
                $("#btnSetBindWeixin").click(function () {
                    //绑定微信
                    BindWx();
                });
            } else {
                //若当前账号已绑定微信，则“是否强制微信登录”字段默认为“N - 否”可编辑更改从码表YES_NO取值，按钮显示为“解绑微信”且可用
                $("#selSetIS_WXIN_LOGIN").renabled();
                $("#btnSetBindWeixin").html("解绑微信");
                //解绑微信
                $("#btnSetBindWeixin").click(function () {
                    UnBindWx();
                });
            }
        }
    });
}

/*
* 密码更改
*/
function ChangePwd() {
    //重置更改文本框
    $("#pwdChangePwd_ORIGINAL_PASSWORD").val("");
    $("#pwdChangePwd_PASSWORD").val("");
    $("#pwdChangePwd_CONFIRM_PASSWORD").val("");
    var changePwdIndex = layerEx.modal(
        "更改密码",
        "#contentModalLayout_ChangePwd",
        function () {
            if (!$("#user-changePwd-form").valid()) return;
            $("#user-changePwd-form").onExPostForm({
                submitUrl: "/System/User/ChangePwd",
                onSuccess: function (response) {
                    //重置更改文本框
                    $("#pwdChangePwd_ORIGINAL_PASSWORD").val("");
                    $("#pwdChangePwd_PASSWORD").val("");
                    $("#pwdChangePwd_CONFIRM_PASSWORD").val("");
                }
            })
        },
        function (index) {

        },
        {
            width: $.layoutWidth1
        });
}

/*
* 绑定微信
*/
function BindWx() {

}

/*
* 解绑微信
*/
function UnBindWx() {
    $.onExPostReq({
        reqUrl: "/System/User/UnBindWx",
        params: { USER_ID: $("#hidSetID").val() },
        onSuccess: function (response) {
            if (response.Result == "1") {
                $.alert(response.NumberMsg, function () {
                    window.location.reload();
                });
            } else {
                $.alert(response.NumberMsg);
            }
        }
    });
}