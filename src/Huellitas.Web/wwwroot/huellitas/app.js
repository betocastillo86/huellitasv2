'use strict';
this.Huellitas = (function (Backbone, Marionette) {
    var App = new Marionette.Application();

    App.addRegions({
        headerRegion: '#header-region',
        mainRegion: '#main-region',
        leftRegion: '#left-region'
    });

    App.rootRoute = '/';

    App.vent.on('menu:choose', function (module) {
        if (App.menuModules.length)
        {
            App.menuModules.selectByKey(module);
        }
        else
        {
            App.menuModules.on('sync', function () {
                App.menuModules.selectByKey(module);
            });
        }
    });

    App.addInitializer(function(){
        App.module('HeaderApp').start();
        App.module('LeftApp').start(App.menuModules);
        //App.module('Main').start();
        //App.module('MainApp').start();
        //App.module('FooterApp').start();
    });

    App.on('before:start', function () {
        App.menuModules = App.request('module:entities');
    });

    App.on('start', function () {
        console.log('Despues de inicializar 2');
        this.startHistory();
        if(!this.getCurrentRoute())
            this.navigate(this.rootRoute, { trigger: true });
    });

    return App;

})(Backbone, Marionette);