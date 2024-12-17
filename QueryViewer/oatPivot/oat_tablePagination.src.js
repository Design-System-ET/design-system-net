//jquery.tablePAgination.0.5.min.js
/**
 * tablePagination - A table plugin for jQuery that creates pagination elements
 *
 * http://neoalchemy.org/tablePagination.html
 *
 * Copyright (c) 2009 Ryan Zielke (neoalchemy.com)
 * licensed under the MIT licenses:
 * http://www.opensource.org/licenses/mit-license.php
 *
 * @name tablePagination
 *
 * @author Ryan Zielke (neoalchemy.org)
 * @version 0.5
 * @requires jQuery v1.2.3 or above
 */
if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){

 (function($) {

     $.fn.tablePagination = function(settings) {
         var defaults = {
             firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
             prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
             lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
             nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),
			 rowsPerPage: 5,
             currPage: 1,
             jstype: "pivot",
             optionsForRows: [5, 10, 25, 50, 100],
             ignoreRows: [],
             topNav: false
         };
         settings = $.extend(defaults, settings);
         return this.each(function() {
             var table = $(this)[0];
             var totalPagesId, currPageId, rowsPerPageId, firstPageId, prevPageId, nextPageId, lastPageId;
             totalPagesId = '#tablePagination_totalPages';
             currPageId = '#tablePagination_currPage';
             rowsPerPageId = '#'+settings.controlName+'tablePagination_rowsPerPage';
             firstPageId = '#tablePagination_firstPage';
             prevPageId = '#tablePagination_prevPage';
             nextPageId = '#tablePagination_nextPage';
             lastPageId = '#tablePagination_lastPage';
             var tblLocation = (defaults.topNav) ? "prev" : "next";
			
			 try {
			 	defaults.rowsPerPage = parseInt(defaults.rowsPerPage);
			 } catch (ERROR){
			 	defaults.rowsPerPage = 10;
			 }
			 	
             var possibleTableRows = $.makeArray($('tbody tr', table));
             var tableRows = $.grep(possibleTableRows, function(value, index) {
                 return ($.inArray(value, defaults.ignoreRows) == -1);
             }, false)

             var numRows = tableRows.length
             var totalPages = resetTotalPages();
             var currPageNumber = (defaults.currPage > totalPages) ? 1 : defaults.currPage;
             if ($.inArray(defaults.rowsPerPage, defaults.optionsForRows) == -1)
                 defaults.optionsForRows.push(defaults.rowsPerPage);

             function hideOtherPages(pageNum) {
                 if (pageNum == 0 || pageNum > totalPages)
                     return;
                 var startIndex = (pageNum - 1) * defaults.rowsPerPage;
                 var endIndex = (startIndex + defaults.rowsPerPage - 1);
                 $(tableRows).show(); //show all rows
                 var filteredRow = 0;
                 for (var i = 0; i < tableRows.length; i++) {
                 	 //tableRows[i].setAttribute("visib","tp")
                     if (i < startIndex || i > endIndex) {
                         $(tableRows[i]).hide() //hide row because of pagination
                     
                    
                     
                     } else {
                     	
                     }
                     
                     if (settings.jstype === "pivot"){
                     	while (tableRows[i].childNodes[0].getAttribute('pivotCorrect')!=null){
                     		tableRows[i].deleteCell(0);
                     	}
                     }
                     
                     
                     if (tableRows[i].getAttribute("visibQ") == "tf"){
                     	$(tableRows[i]).hide() //hide row is filtered
                     //	filteredRow = filteredRow + 1;
                     }
                 }
                 
                 
                 
                 if ((settings.jstype === "pivot") && (startIndex > 1)) /* if previuos rows have a inital span td */
                 {
                 	if( (tableRows[startIndex].childNodes[0].getAttribute('spanCorrect')===null) 
                 	 || (tableRows[startIndex].childNodes[0].getAttribute('spanCorrect') === "0") ){
                		var previuos = tableRows[startIndex-1];
                	
                			for (var prw = 1; prw < startIndex+1; prw++) {
                				if (startIndex - prw < 0) break;
                				var previuos = tableRows[startIndex-prw];
                				
                				for (var itemtd = 0; itemtd < previuos.childNodes.length; itemtd++){ /*begin "for" for previous row*/
                				
                					if ((previuos.childNodes[itemtd] != undefined) && (previuos.childNodes[itemtd].getAttribute('rowspan') != undefined) && (previuos.childNodes[itemtd].getAttribute('rowspan') > 1)){
                						if ((previuos.childNodes[itemtd].getAttribute('rowspan') <= prw) || (previuos.childNodes[itemtd].getAttribute('hidden') != null)) {
                							break;
                						}
                					
                						for (var posR = startIndex; posR < startIndex + (previuos.childNodes[itemtd].getAttribute('rowspan') - prw); posR++){ 
                							var oldspan=0;
                							var newcolspan=0;
                							var plusspan=0;
                							if ((tableRows[posR].childNodes[0].getAttribute('colspan') != undefined) && (tableRows[posR].childNodes[0].getAttribute('colspan') != null))
                							{
                								oldspan = parseInt(tableRows[posR].childNodes[0].getAttribute('colspan'));
                							} else {
                								oldspan = 1;
                							}
                							var plusspan;
                							if ((previuos.childNodes[itemtd].getAttribute('colspan') != undefined) && (previuos.childNodes[itemtd].getAttribute('colspan') != null))
                							{
                								plusspan = parseInt(previuos.childNodes[itemtd].getAttribute('colspan'));
                							} else {
                								plusspan = 1;
                							}
                			
                							var newcolspan = plusspan + oldspan;
                							var newCell = tableRows[posR].insertCell(itemtd);
                							newCell.setAttribute('pivotCorrect', true);
                							newCell.setAttribute('colspan',  plusspan);
                							newCell.setAttribute('class', 'pivotAdd');
                							newCell.setAttribute('style', previuos.childNodes[itemtd].style.cssText + 'border-bottom: none; border-top: none;');
                							if ( posR === startIndex) {
                								newCell.innerHTML = previuos.childNodes[itemtd].innerHTML;
                								newCell.className = previuos.childNodes[itemtd].className + " pivotAdd";
                								//newCell.style = previuos.childNodes[itemtd].style;
                								var imgCollapse = newCell.childNodes[0];
                								newCell.removeChild(imgCollapse)
                							}
                							
                							
	            							               							
                							/*if the modified item has rowSpan > 1 then jump "rowSpan-1" rows*/
                							if ((tableRows[posR].childNodes[0].getAttribute('rowspan') != undefined) && (tableRows[posR].childNodes[0].getAttribute('rowspan') != null)){
                								posR = posR + parseInt(tableRows[posR].childNodes[0].getAttribute('rowspan')) - 1;
                							}
                							
                						}
                					
                							
                						//break;
                				}	
                				
                				}/*end for*/	                			
                			}
                	//	}
                	}
                	
                	
                	
                 }
                 
             }

             function resetTotalPages() {
                 var preTotalPages = Math.round(numRows / defaults.rowsPerPage);
                 var totalPages = (preTotalPages * defaults.rowsPerPage < numRows) ? preTotalPages + 1 : preTotalPages;
                 if ($(table)[tblLocation]().find(totalPagesId).length > 0)
                     $(table)[tblLocation]().find(totalPagesId).html(totalPages);
                 return totalPages;
             }

             function resetCurrentPage(currPageNum) { //here sets the value of the current page
                 if (currPageNum < 1 || currPageNum > totalPages)
                     return;
                 currPageNumber = currPageNum;
                 hideOtherPages(currPageNumber);
                 $(table)[tblLocation]().find(currPageId).val(currPageNumber)
             }

             function resetPerPageValues() {
                 var isRowsPerPageMatched = false;
                 var optsPerPage = defaults.optionsForRows;
                 optsPerPage.sort(function(a, b) { return a - b; });
                 var perPageDropdown = $(table)[tblLocation]().find(rowsPerPageId)[0];
                 perPageDropdown.length = 0;
                 for (var i = 0; i < optsPerPage.length; i++) {
                     if (optsPerPage[i] == defaults.rowsPerPage) {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i], true, true);
                         isRowsPerPageMatched = true;
                     }
                     else {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i]);
                     }
                 }
                 //if (!isRowsPerPageMatched) {
                 //    defaults.optionsForRows == optsPerPage[0];
                 //}
                 
                 if (tableRows.length <= defaults.rowsPerPage){
                 	$('#' + settings.controlName + '_tablePagination').css('display', 'none');
                 } else {
                 	$('#' + settings.controlName + '_tablePagination').css('display', '');
                 }
                 
                 if ((totalPages == 1) || (totalPages == 0)) {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', 'none');
                 } else {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', '');
                 }
             }

             function createPaginationElements() {
             		/*var padding1 = "9px 13px 13px 8px";
						var padding2 = "9px 8px 13px 13px;";
						var padding3 = "9px 8px 13px 13px;";
						var padding4 = "9px 13px 13px 8px;";
					*/
					if((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone()) {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'><option value='5'>5</option></select>");
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<img class='pagefirst' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_firstPage' src='" + defaults.firstArrow + "'>");
						htmlBuffer.push("<img class='pageprev' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_prevPage' src='" + defaults.prevArrow + "'>");
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<img class='pagenext' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_nextPage' src='" + defaults.nextArrow + "'>");
						htmlBuffer.push("<img class='pagelast' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_lastPage' src='" + defaults.lastArrow + "'>");
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
					} else {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'><option value='5'>5</option></select>");
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<span class='pagefirst_ios' id='tablePagination_firstPage'/>"); //src='" + defaults.firstArrow + "'
						htmlBuffer.push("<span class='pageprev_ios'  id='tablePagination_prevPage' />"); //src='" + defaults.prevArrow + "'
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<span class='pagenext_ios'  id='tablePagination_nextPage' />"); //src='" + defaults.nextArrow + "'
						htmlBuffer.push("<span class='pagelast_ios'  id='tablePagination_lastPage' />"); //src='" + defaults.lastArrow + "'
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
						
						/*var padding1 = "1px 5px 5px";
						var padding2 = "1px 0 5px;";
						var padding3 = "1px 0 5px 5px;";
						var padding4 = "1px 5px 5px;";
						if(gx.util.browser.webkit) {
							padding1 = "1px 5px 4px";
							padding2 = "1px 0 4px;";
							padding3 = "1px 0 5px 4px;";
							padding4 = "1px 5px 4px;";
						}
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage'><option value='5'>5</option></select>");
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<img style='vertical-align:middle; padding:" + padding1 + "' id='tablePagination_firstPage' src='" + defaults.firstArrow + "'>");
						htmlBuffer.push("<img style='vertical-align:middle; padding:" + padding2 + "' id='tablePagination_prevPage' src='" + defaults.prevArrow + "'>");
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<img style='vertical-align:middle; padding:" + padding3 + "' id='tablePagination_nextPage' src='" + defaults.nextArrow + "'>");
						htmlBuffer.push("<img style='vertical-align:middle; padding:" + padding4 + "' id='tablePagination_lastPage' src='" + defaults.lastArrow + "'>");
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();*/
					}

             }

             if ($(table)[tblLocation]().find(totalPagesId).length == 0) {
                 if (defaults.topNav) {
                     $(this).before(createPaginationElements());
                 } else {
                     $(this).after(createPaginationElements());
                     if (totalPages == 1) {
                         $('#' + settings.controlName + '_tablePagination_paginater').css('display', 'none');
                     } else {
                         $('#' + settings.controlName + '_tablePagination_paginater').css('display', '');
                     }
                 }
             }
             else {
                 $(table)[tblLocation]().find(currPageId).val(currPageNumber);
             }
             resetPerPageValues();
             hideOtherPages(currPageNumber);

             $(table)[tblLocation]().find(firstPageId).bind('click', function(e) {
                 resetCurrentPage(1)
             });

             $(table)[tblLocation]().find(prevPageId).bind('click', function(e) {
                 resetCurrentPage(currPageNumber - 1)
             });

             $(table)[tblLocation]().find(nextPageId).bind('click', function(e) {
                 resetCurrentPage(parseInt(currPageNumber) + 1)
             });

             $(table)[tblLocation]().find(lastPageId).bind('click', function(e) {
                 resetCurrentPage(totalPages)
             });

             $(table)[tblLocation]().find(currPageId).bind('change', function(e) {
                 resetCurrentPage(this.value)
             });

             $(table)[tblLocation]().find(rowsPerPageId).bind('change', function(e) {
                 defaults.rowsPerPage = parseInt(this.value, 10);
                 totalPages = resetTotalPages();
                 resetCurrentPage(1)
                 if (totalPages == 1) {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', 'none');
                 } else {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', '');
                 }
             });

         })
     };
 }
 
 )(jQuery);
 


