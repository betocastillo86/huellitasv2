var gulp = require("gulp"),
    cssmin = require("gulp-cssmin"),
    watch = require("gulp-watch"),
    webserver = require("gulp-webserver"),
    sass = require("gulp-sass");


// compile sass files
gulp.task("css", function () {
    return gulp.src('./content/sass/styles.scss')
        .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
        .pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(gulp.dest('./content/css/'));
});

//  conf web server
gulp.task('server', function() {
  gulp.src('')
    .pipe(webserver({
      // host: '192.168.1.57',
      // port: 8008,
      livereload: true
    }));
});

// watch task for sass files
gulp.task('watch', function () {
    gulp.watch('./content/sass/*.scss', ['css']);

});

// default task
gulp.task('default', ['watch', 'server']);