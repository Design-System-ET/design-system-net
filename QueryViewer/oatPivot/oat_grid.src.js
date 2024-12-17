
//FILE oat_grid -----------------------------------------------------------------------------------------------------------------------------------------------





//OAT.GRID file
/*
 *  $Id: grid.js,v 1.12 2008/03/18 08:55:41 source Exp $
 *
 *  This file is part of the OpenLink Software Ajax Toolkit (OAT) project.
 *
 *  Copyright (C) 2005-2007 OpenLink Software
 *
 *  See LICENSE file for details.
 */
if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
OAT.GridData = {
	dragging:false, /* object we are dragging */
	resizing:false, /* object we are resizing */
	index:0,        /* column in action */
	x:0,            /* actual x */
	w:0,            /* actual width */
	forbidSort:0,    /* don't sort now */
	LIMIT:15,       /* minimum width */
	ALIGN_CENTER:1,
	ALIGN_LEFT:2,
	ALIGN_RIGHT:3,
	SORT_NONE:1,
	SORT_ASC:2,
	SORT_DESC:3,
	TYPE_STRING:1,
	TYPE_NUMERIC:2,
	TYPE_AUTO:3,
	
	up:function(event) {
		if (OAT.GridData.resizing) {
			var grid = OAT.GridData.resizing;
			OAT.GridData.resizing = false;
			grid.header.cells[OAT.GridData.index].changeWidth(OAT.GridData.w);
			for (var i=0;i<grid.rows.length;i++) {
				grid.rows[i].cells[OAT.GridData.index].changeWidth(OAT.GridData.w);
			} /* for all rows */

			OAT.Dom.unlink(grid.tmp_resize);
			OAT.Dom.unlink(grid.tmp_resize_start);
			
			OAT.GridData.forbidSort = 1;
			var ref = function() { OAT.GridData.forbidSort = 0;	}
			setTimeout(ref,100);
		}
		
		if (OAT.GridData.dragging) {
			var grid = OAT.GridData.dragging;
			OAT.GridData.dragging = false;
			if (grid.tmp_drag) { /* reorder */
				var index = -1;
				for (var i=0;i<grid.header.cells.length;i++) {
					if (grid.header.cells[i].signal) {
						index = i;
						grid.header.cells[i].signalEnd();
					}
				}
				if (index == -1) { return; } /* nothing signaled? wtf? */
				
				
				/* we need to move OAT.GridData.index before index */
				var i1 = OAT.GridData.index;
				var i2 = index;
				

				grid.header.cells[i1].html.parentNode.insertBefore(grid.header.cells[i1].html,grid.header.cells[i2].html);
				var cell = grid.header.cells[i1];
				grid.header.cells.splice(i1,1);
				
				
				var newi = (i1 < i2 ? i2-1 : i2);
				grid.header.cells.splice(newi,0,cell);
				
				
				i1 = grid.columnsDataType.length - 1 - i1;
				i2 = grid.columnsDataType.length - 1 - i2;
				newi =  grid.columnsDataType.length - 1 - newi;
				for (var i=0;i<grid.rows.length;i++) {
					grid.rows[i].cells[i1].html.parentNode.insertBefore(grid.rows[i].cells[i1].html,grid.rows[i].cells[i2].html);
					var cell = grid.rows[i].cells[i1];
					grid.rows[i].cells.splice(i1,1);
					grid.rows[i].cells.splice(newi,0,cell);
				}
				
				for (var i=0;i<grid.header.cells.length;i++) { grid.header.cells[i].number = i; } /* renumber */
				
				OAT.Dom.unlink(grid.tmp_drag);
				OAT.GridData.forbidSort = 1;
				if (grid.options.reorderNotifier && (i1 != i2)) { grid.options.reorderNotifier(i1,i2); }
				var ref = function() { OAT.GridData.forbidSort = 0;	}
				setTimeout(ref,100);
			}
		}
	}, /* OAT.GridData.up() */
	
	move:function(event) {
		if (OAT.GridData.resizing) {
			OAT.Dom.removeSelection(); /* selection removal... */
			var grid = OAT.GridData.resizing;
			var elm = grid.tmp_resize; /* vertical line */
			var pos = OAT.Event.position(event);
			var offs_x = pos[0] - OAT.GridData.mouseX; /* offset */
			var new_x = OAT.GridData.w + offs_x;
			if (new_x >= OAT.GridData.LIMIT) {
				OAT.GridData.w = new_x;
				OAT.GridData.x += offs_x; /* line position */
				OAT.GridData.mouseX = pos[0];
				elm.style.left = OAT.GridData.x + "px";
			} /* if > limit */
		} /* if resizing */
		
		if (OAT.GridData.dragging) {
			OAT.Dom.removeSelection(); /* selection removal... */
			var grid = OAT.GridData.dragging;
			if (!grid.tmp_drag) { /* just moved - create ghost */
				var container = grid.header.cells[OAT.GridData.index].container;
				grid.tmp_drag = OAT.Dom.create("div",{position:"absolute",left:"0px",top:"0px",backgroundColor:"#888"});
				OAT.Style.opacity(grid.tmp_drag,0.5);
				grid.tmp_drag.appendChild(container.cloneNode(true));
				var dims = OAT.Dom.getWH(grid.header.cells[OAT.GridData.index].html);
				grid.tmp_drag.firstChild.style.width = dims[0]+"px";
				container.appendChild(grid.tmp_drag);
				OAT.GridData.w = 0;
			}
			var pos = OAT.Event.position(event);
			var offs_x = pos[0] - OAT.GridData.x;
			var new_x = OAT.GridData.w + offs_x;
			grid.tmp_drag.style.left = new_x + "px";
			OAT.GridData.x = pos[0];
			OAT.GridData.w = new_x;
			/* signal? */
			var sig = -1;
			for (var i=0;i<grid.header.cells.length;i++) {
				var cell = grid.header.cells[i];
				var coords = OAT.Dom.position(cell.container);
				var dims = OAT.Dom.getWH(cell.container)
				var x = coords[0];
				/* IE7 has a *wrong* value of offsetLeft, so we have to do a small hack here */
				if (OAT.Browser.isIE7) { x -= OAT.Dom.position(cell.container.offsetParent)[0]; }
				if (pos[0] >= x && pos[0] <= x+dims[0]) { /* inside this header */
					if (cell.signal) { return; } /* already in */
					for (var i=0;i<grid.header.cells.length;i++) { if (grid.header.cells[i].signal) grid.header.cells[i].signalEnd(); }
					cell.signalStart();
				} /* if inside some header */
			} /* for all cells */
		} /* if dragging */
	} /* OAT.GridData.move() */
} /* GridData */

