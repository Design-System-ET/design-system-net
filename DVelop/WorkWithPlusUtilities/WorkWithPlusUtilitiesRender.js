var wwp_currentDDO = null;
function DVelop_WorkWithPlusUtilities() {
	this.Width;
	this.Height;
	this.EnableAutoUpdateFromDocumentTitle;
	this.EnableFixObjectFitCover;
	this.EnableFloatingLabels;
	this.EnableConvertComboToBootstrapSelect;
	this.EnableUpdateRowSelectionStatus;
	this.AutoUpdateFromDocumentTitle_Enabled;
	this.CurrentTab_ReturnUrl;

	this.show = function () {
		if (!this.initialized) {
			this.initialized = true;

			wwp.settings.columnsSelector.AllowColumnResizing = this.AllowColumnResizing;
			wwp.settings.columnsSelector.AllowColumnReordering = this.AllowColumnReordering;
			wwp.settings.columnsSelector.AllowColumnDragging = this.AllowColumnDragging;
			wwp.settings.columnsSelector.AllowColumnsRestore = this.AllowColumnsRestore;
			wwp.settings.columnsSelector.InfiniteScrolling = (this.ComboLoadType == 'InfiniteScrolling');
			wwp.settings.columnsSelector.InfiniteScrollingPage = this.InfiniteScrollingPage;
			wwp.settings.columnsSelector.RestoreColumnsIconClass = this.RestoreColumnsIconClass;

			wwp.settings.pagBar.IncludeGoTo = this.PagBarIncludeGoTo;

			wwp.labels.UpdateButtonText = this.UpdateButtonText;
			wwp.labels.AddNewOption = this.AddNewOption;
			wwp.labels.OnlySelectedValues = this.OnlySelectedValues;
			wwp.labels.MultipleValuesSeparator = this.MultipleValuesSeparator;
			wwp.labels.SelectAll = this.SelectAll;
			wwp.labels.SortASC = this.SortASC;
			wwp.labels.SortDSC = this.SortDSC;
			wwp.labels.AllowGroupText = this.AllowGroupText;
			wwp.labels.FixLeftText = this.FixLeftText;
			wwp.labels.FixRightText = this.FixRightText;
			wwp.labels.LoadingData = this.LoadingData;
			wwp.labels.CleanFilter = this.CleanFilter;
			wwp.labels.RangeFilterFrom = this.RangeFilterFrom;
			wwp.labels.RangeFilterTo = this.RangeFilterTo;
			wwp.labels.RangePickerInviteMessage = this.RangePickerInviteMessage;
			wwp.labels.NoResultsFound = this.NoResultsFound;
			wwp.labels.SearchButtonText = this.SearchButtonText;
			wwp.labels.SearchMultipleValuesButtonText = this.SearchMultipleValuesButtonText;

			wwp.labels.ColumnSelectorFixedLeftCategory = this.ColumnSelectorFixedLeftCategory;
			wwp.labels.ColumnSelectorFixedRightCategory = this.ColumnSelectorFixedRightCategory;
			wwp.labels.ColumnSelectorNotFixedCategory = this.ColumnSelectorNotFixedCategory;
			wwp.labels.ColumnSelectorNoCategoryText = this.ColumnSelectorNoCategoryText;
			wwp.labels.ColumnSelectorFixedEmpty = this.ColumnSelectorFixedEmpty;
			wwp.labels.ColumnSelectorRestoreTooltip = this.ColumnSelectorRestoreTooltip;

			wwp.labels.PagBarGoToCaption = this.PagBarGoToCaption;
			wwp.labels.PagBarGoToIconClass = this.PagBarGoToIconClass;
			wwp.labels.PagBarGoToTooltip = this.PagBarGoToTooltip;

			gx.fx.obs.addObserver("gx.onafterevent", window, function (thisC) {
				return function (e) {
					SetMinWidthTotalizers();
					if (thisC.EnableUpdateRowSelectionStatus) {
						thisC.UpdateRowSelectedStatus(true);
					}
					if (thisC.EnableFloatingLabels) {
						thisC.FloatingLabels();
					}
					if (thisC.EmpowerTabs) {
						thisC.ExecEmpowerTabs();
					}
					if (thisC.EnableFixObjectFitCover && WWP_IsIE()) {
						thisC.FixObjectFitCover();
					}
					if (thisC.EnableConvertComboToBootstrapSelect) {
						if (wwp_currentDDO != null) {
							ApplyBootstrapSelect_resetSelection(wwp_currentDDO.ddoControl, wwp_currentDDO.target);
							wwp_currentDDO = null;
						}
						ApplyBootstrapSelect();
					}
					if (thisC.IncludeLineSeparator) {
						HideInvisibleRowsWithLineSeparator();
					}
					thisC.PorcessDetailWCs();
					FixCheckboxDoubleClick();
				}
			}(this));

			var thisC = this;
			$.each(['grid.onafterrender', 'gx.endprocessing', 'grid.onafterrefresh'], function (i, gxEventName) {
				gx.fx.obs.addObserver(gxEventName, window, function (thisC) {
					return function (e) {
						if (thisC.EnableConvertComboToBootstrapSelect) {
							ApplyBootstrapSelect();
						}
						thisC.PorcessDetailWCs();
						FixCheckboxDoubleClick();
					}
				}(thisC));
			});

			gx.fx.obs.addObserver("gx.onload", window, function (thisC) {
				return function (e) {
					SetMinWidthTotalizers();
					if (thisC.EnableFloatingLabels) {
						thisC.FloatingLabels();
					}
					if (thisC.EmpowerTabs) {
						thisC.ExecEmpowerTabs();
					}
					if (thisC.EnableFixObjectFitCover && WWP_IsIE()) {
						thisC.FixObjectFitCover();
					}
					if (thisC.EnableConvertComboToBootstrapSelect) {
						ApplyBootstrapSelect();
					}
					if (thisC.IncludeLineSeparator) {
						HideInvisibleRowsWithLineSeparator();
					}
					thisC.PorcessDetailWCs();
				}
			}(this));

			gx.fx.obs.addObserver("gx.onunload", window, function (thisC) {
				return function (e) {
					thisC.initialized = false;
				}
			}(this));

			if (this.EnableUpdateRowSelectionStatus) {
				gx.fx.obs.addObserver("gx.onclick", window, function (thisC) {
					return function (e) {
						thisC.UpdateRowSelectedStatus(true);
					}
				}(this));
			}

			if (this.EnableAutoUpdateFromDocumentTitle && !this.AutoUpdateFromDocumentTitle_Enabled) {
				//Automatically update textblocks with the form caption
				this.AutoUpdateFromDocumentTitle_Enabled = true;
				var target = document.querySelector('head > title');
				var MutationObserver = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver;
				var observer = new MutationObserver(function (mutations) {
					mutations.forEach(function (mutation) {
						$('.AutoUpdateFromDocumentTitle').text(mutation.target.textContent)
					});
				});
				observer.observe(target, { subtree: true, characterData: true, childList: true });
			}
		}

		SetMinWidthTotalizers();
		if (this.EnableFloatingLabels) {
			this.FloatingLabels();
		}
		if (this.EmpowerTabs) {
			this.ExecEmpowerTabs();
		}
		if (this.EnableConvertComboToBootstrapSelect) {
			ApplyBootstrapSelect();
		}
		this.PorcessDetailWCs();

		if (window.frameElement != null && window.frameElement.urlHist != null) {
			window.frameElement.WWPUtils = this;
			if (window.frameElement.CurrentTab_ReturnUrl != null) {
				this.CurrentTab_ReturnUrl = window.frameElement.CurrentTab_ReturnUrl;
			}
		}
		if (this.IncludeLineSeparator) {
			HideInvisibleRowsWithLineSeparator();
		}
	}

	this.updateCurrentTab_ReturnUrl = function () {
		if (window.frameElement != null && window.frameElement.urlHist != null) {
			if (window.frameElement.urlHist.length > 0) {
				this.CurrentTab_ReturnUrl = window.frameElement.urlHist[window.frameElement.urlHist.length - 1];
				var currentLocation = window.frameElement.contentWindow.location + '';
				if (currentLocation.indexOf('#') >= 0) {
					var currentTab_ReturnUrlVal = this.CurrentTab_ReturnUrl;
					if (currentTab_ReturnUrlVal.indexOf('#') >= 0) {
						currentTab_ReturnUrlVal = currentTab_ReturnUrlVal.substring(0, currentTab_ReturnUrlVal.indexOf('#'));
					}
					if (WWP_startsWith(currentLocation, currentTab_ReturnUrlVal)) {
						this.CurrentTab_ReturnUrl = currentLocation;
						window.frameElement.urlHist[window.frameElement.urlHist.length - 1] = this.CurrentTab_ReturnUrl;
					}
				}
				window.frameElement.CurrentTab_ReturnUrl = this.CurrentTab_ReturnUrl;
			} else {
				window.frameElement.CurrentTab_ReturnUrl = null;
			}
		}
	}

	this.CurrentTab_Return = function () {
		if (window.frameElement != null && window.frameElement.urlHist != null && window.frameElement.urlHist.length > 1) {
			window.frameElement.urlHist.splice(window.frameElement.urlHist.length - 2, 2);
		}
	}

	this.DetailWebComponent_Close = function (tableName, doRefresh) {
		WWPActions.GridDetailWebComponent_Close(tableName, doRefresh);
	}

	this.FixObjectFitCover = function () {
		if (WWP_IsIE()) {
			$("img.ObjectFitCover").each(function (i) {
				if (this.getAttribute('src') != '') {
					$(this).css('background-image', "url('" + this.getAttribute('src') + "')");
					$(this).addClass('ObjectFitCoverFix');
					this.setAttribute('src', '');
				}
			});
		}
	}

	this.UpdateRowSelectedStatus = function (retry) {
		$(".TableWithSelectableGrid").each(function (i) {
			$(this).toggleClass('WWPRowSelected', $(this).find('tr.gx-row-selected').length > 0);
			$(this).toggleClass('WWPMultiRowWOPagingSelected', $(this).find("tr input[value='true']:not([disabled])").length > 0);
		});

		if (retry) {
			var thisC = this;
			window.setTimeout(function () { thisC.UpdateRowSelectedStatus(false); }, 100);
		}
	}

	this.PorcessDetailWCs = function () {
		var thisC = this;
		$("td.WCD_ActionColumn,td.WCD_ActionGroupColumn").each(function (i) {
			if (!$(this).hasClass("WCD_processed")) {
				$(this).addClass("WCD_processed");
				var $wcOriginalCell = $('.WCD_' + $(this).closest('table')[0].id.replace('ContainerTbl', '').toUpperCase());
				var wcId = WWP_getWCId(null, this, $wcOriginalCell);
				if (wcId != null && wcId != '') {
					$(this).on($(this).hasClass("WCD_ActionGroupColumn") ? "changed.bs.select" : "click", function (e) {
						if (e.target.tagName != 'P' && e.target.tagName != 'TD'
							&& ($(this).hasClass("WCD_ActionColumn") || e.target.options[e.target.selectedIndex].innerText.indexOf(' WCD') >= 0)) {
							if (!$(this).hasClass("WCD_Expanded") || $(this).hasClass("WCD_ActionGroupColumn") && !$(e.target.options[e.target.selectedIndex]).hasClass("WCD_Expanded")) {
								if (!$(this).parent().next().hasClass('WCD_tr')) {
									var $currentDWCExpandedCell = $(this).parent().find('>td:not(:eq(' + $(this).index() + ')).WCD_Expanded,* .WCD_Expanded');//cuando hay mas de una DWC
									$(this).addClass("WCD_Expanded");
									if ($(this).hasClass("WCD_ActionGroupColumn")) {
										$(e.target.options[e.target.selectedIndex]).addClass("WCD_Expanded");
									}
									if (WWP_startsWith(wcId, 'gxHTMLWrp')) {
										var wcIdAuxSelector = $('#' + wcId.substring(9))
										if (wcIdAuxSelector.length == 1 && wcIdAuxSelector.parent()[0].tagName == 'BODY') {
											wcIdAuxSelector.remove();
										}
									}

									var empowerer = $(this).closest('.gx-grid').data('WWP.Empowerer');
									if (empowerer != null && empowerer.ColumnsOrHeaderFixer != null) {
										empowerer.ColumnsOrHeaderFixer.WCD_PrepareToExpand(this.parentNode);
									}

									var newRow = document.createElement('tr');
									$(newRow).addClass("WCD_tr");
									this.parentNode.parentNode.insertBefore(newRow, this.parentNode.nextSibling);
									var $grid = $(this).closest('table');
									var processedRecords = $grid.data('IS_ProcessedRecords');
									if (processedRecords != null && processedRecords == $grid.find('>tbody').children().length - 1) {
										$grid.data('IS_ProcessedRecords', (processedRecords + 1));
									}

									var showLoading = true;

									var newDiv1 = document.createElement('div');

									var divSectionWCDContainer = document.createElement('div');
									$(divSectionWCDContainer).addClass("SectionWCDContainer");

									var newCol = document.createElement('td');
									newCol.colSpan = $(this).parent().find('>td:visible').length;
									newRow.appendChild(newCol);
									$(divSectionWCDContainer).css('display', 'none');

									if (!showLoading) {
										newCol.appendChild(divSectionWCDContainer);
									}

									if ($("#" + wcId).length > 0) {
										var wcElem = $("#" + wcId);
										var $wcParent = $(wcElem).parent();

										if ($wcParent.hasClass('SectionWCDContainer')) {
											if (showLoading) {
												//showLoading = false;
												//newCol.appendChild(divSectionWCDContainer);
											}
											$wcParent.slideUp(function () {
												$currentDWCExpandedCell.removeClass("WCD_Expanded");
												$(this).closest('.WCD_tr').prev().find('.WCD_ActionColumn,.WCD_ActionGroupColumn,.WCD_ActionGroupColumn .WCD_Expanded').removeClass("WCD_Expanded");
												$(this).closest('.WCD_tr').remove();
											});
										}

										var dateContainer = wcElem.detach();
										dateContainer.find('.gxwebcomponent-body').addClass('Invisible');
										dateContainer.appendTo(divSectionWCDContainer);
										dateContainer.html('');
									} else {
										var divWC = document.createElement('div');
										divWC.id = wcId;
										divSectionWCDContainer.appendChild(divWC);
									}
									if (showLoading) {
										newDiv1.innerHTML = '<div class="WCD_Loading"><i class="fas fa-spinner fa-spin"></i></div>';
										newCol.appendChild(newDiv1);
										newDiv1.appendChild(divSectionWCDContainer);
										$(newDiv1).css('display', 'none');
										$(newDiv1).slideDown(function () {
											$(divSectionWCDContainer).css('min-height', $(this).height());
										});
									}
									if (thisC.DWC_Timer != null) {
										clearInterval(thisC.DWC_Timer);
									}
									thisC.DWC_Timer = setInterval(function () {
										thisC.DetailWC_WCLoaded(showLoading, wcId);
									}, showLoading ? 3000 : 500);

									if (showLoading) {
										thisC.DetailWC_wcRenderHandler = function (e) {
											if (e.containerControl == $("#" + wcId)[0]) {
												thisC.DetailWC_WCLoaded(showLoading, wcId);
											}
										};
										gx.fx.obs.addObserver("webcom.render", window, thisC.DetailWC_wcRenderHandler);
									}
								}
							}
							else {
								e.preventDefault();
								e.stopPropagation();
								$(this).removeClass("WCD_Expanded").find('.WCD_Expanded').removeClass("WCD_Expanded");
								if ($("#" + wcId).length > 0) {
									var $wcElem = $("#" + wcId);
									var wcParent = $wcElem.parent();
									if (wcParent.hasClass('SectionWCDContainer')) {
										var thisRow = this;
										wcParent.slideUp(function () {

											$wcElem = $wcElem.detach();
											if ($wcElem.length == 1) {
												$wcElem.appendTo($wcOriginalCell[0]);
											} else {
												WWP_Debug_Log(false, 'detach div');
												$wcOriginalCell.html('<div id="' + wcId + '"></div>');
												$wcElem = $wcOriginalCell.find('>div');
											}
											$wcElem.html('');

											$(this).closest('.WCD_tr').prev().find('.WCD_ActionColumn,.WCD_ActionGroupColumn').removeClass("WCD_Expanded");
											$(this).closest('.WCD_tr').remove();
											if (typeof window['SetMinWidthPaginationBars'] === "function") {
												SetMinWidthPaginationBars();
											}

											var empowerer = $(thisRow).closest('.gx-grid').data('WWP.Empowerer');
											if (empowerer != null && empowerer.ColumnsOrHeaderFixer != null) {
												empowerer.ColumnsOrHeaderFixer.WCD_Collapsed();
												if (empowerer.ColumnsOrHeaderFixer.settings.right > 0) {
													empowerer.ColumnsOrHeaderFixer.Refresh();
												}
											}
										});
									}
								}
							}
						}
					});
				}
			}
			if ($(this).hasClass("WCD_Opened")) {
				$(this).removeClass("WCD_Opened");
				var $btnToClick = $(this).find('a, img').first();
				window.setTimeout(function () {
					$btnToClick.click();
				}, 0);
			}
		});
	}

	this.DetailWC_WCLoaded = function (showLoading, wcId) {
		if (this.DWC_Timer != null) {
			clearInterval(this.DWC_Timer);
			this.DWC_Timer = null;
		}
		if (this.DetailWC_wcRenderHandler != null || !showLoading) {
			var detailWC_wcRenderHandlerAux = this.DetailWC_wcRenderHandler;
			if (this.DetailWC_wcRenderHandler != null) {
				delete this.DetailWC_wcRenderHandler;
			}
			var thisC = this;
			window.setTimeout(function () {
				var $divSectionWCDContainer = $("#" + wcId).closest('.SectionWCDContainer');
				if (showLoading) {
					$divSectionWCDContainer.parent().find('.WCD_Loading').remove();
				}
				$divSectionWCDContainer.slideDown(function () {
					$(this).css('min-height', '');
					if (typeof window['SetMinWidthPaginationBars'] === "function") {
						SetMinWidthPaginationBars();
					}
					var empowerer = $(this).closest('.gx-grid').data('WWP.Empowerer');
					if (empowerer != null && empowerer.ColumnsOrHeaderFixer != null && empowerer.ColumnsOrHeaderFixer.settings.right > 0) {
						empowerer.ColumnsOrHeaderFixer.Refresh();
					}
				});
				if (detailWC_wcRenderHandlerAux != null) {
					gx.fx.obs.deleteObserver("webcom.render", window, detailWC_wcRenderHandlerAux);
				}
				thisC.UpdateAllTabSliders();
			}, 150);
		}
	}

	this.FloatingLabels = function () {
		var thisC = this;
		$(".AttributeFL>input:checkbox").on("click", function (i) {
			$(".AttributeFL").each(function (i) {
				thisC.FloatingLabels_AnimateLabel(this, null);
			});
			window.setTimeout(function () {
				thisC.FloatingLabels_WatchReadonlyAttributes();
			}, 100);
		});
		$(".ReadonlyAttributeFL>input:checkbox").each(function (i) {
			thisC.FloatingLabels_AnimateLabel_ToggleClases(this, true, false);
		});
		$("img.ReadonlyAttributeFL").each(function (i) {
			thisC.FloatingLabels_AnimateLabel_ToggleClases(this, true, false);
		});
		$(".AttributeFL").each(function (i) {
			if (!thisC.FloatingLabels_IsEventBound(this)) {
				$(this).on("focus blur", function (i) {
					thisC.FloatingLabels_AttributeFocused(this, i);

					//por caso de entrar a trn con att editable vacío que se pone readonly al cambiar otro att (tested en gx18 U5)
					var thisC2 = this;
					setTimeout(function () { thisC.FloatingLabels_AttributeFocused(thisC2, i); }, 200);
				});
			}
			if (WWP_IsIE() && (($(this).get(0).onblur + '' + $(this).get(0).onchange).indexOf('gx.date.') >= 0 || $(this).parent().find('.gx-image-link').length > 0)) {
				$(this).parent().parent().off("DOMSubtreeModified").on("DOMSubtreeModified", function () {
					var elem = $(this).find(".AttributeFL").get(0);
					if (elem != null && !thisC.FloatingLabels_IsEventBound(elem)) {
						$(elem).on("focus blur", function (i) {
							thisC.FloatingLabels_AttributeFocused(this, i);
						});
					}
				});
			}
			thisC.FloatingLabels_AnimateLabel(this, null);
			thisC.FloatingLabels_WatchReadonlyAttribute(this);
		});
		$("input[type=date]").each(function (i) {
			//genexus agrega un input date en XS para que sea manejado por el browser
			if (!thisC.FloatingLabels_IsEventBound(this, 'change', 'FloatingLabels_AnimateLabel')) {
				$(this).on("change", function (i) {
					var id = $(this).attr('id');
					if (WWP_endsWith(id, '-picker')) {
						var $realInput = $('#' + id.substring(0, id.length - 7));
						if ($realInput.length == 1) {
							thisC.FloatingLabels_AnimateLabel($realInput[0], null);
						}
					}
				});
			}
		});
	}
	this.ExecEmpowerTabs = function () {
		var tabsPanel = this;
		$(".gx_usercontrol.ViewTab:not(.TabEmpowered)").each(function () {
			var $tabs = $(this);

			$tabs.addClass('TabEmpowered');

			var tabsSlider = document.createElement('div');
			tabsSlider.innerHTML = '<div class="moveLeft"><div class="moveLeftArrow"></div></div><div class="moveRight"><div class="moveRightArrow"></div></div>';
			tabsSlider.className = 'tabsSlider';

			$tabs[0].insertBefore(tabsSlider, $tabs[0].children[0]);

			var ul = $tabs.find('>ul').detach();
			ul.appendTo(tabsSlider);



			var isRTL = ($('body').css('direction') == 'rtl');
			if (isRTL) {
				$(tabsSlider).addClass('Tabs_RTL');
			}
			var leftProp = isRTL ? 'right' : 'left';


			var tabContainer = ul;
			$(tabsSlider).find(">.moveRight").click(function (event) {
				var firstLiVisible = null;
				var lastLiVisible = $(tabContainer).find('>li:last-child()');
				var lastLiVisibleRight = WWP_TabsLIPositionRight(isRTL, lastLiVisible);
				var newFirstLiVisible = $(tabContainer).find('>li:first-child()');
				var sliderWidth = $(tabsSlider).width();
				var hasRightActions = WWP_IsTabsWithRightActions($(tabsSlider).parent());
				if (hasRightActions) {
					sliderWidth -= $(this).outerWidth();
				}
				var currentLeft = parseInt($(tabContainer).css(leftProp), 10);
				var totalLis = $(tabContainer).find('>li').length;
				$(tabContainer).find('>li').each(function () {
					if (firstLiVisible == null && WWP_TabsLIPositionLeft(isRTL, $(this)) >= currentLeft * -1) {
						//primer LI que esta visible
						firstLiVisible = $(this);
					}
					if (tabsPanel.ForceShowSelectedLI && $(this).hasClass('active')) {
						//se fuerza que se muestre el seleccionado como primero
						newFirstLiVisible = $(this);
						return false;
					}
					else if (!tabsPanel.ForceShowSelectedLI && (sliderWidth - currentLeft - (!hasRightActions && $(this).index() < totalLis - 1 ? $(tabsSlider).find(">.moveRight").outerWidth() : 0) < WWP_TabsLIPositionRight(isRTL, $(this)))) {
						//first menu option that was not visible entirely
						newFirstLiVisible = $(this);
						return false;
					}
					else if (sliderWidth - $(tabsSlider).find(">.moveLeft").outerWidth() >= lastLiVisibleRight - WWP_TabsLIPositionLeft(isRTL, $(this))) {
						//if this option is positioned to the left, all remaining options are shown
						newFirstLiVisible = $(this);
						return false;
					}
				});
				if (newFirstLiVisible[0] == firstLiVisible[0] && newFirstLiVisible.next().length == 1) {
					//no se va a mover (por ejemplo el caso en que un solo tab ocupa todo el ancho disponible) -> forzar que se muestre el siguiente
					newFirstLiVisible = newFirstLiVisible.next();
				}
				tabsPanel.SliderMoveTo(tabContainer, tabsSlider, newFirstLiVisible, isRTL);
			});
			$(tabsSlider).find(">.moveLeft").click(function (event) {
				if (tabsPanel.ForceShowSelectedLI) {
					var newFirstLiVisible = $(tabContainer).find('>li.active');
					var lastLiVisible = $(tabContainer).find('>li:last-child()');
					if (newFirstLiVisible[0] == lastLiVisible[0]) {
						//caso donde se esta viendo solo uno y se elimina -> se mueve a la izquierda
						//para que se vea el ultimo (que es el seleccionado) pero tambien todos los que se puedan
						var lastLiVisibleRight = WWP_TabsLIPositionRight(isRTL, lastLiVisible);
						var sliderWidth = $(tabsSlider).width();
						$(tabContainer).find('>li').each(function () {
							if (sliderWidth - $(tabsSlider).find(">.moveLeft").outerWidth() >= lastLiVisibleRight - WWP_TabsLIPositionLeft(isRTL, $(this))) {
								//if this option is positioned to the left, all remaining options are shown
								newFirstLiVisible = $(this);
								return false;
							}
						});
					}
					tabsPanel.SliderMoveTo(tabContainer, tabsSlider, newFirstLiVisible, isRTL);
				} else {
					var firstLiVisible = $(tabContainer).find('>li:first-child()');
					var sliderWidth = $(tabsSlider).width();
					var hasRightActions = WWP_IsTabsWithRightActions($(tabsSlider).parent());
					if (hasRightActions) {
						sliderWidth -= $(tabsSlider).find(">.moveRight").outerWidth();
					}
					var currentLeft = parseInt($(tabContainer).css(leftProp), 10);
					$(tabContainer).find('>li').each(function () {
						if (WWP_TabsLIPositionLeft(isRTL, $(this)) >= currentLeft * -1) {
							//primer LI que esta visible
							firstLiVisible = $(this);
							return false;
						}
					});
					if (firstLiVisible != null) {
						var newFirstLiVisible = $(tabContainer).find('>li:first-child()');
						var totalLis = $(tabContainer).find('>li').length;
						$(tabContainer).find('>li').each(function () {
							if (WWP_TabsLIPositionLeft(isRTL, $(this)) + sliderWidth - ($(this).index() > 0 ? $(tabsSlider).find(">.moveLeft").outerWidth() : 0) - (!hasRightActions && $(this).index() < totalLis - 1 ? $(tabsSlider).find(">.moveRight").outerWidth() : 0) > WWP_TabsLIPositionLeft(isRTL, firstLiVisible)) {
								//primer LI que de ponerlo visible hace que el firstLiVisible se comience a ver y quede cortado
								newFirstLiVisible = $(this);
								return false;
							}
						});
						if (newFirstLiVisible[0] == firstLiVisible[0] && newFirstLiVisible.prev().length == 1) {
							//no se va a mover (por ejemplo el caso en que un solo tab ocupa todo el ancho disponible) -> forzar que se muestre el anterior
							newFirstLiVisible = newFirstLiVisible.prev();
						}
						tabsPanel.SliderMoveTo(tabContainer, tabsSlider, newFirstLiVisible, isRTL);
					}
				}
			});
			$(tabContainer).on("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
				tabsPanel.UpdateSlider(tabContainer);
			});
			tabsPanel.UpdateSlider(tabContainer);
		});

		if (!this.TabsEmpowererEvents) {
			this.TabsEmpowererEvents = true;
			var thisC = this;
			$(window).resize(function () {
				thisC.UpdateAllTabSliders();
			});
			gx.fx.obs.addObserver("gx.onload", window, function (thisC) {
				return function (e) {
					thisC.UpdateAllTabSliders();
				}
			}(this));
			$(window).on('load', function () {
				thisC.UpdateAllTabSliders();
			});
			gx.fx.obs.addObserver("gx.onafterevent", window, function (thisC) {
				return function (e) {
					thisC.UpdateAllTabSliders();
				}
			}(this));
		}
	}
	function WWP_TabsLIPositionLeft(isRTL, $elem) {
		if (isRTL) {
			if ($elem.index() == 0) {
				return 0;
			} else {
				return $elem.parent().width() - ($elem.position().left + $elem.width());
			}
		} else {
			return $elem.position().left;
		}
	}
	function WWP_TabsLIPositionRight(isRTL, $elem) {
		if (isRTL) {
			return $elem.parent().width() - $elem.position().left;
		} else {
			return $elem.position().left + $elem.width();
		}
	}

	function WWP_IsTabsWithRightActions($tabsContainer) {
		return $tabsContainer.hasClass('TabsWithRightActions') || $tabsContainer.parent().parent().parent().parent().hasClass('TabsWithRightActions');
	}

	this.SliderMoveTo = function (tabContainer, tabsSlider, newFirstLiVisible, isRTL) {
		this.ForceShowSelectedLI = false;
		var newLeft = WWP_TabsLIPositionLeft(isRTL, newFirstLiVisible) * -1;
		if (newLeft < 0) {
			newLeft += $(tabsSlider).find(">.moveLeft").width();
		}
		var $horizontalMenuSilder = $(tabContainer).parent();
		var horizontalMenuSilderWidth = $horizontalMenuSilder.width();
		var hasRightActions = WWP_IsTabsWithRightActions($horizontalMenuSilder.parent());
		if (hasRightActions) {
			horizontalMenuSilderWidth -= $horizontalMenuSilder.find('>.moveRight').outerWidth();
		}
		$(tabsSlider).toggleClass('NeedSliderLeft', newLeft < 0);
		if (!hasRightActions || !$(tabsSlider).hasClass('NeedSliderRight')) {
			$(tabsSlider).toggleClass('NeedSliderRight', horizontalMenuSilderWidth - newLeft < WWP_TabsLIPositionRight(isRTL, $(tabContainer).find('>li:last-child()')));
		}
		$(tabContainer).css(isRTL ? 'right' : 'left', newLeft + 'px');
	}

	this.UpdateAllTabSliders = function () {
		var thisC = this;
		$(".gx_usercontrol.ViewTab.TabEmpowered").each(function () {
			thisC.UpdateSlider($(this).find('>.tabsSlider>ul')[0]);
		});
	}

	this.UpdateSlider = function (tabContainer, retrying) {
		var isRTL = ($('body').css('direction') == 'rtl');
		var menuLeft = parseInt($(tabContainer).css(isRTL ? 'right' : 'left'), 10);
		var $horizontalMenuSilder = $(tabContainer).parent();
		var hasTabs = $(tabContainer).find('>li').length > 0;
		if (hasTabs) {
			var horizontalMenuSilderWidth = $horizontalMenuSilder.width();
			if (WWP_IsTabsWithRightActions($horizontalMenuSilder.parent())) {
				horizontalMenuSilderWidth -= $horizontalMenuSilder.find('>.moveRight').outerWidth();
			}
			if ($horizontalMenuSilder.length > 0) {
				$horizontalMenuSilder
					.toggleClass('NeedSliderRight', hasTabs && (horizontalMenuSilderWidth - menuLeft < WWP_TabsLIPositionRight(isRTL, $(tabContainer).find('>li:last-child()'))))
					.toggleClass('NeedSliderLeft', hasTabs && menuLeft < 0);
			}

			if (this.ForceShowSelectedLI) {
				var $selectedLi = $(tabContainer).find('>li.active');
				if (horizontalMenuSilderWidth - menuLeft < WWP_TabsLIPositionRight(isRTL, $selectedLi)) {
					//hidden (on the right side)
					$(tabContainer).parent().find(">.moveRight").click();
				} else if (menuLeft < WWP_TabsLIPositionLeft(isRTL, $selectedLi) * -1) {
					//hidden (on the left side)
					$(tabContainer).parent().find(">.moveLeft").click();
				} else {
					this.ForceShowSelectedLI = false;
				}
			}
			if (retrying == null && $(tabContainer).css('position') == 'absolute' && $(tabContainer).closest('.entering').length == 1) {
				var thisC = this;
				//soluciona caso de split screen al recargar registro
				setTimeout(function () { thisC.UpdateSlider(tabContainer, true); }, 1);
			}
		} else {
			$horizontalMenuSilder.toggleClass('NeedSliderRight', false);
			$horizontalMenuSilder.toggleClass('NeedSliderLeft', false);
		}
	}

	this.FloatingLabels_IsEventBound = function (elem, type, functionToSearch) {
		if (type == null) {
			type = 'focus';
		}
		if (functionToSearch == null) {
			functionToSearch = 'FloatingLabels_AttributeFocused';
		}
		var datas = jQuery._data(elem, 'events');
		if (datas != null) {
			var data = jQuery._data(elem, 'events')[type];

			if (data === undefined || data.length === 0) {
				return false;
			}
			for (var i = 0; i < data.length; i++) {
				if ((data[i].handler + ' ').indexOf(functionToSearch) >= 0) {
					return true;
				}
			}
		}

		return false;
	}

	this.FloatingLabels_AttributeFocused = function (elem, i) {
		var thisC = this;

		if ("focus" === i.type) {
			$(".AttributeFL").each(function (i) {
				thisC.FloatingLabels_AnimateLabel(this, null);
			});
		}
		else {
			this.FloatingLabels_WatchReadonlyAttributes();
		}
		this.FloatingLabels_AnimateLabel(elem, i);
	}

	this.FloatingLabels_WatchReadonlyAttributes = function () {
		var thisC = this;
		$(".ReadonlyAttributeFL,span.AttributeFL, .Readonlyform-control.AttributeFL").each(function (i) {
			var elem = $(this).closest(".form-group").find(".AttributeFL:not(.Readonlyform-control)").get(0);
			if (elem != null) {
				thisC.FloatingLabels_WatchReadonlyAttribute(elem);
			}
		});
	}

	this.FloatingLabels_WatchReadonlyAttribute = function (inputElem) {
		var $elemToObserve = $(inputElem).closest(".form-group").find(".ReadonlyAttributeFL, span.AttributeFL, .Readonlyform-control.AttributeFL").parent();
		if ($elemToObserve.length == 1 && $elemToObserve.data('wwp-mut-obs') == null) {
			var thisC = this;
			var MutationObserver = window.MutationObserver || window.WebKitMutationObserver;
			var myObserver = new MutationObserver(function (mutationList, obs) {
				var elem = $elemToObserve.closest(".form-group").find(".AttributeFL:not(.Readonlyform-control)").get(0);
				if (elem != null) {
					if ($(elem.parentNode).find('span').length == 0) {
						setTimeout(function () { thisC.FloatingLabels_AnimateLabel(elem, null); }, 200);
					} else {
						thisC.FloatingLabels_AnimateLabel(elem, null);
					}
				}
				if ($elemToObserve[0].tagName != 'P') {
					var myObserver2 = $elemToObserve.data('wwp-mut-obs');
					if (myObserver2 != null) {
						myObserver2.disconnect();
						$elemToObserve.data('wwp-mut-obs', null);
					}
					var $elemToObserve2 = $elemToObserve.find(".ReadonlyAttributeFL, span.AttributeFL, .Readonlyform-control.AttributeFL").parent();
					if ($elemToObserve2.length == 1 && $elemToObserve2.data('wwp-mut-obs') == null) {
						myObserver2 = new MutationObserver(function (mutationList, obs) {
							var elem = $elemToObserve2.closest(".form-group").find(".AttributeFL:not(.Readonlyform-control)").get(0);
							if (elem != null) {
								thisC.FloatingLabels_AnimateLabel(elem, null);
							}
						});
						$elemToObserve2.data('wwp-mut-obs', myObserver2);
						myObserver2.observe($elemToObserve2[0], { subtree: true, attributes: true });
					}
				}
			});
			$elemToObserve.data('wwp-mut-obs', myObserver);
			myObserver.observe($elemToObserve[0], { subtree: true, attributes: true });
		}
	}

	this.FloatingLabels_AnimateLabel = function (elem, i) {
		if (elem.tagName == 'SPAN' && elem.className.indexOf('Readonly') != 0) {
			elem = $(elem).closest('.gx-attribute').find('.form-control')[0];
			if (elem == null) {
				return;
			}
		}
		var controlValue;
		var isDate = ($(elem).get(0).onblur + '' + $(elem).get(0).onchange).indexOf('gx.date.') >= 0;
		if (isDate) {
			controlValue = $(elem).val().replace('/', '').replace('/', '').trim();
		}
		else {
			if (elem.tagName == 'SELECT') {
				controlValue = $(elem).find('option:selected').text();
			}
			else {
				controlValue = elem.value;
				if (controlValue == '0' && ($(elem).get(0).onblur + '' + $(elem).get(0).onchange).indexOf('gx.num.') >= 0) {
					controlValue = '';
				}
			}
		}
		var addReadOnly = false;
		if (!$(elem.parentNode).is(":visible")) {
			addReadOnly = ($(elem).css('display') == 'none') && $($(elem).closest(".gx-attribute").find('span')).css('display') != 'none';
		}
		else {
			if ($(elem).hasClass('ReadonlyExtendedCombo')) {
				addReadOnly = true;
			} else if ((!$(elem).is(":visible"))) {
				if ($(elem).closest(".gx-attribute").find('span').is(":visible")) {
					addReadOnly = true;
				} else {
					var isMergeDataCell = $(elem).closest(".gx-attribute").length == 1
						&& $($(elem).closest(".gx-attribute").get(0).parentNode).hasClass('MergeDataCell')
						&& $($(elem).closest(".gx-attribute").get(0).parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).hasClass('MergeLabelCell');
					if (isMergeDataCell && $($(elem).closest(".gx-attribute").get(0).parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).find('span').is(":visible")) {
						addReadOnly = true;
					}
				}
			}
		}
		addReadOnly = addReadOnly && (elem.id == null || !elem.id.match(/_GXI$/));
		var addEmptyVal = !addReadOnly
			&& elem.type != 'checkbox'
			&& (elem.tagName != 'SPAN' || $(elem).find('>input[type=checkbox]').length == 0)
			&& elem.type != 'file'
			&& elem.type != 'textarea'
			&& elem.tagName != 'IMG'
			&& !$(elem).hasClass('gx-radio-button')
			&& elem.tagName != 'LABEL'
			&& (elem.id == null || !elem.id.match(/_GXI$/))
			&& !(i != null && "focus" === i.type || controlValue != null && controlValue.length > 0 || $(elem).is(':focus'));
		this.FloatingLabels_AnimateLabel_ToggleClases(elem, addReadOnly, addEmptyVal);
	}

	this.FloatingLabels_AnimateLabel_ToggleClases = function (elem, addReadOnly, addEmptyVal) {
		if ($(elem).hasClass('ReadonlyExtendedCombo')) {
			var isMergeDataCell = $(elem.parentNode.parentNode).hasClass('MergeDataCell')
				&& $(elem.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).hasClass('MergeLabelCell');
			if (isMergeDataCell) {
				$(elem.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).addClass("FloatingLabelReadonly").find('span.Label').addClass('AttributeFLLabel').addClass('control-label');
				$(elem.parentNode.parentNode.parentNode.parentNode.parentNode).addClass("TableMergedReadonlyExtendedCombo");
			} else {
				if ($(elem.parentNode.parentNode.previousSibling).hasClass('MergeLabelCell')) {
					$(elem.parentNode.parentNode.previousSibling).addClass("FloatingLabelReadonly").find('span.Label').addClass('AttributeFLLabel').addClass('control-label');
				}
			}

		} else if ($(elem).hasClass('ExtendedCombo')) {
			var isMergeDataCell = $(elem.parentNode).hasClass('MergeDataCell')
				&& $(elem.parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).hasClass('MergeLabelCell');
			if (isMergeDataCell) {
				$(elem.parentNode.parentNode.parentNode.parentNode).addClass("TableMergedExtendedCombo");
			}

		} else {
			var isMergeDataCell = $(elem).closest(".gx-attribute").length == 1
				&& $($(elem).closest(".gx-attribute").get(0).parentNode).hasClass('MergeDataCell')
				&& $($(elem).closest(".gx-attribute").get(0).parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).hasClass('MergeLabelCell');
			if (isMergeDataCell) {
				var gxAttElem = $(elem).closest(".gx-attribute").get(0);
				$(gxAttElem.parentNode).toggleClass("FloatingLabelEmpty", addEmptyVal);
				$(gxAttElem.parentNode).toggleClass("FloatingLabelReadonly", addReadOnly);
				$(gxAttElem.parentNode.parentNode.parentNode.parentNode.parentNode.previousSibling).toggleClass("FloatingLabelEmpty", addEmptyVal).toggleClass("FloatingLabelReadonly", addReadOnly).find('span.Label').addClass('AttributeFLLabel').addClass('control-label');
			}
			else {
				$(elem).closest(".form-group").toggleClass("FloatingLabelEmpty", addEmptyVal).toggleClass("FloatingLabelReadonly", addReadOnly);
			}

			//ocultar/mostrar placeholder
			if (addEmptyVal) {
				var placeholderVal = $(elem).attr('placeholder');
				if (placeholderVal != null && placeholderVal != '') {
					$(elem).attr('origPH', placeholderVal);
					$(elem).attr('placeholder', '');
				}
			} else {
				var origPH = $(elem).attr('origPH');
				if (origPH != null && origPH != '') {
					$(elem).attr('placeholder', origPH);
				}
			}
		}
	}
}

