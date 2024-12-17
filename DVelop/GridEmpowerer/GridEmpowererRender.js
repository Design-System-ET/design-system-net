
var scrollWidthZoom = null;
function WWP_GridEmpowerer() {

	this.show = function () {
		if (!this.eventsAttached) {
			this.eventsAttached = true;

			var thisC = this;

			this.onaftereventxHandler = function () {
				this.onaftereventx();
			}.closure(this);

			$.each(['gx.onafterevent', 'gx.onload', 'grid.onafterrender', 'webcom.all_rendered', 'gx.endprocessing', 'grid.onafterrefresh'], function (i, gxEventName) {
				gx.fx.obs.addObserver(gxEventName, window, thisC.onaftereventxHandler);
			});

			this.resizeHandler = function () {
				this.HandleWindowResize();
			}.closure(this);

			gx.evt.attach(window, "resize", this.resizeHandler);

			this.loadHandler = function () {
				this.HandleWindowLoad();
			}.closure(this);

			gx.evt.attach(window, "load", this.loadHandler);

			if ($('#' + this.ContainerName).closest('.WCD_tr').length == 1) {
				this.insideDWC = true;
			}
		}

		var $grid = WWP_GetGrid(this);
		if ($grid.data('WWP.Empowerer') == null) {
			$grid.data('WWP.Empowerer', this);
		}

		this.Empower();
	}

	this.destroy = function () {
		if (this.resizeHandler != null) {
			gx.evt.detach(window, "resize", this.resizeHandler);
		}
		if (this.loadHandler != null) {
			gx.evt.detach(window, "load", this.loadHandler);
		}
		var thisC = this;
		$.each(['gx.onafterevent', 'gx.onload', 'grid.onafterrender', 'webcom.all_rendered', 'gx.endprocessing', 'grid.onafterrefresh'], function (i, gxEventName) {
			gx.fx.obs.deleteObserver(gxEventName, window, thisC.onaftereventxHandler);
		});

		if (this.timerT != null) {
			clearInterval(this.timerT);
			this.timerT = null;
		}
		if (this.ColumnsOrHeaderFixer != null
			&& this.ColumnsOrHeaderFixer.settings != null
			&& this.ColumnsOrHeaderFixer.settings.myT != null) {
			clearInterval(this.ColumnsOrHeaderFixer.settings.myT);
		}
	}

	this.HandleWindowResize = function () {
		var zoomChanged = false;
		if (scrollHWidth != null || scrollVWidth != null) {
			var currentScrollWidthZoom = Math.round(((window.outerWidth) / window.innerWidth) * 100) / 100;
			if (scrollWidthZoom != null && scrollWidthZoom != currentScrollWidthZoom) {
				scrollHWidth = null;
				scrollVWidth = null;
				zoomChanged = true;
			}
			scrollWidthZoom = currentScrollWidthZoom;
		}
		var $grid = WWP_GetGrid(this);

		if (this.IsReadyToEmpower($grid)) {
			var $gridTable = $grid.find('>table');
			if (this.Empowered) {
				if (this.ColumnsOrHeaderFixer != null) {
					this.ColumnsOrHeaderFixer.Refresh(zoomChanged);
				} else {
					if (this.CategoriesHelper != null) {
						var $gridTable = $grid.find('>table');
						if ($gridTable.data('titlesCategories') == 'P') {
							$gridTable.data('titlesCategories', '');
							this.CategoriesHelper.mergeTitlesCategoryCells();
						}
					}
					if (this.RowGroupsHelper != null) {
						var $gridTable = $grid.find('>table');
						if ($gridTable.data('groupProcessed') == 'P') {
							$gridTable.data('groupProcessed', '');
							this.RowGroupsHelper.mergeGroupRowCellsAndAddTitle();
						}
					}
				}
				if (this.InfiniteScrolling == 'Grid'
					&& $gridTable.find('>tbody').hasClass('gx-infinite-scrolling-element')) {
					this.InfiniteScrolling_FinishedProcessing();
				}
			}
		}
	}

	this.HandleWindowLoad = function () {
		this.Empower();
	}

	this.onaftereventx = function () {
		this.Empower();
	}

	this.IsReadyToEmpower = function ($grid) {
		if ($grid == null) {
			$grid = WWP_GetGrid(this);
		}
		if (this.HasCategories) {
			this.CategoriesHelper = $grid.data('WWP.GridTitlesCategories');
			if (this.CategoriesHelper == null) {
				return false;
			}
		}
		if (this.HasTitleSettings) {
			this.TitleSettingsHelper = $grid.data('WWP.TitleSettings');
			if (this.TitleSettingsHelper == null) {
				return false;
			}
		}
		if (this.HasColumnsSelector) {
			var colSelHelper = $grid.data('WWP.GridColumnSelector');
			if (wwp.settings.columnsSelector.AllowColumnReordering) {
				this.ColumnSelectorHelper = colSelHelper;
			}
			if (colSelHelper == null && (wwp.settings.columnsSelector.AllowColumnReordering || $grid.closest('.HasGridEmpowerer').hasClass('ListViewGrid'))) {
				if ($grid.closest('.HasGridEmpowerer').hasClass('ListViewGrid')) {
					var wwpUC = $('#' + WWP_getParentWCId(this) + 'BTNEDITCOLUMNS').data('wwpUC');
					if (wwpUC != null && wwpUC.control != null) {
						wwpUC = wwpUC.control;
						wwpUC.HasGridEmp = null;//para que se recalcule
						wwpUC.SetDropDownOptionsData(wwpUC.DropDownOptionsData);
					}
				}
				return false;
			}
		}
		if (this.HasRowGroups) {
			this.RowGroupsHelper = $grid.data('WWP.RowGroupsHelper');
			if (this.RowGroupsHelper == null) {
				return false;
			}
		}
		if (this.PopoversInGrid != '' && this.PopoversInGrid != null) {
			var popoversInGridSplitted = this.PopoversInGrid.split('|');
			for (var i = 0; i < popoversInGridSplitted.length; i += 1) {
				var keyName = 'WWP.' + popoversInGridSplitted[i].toUpperCase();
				this[keyName] = $grid.data(keyName);
				if (this[keyName] == null) {
					return false;
				}
			}
		}
		WWP_Debug_Log(false, 'ReadyToEmpower');
		return true;
	}

	this.Empower = function () {
		var $grid = WWP_GetGrid(this);
		if (this.IsReadyToEmpower($grid)) {

			if (this.RowGroupsHelper != null && this.InfiniteScrolling == 'Form') {
				$grid.find('tr.DVGroupByRow table tr[data-gxrow]').remove();
			}
			else if (this.InfiniteScrolling != 'False' && $grid.find('>table>thead>tr>th.WCD_ActionColumn').length > 0) {
				$grid.find('>table>tbody>tr.WCD_tr').each(function () {
					var $trRow = $(this);
					$trRow.find('table>thead>tr[data-gxrow]').each(function () {
						$trRow.find('tr[id="' + $(this).attr('id') + '"]').remove();
					});
				});
			}

			if (this.InfiniteScrolling == 'Grid'
				&& this.ColumnSelectorHelper != null
				&& $grid.hasClass('gx-infinite-scrolling-container')) {
				var gxGrid = this.GetGxGrid();
				if (gxGrid != null
					&& gxGrid.fixedColVisibleCount != null
					&& $grid.find('>table').data('ColumnsOrder') == null
					&& $grid.find('>table>thead>tr>th:eq(0)')[0].style.width == '') {
					//necesario para que se recalculen correctamente los anchos luego de cambiar el orden de las columnas
					gxGrid.fixedColVisibleCount = null;
					$grid.removeClass('gx-infinite-scrolling-container');
				}
			}
			$grid.parent().addClass('GridParentCell');

			if (this.CategoriesHelper != null && !$grid.is(':visible') && WWP_GetInsertGrid(this).length == 1) {
				this.CategoriesHelper.setGridInsertTitlesCategories();
			}

			if (this.TitleSettingsHelper != null) {
				this.TitleSettingsHelper.updateGrid();
			}

			if (typeof (SetMinWidthTotalizers) === 'function') {
				SetMinWidthTotalizers();
			}

			if (this.CategoriesHelper != null) {
				this.CategoriesHelper.updateGridTitlesCategories(false);
			}

			if (this.isRptStyle == null && $grid.find('>table').data('groupProcessed') == null) {
				var firstRow = $grid.find('>table>tbody>tr:eq(0)');
				var secondRow = $grid.find('>table>tbody>tr:eq(1)');
				if (secondRow.length == 1) {
					//si no hay 2 rows se deja en null para que se recalcule
					this.isRptStyle = firstRow.css('background-color') != secondRow.css('background-color');
				}
			}

			if (this.RowGroupsHelper != null) {
				this.RowGroupsHelper.ProcessGroup(false);
			}

			if (this.PopoversInGrid != '' && this.PopoversInGrid != null) {
				var popoversInGridSplitted = this.PopoversInGrid.split('|');
				for (var i = 0; i < popoversInGridSplitted.length; i += 1) {
					this['WWP.' + popoversInGridSplitted[i].toUpperCase()].InitGridPopovers();
				}
			}

			if (this.ColumnSelectorHelper != null) {
				this.UpdateColumnsOrder();
			} else if (this.FixedColumns != '') {
				this.UpdateFixedColumns();
			}

			//merge title categories and a group by rows cells
			if (this.CategoriesHelper != null) {
				this.CategoriesHelper.mergeTitlesCategoryCells();
			}
			if (this.RowGroupsHelper != null && (this.InfiniteScrolling != 'Grid' || $grid.hasClass('gx-infinite-scrolling-container'))) {
				this.RowGroupsHelper.mergeGroupRowCellsAndAddTitle();
			}

			if (this.ColumnSelectorHelper != null && wwp.settings.columnsSelector.AllowColumnReordering && wwp.settings.columnsSelector.AllowColumnDragging) {
				this.AllowReOrderDraggingColumns();
			}

			ApplyBootstrapSelect();

			this.FreezeColumnsOrHeader();

			if (this.InfiniteScrolling != 'False') {
				var $gridTable = $grid.find('>table');
				var is_ProcessedRecords = $gridTable.data('IS_ProcessedRecords');
				var new_IS_ProcessedRecords = $gridTable.find('>tbody').children().length;

				if (this.InfiniteScrolling == 'Grid' && is_ProcessedRecords != null && new_IS_ProcessedRecords != is_ProcessedRecords) {
					if ($grid.find('>table>tbody>tr[data-gxrendering_row]>td:eq(0)').css('max-width') == 'none') {
						this.InfiniteScrolling_SetWidthToNewCells();
					}
				}
				$gridTable.data('IS_ProcessedRecords', new_IS_ProcessedRecords);

				if (this.InfiniteScrolling == 'Grid') {
					if ($gridTable.find('>tbody').hasClass('gx-infinite-scrolling-element')) {
						this.InfiniteScrolling_FinishedProcessing();
					} else if (this.timerT == null) {
						var thisC = this;
						var maxTicks = 200;
						this.timerT = setInterval(function () {
							if ($gridTable.find('>tbody').hasClass('gx-infinite-scrolling-element')) {
								thisC.InfiniteScrolling_FinishedProcessing();
							} else {
								maxTicks--;
								if (maxTicks < 0 && thisC.timerT != null) {
									clearInterval(thisC.timerT);
									thisC.timerT = null;
								}
							}
						}, 10);
					}
				}
			}

			this.Empowered = true;
		}
	}

	this.InfiniteScrolling_SetWidthToNewCells = function () {
		var $grid = WWP_GetGrid(this);
		$grid.find('>table>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>*').each(function () {
			var index = $(this).index();
			var width = $(this).css('width');
			$grid.find('>table>tbody>tr[data-gxrendering_row]').each(function () {
				$(this).find('>*:eq(' + index + ')').css({ 'width': width, 'max-width': width });
			});
		});
	}

	this.InfiniteScrolling_FinishedProcessing = function () {
		if (this.timerT != null) {
			clearInterval(this.timerT);
			this.timerT = null;
		}
		var $grid = WWP_GetGrid(this);

		var $headTr = $grid.find('>table>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)');
		var $footTr = $grid.find('>table>tfoot>tr:not(.FGFootRow)');
		$grid.find('>table>tbody>tr:not(.DVGroupByRow,.FGRowLine):eq(0)>*').each(function () {
			var index = $(this).index();
			var width = $(this).css('width');
			$footTr.find('>*:eq(' + index + ')').css({ 'min-width': width, 'max-width': width });
			$headTr.find('>*:eq(' + index + ')').css({ 'min-width': width, 'max-width': width });
		});
	}

	this.UpdateFixedColumns = function () {
		var thRow = WWP_GetGrid(this).find('>table').find('>thead>tr:last-child()');
		var fixedColumnsSplitted = this.FixedColumns.split(';');
		var colsCount = thRow.children().length;
		for (var i = 0; i < colsCount; i += 1) {
			if (fixedColumnsSplitted[i] != '') {
				thRow.find('>th:eq(' + i + ')').addClass('ConfigFixedColumn FixedColumn' + fixedColumnsSplitted[i]);
			}
		}
	}

	this.UpdateColumnsOrder = function () {
		if (this.ColumnSelectorHelper.DropDownOptionsData_Columns != null && this.ColumnSelectorHelper.DropDownOptionsData_Columns.length > 0) {
			var grid = WWP_GetGrid(this).find('>table');
			if (grid.data('ColumnsOrder') == null) {
				var isInsertGrid = false;
				if (!grid.is(':visible') && grid.find('>tbody>tr').length == 0) {
					//maybe Grid Insert line is displayed
					var gridAux = WWP_GetInsertGrid(this).find('>table');
					if (gridAux.length == 1) {
						grid = gridAux;
						isInsertGrid = true;
						if (grid.data('ColumnsOrder') != null) {
							return;
						}
					}
				}
				var cols = grid.find('>thead>tr:last-child()>th');
				var currentColIndex = -1;

				var fixedColumnsSplitted = (isInsertGrid ? [] : this.FixedColumns.split(';'));
				var fixedLeftCount = 0, fixedRightCount = 0;
				for (var i = 0; i < cols.length; i += 1) {
					if (i < fixedColumnsSplitted.length && (fixedColumnsSplitted[i] == 'L' || fixedColumnsSplitted[i] == 'R')) {
						$(cols[i]).data('fixed', fixedColumnsSplitted[i]);
						if (fixedColumnsSplitted[i] == 'L') {
							fixedLeftCount++;
						} else {
							fixedRightCount++;
						}
					}
					if ($(cols[i]).hasClass('EditableGridAuxCol') && $(cols[i - 1]).hasClass('DisableColMoving') || $(cols[i]).find('>span>input[name=selectAllCheckbox]').length == 1) {
						$(cols[i]).addClass('DisableColMoving');
					}
					if (!$(cols[i]).is('.DisableColMoving,.EditableGridAuxCol')) {
						var addColumn = false;
						if ($(cols[i]).find('>*').length > 0) {
							if ($(cols[i]).hasClass('WWColumn')) {
								addColumn = true;
							} else {
								//si la columna no tiene ninguna ColumnClass se asume que es porque tiene un control type = custom (por ejemplo Rating)
								//y por bug de gx no queda el columnClass (probado en GX17 U9)
								var $gridTable = $(cols[i]).closest('table');
								if ($gridTable.length == 1 && WWP_replaceAll(cols[i].className, 'gx-tab-padding-fix-1', '').trim() == WWP_replaceAll(WWP_replaceAll($gridTable[0].className, 'table-responsive', ''), 'gx-tab-spacing-fix-2', '').trim() + 'Title') {
									addColumn = true;
								}
							}
						}
						if (addColumn) {
							currentColIndex++;
							$(cols[i]).data('colIndex', currentColIndex);
						} else {
							$(cols[i]).addClass('DisableColMoving');
						}
					}
					if (this.InfiniteScrolling != 'False' || wwp.settings.columnsSelector.AllowColumnResizing) {
						$(cols[i]).data('origColIndex', i);
					}
				}

				if (currentColIndex == this.ColumnSelectorHelper.DropDownOptionsData_Columns.length - 1) {
					var movableFixedLeftCount = this.UpdateColumnsOrder_FixedColumns(grid, cols, true, fixedLeftCount);
					var movableFixedRightCount = this.UpdateColumnsOrder_FixedColumns(grid, grid.find('>thead>tr:last-child()>th'), false, fixedRightCount);
					grid.data('ColumnsOrder', '');
					this.ColumnSelectorHelper.TotalizersCalled = null;
					for (var i = movableFixedLeftCount; i < this.ColumnSelectorHelper.DropDownOptionsData_Columns.length - movableFixedRightCount; i += 1) {
						var minO = Math.min.apply(Math, this.ColumnSelectorHelper.DropDownOptionsData_Columns.map(function (o) { return !o.found && !o.fixed ? o.O : 9999; }))
						for (var j = 0; j < this.ColumnSelectorHelper.DropDownOptionsData_Columns.length; j += 1) {
							if (this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].O == minO && !this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].found) {
								this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].O = i;
								this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].found = true;
								this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].freezable = this.UpdateColumnsOrder_IsColumnFreezable(j, grid);

								break;
							}
						}
					}
					for (var i = 0; i < this.ColumnSelectorHelper.DropDownOptionsData_Columns.length; i += 1) {
						delete this.ColumnSelectorHelper.DropDownOptionsData_Columns[i].found;
						for (var j = 0; j < this.ColumnSelectorHelper.DropDownOptionsData_Columns.length; j += 1) {
							if (this.ColumnSelectorHelper.DropDownOptionsData_Columns[j].O == i) {
								this.ReOrderColumn(grid, j, i);
								break;
							}
						}
					}
				} else {
					console.log('ERROR currentColIndex (' + currentColIndex + ') != this.DropDownOptionsData_Columns.length - 1 (' + (this.ColumnSelectorHelper.DropDownOptionsData_Columns.length - 1) + ')');
				}
				this.DropDownOptionsData_Columns = this.ColumnSelectorHelper.DropDownOptionsData_Columns;
			} else if (this.InfiniteScrolling != 'False') {
				//Infinite scrolling -> reorder new records
				var processedRecords = grid.data('IS_ProcessedRecords');
				if (processedRecords != grid.find('>tbody').children().length) {
					var cols = grid.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)').last().find('th');
					var origColIndexesAux = []
					for (var i = 0; i < cols.length; i += 1) {
						origColIndexesAux.push(i);
					}
					for (var i = 0; i < cols.length; i += 1) {
						var colToMove = $(cols[i]).data('origColIndex');
						var colToMoveRealPosition = origColIndexesAux[colToMove];
						if (i != colToMoveRealPosition) {
							grid.find('>tbody>tr:gt(' + (processedRecords - 1) + ')').each(function () {
								$(this).find('>*:eq(' + colToMoveRealPosition + ')').insertBefore($(this).find('>*:eq(' + i + ')'));
							});
							for (var j = 0; j < cols.length; j++) {
								if (origColIndexesAux[j] >= i && origColIndexesAux[j] < colToMoveRealPosition) {
									origColIndexesAux[j]++;
								}
							}
							origColIndexesAux[colToMove] = i;
						}
					}
				}
			}
		}
	}

	this.GetGxGrid = function () {
		return gx.O.getGridByBaseName(this.ControlName.replace('_empowerer', ''));
	}

	this.UpdateColumnsOrder_IsColumnFreezable = function (colSelecortIndex, grid) {
		var isFreezable = false;
		if (this.TitleSettingsHelper != null) {
			var thisC = this;
			grid.find('>thead>tr:last-child()>th').each(function () {
				if ($(this).data('colIndex') == colSelecortIndex) {
					isFreezable = thisC.TitleSettingsHelper.ColumnIsFreezable(this);
					return false;
				}
			});
		}
		return isFreezable;
	}

	this.UpdateColumnsOrder_FixedColumns = function (grid, cols, isLeft, fixedCount) {
		var movableFixedCount = 0;
		var f = (isLeft ? 0 : cols.length - 1);
		while (f >= 0 && f <= cols.length - 1 && $(cols[f]).hasClass('DisableColMoving')) {
			f += (isLeft ? 1 : -1);
		}
		var sideValue = isLeft ? 'L' : 'R';
		var thisC = this;
		if (fixedCount > 0) {
			//move 'design time' fixed columns
			for (var i = 0; i < cols.length; i += 1) {
				var itemPos = (isLeft ? i : cols.length - 1 - i);
				var fixed = $(cols[itemPos]).data('fixed');
				if (fixed == sideValue) {
					var colSelIndex = $(cols[itemPos]).data('colIndex');
					if (colSelIndex != null) {
						this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].O = (isLeft ? movableFixedCount : this.ColumnSelectorHelper.DropDownOptionsData_Columns.length - movableFixedCount - 1);
						if (isLeft) {
							this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].configFixedL = true;
						} else {
							this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].configFixedR = true;
						}
						this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].found = true;
						movableFixedCount++;
					}

					grid.find('>thead>tr:last-child()>th:eq(' + itemPos + ')').addClass('ConfigFixedColumn FixedColumn' + sideValue);
					if (isLeft && itemPos > f || !isLeft && itemPos < f) {
						grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
							thisC.UpdateColumnsOrder_FixedColumns_insert($(this).find('>*:eq(' + itemPos + ')'), $(this).find('>*:eq(' + f + ')'), isLeft);
						});
						if ($(cols[itemPos + 1]).hasClass('EditableGridAuxCol')) {
							grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
								thisC.UpdateColumnsOrder_FixedColumns_insert($(this).find('>*:eq(' + (itemPos + (isLeft ? 1 : 0)) + ')'), $(this).find('>*:eq(' + (f + (isLeft ? 1 : 0)) + ')'), isLeft);
							});
							f += (isLeft ? 1 : -1);
						}
						f += (isLeft ? 1 : -1);
					}
					fixedCount--;
					if (fixedCount == 0) {
						break;
					}
				}
			}
		}
		//move 'runtime' fixed columns
		while (true) {
			var searchO;
			if (isLeft) {
				searchO = Math.min.apply(Math, this.ColumnSelectorHelper.DropDownOptionsData_Columns.map(function (o) { return !o.found && o.F == sideValue ? o.O : 9999; }));
			} else {
				searchO = Math.max.apply(Math, this.ColumnSelectorHelper.DropDownOptionsData_Columns.map(function (o) { return !o.found && o.F == sideValue ? o.O : -1; }));
			}
			if (searchO == (isLeft ? 9999 : -1)) {
				break;
			}
			cols = grid.find('>thead>tr:last-child()>th');
			for (var i = 0; i < cols.length; i += 1) {
				var itemPos = (isLeft ? i : cols.length - 1 - i);
				var colSelIndex = $(cols[itemPos]).data('colIndex');
				if (colSelIndex != null
					&& !this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].found
					&& this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].F == sideValue
					&& this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].O == searchO) {
					this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].O = (isLeft ? movableFixedCount : this.ColumnSelectorHelper.DropDownOptionsData_Columns.length - movableFixedCount - 1);
					if (isLeft) {
						this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].fixedL = true;
					} else {
						this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].fixedR = true;
					}
					this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].found = true;
					this.ColumnSelectorHelper.DropDownOptionsData_Columns[colSelIndex].freezable = true;
					movableFixedCount++;

					grid.find('>thead>tr:last-child()>th:eq(' + itemPos + ')').addClass('FixedColumn' + sideValue);
					grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
						thisC.UpdateColumnsOrder_FixedColumns_insert($(this).find('>*:eq(' + itemPos + ')'), $(this).find('>*:eq(' + f + ')'), isLeft);
					});
					if ($(cols[itemPos + 1]).hasClass('EditableGridAuxCol')) {
						grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
							thisC.UpdateColumnsOrder_FixedColumns_insert($(this).find('>*:eq(' + (itemPos + (isLeft ? 1 : 0)) + ')'), $(this).find('>*:eq(' + (f + (isLeft ? 1 : 0)) + ')'), isLeft);
						});
						f += (isLeft ? 1 : -1);
					}
					f += (isLeft ? 1 : -1);
					break;
				}
			}
		}
		return movableFixedCount;
	}

	this.UpdateColumnsOrder_FixedColumns_insert = function (sel, insertTarget, isBefore) {
		if (isBefore) {
			sel.insertBefore(insertTarget);
		} else {
			sel.insertAfter(insertTarget);
		}
	}

	this.AllowReOrderDraggingColumns = function () {
		var $gridTable = WWP_GetGrid(this).find('>table');
		if ($gridTable.data('ColumnsOrder') != null && $gridTable.data('ColumnsDragging') == null) {
			$gridTable.data('ColumnsDragging', '');
			var $lastOverElem = null;
			var $origDraggingCell = null;
			var origDraggingCellIndex = -1;
			var thisC = this;
			$gridTable.find('>thead>tr>th:not(.DisableColMoving,.ConfigFixedColumn)').each(function () {
				$(this).addClass('WWPDraggable');
				this.draggable = true;
				$(this).off('dragstart').on("dragstart", function (event) {
					event.stopPropagation();
					$origDraggingCell = $(this);
					origDraggingCellIndex = $origDraggingCell.index();
					//$origDraggingCell.css('width', '100px');
					$origDraggingCell.addClass('WWPDraggedColumn');
					$origDraggingCell.parent().addClass('WWPDraggingColumn');
					var dragOverSelector;
					if ($origDraggingCell.hasClass('FixedColumnL')) {
						dragOverSelector = '>th.FixedColumnL:not(.DisableColMoving,.ConfigFixedColumn)';
					} else if ($origDraggingCell.hasClass('FixedColumnR')) {
						dragOverSelector = '>th.FixedColumnR:not(.DisableColMoving,.ConfigFixedColumn)';
					} else {
						dragOverSelector = '>th:not(.DisableColMoving,.ConfigFixedColumn,.FixedColumnL,.FixedColumnR)';
					}
					$origDraggingCell.parent().find(dragOverSelector).on("dragover", function (event) {
						if ($lastOverElem != null) {
							$lastOverElem.removeClass('WWPDragOverUp WWPDragOverDown');
						}
						$lastOverElem = $(event.currentTarget);
						var currentCellIndex = $lastOverElem.index();

						if (origDraggingCellIndex != currentCellIndex) {
							var isUp = (origDraggingCellIndex > currentCellIndex);
							$lastOverElem.addClass('WWPDragOver' + (isUp ? 'Up' : 'Down'));
							event.preventDefault();
						}
						requestAnimationFrame(function () {
							$origDraggingCell.parent().addClass('WWPDraggingColumnMoving');
						});
					}).on("drop", function (event) {
						thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux = JSON.parse(JSON.stringify(thisC.ColumnSelectorHelper.DropDownOptionsData, null, 2));
						if (thisC.DropDownOptionsData_Columns != null) {
							thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns = JSON.parse(JSON.stringify(thisC.DropDownOptionsData_Columns, null, 2));
						}

						var movingCategories = $lastOverElem.hasClass('GridGroupTitle');
						if (movingCategories) {
							var dragCatStartIndex = 0;
							var $elem = $origDraggingCell.parent().children().first();
							while ($elem[0] != $origDraggingCell[0]) {
								if ($elem.is(':visible')) {
									dragCatStartIndex += ($elem[0].colSpan != null ? $elem[0].colSpan : 1);
								}
								$elem = $elem.next();
							}
							var dropCatStartIndex = 0;
							$elem = $origDraggingCell.parent().children().first();
							while ($elem[0] != $lastOverElem[0]) {
								if ($elem.is(':visible')) {
									dropCatStartIndex += ($elem[0].colSpan != null ? $elem[0].colSpan : 1);
								}
								$elem = $elem.next();
							}
							if (dragCatStartIndex < dropCatStartIndex) {
								dropCatStartIndex += ($lastOverElem[0].colSpan != null ? $lastOverElem[0].colSpan : 1) - 1;
							}
							var dragCatTotalItems = $origDraggingCell[0].colSpan != null ? $origDraggingCell[0].colSpan : 1;
							var $columnNewPosition = $lastOverElem.closest('thead').find('>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:visible:eq(' + dropCatStartIndex + ')');
							while ($columnNewPosition.length == 1 && $columnNewPosition.is('.DisableColMoving, :not(:visible)')) {
								//este caso se da cuando hay acciones fijas en los extremos con la misma categoria que columnas de datos por lo que su titulo de categoria aparece mergeado
								$columnNewPosition = (dragCatStartIndex < dropCatStartIndex ? $columnNewPosition.prev() : $columnNewPosition.next());
							}
							var newPositionItemOrderIndex = thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[$columnNewPosition.data('colIndex')].O;
							for (var i = 0; i < dragCatTotalItems; i++) {
								var $columnToMove = $origDraggingCell.closest('thead').find('>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:visible:eq(' + (dragCatStartIndex + (dragCatStartIndex < dropCatStartIndex ? i : (dragCatTotalItems - i - 1))) + ')');
								if (!$columnToMove.hasClass('DisableColMoving')) {
									var currentOrderIndex = thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[$columnToMove.data('colIndex')].O;
									thisC.ColumnSelectorHelper.my_dropDownOptions.updateColumnsSelectorValuesIndexByOrderIndexes(currentOrderIndex, newPositionItemOrderIndex);
								}
							}
						} else {
							var currentOrderIndex = thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[$origDraggingCell.data('colIndex')].O;
							var newPositionItemOrderIndex = thisC.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[$lastOverElem.data('colIndex')].O;
							thisC.ColumnSelectorHelper.my_dropDownOptions.updateColumnsSelectorValuesIndexByOrderIndexes(currentOrderIndex, newPositionItemOrderIndex);
						}

						thisC.ColumnSelectorHelper.my_dropDownOptions.updateColumnSelectorDataBeforeGxEvent();
						if (thisC.ColumnSelectorHelper.OnColumnsChanged) {
							thisC.ColumnSelectorHelper.OnColumnsChanged();
						}
					});
					$origDraggingCell.on("dragend", function (event) {
						if ($lastOverElem != null) {
							$lastOverElem.removeClass('WWPDragOverUp WWPDragOverDown');
						}
						$origDraggingCell.removeClass('WWPDraggedColumn').parent().removeClass('WWPDraggingColumn WWPDraggingColumnMoving').find('>th').off('dragend dragover drop');
					});
				});
			});
		}
	}
	this.ReOrderColumn = function (grid, origIndex, newColIndex) {
		var currentColIndex = -1;
		var newColRealIndex = -1;
		var cols = grid.find('>thead>tr:last-child()>th');
		for (var i = 0; i < cols.length; i += 1) {
			if (!$(cols[i]).is('.DisableColMoving,.EditableGridAuxCol')) {
				currentColIndex++;
				if (currentColIndex == newColIndex) {
					newColRealIndex = i;
					if ($(cols[i]).data('colIndex') == origIndex) {
						//already in position
						break;
					}
				} else if ($(cols[i]).data('colIndex') == origIndex) {
					if (newColRealIndex == -1) {
						//already in position
						break;
					}

					if (!this.ColumnSelectorHelper.TotalizersCalled && typeof (SetMinWidthTotalizers) === 'function') {
						this.ColumnSelectorHelper.TotalizersCalled = true;
						SetMinWidthTotalizers();
					}

					grid.data('colReordered', 'T');
					grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
						$(this).find('>*:eq(' + i + ')').insertBefore($(this).find('>*:eq(' + newColRealIndex + ')'));
					});
					if ($(cols[i + 1]).hasClass('EditableGridAuxCol')) {
						grid.find('>tbody>tr,>thead>tr,>tfoot>tr').each(function () {
							$(this).find('>*:eq(' + (i + 1) + ')').insertBefore($(this).find('>*:eq(' + (newColRealIndex + 1) + ')'));
						});
					}
					break;
				}
			}
		}
	}
	this.FixColumn = function (colIndex, isLeft) {
		var fixVal = (isLeft ? 'L' : 'R')
		if (this.ColumnSelectorHelper != null) {
			this.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux = JSON.parse(JSON.stringify(this.ColumnSelectorHelper.DropDownOptionsData, null, 2));
			if (this.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[colIndex].F == fixVal) {
				this.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[colIndex].F = '';
			} else {
				this.ColumnSelectorHelper.my_dropDownOptions.ColumnsSelectorData_Aux.Columns[colIndex].F = fixVal;
			}
			this.ColumnSelectorHelper.my_dropDownOptions.updateColumnSelectorDataBeforeGxEvent();
			if (this.ColumnSelectorHelper.OnColumnsChanged) {
				this.ColumnSelectorHelper.OnColumnsChanged();
			}
		}
	}

	this.FreezeColumnsOrHeader = function () {
		var grid = WWP_GetGrid(this).find('>table');
		var $gridContainerDiv = grid.closest("div");
		$gridContainerDiv.width('auto');

		if (grid.length == 1) {
			//grid.closest('.HasGridEmpowerer').addClass('GridFixedTitles');
			//grid.closest('.HasGridEmpowerer').addClass('GridFixedColumnBorders');
			var hasFixedTitles = grid.closest('.gx-grid').parent().closest('.HasGridEmpowerer,.gx-grid').hasClass('GridFixedTitles');
			if (hasFixedTitles) {
				grid.closest('.gx-grid').addClass('gx-gridGridFixedTitles');
			}
			var hasVerticalBorders = grid.closest('.gx-grid').parent().closest('.HasGridEmpowerer,.gx-grid').hasClass('GridFixedColumnBorders');
			if (hasVerticalBorders) {
				grid.closest('.gx-grid').addClass('gx-gridGridFixedColumnBorders');
			}
			var hasResizableTitles = wwp.settings.columnsSelector.AllowColumnResizing && this.InfiniteScrolling != 'Grid';
			if (hasResizableTitles) {
				grid.closest('.gx-grid').addClass('gx-gridGridResizableTitles');
			}
			var canFreezeColumn = grid.find('>thead>tr>th.FixedColumnR,>thead>tr>th.FixedColumnL').length > 0;

			if (hasFixedTitles && grid.find('>thead>tr.GridGroupTitleRow').length > 0) {
				console.error('WorkWithPlus Error: a grid cannot have "Grouped titles" and "Fixed Titles" at the same time.');
				grid.closest('.GridFixedTitles').removeClass('GridFixedTitles');
				grid.closest('.gx-gridGridFixedTitles').removeClass('gx-gridGridFixedTitles');
				hasFixedTitles = false;
			}

			if (hasFixedTitles || canFreezeColumn || hasResizableTitles) {
				if (grid.data('Empowered') == null) {
					grid.data('Empowered', '');
					var rightColumns = 0;
					var firstFixedRightColumn;
					if (canFreezeColumn) {
						firstFixedRightColumn = grid.find('>thead>tr:last-child()>th.FixedColumnR:visible').first().index();
						if (firstFixedRightColumn != -1) {
							rightColumns = grid.find('>thead>tr:last-child()>th').length - firstFixedRightColumn;
						}
					}
					var defWidths = null;
					if (hasResizableTitles) {
						defWidths = WWPEmpowerGrid_UpdateGrdiColumnsMinWidth(grid, hasFixedTitles, hasResizableTitles);
					}
					var leftColumns = 0;
					if (hasFixedTitles || canFreezeColumn) {
						var gridName = grid.attr('id').replace('ContainerTbl', '').toUpperCase();
						grid.addClass('ColsFixed');
						var leftColumns = grid.find('>thead>tr>th.FixedColumnL:visible').last().index() + 1;
						grid.find('>*>tr>*').css('left', '');
						grid.parent().off('scroll');
						this.ColumnsOrHeaderFixer = new ColumnsOrHeaderFixer(grid, {
							left: leftColumns,
							right: rightColumns,
							hasFixedTitles: hasFixedTitles,
							hasResizableTitles: hasResizableTitles,
							isRptStyle: this.isRptStyle
						});
					}
					var thisC = this;
					if (hasResizableTitles) {
						grid.find('>thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th, >thead>tr.FGHeaderRowV>th>div').off('mousemove').on("mousemove", function (event) {
							if (event.buttons != 1) {
								grid.find(">thead").removeData('wwpMoving');
							}
							var wwpMovingIndex = grid.find(">thead").data('wwpMoving');
							var colResize = (wwpMovingIndex != null || GetResizedItem(event) != null);
							$(this).css('cursor', colResize ? 'col-resize' : '');
							if (colResize) {
								$(this).parent().find('>.WWPDraggable').each(function () {
									this.draggable = false;
								});
								if (window.getSelection
									&& window.getSelection().focusNode != null
									&& $(window.getSelection().focusNode).closest('ul,tr')[0] == $(this).closest('tr')[0]) {
									if (window.getSelection().empty) {  // Chrome
										window.getSelection().empty();
									} else if (window.getSelection().removeAllRanges) {  // Firefox
										window.getSelection().removeAllRanges();
									}
								}
							} else {
								$(this).parent().find('>.WWPDraggable').each(function () {
									this.draggable = ($(this).find('.ColumnSettings.open').length == 0);
								});
							}
							if (wwpMovingIndex != null) {
								var tdItem = grid.find(">tbody>tr:visible:not(.DVGroupByRow,.FGRowLine):eq(0)>td:eq(" + wwpMovingIndex + ")");
								var newMinWidth = (event.pageX - tdItem.offset().left);
								var nextVisibleCol = tdItem.next();
								while (nextVisibleCol.css('display') == 'none') {
									nextVisibleCol = nextVisibleCol.next();
								}
								var isLastVisibleCol = (nextVisibleCol.length == 0);
								if (isLastVisibleCol && newMinWidth >= tdItem.outerWidth() - 2) {
									newMinWidth += 10;
								}
								if (newMinWidth > 20 && newMinWidth > tdItem.width() && leftColumns + rightColumns > 0) {
									var lastFixedLeftColumn = (leftColumns > 0 ? grid.find('>thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th:eq(' + (leftColumns - 1) + ')') : null);
									var fixedLeftColumnsWidth = (leftColumns > 0 ? lastFixedLeftColumn.offset().left + lastFixedLeftColumn.outerWidth() - grid.parent().offset().left : 0);
									var firstFixedRightColumn = (rightColumns > 0 ? grid.find('>thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th:eq(' + (grid.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th').length - rightColumns) + ')') : null);
									var fixedRightColumnsWidth = (rightColumns > 0 ? grid.parent().offset().left + grid.parent().outerWidth() - firstFixedRightColumn.offset().left : getScrollWidthIfVisible(grid.parent(), true));
									var scrollableDistance = grid.parent().outerWidth() - fixedLeftColumnsWidth - fixedRightColumnsWidth;

									var newMinWidthMax = tdItem.outerWidth() + scrollableDistance - 20;
									if (newMinWidth > newMinWidthMax) {
										newMinWidth = 0;
									}
								}
								if (newMinWidth > 20) {
									var tdsSelector = grid.find('>tbody>tr:visible:not(.DVGroupByRow,.FGRowLine):eq(0)>td:visible');
									tdsSelector.each(function () {
										$(this).data('dvMW', $(this).outerWidth());
									});
									tdsSelector.each(function () {
										$(this).css({ 'min-width': $(this).data('dvMW') + 'px' });
										$(this).removeData('dvMW');
									});
									tdItem.css({ 'min-width': newMinWidth + 'px' });
									if (defWidths == null) {
										defWidths = [];
										grid.find('>tbody>tr:not(.DVGroupByRow,.FGRowLine):eq(0)>td').each(function () {
											defWidths.push(-1);
										});
									}
									if (typeof window['SetMinWidthPaginationBar'] === "function") {
										SetMinWidthPaginationBar(grid[0]);
									}
									if (isLastVisibleCol) {
										grid.parent().scrollLeft(grid.outerWidth());
									}
									if (thisC.ColumnsOrHeaderFixer != null) {
										if (thisC.ColumnsOrHeaderFixer.settings.firstNotFixedLeft_addedPadding != null && thisC.ColumnsOrHeaderFixer.settings.firstNotFixedLeft == tdItem.index()) {
											newMinWidth = (newMinWidth - thisC.ColumnsOrHeaderFixer.settings.firstNotFixedLeft_addedPadding);
											thisC.ColumnsOrHeaderFixer.settings.firstNotFixedLeft_minWidth = newMinWidth;
										}
										else if (thisC.ColumnsOrHeaderFixer.settings.lastNotFixedRight_minWidth != null && thisC.ColumnsOrHeaderFixer.settings.lastNotFixedRight == tdItem.index()) {
											thisC.ColumnsOrHeaderFixer.settings.lastNotFixedRight_minWidth = newMinWidth;
										}
										thisC.ColumnsOrHeaderFixer.Refresh();
									}
									var origIndex = grid.find('>thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th:eq(' + wwpMovingIndex + ')').data('origColIndex');

									defWidths[origIndex != null ? origIndex : wwpMovingIndex] = newMinWidth;
									var cookieName = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1).replace('.aspx', '') + '_' + grid.attr('id').replace('ContainerTbl', '');
									createCookie(cookieName, WWP_replaceAll(JSON.stringify(defWidths, null, 2), '\n', ''));
									////if (canFreezeColumn && rightColumns || (hasFixedTitles && grid.find('>tfoot>tr:not(.FGFootRow)>td:visible').first().css('top') != '')) {
									////	//WWP_FreezeColumnExtension.updateRightColumns(grid, rightColumns);
									////}
									////if (!IsChrome() && grid.find('>tfoot>tr:not(.FGFootRow)>td:visible').length > 0) {
									////	grid.parent().trigger('scroll');
									////}
								}
							}
						}).off('mousedown').on("mousedown", function (event) {
							var resizedItem = GetResizedItem(event);
							if (resizedItem != null) {
								grid.find(">thead").data('wwpMoving', resizedItem.index());

								grid.find(">thead").off('mouseup').on("mouseup", function (event) {
									grid.find(">thead").off('mouseleave').off('mouseup').removeData('wwpMoving');
								});
								grid.find(">thead").off('mouseleave').on("mouseleave", function (event) {
									grid.find(">thead").off('mouseleave').off('mouseup').removeData('wwpMoving');
								});
							}
						});
					}
				} else {
					if (this.ColumnsOrHeaderFixer != null) {
						if (this.InfiniteScrolling != 'False' && grid.data('IS_ProcessedRecords') != grid.find('>tbody').children().length) {
							this.ColumnsOrHeaderFixer.UpdateInfiniteScrollingNewRecords();
						} else {
							//this.ColumnsOrHeaderFixer.Refresh();
						}
					}
				}
			}
			if (hasFixedTitles || canFreezeColumn) {
				if (grid.closest('.gx-ct-body').is('.entering, .leaving')) {
					var thisC = this;
					window.setTimeout(function () {
						thisC.FreezeColumnsOrHeader();
					}, 100);
				} else {
					grid.parent().trigger('scroll');
				}
			}
		}
	}
}

