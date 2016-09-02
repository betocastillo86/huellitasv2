this.Huellitas = (function (Backbone, Marionette) {
    var App = new Marionette.Application();

    App.addRegions({
        headerRegion: '#header-region',
        mainRegion: '#main-region',
        leftRegion: '#left-region',
    });

    App.rootRoute = '/';

    App.addInitializer(function(){
        App.module('HeaderApp').start();
        App.module('LeftApp').start();
        //App.module('Main').start();
        //App.module('MainApp').start();
        //App.module('FooterApp').start();
    });

    App.on('before:start', function () {
        console.log('Antes de empezar 12');
    });

    App.on('start', function () {
        console.log('Despues de inicializar 2');
        this.startHistory();
        if(!this.getCurrentRoute())
            this.navigate(this.rootRoute, { trigger: true });
    });

    return App;

})(Backbone, Marionette);