//FILE oat_pivot -----------------------------------------------------------------------------------------------------------------------------------------------

if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
if (GlobalPivotInterval == undefined) {
	var GlobalPivotInterval = [];
}

OAT.Pivot = function(div, chartDiv, filterDiv, headerRow, dataRows, headerRowIndexes, headerColIndexes, filterIndexes, dataColumnIndex, optObj, query, condFormats, control, pageSize, defaultPicture, QueryViewerCollection, columns, containerName, 
			formatValue, conditionalFormatsColumns, formatValueMeasures, measures, shrinkToFit, disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout, ShowMeasuresAsRows,
			formulaInfo , fullRecord, serverPagination, pageData, orderFildsHidden) {
    var self = this; 
    this.autoPaging = false; this.nextRowWhenAutopaging = 0; this.prevRowWhenAutopaging = 0;
    this.paginationInfo = false;
    this.TempDataStructForAggStepOptimization = []; 
    this.actualPaginationPage = 1;
    this.allDataWithoutSort = jQuery.extend(true, [], dataRows);
    this.IdForQueryViewerCollection = IdForQueryViewerCollection; this.UcId = UcId;
    this.ShowMeasuresAsRows = ShowMeasuresAsRows;
    this.rememberLayoutStateVersion = (serverPagination) ?  "4.3.8SP" : "4.3.8"; 
    if (measures.length < 2){ //si solo tiene una medida no aplica esta propiedad
    	this.ShowMeasuresAsRows = false
    }
    this.swfPath = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/downloadify/media/downloadify.swf') 
    this.downloadImagePath = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/download_file.png'),
    this.allRowsPivot = "vacio";
    fromUndefinedToBlanck(dataRows); this.orderFildsHidden = orderFildsHidden;
    
    if (dataRows[0] != undefined){
    	
    		//agregar indice de fila
    		if (formulaInfo.cantFormulaMeasures > 0){
    			for(var i = 0; i < dataRows.length; i++){
    				dataRows[i][headerRow.length] = i;
    			}
    		}
    		//end agregar indice a filas
    		var sortIntMemory = [];
     		     		
     		//get sort value
     		for(var index = headerRow.length - 1; index > -1 ;index--){ //*dataRows[0].length*/
     			var sortInt = true;
    		    for (var ival = 0; ival < dataRows.length; ival++) {
        		if ( (sortInt) && (dataRows[ival][index] != parseInt(dataRows[ival][index]) ) ){
        			sortInt  = false;
        			break;
        		}
        		}
        		sortIntMemory[index] = sortInt;
     		}
     		var index = 0;
    		if (sortIntMemory[index]){
    				dataRows = dataRows.sort((function(index){
    					return function(a, b){
       						return (parseInt(a[index]) === parseInt(b[index]) ? 0 : (parseInt(a[index]) < parseInt(b[index]) ? -1 : 1));
    					};
					})(index));
    		} else {
    				dataRows = dataRows.sort((function(index){
    					return function(a, b){
       						return (a[index] === b[index] ? 0 : (a[index] < b[index] ? -1 : 1));
       						};
					})(index));
			}
				
			var actualVal = dataRows[0][index];
			var initPos = 0; 
			for (var i=0; i < dataRows.length; i++){
				if ((actualVal != dataRows[i][index])) {
					actualVal = dataRows[i][index]
					dataRows = OAT.PartialSort(dataRows, initPos, i-1, index+1, headerRow.length, sortIntMemory)
					initPos = i;
				} else 	if ((i == dataRows.length-1)){
					dataRows = OAT.PartialSort(dataRows, initPos, i, index+1, headerRow.length, sortIntMemory)
				}
			}
				
    }
    //collapse repeate rows
    var tempDataRows =  dataRows;     
    dataRows = [];
    if (tempDataRows.length > 0){
    	dataRows[0] = tempDataRows[0] 
    }
    var act_pos = 0;
    for (var i = 1; i<tempDataRows.length; i++){
    	var repeated = true;
    	for(var j = 0; j < columns.length; j++){
    		if (tempDataRows[i][j] != tempDataRows[i-1][j]) {repeated = false; break; }	
    	}
    	if (repeated){  
    		for (var j = columns.length; j < columns.length + measures.length; j++){
    			var tot = parseFloat(dataRows[act_pos][j]) + parseFloat(tempDataRows[i][j])
				if ((dataRows[act_pos][j] == "#NuN#") && (tempDataRows[i][j] == "#NuN#")){
    				tot = "#NuN#"
    			} else if (dataRows[act_pos][j] == "#NuN#") {
    				tot = tempDataRows[i][j] + "" 
    			} else if (tempDataRows[i][j] == "#NuN#") {
    				tot = dataRows[act_pos][j] + ""
    			}
    			dataRows[act_pos][j] = tot + "";  
    		}
    		//colapsar filas de formula
    		if (formulaInfo.cantFormulaMeasures > 0){
    			var filaSumar = dataRows[act_pos][headerRow.length];
    			var filaRepetida = tempDataRows[i][headerRow.length];
    			for (var formulasI = 0; formulasI < formulaInfo.measureFormula.length; formulasI++){ //campos formulas
    				if (formulaInfo.measureFormula[formulasI].hasFormula){
    					for(var formulasJ = 0; formulasJ < formulaInfo.measureFormula[formulasI].relatedMeasures.length; formulasJ++){
    						var pos = formulaInfo.measureFormula[formulasI].relatedMeasures[formulasJ];
    						var tot = parseFloat(fullRecord[filaSumar][pos]) + parseFloat(fullRecord[filaRepetida][pos])
    						fullRecord[filaSumar][pos] = tot + "";
    					}
    				}
    			}
    			for (var j = columns.length; j < columns.length + measures.length; j++){ //restantes medidas para mantener coherencia
    				var tot = parseFloat(fullRecord[filaSumar][j]) + parseFloat(fullRecord[filaRepetida][j])
    				fullRecord[filaSumar][j] = tot + "";  
    			}
    			for (var posB = 0; posB < headerRow.length; posB++){ //anular fila ya sumada
    				fullRecord[filaRepetida][posB] = "";
    			}
    		}
			//end colapsar formula info
    	} else {
    		act_pos++
    		dataRows[act_pos] = tempDataRows[i]; 
    	}
    }
    
    //eliminar indice de fila
    if (formulaInfo.cantFormulaMeasures > 0){
    	if (dataRows[0] != undefined){
    		for(var it=0; it < dataRows.length; it++){
    			dataRows[it].splice(headerRow.length,1)
    		}
    	}
    }
    //end borrar incide a filas
    this.serverPagination = serverPagination;
    this.pageData = pageData;
    if (this.serverPagination){
    	this.pageData.PreviousPageNumber = -1;
    	this.pageData.AxisInfo = [];
    	this.pageData.FilterInfo = [];
    }
	//determine where to use or not use auto-pagination
    if ( (!this.serverPagination) && (pageSize != undefined) && ((columns.length > 1) || (measures.length > 0)) ){
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) < 4) && (dataRows.length > 4004)){
    		this.autoPaging = true;
    	}
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) == 4) && (dataRows.length > 2701)){
    		this.autoPaging = true;
    	}
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) == 5) && (dataRows.length > 2501)){
    		this.autoPaging = true;
    	}
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) == 6) && (dataRows.length > 1301)){
    		this.autoPaging = true;
    	}
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) == 7) && (dataRows.length > 601)){ 
    		this.autoPaging = true;
    	}
    	if ((dataRows[0] != undefined) && ((dataRows[0].length - filterIndexes.length) >= 8) && (dataRows.length >= 301)){
    		this.autoPaging = true;
    	}
    }
	if ((filterIndexes.length > 1) && (columns.length - filterIndexes.length <= 2) && (headerColIndexes.length == 0)){
    	this.autoPaging = true;
    }
	if ((this.ShowMeasuresAsRows) || (this.serverPagination)){
		this.autoPaging = false;
	}
	
	this.recordForFormula = fullRecord;
	this.formulaInfo = formulaInfo;
	this.filterIndexes = filterIndexes; /* indexes of column conditions */
	this.initFilterIndexes = [];
	for(var i=0; i < this.filterIndexes.length; i++){
    	this.initFilterIndexes[i] = this.filterIndexes[i];	
    }
	this.getFormulaRowByDataRow = function(row, measureNumber, caseId){
		var value = ""
		var hallado = false
		var numRow = 0
		var searchIn = self.recordForFormula
		//if (self.filterIndexes.length > 0){ searchIn = self.filteredData }
		var addedValues = []; for(var o = 0; o < self.formulaInfo.recordDataLength; o++){addedValues[o] = 0}
		for (var i=0; i < searchIn.length; i++){
			var coincide = false
			for(var j=0; j < row.length; j++){
				if (self.filterIndexes.indexOf(j) != -1){ //case filter to top bar
					var pos = self.filterIndexes.indexOf(j)
					if (self.filterDiv != undefined){
         		    	var s = self.filterDiv.selects[pos]; /* select node */
         		   		var val = OAT.$v(s)
            			if (val == "[all]") { coincide = true } //case [all]
            			else { 
            				coincide = (val == searchIn[i][j])
            				if (!coincide) break;
            			}
            		} else {
            			coincide = true;
            		}
				} else if ((self.filterIndexes.length > 0) && (j >= row.length - measures.length)){ //case filter to top bar y measure item
					coincide = true;
				} else if ((searchIn[i][j] == row[j]) || ((row[j]=="#FoE#") && (searchIn[i][j]==0))){
					coincide = true;
				} else if ((headerRowIndexes != undefined) && (headerRowIndexes.indexOf(j)!=-1) 
					&& (headerRowIndexes.indexOf(j) > headerRowIndexes.length - measures.length)){ //if a measure, when rowConditions is not defined yet
					coincide = true;
				} else if ((dataColumnIndex>0) && (dataColumnIndex==j)){
					coincide = true;	
				} else {
					if ((row[j] == undefined) && (self.filterIndexes.length > 0)){
						coincide = true;
					} else {
						coincide = false;
						break;
					}
				}	 
			}
			if (coincide){
				for (var t = 0; t < self.formulaInfo.measureFormula[measureNumber].relatedMeasures.length; t++){
					var pos = self.formulaInfo.measureFormula[measureNumber].relatedMeasures[t]
					addedValues[pos] = addedValues[pos] + parseFloat(searchIn[i][pos]); 
				}
				hallado = true
				
			}
		}
		if (hallado)
			return addedValues;
		else
			return "#NuN#"//self.EmptyValue; //"#NuN#"
	}
	
	
	//calculate single measure formula value
	for(var mforF = 0; mforF < measures.length; mforF++){
		if (formulaInfo.measureFormula[mforF].hasFormula){
			for(var rforF = 0; rforF < dataRows.length; rforF++){
				if (dataRows[rforF][dataRows[rforF].length - measures.length + mforF]==0){
					var formula = this.getFormulaRowByDataRow(dataRows[rforF], mforF, "");
					var result = EvaluateExpressionPivotJs(formulaInfo.measureFormula[mforF].PolishNotation, formula, formulaInfo)
					if ((result == Infinity) || isNaN(result)) {
						dataRows[rforF][dataRows[rforF].length - measures.length + mforF] = "#FoE#";
					}	
				}
			}
		}
	}
		   
    this.GeneralDataRows = dataRows;
    this.autoPagingRowsPerPage = (pageSize != undefined) ? parseInt(pageSize) : 10;
    this.TotalPagesPaging = parseInt(dataRows.length / this.autoPagingRowsPerPage);
    if ( (dataRows.length % this.autoPagingRowsPerPage) != 0){
    	this.TotalPagesPaging++;
    }
    this.GeneralDistinctValues = [];
    this.GrandTotalsPaging = [];
    this.columns = columns;
    if (dataRows.length > 0){
    	fillGeneralDistinctValues(headerRow.length - measures.length, self, dataRows);
    }
    if (this.autoPaging){
    	var tempDataRows = dataRows;
		dataRows = [];
		for(var i=0; i< Math.min(tempDataRows.length,this.autoPagingRowsPerPage); i++){
			dataRows.append([tempDataRows[i]]);
		}
	}
    this.RowsWhenMoveToFilter = []
    this.FilterByTopFilter = false
    this.options = {
        headingBefore: 1,
        headingAfter: 0,
        agg: 1, /* index of default statistic function, SUM */
        aggTotals: 1, /* dtto for subtotals & totals */
        showChart: 0,
        showRowChart: 0,
        showColChart: 0,
        //type: OAT.PivotData.TYPE_BASIC[0],
        customType: function(data) {
        	return data; }, 
        //currencySymbol: "$",
        showEmpty: 0,
        subtotals: 1,
        totals: 1
    }
    if (optObj) for (p in optObj) { this.options[p] = optObj[p]; }

    this.firstTime = true;
    this.readState = false;
    this.deleteState = false;
    this.defaultPicture = defaultPicture; 
    this.gd = new OAT.GhostDrag();
    this.div = OAT.$(div);
    this.filterDiv = OAT.$(filterDiv);
    this.chartDiv = OAT.$(chartDiv);
    this.defCArray = ["rgb(153,153,255)", "rgb(153,51,205)", "rgb(255,255,204)", "rgb(204,255,255)", "rgb(102,0,102)",
						"rgb(255,128,128)", "rgb(0,102,204)", "rgb(204,204,255)", "rgb(0,0,128)", "rgb(255,0,255)",
						"rgb(0,255,255)", "rgb(255,255,0)"];
	this.QueryViewerCollection = QueryViewerCollection;
	this.containerName = containerName;
	this.formatValues = formatValue;
	this.formatValuesMeasures = formatValueMeasures;
	this.tempBlackLists = []; this.tempCollapsedValues = []; this.oldSortValues = [];
	this.stateChanged = false;
	this.rowsPerPage = pageSize;
	 
	
	
    if (this.chartDiv) {
        OAT.Dom.clear(self.chartDiv);
        var c1 = OAT.Dom.create("div", {}, "pivot_chart");
        var c2 = OAT.Dom.create("div", {}, "pivot_row_chart");
        var c3 = OAT.Dom.create("div", {}, "pivot_col_chart");
        var l1 = OAT.Dom.button("");
        var l2 = OAT.Dom.button("");
        var l3 = OAT.Dom.button("");

        OAT.Dom.append([self.chartDiv, l1, c1, l3, c3, l2, c2]);
        this.charts = {
            main: new OAT.BarChart(c1, {}),
            row: new OAT.BarChart(c2, {}),
            col: new OAT.BarChart(c3, {}),
            mainLink: l1,
            rowLink: l2,
            colLink: l3,
            mainDiv: c1,
            rowDiv: c2,
            colDiv: c3
        }
        OAT.Dom.attach(l1, "click", function() { self.options.showChart = (self.options.showChart + 1) % 2; self.go(false); });
        OAT.Dom.attach(l2, "click", function() { self.options.showRowChart = (self.options.showRowChart + 1) % 2; self.go(false); });
        OAT.Dom.attach(l3, "click", function() { self.options.showColChart = (self.options.showColChart + 1) % 2; self.go(false); });
    }

    this.headerRow = headerRow; /* store data */
    this.allData = dataRows; /* store data */
    this.filteredData = [];
    this.tabularData = []; /* result */

    this.dataColumnIndex = dataColumnIndex; /* store data */
    this.rowConditions = headerRowIndexes; /* indexes of row conditions */
    this.colConditions = headerColIndexes; /* indexes of column conditions */
    
    this.initRowConditions = [];
    for(var i=0; i < this.rowConditions.length; i++){
    	this.initRowConditions[i] = this.rowConditions[i];	
    }
    this.initColConditions = []
    for(var i=0; i < this.colConditions.length; i++){
    	this.initColConditions[i] = this.colConditions[i];	
    }
    
    if ((columns.length == 1) && (measures.length == 0)){
    	this.rowConditions = [0];
    }

    this.conditions = [];
    this.filterDiv.selects = [];
    this.rowStructure = {};
    this.colStructure = {};
    this.colPointers = [];
    this.rowPointers = [];
    this.rowTotals = [[],[]];
    this.colTotals = [[],[]];
    this.gTotal = [];

    this.query = query;
    this.controlName = control;
    this.conditionalFormats = condFormats;
	this.conditionalFormatsColumns = conditionalFormatsColumns;
	this.GreyList = [];
	this.EmptyValue = "#NaV#"
	this.NullValue = "#NuN#"
	
	this.initState = {};
	
	
	
    /* supplemental routines */
    this.saveState = function(state) {
    	
    	
    	
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
		
		try {
			if (!!window.localStorage) 
			{
				localStorage.removeItem(OAT.getURL()+self.query+self.controlName);
				localStorage.setItem(OAT.getURL()+self.query+self.controlName, JSON.stringify(JSON.decycle(state)));
			} else {
				OATSetCookie('"' + OAT.getURL()+self.query+self.controlName + 'cookie' + '"', JSON.stringify(state), null, "/");
			}
		} catch (error) {
			try {
    			OATSetCookie('"' + OAT.getURL()+self.query+self.controlName + 'cookie' + '"', JSON.stringify(state), null, "/");
    		} catch (error) {}
		}
	}
    
    this.getState = function(){
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
		try {
			var i;
			if (localStorage.getItem(OAT.getURL()+self.query+self.controlName) != null){
				var retrievedObject = localStorage.getItem(OAT.getURL()+self.query+self.controlName);	
				i = JSON.retrocycle(JSON.parse(retrievedObject));
			}
			
			if (i == null){
				var cookieValue = OATGetCookie('"' + OAT.getURL()+self.query+self.controlName + 'cookie' + '"')
				if (cookieValue != null){
					i = JSON.parse(cookieValue)
				}
			}
				
			return i;
		} catch (error) {
			try {
				var cookieValue = OATGetCookie('"' + OAT.getURL()+self.query+self.controlName + 'cookie' + '"')
				var i = JSON.parse(cookieValue)
				return i
			} catch (error){
				return null;
			}
		}
    
    }
   
   	this.cleanState = function(){
   		
   	 	for (var il = 0; il < self.filterDiv.selects.length; il++){
				self.filterDiv.selects[il].value = "[all]"
		}
   	 	for(var ci= 0; ci<= self.conditions.length-1; ci++){
	 		 self.conditions[ci].blackList = [];
	 		 self.conditions[ci].whiteList = [];
	 		 self.conditions[ci].greyList = [];
	 	}
   	 	localStorage.removeItem(OAT.getURL()+self.query+self.controlName);
   	 	self.deleteState = false;
   	 
   	 	var viejcolCond = new Array()
     	for(var i=0; i < self.initState.colConditions.length; i++){
     		viejcolCond[i] = self.initState.colConditions[i];
     	}
     	var viejcolRow = new Array();
     	for(var i=0; i < self.initState.rowConditions.length; i++){
     		viejcolRow[i] = self.initState.rowConditions[i];
     	}
     	var viejcolFilt = new Array();
		for(var i=0; i < self.initState.filterIndexes.length; i++){
     		viejcolFilt[i] = self.initState.filterIndexes[i];
     	}		
		this.conditions = jQuery.extend(true, [] ,self.initState.conditions);
		this.rowConditions = viejcolRow;
		this.colConditions = viejcolCond;
		this.filterIndexes = viejcolFilt;  
		this.stateChanged = self.initState.stateChanged;
		this.rowsPerPage = self.initState.rowsPerPage;
		
		var state = {
                query: self.query,
                conditions: self.conditions,
                colConditions: self.colConditions,
                rowConditions: self.rowConditions,
                filterIndexes: self.filterIndexes,
                filterDivSelects: [],
                rowsPerPage: self.rowsPerPage,
          		version: self.rememberLayoutStateVersion
          };
            
         
          self.saveState(state);
		  
   	 
   	}
   	
   	this.cleanStateWhenServerPagination = function(){
   		
   	 	for (var il = 0; il < self.filterDiv.selects.length; il++){
				self.filterDiv.selects[il].value = "[all]"
		}
   	 	for(var ci= 0; ci<= self.conditions.length-1; ci++){
	 		 self.conditions[ci].blackList = [];
	 	}
   	 	//localStorage.removeItem(OAT.getURL()+self.query+self.controlName);
   	 	//self.deleteState = false;
   	 
   	 	var viejcolCond = new Array()
     	for(var i=0; i < self.initState.colConditions.length; i++){
     		viejcolCond[i] = self.initState.colConditions[i];
     	}
     	var viejcolRow = new Array();
     	for(var i=0; i < self.initState.rowConditions.length; i++){
     		viejcolRow[i] = self.initState.rowConditions[i];
     	}
     	var viejcolFilt = new Array();
		for(var i=0; i < self.initState.filterIndexes.length; i++){
     		viejcolFilt[i] = self.initState.filterIndexes[i];
     	}		
		
		for(var i = 0; i < self.conditions.length;i++){
			if ((self.conditions[i])&&(self.conditions[i].distinctValues)){
				self.initState.distinctValues = []
				for(var j=0;j<self.conditions[i].distinctValues.length;j++){
					if (self.initState.conditions[i].distinctValues.indexOf(self.conditions[i].distinctValues[j]) == -1){
						self.initState.conditions[i].distinctValues.push(self.conditions[i].distinctValues[j])
					}	
				}
				self.initState.conditions[i].previousPage = self.conditions[i].previousPage
				self.initState.conditions[i].totalPages   = self.conditions[i].totalPages
			}
		}
		
		self.conditions = jQuery.extend(true, [] ,self.initState.conditions);
		self.pageData.AxisInfo     = jQuery.extend(true, [] ,self.initState.AxisInfo);
		self.pageData.FilterInfo   = jQuery.extend(true, [], self.initState.FilterInfo);
		self.pageData.CollapseInfo = jQuery.extend(true, [], self.initState.CollapseInfo);
		
		self.rowConditions = viejcolRow;
		self.colConditions = viejcolCond;
		self.filterIndexes = viejcolFilt;  
		self.stateChanged  = self.initState.stateChanged;
		self.rowsPerPage   = self.initState.rowsPerPage;
		self.DefaultAction = self.initState.DefaultAction;	
				
		self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
          	self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
            self.preGoWhenServerPagination(false);
        }).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
      	
   	} 
   
   	this.getDataXML = function(){
   		var dataStr;
   		if (self.serverPagination){
   			var temp = self.QueryViewerCollection[self.IdForQueryViewerCollection].getPivottableData_JS();
   			dataStr = temp.split("<OLAPData")[1];
   			dataStr = "<OLAPData " + dataStr
   		} else { 
   			dataStr = '<OLAPData format="adonet">\n <Table>\n'
   		
   			for (var i =0; i < self.GeneralDataRows.length; i++){
   				dataStr = dataStr + '  <Record>\n'
   			
   				for(var iCV=0; iCV < this.columns.length; iCV++){
   					dataStr = dataStr + '    <' + this.columns[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + self.GeneralDataRows[i][iCV]
   					dataStr = dataStr + '</' + this.columns[iCV].getAttribute("dataField") + '>\n'
   				}
   			
   				for (var iCV = 0; iCV < measures.length; iCV++) {
   					dataStr = dataStr + '    <' + measures[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + self.GeneralDataRows[i][iCV + this.columns.length]
   					dataStr = dataStr + '</' + measures[iCV].getAttribute("dataField") + '>\n'
   				}
   				
   				dataStr = dataStr + '  </Record>\n'
   			}		
   			dataStr = dataStr + '</Table>\n</OLAPData>'
   		}
   		return dataStr 
   	}
   	
   	this.getFilteredDataXML = function(){
   		if (self.serverPagination){
   			var temp = self.QueryViewerCollection[self.IdForQueryViewerCollection].getPivottableData_JS();
   			var stringRecord = temp.split("<Record>")
   			
   			var tempData = [];
   			for (var i = 1; i < stringRecord.length; i++) {
     			var recordData = [];
     			var fullRecordData = [];
       			for (var j = 0; j < self.pageData.dataFields.length; j++) {
       				recordData[j] = "#NuN#"
       				var dt = stringRecord[i].split("<" + self.pageData.dataFields[j] + ">")
       				if (dt.length > 1){
       					var at = dt[1].split("</" + self.pageData.dataFields[j] + ">")
       				/*var rp = at[0].replace(/^\s+|\s+$/g, '')
       				recordData[j] = (rp != "") ? rp : undefined*/
       					recordData[j] = at[0]
       					fullRecordData[j] = recordData[j] 
       				} else {
       					if (stringRecord[i].indexOf("<" + self.pageData.dataFields[j] + "/>") >= 0){
       						recordData[j] = ""
       						fullRecordData[j] = ""
       					}
       				
       				}
       			}
       			if (self.filterOK(recordData)){
       				tempData.push(recordData);
       			}
   			
   			}
   			
			var dataStr = '<Table>\n'
   		
   			for (var i =0; i < tempData.length; i++){
   				dataStr = dataStr + '  <Record>\n'
   			
   				for(var iCV=0; iCV < this.columns.length; iCV++){
   					dataStr = dataStr + '    <' + this.columns[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + tempData[i][iCV]
   					dataStr = dataStr + '</' + this.columns[iCV].getAttribute("dataField") + '>\n'
   				}
   			
   				for (var iCV = 0; iCV < measures.length; iCV++) {
   					dataStr = dataStr + '    <' + measures[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + tempData[i][iCV + this.columns.length]
   					dataStr = dataStr + '</' + measures[iCV].getAttribute("dataField") + '>\n'
   				}
   				
   				dataStr = dataStr + '  </Record>\n'
   			}		
   		
   			dataStr = dataStr + '</Table>'
   			return dataStr
   		} else {
   			var dataStr = '<Table>\n'
   		
   			for (var i =0; i < self.filteredData.length; i++){
   				dataStr = dataStr + '  <Record>\n'
   			
   				for(var iCV=0; iCV < this.columns.length; iCV++){
   					dataStr = dataStr + '    <' + this.columns[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + self.filteredData[i][iCV]
   					dataStr = dataStr + '</' + this.columns[iCV].getAttribute("dataField") + '>\n'
   				}
   			
   				for (var iCV = 0; iCV < measures.length; iCV++) {
   					dataStr = dataStr + '    <' + measures[iCV].getAttribute("dataField") + '>'
   					dataStr = dataStr + self.filteredData[i][iCV + this.columns.length]
   					dataStr = dataStr + '</' + measures[iCV].getAttribute("dataField") + '>\n'
   				}
   				
   				dataStr = dataStr + '  </Record>\n'
   			}		
   		
   			dataStr = dataStr + '</Table>'
   			return dataStr
   		} 
   	}
    
    this.getMetadataXML = function(){
    	xml = '<OLAPCube format="'+ this.defaultPicture.getAttribute("format") +'" thousandsSeparator="'+ this.defaultPicture.getAttribute("thousandsSeparator") +'" decimalSeparator="'+ this.defaultPicture.getAttribute("decimalSeparator") +'" dateFormat="'+ this.defaultPicture.getAttribute("dateFormat") +'">'
    	
    	for(var iCV=0; iCV < this.columns.length; iCV++){
    		xml = xml + '<OLAPDimension>'
    		
    		xml = xml + '<name>'+this.columns[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+this.columns[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+this.columns[iCV].getAttribute("description")+'</description> ';
    		xml = xml + '<dataField>'+this.columns[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+this.columns[iCV].getAttribute("dataType")+'</dataType> ';
    		//xml = xml + '<defaultPosition>'+this.columns[iCV].getAttribute("defaultPosition")+'</defaultPosition> ';
    		
    		xml = xml + '<defaultPosition>'
    		var pos = 0;
    		if ((this.columns[iCV].getAttribute("defaultPosition") == "Hidden") || (this.columns[iCV].getAttribute("defaultPosition") == "Data")){
    			xml = xml + this.columns[iCV].getAttribute("defaultPosition")
    		} else {
    			if (self.rowConditions.find(iCV)!=-1){
    				xml = xml + 'rows';
    				pos = self.rowConditions.indexOf(iCV) + 1
    			} else if (self.colConditions.find(iCV)!=-1) {
    				xml = xml + 'columns';
    				pos = self.colConditions.indexOf(iCV) + 1
    			} else {
    				xml = xml + 'pages';
    				pos = self.filterIndexes.indexOf(iCV) + 1
    			}
    		}
    		xml = xml + '</defaultPosition> ';
    		
    		xml = xml + '<axisOrder>' + pos  + '</axisOrder>'; 
    		
    		
    		xml = xml + '<validPositions>'+this.columns[iCV].getAttribute("validPositions")+'</validPositions> ';
    		xml = xml + '<summarize>'+this.columns[iCV].getAttribute("summarize")+'</summarize> ';
    		xml = xml + '<align>'+this.columns[iCV].getAttribute("align")+'</align> ';
    		
    		if (this.columns[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ this.columns[iCV].getAttribute("picture") +'</picture> ';
    		}
    		
    		if (this.columns[iCV].getAttribute("picture") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ this.columns[iCV].getAttribute("format") +'</format> ';
    		}
    		if (this.conditions[iCV].sort==1)
    			xml = xml + '<order>Ascending</order> '
    		else if (this.conditions[iCV].sort==-1)
    			xml = xml + '<order>Descending</order> '
    		else
    			xml = xml + '<order>Custom</order> '
    			 	
    		xml = xml + '<popupDisabled>'+this.columns[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		xml = xml + '<customOrder/> ';	
    		
    		xml = xml + '<filterType>'
    		
    		if (this.conditions[iCV].blackList.length == 0) {
    			xml = xml + 'ShowAllValues';
    		} else {
    			xml = xml + 'ShowSomeValues';
    		}
    		
    		xml = xml + '</filterType>'
    		
    		xml = xml + '<include> ';
    		
    		var findex = self.filterIndexes.find(iCV);
    		if ((findex != -1) && (self.filterDiv.selects[findex].value!="[all]")){ //for chart when usin top filter only include selected filter value
    			if (this.conditions[iCV].blackList.find(self.filterDiv.selects[findex].value) === -1 ){
    				xml = xml + '<value>' + self.filterDiv.selects[findex].value +'</value> ';
    			}
    		} else {
    			for (var val = 0; val < this.conditions[iCV].distinctValues.length; val++){
    				if (this.conditions[iCV].blackList.find(this.conditions[iCV].distinctValues[val]) === -1 ){
    					xml = xml + '<value>' + this.conditions[iCV].distinctValues[val] +'</value> ';
    				}
    			} 
    		}
    		xml = xml + '<value>TOTAL</value> </include>'
    		
    		xml = xml + '<collapseType>'
    		
    		if ((self.conditions[iCV].collapsedValues == undefined) || (self.conditions[iCV].collapsedValues.length == 0)){
    			xml = xml + 'ExpandAllValues'
    			xml = xml + '</collapseType>'
    		} else {
    			xml = xml + 'ExpandSomeValues'
    			xml = xml + '</collapseType>'
    			var distinctValues = self.GeneralDistinctValues[iCV];
    			if (self.serverPagination){
    				distinctValues = self.conditions[iCV].distinctValues;
    			} 
    			xml = xml + '<includeExpand> ';
    			
    			for (var val = 0; val < distinctValues.length; val++){
    				if ((distinctValues[val] != undefined) && ( self.conditions[iCV].collapsedValues.indexOf(distinctValues[val]) == -1 )){
    					xml = xml + '<value>' + distinctValues[val] + '</value>'
    				}
    			}
    			
    			xml = xml + '</includeExpand> ';
    		}
	
    		xml = xml + '</OLAPDimension>'		
    	}
    	
    	for (var iCV = 0; iCV < measures.length; iCV++) {
    		xml = xml + '<OLAPMeasure> ';
    	
    		xml = xml + '<name>'+measures[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+measures[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+ measures[iCV].getAttribute("description")+ '</description> ';
    		xml = xml + '<dataField>'+measures[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+measures[iCV].getAttribute("dataType")+'</dataType> ';
    		xml = xml + '<defaultAggregator>'+measures[iCV].getAttribute("defaultAggregator")+'</defaultAggregator> ';
    		xml = xml + '<validAggregators>'+measures[iCV].getAttribute("validAggregators")+'</validAggregators> ';
    		xml = xml + '<summarize>'+measures[iCV].getAttribute("summarize")+'</summarize> ';
    		
    		if (measures[iCV].getAttribute("format") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ measures[iCV].getAttribute("format") +'</format> ';
    		}
    		
    		xml = xml + '<align>'+measures[iCV].getAttribute("align")+'</align> ';
    		
    		if (measures[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ measures[iCV].getAttribute("picture") +'</picture> ';
    		}
    		xml = xml + '<popupDisabled>'+measures[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		
    		xml = xml + 'null </OLAPMeasure>';
    	}
    	
    	xml = xml + '</OLAPCube>'
    	return xml	
    }
    
    this.createXMLMetadata = function(){
    	var xml = '<OLAPCube format="'+ this.defaultPicture.getAttribute("format") +'" thousandsSeparator="'+ this.defaultPicture.getAttribute("thousandsSeparator") +'" decimalSeparator="'+ this.defaultPicture.getAttribute("decimalSeparator") +'" dateFormat="'+ this.defaultPicture.getAttribute("dateFormat") +'">';
    	 
    	for(var iCV=0; iCV < this.columns.length; iCV++){
    		if ((this.conditions[iCV] != false) && (this.conditions[iCV] != undefined)){    	    	
    		xml = xml + '<OLAPDimension> ';
    	
    		xml = xml + '<name>'+this.columns[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+this.columns[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+this.columns[iCV].getAttribute("description")+'</description> ';
    		xml = xml + '<dataField>'+this.columns[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+this.columns[iCV].getAttribute("dataType")+'</dataType> ';
    		xml = xml + '<defaultPosition>'+this.columns[iCV].getAttribute("defaultPosition")+'</defaultPosition> ';
    		xml = xml + '<validPositions>'+this.columns[iCV].getAttribute("validPositions")+'</validPositions> ';
    		xml = xml + '<summarize>'+this.columns[iCV].getAttribute("summarize")+'</summarize> ';
    		xml = xml + '<align>'+this.columns[iCV].getAttribute("align")+'</align> ';
    		
    		if (this.columns[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ this.columns[iCV].getAttribute("picture") +'</picture> ';
    		}
    		
    		if (this.columns[iCV].getAttribute("picture") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ this.columns[iCV].getAttribute("format") +'</format> ';
    		}
    		
    		if (this.conditions[iCV].sort===1)
    			xml = xml + '<order>ascending</order> '
    		else
    			xml = xml + '<order>descending</order> '
    		xml = xml + '<popupDisabled>'+this.columns[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		xml = xml + '<customOrder/> ';
    		xml = xml + '<include> ';
    		
    		var findex = self.filterIndexes.find(iCV);
    		if ((findex != -1) && (self.filterDiv.selects[findex].value!="[all]")){ //for chart when usin top filter only include selected filter value
    			if (this.conditions[iCV].blackList.find(self.filterDiv.selects[findex].value) === -1 ){
    				xml = xml + '<value>' + self.filterDiv.selects[findex].value.toString().trim()  +'</value> ';
    			}
    		} else {
    			if (!self.serverPagination){
    				for (var val = 0; val < this.conditions[iCV].distinctValues.length; val++){
    					if (this.conditions[iCV].blackList.find(this.conditions[iCV].distinctValues[val]) === -1 ){
    						xml = xml + '<value>' + this.conditions[iCV].distinctValues[val].toString().trim()  +'</value> ';
    					}
    				}
    			} else {
    				if (self.conditions[iCV].state != "none"){
    					for (var val = 0; val < this.conditions[iCV].distinctValues.length; val++){
    						if (this.conditions[iCV].blackList.find(this.conditions[iCV].distinctValues[val]) === -1 ){
    							xml = xml + '<value>' + this.conditions[iCV].distinctValues[val].toString().trim()  +'</value> ';
    						}
    					}	
    				}	
    			} 
    		}
    		xml = xml + '<value>TOTAL</value> </include> <collapse/> null null null ';
    		
    		xml = xml + '<hide> '
    		if (self.serverPagination){
    			if (self.conditions[iCV].state == "none"){
    				for (var val = 0; val < this.conditions[iCV].distinctValues.length; val++){
    					xml = xml + '<value>' + this.conditions[iCV].distinctValues[val].toString().trim()  +'</value> ';
    				}
    			} else if (self.conditions[iCV].state != "all") {
    				for (var yu = 0; yu < this.conditions[iCV].blackList.length; yu++) {
						xml = xml + '<value>' + this.conditions[iCV].blackList[yu].toString().trim()  +'</value> ';    			
	    			}		
    			}
    		} else {
    			for (var yu = 0; yu < this.conditions[iCV].blackList.length; yu++) {
					xml = xml + '<value>' + this.conditions[iCV].blackList[yu].toString().trim()  +'</value> ';    			
    			}
    		}
    		xml = xml + '</hide> '
    		
    		if (self.rowConditions.find(iCV)!=-1){
    			xml = xml + '<condition>row</condition> ';
    			xml = xml + '<filterbar>no</filterbar> ';
    			xml = xml + '<position>'+ self.rowConditions.find(iCV) +'</position>'
    		} else if (self.colConditions.find(iCV)!=-1) {
    			xml = xml + '<condition>col</condition> ';
    			xml = xml + '<filterbar>no</filterbar> ';
    			xml = xml + '<position>'+ self.colConditions.find(iCV) +'</position>'
    		} else {
    			xml = xml + '<condition>none</condition> ';
    			xml = xml + '<filterbar>yes</filterbar> ';
    		}
    		
    		xml = xml + '<filterdivs> ';
    		if (findex != -1){
    			xml = xml + '<value>' + self.filterDiv.selects[findex].value.toString().trim() + '</value> ';
			}
    		xml = xml + '</filterdivs> ';
    		
    		xml = xml + '<restoreview>no</restoreview> ';
    		xml = xml + ' </OLAPDimension>';
    	}
    	}
    	
    	for (var iCV = 0; iCV < measures.length; iCV++) {
    		xml = xml + '<OLAPMeasure> ';
    	
    		xml = xml + '<name>'+measures[iCV].getAttribute("name")+'</name> '
    		xml = xml + '<displayName>'+measures[iCV].getAttribute("displayName")+'</displayName> ';
    		xml = xml + '<description>'+ measures[iCV].getAttribute("description")+ '</description> ';
    		xml = xml + '<dataField>'+measures[iCV].getAttribute("dataField")+'</dataField> ';
    		xml = xml + '<dataType>'+measures[iCV].getAttribute("dataType")+'</dataType> ';
    		xml = xml + '<defaultAggregator>'+measures[iCV].getAttribute("defaultAggregator")+'</defaultAggregator> ';
    		xml = xml + '<validAggregators>'+measures[iCV].getAttribute("validAggregators")+'</validAggregators> ';
    		xml = xml + '<summarize>'+measures[iCV].getAttribute("summarize")+'</summarize> ';
    		
    		if (measures[iCV].getAttribute("format") === ""){    		
    			xml = xml + '<format/> ';
    		} else {
    			xml = xml +  '<format>'+ measures[iCV].getAttribute("format") +'</format> ';
    		}
    		
    		xml = xml + '<align>'+measures[iCV].getAttribute("align")+'</align> ';
    		
    		if (measures[iCV].getAttribute("picture") === ""){
    			xml = xml + '<picture/> '
    		} else {
    			xml = xml +  '<picture>'+ measures[iCV].getAttribute("picture") +'</picture> ';
    		}
    		xml = xml + '<popupDisabled>'+measures[iCV].getAttribute("popupDisabled")+'</popupDisabled> ';
    		
    		xml = xml + ' </OLAPMeasure>';
    		
    	}
    	xml = xml + "</OLAPCube>";
    	
    	return xml;
    }
   
    this.ExportToXML = function(){
    	var xml = '<EXPORT format="XML" type="pivot">';
    	xml = xml + '<METADATA>';
    		for(var iCV=0; iCV < this.columns.length; iCV++){
    			
    			var position = 'row';
    			if (self.rowConditions.find(iCV)!=-1){
    				position = 'row';
    			} else if (self.colConditions.find(iCV)!=-1) {
    				position = 'column';
    			} else {
    				position = 'filter';
    			}
    			
    			xml = xml + '<OLAPDimension ';
    			xml = xml + 'name="'    + this.columns[iCV].getAttribute("dataField")   + '" ';
    			xml = xml + 'label="'   + this.columns[iCV].getAttribute("displayName") + '" ';
    			xml = xml + 'picture="' + this.columns[iCV].getAttribute("picture")     + '" ';
    			xml = xml + 'datatype="'+ this.columns[iCV].getAttribute("dataType")    + '" ';
    			xml = xml + 'showAll="true" ';
    			xml = xml + 'position="' + position                                     + '">';
    			
    			if ((this.conditions[iCV] != undefined) && (this.conditions[iCV].distinctValues != undefined)){
    				for (var val = 0; val < this.conditions[iCV].distinctValues.length; val++){
    					xml = xml + '<VALUE CHECKED='; 
    					if (this.conditions[iCV].blackList.find(this.conditions[iCV].distinctValues[val]) === -1 ){
    						xml = xml + '"true"';
    					} else {
    						xml = xml + '"false"';
    					}
    					if (this.conditions[iCV].collapsedValues.indexOf(this.conditions[iCV].distinctValues[val]) == -1){
    						xml = xml + ' COLLAPSED="false">'
    					} else {
    						xml = xml + ' COLLAPSED="true">'
    					}
    					xml = xml + this.conditions[iCV].distinctValues[val] +'</VALUE>';
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
    		for (var i = 0; i < this.allDataWithoutSort.length; i++){
    			xml = xml + '<ROW ';
    			for(var iCV=0; iCV < this.columns.length; iCV++){
    				xml = xml + this.columns[iCV].getAttribute("dataField") + '="'+ this.allDataWithoutSort[i][iCV] + '" ';
    			}
    			for (var iCV = 0; iCV < measures.length; iCV++) {
    				xml = xml + measures[iCV].getAttribute("dataField")     + '="'+ this.allDataWithoutSort[i][iCV+this.columns.length] + '" ';
    			}
    			xml = xml + '/>'
    		}
    	xml = xml + '</FLATDATA>';
    	
    	xml = xml + '<COLITEMS>' //include measures & dimensions move to columns
    		var previuosXml = xml;
    		try { 
    		var level    = 0;
    		var maxlevel = this.colConditions.length; 
    		if (level === maxlevel){
    			for (var iCV = 0; iCV < measures.length; iCV++) {
    				xml = xml + '<COLITEM measure="' + measures[iCV].getAttribute("displayName") + '" type=""'
    				xml = xml + '/>';
    			}
    		} else {
    			level++;
    			for (var iCV = 0; iCV < this.colStructure.items.length; iCV++) {
    				xml = xml + this.createXMLCOLITEMS(this.colStructure.items[iCV], level, maxlevel);
    			}
    		}
    		} catch (ERROR) {
    			xml = previuosXml;
    		}
    	xml = xml + '</COLITEMS>'
    	
    	xml = xml + '<ROWITEMS>'
    		previuosXml = xml;
    		try {
    			for (var iCV = 0; iCV < this.rowStructure.items.length; iCV++){
    				var level = 0;
    				var maxlevel = this.columns.length - this.colConditions.length - this.filterIndexes.length - 1;
    				if (maxlevel >= level){
    					xml = xml + this.createXMLROWITEMS(this.rowStructure.items[iCV], level, maxlevel);
    				}
    			}
    		} catch (ERROR) {
    			xml = previuosXml;
    		}
    	xml = xml + '</ROWITEMS>'
    	
    	
    	//add Html info
    	xml = xml + '<HTML>';
    	
    	xml = xml + '<HEAD>';
    	xml = xml + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>';
    	
    	xml = xml + '<STYLE>';
    	
    	xml = xml + '.h2title {background-color: #5C5D5F; border-color: #666666; font-family: Verdana; font-weight: normal; font-size: 10pt; height: 44px; color: #E0E0E0;}\n';
		xml = xml + '.h2titlewhite {background-color: #ffffff; font-family: Verdana; font-size: 10pt; font-weight: normal; height: 25px; color: black;}\n';
    	xml = xml + '.even {background-color: #FEFEFE;	font-weight: normal; font-family: Verdana; font-size: 10pt;	padding: 5px; }\n';
		xml = xml + '.h2subtitle {background-color: #5C5D5F; border-color: #666666; font-family: Verdana; font-weight: normal; font-size: 10pt;	height: 22px; color: #E0E0E0;}\n';
		xml = xml + '.gtotal {background-color: #EBEBEB; font-weight: normal; font-family: Verdana; font-size: 10pt;}\n';
		xml = xml + '.pivot_table td.total {background-color: #EBEBEB; font-weight: normal;	font-family: Verdana; font-size: 10pt;}\n';
		xml = xml + '.pivot_table td.subtotal {background-color: #EBEBEB; font-weight: normal;	font-family: Verdana; font-size: 10pt;}\n';
		
		xml = xml + '</STYLE>';
    	xml = xml + '</HEAD>';
    	
    	xml = xml + '<BODY>';
    	xml = xml + '<TABLE border="2">'
		
			for (var i = 0; i < jQuery("#" + self.controlName + "_" + self.query + " tr").length; i++) {//for every row
				xml = xml + '<TR>';
			
				var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[i];
				for (var j = 0; j < tRow.children.length; j++){
					var childText   = tRow.children[j].textContent;
					var hidden      = tRow.children[j].getAttribute('hidden');
					
					var styleString = "";
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						styleString = " style=\"" + tRow.children[j].getAttribute("style") + "\" ";
					}
					
					var classString = "";
					if ((tRow.children[j].getAttribute("class")!=undefined) && (tRow.children[j].getAttribute("class")!=null)){
						classString = " class=\"" + tRow.children[j].getAttribute("class") + "\" ";
					}
					
					if (hidden === null){ 
						var rowSpan   = tRow.children[j].getAttribute('rowspan');
						var colSpan   = tRow.children[j].getAttribute('colspan');
						if (((rowSpan === null) && (colSpan === null)) || (j === tRow.children.length-1)){
							xml = xml + '<TD ' + classString + ' ' + styleString +'>' + childText + '</TD>';
						} else if (colSpan === null) {
							xml = xml + '<TD ' + classString + " " + 'rowspan="' + rowSpan + '" '+ styleString +' >' + childText + '</TD>';
						} else if (rowSpan === null) {
							xml = xml + '<TD ' + classString + " " + 'colspan="' + colSpan + '" '+ styleString +' >' + childText + '</TD>';
						} else {
							xml = xml + '<TD ' + classString + " " + 'colspan="' + colSpan + '" rowspan="' + rowSpan + '" '+ styleString +' >' + childText + '</TD>';
						}
					}
				}
			
				xml = xml + '</TR>';
			}
	
	
			xml = xml + '</TABLE>'; 
			
    	xml = xml + '</BODY>';
    	
    	xml = xml + '</HTML>';
    	//
    	xml = xml + "</EXPORT>";
    	
    	return xml.replace(/\&/g, "&amp;");
    	
    }
    
    this.createXMLROWITEMS = function(item ,level, maxlevel){
    	var parentCollapse = ((item.parent == undefined) || (item.parent.collapsed == undefined)) ? false : item.parent.collapsed;
    	var str = '<ROWITEM dimension="' + columns[level].getAttribute('dataField') +'" collapsed="' + item.collapsed + '" parentCollapsed="' + parentCollapse + '" value="' + item.value + '" level="' + level + '"'
    	if (level === maxlevel){
    		return str + "/>";
    	} else {
    		level++;
    		str = str + ">";
    		for(var i = 0; i < item.items.length; i++){
    			str = str + this.createXMLROWITEMS(item.items[i], level, maxlevel); 
    		}
    		str = str + '</ROWITEM>'
    		return str;
    	}
    }
    
    this.createXMLCOLITEMS = function(item ,level, maxlevel){
    	var str = ""
    	if (level === maxlevel){
    		for (var iCV = 0; iCV < measures.length; iCV++) {
    				str = str + '<COLITEM measure="' + measures[iCV].getAttribute("displayName") + '" type="">'
    					var inRow = self.rowConditions.length - measures.length + 1;
    					str = str +'<VALITEM dimension="' + columns[level-1+inRow].getAttribute('dataField') +'" value="'+item.value+'"/>' //level + cantidad de dimensiones que no estan movidas a las columnas
    					var superItem = item;
    					while (superItem.parent.depth != -1){
    						superItem = superItem.parent;
    						str = str +'<VALITEM dimension="' + columns[level-1+inRow].getAttribute('dataField') +'" value="'+superItem.value+'"/>'
    					}
    				str = str + '</COLITEM>';
    		}
    		return str;
    	} else {
    		level++;
    		for(var i = 0; i < item.items.length; i++){
    			str = str + this.createXMLCOLITEMS(item.items[i], level, maxlevel); 
    		}
    		return str; 
    	}
    }
   
	this.ExportToExcel = function(fileName){
			var table = '<table border="2">'
		
			for (var i = 0; i < jQuery("#" + self.controlName + "_" + self.query + " tr").length; i++) {//for every row
				table = table + '<tr>';
			
				var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[i];
				for (var j = 0; j < tRow.children.length; j++){
					var childText   = tRow.children[j].textContent.replace(/^\s+|\s+$/g, '');
					var hidden      = tRow.children[j].getAttribute('hidden');
					
					var styleString = "";
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						styleString = " style=\"" + tRow.children[j].getAttribute("style") + "\" ";
					}
					
					var parseText = ""
					var previusJap = false;
					for(var c=0; c < childText.length; c++){
						if (childText.charCodeAt(c) < 1000){
							if (previusJap) {
								parseText = parseText + " " + childText[c];
							} else {
								parseText = parseText + childText[c];
							}
							previusJap = false;
						} else {
							var Hex = childText.charCodeAt(c).toString(16);
							parseText = parseText + "&#x" + Hex;
							previusJap = true;
						}
					}
					
					
					if (hidden === null){ 
						var rowSpan   = tRow.children[j].getAttribute('rowspan');
						var colSpan   = tRow.children[j].getAttribute('colspan');
						if (((rowSpan === null) && (colSpan === null)) || (j === tRow.children.length-1)){
							table = table + '<td '+ styleString +'>' + parseText + '</td>';
						} else if (colSpan === null) {
							table = table + '<td rowspan="' + rowSpan + '" '+ styleString +' >' + parseText + '</td>';
						} else if (rowSpan === null) {
							table = table + '<td colspan="' + colSpan + '" '+ styleString +' >' + parseText + '</td>';
						} else {
							table = table + '<td colspan="' + colSpan + '" rowspan="' + rowSpan + '" '+ styleString +' >' + parseText + '</td>';
						}
					}
				}
			
				table = table + '</tr>';
			}
	
			table = table + '</table>'; //</tbody>
			var dtltbl = table;
					
			if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ 
				window.open('data:application/vnd.ms-excel,' + encodeURIComponent(dtltbl));
			} else if ( (!gx.util.browser.isIE()) ||  (9<gx.util.browser.ieVersion()) ){
				var blob = new Blob([dtltbl], {type: "application/vnd.ms-excel"});
            	saveAs( blob, fileName+".xls");
			} else {
				return dtltbl; 
			}
		
	}
	
	
    this.lightOn = function() {
        for (var i = 0; i < self.gd.targets.length; i++) {
            var elm = self.gd.targets[i][0];
            //elm.style.color = "#fcc";//"#f00"; 00f
    //        elm.setAttribute("class", "h2title drag-drop")
            if (gx.util.browser.isIE()){
        		elm.className += " drag-drop";
        	} else {
	        	elm.classList.add("drag-drop");
	        }
        }
    }

    this.lightOff = function() {
        for (var i = 0; i < self.gd.targets.length; i++) {
            var elm = self.gd.targets[i][0];
            try {
            	if (gx.util.browser.isIE() && 11>gx.util.browser.ieVersion()){
        			elm.className.replace("drag-drop");
        		} else {
	        		elm.classList.remove("drag-drop");
	        	}
	        } catch (error) {
	        	
	        }
        }
    }
    self.gd.onFail = self.lightOff;

    this.process = function(elm) {
        self.lightOn();
        elm.style.backgroundColor = "#888";
        elm.style.padding = "2px";
        elm.style.cursor = "pointer";
        //OAT.Dom.attach(elm, "mouseup", function(e) { self.lightOff(); });
    }

    this.filterOK = function(row) { /* does row pass filters? */
        for (var i = 0; i < self.filterIndexes.length; i++) { /* for all filters */
            var fi = self.filterIndexes[i]; /* this column is important */
            var s = self.filterDiv.selects[i]; /* select node */
            if (s.selectedIndex && OAT.$v(s) != row[fi]) { return false; }
        }
        
        //var notInBlackList = true;
       for (var i = 0; i < self.rowConditions.length; i++) { /* row blacklist */
            var value = row[self.rowConditions[i]];
            var cond = self.conditions[self.rowConditions[i]];
            if (cond.blackList.find(value) != -1) { /*notInBlackList = false; */return false; }
         
        }
      	
      	
        for (var i = 0; i < self.colConditions.length; i++) { /* column blacklist */
            var value = row[self.colConditions[i]];
            var cond = self.conditions[self.colConditions[i]];
            if (cond.blackList.find(value) != -1) { return false; }
        }
        return true;
    }

    this.sort = function(cond, index) { /* sort distinct values of a condition */
        var sortFunc;
        var coef = cond.sort; if (cond.sort == 0) { coef = 1 }  if (cond.sort == 2) { coef = -1}
        var numSort = function(a, b) {
            if (a == b) { return 0; }
            return coef * (parseInt(a) > parseInt(b) ? 1 : -1);
        }
        var dictSort = function(a, b) {
            if (a == b) { return 0; }
            return coef * (a > b ? 1 : -1);
        }
        
        if (cond.distinctValues == undefined) return;
        
        //new code
        var sortInt = true;
        for (var ival = 0; ival < cond.distinctValues.length; ival++) {
        	if ((sortInt) && (cond.distinctValues[ival] != parseInt(cond.distinctValues[ival]))){
        		sortInt  = false;
        	}
        }
        if (sortInt) { sortFunc = numSort; } else { sortFunc = dictSort; } //decides the type of sorting
        //end new code
        var testValue = cond.distinctValues[0];
        
        if ((cond.sort != 0) && (cond.sort != 2)){
        	cond.distinctValues.sort(sortFunc);
        } else {
        	if ((index != undefined) && (self.columns[index] != undefined)){
        		cond.distinctValues.sort(sortFunc);
        	 
        	 	var prevValues = [];
        	 	for (var h = 0; h < cond.distinctValues.length; h++){
        	 		prevValues[h] = cond.distinctValues[h]; 
        	 	}
        		
       			cond.distinctValues = [];
       			if (cond.sort == 0){
        			for(var h=0; h<self.columns[index].childNodes.length;h++){
        				if ((self.columns[index].childNodes[h]!=undefined) &&
        					(self.columns[index].childNodes[h].localName!=undefined) &&
								(self.columns[index].childNodes[h].localName==="customOrder")) 
        				{
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if (self.columns[index].childNodes[h].childNodes[n].localName == "Value") {
        							//cond.distinctValues.push(self.columns[index].childNodes[h].childNodes[n].textContent);
        							var notTrimValue = self.columns[index].childNodes[h].childNodes[n].textContent
        							for (var m = 0; m < prevValues.length; m++){
        								if (prevValues[m].trim() == notTrimValue.trim()){
        									cond.distinctValues.push(prevValues[m]);		
        								}	
        							}
        						}
        					}
        				}
        			}
        		} else {
        			for(var h=0; h<self.columns[index].childNodes.length;h++){
        				if ((self.columns[index].childNodes[h]!=undefined) &&
        					(self.columns[index].childNodes[h].localName!=undefined) &&
								(self.columns[index].childNodes[h].localName==="customOrder")) 
        				{
        					for (var n = self.columns[index].childNodes[h].childNodes.length-1; n >= 0; n--) {
        						if (self.columns[index].childNodes[h].childNodes[n].localName == "Value") {
        							//cond.distinctValues.push(self.columns[index].childNodes[h].childNodes[n].textContent);
        							var notTrimValue = self.columns[index].childNodes[h].childNodes[n].textContent
        							for (var m = 0; m < prevValues.length; m++){
        								if (prevValues[m].trim() == notTrimValue.trim()){
        									cond.distinctValues.push(prevValues[m]);		
        								}	
        							}
        						}
        					}
        				}
        			}
        		}
        		if (cond.distinctValues.length < prevValues.length){
        			for(var h = 0; h < prevValues.length; h++){
        				if (cond.distinctValues.indexOf(prevValues[h]) == -1){
        					cond.distinctValues.push(prevValues[h])
        				}
        			}
        		}
        	}
        }
        if ((columns.length == 1) || (measures.length = 0)){
        	if (cond.sort==1){
        		self.allData.sort((function(index){
    					return function(a, b){
       						return (a[index] === b[index] ? 0 : (a[index] < b[index] ? -1 : 1));
    						};
					})(0));
			} else if (cond.sort==-1){
				self.allData.sort((function(index){
    					return function(a, b){
       						return (a[index] === b[index] ? 0 : (a[index] > b[index] ? -1 : 1));
    						};
					})(0));
			} 
        }
    } /* sort */
   
  	/* init routines */
    this.initCondition = function(index) {
        if ((index == self.dataColumnIndex) && (((columns.length > 1) || (measures.length > 0)))) { /* dummy condition */
            self.conditions.push(false);
            return;
        }
        var sortValue = 1;
        //if (self.serverPagination){
        //	sortValue = 0
        //}
        var showSubtotals = 1;
        var hideSubtotalsOption = false;
        var validPosition = "filters;rows;columns;hidden";
        var isDimension = false;
        if (self.columns[index]!=undefined){
        	isDimension = true;
        	if (self.serverPagination){
        		if((self.columns[index].getAttribute("order")!=undefined) && (self.columns[index].getAttribute("order")==="ascending")){
        			sortValue = 1;
        		}	
        	}
        	if((self.columns[index].getAttribute("order")!=undefined) && (self.columns[index].getAttribute("order")==="descending")){
        		sortValue = -1;
        	}
        	if((self.columns[index].getAttribute("order")!=undefined) && (self.columns[index].getAttribute("order")==="custom")){
        		sortValue = 0;
        		if (self.serverPagination){
        			sortValue = 1;
        		}
        	}
        	if((self.columns[index].getAttribute("summarize")!=undefined) && (self.columns[index].getAttribute("summarize")==="yes")){
        		showSubtotals = 1;
        		if ((self.columns[index].firstElementChild != undefined) && (self.columns[index].firstElementChild.localName == "exclude")){
        			showSubtotals = false;
        		}
        	}
        	if((self.columns[index].getAttribute("summarize")!=undefined) && (self.columns[index].getAttribute("summarize")==="no")){
        		showSubtotals = false;
        		hideSubtotalsOption = true;
        	}
        	if((self.columns[index].getAttribute("validPositions")!=undefined)){
        		validPosition = self.columns[index].getAttribute("validPositions");
        	}
        	
        }
        
        var cond = { distinctValues: [], blackList: [], whiteList: [], greyList: [], collapsedValues: [],
        				sort: sortValue, subtotals: showSubtotals, hideSubtotalOption: hideSubtotalsOption, validPosition: validPosition,
        					dataRowPosition: index, isDimension: isDimension, topFilterValue: gx.getMessage("GXPL_QViewerJSAllOption")/*"[all]"*/ }
        if (self.serverPagination){
        	cond.topFilterValue = "[all]"
        	cond.topFilterString=gx.getMessage("GXPL_QViewerJSAllOption")
        }
                
        self.conditions.push(cond);
        if (!self.serverPagination){
        	for (var i = 0; i < self.allData.length; i++) {
        	    var value = self.allData[i][index];
        	    if (value == undefined){
        	    	value = " ";
        	    	self.allData[i][index] = " "; 
        	    }
        	    if (cond.distinctValues.find(value) == -1) { /* not yet present */
        	        cond.distinctValues.push(value);
        	    } /* if new value */
        	} /* for all rows */
        	if ((sortValue != 0) && (sortValue != 2)){
        		self.sort(cond);
        	} else { //sort with order custom
        		if (self.columns[index]!=undefined){ 
        		 var tempCondSort = cond.sort;
        		 if (cond.sort != 2){
        		 	cond.sort = 1;
        		 } else {
        		 	cond.sort = -1;
        		 }
        		 self.sort(cond)
        		 cond.sort = tempCondSort; 
        	 
        		 var prevValues = [];
        		 for (var h = 0; h < cond.distinctValues.length; h++){
        		 	prevValues[h] = cond.distinctValues[h]; 
        		 }
        	 
        		 cond.distinctValues = [];
        		 for(var h=0; h<self.columns[index].childNodes.length;h++){
        			if ((self.columns[index].childNodes[h]!=undefined) &&
        				(self.columns[index].childNodes[h].localName!=undefined) &&
							(self.columns[index].childNodes[h].localName==="customOrder")) 
        			{
        				if (sortValue == 0){
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if (self.columns[index].childNodes[h].childNodes[n].localName == "Value") {
        							//cond.distinctValues.push(self.columns[index].childNodes[h].childNodes[n].textContent);
        							var notTrimValue = self.columns[index].childNodes[h].childNodes[n].textContent
        							for (var m = 0; m < prevValues.length; m++){
        								if (prevValues[m].trim() == notTrimValue.trim()){
        									cond.distinctValues.push(prevValues[m]);		
        								}	
        							}
        						}
        					}
        				} else {
        					for (var n = self.columns[index].childNodes[h].childNodes.length-1; n >= 0 ; n--) {
        						if (self.columns[index].childNodes[h].childNodes[n].localName == "Value") {
        							//cond.distinctValues.push(self.columns[index].childNodes[h].childNodes[n].textContent);
        							var notTrimValue = self.columns[index].childNodes[h].childNodes[n].textContent
        							for (var m = 0; m < prevValues.length; m++){
        								if (prevValues[m].trim() == notTrimValue.trim()){
        									cond.distinctValues.push(prevValues[m]);		
        								}	
        							}
        						}
        					}
        				}
        				if (cond.distinctValues.length < prevValues.length){
        					for (var h = 0; h < prevValues.length; h++){
        						if (cond.distinctValues.indexOf(prevValues[h]) == -1){
        							cond.distinctValues.push(prevValues[h]);
        						}
        					}
        				}
        			}
        		 }
        		}
        	}
        }
        if (self.columns[index] != undefined){
    			for(var h=0; h<self.columns[index].childNodes.length;h++){
        			if ((self.columns[index].childNodes[h]!=undefined) &&
        				(self.columns[index].childNodes[h].localName!=undefined) &&
							(self.columns[index].childNodes[h].localName==="include")) 
        			{
        				for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        					if ((self.columns[index].childNodes[h].childNodes[n].localName != null) && 
        					(self.columns[index].childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        						if (self.columns[index].childNodes[h].childNodes[n].textContent != "TOTAL"){
        							self.conditions[index].topFilterValue = self.columns[index].childNodes[h].childNodes[n].textContent;
        						}
        					}
        				}
        			}
       			}
       }
        
    }
    
    this.restoreSubtotalsAndSortLayout = function(index){
    	//restore save conditions
    	try{
       	if(rememberLayout) {
			mState = self.getState();
			if((mState != undefined) && (mState.version != undefined) && (mState.version === self.rememberLayoutStateVersion)) { //check version
				if ((mState.query == self.query) && (self.conditions.length == mState.conditions.length)  ){
					self.conditions[index].subtotals =  mState.conditions[index].subtotals;
					self.conditions[index].sort 	 =  mState.conditions[index].sort;
					self.conditions[index].collapsedValues = mState.conditions[index].collapsedValues;
					//if (!self.autoPaging){
						//self.conditions[index].distinctValues  =  mState.conditions[index].distinctValues;
					//}
				}
			}
		 }
		} catch (Error) {}
    }
    
    this.applyCustomFilters = function(index){
    	try {
    		if (index == self.dataColumnIndex) { /* dummy condition */
            	return;
        	}
        	if (self.columns[index] != undefined){
    			for(var h=0; h<self.columns[index].childNodes.length;h++){
        			if ((self.columns[index].childNodes[h]!=undefined) &&
        				(self.columns[index].childNodes[h].localName!=undefined) &&
							(self.columns[index].childNodes[h].localName==="include")) 
        			{
        				if (!self.serverPagination)
        				{
        					//push all in blacklist 
        					self.conditions[index].blackList = [];
        					for(var i=0; i<self.GeneralDistinctValues[index].length; i++){
        						self.conditions[index].blackList.push(self.GeneralDistinctValues[index][i])
        					}
        			
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if ((self.columns[index].childNodes[h].childNodes[n].localName != null) && 
        						(self.columns[index].childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        							var ind = self.conditions[index].blackList.indexOf(self.columns[index].childNodes[h].childNodes[n].textContent);
        							if (ind!=-1){
        								self.conditions[index].topFilterValue = self.columns[index].childNodes[h].childNodes[n].textContent;
										self.conditions[index].blackList.splice(ind, 1);
									}
        						}
        					}
        					if (self.filterIndexes.indexOf(index) != -1){
        						self.conditions[index].blackList = [];	
        					}
        				} else {
        					//push all in blacklist
        					self.createFilterInfo({ op: "none", values: "", dim: index });
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if ((self.columns[index].childNodes[h].childNodes[n].localName != null) && 
        						(self.columns[index].childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        							if (self.columns[index].childNodes[h].childNodes[n].textContent != "TOTAL"){
        								if (self.UserFilterValues[index] == undefined) self.UserFilterValues[index] = [];  
        								self.UserFilterValues[index].push(self.columns[index].childNodes[h].childNodes[n].textContent.trim());
        							}
        						}
        					}
        				}
        			}
        			//add expand collapse info
        			var rowPos = self.rowConditions.indexOf(index);
        			var colPos = self.colConditions.indexOf(index);
        			if (((colPos < self.colConditions.length-1) && (colPos!=-1)) || ((rowPos < self.rowConditions.length - measures.length) && (rowPos!=-1))){
        			if ((self.columns[index].childNodes[h]!=undefined) &&
        				(self.columns[index].childNodes[h].localName!=undefined) &&
							(self.columns[index].childNodes[h].localName==="expand")) 
        			{
        				if (!self.serverPagination)
        				{
        					self.conditions[index].collapsedValues = [];
        					for(var i=0; i<self.GeneralDistinctValues[index].length; i++){
        						self.conditions[index].collapsedValues.push(self.GeneralDistinctValues[index][i])
        					}
        				
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if ((self.columns[index].childNodes[h].childNodes[n].localName != null) && 
        						(self.columns[index].childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        							var ind = self.conditions[index].collapsedValues.indexOf(self.columns[index].childNodes[h].childNodes[n].textContent);
        							if (ind!=-1){
										self.conditions[index].collapsedValues.splice(ind, 1);
									}
        						}
        					}	
        				} else {
        					
        					
        					for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        						if ((self.columns[index].childNodes[h].childNodes[n].localName != null) && 
        						(self.columns[index].childNodes[h].childNodes[n].localName.toLowerCase() === "value")) {
        							if (self.UserExpandValues[index] == undefined) self.UserExpandValues[index] = [];
        							self.UserExpandValues[index].push(self.columns[index].childNodes[h].childNodes[n].textContent.trim());
        						}
        					}
        					if (self.UserExpandValues[index]==undefined){
        						self.UserExpandValues[index] = ["#ALLCOLLAPSE#"];
        					}
        				}
        			}
        			}
        		}
        	}
       } catch (error){
       	
       }
    }
    
    /* pseudo-init routines */
    this.pseudoInitCondition = function(index, prevCondition) {
        if (index == self.dataColumnIndex) { /* dummy condition */
            self.conditions.push(false);
            return;
        }
        var sortValue = 1;
        var showSubtotals = 1;
        var hideSubtotalsOption = false;
        var validPosition = "filters;rows;columns;hidden";
        var isDimension = false;
        if (self.columns[index]!=undefined){
        	var isDimension = true;
        	if((self.columns[index].getAttribute("order")!=undefined) && (self.columns[index].getAttribute("order")==="descending")){
        		sortValue = -1;
        	}
        	if((self.columns[index].getAttribute("order")!=undefined) && (self.columns[index].getAttribute("order")==="custom")){
        		sortValue = 0;
        	}
        	if((self.columns[index].getAttribute("summarize")!=undefined) && (self.columns[index].getAttribute("summarize")==="yes")){
        		showSubtotals = 1;
        		if ((self.columns[index].firstElementChild != undefined) && (self.columns[index].firstElementChild.localName == "exclude")){
        			showSubtotals = false;	
        		}
        	}
        	if (prevCondition[index].sort != undefined){
        		sortValue = prevCondition[index].sort;
        	}
        	if (prevCondition[index].subtotals != undefined){
        		showSubtotals = prevCondition[index].subtotals;
        	}
        	if((self.columns[index].getAttribute("summarize")!=undefined) && (self.columns[index].getAttribute("summarize")==="no")){
        		showSubtotals = false;
        		hideSubtotalsOption = true; 
        	}
        	if((self.columns[index].getAttribute("validPositions")!=undefined)){
        		validPosition = self.columns[index].getAttribute("validPositions");
        	}
        }
        
        var cond = { distinctValues: [], blackList: [], whiteList: [], greyList: [], collapsedValues: [],
        					sort: sortValue, subtotals: showSubtotals, hideSubtotalOption: hideSubtotalsOption, validPosition: validPosition,
        					dataRowPosition: index, isDimension: isDimension, topFilterValue: gx.getMessage("GXPL_QViewerJSAllOption")/*"[all]"*/ }
        					
        self.conditions.push(cond);
        for (var i = 0; i < self.allData.length; i++) {
            var value = self.allData[i][index];
            if (value == undefined){
            	value = " ";
            	self.allData[i][index] = " "; 
            }
            if (cond.distinctValues.find(value) == -1) { /* not yet present */
                cond.distinctValues.push(value);
            } /* if new value */
        } /* for all rows */
       
		/*set previus black & collapsed values list*/
		cond.blackList = prevCondition[index].blackList;
		cond.collapsedValues = prevCondition[index].collapsedValues;
	
		if (self.allData.length > 0){
        	self.sort(cond, index);
        }
    }
	
    this.init = function() {
        self.propPage = OAT.Dom.create("div", {});
       
        if (dataRows[0] != undefined){ //if data available
        
        
        for (var i = 0; i < self.headerRow.length; i++) {
            self.initCondition(i);
            self.applyCustomFilters(i);
        }
        
        //save default view values
        var oldConditions = jQuery.extend(true, [] ,self.conditions);
        
        var defcolCond = new Array() 
        for(var i=0; i < self.initRowConditions.length; i++){
        	defcolCond[i] = self.initRowConditions[i];
        }
        var defcolRow = new Array();
        for(var i=0; i < self.initColConditions.length; i++){
        	defcolRow[i] = self.initColConditions[i];
        }
        var defStIndex = new Array();
        for(var i=0; i < self.initFilterIndexes.length; i++){
        	defStIndex[i] = self.initFilterIndexes[i];
        }
        
        self.initState = { 
                query: self.query,
                conditions: oldConditions,
                colConditions: defcolRow,
                rowConditions: defcolCond,
                filterDivSelects: [],
                filterIndexes: defStIndex,
                stateChanged: false,
                rowsPerPage: pageSize
        };
        //end save default view values
        
        for (var i = 0; i < self.headerRow.length; i++) {
        	self.restoreSubtotalsAndSortLayout(i)
        }
        
        //get previuos state
    	var mState;
		//get previus state
		if((rememberLayout) /*&& (!self.autoPaging)*/) {
			mState = self.getState();
		} else {
			mState = null;
		}
		
		var oldFilterDivValues = [];
			
		if(mState != null) {
			if((mState.version != undefined) && (mState.version === self.rememberLayoutStateVersion)) { //check version
				if ((mState.query == self.query) && (self.conditions.length == mState.conditions.length)  
						//&& ((!self.autoPaging) || (mState.filterIndexes.length == 0)) 
				   ){
					//load blacklist
					for(var ci = 0; ci <= mState.conditions.length - 1; ci++) {
						if ((mState.conditions[ci] != false) && (self.conditions[ci] != false)){
							for (var p = 0; p < mState.conditions[ci].blackList.length; p++){
								if ((self.conditions[ci].blackList.indexOf(mState.conditions[ci].blackList[p]) == -1) &&
								(self.GeneralDistinctValues[ci].indexOf(mState.conditions[ci].blackList[p]) != -1)){//self.conditions[ci]
									self.conditions[ci].blackList.append(mState.conditions[ci].blackList[p])
								}
							}
							for (var p = 0; p < mState.conditions[ci].collapsedValues.length; p++){
								if ((self.conditions[ci].collapsedValues.indexOf(mState.conditions[ci].collapsedValues[p]) == -1) &&
								(self.GeneralDistinctValues[ci].indexOf(mState.conditions[ci].collapsedValues[p]) != -1)){
									self.conditions[ci].collapsedValues.append(mState.conditions[ci].collapsedValues[p])
								}
							}
							try {
							self.conditions[ci].sort = mState.conditions[ci].sort;
							self.sort(self.conditions[ci], ci);
							self.oldSortValues[ci] = mState.conditions[ci].sort;
							self.conditions[ci].subtotals = mState.conditions[ci].subtotals;
							} catch (Error){}
						}
					}
					
					self.rowConditions = mState.rowConditions;
					self.colConditions = mState.colConditions;
					self.rowsPerPage   = mState.rowsPerPage;
					
					self.filterIndexes = mState.filterIndexes;
					if (mState.filterDivSelects != undefined){
						for(var fiv = 0; fiv < mState.filterDivSelects.length; fiv++) {
							oldFilterDivValues[fiv] = mState.filterDivSelects[fiv];
						}
					}
					
					self.stateChanged=true	
				}
			}
		}
        
        try {
			if ((mState != null) && (mState.version === self.rememberLayoutStateVersion) && (mState.filterIndexes.length > 0)) {
				var existElm = false;
				for(var i = 0; i < self.conditions.length; i++) {//save the black list create from older state
					var tmp = []
					if((self.conditions[i]) && (self.conditions[i].blackList)) {
						for(var j = 0; j < self.conditions[i].blackList.length; j++) {
							tmp.append(self.conditions[i].blackList[j])
							existElm = true;
						}
					}
					self.tempBlackLists.append([tmp])
					var tmp2 = []
					if((self.conditions[i]) && (self.conditions[i].collapsedValues)){
						for(var j = 0; j < self.conditions[i].collapsedValues.length; j++) {
							tmp2.append(self.conditions[i].collapsedValues[j])
							existElm = true;
						}
					}
					self.tempCollapsedValues.append([tmp2])
				}
				if(!existElm) {
					self.tempBlackLists = false
				}
			} else {
				self.tempBlackLists = false
			}
		} catch (error) {
			self.tempBlackLists = false
		}


        self.gd.clearSources();
        self.gd.clearTargets();
        //draw filter bar
        
        
        if ((self.autoPaging) || (self.rowConditions.length <= 16) || (self.GeneralDataRows.length <= 1200)){
        	self.drawFilters();
        }
        
        //first draw filter then write old valueF
        if (oldFilterDivValues.length > 0){
        	for(var fiv = 0; fiv < oldFilterDivValues.length; fiv++) {
				self.filterDiv.selects[fiv].value = oldFilterDivValues[fiv];
			}
			self.preGoWhenFilterByTopFilter(true)
		}
		
        try {
        	if (self.filterIndexes.length == 0){
        		self.applyFilters();
        		self.createAggStructure();
        		self.fillAggStructure();
        	
        		self.checkAggStructure();
        	
        		self.count();
        	}
        } catch (Error){
        	//alert(Error)
        }
        
       
        
        if (GlobalPivotInterval[UcId]!=undefined){
        	clearInterval( GlobalPivotInterval[UcId] );
        }
        var previousValuePivotWidth = 0
        var antepreviusValuePivotWidth = 0
        GlobalPivotInterval[UcId] = setInterval(function(){
        		if ((self.autoPaging) /*&& (!self.FilterByTopFilter)*/){
        			
        			
        			 if ((jQuery("#"+self.containerName+" #tablePagination_currPage")[0] != undefined) ||
        			 (jQuery("#"+ self.controlName + "_" + self.query + "_tablePagination " + "#tablePagination_currPage")[0] != undefined)){
        			 	var actualPage; 
        			 	if  (jQuery("#"+self.containerName+" #tablePagination_currPage")[0] != undefined) {
        					actualPage = parseInt(jQuery("#"+self.containerName+" #tablePagination_currPage")[0].value);
        				} else {
        					actualPage = parseInt(jQuery("#"+ self.controlName + "_" + self.query + "_tablePagination " + "#tablePagination_currPage")[0].value)        				}
        				var t=0;
        				if (self.actualPaginationPage != actualPage){ //change of page
        					var gonext = (self.actualPaginationPage < actualPage)
        					var lastPage = (actualPage == parseInt(jQuery("#"+self.containerName+" #tablePagination_totalPages")[0].textContent)) 
        					self.actualPaginationPage = actualPage
        					self.changePaginationRows(actualPage, gonext, lastPage);
        					var _pagetot;
        					if  (jQuery("#"+self.containerName+" #tablePagination_totalPages")[0] != undefined) {
        						_pagetot = parseInt(jQuery("#"+self.containerName+" #tablePagination_totalPages")[0].value);
        					} else {
        						_pagetot = parseInt(jQuery("#"+ self.controlName + "_" + self.query + "_tablePagination "+" #tablePagination_totalPages")[0].value);
        					}
        					if (!isNaN(_pagetot)) self.TotalPagesPaging = _pagetot  
        					self.go(false);
        				}	
        				//if ((!gx.util.browser.isFirefox()) || ((document.activeElement.type != undefined) && (document.activeElement != jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage")[0])) ){
        					actual_rowsPerPage = parseInt( jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage")[0].value );
        					if (self.autoPagingRowsPerPage != actual_rowsPerPage){
        						self.autoPagingRowsPerPage = actual_rowsPerPage
        						//self.changePaginationPagesSize(actualPage);
        						self.actualPaginationPage = 1
        						setTimeout( function(){ createPaginationInfo(self, self.RowsWhenMoveToFilter); 
        												self.changePaginationRows(1);
        												self.go(false);},
        						 500 )	
        					}
        			 }
        		} 
        		var actual_rowsPerPage = 0;
        		if (jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage").length > 0){
        			actual_rowsPerPage = parseInt( jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage")[0].value );
        			if( !isNaN(actual_rowsPerPage) ){ 
        				if (self.rowsPerPage != actual_rowsPerPage){
        					self.rowsPerPage = actual_rowsPerPage;
        					self.stateChanged=true;
        					/* must save state change*/
        					
        					var filterDivSelects =  new Array();
							for (var fiv = 0; fiv < self.filterDiv.selects.length ; fiv++){
								filterDivSelects[fiv] = self.filterDiv.selects[fiv].value;
							}
        					
        					var state = {
                				query: self.query,
                				conditions: self.conditions,
                				colConditions: self.colConditions,
                				rowConditions: self.rowConditions,
                				filterIndexes: self.filterIndexes,
                				filterDivSelects: filterDivSelects,
                				rowsPerPage: self.rowsPerPage,
          						version: self.rememberLayoutStateVersion
            				};
            				
            				self.saveState(state); 
            				
        				} else {
        					self.rowsPerPage = actual_rowsPerPage;
        				}
        				
        				if (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table"){
        					if ((jQuery("#" + self.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
								jQuery("#" + self.controlName + "_" + self.query).css({width: "400px"});
							}
							var wd = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 4;
							try{ 
    							if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    								wd = wd + 4;
    							}
    						} catch (Error) {
    						}
							var wd2 = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 1;
							try {
								var borderWidth = jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css("border-right-width");
								if ((borderWidth != undefined) && (borderWidth[0]!='0')){
									wd2 = wd2 - 1;
								}
							} catch (ERROR) {}
							
							var actualWidth = jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].clientWidth
							//if ( (antepreviusValuePivotWidth == 0) || (antepreviusValuePivotWidth == previousValuePivotWidth) ||
 							//		( (previousValuePivotWidth > wd+4) || (previousValuePivotWidth < wd-4) 
 							//				|| (antepreviusValuePivotWidth > previousValuePivotWidth+4) || (antepreviusValuePivotWidth < previousValuePivotWidth-4)) ){
							if ((actualWidth > wd+1) || (actualWidth < wd-1)){	
								jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" });
								if ( (gx.util.browser.isIE()) ){
									jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px", marginBottom: "0px"});
								} else {
									jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px", marginBottom: "5px"});
								}
								antepreviusValuePivotWidth = previousValuePivotWidth
								previousValuePivotWidth = wd 
							}
							if ((jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + this.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
         					   	if ( (gx.util.browser.isIE()) ){
								   	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
								} else {
            					   	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            					}
        					}
        				}
        				if ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
        					jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({height: "65px"})
        				}
        			} 
        		} else {
        			if (jQuery("#" + self.controlName + "_" + self.query)[0]!= undefined){
        				if (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table"){
        					if ((jQuery("#" + self.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
								jQuery("#" + self.controlName + "_" + self.query).css({width: "400px"});
							}
							var wd = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 4;
							try{ 
    							if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    								wd = wd + 4;
    							}
    						} catch (Error) {
    						}
							if ( (antepreviusValuePivotWidth == 0) || (antepreviusValuePivotWidth == previousValuePivotWidth) ||
 									( (previousValuePivotWidth > wd+6) || (previousValuePivotWidth < wd-6) 
 										|| (antepreviusValuePivotWidth > previousValuePivotWidth+6) || (antepreviusValuePivotWidth < previousValuePivotWidth-6)) ){
								jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" });
								antepreviusValuePivotWidth = previousValuePivotWidth
								previousValuePivotWidth = wd 
							} 
						}
					}
        		}
        	} , 250);
        } else { //there's no data
        	
        	OAT.Dom.clear(self.div);
        	self.div.setAttribute("class", "conteiner_table_div");
        	//draw filter bar
        	//myTable.id = self.controlName + "_" + self.query + "_toolbar";
        	self.drawFilters();
        	
        	var table = OAT.Dom.create("table", {}, "pivot_table");
        	table.id = this.controlName + "_" + this.query; //add control name and query name as the id of main table
        	var tbody = OAT.Dom.create("tbody");
        	
        	OAT.Dom.append([table, tbody], [self.div, table]);
        	
        	if(self.colConditions.length > 0) {
			
				var i = 0;
				var tr = OAT.Dom.create("tr");
				self._drawRowConditionsHeadingsCustom(tr);
				
				for(var ni = 0; ni < self.colConditions.length; ni++) {
					tr = self._drawColConditionsHeadingsCustom(tr, ni, (ni === self.colConditions.length - 1));
				}
				
				self.appendRowToTable(tbody, tr, true);
				firstRow = tr;
				
				
				if(self.colConditions.length > 0) {
					var tr = OAT.Dom.create("tr");
					
						for(var m = 0; m < measures.length; m++) {
							var th = OAT.Dom.create("th", {}, "h2titlewhite");
							th.colSpan = 1;
							th.innerHTML = measures[m].attributes.getNamedItem("displayName").nodeValue;
							self.setTitleTexrtAlign(th, th.innerHTML);
							tr.appendChild(th);
						}
					
					//tr.setAttribute("title_row", true);
					self.appendRowToTable(tbody, tr, true);//tbody.appendChild(tr);
				}
				
				 var _mtotalSpan = measures.length - self.colConditions.length
				 if(_mtotalSpan > 0) {
					var th = OAT.Dom.create("th", {}, "h2subtitle ");
					th.innerHTML = "";
					th.colSpan = _mtotalSpan;
					th.style.borderLeftColor = "transparent";
					firstRow.appendChild(th);
				 }
				
			} else {
			
			var tr = OAT.Dom.create("tr");
			
			for (var i=0; i < self.rowConditions.length - (measures.length -1); i++){
				var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2title");
        		var div = OAT.Dom.create("div");
        		div.innerHTML = self.headerRow[self.rowConditions[i]]  + "&nbsp&nbsp&nbsp";
       	 		th.appendChild(div);
        		tr.appendChild(th);
			}
			
			if (self.colConditions.length > 0) {
				for (var i=0; i < self.colConditions.length; i++){
					var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2title");
        			var div = OAT.Dom.create("div");
        			div.innerHTML = self.headerRow[self.colConditions[i]]  + "&nbsp&nbsp&nbsp";
       	 			th.appendChild(div);
        			tr.appendChild(th);
				}	
			}
			
			for (var j = 0; j < self.rowConditions.length; j++) {
            	var cond = self.conditions[self.rowConditions[j]];
            	if (self.isMeasureByName( self.headerRow[self.rowConditions[j]] )){//(measures[0].attributes.getNamedItem("displayName").nodeValue == self.headerRow[self.rowConditions[j]]) { 
            	    var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2titlewhite"); //custom class for 44px height
            	    var div = OAT.Dom.create("div");
            	    div.innerHTML = self.headerRow[self.rowConditions[j]];
					self.setTitleTexrtAlign(div, self.headerRow[self.rowConditions[j]]);
            	    self.setClickEventHandlers(th, self.headerRow[self.rowConditions[j]], "MEASURE", self.rowConditions[j] - columns.length, 'GrandTotal');
            	    OAT.Dom.append([th, div], [tr, th]);
            	}
            }
			 
			 
			if (!self.colConditions.length) {
				var th = OAT.Dom.create("th");
             	self._drawCorner(th, true);
             	th.conditionIndex = -1;
             	tr.appendChild(th);
        	} else { th.style.border = "none"; }
			//tr.setAttribute("title_row", true);
			tbody.appendChild(tr);
			}
			
			
			if (gx.util.browser.isIE()){
				jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({ boxShadow: '0px 0px 0px' })
			}
        	
        	if (GlobalPivotInterval[UcId]!=undefined){
        		clearInterval( GlobalPivotInterval[UcId] );
        	}
        	var previousValuePivotWidth = 0
        	var antepreviusValuePivotWidth = 0
        	
        	setTimeout( function() {
        		if (jQuery("#" + self.controlName + "_" + self.query)[0]!= undefined){
        				if (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table"){
        					if ((jQuery("#" + self.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
								jQuery("#" + self.controlName + "_" + self.query).css({width: "400px"});
							}
							var wd = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 4;
							try{ 
    							if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    								wd = wd + 4;
    							}
    						} catch (Error) {
    						}
    						jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" });
    					}
				}	
        	}, 250)
        	
        	
        	
        	
        }
    } /* init */
   
   
    this.initWhenServerPagination = function() {
        self.propPage = OAT.Dom.create("div", { });
        jQuery(".oat_winrect_container").remove();//delete prevoius pop-ups
        self.UserFilterValues = []; self.UserExpandValues = [];
        for (var i = 0; i < self.headerRow.length; i++) {
            self.initCondition(i);
            //add info for server paging porpouse
            if (self.conditions[i] && self.columns[i]){
            	self.conditions[i].dataField = self.columns[i].getAttribute("dataField")
            	self.conditions[i].state = "all"
            	self.conditions[i].defaultAction = "Include"
            	self.conditions[i].visibles = []
            	self.conditions[i].searchInfo = { previousPage: 0, totalPages: 0, filteredText: "", values: [] }
			}
            self.applyCustomFilters(i);
        }
    
        //save default view values
        var oldConditions = jQuery.extend(true, [] ,self.conditions);
        
        var defcolCond = new Array() 
        for(var i=0; i < self.initRowConditions.length; i++){
        	defcolCond[i] = self.initRowConditions[i];
        }
        var defcolRow = new Array();
        for(var i=0; i < self.initColConditions.length; i++){
        	defcolRow[i] = self.initColConditions[i];
        }
        var defStIndex = new Array();
        for(var i=0; i < self.initFilterIndexes.length; i++){
        	defStIndex[i] = self.initFilterIndexes[i];
        }
        
        self.initState = { 
                query: self.query,
                conditions: oldConditions,
                colConditions: defcolRow,
                rowConditions: defcolCond,
                filterDivSelects: [],
                filterIndexes: defStIndex,
                stateChanged: false,
                rowsPerPage: pageSize,
                AxisInfo: self.createAxisInfo(""),
                CollapseInfo: [],
                FilterInfo: []
        };
        //end save default view values
        
        for (var i = 0; i < self.headerRow.length; i++) {
        	self.restoreSubtotalsAndSortLayout(i)
        }
        
        //get previuos state
    	var mState;
		if(rememberLayout) {
			mState = self.getState();
		} else {
			mState = null;
		}
		
		var oldFilterDivValues = [];
		var stateLoad = false;	
		if(mState != null) {
			if((mState.version != undefined) && (mState.version === self.rememberLayoutStateVersion)) { 
				if ((mState.query == self.query) && (self.conditions.length == mState.conditions.length)){
					stateLoad = true;
					self.UserFilterValues = []
					for(var ci = 0; ci <= mState.conditions.length - 1; ci++) {
						if ((mState.conditions[ci] != false) && (self.conditions[ci] != false)){
							for (var p = 0; p < mState.conditions[ci].distinctValues.length; p++){
								if ((self.conditions[ci].distinctValues.indexOf(mState.conditions[ci].distinctValues[p]) == -1)){ 
									self.conditions[ci].distinctValues.append(mState.conditions[ci].distinctValues[p])
								}
							}
							for (var p = 0; p < mState.conditions[ci].blackList.length; p++){
								if ((self.conditions[ci].blackList.indexOf(mState.conditions[ci].blackList[p]) == -1)){ 
									self.conditions[ci].blackList.append(mState.conditions[ci].blackList[p])
								}
							}
							if (mState.conditions[ci].visibles){
								for (var p = 0; p < mState.conditions[ci].visibles.length; p++){
									if ((self.conditions[ci].visibles.indexOf(mState.conditions[ci].visibles[p]) == -1)){ 
										self.conditions[ci].visibles.append(mState.conditions[ci].visibles[p])
									}
								}
							}
							for (var p = 0; p < mState.conditions[ci].collapsedValues.length; p++){
								if ((self.conditions[ci].collapsedValues.indexOf(mState.conditions[ci].collapsedValues[p]) == -1)){
									self.conditions[ci].collapsedValues.append(mState.conditions[ci].collapsedValues[p])
								}
							}
							try {
								self.conditions[ci].sort = mState.conditions[ci].sort;
								
								self.oldSortValues[ci] = mState.conditions[ci].sort;
								self.conditions[ci].subtotals     = mState.conditions[ci].subtotals;
								self.conditions[ci].defaultAction = mState.conditions[ci].defaultAction;
								self.conditions[ci].state         = mState.conditions[ci].state;
								self.conditions[ci].topFilterValue= mState.conditions[ci].topFilterValue
								
								self.conditions[ci].totalPages    = mState.conditions[ci].totalPages
								self.conditions[ci].previousPage  = mState.conditions[ci].previousPage
								self.conditions[ci].blocked       = mState.conditions[ci].blocked
								
							} catch (Error){}
						}
					}
					
					self.rowConditions = mState.rowConditions;
					self.colConditions = mState.colConditions;
					self.rowsPerPage   = mState.rowsPerPage;
					
					self.filterIndexes = mState.filterIndexes;
					if (mState.filterDivSelects != undefined){
						for(var fiv = 0; fiv < mState.filterDivSelects.length; fiv++) {
							oldFilterDivValues[fiv] = mState.filterDivSelects[fiv];
						}
					}
					
					self.stateChanged=true	
				}
			}
		}
        
        try {
			if ((mState != null) && (mState.version === self.rememberLayoutStateVersion) && (mState.filterIndexes.length > 0)) {
				var existElm = false;
				for(var i = 0; i < self.conditions.length; i++) {//save the black list create from older state
					var tmp = []
					if((self.conditions[i]) && (self.conditions[i].blackList)) {
						for(var j = 0; j < self.conditions[i].blackList.length; j++) {
							tmp.append(self.conditions[i].blackList[j])
							existElm = true;
						}
					}
					self.tempBlackLists.append([tmp])
					var tmp2 = []
					if((self.conditions[i]) && (self.conditions[i].collapsedValues)){
						for(var j = 0; j < self.conditions[i].collapsedValues.length; j++) {
							tmp2.append(self.conditions[i].collapsedValues[j])
							existElm = true;
						}
					}
					self.tempCollapsedValues.append([tmp2])
				}
				if(!existElm) {
					self.tempBlackLists = false
				}
			} else {
				self.tempBlackLists = false
			}
		} catch (error) {
			self.tempBlackLists = false
		}


        self.gd.clearSources();
        self.gd.clearTargets();
       
       
		
        if (GlobalPivotInterval[UcId]!=undefined){
        	clearInterval( GlobalPivotInterval[UcId] );
        }
        var previousValuePivotWidth = 0
        var antepreviusValuePivotWidth = 0
        GlobalPivotInterval[UcId] = setInterval(function(){
        		var actual_rowsPerPage = 0;
        		if (jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage").length > 0){
        			actual_rowsPerPage = parseInt( jQuery("#" + self.controlName + "_" + self.query + "tablePagination_rowsPerPage")[0].value );
        			if( !isNaN(actual_rowsPerPage) ){ 
        				if (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table"){
        					if ((jQuery("#" + self.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
								jQuery("#" + self.controlName + "_" + self.query).css({width: "400px"});
							}
							var wd = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 4;
							try{ 
    							if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    								wd = wd + 4;
    							}
    						} catch (Error) {
    						}
							var wd2 = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 1;
							try {
								var borderWidth = jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css("border-right-width");
								if ((borderWidth != undefined) && (borderWidth[0]!='0')){
									wd2 = wd2 - 1;
								}
							} catch (ERROR) {}
							
							var actualWidth = jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].clientWidth
							if ((actualWidth > wd+1) || (actualWidth < wd-1)){	
								jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" });
								if ( (gx.util.browser.isIE()) ){
									jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px", marginBottom: "0px"});
								} else {
									jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px", marginBottom: "5px"});
								}
								antepreviusValuePivotWidth = previousValuePivotWidth
								previousValuePivotWidth = wd 
							}
							if ((jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + this.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
         					   	if ( (gx.util.browser.isIE()) ){
								   	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
								} else {
            					   	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            					}
        					}
        				}
        				if ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
        					jQuery("#" + self.controlName + "_" + self.query + "_tablePagination").css({height: "65px"})
        				}
        			} 
        		} else {
        			if (jQuery("#" + self.controlName + "_" + self.query)[0]!= undefined){
        				if (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table"){
        					if ((jQuery("#" + self.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
								jQuery("#" + self.controlName + "_" + self.query).css({width: "400px"});
							}
							var wd = jQuery("#" + self.controlName + "_" + self.query)[0].offsetWidth - 4;
							try{ 
    							if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    								wd = wd + 4;
    							}
    						} catch (Error) {
    						}
							if ( (antepreviusValuePivotWidth == 0) || (antepreviusValuePivotWidth == previousValuePivotWidth) ||
 									( (previousValuePivotWidth > wd+6) || (previousValuePivotWidth < wd-6) 
 										|| (antepreviusValuePivotWidth > previousValuePivotWidth+6) || (antepreviusValuePivotWidth < previousValuePivotWidth-6)) ){
								jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" });
								antepreviusValuePivotWidth = previousValuePivotWidth
								previousValuePivotWidth = wd 
							} 
						}
					}
        		}
        	} , 20);
       
       
      
      	
       	if ((mState != null) && (mState.AxisInfo != null)) {
       		self.pageData.AxisInfo = mState.AxisInfo;
       	} else {
       		self.pageData.AxisInfo = self.createAxisInfo("");
       	}
       	
       	if ((mState != null) && (mState.FilterInfo != null)) {
			self.pageData.FilterInfo = mState.FilterInfo;
		} 
		
		if ((self.UserFilterValues.length == 0) && (self.UserExpandValues.length == 0)){
		
			self.pageData.CollapseInfo = self.CreateExpandCollapseInfo("");
       	
       		if (self.QueryViewerCollection[self.IdForQueryViewerCollection].UseRecordsetCache && self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetChanged())
			{
       			self.QueryViewerCollection[self.IdForQueryViewerCollection].getRecordsetCacheKey((function (recordsetCacheKey) {	
       			
       				self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheOldKey = (self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey ? self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey : "");
					self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey = recordsetCacheKey; 
					
       				self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
       					if (!self.QueryViewerCollection[self.IdForQueryViewerCollection].anyError(resXML) || self.QueryViewerCollection[self.IdForQueryViewerCollection].debugServices) {
        			  		
        			  		self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
        			    	self.preGoWhenServerPagination(false);
        			    	if (!stateLoad){
        			    		self.initValueRead(self, 0);
        			    	}
        			    	
        			    } else {
        			   		var errMsg = self.QueryViewerCollection[self.IdForQueryViewerCollection].getErrorFromText(resXML);
                        	self.QueryViewerCollection[self.IdForQueryViewerCollection].renderError(errMsg);
        			   	}
        			}).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
        			
        		}).closure(this));
      		} else {
      		
      			self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
      				if (!self.QueryViewerCollection[self.IdForQueryViewerCollection].anyError(resXML) || self.QueryViewerCollection[self.IdForQueryViewerCollection].debugServices) {
        			  	
        			  	self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
        			    self.preGoWhenServerPagination(false);
        			    if (!stateLoad){
        			    	self.initValueRead(self, 0);
        			    }
        			    
        			 } else {
        			   		var errMsg = self.QueryViewerCollection[self.IdForQueryViewerCollection].getErrorFromText(resXML);
                        	self.QueryViewerCollection[self.IdForQueryViewerCollection].renderError(errMsg);
        			 }   
        		}).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
        	
      		}
      	
      	
      	} else {
      		//when user customize filters or expand-collapsed
      		if (self.QueryViewerCollection[self.IdForQueryViewerCollection].UseRecordsetCache && self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetChanged())
			{
				self.QueryViewerCollection[self.IdForQueryViewerCollection].getRecordsetCacheKey((function (recordsetCacheKey) {	
       			
       				self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheOldKey = (self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey ? self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey : "");
					self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey = recordsetCacheKey; 
      				
      				self.initValueRead(self, 0, true);
      				
      			}).closure(this));	
      		} else {
      			self.initValueRead(self, 0, true);
      		}
      		
      	}
      	
      	//set interval for handler values infinite scroll
	    setInterval(function(){ 
	    				for(var t = 0; t < jQuery(".oat_winrect_container .pivot_popup_fix").length; t++){
	    					if ((!jQuery(".oat_winrect_container .pivot_popup_fix").closest(".oat_winrect_container")[t].style.display) ||
	    					(jQuery(".oat_winrect_container .pivot_popup_fix").closest(".oat_winrect_container")[t].style.display != "none")){
	    					 
	    						if (jQuery(".oat_winrect_container .pivot_popup_fix").length > 0){
	    							var element = jQuery(".oat_winrect_container .pivot_popup_fix")[t];
	    							var scrollBottom = element.scrollHeight - element.clientHeight - element.scrollTop
	    							if (scrollBottom < 25){
	    								var UcId = element.getAttribute("ucid")
	    								var columnNumber = parseInt(element.getAttribute("columnNumber"))
	    								if (UcId == self.UcId){
	    									self.readScrollValue(columnNumber)
	    								}
	    							}
	    						}
	    					}
	    			  	}
	    		},
	    	250)
	    
      	
    } /* initWhenServerPagination */

	this.initValueRead = function(self, columnNumber, allData){
		if (columnNumber >= self.columns.length){
			
			//add items to page select if exists
			try {
	    		for (var iP = 0; iP < jQuery("#" + self.UcId + "_" + self.query + "_pivot_page").find("select").length; iP++){
	    			var s = jQuery("#" + self.UcId + "_" + self.query + "_pivot_page").find("select")[iP];
	    			var filterDim = parseInt(s.getAttribute("id").replace("page_select_",""))
	    			var index = self.filterIndexes[filterDim]	
	    			
	    			if (self.conditions[index].filteredShowValues == undefined){
	    				self.conditions[index].filteredShowValues = []
	    			}
            		var actualValues = self.conditions[index].distinctValues;
        			for (var j = 0; j <  actualValues.length; j++) {
        				var v =  actualValues[j];
        				if (self.conditions[index].filteredShowValues.indexOf(v) == -1){
        					self.conditions[index].filteredShowValues.push(v);
            				if (v != "#NuN#"){
            					OAT.Dom.option(v, v, s);
            				} else {
            					OAT.Dom.option(" ", v, s);
            				}
            			}
            		}
            		if ((self.conditions[index].topFilterValue!="[all]") /*&& (self.conditions[index].topFilterValue!="")*/){
            			var v = self.conditions[index].topFilterValue
            			if (self.conditions[index].filteredShowValues.indexOf(v) != -1){ //value already load
            				s.selectedIndex = self.conditions[index].filteredShowValues.indexOf(v)+1
            			} else {
            				self.conditions[index].filteredShowValues.push(v);
            				if (v != "#NuN#"){
            					OAT.Dom.option(v, v, s);
            				} else {
            					OAT.Dom.option(" ", v, s);
            				}
            				s.selectedIndex = 1
            			}
            		}
	    			
	    			
	    		}
	    	} catch (ERROR) {
	    	
	    	}
	    	
	    	
	    	
			if (allData){
				self.callServiceWhenCustomeValues();
			}
    		return;
    	} else {
    		var cantItems = 10;
    		if ((self.QueryViewerCollection[self.IdForQueryViewerCollection].AutoRefreshGroup != "") || (allData)){
    			cantItems = 0;
    		}
    		self.lastRequestValue = columnNumber;
			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    			var data = JSON.parse(validStr);
	    			
	    			self.conditions[columnNumber].previousPage = data.PageNumber
    				self.conditions[columnNumber].totalPages   = data.PagesCount
	    			self.conditions[columnNumber].blocked      = false
	    			//null value?
    				if (data.Null){
    					self.conditions[columnNumber].hasNull = true;
    					if (self.conditions[columnNumber].distinctValues.indexOf("#NuN#") == -1){
    						self.conditions[columnNumber].distinctValues.push("#NuN#")
    					}
    					var nullIncluded = true;
    					
    					if (!self.conditions[columnNumber].NullIncluded){
    						nullIncluded = false;
    					}
    					if ((nullIncluded) && (self.conditions[columnNumber].visibles.indexOf("#NuN#") == -1)){
    						self.conditions[columnNumber].visibles.push("#NuN#");
    					}
    				} else {
    					self.conditions[columnNumber].hasNull = false;
    				}
	    			
	    			var includeLists = [];
	    			for (var i = 0; i < data.NotNullValues.length; i++){
	    				var value = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
	    				var include = false;
	    				if ((self.conditions[columnNumber].state == "none") && 
	    					(self.UserFilterValues.length > 0) && (self.UserFilterValues[columnNumber] != undefined) 
	    					&& (self.UserFilterValues[columnNumber].length > 0)	&& (self.UserFilterValues[columnNumber].indexOf(value.trim()) != -1))
	    				{
	    					include = true;
	    					includeLists.push(value)
	    				}
		    				
	    				if (self.conditions[columnNumber].distinctValues.indexOf(value) == -1){
	    					self.conditions[columnNumber].distinctValues.push(value)
	    				}
	    				if ( (self.conditions[columnNumber].state == "all") 
	    						&& (self.conditions[columnNumber].visibles.indexOf(value) == -1)){
	    					self.conditions[columnNumber].visibles.push(value)
	    				}
	    				if ( (self.conditions[columnNumber].state == "none") 
	    						&& (self.conditions[columnNumber].blackList.indexOf(value) == -1)
	    						&& (!include)){
	    					self.conditions[columnNumber].blackList.push(value)
	    				}
	    				
	    				if ((allData) && (self.UserExpandValues.length > 0)){//collapsed values
	    					if (self.UserExpandValues[columnNumber] != undefined){
	    						if ((self.UserExpandValues[columnNumber][0] == "#ALLCOLLAPSE#") ||
	    						 	(self.UserExpandValues[columnNumber].indexOf(value.trim()) == -1))
	    						{
    								self.conditions[columnNumber].collapsedValues.push(value);
    							}
    						}
	    				}
	    				
	    			}
	    			
	    			for (var i = 0; i < includeLists.length; i++){
	    				self.createFilterInfo({ op: "pop", values: includeLists[i], dim: columnNumber}, true);
	    			}
	    			
	    			columnNumber++;
	    			self.initValueRead(self, columnNumber, allData)
		   	}).closure(this), [self.columns[columnNumber].getAttribute("dataField") , 1, cantItems, ""]);
	    }
	}
	
	this.callServiceWhenCustomeValues = function(){
		self.pageData.CollapseInfo = self.CreateExpandCollapseInfo("");
       	
      /* 	if (self.QueryViewerCollection[self.IdForQueryViewerCollection].UseRecordsetCache && self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetChanged())
		{
       		self.QueryViewerCollection[self.IdForQueryViewerCollection].getRecordsetCacheKey((function (recordsetCacheKey) {	
       			
       			self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheOldKey = (self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey ? self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey : "");
				self.QueryViewerCollection[self.IdForQueryViewerCollection].RecordsetCacheActualKey = recordsetCacheKey; 
					
       			self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
        		  	self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
        		    self.preGoWhenServerPagination(false);
        		}).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
        		
        	}).closure(this));
      	} else {
      	*/	
      		self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
      			if (!self.QueryViewerCollection[self.IdForQueryViewerCollection].anyError(resXML) || self.QueryViewerCollection[self.IdForQueryViewerCollection].debugServices) {
      				
        		  	self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
        		    self.preGoWhenServerPagination(false);
        		    
        		 } else {
        			var errMsg = self.QueryViewerCollection[self.IdForQueryViewerCollection].getErrorFromText(resXML);
                   	self.QueryViewerCollection[self.IdForQueryViewerCollection].renderError(errMsg);
        		 }      
        	}).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
        	
      	//}
	} 
	
	
	
    /* callback routines */
    this.getOrderReference = function(conditionIndex, anchorRef, functionRef, divRef) { // ** move a row or column to filter bar ---
        return function(target, x, y) {
            /* somehow reorder conditions */
            
            if ((self.conditions[conditionIndex].validPosition != undefined) && (self.conditions[conditionIndex].validPosition.indexOf("filters") == -1)){
            	return;
            }
            self.lightOff();
           
            /* filters */
            if (target == self.filterDiv) {
            	self.stateChanged=true
                self.filterIndexes.push(conditionIndex);
                self.conditions[conditionIndex].blackList = [];
                for (var i = 0; i < self.rowConditions.length; i++) {
                    if (self.rowConditions[i] == conditionIndex) { self.rowConditions.splice(i, 1); }
                }
                for (var i = 0; i < self.colConditions.length; i++) {
                    if (self.colConditions[i] == conditionIndex) { self.colConditions.splice(i, 1); }
                }
                
                if (!self.serverPagination){
                	if (!self.autoPaging){
                		self.preGoWhenMoveTopFilter(conditionIndex);
                	} else {
                		self.preGoWhenFilterByTopFilter(false)
                	}
                }
                self.onDragundDropEventHandle(conditionIndex, "filters") //call event
                
                self.DiseablePivot();
				setTimeout( function(){
					if (!self.serverPagination){
	               		self.go(false);
	               	} else {
	               		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[conditionIndex].dataField, {op:"all", value:"", dim:conditionIndex}, "", "")
	               	}
    	            self.EneablePivot();
				},200)
				return;
            }

            var sourceCI = conditionIndex; /* global index */
            var targetCI = target.conditionIndex; /* global index */
            if (sourceCI == targetCI) {
            	if (gx.util.browser.webkit){
            		if (anchorRef != undefined) {
            			anchorRef.displayRef2([x,y])
            			functionRef([x,y, divRef])
            		} 
            	}
            	return; 
            } /* dragged onto the same */
            var sourceType = false; var sourceI = -1; /* local */
            var targetType = false; var targetI = -1; /* local */
            for (var i = 0; i < self.rowConditions.length; i++) {
                if (self.rowConditions[i] == sourceCI) { sourceType = self.rowConditions; sourceI = i; }
                if (self.rowConditions[i] == targetCI) { targetType = self.rowConditions; targetI = i; }
            }
            for (var i = 0; i < self.colConditions.length; i++) {
                if (self.colConditions[i] == sourceCI) { sourceType = self.colConditions; sourceI = i; }
                if (self.colConditions[i] == targetCI) { targetType = self.colConditions; targetI = i; }
            }
            if (targetCI == -1) {
                /* no cols - lets create one */
                self.colConditions.push(sourceCI);
                self.rowConditions.splice(sourceI, 1);
                if (!autoPaging){
					self.preGoWhenMoveTopFilter(conditionIndex);
				} else {
					self.preGoWhenFilterByTopFilter(false)
				}
                self.go(self);
                return;
            }
            if (targetCI == -2) {
                /* no rows - lets create one */
                self.rowConditions.push(sourceCI);
                self.colConditions.splice(sourceI, 1);
                if (!autoPaging){
                	self.preGoWhenMoveTopFilter(conditionIndex);
                } else {
                	self.preGoWhenFilterByTopFilter(false)
                }
                self.go(self);
                return;
            }
            if (sourceType == targetType) {
                /* same condition type */
                if (sourceI + 1 == targetI) {
                    /* dragged on condition immediately after */
                    targetType.splice(targetI + 1, 0, sourceCI);
                    targetType.splice(sourceI, 1);
                } else {
                    targetType.splice(sourceI, 1);
                    targetType.splice(targetI, 0, sourceCI);
                }
            } else {
                /* different condition type */
                sourceType.splice(sourceI, 1);
                targetType.splice(targetI, 0, sourceCI);
            }
            if (!self.serverPagination){
            	self.go(false);
            } else {
            	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[conditionIndex].dataField, "", "", "")
            }
        }
    }

    this.getClickReference = function(cond, dimensionNumber, contentDiv) {
    	var anchorRef = OAT.Anchor.assign(contentDiv, { title: " ",
                content: self.propPage,
                result_control: false,
                activation: "click",
                type: OAT.WinData.TYPE_RECT,
                width: "auto"
        });
        
        jQuery(contentDiv).data('anchorRef', anchorRef );
        
        var refresh = function() {
            self.propPage._Instant_hide();
            self.go(false);
        }
        return [function(event) {
        	
        	self.ShowPopUp(cond, dimensionNumber, event);
          
        }, anchorRef]
    }
    
    this.ShowPopUp = function(cond, dimensionNumber, event) {
    		
    		var refresh = function() {
            	jQuery(".oat_winrect_container").css({display: "none"});
        	}
        	var eventDiv;
        	var coords;
        	if (event.currentTarget != undefined){
        		eventDiv = event.currentTarget;
        		coords = OAT.Dom.eventPos(event);
        	} else {
        		eventDiv = event[2]
        		coords = [event[0], event[1]]
        	}
        	var toAppend = [];
    		
            self.propPage.style.left = coords[0] + "px";
            self.propPage.style.top = coords[1] + "px";
            self.propPage.setAttribute('id', 'pop-up');
            self.propPage.setAttribute('class', 'oatpop-up');
            OAT.Dom.clear(self.propPage);
            toAppend.append(self.propPage);
            /* contents */
            var close = OAT.Dom.create("div", { margin: "0", position: "absolute", cursor: "pointer" }); //{ position: "absolute", top: "3px", right: "3px", cursor: "pointer" }
            close.setAttribute("class", "oat_winrect_close_b");
            close.innerHTML = "<span></span>";
            OAT.Dom.attach(close, "click", refresh);
			
			
			if (!disableColumnSort){
				var div_order = document.createElement("div");
				div_order.setAttribute("class", "first_popup_subdiv");
			
    	        var asc = OAT.Dom.radio("order");
        	    asc.id = "pivot_order_asc";
        	    OAT.Dom.attach(asc, "change", function() {
        	    	 	if (self.serverPagination){
        	    	 		cond.sort = 1;self.stateChanged=true; 
        	    	 		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, cond.dataField, "", "", "")
        	    	 	} else {
        	    			if ((cond.sort == 0)||(cond.sort == 2)) cond.sort = 0 
        	    			else cond.sort = 1; 
        	    			self.stateChanged=true; self.sort(cond, dimensionNumber); 
        	    		
        	    			self.DiseablePivot();
        	    			setTimeout( function(){
        	    					self.go(false); 
									self.EneablePivot();
								},200)
        	    		
        	    		}
        	    	});
        	    div_order.appendChild(asc);
            
        	    var alabel = OAT.Dom.create("label");
        	    alabel.htmlFor = "pivot_order_asc";
        	    alabel.innerHTML = gx.getMessage("GXPL_QViewerJSAscending");
        	    div_order.appendChild(alabel);
        	    div_order.appendChild(OAT.Dom.create("br"));
            
        	    var desc = OAT.Dom.radio("order");
        	    desc.id = "pivot_order_desc";
        	    OAT.Dom.attach(desc, "change", function() {
        	    	if (self.serverPagination){
        	    	 		cond.sort = -1;self.stateChanged=true; 
        	    	 		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, cond.dataField, "", "", "")
        	    	} else { 
        	    		if ((cond.sort == 0)||(cond.sort == 2)) cond.sort = 2;
        	    		else cond.sort = -1; 
        	    		self.stateChanged=true; self.sort(cond, dimensionNumber);
        	    		if (!self.serverPagination){ 
       		     			self.updateSortImage(eventDiv.firstChild.children[1],2); 
       		     		}
       		     		self.DiseablePivot();
        	    		setTimeout( function(){
       		     			self.go(false); 
       		     			self.EneablePivot();
						},200)
					}
       		     			});
       		     div_order.appendChild(desc);
            
        	    var dlabel = OAT.Dom.create("label");
            	dlabel.htmlFor = "pivot_order_desc";
            	dlabel.innerHTML = gx.getMessage("GXPL_QViewerJSDescending");
            	div_order.appendChild(dlabel);
			
				toAppend.append(div_order);
			
            	var hr1 = OAT.Dom.create("hr", { });
            	toAppend.append(hr1);
            }
            
            var hr2 = OAT.Dom.create("hr", { });
            var hr3 = OAT.Dom.create("hr", { });
            var hr4 = OAT.Dom.create("hr", { });
            var hr5 = OAT.Dom.create("hr", { });
            var hr6 = OAT.Dom.create("hr", { });

			if (((cond.hideSubtotalOption === undefined) || (!cond.hideSubtotalOption))
							&& ((!self.autoPaging) || (self.colConditions.length == 0))
				){
            	var subtotals = OAT.Dom.create("div");
            	if (disableColumnSort){
            		subtotals.setAttribute("class", "first_popup_subdiv");
            	}
            	var subtotal_sel_div = document.createElement("div");
            	var class_check_div = (cond.subtotals)? "check_item_img": "uncheck_item_img";
            	subtotal_sel_div.setAttribute("class", class_check_div);
            	OAT.Dom.attach(subtotal_sel_div, "click", function() { 
            													        cond.subtotals = !(this.getAttribute("class") === "check_item_img");
            													   		var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            													   		this.setAttribute("class", newClass);
            													   		self.stateChanged=true;
            													   		if (self.autoPaging){
            													   			self.DiseablePivot();
            													   			setTimeout( function(){ 
            													   						createPaginationInfo(self, self.RowsWhenMoveToFilter); self.go(false); 
            													   						self.EneablePivot();
            													   					}
            													   					, 500 )
            													   		} else if (!self.serverPagination){
            													   			self.DiseablePivot();
            													   			setTimeout( function(){ 
            													  				self.go(false);
            													  				self.EneablePivot();
            													  			}, 500)
            													  		} else {
            													  			self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, cond.dataField, "", "", "")
            													  		}
            													});
            
            	var sl = OAT.Dom.create("label");
            	sl.innerHTML = gx.getMessage("GXPL_QViewerJSSubtotals");
            	sl.htmlFor = "pivot_checkbox_subtotals";
            	subtotal_sel_div.appendChild(sl);
            	OAT.Dom.append([subtotals, subtotal_sel_div]);
            	toAppend.append(subtotals);
           	}
            
            //toAppend.append(hr2);            
            if(self.stateChanged){
            	var restoreview = OAT.Dom.create("div");
            	var restoreview_sel_div = document.createElement("div");
            	restoreview_sel_div.setAttribute("class", "uncheck_item_img");
            	
            	OAT.Dom.attach(restoreview_sel_div, "click", function() { if ((!self.autoPaging) && (!self.serverPagination)) {
            													self.cleanState();
            													for(var c = 0; c < self.conditions.length; c++){
            														self.sort(self.conditions[c], c)
            													} 
            													cond.subtotals = true; 
            													
            													self.DiseablePivot();
            													setTimeout( function(){
																		self.EneablePivot();
	            														self.go(false); 	
            															refresh(); self.stateChanged = false;
            															
            															self.EneablePivot();
            														},200)
            												} else if (!self.serverPagination) {
            													self.cleanState();
            													for(var c = 0; c < self.conditions.length; c++){
            														self.sort(self.conditions[c], c)
            													}
            													if (jQuery("#"+self.containerName+" #tablePagination_currPage")[0] != undefined){
            											   			jQuery("#"+self.containerName+" #tablePagination_currPage")[0].value = 1;
            											   		}
                												self.actualPaginationPage = 1;
                												self.DiseablePivot();
            													setTimeout( function(){
                													self.preGo(false, null, null, -1);	
                													refresh(); self.stateChanged = false; 
                													
                													self.EneablePivot();
            													},200)	
            												} else {
            													self.cleanStateWhenServerPagination();
            													self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, cond.dataField, "", "", "")
            													refresh(); self.stateChanged = false; 
            												} 
            											  }); 
            		 
            	 
            	var rl = OAT.Dom.create("label");
            	rl.innerHTML = gx.getMessage("GXPL_QViewerJSRestoreDefaultView") //"Restore View";
            	rl.htmlFor = "pivot_checkbox_restoreview";
            	restoreview_sel_div.appendChild(rl);
            	OAT.Dom.append([restoreview, restoreview_sel_div]);
            	toAppend.append(restoreview);
            	//toAppend.append(hr3);  
            }

			/* for pivoting purpuses*/
			if (measures.length > 0){
           		var pivotpurp = OAT.Dom.create("div");
           		var pivotpurp_sel_div = document.createElement("div");
            	var class_check_div_pp = "uncheck_item_img";
            	pivotpurp_sel_div.setAttribute("class", class_check_div_pp);
            	
            	OAT.Dom.attach(pivotpurp_sel_div, "click", function() { if ((!self.autoPaging) && (!self.serverPagination)) { 
            												self.stateChanged=true; self.changedFromColumnToRow(dimensionNumber); 
            											
            												self.DiseablePivot();
            												setTimeout( function(){
            										   			self.go(false); refresh();
            												self.EneablePivot();
            												},200)
            											} else if (!self.serverPagination){
            										   	 if (self.columns.length < 4){
            										   		self.stateChanged=true; self.changedFromColumnToRow(dimensionNumber); 
            										   		
            										   		self.DiseablePivot();
            												setTimeout( function(){
            										   			self.preGo(false, null, null, -1); 
            										   			self.distinctDivs(self.conditions[dimensionNumber], distinct, dimensionNumber); 
            										   			refresh();
            										   		self.EneablePivot();
            												},200)	
            										     }
            										   } else {
            										   		self.stateChanged=true; 
            										   		self.changedFromColumnToRow(dimensionNumber);
            										   		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, cond.dataField, "", "", "")
            										   		refresh();
            										   }	
            										 });
            	 
            	var pvpl = OAT.Dom.create("label");
            	if (self.rowConditions.find(dimensionNumber) != -1)
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerJSPivotDimensionToColumn"); 
            	else
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerJSPivotDimensionToRow");
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            
            	pivotpurp_sel_div.appendChild(pvpl);
            	OAT.Dom.append([pivotpurp, pivotpurp_sel_div]);
            	toAppend.append(pivotpurp);
            	toAppend.append(hr4);
           	}
            /* end pivoting option*/
           
            /*for Ipad & Iphone move to top filter*/
            if (measures.length > 0){
             var ua = navigator.userAgent.toLowerCase();
			 var isAndroid = ua.indexOf("android") > -1;	
             if (gx.util.browser.isIPad() || gx.util.browser.isIPhone() || isAndroid) {
            	if (self.filterIndexes.find(dimensionNumber) === -1){
					var Ipadpurp = OAT.Dom.create("div");
					var Ipadpurp_sel_div = document.createElement("div");
            		Ipadpurp_sel_div.setAttribute("class", "uncheck_item_img");
            		
            		
            		OAT.Dom.attach(Ipadpurp_sel_div, "click", function() { 
													self.filterIndexes.push(dimensionNumber);
                									self.conditions[dimensionNumber].blackList = [];
                									for (var i = 0; i < self.rowConditions.length; i++) {
                    									if (self.rowConditions[i] == dimensionNumber) { self.rowConditions.splice(i, 1); }
                									}
                									for (var i = 0; i < self.colConditions.length; i++) {
                    									if (self.colConditions[i] == dimensionNumber) { self.colConditions.splice(i, 1); }
                									}
                									if (!self.autoPaging){
                										self.preGoWhenMoveTopFilter(dimensionNumber);
                									} else {
                										self.preGoWhenFilterByTopFilter(false)
                									} 
                									self.go(false); refresh();
                										 });
            		 
            		var ippl = OAT.Dom.create("label");
            		ippl.innerHTML = gx.getMessage("GXPL_QViewerJSMoveToFilterBar"); 
            		ippl.htmlFor = "pivot_checkbox_movetocolumn";
            		Ipadpurp_sel_div.appendChild(ippl);
            		
            		OAT.Dom.append([Ipadpurp, Ipadpurp_sel_div]);
            		toAppend.append(Ipadpurp);
            		toAppend.append(hr6);
            	}
             }
            }
			/*end move to top filter*/
			
            var distinct = OAT.Dom.create("div");
            distinct.setAttribute("class","last_div_popup");
            var br1, br2, br3;
            if (!gx.util.browser.isIE()){
             	br1 = OAT.Dom.create("br"); var br2 = OAT.Dom.create("br"); var br3 = OAT.Dom.create("br");
            } else {
            	br1 = OAT.Dom.create("span"); var br2 = OAT.Dom.create("span"); var br3 = OAT.Dom.create("span");
            }
            toAppend.append(distinct);
            
            OAT.Dom.append(toAppend);
            
            self.distinctDivs(cond, distinct, dimensionNumber);

            //self.propPage._Instant_show();
            
            if (gx.util.browser.isIPad() || gx.util.browser.isIPhone()) {
            	jQuery('.oat_winrect_close_b').css({backgroundSize: "30px 30px", height: "30px", width: "30px", right: "-14px", top: "-14px"})
            }

			if (!disableColumnSort){
            	/* this needs to be here because of IE :/ */
            	asc.checked = (cond.sort == 1) || (cond.sort == 0);
            	asc.__checked = asc.checked;
            	desc.checked = (cond.sort == -1) || (cond.sort == 2);
            	desc.__checked = desc.checked;
           }
    }
    
    this.changedFromColumnToRow = function(dimensionNumber){
		
		var index = self.rowConditions.find(dimensionNumber);
		var position = "columns";
		if (index != -1){ //when move to row
        	self.rowConditions.splice(index, 1);
        	if (measures.length > 1)
        		self.colConditions = [dimensionNumber].concat(self.colConditions);
        	else
        		self.colConditions.push(dimensionNumber);
       } else {
       		if (!self.serverPagination){
       			position = "rows";
       			index = self.colConditions.find(dimensionNumber);
       			self.colConditions.splice(index, 1);
       			if (measures.length > 1)
       				self.rowConditions = [dimensionNumber].concat(self.rowConditions);
       			else
        			self.rowConditions.push(dimensionNumber);
        	} else {
        		position = "rows";
        		index = self.colConditions.find(dimensionNumber);
       			self.colConditions.splice(index, 1);
        		var tempRowConditions = [];
            	for(var nI = 0; nI < self.initRowConditions.length - (measures.length - 1); nI++){
            		if (self.initRowConditions[nI] == dimensionNumber){
            			tempRowConditions.push(dimensionNumber)
            		}
            		if (self.rowConditions.indexOf(self.initRowConditions[nI]) > -1)
            		{
            			tempRowConditions.push(self.initRowConditions[nI])
            		}
            	}
            	for(var nI = 0; nI < self.initColConditions.length; nI++){
            		if (self.initColConditions[nI] == dimensionNumber){
            			tempRowConditions.push(dimensionNumber)
            		}
            		if (self.rowConditions.indexOf(self.initColConditions[nI]) > -1)
            		{
            			tempRowConditions.push(self.initColConditions[nI])
            		}
            	}
            	for(var nI = 0; nI < self.initFilterIndexes.length; nI++){
            		if (self.initFilterIndexes[nI] == dimensionNumber){
            			tempRowConditions.push(dimensionNumber)
            		}
            		if (self.rowConditions.indexOf(self.initFilterIndexes[nI]) > -1)
            		{
            			tempRowConditions.push(self.initFilterIndexes[nI])
            		}
            	}
            	tempRowConditions.sort((function(){
    					return function(a, b){
       						return (a == b ? 0 : (a < b ? -1 : 1));
    						};
					})(0));
            	for(var nI = self.initRowConditions.length - (measures.length - 1); nI < self.initRowConditions.length; nI++){
            		tempRowConditions.push(self.initRowConditions[nI])
            	}
            
            	self.rowConditions = [];
            	for(var nI = 0; nI < tempRowConditions.length; nI++){
            		self.rowConditions[nI] = tempRowConditions[nI];
            	}
        	}
       }
	
	   self.onDragundDropEventHandle(dimensionNumber, position);
	}

    this.getDelFilterReference = function(index) {
        return function() {
            var idx = self.filterIndexes.find(index);
            if (idx != -1){
            	self.filterIndexes.splice(idx, 1);
            }
            
            var tempRowConditions = [];
            for(var nI = 0; nI < self.initRowConditions.length - (measures.length - 1); nI++){
            	if (self.initRowConditions[nI] == index){
            		tempRowConditions.push(index)
            	}
            	if (self.rowConditions.indexOf(self.initRowConditions[nI]) > -1)
            	{
            		tempRowConditions.push(self.initRowConditions[nI])
            	}
            }
            for(var nI = 0; nI < self.initColConditions.length; nI++){
            	if (self.initColConditions[nI] == index){
            		tempRowConditions.push(index)
            	}
            	if (self.rowConditions.indexOf(self.initColConditions[nI]) > -1)
            	{
            		tempRowConditions.push(self.initColConditions[nI])
            	}
            }
            for(var nI = 0; nI < self.initFilterIndexes.length; nI++){
            	if (self.initFilterIndexes[nI] == index){
            		tempRowConditions.push(index)
            	}
            	if (self.rowConditions.indexOf(self.initFilterIndexes[nI]) > -1)
            	{
            		tempRowConditions.push(self.initFilterIndexes[nI])
            	}
            }
            tempRowConditions.sort((function(){
    					return function(a, b){
       						return (a == b ? 0 : (a < b ? -1 : 1));
    						};
					})(0));
            for(var nI = self.initRowConditions.length - (measures.length - 1); nI < self.initRowConditions.length; nI++){
            	tempRowConditions.push(self.initRowConditions[nI])
            }
            
            
            self.rowConditions = [];
            for(var nI = 0; nI < tempRowConditions.length; nI++){
            	self.rowConditions[nI] = tempRowConditions[nI];
            }
            
            //self.rowConditions = self.rowConditions.concat(index)
            //self.rowConditions.sort()
            //delete select from self.filterDiv
            for(var ifs = 0; ifs<self.filterDiv.selects.length; ifs++){
            	if (index == self.filterDiv.selects[ifs].filterIndex){
            		self.filterDiv.selects.splice(ifs,1);
            		break;
            	}
            }
            
            self.stateChanged=true
            if (!self.serverPagination){
            	if (!self.autoPaging){
            		self.preGoWhenMoveTopFilter(-1);   
            	} else {
            		self.preGoWhenFilterByTopFilter(false)
            	}
            }
            self.onDragundDropEventHandle(index,"rows")
            
            self.DiseablePivot();
			setTimeout( function(){
            	if (!self.serverPagination){
            		self.go(false);
            	} else {
            		self.conditions[index].topFilterValue = "[all]"
            		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[index].dataField , { op: "all", values: "", dim: index }, "", "")
            	}
            	self.EneablePivot();
			},200)	
        }
    }

	this.valueIsShowed = function(value, dimensionNumber){
		if (!self.serverPagination){
			for( var l = 0; l < self.filteredData.length;  l++ ){
				if(self.filteredData[l][dimensionNumber] === value){
					return true;
				}
			}
			return false;
		} else {
			if (self.conditions[dimensionNumber].state == "all"){
            	return true;
            } else if (self.conditions[dimensionNumber].state == "none"){
            	return false;
            } else if (self.conditions[dimensionNumber].blackList.find(value) != -1) {
            	return false;
            } else if (self.conditions[dimensionNumber].distinctValues.find(value) != -1){
            	return true;	
            } else if (self.conditions[dimensionNumber].defaultAction == "Exclude"){
            	return false;
            } else {
            	return true;
            }
		}
	}

    this.distinctDivs = function(cond, div, dimensionNumber, allFilters) { /* set of distinct values checkboxes */
        var getPair = function(text, id) {
            var div = OAT.Dom.create("div");
            var class_check_div = (cond.blackList.find(value) == -1) && self.valueIsShowed(value, dimensionNumber)? "check_item_img": "uncheck_item_img";
            if (self.serverPagination){
            	var class_check_div = self.valueIsShowed(value, dimensionNumber)? "check_item_img": "uncheck_item_img";
            }
            div.setAttribute("class", class_check_div);
            var ch = OAT.Dom.create("input");
            ch.type = "checkbox";
            ch.id = id;
            var t = OAT.Dom.create("label");
            t.innerHTML = text;
            t.htmlFor = id;
            div.appendChild(t);
            return [div,ch];
        }

     
        //getRef 
           
        var getRefBool = function(checked, value) { 
            	
            	
           setTimeout( function(){
          			
            	if (self.serverPagination){
            		var oper = "pop";
                	if (!checked) {
                		oper = "push";
                	}
                
                	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[dimensionNumber].dataField , { op: oper, values: value, dim: dimensionNumber }, "", "")
				
                	self.stateChanged=true;
                	self.onFilteredChangedEventHandleWhenServerPagination(dimensionNumber);
                	self.EneablePivot();
                	return;
                }
            	
            	var splice = false;
                if (checked) {
                	if (!self.autoPaging){
                    	var index = cond.blackList.find(value);
                    	if (index!= -1){
                    		cond.blackList.splice(index, 1);
                    	}
                    
                    	if (self.autoPaging){
                    		var index = cond.blackList.find(value);
                    		while (index!= -1){
                    			cond.blackList.splice(index, 1);
                    			index = cond.blackList.find(value);
                    		}	
                    	}
                    
                    
                    	var whiteindex = cond.whiteList.find(value);
                    	if ((index == -1)){
                    		for(var rw = 0; rw < self.allData.length; rw ++){ //search for columns or columns with that value in all data
                    			if (self.allData[rw][dimensionNumber] === value){
                    				for(var it=0; it < self.allData[rw].length; it++){
                    					//is condition dimsension?
                    					if (it != dimensionNumber){
                    						var indexCol = self.rowConditions.find(it)
                    						if (indexCol!= -1){
                    							var colValue = self.allData[rw][it];
                    							while (self.conditions[indexCol].blackList.find(colValue) != -1){
                    								self.conditions[indexCol].blackList.splice(self.conditions[indexCol].blackList.find(colValue), 1);
                    							}
                    						} 
                    					}
                    				}
                    			}
                    		}
                    		cond.whiteList.push(value);
                    	} 
                    
                    	while(self.GreyList.find(value) != -1){
                    		index2 = self.GreyList.find(value);
                    		self.GreyList.splice(index2, 1);
                    	}
                    
                    	if (self.autoPaging){
                    		splice = true;
                    	}
                   } else { //add one item when autoPaging
                    	var index = self.conditions[dimensionNumber].blackList.find(value);
                    	if (index!= -1){
                    		self.conditions[dimensionNumber].blackList.splice(index, 1);
                    	}
                    	
                    	var index = self.conditions[dimensionNumber].blackList.find(value);
                    	while (index!= -1){
                    		self.conditions[dimensionNumber].blackList.splice(index, 1);
                    		index = self.conditions[dimensionNumber].blackList.find(value);
                    	}	
                    
                    	var whiteindex = self.conditions[dimensionNumber].whiteList.find(value);
                    	if ((index == -1)){
                    		for(var rw = 0; rw < self.allData.length; rw ++){ //search for columns or columns with that value in all data
                    			if (self.allData[rw][dimensionNumber] === value){
                    				for(var it=0; it < self.allData[rw].length; it++){
                    					//is condition dimsension?
                    					if (it != dimensionNumber){
                    						var indexCol = self.rowConditions.find(it)
                    						if (indexCol!= -1){
                    							var colValue = self.allData[rw][it];
                    							while (self.conditions[indexCol].blackList.find(colValue) != -1){
                    								self.conditions[indexCol].blackList.splice(self.conditions[indexCol].blackList.find(colValue), 1);
                    							}
                    						} 
                    					}
                    				}
                    			}
                    		}
                    		self.conditions[dimensionNumber].whiteList.push(value);
                    	} 
                    
                    	while(self.GreyList.find(value) != -1){
                    		index2 = self.GreyList.find(value);
                    		self.GreyList.splice(index2, 1);
                    	}
                    
                    	if (self.autoPaging){
                    		splice = true;
                    	}
                    }
                } else {
                	if (!self.autoPaging){
                    	cond.blackList.push(value);
                    } else {
                   	 	self.conditions[dimensionNumber].blackList.push(value);
                    }                    
                }
                self.onFilteredChangedEventHandle(dimensionNumber);
                self.stateChanged=true;
                if (!self.autoPaging){
                	self.go(false);
                } else {
                	if (jQuery("#"+self.containerName+" #tablePagination_currPage")[0] != undefined){
                		jQuery("#"+self.containerName+" #tablePagination_currPage")[0].value = 1;
                		self.actualPaginationPage = 1;
                	}
                	if (!splice){
                		self.preGo(false, null, value, dimensionNumber);
                	} else {
                		self.preGo(false, null, value, -1);
                	}
                }
            	
            	self.EneablePivot();
            	//jQuery("#" + UcId + "_" + self.query + "_pivot_page").unblock()
            	
            }, 200);
            
        }
		
        var allRef = function() {
        	self.DiseablePivot();
        	
        	setTimeout( function(){
        		
        		
        		if (self.serverPagination){
            		var oper = "all";
                	//self.conditions[dimensionNumber].blackList = [];
                
                	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[dimensionNumber].dataField , { op: oper, values: "", dim: dimensionNumber }, "", "")
				
                	self.stateChanged=true;
                	self.onFilteredChangedEventHandleWhenServerPagination(dimensionNumber);
                	self.distinctDivs(cond, div, dimensionNumber);
                	self.EneablePivot();
                	return;
                }
        		
        		if (!self.autoPaging){
            		cond.blackList = [];
            	} else {
           			self.conditions[dimensionNumber].blackList = [];
            	}
            	self.stateChanged=true;
            	self.onFilteredChangedEventHandle(dimensionNumber);
            	if (!self.autoPaging){
           		    self.go(false);
            	} else {
            		if (jQuery("#"+self.containerName+" #tablePagination_currPage")[0] != undefined){
            	    		jQuery("#"+self.containerName+" #tablePagination_currPage")[0].value = 1;
            	    		self.actualPaginationPage = 1;
            	    }
           		 	self.preGo(false, null, null, -1);
           		}
            	self.distinctDivs(cond, div, dimensionNumber);
            	self.EneablePivot();
            },200)
        }

        var noneRef = function() {
        	self.DiseablePivot()
        	
        	setTimeout( function(){
            	if (self.serverPagination){
            		var oper = "none";
                	/*self.conditions[dimensionNumber].blackList = [];
                	for (var i = 0; i < self.conditions[dimensionNumber].distinctValues.length; i++) { 
                		self.conditions[dimensionNumber].blackList.push(self.conditions[dimensionNumber].distinctValues[i]); 
                	}*/
                	
                	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[dimensionNumber].dataField , { op: oper, values: "", dim: dimensionNumber }, "", "")
				
                	self.stateChanged=true;
                	self.onFilteredChangedEventHandleWhenServerPagination(dimensionNumber);
                	self.distinctDivs(cond, div, dimensionNumber);
                	self.EneablePivot();
                	return;
                }
            	cond.blackList = [];
            	if (!self.autoPaging){
           	 		for (var i = 0; i < cond.distinctValues.length; i++) { cond.blackList.push(cond.distinctValues[i]); }
            	} else {
            		for (var i = 0; i < self.GeneralDistinctValues[dimensionNumber].length; i++) { self.conditions[dimensionNumber].blackList.push(self.GeneralDistinctValues[dimensionNumber][i]); }//cond.blackList.push(self.GeneralDistinctValues[dimensionNumber][i]); }
            	}
            	self.stateChanged=true;
            	self.onFilteredChangedEventHandle(dimensionNumber);
           		self.go(false);
            	self.distinctDivs(cond, div, dimensionNumber);
            	self.EneablePivot();
            },200)
        }

        var reverseRef = function() {
        	self.DiseablePivot();
        	
        	setTimeout( function(){ 
        		if (self.serverPagination){
            		var oper = "reverse";
                	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[dimensionNumber].dataField , { op: oper, values: "", dim: dimensionNumber }, "", "")
				
                	self.stateChanged=true;
                	self.onFilteredChangedEventHandleWhenServerPagination(dimensionNumber);
                	self.distinctDivs(cond, div, dimensionNumber);
                	self.EneablePivot();
                	return;
                }
        		       	
            	var newBL = [];
            	if (!self.autoPaging){
            		for (var i = 0; i < cond.distinctValues.length; i++) {
            		    var val = cond.distinctValues[i];
            		    if (cond.blackList.find(val) == -1) { newBL.push(val); }
            		}
            	} else {
            		for (var i = 0; i < self.GeneralDistinctValues[dimensionNumber].length; i++) {
            		    var val = self.GeneralDistinctValues[dimensionNumber][i];
            		    if (self.conditions[dimensionNumber].blackList.find(val) == -1) { newBL.push(val); }
            		}
            	}
            	if (!self.autoPaging){
            		cond.blackList = newBL;
            	} else {
            		self.conditions[dimensionNumber].blackList = newBL;
            	}
            	self.stateChanged=true;
            	self.onFilteredChangedEventHandle(dimensionNumber);
	           
           		if (!self.autoPaging){
            		self.go(false);
            	} else {
            		self.preGo(false, null, null, -1);
            	}
           
            	self.distinctDivs(cond, div, dimensionNumber);
            	self.EneablePivot();
            },200)
        }
	
		var searchFilterClick = function(){
        	self.getValuesForColumn(self.UcId, dimensionNumber, this.value)
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
        
		if (self.serverPagination){        
        	var d2 = OAT.Dom.create("div");
        	d2.setAttribute("class", "div_filter_input");
        
        	if (self.serverPagination){
        		var searchInput = document.createElement("input");
        		searchInput.textContent = "none";
        		searchInput.setAttribute("class", "search_input");
        		searchInput.setAttribute("type", "text");
        		searchInput.setAttribute("label", "Search filter...");
        		searchInput.setAttribute("title", "Search filter...");
        		searchInput.setAttribute("placeholder", "Search filter...");
        		searchInput.setAttribute("id" , self.UcId + dimensionNumber);
        		jQuery(searchInput).keyup( searchFilterClick );
        
        		OAT.Dom.append([d2, searchInput], [div, d2]);
        	}
       	}
        
        if (!self.autoPaging){
        	
        	var fixHeigthDiv = OAT.Dom.create("div");
        	if (self.serverPagination){
        		cond = self.conditions[dimensionNumber]
        	}
        	if (cond.distinctValues.length <= 9){
        		fixHeigthDiv.setAttribute("class","pivot_popup_auto");	
        	} else {
            	fixHeigthDiv.setAttribute("class","pivot_popup_fix");
            }
        	
        	
        	for (var i = 0; i < cond.distinctValues.length; i++) {
        		
        	  	var value = cond.distinctValues[i];
        	  	if (!((self.serverPagination) && (self.conditions[dimensionNumber].hasNull) && (value.trim() == self.defaultPicture.getAttribute("textForNullValues")))){
        	  	
            		var pict_value = self.dimensionPictureValue(value, dimensionNumber);
            		pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
            		if (pict_value.length > 33) {
            			var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
            			pict_value = pict_value.substring(0, 32) + resto
            		}
            		pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            		var pair = getPair(pict_value, "pivot_distinct_" + i);
            		pair[0].setAttribute('value', value);
            		fixHeigthDiv.appendChild(pair[0]);
            	
            	
            	
            		OAT.Dom.attach(pair[0], "click", function(){
            									self.DiseablePivot();
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );//this.textContent);            														
            											 });
            											 
            	}
        	}
        	fixHeigthDiv.setAttribute("ucid",self.UcId);
        	fixHeigthDiv.setAttribute("columnnumber",dimensionNumber);
        	fixHeigthDiv.setAttribute("id", "values_"+self.UcId + "_" + dimensionNumber)
        	div.appendChild(fixHeigthDiv);
        } else {
        	
        	var fixHeigthDiv = OAT.Dom.create("div");
        	if (self.GeneralDistinctValues[dimensionNumber].length <= 9){
        		fixHeigthDiv.setAttribute("class","pivot_popup_auto");	
        	}else {
            	fixHeigthDiv.setAttribute("class","pivot_popup_fix");
            }
        	
        	for (var i = 0; i < self.GeneralDistinctValues[dimensionNumber].length; i++) {
        			var value = self.GeneralDistinctValues[dimensionNumber][i];
        			var pict_value = self.dimensionPictureValue(value, dimensionNumber);
        			try{
        				if ((value == "#NuN#") && (!self.formulaInfo.measureFormula[dimensionNumber].hasFormula)){
        					pict_value = "&nbsp;"
        				}
        			} catch (Error) {}
        			
        			pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
            		if (pict_value.length > 33) {
            			var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
            			pict_value = pict_value.substring(0, 32) + resto
            		}
            		pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
        			
            		var pair = getPair(pict_value, "pivot_distinct_" + i);
            		pair[0].setAttribute('value', value);
            		fixHeigthDiv.appendChild(pair[0]);
            
            		//opciones - esta o no entre los distinctValues, si esta se muesta o no, y si no esta aparece en  black list o no 	
            		var checked_value = ((self.conditions[dimensionNumber].blackList.find(value) == -1) && self.valueIsShowed(value, dimensionNumber)  ||  ((self.conditions[dimensionNumber].distinctValues.find(value) == -1) && (self.conditions[dimensionNumber].blackList.find(value) == -1))); 	
            		var class_check_div = (checked_value) ? "check_item_img": "uncheck_item_img";
            		pair[0].setAttribute("class", class_check_div);
            		OAT.Dom.attach(pair[0], "click", function(){
            									self.DiseablePivot();
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );//this.textContent);            														
            											 });
            		
        	}
        	div.appendChild(fixHeigthDiv);
        	
        }
    }
	
	
	this.DiseablePivot = function(){
		try {
			var divDis = jQuery('<div class="disable_popup" ></div>');
		
			if (jQuery("#" + self.containerName).length > 0){
				var ht = Math.min(jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].clientHeight 
						+ jQuery("#" + UcId + "_" + self.query + "_pivot_content")[0].clientHeight,
						jQuery("#" + self.containerName)[0].clientHeight
					)
				var wd = Math.min(	jQuery("#" + UcId + "_" + self.query)[0].clientWidth,
							jQuery("#" + self.containerName)[0].clientWidth
					)
				if (jQuery("#" + self.containerName).closest(".gxwebcomponent").length > 0){ //for gxQuery
					if (jQuery("#" + UcId + "_" + self.query + "_tablePagination").length > 0){
						ht = ht + jQuery("#" + UcId + "_" + self.query + "_tablePagination")[0].clientHeight
					}
					jQuery(divDis).css({
						'width': wd,
						'height': ht,
						'position': 'absolute',
						'top': jQuery("#" + self.containerName)[0].offsetTop,
						'left': jQuery("#" + self.containerName)[0].offsetLeft,
						'background-color':'rgba(0,0,0,0.1)',
						'cursor':'wait',
						'z-index': 99
					});
				} else {
					jQuery(divDis).css({
						'width': wd,
						'height': ht,
						'position': 'absolute',
						'top': jQuery("#" + self.containerName).offset().top, 	
						'left': jQuery("#" + UcId + "_" + self.query).offset().left,
						'background-color':'rgba(0,0,0,0.1)',
						'cursor':'wait',
						'z-index': 999
					});
				}
		
				jQuery("#" + self.containerName).append(divDis)
      	
			} else {
				var ht = Math.min(jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol").find(".pivot_filter_div")[0].clientHeight 
							+ jQuery("#"+UcId+"_"+self.query).closest(".conteiner_table_div")[0].clientHeight,
							jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol")[0].clientHeight
						)
					
				var wd = Math.min(	jQuery("#" + UcId + "_" + self.query)[0].clientWidth,
							jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol")[0].clientWidth
						)		
			
				if (jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol").find(".pivot_pag_div").length > 0){
						ht = ht + jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol").find(".pivot_pag_div")[0].clientHeight
				}
			
				jQuery(divDis).css({
					'width': wd,
					'height': ht,
					'position': 'absolute',
					'top': jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol")[0].offsetTop,
					'left': jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol")[0].offsetLeft,
					'background-color':'rgba(0,0,0,0.1)',
					'cursor':'wait',
					'z-index': 99
				});
				
				jQuery("#"+UcId+"_"+self.query).closest(".gx_usercontrol").append(divDis)	
			}
			jQuery(".oat_winrect_container").block({ message: null })
		} catch(ERROR){}
    }
    
    this.EneablePivot = function(){
    		jQuery(".disable_popup").remove()
           	while (jQuery(".disable_popup").length > 0){
           		jQuery(".disable_popup").remove()	
           	}
           	jQuery(".oat_winrect_container").unblock()
    }       	
	
	this.addExpandCollapseFunctionality = function(th, item, rowConditionNumber, expanded, rowDimension){
		if (true){
			//var divImg = OAT.Dom.create("div",{position:"relative",left:"-15px",bottom:"-15px",width:"12px",height:"12px"});
			var divImg = OAT.Dom.create("div",{width:"12px",height:"12px",display: "inline-block", cssFloat:"left", paddingRight:"5px"});
			//var divImg = OAT.Dom.create("div",{width:"12px",height:"12px",display: "inline-block", cssFloat:"left", paddingRight:"5px", paddingTop: "5px"});
			if (expanded){
				divImg.className += " expanded";
			} else {
				divImg.className += " collapsed";
			}
			th.insertBefore(divImg, th.firstChild);
			th.style.paddingLeft = "0px";
			//th.style.paddingLeft = "15px";
			if (rowDimension){
				self.setClickEventHandlers(divImg, item.value, "DIMENSION", self.rowConditions[rowConditionNumber], item, true);	
			} else {
				self.setClickEventHandlers(divImg, item.value, "DIMENSION", self.colConditions[rowConditionNumber], item, true);
			}
			
		}
		return th;
	}
	
	this.setClickEventHandlers = function(td, itemValue, MeasureOrDimension, dimensionNumber, itemData, expandCollapse){
		if ((!self.ShowMeasuresAsRows) || (self.h < 500)){ 
			jQuery(td).data('itemValue', itemValue );
        	jQuery(td).data('typeMorD', MeasureOrDimension );
        	jQuery(td).data('numberMorD', dimensionNumber );
        	jQuery(td).data('itemInfo', itemData );
        	if ((td!=undefined) && (expandCollapse==undefined)){
        		td.onclick = function(){ self.onClickEventHandle(this); }
        	} else {
        		td.onclick = function(){ self.onClickExpandCollapse(this); }
        	}
        }
    }
    
  
	
	this.getPerFilterValue = function(prevFilterSelectedValue, actualfilterIndex){
		var hayfiltro = false;
		for (var i=0; i < prevFilterSelectedValue.length; i++){
			if (prevFilterSelectedValue[i] != null){
				hayfiltro = true;
			}
		}
		if (!hayfiltro) return false;
		
		var distinctPerFilterValue = [];
		for (var i=0; i < self.GeneralDataRows.length; i++){ //for each row
			var coincide = true;
			for (var j=0; j < prevFilterSelectedValue.length; j++){
				if (prevFilterSelectedValue[j]!=null){
					if (self.GeneralDataRows[i][self.filterIndexes[j]] != prevFilterSelectedValue[j]){
						coincide = false;
					}
				}
			}
			if (coincide){
				distinctPerFilterValue.append(self.GeneralDataRows[i][self.filterIndexes[actualfilterIndex]])
			}
		}
		
		return distinctPerFilterValue;
		
	}
	
    this.drawFilters = function() {
        var savedValues = [];
        var div = self.filterDiv;
        
        var ua = navigator.userAgent.toLowerCase();
		var isAndroid = ua.indexOf("android") > -1;
        if ((gx.util.browser.isIE()) || (isAndroid)){
        	self.filterDiv.className += " pivot_filter_div";
        } else {
        	self.filterDiv.classList.add("pivot_filter_div");
        }
        if (self.filterIndexes.length == 0){
        	self.filterDiv.innerHTML = "drop filters here";
        }
        
        for (var i = 0; i < div.selects.length; i++) {
            savedValues.push([div.selects[i].filterIndex, div.selects[i].selectedIndex, div.selects[i].value]);
        }
        OAT.Dom.clear(div);
        self.gd.addTarget(div);
        div.selects = [];
        if (!self.filterIndexes.length) {
            //div.innerHTML = "[drop filters here]";
            var strng = gx.getMessage("GXPL_QViewerJSDropFiltersHere");
            if(gx.util.browser.isIPad() || gx.util.browser.isIPhone()) {
            	strng = "";
            }
            var ua = navigator.userAgent.toLowerCase();
			var isAndroid = ua.indexOf("android") > -1; //&& ua.indexOf("mobile");
			if (isAndroid) {
				strng = "";
			}
            
            
            if ((strng[strng.length - 1] === "\"") || (strng[strng.length - 1] === "'") || (strng[strng.length - 1] === "]") || (strng[strng.length - 1] === "}") || (strng[strng.length - 1] === "`")) {
            	strng = strng.substring( 0, strng.length - 1);
            }
            
            var spanText = document.createElement('span')
            spanText.textContent = strng;
            
            div.appendChild(spanText);
        }
        
        var loadItems = function(){
        	var s = this;
        	var actualValues = self.conditions[this.filterIndex].distinctValues;
        	for (var j = 0; j <  actualValues.length; j++) {
        		var v =  actualValues[j];
        		if (self.conditions[this.filterIndex].filteredShowValues.indexOf(v) == -1){
        			self.conditions[this.filterIndex].filteredShowValues.push(v);
            		if (v != "#NuN#"){
            			OAT.Dom.option(v, v, s);
            		} else {
            			OAT.Dom.option(" ", v, s);
            		}
            	}
            }
            //load all other items
            self.lastRequestValue = this.filterIndex; var columnNumber = this.filterIndex;
            if (self.conditions[columnNumber].previousPage < self.conditions[columnNumber].totalPages){ 
				self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
					var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    			var data = JSON.parse(validStr);
	    			
	    			self.conditions[columnNumber].previousPage = data.PageNumber
    				self.conditions[columnNumber].totalPages   = data.PagesCount
	    			self.conditions[columnNumber].blocked      = true
	    			//null value?
    				if ((data.Null) && (!self.conditions[columnNumber].hasNull)){
    					self.conditions[columnNumber].hasNull = true;
    					if (self.conditions[columnNumber].distinctValues.indexOf("#NuN#") == -1){
    						self.conditions[columnNumber].distinctValues.push("#NuN#")
    					}
    					if (self.conditions[columnNumber].defaultAction == "Include"){
    						if (self.conditions[columnNumber].visibles.indexOf("#NuN#") == -1){
    							self.conditions[columnNumber].visibles.push("#NuN#");
    						}
    					} else {
    						if (self.conditions[columnNumber].blackList.indexOf("#NuN#") == -1){
    							self.conditions[columnNumber].blackList.push("#NuN#");
    						}
    					}
    				} 
	    			
	    			for (var i = 0; i < data.NotNullValues.length; i++){
	    				var value = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
	    				if (self.conditions[columnNumber].distinctValues.indexOf(value) == -1){
	    					self.conditions[columnNumber].distinctValues.push(value)
	    				
	    					if ( (self.conditions[columnNumber].defaultAction == "Include")
	    							&& (self.conditions[columnNumber].visibles.indexOf(value) == -1)){
	    						self.conditions[columnNumber].visibles.push(value)
	    					}
	    					if ( (self.conditions[columnNumber].state == "Exclude") 
	    							&& (self.conditions[columnNumber].blackList.indexOf(value) == -1)){
	    						self.conditions[columnNumber].blackList.push(value)
	    					}
	    				}
	    			}
	    			
	    			var actualValues = self.conditions[columnNumber].distinctValues;
        			for (var j = 0; j <  actualValues.length; j++) {
        				var v =  actualValues[j];
        				if (self.conditions[columnNumber].filteredShowValues.indexOf(v) == -1){
        					self.conditions[columnNumber].filteredShowValues.push(v);
            				if (v != "#NuN#"){
            					OAT.Dom.option(v, v, s);
            				} else {
            					OAT.Dom.option(" ", v, s);
            				}
            		}
            	}
	    			
		   		}).closure(this), [self.columns[this.filterIndex].getAttribute("dataField"), 1, 0, ""]);
		   	}
        }
        var callgo = function(){
        	self.stateChanged=true
        	if (self.serverPagination){
        		self.DiseablePivot();
        		self.conditions[this.filterIndex].topFilterValue = this.value
        		self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[this.filterIndex].dataField , { op: "pagefilter", values: this.value, dim: this.filterIndex }, "", "")
        		self.EneablePivot();
        		return;
        	} else {
        		if (this.value == "[all]"/*""*/)//selected option [all]
        		{
        			if (!self.autoPaging){
        				self.preGoWhenMoveTopFilter(this.filterIndex)
        			} else {
        				self.preGoWhenFilterByTopFilter(false)
        			}	
        		}
        		else
        		{
        			self.preGoWhenFilterByTopFilter(false);	
        		}
        		self.actualPaginationPage = 1
        	}
        	self.DiseablePivot();

			setTimeout( function(){
        		self.go(false);
        		self.EneablePivot();
			},200)
        }
        var prevFilterSelectedValue = [];
        for (var i = 0; i < self.filterIndexes.length; i++) {
            var index = self.filterIndexes[i];
            var s = OAT.Dom.create("select");
            s.setAttribute("id","page_select_" + i)
            OAT.Dom.option(gx.getMessage("GXPL_QViewerJSAllOption")/*"[all]"*/, "[all]"/*""*/, s);
            
            var actualValues;
            if (self.serverPagination){
            	actualValues = []
            } else {
            	actualValues = self.getPerFilterValue(prevFilterSelectedValue, i);
            } 
            
            if (self.GeneralDistinctValues.length > 0){
            	for (var j = 0; j < self.GeneralDistinctValues[index].length; j++) {
        			var v = self.GeneralDistinctValues[index][j];
        			if (!actualValues){
        				if (v != "#NuN#"){
            				OAT.Dom.option(v, v, s);
            			} else {
            				OAT.Dom.option(" ", v, s);
            			}
            		} else {
            			if (actualValues.indexOf(v) != -1){
            				if (v != "#NuN#"){
            					OAT.Dom.option(v, v, s);
            				} else {
            					OAT.Dom.option(" ", v, s);
            				}
            			}
            		}
        		}
            
            	try {
            		var pos = 0;
            		for (var j = 0; j < self.GeneralDistinctValues[index].length; j++) {
        				var v = self.GeneralDistinctValues[index][j];
        				if (self.conditions[index].topFilterValue === v){
            				s.selectedIndex = pos+1;
            			}
            			if (!actualValues){
            				pos++;
            			} else {
            				if (actualValues.indexOf(v) != -1){
            					pos++;
            				}
            			}
            		}
            	} catch (error){
            		
            	}
            }
            
            if (self.serverPagination){
            	self.conditions[index].filteredShowValues = []
            	var actualValues = self.conditions[index].distinctValues;
        		for (var j = 0; j <  actualValues.length; j++) {
        			var v =  actualValues[j];
        			if (self.conditions[index].filteredShowValues.indexOf(v) == -1){
        				self.conditions[index].filteredShowValues.push(v);
            			if (v != "#NuN#"){
            				OAT.Dom.option(v, v, s);
            			} else {
            				OAT.Dom.option(" ", v, s);
            			}
            		}
            	}
            	if ((self.conditions[index].topFilterValue!="[all]") /*&& (self.conditions[index].topFilterValue!="")*/){
            		var v = self.conditions[index].topFilterValue
            		if (self.conditions[index].filteredShowValues.indexOf(v) != -1){ //value already load
            			s.selectedIndex = self.conditions[index].filteredShowValues.indexOf(v)+1
            		} else {
            			self.conditions[index].filteredShowValues.push(v);
            			if (v != "#NuN#"){
            				OAT.Dom.option(v, v, s);
            			} else {
            				OAT.Dom.option(" ", v, s);
            			}
            			s.selectedIndex = 1
            		}
            	}
            }
            
            s.filterIndex = index;
            for (var j = 0; j < savedValues.length; j++) {
                if (savedValues[j][0] == index) { 
                	for(var it = 0; it < s.length; it++){
                		if (s[it].value == savedValues[j][2]){
                			s.selectedIndex = it
                		}
                	}
                }
            }
            
            if (s.selectedIndex > 0){
            	prevFilterSelectedValue[i] = s.value; 
            } else {
            	prevFilterSelectedValue[i] = null
            }
            
            if (self.serverPagination){
            	OAT.Dom.attach(s, "click", loadItems);
            }
            OAT.Dom.attach(s, "change", callgo);
            div.selects.push(s);
            var d = OAT.Dom.create("div");
            d.setAttribute("class", "inner_filter_div");
            d.innerHTML = self.headerRow[index] + ": ";
			

            var close = document.createElement("div");
            close.setAttribute("class", "close_span_filter");
            close.style.color = "#f00";
            close.style.cursor = "pointer";
            var ref = self.getDelFilterReference(index);
            OAT.Dom.attach(close, "click", ref);
			
			if (self.serverPagination){
				OAT.Dom.append([self.filterDiv, d], [d, s, close]);
			} else if (self.GeneralDataRows.length == 0){
				OAT.Dom.append([self.filterDiv, d], [d, s]);
			} else {
           		 OAT.Dom.append([self.filterDiv, d], [d, s, close]);
            }
        }
        
        
        //draw export image and pop up of export options
        var exportImg = OAT.Dom.create("div");
        exportImg.href = "#";
        exportImg.setAttribute("class","exportOptionsAnchor");
        
        self.exportPage = OAT.Dom.create("div", { padding: "5px" });
        var checkToClose = function(b){
        	source = OAT.Event.source(b);
        	var clean = false;
        	var closing = false;
        	for (var i = 0; i < jQuery(".oat_winrect_container").length; i++){
        		var obj = jQuery(".oat_winrect_container")[i];
        		if (!(source == obj) && !OAT.Dom.isChild(source, obj)){
        			clean = true;
        		} else {
        			clean = false; break;
        		}
        	}
        	for (var i = 0; i < jQuery(".oat_winrect_container").length; i++){
        		if (jQuery(".oat_winrect_container")[i].style.display != "none") {
        			closing = true;
        		}
        	}
        	if ( (self.serverPagination) && 
        		 ((source.getAttribute("class") == 	"oat_winrect_close_b") || (!OAT.Dom.isChild(source, obj))) &&
        		 (closing))
        	{
        			self.resetAllScrollValue(self.UcId);
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
        
        var clickRef = function(event) {
        		var coords = OAT.Event.position(event);
                self.exportPage.style.left = coords[0] + "px";
                self.exportPage.style.top = coords[1] + "px";
                self.exportPage.id = "exportOptionsContainer";
        		OAT.Dom.clear(self.exportPage);
        		
        		var div_upper = document.createElement("div");
        		div_upper.setAttribute("class", "upper_container");
        		
        		//botton to allow show all filters in pop up
        		jQuery('#divtoxml').remove();
    			jQuery('#divtoxls').remove();
    			jQuery('#divtoxlsx').remove();
    			jQuery('#divtoexport').remove();
    			jQuery('#divtohtml').remove();
        		var someExport = false;
        		self.appendExportToXmlOption(div_upper, someExport);
        		self.appendExportToHtmlOption(div_upper, someExport);
        		self.appendExportToPdfOption(div_upper, someExport);
        		self.appendExportToExcelOption(div_upper, someExport);
        		self.appendExportToExcel2010Option(div_upper, someExport);
            
        		self.exportPage.appendChild(div_upper);
        		if (gx.util.browser.isIPad() || gx.util.browser.isIPhone()) {
                	jQuery('.oat_winrect_close_b').css({backgroundSize: "30px 30px", height: "30px", width: "30px", right: "-14px", top: "-14px"})
                }
        } /* clickref */
        OAT.Event.attach(exportImg, "click", clickRef);
        
        if ((self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXML) || (self.QueryViewerCollection[IdForQueryViewerCollection].ExportToHTML)
         || (self.QueryViewerCollection[IdForQueryViewerCollection].ExportToPDF) || (self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXLS)
         || (self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXLSX)){
        	self.filterDiv.appendChild(exportImg);
        }
        
    }
    
    this.appendExportToXmlOption = function(content, someExport){
    		var exportXMLButton;
        	//if (self.QueryViewerCollection[self.controlName.toUpperCase() + "_" + self.controlName].ExportToXML){
        	if (self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXML){	
        		if (gx.util.browser.isIE() && 9>=gx.util.browser.ieVersion()){
        			exportXMLButton = document.createElement("div");
        			exportXMLButton.style.marginBottom = "10px" 
  					exportXMLButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
  					exportXMLButtonSub.setAttribute('id', 'divtoxml');
  					var FileName = self.query
  					if (FileName == ""){
  						FileName = "Query"
  						try {
  							FileName = self.controlName.split("_")[0]
  						} catch (error) {}
  					}
  					setTimeout( function(){
  						jQuery("#divtoxml").downloadify({ 
    						filename: function(){
      								return FileName+'.xml';
    						},
    						data: function(){
    							return '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' + self.ExportToXML();
    						},
    						onComplete: function(){
    						},
    						onCancel: function(){
    						},
    						onError: function(){
    						},
    						//swf: 'QueryViewer/oatPivot/downloadify/media/downloadify.swf',
    						//downloadImage: 'QueryViewer/oatPivot/images/download_file.png',
    						swf: self.swfPath, 
    						downloadImage: self.downloadImagePath, 
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
            		
            		
  					
  				} else {
  					exportXMLButton = OAT.Dom.create("div");
  					exportXMLButton.style.marginBottom = "10px"
  					
  					exportXMLButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  					exportXMLButtonSub.setAttribute('id', 'divtoxml');
  					exportXMLButton.appendChild(exportXMLButtonSub);
  					 
  					exportXMLimg = OAT.Dom.create("img");
  					exportXMLimg.src = self.downloadImagePath
  					exportXMLButtonSub.appendChild(exportXMLimg)
  					 
           			exportXMLButton.setAttribute("class", "export_item_div");
            		var pvpl = OAT.Dom.create("label");
            		pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXml");
            		pvpl.htmlFor = "pivot_checkbox_restoreview";
            		exportXMLButton.appendChild(pvpl);
            		var span = document.createElement("span");
            		exportXMLButton.appendChild(span);
            		
            		OAT.Dom.attach(exportXMLButtonSub, "click",  function() { 	
            												if (self.serverPagination){
            													self.getDataForPivot(self.UcId, 1, -1, true, "", "", "XML", "")	
            												} else {
            													str = self.ExportToXML();
            												
            													if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
            														window.open('data:text/xml,' +  encodeURIComponent('<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' + str) );
																} else {
            														var blob = new Blob([str], {type: "text/xml"});
            														if (self.query != ""){
            															saveAs(blob, self.query+".xml");
            														} else {
            															var name = 'Query'
            															try {
            																name = self.controlName.substr(4).split("_")[0]
            															} catch (error) {}
            															saveAs(blob, name+".xml");
            														}
            													}
            												}
            										   });
            	}
            	if (someExport){
            		if (!gx.util.browser.isIE()){
            			content.appendChild(OAT.Dom.create("br"));
            		}
            	} 
            	content.appendChild(exportXMLButton);
            	someExport = true;
            }
    }
	
	this.ExportToXMLWhenServerPagination = function(){
		 self.QueryViewerCollection[self.IdForQueryViewerCollection].calculatePivottableData_JS((function (resText) {
                self.allDataWithoutSort = OATgetDataFromXMLOldFormat(resText, self.pageData.dataFields)
                self.allData = self.allDataWithoutSort
                
                var prevConditions = jQuery.extend(true, [], self.conditions);
                for (var t = 0; t < self.conditions.length; t++){
                	if (self.conditions[t]){
                		for (var i = 0; i < self.allData.length; i++) {
        	    			var value = self.allData[i][t];
        	    			if (value == undefined){
        	    				value = " ";
        	    				self.allData[i][index] = " "; 
        	    			}
        	    			if (self.conditions[t].distinctValues.find(value) == -1) {
        	    			    self.conditions[t].distinctValues.push(value);
        	    			}
        				}
        				try{
        					self.sort(self.conditions[t], t);
        				} catch(ERROR){}
        			}
        		}
                
                self.applyFilters();
        		self.createAggStructure();
        		self.fillAggStructure();
        		self.checkAggStructure();
        		//self.count();
                
                str = self.ExportToXML();
            	self.allDataWithoutSort = []; self.allData = []; self.filteredData = [];
            	self.conditions = prevConditions;
            												
            	if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
            		window.open('data:text/xml,' +  encodeURIComponent('<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' + str) );
				} else {
            		var blob = new Blob([str], {type: "text/xml"});
            		if (self.query != ""){
            			saveAs(blob, self.query+".xml");
            		} else {
            			var name = 'Query'
            			try {
            				name = self.controlName.substr(4).split("_")[0]
            			} catch (error) {}
            			saveAs(blob, name+".xml");
            		}
            	}
                
         }).closure(this))
	}
	
	this.ExportToHTMLWhenServerPagination = function(){
		var dir = location.href.substr(0,location.href.indexOf(location.pathname)) + gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/pivot.css')
					
		var str = "<!DOCTYPE><HTML><BODY>";
		str = str + "<HEAD>";
		str = str + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>'
		
		str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="' + dir + '" />'
		str = str + "</HEAD><DIV style=\"margin-bottom: 5px;\">"
		str = str + jQuery("#" + self.controlName + "_" + self.query)[0].outerHTML.replace(/display: none;/g,"").replace(/sort-asc/g,"").replace(/sort-desc/g, "");
		str = str + "</DIV></BODY></HTML>";
		
		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
			window.open('data:text/html,' + str);
		} else {
			var blob = new Blob([str], {type: "text/html"});
			if (self.query != ""){
        		saveAs( blob, self.query+".html");
        	} else {
        		var name = 'Query'
        		try {
        			name = self.controlName.substr(4).split("_")[0]
        		} catch (error) {}
        			saveAs( blob, name+".html");
        	}
		}
	}
	
	this.appendExportToHtmlOption = function(content, someExport) {
		var exportHTMLButton;
		if(self.QueryViewerCollection[IdForQueryViewerCollection].ExportToHTML) {
			if (!gx.util.browser.isIE() || 9<gx.util.browser.ieVersion()){ 
				
				 var exportHTMLButton = OAT.Dom.create("div");
				 exportHTMLButton.style.marginBottom = "10px"
  					
  				 var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  				 exportButtonSub.setAttribute('id', 'divtoxml');
  				 exportHTMLButton.appendChild(exportButtonSub);
  					 
  				 var exportimg = OAT.Dom.create("img");
  				 exportimg.src = self.downloadImagePath
  				 exportButtonSub.appendChild(exportimg)
  				 
				 exportHTMLButton.setAttribute("class", "export_item_div");
				 var pvpl = OAT.Dom.create("label");
				 pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportHtml");
            	 pvpl.htmlFor = "pivot_checkbox_restoreview";
            	 exportHTMLButton.appendChild(pvpl);
				 
		
				
				OAT.Dom.attach(exportButtonSub, "click", function() {
					if (self.serverPagination){
						self.getDataForPivot(self.UcId, 1, -1, true, "", "", "HTML", "")	
					} else {
						var dir = location.href.substr(0,location.href.indexOf(location.pathname)) + gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/pivot.css')
					
						var str = "<!DOCTYPE><HTML><BODY>";
						str = str + "<HEAD>";
						str = str + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>'
						//str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="' + dir + '/QueryViewer/oatPivot/css/pivot.css" />'
						str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="' + dir + '" />'
						str = str + "</HEAD><DIV style=\"margin-bottom: 5px;\">"
						str = str + jQuery("#" + self.controlName + "_" + self.query)[0].outerHTML.replace(/display: none;/g,"").replace(/sort-asc/g,"").replace(/sort-desc/g, "");
						str = str + "</DIV></BODY></HTML>";
						//if(!gx.util.browser.isIE()) {
						if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
							window.open('data:text/html,' + str);
						} else {
							var blob = new Blob([str], {type: "text/html"});
							if (self.query != ""){
            					saveAs( blob, self.query+".html");
            				} else {
            					var name = 'Query'
            					try {
            						name = self.controlName.substr(4).split("_")[0]
            					} catch (error) {}
            					saveAs( blob, name+".html");
            				}
						}
					}
				});
			} else {
				exportHTMLButton = document.createElement("div");
				exportHTMLButton.style.marginBottom = "10px" 
				exportHTMLButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
				exportHTMLButtonSub.setAttribute('id', 'divtohtml')
				var FileName = self.query
  				if (FileName == ""){
  					FileName = "Query"
  					try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
  				}
				setTimeout(function() {
					jQuery("#divtohtml").downloadify({
						filename : function() {
							return FileName+'.html';
						},
						data : function() {
							var dir = location.href.substr(0,location.href.indexOf(location.pathname)) + gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/css/pivot.css')

							var str = "<!DOCTYPE><HTML><BODY>";
							str = str + "<HEAD>";
							str = str + '<META content="text/html; charset=utf-8" http-equiv="Content-Type"/>'
							str = str + '<link id="gxtheme_css_reference" rel="stylesheet" type="text/css" href="' + dir + '" />'
							str = str + "</HEAD><DIV style=\"margin-bottom: 5px;\">"
							str = str + jQuery("#" + self.controlName + "_" + self.query)[0].outerHTML.replace(/display: none;/g,"").replace(/sort-asc/g,"").replace(/sort-desc/g, "");
							str = str + "</DIV></BODY></HTML>";
							return str;
						},
						onComplete : function() {
						},
						onCancel : function() {
						},
						onError : function() {
						},
						swf: self.swfPath, 
    					downloadImage: self.downloadImagePath, 
						width : 21,
						height : 21,
						transparent : true,
						append : false
					});
				}, 100);
				
				exportHTMLButton.appendChild(exportHTMLButtonSub);
  				var pvpl = OAT.Dom.create("label");
            	pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportHtml");
            	pvpl.style.paddingLeft = "9px";
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportHTMLButton.appendChild(pvpl);
			}
			if(someExport) {
				if(!gx.util.browser.isIE()) {
					content.appendChild(OAT.Dom.create("br"));
				}
			}
			content.appendChild(exportHTMLButton);
			someExport = true;
		}
	}

    
	this.appendExportToPdfOption = function(content, someExport) {
		var exportPdfButton;
		if(self.QueryViewerCollection[IdForQueryViewerCollection].ExportToPDF) {
			someExport = true;
			if (gx.util.browser.isIE() && (9>=gx.util.browser.ieVersion())) {
				exportPdfButton = document.createElement("div");
				exportPdfButton.style.marginBottom = "10px" 
				exportPdfButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
				
				exportPdfButtonSub.setAttribute('id', 'divtoexport')
				var FileName = self.query
  				if (FileName == ""){
  					FileName = "Query"
  					try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
  				}
				setTimeout(function() {
					jQuery("#divtoexport").downloadify({
						filename : function() {
							return FileName+'.pdf';
						},
						data : function() {
							return btoa(OAT.GeneratePDFOutput(self));
						},
						onComplete : function() {
						},
						onCancel : function() {
						},
						onError : function() {
						},
						dataType: 'base64',
						swf: self.swfPath, 
    					downloadImage: self.downloadImagePath, 
						width : 21,
						height : 21,
						transparent : true,
						append : false
					});
				}, 100);
				
				exportPdfButton.appendChild(exportPdfButtonSub);
				var pvpl = OAT.Dom.create("label");
            	pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportPdf");
            	pvpl.style.paddingLeft = "9px";
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportPdfButton.appendChild(pvpl);
			} else {
				var exportPdfButton = OAT.Dom.create("div");
				var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  				exportButtonSub.setAttribute('id', 'divtoxml');
  				exportPdfButton.appendChild(exportButtonSub);
  					 
  				var exportimg = OAT.Dom.create("img");
  				exportimg.src = self.downloadImagePath
  				exportButtonSub.appendChild(exportimg)
				
				exportPdfButton.setAttribute("class", "export_item_div");
				var pvpl = OAT.Dom.create("label");
				pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportPdf")
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportPdfButton.appendChild(pvpl);
				
				var FileName = self.query
  				if (FileName == ""){
  					FileName = "Query"
  					try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
  				}
				OAT.Dom.attach(exportButtonSub, "click", function() {
					if (self.serverPagination){
						self.getDataForPivot(self.UcId, 1, -1, true, "", "", "PDF", "")	
					} else {
						OAT.GeneratePDFOutput(self, FileName)
					}
				});
			}

			content.appendChild(exportPdfButton);
		}
	}

    
    
	this.appendExportToExcelOption = function(content, someExport) {
		var exportXLSButton;
		if(self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXLS) {

			if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())) {
				var exportXLSButton = OAT.Dom.create("div");
				var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  				exportButtonSub.setAttribute('id', 'divtoxml');
  				exportXLSButton.appendChild(exportButtonSub);
  					 
  				var exportimg = OAT.Dom.create("img");
  				exportimg.src = self.downloadImagePath
  				exportButtonSub.appendChild(exportimg)
				
				exportXLSButton.setAttribute("class", "export_item_div");
				var pvpl = OAT.Dom.create("label");
				pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXls2003")
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportXLSButton.appendChild(pvpl);
            	
            	var FileName = self.query
            	if (FileName == ""){
            		FileName = "Query"
            		try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
            	}
				OAT.Dom.attach(exportButtonSub, "click", function() {
					if (self.serverPagination){
						self.getDataForPivot(self.UcId, 1, -1, true, "", "", "XLS", "")	
					} else {
						self.ExportToExcel(FileName);
					}
				});
			} else {
				exportXLSButton = document.createElement("div");
				exportXLSButton.style.marginBottom = "10px" 
				exportXLSButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
				
				exportXLSButtonSub.setAttribute('id', 'divtoxls');
				var FileName = self.query
  				if (FileName == ""){
  					FileName = "Query"
  					try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
  				}
				setTimeout(function() {
					jQuery("#divtoxls").downloadify({
						filename : function() {
							return FileName+'.xls';
						},
						data : function() {
							return self.ExportToExcel();
						},
						onComplete : function() {
						},
						onCancel : function() {
						},
						onError : function() {
						},
						//transparent: false,
						swf: self.swfPath, 
    					downloadImage: self.downloadImagePath, 
						width : 21,
						height : 21,
						transparent : true,
						append : false
					});
				}, 100);
				exportXLSButton.appendChild(exportXLSButtonSub);
  				var pvpl = OAT.Dom.create("label");
            	pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXls2003");
            	pvpl.style.paddingLeft = "9px";
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportXLSButton.appendChild(pvpl);
			}
			if(someExport) {
				if(!gx.util.browser.isIE()) {
					content.appendChild(OAT.Dom.create("br"));
				}
			}
			content.appendChild(exportXLSButton);
			someExport = true;
		}
	}
	
	this.ExportToXLSXWhenServerPagination = function(){
		 self.QueryViewerCollection[self.IdForQueryViewerCollection].calculatePivottableData_JS((function (resText) {
                //self.GeneralDataRows = OATgetDataFromXMLOldFormat(resText, self.pageData.dataFields)
                var res = OATgetDataFromXMLOldFormat(resText, self.pageData.dataFields, self.orderFildsHidden)
                self.GeneralDataRows  = res[0]  
                self.recordForFormula = res[1]
                self.allData = self.GeneralDataRows
                
                var prevConditions = jQuery.extend(true, [], self.conditions);
                self.GeneralDistinctValues = [];
                for (var t = 0; t < self.conditions.length; t++){
                	if (self.conditions[t]){
                		self.GeneralDistinctValues[t] = []
                		for (var i = 0; i < self.allData.length; i++) {
        	    			var value = self.allData[i][t];
        	    			if (value == undefined){
        	    				value = " ";
        	    				self.allData[i][index] = " "; 
        	    			}
        	    			if (self.conditions[t].distinctValues.find(value) == -1) {
        	    			    self.conditions[t].distinctValues.push(value);
        	    			}
        	    			if (self.GeneralDistinctValues[t].find(value) == -1) {
        	    				self.GeneralDistinctValues[t].push(value);
        	    			}
        				}
        				try{
        					self.sort(self.conditions[t], t);
        				} catch(ERROR){}
        			}
        		}
                
                self.applyFilters();
        		self.createAggStructure();
        		self.fillAggStructure();
        		self.checkAggStructure();
        		//self.count();
                
                var FileName = self.query
            	if (FileName == ""){
            		FileName = "Query"
            		try {
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
            	}										
            	OAT.GenerateExcelOutput(FileName, self);
                
                str = self.ExportToXML();
            	self.GeneralDataRows = []; self.allData = []; self.filteredData = []; self.GeneralDistinctValues = [];
            	self.conditions = prevConditions;
                
         }).closure(this))
	}
	
	this.appendExportToExcel2010Option = function(content, someExport) {
		var exportXLSButton;
		if ((self.QueryViewerCollection[IdForQueryViewerCollection].ExportToXLSX) && ((self.allData.length > 0) || (self.serverPagination))) {

			if ((!gx.util.browser.isIE()) || (9<gx.util.browser.ieVersion()) ) {
				var exportXLSButton = OAT.Dom.create("div");
				var exportButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left", width: "21px", height: "21px", 
  																		overflow: "hidden", marginRight: "10px", cursor: "pointer"});
  				exportButtonSub.setAttribute('id', 'divtoxml');
  				exportXLSButton.appendChild(exportButtonSub);
  					 
  				var exportimg = OAT.Dom.create("img");
  				exportimg.src = self.downloadImagePath
  				exportButtonSub.appendChild(exportimg)
				
				exportXLSButton.setAttribute("class", "export_item_div");
				var pvpl = OAT.Dom.create("label");
				pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXlsx")
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportXLSButton.appendChild(pvpl);
            	
            	var FileName = self.query
            	if (FileName == ""){
            		FileName = "Query"
            		try {
  						//FileName = self.controlName.substr(4).split("_")[0]
  						FileName = self.controlName.split("_")[0]
  					} catch (error) {}
            	}
				OAT.Dom.attach(exportXLSButton, "click", function() {
					if (self.serverPagination){
						self.getDataForPivot(self.UcId, 1, -1, true, "", "", "XLSX", "")	;
					} else {
						OAT.GenerateExcelOutput(FileName, self);//self.ExportToExcel2010(FileName);
					}
				});
			} else {
				exportXLSButton = document.createElement("div");
				exportXLSButton.style.marginBottom = "10px" 
				exportXLSButtonSub = OAT.Dom.create("div", { paddingLeft: "1px", position: "relative", cssFloat:"left"});
				
				exportXLSButtonSub.setAttribute('id', 'divtoxlsx');
				var FileName = self.query
				if (FileName == ""){
					FileName = "Query"
					try {
						FileName = self.controlName.split("_")[0]
					} catch (error) {}
				}
				setTimeout(function() {
					jQuery("#divtoxlsx").downloadify({
						filename : function() {
							return FileName+'.xlsx';
						},
						data : function() {
							return OAT.GenerateExcelOutput("", self);//self.ExportToExcel2010();
						},
						onComplete : function() {
						},
						onCancel : function() {
						},
						onError : function() {
						},
						dataType: 'base64',
						//transparent: false,
						swf: self.swfPath, 
    					downloadImage: self.downloadImagePath, 
						width : 21,
						height : 21,
						transparent : true,
						append : false
					});
				}, 100);
				exportXLSButton.appendChild(exportXLSButtonSub);
  				var pvpl = OAT.Dom.create("label");
            	pvpl.innerHTML = gx.getMessage("GXPL_QViewerContextMenuExportXlsx");
            	pvpl.style.paddingLeft = "9px";
            	pvpl.htmlFor = "pivot_checkbox_restoreview";
            	exportXLSButton.appendChild(pvpl);
			}
			if(someExport) {
				if(!gx.util.browser.isIE()) {
					content.appendChild(OAT.Dom.create("br"));
				}
			}
			content.appendChild(exportXLSButton);
			someExport = true;
		}
	}

    		
    this.countTotals = function() { /* totals */
        self.rowTotals = [[],[]];
        self.colTotals = [[],[]];
        self.gTotal = [];
        for (var i = 0; i < self.w; i++) { self.colTotals[0].push([]); self.colTotals[1].push([]);}
        for (var i = 0; i < self.h; i++) { self.rowTotals[0].push([]); self.rowTotals[1].push([]);}
		
		var colTotalsWithNun = [];
        for (var i = 0; i < self.w; i++) {
            for (var j = 0; j < self.h; j++) {
                var val = self.tabularData[i][j][0];
                if ((val != self.EmptyValue) && (val != "#NuN#")){
                	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                		self.colTotals[0][i].push(self.tabularData[i][j][2][0])
                	} else {	
                		self.colTotals[0][i].push(val);
                	}
                	self.colTotals[1][i].push(self.tabularData[i][j][1]);
                	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                		self.rowTotals[0][j].push(self.tabularData[i][j][2][0]);
                	} else {
                		self.rowTotals[0][j].push(val);
                	}
                	self.rowTotals[1][j].push(self.tabularData[i][j][1]);
                	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                		self.gTotal.push(self.tabularData[i][j][2][0]);
                	} else {	
                		self.gTotal.push(val);
                	}
                } else if (val == "#NuN#"){
                	colTotalsWithNun[i] = true;
                }
            }
        }

        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        for (var i = 0; i < self.rowTotals[0].length; i++) { 
        	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
        		self.rowTotals[0][i] = self.calculateFormulaTotal(self.rowTotals[0][i], measures.length-1, "MesaureAsColumn");
        	} else {
        		self.rowTotals[0][i] = func(self.rowTotals[0][i]);
        	}
        }
        for (var i = 0; i < self.colTotals[0].length; i++) {
        	if ((self.colTotals[0][i].length == 0) && (colTotalsWithNun[i])) self.colTotals[0][i] = "#NuN#" 
        	else {
        		if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
        			self.colTotals[0][i] = self.calculateFormulaTotal(self.colTotals[0][i], measures.length-1, "MesaureAsColumn")
        		} else { 
        			self.colTotals[0][i] = func(self.colTotals[0][i]);
        		} 
        	}
        }
        	
        
        if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
        	self.gTotal =  self.calculateFormulaTotal(self.gTotal, measures.length-1, "MesaureAsColumn")
        } else {
        	self.gTotal = func(self.gTotal);
        }
    }

    this.countSubTotals = function() { /* sub-totals */
        function clean(ptrArray, count) {
            for (var i = 0; i < ptrArray.length - 1; i++) {
                var stack = ptrArray[i];
                for (var j = 0; j < stack.length; j++) {
                    stack[j].totals = [];
                    stack[j].filtrows = [];
                    for (var k = 0; k < count; k++) { stack[j].totals.push([]); stack[j].filtrows.push([]);}
                }
            }
        }
        clean(self.colPointers, self.h);
        clean(self.rowPointers, self.w);

        function addTotal(arr, arrIndex, totalIndex, value, filtrows) {
            if (!arr.length) { return; }
            var item = arr[arr.length - 1][arrIndex].parent;
            while (item.parent) {
            	item.totals[totalIndex].push(value);
               	item.filtrows[totalIndex].push(filtrows);
                item = item.parent;
            }
        }
        for (var i = 0; i < self.w; i++) {
            for (var j = 0; j < self.h; j++) {
                var val = self.tabularData[i][j][0];
                var filtrows = self.tabularData[i][j][1];
                if (val != self.EmptyValue){
                	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                		addTotal(self.colPointers, i, j, self.tabularData[i][j][2][0], filtrows);
                		addTotal(self.rowPointers, j, i, self.tabularData[i][j][2][0], filtrows);
                	} else {
                		addTotal(self.colPointers, i, j, val, filtrows);
                		addTotal(self.rowPointers, j, i, val, filtrows);
                	}
                }
            }
        }

        function apply(ptrArray, func) {
            for (var i = 0; i < ptrArray.length - 1; i++) {
                var stack = ptrArray[i];
                for (var j = 0; j < stack.length; j++) {
                    var totals = stack[j].totals;
                    var filtrows = stack[j].filtrows;
                    for (var k = 0; k < totals.length; k++) {
                       if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                       		var addValue = self.calculateFormulaTotal(totals[k], measures.length-1, "MesaureAsColumn")
                       		totals[k] = { array: totals[k], value: addValue, rows: filtrows[k] };	
                       } else {
                       		totals[k] = { array: totals[k], value: func(totals[k]), rows: filtrows[k] };
                       }
                    }
                }
            }
        }
        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        apply(self.colPointers, func);
        apply(self.rowPointers, func);
    }

    this.countPointers = function() { /* create arrays of pointers to levels of agg structures */
        function count(struct, arr, propName) {
				if(!self.ShowMeasuresAsRows) {
					self[propName] = [];
					var stack = [struct];
					for(var i = 0; i < arr.length; i++) {
						var newstack = [];
						for(var j = 0; j < stack.length; j++) {
							var item = stack[j];
							for(var k = 0; k < item.items.length; k++) {
								newstack.push(item.items[k]);
							}
						}
						stack = newstack;
						self[propName].push(stack.copy());
					}
				} else {
					self[propName] = [];
					var stack = [struct];
					var dim = arr.length - (measures.length - 1) + 1
					if (propName == "colPointers"){
						dim = arr.length
					}
					for(var i = 0; i < dim; i++) { 
						var newstack = [];
						for(var j = 0; j < stack.length; j++) {
							var item = stack[j];
							for(var k = 0; k < item.items.length; k++) {
								newstack.push(item.items[k]);
							}
						}
						stack = newstack;
						self[propName].push(stack.copy());
					}
				}
        }

        count(self.rowStructure, self.rowConditions, "rowPointers");
        count(self.colStructure, self.colConditions, "colPointers");
    }

    this.countOffsets = function() { /* starting offsets for aggregate structures */
        function count(ptrArray) {
            for (var i = 0; i < ptrArray.length; i++) {
                var stack = ptrArray[i];
                var counter = 0;
                for (var j = 0; j < stack.length; j++) {
                    var item = stack[j];
                    item.offset = counter;
                    counter += item.spanData;
                }
            }
        }

        count(self.rowPointers);
        count(self.colPointers);
    }

    this.count = function() { /* create tabularData from filteredData Sets the span of ths */
        /* compute spans = table dimensions */
        function spans(ptr, arr, isForRow, actualDepth, maxDepth) { /* return span for a given aggregate pointer */
            var s = 0;
            var sD = 0; var draw = 0;
            if (!ptr.items){
                ptr.span = 1;
                ptr.spanData = 1;
                ptr.spanDraw = 1;
                return [ptr.span, ptr.spanData, ptr.spanDraw];
            }
            
            for (var i = 0; i < ptr.items.length; i++) {
                var tmp = spans(ptr.items[i], arr, isForRow, actualDepth+1, maxDepth);
                s += tmp[0];
                sD += tmp[1];
                if ((!isForRow) || (actualDepth < maxDepth)){
                	draw += tmp[2];
                }
            }
             
            
            ptr.span = s;
            ptr.spanData = sD;
            ptr.spanDraw = draw;
            if (ptr.items.length && ptr.items[0].items) {
            	if (!self.ShowMeasuresAsRows){
                	var cond = self.conditions[arr[ptr.items[0].depth]];
                	if ((cond.subtotals) && ((!self.autoPaging) || (self.colConditions.length == 0))) { // aument span value for subtotal rows
                	
                		var punt = ptr.items;
                		var prof = 0;
                		if (ptr.items.length > 0){
                			while (punt != false) //calc depth 
                			{
                				prof++;
                				punt = punt[0].items;
                			}   
                		}
                		for(var hi=0; hi < ptr.items.length; hi++){
                			var cantmeasures = measures.length;
                			if ((ptr.items[hi].items != null) && ( (prof - cantmeasures > 0) ) ){ //((columns.length < 3) && (ptr.items[hi].items.length > 1)) || 
                				//if depth of subitem (that is the next dimension columns) if mayor than 1 (there are another column --a dimension?)then an aditional row is needed for the sum of the next dimension
                				ptr.span = ptr.span + 1;
                				ptr.spanDraw = ptr.spanDraw + 1; 
                			}
                		}
                	}
                } 
            }
            if ((!isForRow) || (actualDepth < maxDepth)){
            } else {
            	ptr.spanDraw = 1;
            }
            if ((ptr.collapsed!=undefined) && (ptr.collapsed)){
            	ptr.span = 1; ptr.spanDraw = 1;
                //ptr.spanData = 2;
            	return [ptr.span, ptr.spanData, ptr.spanDraw];
            }
            return [ptr.span, ptr.spanData, ptr.spanDraw];
        }
        function spansWhenMeasuresAsRows(ptr, arr, actualDepth, maxDepth) { /* return span for a given aggregate pointer */
            var s = 0;
            var sD = 0;
            if (!ptr.items){
                ptr.span = 1;
                ptr.spanData = 1;
                return [ptr.span, ptr.spanData];
            }
            var ramasSecas = 0;
            for (var i = 0; i < ptr.items.length; i++) {
                var tmp = spansWhenMeasuresAsRows(ptr.items[i], arr, actualDepth + 1, maxDepth);
                s += tmp[0];
                sD += tmp[1];
                if (tmp[0]==0) ramasSecas=ramasSecas+1;
            }
            ptr.span = s;
            ptr.spanData = sD;
            if ((actualDepth != -1) && (actualDepth < maxDepth)){
            		var cond = self.conditions[arr[actualDepth+1]];
                	if (cond.subtotals) { // aument span value for subtotal rows
                		//for(var hi=0; hi < ptr.items.length; hi++){
                			ptr.span = ptr.span + measures.length*(ptr.items.length-ramasSecas); 
                		//}
                	}
             
            }
            if ((ptr.collapsed!=undefined) && (ptr.collapsed)){
            	ptr.span = 1;
                //ptr.spanData = 2;
            	return [ptr.span, ptr.spanData];
            }
            return [ptr.span, ptr.spanData];
        }
        
        if (!self.ShowMeasuresAsRows){
        	var maxDepth = self.rowConditions.length - (measures.length - 1) - 1
        	var initDepth = -1
        	spans(self.rowStructure, self.rowConditions, true, initDepth, maxDepth);
        	spans(self.colStructure, self.colConditions, false);
        } else {
        	var maxDepth = self.rowConditions.length - (measures.length - 1) - 2
        	var initDepth = -1
        	spansWhenMeasuresAsRows(self.rowStructure, self.rowConditions, initDepth, maxDepth);
        	spansWhenMeasuresAsRows(self.colStructure, self.colConditions, 0, 0);
        }

        self.countPointers();
        self.countOffsets();

		 
        /* create blank table */
        self.tabularData = [];
        var filterDataId = []; var formulaData = []
        self.w = 1;
        self.h = 1;
        if (self.colConditions.length) { self.w = self.colPointers[self.colPointers.length - 1].length; }
        if (self.rowConditions.length) { self.h = self.rowPointers[self.rowPointers.length - 1].length; }
		
		if (!self.ShowMeasuresAsRows){
			
        for (var i = 0; i < self.w; i++) {
            var col = new Array(self.h);
            var fil = new Array(self.h); var fla = new Array(self.h); 
            for (var j = 0; j < self.h; j++) { col[j] = []; fil[j] = []; fla[j] = []}
            self.tabularData.push(col);
            filterDataId.push(fil); formulaData.push(fla);
        }

        function coords(struct, arr, row) {
            var pos = 0;
            var ptr = struct;
            for (var i = 0; i < arr.length; i++) {
                var rindex = arr[i];
                var value = row[rindex];
                var o = false;
                for (var j = 0; j < ptr.items.length; j++) {
                    if (ptr.items[j].value != value) {
                        pos += ptr.items[j].spanData;
                    } else {
                        o = ptr.items[j];
                        break;
                    }
                }
                if (!self.autoPaging){
                	
                	if (o) 
                	ptr = o;
                } else {
                	if (o) { ptr = o; }
                	else { return -1; }
                }
            } /* for all conditions */
            return pos;
        }
		
		function coordsY(struct, arr, row) {
            var pos = 0;
            var ptr = struct;
            var lessArr = [];
            for(var i = 0; i < (self.rowConditions.length - measures.length + 1); i++){
            	lessArr.push(arr[i])
            }
            
            for (var i = 0; i < lessArr.length; i++) {
                var rindex = lessArr[i];
                var value = row[rindex];
                var o = false;
                for (var j = 0; j < ptr.items.length; j++) {
                    if (ptr.items[j].value != value) {
                        pos += ptr.items[j].spanData;
                    } else {
                        o = ptr.items[j];
                        break;
                    }
                }
                if (!self.autoPaging){
                	
                	if (o) 
                	ptr = o;
                } else {
                	if (o) { ptr = o; }
                	else { return -1; }
                }
            } /* for all conditions */
            return pos;
        }

		
        for (var i = 0; i < self.filteredData.length; i++) { /* reposition value array to grid */
            var row = self.filteredData[i];
            var x = coords(self.colStructure, self.colConditions, row);
            //C1Line
            var y = coordsY(self.rowStructure, self.rowConditions, row);
            //var y = coords(self.rowStructure, self.rowConditions, row);
            var val = row[self.dataColumnIndex];
            val = val.toString();
            val = val.replace(/,/g, '.');
            val = val.replace(/%/g, '');
            val = val.replace(/ /g, '');
            if (val != "#NuN#"){
            	val = parseFloat(val); 
           		if (isNaN(val)) { val = 0; }
            }
            if (!self.autoPaging){
            	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
            		var formval = self.getFormulaRowByDataRow(row, measures.length-1, "grandTotal-Tab")
            		if (val == 0){
            			self.tabularData[x][y].push(val);
            			self.tabularData[x][y] = self.calculateFormulaTotal([formval], measures.length-1, "MesaureAsColumn")
            		} else {
            			self.tabularData[x][y].push(self.calculateFormulaTotal([formval], measures.length-1, "MesaureAsColumn"));
            		}
            		formulaData[x][y].push(formval);
            	} else {
            		self.tabularData[x][y].push(val);
            	}
            	filterDataId[x][y].push(i);
            } else {
            	if ( (x!=-1) && (y!=-1) ) {
            		self.tabularData[x][y].push(val);
            		filterDataId[x][y].push(i);
            		if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
            			var formval = self.getFormulaRowByDataRow(row, measures.length-1, "grandTotal-Tab")
            			formulaData[x][y].push(formval);
            		}
            	}
            }
        }
        var func = OAT.Statistics[OAT.Statistics.list[self.options.agg].func]; /* statistics */
        for (var i = 0; i < self.w; i++) {
            for (var j = 0; j < self.h; j++) {
            		var cellNotFill = (self.tabularData[i][j].length == 0)
            		var isNuN = ((self.tabularData[i][j].length == 1) && (self.tabularData[i][j][0] == "#NuN#"))
            		var result = parseFloat(func(self.tabularData[i][j]));
            		if ((!cellNotFill) && (!isNuN))//(result != 0)
                		self.tabularData[i][j] = [result, filterDataId[i][j]];
                	else if (!isNuN) 
                		self.tabularData[i][j] = [self.EmptyValue, filterDataId[i][j]]; /* this 0 is for non value, but if use for TOTAL and subTotal purposes*/
                	else { 	
                		self.tabularData[i][j] = ["#NuN#", filterDataId[i][j]];
                	}	
                	if ((measures.length > 0) && (self.formulaInfo.measureFormula[measures.length-1].hasFormula)){
                		if ((!cellNotFill) && (!isNuN)){
                			self.tabularData[i][j] = [result, filterDataId[i][j]];
                		}
                		self.tabularData[i][j].push(formulaData[i][j])
                	}	
            }
        }

        self.options.subtotals = 0;
        for (var i = 0; i < self.conditions.length; i++) {
            var cond = self.conditions[i];
            if (cond.subtotals) { self.options.subtotals = true; }
        }
        /*if (self.options.subtotals) {*/ self.countSubTotals(); /*}*/
        if (self.options.totals) { self.countTotals(); }
        
        }
    } /* Pivot::count() */

    this._drawGTotal = function(tr, grandTotals) {
    	var td = OAT.Dom.create("td", {}, "gtotal");
    	if (self.gTotal != NaN){
    		
    		if ((self.autoPaging) && (self.FilterByTopFilter)){
    			var grandLastMeasureTotal = calculateGrandTotalForLastMeasure(self);
    			td.innerHTML = self.defaultPictureValue(grandLastMeasureTotal.toString(), true, 0);
        		td = self.applyConditionalFormats(td, grandLastMeasureTotal.toString(), true, 0);
        		self.setClickEventHandlers(td, grandLastMeasureTotal.toString(), "MEASURE", measures.length - 1, 'GrandTotal');
    		} else if ((!self.autoPaging) || (self.FilterByTopFilter)){
    			if (isNaN(self.gTotal)) { self.gTotal = 0; }
    			td.innerHTML = self.defaultPictureValue(self.gTotal.toString(), true, 0);
        		td = self.applyConditionalFormats(td, self.gTotal.toString(), true, 0);
        		self.setClickEventHandlers(td, self.gTotal.toString(), "MEASURE", measures.length - 1, 'GrandTotal');	
    		} else {
    			var resSum = grandTotals;//sumGrandPagingTotals(this.GeneralDataRows, self.conditions, measures.length, self.formulaInfo, self);
    			td.innerHTML = self.defaultPictureValue( resSum[ resSum.length - 1 ].toString(), true, 0);
        		td = self.applyConditionalFormats(td, resSum[ resSum.length - 1 ].toString(), true, 0);
        		self.setClickEventHandlers(td, resSum[ resSum.length - 1 ].toString(), "MEASURE", measures.length - 1, 'GrandTotal'); 	
    		}
        } else {
			 td.innerHTML = "-"      	
        }
        tr.appendChild(td);
    }

    this._drawRowTotals = function(tr, GrandTotals, partialGrandtotalsmeasuresList, columnsDataHide) { //for Grand Total only
		var granSumaLateral = []; for(var iGSL = 0; iGSL < measures.length-1; iGSL++){ granSumaLateral[iGSL] = [];}
        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        if (self.colConditions.length) {
        	for (var i = 0; i < self.w; i++) {
        		var filteredRowNumber = -1;
        		if (self.colConditions.length > 0){
        			for (var fg=0; fg < measures.length -1; fg++)
           			{
           				var td = OAT.Dom.create("td", {}, "total");
           				for (var ti = 0; ti < partialGrandtotalsmeasuresList.length; ti++){
           					if (partialGrandtotalsmeasuresList[ti][2] === i) /*if is the record for this row*/
           					{
           						if (func(partialGrandtotalsmeasuresList[ti][0][fg]) != NaN){ /*partial grand total, the sum of all values from the above row*/
           							td.innerHTML = self.defaultPictureValue(func(partialGrandtotalsmeasuresList[ti][0][fg]).toString(), false, fg);//partialGrandtotalsmeasuresList[ti][1]);
            	        			td = self.applyConditionalFormats(td, func(partialGrandtotalsmeasuresList[ti][0][fg]).toString(), false, fg);//partialGrandtotalsmeasuresList[ti][1]);
            	        			self.setClickEventHandlers(td, func(partialGrandtotalsmeasuresList[ti][0][fg]).toString(), "MEASURE", fg, ['PartialGrandTotal', partialGrandtotalsmeasuresList[ti][3]] );
            	        			filteredRowNumber = ti;	
            	        			granSumaLateral[fg].push(func(partialGrandtotalsmeasuresList[ti][0][fg]));
            	        		} else {
            	        			td.innerHTML = "-"
            	        		} 
           					}
           				}
           				if (columnsDataHide.indexOf(i)==-1){ 
           					tr.appendChild(td);
           				}
           			}
            	}
           	
            	var td = OAT.Dom.create("td", {}, "total");
            	if (!isNaN(self.colTotals[0][i]) || self.colTotals[0][i] == "#NuN#" || self.colTotals[0][i] == "#FoE#"){
            		td.innerHTML = self.defaultPictureValue(self.colTotals[0][i].toString(), true, 0);
            		td = self.applyConditionalFormats(td, self.colTotals[0][i].toString(), true, 0);
            		if (filteredRowNumber != -1){
            			self.setClickEventHandlers(td, self.colTotals[0][i].toString(), "MEASURE", measures.length - 1, ['PartialGrandTotal', partialGrandtotalsmeasuresList[filteredRowNumber][3]] );
            		} else {
            			self.setClickEventHandlers(td, self.colTotals[0][i].toString(), "MEASURE", measures.length - 1, ['PtrTotals', self.colTotals[1][i]] );
            		}
            	} else {
            		td.innerHTML = ""
            	}
            	if (columnsDataHide.indexOf(i)==-1){
            		tr.appendChild(td);
            	}
            	if (!self.colPointers.length) { continue; }
            	var item = self.colPointers[self.colPointers.length - 1][i].parent;
            	while (item.parent) {
            	    var cond = self.conditions[self.colConditions[item.depth]];
            	    if (cond.subtotals && item.offset + item.spanData - 1 == i) {
            	        var td = OAT.Dom.create("td", {}, "total");
            	        var tmp = [];
            	        for (var l = 0; l < item.totals.length; l++) { tmp.append(item.totals[l].array); }
            	        
            	        if (func(tmp) != NaN){
            	        	td.innerHTML = self.defaultPictureValue(func(tmp).toString(), true, 0);
            	        	td = self.applyConditionalFormats(td, func(tmp).toString(), true, 0);
            	        	self.setClickEventHandlers(td, func(tmp).toString(), "MEASURE", measures.length - 1, 'GandTotal'); 
            	        } else {
            	        	td.innerHTML = "-"
            	        }
            	        tr.appendChild(td);
            	    } /* irregular subtotal */
            	    item = item.parent;
            	}
        	}
        }
        
     
        	/* draw grand total for several values*/
       		if (self.colConditions.length > 0){
       			for(var ind = 0; ind < measures.length-1; ind++){
       				var td = OAT.Dom.create("td", {}, "gtotal");  
       				td.innerHTML = self.defaultPictureValue(func(granSumaLateral[ind]).toString(), false, ind); //here the grand total for not the last measures
       				td = self.applyConditionalFormats(td, func(granSumaLateral[ind]).toString(), false, ind);   //
       				self.setClickEventHandlers(td, func(granSumaLateral[ind]).toString(), "MEASURE", ind, 'GrandTotal');
       				tr.appendChild(td);
       			}
       		}
        
       		self._drawGTotal(tr, GrandTotals);
        //}
    }

    this._drawRowSubtotals = function(tr, i, ptr, subtotalsmeasuresList, td_temp_forCollapseInfo, itemColumnNumber) { /* subtotals for i-th row */
        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        var partialSubtotals = []; for (var iPS = 0; iPS < measures.length - 1; iPS++){ partialSubtotals[iPS] = []} //para agregar los valores totales de las ultimas columnas
        for (var k = 0; k < self.w; k++) {
            var td = OAT.Dom.create("td", {}, "subtotal");
            if (ptr.totals[k].array.length > 0) {				/* sumo valores para filas de subtotales solo si hay valores para sumar si no, se muestran espacios en blanco*/
           		if (self.colConditions.length > 0) {
        			for (var fg=0; fg < measures.length -1; fg++)
           			{
           				var td2 = OAT.Dom.create("td", {}, "total");
						
           				if (subtotalsmeasuresList[0] != undefined){
           					for( var colP = 0; colP < subtotalsmeasuresList.length; colP++){
           						if (subtotalsmeasuresList[colP][2]==(k)){//es la columna que corresponde
           							var tempValues = []
           							for(var iSML = 0; iSML < subtotalsmeasuresList[colP][0][fg].length; iSML++){
           									if (itemColumnNumber < subtotalsmeasuresList[colP][0][fg][iSML][1]){
           										tempValues.push(subtotalsmeasuresList[colP][0][fg][iSML][0])
           										subtotalsmeasuresList[colP][0][fg][iSML][1] = itemColumnNumber
           									} 
           							}
           							if ( (func( tempValues ) != NaN)){
           								td2.innerHTML = self.defaultPictureValue( func( tempValues ).toString(), false, fg);//subtotalsmeasuresList[0][1]);
           								td2 = self.applyConditionalFormats(td2, func( tempValues ).toString(), false, fg);//subtotalsmeasuresList[0][1]);
           								self.setClickEventHandlers(td2, func( tempValues ).toString(), "MEASURE", fg , [ 'PartialGrandTotal', subtotalsmeasuresList[0][3]]);
           								partialSubtotals[fg].push(func(tempValues));
           							} else {
           								td2.innerHTML = "";
           							}			
           							
           						}
           					}
           				}
           				//if (subtotalsmeasuresList[0] != undefined) {
           					//subtotalsmeasuresList.splice(0,1);
           				//}
           				tr.appendChild(td2);
           			}
            	 }
            	td.innerHTML  = self.defaultPictureValue(ptr.totals[k].value.toString(), true, 0);
            	if (td_temp_forCollapseInfo){
            		td_temp_forCollapseInfo.innerHTML = self.defaultPictureValue(ptr.totals[k].value.toString(), true, 0);
            	}
            	td = self.applyConditionalFormats(td, ptr.totals[k].value.toString(), true, 0);
            	self.setClickEventHandlers(td, ptr.totals[k].value.toString(), "MEASURE", measures.length - 1, ['PtrTotals', ptr.totals[k].rows]); //ptr.items has the relation of the filteredRows from this subtotal
            } else {
            	 if (self.colConditions.length > 0) {
        			for (var fg=0; fg < measures.length -1; fg++)
           			{
           				var td2 = OAT.Dom.create("td", {}, "total");
           				td2.innerHTML = "";
           				tr.appendChild(td2);
           			}
            	 }
            	 td.innerHTML  = "";
            }
            
            tr.appendChild(td);
            if (!self.colPointers.length) { continue; }
            var item = self.colPointers[self.colPointers.length - 1][k].parent;
           
            while (item.parent) {
            	var cond = self.conditions[self.colConditions[item.depth]];
                if (cond.subtotals && item.offset + item.spanData - 1 == k) {
                    var td = OAT.Dom.create("td", {}, "subtotal");
                    tr.appendChild(td);
                    var tmp = [];
                    for (var l = 0; l < ptr.totals.length; l++) {
                        if (l >= item.offset && l < item.spanData + item.offset) { tmp.append(ptr.totals[l].array); }
                    } /* for all possible totals of this row */
					
					if ( func(tmp) != NaN){ 
                    	td.innerHTML = self.defaultPictureValue(func(tmp).toString(), true, 0);
                    	td = self.applyConditionalFormats(td, func(tmp).toString(), true, 0);
                    	self.setClickEventHandlers(td, func(tmp).toString(), "MEASURE", measures.length - 1, '');
                  	} else {
                  		td.innerHTML = "-";
                  	}
                } /* irregular subtotal */
                item = item.parent;
            }
        } /* for all regular subtotals */
       
        /* here the value for the latest columns of a subtotal row*/
        if (self.options.totals && self.colConditions.length) {
        	
        	if (self.colConditions.length > 0){
        		var td = OAT.Dom.create("td", {}, "total"); /* here the first measures (as rowConditions) */
           		for (var ind = 0; ind < measures.length - 1; ind++) {
           			
           				var td = OAT.Dom.create("td", {}, "total");
           				var tmp = partialSubtotals[ind];
           				if ( func(tmp) != NaN ){
            				td.innerHTML = self.defaultPictureValue( func(tmp).toString(), false, ind);
            				td = self.applyConditionalFormats(td, func(tmp).toString(), false, ind);
            				//sin clickEventHandler
            			} else {
            				td.innerHTML = "-"
            			}
           				tr.appendChild(td);
           			
					//self._drawRowSubtotalsForMeasures(tr, i, ptr, ind, -1);
				}
           	}
        	
            var tmp = [];
            for (var l = 0; l < ptr.totals.length; l++) { tmp.append(ptr.totals[l].array); }
            var td = OAT.Dom.create("td", {}, "total");
            
            if (self.formulaInfo.measureFormula[measures.length-1].hasFormula){
            	var totals_ = self.calculateFormulaTotal(tmp, measures.length-1, "MeasureInRows");
            	if ((totals_ != NaN) && (!isNaN(totals_))){
            		td.innerHTML = self.defaultPictureValue( totals_.toString(), true, 0);
            		td = self.applyConditionalFormats(td, totals_.toString(), true, 0);
            		self.setClickEventHandlers(td, totals_.toString(), "MEASURE", measures.length - 1, '');
            	} else {
            		td.innerHTML = "-"
            	}
            } else {
            	if ( func(tmp) != NaN ){
            		td.innerHTML = self.defaultPictureValue( func(tmp).toString(), true, 0);
            		td = self.applyConditionalFormats(td, func(tmp).toString(), true, 0);
            		self.setClickEventHandlers(td, func(tmp).toString(), "MEASURE", measures.length - 1, '');
            	} else {
            		td.innerHTML = "-"
            	}
            }
            tr.appendChild(td);
        }
    }
    
    this._drawRowSubtotalsAutoPaging = function(tr, subtotals) { /* subtotals for i-th row */
        for (var fg=0; fg < subtotals.length; fg++)
        {
        	var lastMeasure = (fg==subtotals.length-1)?true:false;
        	var td2 = OAT.Dom.create("td", {}, "total");
			if ( subtotals[fg] != NaN){
           		td2.innerHTML = self.defaultPictureValue( subtotals[fg].toString() , lastMeasure, fg);
           		td2 = self.applyConditionalFormats(td2, subtotals[fg].toString(), lastMeasure, fg);
           		self.setClickEventHandlers(td2, subtotals[fg].toString(), "MEASURE", fg, '');
           	} else {
           		td2.innerHTML = " ";
           	}
         	tr.appendChild(td2);
        }
            	
    }
    
    this._sumValuesInDept = function(item, dept, array){
    	if (dept === 0){
    		var items  = item.items;
    		var totals_ = 0;
    		if (items != false){
    			for(var iv = 0; iv < items.length; iv++) {
					totals_ = totals_ + parseFloat(items[iv].value);
					array.append(parseFloat(items[iv].value));
				}
			} else {
				array.append(parseFloat(item.value));
				return parseFloat(item.value);
			}
			return totals_
    	} else {
    		var subitems  = item.items;
    		var subtotals_ = 0;
    		for(var iv = 0; iv < subitems.length; iv++) {
    			subtotals_ = subtotals_ + self._sumValuesInDept(subitems[iv], dept-1, array);
    		}
    		return subtotals_
    	}
    }
    
    this._sumValuesInDept = function(item, dept, array){
    	if (dept === 0){
    		var items  = item.items;
    		var totals_ = 0;
    		if (items != false){
    			for(var iv = 0; iv < items.length; iv++) {
					totals_ = totals_ + parseFloat(items[iv].value);
					if (items[iv].value == "#NuN#"){
						array.append("#NuN#");
					} else {
						array.append(parseFloat(items[iv].value));
					}
				}
			} else {
				array.append(parseFloat(item.value));
				return parseFloat(item.value);
			}
			return totals_
    	} else {
    		var subitems  = item.items;
    		var subtotals_ = 0;
    		for(var iv = 0; iv < subitems.length; iv++) {
    			subtotals_ = subtotals_ + self._sumValuesInDept(subitems[iv], dept-1, array);
    		}
    		return subtotals_
    	}
    }
    
     this._sumValuesInDeptFormula = function(item, dept, array, dimNum){
    	if (dept === 0){
    		var items  = item.items;
    		if (items != false){
    			for(var iv = 0; iv < items.length; iv++) {
    				var ref = items[iv];
    				for (var l = 0; l < dimNum; l++){
    					ref = ref.parent
    				}
    				var addRow = self.getFormulaRowByCoord(ref, [], dimNum, "")
					array.append([addRow]);
				}
			} else {
				var addRow = self.getFormulaRowByCoord(items[iv], [], dimNum, "")
				array.append([addRow]);
				return 
			}
			return 
    	} else {
    		var subitems  = item.items;
    		var subtotals_ = 0;
    		for(var iv = 0; iv < subitems.length; iv++) {
    			self._sumValuesInDeptFormula(subitems[iv], dept-1, array, dimNum);
    		}
    		return
    	}
    }
    
    this._drawRowSubtotalsForMeasures = function(tr, i, ptr, dept, dimNum, th , td_collection_forCollapseInfo) { // subtotals for i-th row Idem al anterior pero calcula medidas para mesaures
            
            var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; // statistics 
            var td = OAT.Dom.create("td", {}, "subtotal");
            var _measureNumber = dept;
            var cantDimensions = self.rowConditions.length - (measures.length - 1); //esta es la cantidad de dimensiones, es decir columnas iniciales que no son valores
           
            if ((cantDimensions >= 3) && (dimNum != -1)){
            	dept = (cantDimensions - dimNum) + dept -1;
            }else{
            	dept = cantDimensions - 1 + dept; //"quito" el primer y sumo profundidad de esta mesearue
            }
            
            var tmp = [];
            if (self.formulaInfo.measureFormula[_measureNumber].hasFormula){
            	var totals_ = self._sumValuesInDeptFormula(ptr, dept, tmp, _measureNumber);
            } else {
            	var totals_ = self._sumValuesInDept(ptr, dept, tmp);
            }	 
            var y;
            if (tmp.length > 0) {
            	if (self.formulaInfo.measureFormula[_measureNumber].hasFormula){
            		var res = self.calculateFormulaTotal(tmp, _measureNumber, "MeasureInRows")
            		if (res != NaN){
            			td.innerHTML = self.defaultPictureValue(res.toString(), false, _measureNumber); //before: dept
            			td = self.applyConditionalFormats(td, res.toString(), false, _measureNumber); //before: dept
            			self.setClickEventHandlers(td, res.toString(), "MEASURE", _measureNumber, ['PtrTotals', ptr.filtrows]);
            			if (th != undefined){
            				self.setClickEventHandlers(th, th.innerHTML, "DIMENSION", _measureNumber, ['PtrTotals', ptr.filtrows]);
            			}
            		} else {
            			td.innerHTML = ""; 
            		}
            	} else {
            		if (func(tmp) != NaN){
            			td.innerHTML = self.defaultPictureValue(func(tmp).toString(), false, _measureNumber); //before: dept
            			td = self.applyConditionalFormats(td, func(tmp).toString(), false, _measureNumber); //before: dept
            			self.setClickEventHandlers(td, func(tmp).toString(), "MEASURE", _measureNumber, ['PtrTotals', ptr.filtrows]);
            			if (th != undefined){
            				self.setClickEventHandlers(th, th.innerHTML, "DIMENSION", _measureNumber, ['PtrTotals', ptr.filtrows]);
            			}
            		} else {
            			td.innerHTML = "";
            		}
            	}
            } else {
            	td.innerHTML = ""
            }
            
            if ((td_collection_forCollapseInfo != undefined) && (td_collection_forCollapseInfo[_measureNumber])){
            	td_collection_forCollapseInfo[_measureNumber].innerHTML = td.innerHTML
            	td_collection_forCollapseInfo[_measureNumber] = false
            }
            
            tr.appendChild(td);
    }
    
    
    this._drawCorner = function(th, target) {
    	if (measures.length > 0){
    		th.innerHTML = self.headerRow[self.dataColumnIndex];	
        	th.className = "h2titlewhite";
        	this.setTitleTexrtAlign(th, th.innerHTML);
        	self.setClickEventHandlers(th, self.headerRow[self.dataColumnIndex], "MEASURE", self.dataColumnIndex - columns.length, 'GrandTotal');
        }
    }
    
    this._drawCornerCustom = function(th, target) {
        th.innerHTML = ""; //self.headerRow[self.dataColumnIndex];
        th.className = "h2titlewhite";
        this.setTitleTexrtAlign(th, th.innerHTML);
    }
	
	this._drawRowConditionsHeadingsCustom = function(tr) {
        /* rowConditions headings */
        //var tr = OAT.Dom.create("tr");
        for (var j = 0; j < self.rowConditions.length; j++) {
            var cond = self.conditions[self.rowConditions[j]];
            if (self.isMeasureByName( self.headerRow[self.rowConditions[j]] )){//(measures[0].attributes.getNamedItem("displayName").nodeValue == self.headerRow[self.rowConditions[j]]) { 
                if (!self.ShowMeasuresAsRows){
                	var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2titlewhite"); //custom class for 44px height
                	var div = OAT.Dom.create("div");
                	div.innerHTML = self.headerRow[self.rowConditions[j]];
                	this.setTitleTexrtAlign(div, self.headerRow[self.rowConditions[j]]);  
                	th.rowSpan = self.colConditions.length + 2; //2
                }
            } else {
                var th = OAT.Dom.create("th", { cursor: "pointer", height: "25px" }, "h2title");
                var divCont = OAT.Dom.create("div", {position:"relative"});
                 
                var div = OAT.Dom.create("div", {overflow:"hidden"});
                if (!self.serverPagination){
                	div.innerHTML = self.headerRow[self.rowConditions[j]]  + "&nbsp&nbsp&nbsp";
                } else {
                	div.innerHTML = self.headerRow[self.rowConditions[j]].replace(/ /g,"&nbsp;")  + "&nbsp&nbsp&nbsp&nbsp";
                }
                if ((self.GeneralDataRows.length > 0) || (self.serverPagination)){
                	var resp = self.getClickReference(cond, self.rowConditions[j], div);
                	var ref = resp[0]
                	var anchorRef = resp[1] 
                	OAT.Dom.attach(th, "click", ref);
                	var callback = self.getOrderReference(self.rowConditions[j], anchorRef, ref, div);
                	self.gd.addSource(div, self.process, callback);
                	self.gd.addTarget(th);
                }
                th.conditionIndex = self.rowConditions[j];
                if (!self.ShowMeasuresAsRows) {
                	th.rowSpan = self.colConditions.length + 2;
                } else {
                	th.rowSpan = self.colConditions.length + 1;
                }
                
                var divImg = OAT.Dom.create("div",{position:"absolute",right:"0px",bottom:"2px",width:"12px",height:"12px"});
				try {
					//if (!self.serverPagination){
						this.updateSortImage(divImg, cond.sort);
					//}
              	} catch (Error) {}
                OAT.Dom.append([th, divCont], [divCont, div], [divCont, divImg], [tr, th]);
            }
        }
        if (self.ShowMeasuresAsRows) //add title of Measure
		{
			var largo = 80;
            var th = OAT.Dom.create("th", { /*cursor: "pointer",*/ height: "25px" }, "h2title");
            var divCont = OAT.Dom.create("div", {position:"relative", width:largo+"px"}); 
            var div = OAT.Dom.create("div", {overflow:"hidden"});
            div.innerHTML = gx.getMessage("GXPL_QViewerJSMeasures") + "&nbsp&nbsp&nbsp"; 
            OAT.Dom.append([th, divCont], [divCont, div], [tr, th]);
            th.rowSpan = self.colConditions.length + 1;
            //th.style.cursor = "pointer";
            OAT.Dom.append([th, div], [tr, th]);
        }   
	}
	
	this.isMeasureByName = function(headerName){
		var ismeasure = false;
		for(var i=0; i < measures.length - 1; i++){
			if (measures[i].attributes.getNamedItem("displayName").nodeValue == headerName){
				ismeasure = true;
		}}
		return ismeasure;
	}
	
	this.setTitleTexrtAlign = function(div, header){
		for(var i = 0; i< measures.length; i++){
			if (measures[i].getAttribute("displayName") === header){
				if ((measures[i].getAttribute("dataType") === "integer") || (measures[i].getAttribute("dataType") === "real")){
					div.style.textAlign = "right"
				}
				if (measures[i].getAttribute("date") === "integer"){
					div.style.textAlign = "left"
				}
			} 
		}
	}
	
	this.updateSortImage = function(div, order) {
		var path = "none";
		switch (order) {
			//case 0: path = "none"; break; //0
			case 0: path = "asc"; break;
			case 1: path = "asc"; break;   //1
			case -1: path = "desc"; break; //-1
			case 2: path = "desc"; break;
		}
		
		var ua = navigator.userAgent.toLowerCase();
		var isAndroid = ua.indexOf("android") > -1;
		if (gx.util.browser.isIE() || (isAndroid)){
        	div.className.replace("sort-none");
        	div.className.replace("sort-asc");
        	div.className.replace("sort-desc");
        } else {
	       	div.classList.remove("sort-none");
	       	div.classList.remove("sort-asc");
	       	div.classList.remove("sort-desc");
	    }
		if (gx.util.browser.isIE() || (isAndroid)){
        	div.className += " sort-"+path;
        } else {
	        div.classList.add("sort-"+path);
	    }
		//div.style.backgroundImage = "url("+OAT.Preferences.imagePath+"Grid_"+path+".gif)";
	}
	
    this._drawRowConditionsHeadings = function(tbody) {
        /* rowConditions headings */
        var tr = OAT.Dom.create("tr");
        //tr.setAttribute("title_row", true);
        for (var j = 0; j < self.rowConditions.length; j++) {
            var cond = self.conditions[self.rowConditions[j]];
            if (self.isMeasureByName( self.headerRow[self.rowConditions[j]] )){//(measures[0].attributes.getNamedItem("displayName").nodeValue == self.headerRow[self.rowConditions[j]]) { 
                if (!self.ShowMeasuresAsRows){
                	var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2titlewhite"); //custom class for 44px height
                	var div = OAT.Dom.create("div");
                	div.innerHTML = self.headerRow[self.rowConditions[j]];
                	self.setTitleTexrtAlign(div, self.headerRow[self.rowConditions[j]]);
                	self.setClickEventHandlers(th, self.headerRow[self.rowConditions[j]], "MEASURE", self.rowConditions[j] - columns.length, 'GrandTotal');
                	OAT.Dom.append([th, div], [tr, th]);
                }
            } else {
            	var largo = (self.headerRow[self.rowConditions[j]].length > 8) ? self.headerRow[self.rowConditions[j]].length * 10 : 80;
                var th = OAT.Dom.create("th", { cursor: "pointer", height: "25px" }, "h2title");
                var divCont = OAT.Dom.create("div", {position:"relative", width:largo+"px"}); 
                var div = OAT.Dom.create("div", {overflow:"hidden"});
                div.innerHTML = self.headerRow[self.rowConditions[j]] + "&nbsp&nbsp&nbsp"; 
                var resp = self.getClickReference(cond, self.rowConditions[j], div);
                var ref = resp[0] 
                var anchorRef = resp[1] 
                OAT.Dom.attach(th, "click", ref);
                var callback = self.getOrderReference(self.rowConditions[j], anchorRef, ref, div);
                self.gd.addSource(div, self.process, callback);
                self.gd.addTarget(th);
                //self.TitleEventManager(th, div, ref, callback);
                th.conditionIndex = self.rowConditions[j];
                
        
				var divImg = OAT.Dom.create("div",{position:"absolute",right:"0px",bottom:"2px",width:"12px",height:"12px"});
				//if (!self.serverPagination){
					if (cond!=undefined){
						this.updateSortImage(divImg, cond.sort);
					} else {
						this.updateSortImage(divImg, 0);
					}			
                //}
                OAT.Dom.append([th, divCont], [divCont, div], [divCont, divImg], [tr, th]);
            }
        }
        
        
        if (self.ShowMeasuresAsRows){
        		//"Measure" Title
        		var largo = 80;
                var th = OAT.Dom.create("th", { /*cursor: "none",*/ height: "25px" }, "h2title");
                var divCont = OAT.Dom.create("div", {position:"relative", width:largo+"px"}); 
                var div = OAT.Dom.create("div", {overflow:"hidden"});
                div.innerHTML = gx.getMessage("GXPL_QViewerJSMeasures") + "&nbsp&nbsp&nbsp"; 
                OAT.Dom.append([th, divCont], [divCont, div], [tr, th]);
                //"Value" Title
                var th = OAT.Dom.create("th", { cursor: "none" }, "h2titlewhite"); //custom class for 44px height
                var div = OAT.Dom.create("div");
                div.innerHTML = gx.getMessage("GXPL_QViewerJSValue");
                th.style.textAlign = "right"
                OAT.Dom.append([th, div], [tr, th]);
        }
        
        	 
        var th = OAT.Dom.create("th"); /* blank space above */
        if (!self.colConditions.length) {
            	self._drawCorner(th, true);
            	th.conditionIndex = -1;
        } else { th.style.border = "none"; }
        if (self.colStructure.span != null){
        	th.colSpan = self.colStructure.span + (self.options.headingBefore ? 1 : 0) + (self.options.totals ? 1 : 0);
        } else {
        	th.colSpan = (self.options.headingBefore ? 1 : 0) + (self.options.totals ? 1 : 0);
        }
        if ((measures.length > 0) && (!self.ShowMeasuresAsRows)){
        	tr.appendChild(th);
        }
        if (self.colConditions.length) { /* blank space after */
            var th = OAT.Dom.create("th", { border: "none", width: "0px;" });
            //th.hidden = true;
            tr.appendChild(th);
        }
        if ((measures.length > 0) && (tr.cells[1] != undefined) && (tr.cells[1].innerHTML == "")) { tr.hidden = true; } //hideemptycolrow
        else {
        	self.appendRowToTable(tbody, tr, true);
        	//tbody.appendChild(tr);
        }
        /////////////////////////////////////////////////// MOVE FILTERS TO TOOLBAR
        if ((measures.length > 0) && (tr.cells[1]!=undefined) && (tr.cells[1].innerHTML == "")) {
            var toolbarTable = document.getElementById(self.controlName + "_" + self.query + "_toolbar");
            toolbarTable.rows[0].appendChild(tr.cells[0]);
        }
        /////////////////////////////////////////////////// MOVE FILTERS TO TOOLBAR
    }
	
	this._drawColConditionsHeadingsCustom = function(tr, i, last) {
        var cond = self.conditions[self.colConditions[i]];
        var th = OAT.Dom.create("th", { cursor: "pointer" }, "h2title");
        var largo = (self.headerRow[self.colConditions[i]].length > 8) ? ((self.headerRow[self.colConditions[i]].length + 3) * 10 ): 80;
        var divCont = OAT.Dom.create("div", {position:"relative", width:largo+"px"});
        if (last)
        {
        	th.style.borderRightColor = "transparent";
        }
        var div = OAT.Dom.create("div", {overflow:"hidden"});
        if (!self.serverPagination){
        	div.innerHTML = self.headerRow[self.colConditions[i]]  + "&nbsp&nbsp";
        } else {
        	div.innerHTML = self.headerRow[self.colConditions[i]].replace(/ /g,"&nbsp") + "&nbsp&nbsp&nbsp";
        }
        if ((self.GeneralDataRows.length > 0) || (self.serverPagination)){
        	var resp = self.getClickReference(cond, self.colConditions[i], div);
        	var ref = resp[0]
        	var anchorRef = resp[1] 
        
        	OAT.Dom.attach(th, "click", ref);
        	var callback = self.getOrderReference(self.colConditions[i], anchorRef, ref, div);
        	self.gd.addSource(div, self.process, callback);
        	self.gd.addTarget(th);
        }
        
        var divImg = OAT.Dom.create("div",{position:"absolute",right:"0px",bottom:"2px",width:"12px",height:"12px"});
		try {
			//if (!self.serverPagination){
				this.updateSortImage(divImg, cond.sort);
			//}
      	} catch (error) {}
        divCont.appendChild(div);
        //if (!self.serverPagination){
        	divCont.appendChild(divImg);
        //}
        
        th.conditionIndex = self.colConditions[i];
        th.appendChild(divCont);
        //th.hidden = true;
        tr.appendChild(th);
        
        return tr;
    }
    
    this._drawColConditionsHeadings = function(tr, i) {
        var cond = self.conditions[self.colConditions[i]];
        var th = OAT.Dom.create("th", { cursor: "pointer" }, "even");
        var div = OAT.Dom.create("div");
        div.innerHTML = self.headerRow[self.colConditions[i]];
        var resp = self.getClickReference(cond, self.colConditions[i], div);
        var ref = resp[0]
        var anchorRef = resp[1] 
        OAT.Dom.attach(th, "click", ref);
        var callback = self.getOrderReference(self.colConditions[i], anchorRef, ref, div);
        self.gd.addSource(div, self.process, callback);
        self.gd.addTarget(th);
        th.conditionIndex = self.colConditions[i];
        th.appendChild(div);
        th.hidden = true;
        //tr.appendChild(th);

        /////////////////////////////////////////////////// MOVE FILTERS TO TOOLBAR
        if (th.textContent == self.headerRow[self.colConditions[i]]) {
            var toolbarTable = document.getElementById(self.controlName + "_" + self.query + "_toolbar");
            th.hidden = false;
            toolbarTable.rows[0].appendChild(th);
        }
        /////////////////////////////////////////////////// MOVE FILTERS TO TOOLBAR
    }

    this.getClassName = function(i, j) { /* decide odd/even class */
       return "even";
    }
    
    this.setStyleValues = function(elem, styleValues){
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
    
    
    this.applyFormatValues = function(th, value, columnNumber){ /* Format for dimensions ("header columns") */
    	//apply default format
    	var defaultFormats = self.columns[columnNumber].getAttribute("format");
    	if ((defaultFormats!=null) && (defaultFormats != "")){
    		th = self.setStyleValues(th, defaultFormats);
    	}
    	//apply format value
    	for(var i = 0; i < self.formatValues.length; i++) {
    		if(self.formatValues[i].columnNumber == columnNumber){ //a format for this column
    			if (self.formatValues[i].value === value){
    				th = self.setStyleValues(th, self.formatValues[i].format);
    			}
    		}
    	}
    	var measureDataType = self.columns[columnNumber].getAttribute("dataType");
    	//apply conditional values
    	var equal = [];
		var notequal = [];
		var greaterThan = [];
		var greaterOrEqual = [];
		var lessThan = [];
		var lessOrEqual = [];
		var greaterOrEqual = [];
		var between = [];
		for(var i = 0; i < self.conditionalFormatsColumns.length; i++) {
			if(self.conditionalFormatsColumns[i].columnNumber == columnNumber) {
				if(self.conditionalFormatsColumns[i].operation1 == "equal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						equal[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						equal[0] = self.conditionalFormatsColumns[i].value1
					}
					equal[1] = self.conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation1 == "notequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						notequal[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						notequal[0] = self.conditionalFormatsColumns[i].value1
					}
					notequal[1] = self.conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation1 == "less") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessThan[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						lessThan[0] = self.conditionalFormatsColumns[i].value1;
					}
					lessThan[1] = self.conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation1 == "greater") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterThan[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						greaterThan[0] = self.conditionalFormatsColumns[i].value1;
					}
					greaterThan[1] = self.conditionalFormatsColumns[i].format;
				}
				if ((self.conditionalFormatsColumns[i].operation1 == "greaterequal") && (self.conditionalFormatsColumns[i].operation2 == undefined)) {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterOrEqual[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						greaterOrEqual[0] = self.conditionalFormatsColumns[i].value1;
					}
					greaterThan[1] = self.conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation1 == "lessequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessOrEqual[0] = parseFloat(self.conditionalFormatsColumns[i].value1);
					} else {
						lessOrEqual[0] = self.conditionalFormatsColumns[i].value1
					}
					lessThan[1] = self.conditionalFormatsColumns[i].format;
				}
				if(self.conditionalFormatsColumns[i].operation2 && self.conditionalFormatsColumns[i].operation1 == "greaterequal") {  //when interval
					//greaterOrEqual = []
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
						between[2] = self.conditionalFormatsColumns[i].format;
					}
				}
			}
		}
		
		var comparisons = new Array(3);
		
		th.style.textAlign = "left";
		if (measureDataType === "real"){
			th.style.textAlign = "left";
			value = parseFloat(value);
		}
		
		if (measureDataType === "integer") {
			
			th.style.textAlign = "right";
			value = parseInt(value);
			
		}
		
		if (measureDataType === "real"){
			th.style.textAlign = "right";
		}
		
		if (measureDataType != "date"){
			if ((equal[0] != undefined) && (value == equal[0])){
				th = self.setStyleValues(th, equal[1]);
			}
			if ((notequal[0] != undefined) && (value != notequal[0])){
				th = self.setStyleValues(th, notequal[1]);
			}
			if ( ((greaterThan[0] != undefined) && (value > greaterThan[0])) || 
					((greaterOrEqual[0] != undefined)	&& (value >= greaterOrEqual[0])) ){                          
				th = self.setStyleValues(th, greaterThan[1]);
			} 
			if ( ((lessThan[0] != undefined) && (value < lessThan[0])) || 
			 ((lessOrEqual[0] != undefined) && (value <= lessOrEqual[0])) ){
				th = self.setStyleValues(th, lessThan[1]);
			} 
			if  ((between[0] != undefined && between[1] != undefined) && (value >= between[0] && value <= between[1])) {
				th = self.setStyleValues(th, between[2]);
			}
			return th;
		}
		
		
		if (measureDataType === "date"){
			th.style.textAlign = "right";
			var dates = value.split("-");
			if ((self.defaultPicture.getAttribute("dateFormat")!= undefined) && (self.defaultPicture.getAttribute("dateFormat")!= null)) {
				picture = self.defaultPicture.getAttribute("dateFormat").split("");
			}
			var dateElements = new Array(3);
			dateElements[0] = parseInt(dates[0]);
			dateElements[1] = parseInt(dates[1]);
			dateElements[2] = parseInt(dates[2]);
			//for (var i=0; i<=2; i++ ){
				//if (picture[i] != undefined){
				//	if (picture[i] === "M") dateElements[1] = parseInt(dates[1]);
				//	if (picture[i] === "D") dateElements[2] = parseInt(dates[2]);
				//	if (picture[i] === "Y") dateElements[0] = parseInt(dates[0]);
				//} //falta el caso en que no hay picture por defecto
			//}
			
			if ( (greaterThan[0]!=undefined) ){
				var cmpar = greaterThan[0].split("-");
				var cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] < dateElements[0]) || ((cmparElements[0] <= dateElements[0]) && (cmparElements[1] < dateElements[1]))
			       || ((cmparElements[0] <= dateElements[0]) &&  (cmparElements[1] <= dateElements[1]) && (cmparElements[2] < dateElements[2]) ) ){
			    		th = self.setStyleValues(th, greaterThan[1]);
			    } 
			    
			}
			
			
			if ( (lessThan[0]!=undefined) ){
				var cmpar = lessThan[0].split("-");
				cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
			    
			    if  ( (cmparElements[0] > dateElements[0]) || ((cmparElements[0] >= dateElements[0]) &&  (cmparElements[1] > dateElements[1]))
			       ||    ((cmparElements[0] >= dateElements[0]) &&  (cmparElements[1] >= dateElements[1]) && (cmparElements[2] > dateElements[2]) )   ){
			    		th = self.setStyleValues(th, lessThan[1]);
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
			    		th = self.setStyleValues(th, between[2]);
			    } 
			    
			}
			
		}
		
		
		if(value > greaterThan[0]) {
			th = self.setStyleValues(th, greaterThan[1]);
		} else {
			if(value < lessThan[0]) {
				th = self.setStyleValues(th, lessThan[1]);
			} else {
				if(value >= between[0] && value <= between[1]) {
					th = self.setStyleValues(th, between[2]);
				}
			}
		}
    	
    	return th;
    }
    
	this.applyConditionalFormats = function(td, value, lastMEasure ,measureNumber) { /* format for measures (row data) */
		if (measures.length > 0){
		var defaultFormats;
		var measureDataType;
		if (lastMEasure){
			measureNumber   =  measures.length - 1;
    		measureDataType =  measures[measures.length - 1].getAttribute("dataType");
    		defaultFormats    =  measures[measures.length - 1].getAttribute("format"); 
    	} else {
    		measureDataType =  measures[measureNumber].getAttribute("dataType");
    		defaultFormats    =  measures[measureNumber].getAttribute("format");
    	}
    	
    	//apply default format
    	if ((defaultFormats!=null) && (defaultFormats != "")){
    		td = self.setStyleValues(td, defaultFormats);
    	}
    	
    	//apply format value
    	for(var i = 0; i < self.formatValuesMeasures.length; i++) {
    		if(self.formatValuesMeasures[i].columnNumber == measureNumber){ //a format for this column
    			if (self.formatValuesMeasures[i].value === value){
    				td = self.setStyleValues(td, self.formatValuesMeasures[i].format);
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
		for(var i = 0; i < self.conditionalFormats.length; i++) {
			if(self.conditionalFormats[i].columnNumber == measureNumber) { 
				if(self.conditionalFormats[i].operation1 == "equal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						equal[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						equal[0] = self.conditionalFormats[i].value1
					}
					equal[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation1 == "notequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						notequal[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						notequal[0] = self.conditionalFormats[i].value1
					}
					notequal[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation1 == "less") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessThan[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						lessThan[0] = self.conditionalFormats[i].value1
					}
					lessThan[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation1 == "lessequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						lessOrEqual[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						lessOrEqual[0] = self.conditionalFormats[i].value1
					}
					lessThan[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation1 == "greater") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterThan[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						greaterThan[0] = self.conditionalFormats[i].value1;
					}
					greaterThan[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation1 == "greaterequal") {
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						greaterOrEqual[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						greaterOrEqual[0] = self.conditionalFormats[i].value1;
					}
					greaterThan[1] = self.conditionalFormats[i].format;
				}
				if(self.conditionalFormats[i].operation2 && self.conditionalFormats[i].operation1 == "greaterequal") {  //when interval
					greaterOrEqual = []
					if ( (measureDataType === "real") || (measureDataType === "integer")){
						between[0] = parseFloat(self.conditionalFormats[i].value1);
					} else {
						between[0] = self.conditionalFormats[i].value1;
					}
					if(self.conditionalFormats[i].operation2 && self.conditionalFormats[i].operation2 == "lessequal") {
						if ( (measureDataType === "real") || (measureDataType === "integer")){
							between[1] = parseFloat(self.conditionalFormats[i].value2);
						} else {
							between[1] = self.conditionalFormats[i].value2;
						}
						between[2] = self.conditionalFormats[i].format;
					}
				}
			}
		}
		
		if (measureDataType != "date"){
			if ((equal[0] != undefined) && (value == equal[0])){
				td = self.setStyleValues(td, equal[1]);
			}
			if ((notequal[0] != undefined) && (value != notequal[0])){
				td = self.setStyleValues(td, notequal[1]);
			}
			if ( ((greaterThan[0] != undefined) && (value > greaterThan[0])) || 
					((greaterOrEqual[0] != undefined)	&& (value >= greaterOrEqual[0])) ){                          
				td = self.setStyleValues(td, greaterThan[1]);
			} 
			if ( ((lessThan[0] != undefined) && (value < lessThan[0])) || 
			 ((lessOrEqual[0] != undefined) && (value <= lessOrEqual[0])) ){
				td = self.setStyleValues(td, lessThan[1]);
			} 
			if  ((between[0] != undefined && between[1] != undefined) && (value >= between[0] && value <= between[1])) {
				td = self.setStyleValues(td, between[2]);
			}
		}
				
		if (measureDataType === "date"){
			var dates = value.split("-");
			if ((self.defaultPicture.getAttribute("dateFormat")!= undefined) && (self.defaultPicture.getAttribute("dateFormat")!= null)) {
				picture = self.defaultPicture.getAttribute("dateFormat").split("");
			}
			var dateElements = new Array(3);
			dateElements[0] = parseInt(dates[0]);
			dateElements[1] = parseInt(dates[1]);
			dateElements[2] = parseInt(dates[2]);
			//for (var i=0; i<=2; i++ ){
				//if (picture[i] != undefined){
				//	if (picture[i] === "M") dateElements[1] = parseInt(dates[1]);
				//	if (picture[i] === "D") dateElements[2] = parseInt(dates[2]);
				//	if (picture[i] === "Y") dateElements[0] = parseInt(dates[0]);
				//} //falta el caso en que no hay picture por defecto
			//}
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
			    			td = self.setStyleValues(td, equal[1]);
			    		else if (greaterThan[1] != undefined)
			    			td = self.setStyleValues(td, greaterThan[1]);
			    		else if (lessThan[1] != undefined)
			    			td = self.setStyleValues(td, lessThan[1]);
			    } 
			    
			}
			
			if ( (notequal[0]!=undefined)){
				var cmpar = notequal[0].split("-");
				var cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] != dateElements[0]) || ((cmparElements[0] != dateElements[0]) && (cmparElements[1] != dateElements[1])) ){
			    		td = self.setStyleValues(td, notequal[1]);
			    } 
			    
			}
			
			if ( (greaterThan[0]!=undefined) || (greaterOrEqual[0]!=undefined) ){
				var cmpar; 
				if (greaterThan[0].split("-") != undefined){
					cmpar = greaterThan[0].split("-");
				} else {
					cmpar = greaterOrEqual[0].split("-");
				}
				var cmparElements = new Array(3);
				cmparElements[1] = parseInt(cmpar[1]);
				cmparElements[2] = parseInt(cmpar[2]);
				cmparElements[0] = parseInt(cmpar[0]);
				
			    if ( (cmparElements[0] < dateElements[0]) || ((cmparElements[0] <= dateElements[0]) && (cmparElements[1] < dateElements[1]))
			       || ((cmparElements[0] <= dateElements[0]) &&  (cmparElements[1] <= dateElements[1]) && (cmparElements[2] < dateElements[2]) ) ){
			    		td = self.setStyleValues(td, greaterThan[1]);
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
			    		td = self.setStyleValues(td, lessThan[1]);
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
			    		td = self.setStyleValues(td, between[2]);
			    } 
			    
			}
			
			} catch (ERROR){
				
			}
		}
		
		
		
		}
		return td;
		 
	}

    this.dimensionPictureValue = function(value, dimensionNumber){  //Picture for dimensions values
    	if (value == "#NuN#"){
    		var defaultNull = self.defaultPicture.getAttribute("textForNullValues");
    		if (defaultNull != undefined){
    			return defaultNull;
    		}
    		return "";
    	}
    	var result = "";
    	var dat
    	var customPicture;
    	
    	dat = self.columns[dimensionNumber].getAttribute("dataType");
    	customPicture = self.columns[dimensionNumber].getAttribute("picture");
    	var newValue = self.SetPictureValue(value, dat, self.defaultPicture, customPicture);
    	
    	return newValue;
    }
    
    this.getMeasureNumber = function(dataField, measures)
	{
		for(var mI=0; mI < measures.length; mI++){
			if (measures[mI].getAttribute("dataField") == dataField){
				return mI;
			}
		}
	} 
	
	this.getMeasureName = function(dataField, measures)
	{
		for(var mI=0; mI < measures.length; mI++){
			if (measures[mI].getAttribute("dataField") == dataField){
				return 	measures[mI].getAttribute("displayName");
			}
		}
	}  
	   
    this.defaultPictureValue = function(value, lastMEasure ,measureNumber) { //Picture for measures values
    	if (value == "#NuN#"){
    		var defaultNull = self.defaultPicture.getAttribute("textForNullValues");
    		if (defaultNull != undefined){
    			return defaultNull;
    		}
    		return "";
    	}
    	if ((value == "#NaV#") || (isNaN(value))){
    		return "-"
    	}
    	var result = "";
    	var dat
    	var customPicture;
    	
    	if (measures.length > 0){
    		if (lastMEasure){
    			dat = measures[measures.length - 1].getAttribute("dataType");
    			customPicture =  measures[measures.length - 1].getAttribute("picture"); 
    		} else {
    			dat = measures[measureNumber].getAttribute("dataType");
    			customPicture =  measures[measureNumber].getAttribute("picture");
    		}
			var newValue = self.SetPictureValue(value, dat, self.defaultPicture, customPicture);
    		return newValue;
    	} else {
    		return value;
    	}
    }
	
	this.SetPictureValue = function(value, type, defaultPicture, picture){ /* function responsible for set pictures */
		var thSepF = function(thousandsSeparator, record, picture){
			if ((thousandsSeparator!= undefined) && (thousandsSeparator != null)) {
				var negativeValue = (record[0] == "-");
				if (negativeValue) record = record.replace("-","");
				
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
    			
    			if (negativeValue) record = "-" + record;
    			
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
					
					if (type=="real"){
    					newValue = parseFloat(newValue).toFixed(decimalPlaces)    					
    				}
    				valueSplit = newValue.split(".");
					var picturewithoutsep = "";
					for (var ip = 0; ip < pictureSplit[0].length; ip++){
						if (pictureSplit[0][ip]!=","){
							picturewithoutsep = picturewithoutsep + pictureSplit[0][ip];  
						}
					}
    				
    				if (picturewithoutsep.indexOf("9")!=-1){
    					var maxobligatedpos = picturewithoutsep.length - picturewithoutsep.indexOf("9"); //para la parte entera
						if(valueSplit[0].length < maxobligatedpos) {
							var temp = valueSplit[0];
							for(var mi = 0; mi < maxobligatedpos - valueSplit[0].length; mi++) {
								temp = "0" + temp;
							}
							 valueSplit[0] = temp;
						}
					}   	
								
    				valueSplit[0] = thSepF(thseparator, valueSplit[0], pictureSplit[0]);
    				
    			} else {
    				if (type=="real"){
    					newValue = parseFloat(newValue).toFixed(2);
    					valueSplit = newValue.split(".");
    				}
    			}
    			
    			
    			
    			
    			if((picture != undefined) && (picture != "")) {	
    				newValue = valueSplit[0];
    			} else {
    				//valueSplit[0] = thSepFDefault(thseparator, valueSplit[0]);
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
					//if ((parseFloat(value) === 0) && (!self.ShowMeasuresAsRows)) newValue = "";
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
						}else if (picture == "9999/99/99") 
						{
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}
						}
						else {
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
				if (value == "") return "";
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
						}else
						    if (dividepicture[0] == "9999/99/99") 
							 if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
						 	 } else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];
							 } else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							 }
						else {
							/*if(valueSplit[0].length == 4) {
								valueSplit[0] = valueSplit[0].substr(valueSplit[0].length - 2, 2);
							}
							if ((defaultdate == undefined) || (defaultdate == null) || (defaultdate == "MDY")){
								newValue = valueSplit[1] + "/" + valueSplit[2] + "/" + valueSplit[0];
							} else if ((defaultdate!=undefined) && (defaultdate == "YMD")){ //for japanese
								newValue = valueSplit[0] + "/" + valueSplit[1] + "/" + valueSplit[2];
							} else { //DMY
								newValue = valueSplit[2] + "/" + valueSplit[1] + "/" + valueSplit[0];
							}*/
							newValue = "";
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
					//var dividepicture = picture.split(":");
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
					newValue = value.toUpperCase(); 
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
		//if (lastCharacter == true) {
        	//newValue = newValue + picture.charAt(picture.length - 1);
    	//}
		return newValue;
	}
	
	this.concatToGrandTotal = function(grandTotal, subArray){
		var res = grandTotal;
		for (var sAi = 0; sAi < subArray.length; sAi++){ //for every element of a subtotal
			var exists = false;
			for(var ief = 0; ief < res.length; ief++){
				if (res[ief][2] === subArray[sAi][2]){
					exists = true;
					for (var rMPos = 0; rMPos < measures.length-1; rMPos++)
						/*res[ief][0][rMPos] = res[ief][0][rMPos].concat(subArray[sAi][0][rMPos]);*/
						for (var iRMPos = 0; iRMPos < subArray[sAi][0][rMPos].length; iRMPos++){
							if (subArray[sAi][0][rMPos][iRMPos][1] == 99){
								res[ief][0][rMPos].push(subArray[sAi][0][rMPos][iRMPos][0]);
							}	
						}	
				}
			}
			if (!exists){
				var deepCopy = jQuery.extend(true, [], subArray[sAi]);
				var temp = []
				for (var iDC = 0; iDC < deepCopy[0].length; iDC++){
					var subTemp = []
					for (var subIDC = 0; subIDC < deepCopy[0][iDC].length; subIDC++){
						subTemp.push(deepCopy[0][iDC][subIDC][0]);
					}
					temp.push(subTemp)
				}
				deepCopy[0] = temp
				res.push(deepCopy); 
			}
		}
		return res;
	}
	
	this.countedRows = 0;
	
	this.appendRowToTable = function(tbody, row, isTitleRow){
		if ( ((!self.autoPaging) /*|| (self.FilterByTopFilter)*/) && (pageSize) && (!self.serverPagination)) {
			if (!isTitleRow){
				self.countedRows++;
			}
			if (self.countedRows > pageSize){
				row.style.display = "none";
			}	
		}
		if ((self.serverPagination) && (self.ExportTo)){
			if (!isTitleRow){
				self.countedRows++;
			}
			if (self.countedRows > self.rowsPerPage){
				row.style.display = "none";
			}
		}
		if (isTitleRow){
			row.setAttribute("title_row", true);
		}
		
		if (self.autoPaging){ //&& (self.paginationInfo) && (self.paginationInfo.HideLastSubTotal)) {
			var extrarows = (!self.paginationInfo) ? 0 : self.paginationInfo.pages[self.actualPaginationPage-1].extraRows
			if (!isTitleRow){
				if (self.countedRows < extrarows){
					row.style.display = "none";
				}
				self.countedRows++;
			}
			if (self.countedRows > (self.autoPagingRowsPerPage+extrarows)){
				row.style.display = "none";
			}
		}
		
		tbody.appendChild(row);
	}
	
	this.drawTable = function() {
			OAT.Dom.clear(self.div);
			// START create toolbar table

			self.div.setAttribute("class", "conteiner_table_div");
			var myTable = OAT.Dom.create("table", {}, "");
			myTable.id = self.controlName + "_" + self.query + "_toolbar";
			var myTbody = OAT.Dom.create("tbody");
			var myRow = OAT.Dom.create("tr");
			self.div.appendChild(myTable);
			// END

			var table = OAT.Dom.create("table", {}, "pivot_table");
			table.id = this.controlName + "_" + this.query;
			//add control name and query name as the id of main table
			var tbody = OAT.Dom.create("tbody");

		
			if((self.autoPaging) && (self.allData.length < self.filteredData.length) && (self.colPointers.length > 1)) {
				self.allData = self.filteredData;
			} else if((self.autoPaging) && (self.filteredData.length > self.allData.length) && (self.colPointers.length === 0)) {
				self.filteredData = self.allData;
			}

			self.drawFilters();
			self.countedRows = 0;
			if((gx.util.browser.isIE())) {
				var divIeContainer = document.createElement("div");
				divIeContainer.setAttribute("class", "divIeContainer");
				self.div.appendChild(divIeContainer);
				OAT.Dom.append([table, tbody], [divIeContainer, table]);
				table.style.marginBottom = "0px";
			} else {
				OAT.Dom.append([table, tbody], [self.div, table]);
			}
			
			self.countedRows = 0;
			var firstRow;
			var firstRowTotalSpan = self.colConditions.length;
			if(self.colConditions.length > 0) {
				var i = 0;
				var tr = OAT.Dom.create("tr");
				self._drawRowConditionsHeadingsCustom(tr);
				// create a list of the dimension name, that are not pivot, with heigth span equal to   colConditions.length
				for(var ni = 0; ni < self.colConditions.length; ni++) {// creat a list of the dimension name, that are pivot as columns
					tr = self._drawColConditionsHeadingsCustom(tr, ni, (ni === self.colConditions.length - 1));
				}
				//tr.setAttribute("title_row", true);
				self.appendRowToTable(tbody, tr, true);//tbody.appendChild(tr);
				firstRow = tr;
			}
			try {
				var _mtotalSpan = 0 - firstRowTotalSpan;
				var collapsedColInfo = [false, 0, 0] //firts son of a collapsed parent, the collapsed parent, total hidden 
				var columnsDataHide = []
				var columnsDataHideFillWithBlanck = []
				var td_temp_forCollapseInfo = false;
				var td_collection_forCollapseInfo = [];
				for (var i = 0; i < measures.length; i++){
					td_collection_forCollapseInfo[i] = false;
				}
				
				for(var i = 0; i < self.colConditions.length; i++) {
					var tr = OAT.Dom.create("tr");

					var stack = self.colPointers[i];
					var drawColumn = 0; //number of actual column being draw 
					for(var j = 0; j < stack.length; j++) {//column values here the diferents values of the pivoted dimensions
						var item = stack[j];
						var th = OAT.Dom.create("th", {}, "even");
						if(item.span === 0)
							item.span = 1;
						th.colSpan = item.span * measures.length;
						if(item.span === 0) {//calc for the span of the top right blanck cell
							_mtotalSpan++;
						} else {
							_mtotalSpan = _mtotalSpan + (item.span * measures.length);
						}
						th.innerHTML = self.dimensionPictureValue(item.value, self.colConditions[i]);
						
						th = self.applyFormatValues(th, item.value, self.colConditions[i]);
						
						
						//find if some parent of the item is collapsed
						var tempItem = item;
						var hide = false;
						var blankcell = false; 
						while (tempItem.parent){
							if (tempItem.parent.collapsed==undefined){
								collapsedColInfo[0] = false;
								break;
							}
							if (tempItem.parent.collapsed){
								if ((!collapsedColInfo[0]) || (tempItem.parent != collapsedColInfo[1])){
									blankcell = true;
									if (i == self.colConditions.length -1){
										columnsDataHideFillWithBlanck.append(j);
									} 
								} else {
									collapsedColInfo[2]++;
								}
								if ((i == self.colConditions.length -1) && (!blankcell)) //the last col conditions
								{	
									columnsDataHide.append(j);
								}	
								collapsedColInfo[0] = true;
								collapsedColInfo[1] = tempItem.parent; 
								th.innerHTML = ""	
								hide = true;
								th.colSpan = 1;
								break;
							} else {
								tempItem = tempItem.parent; 	
							}				
						}
						var itemCollapsed = (self.conditions[self.colConditions[i]].collapsedValues.indexOf(item.value) != -1);
						if ((!hide) && (i < self.colConditions.length - 1)){
							th = self.addExpandCollapseFunctionality(th, item, i, !itemCollapsed, false);
						}
						if ((!hide)||(blankcell)){ //when no hide or when collapse add one column blank cells
							tr.appendChild(th);
						}

						var cond = self.conditions[self.colConditions[item.depth]];
						if (cond.subtotals && i + 1 < self.colConditions.length) {// subtotal columns
							var th = OAT.Dom.create("th", {}, "h2subtitle ");
							th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + item.value;
							th.rowSpan = self.colConditions.length - i + 1;
							_mtotalSpan = _mtotalSpan + self.colConditions.length - i;
							if (!((collapsedColInfo[0])&&(collapsedColInfo[1]!=item))){ //if an item parent is collapse dont show subtotals
								tr.appendChild(th);
							}
						}
						
						//advance the column counter
						drawColumn = drawColumn + item.span * measures.length
					}
					if(self.options.totals && i == 0) {
						var th = OAT.Dom.create("th", {}, "h2subtitle");
						th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
						//"TOTAL" gran total
						th.rowSpan = self.colConditions.length;
						if(self.colConditions.length > 0) {
							th.colSpan = measures.length;
							_mtotalSpan = _mtotalSpan + self.colConditions.length + (measures.length - 1)
						} else {
							_mtotalSpan = _mtotalSpan + self.colConditions.length
						}
						totalSpan = self.colConditions.length
						tr.appendChild(th);
					}
					if(self.options.headingAfter) {// column headings after
						self._drawColConditionsHeadings(tr, i);
					}
					//tr.setAttribute("title_row", true);
					self.appendRowToTable(tbody, tr, true);//tbody.appendChild(tr);

				}

				/*add a column only with the name of  measures */
				if(self.colConditions.length > 0) {
					var tr = OAT.Dom.create("tr");
					for(var j = 0; j <= stack.length - collapsedColInfo[2]; j++) {
						for(var m = 0; m < measures.length; m++) {
							var th = OAT.Dom.create("th", {}, "h2titlewhite");
							th.colSpan = 1;
							th.innerHTML = measures[m].attributes.getNamedItem("displayName").nodeValue;
							self.setTitleTexrtAlign(th, th.innerHTML);
							tr.appendChild(th);
						}
					}
					//tr.setAttribute("title_row", true);
					self.appendRowToTable(tbody, tr, true);//tbody.appendChild(tr);
				}
				/* end of column only with the name of  measures */

				if(_mtotalSpan > 0) {
					var th = OAT.Dom.create("th", {}, "h2subtitle ");
					th.innerHTML = "";
					th.colSpan = _mtotalSpan;
					th.style.borderLeftColor = "transparent";
					firstRow.appendChild(th);
				}
				
				/* first connector */
				if((self.rowConditions.length && self.options.headingBefore) && (self.colConditions.length === 0)) {
					self._drawRowConditionsHeadings(tbody);
				} else if ((self.rowConditions.length==0) && (self.options.headingBefore) && (self.colConditions.length === 0) ){
					try {
						self._drawRowConditionsHeadings(tbody);
					} catch (Error) {}
				}
				
				var several_totals = measures.length > 1;
				var colSpan;
				if(several_totals) {
					colSpan = self.rowConditions.length - (measures.length - 1);
				} else {
					colSpan = self.rowConditions.length;
				}

				var collapsedInfo = [false, 9999, true, 0]
				//[working under collapse item, row condition of the collapse item, first row of collapses rows, actual row condition]
				var subtotalsmeasuresList = [];
				/* store values to show in the subtotal row */
				var grandtotalsmeasuresList = [];
				//C1Line
				var lineaEnBlanco = false;
				for(var i = 0; i < self.h; i++) {
					
					var tr = OAT.Dom.create("tr");
					if(self.rowConditions.length) {
						var item = self.rowPointers[self.rowConditions.length - 1][i];
						/* stack has number of values equal to height of table */
						var ptrArray = [];
						var ptr = item;
						while(ptr.parent) {
							ptrArray.unshift(ptr);
							ptr = ptr.parent;
						}
					}

					for(var j = 0; j < self.rowConditions.length - (measures.length - 1); j++) {/* row header values */
						var item = ptrArray[j];
						if((item != undefined) && (item.offset == i)) {
							//add collapse image option
							var itemCollapsed = (self.conditions[self.rowConditions[j]].collapsedValues.indexOf(item.value) != -1);
							if (j >= (self.rowConditions.length - (measures.length - 1) - 1)){
								itemCollapsed = false;
							}

							var th = OAT.Dom.create("th", {}, "even");
							if((!collapsedInfo[0]) || (collapsedInfo[1] >= j)) {//if the line is not collapsed
								th.rowSpan = ptrArray[j].spanDraw;//ptrArray[j].span;
								//th.rowSpan = ptrArray[j].span;
								th.innerHTML = self.dimensionPictureValue(item.value, self.rowConditions[j]);
								/* picture for columns */
								th = self.applyFormatValues(th, item.value, self.rowConditions[j]);
								/* format  for columns */
								self.setClickEventHandlers(th, item.value, "DIMENSION", self.rowConditions[j], item);

								if((j < self.rowConditions.length - (measures.length - 1) - 1) && ((measures.length > 0) || (j < self.rowConditions.length - 1))) {
									th = self.addExpandCollapseFunctionality(th, item, j, !itemCollapsed, true);
								}
							} else {
								th.rowSpan = 1;
								th.innerHTML = "";
								/* picture for columns */
							}

							if(collapsedInfo[1] >= j) {
								collapsedInfo[0] = itemCollapsed;
								collapsedInfo[3] = item;
								if(itemCollapsed) {
									collapsedInfo[1] = j;
								} else {
									collapsedInfo[1] = 9999;
								} 
								collapsedInfo[2] = true;
							}
						
							lineaEnBlanco = false;	
							tr.appendChild(th);
						} else {
							if (measures.length > 0){
								lineaEnBlanco = true;
							}
						}
					}
					/* blank space before */
					if ((self.rowConditions.length - (measures.length - 1) == 0) && (i > 0) && (measures.length > 0)){
						lineaEnBlanco = true;
					}
	
					/* initial "crude data" part of a row, only the data of the measures shows in one column*/
					var mesauresList = new Array();

					var measureslength = measures.length;
					if(measureslength === 0) {
						measureslength = 1;
					}

					for(var ji = 0; ji < (measureslength - 1); ji++) {
						var item = ptrArray[self.rowConditions.length - (measureslength - 1) + ji];

						var td = OAT.Dom.create("td", {}, "even");

						if(item.value != self.EmptyValue) {
							if ((!collapsedInfo[0]) || (self.autoPaging)){
								td.innerHTML = self.defaultPictureValue(item.value, false, ji);
								td = self.applyConditionalFormats(td, item.value, false, ji);
								self.setClickEventHandlers(td, item.value, "MEASURE", ji, item);
							}
						} else {
							td.innerHTML = "";
						}
						if (collapsedInfo[0] && (!td_collection_forCollapseInfo[ji])){
							td_collection_forCollapseInfo[ji] = td;
						}
						
						if (!mesauresList[ji]){
							mesauresList[ji] = []
						}
						var rowNumValue = (ptrArray[ptrArray.length - 1].row != undefined) ? ptrArray[ptrArray.length - 1].row : item.row 
						mesauresList[ji].append([[item.value, [rowNumValue]]]);
						/* when some conditions is move to columns, remember the value and the asociate filteredDataRow*/
						
						if(self.colConditions.length === 0) {
							tr.appendChild(td);
						}
					}

					if(self.colConditions.length && i == 0 && self.options.headingBefore) {
						var th = OAT.Dom.create("th");
						if(!self.rowConditions.length) {
							self._drawCorner(th, true);
							th.conditionIndex = -2;
						} else {
							th.style.border = "none";
						}
						th.rowSpan = self.rowStructure.span;
						th.hidden = true;
					}
					var tdTempSubtotalForColumn = false;
					var sumaLaterales = []; for (var iSL=0; iSL < measureslength - 1; iSL++){ sumaLaterales[iSL] = [];}
					for(var j = 0; j < self.w; j++) {/* data */
						var result = self.tabularData[j][i][0]; 
						var isNun = false;
						try {
							isNun = ((result == "#NaV#") && (self.tabularData[j][i][1].length > 0));	
						} catch (Error){}
						/* add empty space for measures and col condition, and fill if here theres a value */
						if(self.colConditions.length > 0) {
							for(var fg = 0; fg < measureslength - 1; fg++) {
								var td = OAT.Dom.create("td", {}, self.getClassName(i, j));
								if ((result != self.EmptyValue) || isNun) {
									//C1Line
									var valMeasureToAdd = getValueMeasureFromMeasureList(mesauresList, self.tabularData[j][i][1][0], fg, self.filteredData, measures.length) 
									td.innerHTML = self.defaultPictureValue(valMeasureToAdd, false, fg);
									td = self.applyConditionalFormats(td, valMeasureToAdd, false, fg);
									self.setClickEventHandlers(td, valMeasureToAdd, "MEASURE", fg, self.tabularData[j][i][1]);
									var pres = false;
									for(var mll = 0; mll < subtotalsmeasuresList.length; mll++) {
										if(subtotalsmeasuresList[mll][2] === j) {
											if (valMeasureToAdd == "#NuN#"){
												subtotalsmeasuresList[mll][0][fg].push(["#NuN#",99]);
											} else if (valMeasureToAdd == "#FoE#"){
												listValoresMeasures[fg].append([["#FoE#",99]])	
											} else {
												subtotalsmeasuresList[mll][0][fg].push([parseFloat(valMeasureToAdd),99]); //el segundo valor el maximo nivel en que ya se uso el valor
											}
											subtotalsmeasuresList[mll][3].append(self.tabularData[j][i][1].slice());
											pres = true
										}
									}
 
									if(!pres) {
										var listValoresMeasures = []; for(var lVMi = 0; lVMi < measureslength-1; lVMi++){ listValoresMeasures[lVMi] = [] }
										if (valMeasureToAdd == "#NuN#"){
											listValoresMeasures[fg].append([["#NuN#",99]])
										} else if (valMeasureToAdd == "#FoE#"){
											listValoresMeasures[fg].append([["#FoE#",99]])	
										} else {
											listValoresMeasures[fg].append([[parseFloat(valMeasureToAdd),99]])
										}
										subtotalsmeasuresList.append([[ listValoresMeasures, fg, j, self.tabularData[j][i][1].slice()]]);
									}
									if (!isNaN(parseFloat(valMeasureToAdd))){
										sumaLaterales[fg].push(parseFloat(valMeasureToAdd));
									}
								} else {
									td.innerHTML = "";
								}
								if (columnsDataHideFillWithBlanck.indexOf(j)!=-1){
									td.innerHTML = "";
								}
								if (columnsDataHide.indexOf(j)==-1){ //dont add if column is hide because of collapse column dimension
									tr.appendChild(td);
								}
							}
						}
						/* */

						var td = OAT.Dom.create("td", {}, self.getClassName(i, j));
						
						if (columnsDataHide.indexOf(j)==-1){
							if(result != self.EmptyValue) {
								td.innerHTML = self.defaultPictureValue(result.toString(), true, 0);
								td = self.applyConditionalFormats(td, result.toString(), true, 0);
								self.setClickEventHandlers(td, result, "MEASURE", measureslength - 1, self.tabularData[j][i][1]);
							} else {
								td.innerHTML = "";
							}
							
							if ((((columnsDataHideFillWithBlanck.indexOf(j)!=-1) || (collapsedInfo[0]))) && (!self.autoPaging)){
								if (!td_temp_forCollapseInfo) td_temp_forCollapseInfo = td;
								if (!tdTempSubtotalForColumn) tdTempSubtotalForColumn = td;
								td.innerHTML = "";
							}
							
							if((measures.length > 0) ){//&& (!collapsedInfo[0])) {
								tr.appendChild(td);
							}
						}
						/* column subtotals */
						if(measures.length > 0) {
							if(self.colPointers.length > 0) { // && self.options.subtotals
								var item = self.colPointers[self.colPointers.length - 1][j].parent;
								while(item.parent) {
									var tempItem = item.parent
									var collapseFather = false;
									while (tempItem != undefined){
										collapseFather = ((item.parent.collapsed != undefined) && (item.parent.collapsed)) ? true : false;
										tempItem = tempItem.parent
									}
									      
									if (!collapseFather){
										var cond = self.conditions[self.colConditions[item.depth]];
										if(item.offset + item.spanData - 1 == j){ //&& cond.subtotals) {
											var td = OAT.Dom.create("td", {}, "subtotal");
											
											if (item.totals[i].value > 0) {
												td.innerHTML = self.defaultPictureValue(item.totals[i].value.toString(), true, 0);
												td = self.applyConditionalFormats(td, item.totals[i].value.toString(), true, 0);
												self.setClickEventHandlers(td, item.totals[i].value.toString(), "MEASURE", measureslength - 1, item);
												if (tdTempSubtotalForColumn != false){
													tdTempSubtotalForColumn.innerHTML = self.defaultPictureValue(item.totals[i].value.toString(), true, 0);
													tdTempSubtotalForColumn = false;
												}
											} else {
												td.innerHTML = ""
											}
											if (cond.subtotals/*self.options.subtotals*/){
												tr.appendChild(td);
											}
										}
									}
									item = item.parent;
								}
							} /* if subtotals */
						}
					}/* for all rows */
					
					if(self.options.totals && self.colConditions.length) {/* columns totals */
						/* totals for other measure when move to column (its not properly a total, indeed is the value for that row of the measure)*/
						if(self.colConditions.length > 0) {//dont last mesaures totals
							for(var fg = 0; fg < measureslength - 1; fg++) {
								var td = OAT.Dom.create("td", {}, "total");
								//get total //TODO: soportar total para formulas
								//C1Line
								var _colTotal = 0;
								for (var posIt = 0; posIt < sumaLaterales[fg].length; posIt++){
										if (!isNaN(sumaLaterales[fg][posIt])){
											_colTotal = parseFloat(sumaLaterales[fg][posIt]) + _colTotal
										}
								}
								
								td.innerHTML = self.defaultPictureValue(_colTotal.toString()/*mesauresList[0][fg][0]*/, false, fg);
								td = self.applyConditionalFormats(td, _colTotal.toString(), false, fg);
								self.setClickEventHandlers(td, _colTotal.toString(), "MEASURE", fg, ['PtrTotals', self.rowTotals[1][i]]);
								tr.appendChild(td);
							}
						}
						/* */
						if (self.rowConditions.length) {//last meseasure total
							var td = OAT.Dom.create("td", {}, "total");
							if (!isNaN(self.rowTotals[0][i])){
								td.innerHTML = self.defaultPictureValue(self.rowTotals[0][i].toString(), true, 0);
								td = self.applyConditionalFormats(td, self.rowTotals[0][i].toString(), true, 0);
								self.setClickEventHandlers(td, self.rowTotals[0][i].toString(), "MEASURE", measureslength - 1, ['PtrTotals', self.rowTotals[1][i]]);
							} else {
								td.innerHTML = "-"
							}
							//check the value of asociate filteredData row
							tr.appendChild(td);
						} else {
							self._drawGTotal(tr);
						}
					}

					if(self.colConditions.length && i == 0 && self.options.headingAfter) {/* blank space after */
						var th = OAT.Dom.create("th");
						if(!self.rowConditions.length) {
							self._drawCorner(th, true);
							th.conditionIndex = -2;
						} else {
							th.style.border = "none";
						}
						th.rowSpan = self.rowStructure.span + (self.options.totals && self.rowConditions.length ? 1 : 0);
						tr.appendChild(th);
					}
					if(collapsedInfo[0] && !collapsedInfo[2]) {

					} else {
						if (!lineaEnBlanco){//C1Line
							self.appendRowToTable(tbody, tr, false);//tbody.appendChild(tr);
							collapsedInfo[2] = false;
						}
					}

					var subTotalRowNumber = self.rowConditions.length - 2;
					if((several_totals) && (self.rowConditions.length - 2 >= 0) && (ptrArray[self.rowConditions.length - 2] != undefined) && (ptrArray[self.rowConditions.length - 2].items != undefined) && (ptrArray[self.rowConditions.length - 2].items.length <= 1) && (self.colConditions.length <= 0)) {
						subTotalRowNumber = self.rowConditions.length - 2 - (measureslength - 1);
					}
					if (self.rowConditions.length - (measures.length - 1) <= 0){ //si todas las conditions de la row son masures o se movieron al filtro
						subTotalRowNumber = -1 //no hay subtotales que mostrar
					}

					for(var j = subTotalRowNumber; j >= 0; j--) {//subtotal rows
						var item = ptrArray[j];
						//last row's item		
						var eleije = item.value;
						var cond = self.conditions[self.rowConditions[item.depth]];
						
						var subTotalMostrado = false;
						
						var conditionNumber = item.conditionNumber;
						var itemColumnNumber = self.rowConditions.indexOf(conditionNumber) + 1
						var dimensionTotalColumns = self.rowConditions.length - measures.length + 1 //if dimensionTotalColumns > itemColumnNumber => item es measure //if dimesnionTotalColumns == itemColumnNumber => item es dimension, pero la ultima de las filas (no suma, su info siempre se colapsa en una linea) 
						if((item.offset + item.spanData - 1 == i) && ((columns.length < 2 ) || (self.colConditions.length != 0) || (j < columns.length - 1))
								&& ((self.colConditions.length == 0) || (itemColumnNumber < dimensionTotalColumns))
								&& ((!self.autoPaging) || (self.colConditions.length == 0))
								//&& ((self.colConditions.length == 0) || (measures.length < 2) || (self.rowConditions.length - 1 < 2) || ((j < self.rowConditions.length - measures.length + 1) && ((self.rowConditions.length - measures.length)>= 3)) )//(j <= self.rowConditions.length - self.colConditions.length - 1) )
								 )
								//index j, must be an item of the uppers row, valid to add as subtotal, only first position of array ptrArray
						{
							//decide where to add or not subTotal row 
							var aPsums;
							if (self.autoPaging) {
								var dRow = getDataRowFromItem(item);
								//the item to sum as an array
								aPsums = getTotalsForDataRow(self.GeneralDataRows, dRow, measureslength, self.conditions, self.filterIndexes, self.allData, self.filterDiv.selects, self);
							}
							var tr = OAT.Dom.create("tr");
							var th = OAT.Dom.create("th", {}, "h2subtitle");

							th.colSpan = colSpan - j;
							
							if (item.value != "#NuN#") {
								th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + item.value;
							} else {
								th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + " ";
							}
							tr.appendChild(th);

							if(!self.autoPaging) {
								if(several_totals) {//here: the sum of subtotales except the last measure
									for(var ind = 0; ind < measureslength - 1; ind++) {
										if(self.colConditions.length === 0) {
											var sub_td = false;
											if (!(collapsedInfo[0] && !collapsedInfo[2] && (collapsedInfo[3] != item) && (j>=collapsedInfo[1]))){
												sub_td = td_collection_forCollapseInfo
											}
											if(ind > 0) {
												self._drawRowSubtotalsForMeasures(tr, i, item, ind, j, undefined, sub_td)//td_collection_forCollapseInfo);
												//j actual dimension
											} else {
												self._drawRowSubtotalsForMeasures(tr, i, item, ind, j, th       , sub_td)//td_collection_forCollapseInfo);
												//j actual dimension //only th, to add the event on click
											}
										}
										var totals_ = 0;
									}
								}
							}
							grandtotalsmeasuresList = self.concatToGrandTotal(grandtotalsmeasuresList, subtotalsmeasuresList);
							
							if(measures.length > 0) {
								if(self.autoPaging) {
									if(aPsums)
										self._drawRowSubtotalsAutoPaging(tr, aPsums);
								} else {
									var sub_td = td_temp_forCollapseInfo
									if(collapsedInfo[0] && !collapsedInfo[2] && (collapsedInfo[3] != item) && (j>=collapsedInfo[1])){
										sub_td = false;
									} else {
										td_temp_forCollapseInfo = false;	
									}
									self._drawRowSubtotals(tr, i, item, subtotalsmeasuresList, sub_td, itemColumnNumber);
									/* add "regular" subtotals */
								}
							}
							//subtotalsmeasuresList = [];
							if((!self.autoPaging) || (aPsums)) {
								if(collapsedInfo[0] && !collapsedInfo[2] && (collapsedInfo[3] != item) && (j>=collapsedInfo[1])){
									
								} else {
									if (cond.subtotals){
										self.appendRowToTable(tbody, tr, false);//tbody.appendChild(tr);
									}
								}
							}
						}
					}

					if ((subTotalRowNumber === -1)){
						grandtotalsmeasuresList = self.concatToGrandTotal(grandtotalsmeasuresList, subtotalsmeasuresList);
						subtotalsmeasuresList = [];
					} else if ((self.rowConditions.length - measures.length) == 0){ //cuando no se calulan los subtotales porque solo hay una dimension en las filas
						//agrego para el gran total
						grandtotalsmeasuresList = self.concatToGrandTotal(grandtotalsmeasuresList, subtotalsmeasuresList);
						subtotalsmeasuresList = [];
					}
					
					//if ((subtotalsmeasuresList.length > 0) && (self.rowConditions.length - 1 >= 2)) { //si no se calculan o muestran
						//el (self.rowConditions.length - 1 >= 2) es por el caso 2dim 2measures n=> no borra, pero con 3dim o mas si
						//grandtotalsmeasuresList = self.concatToGrandTotal(grandtotalsmeasuresList, subtotalsmeasuresList);
						//subtotalsmeasuresList = [];
					//}
				} /* for each row */

			} catch (ERROR) {
				//alert(ERROR);
			}

			/* code for the last row, GRAND TOTAL ROW */
			/* GRAND TOTAL ROW	*/
			if((measures.length > 0) && ((!self.autoPaging) || (self.colConditions.length == 0))){

				if((self.options.totals && self.rowConditions.length) && ((!self.autoPaging) || (self.TotalPagesPaging == self.actualPaginationPage) /*|| (self.FilterByTopFilter)*/)) {
					var tr = OAT.Dom.create("tr");

					if(colSpan != 0) {
						var th = OAT.Dom.create("th", {}, "h2subtitle");
						th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
						th.colSpan = colSpan;
						tr.appendChild(th);
					}

					var GrandTotals = new Array();
					if ((self.autoPaging) /*&& (!self.FilterByTopFilter)*/){
						GrandTotals = sumGrandPagingTotals(this.GeneralDataRows, self.conditions, measures.length, self.formulaInfo, self);
					}
					if((several_totals)) {// && (self.colConditions.length ===0)){
						for(var ind = 0; ind < measures.length - 1; ind++) {
							numCol = self.rowConditions.length - (measures.length - 1) + ind;

							var td = OAT.Dom.create("td", {}, "gtotal");
							var values_ = this.rowPointers[numCol];
							var totals_ = 0;
							
							var valuesToOperate = []
							for(var i = 0; i < values_.length; i++) {
								if (self.formulaInfo.measureFormula[ind].hasFormula){
									var ref = values_[i]
									for (var t=0; t < ind; t++){
										ref = ref.parent
									}	
									valuesToOperate.push(self.getFormulaRowByCoord(ref, false, ind, "MeasureInRows"))
								}
								if (OATIsNotEmptyValue(values_[i].value)){ 
									totals_ = totals_ + parseFloat(values_[i].value);
								}
							}

							if((!self.autoPaging) /*|| (self.FilterByTopFilter)*/){
								if (self.formulaInfo.measureFormula[ind].hasFormula){
									 totals_ = self.calculateFormulaTotal(valuesToOperate, ind, "MeasureInRows");
									 GrandTotals[ind] = totals_;	
								} else {
									GrandTotals[ind] = totals_;
								}
							}

							if(self.colConditions.length === 0) {
								if((self.autoPaging) /*&& (!self.FilterByTopFilter)*/){
									if(!isNaN(totals_)) {
										td.innerHTML = self.defaultPictureValue(GrandTotals[ind].toString(), false, ind);
										td = self.applyConditionalFormats(td, GrandTotals[ind].toString(), false, ind);
									} else {
										td.innerHTML = "";
									}
									tr.appendChild(td);
								} else {
									if(!isNaN(totals_)) {
										td.innerHTML = self.defaultPictureValue(totals_.toString(), false, ind);
										td = self.applyConditionalFormats(td, totals_.toString(), false, ind);
										self.setClickEventHandlers(td, totals_.toString(), "MEASURE", ind, 'GrandTotal');
										if(ind == 0) {
											self.setClickEventHandlers(td, gx.getMessage("GXPL_QViewerJSTotal"), "DIMENSION", ind, 'GrandTotal');
										}
									} else {
										td.innerHTML = "";
									}
									tr.appendChild(td);
								}
							}
						}
					}

					self._drawRowTotals(tr, GrandTotals, grandtotalsmeasuresList, columnsDataHide);
					self.appendRowToTable(tbody, tr, false);//tbody.appendChild(tr);
				}

				/* second connector */
				if(self.rowConditions.length && self.options.headingAfter) {
					self._drawRowConditionsHeadings(tbody);
				}
			}
			
		} /* drawTable */
	
	
		this.drawTableWhenServerPagination = function() {
			OAT.Dom.clear(self.div);
			// START create toolbar table

			self.div.setAttribute("class", "conteiner_table_div");
			var myTable = OAT.Dom.create("table", {}, "");
			myTable.id = self.controlName + "_" + self.query + "_toolbar";
			var myTbody = OAT.Dom.create("tbody");
			var myRow = OAT.Dom.create("tr");
			self.div.appendChild(myTable);
			// END

			var table = OAT.Dom.create("table", {}, "pivot_table");
			table.id = this.controlName + "_" + this.query;
			//add control name and query name as the id of main table
			var tbody = OAT.Dom.create("tbody");

			self.drawFilters();
			self.countedRows = 0;
			if((gx.util.browser.isIE())) {
				var divIeContainer = document.createElement("div");
				divIeContainer.setAttribute("class", "divIeContainer");
				self.div.appendChild(divIeContainer);
				OAT.Dom.append([table, tbody], [divIeContainer, table]);
				table.style.marginBottom = "0px";
			} else {
				OAT.Dom.append([table, tbody], [self.div, table]);
			}
			
			self.countedRows = 0;
			var firstRow;
			var firstRowTotalSpan = self.colConditions.length;
			if(self.colConditions.length > 0) {
				var i = 0;
				var tr = OAT.Dom.create("tr");
				self._drawRowConditionsHeadingsCustom(tr);
				// create a list of the dimension name, that are not pivot, with heigth span equal to   colConditions.length
				for(var ni = 0; ni < self.colConditions.length; ni++) {// creat a list of the dimension name, that are pivot as columns
					tr = self._drawColConditionsHeadingsCustom(tr, ni, (ni === self.colConditions.length - 1));
				}
				//tr.setAttribute("title_row", true);
				self.appendRowToTable(tbody, tr, true);//tbody.appendChild(tr);
				firstRow = tr;
			}
			try {
				
				for(var i = 0; i < self.colConditions.length; i++) { //show columns particular values
					var tr = OAT.Dom.create("tr");
					
					
					var j = 0
					while (j < self.pageData.columnsHeaders.length){
						
						
					
						if ((!self.pageData.columnsHeaders[j].subTotal)){
					
							var colSpan = 1;
							var columnValue = self.pageData.columnsHeaders[j].subHeaders[i].value
							while ((j+1 < self.pageData.columnsHeaders.length)
								&& ((!self.pageData.columnsHeaders[j+1].subTotal) || (i < self.pageData.columnsHeaders[j+1].subHeaders.length-1)) 
								&& (self.pageData.columnsHeaders[j+1].subHeaders[i].value == columnValue)){
								colSpan++;
								j++;
							}
							j++;
							
						
							var th = OAT.Dom.create("th", {}, "even");
							th.colSpan = colSpan;
							th.innerHTML = self.dimensionPictureValue(columnValue.trim(), self.colConditions[i]);
							th = self.applyFormatValues(th, columnValue.trim(), self.colConditions[i]);
							tr.appendChild(th);
						} else { 
							var isGrandTotal = (j >= (self.pageData.columnsHeaders.length-measures.length))
							if (self.ShowMeasuresAsRows){
								isGrandTotal = (j >= (self.pageData.columnsHeaders.length-1))
							}
							if (isGrandTotal){
								//Grand Total
								if (i==0){
									var th = OAT.Dom.create("th", {}, "h2subtitle");
									th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
									th.rowSpan = self.colConditions.length;
									if (!self.ShowMeasuresAsRows){
										th.colSpan = measures.length;
									}
									tr.appendChild(th);
								}
								if (!self.ShowMeasuresAsRows){
									j = j + measures.length
								} else {
									j++;
								}
							} else {
								//subtotal columns
								if (i < self.pageData.columnsHeaders[j].subHeaders.length){ 
									var th = OAT.Dom.create("th", {}, "h2subtitle ");
									
									var columnValue = self.pageData.columnsHeaders[j].subHeaders[i].value
									th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + columnValue.trim();
									th.rowSpan = self.colConditions.length-i;
									if (!self.ShowMeasuresAsRows){
										th.colSpan = measures.length;
									}
									tr.appendChild(th);
								}
								
								if (!self.ShowMeasuresAsRows){
									j = j + measures.length
								} else {
									j++;
								}
							}
						}
					}
					self.appendRowToTable(tbody, tr, true);
				}
				
				if(self.colConditions.length > 0) {
					if(self.pageData.columnsHeaders.length - self.colConditions.length > 0) { //fill first row
						var th = OAT.Dom.create("th", {}, "h2subtitle ");
						th.innerHTML = "";
						th.colSpan = self.pageData.columnsHeaders.length - self.colConditions.length
						th.style.borderLeftColor = "transparent";
						firstRow.appendChild(th);
					}
					
					//add a column only with the name of  measures
					if (!self.ShowMeasuresAsRows){
						var tr = OAT.Dom.create("tr");
						for(var j = 0; j < (self.pageData.columnsHeaders.length / measures.length); j++) {
							for(var m = 0; m < measures.length; m++) {
								var th = OAT.Dom.create("th", {}, "h2titlewhite");
								th.colSpan = 1;
								th.innerHTML = measures[m].attributes.getNamedItem("displayName").nodeValue;
								self.setTitleTexrtAlign(th, th.innerHTML);
								tr.appendChild(th);
							}
						}
						self.appendRowToTable(tbody, tr, true);
					}
					//end of column only with the name of  measures
				}
				
				
				
				// first connector
				if((self.rowConditions.length && self.options.headingBefore) && (self.colConditions.length === 0)) {
					self._drawRowConditionsHeadings(tbody);
				} else if ((self.rowConditions.length==0) && (self.options.headingBefore) && (self.colConditions.length === 0) ){
					try {
						self._drawRowConditionsHeadings(tbody);
					} catch (Error) {}
				}
				
				var several_totals = measures.length > 1;
				var colSpan;
				if(several_totals) {
					colSpan = self.rowConditions.length - (measures.length - 1);
				} else {
					colSpan = self.rowConditions.length;
				}

				
				var lineaEnBlanco = false;
				//for all rows
				for(var i = 0; i < self.pageData.rows.length; i++) {
					
					var tr = OAT.Dom.create("tr");
					var row = self.pageData.rows[i];
					// row header values
					for(var j = 0; j < row.headers.length; j++) {
							//add collapse image option
							var item = row.headers[j]
							
							if (item.rowSpan > 0){
								var th = OAT.Dom.create("th", {}, "even");
							
								th.rowSpan = item.rowSpan
															
								th.innerHTML = self.dimensionPictureValue(item.value.trim(), self.rowConditions[j]);
								//picture for columns
								th = self.applyFormatValues(th, item.value, self.rowConditions[j]);
								//format for columns 
								//event handlers
								self.setClickEventHandlers(th, item.value, "DIMENSION", self.rowConditions[j], {cell: j, row: row});
								if ((j < self.rowConditions.length - (measures.length-1) -1) && (!self.ShowMeasuresAsRows)){
									var itemCollapsed = (self.conditions[self.rowConditions[j]].collapsedValues.indexOf(item.value) != -1)
									th = self.addExpandCollapseFunctionality(th, item, j, !itemCollapsed, true);
								}
								lineaEnBlanco = false;	
								tr.appendChild(th);
							}
						
					}
					
					//add blank spaces when row is collapsed
					if (row.headers.length < self.rowConditions.length - (measures.length-1)){
						var collapsedCells = self.rowConditions.length - (measures.length-1) - row.headers.length;
						for (var cc = 0; cc < collapsedCells; cc++){
							var th = OAT.Dom.create("th", {}, "even");
							th.innerHTML = ""
							tr.appendChild(th);
						} 
					}
					
					// blank space before
					if ((self.rowConditions.length - (measures.length - 1) == 0) && (i > 0) && (measures.length > 0)){
						lineaEnBlanco = true;
					}
	
					if (self.ShowMeasuresAsRows){
						// add cell with measure name 
						var td = OAT.Dom.create("td", {}, "even");
						
						var measureTitle = self.getMeasureName(row.dataField, measures);
						td.innerHTML = measureTitle
						td.style.textAlign = "left"
						self.setClickEventHandlers(td, measureTitle, "MEASURE", getMeasureNumberByName(measureTitle, measures), "GrandTotal");
						
						tr.appendChild(td);
						// end cell with measure name 
					}
					
					// initial "crude data" part of a row, only the data of the measures shows in one column
					var mesauresList = new Array();

					var measureslength = measures.length;
					if(measureslength === 0) {
						measureslength = 1;
					}

					if (!row.subTotal){ //value cells
						for(var ji = 0; ji < row.cells.length; ji++) {
							var value = row.cells[ji].value;
							
							if ((self.colConditions.length > 0) && (self.pageData.columnsHeaders[ji].subTotal)){ //subtotal or total column
								var td = OAT.Dom.create("td", {}, "subtotal");
								
								if(value != self.EmptyValue) {
									var mN = self.getMeasureNumber(row.cells[ji].dataField, measures);
								
									td.innerHTML = self.defaultPictureValue(value, false, mN);
									td = self.applyConditionalFormats(td, value, false, mN);
									self.setClickEventHandlers(td, value, "MEASURE", mN, {cell: ji, row: row});
								} else {
									td.innerHTML = "";
								}
								tr.appendChild(td);
							} else { //normal crude data
								var td = OAT.Dom.create("td", {}, "even");
	
								if(value != self.EmptyValue) {
									var mN = self.getMeasureNumber(row.cells[ji].dataField, measures);
									
									td.innerHTML = self.defaultPictureValue(value, false, mN);
									td = self.applyConditionalFormats(td, value, false, mN);
									self.setClickEventHandlers(td, value, "MEASURE", mN, {cell: ji, row: row});
								} else {
									td.innerHTML = "";
								}
								tr.appendChild(td);
							}
						}
					} else {
						//subtotal or total row
						var tr = OAT.Dom.create("tr");
							
						if(row.headers.length == 0) {
							//grand Total
							var th = OAT.Dom.create("th", {}, "h2subtitle");
							th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
							th.colSpan = colSpan;
							if (self.ShowMeasuresAsRows){
								if (row.rowSpan > 0){
									th.rowSpan = row.rowSpan
									tr.appendChild(th);
								}
							} else {
								tr.appendChild(th);
							}
						} else {
							//sub total
							if (i==0){//append header, not totalized, only for firsr row
								for (var h=0; h < row.headers.length-1; h++){ 
									var item = row.headers[h]
									var th = OAT.Dom.create("th", {}, "even");
									th.rowSpan = item.rowSpan
									th.innerHTML = self.dimensionPictureValue(item.value.trim(), self.rowConditions[h]);
									th = self.applyFormatValues(th, item.value, self.rowConditions[h]);
									self.setClickEventHandlers(th, item.value, "DIMENSION", self.rowConditions[h], {cell: h, row: row, isTotal: false});
									tr.appendChild(th);
								}
							}
							
							var th = OAT.Dom.create("th", {}, "h2subtitle");
							th.colSpan = colSpan - (row.headers.length - 1);
							var value = row.headers[row.headers.length - 1].value.trim()
							if (value == "#NuN#"){
								th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + self.defaultPicture.getAttribute("textForNullValues");//value;
							} else {
								th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + value;
							}
							self.setClickEventHandlers(th, gx.getMessage("GXPL_QViewerJSTotalFor") + " " + item.value, "DIMENSION", getMeasureNumberByName(item.dataField, columns), ['PtrTotals', value, row]);
							if (self.ShowMeasuresAsRows){
								if (row.rowSpan > 0){
									th.rowSpan = row.rowSpan
									tr.appendChild(th);
								}
							} else {
								tr.appendChild(th);
							}
						}
						
						if (self.ShowMeasuresAsRows){
							// add cell with measure name
							var td = OAT.Dom.create("td", {}, "gtotal");
							
							var measureTitle = self.getMeasureName(row.dataField, measures);
							td.innerHTML = measureTitle
							td.style.textAlign = "left"
							//self.setClickEventHandlers(td, measureTitle, "MEASURE", getMeasureNumberByName(measureTitle, measures), "GrandTotal");
						
							tr.appendChild(td);
							// end cell with measure name
						}
						
						for(var ind = 0; ind < row.cells.length; ind++) {
							var value = row.cells[ind].value;
							
							var mN = self.getMeasureNumber(row.cells[ind].dataField, measures);
							
							var td = OAT.Dom.create("td", {}, "gtotal");
							td.innerHTML = self.defaultPictureValue(value, false, mN);
							td = self.applyConditionalFormats(td, value, false, mN);
							if(row.headers.length == 0) {
								self.setClickEventHandlers(td, value, "MEASURE", ind, 'GrandTotal');
							} else {
								self.setClickEventHandlers(td, value, "MEASURE", measureslength - 1, ['PtrTotals', value, row]);
							}
							tr.appendChild(td);
						}
					}


					self.appendRowToTable(tbody, tr, false);
					
				} // for each row

			} catch (ERROR) {
				//alert(ERROR);
			}

			
			
		} /* drawTableWhenServerPagination */

		
	
		this.drawTableWhenShowMeasuresAsRows = function() {
			OAT.Dom.clear(self.div);
			// START create toolbar table

			self.div.setAttribute("class", "conteiner_table_div");
			var myTable = OAT.Dom.create("table", {}, "");
			myTable.id = self.controlName + "_" + self.query + "_toolbar";
			var myTbody = OAT.Dom.create("tbody");
			var myRow = OAT.Dom.create("tr");
			self.div.appendChild(myTable);
			// END

			var table = OAT.Dom.create("table", {}, "pivot_table");
			table.id = this.controlName + "_" + this.query;
			//add control name and query name as the id of main table
			var tbody = OAT.Dom.create("tbody");
			
			self.drawFilters();
			self.countedRows = 0;
			if((gx.util.browser.isIE())) {
				var divIeContainer = document.createElement("div");
				divIeContainer.setAttribute("class", "divIeContainer");
				self.div.appendChild(divIeContainer);
				OAT.Dom.append([table, tbody], [divIeContainer, table]);
				table.style.marginBottom = "0px";
			} else {
				OAT.Dom.append([table, tbody], [self.div, table]);
			}

			if((self.autoPaging) && (self.allData.length < self.filteredData.length) && (self.colPointers.length > 1)) {
				self.allData = self.filteredData;
			} else if((self.autoPaging) && (self.filteredData.length > self.allData.length) && (self.colPointers.length === 0)) {
				self.filteredData = self.allData;
			}

			var firstRow;
			var firstRowTotalSpan = self.colConditions.length;
			if(self.colConditions.length > 0) {
				var i = 0;
				var tr = OAT.Dom.create("tr");
				self._drawRowConditionsHeadingsCustom(tr);
				// create a list of the dimension name, that are not pivot, with heigth span equal to   colConditions.length
				for(var ni = 0; ni < self.colConditions.length; ni++) {// creat a list of the dimension name, that are pivot as columns
					tr = self._drawColConditionsHeadingsCustom(tr, ni, (ni === self.colConditions.length - 1));
				}
				tr.setAttribute("title_row", true);
				//tbody.appendChild(tr);
				self.appendRowToTable(tbody, tr, true);
				firstRow = tr;
			}
			try {
				var _mtotalSpan = 0 - firstRowTotalSpan;
				var collapsedColInfo = [false, 0, 0] //firts son of a collapsed parent, the collapsed parent, total hidden 
				var columnsDataHide = []
				var columnsDataHideFillWithBlanck = []
				var td_temp_forCollapseInfo = false;
				var td_collection_forCollapseInfo = [];
				for (var i = 0; i < measures.length; i++){
					td_collection_forCollapseInfo[i] = false;
				}
				
				for(var i = 0; i < self.colConditions.length; i++) {
					var tr = OAT.Dom.create("tr");

					var stack = self.colPointers[i];
					var drawColumn = 0; //number of actual column being draw 
					for(var j = 0; j < stack.length; j++) {//column values here the diferents values of the pivoted dimensions
						var item = stack[j];
						var th = OAT.Dom.create("th", {}, "even");
						if(item.span === 0)
							item.span = 1;
						th.colSpan = item.span/* * (measures.length-1); */
						if (i==0){
							if(item.span === 0) {//calc for the span of the top right blanck cell
								_mtotalSpan++;
							} else {
								_mtotalSpan = _mtotalSpan + (item.span * measures.length);
							}
						}
						th.innerHTML = self.dimensionPictureValue(item.value, self.colConditions[i]);
						
						th = self.applyFormatValues(th, item.value, self.colConditions[i]);
						
						
						//find if some parent of the item is collapsed
						var tempItem = item;
						var hide = false;
						var blankcell = false; 
						while (tempItem.parent){
							if (tempItem.parent.collapsed==undefined){
								collapsedColInfo[0] = false;
								break;
							}
							if (tempItem.parent.collapsed){
								if ((!collapsedColInfo[0]) || (tempItem.parent != collapsedColInfo[1])){
									blankcell = true;
									if (i == self.colConditions.length -1){
										columnsDataHideFillWithBlanck.append(j);
									} 
								} else {
									collapsedColInfo[2]++;
								}
								if ((i == self.colConditions.length -1) && (!blankcell)) //the last col conditions
								{	
									columnsDataHide.append(j);
								}	
								collapsedColInfo[0] = true;
								collapsedColInfo[1] = tempItem.parent; 
								th.innerHTML = ""	
								hide = true;
								th.colSpan = 1;
								break;
							} else {
								tempItem = tempItem.parent; 	
							}				
						}
						
						if ((!hide)||(blankcell)){ //when no hide or when collapse add one column blank cells
							tr.appendChild(th);
						}

										
						//advance the column counter
						drawColumn = drawColumn + item.span * measures.length
					}
					if(self.options.totals && i == 0) {
						var th = OAT.Dom.create("th", {}, "h2subtitle");
						th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
						//"TOTAL" gran total
						th.rowSpan = self.colConditions.length;
						if(self.colConditions.length > 0) {
							th.colSpan = measures.length;
							_mtotalSpan = _mtotalSpan + self.colConditions.length + (measures.length - 1)
						} else {
							_mtotalSpan = _mtotalSpan + self.colConditions.length
						}
						totalSpan = self.colConditions.length
						tr.appendChild(th);
					}
					if(self.options.headingAfter) {// column headings after
						self._drawColConditionsHeadings(tr, i);
					}
					tr.setAttribute("title_row", true);
					//tbody.appendChild(tr);
					self.appendRowToTable(tbody, tr, true);
				}

				
				if(_mtotalSpan > 2) { 
					var th = OAT.Dom.create("th", {}, "h2subtitle ");
					th.innerHTML = "";
					if (_mtotalSpan < 1000) {
						th.colSpan = _mtotalSpan;
					}
					th.style.borderLeftColor = "transparent";
					firstRow.appendChild(th);
				}
				
				/* first connector */
				if((self.rowConditions.length && self.options.headingBefore) && (self.colConditions.length === 0)) {
					self._drawRowConditionsHeadings(tbody);
				}
				
				var several_totals = measures.length > 1;
				var colSpan;
				if(several_totals) {
					colSpan = self.rowConditions.length - (measures.length - 1);
				} else {
					colSpan = self.rowConditions.length;
				}

				var collapsedInfo = [false, 9999, true, 0]
				//[working under collapse item, row condition of the collapse item, first row of collapses rows, actual row condition]
				var subtotalsmeasuresList = []; for(var l=0; l<measures.length;l++){subtotalsmeasuresList[l]=[];subtotalsmeasuresList[l][0]=[];}
				/* store values to show in the grandTotal row */
				var grandtotalsmeasuresList =[]; for(var l=0;l<measures.length;l++){grandtotalsmeasuresList[l]=[];grandtotalsmeasuresList[l][0]=[];}
				
				var searchIn = []; for(var l=0; l < self.recordForFormula.length; l++){ searchIn[l] = self.recordForFormula[l];}
				var searchVal = [];
				if (self.colConditions.length > 0){
					searchVal = []; for(var l=0; l < self.allData.length; l++){ searchVal[l] = self.allData[l];}
				}
				var rowsPositions = []; //if (self.colConditions.length > 0){ for(var l=0; l < self.colConditions.length; l++){ searchIn[l] = self.colConditions[l];} }
				for(var i = 0; i < self.h; i++) {
					//actualColumnIndex = 0;
					var tr = OAT.Dom.create("tr");
					if(self.rowConditions.length) {
						var dim = self.rowConditions.length - (measures.length - 1)
						var item = self.rowPointers[dim][i];  
						/* stack has number of values equal to height of table */
						var ptrArray = [];
						var ptr = item;
						while(ptr.parent) {
							ptrArray.unshift(ptr);
							ptr = ptr.parent;
						}
					} else {
						var ptrArray = [self.rowPointers[0][0]];
					}
					
					var firstCellPositionOfthisRow = -1//the column number where is draw the cell of this row
					
					for(var j = 0; j < self.rowConditions.length - (measures.length - 1); j++) {/* row header values */
						var item = ptrArray[j];
						if((item != undefined) && (item.offset == i)) {
							if (firstCellPositionOfthisRow == -1){
								firstCellPositionOfthisRow = j
							}
							//add collapse image option
							var itemCollapsed = (self.conditions[self.rowConditions[j]].collapsedValues.indexOf(item.value) != -1);
							if (j >= (self.rowConditions.length - (measures.length - 1) - 1)){
								itemCollapsed = false;
							}

							var th = OAT.Dom.create("th", {}, "even");
							if((!collapsedInfo[0]) || (collapsedInfo[1] >= j)) {//if the line is not collapsed
								th.rowSpan = ptrArray[j].span;
								th.innerHTML = self.dimensionPictureValue(item.value, self.rowConditions[j]);
								/* picture for columns */
								th = self.applyFormatValues(th, item.value, self.rowConditions[j]);
								/* format  for columns */
								self.setClickEventHandlers(th, item.value, "DIMENSION", self.rowConditions[j], item);
	
							} else {
								th.rowSpan = 1;
								th.innerHTML = "";
								/* picture for columns */
							}

							if(collapsedInfo[1] >= j) {
								collapsedInfo[0] = itemCollapsed;
								collapsedInfo[3] = item;
								if(itemCollapsed) {
									collapsedInfo[1] = j;
								} else {
									collapsedInfo[1] = 9999;
								} 
								collapsedInfo[2] = true;
							}
						
								
							tr.appendChild(th);
						}
					}
					/* blank space before */
					var measureslength = measures.length;
					if(measureslength === 0) {
						measureslength = 1;
					}
					
					/* add cell with measure name */
					var td = OAT.Dom.create("td", {}, "even");
					var item = ptrArray[self.rowConditions.length - (measureslength - 1)];
					if(item.value != self.EmptyValue) {
						if (!collapsedInfo[0]){
							td.innerHTML = item.value
							td.style.textAlign = "left"
							self.setClickEventHandlers(td, item.value, "MEASURE", getMeasureNumberByName(measureTitle, measures), "GrandTotal");
						}
					} else {
						td.innerHTML = "";
					}
					tr.appendChild(td);
					/* end cell with measure name */
					
					/* initial "crude data" part of a row, only the data of the measures shows in one column*/
					var mesauresList = new Array();
					/* store values to lateral total */
					var lateralMeasureList = [];
					/* add cell with measure value */
					var measureTitle = item.value;
					var measureNumber = getMeasureNumberByName(measureTitle, measures);
					if (self.colConditions.length == 0){
						var itemMV = item.items[0];
						var td = OAT.Dom.create("td", {}, "even");
						if (itemMV != self.EmptyValue) {
							if (!collapsedInfo[0]){
								td.innerHTML = self.defaultPictureValue(itemMV.value, false, measureNumber);
								td = self.applyConditionalFormats(td, itemMV.value, false, measureNumber);
								
								//for total calculation
								self.setClickEventHandlers(td, itemMV.value, "MEASURE", measureNumber, [Math.floor(item.row/measures.length)]);
								if (self.formulaInfo.measureFormula[measureNumber].hasFormula){
									var formulaRow = self.getFormulaRowByCoord(item, [], measureNumber, "MeasureInRows", searchIn);
									if (formulaRow != self.EmptyValue){
										grandtotalsmeasuresList[measureNumber][0].push(formulaRow)
										subtotalsmeasuresList[measureNumber][0].push([formulaRow, -1])
								
									}
								} else {
									var saveValue = (itemMV.value != "#NuN#") ? parseFloat(itemMV.value) : "#NuN#";
									grandtotalsmeasuresList[measureNumber][0].push(saveValue)
									subtotalsmeasuresList[measureNumber][0].push([saveValue, -1])
								
								}  
							}
						} else {
							td.innerHTML = "";
						}
						if (collapsedInfo[0] && (!td_collection_forCollapseInfo[ji])){
								td_collection_forCollapseInfo[ji] = td;
						}
					
						mesauresList.append([[itemMV.value, [itemMV.row]]]);
						if(self.colConditions.length === 0) {
								tr.appendChild(td);
						}
						/* end add cell with measure value */
					} else {
						/* add cells with measures value */
						var colPointerNumber = self.colPointers.length-1
						if (self.colPointers[self.colPointers.length-1].length == 0) colPointerNumber--;
						if (self.colPointers[colPointerNumber] != undefined){  
							if (measureNumber == 0) { 
								if ( (rowsPositions.length > 0) && (!isNaN(rowsPositions[0])) ){ 
											searchVal.splice( rowsPositions[0], 1)  
								}
								rowsPositions = []; 
							}
						for (var j = 0; j < self.colPointers[colPointerNumber].length; j++){
							var columnCoord = self.colPointers[colPointerNumber][j] 
							var rowCoord = item
							
							
							var value
							if (measureNumber > 0){ 
								try {
								if (rowsPositions[j] != self.EmptyValue){
									value = [ searchVal[rowsPositions[j]][self.headerRow.indexOf(measureTitle)]   ,  rowsPositions[j] ]
								} else {
									value = self.EmptyValue;
								}
								} catch (Error){
									var t;
								}
							} else {
								value = self.getMeasureValueCoord(rowCoord, columnCoord, self.headerRow.indexOf(measureTitle), searchVal);
								if ((value != self.EmptyValue) && (measureNumber == 0)){
									rowsPositions.append(value[1]);
								} else if (measureNumber == 0) { rowsPositions.append(self.EmptyValue); }
							}
							var td = OAT.Dom.create("td", {}, "even");
							if (value != self.EmptyValue){
								td.innerHTML = self.defaultPictureValue(value[0], false, measureNumber);
								td = self.applyConditionalFormats(td, value[0], false, measureNumber);
								
								self.setClickEventHandlers(td, value[0], "MEASURE", measureNumber, [value[1]]);
								
								//for total calculation
								var subValue = parseFloat(value[0])
								if (value[0] == "#NuN#") {subValue = "#NuN#"}
								if (self.formulaInfo.measureFormula[measureNumber].hasFormula){
									subValue = self.getFormulaRowByCoord(rowCoord, columnCoord, measureNumber, "MeasureInRows", searchIn);
									if (subValue == self.EmptyValue){ subValue = NaN}
								}
								
								if (grandtotalsmeasuresList[measureNumber][j] == undefined){
									grandtotalsmeasuresList[measureNumber][j] = [subValue];
								} else {
									grandtotalsmeasuresList[measureNumber][j].push(subValue);
								}
								if (subtotalsmeasuresList[measureNumber][j] == undefined) {
									subtotalsmeasuresList[measureNumber][j] = [[subValue,-1]];
								} else {
									subtotalsmeasuresList[measureNumber][j].push([subValue, -1])
								}
								lateralMeasureList.push(subValue);
								
							} else { 
								td.innerHTML = ""
								if (grandtotalsmeasuresList[measureNumber][j] == undefined){
									grandtotalsmeasuresList[measureNumber][j] = [0];
								} else {
									grandtotalsmeasuresList[measureNumber][j].push(0);
								}
								if (subtotalsmeasuresList[measureNumber][j] == undefined) {
									subtotalsmeasuresList[measureNumber][j] = [[NaN,-1]];
								} else {
									subtotalsmeasuresList[measureNumber][j].push([NaN, -1])	
								}
							}	
							tr.appendChild(td); 
						}
						}
						/* end cells with mwasures values*/
					}
					

				if(self.colConditions.length && i == 0 && self.options.headingAfter) {/* blank space after */
						var th = OAT.Dom.create("th");
						if(!self.rowConditions.length) {
							self._drawCorner(th, true);
							th.conditionIndex = -2;
						} else {
							th.style.border = "none";
						}
						th.rowSpan = self.rowStructure.span + (self.options.totals && self.rowConditions.length ? 1 : 0);
						tr.appendChild(th);
					}
					if(collapsedInfo[0] && !collapsedInfo[2]) {

					} else {
						self.appendRowToTable(tbody, tr, false);
						//tbody.appendChild(tr);
						collapsedInfo[2] = false;
					}

					var subTotalRowNumber = self.rowConditions.length - 2;
					if((several_totals) && (self.rowConditions.length - 2 >= 0) && (ptrArray[self.rowConditions.length - 2] != undefined) && (ptrArray[self.rowConditions.length - 2].items != undefined) && (ptrArray[self.rowConditions.length - 2].items.length <= 1) && (self.colConditions.length <= 0)) {
						subTotalRowNumber = self.rowConditions.length - 2 - (measureslength - 1);
					}
					
					//add lateral total
					if ((self.options.totals && self.colConditions.length)){
						var td = OAT.Dom.create("td", {}, "total");
						total_ = 0
						if (lateralMeasureList.length > 0){
							if (self.formulaInfo.measureFormula[getMeasureNumberByName(measureTitle, measures)].hasFormula){ 
								total_ = self.calculateFormulaTotal(lateralMeasureList, getMeasureNumberByName(measureTitle, measures), "MeasureInRows")
							} else {
								total_ = lateralMeasureList.reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
								if (isNaN(total_) && (total_!="#NuN#") && (total_!="#FoE#")) total_ = 0;
							}
						}
						if (total_ != 0){
							td.innerHTML = self.defaultPictureValue(total_.toString(), false, getMeasureNumberByName(measureTitle, measures));
							td = self.applyConditionalFormats(td, total_.toString(), false, getMeasureNumberByName(measureTitle, measures));
						} else {
							td.innerHTML = ""
						}
						tr.appendChild(td);
					}
					lateralMeasureList = [];
					//add subtotals row
					if (self.options.totals) {
						if (((self.rowConditions.length - (measures.length - 1)) > 1) ){
						 var MaxDept = self.rowConditions.length - (measures.length - 1) - 2	
						 for(var relativeDim=MaxDept; relativeDim >= 0; relativeDim--){ 	
							var addSubTotal = false;
							//var relativeDim = 0 
							if (i+1 < self.rowPointers[dim].length){
								var itemTemp = self.rowPointers[dim][i+1];  
								var ptrArrayTemp = [];
								var ptrTemp = itemTemp;
								while(ptrTemp.parent) {
									ptrArrayTemp.unshift(ptrTemp);
									ptrTemp = ptrTemp.parent;
								}
								//var nexRowValue = ptrArrayTemp[relativeDim].value 
								//var actualValue = ptrArray[relativeDim].value
								//addSubTotal = (nexRowValue != actualValue) //if next value is different add subtotal
								for (var pDim = relativeDim; pDim >=0; pDim--){
									if (ptrArrayTemp[pDim].value != ptrArray[pDim].value){
										addSubTotal = true;
									}
								}
							} else {
								addSubTotal = true
							}
							
							addSubTotal = addSubTotal && (self.conditions[self.rowConditions[relativeDim]].subtotals==1)
							
							if (addSubTotal){
								for (var sumM = 0; sumM < measures.length; sumM++){ //one row for each measures
									var tr = OAT.Dom.create("tr");
								
									if (sumM == 0){								
										var th = OAT.Dom.create("th", {}, "h2subtitle");
										th.colSpan = (self.rowConditions.length - (measures.length - 1)) - relativeDim;
										th.rowSpan = measures.length
										th.innerHTML = gx.getMessage("GXPL_QViewerJSTotalFor") + " " + ptrArray[relativeDim].value;
										tr.appendChild(th);
									}
									
									//add measure name
									var td = OAT.Dom.create("td", {}, "total");
									var value = measures[sumM].getAttribute("displayName");
									td.innerHTML = value
									td.style.textAlign = "left"
									self.setClickEventHandlers(td, value, "MEASURE", sumM, "GrandTotal");
									tr.appendChild(td);
									
									//add sub total value
									if (self.colConditions.length == 0){
										//add sub-total value
										var td = OAT.Dom.create("td", {}, "total");
										var valuesToOperate = []
										for (var vt = 0; vt < subtotalsmeasuresList[sumM][0].length; vt++){
											if ((subtotalsmeasuresList[sumM][0][vt][1] == -1) || (subtotalsmeasuresList[sumM][0][vt][1] > relativeDim)){
												if ((!isNaN(subtotalsmeasuresList[sumM][0][vt][0]) || (subtotalsmeasuresList[sumM][0][vt][0]=="#NuN#")) ||  self.formulaInfo.measureFormula[sumM].hasFormula){
													valuesToOperate.push(subtotalsmeasuresList[sumM][0][vt][0])
													subtotalsmeasuresList[sumM][0][vt][1] = relativeDim   }
											}
										}
										var total_ = 0
										if (valuesToOperate.length != 0){
											if (self.formulaInfo.measureFormula[sumM].hasFormula){
												total_ = self.calculateFormulaTotal(valuesToOperate, sumM, "MeasureInRows")	
											} else {
												if (self.IsReduceNuN(valuesToOperate)) total_ = "#NuN#"; else										
										 		total_ = valuesToOperate.reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
										 		if (isNaN(total_) && (total_ != "#NuN#") && (total_ != "#FoE#")) total_ = 0;
										 	}
										}
										lateralMeasureList.push(parseFloat(total_)) 
										if (total_ != 0){
											td.innerHTML = self.defaultPictureValue(total_.toString(), false, sumM);
											td = self.applyConditionalFormats(td, total_.toString(), false, sumM);
										} else {
											td.innerHTML = ""
										}
							
										tr.appendChild(td);
							
										
									} else {
										//add sub-total values
										var colPointerNumber = self.colPointers.length-1
										if (self.colPointers[self.colPointers.length-1].length == 0) colPointerNumber--;
										if (self.colPointers[colPointerNumber] != undefined) { 
										 for (var cP = 0; cP < self.colPointers[colPointerNumber].length; cP++){
											var td = OAT.Dom.create("td", {}, "total");
											var valuesToOperate = []
											for (var vt = 0; vt < subtotalsmeasuresList[sumM][cP].length; vt++){
												if ((subtotalsmeasuresList[sumM][cP][vt][1] == -1) || (subtotalsmeasuresList[sumM][cP][vt][1] > relativeDim)){
													if ((!isNaN(subtotalsmeasuresList[sumM][cP][vt][0])) || (subtotalsmeasuresList[sumM][cP][vt][0] == "#NuN#") ||  self.formulaInfo.measureFormula[sumM].hasFormula){ 
														valuesToOperate.push(subtotalsmeasuresList[sumM][cP][vt][0])
														subtotalsmeasuresList[sumM][cP][vt][1] = relativeDim}
												}
											}
											var total_ = 0
											if (valuesToOperate.length != 0){
												if (self.formulaInfo.measureFormula[sumM].hasFormula){
													total_ = self.calculateFormulaTotal(valuesToOperate, sumM, "MeasureInRows")	
												} else {
													if (self.IsReduceNuN(valuesToOperate)) total_ = "#NuN#"; else
													total_ = valuesToOperate.reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
													if (isNaN(total_) && (total_ != "#NuN#") && (total_ != "#FoE#")) total_ = 0;
												}
											}
											if ( (total_ != 0) && ((!isNaN(total_)) || (total_ == "#NuN#") || (total_ == "#FoE#")) ) {
												if (self.formulaInfo.measureFormula[sumM].hasFormula){
													lateralMeasureList.append(valuesToOperate)
												} else {
													lateralMeasureList.push(parseFloat(total_))
												} 
												td.innerHTML = self.defaultPictureValue(total_.toString(), false, sumM);
												td = self.applyConditionalFormats(td, total_.toString(), false, sumM);
											} else {
												td.innerHTML = ""
											}
											tr.appendChild(td);
										 }
										}
										if ((self.colConditions.length)){ //add lateral total to this sub-total row
											var td = OAT.Dom.create("td", {}, "gtotal");
											var total_ = 0
											if (lateralMeasureList.length != 0){
												if (self.formulaInfo.measureFormula[sumM].hasFormula){ 
													total_ = self.calculateFormulaTotal(lateralMeasureList, sumM, "MeasureInRows")
												} else {
													if (self.IsReduceNuN(lateralMeasureList)) total_ = "#NuN#"; else
													total_ = lateralMeasureList.reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
												}
											}
											td.innerHTML = self.defaultPictureValue(total_.toString(), false, sumM);
											td = self.applyConditionalFormats(td, total_.toString(), false, sumM);
											tr.appendChild(td);
											lateralMeasureList = [];
										}
									}
									
							 		self.appendRowToTable(tbody, tr, false);
							 	}
							 }
							}	
						}
					}
				

				} /* for each row */

			

			/* code for the last row, GRAND TOTAL ROW */
			/* GRAND TOTAL ROW	*/
			if((measures.length > 0) && ((self.rowConditions.length - measures.length + 1) >0)){

				if((self.options.totals && self.rowConditions.length) && ((!self.autoPaging) || (self.TotalPagesPaging == self.actualPaginationPage) || (self.FilterByTopFilter))) {
										
					for (var m =0; m < measures.length; m++ ){ //for every measure
						
						var tr = OAT.Dom.create("tr");

						if ((colSpan != 0) && (m==0)){ //add grandTotal title cell
							var th = OAT.Dom.create("th", {}, "h2subtitle");
							th.innerHTML = gx.getMessage("GXPL_QViewerJSTotal");
							th.colSpan = colSpan;
							th.rowSpan = measures.length;
							tr.appendChild(th);
						}
						
						//add measure title cell
						var td = OAT.Dom.create("td", {}, "gtotal");
						var value = measures[m].getAttribute("displayName");
						if(/*item.*/value != self.EmptyValue) {
								td.innerHTML = value
								td.style.textAlign = "left"
								self.setClickEventHandlers(td, value, "MEASURE", m, "GrandTotal");
						} else {
							td.innerHTML = "";
						}
						tr.appendChild(td);
						
						var lateralMeasureList = [];
						if (self.colConditions.length == 0){
							//add grand total value
							var td = OAT.Dom.create("td", {}, "gtotal");
							var total_ = 0
							if (grandtotalsmeasuresList[m][0].length > 0){
								if (self.formulaInfo.measureFormula[m].hasFormula){
									total_ = self.calculateFormulaTotal(grandtotalsmeasuresList[m][0], m, "MeasureInRows")
								} else {
									if (self.IsReduceNuN(grandtotalsmeasuresList[m][0])) total_ = "#NuN#"; else
									total_ = grandtotalsmeasuresList[m][0].reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
								}
							}
							if (!isNaN(total_) || (total_=="#NuN#") || (total_=="#FoE#")){
								td.innerHTML = self.defaultPictureValue(total_.toString(), false, m);
								td = self.applyConditionalFormats(td, total_.toString(), false, m);
							} else {
								td.innerHTML = "";
							}
							
							tr.appendChild(td);
							
							//lateralMeasureList.push(parseFloat(total_))
						} else {
							//add grand total values
							var colPointerNumber = self.colPointers.length-1
							if (self.colPointers[self.colPointers.length-1].length == 0) colPointerNumber--;
							if (self.colPointers[colPointerNumber] != undefined){  
							  for (var j = 0; j < self.colPointers[colPointerNumber].length; j++){
								var td = OAT.Dom.create("td", {}, "gtotal");
								var total_ = 0
								if  ((grandtotalsmeasuresList[m][j]!=undefined) && (grandtotalsmeasuresList[m][j].length > 0)){
									if (self.formulaInfo.measureFormula[m].hasFormula){
										total_ = self.calculateFormulaTotal(grandtotalsmeasuresList[m][j], m, "MeasureInRows")
									} else {
										if (self.IsReduceNuN(grandtotalsmeasuresList[m][j])) total_ = "#NuN#"; else
										total_ = grandtotalsmeasuresList[m][j].reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
									}
								}
								if (!isNaN(total_) || (total_=="#NuN#") || (total_=="#FoE#")){
									td.innerHTML = self.defaultPictureValue(total_.toString(), false, m);
									td = self.applyConditionalFormats(td, total_.toString(), false, m);
								} else {
									td.innerHTML = "";
								}
									
								tr.appendChild(td);
								
								if (self.formulaInfo.measureFormula[m].hasFormula){
									lateralMeasureList.append(grandtotalsmeasuresList[m][j])
								} else {
									if (!isNaN(total_)){
										lateralMeasureList.push(parseFloat(total_))
									}
								} 
							  }
							}
						}
						
						//add lateral total to grand total
						if ((self.options.totals && self.colConditions.length)){
							var td = OAT.Dom.create("td", {}, "gtotal");
							var total_ = 0
							if (lateralMeasureList.length != 0){
								if (self.formulaInfo.measureFormula[m].hasFormula){ 
									total_ = self.calculateFormulaTotal(lateralMeasureList, m, "MeasureInRows")
								} else {
									if (self.IsReduceNuN(lateralMeasureList)) total_ = "#NuN#"; else
									total_ = lateralMeasureList.reduce(function(a, b){if (!isNaN(a) && !isNaN(b)) { return a+b} if (isNaN(a) && isNaN(b)){ return 0 }else if (isNaN(a)){ return b } else { return a; };  })
								}
							}
							td.innerHTML = self.defaultPictureValue(total_.toString(), false, m);
							td = self.applyConditionalFormats(td, total_.toString(), false, m);
							tr.appendChild(td);
						}
					
					
					self.appendRowToTable(tbody, tr, false);//tbody.appendChild(tr);
					}
				}

				/* second connector */
				/*if(self.rowConditions.length && self.options.headingAfter) {
					self._drawRowConditionsHeadings(tbody);
				}*/
			}

			
			
			
			} catch (ERROR) {
				//alert(ERROR);
			}
			//}
		} /* drawTableWhenShowMeasuresAsRows */
	
	
	this.IsReduceNuN = function(values){
		isNuN = false;
		for(var vft = 0; vft < values.length; vft++){
			if ((values[vft] != "#NuN#") && (values[vft] != 0)){
				return false;
			}
			if (values[vft] == "#NuN#") isNuN = true;
		}
		return isNuN;
	}
		
	this.applyFilters = function() { /* create filteredData from allData */
        self.filteredData = [];
        for (var i = 0; i < self.allData.length; i++) {
            if (self.filterOK(self.allData[i])) { self.filteredData.push(self.allData[i]); }
        }
    }

	
	this.createTempDataStructForAggStepOptimization = function(){
		try {
			for(var row = 0; row < self.GeneralDataRows.length; row++){
				for (var i = 0; i < self.GeneralDataRows[0].length; i++){
					for (var j = 0; j < self.GeneralDataRows[0].length; j++){
						if (self.TempDataStructForAggStepOptimization[self.GeneralDataRows[row][i]] == undefined) {
							self.TempDataStructForAggStepOptimization[self.GeneralDataRows[row][i]] = [];
						}
						self.TempDataStructForAggStepOptimization[self.GeneralDataRows[row][i]][self.GeneralDataRows[row][j]] = true;	
					}
				}
			}
		} catch (error) {
			//alert(error)
		}
	}
	
	this.getMeasureValue = function(item, measureNumber, colDim){
		var rowFromItem = [];
		var temp = item;
		var dimValues = [];
		
		var dimPosition = [];
		for (var i = 0; i < colDim.length - measures.length + 1; i++) {
			dimPosition[i] = colDim[colDim.length - measures.length + 1 - (i+1)];
		}
		
		
		for (var i = 0; i < dimPosition.length; i++) {
			dimValues[dimPosition[i]] = temp.value;  
			temp = temp.parent//temp.items
		}
		
		var value = ""
		var hallado = false
		var searchIn = self.allData
		if (self.filterIndexes.length > 0){ searchIn = self.filteredData }
		for (var i=0; i < searchIn.length; i++){ //allData
			var coincide = false
			for(var j=0; j < dimPosition.length; j++){
				if (searchIn[i][dimPosition[j]] == dimValues[dimPosition[j]]){
					coincide = true;
				} else {
					coincide = false;
					break;
				}	 
			}
			if (coincide){
				value = searchIn[i][colDim.length + self.filterIndexes.length - measures.length + 1 + measureNumber];
				hallado = true
				break;
			}
		}
		if (hallado)
			return value;
		else
			return self.EmptyValue;	
		//TODO: repetir la bsuqueda en GeneralDataRows
	}
	
	this.getMeasureValueCoord = function(rowsCoord, colCoord, measurePosition, searchVal){
		var rowFromItem = [];
		var dimValues = [];
		
		var dimPosition = [];
		for (var i = 0; i < self.rowConditions.length - measures.length + 1; i++) {
			dimPosition[i] = self.rowConditions[self.rowConditions.length - measures.length + 1 - (i+1)];
		}
		
		
		for (var i = self.colConditions.length-1; i >= 0; i--){
			dimValues[self.colConditions[i]] = colCoord.value;
			colCoord = colCoord.parent	
		}
		var temp = rowsCoord.parent;
		for (var i = 0; i < dimPosition.length; i++) {
			dimValues[dimPosition[i]] = temp.value;  
			temp = temp.parent//temp.items
		}
		
		var value = ""
		var hallado = false
		var numRow = 0
		var searchIn = searchVal
		
		for (var i=0; i < searchIn.length; i++){
			var coincide = false
			for(var j=0; j < dimValues.length; j++){
				if (searchIn[i][j] == dimValues[j]){
					coincide = true;
				} else {
					if ((dimValues[j] == undefined) && (self.filterIndexes.length > 0)){
						coincide = true;
					} else {
						coincide = false;
						break;
					}
				}	 
			}
			if (coincide){
				value = searchIn[i][measurePosition];
				numRow = i;
				hallado = true;
				//coinciden.append(i);
				break;
			}
		}
		if (hallado){
			//for (var pC = 0; pC < coinciden.length; pC++){
				//searchVal.splice(coinciden[pC], 1)
			//}
			return [value, numRow];
		} else
			return self.EmptyValue;	
		//TODO: repetir la bsuqueda en GeneralDataRows
	}
	
	this.getFormulaRowByCoord = function(rowsCoord, colCoord, measureNumber, caseId, tosearch){
	
		//if ( caseId == "MeasureInRows"){}
	
		var rowFromItem = [];
		var dimValues = [];
		
		var dimPosition = [];
		for (var i = 0; i < self.rowConditions.length - measures.length + 1; i++) {
			dimPosition[i] = self.rowConditions[self.rowConditions.length - measures.length + 1 - (i+1)];
		}
		
		if (colCoord){
			for (var i = self.colConditions.length-1; i >= 0; i--){
				dimValues[self.colConditions[i]] = colCoord.value;
				colCoord = colCoord.parent	
			}
		}
		var temp = rowsCoord.parent;
		for (var i = 0; i < dimPosition.length; i++) {
			dimValues[dimPosition[i]] = temp.value;  
			temp = temp.parent//temp.items
		}
		
		for(var i = 0; i < self.filterIndexes.length; i++){
			var s = self.filterDiv.selects[i];
			var val = OAT.$v(s)
			if (val == "[all]"/*""*/){
				dimValues[self.filterIndexes[i]] = undefined
			} else {
				dimValues[self.filterIndexes[i]] = val
			}
		}
		
		var value = ""
		var hallado = false
		var numRow = 0
		if (tosearch != undefined) { searchIn = tosearch} 
		else { 
			var searchIn = self.recordForFormula
		}
		var coinciden = [];
		//if (self.filterIndexes.length > 0){ searchIn = self.filteredData }
		var addedValues = []; for(var o = 0; o < self.formulaInfo.recordDataLength; o++){addedValues[o] = 0}
		for (var i=0; i < searchIn.length; i++){
			var coincide = false
			for(var j=0; j < dimValues.length; j++){
				if (searchIn[i][j] == dimValues[j]){
					coincide = true;
				} else {
					if ((dimValues[j] == undefined) && (self.filterIndexes.indexOf(j) != -1)){
						coincide = true;
					} else {
						coincide = false;
						break;
					}
				}	 
			}
			if (coincide){
				for (var t = 0; t < self.formulaInfo.measureFormula[measureNumber].relatedMeasures.length; t++){
					var pos = self.formulaInfo.measureFormula[measureNumber].relatedMeasures[t]
					if (searchIn[i][pos] == undefined){
						if (addedValues[pos] == 0) addedValues[pos] = "#NuN#"; 
					} else {
						if (addedValues[pos] == "#NuN#") addedValues[pos] = 0;
						addedValues[pos] = addedValues[pos] + parseFloat(searchIn[i][pos]);
					} 
				}
				hallado = true
				coinciden.append(i);
			}
		}
		if (hallado){
			if (self.formulaInfo.cantFormulaMeasures == 1){
				for (var pC = 0; pC < coinciden.length; pC++){
					tosearch.splice(coinciden[pC], 1)
				}
			}
			return addedValues;
		} else
			return self.EmptyValue;
	
	
	}
	
	
	
	this.calculateFormulaTotal = function(inputData, measureNumber, caseId){
		try {
			var addedValues = [];
			
			if (inputData.length == 0) return "#NuN#"
			 
		//if ( caseId == "MeasureInRows"){
			for(var j=0; j < inputData.length; j++){
				//if (inputData[j] == "#NuN#"){
				//	return "#NuN#";
				//}
				if ((inputData[j] != self.EmptyValue) && (inputData[j] != 0) && ((inputData[j] != "#NuN#") || (self.ShowMeasuresAsRows))){
					for (var i=0; i<inputData[j].length; i++){
						if (addedValues[i] == undefined){ addedValues[i] = 0}
						if (!isNaN(inputData[j][i]) && (inputData[j][i] != "#NuN#")){
							if (addedValues[i] == "#NuN#") addedValues[i] = 0; 
							addedValues[i] = addedValues[i] + inputData[j][i];
						}
						if (inputData[j][i] == "#NuN#" && addedValues[i] == 0){
							addedValues[i] = "#NuN#"
						}
					}
				}
			}
		//}
			if (!self.ShowMeasuresAsRows){
				if (addedValues.length == 0) return "#NuN#"
			} else {
				if (addedValues.length == 0) return NaN
			}
			var result = EvaluateExpressionPivotJs(self.formulaInfo.measureFormula[measureNumber].PolishNotation,
											   addedValues, self.formulaInfo)
			if ((result == Infinity) || isNaN(result)) {
				return "#FoE#";
			}				
			
			return result;
		} catch (Error) {
			return self.EmptyValue;
		}
		
	}
	this.addToStack = function(iterator, item, value, conditionList){
		if (self.GeneralDataRows.length > 3000)
			return true
		if (iterator < 1)
			return true;
		if (item.value == undefined)
			return true;
			
		var value_cond_row_pos = self.conditions[conditionList[iterator]].dataRowPosition;
		var value_prev_cond_row_pos = self.conditions[conditionList[iterator-1]].dataRowPosition;	
		//search if the row exists
		if ((self.allData.length > 800) && (self.filterIndexes.length > 0)){
			return true
		}
		
		for (var i=0; i < self.allData.length; i++){
			if (self.allData[i][value_cond_row_pos] == value){
				if (item.value == self.allData[i][value_prev_cond_row_pos]){
					return true;
				}
			}
		}
		
		if ((!self.autoPaging) || (self.rowConditions.length < 8)){
			for (var i=0; i < self.GeneralDataRows.length; i++){
				if (self.GeneralDataRows[i][value_cond_row_pos] == value){
					if (item.value == self.GeneralDataRows[i][value_prev_cond_row_pos]){
						return true;
					}
				}
			}
		}
		return false;
	}

	this.createAggStructure = function() { /* create a multidimensional aggregation structure */
        function createPart(struct, arr, rows, tempSearchData) {
            struct.items = false;
            struct.depth = -1;
            var stack = [struct];
            var dimensionLenght = arr.length;
            if ((self.ShowMeasuresAsRows) && rows){
            	dimensionLenght -= measures.length - 1;
            }
            for (var i = 0; i < dimensionLenght; i++) { /* for all conditions */
           		/*ordenar tempSearchData*/
           		var cond = self.conditions[arr[i]];
           		
           		
           		var filaAOrdenar = self.conditions[arr[i]].dataRowPosition
           		var index = filaAOrdenar;
           		
           		var coef = cond.sort;
           		
           		
           		if ((i >= 1) && (!self.autoPaging) && (self.rowConditions.length > 16) && (self.GeneralDataRows.length > 1200)) {
           			cond = self.conditions[arr[i-1]];
           			filaAOrdenar = self.conditions[arr[i-1]].dataRowPosition
           			index = filaAOrdenar;
           		
           			coef = cond.sort;
           		}
           		
           		if ((coef != 0) && (coef != 2)){
           			var sortNumeric = true;
        			for (var ival = 0; ival < cond.distinctValues.length; ival++) {
        				if ((sortNumeric) && (cond.distinctValues[ival] != parseInt(cond.distinctValues[ival]))){
        					sortNumeric = false; break;
        				}
        			}
        			if (sortNumeric) {
        				tempSearchData.sort((function(index){
           					return function(a, b){
								return coef * (parseInt(a[index]) === parseInt(b[index]) ? 0 : (parseInt(a[index]) < parseInt(b[index]) ? -1 : 1));
    						};
						})(index));
        			} else {
           				tempSearchData.sort((function(index){
           					return function(a, b){
								return coef * (a[index] === b[index] ? 0 : (a[index] < b[index] ? -1 : 1));
    						};
						})(index));
					}
				}
				
           		 
           		
                 
                var newstack = [];
                
                var itemsInclude = cond.distinctValues.length;
                //if ((coef != 0) && (coef == 1)) cond.distinctValues.sort();
               
				if ((i >= 1) && (!self.autoPaging) && (self.rowConditions.length > 16) && (self.GeneralDataRows.length > 1200)) {        
					var value_cond_row_pos = self.conditions[arr[i]].dataRowPosition;  
					var newStackValues = [];     
            	   	for (var j = 0; j < stack.length; j++){
            	   		var items = [];
            	   		
            	   		for(var l = 0; l < tempSearchData.length; l++){
            	   			var crudeRow = tempSearchData[l];
            	   			
            	   			value = tempSearchData[l][value_cond_row_pos];
            	   			
            	   			var res = self.addToStack3(i, stack[j], tempSearchData[l], arr, coef, sortNumeric)
            	   			if (res == 'break') break;
            	   			
            	   			if (newStackValues.indexOf(value) == -1){
               					//stack[j] y crudeRow "coinciden"
               					
               					
               					
               					if (res){
               						newStackValues.push(value);
               					
               						var collapsed = false;
                        			if (cond.collapsedValues.indexOf(value) != -1)
                        				collapsed = true;
                        			var o = { value: value, parent: stack[j], used: false, items: false, depth: i, collapsed: collapsed, conditionNumber: arr[i]};
                        	    	items.push(o);
                            		newstack.push(o);
               						
               					}
               				}
               				
               			}
               			newStackValues = [];
               			
               		
               			stack[j].items = items;
               		}
               
               
             	} else {
                	for (var j = 0; j < stack.length; j++) { // for all items to be filled 
                	    var items = [];
                    
                		var valuePositionInSearchData = [0];    
                   	 	for (var k = 0; k < itemsInclude; k++) { // for all currently distinct values
                   			
                   			var value = cond.distinctValues[k];
                   			if (cond.blackList.find(value) == -1) { //if not in black list
                   		
                   				if (self.addToStack2(i , stack[j], value, arr, tempSearchData, valuePositionInSearchData, coef, sortNumeric)) {//search if this rows exists
                   				
                   					var collapsed = false;
                        			if (cond.collapsedValues.indexOf(value) != -1)
                        				collapsed = true;
                        			var o = { value: value, parent: stack[j], used: false, items: false, depth: i, collapsed: collapsed, conditionNumber: arr[i]};
                        	    	items.push(o);
                            		newstack.push(o);
                            	
                            	}
                        	
                        		
                        	}
                    	} // distinct values 
                    	stack[j].items = items;
					} // items in stack
			 	}
				
				 
             	stack = newstack;
            } /* conditions */
           
            
            //add measure & value row
            if ((self.ShowMeasuresAsRows) && (rows)){
            	//if (arr.length > 0){
            		var newstack = [];
            		for (var j = 0; j < stack.length; j++) { /* for all items to be filled */
                    	var items = [];
            			var itemsInclude = measures.length;
            			
            			for (var k=0; k < itemsInclude; k++) { //for all measures
            				var measureName = measures[k].getAttribute("displayName")
            				var collapsed = false;
            				var o = { value: measureName, parent: stack[j], used: false, items: false, depth: dimensionLenght, collapsed: collapsed };
            				//items.push(o);
            				var podar = false
            				if (self.colConditions.length == 0){
            					var measureValue = self.getMeasureValue(stack[j], k, arr);
            					if (measureValue == self.EmptyValue) {
            						podar = true;
            					} else {
            						var v = { value: measureValue, parent: o, used: false, items: false, depth: dimensionLenght+1, collapsed: collapsed };
            						o.items = []
            						o.items.push(v)
            					}
            				}
            				//var collapseFalse
            				if (!podar){ 
            					items.push(o)
            					if (self.colConditions.length == 0){
            						newstack.push(v);
            					} else {
            						newstack.push(o);
            					}
            				}
            			}
            			stack[j].items = items;
            		}
            		stack = newstack;
            	//}
            }
        }
		
		//create copy of self.allData
		var tempSearchData = [];
		for(var i=0; i < self.allData.length; i++){
			tempSearchData.push(self.allData[i])
		}
		
		//self.createTempDataStructForAggStepOptimization();
        createPart(self.rowStructure, self.rowConditions, true,  tempSearchData);
        createPart(self.colStructure, self.colConditions, false, tempSearchData);
        //self.TempDataStructForAggStepOptimization = [];
    }

	this.addToStack3 = function(iterator, item, row, conditionList, orden, sortNumeric){
		if (item.value == undefined)
			return true;
			
		//var value_cond_row_pos = self.conditions[conditionList[iterator]].dataRowPosition;
		var value_prev_cond_row_pos = self.conditions[conditionList[iterator-1]].dataRowPosition;
		
		if (item.value == row[value_prev_cond_row_pos]){
			
			var tempItem = item;
			var flag = true;
			for(var resto = 1; resto < iterator; resto++ ){
				var value_prev = self.conditions[conditionList[iterator-1-resto]].dataRowPosition;
				tempItem = tempItem.parent;
				if (tempItem.value != row[value_prev] ){
					flag = false;
					break;
				}
			}	
			if (flag) return true;
		
		} else {
			if ( (orden==0) || (orden == 2) ||
				 ((orden==1) && (!sortNumeric) && (row[value_prev_cond_row_pos] <= item.value)) ||
				 ((orden==-1) && (!sortNumeric) && (row[value_prev_cond_row_pos] >= item.value)) ||
				 ((orden==1) && (sortNumeric) && (parseInt(row[value_prev_cond_row_pos]) <= parseInt(item.value))) ||
				 ((orden==-1) && (sortNumeric) && (parseInt(row[value_prev_cond_row_pos]) >= parseInt(item.value))) 
				 ){
				 	return false
				 } else {
				 	return 'break'
				 }
		}
		
	}

	this.addToStack2 = function(iterator, item, value, conditionList, tempSearchData, valuePosition, orden, sortNumeric){
		if ((self.GeneralDataRows.length > 3000) && ((!self.autoPaging) /*|| (self.rowConditions.length < 8)*/))
			return true
		if (iterator < 1)
			return true;
		if (item.value == undefined)
			return true;
		
		var value_cond_row_pos = self.conditions[conditionList[iterator]].dataRowPosition;
		var value_prev_cond_row_pos = self.conditions[conditionList[iterator-1]].dataRowPosition;	
		//search if the row exists
		if ((self.allData.length > 1000) && (self.filterIndexes.length > 0) && (self.rowConditions.length < 8)){
			return true
		}
		
		var first = true;
		for (var i = valuePosition[0]; i < tempSearchData.length/*self.allData.length*/; i++){
			if ( (orden==0) || (orden == 2) ||
				 ((orden==1) && (!sortNumeric) && (tempSearchData[i][value_cond_row_pos] <= value)) ||
				 ((orden==-1) && (!sortNumeric) && (tempSearchData[i][value_cond_row_pos] >= value)) ||
				 ((orden==1) && (sortNumeric) && (parseInt(tempSearchData[i][value_cond_row_pos]) <= parseInt(value))) ||
				 ((orden==-1) && (sortNumeric) && (parseInt(tempSearchData[i][value_cond_row_pos]) >= parseInt(value))) 
				 ){
				if (tempSearchData[i][value_cond_row_pos] == value){
					if ((first) && (orden != 0) && (orden != 2)) {
						valuePosition[0] = i; first = false;
					}
					if (item.value == tempSearchData[i][value_prev_cond_row_pos]){
						var tempItem = item;
						var flag = true;
						for(var resto = 1; resto < iterator; resto++ ){
							var value_prev = self.conditions[conditionList[iterator-1-resto]].dataRowPosition;
							tempItem = tempItem.parent;
							if (tempItem.value != tempSearchData[i][value_prev] ){
								flag = false;
								break;
							}
						}
						if (flag) return true;
					}
				}
			} else {//se supero el valor
				break;
			}
		}
		
		if ((/*(!self.autoPaging) ||*/ (self.GeneralDataRows.length < 500)) //limitado evitar loop grande
				&& (self.GeneralDataRows.length > self.allData.length)){
			for (var i=0; i < self.GeneralDataRows.length; i++){
				if (self.GeneralDataRows[i][value_cond_row_pos] == value){
					if (item.value == self.GeneralDataRows[i][value_prev_cond_row_pos]){
						if (iterator > 1){
							
							var value_prev = self.conditions[conditionList[iterator-2]].dataRowPosition;
							if (item.parent.value == self.GeneralDataRows[i][value_prev] ){
								return true
							}
							
						} else {
							return true;
						}
					}
				}
			}
		}
		return false;
	}

    this.fillAggStructure = function() { /* mark used branches of aggregation structure */
        function fillPart(struct, arr, row, rowNumber) {
           if (!self.ShowMeasuresAsRows){	
            	var ptr = struct;
            	for (var i = 0; i < arr.length; i++) {
            	    var rindex = arr[i];
            	    var value = row[rindex];
            	    var o = false;
            	    for (var j = 0; j < ptr.items.length; j++) {
            	        if (ptr.items[j].value == value) { 
            	            o = ptr.items[j];
            	            ptr.items[j].row = rowNumber;
            	            break;
            	        }
            	    }
                	if (!self.autoPaging){ 
                		//if (!o) {
                		//	/*alert("Value not found in distinct?!?!? PANIC!!!"); 
                		if (o) ptr = o;
                	} else {
                		if (o) ptr = o;
                	}
            	} /* for all conditions */
            	ptr.used = true;
           } else {
           		var posTitleMeasure = row.length - measures.length;
           		row.splice(posTitleMeasure, 0, "");
           		for(var m = 0; m < measures.length; m++){
           		//agrego a la fila el valor de la medida
           			var measureName = measures[m].getAttribute("displayName")
           			row[posTitleMeasure] = measureName;
           			
					var ptr = struct;
					for (var i = 0; i < arr.length; i++) { 
            		    var rindex = arr[i];//i; //arr[i]; 
            		    var value = row[rindex];
            		    var o = false;
            		    for (var j = 0; j < ptr.items.length; j++) {
            		        if (ptr.items[j].value == value) { 
            		            o = ptr.items[j];
            		            //ptr.items[j].row = rowNumber*measures.length + m;
            		            break;
            		        }
            		    }
                		if (!self.autoPaging){ 
                			//if (!o) { 
                			//	/*alert("Value not found in distinct?!?!? PANIC!!!"); */}
                			if (o) ptr = o;
                		} else {
                			if (o) ptr = o;
                		}
            		} /* for all conditions */
					ptr.used = true;
				}
				row.splice(posTitleMeasure, 1)
           }
        }

        function fillAllPart(struct) {
            var ptr = struct;
            if (!ptr.items) {
                ptr.used = true;
                return;
            }
            for (var i = 0; i < ptr.items.length; i++) { fillAllPart(ptr.items[i]); }
        }

        if (self.options.showEmpty) {
            fillAllPart(self.rowStructure);
            fillAllPart(self.colStructure);
        } else {
            for (var i = 0; i < self.filteredData.length; i++) {
                var row = self.filteredData[i];
                fillPart(self.rowStructure, self.rowConditions, row, i);
                fillPart(self.colStructure, self.colConditions, row, i);
            }
        }
    }

    this.checkAggStructure = function() { /* check structure for empty parts and delete them */
        function check(ptrT) { /* recursive function */
       		if (!self.ShowMeasuresAsRows){
           		if (!ptrT.items) { return ptrT.used; } /* for leaves, return their usage state */
           		for (var i = ptrT.items.length - 1; i >= 0; i--) { /* if node, decide based on children count */
                	if (!check(ptrT.items[i])) { 
                		ptrT.items.splice(i, 1); 
                	}
           	 	}
           	 	return (ptrT.items.length > 0); /* return children state */
         	} else {
         		if (self.colConditions > 0){
         			var tempPt = ptrT
         			if (!tempPt.items) { return tempPt.used; }
         			var itemLg = tempPt.items.length
         			var to = 5;
         			var iterI;
         			for (iterI = itemLg - 1; iterI > -1; iterI--) 
         			{	
         				if (iterI > -1){
         					var ol = 0;
         					var resultado = check(tempPt.items[iterI]); 
         		       		if ( (resultado != undefined) && (!resultado) ) { 
                				tempPt.items.splice(iterI, 1); 
                			}
                		}
           	 		}
           	 		return (tempPt.items.length > 0);
           	 	}
         	}	
        }

        check(self.rowStructure);
        if (!self.ShowMeasuresAsRows){
        	check(self.colStructure);
        }
    }

    this.applyDefaultFormats = function(td, value) {
        var type = typeof value;
        switch (type) {
            case "number":
                value = gx.text.charReplace(value.toString(), ".", gx.decimalPoint);
                td.innerHTML = value;
                break;
            case "date":
                //alert("is a date");
                break;
        }
        return td;
    }

	this.refreshPivot = function(metadata, data, sameQuery){
		if ((metadata!= "") && (data!= "")){
			var parser = new DOMParser();
    		var xmlData = parser.parseFromString(metadata, 'text/xml');
    		var dimensions = xmlData.getElementsByTagName("OLAPDimension");
    		var index;
    		
    		//add hide elements to blacklist
    			for (var bli = 0; bli < self.conditions.length; bli++){
    				if ((self.conditions[bli]) && (self.serverPagination)){
    					var tempBlack = jQuery.extend(true, [], self.conditions[bli].blackList);
    					for (var ib = 0; ib < tempBlack.length; ib++){
    						self.createFilterInfo({ op: "pop", values: tempBlack[ib], dim: bli });
    					}	
    				}
    				self.conditions[bli].blackList = []	
    			}
    			
    			var rowPosition = []; var colPosition = [];
    			for(var dim = 0; dim < dimensions.length; dim++) { //for every dimensions of the other querie
					
					var dimID = dimensions[dim].getElementsByTagName("name")[0].childNodes[0].nodeValue; //get the name - "Identifier" of this dimension
				
					//now search for this name at this querie
					var dimPos = -1;
					for(var itC = 0; itC < self.columns.length; itC++){
						if (self.columns[itC].attributes.getNamedItem("name").nodeValue === dimID)
							dimPos = itC;		//this is the number identifier of this dimension at this pivot	
					}
					
					
					if(dimPos != -1) {//the dimension exist in this pivot
								//columns and rows dimensions and filters
								var position = dimensions[dim].getElementsByTagName("condition")[0].childNodes[0].nodeValue; //where's the dimension? row, columns, filter
								if(position === "row") {
									index = self.colConditions.find(dimPos);
									if(index != -1) { //if it is as columns, change to rows
										self.colConditions.splice(index, 1);
										if(measures.length > 1)
											self.rowConditions = [dimPos].concat(self.rowConditions);
										else
											self.rowConditions.push(dimPos);
									} else {
										index = self.filterIndexes.find(dimPos);
										if(index != -1) {
											self.filterIndexes.splice(index, 1);
											if(measures.length > 1)
												self.rowConditions = [dimPos].concat(self.rowConditions);
											else
												self.rowConditions.push(dimPos);
										}
									}
									rowPosition[parseInt(dimensions[dim].getElementsByTagName("position")[0].childNodes[0].nodeValue)] = dimPos					
								} else if(position === "col") {
									index = self.rowConditions.find(dimPos);
									if(index != -1) {
										self.rowConditions.splice(index, 1);
										if(measures.length > 1)
											self.colConditions = [dimPos].concat(self.colConditions);
										else
											self.colConditions.push(dimPos);
									} else {
										index = self.filterIndexes.find(dimPos);
										if(index != -1) {
											self.filterIndexes.splice(index, 1);
											if(measures.length > 1)
												self.colConditions = [dimPos].concat(self.colConditions);
											else
												self.colConditions.push(dimPos);
										}
									}
									colPosition[parseInt(dimensions[dim].getElementsByTagName("position")[0].childNodes[0].nodeValue)] = dimPos
								} else if(position === "none") {//to filter
									index = self.colConditions.find(dimPos);
									if(index != -1) {
										self.colConditions.splice(index, 1);
										if(measures.length > 1)
											self.filterIndexes = [dimPos].concat(self.filterIndexes);
										else
											self.filterIndexes.push(dimPos);
									} else {
										index = self.rowConditions.find(dimPos);
										if(index != -1) {
											self.rowConditions.splice(index, 1);
											if(measures.length > 1)
												self.filterIndexes = [dimPos].concat(self.filterIndexes);
											else
												self.filterIndexes.push(dimPos);
										}
									}

								}
								
								//set order value
								var order = dimensions[dim].getElementsByTagName("order")[0].childNodes[0].nodeValue;
								if (self.serverPagination){
									if (order == "descending"){
										self.conditions[dimPos].sort = -1
									} else {
										self.conditions[dimPos].sort = 1
									}
								} else {
									if (order == "descending"){
										if ((self.conditions[dimPos].sort == 0)||(self.conditions[dimPos].sort == 2)) self.conditions[dimPos].sort = 2;
        	    						else self.conditions[dimPos].sort = -1; 
        	    						self.stateChanged=true; self.sort(self.conditions[dimPos], dimPos);
        	    					} else {
        	    						if ((self.conditions[dimPos].sort == 0)||(self.conditions[dimPos].sort == 2)) self.conditions[dimPos].sort = 0 
        	    						else self.conditions[dimPos].sort = 1; 
        	    						self.stateChanged=true; self.sort(self.conditions[dimPos], dimPos); 
        	    					}
       		     				}
       		     				
       		     				//reset blacklists
								var hides = dimensions[dim].getElementsByTagName("hide")[0].childNodes;
								for(var sofs = 0; sofs < hides.length; sofs++) {
									if(hides[sofs].tagName === "value") {
										var index = self.conditions[dimPos].blackList.find(hides[sofs].textContent);
										//if not already in the list
										if(index === -1) {
											for (var t = 0; t < self.conditions[dimPos].distinctValues.length; t++){
												var trimValue = self.conditions[dimPos].distinctValues[t].toString().trim();
												if (trimValue == hides[sofs].textContent){
													if (!self.serverPagination){
														self.conditions[dimPos].blackList.append(self.conditions[dimPos].distinctValues[t]);
													} else {
														self.createFilterInfo({ op: "push", values: self.conditions[dimPos].distinctValues[t], dim: dimPos });		
													}	
												}
											} 
										}
									}
								}
								//set values of filter bars
								if (dimensions[dim].getElementsByTagName("filterdivs").length > 0){
									var fils = dimensions[dim].getElementsByTagName("filterdivs")[0].childNodes;
									for(var sofs = 0; sofs < fils.length; sofs++) {
										if(fils[sofs].tagName === "value") {
											var findex = self.filterIndexes.find(dimPos);
											if(findex != -1) {
												if(self.filterDiv.selects[findex] != undefined) {
													self.filterDiv.selects[findex].value = fils[sofs].textContent;
												}
											}
										}
									}
								}
						
					} //else the dimension not exists in this pivot

					
					
					
				}
				
				//set dimension position
				if ((self.rowConditions.length - (measures.length - 1)) == rowPosition.length){
					for (var i = 0; i < rowPosition.length; i++){
						self.rowConditions[i] = rowPosition[i];
					}
				}
				if (self.colConditions.length == colPosition.length){
					for (var i = 0; i < colPosition.length; i++){
						self.colConditions[i] = colPosition[i];
					}
				}
								
				
			if (!self.serverPagination){
    			self.go(false, true);
    		} else {
    			for (var c = 0; c < self.columns.length; c++){
    				self.pageData.AxisInfo = self.createAxisInfo(self.columns[c].getAttribute("dataField"));
    			}
    			self.pageData.CollapseInfo = self.CreateExpandCollapseInfo("");
    			
    			self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
    				if (!self.QueryViewerCollection[self.IdForQueryViewerCollection].anyError(resXML) || self.QueryViewerCollection[self.IdForQueryViewerCollection].debugServices) {
      			
          				self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows);
            			self.goWhenServerPagination(false, true);
            			
            		} else {
        				var errMsg = self.QueryViewerCollection[self.IdForQueryViewerCollection].getErrorFromText(resXML);
                   		self.QueryViewerCollection[self.IdForQueryViewerCollection].renderError(errMsg);
        		 	}
            		
        		}).closure(this), [1, self.rowsPerPage, true, self.pageData.AxisInfo, self.pageData.FilterInfo, self.pageData.CollapseInfo, true]);
    		}
		}
	}
	
	this.changePaginationRows = function(page, gonext, lastPage){
		if (self.autoPaging){
    		var tempDataRows = self.GeneralDataRows;
    		if (self.RowsWhenMoveToFilter.length > 0){ //there's no top filter dimension
    			tempDataRows = self.RowsWhenMoveToFilter;
    		}
    		
    		self.allData = [];
    		var init = 0
    		if (page > 1){
    			if (lastPage) {
    				init = tempDataRows.length - 1; 
    			} else if (gonext){
    				init = self.nextRowWhenAutopaging
    			} else {
    				init = self.prevRowWhenAutopaging	
    			}
    		}
			var end  = init+self.autoPagingRowsPerPage;
			
			var dataRows = [];
			
			if ((!this.paginationInfo) || (!this.paginationInfo.pages)) { //calculate first and last row
				createPaginationInfo(self, tempDataRows);
			}
			
			dataRows = self.paginationInfo.pages[page - 1].rows
			//search rows for blank cell when dimension in columns
    		if (self.colConditions.length > 0){
    			dataRows = self.rowsForBlankCells(dataRows);
    		}
			
			
			self.allData = dataRows;
		
			
			var prevConditions = self.conditions //save black lists
			self.conditions = [];
			for (var i = 0; i < self.headerRow.length; i++) {
            	self.pseudoInitCondition(i, prevConditions);
        	}
        	
      									
		}
	}
	
	this.rowsForBlankCells = function( rows ){
		//get dimension values on columms and rows
		var diferentColValues = [];
		var diferentRowValues = [];
		var refValuesCol = []; var refValuesRow = [];
		for (var dV = 0; dV < rows.length; dV++){
			var dimVals = [];
			var colVals = [];
			for (var cVI = 0; cVI < self.rowConditions.length - (measures.length - 1); cVI++){
				colVals[cVI] = rows[dV][self.rowConditions[cVI]];
			}
			for (var dVI = 0; dVI < self.colConditions.length; dVI++){
				dimVals[dVI] = rows[dV][self.colConditions[dVI]];
			}
			if (refValuesCol.indexOf(dimVals.toString()) == -1){
				diferentColValues.push(dimVals);
				refValuesCol.push(dimVals.toString());
			}
			if (refValuesRow.indexOf(colVals.toString()) == -1){
				diferentRowValues.push(colVals);
				refValuesRow.push(colVals.toString());
			}
		}
		
		//create partial rows from dimension values on columns and rows
		var partialRows = [];
		for (var rC = 0; rC < diferentColValues.length; rC++){
			for (var rV = 0; rV < diferentRowValues.length ; rV++){
				var partRow = [];
				for (var cVI = 0; cVI < self.rowConditions.length - (measures.length - 1); cVI++){
					partRow[self.rowConditions[cVI]] = diferentRowValues[rV][cVI]; 
				}
				for (var dVI = 0; dVI < self.colConditions.length; dVI++){
					partRow[self.colConditions[dVI]] = diferentColValues[rC][dVI]; 
				}
				partialRows.push(partRow);
			}
		}
		
		//get all needed rows
		rows = [];
		for (var a=0; a< partialRows.length; a++){
			var newRow = self.getRowFromDimensionRow(partialRows[a], self.GeneralDataRows);
			rows.push(newRow);
		}
		
		return rows;
	}
	
	this.getRowFromDimensionRow = function(row, rowCollection){
		for(var rCL = 0; rCL < rowCollection.length; rCL++){
			var same = true;
			for (var rwoI = 0; rwoI < row.length; rwoI++){
				if (row[rwoI] != undefined) {
					if (row[rwoI] != rowCollection[rCL][rwoI]){
						same = false; 
						break; 
					}
				}
			}
			if (same){
				return rowCollection[rCL]
			}
		}
		return null
	}
	
	this.countNotFiltered = function(datas){
		var total = 0;
		for (var i = 0; i < datas.length; i++){
			if (!self.notInBlackList( datas[i], self.conditions)){
				total++;
			}
		}	
		
		var rs = parseInt(total / self.autoPagingRowsPerPage); 
		if ((total % self.autoPagingRowsPerPage) != 0){
			rs++;
		}
		return rs;
	}
	
	this.setNewPagesAccount = function(cantPages){
		try {
			if (jQuery("#"+ self.controlName +" #tablePagination_totalPages").length > 0){
				jQuery("#"+ self.containerName +" #tablePagination_totalPages")[0].innerHTML = " " + cantPages;
			} else {
				jQuery("#"+ self.controlName + "_" + self.query + "_tablePagination " + "#tablePagination_totalPages")[0].innerHTML = " " + cantPages;
			}
		} catch (ERROR) {
			if (self.paginationInfo.totalPages > 1){
				var options = {
            		currPage: self.actualPaginationPage,
            		ignoreRows: jQuery('tbody tr[title_row=true]', jQuery("#" + self.controlName + "_" + self.query)),
            		optionsForRows: [10, 15, 20],
            		rowsPerPage: self.autoPagingRowsPerPage,
            		firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
            	 	prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
            	 	lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
            	 	nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),topNav: false,
            		controlName: self.controlName + "_" + self.query,
            		cantPages: self.paginationInfo.totalPages
        		}
        		
        		if (self.getGenerator() == "Ruby"){
        			pseudoPaging(options, jQuery("#" + self.controlName + "_" + self.query))
        		} else {
        			jQuery("#" + self.controlName + "_" + self.query).partialTablePagination(options);		
        		}
        		jQuery("#" + self.controlName + "_" + self.query).css( "margin-bottom", "0px");
           }
		}
	}
		
		
	this.notInBlackList = function(row, conditions){
		var esta = false;
		for (var i = 0; i < row.length; i++){
			if ( (conditions[i]!=undefined) && (conditions[i].blackList != undefined) && (conditions[i].blackList.find(row[i]) != -1)){
				esta = true;
			} 
		}
		return esta;
	}
	
	this.preGo = function(load, autorefreshflag, value, column, init){
		/*get previous state, when first load*/
		if ((init) && (rememberLayout)){
			var mState = self.getState();
			if ((mState != null) && (mState.version === self.rememberLayoutStateVersion)) {
				if ((mState.query == self.query) && (this.conditions.length == mState.conditions.length) && (mState.filterIndexes.length == 0)){
					for(var ci = 0; ci <= mState.conditions.length - 1; ci++) {
						if ((mState.conditions[ci] != false) && (self.conditions[ci] != false)){
							for (var p = 0; p < mState.conditions[ci].blackList.length; p++){
								if ((self.conditions[ci].blackList.indexOf(mState.conditions[ci].blackList[p]) == -1) &&
									(self.conditions[ci].distinctValues.indexOf(mState.conditions[ci].blackList[p]) != -1)){
										self.conditions[ci].blackList.append(mState.conditions[ci].blackList[p])
								}
							}
							self.conditions[ci].sort = mState.conditions[ci].sort;
						}
					}
					
					self.rowConditions = mState.rowConditions;
					self.colConditions = mState.colConditions;
					self.rowsPerPage   = mState.rowsPerPage;
				}
			}
		}
		/*end get state*/
		//setear datos a pivotear
		var tempDataRows;
		if (self.filterIndexes.length > 0) {
			collapseRowForCollapseValues(self);
       		tempDataRows = self.RowsWhenMoveToFilter;
        } else {
			tempDataRows = self.GeneralDataRows;
		}
		//aplicar filtros
		
		var oldDataRows = dataRows;
		dataRows = [];
		var i = 0; var j = 0;
		var tempConsiderNoNewHelper = []; 
		while ((i < this.autoPagingRowsPerPage) && (j < tempDataRows.length) ){
			if ( (column == -1) || ( (j < tempDataRows.length) && (tempDataRows[j][column] != value) ) ){ 
				
				if  (self.isNotFilterByTopFilter(tempDataRows[j]) && (!self.notInBlackList(tempDataRows[j] , self.conditions))){ //ver que no sea esta fila filtrada por ninguna condicion
					//case undefined & value
					if ((tempDataRows[j][column] == undefined) && (value == " ")){
						var isNoNew = self.canBeConsiderNoNew(tempDataRows[j], dataRows, tempConsiderNoNewHelper);
						dataRows.append([tempDataRows[j]]);
						if (!isNoNew){
							i++;
						}
					} else {
						var isNoNew = self.canBeConsiderNoNew(tempDataRows[j], dataRows, tempConsiderNoNewHelper);				
						dataRows.append([tempDataRows[j]]);
						if (!isNoNew){
							i++;
						}
					}
					
					
				}
			}
			j++; //in case there no enought data rows
		}
		
		if (dataRows.length < 1){ //case when theres no value to show
			dataRows = oldDataRows;
			this.go(false, autorefreshflag)
		}
		
		self.allData = dataRows;
		
        var prevConditions = self.conditions //save black lists
		self.conditions = [];
		
		for (var i = 0; i < self.headerRow.length; i++) { //Modificar init conditions para mantener los que no se muestran
          		self.pseudoInitCondition(i, prevConditions);
        }
        
        setTimeout( function(){ createPaginationInfo(self, self.RowsWhenMoveToFilter); }, 500 )
        
		this.go(false, autorefreshflag)
	}
	
	this.canBeConsiderNoNew = function(row, dataRows, helperList){
		if (self.colConditions.length > 0)
		{
			var notNew = false;
			for (var i = 0; i < dataRows.length; i++){
				var same = true;
				for (var j = 0; j < self.rowConditions.length; j++){
					if (dataRows[i][self.rowConditions[j]] != row[self.rowConditions[j]]){
						same = false;
					}
				}
				if (same == true){
					notNew = true;
					break;
				}
			}
		}
		if (self.filterIndexes.length > 0)//si hay dimensiones en los filtros superiores, hay filas que pueden colapsarse
		{
			var notNew = false;
			
			for (var i = 0; i < helperList.length; i++){
				var same = true;
				for (var j = 0; j < self.rowConditions.length - measures.length + 1; j++){
			
					if (helperList[i][self.rowConditions[j]] != row[self.rowConditions[j]]){
							same = false;
					}
						
				}
				if (same == true){
					notNew = true;
					break;
				}
			}
			if (!notNew){
				helperList.push(row);
			}
			
			
		}
		return notNew;
	}
	
	this.preGoWhenMoveTopFilter = function(filterIndex, initLoad){
		if (initLoad == undefined){
			for (var i = 0; i < self.filterDiv.selects.length; i++){
				self.filterDiv.selects[i].value = "[all]"
			}
		} else {
			var out = false;
			for (var i = 0; i < self.filterDiv.selects.length; i++){
				if (self.filterDiv.selects[i].value != "[all]"){
					out = true;
				}
			}
			if ((out) && (!self.autoPaging)) return;
		}
		self.FilterByTopFilter = false
		//get the index of the columns values in the data records of GeneralData, less filter index
		var dataColumns = [];
		for (var i = 0; i < columns.length; i++){
			var pos = i;//parseInt(columns[i].attributes.dataField.nodeValue.substr(1)) -1;
			if ( ((pos) != filterIndex) && (self.filterIndexes.find(pos) == -1) ) {
				dataColumns.append(pos)
			}
		}
		
		var dataRows = self.GeneralDataRows;
		
		if (!self.autoPaging){
			//remove extra data rows, that have the same columns values, except for the value of the columns index
			var tempDataRows = [];
			for (var i = 0; i < dataRows.length; i++){
			
				var existe = false;
				var previousRecord = 0;
				for(var j =0; j < tempDataRows.length; j++){
					var same = true;	
					for (var h=0; h < dataColumns.length; h++){
						if (dataRows[i][dataColumns[h]] != tempDataRows[j][dataColumns[h]]){
							
							same = false;
							break; 
						}
					}
					if (same){
						previousRecord = j;
						existe = true;
						break;
					} 
				}
				if (!existe) {
					var newRecord = [];
					for (var p=0; p < dataRows[i].length; p++)
					{
						newRecord.append([dataRows[i][p]]);
					}
					//newRecord.append([0]);
					tempDataRows.append([newRecord]); //tempDataRows.append([])
					
					for (var t = 0; t < measures.length; t++) {
						var pos;
						if (t < measures.length-1){
						  pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
						} else {
							pos = self.dataColumnIndex
						}
						if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
							var valuesToOperate = self.getFormulaRowByDataRow(dataRows[i], t, "")
							if ((valuesToOperate != self.EmptyValue) && (valuesToOperate != "#NuN#")){
								var result = self.calculateFormulaTotal([valuesToOperate], t, "MeasureInRows")
								if (!isNaN(result)){ 
									newRecord[pos] = result.toString()
								}	
							}
						}
					}
				
				} else { //increase prevoius record value
				
					//increase the entry of the column index (the last one)
					if (measures.length > 0){
						if (!self.formulaInfo.measureFormula[measures.length-1].hasFormula){
							if ((tempDataRows[previousRecord][self.dataColumnIndex] != "#NuN#") && (dataRows[i][self.dataColumnIndex] != "#NuN#")){
								tempDataRows[previousRecord][self.dataColumnIndex] = (parseFloat(tempDataRows[previousRecord][self.dataColumnIndex]) + parseFloat(dataRows[i][self.dataColumnIndex])).toString()
							} else if (tempDataRows[previousRecord][self.dataColumnIndex] == "#NuN#") {
								tempDataRows[previousRecord][self.dataColumnIndex] = dataRows[i][self.dataColumnIndex]
							}
						}
					}
					//increase the entries of other columns
					for (var t = 0; t < measures.length-1; t++) {
						var pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
						if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
							//var addedValues = self.getFormulaRowByDataRow(dataRows[i], t, "")
						} else {
							tempDataRows[previousRecord][pos] = (parseFloat(tempDataRows[previousRecord][pos]) + parseFloat(dataRows[i][pos])).toString()
						}
					}
				}
			}
			//end remove extra data rows, that have the same columns values, except for the value of the columns index
		
			self.allData = tempDataRows;
			self.conditions = [];
			for (var i = 0; i < self.headerRow.length; i++) {
        	    self.initCondition(i);
        	}
        	for (var i = 0; i < self.headerRow.length; i++) {
        		self.restoreSubtotalsAndSortLayout(i)
        	}
       } else {
       		//remove collapsed rows	
       		collapseRowForCollapseValues(self);
       		//end remove collapsed rows
       		//self.RowsWhenMoveToFilter = tempDataRows;
       		var tempDataRows2 = self.RowsWhenMoveToFilter;
       		var dataRows = [];
			var i = 0;
			var j = 0;
			
			value = null; var ConsiderNoNewHelper = [];
			while ((i < this.autoPagingRowsPerPage) && (j < tempDataRows2.length) ){
				if ((!self.notInBlackList(tempDataRows2[j] , self.conditions)) && (self.isNotFilterByTopFilter(tempDataRows2[j]))) { //ver que no sea esta fila filtrada por ninguna condicion
						//case undefined & value
						var isNoNew = self.canBeConsiderNoNew(tempDataRows2[j], dataRows, ConsiderNoNewHelper);
						dataRows.append([tempDataRows2[j]]);
						if (!isNoNew){
							i++;
						} else {
							//alert('not new')
						}
				}
				j++; //in case there no enought data rows
			}
			self.allData = dataRows;
			self.nextRowWhenAutopaging = j;
			self.prevRowWhenAutopaging = 0;
			
			var prevConditions = self.conditions //save black lists
			self.conditions = [];
		 
			for (var i = 0; i < self.headerRow.length; i++) { //Modificar init conditions para mantener los que no se muestran
           		self.pseudoInitCondition(i, prevConditions);
        	}		
       		
       		self.TotalPagesPaging = parseInt(self.RowsWhenMoveToFilter.length / self.autoPagingRowsPerPage);
    		if ( (self.RowsWhenMoveToFilter.length % self.autoPagingRowsPerPage) != 0){
    			self.TotalPagesPaging++;
    		}
       		
       		
       		setTimeout( function(){ createPaginationInfo(self, self.RowsWhenMoveToFilter); }, 500 )
       		
       }
       
	}
	
	this.isNotFilterByTopFilter = function(row){
    	for (var i = 0; i < self.filterIndexes.length; i++) { /* for all filters */
    	    var fi = self.filterIndexes[i]; /* this column is important */
         	var s = self.filterDiv.selects[i]; /* select node */
            if ((s!=undefined) && (s.selectedIndex && (OAT.$v(s) != "[all]") && (OAT.$v(s) != row[fi]) )) { return false; }
        }
        return true;
    }
	
	this.preGoWhenFilterByTopFilter = function(init){
		if (!self.autoPaging){
			self.FilterByTopFilter = true;
			self.allData = self.GeneralDataRows;
			self.conditions = [];
			
			for (var i = 0; i < self.headerRow.length; i++) {
				self.initCondition(i);
			}
        	for (var i = 0; i < self.headerRow.length; i++) {
        		self.restoreSubtotalsAndSortLayout(i)
        	}
      	} else {
      		//remove collapsed rows	
       		collapseRowForCollapseValues(self);
       		//end remove collapsed rows
      		
      		self.FilterByTopFilter = true;
			self.allData = self.GeneralDataRows;
      		
      		var tempDataRows2 = self.RowsWhenMoveToFilter//self.allData;
       		dataRows = [];
			var i = 0;
			var j = 0;
			column = -1;
			value = null; var ConsiderNoNewHelper = [];
			while ((i < this.autoPagingRowsPerPage) && (j < tempDataRows2.length) ){
				if ( (column == -1) || ( (j < tempDataRows2.length) && (tempDataRows2[j][column] != value) ) ){ 
				
					if (self.isNotFilterByTopFilter(tempDataRows2[j]) && !self.notInBlackList(tempDataRows2[j] , self.conditions)){ //ver que no sea esta fila filtrada por ninguna condicion
						var isNoNew = self.canBeConsiderNoNew(tempDataRows2[j], dataRows, ConsiderNoNewHelper);
						dataRows.append([tempDataRows2[j]]);
						if (!isNoNew){
							i++;
						} else {
							//alert('not new')
						}
					}
				}
				j++; //in case there no enought data rows
			}
			self.allData = dataRows;
			self.nextRowWhenAutopaging = j;
			self.prevRowWhenAutopaging = 0;
			//count all items to show
			/*if (!init){
				var cantRowsCount = 0;
				var tmps = []; var ConsiderNoNewHelper = [];
				for (var t = 0; t < tempDataRows2.length; t++){
					if (self.isNotFilterByTopFilter(tempDataRows2[t]) && !self.notInBlackList(tempDataRows2[t] , self.conditions)){
						var isNoNew = self.canBeConsiderNoNew(tempDataRows2[t], tmps, ConsiderNoNewHelper);				
						tmps.append([tempDataRows2[t]]);
						if (!isNoNew){
							cantRowsCount++;
						} else {
							//alert('not new')
						}
					}
				}
				self.TotalPagesPaging = parseInt(cantRowsCount / self.autoPagingRowsPerPage);
				if ( (cantRowsCount % self.autoPagingRowsPerPage) != 0){
					self.TotalPagesPaging++;
				}
			}*/
			var prevConditions = self.conditions //save black lists
			self.conditions = [];
		 
			for (var i = 0; i < self.headerRow.length; i++) { //Modificar init conditions para mantener los que no se muestran
           		self.pseudoInitCondition(i, prevConditions);
        	}
			
			setTimeout( function(){ createPaginationInfo(self, self.RowsWhenMoveToFilter); }, 500 )
			
        }
	}
	
	//call only on initial load (when autopaging only)
	//looks if are collapse values and collapse row or return false
	this.preGoWhenCollapsedValue = function(){
		//remove collapsed rows	
       	var areValuesCollapsed = collapseRowForCollapseValues(self);
       	//end remove collapsed rows
		if (!areValuesCollapsed) return false 
		
		var tempDataRows2 = self.RowsWhenMoveToFilter;
       	var dataRows = [];
		var i = 0;
		var j = 0;
		
		value = null; var ConsiderNoNewHelper = [];
		while ((i < this.autoPagingRowsPerPage) && (j < tempDataRows2.length) ){
			if (!self.notInBlackList(tempDataRows2[j] , self.conditions)){ //ver que no sea esta fila filtrada por ninguna condicion
				var isNoNew = self.canBeConsiderNoNew(tempDataRows2[j], dataRows, ConsiderNoNewHelper);
				dataRows.append([tempDataRows2[j]]);
				if (!isNoNew){
					i++;
				} else {
					//alert('not new')
				}
			}
			j++; //in case there no enought data rows
		}
		self.allData = dataRows;
		self.nextRowWhenAutopaging = j;
		self.prevRowWhenAutopaging = 0;
		var prevConditions = self.conditions //save black lists
		self.conditions = [];
		 
		for (var i = 0; i < self.headerRow.length; i++) { //Modificar init conditions para mantener los que no se muestran
        	self.pseudoInitCondition(i, prevConditions);
        }		
       		
       	self.TotalPagesPaging = parseInt(self.RowsWhenMoveToFilter.length / self.autoPagingRowsPerPage);
    	if ( (self.RowsWhenMoveToFilter.length % self.autoPagingRowsPerPage) != 0){
    		self.TotalPagesPaging++;
    	}
		
		setTimeout( function(){ createPaginationInfo(self, self.RowsWhenMoveToFilter); }, 500 )
		
		return true;
	}
	
	this.go = function(load, autorefreshflag) {    	
    				
    	if (dataRows[0] != undefined){ //if data available
    						
    	if (!load){
    		self.gd.clearSources();
        	self.gd.clearTargets();
            self.drawFilters();
        	try {
        		self.applyFilters();
        		self.createAggStructure();
        		self.fillAggStructure();
        		self.checkAggStructure();
        		self.count(); /* fill tabularData with values */
        	} catch (ERROR) {
        		//alert(ERROR)
        	}
        	if (  ((autorefreshflag === undefined) || (autorefreshflag === null) || (!autorefreshflag)) ){ 
        		//something change, call QQ
        		var meta = self.createXMLMetadata();
        		var listennings = self.QueryViewerCollection[UcId];
        		if ((listennings != "") && (listennings != null) && (listennings != undefined)){
        			listennings.onValuesChangedEvent(meta);
        		}
        	} 
		}
	
		/* get filtered selected values*/
		var filterDivSelects =  new Array();
		for (var fiv = 0; fiv < self.filterDiv.selects.length ; fiv++){
			filterDivSelects[fiv] = self.filterDiv.selects[fiv].value;
		}
		
        if (!self.firstTime) {
        					
            var state = {
                query: self.query,
                conditions: self.conditions,
                colConditions: self.colConditions,
                rowConditions: self.rowConditions,
                filterIndexes: self.filterIndexes,
                filterDivSelects: filterDivSelects,
                rowsPerPage: self.rowsPerPage,
          		version: self.rememberLayoutStateVersion
            };
            
            
            if ((!self.deleteState)){
            	self.saveState(state);
            }else{
            	self.cleanState();
            }
                        
            self.readState = true;
        } else {
        	self.firstTime = false;
        }
        
        if (!self.ShowMeasuresAsRows){
        	self.drawTable();
        } else {
        	self.drawTableWhenShowMeasuresAsRows()
        }																		
        
        
        //add paging functionality
        var actual_rowsPerPage = 0;
        if (jQuery("#" + this.controlName + "_" + self.query + "tablePagination_rowsPerPage").length > 0){
        	actual_rowsPerPage = parseInt( jQuery("#" + this.controlName + "_" + self.query + "tablePagination_rowsPerPage")[0].value );
        	if( !isNaN(actual_rowsPerPage) ){
				pageSize = actual_rowsPerPage; 
        	}
        }
        
        
        
        var options = {
            currPage: 1,
            ignoreRows: jQuery('tbody tr[title_row=true]', jQuery("#" + this.controlName + "_" + self.query)),
            optionsForRows: [10, 15, 20],
            rowsPerPage: self.rowsPerPage != 'undefined' ? self.rowsPerPage : 50,
            firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
            prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
            lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
            nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),topNav: false,
            controlName: this.controlName + "_" + self.query
        }
        if ( ((!self.autoPaging) /*|| (self.FilterByTopFilter)*/) && (pageSize)) {
            jQuery("#" + this.controlName + "_" + self.query).tablePagination(options);
            jQuery("#" + this.controlName + "_" + self.query).css( "margin-bottom", "0px");
        }  
       
                
        if ((self.autoPaging) && (self.allData.length < self.GeneralDataRows.length) && 
        	((self.filteredData.length >= self.autoPagingRowsPerPage) || (self.actualPaginationPage > 1) || ((self.paginationInfo) && (self.paginationInfo.pages.length>1)) ) 
        	) { 
        	//if pivot has less than 10 rows => no need to paging
        	var options = {
            	currPage: self.actualPaginationPage,
            	ignoreRows: jQuery('tbody tr[title_row=true]', jQuery("#" + this.controlName + "_" + self.query)),
            	optionsForRows: [10, 15, 20],
            	rowsPerPage: self.autoPagingRowsPerPage,
            	firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
             	prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
             	lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
             	nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),topNav: false,
            	controlName: this.controlName + "_" + self.query,
            	cantPages: self.TotalPagesPaging
        	}
        	
        	if (self.getGenerator() == "Ruby"){
        		pseudoPaging(options, jQuery("#" + this.controlName + "_" + self.query))
        	} else {
        		jQuery("#" + this.controlName + "_" + self.query).partialTablePagination(options);		
        	}
        	jQuery("#" + this.controlName + "_" + self.query).css( "margin-bottom", "0px");
        }
        
        if ((jQuery("#" + this.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
			jQuery("#" + this.controlName + "_" + self.query).css({width: "400px"});
		}
		var wd = jQuery("#" + this.controlName + "_" + self.query)[0].offsetWidth - 4;
		try{ 
    		if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    			wd = wd + 4;
    		}
    	} catch (Error) {
    	}
		var wd2 = jQuery("#" + this.controlName + "_" + self.query)[0].offsetWidth - 1;
		jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" }); //jQuery("#" + this.controlName + "_" + self.query + "_pivot_page").css({width: wd+"px" });
		jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px", marginBottom: "5px"});
		if ((jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + this.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
			if ( (gx.util.browser.isIE()) ){
				jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
			} else {
            	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            }
        }
        setInterval( function(){
							//verificar que sea pivot
							if ((jQuery("#" + self.controlName + "_" + self.query).length>0) && (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table")){
								if ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
         				   				if ( (gx.util.browser.isIE()) ){
											jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
										} else {
            								jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            							}
        						}
        					}
        					},
        						 			150)
		
		if ((!gx.util.browser.isIPad()) && (!gx.util.browser.isIPhone())){
				jQuery(".h2title").css({height: "25px"})
				jQuery(".h2titlewhite").css({height: "25px"})
		}
			
			
			setInterval(
				function() {
					if (jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0] != undefined){
						var defht = 21
						try{ 
    						if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    							defht = defht + 2;
    						}
    					} catch (Error) {
    					} 
						jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: defht+"px"});
						var ht = jQuery("#" + self.controlName + "_" + self.query)[0].getBoundingClientRect().top-jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].getBoundingClientRect().bottom
						ht = ht + jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].offsetHeight;
						if (ht > 28){
							jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: ht+"px"});
						}
					}
				},
				100
			)
			//if not pagination
			if ((!self.autoPaging) && ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length < 1) || (!jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").is(':visible')))){
				jQuery("#" + self.containerName + " .conteiner_table_div").css({marginBottom: "5px"});
				setTimeout( function(){
							if ((!self.autoPaging) && ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length < 1) || (!jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").is(':visible')))){
								jQuery("#" + self.containerName + " .conteiner_table_div").css({marginBottom: "5px"});
							}},
        						 			150)
        	}
        	
        //end paging functionality
        if (self.colConditions.length === 0){
        	jQuery(".h2title > div").css({width:"100%"});
        }
        
       } else {
       	 	setInterval(
				function() {
					if (jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0] != undefined){
						var defht = 21
						try{ 
    						if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    							defht = defht + 2;
    						}
    					} catch (Error) {
    					} 
						jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: defht+"px"});
						var ht = jQuery("#" + self.controlName + "_" + self.query)[0].getBoundingClientRect().top-jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].getBoundingClientRect().bottom
						ht = ht + jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].offsetHeight;
						if (ht > 28){
							jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: ht+"px"});
						}
					}
				},
				100
			)
       }
    }
    
    this.gridCache = []
    
    this.cleanGridCache = function(){
    	self.gridCache = [];
    }
    
    this.addPageToCache = function(pageNum, data){
    	self.gridCache.push({page: pageNum, data: data});
    }
    
    this.getPageDataFromCache = function(pageNum){
    	for (var it=0; it < self.gridCache.length; it++){
    		if (self.gridCache[it].page == pageNum){
    			return self.gridCache[it].data;
    		}
    	}
    	return false;
    }
    
    this.goWhenServerPagination = function(load, autorefreshflag) {    	
    	
    	if (!self.getPageDataFromCache(self.pageData.ServerPageNumber)){ //add page data to cache
    		self.addPageToCache(self.pageData.ServerPageNumber, self.pageData.rows)
    	} 
    	//call navigational events
    	if (self.pageData.ServerPageNumber != self.pageData.PreviousPageNumber){
    		self.QueryViewerCollection[self.IdForQueryViewerCollection].CurrentPage = self.pageData.ServerPageNumber;
    		if (self.pageData.ServerPageNumber == 1){
    			if ( typeof(self.QueryViewerCollection[self.IdForQueryViewerCollection].OnFirstPage) == 'function' ) self.QueryViewerCollection[self.IdForQueryViewerCollection].OnFirstPage()
    		} else if (self.pageData.ServerPageNumber == self.pageData.ServerPageCount){
    			if ( typeof(self.QueryViewerCollection[self.IdForQueryViewerCollection].OnLastPage) == 'function' ) self.QueryViewerCollection[self.IdForQueryViewerCollection].OnLastPage()
    		} else if (self.pageData.ServerPageNumber < self.pageData.PreviousPageNumber){
    			if ( typeof(self.QueryViewerCollection[self.IdForQueryViewerCollection].OnPreviousPage) == 'function' ) self.QueryViewerCollection[self.IdForQueryViewerCollection].OnPreviousPage()
    		} else {
    			if ( typeof(self.QueryViewerCollection[self.IdForQueryViewerCollection].OnNextPage) == 'function') self.QueryViewerCollection[self.IdForQueryViewerCollection].OnNextPage()
    		}
    		self.pageData.PreviousPageNumber = self.pageData.ServerPageNumber
    	}
    	
    						
    	if (!load){
    		self.gd.clearSources();
        	self.gd.clearTargets();
            self.drawFilters();
            if (self.QueryViewerCollection[self.IdForQueryViewerCollection].AutoRefreshGroup != ""){
        		if (  ((autorefreshflag === undefined) || (autorefreshflag === null) || (!autorefreshflag)) ){
        			if ((!self.firstTime) || ((self.getState()) && (self.getState().version == self.rememberLayoutStateVersion))){ 
        				//something change, call QQ
        				var meta = self.createXMLMetadata();
        				var listennings = self.QueryViewerCollection[self.IdForQueryViewerCollection];
        				if ((listennings != "") && (listennings != null) && (listennings != undefined)){
        					listennings.onValuesChangedEvent(meta);
        				}
        			}
        		}
        	}
		}
	
		/* get filtered selected values*/
		var filterDivSelects =  new Array();
		for (var fiv = 0; fiv < self.filterDiv.selects.length ; fiv++){
			filterDivSelects[fiv] = self.filterDiv.selects[fiv].value;
		}
		
        if (!self.firstTime) {
        					
            var state = {
                query: self.query,
                conditions: self.conditions,
                colConditions: self.colConditions,
                rowConditions: self.rowConditions,
                filterIndexes: self.filterIndexes,
                filterDivSelects: filterDivSelects,
                rowsPerPage: self.rowsPerPage,
                AxisInfo: self.pageData.AxisInfo,
                FilterInfo: self.pageData.FilterInfo,
          		version: self.rememberLayoutStateVersion
            };
            
            
            if ((!self.deleteState)){
            	self.saveState(state);
            }else{
            	self.cleanState();
            }
                        
            self.readState = true;
        } else {
        	self.firstTime = false;
        }
        
        self.drawTableWhenServerPagination();
        
        //add paging functionality
        var actual_rowsPerPage = 0;
       
       if ((self.pageData.ServerPageCount > 0) && ((self.pageData.ServerPageCount > 1) || (self.pageData.rows.length > 10)) 
       && (self.rowsPerPage > 0)){
       		//if pivot has less than 10 rows => no need to paging
        		var options = {
        	    	currPage: self.pageData.ServerPageNumber,
            		ignoreRows: jQuery('tbody tr[title_row=true]', jQuery("#" + this.controlName + "_" + self.query)),
            		optionsForRows: [10, 15, 20],
            		rowsPerPage: self.rowsPerPage,
            		firstArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageFirst.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/firstBlue.png', true),
            	 	prevArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PagePrevious.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/prevBlue.png', true),
            	 	lastArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageLast.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/lastBlue.png', true),
            	 	nextArrow: ((!gx.util.browser.isIPad()) && !gx.util.browser.isIPhone())?(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/PageNext.png', true):(new Image()).src = gx.util.resourceUrl(gx.basePath + gx.staticDirectory + 'QueryViewer/oatPivot/images/nextBlue.png', true),
            	 	topNav: false,
            		controlName: this.controlName + "_" + self.query,
            		cantPages: self.pageData.ServerPageCount,
            		controlUcId: UcId,
        			control: self
        		}
        		if (self.getGenerator() == "Ruby"){
        			pseudoPaging(options, jQuery("#" + this.controlName + "_" + self.query))
        		} else {
        			jQuery("#" + this.controlName + "_" + self.query).partialTablePagination(options);
        		}
            	jQuery("#" + this.controlName + "_" + self.query).css( "margin-bottom", "0px");
          		
        }
        
        if ((jQuery("#" + this.controlName + "_" + self.query)[0].clientWidth < 380) && (!shrinkToFit)){
			jQuery("#" + this.controlName + "_" + self.query).css({width: "400px"});
		}
		var wd = jQuery("#" + this.controlName + "_" + self.query)[0].offsetWidth - 4;
		try{ 
    		if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    			wd = wd + 4;
    		}
    	} catch (Error) {
    	}	
       
       
                
       
		var wd2 = jQuery("#" + this.controlName + "_" + self.query)[0].offsetWidth - 1;
		jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({width: wd+"px" }); //jQuery("#" + this.controlName + "_" + self.query + "_pivot_page").css({width: wd+"px" });
		jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({width: wd2+"px"});
		if ( (!gx.util.browser.isIE()) ){
			jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({marginBottom: "5px"});
		}
		if ((jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + this.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + this.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
			if ( (gx.util.browser.isIE()) ){
				jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
			} else {
            	jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            }
        }
        setInterval( function(){
        					if ((!gx.util.browser.isIPad()) && (!gx.util.browser.isIPhone())){
								jQuery(".h2title").css({height: "25px"})
								jQuery(".h2titlewhite").css({height: "25px"})
								jQuery(".header_value").css({height: "25px"})
								jQuery(".header_value").css({lineHeight: "25px"})
							}
							//verificar que sea pivot
							if ((jQuery("#" + self.controlName + "_" + self.query).length>0) && (jQuery("#" + self.controlName + "_" + self.query)[0].getAttribute("class") === "pivot_table")){
								if ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length>0) && ( jQuery("#" + self.controlName + "_" + self.query + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
         				   				if ( (gx.util.browser.isIE()) ){
											jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "0px"})
										} else {
            								jQuery("#" + this.controlName + "_" + self.query + "_tablePagination").css({height: "65px", marginBottom: "5px"})
            							}
        						}
        					}
        					},
        						 			150)
		
		if ((!gx.util.browser.isIPad()) && (!gx.util.browser.isIPhone())){
				jQuery(".h2title").css({height: "25px"})
				jQuery(".h2titlewhite").css({height: "25px"})
		}
			
			
		setInterval(
			function() {
				if (jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0] != undefined){
					var defht = 21
					try{ 
    					if (jQuery("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    						defht = defht + 2;
    					}
    				} catch (Error) {
    				} 
    				try{ 
					jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: defht+"px"});
					var ht = jQuery("#" + self.controlName + "_" + self.query)[0].getBoundingClientRect().top-jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].getBoundingClientRect().bottom
					ht = ht + jQuery("#" + UcId + "_" + self.query + "_pivot_page")[0].offsetHeight;
					if (ht > 28){
						jQuery("#" + UcId + "_" + self.query + "_pivot_page").css({height: ht+"px"});
					}
					} catch (Error) {}
				}
			},
			100
		)
			
			
		 if (self.pageData.ServerPageCount == 0){	
			//if not pagination
			jQuery("#" + self.containerName + " .conteiner_table_div").css({marginBottom: "5px"});
			setTimeout( function(){
						if ((jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").length < 1) 
						|| (!jQuery("#" + self.controlName + "_" + self.query + "_tablePagination_paginater").is(':visible'))){
							jQuery("#" + self.containerName + " .conteiner_table_div").css({marginBottom: "5px"});
						}},
    					150)
        
        }
        //end paging functionality
        //if (self.colConditions.length === 0){
        	//jQuery(".h2title > div").css({width:"100%"});
        //}
        
        
    }
    
    this.getDataForPivot = function(UcId, pageNumber, rowsPerPage, recalculateCantPages, AxisChangeDataField, DataFieldFilter, ExportTo, restoreDefaultView){
		if ((recalculateCantPages)) {
			self.cleanGridCache();
		}
		
		if ((recalculateCantPages) && (rowsPerPage > -1)){
			self.rowsPerPage = rowsPerPage;
		}
		self.pageData.PreviousPageNumber = self.pageData.ServerPageNumber
		if ((!recalculateCantPages) && (self.getPageDataFromCache(pageNumber))){
			self.pageData.ServerPageNumber = pageNumber
    		self.pageData.rows = self.getPageDataFromCache(self.pageData.ServerPageNumber)
    		self.preGoWhenServerPagination(); 
    	} else {
			//re-create axis info
			if (AxisChangeDataField != ""){
				self.pageData.AxisInfo = self.createAxisInfo(AxisChangeDataField);
			}
			if (DataFieldFilter != ""){
				self.createFilterInfo(DataFieldFilter);
			}
			self.pageData.CollapseInfo = self.CreateExpandCollapseInfo("");
			
			self.ExportTo = ""
			if (rowsPerPage < 0){ //for export show all pivot
				rowsPerPage = 0;
				self.ExportTo = ExportTo
			}
			
			var layoutChange = true;
			if ((pageNumber > 1) || (!recalculateCantPages)){
				layoutChange = false;
			}
			self.pageData.AxisInfo = self.createAxisInfo(AxisChangeDataField);	
			self.QueryViewerCollection[self.IdForQueryViewerCollection].getPageDataForPivotTable((function (resXML) {
                		self.pageData = OATGetNewDataFromXMLForPivot(resXML, self.pageData, self.ShowMeasuresAsRows, self.ExportTo);
                    	self.preGoWhenServerPagination();
                    	if (self.ExportTo != ""){
                    		var FileName = self.query
            				if (FileName == ""){
            					FileName = "Query"
            					try {
  									FileName = self.controlName.split("_")[0]
  								} catch (error) {}
            				}
                    		if (self.ExportTo == "HTML"){
                    			self.ExportToHTMLWhenServerPagination()
                    		}
                    		if (self.ExportTo == "PDF"){
                    			OAT.GeneratePDFOutput(self, FileName)
                    		}
                    		if (self.ExportTo == "XLS"){
                    			self.ExportToExcel(FileName);
                    		}
                    		if (self.ExportTo == "XML"){
                    			self.ExportToXMLWhenServerPagination();
                    		}
                    		if (self.ExportTo == "XLSX"){
                    			self.ExportToXLSXWhenServerPagination();
                    		}
                    		self.cleanGridCache();
                    	}
                 }).closure(this), [pageNumber, rowsPerPage, recalculateCantPages, self.pageData.AxisInfo,
                  self.pageData.FilterInfo, self.pageData.CollapseInfo, layoutChange]);
                 
         }
                       
	}		
    
    this.CreateExpandCollapseInfo = function(dataField){
    	var CollapseInfo = [];
    	var obj = {};
    	for (var i = 0; i < self.columns.length; i++){
    		var pos = -1;
    		for (var j=0; j<self.conditions.length; j++){
    			if (self.conditions[j].dataField == self.columns[i].getAttribute("dataField")){
    				pos = j;
    			}
    		}
    		if (self.conditions[pos].collapsedValues.length){
    			var notNullVal = {
    					Expanded: [],
    					Collapsed: self.conditions[pos].collapsedValues,
    					DefaultAction: "Expand"
    				}
    			var obj = { DataField: self.conditions[pos].dataField,
    					NullExpanded: true,//TODO: caso Null value
    					NotNullValues: notNullVal
    			  	  }
    			CollapseInfo.push(obj);
    		}	  	  	
    	}
    	return CollapseInfo
    }
    
    this.createAxisInfo = function(dataField){
    	var AxisInfo = [];
    	for (var i = 0; i < self.columns.length; i++){
    		var pos = -1;
    		for (var j=0; j < self.conditions.length; j++){
    			if (self.conditions[j].dataField == self.columns[i].getAttribute("dataField")){
    				pos = j;
    			}
    		}
    		var type = "Rows"; var position = 1;
    		if (self.rowConditions.indexOf(pos) > -1){
    			position = self.rowConditions.indexOf(pos)+1
    		}
    		if (self.colConditions.indexOf(pos) > -1){
    			type = "Columns";
    			position = self.colConditions.indexOf(pos)+1
    		}
    		if (self.filterIndexes.indexOf(pos) > -1){
    			type = "Pages";
    			position = self.filterIndexes.indexOf(pos)+1
    		}
			var obj = {
						DataField : self.columns[i].getAttribute("dataField"),
						Order     : (self.conditions[i].sort == 1) ? "Ascending" : "Descending",
						Axis      : {	Type: type,
										Position: position
									},
						Subtotals : ((self.conditions[i].subtotals == 1) || (self.conditions[i].subtotals)) ? true : false 
					  }
			AxisInfo.push(obj)	
		}
		return AxisInfo;
    }
    
    this.createFilterInfo = function(NewFilter, isFromMetadata){
    	DataFieldFilter = self.conditions[NewFilter.dim].dataField
    	if ((NewFilter.op == "all") || ((NewFilter.op == "pagefilter") && (NewFilter.values == "[all]"))){
    		//remove filter from filterInof
    		var pos = -1;
    		for (var p =0; p < self.pageData.FilterInfo.length; p++){
    			if (DataFieldFilter == self.pageData.FilterInfo[p].DataField) 
    			{ pos = p; break; }
    		}
    		if (pos > -1) self.pageData.FilterInfo.splice(pos, 1)
    		self.conditions[NewFilter.dim].state = "all"
    		self.conditions[NewFilter.dim].visibles = []
    		self.conditions[NewFilter.dim].blackList = []
    		self.conditions[NewFilter.dim].defaultAction = "Include"
    		return;
    	}
    	
    	if (self.conditions[NewFilter.dim].state == "none"){
    		self.conditions[NewFilter.dim].visibles = []
    		for(var u = 0; u < self.conditions[NewFilter.dim].distinctValues.length; u++){
    			if (self.conditions[NewFilter.dim].blackList.indexOf(self.conditions[NewFilter.dim].distinctValues[u]) == -1){
    				self.conditions[NewFilter.dim].blackList.push(self.conditions[NewFilter.dim].distinctValues[u])
    			} 
    		}
    	} else if (self.conditions[NewFilter.dim].state == "all"){
    		self.conditions[NewFilter.dim].blackList = []
    		for(var u = 0; u < self.conditions[NewFilter.dim].distinctValues.length; u++){
    			if (self.conditions[NewFilter.dim].visibles.indexOf(self.conditions[NewFilter.dim].distinctValues[u]) == -1){
    				self.conditions[NewFilter.dim].visibles.push(self.conditions[NewFilter.dim].distinctValues[u])
    			} 
    		}
    	}
    	
    	var notNullValue = [];
    	if (NewFilter.op == "none"){
    		notNullValue = [];
    		self.conditions[NewFilter.dim].state = "none"
    		self.conditions[NewFilter.dim].visibles = []
    		self.conditions[NewFilter.dim].blackList = []
    		self.conditions[NewFilter.dim].defaultAction = "Exclude"
    	} else {
    		if (NewFilter.op == "push"){
    			self.conditions[NewFilter.dim].state = ""
    			var pos = self.conditions[NewFilter.dim].visibles.indexOf(NewFilter.values)
    			if (pos > -1) self.conditions[NewFilter.dim].visibles.splice(pos, 1);
    			if (self.conditions[NewFilter.dim].blackList.indexOf(NewFilter.values) == -1)
    				self.conditions[NewFilter.dim].blackList.push(NewFilter.values)	
    		} else if (NewFilter.op == "pop") {
    			self.conditions[NewFilter.dim].state = ""
    			if (self.conditions[NewFilter.dim].visibles.indexOf(NewFilter.values) == -1)
    				self.conditions[NewFilter.dim].visibles.push(NewFilter.values)
    			var pos = self.conditions[NewFilter.dim].blackList.indexOf(NewFilter.values)
    			if (pos > -1) self.conditions[NewFilter.dim].blackList.splice(pos, 1);
    		} else if (NewFilter.op == "pagefilter") {
    			self.conditions[NewFilter.dim].state = ""
    			self.conditions[NewFilter.dim].defaultAction = "Exclude"
    			self.conditions[NewFilter.dim].visibles = [];
    			self.conditions[NewFilter.dim].visibles.push(NewFilter.values);
    			for(var p = 0; p < self.conditions[NewFilter.dim].distinctValues.length; p++){
    				var val = self.conditions[NewFilter.dim].distinctValues[p]
    				if ((self.conditions[NewFilter.dim].blackList.indexOf(val) == -1) && (val != NewFilter.values)){
    					self.conditions[NewFilter.dim].blackList.push(val)
    				}
    			}
    		} else if (NewFilter.op == "reverse") {
    			if  (self.conditions[NewFilter.dim].defaultAction == "Include"){ 
    				self.conditions[NewFilter.dim].defaultAction = "Exclude"
    			} else {
    				self.conditions[NewFilter.dim].defaultAction = "Include"
    			}
    			if (self.conditions[NewFilter.dim].state == "none"){//si el estado anterior es none pasa a all
    				var pos = -1;
    				for (var p =0; p < self.pageData.FilterInfo.length; p++){
    					if (DataFieldFilter == self.pageData.FilterInfo[p].DataField) 
    					{ pos = p; break; }
    				}
    				if (pos > -1) self.pageData.FilterInfo.splice(pos, 1)
    				self.conditions[NewFilter.dim].state = "all"
    				self.conditions[NewFilter.dim].visibles = []
    				self.conditions[NewFilter.dim].blackList = []
    				return;
    			} else if (self.conditions[NewFilter.dim].state == "all"){//si el estado anterior es all pasa a none
    				notNullValue = [];
    				self.conditions[NewFilter.dim].state = "none"
    				self.conditions[NewFilter.dim].visibles = []
    				self.conditions[NewFilter.dim].blackList = []
    			} else {
    				
    				var tempArrayVisibles = []; for(var tit = 0; tit < self.conditions[NewFilter.dim].visibles.length; tit++){tempArrayVisibles.push(self.conditions[NewFilter.dim].visibles[tit])}
					var tempArrayHiddens = []; for(var tit = 0; tit < self.conditions[NewFilter.dim].blackList.length; tit++){tempArrayHiddens.push(self.conditions[NewFilter.dim].blackList[tit])}
					
					self.conditions[NewFilter.dim].visibles = []
					self.conditions[NewFilter.dim].blackList = []
    		
    				for(var u = 0; u < self.conditions[NewFilter.dim].distinctValues.length; u++){
    					var val = self.conditions[NewFilter.dim].distinctValues[u];
    					if (tempArrayVisibles.indexOf(val) == -1){
    						self.conditions[NewFilter.dim].visibles.push(val)
    					} else {
    						self.conditions[NewFilter.dim].blackList.push(val)
    					}  
    				}
    				for(var u = 0; u < tempArrayHiddens.length; u++){
    					if (self.conditions[NewFilter.dim].visibles.indexOf(tempArrayHiddens[u]) == -1){
    						self.conditions[NewFilter.dim].visibles.push(tempArrayHiddens[u])
    					}
    				}
    				for(var u = 0; u < tempArrayVisibles.length; u++){
    					if (self.conditions[NewFilter.dim].blackList.indexOf(tempArrayVisibles[u]) == -1){
    						self.conditions[NewFilter.dim].blackList.push(tempArrayVisibles[u])
    					}
    				}
    			}
    		}
    	}
    	
    	var filterExist = false; var nullIncluded = true;
    	var included = [];
    	for(var t = 0; t < self.conditions[NewFilter.dim].visibles.length; t++){
    		if (self.conditions[NewFilter.dim].visibles[t] != "#NuN#"){
    			included.push(self.conditions[NewFilter.dim].visibles[t])
    		}
    	}
    	var excluded = [];
    	if (self.conditions[NewFilter.dim].state != "none"){
    		for(var t = 0; t < self.conditions[NewFilter.dim].distinctValues.length; t++){
    			var val = self.conditions[NewFilter.dim].distinctValues[t]
    			if ((val != "#NuN#") && (included.indexOf(val) == -1)){
    				excluded.push(val)
    			}
    		}
    		for(var t = 0; t < self.conditions[NewFilter.dim].blackList.length; t++){
    			if ((self.conditions[NewFilter.dim].blackList[t] != "#NuN#")
    			&& (excluded.indexOf(self.conditions[NewFilter.dim].blackList[t]) == -1)) {
    				excluded.push(self.conditions[NewFilter.dim].blackList[t])
    			}
    		}
    		if ((included.length == 0) && ((self.conditions[NewFilter.dim].defaultAction == "Exclude"))){
    			excluded = [];
    		}
    	}
    	
    	if (NewFilter.op == "none"){
    		nullIncluded = false;
    		included = []; excluded = [];
    	} else {
    		if ( (self.conditions[NewFilter.dim].distinctValues.indexOf("#NuN#") > -1) || 
    				(excluded.indexOf(self.conditions[NewFilter.dim].blackList[t]) != -1)) {
    			if (self.conditions[NewFilter.dim].visibles.indexOf("#NuN#") == -1){
    				nullIncluded = false;
    			}
    		} else {
    			if (self.conditions[NewFilter.dim].defaultAction == "Exclude"){
    				nullIncluded = false;
    			}
    		}
    	}
    	
    	if ((self.conditions[NewFilter.dim].hasNull) && (!(NewFilter.op == "none"))){	
    		//asociated psuedo-Null
    		var reallyPseudoNull = self.defaultPicture.getAttribute("textForNullValues")
    		var finded = false
    		var data_length = 0;
    		for(var u = 0; u < self.conditions[NewFilter.dim].distinctValues.length; u++){
    			data_length = self.conditions[NewFilter.dim].distinctValues[u].length;
    		}
    		for(var u = 0; u < self.conditions[NewFilter.dim].distinctValues.length; u++){
    			if (self.conditions[NewFilter.dim].distinctValues[u].trim() == self.defaultPicture.getAttribute("textForNullValues")){
    				reallyPseudoNull = self.conditions[NewFilter.dim].distinctValues[u];
    				finded = true;
    				break;
    			}
    		}
    		if (!finded){
    			for(var t=0; t<data_length-self.defaultPicture.getAttribute("textForNullValues").length; t++){
    				reallyPseudoNull = reallyPseudoNull + " ";
    			}
    		}
    		 
    		if (!nullIncluded){	
    			if (excluded.indexOf(reallyPseudoNull) == -1){
    				excluded.push(reallyPseudoNull)
    				if (self.conditions[NewFilter.dim].blackList.indexOf(reallyPseudoNull) == -1){
    					self.conditions[NewFilter.dim].blackList.push(reallyPseudoNull)
    				}
    			} 	
    			if (included.indexOf(reallyPseudoNull) != -1){
    				included.splice(included.indexOf(reallyPseudoNull),1)
    			}
    			if (self.conditions[NewFilter.dim].visibles.indexOf(reallyPseudoNull) != -1){
    				self.conditions[NewFilter.dim].visibles.splice(self.conditions[NewFilter.dim].visibles.indexOf(reallyPseudoNull),1)
    			}
    		} else {
    			if (included.indexOf(reallyPseudoNull) == -1){
    				if (excluded.indexOf(reallyPseudoNull) != -1){
    					excluded.splice(excluded.indexOf(reallyPseudoNull),1)
    					included.push(reallyPseudoNull)
    				} else {
    					if (self.conditions[NewFilter.dim].defaultAction == "Exclude"){
    						included.push(reallyPseudoNull)
    					}
    				}
    				if (self.conditions[NewFilter.dim].blackList.indexOf(reallyPseudoNull) != -1){
    					self.conditions[NewFilter.dim].blackList.splice(self.conditions[NewFilter.dim].blackList.indexOf(reallyPseudoNull),1)
    					if (self.conditions[NewFilter.dim].visibles.indexOf(reallyPseudoNull) == -1){
    						self.conditions[NewFilter.dim].visibles.push(reallyPseudoNull)
    					}
    				}
    			}	
    		}
    	}
    	
    	var allValuesLoaded = (self.conditions[NewFilter.dim].previousPage == self.conditions[NewFilter.dim].totalPages)
    	var noFilterNeeded = ( ((nullIncluded) || (!self.conditions[NewFilter.dim].hasNull)) 
    							&&  (excluded.length == 0) && (NewFilter.op != "none") && (NewFilter.op != "push")
    							&&	((self.conditions[NewFilter.dim].defaultAction == "Include") || (allValuesLoaded))
    							&&  (!isFromMetadata)
    						);
    	
    	var pos = 0; var toDelete = false;
    	for (var t = 0; t < self.pageData.FilterInfo.length; t++){
    		if (self.pageData.FilterInfo[t].DataField == DataFieldFilter){
    			filterExist = true; toDelete = true;
    			self.pageData.FilterInfo[t].NullIncluded = nullIncluded
    			self.pageData.FilterInfo[t].NotNullValues.Included = included
    			self.pageData.FilterInfo[t].NotNullValues.Excluded = excluded
    			self.pageData.FilterInfo[t].NotNullValues.DefaultAction = self.conditions[NewFilter.dim].defaultAction
    			pos = t;
    		}
    	}
    	if ((noFilterNeeded) && (toDelete)){
    		self.pageData.FilterInfo.splice(pos, 1)
    	}
    	if ((!filterExist) && (!noFilterNeeded)){
    		var notNullValues = {Included: included, Excluded: excluded, DefaultAction:self.conditions[NewFilter.dim].defaultAction}
    		filter = { DataField: DataFieldFilter, NullIncluded: nullIncluded, NotNullValues: notNullValues }
    		self.pageData.FilterInfo.push(filter);
    	}
   
    }
    
    this.moveToNextPage = function(){
    	if (this.pageData.ServerPageNumber < this.pageData.ServerPageCount){
    		this.getDataForPivot(this.UcId, this.pageData.ServerPageNumber+1, this.rowsPerPage, false, "", "", "", "")
    	}
    }
    
    this.moveToFirstPage = function(){
    	if (this.pageData.ServerPageNumber > 1){
    		this.getDataForPivot(this.UcId, 1, this.rowsPerPage, false, "", "", "", "");
    	}
    }
    
    this.moveToLastPage = function(){
    	if (this.pageData.ServerPageNumber < this.pageData.ServerPageCount){
    		this.getDataForPivot(this.UcId, this.pageData.ServerPageCount, this.rowsPerPage, false, "", "", "", "");
    	}
    }
    
    this.moveToPreviousPage = function(){
    	if (this.pageData.ServerPageNumber > 1){
    		self.getDataForPivot(UcId,  this.pageData.ServerPageNumber-1, this.rowsPerPage, false, "", "", "", "");
    	}
    }
    
    this.preGoWhenServerPagination = function(){
    	//ajustar rows condition position a la data devuelta
    	/*if (self.pageData.rows.length >	0){
    		for (var j = 0; j < self.pageData.rows[0].headers.length; j++){
    			var dataField = self.pageData.rows[0].headers[j].dataField
    			var DimNum = -1;
    			for (var d=0; d < self.conditions.length; d++){
    				if (self.conditions[d].dataField == dataField){
    					DimNum = d;
    				}
    			}
    			self.rowConditions.splice(DimNum, 1)
    		}
    	}*/
    	self.goWhenServerPagination(false, false);
    }
    
    
    this.readScrollValue = function(columnNumber){
    	var dataField =  self.conditions[columnNumber].dataField;
    	
    	if (!self.conditions[columnNumber].blocked){
    		self.conditions[columnNumber].blocked = true;
    		if (!self.conditions[columnNumber].filtered){
    			var page = self.conditions[columnNumber].previousPage + 1;
    			self.lastRequestValue = columnNumber; 
    			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
    				var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    			var res = JSON.parse(validStr);
	    			self.appendNewValueData(self.lastRequestValue, res)
				}).closure(this), [dataField, page, 10, ""]);
    		} else {
    			var ValuePageInfo  = self.conditions[columnNumber].searchInfo
    			var page = ValuePageInfo.previousPage + 1; 
    			self.lastRequestValue = dataField;
    			var filterText = ValuePageInfo.filteredText
    			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
    				var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    			var res = JSON.parse(validStr);
	    			self.appendNewFilteredValueData(res, columnNumber, filterText)
				}).closure(this), [dataField, page, 10, ValuePageInfo.filteredText]);
    		}
    	}
    }
    
    this.getValuesForColumn = function(UcId, columnNumber, filterValue){
		var dataField = self.conditions[columnNumber].dataField;
		if (filterValue != ""){
			var page = 1
			var filterValuePars = filterValue.replace(/\\/g,"\\\\")
			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
						var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    				var res = JSON.parse(validStr);
	    				self.changeValues(UcId, dataField, columnNumber, res, filterValuePars);
			}).closure(this), [dataField, page, 10, filterValuePars]);
		} else {
			self.resetScrollValue(UcId, dataField, columnNumber)
		}
	}
	
	this.appendNewValueData = function(columnNumber, data){
    	if (data.PageNumber > self.conditions[columnNumber].previousPage){
    		self.conditions[columnNumber].previousPage = data.PageNumber
    		self.conditions[columnNumber].totalPages   = data.PagesCount
    		var newValues = [];
    		
    		if (data.Null){
    			if (self.conditions[columnNumber].indexOf("#NuN#") == -1){
    				self.conditions[columnNumber].distinctValues.push("#NuN#")
    			}
    		}
    		
    		//add to differentValues
	    	for (var i = 0; i < data.NotNullValues.length; i++){
	    		var val = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
	    		if (self.conditions[columnNumber].distinctValues.indexOf(val) == -1){
	    			self.conditions[columnNumber].distinctValues.push(val)
	    			newValues.push(val)
	    		}//lo mismo
	    		if (self.conditions[columnNumber].defaultAction == "Include"){
	    			if ((self.conditions[columnNumber].visibles.indexOf(val) == -1)
	    			&& (self.conditions[columnNumber].blackList.indexOf(val) == -1)){
	    				self.conditions[columnNumber].visibles.push(val)
	    			}	
	    		} else {
	    			if ((self.conditions[columnNumber].visibles.indexOf(val) == -1)
	    			&& (self.conditions[columnNumber].blackList.indexOf(val) == -1)){
	    				self.conditions[columnNumber].blackList.push(val)
	    			}
	    		}
	    	}
	    	
	    	for (var nI = 0; nI < newValues.length; nI++){
	    		var checked = true;
	    		if (self.conditions[columnNumber].state != "all"){
	    			if (self.conditions[columnNumber].blackList.indexOf(newValues[nI]) != -1){
	    				checked = false;
	    			}
				}
				
				if (!((self.conditions[columnNumber].hasNull) && (newValues[nI].trim() == self.defaultPicture.getAttribute("textForNullValues")))){
    				self.appendNewPairToPopUp(newValues[nI], columnNumber, checked)
    			}
	    	}
    	}
    	if (self.conditions[columnNumber].previousPage < data.PagesCount)
    		self.conditions[columnNumber].blocked = false;
    }
    
    this.changeValues = function(UcId, dataField, columnNumber, data, filterText){ //when filter by search filter, delete pairs and show new ones
    	var searchInput = jQuery("#"+UcId+columnNumber)[0];
    	
    	var sInput = searchInput.value;
    	if (searchInput.value){
    		sInput = sInput.replace(/\\/g,"\\\\")
    	}
    	if (((searchInput.value) || (searchInput.value == "")) && (sInput != filterText)){
    		return;
    	} 
    	
    	self.conditions[columnNumber].filtered = true;
    	self.conditions[columnNumber].blocked = true;
    	self.removeAllPairsFromPopUp(columnNumber, data.PagesCount>1);
    	
    	//set filtered pagination info
    	self.conditions[columnNumber].searchInfo.previousPage  = 1
    	self.conditions[columnNumber].searchInfo.totalPages  = data.PagesCount
    	self.conditions[columnNumber].searchInfo.filteredText = filterText;
    	
    	for (var i = 0; i < data.NotNullValues.length; i++) {
    		var value = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
    		var alreadyInValues = (self.conditions[columnNumber].distinctValues.indexOf(value) != -1)
    		//append to different values
    		if (self.conditions[columnNumber].distinctValues.indexOf(value) == -1){
	    		self.conditions[columnNumber].distinctValues.push(value)
	    	}
	    	if ((self.conditions[columnNumber].state == "all") || 
    		  ((self.conditions[columnNumber].defaultAction == "Include") && (!alreadyInValues)) ){
    		  	//if Include new values and is a new value
	    		if ((self.conditions[columnNumber].visibles.indexOf(value) == -1) 
	    		 && (self.conditions[columnNumber].blackList.indexOf(value) == -1)){
	    			self.conditions[columnNumber].visibles.push(value)
	    		}	
	    	} 
    		//
    		
    		
    		var checked = true;
	    	if (self.conditions[columnNumber].state != "all"){
	    		if (self.conditions[columnNumber].visibles.find(value) < 0){
	    			checked = false;
	    		}
			}
    		self.conditions[columnNumber].searchInfo.values.push(value);
    		if (!((self.conditions[columnNumber].hasNull) && (value.trim() == self.defaultPicture.getAttribute("textForNullValues")))){
    			self.appendNewPairToPopUp(value, columnNumber, checked);
    		}
	    }
	    
	    if (data.PagesCount > 0)
	    	self.conditions[columnNumber].blocked = false;
    }
    
    this.resetScrollValue = function(UcId, dataField, columnNumber){ //after filtered when input serach is clean, restor values without filter
    	self.conditions[columnNumber].filtered = false;
    	self.conditions[columnNumber].blocked  = true;
    	
    	self.removeAllPairsFromPopUp(columnNumber, data.PagesCount>1);
    	
    	for(var u = 0; u < self.conditions[columnNumber].distinctValues.length; u++){
    		var checked = true;
    		var value = self.conditions[columnNumber].distinctValues[u];
	    	if (self.conditions[columnNumber].state != "all"){
	    		if (self.conditions[columnNumber].visibles.find(value) < 0){
	    			checked = false;
	    		}
			}
			
    		if (!((self.conditions[columnNumber].hasNull) && (value.trim() == self.defaultPicture.getAttribute("textForNullValues")))){
    			self.appendNewPairToPopUp(value, columnNumber, checked) 
    		}
    	}
    	
    	if (self.conditions[columnNumber].previousPage < self.conditions[columnNumber].totalPages)
    		self.conditions[columnNumber].blocked = false;
    }
    
    this.resetAllScrollValue = function(UcId){ //when closing the filter popup
    	for (var id = 0; id < self.conditions.length; id++){
    		self.conditions[id].filtered = false;
    		self.conditions[id].blocked  = true;
    		if (self.conditions[id].previousPage < self.conditions[id].totalPages)
	    		self.conditions[id].blocked  = false;
    	}
    }
    
    this.appendNewFilteredValueData = function(data, columnNumber, filterValue){ //add pairs when filtering by filter input
    	var dataField     = self.lastRequestValue
    	
    	var ValuePageInfo = self.conditions[columnNumber].searchInfo
    	if (((filterValue) || (filterValue=="")) && (ValuePageInfo.filteredText != filterValue)){
    		return;
    	}
    	if (data.PageNumber > ValuePageInfo.previousPage){
    		self.conditions[columnNumber].searchInfo.previousPage = data.PageNumber
    		self.conditions[columnNumber].searchInfo.totalPages =  data.PagesCount
    		
    		if (data.Null){
    			if (self.conditions[columnNumber].distinctValues.indexOf("#NuN#") == -1){
    				self.conditions[columnNumber].distinctValues.push("#NuN#")
    			}
    		}
    		
    		for (var i = 0; i < data.NotNullValues.length; i++){
    			var value = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
    			var alreadyInValues = (self.conditions[columnNumber].distinctValues.indexOf(value) != -1)
    			//append to different values
    			if (self.conditions[columnNumber].distinctValues.indexOf(value) == -1){
	    			self.conditions[columnNumber].distinctValues.push(value)
	    		}
    			if ((self.conditions[columnNumber].defaultAction == "Include") && (!alreadyInValues)){
	    			if ((self.conditions[columnNumber].visibles.indexOf(value) == -1)
	    			 && (self.conditions[columnNumber].blackList.indexOf(value) == -1)){
	    				self.conditions[columnNumber].visibles.push(value)
	    			} 	
	    		} else {
	    			if ((self.conditions[columnNumber].visibles.indexOf(value) == -1)
	    			 && (self.conditions[columnNumber].blackList.indexOf(value) == -1)){
	    				self.conditions[columnNumber].blackList.push(value)
	    			}
	    		}
    			    			
    			var checked = true;
	    		if (self.conditions[columnNumber].state != "all"){
	    			if (self.conditions[columnNumber].visibles.find(value) < 0){
	    				checked = false;
	    			}
				}
				if (!((self.conditions[columnNumber].hasNull) && (value.trim() == self.defaultPicture.getAttribute("textForNullValues")))){
    				self.appendNewPairToPopUp(value, columnNumber, checked)
    			}
    		}
    		if (self.conditions[columnNumber].searchInfo.previousPage < self.conditions[columnNumber].searchInfo.totalPages)
    			self.conditions[columnNumber].blocked = false;
    	}
    },
    
    this.appendNewPairToPopUp = function(value, colNumber, checked){
		var getPair = function(text, id, check) {
            var div = OAT.Dom.create("div");
            var class_check_div = self.valueIsShowed(value, colNumber)? "check_item_img": "uncheck_item_img";
            div.setAttribute("class", class_check_div);
            var ch = OAT.Dom.create("input");
            ch.type = "checkbox";
            ch.id = id;
            var t = OAT.Dom.create("label");
            t.innerHTML = text;
            t.htmlFor = id;
            div.appendChild(t);
            return [div,ch];
        }
		
		var getRefBool = function(checked, value) {
				if (self.serverPagination){
            		var oper = "pop";
                	if (!checked) {
                		oper = "push";
                	}
                
                	self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, self.conditions[colNumber].dataField , { op: oper, values: value, dim: colNumber }, "", "")
				
                	self.stateChanged=true;
                	self.onFilteredChangedEventHandleWhenServerPagination(colNumber);
                	self.EneablePivot();
                	return;
                }
        }
         
         
        var pict_value  = self.dimensionPictureValue(value, colNumber); 
		if (value == "#NuN#"){
    		pict_value = "&nbsp;"
    	}
    	pict_value = pict_value.replace(/\&amp;/g,"&").replace(/\&nbsp;/g," ")
    	if (pict_value.length > 33) {
    		var resto  = (pict_value.substring(32, pict_value.length).trim().length > 0) ? '...' : '';
        	pict_value = pict_value.substring(0, 32) + resto
    	}
        
        pict_value = pict_value.replace(/ /g, "&nbsp;") + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
		var pair = getPair(pict_value, "pivot_distinct_" + i, checked);
		pair[0].setAttribute('value', value);
		var fixHeigthDiv = jQuery("#values_"+ self.UcId + "_" + colNumber)[0]
		fixHeigthDiv.appendChild(pair[0]);
	    
	    if (fixHeigthDiv.children.length > 9){
           	fixHeigthDiv.setAttribute("class","pivot_popup_fix");
        }
	    
        OAT.Dom.attach(pair[0], "click", function(){
            									self.DiseablePivot();
            									var checked = !(this.getAttribute("class") === "check_item_img");
            									var newClass = (this.getAttribute("class") === "check_item_img")? "uncheck_item_img":"check_item_img";
            									this.setAttribute("class", newClass); 
            									getRefBool(checked, this.getAttribute("value") );//this.textContent);            														
            											 });
	}
    
    this.removeAllPairsFromPopUp = function(colNumber, withScroll){
		jQuery("#values_"+ self.UcId + "_" + colNumber).find(".check_item_img").remove()
		jQuery("#values_"+ self.UcId + "_" + colNumber).find(".uncheck_item_img").remove()
		
		jQuery(".last_div_popup .check_item_img").remove()
		jQuery(".last_div_popup .uncheck_item_img").remove()
	
		//set class of pairs container
		if (withScroll){
			jQuery("#values_"+ self.UcId + "_" + colNumber).removeClass("pivot_popup_auto");
			jQuery("#values_"+ self.UcId + "_" + colNumber).addClass("pivot_popup_fix");
    	} else {
    		jQuery("#values_"+ self.UcId + "_" + colNumber).removeClass("pivot_popup_fix");
			jQuery("#values_"+ self.UcId + "_" + colNumber).addClass("pivot_popup_auto");
    	}
	}
    
    var alreadyclicked=false;
    var alreadyclickedTimeout;
    this.onClickEventHandle = function(elemvalue, type, number, item){
    	if (alreadyclicked != 'expand'){
    		if (alreadyclicked){
    			//double click
    			alreadyclicked=false;
    			clearTimeout(alreadyclickedTimeout);
    			var datastr = self.ClickHandle(elemvalue);
    			datastr = datastr.replace(/\&/g, '&amp;');
    			this.QueryViewerCollection[IdForQueryViewerCollection].onItemClickEvent(datastr,true);
    		} else {
    			alreadyclicked=true;
            	alreadyclickedTimeout=setTimeout(function(){
            	    alreadyclicked=false;
            	    //single click
            	    var datastr = self.ClickHandle(elemvalue);
            	    datastr = datastr.replace(/\&/g, '&amp;');
       				self.QueryViewerCollection[IdForQueryViewerCollection].onItemClickEvent(datastr,false)
           		},300);      
        	}
      } else {
      			alreadyclicked=false;
      }
    }
    
    this.onClickExpandCollapse = function(elemvalue){
    	alreadyclicked='expand';
    	
    	var value  = jQuery(elemvalue).data('itemValue');
        var type   = jQuery(elemvalue).data('typeMorD');
        var number = jQuery(elemvalue).data('numberMorD');
        var item   = jQuery(elemvalue).data('itemInfo');
        if (self.conditions[number].collapsedValues.indexOf(value) == -1){
        	//collapse value
        	self.conditions[number].collapsedValues.append(value);
        	
        	if ((!self.serverPagination)){ 
        		var datastr = self.ExpandCollapseHandle(elemvalue);
        		datastr = datastr.replace(/\&/g, '&amp;');
        		self.QueryViewerCollection[IdForQueryViewerCollection].onItemExpandCollapseEvent(datastr,true)
        	} else {
        		self.ExpandCollapseHandleWhenServerPagination(elemvalue, "collapse")
        	}
        } else {
        	//expand value
        	self.conditions[number].collapsedValues.splice(self.conditions[number].collapsedValues.indexOf(value) ,1);
        	
        	if ((!self.serverPagination)){ 
        		var datastr = self.ExpandCollapseHandle(elemvalue);
        		datastr = datastr.replace(/\&/g, '&amp;');
        		self.QueryViewerCollection[IdForQueryViewerCollection].onItemExpandCollapseEvent(datastr,false)
        	} else {
        		self.ExpandCollapseHandleWhenServerPagination(elemvalue, "expande")
        	}
        }
        if (this.autoPaging){
        	collapseRowForCollapseValues(self);
        	self.actualPaginationPage = 1
    		self.changePaginationRows(1);
			self.go(false);	
        } else if (!self.serverPagination){
        	self.go(false);
        } else {
        	//self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, "", "", "", "")
        }
    }
    
    this.onDblClickEventHandle = function(elemvalue){
    	var datastr = self.ClickHandle(elemvalue);
    	datastr = datastr.replace(/\&/g, '&amp;');
    	this.QueryViewerCollection[IdForQueryViewerCollection].onItemClickEvent(datastr,true);
    }
    
    this.cleanValueForNull = function(value){
    	if (value == "#NuN#"){
    		var defaultNull = self.defaultPicture.getAttribute("textForNullValues");
    		if (defaultNull != undefined){
    			return defaultNull;
    		}
    		return "";
    	}
    	return value;
    }
    
    this.isClickedRow = function(row, value, columnOfValue, otherrow, dimension){
    	if (self.rowConditions.indexOf(dimension) == -1){
    		return true;
    	}
    	for (var col = 0; col < columnOfValue; col++){
    		var dimensionNumber = self.rowConditions[col];
    		if (row[dimensionNumber] != otherrow[dimensionNumber]){
    			return false;
    		}
    	}
    	return true;
    }
    
    this.isClickedRowMeasure = function(row, value, otherrow){
    	for (var col = 0; col < row.length; col++){
    		if ((row[col] != undefined) && (row[col] != otherrow[col])){
    			return false;
    		}
    	}
    	return true;
    }
    
    
    
    this.ClickHandle = function(elemvalue){ //TODO: add event handle for server pagination
    	var value = jQuery(elemvalue).data('itemValue');
        var type  =	jQuery(elemvalue).data('typeMorD');
        var number = jQuery(elemvalue).data('numberMorD');
        var item  =	jQuery(elemvalue).data('itemInfo');
    	var datastr = "<DATA><ITEM type=\"" + type +"\" ";
    	  	
    	 
    	if (type === "MEASURE"){
    		datastr = datastr + "name=\"" +  measures[number].getAttribute("name") + "\" ";
    		datastr = datastr + "displayName=\"" +  measures[number].getAttribute("displayName") + "\" ";
    		datastr = datastr + "location=\"data\">"
    		datastr = datastr + self.cleanValueForNull(value) 
    	} else {//DIMENSION
    		datastr = datastr + "name=\"" +  columns[number].getAttribute("name") + "\" ";
    		datastr = datastr + "displayName=\"" +  columns[number].getAttribute("displayName") + "\" ";
    		datastr = datastr + "location=\"rows\">"
    		datastr = datastr + self.cleanValueForNull(value)
    	}
    	
    	datastr = datastr + "</ITEM>";
    	//datastr = datastr + "</DATA>";
    	
    	datastr = datastr + "<CONTEXT>";
    	datastr = datastr + "<RELATED>";
    	var previuosDat = datastr;
    		try {
    			if (!self.serverPagination){
    			 if (item != ""){
    				if ((item != "GrandTotal") && ( (item[0] === undefined) || ((item[0] != "PartialGrandTotal") && (item[0] != "RowSubtotal") && (item[0] != "PtrTotals")) ) ){//regular case, simple data
    					var numRow
    					if (item.row != undefined)
    					 	numRow = item.row;
    					else
    						numRow = item[0];
    						
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						if (type === "MEASURE"){
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[numRow][i]) + "</VALUE>";
    						} else {
    							var prevValues = [];
    							for(var iCV=0; iCV < this.GeneralDataRows.length; iCV++){
    								if ((this.GeneralDataRows[iCV][number] === value) && (self.isClickedRow(this.filteredData[numRow], value, number, this.GeneralDataRows[iCV], number)) 
    								&& (prevValues.indexOf(this.GeneralDataRows[iCV][i]) === -1)
    								&& self.filterOK(this.GeneralDataRows[iCV])){
    									prevValues = prevValues.concat(this.GeneralDataRows[iCV][i])
    									datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.GeneralDataRows[iCV][i]) + "</VALUE>";
    								}
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						if (type === "MEASURE"){
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[numRow][i+this.columns.length]) + "</VALUE>";
    						} else {
    							var prevValues = [];
    							for(var iCV=0; iCV < this.allDataWithoutSort.length; iCV++){
    								if ((this.allDataWithoutSort[iCV][number] === value) && self.filterOK(this.allDataWithoutSort[iCV])
    								 && (prevValues.indexOf(this.allDataWithoutSort[iCV][i+this.columns.length]) === -1)){
    								 	prevValues = prevValues.concat(this.allDataWithoutSort[iCV][i+this.columns.length])
    									datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.allDataWithoutSort[iCV][i+this.columns.length]) + "</VALUE>";
    								}
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    				} else if (item === "GrandTotal") {	//Grand Total, include all filteredData
    					datastr = datastr;
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for(var iCV=0; iCV < this.GeneralDataRows.length; iCV++){
    							if ((prevValues.indexOf(this.GeneralDataRows[iCV][i]) === -1) && self.filterOK(this.GeneralDataRows[iCV])){
    								prevValues = prevValues.concat(this.GeneralDataRows[iCV][i])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.GeneralDataRows[iCV][i]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    				
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for (var iCV = 0; iCV < this.allDataWithoutSort.length; iCV++) {
    							if ((prevValues.indexOf(this.allDataWithoutSort[iCV][i+this.columns.length]) === -1) && self.filterOK(this.allDataWithoutSort[iCV])){
    								prevValues = prevValues.concat(this.allDataWithoutSort[iCV][i+this.columns.length])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.allDataWithoutSort[iCV][i+this.columns.length]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    				} else if (item[0] === "PartialGrandTotal"){ //Partials sums, partial totals for the last row 
    					datastr = datastr;
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ item[1][0] ][i]) + "</VALUE>";
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					var uniqueRows = [];
						jQuery.each(item[1], function(i, el){
    							if(jQuery.inArray(el, uniqueRows) === -1) uniqueRows.push(el);
						});
    					
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						for (var iCV = 0; iCV < uniqueRows.length; iCV++) {
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ uniqueRows[iCV] ][i+this.columns.length]) + "</VALUE>";
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					
    				} else if (item[0] === 'RowSubtotal'){ //partial sums, for rows distinct of the last row
    					datastr = datastr;
    					var items = item[1].items;
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    							for (var j = 0; j < items.length; j++){
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ items[j].row ][i]) + "</VALUE>";
    							}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						for (var j = 0; j < items.length; j++){
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ items[j].row ][i+this.columns.length]) + "</VALUE>";
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					
    				} else if (item[0] === 'PtrTotals'){
    					var filtRows = [];
    					for (var i = 0; i < item[1].length; i++){			//get the list of filtered rows involve in this sum
    						for (var j = 0; j < item[1][i].length; j++){
    							if (filtRows.find(item[1][i][j]) === -1){
    								filtRows.append(item[1][i][j]);
    							}
    						}
    					}
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    							var prevValues = [];
    							for (var j = 0; j < filtRows.length; j++){
    								if (prevValues.indexOf(this.filteredData[ filtRows[j] ][i]) === -1){
    									prevValues = prevValues.concat( this.filteredData[ filtRows[j] ][i])
    									datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ filtRows[j] ][i]) + "</VALUE>";
    								}
    							}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for (var iCV = 0; iCV < filtRows.length; iCV++) {
    							if (prevValues.indexOf(this.filteredData[ filtRows[iCV] ][i+this.columns.length]) === -1){
    								prevValues = prevValues.concat(this.filteredData[ filtRows[iCV] ][i+this.columns.length])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.filteredData[ filtRows[iCV] ][i+this.columns.length]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					
    				}
    				
    			}
    		 } else {
    		 	//when server pagination
    		 	
   				
   			
   				
   				if (self.allRowsPivot == "vacio"){
   					self.allRowsPivot = [] 
   					
   					var temp = self.QueryViewerCollection[self.IdForQueryViewerCollection].getPivottableData_JS();
   					var stringRecord = temp.split("<Record>")
   					for (var i = 1; i < stringRecord.length; i++) {
     					var recordData = [];
     					var fullRecordData = [];
       					for (var j = 0; j < self.pageData.dataFields.length; j++) {
       						recordData[j] = "#NuN#"
       						var dt = stringRecord[i].split("<" + self.pageData.dataFields[j] + ">")
       						if (dt.length > 1){
       							var at = dt[1].split("</" + self.pageData.dataFields[j] + ">")
       							recordData[j] = at[0]
       							fullRecordData[j] = recordData[j] 
       						} else {
       							if (stringRecord[i].indexOf("<" + self.pageData.dataFields[j] + "/>") >= 0){
       								recordData[j] = ""
       								fullRecordData[j] = ""
       							}
       						}
       					}
       					self.allRowsPivot.push(recordData);
   					}
   				}
    		 	
    		 	var isNotSubtotal = false;
    		 	if (item.row) {
    		 		isNotSubtotal = !item.row.subTotal
    		 		if ((!isNotSubtotal) && (item.isTotal != undefined) && (!item.isTotal)){
    		 			isNotSubtotal = true;
    		 		} 
    		 	}
    		 	
    		 	if ((item.row != undefined) && (isNotSubtotal)){
    		 		var row = item.row
    		 		var cellNumber = item.cell
    		 		var numRow = self.pageData.rows.indexOf(row)
    				
    				var pseudoRow = [];
    				for (var i = 0; i < self.pageData.dataFields.length; i++){
    					pseudoRow[i] = undefined;
    				}
    				for (var i = 0; i < row.headers.length; i++){
    					var pos = self.pageData.dataFields.indexOf(row.headers[i].dataField)
    					if ((row.headers[i] != undefined) &&  (row.headers[i].value != undefined)){
    						pseudoRow[pos] = row.headers[i].value
    					}
    				}
    				
    				
    				for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						if ((type === "MEASURE")){
    							if (pseudoRow[i] != undefined){
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(pseudoRow[i]) + "</VALUE>";
    							} else {
    								if (self.filterIndexes.indexOf(i) != -1){
    									var prevValues = [];
    									for(var iCV=0; iCV < self.allRowsPivot.length; iCV++){
    										if ( (prevValues.indexOf(self.allRowsPivot[iCV][i]) === -1) && (self.filterOK(self.allRowsPivot[iCV])) && 
    										self.isClickedRowMeasure(pseudoRow, value, self.allRowsPivot[iCV])){
    											prevValues = prevValues.concat(self.allRowsPivot[iCV][i])
    											datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i]) + "</VALUE>";
    										}
    									}
    								}
    							}
    						} else {
    							var prevValues = [];
    							for(var iCV=0; iCV < self.allRowsPivot.length; iCV++){
    								if ((self.allRowsPivot[iCV][number] === value) && (self.isClickedRow(pseudoRow, value, cellNumber, self.allRowsPivot[iCV], number)) 
    									&& (prevValues.indexOf(self.allRowsPivot[iCV][i]) === -1)	&& self.filterOK(self.allRowsPivot[iCV])){
    									prevValues = prevValues.concat(self.allRowsPivot[iCV][i])
    									datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i]) + "</VALUE>";
    								}
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    				}
    				
    				for (var i = 0; i < measures.length; i++){
    					datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    					datastr = datastr + "<VALUES>";
    					if (type === "MEASURE"){
    						if (pseudoRow[i+this.columns.length] != undefined){
    							datastr = datastr  + "<VALUE>" + self.cleanValueForNull(pseudoRow[i+this.columns.length]) + "</VALUE>";	
    						} else {
    							if (number == i){
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(value) + "</VALUE>";
    							} else {
    								var prevValues = [];
    								for(var iCV=0; iCV < self.allRowsPivot.length; iCV++){
    									if ((self.allRowsPivot[iCV][number+this.columns.length] === value) && (self.isClickedRowMeasure(pseudoRow, value, self.allRowsPivot[iCV])) 
    									&& self.filterOK(self.allRowsPivot[iCV]) && (prevValues.indexOf(self.allRowsPivot[iCV][i+this.columns.length]) === -1)){
    										datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i+this.columns.length]) + "</VALUE>";
    									}
    								}
    							}	
    						}
    					} else {
    						var prevValues = [];
    						for(var iCV=0; iCV < self.allRowsPivot.length; iCV++){
    							if ((self.allRowsPivot[iCV][number] === value) && (self.isClickedRow(pseudoRow, value, cellNumber, self.allRowsPivot[iCV], number))  
    							 && self.filterOK(self.allRowsPivot[iCV]) && (prevValues.indexOf(self.allRowsPivot[iCV][i+this.columns.length]) === -1)){
    							 	prevValues = prevValues.concat(self.allRowsPivot[iCV][i+this.columns.length])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i+this.columns.length]) + "</VALUE>";
    							}
    						}
    					}
    					datastr = datastr + "</VALUES>";
    					datastr = datastr + "</ITEM>";
    				}
    				
    				
    		 	} else {
    		 		if (item === "GrandTotal") {	//Grand Total, include all data
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for(var iCV=0; iCV < self.allRowsPivot.length; iCV++){
    							if ((prevValues.indexOf(self.allRowsPivot[iCV][i]) === -1) && self.filterOK(self.allRowsPivot[iCV])){
    								prevValues = prevValues.concat(self.allRowsPivot[iCV][i])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    				
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for (var iCV = 0; iCV < self.allRowsPivot.length; iCV++) {
    							if ((prevValues.indexOf(self.allRowsPivot[iCV][i+this.columns.length]) === -1) && self.filterOK(self.allRowsPivot[iCV])){
    								prevValues = prevValues.concat(self.allRowsPivot[iCV][i+this.columns.length])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(self.allRowsPivot[iCV][i+this.columns.length]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					
    				} else if (item[0] === 'PtrTotals'){
    					var row = item[2]
    					
    					var pseudoRow = [];
    					for (var i = 0; i < self.pageData.dataFields.length; i++){
    						pseudoRow[i] = undefined;
    					}
    					for (var i = 0; i < row.headers.length; i++){
    						var pos = self.pageData.dataFields.indexOf(row.headers[i].dataField)
    						if ((row.headers[i] != undefined) &&  (row.headers[i].value != undefined)){
    							pseudoRow[pos] = row.headers[i].value
    						}
    					}
    					
    					var filtRows = [];
    					for (var i = 0; i < self.allRowsPivot.length; i++){			//get the list of filtered rows involve in this sum
    						if (self.filterOK(self.allRowsPivot[i])){
    							if (self.isClickedRowMeasure(pseudoRow, value, self.allRowsPivot[i])){
    								filtRows.push(self.allRowsPivot[i]);
    							} 		
    						}
    					}
    					
    					for (var i = 0; i < this.columns.length; i++){
    						datastr = datastr + "<ITEM name=\"" + this.columns[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    							var prevValues = [];
    							for (var j = 0; j < filtRows.length; j++){
    								if (prevValues.indexOf(filtRows[j][i]) === -1){
    									prevValues = prevValues.concat( filtRows[j][i])
    									datastr = datastr  + "<VALUE>" + self.cleanValueForNull(filtRows[j][i]) + "</VALUE>";
    								}
    							}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    					
    					for (var i = 0; i < measures.length; i++){
    						datastr = datastr + "<ITEM name=\"" + measures[i].getAttribute("name") + "\">"
    						datastr = datastr + "<VALUES>";
    						var prevValues = [];
    						for (var iCV = 0; iCV < filtRows.length; iCV++) {
    							if (prevValues.indexOf(filtRows[iCV][i+this.columns.length]) === -1){
    								prevValues = prevValues.concat(filtRows[iCV][i+this.columns.length])
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(filtRows[iCV][i+this.columns.length]) + "</VALUE>";
    							}
    						}
    						datastr = datastr + "</VALUES>";
    						datastr = datastr + "</ITEM>";
    					}
    				}
    		 		
    		 		
    		 	}
    		 	
    		 }
    		} catch (Error){
    			datastr = previuosDat;
    		}
    	datastr = datastr + "</RELATED>";
    	datastr = datastr + "<FILTERS>";
    	previuosDat = datastr;
    		try {	
    			for (var i = 0; i < self.filterIndexes.length; i++){
    				datastr = datastr + "<ITEM name=\"" + this.columns[self.filterIndexes[i]].getAttribute("name") + "\" displayName=\"" + this.columns[self.filterIndexes[i]].getAttribute("displayName") + "\">";
    				datastr = datastr + "<VALUES>"
    				datastr = datastr + "<VALUE>"
    					if (self.filterDiv.selects[i].value === "[all]"){
    						datastr = datastr + gx.getMessage("GXPL_QViewerJSAllOption");//"\"ALL\"";
    					}
    					else {
    						datastr = datastr + self.cleanValueForNull(self.filterDiv.selects[i].value);
    					}
					datastr = datastr + "</VALUE>"
					datastr = datastr + "</VALUES>"
    				datastr = datastr + '</ITEM>';
    			}
    		} catch (Error){
    			datastr = previuosDat;
    		} 
    	datastr = datastr + "</FILTERS>";
    	datastr = datastr + "</CONTEXT>";
    	datastr = datastr + "</DATA>"
    	
    	return datastr;
    }
    
    
    this.ExpandCollapseHandle = function(elemvalue){
    	var value = jQuery(elemvalue).data('itemValue');
        var type  =	jQuery(elemvalue).data('typeMorD');
        var number = jQuery(elemvalue).data('numberMorD');
        var item  =	jQuery(elemvalue).data('itemInfo');
    	var datastr = "<DATA><ITEM ";
    	  	
    	datastr = datastr + "name=\"" +  columns[number].getAttribute("name") + "\" ";
    	datastr = datastr + "displayName=\"" +  columns[number].getAttribute("displayName") + "\" ";
    	datastr = datastr + ">";
    	datastr = datastr + self.cleanValueForNull(value)
    	datastr = datastr + "</ITEM>"; 
    	
    	
    	datastr = datastr + "<CONTEXT>";
    	datastr = datastr + "<EXPANDEDVALUES>";
    	var previuosDat = datastr;
    		try {
    			if (item != ""){
    					var numRow 
    					if (item.row != undefined)
    					 	numRow = item.row;
    					else
    						numRow = item[0];
    						
    					var prevValues = [];
    					for (var iCV=0; iCV < this.GeneralDataRows.length; iCV++) {
    						if (prevValues.indexOf(this.GeneralDataRows[iCV][number]) == -1){
    							prevValues = prevValues.concat(this.GeneralDataRows[iCV][number])
    							if (self.conditions[number].collapsedValues.indexOf(this.GeneralDataRows[iCV][number])===-1){
    								datastr = datastr  + "<VALUE>" + self.cleanValueForNull(this.GeneralDataRows[iCV][number]) + "</VALUE>";
    							}
    						}
    					}
    			}
    		} catch (Error){
    			datastr = previuosDat;
    		}
    	
    	datastr = datastr + "</EXPANDEDVALUES>";
    	datastr = datastr + "</CONTEXT>";
    	datastr = datastr + "</DATA>"
    	
    	return datastr;
    }
    
    this.ExpandCollapseHandleWhenServerPagination = function(elemvalue, action){
    	if (typeof(self.QueryViewerCollection[IdForQueryViewerCollection].ItemExpand) == 'function') {
    		var number = jQuery(elemvalue).data('numberMorD');
    		if (self.conditions[number].previousPage >= self.conditions[number].totalPages){
    			var datastr = self.ExpandCollapseHandleWhenServerPaginationCreateXML(elemvalue)
    			datastr = datastr.replace(/\&/g, '&amp;');
    			self.QueryViewerCollection[IdForQueryViewerCollection].onItemExpandCollapseEvent(datastr,(action == "collapse"))
    			self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, "", "", "", "")
        	} else {
        		self.lastColumnNumber = number;
    			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
	    			var data = JSON.parse(validStr);
	    			var columnNumber = self.lastColumnNumber
	    			
	    			self.conditions[columnNumber].previousPage = data.PageNumber
    				self.conditions[columnNumber].totalPages   = data.PagesCount
	    			self.conditions[columnNumber].blocked      = true
	    			
	    			//null value?
    				if ((data.Null) && (!self.conditions[columnNumber].hasNull)){
    					self.conditions[columnNumber].hasNull = true;
    					if (self.conditions[columnNumber].distinctValues.indexOf("#NuN#") == -1){
    						self.conditions[columnNumber].distinctValues.push("#NuN#")
    					}
    					if (self.conditions[columnNumber].defaultAction == "Include"){
    						if (self.conditions[columnNumber].visibles.indexOf("#NuN#") == -1){
    							self.conditions[columnNumber].visibles.push("#NuN#");
    						}
    					} else {
    						if (self.conditions[columnNumber].blackList.indexOf("#NuN#") == -1){
    							self.conditions[columnNumber].blackList.push("#NuN#");
    						}
    					}
    				} 
	    			
	    			for (var i = 0; i < data.NotNullValues.length; i++){
	    				var val = data.NotNullValues[i].replace(/Unicode_005C/g,"\\");
	    				if (self.conditions[columnNumber].distinctValues.indexOf(val) == -1){
	    					self.conditions[columnNumber].distinctValues.push(val)
	    				
	    					if ( (self.conditions[columnNumber].defaultAction == "Include")
	    							&& (self.conditions[columnNumber].visibles.indexOf(val) == -1)){
	    						self.conditions[columnNumber].visibles.push(val)
	    					}
	    					if ( (self.conditions[columnNumber].state == "Exclude") 
	    							&& (self.conditions[columnNumber].blackList.indexOf(val) == -1)){
	    						self.conditions[columnNumber].blackList.push(val)
	    					}
	    				}
	    			}
	    			
	    			var datastr = self.ExpandCollapseHandleWhenServerPaginationCreateXML(elemvalue)
    				datastr = datastr.replace(/\&/g, '&amp;');
    				
	    			self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, "", "", "", "");
	    			setTimeout( function(){
	    				self.QueryViewerCollection[IdForQueryViewerCollection].onItemExpandCollapseEvent(datastr,(action == "collapse"))
	    			}, 2000);
		   		}).closure(this), [self.columns[number].getAttribute("dataField"), 1, 0, ""]);
    		}
    	} else {
    			self.getDataForPivot(self.UcId, 1, self.rowsPerPage, true, "", "", "", "");
    	}
    }
    
    this.ExpandCollapseHandleWhenServerPaginationCreateXML = function(elemvalue){
    	var value = jQuery(elemvalue).data('itemValue');
        var type  =	jQuery(elemvalue).data('typeMorD');
        var number = jQuery(elemvalue).data('numberMorD');
        var item  =	jQuery(elemvalue).data('itemInfo');
    	var datastr = "<DATA><ITEM ";
    	  	
    	datastr = datastr + "name=\"" +  columns[number].getAttribute("name") + "\" ";
    	datastr = datastr + "displayName=\"" +  columns[number].getAttribute("displayName") + "\" ";
    	datastr = datastr + ">";
    	datastr = datastr + value
    	datastr = datastr + "</ITEM>";
    	
    	
    	datastr = datastr + "<CONTEXT>";
    	datastr = datastr + "<EXPANDEDVALUES>";
    	
    	for(var ex = 0; ex < self.conditions[number].distinctValues.length; ex++){
    		if (self.conditions[number].collapsedValues.indexOf(self.conditions[number].distinctValues[ex]) == -1){
    			datastr = datastr + "<VALUE>" + self.conditions[number].distinctValues[ex] + "</VALUE>"
    		}
    	}
    	
    	datastr = datastr + "</EXPANDEDVALUES>";
    	datastr = datastr + "</CONTEXT>";
    	datastr = datastr + "</DATA>"
    	
    	return datastr;
    }
    
    this.onFilteredChangedEventHandle = function(dimensionNumber){
    	var datastr = "<DATA name=\"" + this.columns[dimensionNumber].getAttribute("name") + "\" displayName=\"" +  this.columns[dimensionNumber].getAttribute("displayName") + "\">"
    	for (var i = 0; i < self.conditions[dimensionNumber].distinctValues.length; i++) {
            		var value = self.conditions[dimensionNumber].distinctValues[i];
            		var checked = ((self.conditions[dimensionNumber].blackList.find(value) == -1) && self.valueIsShowed(value, dimensionNumber)  ||  (/*(cond.distinctValues.find(value) == -1) &&*/ (self.conditions[dimensionNumber].blackList.find(value) == -1)));
            		if (checked){
            				datastr = datastr + '<VALUE>' + value + '</VALUE>';
            		}
       }
       datastr = datastr + "</DATA>"
    	
    	if (this.QueryViewerCollection[IdForQueryViewerCollection].FilterChanged) {
    			datastr = datastr.replace(/\&/g, '&amp;');
            	var xml_doc = this.QueryViewerCollection[IdForQueryViewerCollection].xmlDocument(datastr);
            	var Node = this.QueryViewerCollection[IdForQueryViewerCollection].selectXPathNode(xml_doc, "/DATA");
            	this.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.Name = Node.getAttribute("name");
            	this.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.SelectedValues = [];
            	var valueIndex=-1;
            	for (var i=0; i<Node.childNodes.length; i++)
            	    if (Node.childNodes[i].nodeName == "VALUE")
            	    {
            	        valueIndex++;
            	        this.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.SelectedValues[valueIndex] = Node.childNodes[i].firstChild.nodeValue;
            	    }
            	this.QueryViewerCollection[IdForQueryViewerCollection].FilterChanged();
        }
    }
    
    this.onFilteredChangedEventHandleWhenServerPagination = function(dimensionNumber){
    	if (self.QueryViewerCollection[IdForQueryViewerCollection].FilterChanged) {
    		if (self.conditions[dimensionNumber].previousPage >= self.conditions[dimensionNumber].totalPages){
    			self.onFilteredChangedEventHandleWhenServerPaginationCreateXML(dimensionNumber, self.conditions[dimensionNumber].distinctValues, self.conditions[dimensionNumber].blackList);
    		} else {
    			self.lastColumnNumber = dimensionNumber;
    			self.QueryViewerCollection[self.IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
    				setTimeout( function() {
    					var validStr = resJSON.replace(/\\\\/g, "Unicode_005C").replace(/\\/g, "Unicode_005C")
    					var data = JSON.parse(validStr);
    					self.onFilteredChangedEventHandleWhenServerPaginationCreateXML(self.lastColumnNumber, data.NotNullValues, self.conditions[dimensionNumber].blackList);
    				}, 200)
    			}).closure(this), [self.columns[dimensionNumber].getAttribute("dataField"), 1, 0, ""]);	
    		}
        }
    }
    
    this.onFilteredChangedEventHandleWhenServerPaginationCreateXML = function(dimensionNumber, distinctValues, blackList){
    	var datastr = "<DATA name=\"" + self.columns[dimensionNumber].getAttribute("name") + "\" displayName=\"" +  self.columns[dimensionNumber].getAttribute("displayName") + "\">"
	    for (var i = 0; i < distinctValues.length; i++) {
        	var value = distinctValues[i].replace(/Unicode_005C/g,"\\");
        	var checked = true; 
        	if (self.conditions[dimensionNumber].state == "all"){
            	checked = true;
            } else if (self.conditions[dimensionNumber].state == "none"){
            	checked = false;
            } else if (self.conditions[dimensionNumber].blackList.find(value) != -1) {
            	checked = false;
            } else if (distinctValues.find(value) != -1){
            	checked = true;	
            } else if (self.conditions[dimensionNumber].defaultAction == "Exclude"){
            	checked = false;
            }
        	if (checked){
        		datastr = datastr + '<VALUE>' + value + '</VALUE>';
        	}
       	}
       	datastr = datastr + "</DATA>"
    	
     	datastr = datastr.replace(/\&/g, '&amp;');
        var xml_doc = self.QueryViewerCollection[IdForQueryViewerCollection].xmlDocument(datastr);
        var Node = self.QueryViewerCollection[IdForQueryViewerCollection].selectXPathNode(xml_doc, "/DATA");
        self.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.Name = Node.getAttribute("name");
        self.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.SelectedValues = [];
        var valueIndex=-1;
        for (var i=0; i<Node.childNodes.length; i++){
            if (Node.childNodes[i].nodeName == "VALUE")
            {
                valueIndex++;
                self.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.SelectedValues[valueIndex] = Node.childNodes[i].firstChild.nodeValue;
            }
        }
        self.QueryViewerCollection[IdForQueryViewerCollection].FilterChanged();
    }
    
    this.onDragUndDrop = function(dimensionNumber, distinctValues, blackList){
    	var datastr = "<DATA name=\"" + self.columns[dimensionNumber].getAttribute("name") + "\" displayName=\"" + self.columns[dimensionNumber].getAttribute("displayName") + "\">"
    	for(var i=0; i < distinctValues.length; i++){
    		var value = distinctValues[i];
    		var checked = true;
    		if (self.conditions[dimensionNumber].state == "all"){
    			checked = true;
    		} else if (self.conditions[dimensionNumber].state == "none"){
    			checked = true;
    		} else if (self.conditions[dimensionNumber].defaultAction == "Exclude"){
    			checked = false;
    		}
    		if (checked){
    			datastr = datastr + '<VALUE>' + value + '</VALUE>'
    		}
    	}
    	datastr = datastr + "</DATA>"
    	
    	datastr = datastr.replace(/\&/g, '&amp;');
    	for(var i=0; i<Node.children.length; i++){
    		if (Node.childNodes[i].nodeName == "VALUE"){
    			valueIndex++;
    			self.QueryViewerCollection[IdForQueryViewerCollection].FilterChangedData.SelectedValues[valuesIndex] = Node.childNodes[i].firstChild.nodeValues;
    		}
    	}
    	datastr = datastr + "<VALUE>" + value + "</VALUE>"
    	self.QueryViewerCollection[IdForQueryViewerCollection].FilterChanged();
    }
    
    this.onDragundDropEventHandle = function(conditionIndex, position){
    	var datastr = "<DATA name=\"" + this.columns[conditionIndex].getAttribute("name") + "\" displayName=\"" +  this.columns[conditionIndex].getAttribute("displayName") + "\" position=\"" + position + "\"/>"
    	
    	
    	try {
    		datastr = datastr.replace(/\&/g, '&amp;');
       		this.QueryViewerCollection[IdForQueryViewerCollection].onOAT_PIVOTDragAndDropEvent(datastr);
      	} catch (Error){
      		
      	}
    }
    
    this.getGenerator = function () {
        var gen;
        if (gx.gen.isDotNet()) gen = "C#";
        else if (gx.gen.ruby) gen = "Ruby";
        else gen = "Java";
        return gen;
    }
    
    if ((self.autoPaging) && (self.colConditions.length > 0)){
    	dataRows = self.rowsForBlankCells(dataRows);
    }
   
   	if (self.serverPagination){
   		self.initWhenServerPagination();
   		//self.goWhenServerPagination(false)
   	} else {
    	self.init();
    
    
    	if  ((self.autoPaging) || (self.rowConditions.length <= 16) || (self.GeneralDataRows.length <= 1200)){
    		if (self.filterIndexes.length > 0){
    			self.preGoWhenMoveTopFilter(self.filterIndexes[0], true)
    			self.stateChanged=true;
    			if (self.tempBlackLists){
        			if (!self.autoPaging){
        				for (var i = 0; i < self.conditions.length; i++){
        					if (self.conditions[i]){
        						if (self.tempBlackLists[i]){
        							self.conditions[i].blackList = self.tempBlackLists[i]
        						}
        						if (self.tempCollapsedValues[i]){
        							self.conditions[i].collapsedValues = self.tempCollapsedValues[i]
        						} 
        					}
        				}
        			} 
        		}
        		try {
        			if (self.oldSortValues.length > 0){
						for (var fsort = 0; fsort < self.conditions.length; fsort++){
							if (self.conditions[fsort]){
								self.conditions[fsort].sort = self.oldSortValues[fsort];
								self.sort(self.conditions[fsort]);
							}		
						}
					}
				} catch (Error) {
				} 
        		self.go(false);
    		} else if (!self.autoPaging){
    			self.go(true);
    		} else {
    			var areCollapsedValues = self.preGoWhenCollapsedValue()
    			if (areCollapsedValues){
    				self.go(false)
    			} else {
    				self.preGo(false, null, null, -1, true);
    			}
    		}
    	} else {
    		setTimeout( function(){
    			if (self.filterIndexes.length > 0){
    				self.preGoWhenMoveTopFilter(self.filterIndexes[0], true)
    				self.stateChanged=true;
    				if (self.tempBlackLists){
        				if (!self.autoPaging){
        				//reload blacklist & sort
        					for (var i = 0; i < self.conditions.length; i++){
        						if (self.conditions[i]){
        							if (self.tempBlackLists[i]){
        								self.conditions[i].blackList = self.tempBlackLists[i]
        							}
        							if (self.tempCollapsedValues[i]){
        								self.conditions[i].collapsedValues = self.tempCollapsedValues[i]
        							} 
        						}
        					}
        				} 
        			}
 		       		try {
        				if (self.oldSortValues.length > 0){
							for (var fsort = 0; fsort < self.conditions.length; fsort++){
								if (self.conditions[fsort]){
									self.conditions[fsort].sort = self.oldSortValues[fsort];
									self.sort(self.conditions[fsort]);
								}		
							}
						}
					} catch (Error) {
					} 
        			self.go(false);
    			} else if (!self.autoPaging){
    				self.go(true);
    			} else {
    				var areCollapsedValues = self.preGoWhenCollapsedValue()
    				if (areCollapsedValues){
    					self.go(false)
    				} else {
    					self.preGo(false, null, null, -1, true);
    				}
	    		}
    		}, 500)
    	}
    }
}
try{
OAT.Loader.featureLoaded("pivot");
} catch (ERROR){
	
}

//TODO: add Image for order when server pagination 
function sortUnidimensionalArray(arr, index, self) { /* sort distinct values of a condition */
        //var months = ["january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"];
        var sortFunc;
        var coef = 1;
        var numSort = function(a, b) {
            if (a == b) { return 0; }
            return coef * (parseInt(a) > parseInt(b) ? 1 : -1);
        }
        var dictSort = function(a, b) {
            if (a == b) { return 0; }
            return coef * (a > b ? 1 : -1);
        }
        
        
        //new code
        var sortInt = true;
        for (var ival = 0; ival < arr.length; ival++) {
        	if ((sortInt) && (arr[ival] != parseInt(arr[ival]))){
        		sortInt  = false;
        	}
        }
        if (sortInt) { sortFunc = numSort; } else { sortFunc = dictSort; } //decides the type of sorting
        //end new code
        var testValue = arr[0];
        
        //if (months.find(testValue.toString().toLowerCase()) != -1) { sortFunc = dateSort; }

        arr.sort(sortFunc);
       
       	//when custom order 
        for(var h=0; h<self.columns[index].childNodes.length;h++){
        	if ((self.columns[index].childNodes[h]!=undefined) &&
        		(self.columns[index].childNodes[h].localName!=undefined) &&
					(self.columns[index].childNodes[h].localName==="customOrder")) 
        	{
        		arr = []
        		for (var n = 0; n < self.columns[index].childNodes[h].childNodes.length; n++) {
        			if (self.columns[index].childNodes[h].childNodes[n].localName == "Value") {
        				arr.push(self.columns[index].childNodes[h].childNodes[n].textContent);
        			}
        		}
        	}
         }
        
                
        
        return arr;
} /* sort */

function fillGeneralDistinctValues(numConds, self, rows){
		self.GeneralDistinctValues = [];
		for(var i=0; i < numConds; i++){
			var elems = [];
			for(var j=0; j < rows.length; j++){
				if (rows[j][i] == undefined){
					if (elems.find(" ") == -1){
						elems.append(" ");
					}		
				} else {
					if (elems.find(rows[j][i]) == -1){
						elems.append(rows[j][i]);
					}
				}
			}
			
			self.GeneralDistinctValues[i] = sortUnidimensionalArray(elems, i, self);
		}
}

function fromUndefinedToBlanck(dataRows){
	for (var i = 0; i < dataRows.length; i++){
		for (var j = 0; j < dataRows[i].length; j++){
			if (dataRows[i][j] == undefined){
				dataRows[i][j] = " ";
			}
		}
	}
}

function sumGrandPagingTotals(allDataRows , conditions, cant_measures, formulaInfo, self){
	var size = allDataRows[0].length;
	var init_measure_pos = size - cant_measures;
	var sums = [];
	for (var j = 0; j < cant_measures ; j++){ //set initial value 0, cant_measures is the number of measures
		if (formulaInfo.measureFormula[j].hasFormula){
			sums[j] = [];
			for(var o = 0; o < self.formulaInfo.recordDataLength; o++){sums[j][o] = 0}	
		} else {
			sums[j] = 0;
		}
	}
	var firstValueToSum = true;
	for (var i = 0; i < allDataRows.length; i++){
		if (self.isNotFilterByTopFilter(allDataRows[i]) && !self.notInBlackList(allDataRows[i], self.conditions)){
			for (var t = 0; t < cant_measures; t++){
				if (!formulaInfo.measureFormula[t].hasFormula){
					if((allDataRows[i][init_measure_pos + t] != "#NuN#") && (sums[t] != "#NuN#")) {
						sums[t] = sums[t] + parseFloat(allDataRows[i][init_measure_pos + t]);
					} else if((firstValueToSum) && (allDataRows[i][init_measure_pos + t] == "#NuN#")) {
						sums[t] = "#NuN#";
					} else if((sums[t] == "#NuN#") && (allDataRows[i][init_measure_pos + t] != "#NuN#")) {
						sums[t] = parseFloat(allDataRows[i][init_measure_pos + t]);
					}
				}
			}
			firstValueToSum = false;
		}
	}
	
	
	for (var m = 0; m < cant_measures; m++){
		if (formulaInfo.measureFormula[m].hasFormula){
			for (var i = 0; i < self.recordForFormula.length; i++){
				for (var t = 0; t < self.formulaInfo.measureFormula[m].relatedMeasures.length; t++){
					var pos = self.formulaInfo.measureFormula[m].relatedMeasures[t]
					if (!isNaN(parseFloat(self.recordForFormula[i][pos]))){
						sums[m][pos] = sums[m][pos] + parseFloat(self.recordForFormula[i][pos]);
					} 
				}
			}
		}
	}
	
	for (var m = 0; m < cant_measures; m++){
		if (formulaInfo.measureFormula[m].hasFormula){
			sums[m] = self.calculateFormulaTotal([sums[m]], m, "MeasureInRows")
		}
	}
	return sums;
}

function notInBlackListB(row, conditions){
	var esta = false;
	for (var i = 0; i < row.length; i++){
		if ((conditions[i] != undefined) && (conditions[i].blackList != undefined) && (conditions[i].blackList.find(row[i]) != -1)){
			esta = true;
		} 
	}
	return esta;
}

//return the partial DataRow, from "item", item represents the dimesnion values of a row, ej> [dimension1Value, dimensio2Value] 
function getDataRowFromItem(item){
	var dataR = [];
	var obj = item;
	
	while (obj.depth > 0){
		dataR = [obj.value].concat(dataR);
		obj = obj.parent;
	}
	
	dataR = [obj.value].concat(dataR);
	return dataR;
}

//recives the dimensions value, ej> [dimension1Value, dimensio2Value]
//returns the total sum of measures values 
function getTotalsForDataRow(allData, dataR, cant_measures, conditions, filteredIndexes, pageData, filterValues, self) 
{
	var size = allData[0].length;
	var init_measure_pos = size - cant_measures;
	var sums = [];
	for (var j = 0; j < cant_measures ; j++){ //set initial value 0, cant_measures is the number of measures
		sums[j] = 0;
	} //set initial value 0, cant_measures is the number of measures 
	
	var tempFilteredIndexes = []; for(var pFId = 0; pFId < filteredIndexes.length; pFId++){ tempFilteredIndexes[pFId] = filteredIndexes[pFId]; }
	tempFilteredIndexes.sort();
	for(var p=0; p < tempFilteredIndexes.length; p++){ //if same dimension has been move up to the top filter
		if (tempFilteredIndexes[p] < dataR.length){
			
			if (filterValues[p].value == "[all]"){	
				dataR.splice(tempFilteredIndexes[p], 0, "#-#!");
			} else {
				dataR.splice(tempFilteredIndexes[p], 0, filterValues[p].value);
			}
		}		
	} 
	 
	
	var belongToPageData = false;
	//for each row of data
	var firstValueToSum  = true;
	for(var j = 0; j < allData.length; j++) {
		if(self.isNotFilterByTopFilter(allData[j]) && !self.notInBlackList(allData[j], self.conditions)) {
			var coincide = true;
			//if checks with dataR
			for(var i = 0; i < dataR.length; i++) {
				if((allData[j][i] != dataR[i]) && (dataR[i] != '#-#!')) {
					coincide = false;
				}
				if(!coincide) {
					break;
				}
			}
			//sum values
			if(coincide) {
					if(!belongToPageData) {
						belongToPageData = belongToCollection(allData[j], pageData)
					}
					for(var t = 0; t < cant_measures; t++) {
						if((allData[j][init_measure_pos + t] != "#NuN#") && (sums[t] != "#NuN#")) {
							sums[t] = sums[t] + parseFloat(allData[j][init_measure_pos + t]);
						} else if((firstValueToSum) && (allData[j][init_measure_pos + t] == "#NuN#")) {
							sums[t] = "#NuN#";
						} else if((sums[t] == "#NuN#") && (allData[j][init_measure_pos + t] != "#NuN#")) {
							sums[t] = parseFloat(allData[j][init_measure_pos + t]);
						}
					}
					firstValueToSum = false
			}
		}
	}

	
	//calculate mesaure formula
	try {
		for(var m = 0; m < cant_measures; m++){
			if (self.formulaInfo.measureFormula[m].hasFormula){
				var formval = self.getFormulaRowByDataRow(dataR,m,"");
				sums[m] =self.calculateFormulaTotal([formval], m, "MesaureAsColumn");	
			}
		}
	} catch (Error) {}
	
	if (!belongToPageData)
		return false
		
	return sums;
}

function belongToCollection(row, rowCollection){
	for (var j=0; j < rowCollection.length; j++){
		var coincide = true;
		for (var i=0; i < row.length - measures.length; i++){
			if (rowCollection[j][i] != row[i]){
				coincide = false;
			}
			if (!coincide) {
				break;
			}
		}
		if (coincide) return true;
	}
	return false;
}

//remove rows until get the pagination size
//precond - data is order
function removeExtraRows(dataRows, measures, rowsPerPage){
	//get temporal Distinct Values
	var dtValues = [];
	var totalRows = 1; //last gand total
	var initVal = dataRows[0][0];
	if (dataRows[0].length - 0 -1 > 0){ 
		totalRows = 1 + totalRows;
	}
	var rows = [];
	var cantRows = dataRows.length;
	for (var i = 0; i < dataRows.length; i++) { //for every element from the first column
		if (initVal == dataRows[i][0]){
			rows.append([dataRows[i]]);
			if (dataRows[0].length - measures -0 -1 === 0){
				totalRows = totalRows + 1;
			}
		} else {
			totalRows = 1 + totalRows; //the subtotal for this element
			initVal = dataRows[i][0];
			if (dataRows[0].length - measures -1 > 0){ //recursive call
				totalRows = totalRows + getTrueCantRows(rows, 1, measures); //add the rows added for sub elements
			}
			rows = [];
			rows.append([dataRows[i]]);
		}
		
		if (totalRows > rowsPerPage){
			cantRows = i;
			break;
		}
    }
    if (dataRows[0].length - measures -0 -1 > 0){ //recursive call for the las subgroup
		totalRows = totalRows + getTrueCantRows(rows, 1, measures); //add the rows added for sub elements
	}
	return i;
}

function collapseRowForCollapseValues(self, dataToCollapse){
	//delete pagination info
	self.paginationInfo = false;
	
	//remove extra row when dimensions at filter
	if (self.filterIndexes.length > 0){
		
		self.FilterByTopFilter = true;
		self.allData = self.GeneralDataRows;
      	var dataRows = self.GeneralDataRows;
      	
      	var dataColumns = [];
		for (var i = 0; i < self.columns.length; i++){ //get pos. column not in top filter & not filter in top filter
			if ( (self.filterIndexes.find(i) == -1) ) {
				dataColumns.append(i)
			} else {
				var fi = self.filterIndexes.find(i) 
				var s = self.filterDiv.selects[fi];
				if ((s!=undefined) && (OAT.$v(s) != "[all]")){dataColumns.append(i)}
			}
		}
		
		var tempDataRows = [];
		for (var i = 0; i < dataRows.length; i++){
			
			var existe = false;
			var previousRecord = 0;
			for(var j =0; j < tempDataRows.length; j++){
				var same = true;	
				for (var h=0; h < dataColumns.length; h++){
					if (dataRows[i][dataColumns[h]] != tempDataRows[j][dataColumns[h]]){
						
						same = false;
						break; 
					}
				}
				if (same){
					previousRecord = j;
					existe = true;
					break;
				} 
			}
			if (!existe) {
				var newRecord = [];
				for (var p=0; p < dataRows[i].length; p++)
				{
					newRecord.append([dataRows[i][p]]);
				}
				//newRecord.append([0]);
				tempDataRows.append([newRecord]); //tempDataRows.append([])
				
				for (var t = 0; t < measures.length; t++) {
					var pos;
					if (t < measures.length-1){
					  pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
					} else {
						pos = self.dataColumnIndex
					}
					if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
						var valuesToOperate = self.getFormulaRowByDataRow(dataRows[i], t, "")
						if ((valuesToOperate != self.EmptyValue) && (valuesToOperate != "#NuN#")){
							var result = self.calculateFormulaTotal([valuesToOperate], t, "MeasureInRows")
							if (!isNaN(result)){ 
								newRecord[pos] = result.toString()
							}	
						}
					}
				}
				
			} else { //increase prevoius record value
				
				//increase the entry of the column index (the last one)
				if (measures.length > 0){
					if (!self.formulaInfo.measureFormula[measures.length-1].hasFormula){
						if ((tempDataRows[previousRecord][self.dataColumnIndex] != "#NuN#") && (dataRows[i][self.dataColumnIndex] != "#NuN#")){
							tempDataRows[previousRecord][self.dataColumnIndex] = (parseFloat(tempDataRows[previousRecord][self.dataColumnIndex]) + parseFloat(dataRows[i][self.dataColumnIndex])).toString()
						} else if (tempDataRows[previousRecord][self.dataColumnIndex] == "#NuN#") {
							tempDataRows[previousRecord][self.dataColumnIndex] = dataRows[i][self.dataColumnIndex]
						}
					}
				}
				//increase the entries of other columns
				for (var t = 0; t < measures.length-1; t++) {
					var pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
					if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
						//var addedValues = self.getFormulaRowByDataRow(dataRows[i], t, "")
					} else {
						tempDataRows[previousRecord][pos] = (parseFloat(tempDataRows[previousRecord][pos]) + parseFloat(dataRows[i][pos])).toString()
					}
				}
			}
		}
		//end remove extra data rows, that have the same columns values, except for the value of the columns index
		
		self.RowsWhenMoveToFilter = tempDataRows;
	}	
	//end remove extra row when dimensions at filter
	
	//remove extra row when values collapse
	var tempCollapsesRows = [];
	
	var rowsWithCollapseValues = []
	var collapsesValues = []
	for(var i=0; i<self.conditions.length; i++){
		if ((self.conditions[i].collapsedValues != undefined) && (self.conditions[i].collapsedValues.length > 0)){
			rowsWithCollapseValues.push(self.conditions[i].dataRowPosition)
			collapsesValues.push(self.conditions[i].collapsedValues)
		}
	}
	
	if (rowsWithCollapseValues.length > 0) //are values collapses?
	{
		var dataRows = self.GeneralDataRows
    	if (self.RowsWhenMoveToFilter.length > 0){
    		dataRows = self.RowsWhenMoveToFilter
    	}
		self.FilterByTopFilter = true //similar behaviour to top filter
		
		var tempDataRows = [];
		
	 	for(var i = 0; i < dataRows.length; i++){
	 		var added = false;
	 		
	 		for (var r=0; r<rowsWithCollapseValues.length; r++){
	 			if (collapsesValues[r].find(dataRows[i][rowsWithCollapseValues[r]]) != -1){
	 				added = true;
	 				if (self.isNotFilterByTopFilter(dataRows[i]) 
	 					&& !self.notInBlackList(dataRows[i] , self.conditions)){
	 				
						var existe = false;
						var previousRecord = 0;
						for(var j =0; j < tempDataRows.length; j++){
							var same = true;	
							for (var h=0; h <= rowsWithCollapseValues[r]; h++){
								if (dataRows[i][h] != tempDataRows[j][h]){
									same = false;
									break; 
								}
							}
							if (same){
								previousRecord = j;
								existe = true;
								break;
							} 
						}
					
						if (!existe) {
							var newRecord = [];
							for (var p=0; p < dataRows[i].length; p++)
							{
								newRecord.append([dataRows[i][p]]);
							}
							tempDataRows.append([newRecord]); 
				
							for (var t = 0; t < measures.length; t++) {
								var pos;
								if (t < measures.length-1){
							  		pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
								} else {
									pos = self.dataColumnIndex
								}
								if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
									var valuesToOperate = self.getFormulaRowByDataRow(dataRows[i], t, "")
									if ((valuesToOperate != self.EmptyValue) && (valuesToOperate != "#NuN#")){
										var result = self.calculateFormulaTotal([valuesToOperate], t, "MeasureInRows")
										if (!isNaN(result)){ 
											newRecord[pos] = result.toString()
										}	
									}
								}
							}
						} else { //increase prevoius record value
							//increase the entry of the column index (the last one)
							if (measures.length > 0){
								if (!self.formulaInfo.measureFormula[measures.length-1].hasFormula){
									if ((tempDataRows[previousRecord][self.dataColumnIndex] != "#NuN#") && (dataRows[i][self.dataColumnIndex] != "#NuN#")){
										tempDataRows[previousRecord][self.dataColumnIndex] = (parseFloat(tempDataRows[previousRecord][self.dataColumnIndex]) + parseFloat(dataRows[i][self.dataColumnIndex])).toString()
									} else if (tempDataRows[previousRecord][self.dataColumnIndex] == "#NuN#") {
										tempDataRows[previousRecord][self.dataColumnIndex] = dataRows[i][self.dataColumnIndex]
									}
								}
							}
							//increase the entries of other columns
							for (var t = 0; t < measures.length-1; t++) {
								var pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
								if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
									//var addedValues = self.getFormulaRowByDataRow(dataRows[i], t, "")
								} else {
									tempDataRows[previousRecord][pos] = (parseFloat(tempDataRows[previousRecord][pos]) + parseFloat(dataRows[i][pos])).toString()
								}
							}
						}
					
					
					
					}
					break;
				}	 			
	 		}
	 		
	 		if (!added){
	 			var newRecord = [];
				for (var p=0; p < dataRows[i].length; p++)
				{
					newRecord.append([dataRows[i][p]]);
				}
				tempDataRows.append([newRecord]); 
				
				for (var t = 0; t < measures.length; t++) {
					var pos;
					if (t < measures.length-1){
					  pos = self.rowConditions[self.rowConditions.length - measures.length + 1 + t]
					} else {
				    	pos = self.dataColumnIndex
					}
					if ((self.formulaInfo.measureFormula[t].hasFormula) && (self.filterIndexes.length > 0)){
						var valuesToOperate = self.getFormulaRowByDataRow(dataRows[i], t, "")
						if ((valuesToOperate != self.EmptyValue) && (valuesToOperate != "#NuN#")){
							var result = self.calculateFormulaTotal([valuesToOperate], t, "MeasureInRows")
							if (!isNaN(result)){ 
								newRecord[pos] = result.toString()
							}	
						}
					}
				}
	 		}
	 	}
	 	
	 	self.RowsWhenMoveToFilter = tempDataRows
	 	
	}
	
	if (self.filterIndexes.length > 0){
		var dataColumns = [];
		for (var i = 0; i < self.columns.length; i++){ //get pos. column not in top filter & not filter in top filter
			if ( (self.filterIndexes.find(i) == -1) ) {
				dataColumns.append(i)
			} else {
				var fi = self.filterIndexes.find(i) 
				var s = self.filterDiv.selects[fi];
				if ((s!=undefined) && (OAT.$v(s) != "[all]")){dataColumns.append(i)}
			}
		}
		
		var dataRows = self.RowsWhenMoveToFilter;
		//reordenar segun datos a agrupar
		var sortIntMemory = [];
		
		//get sort value
     	for(var index = dataRows[0].length - 1; index > -1 ;index--){
     		var sortInt = true;
    	    for (var ival = 0; ival < dataRows.length; ival++) {
        	if ( (sortInt) && (dataRows[ival][index] != parseInt(dataRows[ival][index]) ) ){
        		sortInt  = false;
        		break;
        	}
        	}
        	sortIntMemory[index] = sortInt;
     	}
     	
     	var index = dataColumns[0];
    	if (sortIntMemory[index]){
    				dataRows = dataRows.sort((function(index){
    					return function(a, b){
       						return (parseInt(a[index]) === parseInt(b[index]) ? 0 : (parseInt(a[index]) < parseInt(b[index]) ? -1 : 1));
    					};
					})(index));
    	} else {
    				dataRows = dataRows.sort((function(index){
    					return function(a, b){
       						return (a[index] === b[index] ? 0 : (a[index] < b[index] ? -1 : 1));
       						};
					})(index));
		}
		
		var actualVal = dataRows[0][index]; //sort from the first row column
		var initPos = 0; 
		for (var i=0; i < dataRows.length; i++){
			if ((actualVal != dataRows[i][index])) {
				actualVal = dataRows[i][index]
				dataRows = OAT.PartialSort(dataRows, initPos, i-1, index+1, self.headerRow.length, sortIntMemory)
				initPos = i;
			} else 	if ((i == dataRows.length-1)){
				dataRows = OAT.PartialSort(dataRows, initPos, i, index+1, self.headerRow.length, sortIntMemory)
			}
		}
     	
		self.RowsWhenMoveToFilter = dataRows
	}
	//count all items to show
	if ((self.filterIndexes.length > 0) || (rowsWithCollapseValues.length > 0)){
	} else {
		self.RowsWhenMoveToFilter = self.GeneralDataRows
	}
	
	
	return rowsWithCollapseValues.length>0
	
}

function calculateTotalRowsWithSubtotalRows(previusData, newData, self){
	var subs = []
	for (var con = 0; con < self.rowConditions.length-1-(measures.length-1); con++){
		if  (self.conditions[self.rowConditions[con]]){
			subs[con] = (self.conditions[self.rowConditions[con]].subtotals == 1) ? 1 : 0;
		}  
	}
	/*if (dataRows.length == 0){
		return 0;
	}*/
	var subtotals = 0;
	for(var j = 0; j < self.rowConditions.length-1-(measures.length-1); j++){
		if (self.conditions[self.rowConditions[j]].collapsedValues.indexOf(previusData[self.rowConditions[j]]) != -1){
			subtotals = 1;
			break
		}
		if (previusData[self.rowConditions[j]] != newData[self.rowConditions[j]]){
			subtotals = subtotals + subs.reduce(function(a,b){return a + b;}) 	// + (self.rowConditions.length-1-(measures.length-1)-j);
			break;
		}
		subs[j] = 0;
	}
	return subtotals;
	/*var r = dataRows.length-1
	while (r >= 0){
		var previousRecord = dataRows[r];
		for(var j = 0; j < self.rowConditions-1; j++){
			if (dataRows[j] != newData[j]){
	 			subtotals = subtotals + (self.rowConditions - j);
	 		}
		}
	}*/
}

function showSubtotalRowsForFirstColumn(previusData, newData, self){
	var subs = []
	for (var con = 0; con < self.rowConditions.length-1-(measures.length-1); con++){
		if  (self.conditions[self.rowConditions[con]]){
			subs[con] = (self.conditions[self.rowConditions[con]].subtotals == 1) ? 1 : 0;
		}  
	}
	
	var subtotals = 0;
	var j = 0;
	
	if (self.conditions[self.rowConditions[j]].collapsedValues.indexOf(previusData[self.rowConditions[j]]) != -1){
		substotals = 1;
		
	} else if (previusData[self.rowConditions[j]] != newData[self.rowConditions[j]]){
		subtotals = 1; 
	}
	subs[j] = 0;
	
	return subtotals;
}


function createPaginationInfo(self, data){
	if (!self.paginationInfo){
		self.paginationInfo = {}
		self.paginationInfo.pages = []
	} 
	var actualPage = 0;
	var firstRowActualPage = 0;
	var lastRowActualPage = 0;
	var cantRowsActualPage = 0;
	var tempDataRows  = []
	
	var extraRows = 0;
	//for (var iter = 0; iter < data.length; iter++){
	var iter = 0;
	while (iter < data.length){	
		if (cantRowsActualPage >= self.autoPagingRowsPerPage){
			var lastSumShow = false//(cantRowsActualPage == self.autoPagingRowsPerPage) //**
			self.paginationInfo.pages[actualPage] = { firstRow: firstRowActualPage, lastRow: lastRowActualPage, rows: tempDataRows, lastSumShow: lastSumShow, extraRows: extraRows};
			actualPage++;
			firstRowActualPage = iter;
			cantRowsActualPage = 0;
			tempDataRows = [];
			extraRows = 0;
		}
		
		var descartado = false;
		if (!self.notInBlackList(data[iter] , self.conditions) && self.isNotFilterByTopFilter(data[iter])){ //ver que no sea esta fila filtrada por ninguna condicion
			var sum = 0;
			
			if ((tempDataRows.length == 0) && (actualPage > 0) && (!self.paginationInfo.pages[actualPage-1].lastSumShow)){ //verifico que no quede la suma de la primer columna por mostrar de la pagina anterior
				sum = showSubtotalRowsForFirstColumn(self.paginationInfo.pages[actualPage-1].rows[self.paginationInfo.pages[actualPage-1].rows.length-1],
														 data[iter], self)
				if ((sum > 0)){
					tempDataRows.append([self.paginationInfo.pages[actualPage-1].rows[self.paginationInfo.pages[actualPage-1].rows.length-1]])
					extraRows = calculateTotalRowsWithSubtotalRows(tempDataRows[0], data[iter], self)
					sum = 1;
				}
			} //end verifico que no queden sumas por mostrar de la pagina anterior
			
			if ((tempDataRows.length > 0) && ((tempDataRows.length != 1) || (extraRows==0))){
				sum = calculateTotalRowsWithSubtotalRows(tempDataRows[tempDataRows.length-1], data[iter], self)
			}
			
			if (cantRowsActualPage + 1 + sum <= self.autoPagingRowsPerPage){
				tempDataRows.append([data[iter]])
				cantRowsActualPage = cantRowsActualPage + 1 + sum
				lastRowActualPage = iter;
				iter++;	
			} else {
				cantRowsActualPage = cantRowsActualPage + sum
				
				var lastSumShow = (sum > 0) && (cantRowsActualPage == self.autoPagingRowsPerPage) //se mostro la ultima suma
				self.paginationInfo.pages[actualPage] = { firstRow: firstRowActualPage, lastRow: lastRowActualPage, rows: tempDataRows, lastSumShow: lastSumShow, extraRows: extraRows};
				actualPage++;
				firstRowActualPage = iter;
				cantRowsActualPage = 0;
				tempDataRows = [];
				extraRows = 0;
			}
		} else {
			iter++;
		}
			
		
	}
	
	if (cantRowsActualPage < self.autoPagingRowsPerPage){
		self.paginationInfo.pages[actualPage] = { firstRow: firstRowActualPage, lastRow: lastRowActualPage, rows: tempDataRows, extraRows: 0};
		actualPage++;
	}
	self.paginationInfo.totalPages = actualPage;
	
	self.setNewPagesAccount(self.paginationInfo.totalPages);
	self.TotalPagesPaging = self.paginationInfo.totalPages
}

function calculateGrandTotalForLastMeasure(self){
	if (self.actualPaginationPage == self.TotalPagesPaging){
		var dataRows = self.GeneralDataRows
		if (self.RowsWhenMoveToFilter.length > 0){
			dataRows = self.RowsWhenMoveToFilter
		}
		var sum = 0;
		var allNuNValues = true;
		var valuesToOperate = [];
		for (var r=0; r < dataRows.length; r++){
			if (self.isNotFilterByTopFilter(dataRows[r]) && !self.notInBlackList(dataRows[r] , self.conditions)){
				if (self.formulaInfo.measureFormula[measures.length-1].hasFormula){
					var formInfo = self.getFormulaRowByDataRow(dataRows[r], measures.length-1, "")
					if ((formInfo != self.EmptyValue) && (formInfo != "#NuN#")){
						valuesToOperate.push(formInfo)
					}
				} else {
					if (!isNaN(parseFloat(dataRows[r][self.dataColumnIndex]))){
						allNuNValues = false;
						sum = sum + parseFloat(dataRows[r][self.dataColumnIndex])
					}
				}
			}
		}
	
		if (self.formulaInfo.measureFormula[measures.length-1].hasFormula) {
			return self.calculateFormulaTotal([valuesToOperate], measures.length-1, "MeasureInRows")
		} else {
			if (!allNuNValues){
				return sum;
			} else {
				return "#NuN#"
			}
		}
	} else {
		return 0
	}
}


function getTrueCantRows(dataRows, actualcolumn, measures){
	var totalRows = 0;
	if (dataRows[0].length - measures -actualcolumn -1 != 0){
		totalRows = 1 + totalRows;
	}
	var initVal = dataRows[0][actualcolumn];
	var rows = [];
	for (var i = 0; i < dataRows.length; i++) { //for every element from the first column
		if (initVal == dataRows[i][actualcolumn]){
			rows.append([dataRows[i]]);
			if (dataRows[0].length - measures -actualcolumn -1 === 0){
				totalRows = totalRows + 1;
			}
		} else {
			totalRows = 1 + totalRows; //the subtotal for this element
			initVal = dataRows[i][actualcolumn];
			if (dataRows[0].length - measures -actualcolumn -1 > 0){ //recursive call
				totalRows = totalRows + getTrueCantRows(rows, actualcolumn + 1, measures); //add the rows added for sub elements
			}
			rows = [];
			rows.append([dataRows[i]]);
		}
    }  
    if (dataRows[0].length - measures -actualcolumn -1 > 0){ //recursive call
		totalRows = totalRows + getTrueCantRows(rows, actualcolumn + 1, measures); //add the rows added for sub elements
	}
    return totalRows;
}

function getMeasureNumberByName(name, measures){
	var number = 0;
	for(var i = 0; i < measures.length; i++){
		if (measures[i].getAttribute("displayName") == name){
			return i
		}
	}
	return 0
}

function getColumnByDataField(dataField, columns){
	for(var i = 0; i < columns.length; i++){
		if (columns[i].getAttribute("dataField") == dataField){
			return columns[i]
		}
	}
	return null
}

function saveAsIE(){
	var add_point = function(x, y, dragging) {
		x_points.push(x);
		y_points.push(y);
		drag_points.push(dragging);
	};
	var draw = function(){
		canvas.width = canvas.width;
		ctx.lineWidth = 6;
		ctx.lineJoin = "round";
		ctx.strokeStyle = "#000000";
		var
			  i = 0
			, len = x_points.length
		;
		for(; i < len; i++) {
			ctx.beginPath();
			if (i && drag_points[i]) {
				ctx.moveTo(x_points[i-1], y_points[i-1]);
			} else {
				ctx.moveTo(x_points[i]-1, y_points[i]);
			}
			ctx.lineTo(x_points[i], y_points[i]);
			ctx.closePath();
			ctx.stroke();
		}
	};
	var stop_drawing = function() {
		drawing = false;
	};
	var BB = get_blob();
	saveAs(
		  new BB(
			  [self.file.fileName || self.file.fileContent]
			, {type: "text/plain;charset=" + self.file.fileContent.toBase64()}
		)
		, (self.file.fileName || self.file.fileContent) + "." + self.fileExtension
	);
}

//C1Line
function getValueMeasureFromMeasureList(measureList, rowNumber, fg, filterData, cantMeasures){
	for (var itMl = 0; itMl < measureList.length; itMl++){
		if ((measureList[itMl][fg] != undefined) && (measureList[itMl][fg][1][0] == rowNumber)){
			return measureList[itMl][fg][0]
		}
	}
	//when is not in the measure list search in filterData
	return filterData[rowNumber][filterData[0].length-cantMeasures+fg]
}

}