OAT.Grid = function(element, optObj, allowHiding, controlName, query,  columnsDataType, colms, QueryViewerCollection, 
						pageSize, disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout, serverPaging) {
    var self = this;
    self.controlName = controlName;
    self.columnsDataType = columnsDataType;
    self.query = query;
    self.rowsPerPage = "";
    self.columns = colms;
    self.QueryViewerCollection = QueryViewerCollection;
    self.InitPageSize = pageSize; 
    self.disableColumnSort = disableColumnSort
    self.UcId = UcId;
    self.IdForQueryViewerCollection = IdForQueryViewerCollection;
    self.rememberLayout = rememberLayout;
    self.serverPaging = serverPaging;  
    
    self.conditions = new Array( columnsDataType.length );
    for (iC=0; iC < columnsDataType.length; iC++) {
    	self.conditions[iC] = { blackList : [],
    							sort      : 1  
    						  };
    };
    this.options = {
        autoNumber: false,
        allowHiding: false,
        rowOffset: 0,
        sortFunc: false,
        imagePath: OAT.Preferences.imagePath,
        reorderNotifier: false
    }
    if (typeof (optObj) == "object") {
        for (var p in optObj) { self.options[p] = optObj[p]; }
    } else {
        self.options.autoNumber = optObj;
    }
    if (allowHiding) { self.options.allowHiding = true; }

	//if ( (true) || (gx.util.browser.isIE()) ){
		var divContainer = OAT.$(element);
		var divIeContainer = document.createElement("div");
    	divIeContainer.setAttribute("class", "divIeContainer");
    	divContainer.appendChild(divIeContainer);
    	this.div = divIeContainer; 
	//} else {
    //	this.div = OAT.$(element);
    //}
    //	OAT.Dom.clear(self.div);
    this.init = function() {
        //if (self.options.allowHiding) { /* column hiding */
       		var topDiv = OAT.Dom.create("div");
       		topDiv.setAttribute("class", "oatgrid_top_div");
       		topDiv.setAttribute("id", self.controlName + "_grid_top_div");
            var hide = OAT.Dom.create("a");
            hide.href = "#";
            //hide.innerHTML = "visible columns";
            hide.innerHTML = gx.getMessage("GXPL_QViewerJSVisibleColumns");
            hide.id = "oatGridAnchor";
            //topDiv.appendChild(hide);
            self.div.appendChild(topDiv);
            self.propPage = OAT.Dom.create("div", { padding: "5px" });
           
            var refresh = function() { self.propPage._Instant_hide(); }
            var generatePair = function(index) {
           
            }
            var clickRef = function(event) {
           
            }/*  clickref */
           

        OAT.Dom.makePosition(self.div);
        self.html = OAT.Dom.create("table");
        OAT.Dom.addClass(self.html, "oatgrid");
        OAT.Dom.setIdPropertyValue(self.html, self.controlName);
        self.header = new OAT.GridHeader(self);
        self.rows = [];
        self.rowBlock = OAT.Dom.create("tbody");
        OAT.Dom.append([self.div, self.html], [self.html, self.header.html, self.rowBlock]);
        
        
        //draw export image and pop up of export options
        var exportImg = OAT.Dom.create("div");
        exportImg.href = "#";
        exportImg.setAttribute("class","exportOptionsAnchor");
        
        self.exportPage = OAT.Dom.create("div", { padding: "0px" });
        
        var checkToClose = function(b){
        	source = OAT.Event.source(b);
        	var clean = false;
        	var closing = false;
        	var isInside = false
        	for (var i = 0; i < jQuery(".oat_winrect_container").length; i++){
        		var obj = jQuery(".oat_winrect_container")[i];
        		if (!(source == obj) && !OAT.Dom.isChild(source, obj)){
        			clean = true;
        		} else {
        			clean = false; isInside = true; break;
        		}
        	}
        	for (var i = 0; i < jQuery(".oat_winrect_container").length; i++){
        		if (jQuery(".oat_winrect_container")[i].style.display != "none") {
        			closing = true;
        		}
        	}
        	if ( (self.serverPaging) && 
        		 ((source.getAttribute("class") == 	"oat_winrect_close_b") || (!OAT.Dom.isChild(source, obj))) &&
        		 (closing))
        	{
        			self.oat_component.resetAllScrollValue(self.UcId);
        	}
        	if (clean){
        		jQuery(".oat_winrect_container").css({display: "none"});
        	}
        };

        OAT.Dom.attach(document, "mousedown", checkToClose)
        
        OAT.Anchor.assign(exportImg, { title: " ",
                content: self.exportPage,
                result_control: false,
                activation: "click",
                type: OAT.WinData.TYPE_RECT,
                width: "auto"
        });
        
        var generatePair = function(index) {
                var state = (self.header.cells[index].html.style.display != "none");
                var pair = OAT.Dom.create("div");
                var check_class = (state)? "check_item_img": "uncheck_item_img";
                pair.setAttribute("class", check_class);
                var span = OAT.Dom.create("span");
                span.innerHTML = " " + self.header.cells[index].value.innerHTML;
                pair.appendChild(span);
                OAT.Event.attach(pair, "click", function() { // this hide or show the columns
                	var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            		this.setAttribute("class", newClass);
                    var newdisp = (self.header.cells[index].html.style.display == "none" ? "" : "none");
                    self.header.cells[index].html.style.display = newdisp;
                    var numCol = self.columnsDataType.length - 1 - index;
                    //if (self.serverPaging){
                    	numCol = index; 
                    //}
                    for (var i = 0; i < self.rows.length; i++) {
                        self.rows[i].cells[numCol].html.style.display = newdisp;
                    }
                    OAT.saveState({grid : self}, false);
                    
                    if (self.serverPaging){
                    	OAT_JS.grid.setColumnVisibleValue(self.UcId, numCol, (newdisp == ""))
                    }
                    
                });
                return pair;
        }
        
        var clickRef = function(event) {
        		var coords = OAT.Event.position(event);
                self.exportPage.style.left = coords[0] + "px";
                self.exportPage.style.top = coords[1] + "px";
                self.exportPage.id = "exportOptionsContainer";
                if (gx.util.browser.isIE()){
                	self.exportPage.id = "exportOptionsContainerGrid";
                }
                OAT.Dom.clear(self.exportPage);
                //botton to allow show all filters in pop up
        		var someExport = false;
        		var div_upper = document.createElement("div");
        		div_upper.setAttribute("class", "upper_container");
        		
        		jQuery('#divtoxml').remove();
    			jQuery('#divtoxls').remove();
    			jQuery('#divtoxlsx').remove();
    			jQuery('#divtoexport').remove();
    			jQuery('#divtohtml').remove();
        		someExport = self.appendExportToXmlOption(div_upper, someExport);
        		someExport = self.appendExportToHtmlOption(div_upper, someExport);
        		someExport = self.appendExportToPdfOption(div_upper, someExport);
        		someExport = self.appendExportToExcelOption(div_upper, someExport);
        		someExport = self.appendExportToExcel2010Option(div_upper, someExport);
        		
        		self.exportPage.appendChild(div_upper);
        		
        		
        		if (self.options.allowHiding) {
        			if (someExport){
        				var hr = OAT.Dom.create("hr", { });
        				self.exportPage.appendChild(hr);
        			}
        			
        			var div_down = document.createElement("div");
        			div_down.setAttribute("class", "down_container");
        			self.exportPage.appendChild(div_down);
        			
        			var label = document.createElement("span");
        			label.textContent = gx.getMessage("GXPL_QViewerJSVisibleColumns");
        			var div_label = document.createElement("div");
        			div_label.setAttribute("class","div_label_win");
        			div_label.appendChild(label);
        			div_down.appendChild(div_label);
        			
        			var start = (self.autoNumber ? 1 : 0);
                	for (var i = start; i < self.header.cells.length; i++) {
                    	var pair = generatePair(i);
                    	div_down.appendChild(pair);
                	}
                }
                if (gx.util.browser.isIPad() || gx.util.browser.isIPhone()) {
                	jQuery('.oat_winrect_close_b').css({backgroundSize: "30px 30px", height: "30px", width: "30px", right: "-14px", top: "-14px"})
                }
        }
               
        OAT.Event.attach(exportImg, "click", clickRef);
        topDiv.appendChild(exportImg);
              
    }
	
	this.appendExportToXmlOption = function(content, someExport){
			var exportXMLButton;
			var fileName = this.query;
			if (fileName == ""){
				try {
            		fileName = self.controlName.substr(4).split("_")[0]
            	} catch (error) {}	
            }														
    		if (self.QueryViewerCollection[self.IdForQueryViewerCollection].ExportToXML){ 
				if ((!gx.util.browser.isIE()) || 9<gx.util.browser.ieVersion()){
					exportXMLButton = OAT.Dom.create("div");
					var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportButtonSub.setAttribute('id', 'divtoxml');
  					exportXMLButton.appendChild(exportButtonSub);
  					 
  					var exportimg = OAT.Dom.create("img");
  					exportimg.src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png')
  					exportButtonSub.appendChild(exportimg)
					
					exportXMLButton.setAttribute("class", "export_item_div");
					var pvpl = OAT.Dom.create("label");
					pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXml")
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXMLButton.appendChild(pvpl);
					
        			OAT.Dom.attach(exportButtonSub, "click",  function() { 	
        												if (!self.serverPaging){
        													str = OAT.ExportToXML({grid : self}, fileName);
        												} else {
    														OAT_JS.grid.getAllDataRowsForExport(self.UcId, {grid : self}, fileName, "xml")
	    												}
	    											 }
        						   );
				} else {
					exportXMLButton = document.createElement("div");
        			exportXMLButton.style.marginBottom = "10px" 
  					exportXMLButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
  					exportXMLButtonSub.setAttribute('id', 'divtoxml')
					
  					setTimeout( function(){
  						jQuery("#divtoxml").downloadify({ 
    						filename: function(){
      								return fileName+'.xml';
    						},
    						data: function(){
    							//return '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' + OAT.ExportToXML({grid : self});;
    							return OAT.ExportToXML({grid : self});
    						},
    						onComplete: function(){
    						},
    						onCancel: function(){
    						},
    						onError: function(){
    						},
							//swf: 'QueryViewer/oatPivot/downloadify/media/downloadify.swf', 
    						//downloadImage: 'QueryViewer/oatPivot/images/download_file.png',
    						swf: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf'),
    						downloadImage: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'), 
    						width: 21,
    						height: 21,
    						transparent: true,
    						append: false
  						});}, 100);	
  						
  					exportXMLButton.appendChild(exportXMLButtonSub);
  					var pvpl = OAT.Dom.create("label");
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXml");
            		pvpl.style.paddingLeft = "9px";
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXMLButton.appendChild(pvpl);							          	
				}
			
				content.appendChild(exportXMLButton);
				if (!someExport) someExport = true;
			}
			return someExport;
	}
	
	this.appendExportToHtmlOption = function(content, someExport){
			var exportHTMLButton;
			var fileName = this.query;
			if (fileName == ""){
				try {
            		fileName = self.controlName.substr(4).split("_")[0]
            	} catch (error) {}	
            }
			if (self.QueryViewerCollection[self.IdForQueryViewerCollection].ExportToHTML){
				if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){
					exportHTMLButton = OAT.Dom.create("div");
					var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportButtonSub.setAttribute('id', 'divtoxml');
  					exportHTMLButton.appendChild(exportButtonSub);
  					 
  					var exportimg = OAT.Dom.create("img");
  					exportimg.src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png')
  					exportButtonSub.appendChild(exportimg)
					
					exportHTMLButton.setAttribute("class", "export_item_div");
					var pvpl = OAT.Dom.create("label");
					pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportHtml");
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportHTMLButton.appendChild(pvpl);
            		
					OAT.Dom.attach(exportButtonSub, "click",  function() {	
														if (!self.serverPaging){
        													self.ExportToHtml(self, fileName);
        												} else {
    														OAT_JS.grid.getAllDataRowsForExport(self.UcId, self, fileName, "html")
	    												}
            										});
            	} else {
						exportHTMLButton = document.createElement("div");
						exportHTMLButton.style.marginBottom = "10px" 
						exportHTMLButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
						exportHTMLButtonSub.setAttribute('id', 'divtohtml')
  						setTimeout( function(){
  							jQuery("#divtohtml").downloadify({ 
    							filename: function(){
      									return fileName+'.html';
    							},
    							data: function(){
    								var dir = location.href.substr(0,location.href.indexOf(location.pathname)) + gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/grid.css')
            						var str = "<!DOCTYPE><HTML><BODY>"; 
            												
            						str = str + "<HEAD>";
            						str = str + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>'
            						str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="'+dir+'" />'
            						str = str + "</STYLE>"
            												
            						str = str + "</HEAD>"
            												
            						str = str + jQuery("#" + self.controlName)[0].outerHTML.replace(/display: none;/g,"").replace(/Grid_asc.gif/g,"").replace(/Grid_desc.gif/g, "");
									str = str + "</BODY></HTML>";
    								return str;
    							},
    							onComplete: function(){
    							},
    							onCancel: function(){
    							},
    							onError: function(){
    							},
    							swf: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf'),
    							downloadImage: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'),
								width : 21,
								height : 21,
    							transparent: true,
    							append: false
  							});}, 100);	
  							
  						exportHTMLButton.appendChild(exportHTMLButtonSub);
  						var pvpl = OAT.Dom.create("label");
            			pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportHtml");
            			pvpl.style.paddingLeft = "9px";
            			pvpl.htmlFor = "pivot_checkbox_restoreview";
            			exportHTMLButton.appendChild(pvpl);
					}
            		content.appendChild(exportHTMLButton);
            		if (!someExport) someExport = true;
            }
            return someExport;
	}
	
	this.ExportToHtml = function(self, fileName){
		var dir = location.href.substr(0,location.href.indexOf(location.pathname)) + gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/grid.css')
		var str = "<!DOCTYPE><HTML><BODY>"; 
	
		str = str + "<HEAD>";
		str = str + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>'

		str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="'+dir+'" />'
		str = str + "</STYLE>"
		str = str + "</HEAD>"
		str = str + jQuery("#" + self.controlName)[0].outerHTML.replace(/display: none;/g,"").replace(/Grid_asc.gif/g,"").replace(/Grid_desc.gif/g, "");
		str = str + "</BODY></HTML>";

		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
			window.open('data:text/html,' + str);
		} else {
			var blob = new Blob([str], {type: "text/html"});
        	saveAs( blob, fileName+".html");
		}
	}
	
	this.appendExportToPdfOption = function(content, someExport){
			var fileName = this.query;
			if (fileName == ""){
				try {
            		fileName = self.controlName.substr(4).split("_")[0]
            	} catch (error) {}	
            }
	    	var exportPDFButton;
        	if (self.QueryViewerCollection[self.IdForQueryViewerCollection].ExportToPDF){
        		if ( gx.util.browser.isIE() && (9>=gx.util.browser.ieVersion()) ){
        			exportPDFButton = document.createElement("div");
					exportPDFButton.style.marginBottom = "10px" 
					exportPDFButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
				
					exportPDFButtonSub.setAttribute('id', 'divtoexport')
  					setTimeout( function(){
  						jQuery("#divtoexport").downloadify({
    						filename: function(){
      								return fileName+'.pdf';
    						},
    						data: function(){ 
      									return btoa(OAT.ExportToPdf({grid : self}));
    						},
    						onComplete: function(){
    						},
    						onCancel: function(){
    						},
    						onError: function(){
    						},
    						dataType: 'base64',
    						swf: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf'),
    						downloadImage: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'),
							width : 21,
							height : 21,
    						transparent: true,
    						append: false
  					});}, 100);
  					
  					exportPDFButton.appendChild(exportPDFButtonSub);
  					var pvpl = OAT.Dom.create("label");
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportPdf");
            		pvpl.style.paddingLeft = "9px";
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportPDFButton.appendChild(pvpl);
        		} else {
        			exportPDFButton = OAT.Dom.create("div");
        			var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportButtonSub.setAttribute('id', 'divtoxml');
  					exportPDFButton.appendChild(exportButtonSub);
  					 
  					var exportimg = OAT.Dom.create("img");
  					exportimg.src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png')
  					exportButtonSub.appendChild(exportimg)
  					
					exportPDFButton.setAttribute("class", "export_item_div");
					var pvpl = OAT.Dom.create("label");
					pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportPdf");
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportPDFButton.appendChild(pvpl);
        			
        			if (!self.serverPaging){
        				OAT.Dom.attach(exportButtonSub, "click",  function() { 	
        														OAT.ExportToPdf({grid : self}, fileName);
												            });
					} else {
						OAT.Dom.attach(exportButtonSub, "click",  function() { 	
        														OAT_JS.grid.getAllDataRowsForExport(self.UcId, {grid : self}, fileName, "pdf")
        													});
					}
				}
				/*if (someExport){
            			if (!gx.util.browser.isIE()){
            				content.appendChild(OAT.Dom.create("br"));
            			}
            	} */
				content.appendChild(exportPDFButton);
				if (!someExport) someExport = true;
			} 
             
            return someExport;
    }
	
	this.appendExportToExcelOption = function(content, someExport){
			var fileName = this.query;
			if (fileName == ""){
				try {
            		fileName = self.controlName.substr(4).split("_")[0]
            	} catch (error) {}	
            }							
            var exportXLSButton
            if (self.QueryViewerCollection[self.IdForQueryViewerCollection].ExportToXLS){
            	if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){
            		exportXLSButton = OAT.Dom.create("div");
            		var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportButtonSub.setAttribute('id', 'divtoxml');
  					exportXLSButton.appendChild(exportButtonSub);
  					 
  					var exportimg = OAT.Dom.create("img");
  					exportimg.src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png')
  					exportButtonSub.appendChild(exportimg)
					exportXLSButton.setAttribute("class", "export_item_div");
					var pvpl = OAT.Dom.create("label");
					pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXls2003")
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXLSButton.appendChild(pvpl);
            		
            		OAT.Dom.attach(exportButtonSub, "click",  function() {
            												  if (!self.serverPaging){
            													OAT.ExportToExcel({grid : self}, fileName);
            												  } else {
            													OAT_JS.grid.getAllDataRowsForExport(self.UcId, {grid : self}, fileName, "xls")	  	
            												  }
        															});
        		} else {
        			exportXLSButton = document.createElement("div");
					exportXLSButton.style.marginBottom = "10px" 
					exportXLSButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
        			
        			exportXLSButtonSub.setAttribute('id', 'divtoxls');
        			setTimeout( function(){
  						jQuery("#divtoxls").downloadify({ 
    						filename: function(){
      								return fileName+'.xls';
    						},
    						data: function(){
    							return OAT.ExportToExcel({grid : self});
    						},
    						onComplete: function(){
    						},
    						onCancel: function(){
    						},
    						onError: function(){
    						},
    						//transparent: false,
    						swf: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf'),
    						downloadImage: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'),
							width : 21,
							height : 21,
    						transparent: true,
    						append: false
  					});}, 100);	
  					exportXLSButton.appendChild(exportXLSButtonSub);
  					var pvpl = OAT.Dom.create("label");
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXls2003");
            		pvpl.style.paddingLeft = "9px";
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXLSButton.appendChild(pvpl);		
        		}
        		/*if (someExport){
            			if (!gx.util.browser.isIE()){
            				content.appendChild(OAT.Dom.create("br"));
            			}
            	} */
        		content.appendChild(exportXLSButton);
        		if (!someExport) someExport = true;
        	} 
        	
            return someExport;
	}
	
	this.appendExportToExcel2010Option = function(content, someExport){
			var fileName = this.query;
			if (fileName == ""){
				try {
            		fileName = self.controlName.substr(4).split("_")[0]
            	} catch (error) {}	
            }									
            var exportXLSButton
            if (self.QueryViewerCollection[self.IdForQueryViewerCollection].ExportToXLSX){
            	if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){
            		exportXLSButton = OAT.Dom.create("div");
            		var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportButtonSub.setAttribute('id', 'divtoxml');
  					exportXLSButton.appendChild(exportButtonSub);
  					 
  					var exportimg = OAT.Dom.create("img");
  					exportimg.src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png')
  					exportButtonSub.appendChild(exportimg)
					exportXLSButton.setAttribute("class", "export_item_div");
					var pvpl = OAT.Dom.create("label");
					pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXlsx")
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXLSButton.appendChild(pvpl);
            		
            		OAT.Dom.attach(exportButtonSub, "click",  function() { 
            															if (!self.serverPaging){
            																OAT.ExportToExcel2010({grid : self}, fileName);
            															} else {
            																OAT_JS.grid.getAllDataRowsForExport(self.UcId, {grid : self}, fileName, "xlsx")
            															}
        															});
        		} else {
        			exportXLSButton = document.createElement("div");
					exportXLSButton.style.marginBottom = "10px" 
					exportXLSButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
        			
        			exportXLSButtonSub.setAttribute('id', 'divtoxlsx');
        			setTimeout( function(){
  						jQuery("#divtoxlsx").downloadify({ 
    						filename: function(){
      								return fileName+'.xlsx';
    						},
    						data: function(){
    							return OAT.ExportToExcel2010({grid : self});
    						},
    						onComplete: function(){
    						},
    						onCancel: function(){
    						},
    						onError: function(){
    						},
    						//transparent: false,
    						dataType: 'base64',
    						swf: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf'),
    						downloadImage: gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'),
							width : 21,
							height : 21,
    						transparent: true,
    						append: false
  					});}, 100);	
  					exportXLSButton.appendChild(exportXLSButtonSub);
  					var pvpl = OAT.Dom.create("label");
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXlsx");
            		pvpl.style.paddingLeft = "9px";
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXLSButton.appendChild(pvpl);		
        		}
        		/*if (someExport){
            			if (!gx.util.browser.isIE()){
            				content.appendChild(OAT.Dom.create("br"));
            			}
            	} */
        		content.appendChild(exportXLSButton);
        		if (!someExport) someExport = true;
        	} 
        	
            return someExport;
	}
	
    this.clearData = function() {
        self.rows = [];
        OAT.Dom.clear(self.rowBlock);
    }

    this.appendHeader = function(paramsObj, dataField, index) { /* append one header */
        var i = (!index ? self.header.cells.length : index);
        var cell = self.header.addCell(paramsObj, "character" ,i, dataField);
        for (var i = 0; i < self.header.cells.length; i++) {
            self.header.cells[i].number = i;
        }
        return cell;
    }

    this.ieFix = function() { /* xxx */
        for (var i = 0; i < self.header.cells.length; i++) {
            var html = self.header.cells[i].html;
            OAT.Dom.addClass(html, "hover");
            OAT.Dom.removeClass(html, "hover");
            var value = self.header.cells[i].value;
            var dims = OAT.Dom.getWH(value);
        }
    }

    this.createHeader = function(paramsList, fieldList) { /* add new header */
        self.header.clear();
        if (self.options.autoNumber) {
            var cell = self.header.addCell({ value: "&nbsp;#&nbsp;", align: OAT.GridData.ALIGN_CENTER, type: OAT.GridData.TYPE_NUMERIC, draggable: 0, sortable: 0 }, "character");
            var click = function() { for (var i = 0; i < self.rows.length; i++) { self.rows[i].select(); } } /* select all */
            OAT.Event.attach(cell.html, "click", click);
        }
        for (var i = 0; i < paramsList.length; i++) {
            self.appendHeader(paramsList[i], fieldList[i]);
        }
        if (OAT.Browser.isIE) { self.ieFix(); }
    } /* Grid::createHeader */

    this.createRow = function(paramsList, columnsDataType, defaultPicture, customPicture, conditionalFormatsColumns, formatValues, customFormat, column, index, columnVisibility) { /* add new row */
        var number = (!index ? self.rows.length : index);
        var row = new OAT.GridRow(self, number);
        if (index == number || number == self.rows.length) {
            self.rowBlock.appendChild(row.html);
        } else {
            self.rowBlock.insertBefore(row.html, self.rowBlock.childNodes[number]);
        }
        if (self.options.autoNumber) {
            row.addCell({ value: self.options.rowOffset + number + 1, align: OAT.GridData.ALIGN_CENTER }, columnsDataType[i]);
            OAT.Dom.addClass(row.cells[0].html, "index");
        }

        for (var i = 0; i < paramsList.length; i++) {
        	if  ( (column[i] ==undefined) || (column[i].getAttribute("defaultPosition")==undefined) || (column[i].getAttribute("defaultPosition") != "hidden")) 
        		row.addCell(paramsList[i], columnsDataType[i], defaultPicture, customPicture[i], conditionalFormatsColumns, formatValues, customFormat, i, index, columnVisibility[i]); 
        }
        self.rows.splice(number, 0, row);
        return row.html;
    } /* Grid::createRow() */

	this.loadDifferentValues = function(columnNumber, values){
		self.conditions[columnNumber].differentValue = [];
		for (var i = 0; i < values.length; i++){
			self.conditions[columnNumber].differentValue.push(values[i]);
		}
	}

	this.removeAllRows = function(){
		var totalRows = self.rows.length
		for (var l = 0; l < totalRows; l++) {
			self.removeRow(0)
		}
	}
	
	this.removeAllHiddenRows = function(){
		var totalRows = self.rows.length
		var rowNumber = self.rows.length-1;
		while ((rowNumber>0) && (self.rows[rowNumber].html.style.display == "none")){
				self.removeRow(rowNumber);
				rowNumber--;
		}
	}
	
	this.removeRow = function(rowNumber){
		jQuery(self.rows[rowNumber].html).remove()
		
		self.rows.splice(rowNumber , 1);
	}

    this.removeColumn = function(index) {
        self.header.removeColumn(index);
        for (var i = 0; i < self.rows.length; i++) { self.rows[i].removeColumn(index); }
    }

	this.hideColumnHeader = function(index){
		var headerPos = /*self.columnsDataType.length - 1 -*/ index;
		self.header.cells[headerPos].html.style.display = "none";
    }
    
    this.showColumnHeader = function(index){
		var headerPos = self.columnsDataType.length - 1 - index;
		self.header.cells[headerPos].html.style.display = "";
    }

    this.sort = function(index, type, numCol) { 
        if (OAT.GridData.forbidSort) { return; }
		
		
        if (self.options.sortFunc) {
            self.options.sortFunc(numCol, type);
            return;
        }
        for (var i = 0; i < self.header.cells.length; i++) {
        	/* aca cambio la info de estado sobre las oredenaciones de las columnas */
        	self.conditions[i].sort = 1;
            self.header.cells[i].changeSort(OAT.GridData.SORT_NONE);
        }
        self.conditions[index].sort = type;
        self.header.cells[numCol].changeSort(type);
        /* sort elements here */
        var coltype = self.header.cells[numCol].options.type;
        var c1, c2;
        switch (type) {
            case OAT.GridData.SORT_ASC: c1 = 1; c2 = -1; break;
            case OAT.GridData.SORT_DESC: c1 = -1; c2 = 1; break;
        }
        
        var useCustomOrder = (OAT_JS.grid.gridData[self.UcId].customOrderValues) 
        					&& (OAT_JS.grid.gridData[self.UcId].customOrderValues[index] != false) 
        					&& (OAT_JS.grid.gridData[self.UcId].customOrderValues[index].length > 0)
        					  
        
        var numCmp = function(row_a, row_b) {
            var a = row_a.cells[index].options.value; 
            var b = row_b.cells[index].options.value; 
            if (a == b) { return 0; }
            return (parseFloat(a) > parseFloat(b) ? c1 : c2);
        }
        var strCmp = function(row_a, row_b) {
            var a = row_a.cells[index].options.value; 
            var b = row_b.cells[index].options.value; 
            if (a == b) { return 0; }
            return (a > b ? c1 : c2);
        }
        var cusCmp = function(row_a, row_b) {
            var a = row_a.cells[index].options.value; 
            var b = row_b.cells[index].options.value;
            var indexA = OAT_JS.grid.gridData[self.UcId].customOrderValues[index].indexOf(a)
            var indexB = OAT_JS.grid.gridData[self.UcId].customOrderValues[index].indexOf(b) 
            if (indexA == indexB) { return 0; }
            return (indexA > indexB ? c1 : c2);
        }
        var cmp;

        if ((!self.rows.length) || (self.serverPaging)) { return; } /* no work to be done */

        var testValue = self.rows[0].cells[index].options.value; 
        switch (coltype) {
            case OAT.GridData.TYPE_STRING: cmp = strCmp; break;
            case OAT.GridData.TYPE_NUMERIC: cmp = numCmp; break;
            case OAT.GridData.TYPE_AUTO: cmp = (testValue == parseFloat(testValue) ? numCmp : strCmp); break;
        }
        if (useCustomOrder) { cmp = cusCmp; }
        self.rows.sort(cmp);

        /* redo dom, odd & even */
        for (var i = 0; i < self.rows.length; i++) {
            self.rowBlock.appendChild(self.rows[i].html);
            var h = self.rows[i].html;
            OAT.Dom.removeClass(h, "WorkWithEven");
            OAT.Dom.removeClass(h, "WorkWithOdd");
            OAT.Dom.addClass(self.rows[i].html, (i % 2 ? "WorkWithEven" : "WorkWithOdd"));
        }
    } /* Grid::sort() */

	this.applySaveState = function(actualPagesize){
		if ((self.rememberLayout) && (self.rememberLayout!="false")){ 											
			self = this;
			var exists = OAT.getState(self, actualPagesize);
			if (exists){
				OAT.filterRows(self);
			
			
				for(var jC = 0; jC < self.columnsDataType.length; jC++){
					if (self.conditions[jC].sort!=1){
						self.sort( jC, self.conditions[jC].sort, /*self.columnsDataType.length -1 -*/ jC);
					}
				}
			}
		}
	}
	
	this.applySortOrderType = function(columnNumber, sortOrder){
		self.sort(columnNumber, sortOrder, /*self.columnsDataType.length -1 -*/ columnNumber)
	}
	
	this.applyCustomSort = function(columnNumber, sortData, dataRows){
		    var distinctValues = [];
        	for(var h=0; h<sortData.childNodes.length;h++){
        		if ((sortData.childNodes[h]!=undefined) &&
        			(sortData.childNodes[h].localName!=undefined) &&
						(sortData.childNodes[h].localName==="customOrder")) 
        		{
        			for (var n = 0; n < sortData.childNodes[h].childNodes.length; n++) {
        				if (sortData.childNodes[h].childNodes[n].localName == "Value") {
        					distinctValues.push(sortData.childNodes[h].childNodes[n].textContent.trim());
        				}
        			}
        		}
        	}
        	
        	for(var d=0; d < dataRows.length; d++){
        		if ( distinctValues.indexOf(dataRows[d][columnNumber].trim()) == -1){
        			distinctValues.push(dataRows[d][columnNumber])
        		}
        	}
        	
        	var temp = dataRows;
        	dataRows = [];
        	for (var i=0; i < distinctValues.length; i++){
        		for(var j=0; j < temp.length; j++){
        			if(temp[j][columnNumber].trim()===distinctValues[i].trim()){
        				dataRows.push(temp[j]);
        			}
        		}
        	}
        	return [dataRows, distinctValues];
	}
	
	this.applyCustomFilter = function(columnNumber, filterData){
		try {
			var includeValues = []; //the only values to show
			var applyFilter = false;
			for(var h=0; h<filterData.childNodes.length;h++){
        		if ((filterData.childNodes[h]!=undefined) &&
        			(filterData.childNodes[h].localName!=undefined) &&
						(filterData.childNodes[h].localName==="include")) 
        		{
        			applyFilter = true;
        			for (var n = 0; n < filterData.childNodes[h].childNodes.length; n++) {
        				if ((filterData.childNodes[h].childNodes[n].localName != null) && 
        				  (filterData.childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        					includeValues.push(filterData.childNodes[h].childNodes[n].textContent);
        				}
        			}
        		}
         	}
         
         	if (applyFilter){
         		self.conditions[columnNumber].blackList = [];
         		for (var i = 0; i < self.rows.length; i++) {
         		   var col =  columnNumber;
         		   if ((includeValues.find(self.rows[i].cells[col].options.value)===-1) 
         		   	&& (self.conditions[col].blackList.find(self.rows[i].cells[col].options.value)===-1)){ 
         		   	self.conditions[col].blackList.push(self.rows[i].cells[col].options.value);
         	  		OAT.HideGridRow({grid : self}, columnNumber, self.rows[i].cells[columnNumber].options.value);
           			OAT.actualizeBlackList("push", self, columnNumber, self.rows[i].cells[columnNumber].options.value, false);
           		  }
            	}
         	}
        } catch (error){
        	
        }
	}
	
	this.moveToNextPage = function(){
		self.oat_component.moveToNextPage(self.UcId);
	}
	
	this.moveToFirstPage = function(){
		self.oat_component.moveToFirstPage(self.UcId);
	}
	
	this.moveToLastPage = function(){
		self.oat_component.moveToLastPage(self.UcId);
	}
	
	this.moveToPreviousPage = function(){
		self.oat_component.moveToPreviousPage(self.UcId);
	} 
	
	this.refreshPivot = function(metadata, data, sameQuery){
		
		if((metadata != "") && (data != "")) {
			var parser = new DOMParser();
			var xmlData = parser.parseFromString(metadata, 'text/xml');
			var dimensions = xmlData.getElementsByTagName("OLAPDimension");
			
			if (!self.serverPaging){
				OAT.RestoreGridRow({
					grid : self
				});
			}
				
				var dataFieldOrderChanged = "";
				var OrderChanged = "";
				var colPosition = [];
				for(var dim = 0; dim < dimensions.length; dim++) { //for every dimensions of the other querie
					var dimID = dimensions[dim].getElementsByTagName("name")[0].childNodes[0].nodeValue; //get the name - "Identifier" of this dimension
					 
					//now search for this name at this querie
					var dimPos = -1; //the columns number of the dimension in this table
					var dimHeader = -1; //the number of the title (bug!)
					for(var itC = 0; itC < self.columns.length; itC++){
						if (self.columns[itC].attributes.getNamedItem("name").nodeValue === dimID){
							var display = self.columns[itC].attributes.getNamedItem("displayName").nodeValue
							//search in the header for "display" name to know the position
							for(var pD=0; pD < self.header.cells.length; pD++){
								if (self.header.cells[pD].options.value === display){
									dimPos = self.header.cells.length - 1 - pD;		//this is the number of the columns of the dimesnion at this table
									dimHeader = pD;		
								}
							}
						}
					}
					
					if (dimPos != -1){ //the dimension exists in this table
						//make the changes
						var position = dimensions[dim].getElementsByTagName("condition")[0].childNodes[0].nodeValue;
						if(position === "none") {
							var newdisp = "none";
							self.header.cells[dimHeader].html.style.display = newdisp;
							for(var i = 0; i < self.rows.length; i++) {
								self.rows[i].cells[dimPos].html.style.display = newdisp;
							}
							if (self.serverPaging){
                    			OAT_JS.grid.setColumnVisibleValue(self.UcId, dimHeader, (newdisp == ""))
                    		}
						} else {
							var newdisp = "";
							self.header.cells[dimHeader].html.style.display = newdisp;
							for(var i = 0; i < self.rows.length; i++) {
								self.rows[i].cells[dimPos].html.style.display = newdisp;
							}
							if (self.serverPaging){
                    			OAT_JS.grid.setColumnVisibleValue(self.UcId, dimHeader, (newdisp == ""))
                    		}
						}
						
						//set column position
						try {
							if (self.serverPaging){
								var dataFieldId = OAT_JS.grid.gridData[self.UcId].columnDataField[dimHeader];
								colPosition[parseInt(dimensions[dim].getElementsByTagName("position")[0].childNodes[0].nodeValue)] = dataFieldId
							}	
						} catch (error) {}
						//reset blacklists
						if (self.serverPaging){
							var dataFieldId = OAT_JS.grid.gridData[self.UcId].columnDataField[dimHeader];
							OAT_JS.grid.updateFilterInfo(self.UcId, dataFieldId, { op: "all", values: []});
						}
						
						//set order value if changed
						var order = dimensions[dim].getElementsByTagName("order")[0].childNodes[0].nodeValue;
						if ((order == "ascending") && (self.conditions[dimPos].sort == 3)){
							self.sort(dimPos, OAT.GridData.SORT_ASC, dimHeader); 
							OrderChanged = "Ascending"
							dataFieldOrderChanged = dimensions[dim].getElementsByTagName("dataField")[0].childNodes[0].nodeValue
	
							//OAT.saveState(self, true);
						} else if ((order == "descending") && (self.conditions[dimPos].sort != 3)){
							self.sort(dimPos, OAT.GridData.SORT_DESC, dimHeader);
						
							OrderChanged = "Descending" 
							dataFieldOrderChanged = dimensions[dim].getElementsByTagName("dataField")[0].childNodes[0].nodeValue
	            			
						}
						
						var hides = dimensions[dim].getElementsByTagName("hide")[0].childNodes;
						for(var sofs = 0; sofs < hides.length; sofs++) {
							if(hides[sofs].tagName === "value") {
								if (self.serverPaging){
									var dataFieldId = OAT_JS.grid.gridData[self.UcId].columnDataField[dimHeader];
									var distinctValues = OAT_JS.grid.gridData[self.UcId].differentValues[dataFieldId];
									for(var i = 0; i < distinctValues.length; i++){
										if (distinctValues[i].trim() == hides[sofs].textContent){
											OAT_JS.grid.updateFilterInfo(self.UcId, dataFieldId, { op: "push", values: distinctValues[i] });
										}	
									}
								} else {
									var index = self.conditions[dimPos].blackList.find(hides[sofs].textContent);
									//if not already in the list
									if(index === -1) {
										for (var i = 0; i < self.rows.length; i++) { //search exact value
       										var trimValue = self.rows[i].cells[dimPos].options.value.toString().trim();
       										if (trimValue == hides[sofs].textContent){
												self.conditions[dim].blackList.append(self.rows[i].cells[dimPos].options.value);
												break;
											}
										}
									}
								}
							}	
						}
					} //else the dimension doesnt exist in this table
						
				}
			
			
			if (self.serverPaging){
				if (colPosition.length != OAT_JS.grid.gridData[self.UcId].columnDataField.length){
					colPosition = [];
				}
				var dimensions = xmlData.getElementsByTagName("OLAPDimension");
				OAT_JS.grid.refreshPivotWhenServerPagination(self.UcId, dataFieldOrderChanged, OrderChanged, colPosition);
			} else {
				OAT.filterRows(self);
				OAT.saveState({
					grid : self
				}, true, -1);
			}
		}

	}
	
	this.getDataXML = function(){
		if (self.serverPaging){
			spl = self.IdForQueryViewerCollection;
			var temp = self.QueryViewerCollection[spl].getPivottableData_JS();
			var dataStr = temp.split("<OLAPData")[1];
   			dataStr = "<OLAPData " + dataStr
   			return dataStr;
		} else {
			var dataStr = '<OLAPData format="adonet">\n <Table>\n'
			for (var i = 0; i < self.rows.length; i++) {
				dataStr = dataStr + "  <Record>\n"
				for(var iCV=0; iCV < self.columns.length; iCV++){
    				dataStr = dataStr + '   <' + self.columns[iCV].getAttribute("dataField")  + '>'
    				dataStr = dataStr + self.rows[i].cells[iCV].options.value
    				dataStr = dataStr + '</' + self.columns[iCV].getAttribute("dataField")  + '>\n'
    			} 
    			dataStr = dataStr + "  </Record>\n" 
    		}
    		dataStr = dataStr + " </Table>\n</OLAPData>"
			return dataStr
		} 	
	}
	
	this.getFilteredDataXML = function(){
		if (self.serverPaging){
			return OAT_JS.grid.getTableWhenServerPagination(self.UcId);
		} else {
			var dataStr = '<Table>\n'
			for (var i = 0; i < self.rows.length; i++) {
				if ((self.rows[i].html.style.display != "none") || 
				   (self.rows[i].html.getAttribute("visibq") == undefined) || (self.rows[i].html.getAttribute("visibq") == "tt"))
				{
				//if (self.rows[i].html.style.display != "none"){
					dataStr = dataStr + "  <Record>\n"
					for(var iCV=0; iCV < self.columns.length; iCV++){
    					dataStr = dataStr + '   <' + self.columns[iCV].getAttribute("dataField")  + '>'
    					dataStr = dataStr + self.rows[i].cells[iCV].options.value
    					dataStr = dataStr + '</' + self.columns[iCV].getAttribute("dataField")  + '>\n'
    				}
    				dataStr = dataStr + "  </Record>\n"
    			}
    		}
    		dataStr = dataStr + "</Table>\n"
			return dataStr
		}	
	}
	
	
	this.getMetadataXML = function(){
		
		
		var xml = '<OLAPCube format="'+ "compact" +'" thousandsSeparator="'+ "," +'" decimalSeparator="'+ "." +'" dateFormat="'+ "MDY" +'">';
    	 
    	for(var iCV=0; iCV < self.columns.length; iCV++){   	    	
    		xml = xml + '<OLAPDimension> ';
    	
    		xml = xml + '<name>'+self.columns[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+self.columns[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+self.columns[iCV].getAttribute("description")+'</description> ';
    		xml = xml + '<dataField>'+self.columns[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+self.columns[iCV].getAttribute("dataType")+'</dataType> ';
    		xml = xml + '<defaultPosition>'+self.columns[iCV].getAttribute("defaultPosition")+'</defaultPosition> ';
    		xml = xml + '<validPositions>'+self.columns[iCV].getAttribute("validPositions")+'</validPositions> ';
    		xml = xml + '<summarize>'+self.columns[iCV].getAttribute("summarize")+'</summarize> ';
    		xml = xml + '<align>'+self.columns[iCV].getAttribute("align")+'</align> ';
    		
    		if (self.columns[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ self.columns[iCV].getAttribute("picture") +'</picture> ';
    		}
    		
    		if (self.columns[iCV].getAttribute("format") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ self.columns[iCV].getAttribute("format") +'</format> ';
    		}
    		//xml = xml + '<order>'+this.columns[iCV].getAttribute("order")+'</order> ';
    		if (self.conditions[iCV].sort===2)
    			xml = xml + '<order>ascending</order> '
    		else if (self.conditions[iCV].sort===1)
    			xml = xml + '<order>none</order> '
    		else
    			xml = xml + '<order>descending</order> '
    			
    		xml = xml + '<popupDisabled>'+self.columns[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		xml = xml + '<customOrder/> ';
    		xml = xml + '<include> ';
    		
    		var previusValue = [];
        	for (var i = 0; i < self.rows.length; i++) { /* se prodria agregar aqui blaklist (no sustituyendo por que dejaria de funcionar para chart)*/
       			if (self.rows[i].cells[iCV]!=undefined){
       				if (previusValue.find(self.rows[i].cells[iCV].options.value)=== -1){
       					if (self.conditions[iCV].blackList.find( self.rows[i].cells[iCV].options.value ) === -1 ){
            				xml = xml + '<value>' + self.rows[i].cells[iCV].options.value +'</value> ';
            				previusValue.push( self.rows[i].cells[iCV].options.value );
    					}
    				}
    			}
    		}
    		
    		xml = xml + '<value>TOTAL</value> </include> <collapse/> null null null ';
    		
    		    		
    		xml = xml + ' </OLAPDimension>';
    	}
	
		xml = xml + "</OLAPCube>";
    	
    	return xml;
	}
	
    self.init();
    
   var itvl = setInterval(function(){
   			 
			if((jQuery("#" + self.controlName).length > 0) && (jQuery("#" + self.controlName)[0].getAttribute("class") === "oatgrid")) {
				var actual_rowsPerPage = 0;
				if(jQuery("#" + self.controlName + "tablePagination_rowsPerPage").length > 0) {
					//jQuery(".pivot_pag_div").css({marginBottom: "0px"})
					actual_rowsPerPage = parseInt(jQuery("#" + self.controlName + "tablePagination_rowsPerPage")[0].value);
					if(!isNaN(actual_rowsPerPage)) {
						if(self.rowsPerPage != actual_rowsPerPage) {
							var stateChange = (self.rowsPerPage!="")
							self.rowsPerPage = actual_rowsPerPage;
							var conteiner = {
								grid : self
							};
							if (stateChange){
								OAT.saveState(conteiner, false);
							}
						} else {
							self.rowsPerPage = actual_rowsPerPage;
						}
					}
					var wd2 = jQuery("#" + self.controlName)[0].offsetWidth - 1;
					var wd3 = wd2 - 3;
					try{ //for smooth forms
    					if ($("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    						wd3 = wd2 + 1;
    						
    						if (gx.util.browser.isIE()) {
								jQuery(self.div).css({width: wd3+'px'})	
							}
    					}
    				} catch (Error) {
    				}
					try {
						var borderWidth = jQuery("#" + self.controlName + "_tablePagination").css("border-right-width");
						if ((borderWidth != undefined) && (borderWidth[0]!='0')){
							wd2 = wd2 - 1;
						}
					} catch (ERROR) {}
					
					jQuery("#" + self.controlName + "_tablePagination").css({
						width : wd2 + "px"
					});
					jQuery("#" + self.controlName + "_grid_top_div").css({
						width : wd3 + "px"
					});
				} else {
					/*var wd2 = jQuery("#" + self.controlName)[0].offsetWidth - 1;
					var wd3 = wd2 - 3;
					jQuery("#" + self.controlName + "_grid_top_div").css({
						width : wd3 + "px"
					});
					jQuery(".oatgrid").css({
						marginBottom : "0px"
					})*/
					var wid_topBar = jQuery("#" + self.controlName)[0].offsetWidth - 4;
					try{ 
    					if ($("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    						wid_topBar = wid_topBar + 4;
    						
    						if (gx.util.browser.isIE()) {
								jQuery(self.div).css({width: wid_topBar+'px'})	
							}
    					}
    				} catch (Error) {
    				}
					jQuery("#" + self.controlName + "_grid_top_div").css({
						width: wid_topBar + "px"
					})
					jQuery(".oatgrid").css({ marginBottom: "0px" })
				}
				
				
				
				//actualizar colores

				if(jQuery("#" + self.controlName + " tr").length < 500) {
					var nP = 1;
					for(var i = 1; i < jQuery("#" + self.controlName + " tr").length; i++) {
						if(jQuery("#" + self.controlName + " tr")[i].style.display != "none") {
							if(nP % 2 === 1) {
								jQuery("#" + self.controlName + " tr")[i].className = 'odd';
							} else {
								jQuery("#" + self.controlName + " tr")[i].className = 'even';
							}
							nP++;
						}
					}
				}

				if((jQuery("#" + self.controlName + "tablePagination_rowsPerPage").length > 0) && (self.QueryViewerCollection.length === 0)) {
					jQuery(".pivot_pag_div").css({
						marginBottom : "0px"
					})
				}
			}

        	} , 250);
        	   	
        	
}  /* Grid */

OAT.GridHeader = function(grid) {
	var self = this;
	this.cells = [];
	this.grid = grid;
	this.html = OAT.Dom.create("thead");
	this.container = OAT.Dom.create("tr");
	this.html.appendChild(self.container);
	
	this.clear = function() {
		OAT.Dom.clear(self.container);
		self.cells = [];
	}
	
	this.addCell = function(params, columnsDataType, index, dataField) {
		var cell = new OAT.GridHeaderCell(self.grid,params, index, dataField);
		var tds = self.container.childNodes;
		
		if (tds.length && index < tds.length) {
			self.container.insertBefore(cell.html,tds[index]);
		} else { self.container.appendChild(cell.html); }
		
		self.cells.splice(index,0,cell);
		return cell;
	}
	
	self.removeColumn = function(index) {
		OAT.Dom.unlink(self.cells[index].html);
		self.cells.splice(index,1);
		for (var i=0;i<self.cells.length;i++) { self.cells[i].number = i; }
	}
} /* GridHeader */

OAT.GridHeaderCell = function(grid, params_, number, dataField) {
	var self = this;
	this.options = {
		value:"",
		sortable:1,
		draggable:1,
		resizable:1,
		align:OAT.GridData.ALIGN_LEFT,
		sort:OAT.GridData.SORT_NONE,
		type:OAT.GridData.TYPE_AUTO
	}
	
	var params = (typeof(params_) == "object" ? params_ : {value:params_});
	for (var p in params) { self.options[p] = params[p]; }
	
	this.signalStart = function() { /* red line */
		self.signal = 1;
		var dims = OAT.Dom.getWH(self.container);
		self.signalElm = OAT.Dom.create("div",{position:"absolute",width:"2px",height:(dims[1]+2)+"px",left:"-2px",top:"-1px",backgroundColor:"#f00"});
		self.container.appendChild(self.signalElm);
	}
	
	this.signalEnd = function() {
		self.signal = 0;
		OAT.Dom.unlink(self.signalElm);
	}
	
	this.changeWidth = function(width) {
		var w = width;
		if (!w) {
			self.value.style.width = "";
			return;
		}
		if (!self.value.style.width || self.value.style.width == "auto") {
			w -= parseInt(OAT.Style.get(self.value,"paddingLeft"));
			w -= parseInt(OAT.Style.get(self.value,"paddingRight"));
		}
		
		self.value.style.width = w + "px";
	}
	
	this.changeSort = function(type) {
		self.options.sort = type;
		self.updateSortImage();
	}

	this.updateSortImage = function() {
		if (!self.sorter) { return; }
		var path = "none";
		switch (self.options.sort) {
			case OAT.GridData.SORT_NONE: path = "none"; break; //1
			case OAT.GridData.SORT_ASC: path = "asc"; break;   //2
			case OAT.GridData.SORT_DESC: path = "desc"; break; //3
		}
		self.sorter.style.backgroundImage = "url("+self.grid.options.imagePath+"Grid_"+path+".gif)";	
	}
	
	this.signal = 0;
	this.number = number;
	this.grid = grid;
	
	this.html = OAT.Dom.create("td"); /* cell */
	this.container = OAT.Dom.create("div",{position:"relative"}); /* cell interior */
	this.value = OAT.Dom.create("div",{overflow:"hidden"});
	OAT.Dom.addClass(self.value,"header_value");
	OAT.Dom.append([self.html,self.container],[self.container,self.value]);
	this.value.innerHTML = params.value.replace(/ /g, "&nbsp;");
	this.value.setAttribute("title_v", params.value);
	this.value.setAttribute("dataField", dataField);
	this.html.setAttribute("title_v", params.value);
	this.html.setAttribute("dataField", dataField);
	
	if (self.options.sortable) {
		self.html.style.cursor = "pointer";
		//self.value.style.paddingRight = "14px";
		self.sorter = OAT.Dom.create("div",{position:"absolute",right:"0px",bottom:"2px",width:"12px",height:"12px"});
		self.container.appendChild(self.sorter);
		self.updateSortImage();
		var divCont;
		if (gx.util.browser.isIE()){
				divCont = OAT.Dom.create("div", "", "oatfilterwindowGrid");
			} else {
			  divCont = OAT.Dom.create("div", "", "oatfilterwindow");
			}
		OAT.Anchor.assign(self.container, { title: " ",
                content: divCont,
                result_control: false,
                activation: "click",
                type: OAT.WinData.TYPE_RECT,
                width: "auto"
        });
		var callback = function(event) {
			var type = OAT.GridData.SORT_NONE;
			switch (self.options.sort) {
				case OAT.GridData.SORT_NONE: type = OAT.GridData.SORT_ASC; break;
				case OAT.GridData.SORT_ASC: type = OAT.GridData.SORT_DESC; break;
				case OAT.GridData.SORT_DESC: type = OAT.GridData.SORT_ASC; break;
			}
			OAT.showPopup(event, self, type, divCont);
			//self.grid.sort(self.number,type);
		}
		OAT.Event.attach(self.container,"click",callback);
		//OAT.Event.attach(self.container,"mouseup",callback);
	}

	if (self.options.resizable) {
		var mover = OAT.Dom.create("div",{width:"7px",height:"100%",position:"absolute",right:"-5px",top:"0px",cursor:"e-resize"});
		mover.style.backgroundImage = "url("+self.grid.options.imagePath+"Grid_none.gif)";
		self.container.appendChild(mover);
		var callback = function (event) { /* start resizing */
			var pos = OAT.Dom.eventPos(event);
			var dims_grid = OAT.Dom.getWH(self.grid.html);
			var dims_container = OAT.Dom.getWH(self.container);
			var dims_value = OAT.Dom.getWH(self.value);

			OAT.GridData.resizing = self.grid;
			OAT.GridData.index = self.number;
			OAT.GridData.mouseX = pos[0];
			OAT.GridData.w = dims_value[0]; /* total width to be changed */
			var left1 = -2;
			var left2 = dims_container[0];
			if (OAT.Browser.isIE6 && !self.value.style.width) {
				left1 -= 2;
				left2 -= 4;
			}
			OAT.GridData.x = left2; /* initial position of moving red line */
			self.grid.tmp_resize_start = OAT.Dom.create("div",{position:"absolute",left:left1+"px",top:"-1px",backgroundColor:"#f00",width:"2px",height:dims_grid[1]+"px"});
			self.grid.tmp_resize = OAT.Dom.create("div",{position:"absolute",left:left2+"px",top:"-1px",backgroundColor:"#f00",width:"2px",height:dims_grid[1]+"px"});
			self.container.appendChild(self.grid.tmp_resize);
			self.container.appendChild(self.grid.tmp_resize_start);
		}
		var nullCallback = function() {
			self.changeWidth(false);
			for (var i=0;i<self.grid.rows.length;i++) {
				self.grid.rows[i].cells[self.number].changeWidth(false);
			}
		}
		OAT.Event.attach(mover,"mousedown",callback);
		OAT.Event.attach(mover,"dblclick",nullCallback);
	}
	
	if (self.options.draggable) {
		var callback = function(event) {
			if (OAT.GridData.resizing) { return; } /* don't drag when resizing */
			OAT.GridData.dragging = false//self.grid;  //grid dragging disable
			OAT.GridData.index = self.number;
			var pos = OAT.Event.position(event);
			OAT.GridData.x = pos[0];
			self.grid.tmp_drag = false;
		}
		OAT.Event.attach(self.container,"mousedown",callback);
	}

	switch (self.options.align) {
		case OAT.GridData.ALIGN_LEFT: self.html.style.textAlign = "left"; break;
		case OAT.GridData.ALIGN_CENTER: self.html.style.textAlign = "center"; break;
		case OAT.GridData.ALIGN_RIGHT: self.html.style.textAlign = "right"; break;
	}	
	
	var mouseover = function(event) { OAT.Dom.addClass(self.html,"hover"); }
	var mouseout = function(event) { OAT.Dom.removeClass(self.html,"hover"); }
	OAT.Event.attach(self.html,"mouseover",mouseover);
	OAT.Event.attach(self.html,"mouseout",mouseout);
} /* GridHeaderCell */

OAT.GridRow = function(grid, number) {
    var self = this;

    this.clear = function() {
        OAT.Dom.clear(self.html);
        self.cells = [];
    }

    this.removeColumn = function(index) {
        OAT.Dom.unlink(self.cells[index].html);
        self.cells.splice(index, 1);
    }
    this.isDecimal = function(expression, max) {
        var decimal = /^[0-9]+(\.[0-9]+)+$/;
        if (expression.match(decimal)) {
            return true;
        } else {
            return false;
        }
    }
    this.addCell = function(params, columnsDataType, defaultPicture, customPicture, conditionalFormatsColumns, formatValues, customFormat, numCol, index, cellVisible) {  
        var i = (!index ? self.cells.length : index);
        if (params==undefined){
        	params=""
        }
        if (this.isDecimal(params, 2)) {
            params = parseFloat(params).toFixed(2); //force only 2 decimal places
        }
        var cell = new OAT.GridRowCell(params, i, columnsDataType, defaultPicture, customPicture, conditionalFormatsColumns, formatValues, customFormat, numCol, cellVisible);
        var tds = self.html.childNodes;
        if (tds.length && i != tds.length) {
            self.html.insertBefore(cell.html, tds[i]);
            if (!cellVisible) tds[i].style.display = "none"
        } else {
            self.html.appendChild(cell.html);
            if (!cellVisible) cell.html.style.display = "none"
        }
        
        OAT.setClickEventHandlers(self, tds[tds.length-1], params, "DIMENSION", numCol, self.grid.rows.length);
        
        self.cells.splice(i, 0, cell);
        return cell.value;
    }

    this.select = function() {
        self.selected = 1;
        //OAT.Dom.addClass(self.html, "selected");
    }

    this.deselect = function() {
        self.selected = 0;
        OAT.Dom.removeClass(self.html, "selected");
    }

    this.grid = grid; /* parent */
    this.cells = [];
    this.html = OAT.Dom.create("tr");
    this.selected = 0;

    OAT.Dom.addClass(self.html, (number % 2 ? "even" : "odd"));

    var mouseover = function(event) { OAT.Dom.addClass(self.html, "hover"); }
    var mouseout = function(event) { OAT.Dom.removeClass(self.html, "hover"); }
    var click = function(event) {
        if (!event.shiftKey && !event.ctrlKey) {
            /* deselect all */
            for (var i = 0; i < self.grid.rows.length; i++) {
                var r = self.grid.rows[i];
                if (r != self) { r.deselect(); }
            }
        }
        if (event.shiftKey) {
            /* select all above */
            var firstAbove = -1;
            var lastBelow = -1;
            var done = 0;
            for (var i = 0; i < self.grid.rows.length; i++) {
                var r = self.grid.rows[i];
                if (r != self) {
                    if (!done && r.selected) { firstAbove = i; } /* first selected above */
                    if (!done && firstAbove != -1) { r.select(); }
                    if (done && r.selected) { lastBelow = i; } /* last selected below */
                } else {
                    done = 1;
                }
            } /* all rows */
            /* if none are above, then try below */
            if (firstAbove == -1 && lastBelow != -1) {
                var done = 0;
                for (var i = 0; i < self.grid.rows.length; i++) {
                    var r = self.grid.rows[i];
                    if (r == self) { done = 1; }
                    if (done && r != self && i < lastBelow) { r.select(); }
                } /* all rows */
            } /* below */
        } /* if shift */

        self.selected ? self.deselect() : self.select();
    }

    OAT.Event.attach(self.html, "mouseover", mouseover);
    OAT.Event.attach(self.html, "mouseout", mouseout);
    OAT.Event.attach(self.html, "click", click);

}      /* GridRow */

OAT.GridRowCell = function(params_, number, columnsDataType, defaultPicture, customPicture, conditionalFormatsColumns, formatValues, customFormat, numCol, visible) {
    var self = this;

    this.options = {
        value: "",
        align: OAT.GridData.ALIGN_LEFT
    }

    var params = (typeof (params_) == "object" ? params_ : { value: params_ });
    for (p in params) { self.options[p] = params[p]; }

    this.html = OAT.Dom.create("td");
    this.container = OAT.Dom.create("div");
    this.value = OAT.Dom.create("div", { overflow: "hidden" });
    OAT.Dom.addClass(self.value, "row_value");
    this.value.innerHTML = OAT.defaultPictureValue(self.options.value, columnsDataType, defaultPicture, customPicture).replace(/ /g, "&nbsp;"); /* Apply Picture */
    
    this.html = OAT.applyFormatValues(this.html, self.options.value, columnsDataType, numCol, formatValues, conditionalFormatsColumns, customFormat); /* Apply Format */
    
    this.html.setAttribute("title", OAT.defaultPictureValue(self.options.value, columnsDataType, defaultPicture, customPicture));//self.options.value);
    OAT.Dom.append([self.html, self.container], [self.container, self.value]);

    //align numbers right
    if (columnsDataType != "character"){
    	if (!isNaN(self.options.value)) {					
    	    self.options.align = 3; //right
    	    /*
    	    var decimalSeparator = self.options.value.indexOf(".");
    	    if (decimalSeparator != -1) {
    	        self.options.value = self.options.value.substr(0, decimalSeparator + 2);
    	    }*/
    	}
    	if (self.options.value) {
    		self.options.align = 3; //right
    	}
    }
    if (columnsDataType === 'date'){
    	self.options.align = OAT.GridData.ALIGN_RIGHT;
    }
    switch (self.options.align) {
        case OAT.GridData.ALIGN_LEFT: self.html.style.textAlign = "left"; break;
        case OAT.GridData.ALIGN_CENTER: self.html.style.textAlign = "center"; break;
        case OAT.GridData.ALIGN_RIGHT: self.html.style.textAlign = "right"; break;
    }

    this.changeWidth = function(width) {
        var w = width;
        if (!w) {
            self.value.style.width = "";
            return;
        }
        if (!self.value.style.width || self.value.style.width == "auto") {
            w -= parseInt(OAT.Style.get(self.value, "paddingLeft"));
            w -= parseInt(OAT.Style.get(self.value, "paddingRight"));
        }
        self.value.style.width = w + "px";
    }

}         /* GridRowCell */


OAT.applyFormatValues = function(td, value, datatype, columnNumber, formatValues, conditionalFormatsColumns, customFormat){ /* Format for dimensions ("header columns") */
    	var measureDataType = datatype;
    	
    	//apply default format
    	var defaultFormats = customFormat[columnNumber];
    	if ((defaultFormats!=null) && (defaultFormats != "")){
    		td = OAT.setStyleValues(td, defaultFormats);
    	}
    	//apply format value
    	for(var i = 0; i < formatValues.length; i++) {
    		if(formatValues[i].columnNumber == columnNumber){ //a format for this column
    			if (formatValues[i].value === value){
    				td = OAT.setStyleValues(td, formatValues[i].format);
    			}
    		}
    	}
    	//apply conditional values
    	var equal = [];
		var notequal = [];
		var greaterThan = [];
		var greaterOrEqual = [];
		var lessThan = [];
		var lessOrEqual = [];
		var greaterOrEqual = [];
		var between = [];
		for(var i = 0; i < conditionalFormatsColumns.length; i++) {
			if(conditionalFormatsColumns[i].columnNumber == columnNumber) { //self.dataColumnIndex) {
				if(conditionalFormatsColumns[i].operation1 == "equal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						equal[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						equal[0] = self.conditionalFormatsColumns[i].value1
					}
					equal[1] = conditionalFormatsColumns[i].format;
				}
				if(conditionalFormatsColumns[i].operation1 == "notequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						notequal[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						notequal[0] = self.conditionalFormatsColumns[i].value1
					}
					notequal[1] = conditionalFormatsColumns[i].format;
				}
				if(conditionalFormatsColumns[i].operation1 == "less") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessThan[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						lessThan[0] = self.conditionalFormatsColumns[i].value1;
					}
					lessThan[1] = conditionalFormatsColumns[i].format;
				}
				if(conditionalFormatsColumns[i].operation1 == "lessequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessOrEqual[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						lessOrEqual[0] = self.conditionalFormatsColumns[i].value1
					}
					lessThan[1] = conditionalFormatsColumns[i].format;
				}
				if(conditionalFormatsColumns[i].operation1 == "greater") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterThan[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						greaterThan[0] = self.conditionalFormatsColumns[i].value1;
					}
					greaterThan[1] = conditionalFormatsColumns[i].format;
				}
				if(conditionalFormatsColumns[i].operation1 == "greaterequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterOrEqual[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						greaterOrEqual[0] = self.conditionalFormatsColumns[i].value1;
					}
					greaterThan[1] = conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation2 && conditionalFormatsColumns[i].operation1 == "greaterequal") {
					greaterOrEqual = []
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						between[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						between[0] = self.conditionalFormatsColumns[i].value1;
					}
					if(self.conditionalFormatsColumns[i].operation2 && self.conditionalFormatsColumns[i].operation2 == "lessequal") {
						if ( (measureDataType === "real") || (measureDataType === "integer")){
							between[1] = parseFloat(self.conditionalFormatsColumns[i].value2);
						} else {
							between[1] = self.conditionalFormatsColumns[i].value2;
						}
						between[2] = conditionalFormatsColumns[i].format;
					}
				}
			}
		}
		
		var comparisons = new Array(3);
		
		if (measureDataType === "real"){
			value = parseFloat(value);
			if ((greaterThan[0]!=undefined) && (greaterThan[0]!="")){
				comparisons[0] = parseFloat(greaterThan[0]);
			}
			if ( (lessThan[0]!=undefined) && (lessThan[0]!="") ){
				comparisons[1] = parseFloat(lessThan[0]);
			}
			if ( (between[0]!=undefined) && (between[0]!="")){
				comparisons[2] = parseFloat(between[0]);
			}
		}
		
		if (measureDataType === "integer"){
			value = parseInt(value);
			if ((greaterThan[0]!=undefined) && (greaterThan[0]!="")){
				comparisons[0] = parseInt( greaterThan[0] );
			}
			if ((lessThan[0]!=undefined) && (lessThan[0]!="")){
				comparisons[1] = parseInt(lessThan[0]);
			}
			if ((between[0]!=undefined) && (between[0]!="")){
				comparisons[2] = parseInt(between[0]);
			}
			
		}
		
	
		if (measureDataType === "date"){
			var dates = value.split("-");
			
			var dateElements = new Array(3);
			dateElements[0] = parseInt(dates[0]);
			dateElements[1] = parseInt(dates[1]);
			dateElements[2] = parseInt(dates[2]);
			
			try {
			if ( (equal[0]!=undefined) || (greaterOrEqual[0]!=undefined) || (lessOrEqual[0]!=undefined) ){
				var cmpar; 
				if (equal[0]!=undefined){
					cmpar = equal[0].split("-");
				} else if (greaterOrEqual[0]!=undefined){
					cmpar = greaterOrEqual[0].split("-");
				} else if (greaterOrEqual[0]!=undefined){
					cmpar = lessOrEqual[0].split("-");
				}
				var cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] == dateElements[0]) || ((cmparElements[0] == dateElements[0]) && (cmparElements[1] == dateElements[1])) ){
			    		if (equal[1] != undefined)
			    			td = OAT.setStyleValues(td, equal[1]);
			    		else if (greaterThan[1] != undefined)
			    			td = OAT.setStyleValues(td, greaterThan[1]);
			    		else if (lessThan[1] != undefined)
			    			td = OAT.setStyleValues(td, lessThan[1]);
			    } 
			    
			}
			
			if ( (notequal[0]!=undefined)){
				var cmpar = notequal[0].split("-");
				var cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] != dateElements[0]) || ((cmparElements[0] != dateElements[0]) && (cmparElements[1] != dateElements[1])) ){
			    		td = OAT.setStyleValues(td, notequal[1]);
			    } 
			    
			}
			
			
			if ( (greaterThan[0]!=undefined) || (greaterOrEqual[0]!=undefined) ){
				var cmpar; 
				if (greaterThan[0].split("-") != undefined){
					cmpar = greaterThan[0].split("-");
				} else {
					cmpar = greaterOrEqual[0].split("-");
				}
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] < dateElements[0]) || ((cmparElements[0] <= dateElements[0]) && (cmparElements[1] < dateElements[1]))
			       || ((cmparElements[0] <= dateElements[0]) &&  (cmparElements[1] <= dateElements[1]) && (cmparElements[2] < dateElements[2]) ) ){
			    		td = OAT.setStyleValues(td, greaterThan[1]);
			    } 
			    
			}
			
			
			if ( (lessThan[0]!=undefined) || (lessOrEqual[0]!=undefined) ){
				var cmpar;
				if (lessThan[0].split("-")  != undefined){
					cmpar = lessThan[0].split("-");
				} else {
					cmpar = lessOrEqual[0].split("-");
				}
				cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
			    
			    if  ( (cmparElements[0] > dateElements[0]) || ((cmparElements[0] >= dateElements[0]) &&  (cmparElements[1] > dateElements[1]))
			       ||    ((cmparElements[0] >= dateElements[0]) &&  (cmparElements[1] >= dateElements[1]) && (cmparElements[2] > dateElements[2]) )   ){
			    		td = OAT.setStyleValues(td, lessThan[1]);
			    } 
			    
			}
			
			if ( (between[0]!=undefined)  &&  (between[1]!=undefined)){
				var cmpar = between[0].split("-");
				var cmpar2 = between[1].split("-");
				cmparElements = new Array(3);
				cmparElements2 = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				 
				cmparElements2[1] = parseInt(cmpar2[1]);
				cmparElements2[2] = parseInt(cmpar2[2]);
				cmparElements2[0] = parseInt(cmpar2[0]);
				
			    if (( (cmparElements[0] <= dateElements[0]) || ((cmparElements[0] <= dateElements[0]) && (cmparElements[1] < dateElements[1]))
			       || ((cmparElements[0] <= dateElements[0]) &&  (cmparElements[1] <= dateElements[1]) && (cmparElements[2] < dateElements[2]) ) )
			       &&
			       ( (cmparElements2[0] > dateElements[0]) || ((cmparElements2[0] >= dateElements[0]) &&  (cmparElements2[1] > dateElements[1]))
			       || ((cmparElements2[0] >= dateElements[0]) &&  (cmparElements2[1] >= dateElements[1]) && (cmparElements2[2] > dateElements[2]) )   )
			    ) 
			    {
			    		td = OAT.setStyleValues(td, between[2]);
			    } 
			    
			}
			
			} catch (ERROR){
				
			}
			
		}
		
		if (measureDataType != "date"){
			if ((equal[0] != undefined) && (value == equal[0])){
				td = OAT.setStyleValues(td, equal[1]);
			}
			if ((notequal[0] != undefined) && (value != notequal[0])){
				td = OAT.setStyleValues(td, notequal[1]);
			}
			if ( ((greaterThan[0] != undefined) && (value > greaterThan[0])) || 
					((greaterOrEqual[0] != undefined)	&& (value >= greaterOrEqual[0])) ){                          
				td = OAT.setStyleValues(td, greaterThan[1]);
			} 
			if ( ((lessThan[0] != undefined) && (value < lessThan[0])) || 
			 ((lessOrEqual[0] != undefined) && (value <= lessOrEqual[0])) ){
				td = OAT.setStyleValues(td, lessThan[1]);
			} 
			if  ((between[0] != undefined && between[1] != undefined) && (value >= between[0] && value <= between[1])) {
				td = OAT.setStyleValues(td, between[2]);
			}
		}
    	
    	return td;
}

