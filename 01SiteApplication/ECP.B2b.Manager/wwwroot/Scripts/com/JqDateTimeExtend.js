(function ($) {
    //json格式日期转日期型  不带时分秒
    $.dateFormat = function (jsonDate) {
        try {
            if (jsonDate == undefined) {
                return "";
            }
            if (jsonDate != "") {
                jsonDate = jsonDate.replace("T", " ");

                var date = new Date(jsonDate);
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                if (isNaN(month)) {
                    return jsonDate;
                }
                var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                var hours = date.getHours();

                return date.getFullYear() + "-" + month + "-" + day;
            }
            else {
                return jsonDate;
            }
        } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
            return jsonDate;
        }
    },
        //json格式日期转日期型  带时分秒
        $.dateFullFormat = function (jsonDate) {//json日期格式转换为正常格式
            try {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                //return jsonDate;

                if (jsonDate == undefined) {
                    return "";
                }
                if (jsonDate != "") {

                    jsonDate = jsonDate.replace("T", " ");

                    //日期格式，IE8 以下兼容性处理
                    var index = jsonDate.indexOf('.');
                    if (index != -1) {

                        jsonDate = jsonDate.substring(0, index);
                    }
                    //var date = new Date(jsonDate);
                    var date = new Date(jsonDate.replace(/-/g, '/').replace(/T|Z/g, ' ').trim());

                    var date = new Date(jsonDate);
                    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                 
                    var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
                    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
                    var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

                    var milliseconds = date.getMilliseconds();

                    return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds; //+ "." + milliseconds;
                }
                else {
                    return jsonDate;
                }
            } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                return jsonDate;
            }
        },

        $.dateFullFormatByDmy = function (jsonDate) {
            var dmys = jsonDate.replace("  ", " ").split(" ");

            var time = dmys[3];
            //AM与PM是使用12小时制时区分上下午所用的.AM表示上午,PM表示下午.
            if (time.indexOf("AM") > -1) {
                time = time.replace("AM", "");
            }
            else if (time.indexOf("PM") > -1) {
                time = time.replace("PM", "");
                var timeArray = time.split(":");
                timeArray[0] = parseInt(timeArray[0]) + 12;
                time = timeArray.join(":");
            }

            return $.dateFullFormat(dmys[2] + "-" + dmys[0] + "-" + dmys[1] + " " + time);
        },
        $.dateFormatByDmy = function (jsonDate) {
            var dmys = jsonDate.replace("  ", " ").split(" ");

            var time = dmys[3].replace("AM", "").replace("PM", "");

            return $.dateFormat(dmys[2] + "-" + dmys[0] + "-" + dmys[1] + " " + time);
        },

        //json格式日期转日期型  带时分秒
        $.jsonDateFullFormat = function (jsonDate) {//json日期格式转换为正常格式
            try {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
                var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
                var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

                var milliseconds = date.getMilliseconds();
                return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;//+ "." + milliseconds;
            } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                return "";
            }
        },
        //json格式日期转日期型 不带时分秒
        $.jsonDateFormat = function (jsonDate) {//json日期格式转换为正常格式
            try {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                var hours = date.getHours();
                var minutes = date.getMinutes();
                var seconds = date.getSeconds();
                var milliseconds = date.getMilliseconds();
                return date.getFullYear() + "-" + month + "-" + day;
            } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                return "";
            }
        },
        //获取当前的日期时间 格式“yyyy-MM-dd HH:MM:SS”
        $.getNowFormatDate = function () {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            var hour = date.getHours();
            var minute = date.getMinutes();
            var second = date.getSeconds();

            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            if (hour >= 0 && hour <= 9) {
                hour = "0" + hour;
            }
            if (minute >= 0 && minute <= 9) {
                minute = "0" + minute;
            }
            if (second >= 0 && second <= 9) {
                second = "0" + second;
            }

            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                + " " + hour + seperator2 + minute
                + seperator2 + second;
            return currentdate;
        }
})(jQuery);