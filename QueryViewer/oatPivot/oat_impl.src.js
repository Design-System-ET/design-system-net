//OAT_IMPL file
if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
this.contains = function(a, obj) {
    for (var i = 0; i < a.length; i++) {
        if (a[i] === obj) {
            return i;
        }
    }
    return -1;
}

renderJSPivot = function(pivotParams, QueryViewerCollection, queryself){
	if (pivotParams.RealType != "Table") {
		pivotParams.ServerPaging = false;
	}
	if (pivotParams.RealType == "Table"){
		pivotParams.ServerPagingPivot = false;
		jQuery(".oat_winrect_container").remove()
	}
	if ((pivotParams.RememberLayout) && (pivotParams.ServerPaging) && (pivotParams.RealType !=	"PivotTable")){
			var state = OAT.getStateWhenServingPaging(  pivotParams.UcId + '_' + pivotParams.ObjectName, pivotParams.ObjectName)
		if (!state){
			renderJSPivotInter(pivotParams, QueryViewerCollection, null, queryself)
		} else {
			var pageValue = 1;
			if (state.pageSize == "") { state.pageSize = undefined; pageValue = 0; }
			if (this.QueryViewerCollection[pivotParams.UcId].UseRecordsetCache && this.QueryViewerCollection[pivotParams.UcId].RecordsetChanged())
			{
				this.QueryViewerCollection[pivotParams.UcId].getRecordsetCacheKey((function (recordsetCacheKey) {
					
					 this.QueryViewerCollection[pivotParams.UcId].RecordsetCacheOldKey = (this.QueryViewerCollection[pivotParams.UcId].RecordsetCacheActualKey ? this.QueryViewerCollection[pivotParams.UcId].RecordsetCacheActualKey : "");
					 this.QueryViewerCollection[pivotParams.UcId].RecordsetCacheActualKey = recordsetCacheKey; 
					
					this.QueryViewerCollection[pivotParams.UcId].getPageDataForTable((function (resXML) {
									pivotParams.data = resXML;
									//pivotParams.PageSize = state.pageSize;
									pivotParams.previousDataFieldOrder = state.dataFieldOrder;
									pivotParams.orderType = state.orderType;
                       				renderJSPivotInter(pivotParams, QueryViewerCollection, state, queryself)
                        }).closure(this), [pageValue, state.pageSize, true, state.dataFieldOrder, state.orderType, state.filters ]);
                 }).closure(this));
            } else {
            	this.QueryViewerCollection[pivotParams.UcId].getPageDataForTable((function (resXML) {
									pivotParams.data = resXML;
									//pivotParams.PageSize = state.pageSize;
									pivotParams.previousDataFieldOrder = state.dataFieldOrder;
									pivotParams.orderType = state.orderType;
                       				renderJSPivotInter(pivotParams, QueryViewerCollection, state, queryself)
                        }).closure(this), [pageValue, state.pageSize, true, state.dataFieldOrder, state.orderType, state.filters ]);
            }
		}
	} else {
		renderJSPivotInter(pivotParams, QueryViewerCollection, null, queryself)		
	}
} 