OAT.setStyleValues = function(elem, styleValues){
		if (styleValues == undefined)
			return elem;
		function hexToRgb(hex) {
		    // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
    		var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
    		hex = hex.replace(shorthandRegex, function(m, r, g, b) {
        		return r + r + g + g + b + b;
    		});

    		var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    			return result ? {
        			r: parseInt(result[1], 16),
        			g: parseInt(result[2], 16),
        			b: parseInt(result[3], 16)
    			} : null;
		}

    	var styleSplit = styleValues.split(";");
    	for (var j=0; j < styleSplit.length; j++){
    		var particularStyleSplit = styleSplit[j].split(":");
    		
    		switch(particularStyleSplit[0]) {
    			case "color":   if ((particularStyleSplit[1][0]!=undefined) && (particularStyleSplit[1][0]==='#')){
    								elem.style.color = 'rgb(' + hexToRgb(particularStyleSplit[1]).r + ',' + hexToRgb(particularStyleSplit[1]).g + ',' + hexToRgb(particularStyleSplit[1]).b + ')'
    							} else {
    								elem.style.color = particularStyleSplit[1];
    							}
    							break;
    			case "fontStyle":   elem.style.fontStyle = particularStyleSplit[1];
    							break;
    			case "backgroundColor":   elem.style.backgroundColor = particularStyleSplit[1];
    							break;
    			case "textDecoration":   elem.style.textDecoration = particularStyleSplit[1];
    							break;
    			case "fontWeight":   elem.style.fontWeight = particularStyleSplit[1];
    							break;
    			case "fontFamily":   elem.style.fontFamily = particularStyleSplit[1];
    							break;
    			case "fontVariant":   elem.style.fontVariant = particularStyleSplit[1];
    							break;
    			case "fontSize":   elem.style.fontSize = particularStyleSplit[1].replace("px","") + "px";
    							break;
    			case "textAlign":   elem.style.textAlign = particularStyleSplit[1];
    							break;
    			case "lineHeight":   elem.style.lineHeight = particularStyleSplit[1];
    							break;
    			case "textIndent":   elem.style.textIndent = particularStyleSplit[1];
    							break;
    			case "verticalAlign":   elem.style.verticalAlign = particularStyleSplit[1];
    							break;
    			case "wordSpacing":   elem.style.wordSpacing = particularStyleSplit[1];
    							break;
    			case "display":   elem.style.display = particularStyleSplit[1];
    							break;
    			case "borderThickness":   elem.style.borderThickness = particularStyleSplit[1];
    									  elem.style.borderWidth = particularStyleSplit[1] + "px";	
    							break;
    			case "borderColor":   elem.style.borderColor = particularStyleSplit[1];
    							break;
    			case "borderWith":   elem.style.borderWith = particularStyleSplit[1];
    							break;
    			case "borderStyle":   elem.style.borderStyle = particularStyleSplit[1];
    							break;			
    			case "padding":   elem.style.padding = particularStyleSplit[1];
    							break;
    			case "paddingBottom":   elem.style.paddingBottom = particularStyleSplit[1];
    							break;
    			case "paddingLeft":   elem.style.paddingLeft = particularStyleSplit[1];
    							break;
    			case "paddingRight":   elem.style.paddingRight = particularStyleSplit[1];
    							break;
    			case "paddingTop":   elem.style.paddingTop = particularStyleSplit[1];
    							break;				
    		}
    	}
    	return elem;
    }

