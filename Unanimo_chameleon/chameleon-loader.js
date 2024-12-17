var path = "";
if (gx.gen.isJava()) {
	path = window.location.href.includes("/servlet/") ? "../static/" : "static/";
}
	  
if (!document.getElementById("chameleon.nomodule")) {
  var script1 = document.createElement("script");
  script1.setAttribute("id", "chameleon.nomodule");
  script1.setAttribute("src", path + "Unanimo_chameleon/chameleon.js");
  script1.setAttribute("nomodule", "");
  document.head.appendChild(script1);
}

if (!document.getElementById("chameleon.module")) {
  var script2 = document.createElement("script");
  script2.setAttribute("id", "chameleon.module");
  script2.setAttribute("src", path + "Unanimo_chameleon/chameleon.esm.js");
  script2.setAttribute("type", "module");
  document.head.appendChild(script2);
}