renderJSPivotInter = function(pivotParams, QueryViewerCollection, state, queryself){
    var type = pivotParams.RealType
    var container = pivotParams.container
    var page = pivotParams.page
    var content = pivotParams.content
    var chart = pivotParams.chart
    var metadata = pivotParams.metadata
    this.dataString = pivotParams.data
    var queryName = pivotParams.ObjectName
    var controlName = pivotParams.ControlName
    var pageSize = pivotParams.PageSize
    var shrinkToFit = pivotParams.ShrinkToFit
    var DisableColumnSort = pivotParams.DisableColumnSort
    var UcId = pivotParams.UcId
    var rememberLayout = pivotParams.RememberLayout
    this.serverPaging = pivotParams.ServerPaging
    this.previousDataFieldOrder = pivotParams.previousDataFieldOrder; 
    this.previousOrderType = pivotParams.orderType;
    this.previousFilters = (state) ? state.filters : undefined;
    this.previousColumnVisible = (state) ? state.columnVisible : undefined;
    this.previousState = state
    
    this.gridCacheSize = pivotParams.ServerPagingCacheSize;
    
    var ShowMeasuresAsRows = (pivotParams.ShowDataLabelsIn == "Rows")
    if ((rememberLayout == undefined) || (rememberLayout == "false")) {
    	rememberLayout = false
    }
    //recover metadata when metadata error, is there no metadata save it
    try {
    	if (rememberLayout){
    		var savedMd = OAT.GetSavedMetadata(container.id+queryName+UcId+controlName+type+"metadata")
    		if (savedMd === ""){
    			OAT.SaveMetadata(metadata, container.id+queryName+UcId+controlName+type+"metadata")	
    		} else {
    			var t1 = savedMd.split('OLAPDimension').length === metadata.split('OLAPDimension').length;
    			var t2 = savedMd.split('OLAPMeasure').length === metadata.split('OLAPMeasure').length;
    			var t3 = savedMd.split('name').length === metadata.split('name').length;
    			var t4 = true
    			if (t1 && t2 && t3){
    				for(var i=1; i < savedMd.split('name').length; i++){
    					var name1 = savedMd.split('name')[i].substring(2, savedMd.split('name')[i].length).split("\"")[0];
    					var name2 = metadata.split('name')[i].substring(2, metadata.split('name')[i].length).split("\"")[0];
    					if (name1!=name2){
    						t4 = false;
    					}
    				} 
    			} 
    			if (!(t1 && t2 && t3 && t4)){
    				if (!gx.util.browser.isIE()){
						rememberLayout = false	
					} else {
						try {
							if (!!window.localStorage) 
							{
								localStorage.removeItem(OAT.getURL()+container.id+queryName+UcId+controlName+type+"metadata");
								localStorage.removeItem(OAT.getURL()+queryName+UcId);
							}
						} catch (ERROR) {
							
						}
					}
    			}
    		}
    	}
    } catch (ERROR) {
    	
    }
    
    var fullXmlData = jQuery.parseXML(metadata); //metada for formulas a
    var orderFildsHidden = [] 
    
    //Parsear string metadata para remover measures ocultas
    var removeMeasures = []; //number of remove measures
    var headMetadata;
    if (metadata.indexOf('<OLAPDimension') != -1){
    	headMetadata = metadata.substring(0, metadata.indexOf('<OLAPDimension') );
    } else {
    	headMetadata = metadata.substring(0, metadata.indexOf('<OLAPMeasure') );
    }
	var restMetadata = metadata.substring(metadata.indexOf('<OLAPDimension'), metadata.length);
	
	var dimensionString = restMetadata.split("<OLAPDimension");
	for(var i = 1; i < dimensionString.length; i++){
		if (dimensionString[i].length > 0){
			if (dimensionString[i].indexOf('</OLAPDimension>') != -1){
				dimensionString[i] = dimensionString[i].substring(0,dimensionString[i].indexOf('</OLAPDimension>'));
			} else {
				dimensionString[i] = dimensionString[i].substring(0,dimensionString[i].indexOf('/>')) + ">";
			}
			
			if (dimensionString[i].indexOf("defaultPosition=\"hidden\"") != -1){//hide dimension
				try {
					if (dimensionString[i].indexOf("displayName=") != -1){
						var infoDisplayName = dimensionString[i].substring(dimensionString[i].indexOf("displayName=")+13)
						orderFildsHidden.push(infoDisplayName.split('"')[0])
					}
				} catch (ERROR) {}
				dimensionString[i] = "";
			} else {
				dimensionString[i] = "<OLAPDimension " + dimensionString[i] + "</OLAPDimension>"
			}
		} else {
				dimensionString[i] = "";
		}
	}
	
	var measuresString = restMetadata.split("<OLAPMeasure");
	for(var i = 1; i < measuresString.length; i++){
		//if (measuresString[i].indexOf("OLAPDimension") == -1){
			if (measuresString[i].indexOf("</OLAPMeasure") != -1){
				measuresString[i] = "<OLAPMeasure " +  measuresString[i].substring(0,measuresString[i].indexOf('</OLAPMeasure>')) + "</OLAPMeasure>"
			} else if (measuresString[i].indexOf("/>") != -1) {
				measuresString[i] = "<OLAPMeasure " +  measuresString[i].substring(0,measuresString[i].indexOf('/>')) + "/>";
			} else {
				measuresString[i] = "";
			}
		//} else {
		//	measuresString[i] = "";
		//}
	}
	
	var taileMetadata = "</OLAPCube>";
	
	metadata = "";
	metadata = headMetadata;
	for (var i = 1; i < dimensionString.length; i++){
		if (dimensionString[i].length > 0){
			metadata = metadata + dimensionString[i];
		}
	}
	for (var i = 1; i < measuresString.length; i++){
		if (measuresString[i].length > 0){
			metadata = metadata + measuresString[i] 
		}
	}
	metadata = metadata + taileMetadata;
	//end parsing metadata
	
    
	var xmlDoc = jQuery.parseXML(metadata);
	
    var defaultPicture = xmlDoc.childNodes[0];
    
    this.IdForQueryViewerCollection = UcId;
    UcId = UcId.replace(/,/g, "")
    this.UcId = UcId;
    this.pivotDiv = container.id;
    this.query = queryName;
    this.control = controlName;
    this.pageSize = pageSize;
    this.shrinkToFit = shrinkToFit;
	this.disableColumnSort = DisableColumnSort;
    this.header = [];
    this.data = [];
    this.columnNumbers = [];
    this.rowNumbers = [];
    this.filterNumbers = [];
    this.cols = 0;
    this.conditionalFormats = [];
    this.conditionalFormatsColumns = []; //conditional format for columns - needed?
    this.formatValues = [];
    this.formatValuesMeasures = [];
    
    this.forPivotFormatValues = [];
    this.forPivotFormats = [];
    this.forPivotCustomPicture = [];
    this.forPivotCustomFormat = [];
	
	
    //Columns and measures names
    var columns = xmlDoc.getElementsByTagName("OLAPDimension");
    var columnNames = [];
    var rowNames = [];
    var filterNames = [];
    this.measures = xmlDoc.getElementsByTagName("OLAPMeasure");
    var columnsDataType = [];
    var measureNames = [];
    var j = 0;
    var k = 0;
    var preHeader = [];
	var formulaInfo = { measureFormula: [], itemPosition: []}
    //handle pictures
    var orderFilds = [];
    var datePictures = [];
    var intPictures = [];
    var dateFields = [];
    var intFields = [];

    //get columns
    for (var i = 0; i < columns.length; i++) {
        if (columns[i].attributes.getNamedItem("defaultPosition").nodeValue == "rows") {
            columnNames[j] = columns[i].attributes.getNamedItem("displayName").nodeValue;
            preHeader[i] = columnNames[j];
            j++;
        }
        if (columns[i].attributes.getNamedItem("defaultPosition").nodeValue == "columns") {
            rowNames[k] = columns[i].attributes.getNamedItem("displayName").nodeValue;
            preHeader[i] = rowNames[k];
            k++;
        }
        if (columns[i].attributes.getNamedItem("defaultPosition").nodeValue == "filters") {
            filterNames[k] = columns[i].attributes.getNamedItem("displayName").nodeValue;
            preHeader[i] = filterNames[k];
            k++;
        }
		columnsDataType[i] = columns[i].attributes.getNamedItem("dataType").nodeValue;
        //handle formats values
        if (columns[i].childNodes.length > 0) {
            if (columns[i].childNodes != null) {
                for (var m = 0; m < columns[i].childNodes.length; m++) {
                    if (columns[i].childNodes[m].localName == "formatValues") {
                        for (var n = 0; n < columns[i].childNodes[m].childNodes.length; n++) {
                            if (columns[i].childNodes[m].childNodes[n].localName == "value") {
                                var value = {};
                                value.format = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("format").nodeValue;
                                value.recursive = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("recursive").nodeValue;
                                var crude = columns[i].childNodes[m].childNodes[n].textContent;
                                value.value = crude.replace(/^\s+|\s+$/g, '');
                                value.columnNumber = i;
                                this.formatValues.push(value);
                            }
                        }
                    }
                }
            }
        }//handle conditional formats
        if (columns[i].childNodes.length > 0) {
            if (columns[i].childNodes != null) {
                for (var m = 0; m < columns[i].childNodes.length; m++) {
                    if (columns[i].childNodes[m].localName == "conditionalFormats") {
                        for (var n = 0; n < columns[i].childNodes[m].childNodes.length; n++) {
                            if (columns[i].childNodes[m].childNodes[n].localName == "rule") {
                                var format = {};
                                format.format = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("format").nodeValue;
                                format.operation1 = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("op1").nodeValue;
                                format.value1 = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("value1").nodeValue;
                                if (columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("op2") != null) {
                                    format.operation2 = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("op2").nodeValue;
                                    format.value2 = columns[i].childNodes[m].childNodes[n].attributes.getNamedItem("value2").nodeValue;
                                }
                                format.columnNumber = i;
                                this.conditionalFormatsColumns.push(format);
                            }
                        }
                    }

                }
            }
        }

        //manage pictures
        if (columns[i].attributes.getNamedItem("picture").nodeValue != "") {
            if (columns[i].attributes.getNamedItem("dataType").nodeValue == "date") {
                datePictures.push(columns[i].attributes.getNamedItem("picture").nodeValue);
                dateFields.push(columns[i].attributes.getNamedItem("dataField").nodeValue);
            }
            //if (columns[i].attributes.getNamedItem("dataType").nodeValue == "integer" || measures[i].attributes.getNamedItem("dataType").nodeValue == "real") {
            if (columns[i].attributes.getNamedItem("dataType").nodeValue == "integer") {
                intPictures.push(columns[i].attributes.getNamedItem("picture").nodeValue);
                intFields.push(columns[i].attributes.getNamedItem("dataField").nodeValue);
            }
            
        }
        this.forPivotCustomPicture.push(columns[i].attributes.getNamedItem("picture").nodeValue);
        this.forPivotCustomFormat.push(columns[i].attributes.getNamedItem("format").nodeValue);
        orderFilds.push(columns[i].attributes.getNamedItem("dataField").nodeValue); 
        
    }
    
    //var measures;
    for (var i = 0; i < this.measures.length; i++) {
        measureNames[i] = this.measures[i].attributes.getNamedItem("displayName").nodeValue;
        //manage format values
        if (measures[i].childNodes.length > 0) {
            if (measures[i].childNodes != null) {
                for (var m = 0; m < measures[i].childNodes.length; m++) {
                    if (measures[i].childNodes[m].localName == "formatValues") {
                        for (var n = 0; n < measures[i].childNodes[m].childNodes.length; n++) {
                            if (measures[i].childNodes[m].childNodes[n].localName == "value") {
                                var value = {};
                                value.format = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("format").nodeValue;
                                value.recursive = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("recursive").nodeValue;
                                var crude = measures[i].childNodes[m].childNodes[n].textContent;
                                value.value = crude.replace(/^\s+|\s+$/g, '');
                                value.columnNumber = i;
                                this.formatValuesMeasures.push(value);
                            }
                        }
                    }

                }
            }
        }
        //manage conditional formats
        if (measures[i].childNodes.length > 0) {
            if (measures[i].childNodes != null) {
                for (var m = 0; m < measures[i].childNodes.length; m++) {
                    if (measures[i].childNodes[m].localName == "conditionalFormats") {
                        for (var n = 0; n < measures[i].childNodes[m].childNodes.length; n++) {
                            if (measures[i].childNodes[m].childNodes[n].localName == "rule") {
                                var format = {};
                                format.format = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("format").nodeValue;
                                format.operation1 = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("op1").nodeValue;
                                format.value1 = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("value1").nodeValue;
                                if (measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("op2") != null) {
                                    format.operation2 = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("op2").nodeValue;
                                    format.value2 = measures[i].childNodes[m].childNodes[n].attributes.getNamedItem("value2").nodeValue;
                                }
                                format.columnNumber = i; //+ columns.length; Only the measure number
                                this.conditionalFormats.push(format);
                            }
                        }
                    }
                }
            }
        }

        //manage pictures
        if (measures[i].attributes.getNamedItem("picture").nodeValue != "") {
            if (measures[i].attributes.getNamedItem("dataType").nodeValue == "date") {
                datePictures.push(measures[i].attributes.getNamedItem("picture").nodeValue);
                dateFields.push(measures[i].attributes.getNamedItem("dataField").nodeValue);
            }
            if (measures[i].attributes.getNamedItem("dataType").nodeValue == "integer" || measures[i].attributes.getNamedItem("dataType").nodeValue == "real") {
                intPictures.push(measures[i].attributes.getNamedItem("picture").nodeValue);
                intFields.push(measures[i].attributes.getNamedItem("dataField").nodeValue);/* */
            }
        }
         
        //manage formula
        if ((measures[i].attributes.getNamedItem("formula") != undefined) && (measures[i].attributes.getNamedItem("formula").nodeValue != "")) {
        	formulaInfo.measureFormula.push( { hasFormula: true, textFormula: measures[i].attributes.getNamedItem("formula").nodeValue } )
        } else {
        	formulaInfo.measureFormula.push( { hasFormula: false } )
        }
        
        this.forPivotCustomPicture.push(measures[i].attributes.getNamedItem("picture").nodeValue);
        orderFilds.push(measures[i].attributes.getNamedItem("dataField").nodeValue);
    }
	
	
    this.header = preHeader.concat(measureNames);
  
    this.getDataFromXML = function(dataString){
		var stringRecord = dataString.split("<Record>")
    
    	//get server pagination info
    	if (this.serverPaging){
    		this.ServerRecordCount = parseToIntRegisterValue(stringRecord[0], "RecordCount")
    		this.ServerPageCount   = parseToIntRegisterValue(stringRecord[0], "PageCount")
    		this.ServerPageNumber  = parseToIntRegisterValue(stringRecord[0], "PageNumber")
    	}
    	//get records of the table
    	
    
    	for (var i = 1; i < stringRecord.length; i++) {
     		var recordData = [];
     		var fullRecordData = [];
       		for (var j = 0; j < orderFilds.length; j++) {
       			recordData[j] = "#NuN#"
       			var dt = stringRecord[i].split("<" + orderFilds[j] + ">")
       			if (dt.length > 1){
       				var at = dt[1].split("</" + orderFilds[j] + ">")
       				/*var rp = at[0].replace(/^\s+|\s+$/g, '')
       				recordData[j] = (rp != "") ? rp : undefined*/
       				recordData[j] = at[0]
       				fullRecordData[j] = recordData[j] 
       			} else {
       				if (stringRecord[i].indexOf("<" + orderFilds[j] + "/>") >= 0){
       					recordData[j] = ""
       					fullRecordData[j] = ""
       				}
       				
       			}
       		}
       		this.data.push(recordData);
        	
        	var pos_init = orderFilds.length;
       		for (var j = 0; j < orderFildsHidden.length; j++) {
       			fullRecordData[pos_init + j] = undefined
       			var dt = stringRecord[i].split("<" + orderFildsHidden[j] + ">")
       			if (dt.length > 1){
       				var at = dt[1].split("</" + orderFildsHidden[j] + ">")
       				fullRecordData[pos_init + j] = at[0]
       			}
       		}
       		this.fullRecord.push(fullRecordData);
       		if (fullRecordData.length > this.maxLengthRecord) this.maxLengthRecord = fullRecordData.length; 
    	}
	}
    
    this.maxLengthRecord = 0;
    this.data = []
    this.fullRecord = []  //array to store extra data for formulas
    
    if (!pivotParams.ServerPagingPivot){
    	this.getDataFromXML(this.dataString);
    } else {
    	ShowMeasuresAsRows = ((ShowMeasuresAsRows) && (measures.length > 1)) 
    	this.pagingData = {}//OATGetDataFromXMLForPivot(this.dataString, ShowMeasuresAsRows);
    	this.pagingData.dataFields = orderFilds;
    }
    
    var furmulaIndex = {}
	for (var j = 0; j < orderFildsHidden.length; j++) {
		furmulaIndex[orderFildsHidden[j]] = orderFilds.length + j	
	}
	formulaInfo.itemPosition = furmulaIndex
	formulaInfo.recordDataLength = this.maxLengthRecord;
	formulaInfo.cantFormulaMeasures = 0;
    
    for(var n = 0; n < formulaInfo.measureFormula.length; n++){
			if ( formulaInfo.measureFormula[n].hasFormula){
				formulaInfo.cantFormulaMeasures++;
			
				var inlineFormula = formulaInfo.measureFormula[n].textFormula
			
				var inline = inlineFormula	
				var opers = ['*','-','+','/','(',')']	
				for (var j = 0; j < opers.length; j++){
					var inline2 = inline.split(opers[j])
					if (inline2.length > 1){
						inline = ""
						for(var i = 0; i < inline2.length-1; i++){
							inline = inline + inline2[i] + " " + opers[j] + " "  
						}
						inline = inline + inline2[inline2.length-1]
					}
				}
		
				var polishNot = InfixToPostfix(inline)
				formulaInfo.measureFormula[n].polishNotationText = polishNot
				var items = polishNot.split(" ") 
				while (items.indexOf("") != -1){
					 items.splice(items.indexOf(""), 1)
				}
				var relatedMeasure = []
				for(var k = 0; k < items.length;k++){
					if ((opers.indexOf(items[k]) == -1) && (isNaN(parseInt(items[k])))){
						//add item
						var operPositionInDataRow = formulaInfo.itemPosition[items[k]]
						if (relatedMeasure.indexOf(operPositionInDataRow) == -1)
							relatedMeasure.push(operPositionInDataRow)
					}
				}
			
				formulaInfo.measureFormula[n].relatedMeasures = relatedMeasure
			
				var arrayNot = polishNot.split(" ")
				while (arrayNot.indexOf("") != -1){
					 arrayNot.splice(arrayNot.indexOf(""), 1)
				}
				formulaInfo.measureFormula[n].PolishNotation = arrayNot
			} 
		}
	
		var l = 0;
    	var m = 0;
    	var d = 0;
    	for (var i = 0; i < preHeader.length; i++) {
    	    if (this.contains(columnNames, preHeader[i]) != -1) {
    	        this.columnNumbers[l] = i;
    	        l++;
    	    }
    	    if (this.contains(rowNames, preHeader[i]) != -1) {
    	        this.rowNumbers[m] = i;
    	        m++;
    	    }
    	    if (this.contains(filterNames, preHeader[i]) != -1) {
    	        this.filterNumbers[d] = i;
    	        d++;
    	    }
    	}
    
    	//crear arreglo de columnas
    	var colms = new Array();
    	for(var t=0; t < columns.length; t++){
    		colms[t] = columns[t];
    	}
    
    
    var pivot;
    
    queryName = queryName.replace(/\./g,"")
    
    if (type == "PivotTable") {
        OAT.Style.include("pivot.css");
        pivot = OAT_JS.pivot.cb(this.pivotDiv, page, content, chart, defaultPicture, QueryViewerCollection, colms, 
        							this.formatValues, this.conditionalFormatsColumns, this.formatValuesMeasures, this.shrinkToFit, this.disableColumnSort, this.UcId, this.IdForQueryViewerCollection,
        							rememberLayout, ShowMeasuresAsRows, formulaInfo, this.fullRecord,
        							pivotParams.ServerPagingPivot, this.pagingData, orderFildsHidden); 
        if (pivot == 'error'){
        	return;
        }
    } else {
        if (type == "Table") {
        	OAT.Style.include("grid.css");        
            pivot = OAT_JS.grid.cb(this.pivotDiv, this.UcId + '_' + queryName,columnsDataType, defaultPicture, this.forPivotCustomPicture, this.conditionalFormatsColumns, 
            						this.formatValues, this.forPivotCustomFormat, colms, columns, QueryViewerCollection, this.pageSize, this.disableColumnSort, this.UcId, this.IdForQueryViewerCollection,
            						rememberLayout, this);
        }
    }
    //add pagination functionality
    if (type == "Table") {
    	this.pageSize = (this.previousState) ? this.previousState.pageSize : pageSize;
    	
    	var rowNum = this.pageSize; 
    	if ((pivot.rowsPerPage!=undefined)&&(pivot.rowsPerPage != "")){
    			rowNum = pivot.rowsPerPage;
    	} 
    	
    	if (this.pageSize) {
        		setTimeout(function(){
        			var options = {
        				currPage: this.ServerPageNumber,
        				ignoreRows: jQuery('tbody tr[visibQ=tf]', jQuery("#" + this.UcId + "_" + queryName)), 
        				optionsForRows: [10, 15, 20],
        				rowsPerPage: rowNum != 'undefined' ? rowNum : 10,
        				jstype: "table", 
        				topNav: false,
        				controlName: this.UcId  + "_" + queryName,
        				cantPages: this.ServerPageCount,
        				controlUcId: this.UcId,
        				control: this
    				}
    				if (!self.serverPaging){
        				jQuery("#" + this.UcId  + "_" + queryName).tablePagination(options);
        			} else {
        				jQuery("#" + this.UcId  + "_" + queryName).partialTablePagination(options);	
        			}
             		var wd2 = jQuery("#" + this.UcId + "_" + queryName)[0].clientWidth - 1;
					jQuery("#" + this.UcId + "_" + queryName + "_tablePagination").css({width: wd2+"px" });
					if (jQuery("#" + this.UcId + "_" + queryName + "_tablePagination").css('display') === 'none'){
							jQuery("#" + this.UcId+ "_" + queryName).css({marginBottom: "0px"});
					}else{
						jQuery("#" + this.UcId + "_" + queryName).css( "margin-bottom", "0px");
					}
					
					if ((jQuery("#" + this.UcId  + "_" + queryName + "_tablePagination_paginater").length>0) && ( jQuery("#" + this.UcId  + "_" + queryName + "_tablePagination")[0].getBoundingClientRect().bottom < jQuery("#" + this.UcId + "_" + queryName + "_tablePagination_paginater")[0].getBoundingClientRect().bottom)){
         			   		jQuery("#" + this.UcId  + "_" + queryName + "_tablePagination").css({height: "65px", marginBottom: "0px"})
         			}
					var wd = jQuery("#" + this.UcId  + "_" + queryName)[0].offsetWidth - 4;
					jQuery("#" + this.UcId  + "_" + queryName + "_grid_top_div").css({width: wd+"px" });
					
					if ((this.serverPaging) && (this.pageSize == 10)){
        				if (this.ServerPageCount <= 1){ //hide pagiantion
    						jQuery("#" + this.UcId  + "_" + queryName + "_tablePagination").css({ visibility: "hidden",  height: "0px" });
    					}
    				}
					
        		}, 50)
        		
        		
        		
        }
    	var wd = jQuery("#" + this.UcId + "_" + queryName)[0].offsetWidth - 4;
    	try{ 
    		if ($("#MAINFORM")[0].className.indexOf("form-horizontal") > -1){
    			wd = wd + 4;
    		}
    	} catch (Error) {
    	}
		jQuery("#" + this.UcId + "_" + queryName + "_grid_top_div").css({width: wd+"px" });
	    
	    //set interval for handler values infinite scroll
	    if (self.serverPaging){
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
	    								OAT_JS.grid.readScrollValue(UcId, columnNumber)
	    							}
	    						}
	    					}
	    			  	}
	    		},
	    	250)
	    }
	    
    }
	
	if (!pivotParams.ServerPagingPivot){
		if ((jQuery("#" + this.UcId + "_" + queryName)[0].clientWidth < 380) && (!this.shrinkToFit)){
			jQuery("#" + this.UcId + "_" + queryName).css({width: "400px"});
		}
		if (this.shrinkToFit){
			jQuery("#" + this.UcId + "_" + queryName).css({minWidth: "10px"});
		}
		if ((!gx.util.browser.isIPad()) && (!gx.util.browser.isIPhone())){
		
			jQuery(".h2title").css({height: "25px"})
			jQuery(".h2titlewhite").css({height: "25px"})
			jQuery(".header_value").css({height: "25px"})
			jQuery(".header_value").css({lineHeight: "25px"})
		
		}
	}
	
	this.getDataForTable = function(UcId, pageNumber, rowsPerPage, recalculateCantPages, DataFieldOrder, OrderType, DataFieldFilter, DataFieldBlackList, restoreDefaultView, fromExternalRefresh){
		if ((recalculateCantPages) || (DataFieldOrder != "") || (DataFieldFilter != "")) {
			OAT_JS.grid.cleanCache(this, UcId);
		}
		if (DataFieldOrder != "") OAT_JS.grid.gridData[UcId].dataFieldOrder = DataFieldOrder;
		if (OrderType != "") OAT_JS.grid.gridData[UcId].orderType = OrderType; 
		if (DataFieldFilter != "") OAT_JS.grid.updateFilterInfo(UcId, DataFieldFilter, DataFieldBlackList);
		if (restoreDefaultView) { OAT_JS.grid.restoreDefaultView(UcId);  rowsPerPage = OAT_JS.grid.gridData[UcId].rowsPerPage }
		
		if ((recalculateCantPages) || (DataFieldOrder != "") || (DataFieldFilter != "")) {//save state
			OAT.SaveStateWhenServerPaging( OAT_JS.grid.gridData[UcId].grid, UcId, rowsPerPage, OAT_JS.grid.gridData[UcId].dataFieldOrder, OAT_JS.grid.gridData[UcId].orderType, OAT_JS.grid.gridData[UcId].filterInfo, OAT_JS.grid.gridData[UcId].blackLists, OAT_JS.grid.gridData[UcId].columnVisible)
		}
		
		//OAT_JS.grid.gridData[UcId].actualPageNumber = pageNumber
		OAT_JS.grid.gridData[UcId].rowsPerPage = rowsPerPage
		if (rowsPerPage == "") { rowsPerPage = undefined; pageNumber = 0;}
		
		if (!OAT_JS.grid.pageInCache(UcId, pageNumber)){
			this.QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].getPageDataForTable((function (resXML) {
									if (pageNumber == 0) { pageNumber = 1, recalculateCantPages = false; }
                       				OAT_JS.grid.redraw(this, UcId, resXML, recalculateCantPages, DataFieldOrder != "", pageNumber, fromExternalRefresh)
                        }).closure(this), [pageNumber, rowsPerPage, recalculateCantPages, OAT_JS.grid.gridData[UcId].dataFieldOrder, OAT_JS.grid.gridData[UcId].orderType, OAT_JS.grid.gridData[UcId].filterInfo ]);
        } else {
        	OAT_JS.grid.redraw(this, UcId, OAT_JS.grid.pageInCache(UcId, pageNumber), false, false, pageNumber)
        }                
	}
	
	this.getValuesForColumn = function(UcId, columnNumber, filterValue){
		var dataField = OAT_JS.grid.gridData[UcId].columnDataField[columnNumber]
		if (filterValue != ""){
			var page = 1
			QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    				var res = JSON.parse(resJSON);
	    				OAT_JS.grid.changeValues(UcId, dataField, columnNumber, res, filterValue);
			}).closure(this), [dataField, page, 10, filterValue]);
		} else {
			OAT_JS.grid.resetScrollValue(UcId, dataField, columnNumber)
		}
	}
	
	queryself.oat_element = pivot;
	//redraw if table and initial ascending or descending
	if ( (self.serverPaging) && (type == "Table") && (OAT_JS.grid.gridData[this.UcId].dataFieldOrder != "")){
		this.getDataForTable(this.UcId, 1, OAT_JS.grid.gridData[this.UcId].rowsPerPage, false, OAT_JS.grid.gridData[this.UcId].dataFieldOrder, OAT_JS.grid.gridData[this.UcId].orderType, "", "")		
	}
    //return pivot;
}