OAT.defaultPictureValue = function(value, type, defaultPicture, picture){
		if (value=="#NuN#"){
			var defaultNull = defaultPicture.getAttribute("textForNullValues");
    		if (defaultNull == ""){
    			return "&nbsp;";
    		}
    		if (defaultNull != undefined){
    			return defaultNull;
    		}
    		return "&nbsp;";
		}
		
		var thSepF = function(thousandsSeparator, record, picture){
			if ((thousandsSeparator!= undefined) && (thousandsSeparator != null)) {
    			if (record.length > 3){
    				for(var pos = picture.length-1; pos>=0; pos--){
    					if ((picture.length-1-pos) > record.length) break;
    					if (picture[pos] === ","){
    						var ind = picture.length - pos - 1
    						var index = record.length - ind
    						var temp1 = record.substring(0,index)
    						var temp2 = record.substring(index)
    						if (temp1.length>0)
    							record = temp1 + thousandsSeparator +temp2
    						else
    							record = temp2
    					}
    				}
    				
    			}
    			
    		}
    		return record;
		}
		
		var decimalSeparator = (defaultPicture.getAttribute("decimalSeparator") != undefined && defaultPicture.getAttribute("decimalSeparator") != null) ? defaultPicture.getAttribute("decimalSeparator") : ".";
		var decimalPlaces = (defaultPicture.getAttribute("decimalPlaces") != undefined && defaultPicture.getAttribute("decimalPlaces") != null) ? defaultPicture.getAttribute("decimalPlaces") : 2;
		var thseparator = defaultPicture.getAttribute("thousandsSeparator")
		var defaultdate = defaultPicture.getAttribute("dateFormat")
		var newValue = value;
		var lastCharacter = false;
		
		var hasprefix = false;
		var prefix = "";
		if ((type=="integer")  ||  (type=="real")){
			if ((picture!=undefined) || (picture != null)){
				if ((picture[0]!="Z") || (picture[0]!="9") || (picture[0]!=",") || (picture[0]!=".")){
					hasprefix = true;
					var index = 0;
					while ( (picture.length > index) && (picture[index]!="Z") && (picture[index]!="9") && (picture[index]!=",") && (picture[index]!=".") ){
						index++;
					}
					prefix = picture.substring(0, index);
					picture = picture.substring(index);
				}
			}
		}
		
		var hassufix = false;
		var sufix = "";
		if ((type=="integer")  ||  (type=="real")){
			if ((picture!=undefined) || (picture != null)){
				if ((picture[picture.length-1]!="Z") || (picture[picture.length-1]!="9") || (picture[picture.length-1]!=",") || (picture[picture.length-1]!=".")){
					hassufix = true;
					var index = picture.length-1;
					while ( (index > -1) && (picture[index]!="Z") && (picture[index]!="9") && (picture[index]!=",") && (picture[index]!=".") ){
						index--;
					}
					sufix = picture.substring(index+1);
					picture = picture.substring(0, index+1);
				}
			}
		}
		
		switch(type) {
			case "integer":  
			case "real":
			
				var valueSplit = value.split(".");
				var useSeparator = true;
				if ((picture == "") && (type == "integer")){
					useSeparator = false;
				}
				if((picture != undefined) && (picture != "")) {
					useSeparator = picture.indexOf(".") != -1;
					var pictureSplit = picture.split(".");
					
					if (pictureSplit[1]!=undefined){
						decimalPlaces = pictureSplit[1].length;
					} else {
						decimalPlaces = 0;
						useSeparator = false;
					}
					
    				var picturewithoutsep = "";
					for (var ip = 0; ip < pictureSplit[0].length; ip++){
						if (pictureSplit[0][ip]!=","){
							picturewithoutsep = picturewithoutsep + pictureSplit[0][ip];  
						}
					}
    				
    				if (picturewithoutsep.indexOf("9")!=-1){
    					var maxobligatedpos = picturewithoutsep.length - picturewithoutsep.indexOf("9"); 
						if(valueSplit[0].length < maxobligatedpos) {
							var temp = valueSplit[0];
							for(var mi = 0; mi < maxobligatedpos - valueSplit[0].length; mi++) {
								temp = "0" + temp;
							}
							 valueSplit[0] = temp;
						}
					}   				
    				valueSplit[0] = thSepF(thseparator, valueSplit[0], pictureSplit[0]);
    			}
    			
    			if((picture != undefined) && (picture != "")) {	
    				newValue = valueSplit[0];
    			} else {
    				newValue = valueSplit[0];
    			}
    			
    			var z_pos = -1;
    			if((picture != undefined) && (picture != "")) {
					var picteSplit = picture.split(".");
					if (picteSplit.length>1)
						z_pos = picteSplit[1].indexOf("Z");
				}
    			
				if(valueSplit[1] == null) {
					if(useSeparator){
						var numoblig = decimalPlaces;
						if (z_pos != -1){
							numoblig = z_pos;
						}
						if (numoblig>0){
							newValue = newValue + decimalSeparator;
							for(var i = 0; i < numoblig; i++) {
								newValue = newValue + "0";
							}
						}
					}
				} else {
					if(useSeparator){
					
					if (z_pos === -1){ //only 9s in decimal picture
						if(valueSplit[1].length < decimalPlaces) {
							newValue = newValue + decimalSeparator + valueSplit[1];
							for(var i = 0; i < decimalPlaces - valueSplit[1].length; i++) {
								newValue = newValue + "0";
							}
						
						} else {
							if(valueSplit[1].length > decimalPlaces) {
								var newArray = "";
								for(var i = 0; i < decimalPlaces; i++) {
									newArray = newArray + valueSplit[1].charAt(i);
								}
								newValue = newValue + decimalSeparator + newArray;
							} else {
								newValue = newValue + decimalSeparator + valueSplit[1];
							}
						} 
					} else {
						var numberoblig = z_pos;
						if(valueSplit[1].length < decimalPlaces) {
							if ((valueSplit[1].length>0)||(numberoblig>0)){
								newValue = newValue + decimalSeparator + valueSplit[1];
								for(var i = 0; i < numberoblig - valueSplit[1].length; i++) {
									newValue = newValue + "0";
								}
							}
						} else { //idem sin Z
							if(valueSplit[1].length > decimalPlaces) {
								var newArray = "";
								for(var i = 0; i < decimalPlaces; i++) {
									newArray = newArray + valueSplit[1].charAt(i);
								}
								newValue = newValue + decimalSeparator + newArray;
							} else {
								newValue = newValue + decimalSeparator + valueSplit[1];
							}
						} 
					}
					
					}
				}
				
				if (picture.indexOf("9")===-1){
					if (parseFloat(value) === 0) newValue = "";
				}
			break;
			case "date":
				if (value == "") return ""; 
				if(picture === "") {
					var dates = value.split("-");
					
					var newValue = value;
					if((defaultdate != undefined) && (defaultdate != null)) {//if default picture
						pict = defaultdate.split("");
						newValue = "";
						for(var i = 0; i <= 2; i++) {
							if(pict[i] === "M")
								newValue = newValue + dates[1];
							if(pict[i] === "D")
								newValue = newValue + dates[2];
							if(pict[i] === "Y")
								newValue = newValue + dates[0];
							if(i != 2)
								newValue = newValue + "/";
						}
					} else {
						newValue = dates[1] + "/" + dates[2] + "/" + dates[0];
					}
				} else {
					
					var valueSplit = value.split("-");
					newValue = "";
					if(picture == "99/99/9999") {
						if(valueSplit[0].length == 4) {
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];	
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} else {
							newValue = value;
							while(newValue.indexOf("-") != -1) {
								newValue.replace("-", "/");
							}
						}
					} else 
					
						if(picture == "99/99/99") {
							if(valueSplit[0].length == 4) {
								valueSplit[0] = valueSplit[0].substr(valueSplit[0].length - 2, 2);
							}
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];	
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} else 
							if(picture == "9999/99/99") {
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];	
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} else {
							if(valueSplit[0].length == 4) {
								valueSplit[0] = valueSplit[0].substr(valueSplit[0].length - 2, 2);
							}
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];	
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
					}
					


				}

			break; // End execution
			case "datetime":
				if (value=="") return "";
				var dividevalue = value.split("T");//separate date from hour
				if (value.indexOf("T") === -1){
					 dividevalue = value.split(" ");
				}
				//formatting date
				if(picture === "") {
					var dates = dividevalue[0].split("-");
					
					var newValue = value;
					if((defaultdate != undefined) && (defaultdate != null)) {//if default picture
						pict = defaultdate.split("");
						newValue = "";
						for(var i = 0; i <= 2; i++) {
							if(pict[i] === "M")
								newValue = newValue + dates[1];
							if(pict[i] === "D")
								newValue = newValue + dates[2];
							if(pict[i] === "Y")
								newValue = newValue + dates[0];
							if(i != 2)
								newValue = newValue + "/";
						}
					} else {
						newValue = dates[1] + "/" + dates[2] + "/" + dates[0];
					}
				} else {
					var dividepicture = picture.split(" ");
					var valueSplit = dividevalue[0].split("-");
					newValue = "";
					if(dividepicture[0] == "99/99/9999") {
						if(valueSplit[0].length == 4) {
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} else {
							newValue = value;
							while(newValue.indexOf("-") != -1) {
								newValue.replace("-", "/");
							}
						}
					} else 
						if(dividepicture[0] == "99/99/99") {
							if(valueSplit[0].length == 4) {
								valueSplit[0] = valueSplit[0].substr(valueSplit[0].length - 2, 2);
							}
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} else if(dividepicture[0] == "9999/99/99") {
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];	
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						} 						
						else {
							
							newValue = ""
					}
				}
				
				//formating hour
				
				if (picture === ""){
					if (dividevalue.length > 1){
						newValue = newValue + " " + dividevalue[1]; 
					} 
				} else {
					var dividepicture = picture.split(" ");
					if ((picture.split(" ").length < 2) && (dividepicture[0] != "99/99/9999") && (dividepicture[0] != "9999/99/99") && (dividepicture[0] != "99/99/99")){
						dividepicture.append(picture)
					}
					if (dividepicture.length > 1) {
						var digits;
						if (dividevalue.length > 1){
							digits = dividevalue[1].split(":");
							if (digits.length < 2){
								digits[1] = "00";
								digits[2] = "00";
							}
							if (digits.length < 3){
								digits[2] = "00";
							} 
						} else {
							digits = new Array(3)
							digits[0] = "00";
							digits[1] = "00";
							digits[2] = "00";
						}
					 
					
					
						switch(dividepicture[1]){
							case "99":
								newValue = newValue + " " + digits[0];
							break;
							case "99:99":
								newValue = newValue + " " + digits[0] + ":" + digits[1];
							break;
							case "99:99:99":
								newValue = newValue + " " + digits[0] + ":" + digits[1] + ":" + digits[2];
							break;
							default:
								newValue = newValue + " " + digits[0] + ":" + digits[1] + ":" + digits[2];
						}
							
					}
				}
				
				
				
				
			break;
			case "character":
				if (picture == "@!"){
					newValue = value.toUpperCase(); //when @! to upper cases
				}
			break;
			default:
	
		}
		
		
		if (hasprefix){
			newValue = prefix + newValue; 
		}
		if (hassufix){
			newValue = newValue + sufix;
		}
		return newValue;
		
}

