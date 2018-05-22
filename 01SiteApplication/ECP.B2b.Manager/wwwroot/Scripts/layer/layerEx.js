/// <reference path="../_references.js" />
/*
 * @描    述: layer的自定义封装，layer官网 http://www.layui.com/doc/modules/layer.html
 * @作    者: xiaoxin 2016-11-5
 * @修    改: xiaoxin 2016-11-21 添加closeIframeModal
*/
(function (w, $, layer) {
    'use strict';

    var p = w.parent,
        pl = p.layer;

    /*
     * options参数参照layer
     */
    w.layerEx = {
        /*
         * 确认框，类似于系统自带的confirm
         * @param {string} content 内容
         * @param {function} yes 点确定的回调
         * @param {function} cancel 点取消的回调
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        confirm: function (content, yes, cancel, options) {
            var defaultOpt = {
                icon: 3,
                title: false,
                closeBtn: false,
                skin: 'layui-layer-lan',
                btn: ['确定', '取消']
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            var yes2 = function (index, layero) {
                layer.close(index);
                if (yes)
                    yes(index, layero);
            };

            return layer.confirm(content, defaultOpt, yes2, cancel);
        },

        /*
         * 警告窗，类似于系统的alert
         * @param {string} content 内容
         * @param {function} yes 点确定的回调
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        alert: function (content, yes, options) {
            //icon  1-勾号  2-×号    3-问号   4-锁号   5-哭脸    6-笑脸    7-感叹号 
            var defaultOpt = {
                icon: 0,
                title: false,
                closeBtn: false,
                skin: 'layui-layer-lan'
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            var yes2 = function (index, layero) {
                layer.close(index);
                if (yes)
                    yes(index, layero);
            };

            return layer.alert(content, defaultOpt, yes2);
        },

        /*
         * 消息提示框(相对于成功和失败的提示框没有了图标)
         * @param {string} content 内容
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        msg: function (content, options) {
            var defaultOpt = {
                content: content,
                title: false,
                closeBtn: false,
                btn: false,

                time: 3000,
                shade: 0.1,
                shadeClose: true,

                skin: 'layui-layer-lan'
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);
            return layer.open(defaultOpt);
        },

        /*
         * 出现错误时的消息提示框
         * @param {string} content 内容
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        failMsg: function (content, options) {
            var defaultOpt = {
                content: content,
                icon: 5,
                title: false,
                closeBtn: false,
                shade: 0.1,
                shadeClose: true,
                btn: false,
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);
            return layer.open(defaultOpt);
        },


        /*
         * 请求成功时的消息提示框
         * @param {string} content 内容
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        successMsg: function (content, options) {
            var defaultOpt = {
                content: content,
                icon: 6,
                title: false,
                closeBtn: false,
                btn: false,
                time: 3000,
                shade: 0,
                shadeClose: true,
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);
            return layer.open(defaultOpt);
        },


        /*
         * 内容为html字符串的弹框
         * @param {string} title 标题
         * @param {string} html 字符串
         * @param {number} width 宽度
         * @param {string} content 内容
         * @param {function} yes 点确定的回调
         * @param {function} cancel 点取消的回调
         * @param {object} options 参数(可空)
         * @returns {number} layer的index
         */
        htmlModal: function (title, html, width, yes, cancel, options) {
            var defaultOpt = {
                type: 1,
                moveType: 1,
                title: title,
                content: html,
                btn: ['提交', '取消'],
                area: width,
                skin: 'layui-layer-lan',
                yes: yes,
                cancel: cancel,
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            return layer.open(defaultOpt);
        },

        /*
         * iframe的弹窗
         * @param {string} url 请求的url
         * @param {string|boolean} title 标题
         * @param {string|Array} area 宽高 ['70%', '90%'] '500px' 'auto'
         * @param {object} options 参数(可选)
         * @returns {number} layer的index
         */
        iframeModal: function (url, title, options) {
            //console.info(url);
            var defaultOpt = {
                type: 2,
                title: title,
                area: ["100%","100%"],
                maxmin: true,
                skin: 'layui-layer-lan',
                content: url,
                moveType: 1,
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);

            return layer.open(defaultOpt);
        },

        /*
         * iframe获取自身layer索引
         * @returns {} 
         */
        getIframeIndex: function () {
            return pl.getFrameIndex(w.name);
        },

        /*
         * 用于在iframe页关闭自身
         * @returns {number} 关闭的iframe的索引
         */
        closeIframeModal: function () {
            var index = this.getIframeIndex();
            pl.close(index);
            return index;
        },

        /*
         * 最大或最小化或还原iframe
         * @param {number} maxOrMin 0或空: 最大化，1:最小化，其他:还原
         * @returns {number} 当前iframe的layer索引
         */
        maxMinIframe: function (maxOrMin) {
            var index = this.getIframeIndex();
            maxOrMin = maxOrMin || 0;
            if (maxOrMin === 0)
                pl.full(index);
            else if (maxOrMin === 1)
                pl.min(index);
            else
                pl.restore(index);
            return index;
        },

        openLoading: function () {
            return layer.load(1, { shade: [0.1, '#000'] });  //0.1透明度的黑色背景
        },
        closeLoading: function (index) {
            layer.close(index);
        },


        /*
         * 内容为dom元素的弹框
         * @param {string} title 标题
         * @param {string|jQuery} dom jQuery选择器(可以不加$)
         * @param {string} content 内容
         * @param {function} yes 点确定的回调 
         * @param {function} cancel 点取消的回调
         * @param {object} options 参数(可空) 
         * @returns {number} layer的index
         */
        modal: function (title, dom, yes, cancel, options) {
            var $dom = $(dom),
                width = $dom.outerWidth(),
                height = $dom.outerHeight() + 92,
                winWidth = $(window).width(),
                winHidht = $(window).height(),
                isHidden = $dom.is(':hidden');

            width = winWidth < width ? winWidth : width;
            height = winHidht < height ? winHidht : height;

            if (options && options.width) {
                width = options.width;
                delete options.width;
            }


            var defaultOpt = {
                type: 1,
                moveType: 1,
                title: title,
                content: $dom,
                skin: 'layui-layer-lan',
                area: [width + 'px', height + 'px'],
                btn: ['保存', '返回'],
                yes: yes,
                cancel: cancel,
                btn2: cancel,   //设置取消按钮触发传入cancel函数  2016/12/18 xinyang 改
                end: function () {
                    if (!isHidden) {
                        $dom.show();
                    }
                    else {
                        $dom.hide();
                    }
                },
                success: function (layero) {
                    layero.find('.layui-layer-btn').css('text-align', 'center');
                    //防止高度过高出现y轴滚动条时宽度需要进行调整
                    layero.width(layero.width() + 30);
                    layero.height(layero.height() + 10);
                    layero.find('.layui-layer-content').height(layero.find('.layui-layer-content').height() + 10);

                    var aArray = $(layero.find('.layui-layer-btn').find("a"));
                    for (var i = 0; i < aArray.length; i++) {
                        if ($(aArray[i]).text() == "保存")
                        {
                            $(aArray[i]).addClass("glyphicon glyphicon-arrow-up");
                        }
                        else if ($(aArray[i]).text() == "返回")
                        {
                            $(aArray[i]).addClass("glyphicon glyphicon-circle-arrow-left");
                        }
                    }
                }
            };
            if (typeof options === 'object')
                defaultOpt = $.extend(defaultOpt, options);


            return layer.open(defaultOpt);
        },
    };

    w.layeEditIndexArray={ }
    $.fn.layeditInitEx = function () {
        //提交前需要在onSubmitBeforeInit中调用下面方法将内容同步到textarea中
        //layui.layedit.sync(index)
        let that = this;
        //延迟弹出 否则google会有兼容问题
        setTimeout(function () {
            var thisId = $(that).attr("id");
            $(that).hide();
            layui.use('layedit', function () {
                var layedit = layui.layedit;
                var layeditIndex = layedit.build(thisId); //建立编辑器 
                layeEditIndexArray[thisId] = layeditIndex;
            });
        },100);
    };
    $.fn.layeditSyncEx = function () {
        var thisId = $(this).attr("id"); 
        layui.layedit.sync(layeEditIndexArray[thisId]);
        //未填写内容（img判断有无表情包）
        if (!$("<div>" + $(this).val() + "<div/>").text() && $(this).val().indexOf("<img") < 0)
            $(this).val("");
    };
})(window, jQuery, layer);