var OAT_JS = {};

OAT_JS.grid = {
    panel: 1,
    tab: 4,
    div: "",
    needs: ["grid"],
    gridData: [],
    cb: function(content, controlName, columnsDataType, defaultPicture, forPivotCustomPicture, conditionalFormatsColumns, formatValues, forPivotCustomFormar, colms, columns, 
    					QueryViewerCollection, pageSize, disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout, _mthis) {
        this.gridData[UcId] = {};
        this.gridData[UcId].div = content;
        this.gridData[UcId].controlName = controlName;
        
        this.gridData[UcId].actualPageNumber = 1;
        this.gridData[UcId].rowsPerPage = (_mthis.previousState) ? _mthis.previousState.pageSize : pageSize;
        this.gridData[UcId].actualCantPages = _mthis.ServerPageCount;
        
        this.gridData[UcId].grid = new OAT.Grid(content, 0, 1, controlName, query, columnsDataType, colms, QueryViewerCollection, this.gridData[UcId].rowsPerPage, 
        						 disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout, _mthis.serverPaging);
        this.gridData[UcId].grid.oat_component = this;
        
        
        //initialize 
        this.gridData[UcId].columnDataField = []
        for (var id = 0; id < columns.length; id++){
        	this.gridData[UcId].columnDataField[id] = columns[id].getAttribute("dataField");
        }
         
        this.gridData[UcId].grid.createHeader(header, this.gridData[UcId].columnDataField);
        
        //initialize default data
        this.gridData[UcId].defaultValues = {}
        this.gridData[UcId].defaultValues.rowsPerPage = pageSize;
        this.gridData[UcId].defaultValues.dataFieldOrder = "";
        this.gridData[UcId].defaultValues.orderType = ""
        this.gridData[UcId].defaultValues.filterInfo = [];
        
        this.gridData[UcId].IdForQueryViewerCollection = IdForQueryViewerCollection;
        
        //initialize cache
        this.gridData[UcId].cacheSize = (_mthis.gridCacheSize == 0) ? (_mthis.ServerPageCount*((this.gridData[UcId].rowsPerPage > 0) ? this.gridData[UcId].rowsPerPage : 1)) : _mthis.gridCacheSize;
        this.gridData[UcId].gridCache = [];
        this.gridData[UcId].gridCache[0] = { page: 1, pageData: _mthis.dataString};
        this.gridData[UcId].nextCachePos = 1;
        //end initialize cache
        
        //initialize column visibility
        this.gridData[UcId].columnVisible = [];
        for (var id = 0; id < columns.length; id++){
        	this.gridData[UcId].columnVisible[id] = true
        	if (_mthis.previousColumnVisible){
        		this.gridData[UcId].columnVisible[id] = _mthis.previousColumnVisible[id]
        		if (!this.gridData[UcId].columnVisible[id])
        			this.gridData[UcId].grid.hideColumnHeader(id); 
        	} 
        }
        //initialize  dataFields
        this.gridData[UcId].columnDataField = []
        this.gridData[UcId].originalColumnDataField = []
        for (var id = 0; id < columns.length; id++){
        	this.gridData[UcId].columnDataField[id] = columns[id].getAttribute("dataField");
        	this.gridData[UcId].originalColumnDataField[id] = columns[id].getAttribute("dataField");
        }
        
        //initialize filter info
        this.gridData[UcId].filterInfo = [];
        this.gridData[UcId].differentValues = [];
        this.gridData[UcId].blackLists = [];
        this.gridData[UcId].differentValuesPaginationInfo = [];
        this.gridData[UcId].filteredValuesPaginationInfo = [];
        for (var id = 0; id < columns.length; id++){
        	this.gridData[UcId].differentValues[columns[id].getAttribute("dataField")] = [];
        	this.gridData[UcId].blackLists[columns[id].getAttribute("dataField")] = { state: "all", visibles:[], hiddens:[], defaultAction: "Include", hasNull: true};
        	this.gridData[UcId].differentValuesPaginationInfo[columns[id].getAttribute("dataField")] = { blocked:   false,  previousPage: 0,
        																								 totalPages: true, filtered: false }
        	this.gridData[UcId].filteredValuesPaginationInfo[columns[id].getAttribute("dataField")] = { previousPage: 0, totalPages: 0, filteredText: "", values: [] }
        }
        
        this.gridData[UcId].defaultNullText = defaultPicture.getAttribute("textForNullValues");
        
        if (_mthis.previousFilters != undefined){//load previus state filter info
        	this.gridData[UcId].filterInfo = _mthis.previousFilters;
        	for(var u = 0; u < this.gridData[UcId].filterInfo.length; u++){
        		for (var t = 0; t < this.gridData[UcId].filterInfo[u].NotNullValues.Included.length; t++){
        			var val = this.gridData[UcId].filterInfo[u].NotNullValues.Included[t];
        			if (this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].visibles.indexOf(val) == -1){
        				this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].visibles.push(val); 	
        			}
        		}
        		
        		for (var t = 0; t < this.gridData[UcId].filterInfo[u].NotNullValues.Excluded.length; t++){
        			var val = this.gridData[UcId].filterInfo[u].NotNullValues.Excluded[t];
					if (this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].hiddens.indexOf(val) == -1){
        				this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].hiddens.push(val); 	
        			}
        		}
        		
        		if (this.gridData[UcId].filterInfo[u].NotNullValues.Included.length >  0){
        			this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].state = "";
        		} else {
        			this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].state = "none";
        		}
        		
        		this.gridData[UcId].blackLists[this.gridData[UcId].filterInfo[u].DataField].defaultAction = this.gridData[UcId].filterInfo[u].NotNullValues.DefaultAction;
        		
        	}
        }
        //end initialize filter info
        //initialize order info
        this.gridData[UcId].dataFieldOrder = (_mthis.previousDataFieldOrder != undefined) ? _mthis.previousDataFieldOrder : ""
        this.gridData[UcId].orderType = (_mthis.previousOrderType != undefined) ? _mthis.previousOrderType : ""
        //end initialize order info
        //for custom order
        this.gridData[UcId].customOrderValues = [];
        for (var index = 0; index < columns.length; index++){
        	if((columns[index].getAttribute("order")!=undefined) && (columns[index].getAttribute("order")==="custom")){
       			result = this.gridData[UcId].grid.applyCustomSort(index, columns[index], data);
       			data = result[0]
       			this.gridData[UcId].customOrderValues[index] = result[1] 
       		} else {
       			this.gridData[UcId].customOrderValues[index] = false
       		}
       	}	
        
        this.gridData[UcId].rowsMetadata = { columnsDataType: columnsDataType, defaultPicture: defaultPicture, forPivotCustomPicture: forPivotCustomPicture,
        						conditionalFormatsColumns: conditionalFormatsColumns, formatValues: formatValues, forPivotCustomFormat: forPivotCustomFormat, columns: columns}
        for (var i = 0; i < data.length; i++) {
            this.gridData[UcId].grid.createRow(data[i], columnsDataType, defaultPicture, forPivotCustomPicture, conditionalFormatsColumns, formatValues, forPivotCustomFormat, columns, null, this.gridData[UcId].columnVisible);
        }
        
        //for ascending or descending order
        if (!_mthis.serverPaging){
        	for (var index = 0; index < columns.length; index++){
        		if((columns[index].getAttribute("order")!=undefined) && (columns[index].getAttribute("order")==="ascending")){
       				this.gridData[UcId].grid.applySortOrderType(index, 2);
       			}	
       			if((columns[index].getAttribute("order")!=undefined) && (columns[index].getAttribute("order")==="descending")){
       				this.gridData[UcId].grid.applySortOrderType(index, 3);
       			}
       			if (columns[index].getAttribute("order")!=undefined){
       				break;
       			}
       		}
       	} else {
       		var withOrderCustom = false
       		for (var index = 0; index < columns.length; index++){
       			if ( (columns[index].getAttribute("order")!=undefined) && (columns[index].getAttribute("order")!="") ){
       				this.gridData[UcId].dataFieldOrder = columns[index].getAttribute("dataField")
       				this.gridData[UcId].orderType = (columns[index].getAttribute("order") == "descending") ? "Descending" : "Ascending";
       				withOrderCustom = (columns[index].getAttribute("order") == "custom")   
       				break;
       			} 
       		}
       		
       		if ((this.gridData[UcId].dataFieldOrder != "") && (!withOrderCustom)){
       			var columnOrderNumber = 0;
	    		for(var t = 0; t < this.gridData[UcId].grid.columns.length; t++){
	    			if (this.gridData[UcId].dataFieldOrder == this.gridData[UcId].columnDataField[t]){ //this.gridData[UcId].grid.columns[t].getAttribute("dataField")){
	    				columnOrderNumber = t; 
	    				break;
	    			}
	    		}
	    		//var colNumber = columns.length - 1 - columnOrderNumber;
	    		this.gridData[UcId].grid.applySortOrderType(columnOrderNumber, (this.gridData[UcId].orderType.toLowerCase()=="ascending")?2:3);
       		}
       	}
       
        //apply custom filters
        for (var index = 0; index < columns.length; index++){
        	this.gridData[UcId].grid.applyCustomFilter(index, columns[index]);
        }
        
        //load different values
        if (_mthis.serverPaging){
        	OAT_JS.grid.initValueRead(UcId, 0)
        	
        	//call OnFirstPage on load
        	QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].CurrentPage = 1;
        	if ( typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnFirstPage) == 'function' ) QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnFirstPage()
        }
        
        if (!_mthis.serverPaging){
			this.gridData[UcId].grid.applySaveState(this.gridData[UcId].rowsPerPage);
		}
        
        return this.gridData[UcId].grid;
    },
    redraw: function(_mthis, UcId, xmlData, recalculateCantPages, moveToFirstPage, pageNumber, fromExternalRefresh){ 
    	//add data to cache
    	if (!this.pageInCache(UcId, pageNumber)){
    		this.gridData[UcId].gridCache.push({ page: pageNumber, pageData: xmlData })
    		if (this.gridData[UcId].gridCache.length > this.gridData[UcId].cacheSize){
    			this.gridData[UcId].gridCache.splice(0, 1) 
    		}
    	} else {
    		var indCache = -1;
    		for (var cP = 0; cP < this.gridData[UcId].gridCache.length; cP++){
    			if (this.gridData[UcId].gridCache[cP].page == pageNumber){
    				indCache = cP;
    			}
    		}
    		this.gridData[UcId].gridCache.splice(indCache, 1)  
    		this.gridData[UcId].gridCache.push({ page: pageNumber, pageData: xmlData })
    	}
    	//end add data to cache
    	_mthis.data = []
    	_mthis.getDataFromXML(xmlData);
    	if (_mthis.ServerPageCount >= 0){
    		this.gridData[UcId].actualCantPages = _mthis.ServerPageCount;
    	}
    	this.gridData[UcId].grid.removeAllRows();
    	if (recalculateCantPages) {
    		if (jQuery("#" + this.gridData[UcId].controlName + "_tablePagination " + "#tablePagination_totalPages").length > 0){
    			jQuery("#" + this.gridData[UcId].controlName + "_tablePagination " + "#tablePagination_totalPages")[0].innerHTML = " "+_mthis.ServerPageCount;
    			if ((_mthis.ServerPageCount <= 1) && (this.gridData[UcId].rowsPerPage == 10)) { //hide pagiantion
    				jQuery("#" + this.gridData[UcId].controlName + "_tablePagination ").css({ visibility: "hidden",  height: "0px" });
    			} else {
    				jQuery("#" + this.gridData[UcId].controlName + "_tablePagination ").css({ visibility: "visible",  height: "36px" });
    				if (_mthis.ServerPageCount == 1) {
            	         $('#' + this.gridData[UcId].controlName + '_tablePagination_paginater').css('display', 'none');
            	    } else {
            	         $('#' + this.gridData[UcId].controlName + '_tablePagination_paginater').css('display', '');
            	    }
    			}
    		}
    	}
    	
    	//call navigational events
    	if (OAT_JS.grid.gridData[UcId].actualPageNumber != pageNumber){
    		QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].CurrentPage = pageNumber;
    		if (pageNumber == 1){
    			if ( typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnFirstPage) == 'function' ) QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnFirstPage()
    		} else if (pageNumber == this.gridData[UcId].actualCantPages){
    			if ( typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnLastPage) == 'function' ) QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnLastPage()
    		} else if (pageNumber < OAT_JS.grid.gridData[UcId].actualPageNumber){
    			if ( typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnPreviousPage) == 'function' ) QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnPreviousPage()
    		} else {
    			if ( typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnNextPage) == 'function') QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].OnNextPage()
    		}
    		
    	}
    	
    	//call autorefresh
    	if ((!fromExternalRefresh) && (QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].AutoRefreshGroup != "")){
    		var meta = OAT.createXMLMetadata(OAT_JS.grid.gridData[UcId],null,true);
    		
    		var spl =  OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection; 
    		var listennings = QueryViewerCollection[spl]; 
        	if ((listennings != "") && (listennings != null) && (listennings != undefined)){
        	 	listennings.onValuesChangedEvent(meta);
        	}
    	}
    	
    	//set new current page
    	OAT_JS.grid.gridData[UcId].actualPageNumber = pageNumber
    	currPageNumber[this.gridData[UcId].controlName] = pageNumber
    	jQuery("#" + this.gridData[UcId].controlName + "_tablePagination " + "#tablePagination_currPage").val(pageNumber)
    	
    	for (var i = 0; i < data.length; i++) {
    		var dataByField = []
    		var tempcolumnsDataType = []; var tempforPivotCustomPicture = []; var tempforPivotCustomFormat = []; var tempcolumnVisible = [];
    		for (var p = 0; p < this.gridData[UcId].columnDataField.length; p++){
    			var pos = this.gridData[UcId].originalColumnDataField.indexOf(this.gridData[UcId].columnDataField[p])
    			dataByField[p] = data[i][pos];
    			tempcolumnsDataType[p] = this.gridData[UcId].rowsMetadata.columnsDataType[pos];
    			tempforPivotCustomPicture[p] =	this.gridData[UcId].rowsMetadata.forPivotCustomPicture[pos]
    			tempforPivotCustomFormat[p] = this.gridData[UcId].rowsMetadata.forPivotCustomFormat[pos]
    			tempcolumnVisible[p] = this.gridData[UcId].columnVisible[pos]
    		}
            this.gridData[UcId].grid.createRow(dataByField/*data[i]*/, tempcolumnsDataType, this.gridData[UcId].rowsMetadata.defaultPicture, 
            							 tempforPivotCustomPicture, this.gridData[UcId].rowsMetadata.conditionalFormatsColumns, 
            							 this.gridData[UcId].rowsMetadata.formatValues, tempforPivotCustomFormat, this.gridData[UcId].rowsMetadata.columns,
            							 null, tempcolumnVisible);
        }
    },
    getDifferentValues: function(UcId, columnNumber, iter){ //return the list of distint values of a column, and if it checked or not
    	var dataField = this.gridData[UcId].columnDataField[columnNumber];//this.gridData[UcId].grid.columns[columnNumber].getAttribute("dataField")
    	var value = this.gridData[UcId].differentValues[dataField][iter]
    	var pos = this.gridData[UcId].originalColumnDataField.indexOf(this.gridData[UcId].columnDataField[columnNumber])
    	var pict_value = OAT.defaultPictureValue(value.trim(), this.gridData[UcId].rowsMetadata.columnsDataType[pos], 
    							this.gridData[UcId].rowsMetadata.defaultPicture, this.gridData[UcId].rowsMetadata.forPivotCustomPicture[pos]).replace(/ /g, "&nbsp;");
    	
    	if ((this.gridData[UcId].blackLists[dataField].hasNull) && (value.trim() == this.gridData[UcId].defaultNullText)){
    		value = "#NuN#" //tomo este valor como el null
    		return false;//{value: "", checked: true, pict_value: ""} 
    	}
    	var checked = true;
	    if (this.gridData[UcId].blackLists[dataField].state != "all"){
	    	if (this.gridData[UcId].blackLists[dataField].visibles.find(value) < 0){
	    			checked = false;
	    	}
		}
		
    	return {value: value, checked: checked, pict_value: pict_value};
    },
    getCantDifferentValues: function(UcId, columnNumber){
    	var dataField = this.gridData[UcId].columnDataField[columnNumber];//this.gridData[UcId].grid.columns[columnNumber].getAttribute("dataField")
    	return this.gridData[UcId].differentValues[dataField].length
    },
    cleanCache: function(_mthis, UcId){
    	for (var cP = 0; cP < this.gridData[UcId].gridCache.length; cP++){
        	this.gridData[UcId].gridCache[cP] = { page: -1, pageData: ""}; 
        }
        this.gridData[UcId].nextCachePos = 0;
    },
    pageInCache: function(UcId, pageNumber){
    	for (var cP = 0; cP < this.gridData[UcId].gridCache.length; cP++){
    		if (this.gridData[UcId].gridCache[cP].page == pageNumber){
    			return this.gridData[UcId].gridCache[cP].pageData;
    		}
    	}
    	return false;
    },
    updateFilterInfo: function(UcId, DataFieldFilter, NewFilter){
    	if (NewFilter.op == "all"){
    		//remove filter from filterInof
    		var pos = -1;
    		for (var p =0; p < this.gridData[UcId].filterInfo.length; p++){
    			if (DataFieldFilter == this.gridData[UcId].filterInfo[p].DataField) 
    			{ pos = p; break; }
    		}
    		if (pos > -1) this.gridData[UcId].filterInfo.splice(pos, 1)
    		this.gridData[UcId].blackLists[DataFieldFilter].state = "all"
    		this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
    		this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    		this.gridData[UcId].blackLists[DataFieldFilter].defaultAction = "Include"
    		return;
    	}
    	
    	if (this.gridData[UcId].blackLists[DataFieldFilter].state == "none"){
    		this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
    		for(var u = 0; u < this.gridData[UcId].differentValues[DataFieldFilter].length; u++){
    			if (this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(this.gridData[UcId].differentValues[DataFieldFilter][u]) == -1){
    				this.gridData[UcId].blackLists[DataFieldFilter].hiddens.push(this.gridData[UcId].differentValues[DataFieldFilter][u])
    			} 
    		}
    	} else if (this.gridData[UcId].blackLists[DataFieldFilter].state == "all"){
    		this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    		for(var u = 0; u < this.gridData[UcId].differentValues[DataFieldFilter].length; u++){
    			if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(this.gridData[UcId].differentValues[DataFieldFilter][u]) == -1){
    				this.gridData[UcId].blackLists[DataFieldFilter].visibles.push(this.gridData[UcId].differentValues[DataFieldFilter][u])
    			} 
    		}
    	}
    	
    	var notNullValue = [];
    	if (NewFilter.op == "none"){
    		notNullValue = [];
    		this.gridData[UcId].blackLists[DataFieldFilter].state = "none"
    		this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
    		this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    		this.gridData[UcId].blackLists[DataFieldFilter].defaultAction = "Exclude"
    	} else {
    		
    		if (NewFilter.op == "push"){
    			this.gridData[UcId].blackLists[DataFieldFilter].state = ""
    			var pos = this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(NewFilter.values)
    			if (pos > -1) this.gridData[UcId].blackLists[DataFieldFilter].visibles.splice(pos, 1);
    			if (this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(NewFilter.values) == -1)
    				this.gridData[UcId].blackLists[DataFieldFilter].hiddens.push(NewFilter.values)	
    		} else if (NewFilter.op == "pop") {
    			this.gridData[UcId].blackLists[DataFieldFilter].state = ""
    			if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(NewFilter.values) == -1)
    				this.gridData[UcId].blackLists[DataFieldFilter].visibles.push(NewFilter.values)
    			var pos = this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(NewFilter.values)
    			if (pos > -1) this.gridData[UcId].blackLists[DataFieldFilter].hiddens.splice(pos, 1);
    		} else if (NewFilter.op == "reverse") {
    			if  (this.gridData[UcId].blackLists[DataFieldFilter].defaultAction == "Include"){ 
    				this.gridData[UcId].blackLists[DataFieldFilter].defaultAction = "Exclude"
    			} else {
    				this.gridData[UcId].blackLists[DataFieldFilter].defaultAction = "Include"
    			}
    			if (this.gridData[UcId].blackLists[DataFieldFilter].state == "none"){//si el estado anterior es none pasa a all
    				var pos = -1;
    				for (var p =0; p < this.gridData[UcId].filterInfo.length; p++){
    					if (DataFieldFilter == this.gridData[UcId].filterInfo[p].DataField) 
    					{ pos = p; break; }
    				}
    				if (pos > -1) this.gridData[UcId].filterInfo.splice(pos, 1)
    				this.gridData[UcId].blackLists[DataFieldFilter].state = "all"
    				this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
    				this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    				return;
    			} else if (this.gridData[UcId].blackLists[DataFieldFilter].state == "all"){//si el estado anterior es all pasa a none
    				notNullValue = [];
    				this.gridData[UcId].blackLists[DataFieldFilter].state = "none"
    				this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
    				this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    			} else {
    				
    				var tempArrayVisibles = []; for(var tit = 0; tit < this.gridData[UcId].blackLists[DataFieldFilter].visibles.length; tit++){tempArrayVisibles.push(this.gridData[UcId].blackLists[DataFieldFilter].visibles[tit])}
					var tempArrayHiddens = []; for(var tit = 0; tit < this.gridData[UcId].blackLists[DataFieldFilter].hiddens.length; tit++){tempArrayHiddens.push(this.gridData[UcId].blackLists[DataFieldFilter].hiddens[tit])}
					
					this.gridData[UcId].blackLists[DataFieldFilter].visibles = []
					this.gridData[UcId].blackLists[DataFieldFilter].hiddens = []
    		
    				for(var u = 0; u < this.gridData[UcId].differentValues[DataFieldFilter].length; u++){
    					var val = this.gridData[UcId].differentValues[DataFieldFilter][u];
    					if (tempArrayVisibles.indexOf(val) == -1){
    						this.gridData[UcId].blackLists[DataFieldFilter].visibles.push(val)
    					} else {
    						this.gridData[UcId].blackLists[DataFieldFilter].hiddens.push(val)
    					}  
    				}
    				for(var u = 0; u < tempArrayHiddens.length; u++){
    					if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(tempArrayHiddens[u]) == -1){
    						this.gridData[UcId].blackLists[DataFieldFilter].visibles.push(tempArrayHiddens[u])
    					}
    				}
    				for(var u = 0; u < tempArrayVisibles.length; u++){
    					if (this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(tempArrayVisibles[u]) == -1){
    						this.gridData[UcId].blackLists[DataFieldFilter].hiddens.push(tempArrayVisibles[u])
    					}
    				}
    			}
    		}
    	}
    	
    	var filterExist = false; var nullIncluded = true;
    	var included = [];
    	for(var t = 0; t < this.gridData[UcId].blackLists[DataFieldFilter].visibles.length; t++){
    		if (this.gridData[UcId].blackLists[DataFieldFilter].visibles[t] != "#NuN#"){
    			included.push(this.gridData[UcId].blackLists[DataFieldFilter].visibles[t])
    		}
    	}
    	var excluded = [];
    	if (this.gridData[UcId].blackLists[DataFieldFilter].state != "none"){
    		for(var t = 0; t < this.gridData[UcId].differentValues[DataFieldFilter].length; t++){
    			var val = this.gridData[UcId].differentValues[DataFieldFilter][t]
    			if ((val != "#NuN#") && (included.indexOf(val) == -1)){
    				excluded.push(val)
    			}
    		}
    		for(var t = 0; t < this.gridData[UcId].blackLists[DataFieldFilter].hiddens.length; t++){
    			if ((this.gridData[UcId].blackLists[DataFieldFilter].hiddens[t] != "#NuN#")
    			&& (excluded.indexOf(this.gridData[UcId].blackLists[DataFieldFilter].hiddens[t]) == -1)) {
    				excluded.push(this.gridData[UcId].blackLists[DataFieldFilter].hiddens[t])
    			}
    		}
    		if ((included.length == 0) && ((this.gridData[UcId].blackLists[DataFieldFilter].defaultAction == "Exclude"))){
    			excluded = [];
    		}
    	}
    	
    	if (NewFilter.op == "none"){
    		nullIncluded = false;
    		included = []; excluded = [];
    	} else {
    		if ( (this.gridData[UcId].differentValues[DataFieldFilter].indexOf("#NuN#") > -1) || 
    				(excluded.indexOf(this.gridData[UcId].blackLists[DataFieldFilter].hiddens[t]) != -1)) {
    			if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf("#NuN#") == -1){
    				nullIncluded = false;
    			}
    		} else {
    			if (this.gridData[UcId].blackLists[DataFieldFilter].defaultAction == "Exclude"){
    				nullIncluded = false;
    			}
    		}
    	}
    	
    	
    	if ((this.gridData[UcId].blackLists[DataFieldFilter].hasNull) && (!(NewFilter.op == "none"))){	
    		//asociated psuedo-Null
    		var reallyPseudoNull = this.gridData[UcId].defaultNullText
    		var finded = false
    		var data_length = 0;
    		for(var u = 0; u < this.gridData[UcId].differentValues[DataFieldFilter].length; u++){
    			data_length = this.gridData[UcId].differentValues[DataFieldFilter][u].length;
    		}
    		for(var u = 0; u < this.gridData[UcId].differentValues[DataFieldFilter].length; u++){
    			if (this.gridData[UcId].differentValues[DataFieldFilter][u].trim() == this.gridData[UcId].defaultNullText){
    				reallyPseudoNull = this.gridData[UcId].differentValues[DataFieldFilter][u];
    				finded = true;
    				break;
    			}
    		}
    		if (!finded){
    			for(var t=0; t<data_length-this.gridData[UcId].defaultNullText.length; t++){
    				reallyPseudoNull = reallyPseudoNull + " ";
    			}
    		}
    		 
    		if (!nullIncluded){	
    			if (excluded.indexOf(reallyPseudoNull) == -1){
    				excluded.push(reallyPseudoNull)
    				if (this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(reallyPseudoNull) == -1){
    					this.gridData[UcId].blackLists[DataFieldFilter].hiddens.push(reallyPseudoNull)
    				}
    			} 	
    			if (included.indexOf(reallyPseudoNull) != -1){
    				included.splice(included.indexOf(reallyPseudoNull),1)
    			}
    			if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(reallyPseudoNull) != -1){
    				this.gridData[UcId].blackLists[DataFieldFilter].visibles.splice(this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(reallyPseudoNull),1)
    			}
    		} else {
    			if (included.indexOf(reallyPseudoNull) == -1){
    				if (excluded.indexOf(reallyPseudoNull) != -1){
    					excluded.splice(excluded.indexOf(reallyPseudoNull),1)
    					included.push(reallyPseudoNull)
    				} else {
    					if (this.gridData[UcId].blackLists[DataFieldFilter].defaultAction == "Exclude"){
    						included.push(reallyPseudoNull)
    					}
    				}
    				if (this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(reallyPseudoNull) != -1){
    					this.gridData[UcId].blackLists[DataFieldFilter].hiddens.splice(this.gridData[UcId].blackLists[DataFieldFilter].hiddens.indexOf(reallyPseudoNull),1)
    					if (this.gridData[UcId].blackLists[DataFieldFilter].visibles.indexOf(reallyPseudoNull) == -1){
    						this.gridData[UcId].blackLists[DataFieldFilter].visibles.push(reallyPseudoNull)
    					}
    				}
    			}	
    		}
    	}
    	
    	var allValuesLoaded = false;
    	if (this.gridData[UcId].differentValuesPaginationInfo[DataFieldFilter] != null){
    		allValuesLoaded = (this.gridData[UcId].differentValuesPaginationInfo[DataFieldFilter].previousPage == this.gridData[UcId].differentValuesPaginationInfo[DataFieldFilter].totalPages)
    	}
    	var noFilterNeeded = ( ((nullIncluded) || (!this.gridData[UcId].blackLists[DataFieldFilter].hasNull)) 
    							&&  (excluded.length == 0) && (NewFilter.op != "none") && (NewFilter.op != "push")
    							&&	((this.gridData[UcId].blackLists[DataFieldFilter].defaultAction == "Include") || (allValuesLoaded))
    						);
    	
    		
    	var pos = 0;
    	for (var t = 0; t < this.gridData[UcId].filterInfo.length; t++){
    		if (this.gridData[UcId].filterInfo[t].DataField == DataFieldFilter){
    			filterExist = true;
    			this.gridData[UcId].filterInfo[t].NullIncluded = nullIncluded
    			this.gridData[UcId].filterInfo[t].NotNullValues.Included = included
    			this.gridData[UcId].filterInfo[t].NotNullValues.Excluded = excluded
    			this.gridData[UcId].filterInfo[t].NotNullValues.DefaultAction = this.gridData[UcId].blackLists[DataFieldFilter].defaultAction
    			pos = t;
    		}
    	}
    	if (noFilterNeeded){
    		this.gridData[UcId].filterInfo.splice(pos, 1)
    	}
    	if ((!filterExist) && (!noFilterNeeded)){
    		var notNullValues = {Included: included, Excluded: excluded, DefaultAction:this.gridData[UcId].blackLists[DataFieldFilter].defaultAction}
    		filter = { DataField: DataFieldFilter, NullIncluded: nullIncluded, NotNullValues: notNullValues }
    		this.gridData[UcId].filterInfo.push(filter);
    	}
    },
	
    readScrollValue: function(UcId, columnNumber){
    	var dataField =  this.gridData[UcId].columnDataField[columnNumber];
    	var posColumnNumber = this.gridData[UcId].originalColumnDataField.indexOf(this.gridData[UcId].columnDataField[columnNumber])
    	if (!this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked){
    		this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = true;
    		if (!this.gridData[UcId].differentValuesPaginationInfo[dataField].filtered){
    			var ValuePageInfo  = this.gridData[UcId].differentValuesPaginationInfo[dataField]
    			var page = ValuePageInfo.previousPage + 1; 
    			this.gridData[UcId].lastRequestValue = dataField;
    			QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var res = JSON.parse(resJSON);
	    			OAT_JS.grid.appendNewValueData(UcId, res)
				}).closure(this), [dataField, page, 10, ""]);
    		} else {
    			var ValuePageInfo  = this.gridData[UcId].filteredValuesPaginationInfo[dataField]
    			var page = ValuePageInfo.previousPage + 1; 
    			this.gridData[UcId].lastRequestValue = dataField;
    			var filterText = ValuePageInfo.filteredText
    			QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var res = JSON.parse(resJSON);
	    			OAT_JS.grid.appendNewFilteredValueData(UcId, res, posColumnNumber, filterText)
				}).closure(this), [dataField, page, 10, ValuePageInfo.filteredText]);
    		}
    	}
    	var j = 0; 
    },
    appendNewValueData: function(UcId, data, whenFilter){
    	var dataField = this.gridData[UcId].lastRequestValue
    	var ValuePageInfo  = this.gridData[UcId].differentValuesPaginationInfo[dataField]
    	if ((data.PageNumber > ValuePageInfo.previousPage) || (whenFilter)){
    		this.gridData[UcId].differentValuesPaginationInfo[dataField].previousPage = data.PageNumber
    		this.gridData[UcId].differentValuesPaginationInfo[dataField].totalPages   = data.PagesCount
    		var newValues = [];
    		
    		if (data.Null){
    			if (this.gridData[UcId].differentValues[dataField].indexOf("#NuN#") == -1){
    				this.gridData[UcId].differentValues[dataField].push("#NuN#")
    			}
    		}
    		
    		//add to differentValues
	    	for (var i = 0; i < data.NotNullValues.length; i++){
	    		var val = data.NotNullValues[i];
	    		if (this.gridData[UcId].differentValues[dataField].indexOf(val) == -1){
	    			this.gridData[UcId].differentValues[dataField].push(val)
	    			newValues.push(val)
	    		}//lo mismo
	    		if (this.gridData[UcId].blackLists[dataField].defaultAction == "Include"){
	    			if ((this.gridData[UcId].blackLists[dataField].visibles.indexOf(val) == -1)
	    			&& (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(val) == -1)){
	    				this.gridData[UcId].blackLists[dataField].visibles.push(val)
	    			}	
	    		} else {
	    			if ((this.gridData[UcId].blackLists[dataField].visibles.indexOf(val) == -1)
	    			&& (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(val) == -1)){
	    				this.gridData[UcId].blackLists[dataField].hiddens.push(val)
	    			}
	    		}
	    	}
	    	
	    	var columnNumber = this.gridData[UcId].columnDataField.indexOf(dataField);
	    	var originalColumn = this.gridData[UcId].originalColumnDataField.indexOf(dataField)
	    	
	    	this.gridData[UcId].grid.loadDifferentValues(columnNumber, this.gridData[UcId].differentValues[dataField])
	    	
	    	for (var nI = 0; nI < newValues.length; nI++){
	    		var checked = true;
	    		if (this.gridData[UcId].blackLists[dataField].state != "all"){
	    			if (this.gridData[UcId].blackLists[dataField].visibles.find(newValues[nI]) < 0){
	    				checked = false;
	    			}
				}
				
				if (!((this.gridData[UcId].blackLists[dataField].hasNull) && (newValues[nI].trim() == this.gridData[UcId].defaultNullText))){
    				var pict_value = OAT.defaultPictureValue(newValues[nI].trim(), this.gridData[UcId].rowsMetadata.columnsDataType[originalColumn], 
    							this.gridData[UcId].rowsMetadata.defaultPicture, this.gridData[UcId].rowsMetadata.forPivotCustomPicture[originalColumn]).replace(/ /g, "&nbsp;");
    				OAT.appendNewPairToPopUp(this.gridData[UcId], newValues[nI], columnNumber, checked, pict_value, dataField)
    			}
	    	}
    	}
    	if (this.gridData[UcId].differentValuesPaginationInfo[dataField].previousPage < data.PagesCount)
    		this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = false;
    },
    resetScrollValue: function(UcId, dataField, columnNumber){ //after filtered when input serach is clean, restor values without filter
    	this.gridData[UcId].differentValuesPaginationInfo[dataField].filtered = false;
    	this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked  = true;
    	
    	var columnNumber = this.gridData[UcId].columnDataField.indexOf(dataField);
	    var originalColumn = this.gridData[UcId].originalColumnDataField.indexOf(dataField)
    	
    	OAT.removeAllPairsFromPopUp(this.gridData[UcId], columnNumber, OAT_JS.grid.cantPages(UcId,dataField)>1);
    	
    	for(var u = 0; u < this.gridData[UcId].differentValues[dataField].length; u++){
    		var checked = true;
    		var value = this.gridData[UcId].differentValues[dataField][u];
	    	if (this.gridData[UcId].blackLists[dataField].state != "all"){
	    		if (this.gridData[UcId].blackLists[dataField].visibles.find(value) < 0){
	    			checked = false;
	    		}
			}
			var pict_value = OAT.defaultPictureValue(value.trim(), this.gridData[UcId].rowsMetadata.columnsDataType[originalColumn], 
    							this.gridData[UcId].rowsMetadata.defaultPicture, this.gridData[UcId].rowsMetadata.forPivotCustomPicture[originalColumn]).replace(/ /g, "&nbsp;");
    		
    		if (!((this.gridData[UcId].blackLists[dataField].hasNull) && (value.trim() == this.gridData[UcId].defaultNullText))){
    			OAT.appendNewPairToPopUp(this.gridData[UcId], value, columnNumber, checked, pict_value, dataField) 
    		}
    	}
    	
    	if (this.gridData[UcId].differentValuesPaginationInfo[dataField].previousPage < OAT_JS.grid.cantPages(UcId,dataField))
    		this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = false;
    },
    resetAllScrollValue: function(UcId){ //when closing the filter popup
    	for (var id = 0; id < this.gridData[UcId].grid.columns.length; id++){
    		var field = this.gridData[UcId].grid.columns[id].getAttribute("dataField");
    		this.gridData[UcId].differentValuesPaginationInfo[field].filtered = false;
    		this.gridData[UcId].differentValuesPaginationInfo[field].blocked  = true;
    		if (this.gridData[UcId].differentValuesPaginationInfo[field].previousPage < this.gridData[UcId].differentValuesPaginationInfo[field].totalPages)
	    		this.gridData[UcId].differentValuesPaginationInfo[field].blocked  = false;
    	}
    },
    appendNewFilteredValueData: function(UcId, data, columnNumber, filterValue){ //add pairs when filtering by filter input
    	var dataField     = this.gridData[UcId].lastRequestValue
    	var columnNumber = this.gridData[UcId].columnDataField.indexOf(dataField);
    	var originalColumn = this.gridData[UcId].originalColumnDataField.indexOf(dataField)
    	
    	var ValuePageInfo = this.gridData[UcId].filteredValuesPaginationInfo[dataField]
    	if (((filterValue) || (filterValue=="")) && (ValuePageInfo.filteredText != filterValue)){
    		return;
    	}
    	if (data.PageNumber > ValuePageInfo.previousPage){
    		this.gridData[UcId].filteredValuesPaginationInfo[dataField].previousPage = data.PageNumber
    		this.gridData[UcId].filteredValuesPaginationInfo[dataField].totalPages =  data.PagesCount
    		
    		if (data.Null){
    			if (this.gridData[UcId].differentValues[dataField].indexOf("#NuN#") == -1){
    				this.gridData[UcId].differentValues[dataField].push("#NuN#")
    			}
    		}
    		
    		for (var i = 0; i < data.NotNullValues.length; i++){
    			var alreadyInValues = (this.gridData[UcId].differentValues[dataField].indexOf(data.NotNullValues[i]) != -1)
    			//append to different values
    			if (this.gridData[UcId].differentValues[dataField].indexOf(data.NotNullValues[i]) == -1){
	    			this.gridData[UcId].differentValues[dataField].push(data.NotNullValues[i])
	    		}
    			if ((this.gridData[UcId].blackLists[dataField].defaultAction == "Include") && (!alreadyInValues)){
	    			if ((this.gridData[UcId].blackLists[dataField].visibles.indexOf(data.NotNullValues[i]) == -1)
	    			 && (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(data.NotNullValues[i]) == -1)){
	    				this.gridData[UcId].blackLists[dataField].visibles.push(data.NotNullValues[i])
	    			} 	
	    		} else {
	    			if ((this.gridData[UcId].blackLists[dataField].visibles.indexOf(data.NotNullValues[i]) == -1)
	    			 && (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(data.NotNullValues[i]) == -1)){
	    				this.gridData[UcId].blackLists[dataField].hiddens.push(data.NotNullValues[i])
	    			}
	    		}
    			    			
    			var checked = true;
	    		if (this.gridData[UcId].blackLists[dataField].state != "all"){
	    			if (this.gridData[UcId].blackLists[dataField].visibles.find(data.NotNullValues[i]) < 0){
	    				checked = false;
	    			}
				}
				var pict_value = OAT.defaultPictureValue(data.NotNullValues[i].trim(), this.gridData[UcId].rowsMetadata.columnsDataType[originalColumn], 
    							this.gridData[UcId].rowsMetadata.defaultPicture, this.gridData[UcId].rowsMetadata.forPivotCustomPicture[originalColumn]).replace(/ /g, "&nbsp;");
    			if (!((this.gridData[UcId].blackLists[dataField].hasNull) && (data.NotNullValues[i].trim() == this.gridData[UcId].defaultNullText))){
    				OAT.appendNewPairToPopUp(this.gridData[UcId], data.NotNullValues[i], columnNumber, checked, pict_value, dataField)
    			}
    		}
    		if (this.gridData[UcId].filteredValuesPaginationInfo[dataField].previousPage < data.PagesCount)
    			this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = false;
    	}
    },
    initValueRead: function(UcId, columnNumber){
    	if (columnNumber >= this.gridData[UcId].grid.columns.length){
    		return;
    	} else {
    		this.gridData[UcId].grid.lastRequestValue = this.gridData[UcId].grid.columns[columnNumber].getAttribute("dataField")
    		var cantItems = 10;
    		if ((QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].AutoRefreshGroup != "")
    			/*|| (typeof(QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].FilterChanged) == 'function')*/) {
    			cantItems = 0;
    		}
			QueryViewerCollection[this.gridData[UcId].IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var data = JSON.parse(resJSON);
	    			//load data
	    			dataField = this.gridData[UcId].grid.lastRequestValue
	    			this.gridData[UcId].differentValuesPaginationInfo[dataField].previousPage = data.PageNumber
    				this.gridData[UcId].differentValuesPaginationInfo[dataField].totalPages   = data.PagesCount
    				
    				//end load data
	    			var columnNumber = 0;
	    			for(var t = 0; t < this.gridData[UcId].grid.columns.length; t++){
	    				if (this.gridData[UcId].grid.lastRequestValue == this.gridData[UcId].grid.columns[t].getAttribute("dataField")){
	    					columnNumber = t; 
	    					break;
	    				}
	    			}
	    			
	    			
	    			//null value?
    				if (data.Null){
    					this.gridData[UcId].blackLists[dataField].hasNull = true;
    					if (this.gridData[UcId].differentValues[dataField].indexOf("#NuN#") == -1){
    						this.gridData[UcId].differentValues[dataField].push("#NuN#")
    					}
    					var nullIncluded = true;
    					for  (var i = 0; i < this.gridData[UcId].filterInfo.length; i++){
    						if (this.gridData[UcId].filterInfo[i].DataField == dataField){
    							if (!this.gridData[UcId].filterInfo[i].NullIncluded){
    								nullIncluded = false;
    							}
    						} 
    					}
    					if ((nullIncluded) && (this.gridData[UcId].blackLists[dataField].visibles.indexOf("#NuN#") == -1)){
    						this.gridData[UcId].blackLists[dataField].visibles.push("#NuN#");
    					}
    				} else {
    					this.gridData[UcId].blackLists[dataField].hasNull = false;
    				}
    				
    				
    				for (var i = 0; i < data.NotNullValues.length; i++){
	    				if (this.gridData[UcId].differentValues[dataField].indexOf(data.NotNullValues[i]) == -1){
	    					this.gridData[UcId].differentValues[dataField].push(data.NotNullValues[i])
	    				}
	    				if ( (this.gridData[UcId].blackLists[dataField].state == "all") 
	    						&& (this.gridData[UcId].blackLists[dataField].visibles.indexOf(data.NotNullValues[i]) == -1)){
	    					this.gridData[UcId].blackLists[dataField].visibles.push(data.NotNullValues[i])
	    				}
	    				if ( (this.gridData[UcId].blackLists[dataField].state == "none") 
	    						&& (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(data.NotNullValues[i]) == -1)){
	    					this.gridData[UcId].blackLists[dataField].hiddens.push(data.NotNullValues[i])
	    				}
	    			}
    				
    				
	    			
	    			this.gridData[UcId].grid.loadDifferentValues(columnNumber, this.gridData[UcId].differentValues[dataField]);
	    			
	    			columnNumber++;
	    			OAT_JS.grid.initValueRead(UcId, columnNumber)
    				
			}).closure(this), [this.gridData[UcId].grid.lastRequestValue , 1, cantItems, ""]);
    	}
    },
    changeValues: function(UcId, dataField, columnNumber, data, filterText){ //when filter by search filter, delete pairs and show new ones
    	var searchInput = jQuery("#"+UcId+dataField)[0];
    	
    	if (((searchInput.value) || (searchInput.value == "")) && (searchInput.value != filterText)){
    		return;
    	} 
    	
    	var columnNumber = this.gridData[UcId].columnDataField.indexOf(dataField);
	    var originalColumn = this.gridData[UcId].originalColumnDataField.indexOf(dataField)
	    
    	this.gridData[UcId].differentValuesPaginationInfo[dataField].filtered = true;
    	this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = true;
    	OAT.removeAllPairsFromPopUp(this.gridData[UcId], columnNumber, data.PagesCount>1);
    	
    	//set filtered pagination info
    	this.gridData[UcId].filteredValuesPaginationInfo[dataField].previousPage  = 1
    	this.gridData[UcId].filteredValuesPaginationInfo[dataField].totalPages  = data.PagesCount
    	this.gridData[UcId].filteredValuesPaginationInfo[dataField].filteredText = filterText
    	
    	for (var i = 0; i < data.NotNullValues.length; i++) {
    		var alreadyInValues = (this.gridData[UcId].differentValues[dataField].indexOf(data.NotNullValues[i]) != -1)
    		//append to different values
    		if (this.gridData[UcId].differentValues[dataField].indexOf(data.NotNullValues[i]) == -1){
	    		this.gridData[UcId].differentValues[dataField].push(data.NotNullValues[i])
	    	}
	    	if ((this.gridData[UcId].blackLists[dataField].state == "all") || 
    		  ((this.gridData[UcId].blackLists[dataField].defaultAction == "Include") && (!alreadyInValues)) ){
    		  	//if Include new values and is a new value
	    		if ((this.gridData[UcId].blackLists[dataField].visibles.indexOf(data.NotNullValues[i]) == -1) 
	    		 && (this.gridData[UcId].blackLists[dataField].hiddens.indexOf(data.NotNullValues[i]) == -1)){
	    			this.gridData[UcId].blackLists[dataField].visibles.push(data.NotNullValues[i])
	    		}	
	    	} 
    		//
    		
    		
    		var checked = true;
	    	if (this.gridData[UcId].blackLists[dataField].state != "all"){
	    		if (this.gridData[UcId].blackLists[dataField].visibles.find(data.NotNullValues[i]) < 0){
	    			checked = false;
	    		}
			}
    		this.gridData[UcId].filteredValuesPaginationInfo[dataField].values.push(data.NotNullValues[i]);
    		var pict_value = OAT.defaultPictureValue(data.NotNullValues[i].trim(), this.gridData[UcId].rowsMetadata.columnsDataType[originalColumn], 
    							this.gridData[UcId].rowsMetadata.defaultPicture, this.gridData[UcId].rowsMetadata.forPivotCustomPicture[originalColumn]).replace(/ /g, "&nbsp;");
    		if (!((this.gridData[UcId].blackLists[dataField].hasNull) && (data.NotNullValues[i].trim() == this.gridData[UcId].defaultNullText))){
    			OAT.appendNewPairToPopUp(this.gridData[UcId], data.NotNullValues[i], columnNumber, checked, pict_value, dataField);
    		}
	    }
	    
	    if (data.PagesCount > 0)
	    	this.gridData[UcId].differentValuesPaginationInfo[dataField].blocked = false;
    },
    setColumnVisibleValue: function(UcId, column, visible){
    	var origColumnNumber = this.gridData[UcId].originalColumnDataField.indexOf(this.gridData[UcId].columnDataField[column])
    	this.gridData[UcId].columnVisible[origColumnNumber] = visible
    	OAT.SaveStateWhenServerPaging( OAT_JS.grid.gridData[UcId].grid,     UcId,  OAT_JS.grid.gridData[UcId].rowsPerPage, OAT_JS.grid.gridData[UcId].dataFieldOrder, 
    									   OAT_JS.grid.gridData[UcId].orderType, OAT_JS.grid.gridData[UcId].filterInfo, OAT_JS.grid.gridData[UcId].blackLists, OAT_JS.grid.gridData[UcId].columnVisible)
    },
    getTableWhenServerPagination: function(UcId){
    	 var res = QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].getPageDataForTableSync([1, 0, true, "", "", OAT_JS.grid.gridData[UcId].filterInfo ]);
    	 var t = 0;
    	 var records = res.split("<Page PageNumber=\"1\">")
    	 var rec = "<Table>" + records[1]
    	 var last = rec.split("</Page>");
    	 var finalRes = last[0] + "</Table>";
    	 return finalRes;
    },
    setFilterChangedWhenServerPagination: function(UcId, oatDimension){
    	if (QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].FilterChanged) {
    		var df = oatDimension.getAttribute("dataField")
    	
    		var difValues = OAT_JS.grid.gridData[UcId].differentValues[df];
    		var diffValuesPagInfo = OAT_JS.grid.gridData[UcId].differentValuesPaginationInfo[df];
    		
    		if (OAT_JS.grid.gridData[UcId].differentValuesPaginationInfo[df].previousPage == OAT_JS.grid.gridData[UcId].differentValuesPaginationInfo[df].totalPages){
    			var blacInfo = OAT_JS.grid.gridData[UcId].blackLists[df]
    	
    			var datastr = "<DATA name=\"" + oatDimension.getAttribute("name") + "\" displayName=\"" +  oatDimension.getAttribute("displayName") + "\">"
			
				for (var dvi = 0; dvi < difValues.length; dvi++){
					var checked = true;
			   		if (OAT_JS.grid.gridData[UcId].blackLists[df].state != "all"){
			   			if (OAT_JS.grid.gridData[UcId].blackLists[df].visibles.find(difValues[dvi]) < 0){
	    					checked = false;
	    				}
					}
					if (checked){
						datastr = datastr + '<VALUE>' + difValues[dvi] + '</VALUE>';
					}
				}
			    	
    			datastr = datastr + "</DATA>"
    		
        	    var xml_doc = QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].xmlDocument(datastr);
        	    var Node = QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].selectXPathNode(xml_doc, "/DATA");
        	    QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].FilterChangedData.Name = Node.getAttribute("name");
        	    QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].FilterChangedData.SelectedValues = [];
        	    var valueIndex=-1;
        	    for (var i=0; i<Node.childNodes.length; i++)
            	if (Node.childNodes[i].nodeName == "VALUE")
            	{
            	    valueIndex++;
            	    QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].FilterChangedData.SelectedValues[valueIndex] = Node.childNodes[i].firstChild.nodeValue;
           		}
           		QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].FilterChanged();
        	} else {
        		OAT_JS.grid.gridData[UcId].differentValuesPaginationInfo[df].blocked = true;
				var ValuePageInfo = OAT_JS.grid.gridData[UcId].differentValuesPaginationInfo[df]
    			var page = ValuePageInfo.previousPage + 1; 
    			OAT_JS.grid.gridData[UcId].lastRequestValue = df;
    			QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].getAttributeValues((function (resJSON) {
	    			var res = JSON.parse(resJSON);
	    			OAT_JS.grid.appendNewValueData(UcId, res, true)
	    			OAT_JS.grid.setFilterChangedWhenServerPagination(UcId, oatDimension)
				}).closure(this), [df, page, 0, ""]);
        	}
       }
    	
    },
    getAllDataRowsForExport: function(UcId, _selfgrid, fileName, format){
    	var numRepo = (OAT_JS.grid.gridData[UcId].actualPageNumber == 1) ? 1 : 0;
    	var recur = function(numRepo, func){
    		if (numRepo < OAT_JS.grid.gridData[UcId].actualCantPages){
    			numRepo++;
    			QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].getPageDataForTable((function (resXML) {
    				var dataString = resXML;
            		var stringRecord = dataString.split("<Record>");
            		var data = []
    				for (var i = 1; i < stringRecord.length; i++) {
     					var recordData = [];
     					for (var j = 0; j < OAT_JS.grid.gridData[UcId].grid.columns.length; j++) {
       						recordData[j] = "#NuN#"
       						var dt = stringRecord[i].split("<" + OAT_JS.grid.gridData[UcId].grid.columns[j].getAttribute("dataField") + ">")
       						if (dt.length > 1){
       							var at = dt[1].split("</" + OAT_JS.grid.gridData[UcId].grid.columns[j].getAttribute("dataField") + ">")
       							recordData[j] = at[0]
       						}
       					}
       					
       					if (numRepo > 1){ //createRow
       						var row = OAT_JS.grid.gridData[UcId].grid.createRow(recordData, OAT_JS.grid.gridData[UcId].rowsMetadata.columnsDataType, OAT_JS.grid.gridData[UcId].rowsMetadata.defaultPicture, 
            							 OAT_JS.grid.gridData[UcId].rowsMetadata.forPivotCustomPicture, OAT_JS.grid.gridData[UcId].rowsMetadata.conditionalFormatsColumns, 
            							 OAT_JS.grid.gridData[UcId].rowsMetadata.formatValues, OAT_JS.grid.gridData[UcId].rowsMetadata.forPivotCustomFormat, OAT_JS.grid.gridData[UcId].rowsMetadata.columns,
            							 null, OAT_JS.grid.gridData[UcId].columnVisible);
            				row.style.display = "none";
            			}				 
					}
					
					if (numRepo == 1){ //move to first page
						OAT_JS.grid.redraw(this, UcId, resXML, false, true, 1)
					}
					func(numRepo, func)
					
    			}).closure(this), [numRepo, OAT_JS.grid.gridData[UcId].rowsPerPage, false, OAT_JS.grid.gridData[UcId].dataFieldOrder, OAT_JS.grid.gridData[UcId].orderType, OAT_JS.grid.gridData[UcId].filterInfo ]);  			
    		} else {
    			switch (format){
    				case "pdf": OAT.ExportToPdf(_selfgrid, fileName); break;
    				case "xml": OAT.ExportToXML(_selfgrid, fileName); break;
    				case "html": OAT_JS.grid.gridData[UcId].grid.ExportToHtml(_selfgrid, fileName); break;
    				case "xls": OAT.ExportToExcel(_selfgrid, fileName); break;
    				case "xlsx": OAT.ExportToExcel2010(_selfgrid, fileName); break;
    			}
    			OAT_JS.grid.gridData[UcId].grid.removeAllHiddenRows()
    		} 
    	}
    	
    	recur(numRepo, recur)
    },
    restoreDefaultView: function(UcId) {
    	//reset order
    	OAT_JS.grid.gridData[UcId].dataFieldOrder = ""
    	OAT_JS.grid.gridData[UcId].orderType      = ""
    	//reset filter info
    	this.gridData[UcId].filterInfo = [];
    	var tempInfo = [];
    	for (var id = 0; id < OAT_JS.grid.gridData[UcId].grid.columns.length; id++){
    		tempInfo[OAT_JS.grid.gridData[UcId].grid.columns[id].getAttribute("dataField")] = this.gridData[UcId].blackLists[OAT_JS.grid.gridData[UcId].grid.columns[id].getAttribute("dataField")].hasNull; 
    	}
    	for (var id = 0; id < OAT_JS.grid.gridData[UcId].grid.columns.length; id++){
        	this.gridData[UcId].blackLists[OAT_JS.grid.gridData[UcId].grid.columns[id].getAttribute("dataField")] = { state: "all", visibles:[], defaultAction: "Include", hiddens: [],
        																						hasNull: tempInfo[OAT_JS.grid.gridData[UcId].grid.columns[id].getAttribute("dataField")]  };
        }
        
        for ( var c = 0; c < this.gridData[UcId].columnVisible.length; c++){
        	this.gridData[UcId].grid.applySortOrderType(c, 1);
        	this.gridData[UcId].columnVisible[c] = true;
        	this.gridData[UcId].grid.showColumnHeader(c); 
        }
        
        this.gridData[UcId].rowsPerPage = this.gridData[UcId].defaultValues.rowsPerPage
        jQuery("#" + this.gridData[UcId].grid.controlName + "tablePagination_rowsPerPage")[0].value = this.gridData[UcId].defaultValues.rowsPerPage
        
    },
    getStateChange: function(UcId){
    	if (this.gridData[UcId].rowsPerPage != this.gridData[UcId].defaultValues.rowsPerPage)
    	{
    		return true;
    	}
    	if ((this.gridData[UcId].defaultValues.dataFieldOrder != OAT_JS.grid.gridData[UcId].dataFieldOrder) || (this.gridData[UcId].defaultValues.orderType != OAT_JS.grid.gridData[UcId].orderType))
    	{
    		return true;
    	} 
    	if (this.gridData[UcId].filterInfo.length > 0){//TODO: change if can be filter initial values
    		return true
    	}
    	for (var t = 0; t < this.gridData[UcId].columnVisible.length; t++){
    		if (!this.gridData[UcId].columnVisible[t]){
    			return true
    		}
    	}
    	//for (var t = 0; t < this.gridData[UcId].columnDataField.length; t++){
	    //	if (this.gridData[UcId].columnDataField[t] != this.gridData[UcId].originalColumnDataField[t]){
	    //		return true;
	    //	}
	    //}
    	return false
    },
    refreshPivotWhenServerPagination: function(UcId,  dataFieldOrderChanged, OrderChanged, dataFieldPositions){
    	/*if (dataFieldPositions.length > 0){
    		this.gridData[UcId].columnDataField = []
			this.gridData[UcId].columnDataField = dataFieldPositions
		} */
    	self.getDataForTable(UcId, 1, this.gridData[UcId].rowsPerPage, true, dataFieldOrderChanged, OrderChanged, "", "", "", true);
    },
    moveToFirstPage: function(UcId){
    	if (this.gridData[UcId].actualPageNumber > 1){
    		self.getDataForTable(UcId, 1, this.gridData[UcId].rowsPerPage, false, "", "", "", "", false);
    	}
    },
    moveToNextPage: function(UcId){
    	if (this.gridData[UcId].actualPageNumber < this.gridData[UcId].actualCantPages){
    		self.getDataForTable(UcId, this.gridData[UcId].actualPageNumber+1, this.gridData[UcId].rowsPerPage, false, "", "", "", "", false);	
    	}	
    },
    moveToLastPage: function(UcId){
    	if (this.gridData[UcId].actualPageNumber < this.gridData[UcId].actualCantPages){
    		self.getDataForTable(UcId, this.gridData[UcId].actualCantPages, this.gridData[UcId].rowsPerPage, false, "", "", "", "", false);
    	}
    },
    moveToPreviousPage: function(UcId){
    	if (this.gridData[UcId].actualPageNumber > 1){
    		self.getDataForTable(UcId, this.gridData[UcId].actualPageNumber-1, this.gridData[UcId].rowsPerPage, false, "", "", "", "", false);
    	}
    },
    cantPages: function(UcId, dataField){
    	return this.gridData[UcId].differentValuesPaginationInfo[dataField].totalPages
    },
    setDataFieldPosition: function(UcId, dataFieldPositions){
    	 this.gridData[UcId].columnDataField = []
    	 this.gridData[UcId].columnDataField = dataFieldPositions
 	   	 /*if ((QueryViewerCollection[OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection].AutoRefreshGroup != "")){
			var meta = OAT.createXMLMetadata(OAT_JS.grid.gridData[UcId],null,true);
    		var spl =  OAT_JS.grid.gridData[UcId].IdForQueryViewerCollection; 
			var listennings = QueryViewerCollection[spl]; 
			if ((listennings != "") && (listennings != null) && (listennings != undefined)){
	 			listennings.onValuesChangedEvent(meta);
			}
		}*/
    },
    addValueToDifferentValues: function(UcId, dataField, val){ 
    	var originalColumn = this.gridData[UcId].originalColumnDataField.indexOf(dataField);
    	var dataType = this.gridData[UcId].grid.columnsDataType[originalColumn];
    	var sortInt = false;
    	if ((dataType == "integer") || (dataType == "real")){
    		sortInt = true;
    	}
    	
    	
    	
    	var tempData = []; 
    	var added = false;
    	
    	if (val == "#NuN#"){
    		tempData.push(val);
    		added = true;
    	}
    	
    	for(var l=0; l < this.gridData[UcId].differentValues[dataField].length; l++){
    		if (!sortInt){ 
    			if ((val < this.gridData[UcId].differentValues[dataField][l]) && (!added)){
    				tempData.push(val);
    				added = true;	
    			}
    			tempData.push(this.gridData[UcId].differentValues[dataField][l])
    		} else {
    			if ((parseFloat(val) < parseFloat(this.gridData[UcId].differentValues[dataField][l])) && (!added)){
    				tempData.push(val);
    				added = true;	
    			}
    			tempData.push(this.gridData[UcId].differentValues[dataField][l])
    		}	
    	}
    	
    	if (!added) {
    		tempData.push(val)
    	}
    	
    	this.gridData[UcId].differentValues[dataField] = tempData;
    	
    }
}

