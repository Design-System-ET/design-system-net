
var wwp = null;
try {
	if (parent != null && parent.wwp != null) {
		wwp = parent.wwp;
	}
}
catch (e) {
	//avoid exception: DOMException: Blocked a frame with origin "..." from accessing a cross-origin frame.
}
if (wwp == null) {
	wwp = {
		'settings': {
			'columnsSelector': {
				'AllowColumnResizing': false,
				'AllowColumnReordering': false,
				'AllowColumnDragging': false
			},
			'pagBar': {
				'IncludeGoTo': false
			}
		},
		'labels': {
			'UpdateButtonText': '',
			'AddNewOption': '',
			'OnlySelectedValues': '',
			'MultipleValuesSeparator': '',
			'SelectAll': '',
			'SortASC': '',
			'SortDSC': '',
			'AllowGroupText': '',
			'FixLeftText': '',
			'FixRightText': '',
			'LoadingData': '',
			'CleanFilter': '',
			'RangeFilterFrom': '',
			'RangeFilterTo': '',
			'RangePickerInviteMessage': '',
			'NoResultsFound': '',
			'SearchButtonText': '',
			'SearchMultipleValuesButtonText': '',
			'ColumnSelectorFixedLeftCategory': '',
			'ColumnSelectorFixedRightCategory': '',
			'ColumnSelectorNotFixedCategory': '',
			'ColumnSelectorFixedEmpty': '',
			'ColumnSelectorRestoreTooltip': ''
		}
	};
}

var scrollHWidth = null;
var scrollVWidth = null;
function getScrollBarWidth(vertical) {
	if (vertical && scrollVWidth == null || !vertical && scrollHWidth == null) {
		var inner = document.createElement('p');
		if (vertical) {
			inner.style.width = "100%";
			inner.style.height = "200px";
		} else {
			inner.style.width = "200px";
			inner.style.height = "100%";
		}

		var outer = document.createElement('div');
		outer.style.position = "absolute";
		outer.style.top = "0px";
		outer.style.left = "0px";
		outer.style.visibility = "hidden";
		outer.style.width = "150px";
		outer.style.height = "150px";
		outer.style.overflow = "hidden";
		outer.appendChild(inner);

		document.body.appendChild(outer);

		if (vertical) {
			var w1 = inner.offsetWidth;
			outer.style.overflow = 'scroll';
			var w2 = inner.offsetWidth;
			if (w1 == w2) w2 = outer.clientWidth;

			document.body.removeChild(outer);
			scrollVWidth = (w1 - w2);
		} else {
			var w1 = inner.offsetHeight;
			outer.style.overflow = 'scroll';
			var w2 = inner.offsetHeight;
			if (w1 == w2) w2 = outer.clientHeight;

			document.body.removeChild(outer);
			scrollHWidth = (w1 - w2);
		}
	}
	return vertical ? scrollVWidth : scrollHWidth;
}

function setCSSLayersOrder() {
	var headTag = document.getElementsByTagName('head')[0];
	var style = document.createElement('style');
	style.type = 'text/css';
	style.innerHTML = '@layer '
		+ 'bootstrap_css_bootstrap_min_css, '
		+ 'DVelop_Bootstrap_Shared_DVelopBootstrap_css, '
		+ 'DVelop_Shared_daterangepicker_daterangepicker_css, '
		+ 'DVelop_Bootstrap_Shared_fontawesome_vlatest_css_all_min_css, '
		+ 'DVelop_DVHorizontalMenu_DVHorizontalMenu_css, '
		+ 'DVelop_DVPaginationBar_DVPaginationBar_css, '
		+ 'DVelop_DVMessage_DVMessage_css;';
	headTag.insertBefore(style, headTag.children[0]);
}

function hasScroll(elem, vertical) {
	if (elem == null || elem.length != 1) {
		return false;
	}
	if (vertical) {
		return (elem[0].scrollHeight - elem[0].clientHeight) > 0;
	} else {
		return elem[0].scrollWidth - elem[0].clientWidth > 0;
	}
}

function getScrollWidthIfVisible(elem, vertical) {
	return hasScroll(elem, vertical) ? getScrollBarWidth(vertical) : 0;
}

function WWPSelectAll(allBox, upperSelectFilterName) {
	for (var i = 0; i < document.MAINFORM.elements.length; i++) {
		var e = document.MAINFORM.elements[i];

		if (e.type == 'checkbox'
			&& e.name != allBox.name
			&& e.name.toUpperCase().indexOf(upperSelectFilterName) >= 0
			&& e.checked != allBox.checked) {
			e.parentNode.click();
			e.checked = allBox.checked;
		}
	}
}

function WWPSelectAllGXUI(allSelectedVariable) {
	if (document.getElementById(allSelectedVariable).value == 'true') {
		document.getElementById(allSelectedVariable).value = 'false';
	} else {
		document.getElementById(allSelectedVariable).value = 'true';
	}
	WWPDoGXRefresh();
}

function WWPSelectAllRemoveParentOnClick(allBox) {
	allBox.parentNode.onclick = '';
}

function WWPSortColumn(orderAscId, orderById, orderIndex) {
	var selOrderIndex;
	if (orderById != '') {
		selOrderIndex = document.getElementById(orderById).value;
	} else {
		selOrderIndex = orderIndex;
	}
	if (selOrderIndex == orderIndex) {
		if (document.getElementById(orderAscId).value == 'true') {
			document.getElementById(orderAscId).value = 'false';
		} else {
			document.getElementById(orderAscId).value = 'true';
		}
		$('#' + orderAscId).trigger('change');
	} else {
		$('#' + orderById).val(orderIndex);
		$('#' + orderById).trigger('change');
	}
	var wcName = '';
	if (orderAscId != null && orderAscId.indexOf("vORDEREDDSC") > 0) {
		wcName = orderAscId.substring(0, orderAscId.indexOf("vORDEREDDSC"));
	}
	WWPDoGXRefresh(wcName);
}

