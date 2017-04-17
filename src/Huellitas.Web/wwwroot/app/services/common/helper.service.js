
(function () {
    'use strict';

    angular
      .module('huellitasServices')
      .factory('helperService', helperService);

    ///helperService.$inject = [''];

    function helperService() {
        var service = {
            configServiceUrl: configServiceUrl
        };

        return service;

        function configServiceUrl(localUrl)
        {
            if ($window.isIE) {
                var rdn = Math.floor(Math.random() * 600) + 1;
                var url = nosune.Settings.authorityDomain + localUrl;
                return url.indexOf('?') > -1 ? url + '&rdn=' + rdn : url + '?rdn=' + rdn;
            } else {
                return nosune.Settings.authorityDomain + localUrl;
            }
        }
    }
})();
