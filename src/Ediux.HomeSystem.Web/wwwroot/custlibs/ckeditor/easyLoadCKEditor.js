(function ($) {
	var currLCID = abp.localization.currentCulture;
	var loc = currLCID.cultureName;

	if (loc === "zh-Hant") {
		loc = 'zh';
	} else {
		if (loc.cultureName === "zh-Hans") {
			loc = 'zh-cn';
		}
	}

    $.fn.CKEditor = function ($options) {
		var l = abp.localization.getResource('HomeSystem');
		var options = $.extend({
			toolbar: {
				items: [
					'sourceEditing',
					'|',
					'heading',
					'findAndReplace',
					'undo',
					'redo',
					'|',
					'bold',
					'italic',
					'underline',
					'strikethrough',
					'link',
					'subscript',
					'horizontalLine',
					'|',
					'highlight',
					'fontBackgroundColor',
					'fontColor',
					'fontFamily',
					'fontSize',
					'|',
					'bulletedList',
					'numberedList',
					'todoList',
					'|',
					'outdent',
					'indent',
					'alignment',
					'|',
					'imageUpload',
					'imageInsert',
					'blockQuote',
					'insertTable',
					'mediaEmbed',
					'|',
					'code',
					'codeBlock',
					'htmlEmbed'
				]
			},
			language: loc,
			image: {
				toolbar: [
					'imageTextAlternative',
					'imageStyle:inline',
					'imageStyle:block',
					'imageStyle:side'
				]
			},
			table: {
				contentToolbar: [
					'tableColumn',
					'tableRow',
					'mergeTableCells'
				]
			},
			licenseKey: '',
			htmlSupport: {
				allow: [
					{
						name: /.*/,
						attributes: true,
						classes: true,
						styles: true,						
					}
				]
			}
		}, $options);

		var id = '#' + $(this).prop('id');
		ClassicEditor
			.create(document.querySelector(id), options)
			.then(editor => {
				editor.model.document.on('change:data', () => {
					$(id).val(editor.getData());
				});
			})
			.catch(error => {
				abp.notify.error(error, l('Settings:WebSettingsGroupComponents'));
			});
    };
})(jQuery);

initAllEditors();

function initAllEditors() {
	$('textarea').each(function (i, item) {
		initEditor(item);
	});
}

function initEditor(element) {
	var $editorContainer = $(element);
	var inputName = $editorContainer.data('input-id') || $editorContainer.prop('id');
	var $editorInput = $('#' + inputName);
	$editorInput.CKEditor();
}