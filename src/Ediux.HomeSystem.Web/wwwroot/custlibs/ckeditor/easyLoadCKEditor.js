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
					'highlight',
					'|',
					'bold',
					'italic',
					'link',
					'bulletedList',
					'numberedList',
					'fontBackgroundColor',
					'fontColor',
					'fontFamily',
					'fontSize',
					'|',
					'outdent',
					'indent',
					'|',
					'alignment',
					'|',
					'imageUpload',
					'blockQuote',
					'insertTable',
					'mediaEmbed',
					'imageInsert',
					'undo',
					'redo',
					'code',
					'codeBlock',
					'htmlEmbed',
					'horizontalLine',
					'|',
					'findAndReplace'
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