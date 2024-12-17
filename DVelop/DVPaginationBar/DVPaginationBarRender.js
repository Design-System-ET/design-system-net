function DVelop_DVPaginationBar() {
	this.Class;
	this.EmptyGridClass;

	this.ShowFirst = true;
	this.ShowPrevious;
	this.ShowNext;
	this.ShowLast = true;
	this.PagesToShow;

	this.PagingButtonsPosition;
	this.PagingCaptionPosition;
	this.RowsPerPageOptions;

	//Data bindings
	this.CurrentPage = null;
	this.PageCount = null;
	this.RecordCount = null;
	this.Page = null;

	this.SelectedPage;
	this.RowsPerPageSelectedValue;

	//Labels
	this.First = 'First';
	this.Previous;
	this.Next;
	this.Last = 'Last';
	this.Caption;
	this.EmptyGridCaption;
	this.RowsPerPageCaption;

	this.eventsAttached = false;

	this.show = function () {
		if (this.my_paging == undefined) {

			var mainDivId = this.ContainerName + "_DVPaginationBar";
			this.setHtml('<div id="' + mainDivId + '" ></div>');

			this.my_paging = new DVPaginationBar(this);
		}
		if (!(this.CurrentPage == 'NAN' || this.PageCount == 'NAN' || this.RecordCount == 'NAN' || this.Page == 'NAN')) {
			this.my_paging.render();
		}

		if (!this.eventsAttached) {
			this.eventsAttached = true;
			gx.fx.obs.addObserver("gx.onafterevent", window, function (thisC) {
				return function (e) {
					thisC.SetMinWidthPaginationBar();
				}
			}(this));

			gx.fx.obs.addObserver("gx.onload", window, function (thisC) {
				return function (e) {
					thisC.SetMinWidthPaginationBar();
				}
			}(this));
		}

		this.SetMinWidthPaginationBar();
	}

	this.SetCurrentPage = function (data) {
		var oldCurrentPage = this.CurrentPage;
		this.CurrentPage = parseInt(data);
		if (isNaN(this.CurrentPage)) {
			this.CurrentPage = 'NAN';
		}
		if (oldCurrentPage != this.CurrentPage) {
			this.show();
		}
	}

	this.SetMinWidthPaginationBar = function () {
		if ($('#' + this.ContainerName).length > 0) {
			var index = this.ContainerName.indexOf('PAGINATIONBAR');
			if (index > 0) {
				$('#' + this.ContainerName.substring(0, index) + 'TABLEWITHPAGINATIONBAR').find('table.GridWithPaginationBar').each(function () {
					SetMinWidthPaginationBar(this);
					var thisC = this;
					window.setTimeout(function () {
						//sometimes the grid changes width because grid titles are added
						SetMinWidthPaginationBar(thisC);
					}, 100);
				});
			}
		}
	}

	this.GetCurrentPage = function () {
	}

	this.SetPageCount = function (data) {
		var oldPageCount = this.PageCount;
		var pageCount = parseInt(data);
		if (isNaN(pageCount)) {
			if (this.PageCount != null && this.PageCount != -1) {
				this.PageCount = 'NAN';
			}
		}
		else {
			this.PageCount = pageCount;
		}
		if (oldPageCount != this.PageCount) {
			this.show();
		}
	}

	this.GetPageCount = function () {
	}

	this.SetRecordCount = function (data) {
		var oldRecordCount = this.RecordCount;
		this.RecordCount = parseInt(data);
		if (isNaN(this.RecordCount)) {
			this.RecordCount = 'NAN';
		}
		this.UpdatePageCount();
		if (oldRecordCount != this.RecordCount) {
			this.show();
		}
	}

	this.GetRecordCount = function () {
	}

	this.SetPage = function (data) {
		var oldPage = this.Page;
		this.Page = parseInt(data);
		if (isNaN(this.Page)) {
			this.Page = 'NAN';
		}
		this.UpdatePageCount();
		if (oldPage != this.Page) {
			this.show();
		}
	}

	this.GetPage = function () {
	}

	this.SetAppliedFilters = function (data) {
		this.AppliedFiltersDsc = data;
	}

	this.GetAppliedFilters = function () {
	}

	this.UpdatePageCount = function () {
		if (this.RecordCount > 0 && this.Page > 0) {
			this.PageCount = Math.ceil(this.RecordCount / this.Page);
		} else if (this.Page == 0) {
			this.PageCount = (this.RecordCount == 0 ? 0 : 1);
		}
	}
}

