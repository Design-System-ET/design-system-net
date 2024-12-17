//FILE OAT.Dialof

if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
OAT.Dialog = function(i, d, c) {
	var e = this, a = {
		width : 0,
		height : 0,
		modal : 0,
		onshow : function() {
		},
		onhide : function() {
		},
		zIndex : 1E3,
		buttons : 1,
		resize : 1,
		close : 1,
		autoEnter : 1,
		imagePath : OAT.Preferences.imagePath
	};
	if(c)
		for(var f in c)
		a[f] = c[f];
	var b = new OAT.Window({
		close : a.close,
		max : 0,
		min : 0,
		width : a.width,
		height : a.height,
		x : 0,
		y : 0,
		title : i,
		resize : a.resize,
		imagePath : a.imagePath
	});
	OAT.Dom.hide(b.div);
	try {
		OAT.$(d).style.margin = "10px"
	} catch(k) {
	}
	i = OAT.Dom.create("table", {
		marginTop : "1em",
		width : "90%",
		textAlign : "center"
	});
	c = OAT.Dom.create("tbody");
	f = OAT.Dom.create("tr");
	var j = OAT.Dom.create("td", {
		border : "none"
	}), g = OAT.Dom.create("input");
	g.setAttribute("type", "button");
	g.value = " OK ";
	j.appendChild(g);
	var h = OAT.Dom.create("input", {
		marginLeft : "2em"
	});
	h.setAttribute("type", "button");
	h.value = "Cancel";
	j.appendChild(h);
	f.appendChild(j);
	c.appendChild(f);
	i.appendChild(c);
	a.buttons && OAT.$(d).appendChild(i);
	document.body.appendChild(b.div);
	b.content.appendChild(OAT.$(d));
	b.div.style.zIndex = a.zIndex;
	a.modal ? (this.show = function() {
		OAT.Dimmer.show(b.div, {});
		b.accomodate();
		OAT.Dom.center(b.div, 1, 1);
		a.onshow()
	}, this.hide = function() {
		OAT.Dimmer.hide();
		a.onhide()
	}) : (this.show = function() {
		OAT.Dom.show(b.div);
		b.accomodate();
		OAT.Dom.center(b.div, 1, 1);
		a.onshow()
	}, this.hide = function() {
		OAT.Dom.hide(b.div);
		a.onhide()
	});
	b.onclose = this.hide;
	this.accomodate = b.accomodate;
	this.ok = function() {
	};
	this.cancel = function() {
	};
	this.okBtn = g;
	this.cancelBtn = h;
	OAT.Dom.attach(g, "click", function() {
		e.ok()
	});
	OAT.Dom.attach(h, "click", function() {
		e.cancel()
	});
	d = function(a) {
		if(e.okBtn.getAttribute("disabled") != "disabled") {a.keyCode == 13 && e.ok();
			a.keyCode == 27 && e.cancel()
		}
	};
	a.autoEnter && OAT.Dom.attach(b.div, "keypress", d)
};
try {
OAT.Loader.featureLoaded("dialog");
} catch (ERROR) {
	
}

