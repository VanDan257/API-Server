/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

	//config.extraPlugins = 'syntaxhighlight';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowerBrowerUrl = '/Assests/plugins/ckfinder/ckfinder.html';
	config.filebrowerImageBrowerUrl = '/Assests/plugins/ckfinder/ckfinder.html?Type=Image';
	config.filebrowerUploadUrl = '/Assests/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	config.filebrowerImageUploadUrl = '/FileUpLoad';

	CKFinder.setupCKEditor(null, '/Assests/plugins/ckfinder/')

};