function WWPEmpowerGrid_UpdateGrdiColumnsMinWidth(grid, hasFixedTitles, hasResizableTitles) {
	var defWidths = null;
	if (hasResizableTitles) {
		var cookieName = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1).replace('.aspx', '') + '_' + grid.attr('id').replace('ContainerTbl', '');
		defWidths = readCookie(cookieName);
		if (defWidths != null) {
			defWidths = JSON.parse(defWidths);
			if (defWidths.length != grid.find('>tbody>tr:visible:not(.DVGroupByRow):eq(0)>td').length) {
				defWidths = null;
			}
		}
	}

	if (defWidths != null || hasFixedTitles) {
		if (hasFixedTitles) {
			grid.find('>thead>tr>th').css({ 'min-width': '0px' });
			grid.find('>tfoot>tr>td').css({ 'min-width': '0px' });
		}
		var cols = grid.find('>thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th');
		grid.find('>tbody>tr:visible:not(.DVGroupByRow):eq(0)>td').each(function () {
			var tdMinWidth = -1;
			if (defWidths != null) {
				var colIndex = $(this).index();
				var origIndex = cols.length > colIndex ? $(cols[colIndex]).data('origColIndex') : null;
				colIndex = origIndex != null ? origIndex : colIndex;
				if (defWidths[colIndex] > tdMinWidth) {
					tdMinWidth = defWidths[colIndex];
				}
			}
			if (tdMinWidth != -1) {
				$(this).css({ 'min-width': tdMinWidth + 'px' });
			}
		});
	}
	return defWidths;
}

