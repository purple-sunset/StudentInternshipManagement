$('#editor1').ace_wysiwyg({
    toolbar:
    [
        'font',
        null,
        'fontSize',
        null,
        { name: 'bold', className: 'btn-info' },
        { name: 'italic', className: 'btn-info' },
        { name: 'strikethrough', className: 'btn-info' },
        { name: 'underline', className: 'btn-info' },
        null,
        { name: 'insertunorderedlist', className: 'btn-success' },
        { name: 'insertorderedlist', className: 'btn-success' },
        { name: 'outdent', className: 'btn-purple' },
        { name: 'indent', className: 'btn-purple' },
        null,
        { name: 'justifyleft', className: 'btn-primary' },
        { name: 'justifycenter', className: 'btn-primary' },
        { name: 'justifyright', className: 'btn-primary' },
        { name: 'justifyfull', className: 'btn-inverse' },
        null,
        { name: 'createLink', className: 'btn-pink' },
        { name: 'unlink', className: 'btn-pink' },
        null,
        { name: 'insertImage', className: 'btn-success' },
        null,
        'foreColor',
        null,
        { name: 'undo', className: 'btn-grey' },
        { name: 'redo', className: 'btn-grey' }
    ]
    
}).prev().addClass('wysiwyg-style2');

//file input
$('.message-form input[type=file]').ace_file_input()
    .closest('.ace-file-input')
    .addClass('width-90 inline')
    .wrap('<div class="form-group file-input-container"><div class="col-sm-7"></div></div>');

//Add Attachment
//the button to add a new file input
$('#id-add-attachment')
    .on('click', function () {
        var file = $('<input type="file" name="attachment[]" />').appendTo('#form-attachments');
        file.ace_file_input();

        file.closest('.ace-file-input')
            .addClass('width-90 inline')
            .wrap('<div class="form-group file-input-container"><div class="col-sm-7"></div></div>')
            .parent().append('<div class="action-buttons pull-right col-xs-1">\
							<a href="#" data-action="delete" class="middle">\
								<i class="ace-icon fa fa-trash-o red bigger-130 middle"></i>\
							</a>\
						</div>')
            .find('a[data-action=delete]').on('click', function (e) {
                //the button that removes the newly inserted file input
                e.preventDefault();
                $(this).closest('.file-input-container').hide(300, function () { $(this).remove() });
            });
    });