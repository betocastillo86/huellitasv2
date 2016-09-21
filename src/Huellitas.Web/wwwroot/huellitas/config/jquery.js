(function ($) {
    $.fn.toogleWrapper = function (obj, init) {
        init = init === undefined ? true : init;
        obj = obj || {};

        _.defaults(obj, {
            className: '',
            backgroundColor: (this.css('backgroundColor') != 'transparent' ? this.css('backgroundColor') : 'white'),
            zIndex: (this.css('zIndex') == 'auto' || this.css('zIndex') == '0' ? 1000 : parseInt(this.css('zIndex')))
        });

        var $offset = this.offset();
        var $width = this.outerWidth(false);
        var $height = this.outerHeight(false);

        if (init) {
            $('<div>')
                .appendTo('body')
                    .addClass(obj.className)
                    .attr('data-wrapper', true)
                    .css({
                        width: $width,
                        height: $height,
                        top: $offset.top,
                        left: $offset.left,
                        position: 'absolute',
                        zIndex: obj.zIndex + 1,
                        backgroundColor: obj.backgroundColor
                    });

            this.addClass(obj.className);
        }
        else {
            $('[data-wrapper]').remove();
            this.removeClass(obj.className);
        }
    };
})($);