function WWPDoGXRefresh() {
	WWPDoGXRefresh('');
}

function WWPCloseOpenedDWC(gridName) {
	$(".WCD_Expanded").each(function () {
		if (($(this).closest('div.gx-grid').attr('id') + '').toLowerCase() == (gridName + '').toLowerCase() + 'containerdiv') {
			var dwcRow = $(this).closest('tr').next();
			if (dwcRow.hasClass('WCD_tr')) {
				dwcRow.remove();
			}
			$(this).removeClass("WCD_Expanded");
		}
	});
	return true;
}

function WWPDoGXRefresh(wcName) {
	if (wcName == null) {
		wcName = '';
	}
	gx.evt.execEvt(wcName, false, wcName + 'EREFRESH.', this);
}

function WWPDynFilterHideLast(lastIndex) {
	while (lastIndex > 1) {
		if (document.getElementById('Dynamicfiltersrow' + lastIndex).className != 'Invisible') {
			document.getElementById('Dynamicfiltersrow' + lastIndex).className = 'Invisible';
			return true;
		}
		lastIndex--;
	}
	return true;
}

function WWPDynFilterHideLast_AL(dynTableId, lastIndex, fixedFilters) {
	var $dynTable = $('#' + dynTableId);
	var isFlex = ($dynTable.attr('data-gx-flex') != null);
	if (!isFlex) {
		$dynTable = $dynTable.children().eq(0);
	}
	while (lastIndex > 1) {
		var row = $dynTable[0].childNodes[lastIndex + fixedFilters - 1];
		if ((isFlex ? row.childNodes[0] : row).childNodes[0].className.indexOf('Invisible') == -1) {
			$(row).removeClass('DynRowVisible');
			for (var i = 0, cell; cell = (isFlex ? row.childNodes[0] : row).childNodes[i]; i++) {
				cell.className = 'Invisible';
			}
			return true;
		}
		lastIndex--;
	}
	return true;
}


function WWPDynFilterShow(index) {
	document.getElementById('Dynamicfiltersrow' + index).className = '';
	return true;
}

function WWPDynFilterShow_AL(dynTableId, index, fixedFilters) {
	var $dynTable = $('#' + dynTableId);
	var isFlex = ($dynTable.attr('data-gx-flex') != null);
	if (!isFlex) {
		$dynTable = $dynTable.children().eq(0);
	}
	var row = $dynTable[0].childNodes[index + fixedFilters - 1];
	$(row).addClass('DynRowVisible');
	var firstRow = isFlex ? $dynTable[0].childNodes[fixedFilters].childNodes[0] : $dynTable[0].childNodes[fixedFilters];
	for (var i = 0, cell; cell = (isFlex ? row.childNodes[0] : row).childNodes[i]; i++) {
		cell.className = firstRow.childNodes[i].className;
	}
	return true;
}

function WWPDynFilterHideAll(totalRows) {
	while (totalRows > 1) {
		if (document.getElementById('Dynamicfiltersrow' + totalRows).className != 'Invisible') {
			document.getElementById('Dynamicfiltersrow' + totalRows).className = 'Invisible';
		}
		totalRows--;
	}
	return true;
}

function WWPDynFilterHideAll_AL(dynTableId, totalRows, fixedFilters) {
	var $dynTable = $('#' + dynTableId);
	if ($dynTable.length == 1) {
		var isFlex = ($dynTable.attr('data-gx-flex') != null);
		if (!isFlex) {
			$dynTable = $dynTable.children().eq(0);
		}
		if (totalRows == 999) {
			var firstDynRow = $('#' + dynTableId + 'ROW1');
			if (firstDynRow.length == 1) {
				fixedFilters = firstDynRow.parent().index();
				totalRows = $dynTable[0].childNodes.length - fixedFilters;
			} else {
				totalRows = 0;
			}
		}
		while (totalRows > 1) {
			var row = $dynTable[0].childNodes[totalRows + fixedFilters - 1];
			if ((isFlex ? row.childNodes[0] : row).childNodes[0].className.indexOf('Invisible') == -1) {
				$(row).removeClass('DynRowVisible');
				for (var i = 0, cell; cell = (isFlex ? row.childNodes[0] : row).childNodes[i]; i++) {
					cell.className = 'Invisible';
				}
			}
			totalRows--;
		}
	}
	return true;
}

