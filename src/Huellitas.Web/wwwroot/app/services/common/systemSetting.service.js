(function () {

    angular.module('huellitasServices')
        .factory('systemSettingService', systemSettingService);

    systemSettingService.$inject = ['httpService'];

    function systemSettingService(http) {
        return {
            get: get,
            put: put
        };

        function get(filter) {
            return http.get('/api/systemsettings', { params: filter });
        }

        function put(model) {
            return http.put('/api/systemsettings/' + model.id, model);
        }
    }
})();