//ApplyBootstrapSelect - Start
function ApplyBootstrapSelect() {
	$('select.ConvertToDDO:not(.selectpicker_DV)').each(function (g, h) {
		var c = $(h);
		c.val(0);

		c.addClass("selectpicker_DV").selectpicker_DV({
			noneSelectedText: "",
			showContent: false,
			dropupAuto: false,
			size: false
		});

		c.on('show.bs.select', function (e, clickedIndex, isSelected, previousValue) {
			$(this).focus();
		});

		ApplyBootstrapSelect_updateDDO(h);

		c = c.parent();
		c.on("changed.bs.select", function (j, i, l, k) {
			if ($(this).find('>select').hasClass('selectpicker_DV')) {
				$(this).css('display', 'none');
				wwp_currentDDO = {
					'ddoControl': this,
					'target': j.target
				};
			}
		});
		c.on('hidden.bs.dropdown', function (e) {
			var tdItem = $(this).closest('td');
			if (tdItem.length == 1 && tdItem.css('z-index') == parseInt(tdItem.css('z-index'))) {
				tdItem.css('z-index', parseInt(tdItem.css('z-index')) - 100);
			}
			ApplyBootstrapSelect_setSectionGridMinHeight(e.target, false);
		});
		c.on('shown.bs.dropdown', function (e) {
			if ($(this).parent().find('li').length == 0) {
				//fix bug al hacer delete, cancel y rapido intentar abrir un action group del grid nuevamente (testeado en gx18 U5)
				$(this).removeClass('open');
				return;
			}
			ApplyBootstrapSelect_updateDDOOptions(h);
			var tdItem = $(this).closest('td');
			if (tdItem.length == 1 && tdItem.css('z-index') == parseInt(tdItem.css('z-index'))) {
				tdItem.css('z-index', parseInt(tdItem.css('z-index')) + 100);
			}
			WWP_dropdownAutoPosition(e.target, $(e.target).find('.dropdown-menu')[0], $(e.target).find("button")[0])
			ApplyBootstrapSelect_setSectionGridMinHeight(e.target, true);
			var gxCmpContext = gx != null && gx.O != null ? gx.O.CmpContext : null;
			if (gxCmpContext != null) {
				//fix por bug: abrir AI assistant y sin cerrar click en AG de grid -> Update. esto muestra error de JS y no funciona (tested en Gx gx18 u10)
				window.setTimeout(function () {
					var gxCmpContext2 = gx != null && gx.O != null ? gx.O.CmpContext : null;
					if (gxCmpContext2 != null && gxCmpContext != gxCmpContext2) {
						$(document.activeElement).prev().focus();
					}
				}, 0);
			}
		});
	})
}
function ApplyBootstrapSelect_resetSelection(ddoControl, target) {
	$(target).val(0);
	$(target).selectpicker_DV("refresh");
	ApplyBootstrapSelect_updateDDO(target);
	$(ddoControl).css('display', '');
}
function ApplyBootstrapSelect_updateDDO(h) {
	var c = $(h).parent();
	var btn = c.find("button").get(0);
	var btnCaptionSpan = c.find("button > div > div > div").get(0);
	if (btnCaptionSpan != null && btnCaptionSpan.innerText.indexOf(';') >= 0) {
		if (btnCaptionSpan.parentNode.childNodes[0].tagName != 'I') {
			$("<i></i>").addClass(btnCaptionSpan.innerText.substring(btnCaptionSpan.innerText.indexOf(';') + 1)).insertBefore(btnCaptionSpan);
		}
		btnCaptionSpan.innerText = btnCaptionSpan.innerText.substring(0, btnCaptionSpan.innerText.indexOf(';'));
	}
	btn.title = '';
}
function ApplyBootstrapSelect_updateDDOOptions(h) {
	var c = $(h).parent();
	c.find("li").each(function (g2, h2) {
		if ($(h2).index() == 0) {
			$(h2).css({ display: 'none' });
		} else {
			var data1 = h2.childNodes[0];
			if (data1.childNodes[0].tagName != 'I' && data1.childNodes[0].innerText.indexOf(';') >= 0) {
				var iClassName = data1.childNodes[0].innerText.substring(data1.childNodes[0].innerText.indexOf(';') + 1);
				data1.childNodes[0].innerText = data1.childNodes[0].innerText.substring(0, data1.childNodes[0].innerText.indexOf(';'));
				if (iClassName != ' WCD') {
					var data1_i = document.createElement('i');
					data1_i.className = iClassName;
					data1.insertBefore(data1_i, data1.childNodes[0]);
				}
			}
		}
	});
}
function ApplyBootstrapSelect_setSectionGridMinHeight(ddo, setMinHeight) {
	if (!$(ddo).hasClass('dropup')) {
		var ul = $(ddo).find('.dropdown-menu > div > ul').get(0);
		var overflowEl = ul.parentElement;
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
				var minH = $(ul).offset().top + $(ul).outerHeight() + 12 - $(overflowEl).offset().top;//12 = shadow
				$(overflowEl).css({ 'min-height': minH + "px" });
				if (hasScroll($(overflowEl), false)) {
					//has horizontal scroll visible
					minH += getScrollBarWidth();
					$(overflowEl).css({ 'min-height': minH + "px" });
				}
			}
			else {
				$(overflowEl).css({ 'min-height': "" });
			}
		}
	}
	var td = $(ddo).closest('td');
	if (td.css('position') == 'relative') {
		if (setMinHeight) {
			if (td.data('zIndexIncresed') == null && parseInt(td.css('z-index')) > 0) {
				td.css('z-index', (parseInt(td.css('z-index')) + 10));
				td.data('zIndexIncresed', 'T');
			}
		}
		else {
			if (td.data('zIndexIncresed') == 'T') {
				td.css('z-index', (parseInt(td.css('z-index')) - 10));
				td.data('zIndexIncresed', null);
			}
		}
	}
}
//ApplyBootstrapSelect - End

