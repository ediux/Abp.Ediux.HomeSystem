(function ($) {

    $(function () {

        var l = abp.localization.getResource('HomeSystem');

		$('#WelcomeSlogan').CKEditor();
   //     ClassicEditor
			//.create(document.querySelector('#WelcomeSlogan'), { toolbar: {
			//	items: [
			//		'heading',
			//		'highlight',
			//		'|',
			//		'bold',
			//		'italic',
			//		'link',
			//		'bulletedList',
			//		'numberedList',
			//		'fontBackgroundColor',
			//		'fontColor',
			//		'fontFamily',
			//		'fontSize',
			//		'|',
			//		'outdent',
			//		'indent',
			//		'|',
			//		'alignment',
			//		'|',
			//		'imageUpload',
			//		'blockQuote',
			//		'insertTable',
			//		'mediaEmbed',
			//		'imageInsert',
			//		'undo',
			//		'redo',
			//		'code',
			//		'codeBlock',
			//		'htmlEmbed',
			//		'horizontalLine',
			//		'|',
			//		'findAndReplace'
			//	]
			//},
			//	language: 'zh',
			//	image: {
			//	toolbar: [
			//		'imageTextAlternative',
			//		'imageStyle:inline',
			//		'imageStyle:block',
			//		'imageStyle:side'
			//	]
			//},
			//	table: {
			//	contentToolbar: [
			//		'tableColumn',
			//		'tableRow',
			//		'mergeTableCells'
			//	]
			//},
			//	licenseKey: '',
					
					
					
			//	} )
   //         .then(editor => {
   //             editor.model.document.on('change:data', () => {
   //                 $('#WelcomeSlogan').val(editor.getData());
   //                 //  console.log('The data has changed!');
   //             });
   //         })
   //         .catch(error => {
   //             abp.notify.error(error, l('Settings:WebSettingsGroupComponents'));
   //             //console.error(error);
   //         });

        $("#WebSiteSettingsForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();

            ediux.homeSystem.settingManagement.webSiteSettings.update(form).then(function (result) {
                toastr.options.positionClass = 'toast-top-right';
                abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
            });

        });

       
    });

})(jQuery);