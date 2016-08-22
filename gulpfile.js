var gulp = require('gulp');
var exec = require('child_process').exec;
var colors = require('colors');

gulp.task('Tests', function (x) {
    process.chdir('.\\tests\\test.uglymappers');
    exec('dotnet test', function (error, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        if (error) {
            console.error(`TESTS FAILED`.red);
            done(false);
        } else {
            console.error(`TESTS PASSED`.green);
        }
    });
});