function GetResizedItem(event) {
	if (event.target != null) {
		var eventTarget = event.target.tagName == 'TH' ? $(event.target) : $(event.target).closest('th,ul');
		if (eventTarget.length == 1 && eventTarget[0].tagName == 'TH' && eventTarget.parent().parent().next().children().length > 0) {
			var x = event.pageX - eventTarget.offset().left;
			var leftPixelsForMove = 3;
			if (x <= leftPixelsForMove || x >= eventTarget.outerWidth() - 4) {
				if (eventTarget.parent().hasClass('FGHeaderRowV')) {
					//vertical line
					if (event.offsetY < eventTarget.closest('thead').find('>tr:not(.FGHeaderRowH,.FGHeaderRowV)').last().height()) {
						if (eventTarget.hasClass('FixedR')) {
							eventTarget = eventTarget.prev();
							while (eventTarget.css('display') == 'none') {
								eventTarget = eventTarget.prev();
							}
						}
					} else {
						return null;
					}
				} else {
					if (x <= leftPixelsForMove) {
						eventTarget = eventTarget.prev();
						while (eventTarget.css('display') == 'none') {
							eventTarget = eventTarget.prev();
						}
					}
				}
				return eventTarget.length == 1 ? eventTarget : null;
			}
		}
	}
	return null;
}