OAT_JS.pivot = {
    panel: 1,
    tab: 5,
    div: "",
    needs: ["pivot", "statistics"],
    cb: function(pivotdiv, page, content, chart, defaultPicture, QueryViewerCollection, colms, formatValue, conditionalFormatsColumns,
    					 formatValueMeasures, shrinkToFit, disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout, ShowMeasuresAsRows,
    					 formulaInfo, fullRecord, serverPagination, pagingData, ordersHiden) {
        this.div = pivotdiv;
        var cols = columnNumbers.length + rowNumbers.length + filterNumbers.length;
        if (measures.length > 1) {
        	var prevCol = cols;
        	cols = cols + measures.length - 1; 
            for(var i = prevCol; i < cols; i++)
            {
            	columnNumbers.push(i);
            }
        } else {
        	for(var i=0; i < data.length; i++)
        	{
        		data[i].append("0");
        	}
        }
        var pivot;
        try {
            pivot = new OAT.Pivot(content, null, page, header, data, columnNumbers, rowNumbers, filterNumbers, cols, { showChart: 0 }, query, conditionalFormats, UcId, pageSize, defaultPicture, QueryViewerCollection, colms, pivotdiv, 
            								formatValue, conditionalFormatsColumns, formatValueMeasures, measures, shrinkToFit, disableColumnSort, UcId, IdForQueryViewerCollection, rememberLayout,
            								ShowMeasuresAsRows, formulaInfo , fullRecord, serverPagination, pagingData, ordersHiden);
        } catch (Error) {
        	
        }
        return pivot;
    }
}

