/// <binding BeforeBuild='default' />
const { series, parallel, src, dest } = require('gulp');
const concat    = require('gulp-concat');
const uglify    = require('gulp-uglify');
const sass      = require('gulp-sass')(require('sass'))

function js() {
    return src(['wwwroot/lib/jquery/dist/jquery.js',
                'wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.js',
                'wwwroot/lib/datatables.net/jquery.dataTables.js',
                'wwwroot/lib/feather-icons/feather.js',
                'wwwroot/lib/select2/js/select2.js',
                'wwwroot/lib/keymaster/keymaster.js',
                'wwwroot/js/site.js'])
            .pipe(concat('mainbundle.js'))
            // .pipe(uglify())
            .pipe(dest('wwwroot/dist/'))
}

function buildStyles() {
    return src(['wwwroot/lib/bootstrap/dist/css/bootstrap.css',
                'wwwroot/lib/select2/css/select2.css',
                'wwwroot/css/dashboard.css',
                // custom
                'wwwroot/sass/**/*.scss'])
                .pipe(sass().on('error', sass.logError))
                .pipe(concat('mainbundle.css'))
                // .pipe(uglify())
                .pipe(dest('wwwroot/dist'));
}

exports.default = parallel(js, buildStyles);