var	currPageNumber = [];


(function($) {

     $.fn.partialTablePagination = function(settings) {
         var defaults = {
             firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
             prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
             lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
             nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),
			 rowsPerPage: 5,
             currPage: 1,
             jstype: "pivot",
             optionsForRows: [5, 10, 25, 50, 100],
             ignoreRows: [],
             topNav: false,
             cantPages: 10
         };
         settings = $.extend(defaults, settings);
         return this.each(function() {
             var table = $(this)[0];
             var totalPagesId, currPageId, rowsPerPageId, firstPageId, prevPageId, nextPageId, lastPageId;
             totalPagesId = '#tablePagination_totalPages';
             currPageId = '#tablePagination_currPage';
             rowsPerPageId = '#'+settings.controlName+'tablePagination_rowsPerPage';
             firstPageId = '#tablePagination_firstPage';
             prevPageId = '#tablePagination_prevPage';
             nextPageId = '#tablePagination_nextPage';
             lastPageId = '#tablePagination_lastPage';
             var tblLocation = (defaults.topNav) ? "prev" : "next";
			 
			 try {
			 	defaults.rowsPerPage = parseInt(defaults.rowsPerPage);
			 } catch (ERROR){
			 	defaults.rowsPerPage = 10;
			 }
			 	
             var possibleTableRows = $.makeArray($('tbody tr', table));
             var tableRows = $.grep(possibleTableRows, function(value, index) {
                 return ($.inArray(value, defaults.ignoreRows) == -1);
             }, false)

             var numRows = tableRows.length
             var totalPages = resetTotalPages();
             currPageNumber[settings.controlName] = (defaults.currPage > totalPages) ? 1 : defaults.currPage;
             if ($.inArray(defaults.rowsPerPage, defaults.optionsForRows) == -1)
                 defaults.optionsForRows.push(defaults.rowsPerPage);

             function resetTotalPages() {
                return defaults.cantPages;
             }

             function resetCurrentPage(currPageNum, recalculateCantPages) {	//sets current page value
                 if (currPageNum < 1  || (currPageNum > jQuery("#"+settings.controlName+"_tablePagination_paginater #tablePagination_totalPages")[0].innerHTML.replace("&nbsp;","")))
                     return;
                 currPageNumber[settings.controlName] = currPageNum;
                 $(table)[tblLocation]().find(currPageId).val(currPageNumber[settings.controlName])
                 if ((settings.control) && (settings.jstype == "table")){
                 	var cantRows = (recalculateCantPages) ? defaults.rowsPerPage : OAT_JS.grid.gridData[settings.control.UcId].rowsPerPage
                 	settings.control.getDataForTable(settings.controlUcId/*settings.control.UcId*/, currPageNum, cantRows , recalculateCantPages, "", "", "", "")
                 } else if (settings.control) {
                 	var cantRows = (recalculateCantPages) ? defaults.rowsPerPage : settings.control.rowsPerPage
                 	settings.control.getDataForPivot(settings.controlUcId, currPageNum, cantRows , recalculateCantPages, "", "", "", "")
                 }
             }

             function resetPerPageValues() {
                 var isRowsPerPageMatched = false;
                 var optsPerPage = defaults.optionsForRows;
                 optsPerPage.sort(function(a, b) { return a - b; });
                 var perPageDropdown = $(table)[tblLocation]().find(rowsPerPageId)[0];
                 perPageDropdown.length = 0;
                 for (var i = 0; i < optsPerPage.length; i++) {
                     if (optsPerPage[i] == defaults.rowsPerPage) {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i], true, true);
                         isRowsPerPageMatched = true;
                     }
                     else {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i]);
                     }
                 }
                 //if (!isRowsPerPageMatched) {
                 //    defaults.optionsForRows == optsPerPage[0];
                 //}
                 
                 
                 if ((totalPages == 1) || (totalPages == 0)) {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', 'none');
                 } else {
                     $('#' + settings.controlName + '_tablePagination_paginater').css('display', '');
                 }
             }

             function createPaginationElements() {
                 	if((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone()) {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'></select>"); //<option value='5'>5</option>
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<img class='pagefirst' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_firstPage' src='" + defaults.firstArrow + "'>");
						htmlBuffer.push("<img class='pageprev' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_prevPage' src='" + defaults.prevArrow + "'>");
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber[settings.controlName] + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<img class='pagenext' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_nextPage' src='" + defaults.nextArrow + "'>");
						htmlBuffer.push("<img class='pagelast' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_lastPage' src='" + defaults.lastArrow + "'>");
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
					} else {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'><option value='5'>5</option></select>");
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<span class='pagefirst_ios' id='tablePagination_firstPage'/>"); //src='" + defaults.firstArrow + "'
						htmlBuffer.push("<span class='pageprev_ios'  id='tablePagination_prevPage' />"); //src='" + defaults.prevArrow + "'
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber[settings.controlName] + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<span class='pagenext_ios'  id='tablePagination_nextPage' />"); //src='" + defaults.nextArrow + "'
						htmlBuffer.push("<span class='pagelast_ios'  id='tablePagination_lastPage' />"); //src='" + defaults.lastArrow + "'
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
					}
             }

             if ($(table)[tblLocation]().find(totalPagesId).length == 0) {
                 if (defaults.topNav) {
                     $(this).before(createPaginationElements());
                 } else {
                     $(this).after(createPaginationElements());
                 }
             }
             else {
                 $(table)[tblLocation]().find(currPageId).val(currPageNumber[settings.controlName]);
             }
             resetPerPageValues();
             
             $(table)[tblLocation]().find(firstPageId).bind('click', function(e) {
                 resetCurrentPage(1, false)
             });

             $(table)[tblLocation]().find(prevPageId).bind('click', function(e) {
                 resetCurrentPage(currPageNumber[settings.controlName] - 1, false)
             });

             $(table)[tblLocation]().find(nextPageId).bind('click', function(e) {
                 resetCurrentPage(parseInt(currPageNumber[settings.controlName]) + 1, false)  //bind event 
             });

             $(table)[tblLocation]().find(lastPageId).bind('click', function(e) {
             	 resetCurrentPage(parseInt($("#"+settings.controlName+"_tablePagination_paginater #tablePagination_totalPages")[0].textContent.replace("&nbsp;","")), false);	
             });

             $(table)[tblLocation]().find(currPageId).bind('change', function(e) {
             	 resetCurrentPage(parseInt(this.value, 10), false)
             });

             $(table)[tblLocation]().find(rowsPerPageId).bind('change', function(e) {
             	defaults.rowsPerPage = parseInt(this.value, 10);
               	totalPages = resetTotalPages();
                resetCurrentPage(1, true);
             });

         })
     };
 }
 
 )(jQuery);
 
 
 function pseudoPaging(settings, piv){
 	    var defaults = {
             firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
             prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
             lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
             nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),
			 rowsPerPage: 5,
             currPage: 1,
             jstype: "pivot",
             optionsForRows: [5, 10, 25, 50, 100],
             ignoreRows: [],
             topNav: false,
             cantPages: 10
         };
         settings = jQuery.extend(defaults, settings);
         return piv.each(function() {
             var table = jQuery(piv)[0];
             var totalPagesId, currPageId, rowsPerPageId, firstPageId, prevPageId, nextPageId, lastPageId;
             totalPagesId = '#tablePagination_totalPages';
             currPageId = '#tablePagination_currPage';
             rowsPerPageId = '#'+settings.controlName+'tablePagination_rowsPerPage';
             firstPageId = '#tablePagination_firstPage';
             prevPageId = '#tablePagination_prevPage';
             nextPageId = '#tablePagination_nextPage';
             lastPageId = '#tablePagination_lastPage';
             var tblLocation = (defaults.topNav) ? "prev" : "next";
			 
			 try {
			 	defaults.rowsPerPage = parseInt(defaults.rowsPerPage);
			 } catch (ERROR){
			 	defaults.rowsPerPage = 10;
			 }
			 	
             var possibleTableRows = jQuery.makeArray(jQuery('tbody tr', table));
             var tableRows = jQuery.grep(possibleTableRows, function(value, index) {
                 return (jQuery.inArray(value, defaults.ignoreRows) == -1);
             }, false)

             var numRows = tableRows.length
             var totalPages = resetTotalPages();
             currPageNumber[settings.controlName] = (defaults.currPage > totalPages) ? 1 : defaults.currPage;
             if (jQuery.inArray(defaults.rowsPerPage, defaults.optionsForRows) == -1)
                 defaults.optionsForRows.push(defaults.rowsPerPage);

             function resetTotalPages() {
                return defaults.cantPages;
             }

             function resetCurrentPage(currPageNum, recalculateCantPages) {	//sets current page value
                 if (currPageNum < 1  || (currPageNum > jQuery("#"+settings.controlName+"_tablePagination_paginater #tablePagination_totalPages")[0].innerHTML.replace("&nbsp;","")))
                     return;
                 currPageNumber[settings.controlName] = currPageNum;
                 jQuery(table)[tblLocation]().find(currPageId).val(currPageNumber[settings.controlName])
                 if ((settings.control) && (settings.jstype == "table")){
                 	var cantRows = (recalculateCantPages) ? defaults.rowsPerPage : OAT_JS.grid.gridData[settings.control.UcId].rowsPerPage
                 	settings.control.getDataForTable(settings.controlUcId/*settings.control.UcId*/, currPageNum, cantRows , recalculateCantPages, "", "", "", "")
                 } else if (settings.control) {
                 	var cantRows = (recalculateCantPages) ? defaults.rowsPerPage : settings.control.rowsPerPage
                 	settings.control.getDataForPivot(settings.controlUcId, currPageNum, cantRows , recalculateCantPages, "", "", "", "")
                 }
             }

             function resetPerPageValues() {
                 var isRowsPerPageMatched = false;
                 var optsPerPage = defaults.optionsForRows;
                 optsPerPage.sort(function(a, b) { return a - b; });
                 var perPageDropdown = jQuery(table)[tblLocation]().find(rowsPerPageId)[0];
                 perPageDropdown.length = 0;
                 for (var i = 0; i < optsPerPage.length; i++) {
                     if (optsPerPage[i] == defaults.rowsPerPage) {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i], true, true);
                         isRowsPerPageMatched = true;
                     }
                     else {
                         perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i]);
                     }
                 }
                 //if (!isRowsPerPageMatched) {
                 //    defaults.optionsForRows == optsPerPage[0];
                 //}
                 
                 
                 if ((totalPages == 1) || (totalPages == 0)) {
                     jQuery('#' + settings.controlName + '_tablePagination_paginater').css('display', 'none');
                 } else {
                     jQuery('#' + settings.controlName + '_tablePagination_paginater').css('display', '');
                 }
             }

             function createPaginationElements() {
                 	if((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone()) {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'></select>"); //<option value='5'>5</option>
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<img class='pagefirst' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_firstPage' src='" + defaults.firstArrow + "'>");
						htmlBuffer.push("<img class='pageprev' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_prevPage' src='" + defaults.prevArrow + "'>");
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber[settings.controlName] + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<img class='pagenext' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_nextPage' src='" + defaults.nextArrow + "'>");
						htmlBuffer.push("<img class='pagelast' style='vertical-align:middle; width:0px; height:0px;' id='tablePagination_lastPage' src='" + defaults.lastArrow + "'>");
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
					} else {
						var htmlBuffer = [];
						htmlBuffer.push("<div id='" + settings.controlName + "_tablePagination' class='pivot_pag_div'>");
						htmlBuffer.push("<span id='tablePagination_perPage'>");
						
						htmlBuffer.push("<select id='" + settings.controlName + "tablePagination_rowsPerPage' style='margin-top: 1px;'><option value='5'>5</option></select>");
						
						htmlBuffer.push(gx.getMessage("GXPL_QViewerJSPerPage"));
						htmlBuffer.push("</span>");
						htmlBuffer.push("<span id='" + settings.controlName + "_tablePagination_paginater'>");
						htmlBuffer.push("<span class='pagefirst_ios' id='tablePagination_firstPage'/>"); //src='" + defaults.firstArrow + "'
						htmlBuffer.push("<span class='pageprev_ios'  id='tablePagination_prevPage' />"); //src='" + defaults.prevArrow + "'
						htmlBuffer.push("<span style=''>&nbsp;" + gx.getMessage("GXPL_QViewerJSPage") + "&nbsp;</span>");
						htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='" + currPageNumber[settings.controlName] + "' size='1' style='margin-top: 2px;'>");
						htmlBuffer.push("<span>&nbsp;" + gx.getMessage("GXPL_QViewerJSOf") + "</span><span id='tablePagination_totalPages'>&nbsp;" + totalPages + "</span>");
						htmlBuffer.push("<span class='pagenext_ios'  id='tablePagination_nextPage' />"); //src='" + defaults.nextArrow + "'
						htmlBuffer.push("<span class='pagelast_ios'  id='tablePagination_lastPage' />"); //src='" + defaults.lastArrow + "'
						htmlBuffer.push("</span>");
						htmlBuffer.push("</div>");
						return htmlBuffer.join("").toString();
					}
             }

             if (jQuery(table)[tblLocation]().find(totalPagesId).length == 0) {
                 if (defaults.topNav) {
                     jQuery(piv).before(createPaginationElements());
                 } else {
                     jQuery(piv).after(createPaginationElements());
                 }
             }
             else {
                 jQuery(table)[tblLocation]().find(currPageId).val(currPageNumber[settings.controlName]);
             }
             resetPerPageValues();
             
             jQuery(table)[tblLocation]().find(firstPageId).bind('click', function(e) {
                 resetCurrentPage(1, false)
             });

             jQuery(table)[tblLocation]().find(prevPageId).bind('click', function(e) {
                 resetCurrentPage(currPageNumber[settings.controlName] - 1, false)
             });

             jQuery(table)[tblLocation]().find(nextPageId).bind('click', function(e) {
                 resetCurrentPage(parseInt(currPageNumber[settings.controlName]) + 1, false)  //bind event 
             });

             jQuery(table)[tblLocation]().find(lastPageId).bind('click', function(e) {
             	 resetCurrentPage(parseInt(jQuery("#"+settings.controlName+"_tablePagination_paginater #tablePagination_totalPages")[0].textContent.replace("&nbsp;","")), false);	
             });

             jQuery(table)[tblLocation]().find(currPageId).bind('change', function(e) {
             	 resetCurrentPage(parseInt(this.value, 10), false)
             });

             jQuery(table)[tblLocation]().find(rowsPerPageId).bind('change', function(e) {
             	defaults.rowsPerPage = parseInt(this.value, 10);
               	totalPages = resetTotalPages();
                resetCurrentPage(1, true);
             });

         })
     };
 
 }