OAT.Drag={TYPE_X:1,TYPE_Y:2,TYPE_XY:3,elm:!1,mouse_x:0,mouse_y:0,
	move:function(b){function d(a,b){return Math.abs(a-b)<=o}
		if(OAT.Drag.elm){OAT.Dom.removeSelection();
		var e=OAT.Dom.getViewport(),a=OAT.Dom.position(OAT.Drag.elm);
		OAT.Dom.eventPos(b);OAT.Dom.getWH(OAT.Drag.elm);
		if(!(b.clientX>e[0]||0>b.clientX||b.clientY>e[1]||0>b.clientY)){
			for(var e=b.clientX-OAT.Drag.mouse_x,c=b.clientY-OAT.Drag.mouse_y,q=!0,j=OAT.Drag.elm._Drag_movers,h=0;h<j.length;h++)
			{var a=j[h][0],i=j[h][1],a=OAT.Dom.getLT(a);i.restrictionFunction(a[0]+e,a[1]+c)&&(q=!1)}
			for(var k=!0,o=10,h=0;h<j.length;h++)
			{
				for(var a=j[h][0],i=j[h][1],l=OAT.Dom.getWH(a),m=OAT.Dom.position(a),r=m[0]+e,m=m[1]+c,n=0;n<i.magnetsH.length;n++)
				{
					var g=OAT.$(i.magnetsH[n]),f=OAT.Dom.position(g),g=OAT.Dom.getWH(g);
					if(d(r,f[0])){a.style.left=f[0]+"px";k=!1;break}
					if(d(r+l[0],f[0]))
					{a.style.left=f[0]-l[0]+"px";k=!1;break}
					if(d(r,f[0]+g[0]))
					{a.style.left=f[0]+g[0]+"px";k=!1;break}
					if(d(r+l[0],f[0]+g[0]))
					{a.style.left=f[0]+g[0]-l[0]+"px";k=!1;break}}
					for(n=0;n<i.magnetsV.length;n++)
					{g=OAT.$(i.magnetsV[n]);f=OAT.Dom.position(g);
					 g=OAT.Dom.getWH(g);
					 if(d(m,f[1]))
					 {a.style.top=f[1]+"px";k=!1;break}
					 if(d(m+l[1],f[1]))
					 {a.style.top=f[1]-l[1]+"px";k=!1;break}if(d(m,f[1]+g[1])){a.style.top=f[1]+g[1]+"px";k=!1;break}
					 if(d(m+l[1],f[1]+g[1])){a.style.top=f[1]+g[1]-l[1]+"px";k=!1;break}}}if(q&&k){for(h=0;h<j.length;h++)if(a=j[h][0],i=j[h][1],i.moveFunction)i.moveFunction(e,c);else switch(i.type){case OAT.Drag.TYPE_X:OAT.Dom.moveBy(a,e,0);break;case OAT.Drag.TYPE_Y:OAT.Dom.moveBy(a,0,c);break;case OAT.Drag.TYPE_XY:OAT.Dom.moveBy(a,
e,c)}OAT.Drag.mouse_x=b.clientX;OAT.Drag.mouse_y=b.clientY}}}},up:function(){if(OAT.Drag.elm){for(var b=OAT.Drag.elm._Drag_movers,d=0;d<b.length;d++)b[d][1].endFunction(b[d][0]);OAT.Drag.elm=!1}},create:function(b,d,e){var a={type:OAT.Drag.TYPE_XY,restrictionFunction:function(){return!1},endFunction:function(){},moveFunction:!1,magnetsH:[],magnetsV:[],cursor:!0};if(e)for(p in e)a[p]=e[p];var c=OAT.$(b),b=OAT.$(d),d=function(a){OAT.Drag.initiate(a,c)};c._Drag_movers||(OAT.Dom.attach(c,"mousedown",d),c._Drag_movers=
[],c._Drag_cursor=c.style.cursor);a.cursor&&(c.style.cursor="move");c._Drag_movers.push([b,a])},initiate:function(b,d){OAT.Drag.elm=d;OAT.Drag.mouse_x=b.clientX;OAT.Drag.mouse_y=b.clientY},remove:function(b,d){var e=OAT.$(b);OAT.$(d);if(e._Drag_movers){for(var a=-1,c=0;c<e._Drag_movers.length;c++)e._Drag_movers[c][0]==d&&(a=c);-1!=a&&e._Drag_movers.splice(a,1)}},removeAll:function(b){b=OAT.$(b);b._Drag_movers&&(b._Drag_movers=[],b.style.cursor=b._Drag_cursor)},createDefault:function(b,d){if(OAT.Preferences.allowDefaultDrag){var e=
OAT.$(b),a=OAT.Dom.create("div",{position:"absolute",width:"21px",height:"21px",backgroundImage:"url("+OAT.Preferences.imagePath+"drag.png)"}),c=OAT.Dom.getLT(e);a.style.left=c[0]-21+"px";a.style.top=c[1]-21+"px";if(d){e.parentNode.appendChild(a);c=function(a,b){var c=OAT.Dom.getWH(e),d=OAT.Dom.getWH(e.parentNode),o=a+c[0],c=b+c[1];return 20>a||20>b||o>d[0]||c>d[1]};OAT.Drag.create(a,a);OAT.Drag.create(a,e,{restrictionFunction:c});OAT.Dom.hide(a);var q=function(){a._Drag_pending&&OAT.Dom.hide(a)};OAT.Dom.attach(e,
"mouseover",function(){OAT.Dom.show(a);a._Drag_pending=0});OAT.Dom.attach(e,"mouseout",function(){a._Drag_pending=1;setTimeout(q,3E3)})}else c=function(a,b){var c=OAT.Dom.getWH(e),d=OAT.Dom.getWH(e.parentNode),o=a+c[0],c=b+c[1];return 0>a||0>b||o>d[0]||c>d[1]},OAT.Drag.create(e,e,{restrictionFunction:c})}}};OAT.Dom.attach(document,"mousemove",OAT.Drag.move);OAT.Dom.attach(document,"mouseup",OAT.Drag.up);try{
OAT.Loader.featureLoaded("drag");
} catch (ERROR) {
	
}