function parseToIntRegisterValue(string, registerValue)
{
	if (string.indexOf(registerValue) > 0){
    	var tmpstr = string.split(registerValue+'="');
    	if (tmpstr.length == 1) {
    		 tmpstr = string.split(registerValue+"='");
    		 return (tmpstr[1]) ? parseInt(tmpstr[1].split("'")[0]) : -1;
    	}
    	return (tmpstr[1]) ? parseInt(tmpstr[1].split('"')[0]) : -1;
    }
    return -1;
}

function parseToStringRegisterValue(string, registerValue)
{
	if (string.indexOf(registerValue) > 0){
    	var tmpstr = string.split(registerValue+'="');
    	if (tmpstr.length == 1) {
    		 tmpstr = string.split(registerValue+"='");
    		 return (tmpstr[1]) ? (tmpstr[1].split("'")[0]) : -1;
    	}
    	return (tmpstr[1]) ? (tmpstr[1].split('"')[0]) : -1;   
    }
    return -1;
}

function EvaluateExpressionPivotJs(expression, data, formulaInfo)
{
	var tokens = []
	for(var i = 0; i < expression.length; i++){
		tokens[i] = expression[i]
	}
    var evalStack = [];

    while (tokens.length != 0)
    {
        var currentToken = tokens.shift();
        if (isOperator(currentToken))
        {
            var operand1 = evalStack.pop();
            var operand2 = evalStack.pop();

            var result = PerformOperation(parseFloat(operand1), parseFloat(operand2), currentToken);
            evalStack.push(result);
        } else {
        	if (isNaN(parseInt(currentToken))){
        		if (data[formulaInfo.itemPosition[currentToken]] == "#NuN#") return "#NuN#";
        		evalStack.push( data[ formulaInfo.itemPosition[currentToken]  ]);
        	} else {
        		evalStack.push( currentToken );
        	}
        }
    }
    return evalStack.pop();
}

