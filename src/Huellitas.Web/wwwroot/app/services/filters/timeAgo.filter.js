(function () {

    angular
        .module('huellitasServices')
        .filter('timeago', timeAgoFilter);


    function timeAgoFilter()
    {
        return timeAgo;

        function timeAgo(date)
        {
            return date && date != '' ? date.getIntervalTime() : '';
        }
    }

})();