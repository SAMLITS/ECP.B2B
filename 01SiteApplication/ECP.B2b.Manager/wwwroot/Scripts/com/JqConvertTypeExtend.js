/********************************************
* 模块名称：JqConvertTypeExtend
* 功能说明：类型转换扩展
* 创 建 人：LTS
* 创建时间：2017-09-25  
* 修 改 人： 
* 修改时间： 
* ******************************************/

(function ($) {
    /*指定位数转换*/
    $.parseFloatFixed = function (val,length)
    {
        try {
            if (val) {
                //保留两位
                return parseFloat(val).toFixed(length);
            }
            return val;
        }
        catch(e){
            return val;
        }
    };

    /*金额转换*/
    $.parseMoney = function (val)
    {
        return $.parseFloatFixed(val,2);
    };
    /*重量转换*/
    $.parseWeight = function (val) {
        return $.parseFloatFixed(val, 4);
    };
    /*体积转换*/
    $.parseVolume = function (val) {
        return $.parseFloatFixed(val, 4);
    };
})(jQuery);
 