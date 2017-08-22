(function () {
    'use strict';
    
    angular.module('huellitasComponents')
        .service('adsenseService', [function () {
            console.log("Entra al servicio");
        this.url = 'https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js';
        this.isAlreadyLoaded = false;
    }]);

    angular
        .module('huellitasComponents')
        .directive('adsenseHuellitas', adsenseHuellitas);

    adsenseHuellitas.$inject = ['$timeout', 'adsenseService'];

    

    function adsenseHuellitas($timeout, adsenseService) {
        var directive = {
            restrict: 'A',
            scope: {
                adClass: '@',
                adClient: '@',
                adSlot: '@',
                adFormat: '@'
            },
            template: '<ins class="adsbygoogle" style="display:block" ' + (app.Settings.isDebug ? 'data-ad-test="on" data-adtest="on"' : '') +' data-ad-client="{{adClient}}" data-ad-slot="{{adSlot}}" data-ad-format="{{adFormat}}"></ins>',
            //controller: adsenseController,
            link: link
        };

        function link(scope, element, attrs)
        {
            if (!app.Settings.general.adsenseEnabled) {
                element.remove();
            }
            else
            {
                //////if (element[0].offsetWidth) {
                //////    //if (!adsenseService.isAlreadyLoaded) {
                //////    //    var s = document.createElement('script');
                //////    //    s.type = 'text/javascript';
                //////    //    s.src = adsenseService.url;
                //////    //    s.async = true;
                //////    //    document.body.appendChild(s);
                //////    //    adsenseService.isAlreadyLoaded = true;
                //////    //}

                //////    $timeout(function () {
                //////        console.log("registra " + element[0].offsetWidth, attrs.$attr);
                //////        console.log("registre google");
                //////        (adsbygoogle = window.adsbygoogle || []).push({});
                //////    }/*, 2000*/);
                //////}
                //////else {
                //////    console.log("elimina " + element[0].offsetWidth, attrs.$attr);
                //////    element.html('');
                //////}

                $timeout(function () {

                    if (element[0].offsetWidth) {
                        console.log("registra " + element[0].offsetWidth, attrs.adSlot);
                        console.log("registre google");
                        (adsbygoogle = window.adsbygoogle || []).push({});
                    }
                    else {
                        console.log("elimina " + element[0].offsetWidth, attrs.adSlot);
                        element.remove();
                    }
                }, 1000/*, 2000*/);

                

            }
        }

        return directive;
    }

    //adsenseController.$inject = ['$timeout', 'adsenseService'];

    //function adsenseController($timeout, adsenseService) {

    //    if (!adsenseService.isAlreadyLoaded) {
    //        var s = document.createElement('script');
    //        s.type = 'text/javascript';
    //        s.src = adsenseService.url;
    //        s.async = true;
    //        document.body.appendChild(s);
    //        adsenseService.isAlreadyLoaded = true;
    //    }

    //    $timeout(function () {
    //        console.log("registre google");
    //        (adsbygoogle = window.adsbygoogle || []).push({});
    //    }, 1000);
    //}
})();