function PerformOperation(operand1, operand2, operator)
{
    switch(operator)
    {
        case '+': 
            return operand1 + operand2;
        case '-':
            return operand2 - operand1;
        case '*':
            return operand1 * operand2;
        case '/':
            return operand2 / operand1;
        default:
            return;
    }

}

function InfixToPostfix(expression)
{
    //var tokens = expression.split(/([0-9]+|[*+-\/()])/);
    var tokens = expression.split(" ");
    var outputQueue = [];
    var operatorStack = [];

    while (tokens.length != 0)
    {
        var currentToken = tokens.shift();

        if (isOperator(currentToken)) 
        {
            while ((getAssociativity(currentToken) == 'left' && 
                    getPrecedence(currentToken) <= getPrecedence(operatorStack[operatorStack.length-1])) ||
                   (getAssociativity(currentToken) == 'right' && 
                    getPrecedence(currentToken) < getPrecedence(operatorStack[operatorStack.length-1]))) 
            {
                outputQueue.push(operatorStack.pop())
            }

            operatorStack.push(currentToken);

        }
        else if (currentToken == '(')
        {
                operatorStack.push(currentToken);
        }
        else if (currentToken == ')')
        {
            while (operatorStack[operatorStack.length-1] != '(')
            {
    	        if (operatorStack.length == 0)
    	            throw("");

    	        outputQueue.push(operatorStack.pop());
            }	
            operatorStack.pop();		
        } else {
            outputQueue.push(currentToken);
        }   
    }  

    while (operatorStack.length != 0)
    {
        if (!operatorStack[operatorStack.length-1].match(/([()])/))
            outputQueue.push(operatorStack.pop());
        else
            throw("Parenthesis balancing error! Shame on you!");         
    }

    return outputQueue.join(" ");
}    


