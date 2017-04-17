(function () {
    angular.module('huellitasServices')
    .factory('customTableRowService', customTableRowService);

    customTableRowService.$inject = ['httpService'];

    function customTableRowService(http)
    {
        return {
            getByTable: getByTable,
            getSizes: getSizes,
            getSubtypes: getSubtypes,
            getGenres: getGenres
        };

        function getByTable(tableId)
        {
            return http.get('/api/customtables/' + tableId + '/rows');
        }

        function getSizes()
        {
            return getByTable(app.Settings.customTables.animalSize);
        }

        function getSubtypes()
        {
            return getByTable(app.Settings.customTables.animalSubtype);
        }

        function getGenres() {
            return getByTable(app.Settings.customTables.animalGenre);
        }
    }

})();