/// <binding BeforeBuild='build:sassFramework' ProjectOpened='watch-source-files' />
"use_strict";
//TODO: clean up required files and place the needed comments on all the tasks for later use

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    csso = require("gulp-csso"),
    autoprefixer = require("gulp-autoprefixer"),
    less = require("gulp-less"),
    sass = require("gulp-sass"),
    clean = require("gulp-clean");
    fs = require("fs"),
    concat = require("gulp-concat"),
    rename = require("gulp-rename"),
    cssmin = require("gulp-clean-css"),
    minify = require("gulp-minify"),
    uglify = require("gulp-uglify"),
        uglifyes = require("gulp-uglify-es").default,
        merge = require("merge2"),
    babel = require("gulp-babel");

const AUTOPREFIXER_BROWSERS = [
    'ie >= 10',
    'ie_mob >= 10',
    'ff >= 30',
    'chrome >= 34',
    'safari >= 7',
    'opera >= 23',
    'ios >= 7',
    'android >= 4.4',
    'bb >= 10'
];

// Setting the paths to use
var paths = {
    sourceModules: "node_modules/",
    modules: "./Modules/",
    webroot: "./dist/",
    frameworkStyles: "./Sass/",
    frameworkScripts: "./Scripts/",
    frameworkImages: "./Images/src/"
};

gulp.task("build:sass", function () {
    return gulp.src(paths.frameworkStyles + "ittech24.framework.scss")
        .pipe(sass({
            outputStyle: 'nested',
            precision: 10
        }))
        .pipe(autoprefixer({
            browsers: AUTOPREFIXER_BROWSERS
        }))
        .pipe(gulp.dest(paths.webroot + "css"))
        .pipe(csso())
        .pipe(rename("ittech24.framework.min.css"))
        .pipe(gulp.dest(paths.webroot + "css"));
});

// Building the framework typescript into javascript, this will be a multi stage 
// this will generate the declaration file saved into /dist/types
// the concatenated javascript file saved in /dist/js
// the minified and uglyfied javascript file saved in /dist/js
// the javascript map file for the compiled version
gulp.task("build:typescript", function () {
    var ts = require("gulp-typescript");
    var frameworkTs;
    // using the tsconfig for compiling our project
    if (!frameworkTs) {
        frameworkTs = ts.createProject("tsconfig.json");
    }
    // getting all the ts files existing in the project folder and compiling them into
    // individual files on the js temporary output folder
    return gulp.src(paths.frameworkScripts + "*.ts")
        .pipe(frameworkTs())
        .pipe(gulp.dest(paths.frameworkScripts + "js"));
});

gulp.task("build:types", function () {
    return gulp.src(paths.frameworkScripts + "js/*.d.ts")
        .pipe(concat("ittech24-framework.d.ts"))
        .pipe(gulp.dest(paths.webroot + "types"));
});

gulp.task("build:javascript", function () {
    var sourcemaps = require('gulp-sourcemaps');
    
    return gulp.src([paths.frameworkScripts + "js/**/_common.js",
        paths.frameworkScripts + "js/**/!(_initializer)*.js",
        paths.frameworkScripts + "js/**/_initializer.js"])
        .pipe(concat("ittech24-framework.js"))
        .pipe(gulp.dest(paths.webroot + "js"))
        .pipe(rename("ittech24-framework.min.js"))
        .pipe(sourcemaps.init())
        .pipe(uglifyes({ mangle: { toplevel: true } }))
        .pipe(sourcemaps.write("."))
        .pipe(gulp.dest(paths.webroot + "js"));
});


gulp.task("build:clean", function () {
    return gulp.src(paths.frameworkScripts + "js", { read: false })
        .pipe(clean());
});

gulp.task("build", gulp.series("build:typescript", "build:types", "build:javascript", "build:clean"));

gulp.task('watch-source-files', function () {
    gulp.watch(paths.frameworkStyles + "**/*.scss", gulp.series('build:sass'));
    gulp.watch(paths.frameworkScripts + "**/*.ts", gulp.series('build'));
});
