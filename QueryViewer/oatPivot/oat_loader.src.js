/*
 *  $Id: loader.js,v 1.29 2008/03/20 09:48:02 source Exp $
 *
 *  This file is part of the OpenLink Software Ajax Toolkit (OAT) project.
 *
 *  Copyright (C) 2005-2007 OpenLink Software
 *
 *  See LICENSE file for details.
 */

//if (OAT.checkInternetExplorerVersion()){
if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
window.OAT = {};

//load japanese lenguage character images files
if ((gx.languageCode == "jap") || (gx.languageCode == "jpn") || (gx.languageCode == "jp") || (gx.languageCode == "ja") || (gx.languageCode == "jpx") 
	|| (gx.languageCode == "ain") || (gx.languageCode == "jpns") || (gx.languageCode == "ams") || 	(gx.languageCode == 'japanese')
	 || (gx.languageCode == "JAP") || (gx.languageCode == "JP") || (gx.languageCode == "JA") || (gx.languageCode == "JPN")  
	  || (gx.languageCode == "ja-JP") || (gx.languageCode == "ja_JP") || (gx.languageCode == "jp")){
	var b = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/' + 'jsPDF_japanese.js',true);
   
    
	var d=document.getElementsByTagName("head")[0]||document.documentElement;
	var s=d.getElementsByTagName("script");
	var prevLoad = false;
	for (var i=0;s.length>i; i++) {
		if (s[i].src==b) {
			prevLoad = true;
		}
	}
	if (!prevLoad){
		var e=document.createElement("script");
		e.type="text/javascript";
		e.src=b;
		e.onerror=function(){
			var e=document.createElement("script");
			e.type="text/javascript";
			e.src=gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/' + 'jsPDF_japanese.src.js',true); 
			var d=document.getElementsByTagName("head")[0]||document.documentElement;
    		d.insertBefore(e,d.lastChild);
		};
		e.onreadystatechange=function(){
			var document=this.readyState;
			if(document==="loaded"||document==="complete"){
				e.onreadystatechange=null;
			}
		};
		d.insertBefore(e,d.lastChild);
	}
}
/*
	OAT.Loader.preInit(callback) - do something when everything is loaded
	OAT.Loader.loadFeatures(features,callback) - do something when features are loaded
	OAT.Loader.loadedLibs = ["ajax2","window",...]
	
	Contains: 
	* OAT
	* OAT.Preferences
	* OAT.Dom
	* OAT.Event
	* OAT.Style
	* OAT.Browser
	* OAT.Loader
	* OAT.Files
	* OAT.Dependencies
	* OAT.MSG
	* OAT.Debug
*/
var featureList = ["pivot", "grid", "statistics"];  
/* global namespace */


OAT.Preferences = {
	showAjax:1, /* show Ajax window even if not explicitly requested by application? */
	useCursors:1, /* scrollable cursors */
	windowTypeOverride:0, /* do not guess window type */
	xsltPath:"../xslt/",
	//imagePath: "../images/",
	imagePath: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/', true),
	//stylePath: "../styles/",
	stylePath: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/', true),
	version:"19.03.2008",
	httpError:1, /* show http errors */
	allowDefaultResize:1,
	allowDefaultDrag:1
}

OAT.$ = function(something) {
	if (typeof(something) == "string") {
		var elm = document.getElementById(something);
	} else {
		var elm = something;
	}
	if (something instanceof Array) {
		var elm = [];
		for (var i=0;i<something.length;i++) { elm.push(OAT.$(something[i])); }
	}
	if (!elm) return false;
	return elm;
}

OAT.$$ = function(className, root) {
	var e = root || document;
	var elms = e.getElementsByTagName("*");
	var matches = [];

	if (OAT.Dom.isClass(e,className)) { matches.push(e); }
	for(var i=0;i<elms.length;i++) {
		if(OAT.Dom.isClass(elms[i],className)) { matches.push(elms[i]); }
	}
	return matches;
}

OAT.$v = function(something) {
	var e = OAT.$(something);
	if (!e) return false;
	if (!("value" in e)) return false;
	return e.value;
}





/* several helpful prototypes */

