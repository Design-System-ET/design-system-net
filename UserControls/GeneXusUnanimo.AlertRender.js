function GeneXusUnanimo_Alert($) {
	  
	  
	  
	  
	  
	  
	  

	var template = '<div id=\"alert{{id}}\" class=\"alert alert-{{Type}}\" aria-live=\"assertive\"> 	<div class=\"inline-left-xxxl inline-right-xxl\" style=\"flex-grow: 1\">		<div class=\"row\"> <span class=\"alert-title\">{{Title}}</span> </div>		<div class=\"row\"> <span class=\"alert-message\">{{Message}}</span> </div>	</div>	<div class=\"inline-left-xl inline-right-xl\" style=\"flex-grow: 0\">		<button type=\"button\" class=\"alert-close\" data-dismiss=\"alert\" aria-label=\"Close\" id=\"alert{{id}}close\"  data-event=\"Close\" ><span aria-hidden=\"true\">&times;</span></button>	</div>	{{#ShowMultiple}}	<div class=\"alert-badge\">		<span class=\"alert-badge-text\">{{Count}}</span>	</div>	{{/ShowMultiple}}</div>';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnClose = 0; 
	var $container;
	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts

			_iOnClose = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			$(this.getContainerControl())
				.find("[data-event='Close']")
				.on('click', this.onCloseHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts
			this.Init(); 
	}

	this.Scripts = [];

		this.Init = function() {

			  	const UC = this;
				const elId = "alert" + UC.id;
				const el = document.getElementById(elId);
					
				if (UC.Position === "Fixed to bottom") {
					if (!el.classList.contains("alert-fixed")) {
						el.classList.add("alert-fixed");
					}
				}

				if (UC.ShowMultiple === true) {
					if (!el.classList.contains("alert-multiple")) {
						el.classList.add("alert-multiple");
					}
				}
			  
		}


		this.onCloseHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
				 
				 
				 
				 
				 
				 
			}

			if (this.Close) {
				this.Close();
			}
		} 

	this.autoToggleVisibility = true;

	var childContainers = {};
	this.renderChildContainers = function () {
		$container
			.find("[data-slot][data-parent='" + this.ContainerName + "']")
			.each((function (i, slot) {
				var $slot = $(slot),
					slotName = $slot.attr('data-slot'),
					slotContentEl;

				slotContentEl = childContainers[slotName];
				if (!slotContentEl) {				
					slotContentEl = this.getChildContainer(slotName)
					childContainers[slotName] = slotContentEl;
					slotContentEl.parentNode.removeChild(slotContentEl);
				}
				$slot.append(slotContentEl);
				$(slotContentEl).show();
			}).closure(this));
	};

}