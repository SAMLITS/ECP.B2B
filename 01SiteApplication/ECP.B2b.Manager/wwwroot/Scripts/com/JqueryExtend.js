(function ($) {
    $.layoutWidth3 = 1100;
    $.layoutWidth2 = 800;
    $.layoutWidth1 = 450;
    $.selectOptionDef = "<option value=''>请选择</option>";

    $.fn.rdisabled = function () {
        $(this).attr("readonly", true);
        $(this).addClass("ddisabled");
        $(this).parent().append("<div class=\"input_cover\"></div>");
    };
    $.fn.renabled = function () {
        $(this).attr("readonly", false);
        $(this).removeClass("ddisabled");
        $(this).parent().find(".input_cover").remove();
    };
    $.fn.rchange = function (func) {
        $(this).change(func);
        $(this).trigger("change");
    };
    $.fn.rval = function (val) {
        if (val == undefined)
            return $(this).val();

        $(this).val(val);
        $(this).trigger("change");
    },
        ///校验dom是否必填
        $.fn.isRequired = function () {
            var display = $("#lblQueryRequired_" + $(this).attr("name")).css("display");
            if (display == "none") {
                return false;
            }
            else {
                return true;
            }
        },
        ///校验dom是否必填并且值是否为空
        $.fn.isRequiredAndEmpty = function () {
            return $(this).isRequired() && $(this).val() == "";
        }

    $.encodeUnicode = function (str) {
        var res = [];
        for (var i = 0; i < str.length; i++) {
            res[i] = ("00" + str.charCodeAt(i).toString(16)).slice(-4);
        }
        return "\\u" + res.join("\\u");
    },
        $.decodeUnicode = function (str) {
            str = str.replace(/\\/g, "%");
            return unescape(str);
        },

        /*
        *  options Array 集合参数
            IsShowEmptyOption [bool] ： 是否需要空选项
            LookupName [string] : 码表名称  （必须）
            IsBetweenOt [bool]=false  : 是否包含超时数据
            LOOKUP_CODE_List [array]数组  ： 指定查询满足的Code
            ATTIBUTE1 [string] : 属性条件1
            ATTIBUTE2 [string] : 属性条件2
            ATTIBUTE3 [string] : 属性条件3
            ATTIBUTE4 [string] : 属性条件4
            ATTIBUTE5 [string] : 属性条件5
        
            ControlId [string] : 自动绑定控件dom
            DefaultValue [string] ： 加载成功后自动选中到的默认值
            onSuccess  [function]  ： 成功后的回调
        */
        $.GetLookupOptions = function (options) {
            var defaultOpt = {
                IsShowEmptyOption: false,
                LookupName: "",
                IsBetweenOt: false,
                LOOKUP_CODE_List: [],
                ControlId: "",
                DefaultValue: "",
                IsShowAllOption: false,
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);


            $.onExPostReq({
                reqUrl: "/System/LookUpValues/GetLookUpValuesByType",
                params: {
                    LookupName: defaultOpt.LookupName,
                    IsBetweenOt: defaultOpt.IsBetweenOt,
                    LOOKUP_CODE_List: defaultOpt.LOOKUP_CODE_List,
                    ATTIBUTE1: defaultOpt.ATTIBUTE1,
                    ATTIBUTE2: defaultOpt.ATTIBUTE2,
                    ATTIBUTE3: defaultOpt.ATTIBUTE3,
                    ATTIBUTE4: defaultOpt.ATTIBUTE4,
                    ATTIBUTE5: defaultOpt.ATTIBUTE5
                },
                onSuccess: function (res) {

                    if (defaultOpt.ControlId != "") {
                        $(defaultOpt.ControlId).html("")
                        if (defaultOpt.IsShowEmptyOption) {
                            $(defaultOpt.ControlId).append("<option value=''  d-attibute1=''  d-attibute2=''  d-attibute3=''  d-attibute4=''  d-attibute5='' d-tag='' ></option>");
                        }
                        if (defaultOpt.IsShowAllOption){
                            $(defaultOpt.ControlId).append("<option value=''  d-attibute1=''  d-attibute2=''  d-attibute3=''  d-attibute4=''  d-attibute5='' d-tag='' >ALL</option>");
                        }
                        $.each(res, function (index, item) {
                            $(defaultOpt.ControlId).append("<option value='" + item.LOOKUP_CODE + "'  d-attibute1='" + item.ATTIBUTE1 + "'  d-attibute2='" + item.ATTIBUTE2 + "'  d-attibute3='" + item.ATTIBUTE3 + "'  d-attibute4='" + item.ATTIBUTE4 + "'  d-attibute5='" + item.ATTIBUTE5 + "'  d-tag='" + item.TAG + "'  >" + item.LOOKUP_MEANING + "</option>");
                        });

                        //选中默认值
                        $(defaultOpt.ControlId).rval(defaultOpt.DefaultValue);
                    }

                    if (typeof defaultOpt.onSuccess === 'function')
                        defaultOpt.onSuccess(res);
                }
            });
        },

        //获取 json 数量长度
        $.getJsonLength = function (jsonObj) {
            var jsonLength = 0;
            for (var i in jsonObj) {
                jsonLength++;
            }
            return jsonLength;
        },

        //json 转 url 字符串
        $.parseParam = function (param, key) {

            var paramStr = "";

            if (param instanceof String || param instanceof Number || param instanceof Boolean) {

                paramStr += "&" + key + "=" + encodeURIComponent(param);

            } else {

                $.each(param, function (i) {

                    vark = key == null ? i : key + (param instanceof Array ? "[" + i + "]" : "." + i);

                    paramStr += '&' + $.parseParam(this, vark);

                });

            }
            return paramStr.substr(1);
        },
        $.isJSON = function (str) {
            if (typeof str === "object") return true;

            if (!typeof str === "string") return false;

            str = str.replace(/\s/g, '').replace(/\n|\r/, '');

            if (/^\{(.*?)\}$/.test(str))
                return /"(.*?)":(.*?)/g.test(str);

            if (/^\[(.*?)\]$/.test(str)) {
                return str.replace(/^\[/, '')
                    .replace(/\]$/, '')
                    .replace(/},{/g, '}\n{')
                    .split(/\n/)
                    .map(function (s) { return isJSON(s); })
                    .reduce(function (prev, curr) { return !!curr; });
            }

            return false;
        }
})(jQuery);

String.prototype.format = function () {
    if (arguments == undefined || arguments == null || arguments.length == 0) return this;
    for (var s = this, i = 0; i < arguments.length; i++)
        s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
    return s;
};