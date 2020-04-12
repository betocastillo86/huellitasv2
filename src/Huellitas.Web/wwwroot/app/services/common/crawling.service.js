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

        var windows = [];

        function post(model) {
            return http.post('/api/crawlings', model);
        }

        function openCrawlingWindow(routeName, routeComplement, callbackFinished, timeout) {
            var url = routingService.getFullRoute(routeName, routeComplement);
            openCrawlingWindowByUrl(url, callbackFinished, timeout ? timeout : 3500);
        }


        function openCrawlingWindowByUrl(url, callbackFinished, timeout) {
            var windowObj = openWindow();
            closeWindow(windowObj.name);

            function openWindow() {
                url = url + (url.indexOf('?') == -1 ? '?' : '&') + 'angularjs=true';
                var name = 'angular' + url;
                var window = $window.open(url, name, 'width=200,height=200,top=200,left=200');
                var windowObj = { name: name, window: window };
                windows[name] = windowObj;
                return windowObj;
            }

            function closeWindow(name) {

                var window = windows[name].window;
                
                $timeout(function () {

                    delete windows[name];
                    console.log('should close ' + name);

                    if (!windows.length) {
                        window.close();
                        if (callbackFinished) {
                            callbackFinished();
                        }
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