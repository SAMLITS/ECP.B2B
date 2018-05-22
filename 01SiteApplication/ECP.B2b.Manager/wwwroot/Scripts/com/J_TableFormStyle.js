/********************************************
* 模块名称：J_TableFormStyle
* 功能说明：业务实体:用于Table 奇偶行 样式
* 创 建 人：CXY
* 创建时间：2017年4月28日
* 兼容   谷歌|火狐|ie
* 修 改 人：ZH
* 修改时间：2017年8月8日
* ******************************************/

$(function () {
    onInitTableFormStyle();
});

function onInitTableFormStyle()
{

    var istrue = true; //是否有跨列

    $(".form-group").css({
        "margin-bottom":"0px",
        "width":"100%"
    });

    $(".form-group").attr({
        cellspacing: "0",
        cellpadding: "0",
        border: "0"
    });

    $(".form-group tr").css("height", "30px").each(function (index) {

        istrue = true;

        $(this).find("td").each(function (i) {
            var tdObj = $(".form-group tr:eq(" + (index) + ") td:nth-child(" + (i + 1) + ")")
            var col = $(tdObj).attr("colspan");
            if (col == null || col === "undefined" || col==="1") {
                if (istrue === true) {
                    if ((i + 1) % 2 === 0) {
                        $(tdObj).addClass("col-sm-2"); 
                    } else {
                        $(tdObj).addClass("control-label col-sm-1");
                    }
                } else {
                    if ((i + 1) % 2 === 0) {
                        $(tdObj).addClass("control-label col-sm-1");
                    } else {
                        $(tdObj).addClass("col-sm-2 ");
                    }
                }
            }
            else if (col === "3") {
                istrue = true;

                //1100 的情况下
                $(tdObj).find("input,select,textarea").css("width", "97.3%");
            }
            else {
                istrue = false;
            } 
        });
    });
}
