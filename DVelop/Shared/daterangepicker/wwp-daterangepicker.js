var WWP_DateRangePicker_INPUT_CSS_CLASS = 'wwp-daterange-picker-input';
var WWP_DateRangePicker_DATE_RANGE = [1970, 2040];

var defaultWWPDateRangeSettings = {
	minYear: WWP_DateRangePicker_DATE_RANGE[0],
	maxYear: WWP_DateRangePicker_DATE_RANGE[1],
	autoApply: true,
	showWeekNumbers: false,
	showDropdowns: true,
	style: 'Light',
	mondayFirst: false
};

var wwp_dp_closedGridItem;
function WWP_DateRangePicker(selector, opts, valueChangedCallback, cancelCallback) {
	var activeLocale;

	if (selector === "") {
		throw "WWP_DateRangePicker_UC.Attach: Selector cannot be empty";
	}
	if (opts == undefined) {
		console.log("WWP_DateRangePicker_UC: Initializing with default options")
	}

	var $ctrl = $(selector);

	$ctrl = ($ctrl.length === 0) ? $("#" + selector) : $ctrl;

	if ($ctrl.length === 0) {
		throw new "WWP_DateRangePicker_UC.Attach: Selector not found: " + selector;
	}

	var settings = gxOptionsToProviderOptions(opts);
	var ctrl = $ctrl[0];

	var closeFunction = function (picker, ctrl) {
		wwp_dp_closedGridItem = null;
		if ($(ctrl).closest('.gx-grid').length == 1 && $(ctrl).attr('id') != '') {
			$(ctrl).data('wwp-closing-grid-item', '');
			wwp_dp_closedGridItem = $(ctrl).attr('id');
		}
		setTimeout(function () {
			picker.isShowing = true;
			gx.fn.setFocus(ctrl, function () {
				picker.isShowing = false;
			});
		}, 10);
	}

	$ctrl.daterangepicker(settings, function (start, end, label, e) {
		if (valueChangedCallback != undefined) {
			label = (label !== activeLocale.customRangeLabel) ? label : null;
			valueChangedCallback(start.toDate(), end.toDate(), label, e);
		}
		var clickedCalendar = $(e.currentTarget).parents('.daterangepicker').length > 0;
		if (clickedCalendar) {
			closeFunction(picker, ctrl);
		}
	})
		.on('cancel.daterangepicker', function (ev, picker) {
			//Cancel as Clear
			picker.isNullVal = true;
			picker.setStartDate(new Date);
			picker.setEndDate(new Date);
			$ctrl.val('');
			if (valueChangedCallback != undefined) {
				valueChangedCallback(null, null, null, ev);
			}
			closeFunction(picker, ctrl);
		})
		.on('apply.daterangepicker', function (ev, picker) {
        	var val = $ctrl.val();
        	var gxDate = new gx.date.gxdate(val);
        	picker.isNullVal = gx.date.isNullDate(gxDate);
        	if (picker.flatDisplay || picker.isNullVal && picker.startDate.isSame(picker.oldStartDate)) {
        		valueChangedCallback(picker.startDate.toDate(), picker.endDate.toDate(), null, ev);
        	}
        	closeFunction(picker, ctrl);
        })
        .on('show.daterangepicker', function (ev, picker) {
        	$ctrl.closest('.ColumnSettings.open').data('updatingDateValue', '');
        });
	if ($('#' + ctrl.id + '_ShowingDatesFrom'.toUpperCase()).length == 1) {
		$ctrl.on('viewchanged.daterangepicker', function (ev, picker) {
			$('#' + this.id + '_ShowingDatesFrom'.toUpperCase()).val(picker.firstShowingDay.format(picker.locale.format)).change();
		});
	}

	var picker = $ctrl.data('daterangepicker');
	$ctrl.toggleClass(WWP_DateRangePicker_INPUT_CSS_CLASS, true);
	$ctrl.on('change', function (ev) {
		var value = $(this).val();
		if (value === '' || typeof (ev) == KeyboardEvent) {
			if (valueChangedCallback != undefined) {
				valueChangedCallback(null, null, '', ev);
			}
		}
		if (!picker.startDate.isValid() || !picker.endDate.isValid()) {
			picker.clickCancel();
		}
	});

	$ctrl.on('input', function (ev) {
		picker.show(ev);
	});

	activeLocale = $ctrl.data('daterangepicker').locale;

	function gxOptionsToProviderOptions(gxOpts) {
		var settings = {};
		settings["alwaysShowCalendars"] = true;

		if (gxOpts === undefined) {
			return settings;
		}

		var startDate = assignDateProperty(settings, gxOpts, "startDate", "StartDate");
		assignDateProperty(settings, gxOpts, "endDate", "EndDate");
		assignDateProperty(settings, gxOpts, "minDate", "MinDate");
		assignDateProperty(settings, gxOpts, "maxDate", "MaxDate");

		settings["parentEl"] = null;
		assignDateProperty(settings, gxOpts, "parentEl", "parentEl");
		assignDateProperty(settings, gxOpts, "flatDisplay", "flatDisplay", false);

		//Date Picker Properties
		var showDate = assignProperty(settings, gxOpts, "showDatePicker", "DatePicker.Show", true);
		assignProperty(settings, gxOpts, "showDropdowns", "DatePicker.ShowDropdowns", true);
		assignProperty(settings, gxOpts, "minYear", "DatePicker.MinYear");
		assignProperty(settings, gxOpts, "maxYear", "DatePicker.MaxYear");
		assignProperty(settings, gxOpts, "showWeekNumbers", "DatePicker.ShowWeekNumbers");
		assignProperty(settings, gxOpts, "singleDatePicker", "DatePicker.SingleDatePicker", false);

		assignProperty(settings, gxOpts, "linkedCalendars", "DatePicker.LinkedCalendars", true);

		//Time Picker Properties
		var showTime = assignProperty(settings, gxOpts, "timePicker", "TimePicker.Show", false);
		var hour24 = assignProperty(settings, gxOpts, "timePicker24Hour", "TimePicker.Hour24");
		var showSeconds = assignProperty(settings, gxOpts, "timePickerSeconds", "TimePicker.ShowSeconds");
		assignProperty(settings, gxOpts, "timePickerIncrement", "TimePicker.Increment");

		//General Properties
		assignProperty(settings, gxOpts, "drops", "Advanced.Drops", "auto");
		assignProperty(settings, gxOpts, "opens", "Advanced.OpenLocation");
		assignProperty(settings, gxOpts, "autoApply", "Advanced.AutoApply");

		if (gxOpts.FormattedDays != null) {
			var formattedDays = [];
			for (var i = 0; i < gxOpts.FormattedDays.length; i++) {
				formattedDays.push({
					'date': moment(new Date(gxOpts.FormattedDays[i].Date)),
					'disabled': gxOpts.FormattedDays[i].Disabled,
					'class': gxOpts.FormattedDays[i].Class,
					'tooltip': gxOpts.FormattedDays[i].Tooltip
				});
			}
			settings["formattedDays"] = formattedDays;
		} else if (settings["formattedDays"] != null) {
			settings["formattedDays"] = null;
		}


		var showCustomRangeLabel = assignProperty(settings, gxOpts, "showCustomRangeLabel", "ShowCustomRangeLabel", true);
		if (!showCustomRangeLabel) {
			settings["alwaysShowCalendars"] = false;
		}

		settings["autoUpdateInput"] = false; //Always turn off Autupdate.        

		//Localization Options
		var locale = gxOpts["Locale"];
		var localeId = (locale && locale.Id) ? locale.id : null;
		var userLocalCustomFormat = (locale && locale.Format) ? locale.Format : null;
		setLocale(settings, localeId);
		settings.locale.format = (userLocalCustomFormat) ? locale.Format : (defaultWWPDateRangeSettings.dateFormat ? defaultWWPDateRangeSettings.dateFormat : settings.locale.format);
		settings.locale.firstDay = (defaultWWPDateRangeSettings.mondayFirst ? 1 : 0);

		if (showTime && !userLocalCustomFormat) {
			//Add Time to Format when no User Custom Format was specified
			if (showDate) {
				settings.locale.format += ' ';
			} else {
				settings.locale.format = '';
			}
			if (hour24) {
				settings.locale.format += 'HH:mm';
			}
			else {
				settings.locale.format += 'hh:mm';
			}
			if (showSeconds) {
				settings.locale.format += ':ss';
			}
			if (!hour24) {
				settings.locale.format += ' A';
			}
		}

		// Picker Options
		switch (gxOpts["PickerType"]) {
			case 'single':
				settings["singleDatePicker"] = true;
				break;
			default:
				break;
		}
		setRanges(settings, gxOpts);
		var res = $.extend($.extend({}, defaultWWPDateRangeSettings), settings);
		if (!res.optsLoaded) {
			res.settingsBeforeMerge = settings;
		}
		return res;
	}

	function setRanges(settings, gxOpts) {
		var gxRanges = gxOpts["Ranges"];
		var ranges = {};
		if (gxRanges && gxRanges.length > 0) {
			for (var i = 0; i < gxRanges.length; i++) {
				var r = gxRanges[i];
				ranges[r['DisplayName']] = [new Date(r['StartDate']), new Date(r['EndDate'])];
			}
			settings['ranges'] = ranges;
		}
	}

	function setLocale(settings, localeId) {
		var locales = window["WWP_DateRangePicker_Locales"];
		if (!localeId) {
			//Use default KB language            
			var mapgxLangCodetoLanguage = {
				"eng": "English",
				"spa": "Spanish",
				"por": "Portuguese",
				"ita": "Italian",
				"german": "German",
				"chs": "SimplifiedChinese",
				"cht": "TraditionalChinese",
				"jap": "Japanese",
				"arabic": "Arabic"
			}
			localeId = mapgxLangCodetoLanguage[(gx.languageCode + '').toLowerCase()] || gx.languageCode;
		}
		if (localeId && locales[localeId] != null) {
			var userLocale = JSON.parse(JSON.stringify(locales[localeId]));
			if (userLocale) {
				settings["locale"] = userLocale;
			}
			else {
				console.error("WWP_DateRangePicker_UC: Locale not found in resources: " + localeId);
				console.log("WWP_DateRangePicker_UC: Using default locale");
				setLocale(settings, "English");
			}
		}
		else {
			console.log("WWP_DateRangePicker_UC: LocaleId not found for : " + localeId || gx.languageCode);
			console.log("WWP_DateRangePicker_UC: Using default locale");
			setLocale(settings, "English");
		}
	}

	function assignProperty(settings, gxOpts, leftName, rightName, defaultValue) {
		var members = rightName.split(".");
		var currentObj = gxOpts;
		var value = defaultValue;
		for (var i = 0; i < members.length - 1 && currentObj; i++) {
			currentObj = currentObj[members[i]];
		}
		if (currentObj != undefined && currentObj[members[i]] != undefined) {
			value = currentObj[members[i]];
		}
		if (value != undefined) {
			settings[leftName] = value;
		}
		return value;
	}

	function assignDateProperty(settings, gxOpts, leftName, rightName, defaultValue) {
		assignProperty(settings, gxOpts, leftName, rightName, defaultValue);
		if (typeof (settings[leftName]) === "string") {
			settings[leftName] = new Date(settings[leftName]);
		}
		return settings[leftName];
	}

	this.setStartDate = function (startDate) {
		$ctrl.data('daterangepicker').setStartDate(startDate);
	};

	this.setEndDate = function (endDate) {
		$ctrl.data('daterangepicker').setEndDate(endDate);
	};
}

var mapGXDateFormatToDatePickerFormat = function (gxFormat) {
	// %m/%d/%y %I:%M %p
	var format = gxFormat;
	format = format.replace('%m', 'MM');
	format = format.replace('%d', 'DD');
	format = format.replace('%y', 'YY');
	format = format.replace('%Y', 'YYYY');
	if (gxFormat.indexOf('%p') > 0) {
		format = format.replace('%I', 'h');
		format = format.replace('%p', 'A');
	} else {
		format = format.replace('%I', 'HH');
	}

	format = format.replace('%H', 'HH');
	format = format.replace('%M', 'mm');
	format = format.replace('%S', 'ss');
	return format;
}
;$(window).one('load',function(){WWP_VV([['WWP.DatePicker','15.3.3'],['WWP.DateRangePicker','15.3.3']]);});