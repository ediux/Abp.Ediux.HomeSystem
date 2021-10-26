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
					'heading',
					'findAndReplace',
					'|',
					'bold',
					'italic',
					'underline',
					'link',
					'bulletedList',
					'numberedList',
					'fontBackgroundColor',
					'fontColor',
					'fontFamily',
					'fontSize',
					'highlight',
					'|',
					'outdent',
					'indent',
					'alignment',
					'todoList',
					'|',
					'imageUpload',
					'blockQuote',
					'insertTable',
					'mediaEmbed',
					'undo',
					'redo',
					'|',
					'code',
					'codeBlock',
					'sourceEditing',
					'htmlEmbed',
					'horizontalLine'
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