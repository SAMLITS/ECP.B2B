$.validator.addMethod('mobile', function (value, element) {
    var length = value.length;
    var mobile = /^1[3|4|5|7|8]\d{9}$/;
    return this.optional(element) || (length === 11 && mobile.test(value));
}, '手机号码格式错误');

$.validator.addMethod('phone', function (value, element) {
    var tel = /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;
    return this.optional(element) || (tel.test(value));
}, '电话号码格式错误');

$.validator.addMethod('chinese', function (value, element) {
    var chinese = /^[\u4e00-\u9fa5]+$/;
    return this.optional(element) || (chinese.test(value));
}, '只能输入中文');

$.validator.addMethod('chrnum', function (value, element) {
    var chrnum = /^([a-zA-Z0-9]+)$/;
    return this.optional(element) || (chrnum.test(value));
}, '只能输入数字和字母(字符A-Z, a-z, 0-9)');

$.validator.addMethod('ip', function (value, element) {
    var ip = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
    return this.optional(element) || (ip.test(value) && (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256));
}, 'Ip地址格式错误');

$.validator.addMethod('qq', function (value, element) {
    var tel = /^[1-9]\d{4,9}$/;
    return this.optional(element) || (tel.test(value));
}, 'QQ号码格式错误');

$.validator.addMethod('zipCode', function (value, element) {
    var tel = /^[0-9]{6}$/;
    return this.optional(element) || (tel.test(value));
}, '邮政编码格式错误');

$.validator.addMethod('money', function (value, element) {
    var tel = /^(([1-9]\d*)|0)(\.\d{1,2})?$/;
    return this.optional(element) || (tel.test(value));
}, '金额格式错误');
$.validator.addMethod('regexPassword', function (value, element) {
    var chrnum = /^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/;
    return this.optional(element) || (chrnum.test(value));
}, '密码至少包一个大写字母、一个小写字母及一个符号，长度至少8位');
$.validator.addMethod('regexHardPassword', function (value, element) {
    var Modes = 0;
    for (i = 0; i < value.length; i++) {
        Modes |= CharMode(value.charCodeAt(i));
    }
    //return bitTotal(Modes);
    if (bitTotal(Modes) == "3")  //=="3"是说明3个条件都满足
    {
        return true;
    }
    else {
        return false;
    }

    //CharMode函数
    function CharMode(iN) {
        if (iN >= 48 && iN <= 57)//数字
            return 1;
        if (iN >= 65 && iN <= 90) //大写字母
            return 2;
        if ((iN >= 97 && iN <= 122) || (iN >= 65 && iN <= 90))
            //大小写
            return 4;
        //else
        //    return 8; //特殊字符
    }
    //bitTotal函数
    function bitTotal(num) {
        modes = 0;
        for (i = 0; i < 4; i++) {
            if (num & 1) modes++;
            num >>>= 1;
        }
        return modes;
    }

}, '密码必须包含字母大小写+数字，不要使用连续字母或单纯数字！');
$.validator.addMethod('positiveInteger', function (value, element) {
    return (this.optional(element) || /^\d+$/.test(value)) && value > 0;//digits包含0
}, "请输入一个正整数");
$.validator.addMethod('greaterThan', function (value, element, param) {
    return this.optional(element) || value > param;
}, "请输入大于{0} 的数值");
$.validator.addMethod('lessThan', function (value, element, param) {
    return this.optional(element) || value < param;
}, "请输入小于{0} 的数值");