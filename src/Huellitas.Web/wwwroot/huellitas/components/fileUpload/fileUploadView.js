Huellitas.module('Components.FileUpload', function (FileUpload, App, Backbone, Marionette, $, _) {
    FileUpload.UploadView = App.Views.ItemView.extend({
        events: {
            'change input[type=file]': 'fileSelected'
        },
        template: 'fileUpload/fileUpload',
        initialize: function (args) {
            this.setInstancePropertiesFor('url', 'maxSizeMb', 'fileType', 'multiple', 'showLoading', 'asFormat', 'cut');
            this.render();
        },
        serializeData: function () {
            return {
                multiple: this.multiple
            };
        },
        open: function () {
            this.$('input').click();
        },
        fileSelected: function (obj) {
            if (window.FileReader) {
                var input = obj.currentTarget;

                var withInvalidSize = false;
                var withInvalidExtension = false;

                for (var i = 0; i < input.files.length; i++) {
                    var file = input.files[i];
                    var fileSizeMB = file.size / 1024 / 1024;
                    if (fileSizeMB > this.maxSizeMb) {
                        withInvalidSize = true;
                    }
                    else {
                        if (!this.isValidExtension(file)) {
                            withInvalidExtension = true;
                        }
                    }
                }

                if (withInvalidSize) {
                    this.triggerMethod('invalid:size');
                    alert('Imagen de máximo ' + this.maxSizeMb + 'MB');
                }
                else if (withInvalidExtension) {
                    this.triggerMethod('invalid:extension');
                    alert('La extensión no es valida');
                }
                else {
                    this.sendFiles(input.files);
                }
            }
            else {
                this.sendFiles(input.files);
            }
        },
        sendFiles: function (files) {
            for (var i = 0; i < files.length; i++) {
                this.sendFile(files[i]);
            }
        },
        sendFile: function (file) {

            //if (this.showLoading)
            //    this.displayAjaxLoading(true);
            var options = {
                asFormat: this.asFormat,
                cut: this.cut
            };
            this.trigger('file:selected', { file: file, options: options });
        },
        isValidExtension: function (obj) {
            var validExtensions;

            switch (this.fileType) {
                case 'all':
                    validExtensions = /[A-Z0-9 \.]/;
                    break;
                case 'image':
                default:
                    validExtensions = /(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG)/;
            }

            return validExtensions.test(obj.name);
        }
    });
});