function isOperator(token)
{
    if (!token.match(/([*+-\/])/))
        return false;
    else 
        return true;
}


function isNumber(token)
{
    if (!token.match(/([0-9]+)/))
        return false;
    else
        return true;
}


function getPrecedence(token)
{
    switch (token)
    {
    	case '^':
    	    return 9; 
    	case '*':		    
    	case '/':
    	case '%':
    	    return 8;
        case '+':
    	case '-':
    	    return 6;
    	default: 
    	    return -1;
    }
}

function getAssociativity(token)
{
    switch(token)
    {
    	case '+':
    	case '-':
    	case '*':
    	case '/':
    	    return 'left';
    	case '^':
    	    return 'right';
    }
}


function OATSetCookie(name, value, expires, path, domain, secure) {
        document.cookie = name + "=" + escape(value) +
  		((expires == null) ? "" : "; expires=" + expires.toGMTString()) +
  		((path == null) ? "" : "; path=" + path) +
  		((domain == null) ? "" : "; domain=" + domain) +
  		((secure == null) ? "" : "; secure");
}
function OATGetCookie(name) {
        var cname = name + "=";
        var dc = document.cookie;
        if (dc.length > 0) {
            begin = dc.indexOf(cname);
            if (begin != -1) {
                begin += cname.length;
                end = dc.indexOf(";", begin);
                if (end == -1) end = dc.length;
                return unescape(dc.substring(begin, end));
            }
        }
        return null;
}