OAT.GenerateFormatDateForGrid = function(value, picture){
	var dividepicture = picture.split(" ");
	var valueSplit = value[0].split("-");
	var newValue = ""
	switch(picture)
	{
		case "99/99/9999":
  			newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
  			break;
		case "99/99/99":
  			if(valueSplit[0].length == 4) {
					valueSplit[0] = valueSplit[0].substr(valueSplit[0].length - 2, 2);
			}
			newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
  			break;
		default:
  			newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
	}
	return newValue;
}

OAT.HideGridRow = function(_self, colNumber, value){ 
			
			for (var i = 0; i < _self.grid.rows.length; i++) {
				if (_self.grid.rows[i].cells[colNumber].options.value.toString().trim() === value.toString().trim()){
					_self.grid.rows[i].html.style.display = "none";	
					_self.grid.rows[i].html.setAttribute('visibQ','tf');
				}				
			}
			
}

OAT.ShowGridRow = function(_self, colNumber, value){
			
			for (var i = 0; i < _self.grid.rows.length; i++) {
				if (_self.grid.rows[i].cells[colNumber].options.value.toString().trim() === value.toString().trim()){
					_self.grid.rows[i].html.style.display = "";	
					_self.grid.rows[i].html.setAttribute('visibQ','tt');
				}				
			}
			
}

OAT.RestoreGridRow = function(_self){ /* restore GRID  */
		
		for (var i = 0; i < _self.grid.rows.length; i++){ 
			 //if (_self.grid.rows[i].html.getAttribute('visib')!='fp'){
				_self.grid.rows[i].html.style.display = "";
			 //}
			 _self.grid.rows[i].html.setAttribute('visibQ','tt');
		}
		
		for (var colNum = 0; colNum < _self.grid.columnsDataType.length; colNum++){
				_self.grid.conditions[colNum].blackList = [];		
		}
}

OAT.RestoreGridOrder = function(_self){ 
	 //for (var colNum = 0; colNum < _self.grid.columnsDataType.length; colNum++){
	 //		if (_self.grid.conditions[colNum].sort != 1){
	 //			_self.grid.sort( colNum, 1, _self.grid.columnsDataType.length -1 - colNum);					
	 // 	}		
     //}
	_self.grid.sort( 0, 2, _self.grid.columnsDataType.length -1 - 0);
	_self.grid.sort( 0, 1, _self.grid.columnsDataType.length -1 - 0);
}

OAT.RestoreGridColumns = function(_self){ 
	 for (var tC = 0; tC < _self.grid.columnsDataType.length; tC++){
				_self.grid.header.cells[tC].html.style.display = "";
				var j = 0;
				var numCol = _self.grid.columnsDataType.length - 1 - tC;
				for (j = 0; j < _self.grid.rows.length; j++) {
                    _self.grid.rows[j].cells[numCol].html.style.display = "";
                }
	 }
}

OAT.filterRows = function(grid){
		
		var conteiner = {
				grid : grid
		}
		
		for (var colNum = 0; colNum < grid.columnsDataType.length; colNum++){
			
			for(var item=0; item < grid.conditions[colNum].blackList.length; item++){
				OAT.HideGridRow(conteiner, colNum, grid.conditions[colNum].blackList[item]);
				OAT.actualizeBlackList("push", grid, colNum, grid.conditions[colNum].blackList[item], false);
			} 
		}
}

OAT.actualizeBlackList = function(oper, grid, colNumber, value, serverPaging){ //oper = push add value to blackList
	//search all rows
	if (!serverPaging){
		for (var i = 0; i < grid.rows.length; i++) {
			if (grid.rows[i].cells[colNumber].options.value === value){ //for rows with value "value"
				for (var col = 0; col < grid.rows[0].cells.length; col++){ //for every column disntinc from colNumber
					if (col != colNumber){
						//search in the table if there's any visible row with this value (not hidden beacause of pagination)
						var colvalue = grid.rows[i].cells[col].options.value;
						var show = 0;
						for (var j = 0; j < grid.rows.length; j++){ //search for other rows with same value
            				var nextRowValue = grid.rows[j].cells[col].options.value;
            				if (nextRowValue === colvalue) {
            					if ((grid.rows[j].html.style.display != "none" ) 
            				                    || (grid.rows[j].html.getAttribute('visibQ') != "tf")){
            				                    	show = show + 1;
            					 } 
            					//a columns for this value is not hide
            				}
            			}
            		
            			var index = grid.conditions[col].blackList.find(colvalue);
            			if (show===0){ //add to black list
            				if (oper==="push"){
            					if (index===-1){//if it not already there
            						grid.conditions[col].blackList.push(colvalue);
            					}
            				}
            			} else { //remove from black List
            				if (oper==="pop"){
            					if (index!=-1) {
        							grid.conditions[col].blackList.splice(index, 1)
        						}
            				}
            			}
				
					}			
				}
		
			}
		}
	}
	
	if (oper==="push"){
		if (grid.conditions[colNumber].blackList.find(value) === -1)
			grid.conditions[colNumber].blackList.push(value);
	} else { //pop
		var index = grid.conditions[colNumber].blackList.find(value);
        if (index!=-1)
        	grid.conditions[colNumber].blackList.splice(index, 1);
	}
	
}