function WWP_escapeRegExp(string) {
	return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

function WWP_replaceAll(string, find, replace) {
	return (!string) ? "" : string.replace(new RegExp(WWP_escapeRegExp(find), 'g'), replace);
}

function WWP_IsTrnMode(mod) {
	return gx != null && gx.O != null && gx.O.isTransaction() && gx.O.Gx_mode == mod;
}

function WWP_GetGrid(elem) {
	if (elem.GridObjId == null) {
		if (elem.GridInternalName == 'GRID') {
			elem.GridObjId = 'GridContainerDiv';
			var $grid = $('#' + elem.GridObjId);
			if ($grid.length == 0) {
				elem.GridObjId = null;
			} else {
				return $grid;
			}
		}
		if (elem.GridObjId == null) {
			var thisC = elem;
			$(".gx-grid").each(function (i) {
				if (this.id != null && this.id.toUpperCase() == thisC.GridInternalName + 'CONTAINERDIV') {
					thisC.GridObjId = this.id;
					return false;
				}
			});
		}
		if (elem.GridObjId == null) {
			console.log('grid not found');
			return $();
		}
	}

	var $grid2 = $('#' + elem.GridObjId);
	if ($grid2.length == 0 && !elem.insideDWC) {
		console.log('grid not found (2)');
	}
	return $grid2;
}

function WWP_GetInsertGrid(elem) {
	if (elem.GridInternalName == WWP_getCurrentWCId(elem) + 'GRID') {
		WWP_GetGrid(elem);
		if (elem.GridObjId != null) {
			var gridId = elem.GridObjId.substring(0, elem.GridObjId.length - 'GridContainerDiv'.length) + 'GridinsertlineContainerDiv';
			return $('#' + gridId);
		}
	}
	return $();
}

function WWP_HasGridEmpowerer(elem) {
	if (elem.GridInternalName != '' && elem.GridInternalName != null && elem.HasGridEmp == null) {
		elem.HasGridEmp = WWP_GetGrid(elem).parent().closest('.gx-grid,.HasGridEmpowerer').hasClass('HasGridEmpowerer');
	}
	return elem.HasGridEmp;
}

function WWP_NotifyEmpower(elem, key) {
	if (WWP_HasGridEmpowerer(elem)) {
		var $grid = WWP_GetGrid(elem);
		if ($grid.data(key) == null) {
			$grid.data(key, elem);
		}
		var empObj = $grid.data('WWP.Empowerer');
		if (empObj != null) {
			empObj.Empower();
		}
	}
}

function WWP_GetEmpowerer(elem) {
	return WWP_GetGrid(elem).data('WWP.Empowerer');
}

function WWP_ClearEmpowerReference(elem, key) {
	if (WWP_HasGridEmpowerer(elem)) {
		var $grid = WWP_GetGrid(elem);
		$grid.data(key, null);
	}
}

function WWP_endsWith(str, strToSearch) {
	return str != null && strToSearch != null && str.length >= strToSearch.length && str.substring(str.length - strToSearch.length) == strToSearch;
}

function WWP_startsWith(str, strToSearch) {
	return str != null && strToSearch != null && str.indexOf(strToSearch) == 0;
}

function WWP_ddo_needToInvertPosition(dropdownSelector, ulDropDown, triggerButton) {
	var $controlContainer = $(dropdownSelector);
	var overflowEl = ulDropDown.parentElement;
	var overflow;
	for (var i = 0; i < 18; i++) {
		if (overflowEl == null || !$(overflowEl).is('table.table-responsive') && ((overflow = $(overflowEl).css('overflow')) == 'auto' || overflow == 'hidden' || $(overflowEl).css('overflow-x') == 'auto')) {
			break;
		}
		else {
			overflowEl = overflowEl.parentElement;
		}
	}
	var res = { left: false, top: false };

	var $ul = $(ulDropDown);
	if (((overflow = $(overflowEl).css('overflow')) == 'auto' || overflow == 'hidden' || $(overflowEl).css('overflow-x') == 'auto')) {
		res.overflowEl = overflowEl;
		var elemLeft = $ul.offset().left - $(overflowEl).offset().left;
		var elemRight = elemLeft + $ul.width();
		res.left = (!(elemLeft >= 0 && elemRight <= $(overflowEl).width()));

		var elemTop = $ul.offset().top - $(overflowEl).offset().top;
		var elemBottom = elemTop + $ul.height();
		res.top = (!(elemTop >= 0 && elemBottom <= $(overflowEl).height()));
	}

	var rect = $ul[0].getBoundingClientRect()
	var isRTL = WWP_IsRTL();
	res.left = res.left
		|| isRTL && rect.left < 0
		|| !isRTL && (rect.left + rect.width + 12) > window.innerWidth;
	if (!res.top) {
		var rectBottom = (rect.top + rect.height + 12);
		if ($ul.hasClass('DDComponentUL') && $ul.find('>li>div>div.WCD_Loading').length == 1) {
			//se esta cargando un DDComponent por lo que se asume que su tama�o crecera (caso ai assistant)
			rectBottom += 30;
		}
		res.top = rectBottom > window.innerHeight;
	}

	return res;
}

function WWP_dropdownAutoPosition(dropdownSelector, ulDropDown, triggerButton) {
	//reset autoposition
	$(ulDropDown).css({ 'margin-left': '', 'margin-left': '' });
	$(dropdownSelector).removeClass('dropup');

	var autoPos = WWP_ddo_needToInvertPosition(dropdownSelector, ulDropDown, triggerButton);
	if (autoPos.left) {
		WWP_ddo_invertPosition(ulDropDown, triggerButton);
	}
	if (autoPos.top) {
		$(dropdownSelector).addClass('dropup');
		if (autoPos.overflowEl != null) {
			var elemTop = $(ulDropDown).offset().top - $(autoPos.overflowEl).offset().top;
			var elemBottom = elemTop + $(ulDropDown).height();
			if (!(elemTop >= 0 && elemBottom <= $(autoPos.overflowEl).height())) {
				$(dropdownSelector).removeClass('dropup');
			}
		} else {
			var rect = ulDropDown.getBoundingClientRect()
			if (rect.top < 0) {
				$(dropdownSelector).removeClass('dropup');
			}
		}
	}
}

function WWP_IsRTL() {
	return $('body').css('direction') == 'rtl';
}

function WWP_ddo_invertPosition(ulDropDown, triggerButton) {
	var marginDirection = (WWP_IsRTL() ? "right" : "left");
	var newMargin = ($(ulDropDown).outerWidth() - $(triggerButton).outerWidth()) * -1;
	$(ulDropDown).css("margin-" + marginDirection, newMargin + "px");

	var offsetLeft = $(ulDropDown).offset().left;
	if (offsetLeft < 0) {
		newMargin -= offsetLeft;
		$(ulDropDown).css("margin-" + marginDirection, newMargin + "px");
	}
}

function WWP_setSectionGridMinHeight(floatingElem, setMinHeight) {
	var overflowEl = floatingElem.parentElement;
	for (var i = 0; i < 18; i++) {
		if (overflowEl == null || !$(overflowEl).is('table.table-responsive') && ($(overflowEl).css('overflow') == 'auto' || $(overflowEl).css('overflow-x') == 'auto')) {
			break;
		}
		else {
			overflowEl = overflowEl.parentElement;
		}
	}
	if (($(overflowEl).css('overflow') == 'auto' || $(overflowEl).css('overflow-x') == 'auto')) {
		if (setMinHeight) {
			var minH = $(floatingElem).offset().top + $(floatingElem).outerHeight() + 12 - $(overflowEl).offset().top;//12 = shadow
			$(overflowEl).css({ 'min-height': minH + "px" });
			if (hasScroll($(overflowEl), false)) {
				//has horizontal scroll visible
				minH += getScrollBarWidth(false);
				$(overflowEl).css({ 'min-height': minH + "px" });
			}
		}
		else {
			$(overflowEl).css({ 'min-height': "" });
		}
		return overflowEl;
	} else {
		return null;
	}
}

function WWP_getCurrentWCId(currentUC) {
	return currentUC.DesignContainerName.replace('Container', '').replace(currentUC.ControlName.toUpperCase(), '');
}

function WWP_getParentWCId(currentUC) {
	var currentWC = WWP_getCurrentWCId(currentUC);
	if (currentWC != '') {
		return currentWC.substring(0, currentWC.length - 5);
	} else {
		return '';
	}
}

function WWP_getWCParentDiv(parentDivName, currentUC) {
	return $('#' + currentUC.ContainerName.replace('Container', '').replace(currentUC.ControlName.toUpperCase(), parentDivName.toUpperCase()));
}

function WWP_getWCId(parentDivName, currentUC, $wcOriginalCell) {
	if ($wcOriginalCell == null) {
		$wcOriginalCell = WWP_getWCParentDiv(parentDivName, currentUC);
	}
	var wcId;
	if ($wcOriginalCell.children().length > 0) {
		wcId = $wcOriginalCell.children()[0].id;
		$wcOriginalCell.data('wcd-id', wcId);
	} else {
		wcId = $wcOriginalCell.data('wcd-id');
	}

	if (WWP_startsWith(wcId, 'gxHTMLWrp')) {
		var wcIdAuxSelector = $('#' + wcId.substring(9))
		if (wcIdAuxSelector.length == 1 && wcIdAuxSelector.parent()[0].tagName == 'BODY') {
			wcIdAuxSelector.remove();
		}
	}
	return wcId;
}

function WWP_moveWCToDiv(wcId, currentUC, showLoading, divForWC) {
	var $wcElem = $("#" + wcId);
	if ($wcElem.length > 0) {
		if (!$wcElem.parent().hasClass('Invisible')) {
			if ($wcElem.parent().parent().parent().parent().is('ul')) {
				//WC is used by other DDComponent -> close it
				$wcElem.closest('ul').parent().removeClass('open');
			} else {
				//maybe a popover if open -> close it
				WWPActions.Popover_Close(wcId, false);
			}
		}

		$wcElem = $wcElem.detach();
		if (showLoading) {
			$wcElem.find('.gxwebcomponent-body').addClass('Invisible');
		}
		$wcElem.appendTo(divForWC);
	} else {
		var newWcElem = document.createElement('div');
		newWcElem.id = wcId;
		divForWC.appendChild(newWcElem);
		$wcElem = $(newWcElem);
	}
	return $wcElem;
}

function WWP_revertWCMoved(parentDivName, currentUC, currentElem, clearWC) {
	var $wcOriginalCell = WWP_getWCParentDiv(parentDivName, currentUC);
	if ($wcOriginalCell.children().length == 0) {
		var $wcElem = $('#' + WWP_getWCId(parentDivName, currentUC, $wcOriginalCell));
		if ($wcElem.closest('.popover,ul').prev()[0] == currentElem) {//only detach if wc is in the popover/DDComponent of this element			
			$wcElem.parent().css({ 'min-height': $wcElem.parent().height(), 'min-width': $wcElem.parent().width() });
			var $wcElem = $wcElem.detach();
			WWP_Debug_Log(false, 'detach');
			if ($wcElem.length == 1) {
				$wcElem.appendTo($wcOriginalCell[0]);
			} else {
				WWP_Debug_Log(false, 'detach div');
				$wcOriginalCell.html('<div id="' + wcId + '"></div>');
				$wcElem = $wcOriginalCell.find('>div');
			}
			if (clearWC) {
				$wcElem.html('');
			}
		}
	}
}

function WWP_getAjaxCallRestURL(proc) {
	return gx.util.resourceUrl(gx.basePath + (proc.indexOf('/') == 0 ? '' : '/rest/') + WWP_replaceAll(proc, '.', '/'), true);
}

function WWP_getRPCBody(remoteServicesParameters, remoteBody) {
	if (remoteServicesParameters[0] != '"' || remoteServicesParameters[remoteServicesParameters.length - 1] != '"') {
		remoteServicesParameters = '"' + remoteServicesParameters + '"';
	}
	return '{' + remoteServicesParameters + ', "Body": "' + WWP_encodeTextForAjaxBody(remoteBody) + '"}';
}

function WWP_encodeTextForAjaxBody(remoteBody) {
	if (remoteBody != null && remoteBody.length > 0) {
		remoteBody = WWP_replaceAll(remoteBody, '\\', '[\\]');
		remoteBody = WWP_replaceAll(remoteBody, '"', '\\"');
		remoteBody = WWP_replaceAll(remoteBody, '[\\]', '\\\\');
	}
	return remoteBody;
}

function WWP_IsIE() {
	//Is IE 11 or older
	return !WWP_IsChrome() && (window.navigator.userAgent.indexOf("MSIE ") >= 0 || window.navigator.userAgent.indexOf('Edge') >= 0 || (!!window.MSInputMethodContext && !!document.documentMode));
}

function WWP_IsChrome() {
	return (/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) && !(/edge/.test(navigator.userAgent.toLowerCase()));
}

function WWP_OffsetLeft(isRTL, $elem) {
	return $elem.offset().left + (isRTL ? $elem.outerWidth() : 0);
}

function WWP_GxValidControl(controlId) {
	var controlValidStructFld = gx.O.getValidStructFld(controlId);
	if (controlValidStructFld != null) {
		gx.csv.validControls(controlValidStructFld.id, controlValidStructFld.id + 1, true, gx.O);
	}
}

function WWP_PadR(text, count, char) {
	while (text.length < count) {
		text += char;
	}
	return text;
}

function WWP_CreateFontIcon(selector, iconClass) {
	var $selected = $(selector);
	if ($(selector).find('i').length == 0) {
		var i = document.createElement('i');
		i.className = iconClass;
		$selected.prepend(i);
	}
}

var WWPActions_Properties = {};
WWPActions = {

	WCPopup_Close: function (result) {
		if (WWPActions_Properties.CurrentModal != null) {
			$('#' + WWPActions_Properties.CurrentModal + '>div>div>.modal-header .close').data('close-result', result).click().data('close-result', null);
		}
	},

	WCPopup_UpdateTitle: function (title) {
		if (WWPActions_Properties.CurrentModal != null) {
			$('#' + WWPActions_Properties.CurrentModal + '>div>div>.modal-header h4').text(title);
		}
	},

	DropDownComponent_Close: function (anyControlId) {
		var $triggerButton = $('#' + anyControlId).closest('.dropdown-menu.DDComponentUL').prev();
		var uc = $triggerButton.data('wwpControl');
		if (uc != null) {
			uc.control.closingProgramatically = true;

		}
		$triggerButton.click();
		if (uc != null) {
			delete uc.control.closingProgramatically;
		}
	},

	DropDownComponent_CloseWithResult: function (anyControlId, result) {
		var $triggerButton = $('#' + anyControlId).closest('.dropdown-menu.DDComponentUL').prev();
		var uc = $triggerButton.data('wwpControl');
		if (uc != null) {
			uc.control.Result = result;
			uc.control.closingProgramatically = true;
		}
		$triggerButton.click();
		if (uc != null) {
			delete uc.control.closingProgramatically;
		}
	},

	//DropDownComponent_AddButtonClass: function (anyControlId, newClass, onlyIfClosed) {
	//	var $uc = $('#' + anyControlId).closest('.dropdown-menu.DDComponentUL').parent();
	//	if ($uc.length == 1 && (!onlyIfClosed || !$uc.hasClass('open'))) {
	//		$uc.addClass(newClass);
	//	} else if ($uc.length == 0) {
	//		var containerDivId = $('#' + anyControlId).closest('div.Invisible').attr('id');
	//		if (containerDivId != null && WWP_startsWith(containerDivId, 'DIV_')) {
	//			$uc = $('#' + containerDivId.substring(4) + 'Container.gx_usercontrol');
	//			if ($uc.length == 1) {
	//				$uc.addClass(newClass);
	//			}
	//		}
	//	}
	//},

	DateTimePicker_SetMinutesIncrement: function (dateTimeControlId, minutesIncrement) {
		$('#' + dateTimeControlId).attr('data-minInterval', minutesIncrement);
	},

	DateTimePicker_SetOptions: function (dateTimeControlId, optionsJson) {
		var $datePicker = $('#' + dateTimeControlId).data('daterangepicker');
		if ($datePicker != null) {
			var options = JSON.parse(optionsJson);
			WWPDateTimePicker_updateOption($datePicker, 'minDate', options.MinDate != null ? new Date(options.MinDate) : null, true);
			WWPDateTimePicker_updateOption($datePicker, 'maxDate', options.MaxDate != null ? new Date(options.MaxDate) : null, true);
			if (options.DatePicker != null) {
				WWPDateTimePicker_updateOption($datePicker, 'minYear', options.DatePicker.MinYear, false);
				WWPDateTimePicker_updateOption($datePicker, 'maxYear', options.DatePicker.MaxYear, false);
				WWPDateTimePicker_updateOption($datePicker, 'showDropdowns', options.DatePicker.ShowDropdowns, false);
				WWPDateTimePicker_updateOption($datePicker, 'showWeekNumbers', options.DatePicker.ShowWeekNumbers, false);
			}
			WWPDateTimePicker_updateOption($datePicker, 'timePickerIncrement', options.TimePicker != null ? options.TimePicker.Increment : null, false);
			var formattedDays = null;
			if (options.FormattedDays != null) {
				formattedDays = [];
				for (var i = 0; i < options.FormattedDays.length; i++) {
					formattedDays.push({
						'date': moment(new Date(options.FormattedDays[i].Date)),
						'disabled': options.FormattedDays[i].Disabled,
						'class': options.FormattedDays[i].Class,
						'tooltip': options.FormattedDays[i].Tooltip
					});
				}
			}
			WWPDateTimePicker_updateOption($datePicker, 'formattedDays', formattedDays, false);

		} else {
			console.error('Date control not found: ' + dateTimeControlId);
		}
	},

	Textarea_EnterBehaviourToAction: function (textareaInternalName, btnInternalName) {
		var $textareaInternalName = $('#' + textareaInternalName);
		$textareaInternalName.on('keydown', function (e) {
			if (e.keyCode == 13 && !e.shiftKey && !e.altKey) {
				var $prev = $textareaInternalName.prev();
				if (!$prev.is('.dropdown.suggest') || $prev.find('>.dropdown-menu.show').length == 0 && !$textareaInternalName.data('suggestEnterCaptured')) {
					$('#' + btnInternalName).click();
					return false;
				}
			}
		});
	},

	GridDetailWebComponent_Close: function (tableName, doRefresh) {
		$('#' + tableName).closest('.WCD_tr').prev().find('.WCD_Expanded a').click();
		if (doRefresh) {
			window.setTimeout(function () {
				WWPDoGXRefresh();
			}, 200);
		}
	},

	Popover_Close: function (tableName, doRefresh) {
		$('#' + tableName).closest('.popover').prev().data('forceHide', '').popover('hide');
		if (doRefresh) {
			window.setTimeout(function () {
				WWPDoGXRefresh();
			}, 200);
		}
	},

	Tabs_EditCaption: function (anyControlId, tabsName, tabId, caption, isHTML, tabClass) {
		var wcContainerId = $('#' + anyControlId).closest('div.gxwebcomponent').attr('id');
		var wcId = '';
		if (wcContainerId != null && wcContainerId != '') {
			wcId = wcContainerId.substring(wcContainerId.length - 5);
		}

		var $tabA = $('#' + wcId + tabsName.toUpperCase() + 'Container').find('ul.nav-tabs:eq(0)').find('>li>a[data-code=' + tabId + ']');
		if ($tabA.length == 1) {
			if (tabClass != null && tabClass != '') {
				var isActive = $tabA.parent().hasClass('active');
				$tabA.parent()[0].className = tabClass + (isActive ? ' active' : '');
			}
			if (isHTML) {
				$tabA.html(caption);
			} else {
				$tabA.text(caption);
			}
		}
	},

	Mask_Apply: function (controlId, maskStr, grid, isReverse) {
		this.Mask_ApplyMultipleMasks(controlId, maskStr, null, null, grid, isReverse);
	},

	Mask_ApplyMultipleMasks: function (controlId, maskStr, mask2Str, mask3Str, grid, isReverse, isGridItem) {
		if ((grid || '') != '') {
			gx.fx.obs.addObserver("grid.onafterrender", window, function () {
				for (var i = 1; i < 1000; i++) {
					var indexStr = ('000' + i);
					if ($('#' + controlId + '_' + indexStr.substring(indexStr.length - 4, indexStr.length)).length == 1) {
						WWPActions.Mask_ApplyMultipleMasks(controlId + '_' + indexStr, maskStr, mask2Str, mask3Str, false, isReverse, true);
					} else {
						return;
					}
				}
			});
		}
		else {
			var $ctr = $('#' + controlId);
			var hasMask = ($ctr.data('mask') != null);
			if (isGridItem && hasMask) {
				return;
			}
			var callGxValidControl = hasMask && $ctr.val() != '';
			if (callGxValidControl) {
				$ctr.unmask();
			}
			maskStr = WWP_replaceAll(WWP_replaceAll(maskStr, '<THS>', gx.thousandSeparator), '<DEC>', gx.decimalPoint);
			mask2Str = WWP_replaceAll(WWP_replaceAll(mask2Str || '', '<THS>', gx.thousandSeparator), '<DEC>', gx.decimalPoint);
			mask3Str = WWP_replaceAll(WWP_replaceAll(mask3Str || '', '<THS>', gx.thousandSeparator), '<DEC>', gx.decimalPoint);
			if ((mask2Str || '') != '') {
				//primero se aplica la mascara real para que se valide y formatee y finalmente se le agrega el caracter opcional al final para el cambio de mascara
				var maskMaxLength = Math.max(Math.max(maskStr.length, mask2Str.length), mask3Str.length);
				var options = {
					reverse: isReverse,
					onKeyPress: function (cep, e, field, options) {
						var currentVal = $ctr.val();
						var cleanVal = $ctr.cleanVal();
						var cleanValLength = cleanVal.length;
						var maskInputLength = maskStr.replace(/[^0-9a-zA-Z]/g, "").length;
						if (cleanValLength > maskInputLength) {
							if ((mask3Str || '') != '' && cleanValLength > mask2Str.replace(/[^0-9a-zA-Z]/g, "").length) {
								$ctr.mask(mask3Str, options);
								if (currentVal != $ctr.val()) {
									$ctr.val($ctr.masked(cleanVal));
								}
								if ($ctr.cleanVal().length < cleanValLength) {
									$ctr.mask(mask2Str);
									if (currentVal != $ctr.val()) {
										$ctr.val($ctr.masked(cleanVal));
									}
									$ctr.mask(WWP_PadR(mask2Str, maskMaxLength, 'A'), options);
								}
							} else {
								$ctr.mask(mask2Str, options);
								if (currentVal != $ctr.val()) {
									$ctr.val($ctr.masked(cleanVal));
								}
								if ($ctr.cleanVal().length < cleanValLength) {
									$ctr.mask(maskStr);
									if (currentVal != $ctr.val()) {
										$ctr.val($ctr.masked(cleanVal));
									}
									$ctr.mask(WWP_PadR(maskStr, maskMaxLength, 'A'), options);
								} else if ((mask3Str || '') != '') {
									$ctr.mask(WWP_PadR(mask2Str, maskMaxLength, 'A'), options);
								}
							}
						} else {
							$ctr.mask(maskStr);
							if (currentVal != $ctr.val()) {
								$ctr.val($ctr.masked(cleanVal));
							}
							$ctr.mask(WWP_PadR(maskStr, maskMaxLength, 'A'), options);
						}
					}
				};
				var currentValLength = ($ctr.val() || '').replace(/[^0-9a-zA-Z]/g, "").length;
				if (currentValLength <= maskStr.replace(/[^0-9a-zA-Z]/g, "").length) {
					$ctr.mask(maskStr)
						.mask(WWP_PadR(maskStr, maskMaxLength, 'A'), options);
				} else if ((mask3Str || '') == '' || currentValLength <= mask2Str.replace(/[^0-9a-zA-Z]/g, "").length) {
					$ctr.mask(mask2Str, options);
					if ((mask3Str || '') != '') {
						$ctr.mask(WWP_PadR(mask2Str, maskMaxLength, 'A'), options);
					}
				} else {
					$ctr.mask(mask3Str, options);
				}
			} else {
				$ctr.mask(maskStr, { reverse: isReverse });
			}
			if (callGxValidControl) {
				WWP_GxValidControl(controlId);
			}
		}
	},

	//Mask_GetCleanValue: function (controlId, controlToStoreVal) {
	//	$('#' + controlToStoreVal).val($('#' + controlId).cleanVal());
	//	WWP_GxValidControl(controlToStoreVal);
	//},

	//Mask_SetCleanValue: function (controlId, val) {
	//	var $ctr = $('#' + controlId);
	//	var onKeyPress = $ctr.data('mask').options.onKeyPress;
	//	if (onKeyPress != null) {
	//		$ctr.val(val);
	//	} else {
	//		$ctr.val($ctr.masked(val));
	//	}
	//	WWP_GxValidControl(controlId);
	//}

	EmpoweredGrids_Refresh: function () {
		$(".gx-grid>table").each(function () {
			var empowerer = $(this).parent().data('WWP.Empowerer');
			if (empowerer != null
				&& empowerer.ColumnsOrHeaderFixer != null
				&& $(this).data('Empowered') == '') {
				empowerer.ColumnsOrHeaderFixer.Refresh(true);
			}
		});
	},

	Messages_RemoveControlMessage: function (controlInternalName) {
		$('#' + controlInternalName + '_Balloon').remove();
		var unprefixedClass = $('#' + controlInternalName).attr('data-gx-unprefixed-class');
		if (unprefixedClass != null && unprefixedClass != '') {
			$('#' + controlInternalName).attr('class', unprefixedClass).attr('data-gx-unprefixed-class', null);
		}
	},

	ConfirmInTransaction_AttachToButton: function (mainTableInternalName, buttonInternalName) {
		gx.fx.obs.addObserver('gx.keypress', this, function (event) {
			if (event.event.keyCode == 13) {//enter key
				var Ctrl = gx.evt.source(event.event) || gx.dom.getActiveElement() || gx.csv.lastControl;
				var triggersEvt = gx.evt.triggersEvt(Ctrl);
				if (!triggersEvt) {
					event.cancel = true;
					$('#' + buttonInternalName).click();
				}
			}
		});
	},

	ConfirmInTransaction_Confirm: function () {
		var activeElementId = $(document.activeElement).attr('id');
		if (WWP_endsWith(activeElementId, 'vDATA') && activeElementId.length > 5) {
			window.setTimeout(function () {
				gx.O.executeEnterEvent(null, null);
			}, 200);
		} else {
			gx.O.executeEnterEvent(null, null);
		}
	},

	DynamicForms_RefreshParentGrid: function (anyControlId, anyControlName) {
		var wcName = anyControlId.replace(anyControlName.toUpperCase(), '');
		var parentWCName = wcName;
		while (parentWCName.indexOf('W') >= 0) {
			parentWCName = parentWCName.substring(0, parentWCName.lastIndexOf('W'));
			var $btnToRefresh = $('#' + parentWCName + 'BTNREFRESHGRID');
			if ($btnToRefresh.length == 1) {
				$btnToRefresh.click();
				return;
			}
		}

	},

	RadioButton_SetOrientation: function (controlInternalName, isVertical) {
		var $radioSpan = $('[data-gx-for=' + controlInternalName + ']').find('>div>span');
		if (!isVertical) {
			$radioSpan.removeClass('gx-radio-button-vertical');
		}
		$radioSpan.toggleClass('btn-group-vertical', isVertical).toggleClass('btn-group', !isVertical);
		var $firstLabel = $radioSpan.find('>label:eq(0)');
		if ($firstLabel.find('>input').length == 0 || $firstLabel.find('>input').val() == '#Dummy#') {
			$firstLabel.remove();
		}
		$radioSpan.attr('wwp-processed', '');
	},

	CheckBox_SetTitle: function (controlInternalName, title) {
		var $checkLabel = $('[data-gx-checkbox-title][for=' + controlInternalName + ']');
		if ($checkLabel.length == 0) {
			//el checkbox dentro de un FS se renderiza diferente (probado en GX18 U3)
			var $checkLabelParent = $('#' + controlInternalName).parent();
			$checkLabelParent.contents().each(function () {
				if (this.nodeType == 3) {
					$(this).remove();
				}
			});
			$checkLabelParent.append(document.createTextNode(title));
		} else {
			$checkLabel.text(title).attr('data-gx-sr-only', null);
		}
	},

	Control_FixControlValueChanged: function (controlInternalName, btnToCallInternalName) {
		//algunas veces gx bug no dispara el ControlValueChanged (tested in gx18 u3, pasa con text y radio button)
		var $btnToRefresh = $('#' + btnToCallInternalName);
		if ($btnToRefresh.length == 1) {
			var $txt = $('#' + controlInternalName);
			if ($txt.length == 1) {
				$txt.on('blur', function (event) {
					$btnToRefresh.click();
				});
			} else {
				//radio button
				$txt = $('[data-gx-for=' + controlInternalName + ']').find('>div>span.gx-radio-button');
				$txt.on('click', function (event) {
					$btnToRefresh.click();
				});
			}
		}
	},

	Scroll_GoToBottom: function (containerInternalName) {
		var $container = $('#' + containerInternalName);
		if ($container.length === 1) {
			if ($container[0].scrollHeight == 0) {
				window.setTimeout(function () {
					$container[0].scrollTo(0, $container[0].scrollHeight);
				}, 200);
			} else {
				$container[0].scrollTo(0, $container[0].scrollHeight);
			}
		}
		WWP_GXEAIInfo($container);
		var $loadingIcon = $('.AIWaitingResponse.AIAutomaticTooltip');
		if ($loadingIcon.length == 1) {
			window.setTimeout(function () {
				$loadingIcon.tooltip('show');
			}, 6000);
		}
	}
};

function WWP_GXEAIInfo($container) {
	var warningText = '(warning: using GeneXus Enterprise AI for development only)';
	var $aiExamples = $container.find('.TextBlockAIExamples');
	if ($aiExamples.length == 0) {
		$($container.find('.ChatMessageCell').get().reverse()).each(function () {
			if ($(this).html().indexOf(warningText) >= 0 && $(this).find('*:not(br)').length==0) {
				$aiExamples = $(this);
				return false;
			}
		});
	}
	var aiExamplesHtml = $aiExamples.html();
	if (aiExamplesHtml != null && aiExamplesHtml.indexOf(warningText) >= 0) {
		$aiExamples.html(aiExamplesHtml.replace(warningText, '(warning: using GeneXus Enterprise AI for development only <a href="https://workwithplus.com/aipricing" target=_blank><i class="fas fa-circle-info" style="font-size: 12px;padding-left: 3px;"></i></a>)'));
	}
}

function WWP_Debug_Log(isError, msg) {
	///#DEBUG
	console.log((isError ? '[ERROR] ' : '') + msg);
	///#ENDDEBUG
}
///#DEBUG
function WWP_VV(ucs) { }
setCSSLayersOrder();
///#ENDDEBUG

/*function WWP_VV(ucs) {
	try {
		var expected, found, ucName;
		$(ucs).each(function () {
			ucName = this[0];
			expected = WWP_GV(ucName);
			found = this[1];
			if (expected != found) {
				throw new Error();
			}
		});
	} catch (err) {
		var errStack = WWP_replaceAll(err.stack, '/DVelop/Shared/WorkWithPlusCommon.js', '');
		errStack = errStack.substring(errStack.indexOf('/DVelop/'));
		errStack = errStack.substring(0, errStack.indexOf('.js') + 3);
		console.error('Incorrect version of file ' + errStack + ' (UC ' + ucName + ': ' + found + ', expected: ' + expected + ')');
	}
}
function WWP_GV(uc) {
	switch (uc) {
		case 'x':
			return 'y';
		case 'x2':
			return 'y2';
	}
}*/;setCSSLayersOrder();;function WWP_VV(n){var i,r,u,t;try{$(n).each(function(){if(u=this[0],i=WWP_GV(u),r=this[1],i!=r)throw new Error;})}catch(f){t=WWP_replaceAll(f.stack,"/DVelop/Shared/WorkWithPlusCommon.js","");t=t.substring(t.indexOf("/DVelop/"));t=t.substring(0,t.indexOf(".js")+3);console.error("Incorrect version of file "+t+" (UC "+u+": "+r+", expected: "+i+")")}};function WWP_GV(n){switch(n){case"WWP.Chronometer":return"14.3000";case"DVPaginationBar":return"14.3001";case"GXBootstrap.Tooltip":return"15.0";case"GXBootstrap.SidebarMenu":return"15.2.9";case"GXBootstrap.Panel_AL":return"14.3001";case"GXBootstrap.DDOGridTitleSettings":return"15.0.1";case"DVGroupBy":return"15.2.0";case"GXBootstrap.TagsInput":return"15.0.1";case"Bootstrap.DVProgressIndicator":return"1.1";case"GXBootstrap.Panel":return"14.0";case"DynamicWebCanvas":return"14.0";case"WWPPopover":return"15.2.8";case"DVMessage":return"15.3.5";case"GXBootstrap.DDOGridColumnsSelector":return"15.1.0";case"WorkWithPlusUtilities":return"15.3.2";case"GXBootstrap.DDOGridTitleSettingsM":return"15.2.4";case"WWP.DatePicker":return"15.3.3";case"Bootstrap.Panel":return"1.2";case"GXBootstrap.DVProgressIndicator":return"15.0.14";case"GXBootstrap.DDOExtendedCombo":return"15.3.4";case"DVImageZoom":return"14.3001";case"Bootstrap.SidebarMenu":return"15.2.9";case"GXBootstrap.DropDownOptions":return"15.2.1";case"WWP.WorkWithPlusUtilities_FAL":return"15.3.2";case"GXBootstrap.ConfirmPanel":return"15.3.4";case"Bootstrap.TabsPanel":return"1.2";case"GridTitlesCategories":return"15.2.2";case"Bootstrap.Tooltip":return"0";case"Bootstrap.ConfirmPanel":return"15.3.4";case"GXBootstrap.DDComponent":return"15.2.8";case"DVHorizontalMenu":return"15.2.12";case"Bootstrap.TagsInput":return"15.0.1";case"WWP.DateRangePicker":return"15.3.3";case"WWPCalendar":return"15.3.5";case"GXBootstrap.TabsPanel":return"14.1000";case"Bootstrap.DropDownOptions":return"14.0";case"WWP.Suggest":return"15.2.1";case"GXBootstrap.DDORegular":return"15.2.12";case"DVTabsTransform":return"15.0";case"WWP.GridEmpowerer":return"15.2.12";case"WorkWithPlusUtilities_F5":return"15.3.2";}}