function DVPaginationBar(userControl) {

	this.control = userControl;

	this.render = function () {
		var mainDivId = this.control.ContainerName + "_DVPaginationBar";

		var mainDiv = $("#" + mainDivId);
		$(mainDiv).removeClass();
		mainDiv.html('');

		if (this.control.PageCount === 0 || this.control.RecordCount === 0 || this.control.CurrentPage == 0) {
			mainDiv.parent().parent().parent().parent().toggleClass(this.control.Class + 'EmptyGrid', true);
			if (this.control.EmptyGridCaption != '') {
				mainDiv.addClass(this.control.EmptyGridClass);

				var pagingCaptionDiv = document.createElement('div');
				$(pagingCaptionDiv).addClass('PaginationBarCaption');
				if (this.control.AppliedFiltersDsc != null && this.control.AppliedFiltersDsc != '') {
					$(pagingCaptionDiv).html('<div></div><div class="PaginationBarFiltersCaption"></div>');
					$(pagingCaptionDiv).find('>div:eq(0)').html(gx.getMessage(this.control.EmptyGridCaption));
					$(pagingCaptionDiv).find('>div:eq(1)').text(gx.getMessage(this.control.AppliedFiltersDsc));
				} else {
					$(pagingCaptionDiv).html(gx.getMessage(this.control.EmptyGridCaption));
				}
				mainDiv.append(pagingCaptionDiv);
			} else {
				$(mainDiv).css("display", "none");
			}
		} else {
			mainDiv.parent().parent().parent().parent().toggleClass(this.control.Class + 'EmptyGrid', false);
			if (this.control.ChangePage && this.control.PageCount != null && this.control.PageCount != '' && this.control.CurrentPage > 1 && this.control.CurrentPage > this.control.PageCount && (this.control.Page == '' || this.control.Page != 0)) {
				var thisC = this;
				window.setTimeout(function () {
					if (thisC.control.ChangePage && thisC.control.PageCount != null && thisC.control.PageCount != '' && thisC.control.CurrentPage > 1 && thisC.control.CurrentPage > thisC.control.PageCount && (thisC.control.Page == '' || thisC.control.Page != 0)) {
						thisC.control.SelectedPage = thisC.control.PageCount;
						thisC.control.ChangePage();
					}
				}, 100);
			}
			mainDiv.addClass(this.control.Class);
			$(mainDiv).css("display", "table");
			$(mainDiv).css("width", "100%");
			$(mainDiv).parent().parent().parent().css("margin-right", '');
			var gridDiv = mainDiv.parent().parent().parent().parent().find('>div>div>.gx-grid');
			if (gridDiv.length == 1 && gridDiv.css('overflow-x') == 'auto' && parseInt(gridDiv.css('margin-right')) != 0) {
				var newMarginRight = parseInt($(mainDiv).parent().parent().parent().css("margin-right")) + parseInt(gridDiv.css('margin-right'));
				$(mainDiv).parent().parent().parent().css("margin-right", newMarginRight + 'px');
			}

			if (this.control.Caption == '' || this.control.PageCount == -1 || this.control.RecordCount == -1) {
				this.control.Caption = '';
				this.control.PagingCaptionPosition == '';
			}

			if (this.control.PagingCaptionPosition == 'Center'
				&& this.control.PagingButtonsPosition == 'Center') {
				var centerDiv = document.createElement('div');
				$(centerDiv).css("display", "table");
				$(centerDiv).css("margin", "0 auto");
				$(mainDiv).append(centerDiv);
				mainDiv = $(centerDiv);
			}

			var pagingCaptionAdded = false;
			if (this.control.PagingButtonsPosition == 'Center'
				&& this.control.PagingCaptionPosition != 'Center') {
				this.renderPagingCaption(mainDiv);
				pagingCaptionAdded = true;
			}
			this.renderPagingButtons(mainDiv);
			if (!pagingCaptionAdded) {
				this.renderPagingCaption(mainDiv);
			}
		}
	}

	this.renderPagingCaption = function (mainDiv) {
		if (this.control.Caption != '') {
			var pagingCaptionDiv = document.createElement('div');
			$(pagingCaptionDiv).addClass('PaginationBarCaption');

			if (this.control.PagingCaptionPosition == 'Right') {
				$(pagingCaptionDiv).css("right", "0%");
			}
			if (this.control.PagingCaptionPosition == 'Center'
				&& this.control.PagingButtonsPosition == 'Center') {
				$(pagingCaptionDiv).css("float", this.invertFloatIfRTL("left"));
			} else {
				if (this.control.PagingCaptionPosition == 'Center') {
					$(pagingCaptionDiv).css("display", "table");
					$(pagingCaptionDiv).css("margin", "0 auto");
				} else if (this.control.PagingButtonsPosition == 'Center') {
					$(pagingCaptionDiv).css("position", "absolute");
					if (this.control.PagingCaptionPosition == 'Right') {
						$(pagingCaptionDiv).css("right", "0%");
					}
				} else {
					$(pagingCaptionDiv).css("float", this.invertFloatIfRTL(this.control.PagingCaptionPosition == 'Right' ? "right" : "left"));
				}
			}

			var caption = gx.getMessage(this.control.Caption);
			caption = caption.replace("<FIRST_RECORD>", (this.control.CurrentPage - 1) * this.control.Page + 1);
			caption = caption.replace("<LAST_RECORD>", Math.min(this.control.CurrentPage * this.control.Page, this.control.RecordCount));
			caption = caption.replace("<TOTAL_RECORDS>", this.control.RecordCount);
			caption = caption.replace("<CURRENT_PAGE>", this.control.CurrentPage);
			caption = caption.replace("<TOTAL_PAGES>", this.control.PageCount < 0 ? 1 : this.control.PageCount);

			if (this.control.RowsPerPageSelector || wwp.settings.pagBar.IncludeGoTo) {
				$(pagingCaptionDiv).addClass('dropdown');
				$(pagingCaptionDiv).html('<div class="dropdown rowsperpage dropup"><button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">' + caption + '<span class="caret"></span></button><ul class="dropdown-menu"><li class="caption"><span href="#">' + gx.getMessage(this.control.RowsPerPageSelector ? this.control.RowsPerPageCaption : wwp.labels.PagBarGoToCaption) + '</span></li></ul>');

				var ddo = pagingCaptionDiv.children[0];
				var ddoUl = ddo.children[1];

				var captionLI = ddoUl.children[0];
				$(captionLI).on("click", function (e) {
					e.stopPropagation();
				});

				if (this.control.RowsPerPageSelector) {
					var options = this.control.RowsPerPageOptions;
					options = WWP_replaceAll(options, '\\\\', '[\\\\]');
					options = WWP_replaceAll(options, '\\,', '[\\\\\\]');
					var optionsSplitted = options.split(',');
					for (var index2 = 0; index2 < optionsSplitted.length; index2 += 1) {
						var option = optionsSplitted[index2];
						option = WWP_replaceAll(option, '[\\\\\\]', ',');
						option = WWP_replaceAll(option, '\\:', '[\\\\\\]');
						var optionSplitted = option.split(':');
						var optionVal = optionSplitted[0];
						optionVal = WWP_replaceAll(optionVal, '[\\\\\\]', ':');
						optionVal = WWP_replaceAll(optionVal, '[\\\\]', '\\');
						var optionText;
						if (optionSplitted.length > 1) {
							optionText = optionSplitted[1];
							optionText = WWP_replaceAll(optionText, '[\\\\\\]', ':');
							optionText = WWP_replaceAll(optionText, '[\\\\]', '\\');
						} else {
							optionText = optionVal;
						}
						var liOption = document.createElement('li');
						var aLiOption = document.createElement('a');
						var spanLiOption = document.createElement('span');
						var optionTextOriginal = optionText;
						optionText = gx.getMessage(optionTextOriginal);
						var isSelected = (optionVal == this.control.RowsPerPageSelectedValue.toString().trim());
						if (isSelected) {
							liOption.setAttribute("class", "selected");
						}
						spanLiOption.innerHTML = optionText;
						liOption.setAttribute("val", optionVal);

						aLiOption.setAttribute("href", "#");

						var thisC = this;
						$(aLiOption).on("click", function (ek) {
							return function (e) {
								thisC.control.RowsPerPageSelectedValue = ek;
								thisC.updateLIOptions();
								$(this).closest('ul').css('display', 'none');
								thisC.setSectionGridMinHeight(false);
								e.preventDefault();
								e.stopPropagation();
							}
						}(optionVal));
						if (this.control.ChangeRowsPerPage) {
							$(aLiOption).on("click", this.control.ChangeRowsPerPage);
						}

						aLiOption.appendChild(spanLiOption);
						liOption.appendChild(aLiOption);
						ddoUl.appendChild(liOption);
					}
					if (wwp.settings.pagBar.IncludeGoTo) {
						$(ddoUl).append('<li class="divider"></li><li class="caption"><span href="#">' + gx.getMessage(wwp.labels.PagBarGoToCaption) + '</span></li>');
					}
				}
				if (wwp.settings.pagBar.IncludeGoTo) {
					$(ddoUl).append('<li class="goTo"><input type="number" value="1" max="999" ><i class="' + wwp.labels.PagBarGoToIconClass + '" title="' + gx.getMessage(wwp.labels.PagBarGoToTooltip) + '"></i></li>');
					$(ddoUl).find('.goTo>input')
						.on("focus", function (thisC) {
							return function (e) {
								$(this).select();
							}
						}(this))
						.on("keyup", function (thisC) {
							return function (e) {
								if (e.keyCode == 13) {
									$(this).next().click();
								} else {
									var val = $(this).val();
									if (isNaN(parseInt(val)) || parseInt(val) <= 0 || parseInt(val) > thisC.control.PageCount) {
										$(this).val($(this).data('validVal'));
										$(this).select();
									} else {
										$(this).data('validVal', val);
									}
								}
							}
						}(this))
						.data('validVal', '1');
					$(ddoUl).find('.goTo>i').on("click", function (thisC) {
						return function (e) {
							thisC.control.SelectedPage = $(this).prev().val();
						}
					}(this));
					if (this.control.ChangePage) {
						$(ddoUl).find('.goTo>i').on("click", this.control.ChangePage);
					}
				}

				var triggerButton = ddo.children[0];
				this.pagingCaptionDiv = pagingCaptionDiv;
				$(triggerButton).on("click", function (thisC) {
					return function (e) {
						thisC.setSectionGridMinHeight(true);
						if (wwp.settings.pagBar.IncludeGoTo) {
							window.setTimeout(function () {
								$(ddoUl).find('.goTo>input').focus();
							}, 1);
						}
					}
				}(this));

				$(ddo).on('hidden.bs.dropdown', function (thisC) {
					return function (e) {
						thisC.setSectionGridMinHeight(false);
					}
				}(this));
			}
			else {
				$(pagingCaptionDiv).html(caption);
			}

			mainDiv.append(pagingCaptionDiv);
		}
	}

	this.invertFloatIfRTL = function (floatVal) {
		return $('body').css('direction') == 'rtl' ? (floatVal == 'right' ? 'left' : 'right') : floatVal;
	}

	this.updateLIOptions = function () {
		var ddUL = this.pagingCaptionDiv.children[0].children[this.pagingCaptionDiv.children[0].children.length - 1];
		for (var i = 1; i < ddUL.children.length; i++) {
			var liOption = ddUL.children[i];
			if (liOption.getAttribute("val") == this.control.RowsPerPageSelectedValue.toString().trim()) {
				liOption.setAttribute("class", "selected");
			}
			else {
				$(liOption).removeClass("selected");
			}
		}
	}

	this.setSectionGridMinHeight = function (setMinHeight) {
		var dd = this.pagingCaptionDiv.children[0].children[1];
		var sectionGridElement = dd.parentElement;
		for (var i = 0; i < 11; i++) {
			if ($(sectionGridElement).hasClass("SectionGrid")) {
				break;
			}
			else {
				sectionGridElement = sectionGridElement.parentElement;
			}
		}
		if ($(sectionGridElement).hasClass("SectionGrid")) {
			var overflowEl = sectionGridElement.children[0];
			var ddP = dd.parentElement;
			if (setMinHeight) {
				var ddBtn = dd.parentElement.children[0];
				var availableHeight = $(ddBtn).offset().top - $(overflowEl).offset().top;
				if ($(dd).height() + 20 > availableHeight) {
					var ddP = dd.parentElement;
					$(ddP).removeClass("dropup");
					var minH = availableHeight + ddBtn.offsetHeight + $(dd).height() + 12 + 20;//12 = shadow
					$(overflowEl).css({ 'min-height': minH + "px" });
					if ($(overflowEl).height() > overflowEl.scrollHeight) {
						//has horizontal scroll visible
						minH += $(overflowEl).height() - overflowEl.scrollHeight;
						$(overflowEl).css({ 'min-height': minH + "px" });
					}
				}
				else {
					setMinHeight = false;
				}
			}
			if (!setMinHeight) {
				$(overflowEl).css({ 'min-height': "" });
				if (!$(ddP).hasClass("dropup")) {
					$(ddP).addClass("dropup");
				}
			}
		}
	}

	this.renderPagingButtons = function (mainDiv) {

		var pagingMainDiv = document.createElement('div');
		$(pagingMainDiv).addClass('PaginationBarButtons');

		if (this.control.PagingCaptionPosition == 'Center'
				&& this.control.PagingButtonsPosition == 'Center') {
			$(pagingMainDiv).css("float", this.invertFloatIfRTL("left"));
		} else {
			if (this.control.PagingButtonsPosition == 'Center') {
				$(pagingMainDiv).css("display", "table");
				$(pagingMainDiv).css("margin", "0 auto");
			} else if (this.control.PagingCaptionPosition == 'Center') {
				$(pagingMainDiv).css("position", "absolute");
				if (this.control.PagingButtonsPosition == 'Right') {
					$(pagingMainDiv).css("right", "0%");
				}
			} else {
				$(pagingMainDiv).css("float", this.invertFloatIfRTL(this.control.PagingButtonsPosition == 'Right' ? "right" : "left"));
			}
		}

		var ul = document.createElement('ul');
		ul.className = "pagination";
		var li;
		var liA;

		if (this.control.ShowFirst) {
			this.addPagingLI(ul, "first", this.control.CurrentPage == 1, gx.getMessage(this.control.First), '1');
		}

		if (this.control.ShowPrevious) {
			this.addPagingLI(ul, "prev", this.control.CurrentPage == 1, gx.getMessage(this.control.Previous), 'Previous');
		}

		var printedPages = 0;
		this.control.PagesToShow = parseInt("" + this.control.PagesToShow);
		var firstPage = this.control.CurrentPage - Math.ceil(this.control.PagesToShow / 2);
		while (firstPage >= 0 && firstPage + this.control.PagesToShow > this.control.PageCount) {
			firstPage -= 1;
		}
		for (var i = 1; firstPage + i <= this.control.PageCount && printedPages < this.control.PagesToShow; i += 1) {
			var page = firstPage + i;
			if (page >= 1) {
				this.addPagingLI(ul, page == this.control.CurrentPage ? "active" : "hidden-xs", false, '' + page, '' + page);
				printedPages += 1;
			}
		}

		if (this.control.ShowNext) {
			this.addPagingLI(ul, "next", this.control.CurrentPage == this.control.PageCount, gx.getMessage(this.control.Next), 'Next');
		}

		if (this.control.ShowLast) {
			this.addPagingLI(ul, "last", this.control.CurrentPage == this.control.PageCount, gx.getMessage(this.control.Last), this.control.PageCount);
		}

		var filteredDiv = this.control.AppliedFiltersDsc != null && this.control.AppliedFiltersDsc != '' ? '<div class="PaginationBarCaption PaginationBarFiltered"><span></span></div>' : null;
		if (filteredDiv != null && this.control.PagingButtonsPosition == 'Center') {
			$(pagingMainDiv).append(filteredDiv);
		}

		$(pagingMainDiv).append(ul);
		mainDiv.append(pagingMainDiv);

		if (filteredDiv != null) {
			if (this.control.PagingButtonsPosition != 'Center') {
				mainDiv.append(filteredDiv);
				mainDiv.find('>div').last().css("float", $(pagingMainDiv).css("float"));
			}
			mainDiv.find('>div').last().find('span').text(this.control.AppliedFiltersDsc);
		}
	}

	this.addPagingLI = function (ul, className, liDisabled, caption, selectedPage) {
		var li = document.createElement('li');
		li.className = className;
		if (liDisabled) {
			li.className += " disabled";
		}
		liA = document.createElement('a');
		liA.innerText = caption;
		if (!liDisabled) {
			this.attachClickEvent(liA, selectedPage);
		}
		$(li).append(liA);
		$(ul).append(li);
	}

	this.attachClickEvent = function (control, selectedPage) {
		thisC = this;
		control.setAttribute("href", "#");
		$(control).on("click", function (thisC, selPage) {
			return function (e) {
				thisC.control.SelectedPage = selPage;
				e.preventDefault();
				e.stopPropagation();
			}
		}(this, selectedPage));
		if (this.control.ChangePage)
			$(control).on("click", this.control.ChangePage);
	}
}

