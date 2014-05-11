// ==UserScript==
// @name        userscript
// @namespace   sunmi.sinaapp.com
// @description some description
// @include     http://*/*
// @include     https://*/*
// @version     1
// ==/UserScript==

function withjQuery(callback, safe) {
    if (typeof (jQuery) == "undefined") {
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js";

        if (safe) {
            var cb = document.createElement("script");
            cb.type = "text/javascript";
            cb.textContent = "jQuery.noConflict();(" + callback.toString() + ")(jQuery);";
            script.addEventListener('load', function () {
                document.head.appendChild(cb);
            });
        }
        else {
            var dollar = undefined;
            if (typeof ($) != "undefined") dollar = $;
            script.addEventListener('load', function () {
                jQuery.noConflict();
                $ = dollar;
                callback(jQuery);
            });
        }
        document.head.appendChild(script);
    } else {
        callback(jQuery);
    }
    //    var fillUserjs = "function fillusers() { var data= { 'list': 	 { 'id': '1228519006352518457', 'richContent': 'en,enenenenn', 'shortContent': 'en,enenenenn', 'prettyTime': '2014-05-06 14:09', 'type': 0, 'payType': 0, 'isAdmin': false, 'isFromLoginUser': true, 'source': 'hautian', 'sourceLink': '', 'user': { 'id': '5883739243320191863', 'nickName': 'piaoling', 'url': '8477106', 'age': 29, 'sex': 1, 'avatar': 'http://img0.ph.126.net/dqS2tmvR6Yq49p18c89BNw==/6597698687308947087.jpg#3', 'avatarFlag': '1' , 'isVip': false, 'isNormalVip': false, 'isSuperVip': false } , 'isForbidden': false, 'isChartlet': false, 'toUser': { 'url': '8857021', 'nickName': 'xiaoniuzier', 'sex': 2 } ,'relatedData': { } } , 	 { 'id': '-2813587927271595690', 'richContent': 'work', 'shortContent': 'work', 'prettyTime': '2014-05-06 14:08', 'type': 0, 'payType': 0, 'isAdmin': false, 'isFromLoginUser': false, 'source': 'iPhone', 'sourceLink': 'http://love.163.com/app', 'user': { 'id': '-184733160520890604', 'nickName': 'xiaoniuzier', 'url': '8857021', 'age': 22, 'sex': 2, 'avatar': 'http://img0.ph.126.net/vp2pdLiUqLnYRQp96sRUdw==/6608190227259922701.jpg#3', 'avatarFlag': '1' , 'isVip': false, 'isNormalVip': false, 'isSuperVip': false } , 'isForbidden': false, 'isChartlet': false, 'toUser': { 'url': '8477106', 'nickName': 'piaoling', 'sex': 1 } ,'relatedData': { } } ], 'page': { 'totalSize': 163, 'pageCount': 9, 'pageNo': 2, 'pageSize': 20, 'pageMethod': 'page' } };        var  userlist=data.list;                          var table = document.getElementById('love163FriendList'); for (var i = 0; i < userlist.length; i++) { var newRow = table.insertRow(table.rows.length); var newCel1 = newRow.insertCell(0); var newCel2 = newRow.insertCell(1); var newCel3 = newRow.insertCell(2); newCel1.innerHTML = 'richContent'; newCel2.innerHTML = 'c2'; newCel3.innerHTML = 'c3'; } }";
    var fillUserjs = "function fillusers() { alert('test')}";

    var jsfu = document.createElement("script");
    jsfu.type = "text/javascript";
    jsfu.innerHTML = fillUserjs;
    document.head.appendChild(jsfu);

    var newNode = document.createElement("div");
    var newNode1 = document.createElement("div");
    var google_btn = document.createElement("input");
    google_btn.setAttribute("type", "button");
    google_btn.setAttribute("name", "gsearch");
    google_btn.setAttribute("value", "try google");
    google_btn.addEventListener('click', fillusers, true);
    google_btn.setAttribute("class", "btn");
    newNode1.innerHTML = "<input type='button' value='load' onclick=alert('111')  />";
    var newNode2 = document.createElement("div");
    newNode2.innerHTML = " <table id='love163FriendList' style='background-color: Gray' width='100%' border='1'><tr><td>user1</td><td>user1</td><td>user1</td></tr></table>";
    newNode.appendChild(newNode2);
    newNode.appendChild(google_btn);
    newNode.appendChild(newNode1);
    var targetdiv = document.getElementById("wrapper");
    document.body.insertBefore(newNode, targetdiv);
}
function fillusers() {
    var data = { 'list': [{ 'richContent': 'en,enenenenn' }, { 'id': '-2813587927271595690', 'richContent': 'work'}], 'page': { 'totalSize': 163, 'pageCount': 9, 'pageNo': 2, 'pageSize': 20, 'pageMethod': 'page'} };

    var userlist = data.list;
    var table = document.getElementById('love163FriendList');
    for (var i = 0; i < userlist.length; i++) {
        var newRow = table.insertRow(table.rows.length);
        var newCel1 = newRow.insertCell(0);
        var newCel2 = newRow.insertCell(1);
        var newCel3 = newRow.insertCell(2);
        newCel1.innerHTML = "richContent";
        newCel2.innerHTML = "c2";
        newCel3.innerHTML = "c3";
    }
}
withjQuery(function ($) {
    //       //do something
}, true);


 