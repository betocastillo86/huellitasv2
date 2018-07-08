(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('crawlingService', crawlingService);

    crawlingService.$inject = [
        '$window',
        '$timeout',
        '$interval',
        'httpService',
        'routingService'];

    function crawlingService(
        $window,
        $timeout,
        $interval,
        http,
        routingService) {

        var service = {
            post: post,
            openCrawlingWindow: openCrawlingWindow,
            crawlEntireSite: crawlEntireSite
        };

        function post(model) {
            return http.post('/api/crawlings', model);
        }

        function openCrawlingWindow(routeName, routeComplement, callbackFinished) {
            var url = routingService.getFullRoute(routeName, routeComplement);
            openCrawlingWindowByUrl(url, callbackFinished, 3500);
        }

        function openCrawlingWindowByUrl(url, callbackFinished, timeout) {
            var window = openWindow();
            closeWindow(window);

            function openWindow() {
               // return $window.open(url + (url.indexOf('?') == -1 ? '?' : '&') + 'angularjs=true', '_blank');
                url = url + (url.indexOf('?') == -1 ? '?' : '&') + 'angularjs=true';
                return $window.open(url, 'angular'+url, 'width=200,height=200,top=200,left=200')
            }

            function closeWindow(window) {
                $timeout(function () {
                    window.close();
                    if (callbackFinished) {
                        callbackFinished();
                    }
                }, timeout);
            }
        }

        function crawlEntireSite(prefix)
        {
            http.get('/sitemap.xml')
                .then(sitemapCompleted)
                .catch(sitemapError);

            var maxWindowsThreads = 3;
            var currentThreads = 0;
            var finalLocations = [];
            var countLocationsFinished = 0;
            var listLocationsFinished = [];
            
            function sitemapCompleted(response)
            {
                var parser = new DOMParser();
                var xmlDoc = parser.parseFromString(response, "text/xml");

                var locations = xmlDoc.getElementsByTagName("loc");

                _.each(locations, function (location) {
                    location = location.innerHTML;
                    if (!prefix || (prefix && location.startsWith(prefix))) {
                        finalLocations.push(location);
                    }
                });

                console.log("Urls para actualizar " + finalLocations.length);

                var promise = $interval(function () {
                    if (currentThreads <= maxWindowsThreads) {
                        processLocations(countLocationsFinished);
                    }
                    if (countLocationsFinished >= finalLocations.length) {
                        $interval.cancel(promise);
                        alert("Proceso finalizado");
                    }
                }, 1000);
            }

            function processLocations(initialPosition)
            {
                for (var i = initialPosition; i < finalLocations.length; i++) {
                    var url = finalLocations[i];

                    if (currentThreads <= maxWindowsThreads) {

                        if (listLocationsFinished.includes(url)) {
                            continue;
                        }

                        currentThreads++;

                        console.log("Empieza " + url);

                        openCrawlingWindowByUrl(url, function () {
                            currentThreads--;
                            countLocationsFinished++;
                            listLocationsFinished.push(url);
                            console.log("Termina " + url);
                        }, 6000);
                    }
                    else {
                        break;
                    }
                }
            }

            function sitemapError(ex)
            {
                alert("Error ejecutando");
                console.log(ex);
            }
        }

        return service;
    }




})();