OAT.GhostDragData={
	lock:!1,
	up:function(a){
		if(OAT.GhostDragData.lock)
		{
			var b=OAT.GhostDragData.lock,c=b.object;
			if(c.pending)
				c.pending=0,OAT.GhostDragData.lock=!1;
			else{
				OAT.GhostDragData.lock=!1;
				for(var d=OAT.Dom.eventPos(a),a=d[0],d=d[1],e=0,f=0;f<c.targets.length;f++)
				{
					var g=c.targets[f],h=g[1]?g[1](a,d):OAT.GhostDragData.pos(g[0],a,d);
					!e&&h&&(e=1,c.callback(g[0],a,d))
				}
				e?(OAT.Dom.unlink(b),OAT.MSG.send(c,OAT.MSG.GD_END,b)):(OAT.MSG.send(c,OAT.MSG.GD_ABORT,b),c.onFail(),c=OAT.Dom.position(c.originalElement),
				a=c[0],d=c[1],c=new OAT.AnimationPosition(b,{speed:10,delay:10,left:a,top:d}),OAT.MSG.attach(c.animation,OAT.MSG.ANIMATION_STOP,function(){OAT.Dom.unlink(b)}),c.start())}
		}
	},
	move:function(a){if(OAT.GhostDragData.lock){/*OAT.Dom.prevent(a);*/var b=OAT.GhostDragData.lock,c=b.object;c.pending&&(document.body.appendChild(b),b.style.zIndex=2E3,
	c.process&&c.process(b),
	c.pending=0,OAT.MSG.send(c,OAT.MSG.GD_START,b));OAT.Dom.removeSelection();var d=a.clientX-b.mouse_x,c=a.clientY-b.mouse_y,d=parseInt(OAT.Dom.style(b,
"left"))+d,c=parseInt(OAT.Dom.style(b,"top"))+c;b.style.left=d+"px";b.style.top=c+"px";b.mouse_x=a.clientX;b.mouse_y=a.clientY}},pos:function(a,b,c){if(!a||"none"==a.style.display.toLowerCase())return 0;var d=OAT.Dom.position(a),e=d[0]-2,d=d[1]-2,f=parseInt(a.offsetWidth)+2,a=parseInt(a.offsetHeight)+2;return b>=e&&b<=e+f&&c>=d&&c<=d+a}};
	OAT.GhostDrag=function(){
		var a=this;
		this.onFail=function(){};
		this.sources=[];
		this.processes=[];
		this.callbacks=[];
		this.targets=[];
		this.pending=0;
		this.addSource=function(b,c,d){
				var e=OAT.$(b);
				a.sources.push(e);
				a.processes.push(c);
				a.callbacks.push(d);
				OAT.Dom.attach(e,"mousedown",
					function(b){
								OAT.Dom.prevent(b);
								var c=a.sources.find(e);
								-1!=c&&a.startDrag(a.sources[c],a.processes[c],a.callbacks[c],b.clientX,b.clientY);
								
							}
							)
		};
		this.delSource=function(b){b=$(b);b=a.sources.find(b);-1!=b&&(a.sources.splice(b,1),a.processes.splice(b,1),a.callbacks.splice(b,1))};
		this.clearSources=function(){a.sources=[];a.processes=[];a.callbacks=[]};
		this.addTarget=function(b,c,d){b=[OAT.$(b),c,d];a.targets.length&&a.targets[a.targets.length-1][2]?a.targets.splice(a.targets.length-1,0,b):a.targets.push(b)};
		this.delTarget=function(b){for(var b=OAT.$(b),c=-1,d=0;d<a.targets.length;d++)a.targets[d][0]==b&&(c=d);-1!=c&&a.targets.splice(c,1)};
		this.clearTargets=function(){a.targets=[]};
		this.startDrag=function(b,c,d,e,f){OAT.GhostDragData.lock||(a.pending=1,a.originalElement=b,a.callback=d,d=OAT.Dom.create("div",{position:"absolute"}),a.process=c,c=OAT.Dom.position(b),d.style.left=c[0]+"px",d.style.top=c[1]+"px",OAT.Style.opacity(d,0.5),d.appendChild(b.cloneNode(!0)),d.mouse_x=e,d.mouse_y=f,d.object=a,OAT.GhostDragData.lock=d)}};
		OAT.Dom.attach(document,"mousemove",OAT.GhostDragData.move);
		OAT.Dom.attach(document,"mouseup",OAT.GhostDragData.up);
try{
OAT.Loader.featureLoaded("ghostdrag");
} catch (ERROR) {
	
}

OAT.Instant = function(d, c) {
	var a = this;
	this.options = {
		showCallback : !1,
		hideCallback : !1
	};
	for(var e in c)
	a.options[e] = c[e];
	this.state = 1;
	this.elm = OAT.$(d);
	this.handles = [];
	this.hide = function() {
		a.state = 0;
		OAT.Dom.hide(a.elm)
	};
	this.show = function() {
		a.options.showCallback && a.options.showCallback();
		OAT.Dom.show(a.elm);
		a.state = 1
	};
	this.check = function(b) {
		a.state && ( b = OAT.Event.source(b), b == a.elm || OAT.Dom.isChild(b, a.elm) || (a.options.hideCallback && a.options.hideCallback(), a.hide()))
	};
	this.createHandle = function(b) {
		var c = OAT.$(b);
		a.handles.push(c);
		OAT.Event.attach(c, "mousedown", function(b) {-1 != a.handles.find(c) && !a.state && (OAT.Event.cancel(b), a.show())
		})
	};
	this.removeHandle = function(b) {
		b = OAT.$(b);
		b = a.handles.find(b);
		-1 != b && a.handles.splice(b, 1)
	};
	a.elm._Instant_show = a.show;
	a.elm._Instant_hide = a.hide;
	a.hide();
	OAT.Dom.attach(document, "mousedown", a.check)
};
OAT.Instant.assign = function(d, c) {
	new OAT.Instant(d, {
		hideCallback : c
	})
};
try{
OAT.Loader.featureLoaded("instant");
} catch (ERROR) {
	
}

}