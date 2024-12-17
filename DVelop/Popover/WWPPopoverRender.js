function DVelop_WWPPopover() {

	this.show = function () {
		if (this.updatingHTML) {
			return;
		}

		if (this.IsGridItem) {
			WWP_NotifyEmpower(this, 'WWP.' + this.ControlName.toUpperCase());
		} else {
			if (this.ItemInternalName != '') {
				var $span = $('#span_' + this.ItemInternalName);
				if ($span.length == 0) {
					$span = $('#' + this.ItemInternalName);
				}
				if ($span.length == 0) {
					//try Free Style Grid
					$span = $('#span_' + this.ItemInternalName + '_0001');
					if ($span.length == 0) {
						$span = $('#' + this.ItemInternalName + '_0001');
					}
					if ($span.length == 1) {
						this.IsFSGridItem = true;
					}
				}
				if (this.IsFSGridItem) {
					var spanIdPrefix = $span.attr('id');
					spanIdPrefix = spanIdPrefix.substring(0, spanIdPrefix.length - 4);
					var index = 1;
					while (true) {
						var indexStr = '000' + index;
						$span = $('#' + spanIdPrefix + indexStr.substring(indexStr.length - 4));
						if ($span.length == 1) {
							this.InitPopovers($span);
							index++;
						} else {
							break;
						}
					}
				} else {
					if ($span.is('input')) {
						this.InitPopovers($span);
					} else {
						//se retrasa la inicializacion porque aparentemente gx reemplaza el elemento
						var thisC = this;
						window.setTimeout(function () {
							$span = $('#' + $span.attr('id'));
							thisC.InitPopovers($span);
						}, 0);
					}
				}
			}
		}

		//por alguna razon no funciona dentro del data-content poner un style="min-width..." por lo que se agrega dinamicamente una clase con el minWidth
		if (parseInt(this.PopoverWidth) > 0) {
			var popWidth = (this.PopoverWidth + '').trim();
			var $head = $(document.getElementsByTagName('head')[0]);
			var style = document.createElement('style');
			style.type = 'text/css';
			var popWidth = (this.PopoverWidth + '').trim();
			style.innerHTML = '.popoverW' + popWidth + ' { min-width: ' + popWidth + 'px; }';
			var popoverWFound = false;
			$head.children().each(function () {
				if (this != null && this.type == style.type && this.innerHTML == style.innerHTML) {
					popoverWFound = true;
					return false;
				}
			});
			if (!popoverWFound) {
				$head[0].appendChild(style);
			}
		}
	}

	this.Close = function () {
		if (this.$popoverElem != null) {
			this.$popoverElem.data('forceHide', '').popover('hide');
		}
	}

	this.InitGridPopovers = function () {
		if (this.ItemInternalName != '') {
			var $grid = WWP_GetGrid(this).find('>table');
			if ($grid.length == 1 && ($grid.data('PO_' + this.ControlName) == null || $grid.data('IS_ProcessedRecords') != null)) {
				WWP_Debug_Log(false, 'InitGridPopovers');
				$grid.data('PO_' + this.ControlName, '');
				var $gridBody = $grid.find('>tbody');
				var rows = $gridBody.children().length;
				var index = 1;
				if ($grid.data('IS_ProcessedRecords') != null) {
					index = $grid.data('IS_ProcessedRecords') + 1;
				}
				for (; index <= rows; index += 1) {
					var itemIndex = '000' + index;
					itemIndex = itemIndex.substring(itemIndex.length - 4);
					var $span = $gridBody.find('#span_' + this.ItemInternalName + '_' + itemIndex);
					if ($span.length == 0) {
						break;
					}
					$span.closest('tbody').parent().find('thead>tr>th:nth-child(' + ($span.closest('td').index() + 2) + ')').addClass('EditableGridAuxCol');
					this.InitPopovers($span);
				}
			}
		}
	}
	this.InitPopovers = function ($span) {
		var $origSpan = $span;
		if ($span.css('display') == 'none') {
			return;
		}
		if (this.TriggerElement == 'FontIcon') {
			if (this.IsGridItem) {
				$span = $span.find('.WWPPopoverIcon');
			} else {
				$span = $span.closest('td').next().find('.WWPPopoverIcon');
			}
		} else {
			$span.addClass("PopoverValue");
		}
		if (this.Trigger == "Click") {
			$span.addClass("PopoverTriggerClick");
		}
		if ($span.attr('data-placement') != null) {
			return;
		}
		var isInput = $span.is('input');
		var thisC = this;
		$span
			.popover("destroy")
			.attr('data-placement', (WWP_endsWith($span.attr('id'), '_MPAGE') && $span.closest('#TABLEHEADER_MPAGE').length == 1 ? "" : "auto ") + this.Position.toLowerCase())
			.attr('data-html', "true")
			.attr('data-trigger', this.Trigger.toLowerCase())
			.attr('data-content', '<div class="popoverMainDiv' + (parseInt(this.PopoverWidth) > 0 ? ' popoverW' + (this.PopoverWidth + '').trim() : '') + '">' + this.HTML + '</div>')
			.on('show.bs.popover', function (event) {
				WWP_Debug_Log(false, 'show ' + $(this).data('poStatus'));
				thisC.$popoverElem = $span;
				if (thisC.Trigger == "Hover" && (parseInt($(this).data('poStatus')) || 0) != 0 || !thisC.OpenIfEmpty && (isInput ? $span.val() : $span.text()) == '') {
					WWP_Debug_Log(false, 'show return');
					return false;
				}
				if (thisC.IsGridItem && $(this).closest('td').css('position') == 'absolute') {
					var $td = $(this).closest('td');
					if ($td.attr('orig-zIndex') == null) {
						$td.attr('orig-zIndex', $td.css('z-index'));
						$td.css('z-index', parseInt($td.css('z-index')) + 1);
					}
				}
				WWP_Debug_Log(false, 'show end');
			})
			.on('shown.bs.popover', function (event) {
				WWP_Debug_Log(false, 'showw init');
				if (thisC.Trigger == "Hover" && (parseInt($(this).data('poStatus')) || 0) != 0) {
					WWP_Debug_Log(false, 'shown end return ' + $(this).data('poStatus'));
					return false;
				}
				if (thisC.Trigger == "Click" && !thisC.KeepOpened) {
					thisC.bodyMouseDownHandler = function (e) {
						if (e.target != null && e.target.parentElement != null && $(e.target).closest($span.parent()).length == 0) {
							$span.data('forceHide', '');
							$span.popover('hide');
						}
					};
					gx.evt.attach($('body')[0], "mousedown", thisC.bodyMouseDownHandler);
				}

				thisC.PopoverShown(this, false, $origSpan);
				WWP_Debug_Log(false, 'shown end ' + $(this).data('poStatus'));
			})
			.on('hide.bs.popover', function (event) {
				WWP_Debug_Log(false, 'hide init');
				if (thisC.updatingHTML || isInput && thisC.Trigger == "Click" && (thisC.OpenIfEmpty || $span.val() != '') && $span.data('forceHide') == null) {
					return false;
				}
				$span.data('forceHide', null);
				$(this).data('poStatus', (parseInt($(this).data('poStatus')) || 0) + 1);
				thisC.PopoverHide(this);
				if (thisC.OnClose != null) {
					thisC.realHide = $span.next().is(':visible');
				}
				WWP_Debug_Log(false, 'hide end ' + $(this).data('poStatus') + ' ' + thisC.ControlName);
			})
			.on('hidden.bs.popover', function (event) {
				thisC.$popoverElem = null;
				$(this).data('poStatus', (parseInt($(this).data('poStatus')) || 0) - 1);
				WWP_Debug_Log(false, 'hidden start ' + $(this).data('poStatus'));
				if (thisC.IsGridItem && $(this).closest('td').attr('orig-zIndex') != null) {
					var $td = $(this).closest('td');
					$td.css('z-index', parseInt($td.attr('orig-zIndex')));
					$td.removeAttr('orig-zIndex');
				}
				thisC.setSectionGridMinHeight(this, false);
				if (thisC.bodyMouseDownHandler != null) {
					gx.evt.detach($('body')[0], "mousedown", thisC.bodyMouseDownHandler);
					thisC.bodyMouseDownHandler = null;
				}
				if (thisC.OnClose != null && thisC.realHide) {
					thisC.OnClose();
				}
				if ($span.hasClass('AttributeSearch')) {
					window.setTimeout(function () {
						//se ejecuta un resize para que se vuelva a redibujar el menu horizontal (si existe)
						$(window).resize();
					}, 100);
				}
				WWP_Debug_Log(false, 'hidden end');
			})
			.popover()
			.data('poStatus', null);

		if (isInput && this.ReloadOnKeyChange) {
			$span.on("input", function (thisC) {
				return function (e) {
					if (thisC.myT != null) {
						clearInterval(thisC.myT);
					}
					thisC.myT = setInterval(function () {
						clearInterval(thisC.myT);
						if (thisC.OnLoadComponent != null) {
							$span.change();
							//tambien puede ser $span.blur().focus(); pero no funciona en IE 11
							var hasVal = $span.val().length >= thisC.MinimumCharacters;
							if ($span.next().is(':visible') && (hasVal || thisC.OpenIfEmpty)) {
								if (thisC.lastWCLoadedEmpty == null || hasVal || thisC.lastWCLoadedEmpty != (hasVal + '')) {
									thisC.PopoverShown($span[0], true, $origSpan);
								}
							}
							else if (hasVal || !thisC.OpenIfEmpty) {
								$span.popover(hasVal ? 'show' : 'hide');
							}
						}
					}, 300);
				}
			}(this));
		}
		if (this.Trigger == "Click") {
			var retryClick = true;
			$span.click(function (thisC) {
				return function (e) {
					if (!$span.next().is(':visible')) {
						if (retryClick) {
							retryClick = false;
							$span.click();
						}
					} else {
						retryClick = true;
					}
				}
			}(this));
		}
	}

	this.PopoverHide = function (currentElem) {
		if (this.HTML == '') {
			WWP_revertWCMoved(this.getWCParentDivName(), this, currentElem, this.Load == 'OnEveryTrigger');
		}
		var $popover = $(currentElem).next();
		if (this.resetBottom) {
			$popover.css('top', $popover.css('top'));
			$popover.css('bottom', '');
		}
		if (this.resetRight) {
			$popover.css('left', $popover.css('left'));
			$popover.css('right', '');
		}
	}

	this.getWCParentDivName = function () {
		return ('DIV_' + (this.Load == 'OnEveryTrigger' ? 'WWPAUXWC' : this.ControlName));
	}

	this.forceOpenDirectionLeft = function (currentElem, $popover) {
		var rect = $popover[0].getBoundingClientRect();
		return (rect.left + Math.max(rect.width, 200)) > window.innerWidth;
	}

	this.PopoverShown = function (currentElem, refreshing, $origSpan) {
		var $popover = $(currentElem).next();
		var $popoverArrow = $popover.find('>.arrow');
		if ($popoverArrow.length == 1) {
			if ($popover.is('.bottom,.top')) {
				$popoverArrow.css('left', $popoverArrow.css('left'));
			} else {
				$popoverArrow.css('top', $popoverArrow.css('top'));
			}
		}
		if ($popover.is('.top')) {
			this.resetBottom = true;
			//TODO:chequear en IE11, probablemente haya que cambiar la siguiente linea por: $popover.css('bottom', $popover.height() - parseInt($popover.css('top')));
			$popover.css('bottom', $popover.css('bottom'));
			$popover.css('top', 'auto');
		} else {
			$popover.css('bottom', '');
		}
		if ($popover.is('.left') || $popover.is('.top, .bottom') && this.forceOpenDirectionLeft(currentElem, $popover)) {
			this.resetRight = true;
			$popover.css('right', $popover.css('right'));
			$popover.css('left', 'auto');
			if ($popover.is('.top, .bottom')) {
				$popoverArrow.css({ 'left': 'auto', 'right': $popoverArrow.css('right') });
			}
		} else {
			$popover.css('right', '');
			if ($popover.prev().hasClass('AttributeSearch')) {
				//se corrije en el caso de que el search no pueda crecer hacer la izquierda por falta
				//de espacio (pasa en amur con el explorador chico)
				var isRtl = $('body').css('direction') == 'rtl';
				var leftShift = WWP_OffsetLeft(isRtl, $popover.prev()) - WWP_OffsetLeft(isRtl, $popover);
				if (leftShift < -2 || leftShift > 2) {
					$popover.css('left', parseInt($popover.css('left')) + leftShift);
				}
			}
		}
		var $popoverContent = $popover.find('.popover-content');
		var $item = $popoverContent.find('>div');
		if ($item.length == 0) {
			var newDiv = document.createElement('div');
			$popoverContent.append(newDiv);
			$item = $(newDiv);
		}

		if (this.HTML != '') {
			if (this.OnLoadComponent != null) {
				this.updatingHTML = true;
				this.OnLoadComponent();
				delete this.updatingHTML;
			}
			$item.html(this.HTML);
		} else {
			var wcId = WWP_getWCId(this.getWCParentDivName(), this);
			var $wcElem = $("#" + wcId);
			var callWCLoad = (this.IsGridItem || this.IsFSGridItem || this.Load == 'OnEveryTrigger' || this.Load == 'OnFirstTrigger' && $wcElem.children().length == 0);
			var showLoading = this.ShowLoading && callWCLoad;
			if (callWCLoad || $item.children().length == 0) {
				if (refreshing) {
					$popoverContent.css('min-height', $popoverContent.height());
				}
				$item.html('');

				var newItemInnerDiv = document.createElement('div');

				if (showLoading) {
					$(newItemInnerDiv).css('display', 'none');
					var newItemMainDiv = document.createElement('div');
					newItemMainDiv.innerHTML = '<div class="WCD_Loading"><i class="fas fa-spinner fa-spin"></i></div>';
					$item.append(newItemMainDiv);
					newItemMainDiv.appendChild(newItemInnerDiv);
					$(newItemMainDiv).css('min-height', $(newItemMainDiv).height());
				} else {
					$item.append(newItemInnerDiv);
				}
				var $wcElem = WWP_moveWCToDiv(wcId, this, showLoading, newItemInnerDiv);
				var thisC = this;
				if (showLoading) {
					window.setTimeout(function () {
						thisC.wcLoaded($popoverContent, newItemInnerDiv, $wcElem, showLoading);
					}, 3000);
				}
				if (callWCLoad) {
					$wcElem.html('');
					if (showLoading) {
						this.wcRenderHandler = function (e) {
							if (e.containerControl == $wcElem[0]) {
								thisC.wcLoaded($popoverContent, newItemInnerDiv, $wcElem, showLoading);
							}
						};
						gx.fx.obs.addObserver("webcom.render", window, this.wcRenderHandler);
					}
					if (this.IsGridItem) {
						$(currentElem).closest('td').next().find('a,img').click();
					} else if (this.IsFSGridItem) {
						var spanId = $origSpan.attr('id');
						var elemToClickId = spanId.substring(0, spanId.length - 5) + '_POPOVER' + spanId.substring(spanId.length - 5);
						var $elemToClick = $('#' + elemToClickId);
						if ($elemToClick.length == 0) {
							var spanPrefix = 'span_' + WWP_getCurrentWCId(thisC);
							$elemToClick = $('#' + spanPrefix + 'v' + elemToClickId.substring(spanPrefix.length));
						}
						$elemToClick.click();
					} else if (this.OnLoadComponent != null) {
						if (this.ReloadOnKeyChange && this.MinimumCharacters > 1) {
							this.lastWCLoadedEmpty = ($(currentElem).val().length >= this.MinimumCharacters) + '';
						}
						this.OnLoadComponent();
					}
				}
			}
		}
		this.setSectionGridMinHeight(currentElem, true);
	}

	this.wcLoaded = function ($popoverContent, newItemInnerDiv, $wcElem, showLoading) {
		if ($(newItemInnerDiv).css('display') == 'none') {
			WWP_Debug_Log(false, 'wcLoaded');
			if (showLoading) {
				$(newItemInnerDiv).parent().find('.WCD_Loading').remove();
				$wcElem.find('.gxwebcomponent-body').removeClass('Invisible');
				var thisC = this;
				$(newItemInnerDiv).slideDown(function () {
					$(this.parentElement).css('min-height', '');
					$popoverContent.css('min-height', '');
					WWP_GXEAIInfo($(this.parentElement));
				});
			}
			if (this.wcRenderHandler != null) {
				var wcRenderHandlerAux = this.wcRenderHandler;
				delete this.wcRenderHandler;
				window.setTimeout(function () {
					//se hace asi porque sino en algun caso el WC se levanta "raro" (caso DD user info no se muestra el ChangePass)
					gx.fx.obs.deleteObserver("webcom.render", window, wcRenderHandlerAux);
				}, 1);
			}
			if (WWP_IsIE() && $popoverContent.find('.FSResultCategoriesCell')) {
				//por comportamiento raro de IE11 que no se puede hacer click en nada
				window.setTimeout(function () {
					$popoverContent.find('.FSResultCategoriesCell').scrollTop(1);
				}, 500);
			}
		}
	}

	this.setSectionGridMinHeight = function (currentElem, setMinHeight) {
		if (this.IsGridItem) {
			var td = $(currentElem).closest('td');
			if (setMinHeight) {
				if (td.data('origZIndex') == null && parseInt(td.css('z-index')) > 0) {
					var origZI = td.css('z-index');
					td.css('z-index', (parseInt(origZI) + 10));
					td.data('origZIndex', origZI);
				}
			}
			else {
				var origZI = td.data('origZIndex');
				if (origZI != null) {
					td.css('z-index', origZI);
					td.data('origZIndex', null);
				}
			}
		}
		//if (setMinHeight) {
		//	var $popover = $(currentElem).next();
		//	if (!$popover.is('.popover.top')) {
		//		WWP_setSectionGridMinHeight($popover[0], setMinHeight);
		//	}
		//} else {
		//	WWP_setSectionGridMinHeight(currentElem, setMinHeight);
		//}
	}
};$(window).one('load',function(){WWP_VV([['WWPPopover','15.2.8']]);});