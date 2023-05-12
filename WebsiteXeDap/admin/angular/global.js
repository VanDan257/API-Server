var current_url = "https://localhost:44312";
var _user = JSON.parse(localStorage.getItem("user"));
makeScript = function (url) {
	var script = document.createElement('script');
	script.setAttribute('src', url);
	script.setAttribute('type', 'text/javascript');
	// document.getElementById('mainDiv').appendChild(script);
};