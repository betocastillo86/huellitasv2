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
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    replace = require("gulp-replace"),
    flatten = require("gulp-flatten"),
    concat = require('gulp-concat');

var paths = {
    webroot: './wwwroot/',
    approotAdmin: './wwwroot/app/admin',
    approotFront: './wwwroot/app/front',
    approotServices: './wwwroot/app/services',
    external: './node_modules/externalHuellitas/',
    sassFront : './wwwroot/sass/'
};

/********************ADMINISTRADOR*******************************/

paths.libsAdmin = [
    paths.external + 'gentelella/vendors/jquery/dist/jquery.js',
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

paths.libsFront = [
    paths.external + 'angular/angular.js',
    paths.external + 'ngstorage/ngStorage.js',
    paths.external + 'angular-route/angular-route.js',
    paths.external + 'angular-sanitize/angular-sanitize.js',
    paths.external + 'angular-animate/angular-animate.js',
    paths.external + 'underscore/underscore.js',
    paths.external + 'moment/moment.js',
    paths.external + 'moment/locale/es.js',
    paths.external + 'pikaday/pikaday.js',
    paths.external + 'gentelella/vendors/jquery/dist/jquery.js',
    paths.external + 'gentelella/vendors/bootstrap/js/modal.js'
];

paths.concatJsDestAdmin = paths.webroot + "js/site.min.js";
paths.concatJsDestFront = paths.webroot + "js/front.site.min.js";

gulp.task("release", ['scriptsReleaseAdmin', 'cssAdmin']);

gulp.task('scriptsReleaseAdmin', function () {

    var files = [
        paths.approotAdmin + '**/*.js'
    ];

    files.concat(paths.libsAdmin);

    console.log('Inicia tarea scripts', files);

    return gulp.src(files, { base: '.' })
    .pipe(concat(paths.concatJsDestAdmin))
    //.pipe(uglify())
    .pipe(gulp.dest('.'));
});

gulp.task('cssAdmin', ['moveResources'], function () {
    
    var files = [
        paths.external + 'gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
        paths.external + 'gentelella/vendors/font-awesome/css/font-awesome.min.css',
        paths.external + 'gentelella/build/css/custom.min.css',
        paths.external + 'textAngular/dist/textAngular.css',
        paths.external + 'angucomplete-alt/angucomplete-alt.css',
        paths.external + 'pikaday/css/pikaday.css',
        paths.webroot + 'css/huellitas.css'
    ];

    console.log('Inicia tarea de css con ', files);

    return gulp.src(files, { base: '.' })
        .pipe(cssConcat(paths.webroot + "css/styles.css"))
        //.pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(replace(/\.\.\/\.\.\/node_modules\/externalHuellitas\/\gentelella\/vendors\/bootstrap\/dist\/fonts/g, '/fonts'))
        .pipe(replace(/\.\.\/\.\.\/node_modules\/externalHuellitas\/\gentelella\/vendors\/font-awesome\/fonts/g, '/fonts'))
        .pipe(gulp.dest('.'));
});

gulp.task('moveResources', function () {
    var filesToMove = [
        paths.external + 'gentelella/vendors/bootstrap/dist/fonts/*.*',
        paths.external + 'gentelella/vendors/font-awesome/fonts/*.*'
    ];
    return gulp.src(filesToMove, { base: '.' })
        .pipe(flatten())
        .pipe(gulp.dest(paths.webroot+'fonts'));
})

gulp.task('scriptsDevAdmin', function () {
    console.log('Se generan los archivos', paths.libsAdmin);
    return gulp.src(paths.libsAdmin, { base: '.' })
            .pipe(concat(paths.concatJsDestAdmin))
            //.pipe(uglify())
            .pipe(gulp.dest('.'));
});

/*************************FIN ADMINISTRADOR*************************/

/*************************FRONT*************************/

//gulp.task('sassFront', function () {
//    return gulp.src(paths.sassFront + 'styles.scss')
//        .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
//        .pipe(cssmin({ keepSpecialComments: 0 }))
//        .pipe(gulp.dest('./content/css/'));
//});


gulp.task('scriptsDevFront', function () {
    console.log('Se generan los archivos', paths.libsFront);
    return gulp.src(paths.libsFront, { base: '.' })
        .pipe(concat(paths.concatJsDestFront))
        //.pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('cssFront',['sassFront'], function () {
    var files = [
        paths.external + 'gentelella/vendors/bootstrap/dist/css/bootstrap-theme.min.css',
        paths.external + 'gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
        paths.webroot + 'css/front/styles.css',
        paths.webroot + 'css/front.huellitas.css'
    ];

    console.log('Inicia tarea de css con ', files);

    return gulp.src(files, { base: '.' })
        .pipe(cssConcat(paths.webroot + "css/front.styles.css"))
        .pipe(replace(/\"fonts\//g, '\"/fonts/'))
        //.pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(gulp.dest('.'));
});

gulp.task('sassFront', function () {
    console.log(paths.sassFront + 'styles.scss')
    return gulp.src(paths.sassFront + 'styles.scss')
        .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
        .pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(gulp.dest(paths.webroot + 'css/front/'));
});