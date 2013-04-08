// Copyright (c) 2012 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

// Called when the user clicks on the browser action.
chrome.browserAction.onClicked.addListener(function(tab) {
   var myCode =
    "    var floatAdCount = 0;                                                                          "
    + "        var divs = document.getElementsByTagName('div');                                           "
    + "        if (divs) {                                                                                "
    + "            for (var i = divs.length - 1; i >= 0; i--) {                                           "
    + "                var zIndex = window.getComputedStyle(divs[i], null).getPropertyValue('z-index');   "
    + "                if (zIndex > 100) {                                                                "
    + "                    var node = divs[i];                                                            "
    + "                    node.parentNode.removeChild(node);                                             "
    + "                    floatAdCount++;                                                                "
    + "                }                                                                                  "
    + "            }                                                                                      "
    + "        }                                                                                          "
    + "        console.log(floatAdCount);                                                                  "

    + "   var bodyMarginLeft = parseInt(window.getComputedStyle(document.body, null).getPropertyValue('margin-left'));        "
    + "   var bodyMarginRight = parseInt(window.getComputedStyle(document.body, null).getPropertyValue('margin-right'));      "
    + "   var rootAdCount = 0;                                                                                                "
    + "   for (var i = document.body.childNodes.length - 1; i >= 0; i--) {                                                    "
    + "       var node = document.body.childNodes[i];                                                                         "
    + "       if (node.tagName == 'DIV') {                                                                                    "
    + "           var nodeMarginLeft = parseInt(window.getComputedStyle(node, null).getPropertyValue('left'));                "
    + "           var nodeWidth = parseInt(window.getComputedStyle(node, null).getPropertyValue('width'));                    "
    + "           var nodeMarginRight = parseInt(window.getComputedStyle(node, null).getPropertyValue('margin-right'));       "
    + "           if (nodeMarginLeft + nodeWidth < bodyMarginLeft || nodeMarginRight + nodeWidth < bodyMarginRight) {         "
    + "               try {                                                                                                   "
    + "                   node.parentNode.removeChild(node);                                                                  "
    + "                   rootAdCount++;                                                                                      "
    + "               } catch (e) { };                                                                                        "
    + "           }                                                                                                           "
    + "       }                                                                                                               "
    + "   }                                                                                                                   "
    + "   console.log(rootAdCount);                                                                                           ";
	
  chrome.tabs.executeScript(null,
      {code: myCode});
});
