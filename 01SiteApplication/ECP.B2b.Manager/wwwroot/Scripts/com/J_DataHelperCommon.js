/********************************************
* 模块名称：J_DataHelperCommon
* 功能说明：公共增删改查、分页操作  
* 创 建 人：xinyang
* 创建时间：2016-12-17
* 修 改 人：
* 修改时间：
* ******************************************/
(function ($) {

    /*返回结果是否有权限操作*/
    $.ReqAuthorityValid = function (response) {
        if ($.isJSON(response)) {
            var resJson;
            if (typeof response === "object")
                resJson = response;
            else
                resJson = JSON.parse(response);
            if (resJson.Result == "4") {
                //无权限访问
                if (resJson.NumberMsg != 0) {
                    $.alert(resJson.NumberMsg, null, null);
                }
                else {
                    $.alert(3130, null, null);
                }
                return false;
            }
        }

        return true;
    },
        /*UI控件自动赋值*/
        $.SetFormValueUI = function (rowdata, formStr) {
            formStr = $(formStr);
            for (var name in rowdata) {
                var control = $(formStr.selector + ' [name=' + name + ']');
                if (control.length > 0) {
                    var l_data_format = control.attr("l_data_format");
                    var dataVal = rowdata[name];
                    switch (l_data_format) {
                        case "money":
                            dataVal = $.parseMoney(dataVal);
                            break;
                        case "weight":
                            dataVal = $.parseMoney(dataVal);
                            break;
                        case "volume":
                            dataVal = $.parseMoney(dataVal);
                            break;
                        default:
                            dataVal = rowdata[name];
                            break;
                    }
                    $(control).val(dataVal);
                }
            }
        };
    $.onSubmitBeforeRun = function (defaultOpt, onSubmitFormAction) {
        if (defaultOpt.submitBeforeAlertNumber) {

            if (defaultOpt.onAlertBeforeCallback()) {
                $.alert(defaultOpt.submitBeforeAlertNumber, function () {
                    if (typeof defaultOpt.onAlertOkCallback === "function") {
                        //执行点击确认后的回调方法
                        defaultOpt.onAlertOkCallback();
                    }
                    onSubmitFormAction(); //执行提交
                }, undefined, "alert");
            }
            else {
                onSubmitFormAction(); //执行提交
            }
        }
        else if (defaultOpt.submitBeforeConfirmNumber) {
            if (defaultOpt.onConfirmBeforeCallback()) {
                $.confirm(defaultOpt.submitBeforeConfirmNumber, function () {
                    //确认回调
                    if (typeof defaultOpt.onConfirmOkCallback === "function") {
                        //执行点击确认后的回调方法
                        defaultOpt.onConfirmOkCallback();
                    }

                    onSubmitFormAction(); //执行提交

                }, function () {
                    //取消回调
                    if (typeof defaultOpt.onConfirmNoCallback === "function") {
                        //执行点击取消后的回调方法
                        defaultOpt.onConfirmNoCallback();
                    }
                });
            }
            else {
                onSubmitFormAction(); //执行提交
            }
        }
        else {
            onSubmitFormAction(); //执行提交
        }
    }

    $.fn.extend({
        /*
         * 同页面form数据新增保存    在新增form处调用
         * @param {object} options 参数集合
         *        {string} title 弹窗标题
         *        {int} modalWidth 弹出窗体宽度
         *        {string} submitUrl 提交Url           必须
         *        {function} onSuccess 保存成功回调
         *        {function} onFailed 保存失败回调
         *        {function} onCancel 取消按钮回调
         *        {string}  reloaddg：数据控件dom    必须
         *        {bool} IsResetForm 是否重置表单   默认 true

         *        {function}   onAlertBeforeCallback     弹出“提示”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeAlertNumber   提交前弹出的消息“提示”编码，点击确定后才会执行提交
         *        {function}   onAlertOkCallback         消息“提示”点击确定过后的回调，回调执行完过后执行提交

         *        {function}   onConfirmBeforeCallback   弹出消息“确认”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeConfirmNumber 提交前弹出的消息“确认”编码，点击确定后才会执行提交，点击取消则放弃提交
         *        {function}   onConfirmOkCallback       消息“确认”点击确定过后的回调，回调执行完过后执行提交
         *        {function}   onConfirmNoCallback       消息“确认”点击取消过后的回调
         *        {function} onSubmitBeforeValid    保存之前校验函数，返回false则校验不通过，停止执行提交，返回true则校验通过继续执行
         *        {function} onSubmitBeforeInit     提交保存之前初始化函数，返回false则停止执行，返回true则继续执行
         *
         *        {object} alertOpt 关于alert消息弹出配置参数
                         {function} onSuccessAlert      操作成功弹出消息
                         {function} onFailedAlert       操作错误弹出消息
         *
         *
         * @param {object} layerModelOptions    弹出layer参数对象

         */
        onExPartialAdd: function (options, layerModelOptions) {
            var contentLayout = "#" + $(this).attr("id");
            var defaultOpt = {
                submitUrl: "",
                title: "新增",
                IsResetForm: true,
                onAlertBeforeCallback: function () {
                    return true;
                },
                onConfirmBeforeCallback: function () {
                    return true;
                },
                onSubmitBeforeValid: function () {
                    return true;
                },
                onSubmitBeforeInit: function () {
                    return true;
                },
                alertOpt: {}
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            var formStr = "#" + $(this).attr("id");
            if (defaultOpt.IsResetForm) {
                $(formStr)[0].reset();
                $(formStr).find("input").val(""); //清空重置表单
            }


            var defaultLayerOpt = {
                width: defaultOpt.modalWidth
            };
            if (typeof layerModelOptions === 'object')
                defaultLayerOpt = $.extend(defaultLayerOpt, layerModelOptions);

            var layerIndex = layerEx.modal(defaultOpt.title, formStr, function () {

                if (defaultOpt.onSubmitBeforeInit()) {

                    if (!$(formStr).valid() || !defaultOpt.onSubmitBeforeValid()) return;
                    //执行提交
                    function onSubmitFormAction() {


                        $.onExPostReq({
                            reqUrl: defaultOpt.submitUrl,
                            params: $(formStr).serializeJSON(),
                            onSuccess: function (response) {
                                if (response.Result == "1") {
                                    layer.close(layerIndex);

                                    if (typeof defaultOpt.alertOpt.onSuccessAlert === 'function') {
                                        defaultOpt.alertOpt.onSuccessAlert(response);
                                    }
                                    else {
                                        $.alert(response.NumberMsg, null, null, "msg");
                                    }

                                    $(defaultOpt.reloaddg).bootstrapTable('refresh');
                                    if (typeof defaultOpt.onSuccess === 'function') {
                                        defaultOpt.onSuccess(response);
                                    }
                                }
                                else {
                                    if (typeof defaultOpt.alertOpt.onFailedAlert === 'function') {
                                        defaultOpt.alertOpt.onFailedAlert(response);
                                    }
                                    else {
                                        $.alert(response.NumberMsg, null, null);
                                    }

                                    if (typeof defaultOpt.onFailed === 'function') {
                                        defaultOpt.onFailed(response);
                                    }
                                }
                            }
                        });
                    }

                    $.onSubmitBeforeRun(defaultOpt, onSubmitFormAction);
                }
            }, function (index) {
                layer.close(index);
                if (typeof defaultOpt.onCancel === 'function') {
                    defaultOpt.onCancel();
                }
            }, defaultLayerOpt);
            //防止高度过高出现y轴滚动条时宽度需要进行调整
            //$("#layui-layer" + layerIndex).width($("#layui-layer" + layerIndex).width() + 30);
        },
        /*
         * 同页面form数据编辑保存    在编辑form处调用
         * @param {array} options 参数集合
         *        {string} idValue 数据id      必须
         *        {string} title 弹窗标题
         *        {string} initUrl 初始化Url   必须
                  {int}    modalWidth 弹窗宽度 
         *        {string} submitUrl 提交Url   必须
         *        {function} onSuccess  保存成功后回调方法
         *        {function} onInitSuccess  数据初始化成功后回调方法
         *        {string}  reloaddg：数据控件dom    必须

         *        {function}   onAlertBeforeCallback     弹出“提示”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeAlertNumber   提交前弹出的消息“提示”编码，点击确定后才会执行提交
         *        {function}   onAlertOkCallback         消息“提示”点击确定过后的回调，回调执行完过后执行提交

         *        {function}   onConfirmBeforeCallback   弹出消息“确认”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeConfirmNumber 提交前弹出的消息“确认”编码，点击确定后才会执行提交，点击取消则放弃提交
         *        {function}   onConfirmOkCallback       消息“确认”点击确定过后的回调，回调执行完过后执行提交
         *        {function}   onConfirmNoCallback       消息“确认”点击取消过后的回调
         *        {function} onSubmitBeforeValid    保存之前校验函数，返回false则校验不通过，停止执行提交，返回true则校验通过继续执行
         *        {function} onSubmitBeforeInit     提交保存之前初始化函数，返回false则停止执行，返回true则继续执行
         *

         *        {object} alertOpt 关于alert消息弹出配置参数
                         {function} onSuccessAlert      操作成功弹出消息
                         {function} onFailedAlert       操作错误弹出消息

         * @param {array} layerModelOptions    弹出layer参数对象
         */
        onExPartialEdit: function (options, layerModelOptions) {
            var contentLayout = "#" + $(this).attr("id");
            var defaultOpt = {
                idValue: "",
                title: "编辑",
                initUrl: "",
                submitUrl: "",
                onSuccess: null,
                onInitSuccess: null,
                onAlertBeforeCallback: function () {
                    return true;
                },
                onConfirmBeforeCallback: function () {
                    return true;
                },
                onSubmitBeforeValid: function () {
                    return true;
                },
                onSubmitBeforeInit: function () {
                    return true;
                },
                alertOpt: {}
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);


            var formStr = "#" + $(this).attr("id");


            $.onExPostReq({
                reqUrl: defaultOpt.initUrl,
                params: { "ID": defaultOpt.idValue },
                onSuccess: function (response) {
                    var rowdata = response.RetValue;
                    //赋值
                    $.SetFormValueUI(rowdata, formStr);

                    if (defaultOpt.onInitSuccess != null)
                        defaultOpt.onInitSuccess(rowdata);

                    var defaultLayerOpt = {
                        width: defaultOpt.modalWidth
                    };
                    if (typeof layerModelOptions === 'object')
                        defaultLayerOpt = $.extend(defaultLayerOpt, layerModelOptions);

                    var layerIndex = layerEx.modal(defaultOpt.title, formStr, function () {

                        if (defaultOpt.onSubmitBeforeInit()) {
                            if (!$(formStr).valid() || !defaultOpt.onSubmitBeforeValid()) return;
                            //执行提交
                            function onSubmitFormAction() {

                                $.onExPostReq({
                                    reqUrl: defaultOpt.submitUrl,
                                    params: $(formStr).serializeJSON(),
                                    onSuccess: function (response) {
                                        if (response.Result == "1") {
                                            layer.close(layerIndex);

                                            if (typeof defaultOpt.alertOpt.onSuccessAlert === 'function') {
                                                defaultOpt.alertOpt.onSuccessAlert(response);
                                            }
                                            else {
                                                $.alert(response.NumberMsg, null, null, "msg");
                                            }


                                            if (defaultOpt.onSuccess != null) {
                                                defaultOpt.onSuccess(response);  //成功回调方法
                                            }
                                            else {
                                                $(defaultOpt.reloaddg).bootstrapTable('refresh');
                                            }
                                        }
                                        else {
                                            if (typeof defaultOpt.alertOpt.onFailedAlert === 'function') {
                                                defaultOpt.alertOpt.onFailedAlert(response);
                                            }
                                            else {
                                                $.alert(response.NumberMsg, null, null);
                                            }
                                        }
                                    }
                                });
                            }

                            $.onSubmitBeforeRun(defaultOpt, onSubmitFormAction);
                        }

                    }, function (index) {
                        layer.close(index);
                    }, defaultLayerOpt);

                },
                onError: function () {
                    //初始化时出现未知异常！
                    $.alert(3015, null, null);
                }
            });
        },
        /*
        * 同页面form数据详细    在编辑form处调用
        * @param {string} idValue 数据id
        * @param {string} title 弹窗标题
        * @param {string} initUrl 初始化Url   
        */
        onExPartialShow: function (idValue, title, initUrl) {
            var formStr = "#" + $(this).attr("id");

            $.onExPostReq({
                reqUrl: initUrl,
                params: { "ID": idValue },
                onSuccess: function (response) {
                    var rowdata = response.RetValue;
                    //赋值
                    $.SetFormValueUI(rowdata, formStr);
                    var layerIndex = layerEx.modal(title, formStr, undefined, undefined, { btn: ['取消'] });
                },
                onError: function () {
                    //初始化时出现未知异常！
                    $.alert(3015, null, null);
                }
            });
        },

        /*
        * 数据删除   在删除按钮处调用
        $(reloaddg).onExDelete();//reloaddg为bootstrapTable dom
         
        * @param {string} submitUrl 提交Url
        */
        onExDelete: function (submitUrl) {
            var that = this;
            var rows = $(that).bootstrapTable('getSelections');
            if (rows.length == 1) {
                //是否确定删除?
                $.confirm(3016, function () {

                    $.onExPostReq({
                        reqUrl: submitUrl,
                        params: { ID: rows[0]["ID"] },
                        onSuccess: function (respone) {
                            if (respone.Result == "1") {
                                $.alert(respone.NumberMsg, null, null, "msg");
                                $(that).bootstrapTable('refresh');
                            }
                            else {
                                $.alert(respone.NumberMsg, null, null);
                            }
                        }
                    });
                });
            }
            else {
                //请选择一行数据！
                $.alert(2001, null, null);
            }

        },

        /*
        * Action新页面form数据新增保存  在内容区域调用

        * @param {array} options    参数集合
        * reqUrl：                  页面url                    必须
        * params：                  页面参数               
        * modalLayout：             弹出form面板id，需加#      必须
        * modalWidth：              弹出窗体宽度
        * submitUrl：               保存Url                默认等于reqUrl
        * title：                   弹窗标题
        * reloaddg：                数据控件dom    必须

         *        {function}   onAlertBeforeCallback     弹出“提示”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeAlertNumber   提交前弹出的消息“提示”编码，点击确定后才会执行提交
         *        {function}   onAlertOkCallback         消息“提示”点击确定过后的回调，回调执行完过后执行提交

         *        {function}   onConfirmBeforeCallback   弹出消息“确认”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeConfirmNumber 提交前弹出的消息“确认”编码，点击确定后才会执行提交，点击取消则放弃提交
         *        {function}   onConfirmOkCallback       消息“确认”点击确定过后的回调，回调执行完过后执行提交
         *        {function}   onConfirmNoCallback       消息“确认”点击取消过后的回调

         *        {function} onSubmitBeforeValid    保存之前校验函数，返回false则校验不通过，停止执行提交，返回true则校验通过继续执行
         *        {function} onSubmitBeforeInit     提交保存之前初始化函数，返回false则停止执行，返回true则继续执行
         *
         *        {object} alertOpt 关于alert消息弹出配置参数
                         {function} onSuccessAlert      操作成功弹出消息
                         {function} onFailedAlert       操作错误弹出消息


        * @param {array} layerModelOptions    弹出layer参数对象

        */
        onExActionAdd: function (options, layerModelOptions) {


            try {
                var contentLayout = "#" + $(this).attr("id");
                $(contentLayout).html("");
                var defaultOpt = {
                    reqUrl: "",
                    params: {},
                    modalLayout: "#xxx",
                    submitUrl: "",
                    title: "新增",
                    reloaddg: "#xxx",
                    onAlertBeforeCallback: function () {
                        return true;
                    },
                    onConfirmBeforeCallback: function () {
                        return true;
                    },
                    onSubmitBeforeValid: function () {
                        return true;
                    },
                    onSubmitBeforeInit: function () {
                        return true;
                    },
                    alertOpt: {}
                };
                if (typeof options === 'object')
                    defaultOpt = $.extend(defaultOpt, options);
                if (defaultOpt.submitUrl == "")
                    defaultOpt.submitUrl = defaultOpt.reqUrl;

                $.onExGetReq({
                    reqUrl: defaultOpt.reqUrl,
                    params: defaultOpt.params,
                    dataType: 'html',
                    onSuccess: function (html) {
                        $(contentLayout).html(html);

                        //更新页面样式布局
                        if (typeof (onInitTableFormStyle) === 'function') {
                            onInitTableFormStyle();
                        }

                        $(defaultOpt.modalLayout).onExPartialAdd(
                            {
                                submitUrl: defaultOpt.submitUrl,
                                title: defaultOpt.title,
                                modalWidth: defaultOpt.modalWidth,
                                reloaddg: defaultOpt.reloaddg,
                                onSuccess: function (response) {
                                    $(contentLayout).html("");
                                    //onExPartialAdd 中以弹出消息与刷新
                                    //layerEx.successMsg(response.PromptMsg);
                                    //$(defaultOpt.reloaddg).bootstrapTable('refresh');

                                    if (typeof defaultOpt.onSuccess === 'function') {
                                        defaultOpt.onSuccess(response);
                                    }
                                },
                                onCancel: function (index) {
                                    $(contentLayout).html("");
                                },
                                IsResetForm: false,  //Action的方式进去不需要重置表单，重置的话反而会丢掉表单内的默认值

                                submitBeforeAlertNumber: defaultOpt.submitBeforeAlertNumber,
                                onAlertOkCallback: defaultOpt.onAlertOkCallback,
                                submitBeforeConfirmNumber: defaultOpt.submitBeforeConfirmNumber,
                                onConfirmOkCallback: defaultOpt.onConfirmOkCallback,
                                onConfirmNoCallback: defaultOpt.onConfirmNoCallback,
                                onAlertBeforeCallback: defaultOpt.onAlertBeforeCallback,
                                onConfirmBeforeCallback: defaultOpt.onConfirmBeforeCallback,
                                onSubmitBeforeValid: defaultOpt.onSubmitBeforeValid,
                                alertOpt: defaultOpt.alertOpt,
                                onSubmitBeforeInit: defaultOpt.onSubmitBeforeInit
                            }, layerModelOptions);
                    },
                    onError: function () {
                        //出现未知异常
                        $.alert(3014, null, null);
                    }
                });
            }
            catch (e) {
                //初始化时出现未知异常！
                $.alert(3015, null, null);
            }
        },

        /*
        * Action新页面form数据编辑保存  在内容区域调用

        * @param {array} options    参数集合
        * reqUrl：                  页面url                    必须
        * dataUrl:                  页面初始化数据url           必须
        * params：                  页面参数                   必须至少Id
        *        {function} onSuccess 保存成功回调
        *        {function} onFailed 保存失败回调
        *        {function} onCancel 取消按钮回调
        * modalLayout：             弹出form面板id，需加#       必须
        * modalWidth：              弹出窗体宽度
        * submitUrl：               保存Url                   默认等于reqUrl
        * title：                   弹窗标题
        * reloaddg：                数据控件dom    必须
        * isNotSelectedRow:         是否不需要选中行 
        * onInitSuccess：           数据初始化成功后的回调方法

         *        {function}   onAlertBeforeCallback     弹出“提示”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeAlertNumber   提交前弹出的消息“提示”编码，点击确定后才会执行提交
         *        {function}   onAlertOkCallback         消息“提示”点击确定过后的回调，回调执行完过后执行提交

         *        {function}   onConfirmBeforeCallback   弹出消息“确认”之前回调方法，返回 false 则不弹出后续消息 直接执行提交
         *        {int/string} submitBeforeConfirmNumber 提交前弹出的消息“确认”编码，点击确定后才会执行提交，点击取消则放弃提交
         *        {function}   onConfirmOkCallback       消息“确认”点击确定过后的回调，回调执行完过后执行提交
         *        {function}   onConfirmNoCallback       消息“确认”点击取消过后的回调
         *        {function} onSubmitBeforeValid    保存之前校验函数，返回false则校验不通过，停止执行提交，返回true则校验通过继续执行
         *        {function} onSubmitBeforeInit     提交保存之前初始化函数，返回false则停止执行，返回true则继续执行
         *
         *        {object} alertOpt 关于alert消息弹出配置参数
                         {function} onSuccessAlert      操作成功弹出消息
                         {function} onFailedAlert       操作错误弹出消息


        * @param {array} layerModelOptions    弹出layer参数对象

        */
        onExActionEdit: function (options, layerModelOptions) {

            var rows = $(options.reloaddg).bootstrapTable('getSelections');
            if (rows.length == 1 || options.isNotSelectedRow) {
                var ID = "";
                if (options.isNotSelectedRow) {
                    ID = options.params.EDIT_ID;
                }
                else if (rows.length == 1) {
                    ID = rows[0]["ID"];
                }
                if (!ID) {
                    $.alert("3098");
                    return;
                }

                try {

                    var contentLayout = "#" + $(this).attr("id");
                    $(contentLayout).html("");
                    var defaultOpt = {
                        reqUrl: "",
                        params: {},
                        modalLayout: "#xxx",
                        submitUrl: "",
                        title: "编辑",
                        onAlertBeforeCallback: function () {
                            return true;
                        },
                        onConfirmBeforeCallback: function () {
                            return true;
                        },
                        onSubmitBeforeValid: function () {
                            return true;
                        },
                        onSubmitBeforeInit: function () {
                            return true;
                        },
                        alertOpt: {}
                    };
                    if (typeof options === 'object')
                        defaultOpt = $.extend(defaultOpt, options);
                    if (defaultOpt.submitUrl == "")
                        defaultOpt.submitUrl = defaultOpt.reqUrl;

                    $.onExGetReq({
                        reqUrl: defaultOpt.reqUrl,
                        params: defaultOpt.params,
                        dataType: 'html',
                        onSuccess: function (html) {

                            $.onExPostReq({
                                reqUrl: defaultOpt.dataUrl,
                                params: { ID: ID },
                                onSuccess: function (data) {

                                    $(contentLayout).html(html);

                                    //更新页面样式布局
                                    if (typeof (onInitTableFormStyle) === 'function') {
                                        onInitTableFormStyle();
                                    }

                                    //赋值
                                    $.SetFormValueUI(data, defaultOpt.modalLayout);

                                    if (typeof defaultOpt.onInitSuccess == 'function')
                                        defaultOpt.onInitSuccess(data);


                                    var defaultLayerOpt = {
                                        width: defaultOpt.modalWidth
                                    };
                                    if (typeof layerModelOptions === 'object')
                                        defaultLayerOpt = $.extend(defaultLayerOpt, layerModelOptions);



                                    var formStr = defaultOpt.modalLayout;
                                    var layerIndex = layerEx.modal(defaultOpt.title, formStr, function () {

                                        if (defaultOpt.onSubmitBeforeInit()) {
                                            if (!$(formStr).valid() || !defaultOpt.onSubmitBeforeValid()) return;

                                            //执行提交
                                            function onSubmitFormAction() {
                                                $.onExPostReq({
                                                    reqUrl: defaultOpt.submitUrl,
                                                    params: $(formStr).serializeJSON(),
                                                    onSuccess: function (response) {

                                                        if (response.Result == "1") {
                                                            layer.close(layerIndex);
                                                            $(contentLayout).html("");

                                                            if (typeof defaultOpt.alertOpt.onSuccessAlert === 'function') {
                                                                defaultOpt.alertOpt.onSuccessAlert(response);
                                                            }
                                                            else {
                                                                $.alert(response.NumberMsg, null, null, "msg");
                                                            }

                                                            $(defaultOpt.reloaddg).bootstrapTable('refresh');

                                                            if (typeof defaultOpt.onSuccess === 'function') {
                                                                defaultOpt.onSuccess(response);
                                                            }
                                                        }
                                                        else {
                                                            if (typeof defaultOpt.alertOpt.onFailedAlert === 'function') {
                                                                defaultOpt.alertOpt.onFailedAlert(response, layerIndex);
                                                            }
                                                            else {
                                                                $.alert(response.NumberMsg, null, null);
                                                            }

                                                            if (typeof defaultOpt.onFailed === 'function') {
                                                                defaultOpt.onFailed(response);
                                                            }
                                                        }
                                                    }
                                                });
                                            }

                                            $.onSubmitBeforeRun(defaultOpt, onSubmitFormAction);
                                        }

                                    }, function (index) {
                                        layer.close(index);
                                        $(contentLayout).html("");

                                        if (typeof defaultOpt.onCancel === 'function') {
                                            defaultOpt.onCancel();
                                        }

                                    }, defaultLayerOpt);
                                },
                                onError: function () {
                                    //初始化数据时出现未知异常！
                                    $.alert(3017, null, null);
                                }
                            });
                        },
                        onError: function () {
                            //打开界面时出现未知异常！
                            $.alert(3018, null, null);
                        }
                    });
                }
                catch (e) {
                    //初始化时出现未知异常！
                    $.alert(3015, null, null);
                }
            }
            else {
                //请选择一行数据！
                $.alert(2001, null, null);
            }
        },
        /*
       * Action新页面form数据详细显示  在内容区域调用

       * @param {array} options    参数集合
       * reqUrl：                  页面url                     必须
       * dataUrl:                  页面初始化数据url            必须
       * params：                  页面参数                    必须至少Id
       * modalLayout：             弹出form面板id，需加#        必须
       * reloaddg：                数据控件dom                 必须
       * isNotSelectedRow:         是否不需要选中行
       * title：                   弹窗标题
       * modalWidth：              弹出窗体宽度
       * onInitSuccess:            数据初始化成功回调
       * onInitError:              数据初始化错误回调
       * onAlertSuccessEnd:        完全弹出后的回调

       * @param {array} layerModelOptions    弹出layer参数对象

       */
        onExActionShow: function (options, layerModelOptions) {

            var rows = $(options.reloaddg).bootstrapTable('getSelections');
            if (rows.length == 1 || options.isNotSelectedRow) {
                var ID = "";
                if (options.isNotSelectedRow) {
                    ID = options.params.EDIT_ID;
                }
                else if (rows.length == 1) {
                    ID = rows[0]["ID"];
                }
                if (!ID) {
                    $.alert("3098");
                    return;
                }

                try {

                    var contentLayout = "#" + $(this).attr("id");
                    $(contentLayout).html("");

                    var defaultOpt = {
                        reqUrl: "",
                        params: {},
                        modalLayout: "#xxx",
                        title: "查看",
                    };
                    if (typeof options === 'object')
                        defaultOpt = $.extend(defaultOpt, options);

                    var url = defaultOpt.reqUrl + "?ID=" + ID;

                    $.onExGetReq({
                        reqUrl: url,
                        params: defaultOpt.params,
                        dataType: 'html',
                        onSuccess: function (html) {
                            //初始化数据
                            $(defaultOpt.modalLayout).onExInitDataForm({
                                dataUrl: defaultOpt.dataUrl,
                                params: { ID: ID },
                                onSuccessBindBefore: function (data) {
                                    $(contentLayout).html(html);
                                },
                                onSuccess: function (data) {
                                    if (typeof defaultOpt.onInitSuccess == 'function')
                                        defaultOpt.onInitSuccess(data);

                                    for (var name in data) {
                                        if ($(defaultOpt.modalLayout).find('[name=' + name + ']').length > 0) {
                                            $(defaultOpt.modalLayout).find('[name=' + name + ']').attr("disabled", "disabled");
                                            //$(defaultOpt.modalLayout + ' [name=' + name + ']').val(data[name]);
                                        }
                                    }

                                    var defaultLayerOpt = {
                                        btn: ["返回"],  //只显示一个返回按钮
                                        width: defaultOpt.modalWidth,
                                        cancel: function (index)
                                        {
                                            layer.close(index);
                                            $(contentLayout).html("");
                                        }
                                    };
                                    if (typeof layerModelOptions === 'object')
                                        defaultLayerOpt = $.extend(defaultLayerOpt, layerModelOptions);


                                    //隐藏过后再弹出  否则隐藏部分高度也会算进去
                                    $(defaultOpt.modalLayout).find(".btnModalLayout").hide();
                                    var layerIndex = layerEx.modal(defaultOpt.title, defaultOpt.modalLayout,
                                        undefined,
                                        undefined, defaultLayerOpt);

                                    //将 btnModalLayout 区域内的按钮放到 modal 下面
                                    $(defaultOpt.modalLayout).find(".btnModalLayout a").removeClass("btn");
                                    $(defaultOpt.modalLayout).find(".btnModalLayout a").addClass("layui-layer-btn10");
                                    $(defaultOpt.modalLayout).find(".btnModalLayout a").prependTo($("#layui-layer" + layerIndex).find('.layui-layer-btn'));
                                    $("#layui-layer" + layerIndex).find('.layui-layer-btn .layui-layer-btn0').click(function () {
                                        layer.close(layerIndex);
                                        $(contentLayout).html("");
                                    });


                                    if (typeof defaultOpt.onAlertSuccessEnd == 'function')
                                        defaultOpt.onAlertSuccessEnd(data, layerIndex);
                                },
                                onError: defaultOpt.onInitError
                            })
                        }
                    });
                }
                catch (e) {
                    //初始化时出现未知异常！
                    $.alert(3015, null, null);
                }
            }
            else {
                //请选择一行数据！
                $.alert(2001, null, null);
            }
        },


        /*
       * POST 表单提交
       * @param {array} options    参数集合
       * submitUrl：               提交url   必须
       * onFailed：                错误回调方法
       * onSuccess：               成功回调方法
       * reloaddg：                数据列表
       * {function} onSubmitBeforeValid    保存之前校验函数，返回false则校验不通过，停止执行提交，返回true则校验通过继续执行
       * {function} onSubmitBeforeInit     提交保存之前初始化函数，返回false则停止执行，返回true则继续执行

       *
       *        {object} alertOpt 关于alert消息弹出配置参数
                         {function} onSuccessAlert      操作成功弹出消息
                         {function} onFailedAlert       操作错误弹出消息
       */
        onExPostForm: function (options) {
            var defaultOpt = {
                submitUrl: "",
                onSubmitBeforeValid: function () {
                    return true;
                },
                onSubmitBeforeInit: function () {
                    return true;
                },
                alertOpt: {}
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            if (defaultOpt.onSubmitBeforeInit()) {

                if (!$(this).valid() || !defaultOpt.onSubmitBeforeValid()) return;

                $.onExPostReq({
                    reqUrl: defaultOpt.submitUrl,
                    params: $(this).serializeJSON(),
                    onSuccess: function (response) {
                        if (response.Result == "1") {
                            if (typeof defaultOpt.alertOpt.onSuccessAlert === 'function') {
                                defaultOpt.alertOpt.onSuccessAlert(response);
                            }
                            else {
                                $.alert(response.NumberMsg, function () {
                                    if ($(defaultOpt.reloaddg).length > 0)
                                        $(defaultOpt.reloaddg).bootstrapTable('refresh');

                                    if (typeof defaultOpt.onSuccess === 'function') {
                                        defaultOpt.onSuccess(response);
                                    }
                                }, null);
                            }
                        }
                        else {
                            if (typeof defaultOpt.alertOpt.onFailedAlert === 'function') {
                                defaultOpt.alertOpt.onFailedAlert(response);
                            }
                            else {
                                $.alert(response.NumberMsg, null, null);
                            }
                            if (typeof defaultOpt.onFailed === 'function') {
                                defaultOpt.onFailed(response);
                            }
                        }
                    }
                });
            }
        },
        /*
       * 初始化Form页面数据
       * @param {array} options    参数集合 
       * dataUrl:                  页面初始化数据url           必须
       * params：                  页面参数          至少一个ID
       * onSuccess:                成功加载回调
       * onSuccessBindBefore:      成功加载 绑定数据前回调
       * onError:                  错误回调
       */
        onExInitDataForm: function (options) {
            var that = this;

            var defaultOpt = {
                params: { ID: "" }
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            $.onExPostReq({
                reqUrl: defaultOpt.dataUrl,
                params: defaultOpt.params,
                onSuccess: function (data) {

                    if (typeof defaultOpt.onSuccessBindBefore === 'function') {
                        defaultOpt.onSuccessBindBefore(data);
                    }

                    //更新页面样式布局
                    if (typeof (onInitTableFormStyle) === 'function') {
                        onInitTableFormStyle();
                    }

                    if ($(that).length == 0)
                        that = $(that.selector);
                    //赋值
                    $.SetFormValueUI(data, that);


                    if (typeof defaultOpt.onSuccess === 'function') {
                        defaultOpt.onSuccess(data);
                    }
                },
                onError: function () {
                    //初始化数据时出现未知异常！
                    $.alert(3017, null, null);
                    if (typeof defaultOpt.onError === 'function') {
                        defaultOpt.onError();
                    }
                }
            });
        },


        /*
        * Bootstrap Table 分页查询方法
        * @param {array} options    参数集合
 
        * url：                  数据加载url                    必须 
        * columns：              显示列集合
        */
        onExBootstrapPageQuery: function (options, queryParamsFrm, cfgOpt) {
            //参考1：http://bootstrap-table.wenzhixin.net.cn/zh-cn/documentation/
            //参考2：http://blog.csdn.net/rickiyeat/article/details/56483577
            var defQueryParamsFrm = "#frmQueryWhere";
            if (queryParamsFrm)
                defQueryParamsFrm = queryParamsFrm;

            var defaultcfgOpt = {
                isShowFColumns: true,   //是否自动显示出创建人 创建时间  修改人  修改时间 四列
                isShowCheckbox: true //是否自动显示 选择框
            };
            if (typeof cfgOpt === 'object')
                defaultcfgOpt = $.extend(defaultcfgOpt, cfgOpt);


            var defaultOpt = {
                method: 'post',                      //请求方式（*）
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded",
                toolbar: '#toolbar',                //工具按钮用哪个容器
                striped: true,                      //是否显示行间隔色
                pagination: true,                   //是否显示分页（*）
                sortName: "ID",
                sortOrder: 'desc',
                singleSelect: false,
                queryParams: function (params) {
                    var pageIndex = params.offset / params.limit + 1;
                    return {
                        PageInfo: {
                            PageSize: params.limit,
                            PageIndex: pageIndex,
                            SortName: params.sort,
                            SortOrder: params.order
                        },
                        QueryParams: $(defQueryParamsFrm).serializeJSON()
                    }
                },
                uniqueId: "ID",
                sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                pageNumber: 1,                       //初始化加载第一页，默认第一页
                pageSize: 10,                       //每页的记录行数（*）
                pageList: [10, 20, 30, 40, 50],        //可供选择的每页的行数（*）
                showRefresh: true,                  //是否显示刷新按钮
                search: false, //不显示 搜索框
                showColumns: true, //显示下拉框（选择显示的列）
                minimumCountColumns: 2,             //最少允许的列数
                height: 440,                //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                responseHandler: function (res) {
                    return {
                        "total": res.Total,//总页数
                        "rows": res.Data,   //数据
                        "page": res.PageIndex
                    };
                },
                clickToSelect: true, //设置true 将在点击行时，自动选择rediobox 和 checkbox
                singleSelect: true    //设置True 将禁止多选  
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            if (defaultcfgOpt.isShowCheckbox)
                defaultOpt.columns.unshift({ checkbox: true });

            if (defaultcfgOpt.isShowFColumns) {
                defaultOpt.columns.push({ field: 'CREATION_DATE', title: '创建时间', width: 140, sortable: true, formatter: $.dateFullFormat });
                defaultOpt.columns.push({ field: 'CREATOR', title: '创建人', width: 120, sortable: true });
                defaultOpt.columns.push({ field: 'LAST_UPDATE_DATE', title: '修改时间', width: 140, sortable: true, formatter: $.dateFullFormat });
                defaultOpt.columns.push({ field: 'EDITOR', title: '修改人', width: 120, sortable: true });
            }

            //加上...样式
            for (var i = 0; i < defaultOpt.columns.length; i++) {
                defaultOpt.columns[i].class = "tableColumnLeaveOut";
            }


            $(this).bootstrapTable(defaultOpt);



            //如果说 toolbar 内没有任何标签内容时则空出所占空间
            if (defaultOpt.toolbar) {
                if ($(defaultOpt.toolbar).children().length == 0) {
                    $(defaultOpt.toolbar).parent().parent().remove();
                }
            }

        },
        /*
     * Bootstrap Table 分页查询方法  查询控制
     * @param {array} options    参数集合
 
     * url：                  数据加载url                    必须 
     * columns：              显示列集合
     */
        onExBootstrapPageCQuery: function (options, queryParamsFrm) {
            //参考1：http://bootstrap-table.wenzhixin.net.cn/zh-cn/documentation/
            //参考2：http://blog.csdn.net/rickiyeat/article/details/56483577
            var defQueryParamsFrm = "#frmQuerySearchWhere";
            if (queryParamsFrm)
                defQueryParamsFrm = queryParamsFrm;


            var defaultOpt = {
                method: 'post',                      //请求方式（*）
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded",
                //toolbar: '#toolbar',                //工具按钮用哪个容器
                striped: true,                      //是否显示行间隔色
                pagination: true,                   //是否显示分页（*）
                sortName: "ID",
                sortOrder: 'desc',
                singleSelect: false,
                queryParams: function (params) {
                    var pageIndex = params.offset / params.limit + 1;
                    return {
                        PageInfo: {
                            PageSize: params.limit,
                            PageIndex: pageIndex,
                            SortName: params.sort,
                            SortOrder: params.order
                        },
                        QueryParams: $(defQueryParamsFrm).serializeJSON()
                    }
                },
                uniqueId: "ID",
                sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                pageNumber: 1,                       //初始化加载第一页，默认第一页
                pageSize: 10,                       //每页的记录行数（*）
                pageList: [10, 20, 30, 40, 50],        //可供选择的每页的行数（*）
                showRefresh: false,                  //是否显示刷新按钮
                search: false, //不显示 搜索框
                showColumns: false, //显示下拉框（选择显示的列）
                minimumCountColumns: 2,             //最少允许的列数
                height: 380,                //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                responseHandler: function (res) {
                    return {
                        "total": res.Total,//总页数
                        "rows": res.Data,   //数据
                        "page": res.PageIndex
                    };
                },
                clickToSelect: true, //设置true 将在点击行时，自动选择rediobox 和 checkbox
                singleSelect: true    //设置True 将禁止多选  
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            defaultOpt.columns.unshift({ checkbox: true });
            //加上...样式
            for (var i = 0; i < defaultOpt.columns.length; i++) {
                defaultOpt.columns[i].class = "tableColumnLeaveOut";
            }


            $(this).bootstrapTable(defaultOpt);
        }
    });


    /*
        * 非表单普通Post请求 
        * @param {array} options    参数集合
        * reqUrl：                  请求url                    必须 
        * params：                  请求参数
        * dataType:                 数据返回类型
        * onError：                 访问错误回调方法
        * onSuccess：               成功回调方法
        */
    $.onExPostReq = function (options) {
        if (!options.dataType) {
            options.dataType = 'Json';
        }

        var loadIndex = layerEx.openLoading();
        $.ajax({
            type: 'Post',
            data: options.params,
            dataType: options.dataType,
            url: options.reqUrl,
            success: function (response) {
                layerEx.closeLoading(loadIndex);

                if ($.ReqAuthorityValid(response)) {
                    if (typeof options.onSuccess === 'function') {
                        options.onSuccess(response);
                    }
                }
            },
            error: function () {
                layerEx.closeLoading(loadIndex);
                if (typeof options.onError === 'function') {
                    options.onError();
                }
                else {
                    //出现未知异常
                    $.alert(3014, null, null);
                }
            },
        });
    }

    /*
        * 非表单普通Get请求 
        * @param {array} options    参数集合
        * reqUrl：                  请求url                    必须 
        * params：                  请求参数
        * dataType:                 数据返回类型
        * onError：                 访问错误回调方法
        * onSuccess：               成功回调方法
        */
    $.onExGetReq = function (options) {
        if (!options.dataType) {
            options.dataType = 'Json';
        }


        var loadIndex = layerEx.openLoading();
        $.ajax({
            type: 'Get',
            data: options.params,
            dataType: options.dataType,
            url: options.reqUrl,
            success: function (response) {
                layerEx.closeLoading(loadIndex);

                if ($.ReqAuthorityValid(response)) {

                    if (typeof options.onSuccess === 'function') {
                        options.onSuccess(response);
                    }
                }
            },
            error: function () {
                layerEx.closeLoading(loadIndex);
                if (typeof options.onError === 'function') {
                    options.onError();
                }
                else {
                    //出现未知异常
                    $.alert(3014, null, null);
                }
            },
        });
    }
})(jQuery);