OAT.showPopup = function(pos, _self, _type, div) {
			
			var toAppend = [];
			
			
        	var refresh = function() {
        	    jQuery(".oat_winrect_container").css({display: "none"});
        	}
        	
        	var colNumber = /*_self.grid.columns.length - 1 -*/ _self.number;//_self.grid.rows[0].cells.length - 1 - _self.number;
        	
            var coords = pos;
            
            OAT.Dom.clear(div);
            toAppend.append(div);
            /* contents */
            
			if (!_self.grid.disableColumnSort){
				var div_order = document.createElement("div");
				div_order.setAttribute("class", "first_popup_subdiv");
		 
	            var asc = OAT.Dom.radio("order");
	            asc.id = "pivot_order_asc";
	            asc.checked = (_self.grid.conditions[colNumber].sort === 2);  // _self.grid.conditions[colNumber] puede dar undefined
	            OAT.Dom.attach(asc, "change", function() { });
	            OAT.Dom.attach(asc, "click", function() { 
	            	_self.grid.sort(colNumber, OAT.GridData.SORT_ASC, _self.number); 
	            	OAT.saveState(_self, true);
	            	if (self.serverPaging){
	            		var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[_self.number];//_self.grid.columns[colNumber].getAttribute("dataField")
	            		self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, false, dataFieldId, "Ascending", "", "")
	            	} 
	            });
	            div_order.appendChild(asc);
            
	            var alabel = OAT.Dom.create("label");
	            alabel.htmlFor = "pivot_order_asc";
	            //alabel.innerHTML = "Ascending";
	            alabel.innerHTML = gx.getMessage("GXPL_QViewerJSAscending");
	            div_order.appendChild(alabel);
	            div_order.appendChild(OAT.Dom.create("br"));
	            var desc = OAT.Dom.radio("order");
	            desc.id = "pivot_order_desc";
	            desc.checked = (_self.grid.conditions[colNumber].sort === 3);
	            OAT.Dom.attach(desc, "change", function() { });
	            OAT.Dom.attach(desc, "click", function() {
	            	_self.grid.sort(colNumber, OAT.GridData.SORT_DESC, _self.number); 
	            	OAT.saveState(_self, true);
	            	if (self.serverPaging){
	            		var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[_self.number];//_self.grid.columns[colNumber].getAttribute("dataField") //dataField
	            		self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, false, dataFieldId, "Descending", "", "")
					}
	            });
	            var dlabel = OAT.Dom.create("label");
	            dlabel.htmlFor = "pivot_order_desc";
	            //dlabel.innerHTML = "Descending";
	            dlabel.innerHTML = gx.getMessage("GXPL_QViewerJSDescending");
	            div_order.appendChild(desc);
	            div_order.appendChild(dlabel);
	            
	            toAppend.append(div_order);
	            
	            if (self.header.length>1){
	            	var hr4 = OAT.Dom.create("hr", { });
           			//begin drag options
           			toAppend.append(hr4);
           		} 
           }
			
            var hr1 = OAT.Dom.create("hr", { });
            var hr3 = OAT.Dom.create("hr", { });
           	
           	//to left
           	var dragDiv = OAT.Dom.create("div");
           	if (_self.grid.disableColumnSort){
           		dragDiv.setAttribute("class", "first_popup_subdiv");
           	}
           	if (/*(!_self.grid.serverPaging) &&*/ (_self.container.parentNode.cellIndex > 0)){
            	
            	var dragDiv_L_sel_div = document.createElement("div");
            	dragDiv_L_sel_div.setAttribute("class", "move_item_img");
            	
            	OAT.Dom.attach(dragDiv_L_sel_div, "click", function() {																	
																var origen = _self.container.parentNode.cellIndex; //the real position of the item click
																var destino = origen - 1; 
																var i1 = 0; 
																var i2 = 0;
																
																var strDestino = _self.container.parentNode.parentNode.children[destino].getAttribute('title_v');
																for (var i = 0; i < _self.grid.header.cells.length; i++){
																	if (_self.container.children[0].getAttribute('title_v') === _self.grid.header.cells[i].value.innerHTML.replace(/\&nbsp;/g, " ") ){
																		i1 = i; //pos on cell array
																	}
																	if (strDestino === _self.grid.header.cells[i].value.innerHTML.replace(/\&nbsp;/g, " ") ){
																		i2 = i;
																	}
																}
																
																_self.grid.header.cells[i1].html.parentNode.insertBefore(_self.grid.header.cells[i1].html,_self.grid.header.cells[i2].html);
																
																
																var datRow = jQuery("#" + _self.grid.controlName + " tr")[1];
																for (var i=0;i<_self.grid.rows[0].cells.length;i++) {
																	if ((datRow.children[origen].getAttribute('title') === _self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))
																	|| (  !isNaN(parseInt(datRow.children[origen].getAttribute('title')))   &&  (parseInt(datRow.children[origen].getAttribute('title'))  === parseInt(_self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))) )  ){
																		i1 = i;
																	}
																	if ((datRow.children[destino].getAttribute('title') === _self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))
																	|| (  !isNaN(parseInt(datRow.children[destino].getAttribute('title')))   &&  (parseInt(datRow.children[destino].getAttribute('title'))  === parseInt(_self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))) )  ){
																		i2 = i;
																	}
																}
																															
																var newi = (i1 < i2 ? i2-1 : i2);
																																
																newi =  _self.grid.columnsDataType.length - 1 - newi;
																for (var i=0;i<_self.grid.rows.length;i++) {
																	_self.grid.rows[i].cells[i1].html.parentNode.insertBefore(_self.grid.rows[i].cells[i1].html,_self.grid.rows[i].cells[i2].html);
																	var cell = _self.grid.rows[i].cells[i1];
																	_self.grid.rows[i].cells.splice(i1,1);
																	_self.grid.rows[i].cells.splice(newi,0,cell);
																}
																
																var dataFieldsPos = [];
																for(var idF = 0; idF < jQuery("#"+_self.grid.controlName).find("thead td").length; idF++){
																	dataFieldsPos.push(jQuery("#"+_self.grid.controlName).find("thead td")[idF].getAttribute("dataField"))
																}
																OAT_JS.grid.setDataFieldPosition(_self.grid.UcId,  dataFieldsPos);
																refresh();	
														   }); 
            	
            	var draglabel = OAT.Dom.create("label");
            	draglabel.innerHTML = gx.getMessage("GXPL_QViewerJSMoveColumnToLeft") //"Restore View";
            	draglabel.htmlFor = "move_column_to_left";
            	dragDiv_L_sel_div.appendChild(draglabel);
            	
            	OAT.Dom.append([dragDiv, dragDiv_L_sel_div]);
            	if (_self.container.parentNode.cellIndex >= _self.grid.header.cells.length-2){ //TODO: check
            		toAppend.append(dragDiv);
            	}
            }
             
            //to right
            if (/*(!_self.grid.serverPaging) &&*/ (_self.container.parentNode.cellIndex < _self.grid.header.cells.length-1)){ //si no es la ultima columna
            	
				var dragDiv_R_sel_div = document.createElement("div");
            	dragDiv_R_sel_div.setAttribute("class", "move_item_img");
            	
            	OAT.Dom.attach(dragDiv_R_sel_div, "click", function() {
            													var origen = _self.container.parentNode.cellIndex; //the real position of the item click
																var destino = origen + 1; 
																var i1 = 0; 
																var i2 = 0;
																
																//var strDestino = _self.container.parentNode.parentNode.children[destino].textContent;
																var strDestino = _self.container.parentNode.parentNode.children[destino].getAttribute('title_v');
																for (var i = 0; i < _self.grid.header.cells.length; i++){
																	if (_self.container.children[0].getAttribute('title_v') /*textContent*/ === _self.grid.header.cells[i].value.innerHTML.replace(/\&nbsp;/g, " ") ){
																		i1 = i; //pos on cell array
																	}
																	if (strDestino === _self.grid.header.cells[i].value.innerHTML.replace(/\&nbsp;/g, " ") ){
																		i2 = i;
																	}
																}
																
																_self.grid.header.cells[i1].html.parentNode.insertBefore(_self.grid.header.cells[i2].html,_self.grid.header.cells[i1].html);
																
																
																var datRow = jQuery("#" + _self.grid.controlName + " tr")[1];
																for (var i=0;i<_self.grid.rows[0].cells.length;i++) {
																	if ((datRow.children[origen].getAttribute('title') /*textContent*/ === _self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))
																	|| (  !isNaN(parseInt(datRow.children[origen].getAttribute('title')))   &&  (parseInt(datRow.children[origen].getAttribute('title'))  === parseInt(_self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))) )  ){
																		i1 = i;
																	}
																	if ((datRow.children[destino].getAttribute('title') /*textContent*/ === _self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))
																	|| (  !isNaN(parseInt(datRow.children[destino].getAttribute('title')))   &&  (parseInt(datRow.children[destino].getAttribute('title'))  === parseInt(_self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " "))) )  ){
																		i2 = i;
																	}
																}
																															
																var newi = (i1 < i2 ? i2-1 : i2);
																																
																newi =  _self.grid.columnsDataType.length - 1 - newi;
																for (var i=0;i<_self.grid.rows.length;i++) {
																	_self.grid.rows[i].cells[i1].html.parentNode.insertBefore(_self.grid.rows[i].cells[i2].html,_self.grid.rows[i].cells[i1].html);
																	var cell = _self.grid.rows[i].cells[i1];
																	_self.grid.rows[i].cells.splice(i1,1);
																	_self.grid.rows[i].cells.splice(newi,0,cell);
																}
																
																var dataFieldsPos = [];
																for(var idF = 0; idF < jQuery("#"+_self.grid.controlName).find("thead td").length; idF++){
																	dataFieldsPos.push(jQuery("#"+_self.grid.controlName).find("thead td")[idF].getAttribute("dataField"))
																}
																OAT_JS.grid.setDataFieldPosition(_self.grid.UcId,  dataFieldsPos);																
            													refresh();					
            											   }); 
            
            	
            	var draglabelR = OAT.Dom.create("label");
            	draglabelR.innerHTML = gx.getMessage("GXPL_QViewerJSMoveColumnToRight") 
            	draglabelR.htmlFor = "move_column_to_right";
            	dragDiv_R_sel_div.appendChild(draglabelR);
            	OAT.Dom.append([dragDiv, dragDiv_R_sel_div]);
            	toAppend.append(dragDiv);
            }
            //end drag options
			
			 if (OAT.gridStateChanged(_self)){
			 	toAppend.append(hr1);
			 	
            	var restoreview = OAT.Dom.create("div");
            	var restoreview_sel_div = document.createElement("div");
            	restoreview_sel_div.setAttribute("class", "uncheck_item_img");
            	
            	OAT.Dom.attach(restoreview_sel_div, "click" , function() { 
            												if (!_self.grid.serverPaging){
            													OAT.RestoreGridRow(_self); 
            													OAT.RestoreGridOrder(_self); 
            													OAT.RestoreGridColumns(_self); 
            													OAT.saveState(_self, "1"); 
            													refresh() 
            												} else {
            													self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", "", "", true)
            													refresh() 
            												}
            													});
            															 
            	var rl = OAT.Dom.create("label");
            	rl.innerHTML = gx.getMessage("GXPL_QViewerJSRestoreDefaultView"); 
            	rl.htmlFor = "pivot_checkbox_restoreview";
            	restoreview_sel_div.appendChild(rl);
            	OAT.Dom.append([restoreview, restoreview_sel_div]);
            	toAppend.append(restoreview);
            }
			
            var distinct = OAT.Dom.create("div");
            distinct.setAttribute("class", "last_div_popup");
			OAT.distinctDivs(_self, distinct);
			toAppend.append(hr3);
			
            toAppend.append(distinct);
           
			
			OAT.Dom.append(toAppend);
			
            if (gx.util.browser.isIPad() || gx.util.browser.isIPhone()) {
            	jQuery('.oat_winrect_close_b').css({backgroundSize: "30px 30px", height: "30px", width: "30px", right: "-14px", top: "-14px"})
            }
			        
}

OAT.distinctDivs = function(_self, div) { /* set of distinct values checkboxes */
		var colNumber = _self.grid.columns.length - 1 - _self.number;
		var realColNumber = _self.container.parentNode.cellIndex; //the real position of the item click
		if (self.serverPaging){
			colNumber = realColNumber 
		}
		if (!self.serverPaging){
			try {
				var datRow = jQuery("#" + _self.grid.controlName + " tr")[1];
				for (var i=0;i<_self.grid.columns.length;i++) {
					//var val1 = datRow.children[realColNumber].textContent
					var val1 = datRow.children[realColNumber].getAttribute('title')
					var val2 = _self.grid.rows[0].cells[i].value.innerHTML.replace(/\&nbsp;/g, " ") 
					if (val1 === val2){
						colNumber = i;
					}
				}
			} catch (ERROR) {}
		}
		//var colNumber = _self.grid.rows[0].cells.length - 1 - _self.number;
        var getPair = function(text, id) {
            var div = OAT.Dom.create("div");
            var ch = OAT.Dom.create("input");
            //ch.type = "checkbox";
            //ch.id = id;
            var t = OAT.Dom.create("label");
            t.innerHTML = text;
            t.htmlFor = id;
            //div.appendChild(ch);
            div.appendChild(t);
            return [div, ch];
        }

        var getRef = function(ch, value) {
            return function() {
                if (ch.checked) {
                	OAT.ShowGridRow(_self, colNumber ,value);
                	OAT.actualizeBlackList("pop", _self.grid, colNumber, value, self.serverPaging);
                } else {
                	OAT.HideGridRow(_self, colNumber ,value);
                	OAT.actualizeBlackList("push", _self.grid, colNumber, value, self.serverPaging);
                }
                OAT.onFilteredChangedEventHandle(_self, colNumber);
                OAT.saveState(_self, true);
            }
        }

		var getRefBool = function(checked, value) {
				var oper = "pop";
                if (checked) {
                	if (!self.serverPaging){
                		OAT.ShowGridRow(_self, colNumber ,value);
                	}
                	OAT.actualizeBlackList("pop", _self.grid, colNumber, value, self.serverPaging);
                } else {
                	if (!self.serverPaging){
                		OAT.HideGridRow(_self, colNumber ,value);
                	}
                	oper = "push";
                	OAT.actualizeBlackList("push", _self.grid, colNumber, value, self.serverPaging);
                }
                
                if (self.serverPaging){
	            	var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber];//_self.grid.columns[colNumber].getAttribute("dataField")
	            	self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", dataFieldId, { op: oper, values: value } )
				}
                
                OAT.onFilteredChangedEventHandle(_self, colNumber);
                OAT.saveState(_self, true, colNumber);
        }		
		
        var allRef = function() {
            if (!self.serverPaging){
            	_self.grid.conditions[colNumber].blackList = [];
            	OAT.RestoreGridRow(_self);
            	OAT.saveState(_self, true, colNumber)
            } else {
            	var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber];
               	//_self.grid.conditions[colNumber].blackList = [];
	            self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", dataFieldId, { op: "all", values: []} )
            }
            OAT.onFilteredChangedEventHandle(_self, colNumber);
            OAT.distinctDivs(_self, div);
        }

        var noneRef = function() {
            if (!self.serverPaging){
            	_self.grid.conditions[colNumber].blackList = [];
            	for (var i = 0; i < _self.grid.rows.length; i++) {
            		for (var col = 0; col < _self.grid.columns.length; col++){ 
            			if (_self.grid.conditions[col].blackList.find(_self.grid.rows[i].cells[col].options.value)===-1) 
            				_self.grid.conditions[col].blackList.push(_self.grid.rows[i].cells[col].options.value);
            		}
            		OAT.HideGridRow(_self, colNumber , _self.grid.rows[i].cells[colNumber].options.value);
            	}
            } else {
               	var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber];
               	self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", dataFieldId, { op: "none", values: []} )
			}
            
            OAT.saveState(_self, true, colNumber);
            OAT.onFilteredChangedEventHandle(_self, colNumber);
            OAT.distinctDivs(_self, div);
        }

        var reverseRef = function() {
            if (!self.serverPaging){
            	var newBL = [];
            	for (var i = 0; i < _self.grid.rows.length; i++) {
            	    var val = _self.grid.rows[i].cells[colNumber].options.value;
            	    if (_self.grid.conditions[colNumber].blackList.find(val) == -1) { newBL.push(val); }
            	}
            
           		for (var col = 0; col < _self.grid.columns.length; col++){
            		_self.grid.conditions[col].blackList = [];
            	}
            
            	_self.grid.conditions[colNumber].blackList = newBL;
            	
				for (var i = 0; i < _self.grid.rows.length; i++){
					_self.grid.rows[i].html.style.display = "";
				_self.grid.rows[i].html.setAttribute('visibQ','tt');
				}
				OAT.filterRows( _self.grid );
			} else {
				var dataFieldId = OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber];
               	self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", dataFieldId, { op: "reverse", values: []} )
			}
			
			OAT.saveState(  _self , true, colNumber );
            OAT.distinctDivs(_self, div);
            OAT.onFilteredChangedEventHandle(_self, colNumber);
        }
        
        var searchFilterClick = function(){
        	self.getValuesForColumn(_self.grid.UcId, colNumber, this.value)
        }
		
        OAT.Dom.clear(div);
        var d = OAT.Dom.create("div");
		d.setAttribute("class", "div_buttons_popup");
        
        var all = document.createElement("button");
        all.textContent = gx.getMessage("GXPL_QViewerJSAll");
        all.setAttribute("class", "btn");
        jQuery(all).click( allRef );

        var none = document.createElement("button");
        none.textContent = gx.getMessage("GXPL_QViewerJSNone");
        none.setAttribute("class", "btn");
        jQuery(none).click( noneRef );

        var reverse = document.createElement("button");
        reverse.textContent = gx.getMessage("GXPL_QViewerJSReverse");
        reverse.setAttribute("class", "btn");
        jQuery(reverse).click( reverseRef );

		OAT.Dom.append([d, all, none, reverse], [div, d]);
        
        var d2 = OAT.Dom.create("div");
        d2.setAttribute("class", "div_filter_input");
        
        if (self.serverPaging){
        	var searchInput = document.createElement("input");
        	searchInput.textContent = "none";
        	searchInput.setAttribute("class", "search_input");
        	searchInput.setAttribute("type", "text");
        	searchInput.setAttribute("label", "Search filter...");
        	searchInput.setAttribute("title", "Search filter...");
        	searchInput.setAttribute("placeholder", "Search filter...");
        	searchInput.setAttribute("id" , _self.grid.UcId + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]);
        	jQuery(searchInput).keyup( searchFilterClick );
        
        	OAT.Dom.append([d2, searchInput], [div, d2]);
        }
                
        var fixHeigthDiv = OAT.Dom.create("div");
        
        if  (!self.serverPaging){
        	var previusValue = [];
        	for (var i = 0; i < _self.grid.rows.length; i++) { /*_self.grid.rows[_self.number].cells.length*/
       			if (previusValue.find(_self.grid.rows[i].cells[colNumber].options.value)=== -1){
            		var value = _self.grid.rows[i].cells[colNumber].options.value;
            		var pict_value = _self.grid.rows[i].cells[colNumber].value.innerHTML;
            		pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
            		if (pict_value.length > 33) {
            			var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
            			pict_value = pict_value.substring(0, 32) + resto
            		}
            		pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            		var pair = getPair(pict_value, "pivot_distinct_" + i);
            		pair[0].setAttribute('value', value);
            		fixHeigthDiv.appendChild(pair[0]);
            	
            		var checked_value = (_self.grid.conditions[colNumber].blackList.find(value) == -1);
            		var class_check_div = (checked_value) ? "check_item_img": "uncheck_item_img";
            		pair[0].setAttribute("class", class_check_div);
            	
            		OAT.Dom.attach(pair[0], "click", function(){
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );//this.textContent);          														
            											 }); 
            	
            	
            		previusValue.push(_self.grid.rows[i].cells[colNumber].options.value);
          		}	
        	}
        	if (previusValue.length <= 9){
        		fixHeigthDiv.setAttribute("class","pivot_popup_auto");	
        	} else {
        	   	fixHeigthDiv.setAttribute("class","pivot_popup_fix");
        	}
        } else {
        	var cantPairs = OAT_JS.grid.getCantDifferentValues(_self.grid.UcId, colNumber);
        	for (var i = 0; i < cantPairs; i++){
        		var pairData = OAT_JS.grid.getDifferentValues(UcId, colNumber, i)
        		if (pairData){
        			var value = pairData.value;
        			var pict_value = pairData.pict_value; 
        			if ((pairData.value == "#NuN#") || (pict_value.trim() == "")){
        				pict_value = pict_value + "&nbsp;"
        			}
        			pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
            		if (pict_value.length > 33) {
            			var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
            			pict_value = pict_value.substring(0, 32) + resto
            		}
            		pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
        			var pair = getPair(pict_value, "pivot_distinct_" + i);
            		pair[0].setAttribute('value', value);
            		fixHeigthDiv.appendChild(pair[0]);
            	
            		var checked_value = pairData.checked;
            		var class_check_div = (checked_value) ? "check_item_img": "uncheck_item_img";
            		pair[0].setAttribute("class", class_check_div);
            		
            		OAT.Dom.attach(pair[0], "click", function(){
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );          														
            											 });
            	}
        	}
        	if (OAT_JS.grid.getCantDifferentValues(_self.grid.UcId, colNumber) <= 9){
        		fixHeigthDiv.setAttribute("class","pivot_popup_auto");	
        	} else {
        	   	fixHeigthDiv.setAttribute("class","pivot_popup_fix");
        	}
        	fixHeigthDiv.setAttribute("ucid",_self.grid.UcId);
        	fixHeigthDiv.setAttribute("columnnumber",colNumber);
        	fixHeigthDiv.setAttribute("id", "values_"+_self.grid.UcId + "_" +OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber])//_self.grid.UcId + "_" + colNumber)
        }
        
        
        div.appendChild(fixHeigthDiv);
}


OAT.appendNewPairToPopUp = function(_self, value, colNumber, checked, pict_value, dataField){
	var getPair = function(text, id) {
            var div = OAT.Dom.create("div");
            var ch = OAT.Dom.create("input");
            var t = OAT.Dom.create("label");
            t.innerHTML = text;
            t.htmlFor = id;
            div.appendChild(t);
            return [div, ch];
    }

	var getRefBool = function(checked, value) {
				var oper = "pop";
                if (!checked) {
                	oper = "push";
                }
                var filteredValues = _self.grid.conditions[colNumber].blackList
	            self.getDataForTable(_self.grid.UcId, 1, _self.grid.rowsPerPage, true, "", "", dataField, { op: oper, values: value})
				
                OAT.onFilteredChangedEventHandle(_self, colNumber);
                OAT.saveState(_self, true, colNumber);
        }
	
	var pict_value = pict_value;
	if (value == "#NuN#"){
    	pict_value = "&nbsp;"
    }
    pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
    if (pict_value.length > 33) {
    	var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
        pict_value = pict_value.substring(0, 32) + resto
    }
    pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
	var pair = getPair(pict_value, "pivot_distinct_" + i);
	pair[0].setAttribute('value', value);
	var fixHeigthDiv = jQuery("#values_"+ _self.grid.UcId + "_" + dataField)[0]//colNumber)[0]
	fixHeigthDiv.appendChild(pair[0]);
	
	var checked_value = checked;
    var class_check_div = (checked_value) ? "check_item_img": "uncheck_item_img";
    pair[0].setAttribute("class", class_check_div);
	
	OAT.Dom.attach(pair[0], "click", function(){
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );          														
            											 });
	
}

OAT.removeAllPairsFromPopUp = function(_self, colNumber, withScroll){
	jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).find(".check_item_img").remove()
	jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).find(".uncheck_item_img").remove()
	
	jQuery(".last_div_popup .check_item_img").remove()
	jQuery(".last_div_popup .uncheck_item_img").remove()
	
	//set class of pairs container
	if (withScroll){
		jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).removeClass("pivot_popup_auto");
		jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).addClass("pivot_popup_fix");
    } else {
    	jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).removeClass("pivot_popup_fix");
		jQuery("#values_"+ _self.grid.UcId + "_" + OAT_JS.grid.gridData[_self.grid.UcId].columnDataField[colNumber]).addClass("pivot_popup_auto");
    }
}
/* statefull Rutines */
OAT.onFilteredChangedEventHandle = function(self, dimensionNumber){
    	if (self.grid.serverPaging){
			return OAT_JS.grid.setFilterChangedWhenServerPagination(self.grid.UcId, self.grid.columns[dimensionNumber]);
		} else {
    	
    		var datastr = "<DATA name=\"" + self.grid.columns[dimensionNumber].getAttribute("name") + "\" displayName=\"" +  self.grid.columns[dimensionNumber].getAttribute("displayName") + "\">"
    	
    		var previusValue = [];
        	for (var i = 0; i < self.grid.rows.length; i++) { 
       			if (previusValue.find(self.grid.rows[i].cells[dimensionNumber].options.value)=== -1){
       				if (self.grid.conditions[dimensionNumber].blackList.find( self.grid.rows[i].cells[dimensionNumber].options.value ) === -1 ){
            			datastr = datastr + '<VALUE>' + self.grid.rows[i].cells[dimensionNumber].options.value + '</VALUE>';
            			previusValue.push( self.grid.rows[i].cells[dimensionNumber].options.value );
    				}
    			}
    		}
        	datastr = datastr + "</DATA>"
    		var spl = self.grid.IdForQueryViewerCollection  //self.grid.controlName.toUpperCase().split("_")[0] + "_" + self.grid.controlName.split("_")[0];
    		if (self.grid.QueryViewerCollection[spl].FilterChanged) {
            	var xml_doc = self.grid.QueryViewerCollection[spl].xmlDocument(datastr);
            	var Node = self.grid.QueryViewerCollection[spl].selectXPathNode(xml_doc, "/DATA");
            	self.grid.QueryViewerCollection[spl].FilterChangedData.Name = Node.getAttribute("name");
            	self.grid.QueryViewerCollection[spl].FilterChangedData.SelectedValues = [];
            	var valueIndex=-1;
            	for (var i=0; i<Node.childNodes.length; i++)
            	    if (Node.childNodes[i].nodeName == "VALUE")
            	    {
            	        valueIndex++;
            	        self.grid.QueryViewerCollection[spl].FilterChangedData.SelectedValues[valueIndex] = Node.childNodes[i].firstChild.nodeValue;
            	    }
            	self.grid.QueryViewerCollection[spl].FilterChanged();
        	}
    	
      }
}

