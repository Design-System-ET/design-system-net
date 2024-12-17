if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
OAT.Statistics = {
	list : [{
		longDesc : "Count",
		shortDesc : "COUNT",
		func : "count"
	}, {
		longDesc : "Sum",
		shortDesc : "SUM",
		func : "sum"
	}, {
		longDesc : "Product",
		shortDesc : "PRODUCT",
		func : "product"
	}, {
		longDesc : "Arithmetic mean",
		shortDesc : "MEAN",
		func : "amean"
	}, {
		longDesc : "Maximum",
		shortDesc : "MAX",
		func : "max"
	}, {
		longDesc : "Minimum",
		shortDesc : "MIN",
		func : "min"
	}, {
		longDesc : "Distinct values",
		shortDesc : "DISTINCT",
		func : "distinct"
	}, {
		longDesc : "Variance",
		shortDesc : "VAR",
		func : "variance"
	}, {
		longDesc : "Standard deviation",
		shortDesc : "STDDEV",
		func : "deviation"
	}, {
		longDesc : "Median",
		shortDesc : "MEDIAN",
		func : "median"
	}, {
		longDesc : "Mode",
		shortDesc : "MODE",
		func : "mode"
	}],
	count : function(a) {
		return a.length
	},
	sum : function(a) {
		if (a == "#FoE#") return "#NaV#"
		var allNuN = (a.length > 0);
		for(var c = 0, b = 0; b < a.length; b++){
			if (a[b] != "#NuN#") allNuN = false;
			if (!isNaN(a[b]))
				c += a[b];
		}
		if (allNuN) return "#NuN#"
		return c
	},
	product : function(a) {
		for(var c = 1, b = 0; b < a.length; b++)
		c *= a[b];
		return c
	},
	amean : function(a) {
		for(var c = 0, b = 0; b < a.length; b++)
		c += a[b];
		return c = a.length ? c / a.length : 0
	},
	max : function(a) {
		for(var c = Number.MIN_VALUE, b = 0; b < a.length; b++)a[b] > c && ( c = a[b]);
		return c
	},
	min : function(a) {
		for(var c = Number.MAX_VALUE, b = 0; b < a.length; b++)a[b] < c && ( c = a[b]);
		return c
	},
	distinct : function(a) {
		for(var c = 0, b = {}, d = 0; d < a.length; d++)
		b[a[d]] = 1;
		for(p in b)
		c++;
		return c
	},
	deviation : function(a) {
		a = OAT.Statistics.variance(a);
		return Math.sqrt(a)
	},
	variance : function(a) {
		if(2 > a.length)
			return 0;
		for(var c = 0, b = OAT.Statistics.amean(a), d = 0; d < a.length; d++)
		c += (a[d] - b) * (a[d] - b);
		return c / (a.length - 1)
	},
	median : function(a) {
		return a.sort(function(a,b){return a-b})[Math.floor(a.length / 2)]
	},
	mode : function(a) {
		for(var c = {}, b = 0; b < a.length; b++) {
			var d = a[b] + "";
			d in c ? c[d]++ : c[d] = 1
		}
		var a = 0, b = "", e;
		for(e in c) d = c[e], d > a && ( a = d, b = e);
		return parseFloat(b)
	}
};
try{
	OAT.Loader.featureLoaded("statistics");
} catch (ERROR) {
	
}