function SetMinWidthTotalizers() {
	$("table.GridWithTotalizer").each(function () {
		var idGrid = $(this).attr('id');
		var tableTotalizerId = idGrid.substring(0, idGrid.indexOf("ContainerTbl")).toUpperCase() + "TABLETOTALIZER";
		if ($("#" + tableTotalizerId).length > 0 && $(this).data('wwpTotRowAdded') == null) {
			$(this).data('wwpTotRowAdded', 'T');
			var totalizerTable = $("#" + tableTotalizerId);
			totalizerTable.css({ display: 'none' });
			if ($(this).css('display') != 'none' && $(this).find('>tbody>tr').length > 0) {

				var newRow = document.createElement('tr');
				newRow.className = $(this).find('>tbody>tr:not(.DVGroupByRow):eq(0)')[0].className;
				$(newRow).addClass('TotalizerRow');
				newRow.style = $(this).find('>tbody>tr:not(.DVGroupByRow):eq(0)')[0].style;
				var copyBackgroundColor = (!totalizerTable.parent().parent().hasClass('Invisible'));//backwards compatibility
				var hasTotData = false;
				var rowConditionalFormattingClass = null;
				$(this).find('>tbody>tr:not(.DVGroupByRow):eq(0)>td').each(function () {
					var newCol = document.createElement('td');
					newCol.className = this.className;
					if (rowConditionalFormattingClass == null && newCol.className.indexOf('FirstColumn') > 0) {
						rowConditionalFormattingClass = newCol.className.substring(newCol.className.lastIndexOf(' ') + 1);
						rowConditionalFormattingClass = WWP_replaceAll(rowConditionalFormattingClass, 'FirstColumn', '');
					}
					if (rowConditionalFormattingClass != null) {
						$(newCol).removeClass(rowConditionalFormattingClass + 'FirstColumn');
						$(newCol).removeClass(rowConditionalFormattingClass);
					} else if (newCol.className.indexOf('SingleCell') > 0) {
						//cell with conditional formatting
						newCol.className = newCol.className.substring(0, newCol.className.lastIndexOf(' '));
						newCol.className = newCol.className.substring(0, newCol.className.lastIndexOf(' '));
					}
					newCol.style.display = this.style.display;
					if (copyBackgroundColor) {
						$(newCol).css('background-color', totalizerTable.css('background-color'));
					}
					if ($('body').css('direction') == 'rtl') {
						$(newCol).css('text-align', ($(this).css('text-align') == 'left' ? "right" : "left"));
					} else {
						$(newCol).css('text-align', $(this).css('text-align'));
					}
					$(newCol).append(totalizerTable.find("td:eq(" + $(this).index() + ")>*").clone());
					$(newCol).find('[id]').removeAttr('id').removeAttr('name');
					newRow.appendChild(newCol);
					hasTotData = hasTotData || $(newCol).css('display') != 'none' && newCol.innerHTML != '';
				});

				if (hasTotData) {
					$(this).find('>tfoot').prepend(newRow);
				}
				$(this).parent().toggleClass('TotalizerFooterVisible', hasTotData);
			}
		}
	});
	$("table.TableTotalizerAl").each(function () {
		var prevRow = $(this).closest('.row').prev();
		if (prevRow.length == 1 && prevRow.find('>div>div').length == 1 && prevRow.find('>div>div').children().length == 0) {
			//Grid not loaded yet
			window.setTimeout(function () {
				SetMinWidthTotalizers();
			}, 100);
		}
	});
}

