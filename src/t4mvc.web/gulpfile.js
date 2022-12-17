const { series, parallel, src, dest } = require('gulp');
const concat                = require('gulp-concat');
const uglify                = require('gulp-uglify');

function js() {
    return src(['wwwroot/lib/jquery/dist/jquery.js',
                'wwwroot/lib/bootstrap/dist/js/bootstrap.js',
                'wwwroot/lib/datatables.net/jquery.dataTables.js',
                'wwwroot/lib/feather-icons/feather.js',
                'wwwroot/lib/select2/js/select2.js',
                'wwwroot/lib/keymaster/keymaster.js',
                'wwwroot/js/site.js'])
            .pipe(concat('mainbundle.js'))
            // .pipe(uglify())
            .pipe(dest('wwwroot/dist/'))
}

function css() {
    return src(['wwwroot/lib/bootstrap/dist/css/bootstrap.css',
                'wwwroot/lib/select2/css/select2.css',
                // custom
                'wwwroot/css/site.css'])
                .pipe(concat('mainbundle.css'))
                // .pipe(uglify())
                .pipe(dest('wwwroot/dist/'))
}

exports.default = parallel(js, css);