module.exports = function (grunt) {
    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        jshint: {
            options: {
                jshintrc: './.jshintrc'
            },
            dist: ['js/caleran.js']
        },
        uglify: {
            build: {
                options: {
                    sourceMap: true
                },
                src: ['js/caleran.js', 'build/vendor/jquery.hammer.js'],
                dest: 'build/js/caleran.min.js'
            },
            docs: {
                options: {
                    sourceMap: false,
                    compress: {
                        global_defs: {
                            "DEBUG": false
                        },
                        dead_code: true
                    }
                },
                src: [
                    'build/vendor/jquery.min.js',
                    'build/vendor/moment.min.js',
                    'build/js/caleran.min.js',
                    'docs/includes/bootstrap.min.js',
                    'docs/includes/prism.js',
                    'docs/includes/pace.js',
                    'docs/includes/toc.min.js'
                ],
                dest: 'docs/includes/scripts.min.js'
            }
        },
        'dart-sass': {
            dist: {
                options: {
                    outputStyle: 'compressed',
                    sourceMap: false
                },
                files: {
                    'build/css/caleran.min.css': 'css/caleran.scss'
                }
            }
        },
        postcss: {
            options: {
                processors: [
                    require("autoprefixer")({ overrideBrowserslist: ["last 3 versions", "ie > 9", "> 1%"] })
                ]
            },
            files: {
                expand: true,
                cwd: "build/css/",
                dest: "build/css/",
                src: ["caleran.min.css"]
            }
        },
        watch: {
            scripts: {
                files: ['js/caleran.js'],
                tasks: ['jshint', 'uglify']
            },
            styles: {
                files: ['css/caleran.scss'],
                tasks: ['dart-sass', 'postcss']
            },
            docs: {
                files: ['readme.md', 'docs/includes/template.html'],
                tasks: 'markdown'
            }
        },
        markdown: {
            all: {
                files: [{
                    expand: true,
                    src: 'readme.md',
                    dest: 'docs/',
                    ext: '.html'
                }],
                options: {
                    template: 'docs/includes/template.html',
                    autoTemplate: true,
                    autoTemplateFormat: 'html'
                }
            }
        },
        compress: {
            main: {
                options: {
                    archive: 'output/caleran.zip'
                },
                files: [{
                    src: ['css/**'],
                    dest: '/',
                }, {
                    src: ['build/**'],
                    dest: '/'
                }, {
                    src: ['js/**'],
                    dest: '/'
                }, {
                    src: ['docs/**', '!docs/includes/back.jpg', '!docs/includes/icons/**/*'],
                    dest: '/'
                }, {
                    src: ['tests/*.test.js', 'tests/jasmine.tmpl'],
                    dest: '/'
                }, {
                    src: ['gruntfile.js', '.gitignore', '.jshintrc', 'package.json', 'readme.md', 'CHANGELOG'],
                    dest: '/'
                },]
            },
            screenshots: {
                options: {
                    archive: 'output/screenshots.zip'
                },
                files: [{
                    expand: true,
                    cwd: 'toolbox/screenshots/',
                    src: ['*.png', '!inline.png', '!thumbnail.png'],
                    dest: '/'
                }]
            },
        },
        copy: {
            main: {
                expand: true,
                cwd: 'toolbox',
                src: ['inline.png', 'thumbnail.png'],
                dest: 'output/',
            },
        },
        browserSync: {
            dev: {
                bsFiles: {
                    src: [
                        'build/css/*.min.css',
                        'build/js/*.min.js'
                    ]
                },
                options: {
                    watchTask: true,
                    server: {
                        baseDir: "./"
                    },
                    startPath: "docs/single-test.html"
                }
            },
            docs: {
                bsFiles: {
                    src: [
                        'docs/**/*'
                    ]
                },
                options: {
                    watchTask: true,
                    server: {
                        baseDir: "./"
                    },
                    startPath: "docs/readme.html"
                }
            },
            test: {
                bsFiles: {
                    src: [
                        'tests/*.test.js',
                        'tests/output/caleran.html'
                    ]
                },
                options: {
                    watchTask: true,
                    online: false,
                    server: {
                        baseDir: "./"
                    },
                    startPath: "tests/output/caleran.html"
                }
            },
            browsertest: {
                bsFiles: {
                    src: [
                        'tests/output/caleran.html'
                    ]
                },
                options: {
                    watchTask: false,
                    online: false,
                    browser: ["chrome", "firefox", "internet explorer"],
                    server: {
                        baseDir: "./"
                    },
                    startPath: "tests/output/caleran.html",
                    notify: false,
                    codeSync: false
                }
            }
        },
        karma: {
            options: {
                basePath: '',
                frameworks: ['jasmine'],
                listenAddress: 'localhost',
                reporters: ['dots', 'code', 'coverage'],
                preprocessors: { 'js/caleran.js': 'coverage' },
                port: 9876,
                colors: true,
                files: [],
                browsers: [],
                autoWatch: false,
                singleRun: true,
                customLaunchers: {
                    ChromeMobile: {
                        base: 'Chrome',
                        flags: ['--window-size=414,736', '--use-mobile-user-agent', '--user-agent="Mozilla/5.0 (Linux; Android 7.0; SM-G930V Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.125 Mobile Safari/537.36"']
                    }
                },
                htmlReporter: {
                    outputFile: 'tests/output/caleran.html',
                    pageTitle: 'Caleran Behaviour Tests',
                    groupSuites: true,
                    useCompactStyle: true,
                    useLegacyStyle: true
                },
                codeReporter: {
                    outputPath: 'tests/code',
                    testFiles: ['tests/*.test.js'],
                    cssFiles: ['build/css/caleran.min.css']
                },
                coverageReporter: {
                    type: 'html',
                    dir: 'tests/coverage/'
                }
            },
            test: {
                files: [{
                    src: ['build/vendor/jquery.min.js', 'build/vendor/moment.min.js', 'build/vendor/jquery.hammer.js', 'js/caleran.js', 'tests/caleran.test.js', 'tests/caleran.inline.test.js', 'tests/caleran.hotelmode.test.js', 'tests/caleran.ranges.test.js']
                }],
                browsers: ['Chrome'],
            },
            ranges: {
                files: [{
                    src: ['build/vendor/jquery.min.js', 'build/vendor/moment.min.js', 'build/vendor/jquery.hammer.js', 'js/caleran.js', 'tests/caleran.ranges.test.js']
                }],
                browsers: ['Chrome', 'ChromeMobile']
            },
            tz: {
                files: [{
                    src: ['build/vendor/jquery.min.js', 'build/vendor/moment.min.js', 'build/vendor/jquery.hammer.js', 'js/caleran.js', 'tests/caleran.test.js', 'tests/caleran.inline.test.js', 'tests/caleran.hotelmode.test.js', 'tests/caleran.ranges.test.js']
                }],
                browsers: ['Chrome', 'Firefox', 'ChromeMobile'],
            },
            mobile: {
                files: [{
                    src: ['build/vendor/jquery.min.js', 'build/vendor/moment.min.js', 'build/vendor/jquery.hammer.js', 'js/caleran.js', 'tests/caleran.test.js', 'tests/caleran.inline.test.js', 'tests/caleran.hotelmode.test.js', 'tests/caleran.ranges.test.js']
                }],
                browsers: ['ChromeMobile'],
            }
        }
    });

    grunt.loadNpmTasks('grunt-markdown');
    grunt.loadNpmTasks('grunt-postcss');
    grunt.loadNpmTasks('grunt-browser-sync');
    grunt.loadNpmTasks('grunt-contrib-compress');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-dart-sass');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-karma');

    grunt.registerTask('min', ['jshint', 'uglify', 'dart-sass', 'postcss', 'markdown', 'compress', 'copy']);
    grunt.registerTask('default', ['jshint', 'uglify', 'dart-sass', 'postcss', 'markdown', 'compress', 'copy', 'karma:tz']);
    grunt.registerTask('ranges', ['karma:ranges']);
    grunt.registerTask('watcher', ['browserSync:dev', 'watch']);
    grunt.registerTask('watchdocs', ['browserSync:docs', 'watch']);
    grunt.registerTask('test', ['browserSync:test', 'watch:test']);
    grunt.registerTask('testbrowsers', ['browserSync:browsertest']);
};
