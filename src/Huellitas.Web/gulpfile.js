/// <binding />

/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
//var bower = require('gulp-bower'),
//    uglify = require('gulp-uglify'),
//    concat = require('gulp-concat');

var uglify = require('gulp-uglify'),
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
    approotComponents: './wwwroot/app/components',
    external: './bower_components/',
    sassFront : './wwwroot/sass/'
};

/********************ADMINISTRADOR*******************************/

paths.libsAdmin = [
    paths.external + 'gentelella/vendors/jquery/dist/jquery.js',
    paths.external + 'angular/angular.js',
    paths.external + 'gentelella/vendors/bootstrap/dist/js/bootstrap.js',
    paths.external + 'ngstorage/ngStorage.js',
    paths.external + 'angular-route/angular-route.js',
    //paths.external + 'angular-sanitize/angular-sanitize.js',
    paths.external + 'underscore/underscore.js',
    paths.external + 'textAngular/dist/textAngular-rangy.min.js',
    //paths.external + 'textAngular/dist/textAngular-sanitize.min.js',
    paths.webroot + 'js/textAngular-sanitize-admin.js',
    paths.external + 'textAngular/dist/textAngular.min.js',
    paths.external + 'moment/moment.js',
    paths.external + 'moment/locale/es.js',
    paths.external + 'pikaday/pikaday.js',,
    //paths.external + 'gentelella/vendors/bootstrap/js/modal.js',
    paths.external + 'angucomplete-alt/angucomplete-alt.js',
];

paths.libsFront = [
    paths.external + 'gentelella/vendors/jquery/dist/jquery.js',
    paths.external + 'angular/angular.js',
    paths.external + 'ngstorage/ngStorage.js',
    paths.external + 'angular-route/angular-route.js',
    paths.external + 'angular-sanitize/angular-sanitize.js',
    paths.external + 'angular-animate/angular-animate.js',
    paths.external + 'underscore/underscore.js',
    paths.external + 'moment/moment.js',
    paths.external + 'moment/locale/es.js',
    paths.external + 'pikaday/pikaday.js',
    paths.external + 'gentelella/vendors/bootstrap/js/modal.js',
    paths.external + 'angucomplete-alt/angucomplete-alt.js',
    paths.external + 'fullpage.js/dist/jquery.fullpage.js'
];

paths.concatJsDestAdmin = paths.webroot + "js/admin.site.min.js";
paths.concatJsDestFront = paths.webroot + "js/front.site.min.js";
 
gulp.task('resourcesAdminRelease', function () {
    var files = paths.libsAdmin;

    console.log('Inicia tarea resourcesAdminRelease ', files);

    return gulp.src(files, { base: '.' })
        .pipe(concat(paths.webroot + "js/admin.resources.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('resourcesAdminDev', function () {
    var files = paths.libsAdmin;

    console.log('Inicia tarea resourcesAdminDev ', files);

    return gulp.src(files, { base: '.' })
        .pipe(concat(paths.webroot + "js/admin.resources.min.js"))
        .pipe(gulp.dest('.'));
});

gulp.task('scriptsReleaseAdmin', ['resourcesAdminRelease'], function () {

    var files = [];
    //files = files.concat(paths.libsAdmin);
    files = files.concat([
        paths.approotAdmin + '/**/*.js',
        paths.approotServices + '/**/*.js', 
        paths.approotComponents + '/**/*.js'
    ]);

    console.log('Inicia tarea scripts', files);

    return gulp.src(files, { base: '.' })
    .pipe(concat(paths.concatJsDestAdmin))
    .pipe(uglify())
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
        paths.external + 'fullpage.js/jquery.fullPage.css',
        paths.webroot + 'css/admin.huellitas.css'
    ];

    console.log('Inicia tarea de css con ', files);

    return gulp.src(files, { base: '.' })
        .pipe(cssConcat(paths.webroot + "css/admin.styles.css"))
        .pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/bootstrap\/dist\/fonts/g, '/fonts'))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/font-awesome\/fonts/g, '/fonts'))
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

gulp.task('scriptsDevAdmin', ['resourcesAdminDev'], function () {
    return gulp.src([''], { base: '.' })
            .pipe(concat(paths.concatJsDestAdmin))
            .pipe(gulp.dest('.'));
});

/*************************FIN ADMINISTRADOR*************************/

/*************************FRONT*************************/
gulp.task('scriptsDevFront', ['fullpage'], function () {
    console.log('Se generan los archivos', paths.libsFront);
    
    return gulp.src(paths.libsFront, { base: '.' })
        .pipe(concat(paths.concatJsDestFront))
        //.pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('fullpage', function () {
    return gulp.src(paths.external + 'fullpage.js/jquery.fullPage.min.js', { base: '.' })
        .pipe(concat(paths.webroot + "js/jquery.fullPage.min.js"))
        .pipe(gulp.dest('.'));
});

gulp.task('cssFront',['sassFront'], function () {
    var files = [
        
        paths.external + 'gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
        paths.webroot + 'css/sassfront/styles.css',
        paths.external + 'angucomplete-alt/angucomplete-alt.css',
        paths.external + 'pikaday/css/pikaday.css',
        paths.webroot + 'css/front.huellitas.css',
        paths.external + 'fullpage.js/dist/jquery.fullpage.css'
    ];

    console.log('Inicia tarea de css con ', files);

    return gulp.src(files, { base: '.' })
        .pipe(cssConcat(paths.webroot + "css/front.styles.css"))
        .pipe(replace(/\"fonts\//g, '\"/fonts/'))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/gentelella\/vendors\/bootstrap\/dist\/fonts/g, '/fonts'))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/font-awesome\/fonts/g, '/fonts'))
        .pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(gulp.dest('.'));
});

gulp.task('sassFront', function () {
    console.log(paths.sassFront + 'styles.scss')
    return gulp.src(paths.sassFront + 'styles.scss')
        .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
        //.pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(gulp.dest(paths.webroot + 'css/sassfront/'));
});

gulp.task('scriptsReleaseFront', function () {

    var files = [];
    files = files.concat(paths.libsFront);
    files = files.concat([
        paths.approotFront + '/**/*.js',
        paths.approotServices + '/**/*.js',
        paths.approotComponents + '/**/*.js'
    ]);
    
    console.log('Inicia tarea scripts', files);

    return gulp.src(files, { base: '.' })
        .pipe(concat(paths.concatJsDestFront))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('release', ['cssFront', 'cssAdmin', 'scriptsReleaseAdmin', 'scriptsReleaseFront'], function () {

});