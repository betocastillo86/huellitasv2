/// <binding />

/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
//var bower = require('gulp-bower'),
//    uglify = require('gulp-uglify'),
//    concat = require('gulp-concat');

var bower = require('gulp-bower'),
    uglify = require('gulp-uglify'),
    wrap = require('gulp-wrap'),
    watch = require('gulp-watch'),
    declare = require('gulp-declare'),
    handlebars = require('gulp-handlebars'),
    cssConcat = require('gulp-concat-css'),
    concat = require('gulp-concat');

var paths = {
    webroot: './wwwroot/',
    approot: './wwwroot/app/',
    external: './node_modules/externalHuellitas/'
};

gulp.task('externallibs', function () {
    return bower()
        .pipe(gulp.dest(paths.external));
});



paths.js = [
    paths.approot + 'admin.module.js'
    //paths.webroot + 'huellitas/config/**/*.js',
    //paths.webroot + 'huellitas/entities/**/*.js',
]

paths.libs = [
    paths.external + 'gentelella/vendors/jquery/dist/jquery.min.js',
    paths.external + 'gentelella/vendors/bootstrap/dist/js/bootstrap.min.js',
    paths.external + 'angular/angular.js',
    paths.external + 'ngstorage/ngStorage.js',
    paths.external + 'angular-sanitize/angular-sanitize.js',
    paths.external + 'angular-route/angular-route.js',
    paths.external + 'angucomplete-alt/angucomplete-alt.js',
    paths.external + 'underscore/underscore.js',
    paths.external + 'textAngular/dist/textAngular-rangy.min.js',
    paths.external + 'textAngular/dist/textAngular-sanitize.min.js',
    paths.external + 'textAngular/dist/textAngular.min.js',
    paths.external + 'moment/moment.js',
    paths.external + 'moment/locale/es.js',
    paths.external + 'pikaday/pikaday.js'
];

paths.css = [
    paths.external + 'gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
    paths.external + 'gentelella/vendors/font-awesome/css/font-awesome.min.css',
    paths.external + 'gentelella/build/css/custom.min.css',
    paths.external + 'textAngular/dist/textAngular.css',
    paths.external + 'pikaday/css/pikaday.css',
    paths.webroot + 'css/huellitas.css'
];

paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatJsTemplatesDest = paths.webroot + "js/templates.min.js";
paths.concatCssDest = paths.webroot + "css/styles.css";

var finalPaths = [];
finalPaths = finalPaths.concat(paths.libs);
finalPaths = finalPaths.concat(paths.js);


//gulp.task('watcher', function () {
//    console.log('Inicia tarea watcher');
//    //return gulp.watch(paths.webroot + 'huellitas/**/*.js', function () {
//    //    console.log('Concatena en desarrollo el main');
//    //    gulp.src(finalPaths, { base: '.' })
//    //            .pipe(concat(paths.concatJsDest))
//    //            .pipe(gulp.dest('.'));
//    //});
//    return gulp.watch(paths.webroot + 'huellitas/**/*.html', {}, ['templatesHandlebars']);

//});

gulp.task('scriptsRelease', function () {
    console.log('Inicia tarea scripts');

    return gulp.src(finalPaths, { base: '.' })
    .pipe(concat(paths.concatJsDest))
    //.pipe(uglify())
    .pipe(gulp.dest('.'));
});

gulp.task('css', function () {
    console.log('Inicia tarea de css con ');

    for (var i = 0; i < paths.css.length; i++) {
        console.log(paths.css[i]);
    }

    return gulp.src(paths.css, { base: '.' })
    .pipe(cssConcat(paths.concatCssDest))
    .pipe(gulp.dest('.'));
});

gulp.task('scriptsDev', function () {
    return gulp.src(paths.libs, { base: '.' })
            .pipe(concat(paths.concatJsDest))
            //.pipe(uglify())
            .pipe(gulp.dest('.'));
});

//gulp.task('templatesHandlebars', function () {
//    console.log('Ejecutando tarea templatesHandlebars');
//    var path = [paths.webroot + 'huellitas/apps/**/*.html', paths.webroot + 'huellitas/components/**/*.html'];
//    console.log(path);
//    return gulp.src(path)
//        .pipe(handlebars({ handlebars: require('handlebars') }))
//        .pipe(wrap('Handlebars.template(<%= contents %>)'))
//        .pipe(declare({
//            namespace: 'HTempl',
//            noRedeclare: true,
//            //processName: declare.processNameByPath
//            processName: function (path) {
//                path = path.split(/(wwwroot\\huellitas\\apps\\|wwwroot\\huellitas\\components\\)/)[2];
//                var finalPath = path.replace('templates\\', '').replace(/\\/g, '/').replace('.js', '');
//                return finalPath;
//            }
//        }))
//        .pipe(concat(paths.concatJsTemplatesDest))
//        .pipe(gulp.dest('.'));
//});