function SetMinWidthPaginationBars() {
	$("table.GridWithPaginationBar").each(function () {
		SetMinWidthPaginationBar(this);
	});
}

function SetMinWidthPaginationBar(grid) {
	var idGrid = $(grid).attr('id');
	if ($(grid).parent().length > 0 && $(grid).parent().css('overflow-x') != 'auto') {
		var width = $(grid).outerWidth();
		if (idGrid != null) {
			var pagingBarId = idGrid.substring(0, idGrid.indexOf("ContainerTbl")).toUpperCase() + "PAGINATIONBARContainer_DVPaginationBar";
			width = width - parseInt($("#" + pagingBarId).css('margin-left')) - parseInt($("#" + pagingBarId).css('margin-right'));
			if ($("#" + pagingBarId).parent().width() < width) {
				$("#" + pagingBarId).css({ minWidth: width });
			} else {
				$("#" + pagingBarId).css({ minWidth: 0 });
			}
		}
	}
}

function ClearMinWidthPaginationBars() {
	$("table.GridWithPaginationBar").each(function () {;
		if ($(this).parent().length > 0 && $(this).parent().css('overflow-x') != 'auto') {
			var idGrid = $(this).attr('id');
			if (idGrid != null) {
				var pagingBarId = idGrid.substring(0, idGrid.indexOf("ContainerTbl")).toUpperCase() + "PAGINATIONBARContainer_DVPaginationBar";
				$("#" + pagingBarId).css({ minWidth: 0 });
			}
		}
	});
}

$(window).resize(function () {
	SetMinWidthPaginationBars();
});

$(window).on('load', function () {
	SetMinWidthPaginationBars();
});

;$(window).one('load',function(){WWP_VV([['DVPaginationBar','14.3001']]);});