OAT.WinData = {
	TYPE_TEMPLATE : -1,
	TYPE_AUTO : 0,
	TYPE_MS : 1,
	TYPE_MAC : 2,
	TYPE_ROUND : 3,
	TYPE_RECT : 4,
	TYPE_ODS : 5
};
OAT.Win = function(a) {
	var b = this;
	this.options = {
		title : "",
		x : 0,
		y : 0,
		visibleButtons : "cmMfr",
		enabledButtons : "cmMfr",
		innerWidth : 0,
		innerHeight : 0,
		outerWidth : 350,
		outerHeight : !1,
		stackGroupBase : 100,
		type : OAT.WinData.TYPE_AUTO,
		template : !1,
		className : !1
	};
	for(var c in a)
	this.options[c] = a[c];
	b.dom = {
		buttons : {
			c : !1,
			m : !1,
			M : !1,
			f : !1,
			r : !1
		},
		container : !1,
		content : !1,
		title : !1,
		caption : !1,
		status : !1,
		resizeContainer : !1
	};
	b.moveTo = function(a, c) {
		b.dom.container.style.left = a + "px";
		b.dom.container.style.top = c + "px"
	};
	b.innerResizeTo = function() {
	};
	b.outerResizeTo = function() {
	};
	b.show = function() {
		document.body.appendChild(b.dom.container);
		OAT.Dom.show(b.dom.container)
	};
	b.hide = function() {
		OAT.Dom.hide(b.dom.container)
	};
	b.close = b.hide();
	b.minimize = function() {
	};
	b.maximize = function() {
	};
	b.flip = function() {
	};
	b.accomodate = function(a) {
		a = OAT.Dom.getWH(a);
		b.innerResizeTo(a[0], a[1])
	};
	b.options.type == OAT.WinData.TYPE_TEMPLATE && OAT.WinTemplate(b);
	b.options.type == OAT.WinData.TYPE_MS && OAT.WinMS(b);
	b.options.type == OAT.WinData.TYPE_MAC && OAT.WinMAC(b);
	b.options.type == OAT.WinData.TYPE_RECT && OAT.WinRECT(b);
	b.options.type == OAT.WinData.TYPE_ROUND && OAT.WinROUND(b);
	b.options.type == OAT.WinData.TYPE_ODS && OAT.WinODS(b);
	-1 != b.options.enabledButtons.indexOf("m") && b.dom.buttons.m && OAT.Dom.attach(b.dom.buttons.m, "click", b.minimize);
	-1 != b.options.enabledButtons.indexOf("M") && b.dom.buttons.M && OAT.Dom.attach(b.dom.buttons.M, "click", b.maximize);
	-1 != b.options.enabledButtons.indexOf("c") && b.dom.buttons.c && OAT.Dom.attach(b.dom.buttons.c, "click", b.hide);
	-1 != b.options.enabledButtons.indexOf("f") && b.dom.buttons.f && OAT.Dom.attach(b.dom.buttons.f, "click", b.flip);
	-1 != b.options.enabledButtons.indexOf("r") && b.dom.buttons.r && OAT.Resize.create(b.dom.buttons.r, b.dom.resizeContainer, OAT.Resize.TYPE_XY);
	b.dom.title && OAT.Drag.create(b.dom.title, b.dom.container);
	b.moveTo(b.options.x, b.options.y);
	(b.options.outerWidth || b.options.outerHeight) && b.outerResizeTo(b.options.outerWidth, b.options.outerHeight);
	(b.options.innerWidth || b.options.innerHeight) && b.innerResizeTo(b.options.innerWidth, b.options.innerHeight);
	b.dom.caption && (b.dom.caption.innerHTML = b.options.title);
	b.options.stackGroupBase && OAT.WinManager.addWindow(b.options.stackGroupBase, b.dom.container);
	b.hide()
};
OAT.WinTemplate = function(a) {
	var b = a.options.template;
	if(b) {
		for(var c = "function" == typeof b ? b() : OAT.$(b).cloneNode(!0), g = {
			oat_w_ctr : "container",
			oat_w_title_ctr : "title",
			oat_w_title_t_ctr : "caption",
			oat_w_content : "content",
			oat_w_max_b : ["buttons", "M"],
			oat_w_min_b : ["buttons", "m"],
			oat_w_close_b : ["buttons", "c"],
			oat_w_flip_b : ["buttons", "f"],
			oat_w_resize_handle : ["buttons", "r"]
		}, e = [c], b = c.getElementsByTagName("*"), c = 0; c < b.length; c++)
		e.push(b[c]);
		for( c = 0; c < e.length; c++) {
			var b = e[c], f;
			for(f in g)
			if(OAT.Dom.isClass(b, f)) {
				var d = g[f];
				d instanceof Array ? a.dom[d[0]][d[1]] = b : a.dom[d] = b
			}
		}
		a.moveTo = function(b, c) {
			a.dom.container.style.left = b + "px";
			a.dom.container.style.top = c + "px"
		};
		a.outerResizeTo = function(b, c) {
			a.dom.container.style.width = b ? b + "px" : "auto";
			a.dom.container.style.height = c ? c + "px" : "auto"
		};
		a.innerResizeTo = function(b, c) {
			a.dom.content.style.width = b ? b + "px" : "auto";
			a.dom.content.style.height = c ? c + "px" : "auto"
		}
	} else
		alert("OAT Window cannot be created, as a template is required but not specified!")
};
OAT.WinMS = function(a) {
	OAT.Style.include("winms.css");
	a.dom.container = OAT.Dom.create("div", {
		position : "absolute"
	}, "oat_winms_container");
	a.dom.resizeContainer = a.dom.container;
	a.dom.content = OAT.Dom.create("div", {}, "oat_winms_content");
	a.dom.title = OAT.Dom.create("div", {}, "oat_winms_title");
	a.dom.caption = OAT.Dom.create("span", {}, "oat_winms_caption");
	a.dom.status = OAT.Dom.create("div", {}, "oat_winms_status");
	OAT.Dom.append([a.dom.container, a.dom.title, a.dom.content, a.dom.status]);
	-1 != a.options.visibleButtons.indexOf("c") && (a.dom.buttons.c = OAT.Dom.create("div", {}, "oat_winms_close_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.c]));
	-1 != a.options.visibleButtons.indexOf("M") && (a.dom.buttons.M = OAT.Dom.create("div", {}, "oat_winms_max_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.M]));
	-1 != a.options.visibleButtons.indexOf("m") && (a.dom.buttons.m = OAT.Dom.create("div", {}, "oat_winms_min_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.m]));
	-1 != a.options.visibleButtons.indexOf("r") && (a.dom.buttons.r = OAT.Dom.create("div", {}, "oat_winms_resize_b"), OAT.Dom.append([a.dom.container, a.dom.buttons.r]));
	OAT.Dom.append([a.dom.title, a.dom.caption]);
	a.outerResizeTo = function(b, c) {
		a.dom.container.style.width = b ? b + "px" : "auto";
		a.dom.container.style.height = c ? c + "px" : "auto"
	}
};
OAT.WinMAC = function(a) {
	OAT.Style.include("winmac.css");
	a.dom.container = OAT.Dom.create("div", {
		position : "absolute"
	}, "oat_winmac_container");
	a.dom.resizeContainer = a.dom.container;
	a.dom.content = OAT.Dom.create("div", {}, "oat_winmac_content");
	a.dom.title = OAT.Dom.create("div", {}, "oat_winmac_title");
	a.dom.caption = OAT.Dom.create("span", {}, "oat_winmac_caption");
	a.dom.status = OAT.Dom.create("div", {}, "oat_winmac_status");
	OAT.Dom.append([a.dom.container, a.dom.title, a.dom.content, a.dom.status]);
	a.dom.buttons.lc = OAT.Dom.create("div", {}, "oat_winmac_leftCorner");
	OAT.Dom.append([a.dom.title, a.dom.buttons.lc]);
	a.dom.buttons.rc = OAT.Dom.create("div", {}, "oat_winmac_rightCorner");
	OAT.Dom.append([a.dom.title, a.dom.buttons.rc]);
	-1 != a.options.visibleButtons.indexOf("c") && (a.dom.buttons.c = OAT.Dom.create("div", {}, "oat_winmac_close_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.c]));
	-1 != a.options.visibleButtons.indexOf("M") && (a.dom.buttons.M = OAT.Dom.create("div", {}, "oat_winmac_max_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.M])); -1 != a.options.visibleButtons.indexOf("m") && (a.dom.buttons.m = OAT.Dom.create("div", {}, "oat_winmac_min_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.m]));
	-1 != a.options.visibleButtons.indexOf("r") && (a.dom.buttons.r = OAT.Dom.create("div", {}, "oat_winmac_resize_b"), OAT.Dom.append([a.dom.container, a.dom.buttons.r]));
	OAT.Dom.append([a.dom.title, a.dom.caption]);
	a.outerResizeTo = function(b, c) {
		a.dom.container.style.width = b ? b + "px" : "auto";
		a.dom.container.style.height = c ? c - 8 + "px" : "auto"
	}
};
OAT.WinRECT = function(a) {
	OAT.Style.include("winrect.css");
	a.dom.container = OAT.Dom.create("div", {
		position : "absolute"
	}, "oat_winrect_container");
	a.dom.resizeContainer = a.dom.container;
	a.dom.content = OAT.Dom.create("div", {}, "oat_winrect_content");
	a.dom.title = OAT.Dom.create("div", {}, "oat_winrect_title");
	a.dom.caption = OAT.Dom.create("span", {}, "oat_winrect_caption");
	a.dom.status = OAT.Dom.create("div", {}, "oat_winrect_status");
	OAT.Dom.append([a.dom.container, a.dom.title, a.dom.content, a.dom.status]);
	-1 != a.options.visibleButtons.indexOf("c") && (a.dom.buttons.c = OAT.Dom.create("div", {}, "oat_winrect_close_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.c]));
	//-1 != a.options.visibleButtons.indexOf("r") && (a.dom.buttons.r = OAT.Dom.create("div", {}, "oat_winrect_resize_b"), OAT.Dom.append([a.dom.container, a.dom.buttons.r]));
	OAT.Dom.append([a.dom.title, a.dom.caption]);
	a.outerResizeTo = function(b, c) {
		a.dom.container.style.width = b ? b + "px" : "auto";
		a.dom.container.style.height = c ? c + "px" : "auto"
	}
};
OAT.WinROUND = function(a) {
	OAT.Style.include("winround.css");
	a.dom.container = OAT.Dom.create("div", {
		position : "absolute"
	}, "oat_winround_container");
	a.dom.resizeContainer = a.dom.container;
	a.dom.table = OAT.Dom.create("table", {}, "oat_winround_wrapper");
	a.dom.tr_t = OAT.Dom.create("tr", {});
	a.dom.td_lt = OAT.Dom.create("td", {}, "oat_winround_lt");
	a.dom.td_t = OAT.Dom.create("td", {}, "oat_winround_t");
	a.dom.td_rt = OAT.Dom.create("td", {}, "oat_winround_rt");
	a.dom.tr_m = OAT.Dom.create("tr", {});
	a.dom.td_l = OAT.Dom.create("td", {}, "oat_winround_l");
	a.dom.td_m = OAT.Dom.create("td", {}, "oat_winround_m");
	a.dom.td_r = OAT.Dom.create("td", {}, "oat_winround_r");
	a.dom.tr_b = OAT.Dom.create("tr", {});
	a.dom.td_lb = OAT.Dom.create("td", {}, "oat_winround_lb");
	a.dom.td_b = OAT.Dom.create("td", {}, "oat_winround_b");
	a.dom.td_rb = OAT.Dom.create("td", {}, "oat_winround_rb");
	a.dom.content = OAT.Dom.create("div", {}, "oat_winround_content");
	a.dom.title = OAT.Dom.create("div", {}, "oat_winround_title");
	a.dom.caption = OAT.Dom.create("span", {}, "oat_winround_caption");
	a.dom.status = OAT.Dom.create("div", {}, "oat_winround_status");
	-1 != a.options.visibleButtons.indexOf("c") && (a.dom.buttons.c = OAT.Dom.create("div", {}, "oat_winround_close_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.c]));
	-1 != a.options.visibleButtons.indexOf("r") && (a.dom.buttons.r = OAT.Dom.create("div", {}, "oat_winround_resize_b"), OAT.Dom.append([a.dom.td_rb, a.dom.buttons.r]));
	OAT.Dom.append([a.dom.title, a.dom.caption]);
	OAT.Browser.isIE ? (a.dom.container.innerHTML = '<table class="oat_winround_wrapper"><tr><td class="oat_winround_lt"></td><td class="oat_winround_t"></td><td class="oat_winround_rt"></td></tr><tr><td class="oat_winround_l"></td><td class="oat_winround_m"></td><td class="oat_winround_r"></td></tr><tr><td class="oat_winround_lb"></td><td class="oat_winround_b"></td><td class="oat_winround_rb"></td></tr></table>', a.dom.container.childNodes[0].childNodes[0].childNodes[0].childNodes[1].appendChild(a.dom.title), a.dom.container.childNodes[0].childNodes[0].childNodes[1].childNodes[1].appendChild(a.dom.content), a.dom.container.childNodes[0].childNodes[0].childNodes[2].childNodes[1].appendChild(a.dom.status), a.dom.container.childNodes[0].childNodes[0].childNodes[2].childNodes[2].appendChild(a.dom.buttons.r)) : (OAT.Dom.append([a.dom.tr_t, a.dom.td_lt, a.dom.td_t, a.dom.td_rt]), OAT.Dom.append([a.dom.tr_m, a.dom.td_l, a.dom.td_m, a.dom.td_r]), OAT.Dom.append([a.dom.tr_b, a.dom.td_lb, a.dom.td_b, a.dom.td_rb]), OAT.Dom.append([a.dom.table, a.dom.tr_t, a.dom.tr_m, a.dom.tr_b]), OAT.Dom.append([a.dom.td_t, a.dom.title]), OAT.Dom.append([a.dom.td_m, a.dom.content]), OAT.Dom.append([a.dom.td_b, a.dom.status]), OAT.Dom.append([a.dom.container, a.dom.table]));
	a.outerResizeTo = function(b, c) {
		a.dom.container.style.width = b ? b + "px" : "auto";
		a.dom.container.style.height = c ? c + "px" : "auto"
	}
};
OAT.WinODS = function(a) {
	OAT.Style.include("winods.css");
	a.dom.container = OAT.Dom.create("div", {
		position : "absolute"
	}, "oat_winods_container");
	a.dom.resizeContainer = a.dom.container;
	a.dom.content = OAT.Dom.create("div", {}, "oat_winods_content");
	a.dom.title = OAT.Dom.create("div", {}, "oat_winods_title");
	a.dom.caption = OAT.Dom.create("span", {}, "oat_winods_caption");
	a.dom.status = OAT.Dom.create("div", {}, "oat_winods_status");
	OAT.Dom.append([a.dom.container, a.dom.title, a.dom.content, a.dom.status]);
	-1 != a.options.visibleButtons.indexOf("c") && (a.dom.buttons.c = OAT.Dom.create("div", {}, "oat_winods_close_b"), OAT.Dom.append([a.dom.title, a.dom.buttons.c]));
	-1 != a.options.visibleButtons.indexOf("r") && (a.dom.buttons.r = OAT.Dom.create("div", {}, "oat_winods_resize_b"), OAT.Dom.append([a.dom.container, a.dom.buttons.r]));
	OAT.Dom.append([a.dom.title, a.dom.caption]);
	a.outerResizeTo = function(b, c) {
		a.dom.container.style.width = b ? b + "px" : "auto";
		a.dom.container.style.height = c ? c + "px" : "auto"
	}
};
OAT.WinManager = {
	stackingGroups : {},
	addWindow : function(a, b) {
		if( a in OAT.WinManager.stackingGroups)
			var c = OAT.WinManager.stackingGroups[a];
		else
			c = new OAT.Layers(a), OAT.WinManager.stackingGroups[a] = c
		c.addLayer(b, "click")
	},
	removeWindow : function() {
		if( zI in OAT.WinManager.stackingGroups)
			var a = OAT.WinManager.stackingGroups[zI];
		else
			a = new OAT.Layers(zI), OAT.WinManager.stackingGroups[zI] = a
		a.removeLayer(container)
	}
};
try {
OAT.Loader.featureLoaded("win");
} catch (ERROR) {
	
}

OAT.Resize={TYPE_X:1,TYPE_Y:2,TYPE_XY:3,element:[],mouse_x:0,mouse_y:0,move:function(a){if(OAT.Resize.element.length){for(var e=a.clientX-OAT.Resize.mouse_x,f=a.clientY-OAT.Resize.mouse_y,b=1,c=0;c<OAT.Resize.element.length;c++){var d=OAT.Resize.element[c][0],d=OAT.Dom.getWH(d),i=OAT.Resize.element[c][2],g=e,h=f;switch(OAT.Resize.element[c][1]){case OAT.Resize.TYPE_X:h=0;break;case -OAT.Resize.TYPE_X:g=-e;h=0;break;case OAT.Resize.TYPE_Y:g=0;break;case -OAT.Resize.TYPE_Y:g=0;h=-f;break;case -OAT.Resize.TYPE_XY:g=
-e,h=-f}i(d[0]+g,d[1]+h)&&(b=0)}if(b){for(c=0;c<OAT.Resize.element.length;c++)switch(d=OAT.Resize.element[c][0],OAT.Resize.element[c][1]){case OAT.Resize.TYPE_X:OAT.Dom.resizeBy(d,e,0);break;case -OAT.Resize.TYPE_X:OAT.Dom.resizeBy(d,-e,0);break;case OAT.Resize.TYPE_Y:OAT.Dom.resizeBy(d,0,f);break;case -OAT.Resize.TYPE_Y:OAT.Dom.resizeBy(d,0,-f);break;case OAT.Resize.TYPE_XY:OAT.Dom.resizeBy(d,e,f);break;case -OAT.Resize.TYPE_XY:OAT.Dom.resizeBy(d,-e,-f)}OAT.Resize.mouse_x=a.clientX;OAT.Resize.mouse_y=
a.clientY}}},up:function(){for(var a=0;a<OAT.Resize.element.length;a++)OAT.Resize.element[a][3]();OAT.Resize.element=[]},create:function(a,e,f,b,c){var d=OAT.$(a),a=OAT.$(e),e=function(){return!1},i=function(){return!1};b&&(e=b);c&&(i=c);switch(f){case OAT.Resize.TYPE_XY:d.style.cursor="nw-resize";break;case OAT.Resize.TYPE_X:d.style.cursor="w-resize";break;case OAT.Resize.TYPE_Y:d.style.cursor="n-resize"}b=function(a){OAT.Resize.element=d._Resize_movers;OAT.Resize.mouse_x=a.clientX;OAT.Resize.mouse_y=a.clientY;
a.cancelBubble=!0};d._Resize_movers||(OAT.Dom.attach(d,"mousedown",b),d._Resize_movers=[]);d._Resize_movers.push([a,f,e,i])},remove:function(a,e){var f=OAT.$(a);OAT.$(e);if(f._Resize_movers){for(var b=-1,c=0;c<f._Resize_movers.length;c++)f._Resize_movers[c][0]==e&&(b=c);-1!=b&&f._Resize_movers.splice(b,1)}},removeAll:function(a){a=OAT.$(a);a._Resize_movers&&(a._Resize_movers=[])},createDefault:function(a,e,f){if(OAT.Preferences.allowDefaultResize){var b=OAT.Dom.create("div",{position:"absolute",width:"10px",
height:"10px",right:"0px",fontSize:"1px",bottom:"0px",backgroundImage:"url("+OAT.Preferences.imagePath+"resize.gif)"});a.appendChild(b);OAT.Resize.create(b,a,OAT.Resize.TYPE_XY,e,f);OAT.Dom.hide(b);var c=function(){b._Resize_pending&&OAT.Dom.hide(b)};OAT.Dom.attach(a,"mouseover",function(){OAT.Dom.show(b);b._Resize_pending=0});OAT.Dom.attach(a,"mouseout",function(){b._Resize_pending=1;setTimeout(c,2E3)})}}};OAT.Dom.attach(document,"mousemove",OAT.Resize.move);OAT.Dom.attach(document,"mouseup",OAT.Resize.up);
try {
OAT.Loader.featureLoaded("resize");
} catch (ERROR) {
	
}

OAT.Ajax = {
	number : 0,
	httpError : 1,
	startRef : !1,
	endRef : !1,
	errorRef : !1,
	cancel : !1,
	dialog : !1,
	GET : 1,
	POST : 2,
	SOAP : 4,
	PUT : 8,
	MKCOL : 16,
	PROPFIND : 32,
	PROPPATCH : 64,
	AUTH_BASIC : 1024,
	AUTH_DIGEST : 512,
	TYPE_TEXT : 0,
	TYPE_XML : 1,
	user : "",
	password : "",
	imagePath : OAT.Preferences.imagePath,
	requests : [],
	startNotify : function() {
		if(OAT.Ajax.startRef)
			OAT.Ajax.startRef();
		else if(-1 != OAT.Loader.loadedLibs.find("dialog")) {
			if(!OAT.Ajax.dialog) {
				var a = OAT.Ajax.imagePath;
				"/" == a.charAt(a.length - 1) && a.substring(0, a.length - 1);
				a = OAT.Dom.create("div");
				a.innerHTML = "Ajax call in progress...";
				var c = OAT.Dom.create("div"), d = OAT.Dom.create("img");
				d.setAttribute("src", OAT.Ajax.imagePath + "/progress.gif");
				c.appendChild(d);
				a.appendChild(c);
				OAT.Ajax.dialog = new OAT.Dialog("Please wait", a, {
					width : 260,
					modal : 0,
					zIndex : 1001,
					resize : 0,
					imagePath : OAT.Ajax.imagePath + "/"
				});
				OAT.Ajax.dialog.ok = OAT.Ajax.dialog.hide;
				OAT.Ajax.dialog.cancel = OAT.Ajax.dialog.hide;
				OAT.Ajax.setCancel(OAT.Ajax.dialog.cancelBtn)
			}
			OAT.Ajax.dialog.show()
		}
	},
	endNotify : function() {
		OAT.Ajax.endRef ? OAT.Ajax.endRef() : -1 != OAT.Loader.loadedLibs.find("dialog") && OAT.Ajax.dialog && OAT.Ajax.dialog.hide()
	},
	command : function(a, c, d, g, j, f) {(OAT.Ajax.startRef || OAT.Preferences.showAjax) && !OAT.Ajax.number && OAT.Ajax.startNotify();
		OAT.Ajax.number++;
		var b = new OAT.XMLHTTP, e = null, h = {
			state : 1
		};
		OAT.Ajax.requests.push(h);
		b.setResponse(function() {
			if(h.state && 4 == b.getReadyState()) {
				var a = b.getAllResponseHeaders();
				OAT.Ajax.number--;
				(OAT.Ajax.endRef || OAT.Preferences.showAjax) && !OAT.Ajax.number && OAT.Ajax.endNotify();
				if("2" == b.getStatus().toString().charAt(0) || 0 == b.getStatus())
					if(j == OAT.Ajax.TYPE_TEXT)
						g(b.getResponseText(), a);
					else {
						if(OAT.Dom.isIE() || OAT.Dom.isWebKit()) {
							xmlStr = b.getResponseText();
							var c = OAT.Xml.createXmlDoc(xmlStr)
						} else
							c = b.getResponseXML();
						g(c, a)
					}
				else
					OAT.Ajax.errorRef ? OAT.Ajax.errorRef(b.getStatus(), b.getResponseText(), a) : OAT.Ajax.httpError && confirm("Problem retrieving data, status=" + b.getStatus() + ", do you want to see returned problem description?") && alert(b.getResponseText())
			}
		});
		e = d();
		a & OAT.Ajax.GET && ( d = /\?/.test(c) ? c + "&" + e : c + "?" + e, b.open("GET", d, !0));
		a & OAT.Ajax.POST && (b.open("POST", c, !0), b.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"));
		a & OAT.Ajax.SOAP && b.open("POST", c, !0);
		a & OAT.Ajax.PUT && b.open("PUT", c, !0);
		a & OAT.Ajax.MKCOL && b.open("MKCOL", c, !0);
		a & OAT.Ajax.PROPFIND && b.open("PROPFIND", c, !0);
		a & OAT.Ajax.PROPPATCH && b.open("PROPPATCH", c, !0);
		a & OAT.Ajax.AUTH_BASIC && b.setRequestHeader("Authorization", "Basic " + OAT.Crypto.base64e(OAT.Ajax.user + ":" + OAT.Ajax.password));
		if(f)
			for(var i in f)
			b.setRequestHeader(i, f[i]);
		b.send(a & OAT.Ajax.GET ? null : e)
	},
	setStart : function(a) {
		OAT.Ajax.startRef = a
	},
	setEnd : function(a) {
		OAT.Ajax.endRef = a
	},
	handleError : function(a) {
		OAT.Ajax.errorRef = a
	},
	setCancel : function(a) {
		a = OAT.$(a);
		OAT.Ajax.cancel = a;
		OAT.Dom.attach(a, "click", OAT.Ajax.cancelAll)
	},
	cancelAll : function() {
		for(var a = 0; a < OAT.Ajax.requests.length; a++)
		OAT.Ajax.requests[a].state && (OAT.Ajax.requests[a].state = 0);
		(OAT.Ajax.endRef || OAT.Preferences.showAjax) && OAT.Ajax.endNotify();
		OAT.Ajax.number = 0
	}
};
OAT.XMLHTTP = function() {
	this.obj = this.iframe = !1;
	this.open = function(a, c, d) {
		this.iframe ? this.temp_src = c : this.obj.open(a, c, d)
	};
	this.send = function(a) {
		this.iframe ? this.ifr.src = this.temp_src : this.obj.send(a)
	};
	this.setResponse = function(a) {
		this.iframe ? OAT.Dom.attach(this.ifr, "load", a) : this.obj.onreadystatechange = a
	};
	this.getResponseText = function() {
		return this.iframe ? this.ifr.contentWindow.document.body.innerHTML : this.obj.responseText
	};
	this.getResponseXML = function() {
		return this.iframe ? (alert("IFRAME mode active -> XML data not supported"), "") : this.obj.responseXML
	};
	this.getReadyState = function() {
		return this.iframe ? 4 : this.obj.readyState
	};
	this.getStatus = function() {
		return this.iframe ? 200 : this.obj.status
	};
	this.setRequestHeader = function(a, c) {
		this.iframe || this.obj.setRequestHeader(a, c)
	};
	this.getAllResponseHeaders = function() {
		return !this.iframe ? this.obj.getAllResponseHeaders() : {}
	};
	this.isIframe = function() {
		return this.iframe
	};
	window.XMLHttpRequest ? this.obj = new XMLHttpRequest : window.ActiveXObject && (this.obj = new ActiveXObject("Microsoft.XMLHTTP"));
	this.obj || (this.iframe = !0, this.ifr = OAT.Dom.create("iframe"), this.ifr.style.display = "none", this.ifr.src = "javascript:;", document.body.appendChild(this.ifr))
};
OAT.XMLHTTP_supported = function() {
	return !(new OAT.XMLHTTP).isIframe()
};
try {
OAT.Loader.featureLoaded("ajax");
} catch (ERROR) {
	
}


OAT.AnchorData={active:!1,window:!1};
OAT.Anchor={
	appendContent:function(b){
		if(b.content&&b.window)
		{	
			"function"==typeof b.content&&(b.content=b.content());
			var d=b.window;
			OAT.Dom.clear(d.dom.content);
			d.dom.content.appendChild(b.content);OAT.Anchor.fixSize(d)
		}
	},
	callForData:function(b,d){
		var e=b.window;
		b.stat=1;
		b.title&&(e.dom.caption.innerHTML=b.title);
		b.status&&(e.dom.status.innerHTML=b.status);
		var a=b.datasource;
		if(a)
		{	a.connection=b.connection;var f=b.elm.innerHTML,c=function(){e.dom.caption.innerHTML=b.elm.innerHTML;
			if(b.title)e.dom.caption.innerHTML=b.title};a.bindRecord(c);a.bindEmpty(c)
		}
		switch(b.result_control){case "grid":c=new OAT.FormObject.grid(0,0,0,1);c.showAll=!0;b.content=c.elm;c.elm.style.position="relative";c.init();a.bindRecord(c.bindRecordCallback);a.bindPage(c.bindPageCallback);a.bindHeader(c.bindHeaderCallback);break;case "form":var h=!1;b.content=OAT.Dom.create("div");
		h=new OAT.Form(b.content,{onDone:function(){e.resizeTo(h.totalWidth+5,h.totalHeight+5);b.anchorTo(d[0],d[1])}});a.bindFile(function(a){a=OAT.Xml.createXmlDoc(a);
h.createFromXML(a)});break;case "timeline":c=new OAT.FormObject.timeline(0,20,0);b.content=c.elm;c.elm.style.position="relative";c.elm.style.width=b.width-5+"px";c.elm.style.height=b.height-65+"px";c.init();for(var i=0;i<c.datasources[0].fieldSets.length;i++)c.datasources[0].fieldSets[i].realIndexes=[i];a.bindPage(c.bindPageCallback)}OAT.Anchor.appendContent(b);if(a){a.options.query=a.options.query.replace(/\$link_name/g,f);b.connection.options.endpoint=b.href;b.connection.options.url=b.href;switch(a.type){case OAT.DataSourceData.TYPE_SPARQL:f=
new OAT.SparqlQuery;f.fromString(a.options.query);f=f.variables.length?"format=xml":"format=rdf";a.options.query="query="+encodeURIComponent(a.options.query)+"&"+f;break;case OAT.DataSourceData.TYPE_GDATA:a.options.query=a.options.query?"q="+encodeURIComponent(a.options.query):""}a.advanceRecord(0)}},fixSize:function(b){setTimeout(
		function() {
			/*if(OAT.AJAX.requests.length)
				OAT.Anchor.fixSize(b);
			else
				for(var d = OAT.Dom.getWH(b.dom.content)[1]; OAT.Dom.getWH(b.dom.content)[1] + 50 > OAT.Dom.getWH(b.dom.container)[1]; ) {650 > OAT.Dom.getWH(b.dom.container)[0] && (b.dom.container.style.width = OAT.Dom.getWH(b.dom.container)[0] + 100 + "px");
					if(d == OAT.Dom.getWH(b.dom.content)[1]) {
						b.dom.container.style.width = OAT.Dom.getWH(b.dom.container)[0] - 100 + "px";
						300 < OAT.Dom.getWH(b.dom.content)[1] && (b.dom.content.style.height = "300px", b.dom.content.style.overflow = "auto");
						b.dom.container.style.height = OAT.Dom.getWH(b.dom.content)[1] + 40 + "px";
						break
					}
					d = OAT.Dom.getWH(b.dom.content)[1]
				}*/
		}
,50)}
		,assign:function(b,d){
			var e = OAT.$(b),
			 a = {
					href : !1,
					newHref : "javascript:void(0)",
					connection : !1,
					datasource : !1,
					content : !1,
					status : !1,
					title : !1,
					result_control : "grid",
					activation : "hover",
					width : 340,
					height : !1,
					elm : e,
					window : !1,
					arrow : !1,
					type : OAT.WinData.TYPE_RECT,
					visibleButtons : "cr",
					enabledButtons : "cr",
					template : !1
				}, 
			f;
			for(f in d)
				a[f] = d[f];
			var c = new OAT.Win({
				outerWidth : a.width,
				outerHeight : a.height,
				title : "Loading...",
				type : a.type,
				status : a.status,
				visibleButtons : a.visibleButtons,
				enabledButtons : a.enabledButtons,
				template : a.template
			});
			OAT.Dom.attach(c.dom.container, "mouseover", function() {
				var a = OAT.AnchorData.active; a && "hover" == a.activation && a.endClose()
			});
			OAT.Dom.attach(c.dom.container, "mouseout", function() {
				var a = OAT.AnchorData.active;
				a && "hover" == a.activation && a.startClose()
			});
	f = OAT.Dom.create("div", {});
	//OAT.Dom.append([c.dom.container, f]);

		//a.arrow = f;
		a.window = c;
		c.close = function() {
			OAT.Dom.hide(c.dom.container)
		};
		c.onclose = c.close;
		c.close();
		a.stat = 0;
		!a.href && "href" in e && (a.href = e.href);
		"a" == e.tagName.toString().toLowerCase() && OAT.Dom.changeHref(e, a.newHref);
		a.displayRef = function(b) {
			var c = a.window;
			c.hide();
			OAT.AnchorData.active = a;
			b = OAT.Dom.eventPos(b);
			OAT.AnchorData.window = c;
			a.stat ? OAT.Anchor.appendContent(a) : OAT.Anchor.callForData(a, b);
			a.activation == "focus" && ( b = OAT.Dom.position(e));
			a.anchorTo(b[0], b[1]);
			c.show();
			a.anchorTo(b[0], b[1])
		};
		a.displayRef2 = function(b) {
			var c = a.window;
			c.hide();
			OAT.AnchorData.active = a;
			//b = OAT.Dom.eventPos(b);
			OAT.AnchorData.window = c;
			a.stat ? OAT.Anchor.appendContent(a) : OAT.Anchor.callForData(a, b);
			a.activation == "focus" && ( b = OAT.Dom.position(e));
			a.anchorTo(b[0], b[1]);
			c.show();
			a.anchorTo(b[0], b[1])
		};
		a.anchorTo = function(b, c) {
			var e = a.window, d = OAT.Dom.getFreeSpace(b, c), f = OAT.Dom.getWH(e.dom.container);
			if(d[1])
				var j = c - 30 - f[1], g = "bottom";
			else {
				j = c + 30;
				g = "top"
			}
			if(d[0]) {
				d = b + 20 - f[0];
				g = g + "right"
			} else {
				d = b - 30;
				g = g + "left"
			}d < 0 && ( d = 10);
			j < 0 && ( j = 10);
			//OAT.Dom.addClass(a.arrow, "oat_anchor_arrow_" + g);
			e.moveTo(d, j - 20)
		};
		a.closeRef=function(){
			
			if(a.closeFlag) {
				a.window.hide();
				OAT.AnchorData.active = false
			}};
			a.close=function(){a.window.hide()};
			a.startClose=function()
			{a.closeFlag=1;setTimeout(a.closeRef,1E3)};
			a.endClose=function()
			{a.closeFlag=0};
			switch(a.activation)
			{case "hover":OAT.Dom.attach(e,"mouseover",a.displayRef);
			OAT.Dom.attach(e,"mouseout",a.startClose);
			break;case "click":OAT.Dom.attach(e,"click",a.displayRef);
			break;case "dblclick":OAT.Dom.attach(e,"dblclick",a.displayRef);
			break;case "focus":OAT.Dom.attach(e,"focus",a.displayRef),OAT.Dom.attach(e,"blur",a.close)}
			
			return a
			
			},
			
			
			close:function(b,d){
				b=OAT.$(b);
				"BODY"!=b.tagName&&(b.className.match(/^oat_win.+_container$/)?(OAT.Dom.hide(b),d&&this.close(b.parentNode)):this.close(b.parentNode))
			},
			openAnchor:function(){
				var c = this.window;
				c.hide();
				OAT.AnchorData.active = this;
				b = OAT.Dom.eventPos(b);
				OAT.AnchorData.window = c;
				a.stat ? OAT.Anchor.appendContent(a) : OAT.Anchor.callForData(a, b);
				a.activation == "focus" && ( b = OAT.Dom.position(e));
				a.anchorTo(b[0], b[1]);
				c.show();
				a.anchorTo(b[0], b[1])
			}
		};
try{
OAT.Loader.featureLoaded("anchor");
} catch (ERROR) {
	
}


OAT.Animation = function(f, d) {
	var b = this;
	this.elm = OAT.$(f);
	this.options = {
		delay : 50,
		startFunction : function() {
		},
		conditionFunction : function() {
		},
		stepFunction : function() {
		},
		stopFunction : function() {
		}
	};
	for(var e in d)
	b.options[e] = d[e];
	this.step = function() {
		setTimeout(function() {
			b.running && (b.options.conditionFunction(b) ? (b.running = 0, b.options.stopFunction(b), OAT.MSG.send(b, OAT.MSG.ANIMATION_STOP, b)) : (b.options.stepFunction(b), b.step(b)))
		}, b.options.delay)
	};
	this.start = function() {
		b.running = 1;
		b.options.startFunction(b);
		b.step()
	};
	this.stop = function() {
		b.running = 0
	}
};
OAT.AnimationSize = function(f, d) {
	var b = this;
	this.options = {
		width : -1,
		height : -1,
		delay : 50,
		speed : 1
	};
	for(var e in d)
	b.options[e] = d[e];
	this.animation = new OAT.Animation(f, {
		delay : b.options.delay,
		startFunction : function(a) {
			a.stepX = 0;
			a.stepY = 0;
			var c = OAT.Dom.getWH(a.elm);
			a.width = c[0];
			a.height = c[1];
			a.diffX = -1 == b.options.width ? 0 : b.options.width - c[0];
			a.diffY = -1 == b.options.height ? 0 : b.options.height - c[1];
			a.signX = 0 <= a.diffX ? 1 : -1;
			a.signY = 0 <= a.diffY ? 1 : -1;
			var c = a.diffX * a.diffX, d = a.diffY * a.diffY;
			a.stepX = a.signX * Math.sqrt(b.options.speed * b.options.speed * c / (c + d));
			a.stepY = a.signY * Math.sqrt(b.options.speed * b.options.speed * d / (c + d))
		},
		stopFunction : function(a) {-1 != b.options.width && (a.elm.style.width = b.options.width + "px");
			-1 != b.options.height && (a.elm.style.height = b.options.height + "px")
		},
		conditionFunction : function(a) {
			var c = 0 < a.signX ? a.width >= b.options.width : a.width <= b.options.width, a = 0 < a.signY ? a.height >= b.options.height : a.height <= b.options.height;
			-1 == b.options.width && ( c = 1);
			-1 == b.options.height && ( a = 1);
			return c && a
		},
		stepFunction : function(a) {
			a.width += a.stepX;
			a.height += a.stepY;
			var c = parseInt(a.width), d = parseInt(a.height);
			-1 != b.options.width && (a.elm.style.width = (0 <= c ? c : 0) + "px");
			-1 != b.options.height && (a.elm.style.height = (0 <= d ? d : 0) + "px")
		}
	});
	this.start = b.animation.start;
	this.stop = b.animation.stop
};
OAT.AnimationPosition = function(f, d) {
	var b = this;
	this.options = {
		left : -1,
		top : -1,
		delay : 50,
		speed : 1
	};
	for(var e in d)
	b.options[e] = d[e];
	this.animation = new OAT.Animation(f, {
		delay : b.options.delay,
		startFunction : function(a) {
			a.stepX = 0;
			a.stepY = 0;
			var c = OAT.Dom.getLT(a.elm);
			a.left = c[0];
			a.top = c[1];
			a.diffX = -1 == b.options.left ? 0 : b.options.left - c[0];
			a.diffY = -1 == b.options.top ? 0 : b.options.top - c[1];
			a.signX = 0 <= a.diffX ? 1 : -1;
			a.signY = 0 <= a.diffY ? 1 : -1;
			var c = a.diffX * a.diffX, d = a.diffY * a.diffY;
			a.stepX = a.signX * Math.sqrt(b.options.speed * b.options.speed * c / (c + d));
			a.stepY = a.signY * Math.sqrt(b.options.speed * b.options.speed * d / (c + d))
		},
		stopFunction : function(a) {-1 != b.options.left && (a.elm.style.left = b.options.left + "px");
			-1 != b.options.top && (a.elm.style.top = b.options.top + "px")
		},
		conditionFunction : function(a) {
			var c = 0 < a.signX ? a.left >= b.options.left : a.left <= b.options.left, a = 0 < a.signY ? a.top >= b.options.top : a.top <= b.options.top;
			-1 == b.options.left && ( c = 1);
			-1 == b.options.top && ( a = 1);
			return c && a
		},
		stepFunction : function(a) {
			a.left += a.stepX;
			a.top += a.stepY;
			var c = parseInt(a.left), d = parseInt(a.top);
			-1 != b.options.left && (a.elm.style.left = c + "px");
			-1 != b.options.top && (a.elm.style.top = d + "px")
		}
	});
	this.start = b.animation.start;
	this.stop = b.animation.stop
};
OAT.AnimationOpacity = function(f, d) {
	var b = this;
	this.options = {
		opacity : 1,
		delay : 50,
		speed : 0.1
	};
	for(var e in d)
	b.options[e] = d[e];
	this.animation = new OAT.Animation(f, {
		delay : b.options.delay,
		startFunction : function(a) {
			a.opacity = 1;
			OAT.Browser.isGecko && (a.opacity = parseFloat(OAT.Dom.style(a.elm, "opacity")));
			if(OAT.Browser.isIE) {
				var c = OAT.Dom.style(a.elm, "filter").match(/alpha\(opacity=([^\)]+)\)/);
				c && (a.opacity = parseFloat(c[1]) / 100)
			}
			a.step_ = 1;
			a.diff = b.options.opacity - a.opacity;
			a.sign = 0 <= a.diff ? 1 : -1;
			a.step_ = a.sign * b.options.speed
		},
		stopFunction : function(a) {
			OAT.Style.opacity(a.elm, b.options.opacity)
		},
		conditionFunction : function(a) {
			return 0 < a.sign ? a.opacity + 1.0E-4 >= b.options.opacity : a.opacity - 1.0E-4 <= b.options.opacity
		},
		stepFunction : function(a) {
			a.opacity += a.step_;
			OAT.Style.opacity(a.elm, a.opacity)
		}
	});
	this.start = b.animation.start;
	this.stop = b.animation.stop
};
OAT.AnimationCSS = function(f, d) {
	var b = this;
	this.options = {
		delay : 50,
		property : !1,
		start : 0,
		step : 1,
		stop : 10
	};
	for(var e in d)
	b.options[e] = d[e];
	this.animation = new OAT.Animation(f, {
		delay : b.options.delay,
		startFunction : function(a) {
			a[b.options.property] = b.options.start;
			a.elm.style[b.options.property] = b.options.start
		},
		stopFunction : function(a) {
			a.elm.style[b.options.property] = b.options.stop
		},
		conditionFunction : function(a) {
			return a[b.options.property] == b.options.stop
		},
		stepFunction : function(a) {
			a[b.options.property] += b.options.step;
			a.elm.style[b.options.property] = a[b.options.property]
		}
	});
	this.start = b.animation.start;
	this.stop = b.animation.stop
};
try{
OAT.Loader.featureLoaded("animation");
} catch (ERROR) {
	
}

OAT.getURL = function() {
	try{
		var dir = location.href.substring(0, location.href.lastIndexOf('\/'));
		var dom = dir;
		if(dom.substr(0, 7) == 'http:\/\/')
			dom = dom.substr(7);
		var path = '';
		var pos = dom.indexOf('\/');
		if(pos > -1) {
			path = dom.substr(pos + 1);
			dom = dom.substr(0, pos);
		}
		var page = location.href.substring(dir.length + 1, location.href.length + 1);
		var pos = page.indexOf('?');
		if(pos > -1) {
			page = page.substring(0, pos);
		}
		pos = page.indexOf('#');
		if(pos > -1) {
			page = page.substring(0, pos);
		}
		var ext = '';
		pos = page.indexOf('.');
		if(pos > -1) {
			ext = page.substring(pos + 1);
			page = page.substr(0, pos);
		}
		var file = page;
		if(ext != '')
			file += '.' + ext;
		if(file == '')
			page = 'index';
		args = location.search.substr(1).split("?");
		
		return path + page;
	} catch (ERROR) {
		return "";
	}	
}

OAT.PartialSort = function(data, a, z, index, arrayLength, sortIntMemory){
     			var partialArray = []
     			for(var p = 0; p <= z-a; p++){
     				partialArray[p] = data[a+p]
     			}
     		
     			if (sortIntMemory[index]){
    				partialArray.sort((function(index){
    				return function(a, b){
						return (parseInt(a[index]) === parseInt(b[index]) ? 0 : (parseInt(a[index]) < parseInt(b[index]) ? -1 : 1));
    				};
					})(index));
    			} else {
    				partialArray.sort((function(index){
    					return function(a, b){
       						return (a[index] === b[index] ? 0 : (a[index] < b[index] ? -1 : 1));
    						};
					})(index));
				}
				
				//recursive call
				if (index < arrayLength-1){
					var actualVal = partialArray[0][index];
					var initPos = 0; 
					for (var i=0; i < partialArray.length; i++){
						if ((actualVal != partialArray[i][index])) {
							actualVal = partialArray[i][index]
							partialArray = OAT.PartialSort(partialArray, initPos, i-1, index+1, arrayLength, sortIntMemory)
							initPos = i;
						} else 	if ((i == partialArray.length-1)){
							partialArray = OAT.PartialSort(partialArray, initPos, i, index+1, arrayLength, sortIntMemory)
						}
					}
				}
				
				for(var p = a; p <= z; p++){
     				data[p] = partialArray[p-a]
     			}
				return data
     		}


OAT.BarChart=function(o,m){var a=this;this.options={percentage:!1,spacing:25,paddingLeft:30,paddingBottom:30,paddingTop:5,width:15,colors:["#f00","#0f0","#00f"],border:!0,grid:!0,gridDesc:!0,gridNum:6,shadow:!0,shadowColor:"#222",shadowOffset:2};for(var n in m)this.options[n]=m[n];this.maxValue=0;this.data=[];this.textX=[];this.textY=[];this.div=OAT.$(o);OAT.Dom.makePosition(a.div);this.getFreeH=function(){return OAT.Dom.getWH(a.div)[1]-a.options.paddingTop-a.options.paddingBottom};this.drawColumn=function(c,
j){var b=a.options,g=a.freeH,d="object"==typeof a.data[c]?a.data[c]:[a.data[c]];if(b.percentage){for(var h=[],e=0,f=0;f<d.length;f++)e+=d[f];for(f=0;f<d.length;f++)h.push(a.maxValue*d[f]/e)}else h=d;for(var l=b.paddingBottom,e=0,d=[],f=0;f<h.length;f++){var k=h[f],i=OAT.Dom.create("div",{position:"absolute",fontSize:"0px",lineHeight:"0px"});b.border&&(i.style.border="1px solid #000");i.style.width=b.width+"px";i.style.backgroundColor=b.colors[f];i.style.left=j+"px";i.style.bottom=l+"px";k=a.scale(k);
i.style.height=k+"px";l+=k;e+=k;d.push(i)}b.shadow&&(h=OAT.Dom.create("div",{position:"absolute",fontSize:"0px",lineHeight:"0px"}),h.style.left=parseInt(j)+b.shadowOffset+"px",h.style.bottom=b.paddingBottom+b.shadowOffset+"px",h.style.width=b.width+"px",h.style.height=e+"px",h.style.backgroundColor=b.shadowColor,b.border&&(h.style.borderColor=b.shadowColor,h.style.borderWidth="1px",h.style.borderStyle="solid"),a.div.appendChild(h));for(f=0;f<d.length;f++)a.div.appendChild(d[f]);a.textX.length&&(e=
OAT.Dom.create("div",{position:"absolute"}),e.className="textX",e.innerHTML=a.textX[c],e.style.top=g+b.paddingTop+2+"px",e.style.left=j+"px",a.div.appendChild(e),g=OAT.Dom.getWH(e),e.style.left=j+Math.round(b.width/2)-Math.round(g[0]/2)+"px")};this.draw=function(){OAT.Dom.clear(a.div);a.div.style.width="100%";if(a.data.length){var c=a.options.paddingLeft,j=a.data.length*(a.options.width+a.options.spacing);a.freeH=a.getFreeH();for(var b=a.maxValue=0;b<a.data.length;b++)if("object"==typeof a.data[b]){for(var g=
0,d=0;d<a.data[b].length;d++)g+=parseFloat(a.data[b][d]);g>a.maxValue&&(a.maxValue=g)}else parseFloat(a.data[b])>a.maxValue&&(a.maxValue=a.data[b]);if(a.options.grid){b=(a.options.percentage?100:a.maxValue)/a.options.gridNum;g=Math.floor(Math.log(b)/Math.log(10));g=Math.pow(10,g);g*=Math.round(b/g);for(b=0;b<=(a.options.percentage?100:a.maxValue);b+=g){var b=Math.round(1E3*b)/1E3,d=OAT.Dom.create("div",{position:"absolute",width:j+"px",height:"1px",fontSize:"0px",lineHeight:"0px",backgroundColor:"#000"}),
h=a.options.percentage?a.scale(b/100*a.maxValue):a.scale(b),h=a.options.paddingTop+a.freeH-h;d.style.left=c+"px";d.style.top=h+"px";a.div.appendChild(d);if(a.options.gridDesc){var e=OAT.Dom.create("div",{position:"absolute"});e.innerHTML=b;e.className="textY";e.style.left=c+"px";e.style.top=h+"px";a.div.appendChild(e);d=OAT.Dom.getWH(e);e.style.left=c-2-d[0]+"px";e.style.top=h-Math.round(d[1]/2)+"px"}}}c+=Math.round(a.options.spacing/2);for(b=0;b<a.data.length;b++)a.drawColumn(b,c),c+=a.options.width+
a.options.spacing;if(a.textY.length){var f=OAT.Dom.create("div",{position:"absolute"});f.className="legend";f.style.left=c+"px";f.style.top=a.options.paddingTop+"px";a.div.appendChild(f);for(b=a.data[0].length-1;0<=b;b--)d=OAT.Dom.create("div",{clear:"left"}),c=OAT.Dom.create("div"),c.className="legend_box",c.style.backgroundColor=a.options.colors[b],d.appendChild(c),c=OAT.Dom.text(a.textY[b]),d.appendChild(c),f.appendChild(d)}g=a.options.paddingLeft+a.data.length*(a.options.width+a.options.spacing)+
5;a.textY.length&&(d=OAT.Dom.getWH(f),g+=d[0]+5+Math.round(a.options.spacing/2));isNaN(g)||(a.div.style.width=g+"px")}};this.scale=function(c){return Math.round(c/a.maxValue*a.freeH)};this.attachData=function(c){a.data=c};this.attachTextX=function(c){a.textX=c};this.attachTextY=function(c){a.textY=c}};
try {
OAT.Loader.featureLoaded("barchart");
} catch (ERROR) {
	
}


OAT.Crypto={base64e:function(l){var h="",j,i,f="",m,b,k="",a=0;if(!l.length)return h;do j=l.charCodeAt(a++),i=l.charCodeAt(a++),f=l.charCodeAt(a++),m=j>>2,j=(j&3)<<4|i>>4,b=(i&15)<<2|f>>6,k=f&63,isNaN(i)?b=k=64:isNaN(f)&&(k=64),h=h+"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(m)+"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(j)+"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(b)+"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(k);
while(a<l.length);return h},base64d:function(l){if(!l)return"";var h="",j,i,f="",m,b="",k=0;/[^A-Za-z0-9\+\/\=]/g.exec(l)&&alert("There were invalid base64 characters in the input text.\nValid base64 characters are A-Z, a-z, 0-9, '+', '/', and '='\nExpect errors in decoding.");l=l.replace(/[^A-Za-z0-9\+\/\=]/g,"");if(!l.length)return h;do j="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(l.charAt(k++)),i="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(l.charAt(k++)),
m="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(l.charAt(k++)),b="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(l.charAt(k++)),j=j<<2|i>>4,i=(i&15)<<4|m>>2,f=(m&3)<<6|b,h+=String.fromCharCode(j),64!=m&&(h+=String.fromCharCode(i)),64!=b&&(h+=String.fromCharCode(f));while(k<l.length);return h},md5:function(l){function h(b,f,a,c,d,e){b=m(m(f,b),m(c,e));return m(b<<d|b>>>32-d,a)}function j(b,f,a,c,d,e,g){return h(f&a|~f&c,b,f,d,e,g)}function i(b,
f,a,c,d,e,g){return h(f&c|a&~c,b,f,d,e,g)}function f(b,f,a,c,d,e,g){return h(a^(f|~c),b,f,d,e,g)}function m(b,f){var a=(b&65535)+(f&65535);return(b>>16)+(f>>16)+(a>>16)<<16|a&65535}return function(b){for(var f="",a=0;a<4*b.length;a++)f+="0123456789abcdef".charAt(b[a>>2]>>8*(a%4)+4&15)+"0123456789abcdef".charAt(b[a>>2]>>8*(a%4)&15);return f}(function(b,k){b[k>>5]|=128<<k%32;b[(k+64>>>9<<4)+14]=k;for(var a=1732584193,c=-271733879,d=-1732584194,e=271733878,g=0;g<b.length;g+=16)var l=a,o=c,p=d,n=e,a=
j(a,c,d,e,b[g+0],7,-680876936),e=j(e,a,c,d,b[g+1],12,-389564586),d=j(d,e,a,c,b[g+2],17,606105819),c=j(c,d,e,a,b[g+3],22,-1044525330),a=j(a,c,d,e,b[g+4],7,-176418897),e=j(e,a,c,d,b[g+5],12,1200080426),d=j(d,e,a,c,b[g+6],17,-1473231341),c=j(c,d,e,a,b[g+7],22,-45705983),a=j(a,c,d,e,b[g+8],7,1770035416),e=j(e,a,c,d,b[g+9],12,-1958414417),d=j(d,e,a,c,b[g+10],17,-42063),c=j(c,d,e,a,b[g+11],22,-1990404162),a=j(a,c,d,e,b[g+12],7,1804603682),e=j(e,a,c,d,b[g+13],12,-40341101),d=j(d,e,a,c,b[g+14],17,-1502002290),
c=j(c,d,e,a,b[g+15],22,1236535329),a=i(a,c,d,e,b[g+1],5,-165796510),e=i(e,a,c,d,b[g+6],9,-1069501632),d=i(d,e,a,c,b[g+11],14,643717713),c=i(c,d,e,a,b[g+0],20,-373897302),a=i(a,c,d,e,b[g+5],5,-701558691),e=i(e,a,c,d,b[g+10],9,38016083),d=i(d,e,a,c,b[g+15],14,-660478335),c=i(c,d,e,a,b[g+4],20,-405537848),a=i(a,c,d,e,b[g+9],5,568446438),e=i(e,a,c,d,b[g+14],9,-1019803690),d=i(d,e,a,c,b[g+3],14,-187363961),c=i(c,d,e,a,b[g+8],20,1163531501),a=i(a,c,d,e,b[g+13],5,-1444681467),e=i(e,a,c,d,b[g+2],9,-51403784),
d=i(d,e,a,c,b[g+7],14,1735328473),c=i(c,d,e,a,b[g+12],20,-1926607734),a=h(c^d^e,a,c,b[g+5],4,-378558),e=h(a^c^d,e,a,b[g+8],11,-2022574463),d=h(e^a^c,d,e,b[g+11],16,1839030562),c=h(d^e^a,c,d,b[g+14],23,-35309556),a=h(c^d^e,a,c,b[g+1],4,-1530992060),e=h(a^c^d,e,a,b[g+4],11,1272893353),d=h(e^a^c,d,e,b[g+7],16,-155497632),c=h(d^e^a,c,d,b[g+10],23,-1094730640),a=h(c^d^e,a,c,b[g+13],4,681279174),e=h(a^c^d,e,a,b[g+0],11,-358537222),d=h(e^a^c,d,e,b[g+3],16,-722521979),c=h(d^e^a,c,d,b[g+6],23,76029189),a=
h(c^d^e,a,c,b[g+9],4,-640364487),e=h(a^c^d,e,a,b[g+12],11,-421815835),d=h(e^a^c,d,e,b[g+15],16,530742520),c=h(d^e^a,c,d,b[g+2],23,-995338651),a=f(a,c,d,e,b[g+0],6,-198630844),e=f(e,a,c,d,b[g+7],10,1126891415),d=f(d,e,a,c,b[g+14],15,-1416354905),c=f(c,d,e,a,b[g+5],21,-57434055),a=f(a,c,d,e,b[g+12],6,1700485571),e=f(e,a,c,d,b[g+3],10,-1894986606),d=f(d,e,a,c,b[g+10],15,-1051523),c=f(c,d,e,a,b[g+1],21,-2054922799),a=f(a,c,d,e,b[g+8],6,1873313359),e=f(e,a,c,d,b[g+15],10,-30611744),d=f(d,e,a,c,b[g+6],
15,-1560198380),c=f(c,d,e,a,b[g+13],21,1309151649),a=f(a,c,d,e,b[g+4],6,-145523070),e=f(e,a,c,d,b[g+11],10,-1120210379),d=f(d,e,a,c,b[g+2],15,718787259),c=f(c,d,e,a,b[g+9],21,-343485551),a=m(a,l),c=m(c,o),d=m(d,p),e=m(e,n);return[a,c,d,e]}(function(b){for(var f=[],a=0;a<8*b.length;a+=8)f[a>>5]|=(b.charCodeAt(a/8)&255)<<a%32;return f}(l),8*l.length))},sha:function(l){function h(h,i){var f=(h&65535)+(i&65535);return(h>>16)+(i>>16)+(f>>16)<<16|f&65535}return function(h){for(var i="",f=0;f<4*h.length;f++)i+=
"0123456789abcdef".charAt(h[f>>2]>>8*(3-f%4)+4&15)+"0123456789abcdef".charAt(h[f>>2]>>8*(3-f%4)&15);return i}(function(j,i){j[i>>5]|=128<<24-i%32;j[(i+64>>9<<4)+15]=i;for(var f=Array(80),m=1732584193,b=-271733879,k=-1732584194,a=271733878,c=-1009589776,d=0;d<j.length;d+=16){for(var e=m,g=b,l=k,o=a,p=c,n=0;80>n;n++){f[n]=16>n?j[d+n]:(f[n-3]^f[n-8]^f[n-14]^f[n-16])<<1|(f[n-3]^f[n-8]^f[n-14]^f[n-16])>>>31;var q=m<<5|m>>>27,r;r=20>n?b&k|~b&a:40>n?b^k^a:60>n?b&k|b&a|k&a:b^k^a;q=h(h(q,r),h(h(c,f[n]),20>
n?1518500249:40>n?1859775393:60>n?-1894007588:-899497514));c=a;a=k;k=b<<30|b>>>2;b=m;m=q}m=h(m,e);b=h(b,g);k=h(k,l);a=h(a,o);c=h(c,p)}return[m,b,k,a,c]}(function(h){for(var i=[],f=0;f<8*h.length;f+=8)i[f>>5]|=(h.charCodeAt(f/8)&255)<<24-f%32;return i}(l),8*l.length))}};
try{
OAT.Loader.featureLoaded("crypto");
} catch (ERROR) {
	
}

}
