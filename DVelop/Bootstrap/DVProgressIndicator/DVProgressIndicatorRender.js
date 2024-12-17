function DVProgressIndicator() {

	this.Type;
	this.CircleCaptionType;
	this.Caption;
	this.Subtitle;
	this.RawHTML;
	this.Cls;
	this.Percentage;
	this.BarWidth;
	this.CircleWidth;
	this.CircleProgressWidth;
	this.AnimateOnStart;

	this.show = function () {
		if (this.my_progressIndicator == undefined) {

			var mainDivId = this.ContainerName + "_DVProgressIndicator";
			this.setHtml('<div id="' + mainDivId + '" class="' + this.Cls + '" ></div>');

			this.my_progressIndicator = new DVProgressIndicator2(this);
			this.my_progressIndicator.render();
		}
		else {
			this.my_progressIndicator.updateValue();
		}
	}
}

function DVProgressIndicator2(userControl) {

	this.control = userControl;

	this.render = function () {
		var mainDivId = this.control.ContainerName + "_DVProgressIndicator";

		var mainDiv = $("#" + mainDivId);
		mainDiv.html('');

		if (this.control.Type == 'Bar') {
			mainDiv.html('<div class="progress" ' + (this.control.BarWidth != '' ? 'style="width: ' + this.control.BarWidth + ';"' : '') + '><div id="' + this.control.ContainerName + '_DVPIBar" class="progress-bar" style="width: 0%;">' + this.control.Caption + '</div></div>');
			if (this.control.AnimateOnStart) {
				setTimeout(this.bar_setWidth.bind(this), 1);
			}
		} else {
			if (this.control.CircleCaptionType == null || this.control.CircleCaptionType == '') {
				this.control.CircleCaptionType = 'Caption';
			}
			mainDiv.css('position', 'relative');
			var cirlceRadius = (this.control.CircleWidth - this.control.CircleProgressWidth) / 2;
			var center = this.control.CircleProgressWidth / 2 + cirlceRadius;
			this.lineLength = 2 * cirlceRadius * Math.PI;
			if (this.control.CircleCaptionType == 'Caption') {
				mainDiv.html('<svg class="ProgressIndicatorCircle" height="' + this.control.CircleWidth + '" width="' + this.control.CircleWidth + '"><circle id="' + this.control.ContainerName + '_DVPICircleB" class="BackCircle" cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-width: ' + this.control.CircleProgressWidth + 'px;"></circle><circle id="' + this.control.ContainerName + '_DVPICircle"  cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-dashoffset: ' + this.lineLength + ';stroke-dasharray: ' + this.lineLength + ';stroke-width: ' + this.control.CircleProgressWidth + 'px;" class="ProgressCircle"></circle></svg><div class="ProgressIndicatorCircle" style="position: absolute;top: 0;left: 0;width: ' + this.control.CircleWidth + 'px;height: ' + this.control.CircleWidth + 'px;"><div style="left: 50%;position: absolute;"><span id="' + this.control.ContainerName + '_DVPIText" class="CircleCaption" style="position: relative; left: -50%; line-height: ' + this.control.CircleWidth + 'px;">' + this.control.Caption + '</span></div></div>');
			} else if (this.control.CircleCaptionType == 'CaptionAndSubtitle') {
				mainDiv.html('<svg class="ProgressIndicatorCircle" height="' + this.control.CircleWidth + '" width="' + this.control.CircleWidth + '"><circle id="' + this.control.ContainerName + '_DVPICircleB" class="BackCircle" cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-width: ' + this.control.CircleProgressWidth + 'px;"></circle><circle id="' + this.control.ContainerName + '_DVPICircle"  cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-dashoffset: ' + this.lineLength + ';stroke-dasharray: ' + this.lineLength + ';stroke-width: ' + this.control.CircleProgressWidth + 'px;" class="ProgressCircle"></circle></svg><div class="ProgressIndicatorCircle" style="position: absolute;top: 0;left: 0;width: ' + this.control.CircleWidth + 'px;height: ' + this.control.CircleWidth + 'px;"><div style="left: 50%;position: absolute;" class="CircleCaptionContainer"><span id="' + this.control.ContainerName + '_DVPIText" class="CircleCaption" style="left: -50%;position: relative;">' + this.control.Caption + '</span></div><div style="left: 50%;position: absolute;" class="CircleSubtitleContainer"><span class="CircleSubtitle" style="left: -50%;position: relative;">' + this.control.Subtitle + '</span></div></div>');
			} else {
				mainDiv.html('<svg class="ProgressIndicatorCircle" height="' + this.control.CircleWidth + '" width="' + this.control.CircleWidth + '"><circle id="' + this.control.ContainerName + '_DVPICircleB" class="BackCircle" cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-width: ' + this.control.CircleProgressWidth + 'px;"></circle><circle id="' + this.control.ContainerName + '_DVPICircle"  cx="' + center + '" cy="' + center + '" r="' + cirlceRadius + '" style="stroke-dashoffset: ' + this.lineLength + ';stroke-dasharray: ' + this.lineLength + ';stroke-width: ' + this.control.CircleProgressWidth + 'px;" class="ProgressCircle"></circle></svg><div class="ProgressIndicatorCircle" style="position: absolute;top: 0;left: 0;width: ' + this.control.CircleWidth + 'px;height: ' + this.control.CircleWidth + 'px;">' + this.control.RawHTML + '</div>');
			}

			if (this.control.AnimateOnStart) {
				setTimeout(this.circle_setStrokeDashoffset.bind(this), 1);
			}
		}
	}

	this.updateValue = function () {
		var mainDiv = $("#" + this.control.ContainerName + "_DVProgressIndicator");
		$(mainDiv).removeClass();
		if (this.control.Cls != '') {
			$(mainDiv).addClass(this.control.Cls);
		}

		if (this.control.Type == 'Bar') {
			$('#' + this.control.ContainerName + '_DVPIBar').get(0).innerHTML = this.control.Caption;
			if (this.control.BarWidth != '') {
				$(mainDiv).get(0).childNodes[0].style.width = this.control.BarWidth;
			}
			this.bar_setWidth();
		} else {
			$('#' + this.control.ContainerName + '_DVPICircleB').get(0).style.cssText = 'stroke-width: ' + this.control.CircleProgressWidth + 'px;';
			if (this.control.CircleCaptionType != 'RawHTML') {
				$('#' + this.control.ContainerName + '_DVPIText').get(0).textContent = this.control.Caption;
			}
			this.circle_setStrokeDashoffset();
		}
	}

	this.circle_setStrokeDashoffset = function () {
		var perc = this.control.Percentage;
		if (perc > 100) {
			perc = 100;
		}
		if (/MSIE \d|Trident.*rv:/.test(navigator.userAgent)) {
			var dasharray;
			if (perc >= 25) {
				dasharray = ((perc - 25) * this.lineLength / 100) + ' ' + ((100 - perc) * this.lineLength / 100) + ' ' + (25 * this.lineLength / 100);
			}
			else {
				dasharray = '0 ' + (75 * this.lineLength / 100) + ' ' + (perc * this.lineLength / 100) + ' ' + ((25 - perc) * this.lineLength / 100);
			}
			$('#' + this.control.ContainerName + '_DVPICircle').get(0).style.cssText = 'stroke-dasharray: ' + dasharray + ';stroke-width: ' + this.control.CircleProgressWidth + 'px;';
		}
		else {
			var newOffSet = (100 - perc) * this.lineLength / 100;
			$('#' + this.control.ContainerName + '_DVPICircle').get(0).style.cssText = 'stroke-dasharray: ' + this.lineLength + ';stroke-dashoffset: ' + newOffSet + ';stroke-width: ' + this.control.CircleProgressWidth + 'px;';
		}
	}

	this.bar_setWidth = function () {
		var perc = this.control.Percentage;
		if (perc > 100) {
			perc = 100;
		}
		$('#' + this.control.ContainerName + '_DVPIBar').get(0).style.width = perc + '%';
	}
};$(window).one('load',function(){WWP_VV([['Bootstrap.DVProgressIndicator','1.1'],['GXBootstrap.DVProgressIndicator','15.0.14']]);});