OAT.setClickEventHandlers = function(self, td, itemValue, MeasureOrDimension, dimensionNumber, itemData){
		jQuery(td).data('itemValue', itemValue );
        jQuery(td).data('typeMorD', MeasureOrDimension );
        jQuery(td).data('numberMorD', dimensionNumber );
        jQuery(td).data('itemInfo', itemData );
        td.onclick = function(){ OAT.onClickEventHandle(self, this); }
        //td.ondblclick = function(){ OAT.onDblClickEventHandle(self, this); }
}

var alreadyclicked=false;
var alreadyclickedTimeout;    
OAT.onClickEventHandle = function(self, elemvalue){
	if (alreadyclicked){
    		//double click
    		alreadyclicked=false;
    		clearTimeout(alreadyclickedTimeout);
    		var datastr = OAT.ClickHandle(self, elemvalue);
    		var spl = self.grid.IdForQueryViewerCollection 
       		self.grid.QueryViewerCollection[spl].onItemClickEvent(datastr,true)
   } else {
    		alreadyclicked=true;
            alreadyclickedTimeout=setTimeout(function(){
                alreadyclicked=false;
                //single click
                var datastr = OAT.ClickHandle(self, elemvalue);
    			var spl = self.grid.IdForQueryViewerCollection;
    			self.grid.QueryViewerCollection[spl].onItemClickEvent(datastr,false);
           	},300);      
   }
}
    
OAT.onDblClickEventHandle = function(self, elemvalue){
    	var datastr = OAT.ClickHandle(self, elemvalue);
    	var spl = self.grid.IdForQueryViewerCollection //self.grid.controlName.toUpperCase().split("_")[0] + "_" + self.grid.controlName.split("_")[0];
    	self.grid.QueryViewerCollection[spl].onItemClickEvent(datastr,true);
}
    
OAT.ClickHandle = function(self, elemvalue){
    	var value = jQuery(elemvalue).data('itemValue');
        var type  =	jQuery(elemvalue).data('typeMorD');
        var number = jQuery(elemvalue).data('numberMorD');
        var columnNumber = jQuery(elemvalue).data('itemInfo');
    	var datastr = "<DATA><ITEM type=\"" + type +"\" ";
    	  	
    	 
    	
    	datastr = datastr + "name=\"" +  self.grid.columns[number].getAttribute("name") + "\" ";
    	datastr = datastr + "displayName=\"" +  self.grid.columns[number].getAttribute("displayName") + "\" ";
    	datastr = datastr + "location=\"rows\">"
    	datastr = datastr + value 
    	
    	
    	datastr = datastr + "</ITEM>";
    	
    	datastr = datastr + "<CONTEXT>";
    	datastr = datastr + "<RELATED>";
    		for (var i = 0; i < self.grid.rows[columnNumber].cells.length; i++){
    			datastr = datastr + "<ITEM name=\"" + self.grid.columns[i].getAttribute("name") + "\">";
    			datastr = datastr + "<VALUES>";
    				datastr = datastr + "<VALUE>" +  self.grid.rows[columnNumber].cells[i].options.value  + "</VALUE>";
    			datastr = datastr + "</VALUES>";
    			datastr = datastr + "</ITEM>";
    		}
    	datastr = datastr + "</RELATED>";
    	datastr = datastr + "<FILTERS>";
    	datastr = datastr + "</FILTERS>";
    	datastr = datastr + "</CONTEXT>";
    	
    	datastr = datastr + "</DATA>"
    	
    	return datastr;
    }
    
OAT.gridStateChanged = function(self){ //true is state changed
	if (!self.grid.serverPaging){
		var changed = false;
		for (var tC = 0; tC < self.grid.columnsDataType.length; tC++){
			if ((self.grid.header.cells[tC]!=undefined) && (self.grid.header.cells[tC].html.style.display != "")) //some columns is hidden
				changed = true;
		}
		for (var tC = 0; tC < self.grid.conditions.length; tC++){
			if (self.grid.conditions[tC].sort != 1) //some columns changes order
				changed = true;
			if (self.grid.conditions[tC].blackList.length > 0) //rows are hidden
				changed = true;
		}
		if ((self.grid.InitPageSize != undefined) && (self.grid.rowsPerPage != self.grid.InitPageSize)) return true;
		return changed;
	} else {
		return OAT_JS.grid.getStateChange(self.grid.UcId)
	}
}


OAT.createXMLMetadata = function(self, columnNumber, serverPagination){
	
	var xml = '<OLAPCube format="'+ "compact" +'" thousandsSeparator="'+ "," +'" decimalSeparator="'+ "." +'" dateFormat="'+ "MDY" +'">';
    	 
    	for(var iCV=0; iCV < self.grid.columns.length-1; iCV++){
    		//var iCV = columnNumber;   	    	
    		xml = xml + '<OLAPDimension> ';
    	
    		xml = xml + '<name>'+self.grid.columns[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+self.grid.columns[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+self.grid.columns[iCV].getAttribute("description")+'</description> ';
    		xml = xml + '<dataField>'+self.grid.columns[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+self.grid.columns[iCV].getAttribute("dataType")+'</dataType> ';
    		xml = xml + '<defaultPosition>'+self.grid.columns[iCV].getAttribute("defaultPosition")+'</defaultPosition> ';
    		xml = xml + '<validPositions>'+self.grid.columns[iCV].getAttribute("validPositions")+'</validPositions> ';
    		xml = xml + '<summarize>'+self.grid.columns[iCV].getAttribute("summarize")+'</summarize> ';
    		xml = xml + '<align>'+self.grid.columns[iCV].getAttribute("align")+'</align> ';
    		
    		if (self.grid.columns[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ self.grid.columns[iCV].getAttribute("picture") +'</picture> ';
    		}
    		
    		if (self.grid.columns[iCV].getAttribute("picture") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ self.grid.columns[iCV].getAttribute("format") +'</format> ';
    		}
    		//xml = xml + '<order>'+this.columns[iCV].getAttribute("order")+'</order> ';
    		if (self.grid.conditions[iCV].sort===2)
    			xml = xml + '<order>ascending</order> '
    		else if (self.grid.conditions[iCV].sort===1)
    			xml = xml + '<order>none</order> '
    		else
    			xml = xml + '<order>descending</order> '
    			
    		xml = xml + '<popupDisabled>'+self.grid.columns[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		xml = xml + '<customOrder/> ';
    		xml = xml + '<include> ';
    		
    		if (!serverPagination){
    			var previusValue = [];
        		for (var i = 0; i < self.grid.rows.length; i++) { /* se prodria agregar aqui blaklist (no sustituyendo por que dejaria de funcionar para chart)*/
       				if (self.grid.rows[i].cells[iCV]!=undefined){
       					if (previusValue.find(self.grid.rows[i].cells[iCV].options.value)=== -1){
       						if (self.grid.conditions[iCV].blackList.find( self.grid.rows[i].cells[iCV].options.value ) === -1 ){
            					xml = xml + '<value>' + self.grid.rows[i].cells[iCV].options.value.toString().trim() +'</value> ';
            					previusValue.push( self.grid.rows[i].cells[iCV].options.value );
    						}
    					}
    				}
    			}
    		} else {
    			var dF = self.grid.columns[iCV].getAttribute("dataField");
    			if ((self.blackLists[dF].state != "none") && (self.blackLists[dF].state != "all")){
    				for ( var i = 0; i < self.blackLists[dF].visibles.length; i++){
    					xml = xml + '<value>' + self.blackLists[dF].visibles[i].trim() +'</value> ';
    				}
    			} else if (self.blackLists[dF].state != "none"){
    				for ( var i = 0; i < self.differentValues[dF].length; i++){
    					xml = xml + '<value>' + self.differentValues[dF][i].trim() +'</value> ';
    				}
    			}
    		}
    		xml = xml + '<value>TOTAL</value> </include> <collapse/> null null null ';
    		
    		xml = xml + '<hide> '
    		if (!serverPagination){
    			for (var yu = 0; yu < self.grid.conditions[iCV].blackList.length; yu++) {
    				//new code
    				var isBlack = (iCV === 0); //this code is necessesary because of diferent representation of black list in pivot and table
    				for (var l = 0; l < self.grid.rows.length; l++) {
						if (self.grid.rows[l].cells[iCV].options.value === self.grid.conditions[iCV].blackList[yu]) { //for rows with value "value"
							for (var col = 0; col < iCV; col++){ //for every column previuos to colNumber
								var colvalue = self.grid.rows[l].cells[col].options.value;
								if (self.grid.conditions[col].blackList.find(colvalue) === -1){ //if the value is not in black list
									isBlack = true;
								}
            				}
            			}
    				}
    				//end of new code 6/6/2012
    				if (isBlack === true){
						xml = xml + '<value>' + self.grid.conditions[iCV].blackList[yu].toString().trim() + '</value> ';
					}
				    			
    			}
    		} else {
    			var dF = self.grid.columns[iCV].getAttribute("dataField");
    			if ((self.blackLists[dF].state != "none") && (self.blackLists[dF].state != "all")){
    				for ( var i = 0; i < self.blackLists[dF].hiddens.length; i++){
    					xml = xml + '<value>' + self.blackLists[dF].hiddens[i].trim() +'</value> ';
    				}
    			} else if (self.blackLists[dF].state != "all"){
    				for ( var i = 0; i < self.differentValues[dF].length; i++){
    					xml = xml + '<value>' + self.differentValues[dF][i].trim() +'</value> ';
    				}
    			}
    		}
    		xml = xml + '</hide> '
    		
    		var numCol = self.grid.columnsDataType.length - 1 - iCV;
    		try{
    			if (self.grid.header.cells[numCol] != undefined){
    				if ( self.grid.header.cells[numCol].html.style.display === "") {
    					xml = xml + '<condition>table</condition> ';
    					xml = xml + '<filterbar>no</filterbar> ';
    					xml = xml + '<position>'+ self.columnDataField.indexOf(self.grid.columns[iCV].getAttribute("dataField")) +'</position>'
    				} else {
    					xml = xml + '<condition>none</condition> ';
    					xml = xml + '<filterbar>yes</filterbar> ';
    					xml = xml + '<position>'+ self.columnDataField.indexOf(self.grid.columns[iCV].getAttribute("dataField")) +'</position>'
    				}
    			} 
    		} catch (ERROR){
    			
    		}
    		xml = xml + '<restoreview>no</restoreview> ';
    		xml = xml + ' </OLAPDimension>';
    	}
    	
	
		xml = xml + "</OLAPCube>";
    	
    	return xml;
}

OAT.ExportToPdf = function(_self, fileName){
		//calc max length of paper
		var hgt = 20 + jQuery("#" + _self.grid.controlName + " tr").length*30 + 5;
		if (hgt < 841){
			hgt = 841;
		}
		
		//calc max width of paper
		var wdt = 0;
		var tRow = jQuery("#" + _self.grid.controlName + " tr")[0].children.length;
		wdt = 20 + tRow*(30+65) + 5; 
		if (wdt < 595){
				wdt = 595;
		}
		
		//calculate columns width
		var columnsWidth = [];
		
		for (var i = 0; i < jQuery("#" + _self.grid.controlName + " tr").length; i++) {												
	    	
	    	var tRow = jQuery("#" + _self.grid.controlName + " tr")[i];
	    	for (var j = 0; j < tRow.children.length; j++){//for every cell in the row
	    		var childText = tRow.children[j].textContent;
				var hidden    = tRow.children[j].getAttribute('hidden');
				
				if (hidden === null){
					if (columnsWidth[j] == undefined){
						columnsWidth[j] = 30;
					}
					
					
					//japanese character
					var jChar = 0;
					for (var p = 0; p < childText.length; p++){
						if (childText.charCodeAt(p) > 1000){
							jChar++;
						}
					}
					
					if (jChar===0){
						if (childText.length > 14){
							if ( childText.length * 1.68 > columnsWidth[j] ){
								columnsWidth[j] = childText.length * 1.68;
							}
						}
					} else {
						var w = jChar*2.8 + (childText.length-jChar)*1.68;
						if ( w > columnsWidth[j]){
							columnsWidth[j] = w;
						}
					}
				}
			}
	    }
		//recalculate width
		var nw = 0
		for (var i = 0; i < columnsWidth.length; i++){
			nw = nw + columnsWidth[i]*2.5 + 65;
		}
		nw = 20 + nw + 5;
		if (nw > wdt){
			wdt = nw;
		}
		
		var getXOffset = function(colNro, columnsWidth){
			var offset = 0;
			for (var i = 0; i < colNro; i++){
				offset = offset + columnsWidth[i];
			}
			return offset;
		}
		
		var getYOffset = function(rowNro, rowsHeight){
			var offsety = 0;
			for (var j = 0; j < rowNro; j++){
				offsety = offsety + rowsHeight[j];
			}
			return offsety;
		}
		
		var doc;
		if (wdt <= hgt){
			doc = new jsPDF('portrait' , 'mm', 'a4', false, wdt, 792); //landscape or portrait
		} else {
			doc = new jsPDF('landscape', 'mm', 'a4', false, wdt, 792); //landscape or portrait
		}
	    doc.setFontSize(8);
	    
	    doc.line(18, 13, 20 + getXOffset(jQuery("#" + _self.grid.controlName + " tr")[0].children.length, columnsWidth), 13);
	    
	    var y = -1;
	    var nroPag = 1
	    var verticalHeight = Math.min((jQuery("#" + _self.grid.controlName + " tr").length) - (nroPag-1)*26 , 26)
	    for (var i = 0; i < jQuery("#" + _self.grid.controlName + " tr").length; i++) {												
	    	y++;
	    	var tRow = jQuery("#" + _self.grid.controlName + " tr")[i];
	    	
	    	for (var j = 0; j < tRow.children.length; j++){//for every cell in the row
	    		
	    		//vertical line
	    		doc.line(18 + getXOffset(j, columnsWidth), 13, 18 + getXOffset(j, columnsWidth), 23 +  (verticalHeight-1)*10);
	    		
	    		var childText = tRow.children[j].textContent;
				var hidden    = tRow.children[j].getAttribute('hidden');
				
				var imgTxtData = []
				if (childText.charCodeAt(0) > 1000) {
					for (var cNo = 0; cNo < childText.length; cNo++) {
						imgTxtData[cNo] = OAT.getCharacterImg(childText.charCodeAt(cNo))
					}
				} else {
					var posI = -1;
					for (var p = 1; p < childText.length; p++){
						if (childText.charCodeAt(p) > 1000){
							posI = p;
							break;
						}
					}
					if (posI > 0){
						var tempchildText = childText.substring(0, posI);
						var posE = 0
						for (var cNo = posI; cNo < childText.length; cNo++) {
							imgTxtData[posE] = OAT.getCharacterImg(childText.charCodeAt(cNo))
							posE++;
						}
						childText = tempchildText;
					}
				}
				
				//set styles
				var hasBackground = false;
				doc.setTextColor(0,0,0);
				doc.setFontStyle('normal');
				var IsTextAlignRight = !isNaN(parseFloat(childText));
				var textWidht = doc.getStringUnitWidth(childText); 
				if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
					var attributes = tRow.children[j].getAttribute("style").split(";");
					for(var at = 0; at < attributes.length; at++){
						var detail = attributes[at].split(":");
						if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								doc.setTextColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
								 
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
								//var alg = detail[1].replace(/^\s+|\s+$/g, '');
								IsTextAlignRight = (detail[1].replace(/^\s+|\s+$/g, '') == "right")
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
								doc.setFontStyle('italic')
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
							if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
								doc.setFontStyle('bold')
							}
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
							var rgb = detail[1].replace(/^\s+|\s+$/g, '');
							rgb = rgb.substring(4,rgb.length);
							rgb = rgb.substring(0,rgb.length-1);
							doc.setFillColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
							hasBackground = true;
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="border-color"){
							var rgb = detail[1].replace(/^\s+|\s+$/g, '');
							rgb = rgb.substring(4,rgb.length);
							rgb = rgb.substring(0,rgb.length-1);
							doc.setDrawColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
						}
							
					}
				}
				
				if ((hasBackground) && ( j == (tRow.children.length-1) )){//for the last column more width
					doc.rect(18 + getXOffset(j, columnsWidth),  23 + (y-1)*10, 32, 10, 'FD');
				} else if (hasBackground) {
					doc.rect(18 + getXOffset(j, columnsWidth),  23 + (y-1)*10, 30, 10, 'FD');
				}
				
				doc.setDrawColor(0, 0, 0);
				doc.setFontSize(8);
								
				if (hidden === null){ 
					var preImage = 0;
					if (childText.charCodeAt(0) < 1000){
						preImage = childText.length + 2
						if (IsTextAlignRight){
							doc.text(20 + getXOffset(j+1, columnsWidth) - textWidht*3 - 4, 20 + y*10, childText);
						} else {
							doc.text(20 + getXOffset(j, columnsWidth), 20 + y*10, childText);
						}
					}
					if (imgTxtData.length > 0) {
						for (var cNo = 0; cNo < imgTxtData.length; cNo++) {
							doc.addImage(imgTxtData[cNo], 'JPEG', 20 + getXOffset(j, columnsWidth) + preImage + cNo*2.5, 20 + y*10 -2.5, 2.5, 2.5);
						}
					} 
				}
				
				
	    	
	    	}
	    	//last vertical line
	    	doc.line(20 + getXOffset(tRow.children.length, columnsWidth), 13, 20 + getXOffset(tRow.children.length, columnsWidth), 23 +  (verticalHeight-1)*10);
	    	
			doc.line(18, 23 + y*10, 20 + getXOffset(tRow.children.length, columnsWidth), 23 + y*10);
			if (y >= 25){
					doc.setDrawColor(0,0,0)
					nroPag++
					verticalHeight = Math.min((jQuery("#" + _self.grid.controlName + " tr").length-1) - (nroPag-1)*26 + 1, 26)
					y = -1
					doc.addPage()
					//top horizontal line
					doc.line(18, 13, 20 + getXOffset(jQuery("#" + _self.grid.controlName + " tr")[0].children.length, columnsWidth), 13);
			}	
		}
		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
				doc.output('dataurlnewwindow');	
		} else if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){												
			doc.save(fileName+'.pdf');
		} else {
			return doc.output();
		}
}

