this.Huellitas = (function (Backbone, Marionette) {
    var App = new Marionette.Application();

    App.addRegions({
        headerRegion: '#header-region',
        mainRegion: '#main-region',
        footerRegion: '#footer-region',
    });

    App.rootRoute = '/';

    App.addInitializer(function(){
        App.module('HeaderApp').start();
        //App.module('FooterApp').start();
    });

    App.on('before:start', function () {
        console.log('Antes de empezar 12');
    });

    App.on('initialize:after', function () {
        console.log('Despues de inicializar 2');
        this.startHistory();
        if(!this.getCurrentRoute())
            this.navigate(this.rootRoute, { trigger: true });
    });

    return App;

})(Backbone, Marionette);