function FixCheckboxDoubleClick() {
	if (!(/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) || (/edge/.test(navigator.userAgent.toLowerCase()))) {
		$('html').toggleClass('notChrome', true);
	}

	$('.gx-grid>table>tbody>tr>td>label>input:checkbox:not(.dblClickFix)').each(function () {
		$(this).addClass('dblClickFix');
		if (this.onclick == null) {
			this.disabledAux = 0;
			//TODO: por pruebas hechas con ejemplo de JP esto parecería dejar nu memory leak (no tengo claro si es por problema de GX o nuestro)
			$(this).on('click', function (event) {
				if (this.value + '' == this.checked + '' && WWP_IsIE()) {
					return false;
				}
				this.disabledAux++;
				var ret = (this.disabledAux == 1);
				var thisC = this;
				window.setTimeout(function () { thisC.disabledAux--; }, 200);
				return ret;
			});
		}
	});

	$('.FixingTopInvisible>*').attr('tabindex', '-1');
}

HideInvisibleRowsWithLineSeparator = function () {
	$(".TableWithLineSeparator > div.row").each(function () {
		$(this).removeClass("FixLineSeparatorRow");
		if ($(this).is(':visible') && $(this).outerHeight(false) <= 1) {
			$(this).addClass("FixLineSeparatorRow");
		}
	});
	$(".DF_LineSeparator").each(function () {
		$(this).removeClass("FixLineSeparatorRow");
		if ($(this).is(':visible') && $(this).outerHeight(false) <= 1) {
			$(this).addClass("FixLineSeparatorRow");
		}
	});
}

$(window).on('load', function () {
	SetMinWidthTotalizers();
	FixCheckboxDoubleClick();
});
;$(window).one('load',function(){WWP_VV([['WorkWithPlusUtilities','15.3.2'],['WWP.WorkWithPlusUtilities_FAL','15.3.2'],['WorkWithPlusUtilities_F5','15.3.2']]);});