OAT.ExportToExcel = function(_self, fileName){
	
		var table = '<table><tbody>'
		
		for (var i = 0; i < jQuery("#" + _self.grid.controlName + " tr").length; i++) {//for every row
			table = table + '<tr>';
			
			var tRow = jQuery("#" + _self.grid.controlName + " tr")[i];
			for (var j = 0; j < tRow.children.length; j++){//for every cell in the row
				var childText;
				if (i===0){
					childText = tRow.children[j].getAttribute("title_v");
					if (childText === undefined){
						childText = tRow.children[j].textContent;
					}
				} else {
					childText = tRow.children[j].getAttribute("title");
					childText = tRow.children[j].textContent;
					if (childText === undefined){
						childText = tRow.children[j].textContent;
					}
				}
				var hidden    = tRow.children[j].getAttribute('hidden');
				if (hidden === null){ 
					var rowSpan   = tRow.children[j].getAttribute('rowspan');
					var colSpan   = tRow.children[j].getAttribute('colspan');
					
					var styleString = "";
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						styleString = " style=\"" + tRow.children[j].getAttribute("style") + "\" ";
					}
					
					if ((rowSpan === null) && (colSpan === null)){
						table = table + '<td ' + styleString + '>' + childText + '</td>';
					} else if (colSpan === null) {
						table = table + '<td ' + styleString + ' rowspan="' + rowSpan + '">' + childText + '</td>';
					} else if (rowSpan === null) {
						table = table + '<td ' + styleString + ' colspan="' + colSpan + '">' + childText + '</td>';
					} else {
						table = table + '<td ' + styleString + ' colspan="' + colSpan + '" rowspan="' + rowSpan + '">' + childText + '</td>';
					}
				}
			}
		
			table = table + '</tr>';
		}
	
	
		table = table + '</tbody></table>';
		var dtltbl = table; 
		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ 
			window.open('data:application/vnd.ms-excel,' + encodeURIComponent(dtltbl));
		} else if ((!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion()))){
			//window.open('data:application/vnd.ms-excel,' + encodeURIComponent(dtltbl));
			var blob = new Blob([dtltbl], {type: "application/vnd.ms-excel"});
            saveAs( blob, fileName+".xls");
		} else {
			return dtltbl;
		}
		
	
}


OAT.ExportToExcel2010 = function(_self, fileName){
	
	
	function componentToHex(c) {
    	var hex = c.toString(16);
    	return hex.length == 1 ? "0" + hex : hex;
	}

	function rgbToHex(r, g, b) {
    	return componentToHex(r) + componentToHex(g) + componentToHex(b);
	}

	
	
	dataTable = [];
	for (var i = 0; i < jQuery("#" + _self.grid.controlName + " tr").length; i++) {//for every row
			dataRow = [];
			
			var tRow = jQuery("#" + _self.grid.controlName + " tr")[i];
			for (var j = 0; j < tRow.children.length; j++){//for every cell in the row
				var childText = tRow.children[j].textContent;
				var hidden    = tRow.children[j].getAttribute('hidden');
				if (hidden === null){ 
					var rowSpan   = tRow.children[j].getAttribute('rowspan');
					var colSpan   = tRow.children[j].getAttribute('colspan');
					
					var styleString = "";
					var cellObject = {value: childText}; 
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						var attributes = tRow.children[j].getAttribute("style").split(";");
						for(var at = 0; at < attributes.length; at++){
							var detail = attributes[at].split(":");
							if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
								cellObject.fontColor = hex; 
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
								var alg = detail[1].replace(/^\s+|\s+$/g, '');
								cellObject.vAlign = alg;
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
								if (detail[1].replace(/^\s+|\s+$/g, '') === "italic"){
									cellObject.italic = 1;
								}
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
								if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
									cellObject.bold = 1;
								}
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
								cellObject.fill = hex;
							}
							
						}
					}
					
					if ((rowSpan === null) && (colSpan === null)){
						dataRow.push( cellObject )
					} else if (colSpan === null) {
						cellObject.rowSpan = parseInt(rowSpan);
						dataRow.push( cellObject )
					} else if (rowSpan === null) {
						cellObject.colSpan = parseInt(colSpan);
						dataRow.push( cellObject )
					} else {
						cellObject.colSpan = parseInt(colSpan);
						cellObject.rowSpan = parseInt(rowSpan);
						dataRow.push( cellObject )
					}
				}
			}
		
			dataTable.push(dataRow);
		}
	
		var sheet = xlsx({
      				creator: 'Genexus',
      				lastModifiedBy: 'Genexus',
      				pivot: false,
      				worksheets: [{
        					data: dataTable,
        					name: 'Sheet 1'
      							}]
    					 });
    	if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
				window.location = sheet.href();	
		} else if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){												
				//window.location = sheet.href();
				var byteCharacters = atob(sheet.base64);
				function charCodeFromCharacter(c) {
    				return c.charCodeAt(0);
				}

				var byteNumbers = Array.prototype.map.call(byteCharacters,charCodeFromCharacter);
				var uint8Data = new Uint8Array(byteNumbers);
				
    			var blob = new Blob([uint8Data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            	saveAs( blob, fileName+".xlsx");
		} else {
			return sheet.base64;	
		}
}

OAT.ExportToXML = function(self, fileName){
    	var xml = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><EXPORT format="XML" type="flat">';
    	xml = xml + '<METADATA>';
    		for(var iCV=0; iCV < self.grid.columns.length; iCV++){
    			
    			var position = 'row';
    			
    			xml = xml + '<OLAPDimension ';
    			xml = xml + 'name="'    + self.grid.columns[iCV].getAttribute("dataField")   + '" ';
    			xml = xml + 'label="'   + self.grid.columns[iCV].getAttribute("displayName") + '" ';
    			xml = xml + 'picture="' + self.grid.columns[iCV].getAttribute("picture")     + '" ';
    			xml = xml + 'datatype="'+ self.grid.columns[iCV].getAttribute("dataType")    + '" ';
    			xml = xml + 'showAll="false" ';
    			xml = xml + 'position="' + position                                     + '">';
    			
    			
    			var previusValue = [];
        		for (var i = 0; i < self.grid.rows.length; i++) { 
       				if (previusValue.find(self.grid.rows[i].cells[iCV].options.value)=== -1){
       					xml = xml + '<VALUE CHECKED=';	
       					if (self.grid.conditions[iCV].blackList.find( self.grid.rows[i].cells[iCV].options.value ) === -1 ){
            				previusValue.push( self.grid.rows[i].cells[iCV].options.value );
            				xml = xml + '"true"';
    					} else {
    						xml = xml + '"false"';
    					}
    					xml = xml + ' COLLAPSED="false">'
    					
    					/*Set the value with the original format, for numeric values*/
    					var valuetoString = self.grid.rows[i].cells[iCV].options.value;
    					if (!isNaN(parseFloat(self.grid.rows[i].cells[iCV].options.value))) {
                			var numRecord = parseFloat(self.grid.rows[i].cells[iCV].options.value);
                			if (data != undefined){
                				for(var di = 0; di < data.length; di++){
                					if (data[di] != undefined){
                						for(var dj = 0; dj < data[di].length; dj++){
                							if ((parseFloat(data[di][dj]) != undefined) && ( !isNaN(data[di][dj]) ) ){
											 	if ( parseFloat(data[di][dj]) ===  parseFloat(self.grid.rows[i].cells[iCV].options.value)){
											 		valuetoString = data[di][dj];
											 	}               						
											 }
                						}
                					}
                				}
                			}
                		} 
    					
    					xml = xml + valuetoString +'</VALUE>';
    				}
    			}
    			
    			xml = xml + '</OLAPDimension>';	
    		}
    		
    		for (var iCV = 0; iCV < measures.length; iCV++) {
    			xml = xml + '<OLAPMeasure ';
    			xml = xml + 'name="'      + measures[iCV].getAttribute("dataField")   + '" ';
    			xml = xml + 'label="'     + measures[iCV].getAttribute("displayName") + '" ';
    			xml = xml + 'picture="'	  + measures[iCV].getAttribute("picture")     + '" '; 
    			xml = xml + 'datatype="'  + measures[iCV].getAttribute("dataType")    + '" ';
    			xml = xml + 'showAll="true" ';
    			xml = xml + 'aggregator="sum"/>';
    		}
    		
    	xml = xml + '</METADATA>';
    	
    	xml = xml + '<FLATDATA>';
    	    if (data != undefined){
    			for (var i = 0; i < data.length; i++){
    				xml = xml + '<ROW ';
    				for(var iCV=0; iCV < data[0].length; iCV++){
    					var valuetoString = data[i][iCV];		
    					xml = xml + self.grid.columns[iCV].getAttribute("dataField") + '="'+ valuetoString + '" ';
    				}
    				xml = xml + '/>';	
    			}
    		} else {
    		 for (var i = 0; i < self.grid.rows.length; i++){
    			xml = xml + '<ROW ';
    			
    			
    			 for(var iCV=0; iCV < self.grid.rows[0].cells.length; iCV++){
    				/*Set the value with the original format, for numeric values*/
    				var valuetoString = "";	
    				 valuetoString = self.grid.rows[i].cells[iCV].options.value;
    				 if (!isNaN(parseFloat(self.grid.rows[i].cells[iCV].options.value))) {
                		var numRecord = parseFloat(self.grid.rows[i].cells[iCV].options.value);
                		if (data != undefined){
                			for(var di = 0; di < data.length; di++){
                				if (data[di] != undefined){
                					for(var dj = 0; dj < data[di].length; dj++){
                						if ((parseFloat(data[di][dj]) != undefined) && ( !isNaN(data[di][dj]) ) ){
										 	if ( parseFloat(data[di][dj]) ===  parseFloat(self.grid.rows[i].cells[iCV].options.value)){
										 		valuetoString = data[di][dj];
										 	}               						
										 }
                					}
                				}
                			}
                		}
                	  } 
                	  xml = xml + self.grid.columns[iCV].getAttribute("dataField") + '="'+ valuetoString + '" ';
    			 }
    			
    			xml = xml + '/>'
    		 }
    		}
    	xml = xml + '</FLATDATA>';
    	
    	
    	xml = xml + '<HTML>';
    	
    	xml = xml + '<HEAD>';
    	xml = xml + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>';
    	
    	xml = xml + '<STYLE>';
    	
    	xml = xml + '.odd {background-color: #FEFEFE; font-family: Verdana; font-size: 10pt;}\n';
    	xml = xml + '.event {background-color: #EBEBEB; font-family: Verdana; font-size: 10pt;}\n';
		xml = xml + '.even {background-color: #FEFEFE;	font-weight: normal; font-family: Verdana; font-size: 10pt;	padding: 5px; }\n';
		xml = xml + 'tr {border-left: 1px solid #BBBBBB; border-right: 1px solid #BBBBBB; line-height: 22px;}\n';
		xml = xml + 'table {border-collapse: collapse;}\n'
		
		xml = xml + '</STYLE>';
    	xml = xml + '</HEAD>';
    	xml = xml + '<BODY>';
    	
    	for (var i = 0; i < jQuery("#" + self.grid.controlName + " tr").length; i++) {//for every row
			xml = xml + '<TR>';
			
			var tRow = jQuery("#" + self.grid.controlName + " tr")[i];
			for (var j = 0; j < tRow.children.length; j++){//for every cell in the row
				var childText = tRow.children[j].textContent;
				var hidden    = tRow.children[j].getAttribute('hidden');
				if (hidden === null){ 
					var rowSpan   = tRow.children[j].getAttribute('rowspan');
					var colSpan   = tRow.children[j].getAttribute('colspan');
					
					var styleString = "";
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						styleString = " style=\"" + tRow.children[j].getAttribute("style") + "\" ";
					}
					
					var classString = "";
					if ((tRow.getAttribute("class")!=undefined) && (tRow.getAttribute("class")!=null)){
						classString = " class=\"" + tRow.getAttribute("class") + "\" ";
					}
					
					if ((rowSpan === null) && (colSpan === null)){
						xml = xml + '<TD '+ classString + ' '+ styleString +' >' + childText + '</TD>';
					} else if (colSpan === null) {
						xml = xml + '<TD '+ classString + ' '+ styleString +' rowspan="' + rowSpan + '">' + childText + '</TD>';
					} else if (rowSpan === null) {
						xml = xml + '<TD '+ classString + ' '+ styleString +' colspan="' + colSpan + '">' + childText + '</TD>';
					} else {
						xml = xml + '<TD '+ classString + ' '+ styleString +' colspan="' + colSpan + '" rowspan="' + rowSpan + '">' + childText + '</TD>';
					}
				}
			}
		
			xml = xml + '</TR>';
		}
	
    	
    	xml = xml + '</BODY>';
    	xml = xml + '</HTML>';
    	
    	
    	xml = xml + "</EXPORT>";
    	
    	if ((!gx.util.browser.isIE()) || 9<gx.util.browser.ieVersion()){
    		xml = xml.replace(/\&/g, "&amp;");
    		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari			
        	    window.open('data:text/xml,' +  encodeURIComponent(xml) );
        	} else {
            	var blob = new Blob([xml], {type: "text/xml"});
            	saveAs(blob, fileName+".xml");
            }
        } else {
       		return xml.replace(/\&/g, "&amp;");	
        }    	
}

OAT.SaveStateWhenServerPaging = function(self, UcId, pageSize, dataFieldOrder, orderType, filters, blackLists, columnVisible){
	var state = {
		pageSize: pageSize,
		dataFieldOrder: dataFieldOrder, 
		orderType: orderType,
		filters: filters,
		blackLists: blackLists,
		columnVisible: columnVisible
	}
	
	var columnDisplay = new Array(self.columnsDataType);
	for (var tC = 0; tC < self.columnsDataType.length; tC++){
		if (self.header.cells[tC] != undefined){
			columnDisplay[tC] = self.header.cells[tC].html.style.display;
		}
	}
	
	localStorage.setItem(OAT.getURL()+self.controlName+self.query, JSON.stringify(state));
}

OAT.saveState = function(self, repage, columnChange, serverInfo) {
    	
    	if (self.grid.serverPaging) {
    		return;
    	}
    	
		if( typeof JSON.decycle !== 'function') {
			JSON.decycle = function decycle(object) {'use strict';

				var objects = [], // Keep a reference to each unique object or array
				paths = [];
				return ( function derez(value, path) {

					var i, // The loop counter
					name, // Property name
					nu;
					switch (typeof value) {
						case 'object':
							if(!value) {
								return null;
							}
							for( i = 0; i < objects.length; i += 1) {
								if(objects[i] === value) {
									return {
										$ref : paths[i]
									};
								}
							}
							objects.push(value);
							paths.push(path);
							if(Object.prototype.toString.apply(value) === '[object Array]') {
								nu = [];
								for( i = 0; i < value.length; i += 1) {
									nu[i] = derez(value[i], path + '[' + i + ']');
								}
							} else {
								nu = {};
								for(name in value) {
									if(Object.prototype.hasOwnProperty.call(value, name)) {
										nu[name] = derez(value[name], path + '[' + JSON.stringify(name) + ']');
									}
								}
							}
							return nu;
						case 'number':
						case 'string':
						case 'boolean':
							return value;
					}
				}(object, '$'));
			};
		}
		
		var columnDisplay = new Array(self.grid.columnsDataType);
		for (var tC = 0; tC < self.grid.columnsDataType.length; tC++){
			if (self.grid.header.cells[tC] != undefined){
				columnDisplay[tC] = self.grid.header.cells[tC].html.style.display;
			}
		}
		
		if (repage === "1") self.grid.rowsPerPage = self.grid.InitPageSize;
		
		var state = {
			conditions : self.grid.conditions,
			columnDisplay : columnDisplay,
			rowsPerPage: self.grid.rowsPerPage
		}
		
    	localStorage.setItem(OAT.getURL()+self.grid.controlName+self.grid.query, JSON.stringify(JSON.decycle(state)));
    	
    	var rowNum;
    	if(!isNaN(self.grid.rowsPerPage) && (self.grid.rowsPerPage!="")) {
    		rowNum = self.grid.rowsPerPage;
    	} else {
    		rowNum = self.grid.InitPageSize;
    	}
    	if (repage ==="1") rowNum = self.grid.InitPageSize
    	/* re - pagination */
		var options = {
        	currPage: 1,
        	ignoreRows: jQuery('tbody tr[visibQ=tf]', jQuery("#" + self.grid.controlName)),
        	optionsForRows: [10, 15, 20],
        	rowsPerPage: rowNum != 'undefined' ? rowNum : 50,
        	firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
            prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
            lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
            nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),
       		jstype: "table",
        	topNav: false,
        	controlName: self.grid.controlName
    	}
    	if ((self.grid.InitPageSize) && (repage)) {
			jQuery("#" + self.grid.controlName).tablePagination(options);
			setTimeout(function(){
				var wd2 = jQuery("#" + self.grid.controlName)[0].offsetWidth - 1;
				jQuery("#" + self.grid.controlName + "_tablePagination").css({width: wd2+"px" })
				jQuery("#" + self.grid.controlName).css( "margin-bottom", "0px");
			}, 500);
		}
		/* end of re pagination*/
    	
    	/* Begin of autorefresh */
    	//if ((columnChange == undefined) || (columnChange != -1)){
    	if ((columnChange != undefined) && (columnChange > -1)){
    		var meta = OAT.createXMLMetadata(self, columnChange);
    	
    		//var spl = self.grid.div.id + "_" + self.grid.controlName.split("_")[0];
    		var spl =  self.grid.IdForQueryViewerCollection; //self.grid.controlName.toUpperCase().split("_")[0] + "_" + self.grid.controlName.split("_")[0];
    		var listennings = self.grid.QueryViewerCollection[spl]; //["QUERYVIEWER2Container_Queryviewer2"];//[self.grid.controlName];
        		//var listennings = self.QueryViewerCollection["QUERYVIEWER1Container_Queryviewer1"];
        	if ((listennings != "") && (listennings != null) && (listennings != undefined)){
        	 	listennings.onValuesChangedEvent(meta);
        	}
    	}
    	
    	/* -------------------- */
}

OAT.getStateWhenServingPaging = function(controlName, queryName){
	var retrievedObject = localStorage.getItem(OAT.getURL()+controlName+queryName);
	var oldState        = JSON.parse(retrievedObject);
	return oldState;
}

OAT.getState = function(grid, actualPageSize){
    	if (typeof JSON.retrocycle !== 'function') {
    		JSON.retrocycle = function retrocycle($) {
        			'use strict';

        			var px =
            			/^\$(?:\[(?:\d+|\"(?:[^\\\"\u0000-\u001f]|\\([\\\"\/bfnrt]|u[0-9a-zA-Z]{4}))*\")\])*$/;

        				(function rez(value) {

         	   				var i, item, name, path;

            				if (value && typeof value === 'object') {
                				if (Object.prototype.toString.apply(value) === '[object Array]') {
                    				for (i = 0; i < value.length; i += 1) {
                        				item = value[i];
                        				if (item && typeof item === 'object') {
                            			path = item.$ref;
                            			if (typeof path === 'string' && px.test(path)) {
                                			value[i] = eval(path);
                            			} else {
                                			rez(item);
                            			}
                        		}
                   	 	  	}
                		} else {
                    		for (name in value) {
                        		if (typeof value[name] === 'object') {
                            		item = value[name];
                            		if (item) {
                                		path = item.$ref;
                                		if (typeof path === 'string' && px.test(path)) {
                                    		value[name] = eval(path);
                                		} else {
                                    		rez(item);
                                		}
                            		}
                        		}
                    		}
                		}
            		}
        	}($));
        	return $;
    		};
		}
    	var retrievedObject = localStorage.getItem(OAT.getURL()+grid.controlName+grid.query);
		var oldState = JSON.retrocycle(JSON.parse(retrievedObject));
		
		if ( (oldState != null) && ( (oldState.rowsPerPage+" ") == (actualPageSize+" ")) ){
			
			for (var tC = 0; tC < grid.columnsDataType.length; tC++){
				grid.header.cells[tC].html.style.display = oldState.columnDisplay[tC];
				var numCol = grid.columnsDataType.length - 1 - tC;
				var j = 0;
				for (j = 0; j < grid.rows.length; j++) {
                        grid.rows[j].cells[numCol].html.style.display = oldState.columnDisplay[tC];
                }
			}
			 
			grid.conditions = oldState.conditions;
			grid.rowsPerPage = oldState.rowsPerPage;
			return true;
		} else
			return false;
    
}

OAT.SaveMetadata = function(metaDataString, key){
	try {
		if (!!window.localStorage) 
		{
			localStorage.removeItem(OAT.getURL()+key);
			localStorage.setItem(OAT.getURL()+key, metaDataString);
		}
	} catch (error) {
			
	}
}

OAT.GetSavedMetadata = function(key){
	try {
		if (localStorage.getItem(OAT.getURL()+key) != null){
			var mdata = localStorage.getItem(OAT.getURL()+key);	
			if ((mdata != undefined) && (mdata != null)){
				return mdata;
			}
		}
	} catch (error) {
		return "";
	}
	return ""
}

if (!gx.util.browser.webkit){		
	OAT.Event.attach(document,"mouseup",OAT.GridData.up);
	OAT.Event.attach(document,"mousemove",OAT.GridData.move);
}
try {
OAT.Loader.featureLoaded("grid");
} catch (ERROR){
	
}


}





