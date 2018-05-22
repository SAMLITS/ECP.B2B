(function ($) {
    //ERROR-错误；GEN-其它；HINT-警告；NOTE-提示；
    //formatParams [] 数组传入
    $.alert = function (msgNumber, yesCallback, formatParams,type) {
        $.onExPostReq({
            reqUrl: "/common/alertmsg",
            params: { msgNumber: msgNumber },
            onSuccess: function (response) {

                var icon = -1;
                if (response.MESSAGE_TYPE == "ERROR")
                    icon = 2;
                else if (response.MESSAGE_TYPE == "GEN")
                    icon = 5;
                else if (response.MESSAGE_TYPE == "HINT")
                    icon = 7;
                else if (response.MESSAGE_TYPE == "NOTE")
                    icon = 6;

                if (type == "msg") {
                    layerEx.msg(
                        response.MESSAGE_TEXT.format(formatParams),
                        {
                            icon: icon,
                            title: response.MESSAGE_TYPE + "-" + response.MESSAGE_NUMBER
                        }
                    );
                }
                else {
                    layerEx.alert(
                        response.MESSAGE_TEXT.format(formatParams),
                        yesCallback,
                        {
                            icon: icon,
                            title: response.MESSAGE_TYPE + "-" + response.MESSAGE_NUMBER
                        }
                    );
                }
            }
        });
    },

    /*
    * 初始化Form页面数据
    * @param {array} options    参数集合
    * formatParams:             格式化参数数组
        btn:    按钮数组    （前两个按钮的触发事件放在 yesCallback 与 noCallback 回调中）
        btn3：第三个按钮触发事件
        btn4：第四个按钮触发事件
            ...........
    */
        $.confirm = function (msgNumber, yesCallback, noCallback, options)
    {
        var defaultOpt = {
            formatParams: [], 
            btn: ['确定', '取消'],
            btn3: function () { },
            btn4: function () { },
            btn5: function () { },
            btn6: function () { },
            btn7: function () { },
        };
        if (typeof options === 'object')
            defaultOpt = $.extend(defaultOpt, options);

        $.onExPostReq({
            reqUrl: "/common/alertmsg",
            params: { msgNumber: msgNumber },
            onSuccess: function (response) {
                
                var confirmOptions = {
                    title: response.MESSAGE_TYPE + "-" + response.MESSAGE_NUMBER,
                    btn: defaultOpt.btn,
                    btn3: defaultOpt.btn3,
                    btn4: defaultOpt.btn4,
                    btn5: defaultOpt.btn5,
                    btn6: defaultOpt.btn6,
                    btn7: defaultOpt.btn7
                };


                layerEx.confirm(
                    response.MESSAGE_TEXT.format(defaultOpt.formatParams),
                    yesCallback,
                    noCallback,
                    confirmOptions
                );
            }
        });
    }
})(jQuery);