function OATIsNotEmptyValue(value){
	return (value != "#NaV#") && (value != "#NuN#") 
} 

function OATGetRowsFromXML(data, obj, ShowMeasuresAsRows){
	var rows = [];
	
	var rowsString = data.split("<Rows>")
	if (rowsString.length){
		rowsString = rowsString[1].split("</Rows>")
		rowsString = rowsString[0].split("<Row>")
		var isTitle = 0;
		for (var l = 1; l < rowsString.length; l++){
			var row = { headers: [], cells: [], subTotal: false, dataField: -1, rowSpan: 0}
			
			var subTotal = parseToStringRegisterValue(rowsString[l], "Subtotal")
			if (subTotal == "true"){
				row.subTotal = true 
			}
			var headerString
			if (rowsString[l].indexOf("</Header>") > 0){
				headerString = rowsString[l].split("</Header>")[0].replace("<Header>","")
			} else {
				headerString = rowsString[l].split("/>")[0].replace("<Header","")
			}
			
			row.dataField = parseToStringRegisterValue(headerString, "DataField"); //for columns in rows only
			
			//get dataFields of headers
			var headerrep = headerString.replace(/<\//g, "-*-5").replace(/\/>/g, "-*-5")
			var headersItems = headerrep.split("-*-5");			

			for (var df = 1; df < headersItems.length; df++){
				var value;
				var datafield = headersItems[df].split(">")[0]
				if (datafield == ""){
					datafield = headersItems[df-1].split("<")[1].split(" ")[0]
					value = "#NuN#";
				} else {
					try {
						value = headersItems[df-1].split(datafield + ">")[1].split("<")[0]
					} catch (ERROR){
						value = "#NuN#"; 
					}
				}
				
				var h = { dataField: datafield, value: value, rowSpan: 1 }
				
				
				var totalizedItems = headersItems.length - 1
				if (ShowMeasuresAsRows){
					totalizedItems = headersItems.length
					if (row.subTotal){
						totalizedItems--;
					}
				}
				var sumarized = (df - totalizedItems >= 0)
				
				if ((rows.length > 0) && (!sumarized)){ //set rowspan
					if ((df-1 == 0) || (row.headers[df-2].rowSpan==0)){ //no es la 1er columna, pero la anterior tiene span
						if (value == rows[l-2].headers[df-1].value){
							h.rowSpan = 0
							var ant = l-2
							while (ant >= 0){
								if (rows[ant].headers[df-1].rowSpan > 0){
									rows[ant].headers[df-1].rowSpan++
									break;
								}
								ant--;
							}
						}
					}
				}
				 
				
				row.headers.push(h);
			}
			
			if ((ShowMeasuresAsRows) &&  (row.subTotal)){
				if ((l==1) || (!rows[l-2].subTotal) || (row.headers.length != rows[l-2].headers.length)){
					row.rowSpan = 1
				} else {
					var ant = l-2;
					while (ant >= 0){
						if (rows[ant].rowSpan > 0){
							rows[ant].rowSpan++
							break;
						}
						ant--
					}
				}
			} 
			//get cells values
			var cellsString = rowsString[l].split("<Cells>")[1].split("</Cell>")
			for (var ci = 0; ci < cellsString.length-1; ci++){
				
				var dField = (row.dataField != -1) ? row.dataField : obj.columnsHeaders[ci].dataField; 
				//add empty cells
				for (var ec = 1; ec < cellsString[ci].split("<Cell />").length; ec++)
				{
					var c = { value: "#NuN#", dataField: dField }
					row.cells.push(c);
				}
				
				var value = cellsString[ci].split("<Cell>")[1]
				var c = { value: value, dataField: dField }
				row.cells.push(c);
			}
			
			rows.push(row);
		}
		
	}
	return rows;
}

function OATGetColumnsHeadersFromXML(dataString){
	var columnsString = dataString.split("<Columns>")
	var columnsHeaders = [];
	if (columnsString.length > 1){
		
		columnsString = columnsString[1].split("</Columns>")[0]
		var headerString = columnsString.split("<Header")
		
		for (var t = 1; t < headerString.length; t++){
		  var o = parseToStringRegisterValue(headerString[t], "DataField")
		  var subTotal = parseToStringRegisterValue(headerString[t], "Subtotal")
		  
		  var subHeaders = [];
		  var subHeadersString = headerString[t].replace("DataField=\""+o+"\">","").replace("DataField='"+o+"'>","").replace("</Header>","").replace(/IsNull="true" \/>/g, ">#NuN#<F>").replace(/IsNull='true' \/>/g, ">#NuN#<F>").split("<")
		  if (subHeadersString.length > 1){ //hay dimensiones en las columnas
		  	for(var sh = 1; sh < subHeadersString.length-1; sh++){
		  		if (sh % 2 == 1){
		  			var datafield = subHeadersString[sh].split(">")[0]
					var value = subHeadersString[sh].split(datafield + ">")[1]
					var h = { dataField: datafield, value: value, colSpan: 1 }
					subHeaders.push(h)
				}
		  	}
		  }
		  
		  columnsHeaders.push({ dataField: o,  subTotal: (subTotal=="true"), subHeaders: subHeaders})
		  
		}
		
		//obj.columnsHeaders = columnsHeaders;
	}
	return columnsHeaders
}

function OATGetDataFromXMLForPivot(data, ShowMeasuresAsRows){
	var stringRecord = dataString.split("<Record>")
	
	var obj = {};
	obj.ServerRecordCount = parseToIntRegisterValue(data, "RecordCount") 
    obj.ServerPageCount   = parseToIntRegisterValue(data, "PageCount")
    obj.ServerPageNumber  = parseToIntRegisterValue(data, "PageNumber")
	
	obj.columnsHeaders = OATGetColumnsHeadersFromXML(data);
	obj.rows =  OATGetRowsFromXML(data, obj, ShowMeasuresAsRows);
	
	
	return obj;
}

function OATGetNewDataFromXMLForPivot(data, obj, ShowMeasuresAsRows, exportTo){
		data = data.replace(/<Cell\/>/g,"<Cell \/>")
		if ((exportTo == undefined) || (exportTo == "")){
			if (parseToIntRegisterValue(data, "RecordCount") != -1){
				obj.ServerRecordCount = parseToIntRegisterValue(data, "RecordCount")
			}
			if (parseToIntRegisterValue(data, "PageCount") != -1){
				obj.ServerPageCount   = parseToIntRegisterValue(data, "PageCount")
			}
		}
		obj.ServerPageNumber  = parseToIntRegisterValue(data, "PageNumber")
		
		if (data.split("<Columns>").length > 1){
			obj.columnsHeaders = OATGetColumnsHeadersFromXML(data);
		}
		
		obj.rows =  OATGetRowsFromXML(data, obj, ShowMeasuresAsRows);
		
		return obj;
}

function OATgetDataFromXMLOldFormat(dataString, orderFilds, orderFildsHidden){
	var stringRecord = dataString.split("<Record>")
    	
    var data = []; 
    var fullData = [];
    for (var i = 1; i < stringRecord.length; i++) {
     	var recordData = [];
     	var fullRecordData = [];
       	for (var j = 0; j < orderFilds.length; j++) {
       		recordData[j] = "#NuN#"
       		var dt = stringRecord[i].split("<" + orderFilds[j] + ">")
       		if (dt.length > 1){
       			var at = dt[1].split("</" + orderFilds[j] + ">")
       				/*var rp = at[0].replace(/^\s+|\s+$/g, '')
       				recordData[j] = (rp != "") ? rp : undefined*/
       			recordData[j] = at[0]
       			fullRecordData[j] = recordData[j] 
    		}
    	}
    	data.push(recordData);
        	
       	if (orderFildsHidden != undefined){
       		var pos_init = orderFilds.length;
       		for (var j = 0; j < orderFildsHidden.length; j++) {
       			fullRecordData[pos_init + j] = undefined
       			var dt = stringRecord[i].split("<" + orderFildsHidden[j] + ">")
       			if (dt.length > 1){
       				var at = dt[1].split("</" + orderFildsHidden[j] + ">")
       				fullRecordData[pos_init + j] = at[0]
       			}
       		}
       		fullData.push(fullRecordData);
       	}
      	/*	if (fullRecordData.length > this.maxLengthRecord) this.maxLengthRecord = fullRecordData.length;*/ 
    }
    
    if (orderFildsHidden != undefined){
    	return [data, fullData];
    }else {
    	return data;
   	}
}

function init() {
	
}

}