Array.prototype.copy = function() {
	var a = [];
	for (var i=0;i<this.length;i++) { a.push(this[i]); }
	return a;
}

Array.prototype.find = function(str) {
	for (var i=0;i<this.length;i++) if (this[i] == str) { return i; }
	return -1;
}

Array.prototype.append = function(arr) {
	var a = arr;
	if (!(arr instanceof Array)) { a = [arr]; }
	for (var i=0;i<a.length;i++) { this.push(a[i]); }
}

String.prototype.trim = function() {
	var result = this.match(/^ *(.*?) *$/);
	return (result ? result[1] : this);
}

OAT.Dom = { /* DOM common object */
    create: function(tagName, styleObj, className) {
        var elm = document.createElement(tagName);
        if (styleObj) {
            for (prop in styleObj) { elm.style[prop] = styleObj[prop]; }
        }
        if (className) { elm.className = className; }
        return elm;
    },

    createNS: function(ns, tagName) {
        if (document.createElementNS) {
            var elm = document.createElementNS(ns, tagName);
        } else {
            var elm = document.createElement(tagName);
            elm.setAttribute("xmlns", ns);
        }
        return elm;
    },

    text: function(text) {
        var elm = document.createTextNode(text);
        return elm;
    },

    button: function(label) {
        var b = OAT.Dom.create("input");
        b.type = "button";
        b.value = label;
        return b;
    },

    radio: function(name) {
        if (OAT.Browser.isIE) {
            var elm = document.createElement('<input type="radio" name="' + name + '" />');
            return elm;
        } else {
            var elm = OAT.Dom.create("input");
            elm.name = name;
            elm.type = "radio";
            return elm;
        }
    },

    image: function(src, srcBlank, w, h) {
        w = (w ? w + 'px' : 'auto');
        h = (h ? h + 'px' : 'auto');
        var elm = OAT.Dom.create("img", { width: w, height: h });
        OAT.Dom.imageSrc(elm, src, srcBlank);
        return elm;
    },

    imageSrc: function(element, src, srcBlank) {
        var elm = OAT.$(element);
        var png = !!src.toLowerCase().match(/png$/);
        if (png && OAT.Browser.isIE) {
            if (!srcBlank) srcBlank = OAT.Preferences.imagePath + 'Blank.gif';
            elm.src = srcBlank;
            elm.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "', sizingMethod='image')";
        } else {
            elm.src = src;
        }
    },
    option: function(name, value, parent) {
        var opt = OAT.Dom.create("option");
        opt.innerHTML = name;
        opt.value = value;
        if (parent) { OAT.$(parent).appendChild(opt); }
        return opt;
    },
    append: function() {
        for (var i = 0; i < arguments.length; i++) {
            var arr = arguments[i];
            if (!(arr instanceof Array)) { continue; }
            if (arr.length < 2) { continue; }
            var parent = OAT.$(arr[0]);
            for (var j = 1; j < arr.length; j++) {
                var children = arr[j];
                if (!(children instanceof Array)) { children = [children]; }
                for (var k = 0; k < children.length; k++) {
                    var child = children[k];
                    parent.appendChild(OAT.$(child));
                }
            }
        }
    },

    hide: function(element) {
        if (arguments.length > 1) {
            for (var i = 0; i < arguments.length; i++) { OAT.Dom.hide(arguments[i]); }
            return;
        }
        if (element instanceof Array) {
            for (var i = 0; i < element.length; i++) { OAT.Dom.hide(element[i]); }
            return;
        }
        var elm = OAT.$(element);
        if (!elm) { return; }
        /* ie input hack */
        var inputs_ = elm.getElementsByTagName("input");
        var inputs = [];
        for (var i = 0; i < inputs_.length; i++) { inputs.push(inputs_[i]); }
        if (elm.tagName.toLowerCase() == "input") { inputs.push(elm); }
        for (var i = 0; i < inputs.length; i++) {
            var inp = inputs[i];
            if (inp.type == "radio" || inp.type == "checkbox") {
                if (!inp.__checked) { inp.__checked = (inp.checked ? "1" : "0"); }
            }
        }
        elm.style.display = "none";
    },

    show: function(element) {
        if (arguments.length > 1) {
            for (var i = 0; i < arguments.length; i++) { OAT.Dom.show(arguments[i]); }
            return;
        }
        if (element instanceof Array) {
            for (var i = 0; i < element.length; i++) { OAT.Dom.show(element[i]); }
            return;
        }
        var elm = OAT.$(element);
        if (!elm) { return; }
        elm.style.display = "";
        /* ie input hack */
        var inputs_ = elm.getElementsByTagName("input");
        var inputs = [];
        for (var i = 0; i < inputs_.length; i++) { inputs.push(inputs_[i]); }
        if (elm.tagName.toLowerCase() == "input") { inputs.push(elm); }
        for (var i = 0; i < inputs.length; i++) {
            var inp = inputs[i];
            if (inp.type == "radio" || inp.type == "checkbox") {
                if (inp["__checked"] && inp.__checked === "1") { inp.checked = true; }
                if (inp["__checked"] && inp.__checked === "0") { inp.checked = false; }
                inp.__checked = false;
            }
        }
    },

    clear: function(element) {
        var elm = OAT.$(element);
        while (elm.firstChild) { elm.removeChild(elm.firstChild); }
    },

    unlink: function(element) {
        var elm = OAT.$(element);
        if (!elm) { return; }
        if (!elm.parentNode) { return; }
        elm.parentNode.removeChild(elm);
    },

    center: function(element, x, y, reference) {
        var elm = OAT.$(element);
        var p = elm.offsetParent;
        if (reference) { p = reference; }
        if (!p) { return; }
        var par_dims = (p == document.body || p.tagName.toLowerCase() == "html" ? OAT.Dom.getViewport() : OAT.Dom.getWH(p));
        var dims = OAT.Dom.getWH(elm);
        var new_x = Math.round(par_dims[0] / 2 - dims[0] / 2);
        var new_y = Math.round(par_dims[1] / 2 - dims[1] / 2);
        if (new_y < 0) { new_y = 30; }
        var s = OAT.Dom.getScroll();
        if (p == document.body || p.tagName.toLowerCase() == "html") {
            new_x += s[0];
            new_y += s[1];
        }
        if (x) { elm.style.left = new_x + "px"; }
        if (y) { elm.style.top = new_y + "px"; }
    },

    isChild: function(child, parent) {
        var c_elm = OAT.$(child);
        var p_elm = OAT.$(parent);
        /* walk up from the child. if we find parent element, return true */
        var node = c_elm.parentNode;
        do {
            if (!node) { return false; }
            if (node == p_elm) { return true; }
            node = node.parentNode;
        } while (node != document.body && node != document);
        return false;
    },

    isKonqueror: function() { return (navigator.userAgent.match(/konqueror/i) ? true : false); },
    isKHTML: function() { return (navigator.userAgent.match(/khtml/i) ? true : false); },
    isIE: function() { return (document.attachEvent && !document.addEventListener ? true : false); },
    isIE7: function() { return (navigator.userAgent.match(/msie 7/i) ? true : false); },
    isIE6: function() { return (OAT.Dom.isIE() && !OAT.Dom.isIE7()); },
    isGecko: function() { return ((!OAT.Dom.isKHTML() && navigator.userAgent.match(/Gecko/i)) ? true : false); },
    isOpera: function() { return (navigator.userAgent.match(/Opera/) ? true : false); },
    isWebKit: function() { return (navigator.userAgent.match(/AppleWebKit/) ? true : false); },
    isMac: function() { return (navigator.platform.toString().match(/mac/i) ? true : false); },
    isLinux: function() { return (navigator.platform.toString().match(/linux/i) ? true : false); },
    isWindows: function() { return (navigator.userAgent.toString().match(/windows/i) ? true : false); },

    color: function(str) {
        var hex2dec = function(hex) { return parseInt(hex, 16); }
        /* returns [col1,col2,col3] in decimal */
        if (str.match(/#/)) {
            /* hex */
            if (str.length == 4) {
                var tmpstr = "#" + str.charAt(1) + str.charAt(1) + str.charAt(2) + str.charAt(2) + str.charAt(3) + str.charAt(3);
            } else {
                var tmpstr = str;
            }
            var tmp = tmpstr.match(/#(..)(..)(..)/);
            return [hex2dec(tmp[1]), hex2dec(tmp[2]), hex2dec(tmp[3])];
        } else {
            /* decimal */
            var tmp = str.match(/\(([^,]*),([^,]*),([^\)]*)/);
            return [parseInt(tmp[1]), parseInt(tmp[2]), parseInt(tmp[3])];
        }
    },

    isClass: function(something, className) {
        var elm = OAT.$(something);
        if (!elm) { return false; }
        if (className == "*") { return true; }
        if (className == "") { return false; }
        if (!elm.className) { return false; }
        var arr = elm.className.split(" ");
        var index = arr.find(className);
        return (index != -1);
    },

    addClass: function(something, className) {
        var elm = OAT.$(something);
        if (!elm) { return; }
        if (OAT.Dom.isClass(elm, className)) { return; }
        var arr = elm.className.split(" ");
        arr.push(className);
        if (arr[0] == "") { arr.splice(0, 1); }
        elm.className = arr.join(" ");
    },

    setIdPropertyValue: function(something, value) {
        var elm = OAT.$(something);
        if (!elm) { return; }
        elm.id = value;
    },

    removeClass: function(something, className) {
        var elm = OAT.$(something);
        if (!elm) { return; }
        if (!OAT.Dom.isClass(elm, className)) { return; } /* cannot remove non-existing class */
        if (className == "*") { elm.className = ""; } /* should not occur */
        var arr = elm.className.split(" ");
        var index = arr.find(className);
        if (index == -1) { return; } /* should NOT occur! */
        arr.splice(index, 1);
        elm.className = arr.join(" ");
    },

    getViewport: function() {
        if (OAT.Browser.isWebKit) {
            return [window.innerWidth, window.innerHeight];
        }
        if (OAT.Browser.isOpera || document.compatMode == "BackCompat") {
            return [document.body.clientWidth, document.body.clientHeight];
        } else {
            return [document.documentElement.clientWidth, document.documentElement.clientHeight];
        }
    },

    position: function(something) {
        var elm = OAT.$(something);
        var parent = elm.offsetParent;
        if (elm == document.body || elm == document || !parent) { return OAT.Dom.getLT(elm); }
        var parent_coords = OAT.Dom.position(parent);
        var c = OAT.Dom.getLT(elm);
        /*
        var x = elm.offsetLeft - elm.scrollLeft + parent_coords[0];
        var y = elm.offsetTop - elm.scrollTop + parent_coords[1];
        */

        /*
        this is interesting: Opera with no scrolling reports scrollLeft/Top equal to offsetLeft/Top for <input> elements
        */
        var x = c[0];
        var y = c[1];
        if (!OAT.Browser.isOpera || elm.scrollTop != elm.offsetTop || elm.scrollLeft != elm.offsetLeft) {
            x -= elm.scrollLeft;
            y -= elm.scrollTop;
        }

        if (OAT.Browser.isWebKit && parent == document.body && OAT.Dom.style(elm, "position") == "absolute") { return [x, y]; }

        x += parent_coords[0];
        y += parent_coords[1];
        return [x, y];
    },

    getLT: function(something) {
        var elm = OAT.$(something);
        var curr_x, curr_y;
        if (elm.style.left && elm.style.position != "relative") {
            curr_x = parseInt(elm.style.left);
        } else {
            curr_x = elm.offsetLeft;
        }
        if (elm.style.top && elm.style.position != "relative") {
            curr_y = parseInt(elm.style.top);
        } else {
            curr_y = elm.offsetTop;
        }
        return [curr_x, curr_y];
    },

    getWH: function(something) {
        /*
        This is tricky: we need to measure current element's width & height.
        If this property was already set (thus available directly through elm.style),
        everything is ok.
        If nothing was set yet:
        * IE stores this information in offsetWidth and offsetHeight
        * Gecko doesn't count borders into offsetWidth and offsetHeight
        Thus, we need another means for counting real dimensions.
        */
        var curr_w, curr_h;
        var elm = OAT.$(something);
        if (elm.style.width && !elm.style.width.match(/%/) && elm.style.width != "auto") {
            curr_w = parseInt(elm.style.width);
        } else if (OAT.Style.get(elm, "width") && !OAT.Browser.isIE) {
            curr_w = Math.round(parseFloat(OAT.Style.get(elm, "width")));
        } else {
            curr_w = elm.offsetWidth;
            if (elm.tagName.toLowerCase() == "input") { curr_w += 5; }
        }

        if (elm.style.height && !elm.style.height.match(/%/) && elm.style.height != "auto") {
            curr_h = parseInt(elm.style.height);
        } else if (OAT.Style.get(elm, "height") && !OAT.Browser.isIE) {
            curr_h = Math.round(parseFloat(OAT.Style.get(elm, "height")));
        } else {
            curr_h = elm.offsetHeight;
            if (elm.tagName.toLowerCase() == "input") { curr_h += 5; }
        }

        /* one more bonus - if we are getting height of document.body, take window size */
        if (elm == document.body) {
            curr_h = (OAT.Browser.isIE ? document.body.clientHeight : window.innerHeight);
        }
        return [curr_w, curr_h];
    },

    moveBy: function(element, dx, dy) {
        var curr_x, curr_y;
        var elm = OAT.$(element);
        /*
        If the element is not anchored to left top corner, strange things will happen during resizing;
        therefore, we need to make sure it is anchored properly.
        */
        if (OAT.Dom.style(elm, "position") == "absolute") {
            if (!elm.style.left) {
                elm.style.left = elm.offsetLeft + "px";
                elm.style.right = "";
            }
            if (!elm.style.top) {
                elm.style.top = elm.offsetTop + "px";
                elm.style.bottom = "";
            }
        }
        var tmp = OAT.Dom.getLT(elm);
        curr_x = tmp[0];
        curr_y = tmp[1];
        var x = curr_x + dx;
        var y = curr_y + dy;
        elm.style.left = x + "px";
        elm.style.top = y + "px";
    },

    resizeBy: function(element, dx, dy) {
        var curr_w, curr_h;
        var elm = OAT.$(element);
        /*
        If the element is not anchored to left top corner, strange things will happen during resizing;
        therefore, we need to make sure it is anchored properly.
        */
        if (OAT.Dom.style(elm, "position") == "absolute" && dx) {
            if (!elm.style.left) {
                elm.style.left = elm.offsetLeft + "px";
                elm.style.right = "";
            }
            if (!elm.style.top && dy) {
                elm.style.top = elm.offsetTop + "px";
                elm.style.bottom = "";
            }
        }
        var tmp = OAT.Dom.getWH(elm);
        curr_w = tmp[0];
        curr_h = tmp[1];
        var w = curr_w + dx;
        var h = curr_h + dy;
        if (dx) { elm.style.width = w + "px"; }
        if (dy) { elm.style.height = h + "px"; }
    },

    decodeImage: function(data) {
        var decoded = OAT.Crypto.base64d(data);
        var mime = "image/";
        switch (decoded.charAt(1)) {
            case "I": mime += "gif"; break;
            case "P": mime += "png"; break;
            case "M": mime += "bmp"; break;
            default: mime += "jpeg"; break;

        }
        var src = "data:" + mime + ";base64," + data;
        return src;
    },

    removeSelection: function() {
        var selObj = false;
        if (document.getSelection && !OAT.Browser.isGecko) { selObj = document.getSelection(); }
        if (window.getSelection) { selObj = window.getSelection(); }
        if (document.selection) { selObj = document.selection; }
        if (selObj) {
            if (selObj.empty) { selObj.empty(); }
            if (selObj.removeAllRanges) { selObj.removeAllRanges(); }
        }
    },

    getScroll: function() {
        if (OAT.Browser.isWebKit || (OAT.Browser.isIE && document.compatMode == "BackCompat")) {
            var l = document.body.scrollLeft;
            var t = document.body.scrollTop;
        } else {
            var l = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);
            var t = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
        }
        return [l, t];
    },

    getFreeSpace: function(x, y) {
        var scroll = OAT.Dom.getScroll();
        var port = OAT.Dom.getViewport();
        var spaceLeft = x - scroll[0];
        var spaceRight = port[0] - x + scroll[0];
        var spaceTop = y - scroll[1];
        var spaceBottom = port[1] - y + scroll[1];
        var left = (spaceLeft > spaceRight);
        var top = (spaceTop > spaceBottom);
        return [left, top];

    },

    toSafeXML: function(str) {
        if (typeof (str) != "string") { return str; }
        return str.replace(/&/g, "&amp;").replace(/>/g, "&gt;").replace(/</g, "&lt;");
    },

    fromSafeXML: function(str) {
        return str.replace(/&amp;/g, "&").replace(/&gt;/g, ">").replace(/&lt;/g, "<");
    },

    uriParams: function() {
        var result = {};
        var s = location.search;
        if (s.length > 1) { s = s.substring(1); }
        if (!s) { return result; }
        var parts = s.split("&");
        for (var i = 0; i < parts.length; i++) {
            var part = parts[i];
            if (!part) { continue; }
            var index = part.indexOf("=");
            if (index == -1) { result[decodeURIComponent(part)] = ""; continue; } /* not a pair */

            var key = part.substring(0, index);
            var val = part.substring(index + 1);
            key = decodeURIComponent(key);
            val = decodeURIComponent(val);

            var r = false;
            if ((r = key.match(/(.*)\[\]$/))) {
                key = r[1];
                if (key in result) {
                    result[key].push(val);
                } else {
                    result[key] = [val];
                }
            } else {
                result[key] = val;
            }
        }
        return result;
    },

    changeHref: function(elm, newHref) {
        /* opera cannot do this with elements not being part of the page :/ */
        var ok = false;
        var e = OAT.$(elm);
        var node = e;
        while (node.parentNode) {
            node = node.parentNode;
            if (node == document.body) { ok = true; }
        }
        if (ok) {
            e.href = newHref;
        } else if (e.parentNode) {
            var oldParent = e.parentNode;
            var next = e.nextSibling;
            document.body.appendChild(e);
            e.href = newHref;
            OAT.Dom.unlink(e);
            oldParent.insertBefore(e, next);
        } else {
            document.body.appendChild(e);
            e.href = newHref;
            OAT.Dom.unlink(e);
        }
    },

    makePosition: function(elm) {
        var e = OAT.$(elm);
        if (OAT.Dom.style(e, "position") != "absolute") {
            e.style.position = "relative";
        }
    }
}

OAT.Style = { /* Style helper */
	include:function(file,force) {
		if (!file) return;
		file = OAT.Preferences.stylePath + file;
		if (!force) { /* prevent loading when already loaded */
			var styles = document.getElementsByTagName('link');
			var host = location.protocol + '//' + location.host;
			for (var i=0;i<styles.length;i++)
				if (file == styles[i].getAttribute('href') || host+file==styles[i].getAttribute('href'))
					return;
		}
		var elm = OAT.Dom.create("link");
		elm.rel = "stylesheet";
		elm.type = "text/css";
		elm.href = file;
		//document.getElementsByTagName("head")[0].appendChild(elm);
		jQuery("head").prepend(elm);
		if (gx.util.browser.isIE()){
			var reference = "Resources/English/GeneXusXEv2.css";
			for (var i=0; i <= styles.length - 1; i++){
				var id = styles[i].getAttribute("id");
				if (id === "gxtheme_css_reference"){
					reference = styles[i].getAttribute("href");
				}
			}
			jQuery("head").prepend(jQuery('<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="' + reference + '" />'))
		}
	},

	get:function(elm,property) {
		var element = OAT.$(elm);
		if (document.defaultView && document.defaultView.getComputedStyle) {
			var cs = document.defaultView.getComputedStyle(element,'');
			if (!cs) { return true; }
			return cs[property];
		} else {
			try {
				var out = element.currentStyle[property];
			} catch (e) {
				var out = element.getExpression(property);
			}
			return out;
		} 
	},
	
	background:function(element,src) {
		var elm = OAT.$(element);
		var png = !!src.toLowerCase().match(/png$/);
		if (png && OAT.Browser.isIE) {
			elm.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+src+"', sizingMethod='crop')";
		} else {
			elm.style.backgroundImage="url("+src+")";
		}
	},

	apply:function(something,obj) {
		var elm = OAT.$(something);
		if (!elm) {return;}
		for (var p in obj) { elm.style[p] = obj[p]; }
	},
	
	opacity:function(element,opacity) {
		var o = Math.max(opacity,0);
		var elm = OAT.$(element);
		if (OAT.Browser.isIE) {
			elm.style.filter = "alpha(opacity="+Math.round(o*100)+")";
		} else {
			elm.style.opacity = o;
		}
	}
}
OAT.Dom.style = OAT.Style.get; /* backward compatibility */
OAT.Dom.applyStyle = OAT.Style.apply; /* backward compatibility */

OAT.Browser = { /* Browser helper */
	isIE:OAT.Dom.isIE(),
	isIE6:OAT.Dom.isIE6(),
	isIE7:OAT.Dom.isIE7(),
	isGecko:OAT.Dom.isGecko(),
	isOpera:OAT.Dom.isOpera(),
	isKonqueror:OAT.Dom.isKonqueror(),
	isKHTML:OAT.Dom.isKHTML(),
	isWebKit:OAT.Dom.isWebKit(),
	isMac:OAT.Dom.isMac(),
	isLinux:OAT.Dom.isLinux(),
	isWindows:OAT.Dom.isWindows()
}

OAT.Event = { /* Event helper */
	attach:function(elm,event,callback) {
		var element = OAT.$(elm);
		if (element.addEventListener) {	/* gecko */
			element.addEventListener(event,callback,false);
		} else if (element.attachEvent) { /* ie */
			element.attachEvent("on"+event,callback);
		} else { /* ??? */
			element["on"+event] = callback;
		}
	},
	detach:function(elm,event,callback) {
		var element = OAT.$(elm);
		if (element.removeEventListener) { /* gecko */
			element.removeEventListener(event,callback,false);
		} else if (element.detachEvent) { /* ie */
			element.detachEvent("on"+event,callback);
		} else { /* ??? */
			element["on"+event] = false;
		}
	},
	source:function(event) {
		return (event.target ? event.target : event.srcElement);
	},
	cancel:function(event) {
		event.cancelBubble = true;
		if (event.stopPropagation) { event.stopPropagation(); }
	},
	position:function(event) {
		var scroll = OAT.Dom.getScroll();
		return [event.clientX+scroll[0],event.clientY+scroll[1]];
	},
	prevent:function(event) {
		if (event.preventDefault) { event.preventDefault(); }
		if (gx.util.browser.isIE()){
			event.returnValue = false;
		}
	}
}
OAT.Dom.attach = OAT.Event.attach;
OAT.Dom.detach = OAT.Event.detach;
OAT.Dom.source = OAT.Event.source;
OAT.Dom.eventPos = OAT.Event.position;
OAT.Dom.prevent = OAT.Event.prevent;

OAT.Loader = { /* first part of loader object */
    toolkitPath: false,
    loadOccurred: 0, /* was window.onload fired? */
    openAjax: false, /* OpenAjax.js included? */

    include: function(file) {
        var path = OAT.Loader.toolkitPath || "";
        var value = (typeof (file) == "object" ? file : [file]);
        for (var i = 0; i < value.length; i++) {
            var name = path + value[i];
            var script = document.createElement("script");
            script.src = name;
            document.getElementsByTagName("head")[0].appendChild(script);
        }
    },

    findPath: function() { /* scan for loader.js and OpenAjax.js */
        /*
        var head = document.getElementsByTagName("head")[0];
        var children = head.childNodes;
        for (var i = 0; i < children.length; i++) {
        var s = children[i];
        if (s.nodeType != 1 || s.tagName.toLowerCase() != "script") { continue; }
        var re = false;
        if ((re = s.src.match(/^(.*)loader\.js$/))) { OAT.Loader.toolkitPath = re[1]; }
        if ((re = s.src.match(/^(.*)OpenAjax.js$/))) { OAT.Loader.openAjax = true; }
        }
        */
        OAT.Loader.toolkitPath = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/js/', true);
    },

    startOpenAjax: function() {
        OpenAjax.hub.registerLibrary("oat", "http://www.openlinksw.com/oat", "1.0");
    }
}

OAT.MSG = { /* messages */
	DEBUG:0,
	OAT_DEBUG:0,
	OAT_LOAD:1,
	ANIMATION_STOP:2,
	TREE_EXPAND:3,
	TREE_COLLAPSE:4,
	DS_RECORD_PREADVANCE:5,
	DS_RECORD_ADVANCE:6,
	DS_PAGE_PREADVANCE:7,
	DS_PAGE_ADVANCE:8,
	AJAX_START:9,
	AJAX_ERROR:10,
	GD_START:11,
	GD_ABORT:12,
	GD_END:13,
	DOCK_DRAG:14,
	DOCK_REMOVE:15,
	SLB_OPENED:16,
	SLB_CLOSED:17,
	registry:[],
	attach:function(sender,msg,callback) {
		if (!sender) { return; }
		OAT.MSG.registry.push([sender,msg,callback]);
	},
	detach:function(sender,msg,callback) {
		if (!sender) { return; }
		var index = -1;
		for (var i=0;i<OAT.MSG.registry.length;i++) {
			var rec = OAT.MSG.registry[i];
			if (rec[0] == sender && rec[1] == msg && rec[2] == callback) { index = i; }
		}
		if (index != -1) { OAT.MSG.registry.splice(index,1); }
	},
	send:function(sender,msg,event) {
		for (var i=0;i<OAT.MSG.registry.length;i++) {
			var record = OAT.MSG.registry[i];
			var senderOK = (sender == record[0] || record[0] == "*");
			if (!senderOK) { continue; }
			var msgOK = (msg == record[1] || record[1] == "*");
			if (!msgOK && record[1].toString().match(/\*/)) { /* try regexp match */
				var re = new RegExp(record[1]);
				var str = "";
				for (var p in OAT.MSG) {
					var v = OAT.MSG[p];
					if (v == msg) { str = p; }
				}
				if (str.match(re)) { msgOK = true; }
			} /* regexp */
			if (msgOK) { record[2](sender,msg,event); }
		} /* for all listeners */
	} /* send message */
}

OAT.Debug = {
	data:[],
	attach:function(sender,msg) {
		OAT.MSG.attach(sender,msg,OAT.Debug.listen);
	},
	listen:function(sender,msg,event) {
		var name = "n/a";
		for (var p in OAT.MSG) {
			var v = OAT.MSG[p];
			if (typeof(v) == "number" && v == msg) { name = p; }
		}
		OAT.Debug.data.push([sender,name,event]);
	}
}
//OAT.Debug.attach("*",OAT.MSG.OAT_DEBUG);

/*
	OAT Load:
	1) check path
	2) listen for window.onload (or ask OpenAjax to do this)
	3) include bootstrap
	--
	4) prepare set of files to be included
	5) wait until all are loaded
	(5.5) if window is loaded, load proper window type
	6) everything is loaded and window.onload occurred:
		6a) execute window._init, if present
		6b) start declarative scanner, if present
		6c) execute window.init, if present
*/
OAT.Loader.findPath();
if (OAT.Loader.openAjax) { OAT.Loader.startOpenAjax(); }
//OAT.Event.attach(window,"load",function(){OAT.Loader.loadOccurred = 1;});

OAT.Event.attach(window, "load", function() {
    OAT.Loader.loadOccurred = 1;
    OAT.Loader.callListeners();
});

//OAT.Loader.include("bootstrap.js");
//OAT.Loader.include("jquery.tablePagination.0.5.min.js");

//OAT.Loader.include("jquery.columnfilter.js");

OAT.Loader.listeners = new Array();
OAT.Loader.addListener = function(callback) {
    if (OAT.Loader.loadOccurred == 1)
        callback(); // Habr�a que ser un poco m�s defensivo ac�, chequer que el typeof sea function
    else {
        OAT.Loader.listeners.push(callback);
    }
};

OAT.Loader.callListeners = function() {
    for (var i = 0, len = this.listeners.length; i < len; i++)
        OAT.Loader.listeners[i](); // Habr�a que ser un poco m�s defensivo ac�, chequear que el typeof sea function
};

}