function ColumnsOrHeaderFixer($table, param) {

	this.bindScrollEvent = function () {
		var parent = this.settings.$parent;
		var table = this.settings.$table;

		var thisC = this;

		if (this.settings.hasFixedTitles) {
			parent.data('lastScrollLeft', 0);
			parent.data('lastScrollTop', 0);
			parent.scroll(function () {
				if (thisC.settings.$table.find(">thead").data('wwpMoving') != null) {
					thisC.updateAllRelative();
				} else {
					var lastScrollLeft = parent.data('lastScrollLeft');
					var lastScrollTop = parent.data('lastScrollTop');

					var scrollLeft = parent.scrollLeft();
					var scrollTop = parent.scrollTop();

					parent.data('lastScrollLeft', scrollLeft);
					parent.data('lastScrollTop', scrollTop);

					thisC.propareToScroll(parent, lastScrollLeft, lastScrollTop, scrollLeft, scrollTop);
				}
			});
			parent.off('mousewheel mousedown keydown').on('mousewheel mousedown keydown', function (e) {
				if (e.type == 'mousewheel' && e.originalEvent != null && e.originalEvent.wheelDelta != null) {
					var scrollTop = parent.scrollTop();
					thisC.propareToScroll(parent, null, scrollTop - 1, null, scrollTop);
				} else if (e.type == 'keydown') {
					if (e.keyCode == 37 || e.keyCode == 39) {
						//left or right
						var scrollLeft = parent.scrollLeft();
						thisC.propareToScroll(parent, scrollLeft - 1, null, scrollLeft, null);
					} else if (e.keyCode >= 32 && e.keyCode <= 40) {
						//up or down
						var scrollTop = parent.scrollTop();
						thisC.propareToScroll(parent, null, scrollTop - 1, null, scrollTop);
					}
				} else if (e.type == 'mousedown' && $(event.target).hasClass('gx-grid')) {
					if (event.offsetY >= parent.outerHeight() - getScrollWidthIfVisible(parent, false)) {
						var scrollLeft = parent.scrollLeft();
						thisC.propareToScroll(parent, scrollLeft - 1, null, scrollLeft, null);
					} else if (event.offsetX >= parent.outerWidth() - getScrollWidthIfVisible(parent, true)) {
						var scrollTop = parent.scrollTop();
						thisC.propareToScroll(parent, null, scrollTop - 1, null, scrollTop);
					}
				}
			});
		}
	}

	this.propareToScroll = function (parent, lastScrollLeft, lastScrollTop, scrollLeft, scrollTop) {
		//to avoid flickering:
		//when scrolling Horizontal -> make frozen columns absolute
		//when scrolling Vertical -> make fixed title and footer absolute
		if (this.settings.myT != null) {
			clearInterval(this.settings.myT);
			this.settings.myT = null;
		}
		scrollLeft = (scrollLeft != null ? scrollLeft : parent.scrollLeft());
		scrollTop = (scrollTop != null ? scrollTop : parent.scrollTop());
		lastScrollLeft = (lastScrollLeft != null ? lastScrollLeft : scrollLeft);
		lastScrollTop = (lastScrollTop != null ? lastScrollTop : scrollTop);
		if (lastScrollLeft != scrollLeft) {
			if (parent.data('isScrollingH') == null) {
				parent.data('isScrollingV', null);
				parent.data('isScrollingH', 'T');
				this.updateAllRelative();
				this.updateFrozenColumnsAbsolute();
			}
		} else if (lastScrollTop != scrollTop) {
			if (parent.data('isScrollingV') == null && this.settings.hasFixedTitles) {
				parent.data('isScrollingH', null);
				parent.data('isScrollingV', 'T');
				this.updateAllRelative();
				this.updateTitlesAbsolute();
			}
		}
		if (scrollLeft != parent.scrollLeft()) {
			//IE fix
			parent.scrollLeft(scrollLeft);
		}
		if (scrollTop != parent.scrollTop()) {
			//IE fix
			parent.scrollTop(scrollTop);
		}
		var thisC = this;
		this.settings.myT = setInterval(function () {
			clearInterval(thisC.settings.myT);
			thisC.settings.myT = null;
			thisC.unprepareScroll(parent);
		}, 300);
	}
	this.unprepareScroll = function (parent) {
		//to test:
		//$('#GridContainerDiv').data('WWP.Empowerer').ColumnsOrHeaderFixer.settings.unprepare = false
		if (this.settings.unprepare) {
			var scrollLeft = parent.scrollLeft();
			var scrollTop = parent.scrollTop();

			parent.data('isScrollingV', null);
			parent.data('isScrollingH', null);
			this.updateAllRelative();

			if (scrollLeft != parent.scrollLeft()) {
				//IE fix
				parent.scrollLeft(scrollLeft);
			}
			if (scrollTop != parent.scrollTop()) {
				//IE fix
				parent.scrollTop(scrollTop);
			}
		}
	}

	this.updateAllRelative = function () {
		WWP_Debug_Log(false, 'updateAllRelative');
		var parent = this.settings.$parent;
		var grid = this.settings.$table;

		var left = parent.scrollLeft();
		var scrollTop = parent.scrollTop();

		if (this.settings.left > 0) {
			grid.find('>tbody>tr:not(.FGRowLine,.WCD_tr)>td:nth-child(' + (this.settings.firstNotFixedLeft + 1) + '), >thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th:nth-child(' + (this.settings.firstNotFixedLeft + 1) + '), >tfoot>tr:not(.FGFootRow)>td:nth-child(' + (this.settings.firstNotFixedLeft + 1) + ')').css({
				'padding-left': ''
			});
			if (this.settings.hasGroupTitles) {
				grid.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + this.settings.firstNotFixedLeft + ']').css({ 'padding-left': '' });
			}
			if (this.settings.firstNotFixedLeft_minWidth != null) {
				grid.find(this.settings.S_RowCells + ':nth-child(' + (this.settings.firstNotFixedLeft + 1) + '):eq(0)').css({
					'min-width': this.settings.firstNotFixedLeft_minWidth
				});
				this.settings.firstNotFixedLeft_minWidth = null;
				this.settings.firstNotFixedLeft_addedPadding = null;
			}
		}
		if (this.settings.hasRowGroups && this.settings.left + this.settings.right > 0) {
			grid.find('>tbody>tr.DVGroupByRow>td').css({ 'padding-top': '' });
		}

		if (this.settings.right > 0 && this.settings.lastNotFixedRight_minWidth != null) {
			grid.find(this.settings.S_RowCells + ':nth-child(' + (this.settings.lastNotFixedRight + 1) + '):eq(0)').css({
				'min-width': this.settings.lastNotFixedRight_minWidth
			});
			this.settings.lastNotFixedRight_minWidth = null;
		}

		grid.find(this.settings.S_Rows).css('height', '');
		grid.find('>thead>tr:not(.FGHeaderRowH)>th, >tfoot>tr:not(.FGFootRow)>td').css('width', '');
		this.updateFixedTitlesRelative(scrollTop);

		grid.find('>thead>tr:not(.FGHeaderRowH)>th,>tfoot>tr:not(.FGFootRow)>td').css({ 'left': '', 'visibility': '', 'right': '', 'padding-top': '' });

		if (this.settings.left > 0) {
			this.settings.leftColumns.css({ "left": left, 'position': 'relative' });
		}

		for (var colIndex = 0; colIndex < this.settings.left; colIndex++) {
			grid.find(this.settings.S_RowCells + ':nth-child(' + (colIndex + 1) + ')').css({ 'top': '', 'visibility': '', 'padding-top': '' }).removeClass('FGCuttedCell');
		}

		for (var colIndex = this.settings.firstRightColumn; colIndex < this.settings.totalColumns; colIndex++) {
			grid.find(this.settings.S_RowCells + ':nth-child(' + (colIndex + 1) + ')').css({ 'top': '', 'visibility': '', 'padding-top': '' }).removeClass('FGCuttedCell');
		}

		if (this.hasRowGroupsWithTot && !this.settings.hasFixedTitles) {
			grid.find('>tbody>tr.DVGroupByRow>td[titleCell]').css('position', 'absolute');
		}

		var gridWidth = null;
		var right = null;
		if (this.settings.right > 0 || this.settings.hasRowGroups) {
			gridWidth = grid.outerWidth();
			var right = gridWidth - this.tableAvailableWidthForGrid() - left;
			if (right < 0) {
				//console.log('fixing right, probable bug');
				right = 0;
			}
		}
		if (this.settings.right > 0) {
			this.settings.rightColumns.css({ 'right': right, 'position': 'relative', 'left': '' });

			grid.find('>tbody>tr:not(.FGRowLine,.WCD_tr)>td:nth-child(' + (this.settings.lastNotFixedRight + 1) + '), >thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th:nth-child(' + (this.settings.lastNotFixedRight + 1) + '), >tfoot>tr:not(.FGFootRow)>td:nth-child(' + (this.settings.lastNotFixedRight + 1) + ')').css({
				'padding-right': ''
			});
			if (this.settings.hasGroupTitles) {
				if (grid.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + this.settings.lastNotFixedRight + ']').css({ 'padding-right': '' }).length == 0) {

					//first right not fixed is grouped
					var colIndexAux = this.settings.lastNotFixedRight - 1;
					while (colIndexAux >= 0 && grid.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + colIndexAux + ']').length == 0) {
						colIndexAux--;
					}
					grid.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + colIndexAux + ']').css({ 'padding-right': '' });
				}
			}
		}
		if (this.settings.hasVerticalBorders) {
			var verticalLinesHeight = grid.parent().outerHeight() - getScrollWidthIfVisible(grid.parent(), false) - parseInt(grid.css('margin-top')) - parseInt(grid.parent().css('border-bottom-width')) - parseInt(grid.parent().css('border-top-width')) - 1;
			var verticalLinesTop = grid.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)').last().outerHeight() * -1;
			var $expandedRow;
			if (this.settings.hasExpandedWC) {
				$expandedRow = grid.find('>tbody>tr.WCD_tr');
				if ($expandedRow.length == 1) {
					verticalLinesHeight = $expandedRow.position().top;
				} else {
					delete this.settings.hasExpandedWC;
				}
			}
			this.updateVerticalBordersCss(verticalLinesTop, verticalLinesHeight);
		}

		if (this.settings.hasRowGroups) {
			if (this.settings.hasFixedTitles) {
				if (this.settings.left + this.settings.right > 0) {
					//este caso por ahora no se permite, dejo esta linea para cuando lo retomemos
					grid.find('>tbody>tr.DVGroupByRow>td[titleCell]').css({ "left": left, 'position': 'relative', 'top': 'inherit' });
					//TODO:revisar padding-right;
				}
			} else {
				grid.find('>tbody>tr.DVGroupByRow>td[titleCell]').css({ "padding-left": left, "padding-right": (this.DVGroupByRowOriginalPaddingR != null ? right + this.DVGroupByRowOriginalPaddingR : '') });
			}
		}

		this.updateFixedTitlesRelative(scrollTop);

		if (gridWidth != null && gridWidth != this.settings.$table.outerWidth()) {
			gridWidth = grid.outerWidth();
			var right = gridWidth - this.tableAvailableWidthForGrid() - left;
			if (right < 0) {
				//console.log('fixing right, probable bug');
				right = 0;
			}
			this.settings.rightColumns.css({ 'right': right });
		}
	}

	this.updateVerticalBordersCss = function (verticalLinesTop, verticalLinesHeight) {
		var grid = this.settings.$table;
		if (this.settings.hasGroupTitles) {
			var groupRowHeight = grid.find('>thead>tr.GridGroupTitleRow').outerHeight();
			grid.find('>thead>tr.FGHeaderRowV>th>div').each(function () {
				var indexToSearch;
				if ($(this).parent().hasClass('FixedR')) {
					indexToSearch = $(this).parent().index();
				} else {
					var $nextVisible = $(this).parent().next();
					while ($nextVisible.length == 1 && !$nextVisible.is(':visible')) {
						$nextVisible = $nextVisible.next();
					}
					indexToSearch = $nextVisible.index();
				}
				if (grid.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + indexToSearch + ']').length > 0) {
					if (verticalLinesTop != null) {
						$(this).css({ 'top': verticalLinesTop - groupRowHeight, height: verticalLinesHeight });
					} else {
						$(this).css({ height: verticalLinesHeight });
					}
				} else {
					if (verticalLinesTop != null) {
						$(this).css({ 'top': verticalLinesTop, height: verticalLinesHeight - groupRowHeight });
					} else {
						$(this).css({ height: verticalLinesHeight - groupRowHeight });
					}
				}
			});
		} else {
			if (verticalLinesTop != null) {
				grid.find('>thead>tr.FGHeaderRowV>th>div').css({ 'top': verticalLinesTop, height: verticalLinesHeight });
			} else {
				grid.find('>thead>tr.FGHeaderRowV>th>div').css({ height: verticalLinesHeight });
			}
		}
	}

	this.updateFixedTitlesRelative = function (scrollTop) {
		var parent = this.settings.$parent;
		var grid = this.settings.$table;
		grid.find('>thead>tr>th').css({ 'top': this.settings.hasFixedTitles ? scrollTop : '', position: 'relative' }).removeClass('FGCuttedCell');
		if (this.settings.footer) {
			var top = '';
			if (this.settings.hasFixedTitles) {
				var fRow = grid.find('>tfoot>tr:not(.FGFootRow)');
				top = scrollTop + parent.outerHeight() - parseInt(grid.css('border-bottom-width')) - parseInt(grid.parent().css('border-top-width')) - parseInt(grid.parent().css('border-bottom-width')) - fRow.outerHeight() - (fRow.offset().top - grid.offset().top) - getScrollWidthIfVisible(parent, false);
				if (top > 0) {
					//console.log('finxing top, probable bug');
					top = 0;
				}
			}
			grid.find('>tfoot>tr>td').css({ 'top': top, position: 'relative' }).removeClass('FGCuttedCell');
		}
	}

	this.updateTitlesAbsolute = function () {
		var table = this.settings.$table;

		var scrollLeft = table.parent().scrollLeft();

		var firstVisibleCell = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV)>th:visible').first();
		var origPositionVal = firstVisibleCell.css('position');
		var minLeft = firstVisibleCell.css({ 'position': 'absolute', 'left': '' }).css('left');
		if (minLeft == 'auto') {
			minLeft = firstVisibleCell.position().left;
		} else {
			minLeft = parseInt(minLeft);
		}
		firstVisibleCell.css({ 'position': origPositionVal });

		var availableWidth = this.tableAvailableWidthForGrid();
		var maxLeft = availableWidth + minLeft;

		//TODO:fijar anchos antes de sacar?
		var lefts = [], widths = [];
		for (var colIndex = 0; colIndex < this.settings.totalColumns; colIndex++) {
			var thItem = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV)>th:eq(' + colIndex + ')');
			if (colIndex < this.settings.left) {
				lefts.push(colIndex > 0 ? lefts[colIndex - 1] + widths[colIndex - 1] : minLeft);
			} else if (colIndex >= this.settings.firstRightColumn) {
				//do not set left yet
				lefts.push(0);
			} else {
				lefts.push(thItem.position().left);
			}
			widths.push(thItem.is(":visible") ? thItem.outerWidth() : 0);
			if (thItem.find('>*').length > 0) {
				var paddingTop = thItem.find('>*:eq(0)').offset().top - thItem.offset().top;
				if (thItem.find('>span>.AttributeCheckBox').length == 1) {
					paddingTop = thItem.find('>span>.AttributeCheckBox').offset().top - thItem.offset().top - parseInt(thItem.find('>span>.AttributeCheckBox').css('margin-top'));
				}
				if (paddingTop > 0) {
					thItem.css('padding-top', paddingTop);
				}
			}
			if (this.settings.foot) {
				var tfItem = table.find('>tfoot>tr:not(.FGFootRow)>td:eq(' + colIndex + ')');
				if (tfItem.find('>*').length > 0) {
					var paddingTop = tfItem.find('>*:eq(0)').offset().top - tfItem.offset().top;
					if (paddingTop > 0) {
						tfItem.css('padding-top', paddingTop);
					}
				}
			}
		}
		for (var colIndex = this.settings.totalColumns - 1; colIndex >= this.settings.firstRightColumn; colIndex--) {
			lefts[colIndex] = (colIndex == this.settings.totalColumns - 1 ? maxLeft : lefts[colIndex + 1]) - widths[colIndex];
		}
		table.find(this.settings.S_Rows + ':eq(0)>td').each(function () {
			$(this).css('min-width', widths[$(this).index()]);//TODO:reversar min-width?
		});
		var rowHeight = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV)>th:visible').first().outerHeight();
		var headTop = parseInt(table.css('margin-top')) + parseInt(table.parent().css('border-top-width'));
		table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV)>th').css({ 'top': ''/*headTop*/, 'position': 'absolute', 'height': rowHeight });
		table.find('>thead>tr.FGHeaderRowV>th').css({ 'top': ''/*parseInt(headTop) + rowHeight*/, 'position': 'absolute' });
		table.find('>thead>tr.FGHeaderRowH>th').css({ 'top': ''/*parseInt(headTop) + rowHeight*/, 'position': 'absolute', 'height': table.find('>thead>tr.FGHeaderRowH>th:eq(0)').outerHeight(), 'width': availableWidth });
		table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV)').css({ 'height': rowHeight });

		rowHeight = table.find('>tfoot>tr:not(.FGFootRow)>td:visible').first().outerHeight();
		var tfRowTop = table.parent().outerHeight() - table.find('>tfoot>tr:not(.FGFootRow)').outerHeight() - getScrollWidthIfVisible(table.parent(), false) - parseInt(table.parent().css('border-bottom-width'));
		table.find('>tfoot>tr:not(.FGFootRow)>td').css({ 'top': tfRowTop, 'position': 'absolute', 'height': rowHeight });
		table.find('>tfoot>tr.FGFootRow>td').css({ 'top': tfRowTop - parseInt(table.find('>tfoot>tr.FGFootRow').css('height')), 'position': 'absolute', 'height': table.find('>tfoot>tr.FGFootRow').height(), 'width': table.parent().outerWidth() - getScrollWidthIfVisible(table.parent(), true) });


		table.find('>thead>tr:not(.FGHeaderRowH)>th, >tfoot>tr:not(.FGFootRow)>td').each(function () {
			var width = widths[$(this).index()];
			for (var i = 1; i < this.colSpan; i++) {
				width += widths[$(this).index() + i];
			}
			if (lefts[$(this).index()] > maxLeft || lefts[$(this).index()] + width < minLeft) {
				$(this).css({ 'visibility': 'hidden', 'left': -1000 });
			} else if (lefts[$(this).index()] + width - 1 > maxLeft) {
				//cutted on the right
				$(this).css({ 'width': (maxLeft - lefts[$(this).index()]), 'left': lefts[$(this).index()], 'visibility': '' });
				if (!$(this).parent().hasClass('FGHeaderRowV')) {
					$(this).addClass('FGCuttedCell');
				}
			} else if (lefts[$(this).index()] < minLeft) {
				//cutted on the left
				$(this).css({ 'width': width - (minLeft - lefts[$(this).index()]), 'left': minLeft, 'visibility': '' });
				if (!$(this).parent().hasClass('FGHeaderRowV')) {
					$(this).addClass('FGCuttedCell');
				}
			} else {
				$(this).css({ 'width': width, 'left': lefts[$(this).index()], 'visibility': '' });
			}
		});
	}

	this.tableAvailableWidthForGrid = function () {
		return this.settings.$parent.outerWidth() - (this.settings.hasFixedTitles ? getScrollWidthIfVisible(this.settings.$parent, true) : 0) - parseInt(this.settings.$parent.css('border-left-width')) - parseInt(this.settings.$parent.css('border-right-width'));
	}

	this.tableAvailableHeightForGrid = function () {
		return this.settings.$parent.outerHeight() - getScrollWidthIfVisible(this.settings.$parent, false) - parseInt(this.settings.$parent.css('border-top-width')) - parseInt(this.settings.$parent.css('border-bottom-width'));
	}

	// Set table left column fixed
	this.updateFrozenColumnsAbsolute = function () {
		if (this.settings.left + this.settings.right == 0) {
			return;
		}

		WWP_Debug_Log(false, 'updateFrozenColumnsAbsolute');
		var table = this.settings.$table;

		var scrollTop = table.parent().scrollTop();

		var lefts = [], widths = [], rowHeights = [], tops = [], firstNotFixedLeftPadding = 0, lastNotFixedRightPadding = 0;
		for (var colIndex = 0; colIndex < this.settings.left; colIndex++) {
			var thItem = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + colIndex + ')');
			lefts.push(thItem.position().left);
			widths.push(thItem.outerWidth());
			if (thItem.find('>*').length > 0) {
				var paddingTop = thItem.find('>*:eq(0)').offset().top - thItem.offset().top;
				if (paddingTop > 0) {
					thItem.css('padding-top', paddingTop);
				}
			}
			if (table.find(this.settings.S_RowCells + ':eq(' + colIndex + ')').css('display') != 'none') {
				firstNotFixedLeftPadding += widths[colIndex];
			}
			var thCatItem = table.find('>thead>tr.GridGroupTitleRow>th:eq(' + colIndex + ')');
			if (thCatItem.length > 0) {
				if (thCatItem.find('>*').length > 0) {
					var paddingTop = thCatItem.find('>*:eq(0)').offset().top - thCatItem.offset().top;
					if (paddingTop > 0) {
						thCatItem.css('padding-top', paddingTop);
					}
				}
			}
		}
		for (var colIndex = this.settings.firstRightColumn; colIndex < this.settings.totalColumns; colIndex++) {
			var thItem = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + colIndex + ')');
			lefts.push(thItem.position().left);
			widths.push(thItem.outerWidth());
			if (thItem.find('>*').length > 0) {
				var paddingTop = thItem.find('>*:eq(0)').offset().top - thItem.offset().top;
				if (paddingTop > 0) {
					thItem.css('padding-top', paddingTop);
				}
			}
			if (table.find(this.settings.S_RowCells + ':eq(' + colIndex + ')').css('display') != 'none') {
				lastNotFixedRightPadding += widths[widths.length - 1];
			}
			var thCatItem = table.find('>thead>tr.GridGroupTitleRow>th:eq(' + colIndex + ')');
			if (thCatItem.length > 0) {
				if (thCatItem.find('>*').length > 0) {
					var paddingTop = thCatItem.find('>*:eq(0)').offset().top - thCatItem.offset().top;
					if (paddingTop > 0) {
						thCatItem.css('padding-top', paddingTop);
					}
				}
			}
		}
		var $cleanedRows = table.find('>tbody>tr:not(.FGRowLine,.WCD_tr)');
		var rowsSelectorLength = $cleanedRows.length;//do not exclude .DVGroupByRow to get the right row index
		for (var rowIndex = 0; rowIndex < rowsSelectorLength; rowIndex++) {
			var r = $($cleanedRows[rowIndex]);
			rowHeights.push(r.outerHeight());
			tops.push(r.position().top - scrollTop + parseInt(table.parent().css('border-top-width')));
			r.css('height', rowHeights[rowHeights.length - 1]);
		}
		var thRowHeight = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)').outerHeight();
		var tfRowHeight = table.find('>tfoot>tr:not(.FGFootRow)').outerHeight();
		var thRowTop = parseInt(table.css('margin-top')) + parseInt(table.parent().css('border-top-width'));
		var maxTop = thRowTop + this.tableAvailableHeightForGrid();
		var tfRowTop = maxTop - table.find('>tfoot>tr:not(.FGFootRow)').outerHeight();
		//TODO: test with GX paging buttons

		var thisC = this;
		$cleanedRows.find('>td').each(function () {
			var cell = $(this);
			var colIndex = cell.index();
			if (colIndex < thisC.settings.left || colIndex >= thisC.settings.firstRightColumn) {
				if (colIndex >= thisC.settings.firstRightColumn) {
					colIndex = colIndex - (thisC.settings.firstRightColumn - thisC.settings.left);
				}
				if (cell.find('>*').length > 0) {
					var paddingTop = cell.find('>*:eq(0)').offset().top - cell.offset().top;
					if (paddingTop > 0) {
						cell.css('padding-top', paddingTop);
					}
				}
			}
		});

		if (!this.settings.initializing) {
			var groupByRowWidth = this.settings.hasRowGroups ? this.tableAvailableWidthForGrid() : null;
			var groupByRowWidthRemaining = null, groupByRowMinWidth = null;
			var setTops = this.settings.hasFixedTitles;
			table.find('>tbody>tr:not(.FGRowLine,.WCD_tr)>td, >thead>tr:not(.FGHeaderRowH,.GridGroupTitleRow)>th, >tfoot>tr:not(.FGFootRow)>td').each(function () {
				var cell = $(this);
				var colIndex = cell.index();
				if (colIndex < thisC.settings.left || colIndex >= thisC.settings.firstRightColumn || thisC.settings.left == 0 && cell.is('[titleCell]')) {
					if (colIndex >= thisC.settings.firstRightColumn) {
						colIndex = colIndex - (thisC.settings.firstRightColumn - thisC.settings.left);
					}
					if (cell.parent().parent()[0].tagName == 'TBODY') {
						var rowIndex = $cleanedRows.index(cell.parent());
						var top = tops[rowIndex];
						var height = rowHeights[rowIndex];
						if (thisC.settings.hasRowGroups && cell.parent().hasClass('DVGroupByRow')) {
							setBackground(cell);
							if (!cell.is('[titleCell]') || cell.attr('colspan') == thisC.settings.totalColumnsVisibles) {
								var width = (!cell.is('[titleCell]') ? widths[colIndex] : groupByRowWidth);
								var zIndex = cell.is('[titleCell]') ? (thisC.settings.zIndex + 3) : (cell.index() >= thisC.settings.firstRightColumn ? thisC.settings.zIndex : '');
								cell.css({ 'padding-left': '', 'padding-right': '', 'position': 'absolute', 'left': lefts[colIndex], 'width': width, 'top': 'inherit', 'height': (height - (thisC.settings.isIE ? 1 : 0)) + 'px', 'z-index': zIndex });
							} else {
								if (groupByRowWidthRemaining == null) {
									groupByRowWidthRemaining = groupByRowWidth;
									for (var colIndex2 = thisC.settings.firstRightColumn; colIndex2 < thisC.settings.totalColumns; colIndex2++) {
										var thItem = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + colIndex2 + ')');
										if (thItem.is(':visible')) {
											groupByRowWidthRemaining -= widths[colIndex2 - (thisC.settings.firstRightColumn - thisC.settings.left)];
										}
									}
									if (table.data('groupNoNotFixedTot') != null) {
										groupByRowMinWidth = groupByRowWidthRemaining - (thisC.settings.hasVerticalBorders ? 1 : 0);
									} else if (thisC.settings.left > 0) {
										groupByRowMinWidth = 0;
										for (var colIndex2 = 0; colIndex2 < thisC.settings.left; colIndex2++) {
											var thItem = table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + colIndex2 + ')');
											if (thItem.is(':visible')) {
												groupByRowMinWidth += widths[colIndex2];
											}
										}
										if (thisC.settings.hasVerticalBorders) {
											groupByRowMinWidth--;
										}
									}
								}
								cell.css({ 'padding-left': '', 'padding-right': '', 'position': 'absolute', 'left': lefts[colIndex], 'max-width': groupByRowWidthRemaining, 'min-width': groupByRowMinWidth, 'top': 'inherit', 'height': (height - (thisC.settings.isIE ? 1 : 0)) + 'px', 'z-index': (thisC.settings.zIndex + 3) });
							}
						} else {
							if (top > maxTop || top + height < thRowTop) {
								cell.css({ 'visibility': 'hidden', 'position': 'absolute', 'top': (setTops ? -1000 : ''), 'left': -1000 });
							} else if (top + height > maxTop + 1) {
								//cutted on bottom
								cell.css({ 'padding-top': '', 'visibility': '', 'position': 'absolute', 'right': '', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': (setTops ? top + (thisC.settings.isIE ? 1 : 0) : ''), 'height': (maxTop - top - (thisC.settings.isIE ? 1 : 0)) + 'px' });
								cell.addClass('FGCuttedCell');
							} else if (top < thRowTop) {
								//cutted on top
								cell.css({ 'visibility': '', 'position': 'absolute', 'right': '', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': (setTops ? thRowTop : ''), 'height': (height - (thRowTop - top)) + 'px' });
								cell.addClass('FGCuttedCell');
							} else {
								cell.css({ 'visibility': '', 'position': 'absolute', 'right': '', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': '', 'height': (height - (thisC.settings.isIE ? 1 : 0)) + 'px' });
								if (setTops && scrollTop > 0) {
									var currentTop = cell.css('top');
									if (currentTop == 'auto') {
										cell.css({ 'top': cell.position().top - scrollTop });
									} else {
										cell.css({ 'top': parseInt(cell.css('top')) - scrollTop });
									}
								}
							}
						}
					} else if (cell[0].tagName == 'TH') {
						if (cell.parent().hasClass('FGHeaderRowV')) {
							cell.css({ 'position': 'absolute', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': '' });
						} else {
							cell.css({ 'position': 'absolute', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': '', 'height': thRowHeight + 'px' });
						}
					} else {
						cell.css({ 'position': 'absolute', 'left': lefts[colIndex], 'width': widths[colIndex], 'top': (setTops ? tfRowTop : ''), 'height': (tfRowHeight - 1) + 'px' });
					}
				}
			});
			if (this.settings.hasGroupTitles) {
				thRowHeight = table.find('>thead>tr.GridGroupTitleRow').outerHeight();
				table.find('>thead>tr.GridGroupTitleRow>th').each(function () {
					var cell = $(this);
					var colIndex = parseInt(cell.attr('mColIndex'));
					if (colIndex < thisC.settings.left || colIndex >= thisC.settings.firstRightColumn) {
						if (colIndex >= thisC.settings.firstRightColumn) {
							colIndex = colIndex - (thisC.settings.firstRightColumn - thisC.settings.left);
						}
						var width = widths[colIndex];
						var colSpan = this.colSpan;
						for (var i = 1; i < colSpan; i++) {
							width += widths[colIndex + i];
						}
						cell.css({ 'position': 'absolute', 'left': lefts[colIndex], 'width': width, 'top': (setTops ? thRowTop : ''), 'height': thRowHeight + 'px' });
					}
				});
			}
		}

		if (this.settings.left > 0) {
			this.appendPaddingToFirstNotFixed(table, true, this.settings.firstNotFixedLeft, firstNotFixedLeftPadding);
		}
		if (this.settings.right > 0) {
			this.appendPaddingToFirstNotFixed(table, false, this.settings.lastNotFixedRight, lastNotFixedRightPadding);
		}

		if (this.settings.left > 0 || this.settings.right > 0) {
			/*var leftDif = (table.find('>thead>tr').offset().left - table.find('>thead>tr:not(.FGHeaderRowH)>th:nth-child(' + (this.settings.firstNotFixedLeft + 1) + ')').offset().left);
			if (leftDif != 0) {
				if (this.settings.left > 0) {
					var minW = this.settings.firstNotFixedLeft_minWidth;
					this.appendPaddingToFirstNotFixed(table, true, this.settings.firstNotFixedLeft, leftDif);
					this.settings.firstNotFixedLeft_minWidth = minW;
				}
				if (this.settings.right > 0) {
					minW = this.settings.lastNotFixedRight_minWidth;
					this.appendPaddingToFirstNotFixed(table, false, this.settings.lastNotFixedRight, leftDif);
					this.settings.lastNotFixedRight_minWidth = minW;
				}
			}*/
		}
	}

	this.appendPaddingToFirstNotFixed = function (table, isLeft, colIndex, paddingToAdd) {
		var paddingDir = (isLeft ? 'left' : 'right');

		colIndex++;//nth is 1 based

		table.find(this.settings.S_RowCells + ':nth-child(' + colIndex + ')').css(
			'padding-' + paddingDir, (paddingToAdd + parseInt(table.find(this.settings.S_RowCells + ':nth-child(' + colIndex + '):eq(0)').css('padding-' + paddingDir))) + 'px'
		);
		table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:nth-child(' + colIndex + ')').css(
			'padding-' + paddingDir, (paddingToAdd + parseInt(table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:nth-child(' + colIndex + '):eq(0)').css('padding-' + paddingDir))) + 'px'
		);
		table.find('>tfoot>tr:not(.FGFootRow)>td:nth-child(' + colIndex + ')').css(
			'padding-' + paddingDir, (paddingToAdd + parseInt(table.find('>tfoot>tr:not(.FGFootRow)>td:nth-child(' + colIndex + '):eq(0)').css('padding-' + paddingDir))) + 'px'
		);

		if (this.settings.hasGroupTitles) {
			table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + (colIndex - 1) + ']').css(
				'padding-' + paddingDir, (paddingToAdd + parseInt(table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + (colIndex - 1) + ']:eq(0)').css('padding-' + paddingDir))) + 'px'
			);
			if (!isLeft && table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + (colIndex - 1) + ']').length == 0) {
				//first right not fixed is grouped
				var colIndexAux = colIndex - 2;
				while (colIndexAux >= 0 && table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + colIndexAux + ']').length == 0) {
					colIndexAux--;
				}
				table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + colIndexAux + ']').css(
					'padding-' + paddingDir, (paddingToAdd + parseInt(table.find('>thead>tr.GridGroupTitleRow>th[mColIndex=' + colIndexAux + ']:eq(0)').css('padding-' + paddingDir))) + 'px'
				);
			}
		}
		if (this.settings.hasRowGroups && table.find('>tbody>tr.DVGroupByRow:eq(0)>td[totCol]:nth-child(' + colIndex + ')').length > 0) {
			var origPadding = parseInt(this.settings.$table.find('>tbody>tr.DVGroupByRow:eq(0)>td[totCol]:nth-child(' + colIndex + ')').css("padding-" + paddingDir));
			table.find('>tbody>tr.DVGroupByRow>td:nth-child(' + colIndex + ')').css("padding-" + paddingDir, origPadding + paddingToAdd);
		}

		var minWidth = table.find(this.settings.S_RowCells + ':nth-child(' + colIndex + '):eq(0)').css('min-width');
		table.find(this.settings.S_RowCells + ':nth-child(' + colIndex + '):eq(0)').css({
			'min-width': (paddingToAdd + parseInt(minWidth)) + 'px'
		});

		if (isLeft) {
			this.settings.firstNotFixedLeft_minWidth = minWidth;
			this.settings.firstNotFixedLeft_addedPadding = paddingToAdd;
		}
		else if (this.settings.left == 0 || this.settings.firstNotFixedLeft != this.settings.lastNotFixedRight) {
			this.settings.lastNotFixedRight_minWidth = minWidth;
		}
	}

	this.geVisibleColumns = function () {
		var visibleColumns = "";
		this.settings.$table.find(">thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th*").each(function (k, row) {
			if (visibleColumns != '') {
				visibleColumns += ";";
			}
			visibleColumns += ($(this).css('display') != 'none' ? '1' : '0');
		});
		return visibleColumns;
	}

	this.getVisibleColumnsCount = function () {
		var visibleColumns = 0;
		this.settings.$table.find(">thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th*").each(function (k, row) {
			if ($(this).css('display') != 'none') {
				visibleColumns++;
			}
		});
		return visibleColumns;
	}

	this.undoAllModifications = function () {
		if (this.preparedAndFixed) {
			this.preparedAndFixed = false;
			//this.settings.$table.find('>tbody>tr.FGRowLine').remove();

			this.settings.$table.find('>thead>tr.FGHeaderRowV,>thead>tr.FGHeaderRowH,>tbody>tr.FGRowLine,>tfoot>tr.FGFootRow').remove();

			this.settings.$table.find(this.settings.S_Rows).css('height', '');
			this.settings.$table.find('>thead>tr>th,>tfoot>tr>td,>tbody>tr>td').css({
				'left': '',
				'position': '',
				'padding-left': '',
				'top': '',
				'visibility': '',
				'padding-top': '',
				'min-width': '',
				'max-width': '',
				'right': '',
				'padding-right': '',
				'width': '',
				'height': '',
				'background-color': ''
			}).removeClass('FGCuttedCell');

			//this.settings.$table.find('>tbody>tr>td>*').css('display', '');
			this.settings.$table.css('border-style', '').removeClass('FTBRemoveBorders');

			this.settings.$parent.off('scroll mousewheel mousedown keydown');
		}
		WWPEmpowerGrid_UpdateGrdiColumnsMinWidth(this.settings.$table, this.settings.hasFixedTitles, this.settings.hasResizableTitles);
	}

	this.prepareAndFix = function () {
		var table = this.settings.$table;
		if (this.settings.left + this.settings.right > 0 && this.gridWidthOverlapped > 0 && this.gridWidthOverlapped >= this.tableAvailableWidthForGrid()) {
			this.settings.left = 0;
			this.settings.right = 0;
		}
		this.visibleColumns = this.geVisibleColumns();
		if (this.settings.hasFixedTitles || this.settings.left + this.settings.right > 0) {
			this.settings.$parent.css("overflow-x", "auto");
			this.settings.$parent.css("overflow-y", this.settings.hasFixedTitles ? "auto" : "hidden");

			this.preparedAndFixed = true;
			var thisC = this;

			if (this.settings.left > 0) {
				this.settings.leftColumns = $();

				var tr = table.find(">thead>tr, " + this.settings.S_Rows + ", >tfoot>tr");
				tr.each(function (k, row) {
					thisC.solverLeftColspan(row, function (cell) {
						thisC.settings.leftColumns = thisC.settings.leftColumns.add(cell);
					});
				});

				var selectedRows = table.find(this.settings.S_Rows + '.gx-row-selected');
				selectedRows.removeClass('gx-row-selected');
				this.settings.leftColumns.each(function (k, cell) {
					var cell = $(cell);

					setBackground(cell);
					cell.css({
						'position': 'relative',
						'z-index': (thisC.settings.zIndex + (cell[0].tagName == 'TH' ? 2 : (cell.parent().parent()[0].tagName == 'TFOOT' ? 1 : 0)))
					});
				});
				selectedRows.addClass('gx-row-selected');
			}

			if (this.settings.right > 0) {
				this.settings.rightColumns = $();

				table.find('>thead>tr, ' + this.settings.S_Rows + ', >tfoot>tr').each(function (k, row) {
					thisC.solveRightColspan(row, function (cell) {
						thisC.settings.rightColumns = thisC.settings.rightColumns.add(cell);
					});
				});

				var selectedRows = table.find(this.settings.S_Rows + '.gx-row-selected');
				selectedRows.removeClass('gx-row-selected');
				this.settings.rightColumns.each(function (k, cell) {
					var cell = $(cell);

					setBackground(cell);
					cell.css({
						'position': 'relative',
						'z-index': (thisC.settings.zIndex + (cell[0].tagName == 'TH' ? 2 : (cell.parent().parent()[0].tagName == 'TFOOT' ? 1 : 0)))
					});
				});
				selectedRows.addClass('gx-row-selected');
			}

			if (this.settings.hasFixedTitles) {
				table.find('>thead>tr>th').each(function (k, cell) {
					cell = $(cell);
					var index = cell.index();
					if (index >= thisC.settings.left && index < thisC.settings.firstRightColumn) {
						setBackground(cell);
						cell.css('z-index', thisC.settings.zIndex);
					}
				});
				table.find('>tfoot>tr>td').each(function (k, cell) {
					cell = $(cell);
					var index = cell.index();
					if (index >= thisC.settings.left && index < thisC.settings.firstRightColumn) {
						setBackground(cell);
						cell.css('z-index', thisC.settings.zIndex - 1);
					}
				});
			}

			if (this.settings.hasFixedTitles || this.settings.left + this.settings.right > 0) {
				table.css('border-style', 'none');

				var headerLineCell = this.gridHeaderLineCell();
				if (headerLineCell.length == 0) {
					var headerRow = table.find('>thead>tr:last-child()');
					var visibleCols = headerRow.find('>*:visible').length;
					if (visibleCols == 0) {
						//caso en que el grid no esta visible aun (por ejemplo un DWC que se esta cargando)
						visibleCols = this.getVisibleColumnsCount();
					}
					this.settings.hasVerticalBorders = table.closest('.gx-grid').parent().closest('.HasGridEmpowerer,.gx-grid').hasClass('GridFixedColumnBorders')
					if (this.settings.hasVerticalBorders) {
						var newR = document.createElement('tr');
						newR.className = 'FGHeaderRowV';
						$(newR).css('height', '0');
						for (var i = 0; i < this.settings.totalColumns; i++) {
							headerLineCell = document.createElement('th');
							var thItem = table.find('>thead>tr:not(.FGHeaderRowH)').last().find('>th:eq(' + i + ')');
							var display = thItem.css('display');
							if (display == 'none' && thItem[0].style.display != 'none' && thItem.hasClass('hidden-xs')) {
								display = '';
							}
							if (display != 'none') {
								if (thItem.hasClass('hidden-xs')) {
									$(headerLineCell).addClass('hidden-xs');
								}
								$(headerLineCell).css({
									'z-index': this.settings.zIndex + (i < this.settings.left || i >= this.settings.firstRightColumn ? 2 : 0)
								});
								var lineDiv = document.createElement('div');
								if (this.settings.lastNotFixedRight == i) {
									$(lineDiv).css('visibility', 'hidden');
								}
								$(headerLineCell).append(lineDiv);
							} else {
								$(headerLineCell).css('display', display);
							}
							$(newR).append(headerLineCell);
							if (i < this.settings.left) {
								this.settings.leftColumns = this.settings.leftColumns.add($(headerLineCell));
							} else if (i >= this.settings.firstRightColumn) {
								this.settings.rightColumns = this.settings.rightColumns.add($(headerLineCell));
								$(headerLineCell).addClass('FixedR');
							}
						}
						table.find('>thead').append(newR);
					}
					if (parseFloat(headerRow.css('border-bottom-width')) > 0) {
						var newR = document.createElement('tr');
						newR.className = 'FGHeaderRowH';
						$(newR).css('height', headerRow.css('border-bottom-width'));
						headerLineCell = document.createElement('th');
						headerLineCell.colSpan = visibleCols;
						$(headerLineCell).css({ 'background-color': headerRow.css('border-bottom-color'), 'z-index': this.settings.zIndex + 2 });
						$(newR).append(headerLineCell);
						table.find('>thead').append(newR);
					}
					var dataRow = table.find(this.settings.S_Rows + ':eq(0)');
					if (parseFloat(dataRow.css('border-top-width')) > 0) {
						var borderWidth = dataRow.css('border-top-width');
						var borderColor = dataRow.css('border-top-color');

						var processedRecords = table.data('IS_ProcessedRecords');
						if (processedRecords != null && table.find('>tbody').children().length != processedRecords) {
							processedRecords = null;
						}
						var rptStyleStr = (this.settings.isRptStyle ? '<tr class="FGRowLine" style="display:none" />' : '');
						if (this.settings.isIE) {
							$(rptStyleStr + '<tr class="FGRowLine"><td colSpan="' + visibleCols + '"><div></div></td></tr>').insertAfter(table.find('>tbody>tr'));
							table.find('>tbody>tr.FGRowLine>td>div').css('border-top', 'solid ' + borderWidth + ' ' + borderColor);
						} else {
							$(rptStyleStr + '<tr class="FGRowLine"><td colSpan="' + visibleCols + '"></td></tr>').insertAfter(table.find('>tbody>tr'));
							table.find('>tbody>tr.FGRowLine').css('height', borderWidth);
							table.find('>tbody>tr.FGRowLine>td').css('background-color', borderColor);
						}
						table.find('>tbody>tr.FGRowLine:last-child()').remove();
						if (processedRecords != null) {
							table.data('IS_ProcessedRecords', table.find('>tbody').children().length);
						}
					}
					if (this.settings.footer) {
						var fRow = table.find('>tfoot>tr');
						if (parseFloat(fRow.css('border-top-width')) > 0) {
							var newR = document.createElement('tr');
							newR.className = 'FGFootRow';
							$(newR).css('height', fRow.css('border-top-width'));
							var footerLineCell = document.createElement('td');
							footerLineCell.colSpan = visibleCols;
							$(footerLineCell).css({ 'background-color': fRow.css('border-top-color'), 'z-index': this.settings.zIndex + 2 });
							$(newR).append(footerLineCell);
							$(newR).insertBefore(fRow);
						}
					}
				}
				table.addClass('FTBRemoveBorders');
			}

			this.updateAllRelative();
			if (!this.settings.hasFixedTitles) {
				this.updateFrozenColumnsAbsolute();
			}
		} else {
			this.settings.$parent.css("overflow", "");
			this.settings.$parent.css("overflow-y", "");
		}
		this.bindScrollEvent();
	}

	this.gridHeaderLineCell = function () {
		return this.settings.$table.find('>thead>tr.FGHeaderRowH>th');
	}

	// Set fixed cells backgrounds
	function setBackground(elements) {
		elements.each(function (k, element) {
			var element = $(element);
			var parent = $(element).parent();

			var elementBackground = element.css("background-color");
			elementBackground = (elementBackground == "transparent" || elementBackground == "rgba(0, 0, 0, 0)") ? null : elementBackground;

			if (!elementBackground) {
				var parentBackground = parent.css("background-color");
				elementBackground = (parentBackground == "transparent" || parentBackground == "rgba(0, 0, 0, 0)") ? "white" : parentBackground;
			}
			element.css("background-color", elementBackground);
		});
	}

	this.solverLeftColspan = function (row, action) {
		if ($(row).hasClass('GridGroupTitleRow')) {
			for (var colsRight = 0; colsRight < this.settings.left; colsRight++) {
				if ($(row.childNodes[colsRight]).attr('mColIndex') <= colsRight) {
					action($(row.childNodes[colsRight]));
				}
			}
		} else {
			for (var colsRight = 1; colsRight <= this.settings.left; colsRight++) {
				var cell = $(row).find("> *:nth-child(" + colsRight + ")");
				action(cell);
			}
		}
	}

	this.solveRightColspan = function (row, action) {
		if ($(row).hasClass('GridGroupTitleRow')) {
			for (var colsRight = 0; colsRight < row.childNodes.length; colsRight++) {
				if ($(row.childNodes[colsRight]).attr('mColIndex') >= this.settings.firstRightColumn) {
					action($(row.childNodes[colsRight]));
				}
			}
		} else {
			for (var colsRight = 1; colsRight <= this.settings.right; colsRight++) {
				var cell = $(row).find("> *:nth-child(" + (this.settings.firstRightColumn + colsRight) + ")");
				action(cell);
			}
		}
	}

	this.UpdateInfiniteScrollingNewRecords = function () {
		this.visibleColumns = null;
		this.Refresh();
	}

	this.WCD_PrepareToExpand = function (rowToExpand) {
		if (this.settings.hasVerticalBorders) {
			this.settings.hasExpandedWC = true;
			var grid = this.settings.$table;
			var verticalLinesHeight = $(rowToExpand).position().top + $(rowToExpand).outerHeight();
			var $prevExpandedRow = grid.find('>tbody>tr.WCD_tr');
			if ($prevExpandedRow.length == 1 && $prevExpandedRow.index() < $(rowToExpand).index()) {
				verticalLinesHeight -= $prevExpandedRow.outerHeight();
			}
			this.updateVerticalBordersCss(null, verticalLinesHeight);

			var newRVerticalLinesHeight = grid.parent().outerHeight() - getScrollWidthIfVisible(grid.parent(), false) - parseInt(grid.css('margin-top')) - parseInt(grid.parent().css('border-bottom-width')) - parseInt(grid.parent().css('border-top-width')) - 1;
			newRVerticalLinesHeight -= verticalLinesHeight;
			if ($prevExpandedRow.length == 1) {
				newRVerticalLinesHeight -= $prevExpandedRow.outerHeight();
			}
			grid.find('>tbody>tr.FGHeaderRowV').remove();
			if (newRVerticalLinesHeight > 0) {
				var $fgHeaderRowV = grid.find('>thead>tr.FGHeaderRowV');

				var newR = document.createElement('tr');
				newR.className = 'FGHeaderRowV';
				$(newR).css('height', '0');
				newR.innerHTML = WWP_replaceAll(WWP_replaceAll($fgHeaderRowV[0].innerHTML, '<th', '<td'), '</th', '</td');

				$(newR).find('>td>div').css({ top: '', height: newRVerticalLinesHeight });

				rowToExpand.parentNode.insertBefore(newR, rowToExpand.nextSibling);
			}
		}
	}

	this.WCD_Collapsed = function () {
		if (this.settings.hasVerticalBorders) {
			delete this.settings.hasExpandedWC;
			var grid = this.settings.$table;
			var verticalLinesHeight = grid.parent().outerHeight() - getScrollWidthIfVisible(grid.parent(), false) - parseInt(grid.css('margin-top')) - parseInt(grid.parent().css('border-bottom-width')) - parseInt(grid.parent().css('border-top-width')) - 1;
			this.updateVerticalBordersCss(null, verticalLinesHeight);
			grid.find('>tbody>tr.FGHeaderRowV').remove();
		}
	}

	this.Refresh = function (regenerateGrid) {
		WWP_Debug_Log(false, 'refreshing');
		var rightColumns = 0;
		var thSelectorStr = '>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th';
		var firstFixedRightColumn = this.settings.$table.find(thSelectorStr + '.FixedColumnR:visible').first().index();
		if (firstFixedRightColumn != -1) {
			rightColumns = this.settings.$table.find(thSelectorStr).length - firstFixedRightColumn;
		}
		var leftColumns = this.settings.$table.find(thSelectorStr + '.FixedColumnL:visible').last().index() + 1;
		regenerateGrid = regenerateGrid || (this.settings.origLeft + this.settings.origRight > 0 != leftColumns + rightColumns > 0) || this.geVisibleColumns() != this.visibleColumns;
		if (regenerateGrid) {
			WWP_Debug_Log(false, 'changed');

			var categoriesHelper = this.settings.$parent.data('WWP.GridTitlesCategories');
			if (categoriesHelper != null) {
				this.settings.$table.data('titlesCategories', null);
				categoriesHelper.mergeTitlesCategoryCells();
			}

			var rowGroupsHelper = this.settings.$parent.data('WWP.RowGroupsHelper');
			if (rowGroupsHelper != null) {
				this.settings.$table.data('groupProcessed', null);
				rowGroupsHelper.mergeGroupRowCellsAndAddTitle();
			}

			this.undoAllModifications();

			this.gridWidthOverlapped = 0;
			this.gridWidthNotOverlapped = 99999;
			this.initializeSettings(this.settings.$table, {
				left: leftColumns,
				right: rightColumns,
				hasFixedTitles: this.settings.hasFixedTitles,
				hasResizableTitles: this.settings.hasResizableTitles,
				isRptStyle: this.settings.isRptStyle
			});
			this.prepareAndFix();
		} else if (this.preparedAndFixed) {
			this.updateAllRelative();
			if (!this.settings.hasFixedTitles) {
				this.updateFrozenColumnsAbsolute();
			}
		}
		this.checkIfFixedColumnsExceedWidth();
	}

	this.checkIfFixedColumnsExceedWidth = function () {
		if (this.settings.origLeft + this.settings.origRight > 0) {
			var overlapped = false;
			var gridWidth = this.tableAvailableWidthForGrid();
			if (gridWidth < this.gridWidthNotOverlapped) {
				if (this.gridWidthOverlapped >= gridWidth) {
					overlapped = true;
				} else {
					if (!this.preparedAndFixed) {
						this.initializeSettings(this.settings.$table, {
							left: this.settings.origLeft,
							right: this.settings.origRight,
							hasFixedTitles: this.settings.hasFixedTitles,
							hasResizableTitles: this.settings.hasResizableTitles,
							isRptStyle: this.settings.isRptStyle
						});
						this.prepareAndFix();
					}
					WWP_Debug_Log(false, 'calculating');
					var maxLeft = 0;
					if (this.settings.left > 0) {
						var cell = this.settings.$table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + (this.settings.left - 1) + ')');
						maxLeft = cell.offset().left + cell.outerWidth();
					}
					var minRight;
					if (this.settings.right > 0) {
						minRight = this.settings.$table.find('>thead>tr:not(.FGHeaderRowH,.FGHeaderRowV,.GridGroupTitleRow)>th:eq(' + this.settings.firstRightColumn + ')').offset().left;
					} else {
						minRight = gridWidth + this.settings.$table.offset().left + this.settings.$table.parent().scrollLeft();
					}
					if (maxLeft + 50 >= minRight) {
						overlapped = true;
					}
				}
			}
			if (overlapped) {
				if (this.gridWidthOverlapped < gridWidth) {
					//se suma scroll bar por el caso borde en que aparece o desaparece el scroll
					this.gridWidthOverlapped = gridWidth + getScrollBarWidth(true);
					if (this.gridWidthNotOverlapped < this.gridWidthOverlapped) {
						this.gridWidthNotOverlapped = this.gridWidthOverlapped;
					}
				}
				WWP_Debug_Log(false, 'overlaped');
				if (this.settings.left + this.settings.right > 0) {
					this.undoAllModifications();
					this.prepareAndFix();
				}
				return true;
			} else {
				WWP_Debug_Log(false, 'not overlaped');
				if (this.gridWidthNotOverlapped > gridWidth && this.gridWidthNotOverlapped >= this.gridWidthOverlapped) {
					this.gridWidthNotOverlapped = gridWidth;
				}
				if (this.preparedAndFixed && this.settings.left + this.settings.right == 0) {
					this.undoAllModifications();
				}
				if (!this.preparedAndFixed) {
					this.initializeSettings(this.settings.$table, {
						left: this.settings.origLeft,
						right: this.settings.origRight,
						hasFixedTitles: this.settings.hasFixedTitles,
						hasResizableTitles: this.settings.hasResizableTitles,
						isRptStyle: this.settings.isRptStyle
					});
					this.prepareAndFix();
				}
			}
		}
		return false;
	}

	this.initializeSettings = function ($table, param) {

		var defaults = {
			head: true,
			foot: true,
			left: 0,
			right: 0,
			zIndex: 2
		};
		if (this.settings != null && this.settings.myT != null) {
			clearInterval(this.settings.myT);
		}
		this.settings = $.extend({}, defaults, param);

		this.settings.$table = $table;
		this.settings.$parent = this.settings.$table.parent();
		this.settings.totalColumns = this.settings.$table.find(">thead>tr:last-child()>*").length;
		this.settings.firstRightColumn = (this.settings.totalColumns - this.settings.right);
		this.settings.firstNotFixedLeft = null;
		this.settings.lastNotFixedRight = null;
		this.settings.footer = this.settings.$table.find('>tfoot>tr>td:eq(0)').length > 0;
		this.settings.isIE = WWP_IsIE();
		this.settings.S_Rows = '>tbody>tr:not(.DVGroupByRow,.FGRowLine,.Invisible)';
		this.settings.S_RowCells = '>tbody>tr:not(.DVGroupByRow,.FGRowLine,.Invisible)>td';
		this.settings.unprepare = true;
		this.settings.origLeft = this.settings.left;
		this.settings.origRight = this.settings.right;
		this.settings.hasGroupTitles = (this.settings.$table.find(">thead>tr.GridGroupTitleRow").length > 0);
		this.settings.hasRowGroups = (this.settings.$table.find(">tbody>tr.DVGroupByRow:eq(0)").length > 0);
		this.hasRowGroupsWithTot = this.settings.hasRowGroups && this.settings.$table.find('>tbody>tr.DVGroupByRow>td:visible[totCol]:eq(0)').length > 0;
		this.DVGroupByRowOriginalPaddingR = this.settings.hasRowGroupsWithTot ? parseInt(this.settings.$table.find('>tbody>tr.DVGroupByRow>td[titleCell]:eq(0)').css("padding-right")) : null;

		var thisC = this;
		if (this.settings.left + this.settings.right > 0) {
			var hasNotFrezzedColumn = false;
			if (this.settings.hasRowGroups && this.settings.hasFixedTitles) {
				console.warn('WorkWithPlus Error: a grid cannot have "Grouped by", "Fixed Titles" and a "Fixed Column" at the same time.');
			} else {
				this.settings.$table.find('>thead>tr>th:visible').each(function () {
					if ((thisC.settings.left == 0 || $(this).index() > thisC.settings.left - 1)
						&& (thisC.settings.right == 0 || $(this).index() < thisC.settings.firstRightColumn)) {
						hasNotFrezzedColumn = true;
						return false;
					}
				});
			}
			if (!hasNotFrezzedColumn) {
				this.settings.left = 0;
				this.settings.right = 0;
			}
		}

		this.settings.totalColumnsVisibles = this.settings.$table.find(">thead>tr:last-child()>*:visible").each(function () {
			var thisIndex = $(this).index();
			if (thisC.settings.firstNotFixedLeft == null && thisIndex >= thisC.settings.left) {
				thisC.settings.firstNotFixedLeft = thisIndex;
			}
			if (thisIndex < thisC.settings.firstRightColumn) {
				thisC.settings.lastNotFixedRight = thisIndex;
			}
			if (thisIndex < thisC.settings.left) {
				$(this).addClass('FixedColumnL');
			} else if (thisIndex >= thisC.settings.firstRightColumn) {
				$(this).addClass('FixedColumnR');
			}
		}).length;
	}

	this.gridWidthOverlapped = 0;
	this.gridWidthNotOverlapped = 99999;
	this.initializeSettings($table, param);
	if (typeof (SetMinWidthTotalizers) === 'function') {
		SetMinWidthTotalizers();
	}
	this.prepareAndFix();
	this.checkIfFixedColumnsExceedWidth();
}
;$(window).one('load',function(){WWP_VV([['WWP.GridEmpowerer','15.2.12']]);});