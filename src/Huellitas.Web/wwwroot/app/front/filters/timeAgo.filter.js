(function () {

    angular
        .module('huellitas')
        .filter('timeago', timeAgoFilter);


    function timeAgoFilter()
    {
        return timeAgo;

        function timeAgo(date)
        {
            return date.getIntervalTime();
        }
    }

})();