// ==UserScript==
// @name         Try another Search engine
// @author       tomorrow.cyz@gmail.com
// @namespace    http://t.sina.com.cn/chenyuzhi
// @description  try another search engine when you perform baidu/google search
// @include      http://www.baidu.com/s?*
// @include      http://baidu.com/s?*
// @include      http://www.google.com.hk/*
// @include      http://google.com.hk/*
// ==/UserScript==
function g_search_func() {
    var keywords = document.getElementById("kw");
    window.location.href = "http://www.google.com.hk/search?q=" + keywords.value;
}

if (document.location.href.indexOf('baidu.com') != -1) {
    var tools = document.getElementById('tools');
    if (tools) {
        tools.parentNode.removeChild(tools);
    }
    var con = document.getElementById('mCon');
    if (con) {
        con.parentNode.removeChild(con);
    }
    var menus = document.getElementById('mMenu');
    if (menus) {
        menus.parentNode.removeChild(menus);
    }
    var allEle, thisEle;
    allEle = document.evaluate(
	    "//*[@class='tools']",
	    document,
	    null,
	    XPathResult.UNORDERED_NODE_SNAPSHOT_TYPE,
	    null);
    for (var i = 0; i < allEle.snapshotLength; i++) {
        thisEle = allEle.snapshotItem(i);
        thisEle.parentNode.removeChild(thisEle);
    }
    var google_btn = document.createElement("input");
    var baidu_btn = document.getElementById("kw");
    google_btn.setAttribute("type", "button");
    google_btn.setAttribute("name", "gsearch");
    google_btn.setAttribute("value", "try google");
    google_btn.addEventListener('click', g_search_func, true);
    google_btn.setAttribute("class", "btn");
    baidu_btn.parentNode.insertBefore(google_btn, baidu_btn);
}
else {
    var allEle, thisEle;
    allEle = document.evaluate(
    "//input[@name='q']",
    document,
    null,
    XPathResult.UNORDERED_NODE_SNAPSHOT_TYPE,
    null);
    for (var i = 0; i < allEle.snapshotLength; i++) {
        thisEle = allEle.snapshotItem(i);
        break;
    }
    var keywords = thisEle.value;

    var resultStats = document.getElementById('resultStats');
    if (resultStats) {
        var baidu_link = document.createElement("div");
        var url = "http://www.baidu.com/s?wd=" + keywords;
        baidu_link.innerHTML = "<a  class='gl nobr'  style='color:#4373db' href= '" + url + "' ><strong>try baidu</strong></a>";
    }
}



