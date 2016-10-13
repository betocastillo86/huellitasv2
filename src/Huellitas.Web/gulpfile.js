/// <binding BeforeBuild='css' AfterBuild='templatesHandlebars' />
/// <binding BeforeBuild='externallibs' />
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


gulp.task('externallibs', function () {
    return bower()
        .pipe(gulp.dest('wwwroot/lib/'));
});

var paths = {
    webroot: './wwwroot/'
};

paths.js = [
    paths.webroot + 'huellitas/app.js',
    paths.webroot + 'huellitas/config/**/*.js',
    paths.webroot + 'huellitas/entities/**/*.js',
    paths.webroot + 'huellitas/views/**/*.js',
    paths.webroot + 'huellitas/apps/**/*.js'
]
paths.libs = [
    paths.webroot + 'lib/jquery/dist/jquery.js',
    paths.webroot + 'lib/underscore/underscore.js',
    paths.webroot + 'lib/backbone/backbone.js',
    paths.webroot + 'lib/backbone.stickit/backbone.stickit.js',
    paths.webroot + 'lib/backbone-validation/dist/backbone-validation.js',
    paths.webroot + 'lib/marionette/lib/backbone.marionette.js',
    paths.webroot + 'lib/handlebars/handlebars.js',
    paths.webroot + 'lib/marionette.handlebars/dist/marionette.handlebars.js',
];

paths.css = [
    paths.webroot + 'lib/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
    paths.webroot + 'lib/gentelella/vendors/font-awesome/css/font-awesome.min.css',
    paths.webroot + 'lib/gentelella/build/css/custom.min.css',
    paths.webroot + 'css/huellitas.css'
];
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatJsTemplatesDest = paths.webroot + "js/templates.min.js";
paths.concatCssDest = paths.webroot + "css/styles.css";

var finalPaths = [];
finalPaths = finalPaths.concat(paths.libs);
finalPaths = finalPaths.concat(paths.js);


gulp.task('watcher', function () {
    console.log('Inicia tarea watcher');
    //return gulp.watch(paths.webroot + 'huellitas/**/*.js', function () {
    //    console.log('Concatena en desarrollo el main');
    //    gulp.src(finalPaths, { base: '.' })
    //            .pipe(concat(paths.concatJsDest))
    //            .pipe(gulp.dest('.'));
    //});
    return gulp.watch(paths.webroot + 'huellitas/**/*.html', {}, ['templatesHandlebars']);

});

gulp.task('scripts', function () {
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

gulp.task('dev', function () {
    return gulp.src(paths.libs, { base: '.' })
            .pipe(concat(paths.concatJsDest))
            .pipe(uglify())
            .pipe(gulp.dest('.'));
});

gulp.task('templatesHandlebars', function () {
    console.log('Ejecutando tarea templatesHandlebars');
    var path = [paths.webroot + 'huellitas/apps/**/*.html', paths.webroot + 'huellitas/components/**/*.html'];
    console.log(path);
    return gulp.src(path)
        .pipe(handlebars({handlebars:require('handlebars')}))
        .pipe(wrap('Handlebars.template(<%= contents %>)'))
        .pipe(declare({
            namespace: 'HTempl',
            noRedeclare: true,
            //processName: declare.processNameByPath
            processName: function (path) {
                path = path.split(/(wwwroot\\huellitas\\apps\\|wwwroot\\huellitas\\components\\)/)[2];
                var finalPath = path.replace('templates\\','').replace(/\\/g, '/').replace('.js', '');
                return finalPath;
            }
        }))
        .pipe(concat(paths.concatJsTemplatesDest))
        .pipe(gulp.dest('.'));
});