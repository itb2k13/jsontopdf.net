
var editor = ace.edit("editor");
editor.setTheme("ace/theme/monokai");
editor.session.setMode("ace/mode/json");
editor.setHighlightActiveLine(false);
editor.setShowPrintMargin(false);
editor.setFontSize(18);

var htmlEditor = ace.edit("htmlEditor");
htmlEditor.setTheme("ace/theme/monokai");
htmlEditor.session.setMode("ace/mode/html");
htmlEditor.setHighlightActiveLine(false);
htmlEditor.setShowPrintMargin(false);
htmlEditor.setFontSize(18);

$('#view').hide();

$.ajax({
	type: "GET",
	url: "demo.html",
	success: function (data) {
		htmlEditor.setValue(data);
		htmlEditor.clearSelection();
	}
});

$.ajax({
	type: "GET",
	url: "demo.json",
	success: function (data) {
		editor.setValue(JSON.stringify(data, null, '\t'));
		editor.clearSelection();
	}
});

var convert = function () {

	$.ajax({
		type: "POST",
		url: "api/v1/values",
		data: JSON.stringify({ "Data": editor.getValue(), "Template": htmlEditor.getValue() }),
		beforeSend: function () {
			$('#submit').addClass('disabled');
			$('#view').hide();
		},
		success: function (data) {
			$('#view').attr('href', data);
			$('#view').show();
			$('#submit').removeClass('disabled');
		},
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		}
	});

};