/**
 *
                 888
                  888
                  888
 .d8888b  8888b.  888  .d88b.  888d888 8888b.  88888b.
d88P"        "88b 888 d8P  Y8b 888P"      "88b 888 "88b
888      .d888888 888 88888888 888    .d888888 888  888
Y88b.    888  888 888 Y8b.     888    888  888 888  888
 "Y8888P "Y888888 888  "Y8888  888    "Y888888 888  888


 * @name: caleran - the date range picker
 * @description: An inline/popup date range picker
 * @version: 1.4.17
 * @author: Taha PAKSU <tpaksu@gmail.com>
 **/
/* jshint laxbreak: true */
import moment from "moment";
(function ($, window, document, undefined) {
    /**
     *  The main caleran class
     *  @class caleran
     */
    var caleran = function (elem, options) {
        this.elem = elem;
        this.$elem = $(elem);
        this.options = options;
        this.metadata = this.$elem.data('plugin-options');
    };
    /**
     * Prototype of caleran plugin
     * @prototype caleran
     */
    caleran.prototype = {
        /////////////////////////////////////////////////////////////////////
        // public properties that can be set through plugin initialization //
        /////////////////////////////////////////////////////////////////////
        public: function () {
            return {
                startDate: moment().startOf('day'), // (d) the selected start date, initially today
                endDate: moment().startOf('day'), // (d) the selected end date, initially today
                format: 'L', // (d) the default format for showing in input, default short date format of locale
                dateSeparator: ' - ', // (d) if not used as a single date picker, this will be the seperator
                calendarCount: 2, // (d) how many calendars will be shown in the plugin screen
                mobileBreakpoint: 760, // width in pixels to switch to mobile layout on initialization
                isHotelBooking: false, // (d) hotel booking style ranges
                dayText: {},
                inline: false, // (d) display as an inline input replacement
                minDate: null, // (d) minimum selectable date, default null (no minimum)
                maxDate: null, // (d) maximum selectable date, default null (no maximum)
                showHeader: true, // (d) visibility of the part which displays the selected start and end dates
                showFooter: true, // (d) visibility of the part which contains user defined ranges
                rangeOrientation: 'horizontal', // (d) show ranges horizontally below the calendars or vertically on the side
                verticalRangeWidth: 180, // (d) range panel width when rangeOrientation is set to vertical
                showButtons: false, // (d) visibility of the part which contains the buttons on desktop dropdown view
                startOnMonday: false, // (d) if you want to start the calendars on monday, set this to true
                container: 'body', // (d) the container of the dropdowns
                oneCalendarWidth: 230, // (d) the width of one calendar, if two calendars are shown, the input width will be 2 * this setting.
                enableKeyboard: true, // (d) enables keyboard navigation
                showOn: 'bottom', // (d) dropdown placement position relative to input element ( "top", "left", "right", "bottom", "center")
                arrowOn: 'left', // (d) arrow position ("left" "right" "center" for showOn "bottom" or "top", and vice versa)
                autoAlign: true, // (d) automatically reposition the picker when window resize or scroll or dropdown show
                locale: moment.locale(), // (d) moment locale setting, different inputs: https://momentjs.com/docs/#/i18n/changing-locale/ , available locales: https://momentjs.com/ (bottom of the page)
                singleDate: false, // (d) if you want a single date picker, set this to true
                target: null, // (d) the element to update after selection, defaults to the element that is instantiated on
                autoCloseOnSelect: false, // (d) closes the dropdown/modal after valid selection
                startEmpty: false, // (d) starts with no value selected
                isRTL: false, // flag to use RTL layout
                ranges: [
                    {
                        // (d) default range objects array, one range is defined like
                        title: 'Today', // {title(string), startDate(moment object), endDate(moment object) }
                        startDate: moment(),
                        endDate: moment(),
                    },
                    {
                        title: '3 Days',
                        startDate: moment(),
                        endDate: moment().add(2, 'days'),
                    },
                    {
                        title: '5 Days',
                        startDate: moment(),
                        endDate: moment().add(4, 'days'),
                    },
                    {
                        title: '1 Week',
                        startDate: moment(),
                        endDate: moment().add(6, 'days'),
                    },
                    {
                        title: 'Till Next Week',
                        startDate: moment(),
                        endDate: moment().endOf('week'), // if you use Monday as week start, you should use "isoweek" instead of "week"
                    },
                    {
                        title: 'Till Next Month',
                        startDate: moment(),
                        endDate: moment().endOf('month'),
                    },
                ],
                rangeLabel: 'Ranges: ', // (d) the title of defined ranges
                cancelLabel: 'Cancel', // (d) the text on the cancel button
                applyLabel: 'Apply', // (d) the text on the apply button
                nextMonthIcon: "<i class='fa fa-arrow-right'></i>", // (d) the html of the next month icon
                prevMonthIcon: "<i class='fa fa-arrow-left'></i>", // (d) the html of the prev month icon
                rangeIcon: "<i class='fa fa-retweet'></i>", // (d) the html of the range label icon
                headerSeparator: "<i class='fa fa-chevron-right'></i>", // (d) the html of the icon between header dates
                onbeforeselect: function () {
                    // (d) triggered before selection is applied, can be reverted with returning false
                    return true;
                }, // (d) event which is triggered before selecting the end date ( a range selection is completed )
                onafterselect: function () {}, // (d) event which is triggered after selecting the end date ( the input value changed )
                onbeforeshow: function () {}, // (d) event which is triggered before showing the dropdown
                onbeforehide: function () {}, // (d) event which is triggered before hiding the dropdown
                onaftershow: function () {}, // (d) event which is triggered after showing the dropdown
                onafterhide: function () {}, // (d) event which is triggered after hiding the dropdown
                onfirstselect: function () {}, // (d) event which is triggered after selecting the first date of ranges
                onrangeselect: function () {}, // (d) event which is triggered after selecting a range from the defined range links
                onbeforemonthchange: function () {
                    // (d) event which is triggered before switching the month, can be prevented with returning false
                    return true;
                }, // (d) event which fires before changing the first calendar month of multiple calendars, or the month of a single calendar
                onaftermonthchange: function () {}, // (d) event which fires after changing the first calendar month of multiple calendars, or the month of a single calendar
                onafteryearchange: function () {}, // (d)event which fires after changing the first calendar year of multiple calendars, or the year of a single calendar
                ondraw: function () {}, // (d) event which fires after a complete redraw occurs
                onBeforeInit: function () {}, // (d) event which is fired before initialization and after configuration
                onBeforeDestroy: function () {
                    return true;
                }, // (d) event which is fired before destruction
                oninit: function () {}, // (d) event which is fired after complete initialization
                ondestroy: function () {}, // (d) event which is fired after complete destruction
                validateClick: function () {
                    return true;
                }, // (d) event which is fired on cell click, returning false will prevent selection
                onCancel: function () {
                    return true;
                }, // (d) event which is fired on cancel button click, returning false will prevent cancel
                disableDays: function () {
                    // (d) function which is used to disable the related day with returning true after checks
                    return false;
                },
                disabledRanges: [], // (d) array which contains disabled date ranges, refer to docs for the structure
                continuous: false, // (d) flag to make sure the range selected doesn't contain disabled dates
                disableOnlyStart: false, // (d) flag to apply the disables only to start dates
                disableOnlyEnd: false, // (d) flag to apply the disables only to end days
                minSelectedDays: 0, // (d) minimum number of selected days to be accepted
                enableMonthSwitcher: true, // (d) flag to enable the month switcher when clicking the month name on the calendar titles
                enableYearSwitcher: true, // (d) flag to enable the year switcher when clicking the year text on the calendar titles
                enableSwipe: true, // (d) flag to enable the swiped month switch on mobile displays
                numericMonthSwitcher: false, // (d) flag to enable displaying numbers instead of month names in month switcher
                monthSwitcherFormat: 'MMMM', // (d) Changes the month name display format on the month switcher. Default: "MMMM"
                showWeekNumbers: false, // (d) flag to show/hide week numbers
                hideOutOfRange: false, // (d) hides or disables out of range months, years, navigation arrows
                DOBCalendar: false, // (d) Birthdate selection mode
            };
        },
        //////////////////////////////////////////
        // private variables for internal usage //
        //////////////////////////////////////////
        private: function () {
            return {
                startSelected: false, // flag which indicates the start date is selected on the calendar, and the next click will define the end date.
                currentDate: moment().startOf('day'), // the current month which is shown on the first calendar
                endSelected: true, // flag which indicates that the end date is selected. Also means that a complete range is selected.
                hoverDate: null, // the day element which is currently being hovered via mouse
                keyboardHoverDate: null, // the day element which is currently being hovered via keyboard
                headerStartDay: null, // header start day information container element
                headerStartDate: null, // header start date of month container element
                headerStartWeekday: null, // header start week day text container element
                headerEndDay: null, // header end day information container element
                headerEndDate: null, // header end date of month container element
                headerEndWeekday: null, // header end week day text container element
                swipeTimeout: null, // swipe debouncing timeout variable
                isMobile: false, // current environment is mobile or desktop indicator
                valElements: [
                    'BUTTON',
                    'OPTION',
                    'INPUT',
                    'LI',
                    'METER',
                    'PROGRESS',
                    'PARAM',
                ], // elements which support value property
                dontHideOnce: false, // flag that tells the dropdown not to close once
                initiator: null, // element which triggered the dropdown to show
                initComplete: false, // is the plugin completely initialized?
                startDateBackup: null, // start date clone while switching months, used for range selection after month/year switch
                startDateInitial: null, // start date clone when displaying the plugin to use on cancellation operation
                endDateInitial: null, // end date clone when displaying the plugin to use on cancellation operation
                firstValueSelected: false, // used with config.startEmpty, set to true if the initial range selection has been made.
                throttleTimeout: null, // debouncer function timeout variable
                documentEvent: null, // used for separating document bound events for multiple instances
                delayInputUpdate: false, // used for preventing the input to be updated until the apply button is clicked
                lastScrollX: 0, // horizontal buffer variable of scroll positions for using inside requestAnimationFrame
                lastScrollY: 0, // vertical buffer variable of scroll positions for using inside requestAnimationFrame
                isTicking: false, // requestAnimationFrame ticking suppressor
                parentScrollEventsAttached: false, // runonce flag for input's scrollable parents, which scroll events are attached once
                rafID: 0, // requestanimationframe ID for cancellation
                disabledDays: {},
            };
        },
        /**
         * initialize the plugin
         * @return caleran object
         */
        init: function () {
            this.config = $.extend(
                {},
                this.public(),
                this.options,
                this.metadata
            );
            this.globals = $.extend({}, this.private());
            this.globals.isMobile = this.checkMobile();
            this.config.onBeforeInit(this);
            this.applyConfig();
            this.fetchInputs();
            this.drawUserInterface();
            this.drawOverlay();
            this.addInitialEvents();
            this.addKeyboardEvents();
            this.$elem.data('caleran', this);
            this.config.oninit(this);
            this.globals.initComplete = true;
            this.globals.lastScrollX = 0;
            this.globals.lastScrollY = 0;
            $(window).trigger('resize.caleran');
            return this;
        },
        /**
         * validates start and end dates,
         * swaps dates if end > start,
         * sets visible month of first selection
         *
         * @return void
         */
        validateDates: function () {
            // validate start & end dates
            var swap;
            if (
                moment(this.config.startDate, this.config.format).isValid() &&
                moment(this.config.endDate, this.config.format).isValid()
            ) {
                this.config.startDate = moment(
                    this.config.startDate,
                    this.config.format
                )
                    .middleOfDay()
                    .locale(this.config.locale);
                this.config.endDate = moment(
                    this.config.endDate,
                    this.config.format
                )
                    .middleOfDay()
                    .locale(this.config.locale);
                if (this.config.startDate.isAfter(this.config.endDate, 'day')) {
                    swap = this.config.startDate.clone();
                    this.config.startDate = this.config.endDate.clone();
                    this.config.endDate = swap.clone();
                    swap = null;
                }
            } else {
                this.config.startDate = moment()
                    .middleOfDay()
                    .locale(this.config.locale);
                this.config.endDate = moment()
                    .middleOfDay()
                    .locale(this.config.locale);
            }
            this.globals.currentDate = moment(
                this.config.startDate,
                this.config.format
            );
            // validate min & max dates
            if (
                this.config.minDate !== null &&
                moment(this.config.minDate, this.config.format).isValid()
            ) {
                this.config.minDate = moment(
                    this.config.minDate,
                    this.config.format
                ).middleOfDay();
            } else {
                this.config.minDate = null;
            }
            if (
                this.config.maxDate !== null &&
                moment(this.config.maxDate, this.config.format).isValid()
            ) {
                this.config.maxDate = moment(
                    this.config.maxDate,
                    this.config.format
                ).middleOfDay();
            } else {
                this.config.maxDate = null;
            }
            if (
                this.config.minDate !== null &&
                this.config.maxDate !== null &&
                this.config.minDate.isAfter(this.config.maxDate, 'day')
            ) {
                swap = this.config.minDate.clone();
                this.config.minDate = this.config.maxDate.clone();
                this.config.maxDate = swap.clone();
                swap = null;
            }

            // validate start and end dates according to min and max dates
            if (
                this.config.minDate !== null &&
                this.config.startDate !== null &&
                this.config.minDate.isAfter(this.config.startDate, 'day')
            ) {
                this.config.startDate = this.config.minDate.clone();
            }
            if (
                this.config.minDate !== null &&
                this.config.endDate !== null &&
                this.config.minDate.isAfter(this.config.endDate, 'day')
            ) {
                this.config.endDate = this.config.minDate.clone();
            }

            if (
                this.config.maxDate !== null &&
                this.config.startDate !== null &&
                this.config.maxDate.isBefore(this.config.startDate, 'day')
            ) {
                this.config.startDate = this.config.maxDate.clone();
            }
            if (
                this.config.maxDate !== null &&
                this.config.endDate !== null &&
                this.config.maxDate.isBefore(this.config.endDate, 'day')
            ) {
                this.config.endDate = this.config.maxDate.clone();
            }
            
            if (
                this.checkRangeContinuity() === false ||
                (this.config.disableOnlyStart == true &&
                    this.isDisabled(
                        this.config.startDate,
                        this.config.isHotelBooking
                    )) ||
                (this.config.singleDate === false &&
                    this.config.disableOnlyEnd &&
                    this.isDisabled(
                        this.config.endDate,
                        this.config.isHotelBooking
                    )) ||
                (this.config.startEmpty &&
                    this.globals.firstValueSelected == false)
            ) {
                this.clearInput();
            }
        },
        /**
         * sets config variables and relations between them,
         * for example "inline" property converts the input to hidden input,
         * applies default date from input to plugin and vice versa .. etc.
         *
         * @return void
         */
        applyConfig: function () {
            // set target element to be updated
            if (this.config.target === null) this.config.target = this.$elem;

            // disable dobcalendar if inline
            if (this.config.inline === true) this.config.DOBCalendar = false;

            // switch to singledate when dobcalendar is true
            if (this.config.DOBCalendar == true) this.config.singleDate = true;

            // fix dates
            ['startDate', 'endDate', 'minDate', 'maxDate'].forEach(function (
                key
            ) {
                this.config[key] = this.fixDateTime(this.config[key]);
            },
            this);

            this.config.ranges.map(function (range) {
                range.startDate = this.fixDateTime(range.startDate);
                range.endDate = this.fixDateTime(range.endDate);
                return range;
            }, this);

            // create container relative to environment and settings
            if (this.globals.isMobile === false) {
                if (this.config.inline === true) {
                    this.container = this.$elem
                        .wrapAll(
                            "<div class='caleran-container caleran-inline' tabindex='1' onclick=''></div>"
                        )
                        .parent();
                    this.input = $(
                        "<div class='caleran-input'></div>"
                    ).appendTo(this.container);
                    this.elem.type = 'hidden';
                    this.config.showButtons = false;
                    this.setViewport();
                } else {
                    this.container = $(
                        "<div class='caleran-container caleran-popup' style='display: none;' onclick=''><div class='caleran-box-arrow-top'></div></div>"
                    ).appendTo(this.config.container);
                    this.input = $(
                        "<div class='caleran-input'></div>"
                    ).appendTo(this.container);
                    if (this.config.showButtons) {
                        this.globals.delayInputUpdate = true;
                        this.config.autoCloseOnSelect = false;
                    }
                }
                if (this.config.rangeOrientation === 'horizontal') {
                    this.input.css(
                        'width',
                        this.config.calendarCount *
                            this.config.oneCalendarWidth +
                            'px'
                    );
                } else {
                    this.input.css(
                        'width',
                        this.config.calendarCount *
                            this.config.oneCalendarWidth +
                            this.config.verticalRangeWidth +
                            'px'
                    );
                }
            } else {
                if (this.config.inline === true) {
                    this.container = this.$elem
                        .wrapAll(
                            "<div class='caleran-container-mobile caleran-inline' tabindex='1' onclick=''></div>"
                        )
                        .parent();
                    this.input = $(
                        "<div class='caleran-input'></div>"
                    ).appendTo(this.container);
                    this.elem.type = 'hidden';
                    this.config.showButtons = false;
                } else {
                    this.container = $(
                        "<div class='caleran-container-mobile' onclick=''></div>"
                    ).appendTo(this.config.container);
                    this.input = $(
                        "<div class='caleran-input' style='display: none;'></div>"
                    ).appendTo(this.container);
                    if (this.config.showButtons) {
                        this.config.autoCloseOnSelect = false;
                    }
                    if (!this.config.autoCloseOnSelect)
                        this.globals.delayInputUpdate = true;
                }
                // disable the soft keyboard on mobile devices
                this.$elem.on('focus', function () {
                    $(this).blur();
                });
            }

            if (this.config.isHotelBooking) {
                this.container.addClass('caleran-hotel-style');
            }

            if (this.config.isRTL) {
                this.container.css('direction', 'rtl');
                this.container.addClass('caleran-rtl');
            }

            this.clearRangeSelection();
        },
        /**
         * Clears the selected range info
         */
        clearRangeSelection: function () {
            for (var range = 0; range < this.config.ranges.length; range++) {
                this.config.ranges[range].selected = false;
            }
            this.container.find('.caleran-range').each(function () {
                $(this).removeClass('caleran-range-selected');
            });
        },
        /**
         * Parse input from the source element's value and apply to config
         * @return void
         */
        fetchInputs: function () {
            var elValue = null;
            if (
                $.inArray(
                    this.config.target.get(0).tagName,
                    this.globals.valElements
                ) !== -1
            ) {
                elValue = this.config.target.val();
            } else {
                elValue = this.config.target.text();
            }
            if (
                this.config.singleDate === false &&
                elValue.indexOf(this.config.dateSeparator) > 0
            ) {
                var parts = elValue.split(this.config.dateSeparator);
                if (parts.length == 2) {
                    if (
                        moment(
                            parts[0],
                            this.config.format,
                            this.config.locale
                        ).isValid() &&
                        moment(
                            parts[1],
                            this.config.format,
                            this.config.locale
                        ).isValid()
                    ) {
                        this.config.startDate = moment(
                            parts[0],
                            this.config.format,
                            this.config.locale
                        ).middleOfDay();
                        this.config.endDate = moment(
                            parts[1],
                            this.config.format,
                            this.config.locale
                        ).middleOfDay();
                        this.globals.firstValueSelected = true;
                    }
                }
            } else if (this.config.singleDate === true) {
                var value = elValue;
                if (
                    value != '' &&
                    moment(
                        value,
                        this.config.format,
                        this.config.locale
                    ).isValid()
                ) {
                    this.config.startDate = moment(
                        value,
                        this.config.format,
                        this.config.locale
                    ).middleOfDay();
                    this.config.endDate = moment(
                        value,
                        this.config.format,
                        this.config.locale
                    ).middleOfDay();
                    this.globals.firstValueSelected = true;
                }
            } // clear input if startEmpty is defined
            if (this.config.startEmpty && !this.globals.firstValueSelected) {
                this.clearInput();
            }
            // validate inputs
            this.validateDates();
        },
        /**
         * Draws the plugin interface when needed
         * @return void
         */
        drawUserInterface: function () {
            this.drawHeader();
            this.calendars = this.input.find('.caleran-calendars').first();
            var nextCal = this.globals.currentDate.clone().middleOfDay();
            this.globals.disabledDays = {};
            for (
                var calendarIndex = 0;
                calendarIndex < this.config.calendarCount;
                calendarIndex++
            ) {
                this.drawCalendarOfMonth(nextCal);
                nextCal = nextCal.add(1, 'month');
            }
            // remove last border
            this.calendars
                .find('.caleran-calendar')
                .last()
                .addClass('no-border-right');
            this.drawArrows();
            this.drawFooter();
            if (
                (this.globals.isMobile === true ||
                    this.config.inline === false) &&
                this.globals.initComplete
            ) {
                this.setViewport();
            }
            if (this.globals.startSelected === false) {
                if (this.globals.initComplete) {
                    this.updateInput(false);
                } else {
                    var delayState = this.globals.delayInputUpdate;
                    this.globals.delayInputUpdate = false;
                    this.updateInput(false);
                    this.globals.delayInputUpdate = delayState;
                }
            }
            this.reDrawCells();
        },
        /**
         * Draws the overlay to prevent clickthroughs when an instance is open on mobile view
         * @return void
         */
        drawOverlay: function () {
            if (this.globals.isMobile == false) return;
            if ($('.caleran-overlay').length == 0) {
                this.overlay = $(
                    "<div class='caleran-overlay'></div>"
                ).appendTo('body');
                this.overlay.on('click.caleran tap.caleran', function () {
                    var visibleInstances = $(
                        'body > .caleran-container-mobile'
                    );
                    if (visibleInstances.length > 0) {
                        visibleInstances.each(function () {
                            if ($(this).css('display') != 'none') {
                                $(this).find('.caleran-cancel').click();
                            }
                        });
                    }
                });
            } else {
                this.overlay = $('.caleran-overlay').first();
            }
        },
        /**
         * Draws the header part of the plugin, which contains start and end date displays
         * @return void
         */
        drawHeader: function () {
            var headers =
                "<div class='caleran-header'>" +
                "<div class='caleran-header-start'>" +
                "<div class='caleran-header-start-day'></div>" +
                "<div class='caleran-header-start-date'></div>" +
                "<div class='caleran-header-start-weekday'></div>" +
                '</div>';
            if (this.config.singleDate === false) {
                headers +=
                    "<div class='caleran-header-separator'>" +
                    this.config.headerSeparator +
                    '</div>' +
                    "<div class='caleran-header-end'>" +
                    "<div class='caleran-header-end-day'></div>" +
                    "<div class='caleran-header-end-date'></div>" +
                    "<div class='caleran-header-end-weekday'></div>" +
                    '</div>';
            }
            headers += "</div><div class='caleran-calendars'></div>";
            this.input.append(headers);
            if (this.config.showHeader === false) {
                this.input.find('.caleran-header').hide();
            }
            this.globals.headerStartDay = this.input.find(
                '.caleran-header-start-day'
            );
            this.globals.headerStartDate = this.input.find(
                '.caleran-header-start-date'
            );
            this.globals.headerStartWeekday = this.input.find(
                '.caleran-header-start-weekday'
            );
            this.globals.headerEndDay = this.input.find(
                '.caleran-header-end-day'
            );
            this.globals.headerEndDate = this.input.find(
                '.caleran-header-end-date'
            );
            this.globals.headerEndWeekday = this.input.find(
                '.caleran-header-end-weekday'
            );
            this.updateHeader();
        },
        /**
         * Updates the start and end dates in the header
         * @return void
         */
        updateHeader: function () {
            if (this.config.startDate)
                this.config.startDate.locale(this.config.locale);
            if (this.config.endDate)
                this.config.endDate.locale(this.config.locale);
            if (
                this.config.startEmpty &&
                this.globals.firstValueSelected === false
            )
                return;
            if (this.config.startDate !== null) {
                this.globals.headerStartDay.text(
                    this.localizeNumbers(this.config.startDate.date())
                );
                if (this.globals.isMobile)
                    this.globals.headerStartDate.text(
                        this.config.startDate.format('MMM') +
                            ' ' +
                            this.localizeNumbers(this.config.startDate.year())
                    );
                else
                    this.globals.headerStartDate.text(
                        this.config.startDate.format('MMMM') +
                            ' ' +
                            this.localizeNumbers(this.config.startDate.year())
                    );
                this.globals.headerStartWeekday.text(
                    this.config.startDate.format('dddd')
                );
            } else {
                this.globals.headerStartDay.text('');
                this.globals.headerStartDate.text('');
                this.globals.headerStartWeekday.text('');
            }
            if (this.config.singleDate === false) {
                if (this.config.endDate !== null) {
                    this.globals.headerEndDay.text(
                        this.localizeNumbers(this.config.endDate.date())
                    );
                    if (this.globals.isMobile)
                        this.globals.headerEndDate.text(
                            this.config.endDate.format('MMM') +
                                ' ' +
                                this.localizeNumbers(this.config.endDate.year())
                        );
                    else
                        this.globals.headerEndDate.text(
                            this.config.endDate.format('MMMM') +
                                ' ' +
                                this.localizeNumbers(this.config.endDate.year())
                        );
                    this.globals.headerEndWeekday.text(
                        this.config.endDate.format('dddd')
                    );
                } else {
                    this.globals.headerEndDay.text('');
                    this.globals.headerEndDate.text('');
                    this.globals.headerEndWeekday.text('');
                }
            }
        },
        /**
         * checks for updateInput to be run or dismissed
         * @return {boolean} whether the input should be updated or not
         */
        isUpdateable: function () {
            var returnReasons = this.globals.delayInputUpdate;
            var clearReasons =
                this.config.startEmpty && !this.globals.firstValueSelected;
            clearReasons =
                clearReasons ||
                (this.config.singleDate === true &&
                    this.config.startDate === null);
            clearReasons =
                clearReasons ||
                (this.config.singleDate === false &&
                    (this.config.startDate === null ||
                        this.config.endDate === null));
            if (clearReasons) this.clearInput();
            if (clearReasons || returnReasons) return false;
            return true;
        },
        /**
         * Updates the connected input element value when the value is chosen
         * @return void
         */
        updateInput: function (withEvents) {
            if (this.config.startDate)
                this.config.startDate.locale(this.config.locale);
            if (this.config.endDate)
                this.config.endDate.locale(this.config.locale);
            if (!this.isUpdateable()) return;
            if (
                $.inArray(
                    this.config.target.get(0).tagName,
                    this.globals.valElements
                ) !== -1
            ) {
                if (this.config.singleDate === false) {
                    this.config.target.val(
                        this.config.startDate.format(this.config.format) +
                            this.config.dateSeparator +
                            this.config.endDate.format(this.config.format)
                    );
                } else {
                    this.config.target.val(
                        this.config.startDate.format(this.config.format)
                    );
                }
            } else {
                if (this.config.singleDate === false) {
                    this.config.target.text(
                        this.config.startDate.format(this.config.format) +
                            this.config.dateSeparator +
                            this.config.endDate.format(this.config.format)
                    );
                } else {
                    this.config.target.text(
                        this.config.startDate.format(this.config.format)
                    );
                }
            }
            if (this.globals.initComplete === true && withEvents === true) {
                this.config.onafterselect(
                    this,
                    this.config.startDate.clone(),
                    this.config.endDate.clone()
                );
                this.input.trigger('change');
            }
        },
        /**
         * Clears the input and prepares it for a new date range selection
         * @return void
         */
        clearInput: function (stayEmpty) {
            if (
                $.inArray(
                    this.config.target.get(0).tagName,
                    this.globals.valElements
                ) !== -1
            ) {
                if (this.config.singleDate === false)
                    this.config.target.val('');
                else this.config.target.val('');
            } else {
                if (this.config.singleDate === false)
                    this.config.target.text('');
                else this.config.target.text('');
            }
            this.config.startDate = null;
            this.config.endDate = null;

            if (stayEmpty) {
                this.config.startEmpty = true;
                this.globals.firstValueSelected = false;
            } else {
                if (this.config.startEmpty == true)
                    this.globals.firstValueSelected = false;
            }

            if (this.globals.initComplete) {
                this.updateHeader();
                var applyButton =
                    typeof this.footer == 'undefined'
                        ? []
                        : this.footer.find('.caleran-apply');
                if (applyButton.length > 0)
                    applyButton.attr('disabled', 'disabled');
            }
        },
        /**
         * Draws the arrows of the month switcher
         * @return void
         */
        drawArrows: function () {
            var hideLeftArrow =
                this.config.hideOutOfRange &&
                this.config.minDate &&
                this.globals.currentDate
                    .clone()
                    .add(-1, 'month')
                    .isBefore(this.config.minDate, 'month');
            var hideRightArrow =
                this.config.hideOutOfRange &&
                this.config.maxDate &&
                this.globals.currentDate
                    .clone()
                    .add(this.config.calendarCount, 'month')
                    .isAfter(this.config.maxDate, 'month');
            if (this.container.find('.caleran-title').length > 0) {
                if (this.globals.isMobile) {
                    if (!hideLeftArrow)
                        this.container
                            .find('.caleran-title')
                            .prepend(
                                "<div class='caleran-prev'>" +
                                    this.config.prevMonthIcon +
                                    '</div>'
                            );
                    if (!hideRightArrow)
                        this.container
                            .find('.caleran-title')
                            .append(
                                "<div class='caleran-next'>" +
                                    this.config.nextMonthIcon +
                                    '</div>'
                            );
                } else {
                    if (this.config.isRTL) {
                        if (!hideLeftArrow)
                            this.container
                                .find('.caleran-title')
                                .last()
                                .prepend(
                                    "<div class='caleran-prev'>" +
                                        this.config.prevMonthIcon +
                                        '</div>'
                                );
                        if (!hideRightArrow)
                            this.container
                                .find('.caleran-title')
                                .first()
                                .append(
                                    "<div class='caleran-next'>" +
                                        this.config.nextMonthIcon +
                                        '</div>'
                                );
                    } else {
                        if (!hideLeftArrow)
                            this.container
                                .find('.caleran-title')
                                .first()
                                .prepend(
                                    "<div class='caleran-prev'>" +
                                        this.config.prevMonthIcon +
                                        '</div>'
                                );
                        if (!hideRightArrow)
                            this.container
                                .find('.caleran-title')
                                .last()
                                .append(
                                    "<div class='caleran-next'>" +
                                        this.config.nextMonthIcon +
                                        '</div>'
                                );
                    }
                }
            }
        },
        /**
         * Draws a single calendar
         * @param  [momentobject] _month: The moment object containing the desired month, for example given "18/02/2017" as moment object draws the calendar of February 2017.
         * @return void
         */
        drawCalendarOfMonth: function (_month) {
            var calendarStart = moment(_month)
                .locale(this.config.locale)
                .startOf('month')
                .startOf('isoweek')
                .middleOfDay();
            var startOfWeek = calendarStart.day();
            if (startOfWeek == 1 && this.config.startOnMonday === false) {
                calendarStart.add(-1, 'days');
                startOfWeek = 0;
            } else if (
                startOfWeek === 0 &&
                this.config.startOnMonday === true
            ) {
                calendarStart.add(1, 'days');
                startOfWeek = 1;
            }
            if (calendarStart.isAfter(moment(_month).date(1)))
                calendarStart.add(-7, 'day');
            var calendarOutput =
                "<div class='caleran-calendar" +
                (this.config.showWeekNumbers
                    ? ' caleran-calendar-weeknumbers'
                    : '') +
                "' data-month='" +
                _month.month() +
                "'>";
            var boxCount = 0;
            var monthClass = '',
                yearClass = '';
            if (this.config.enableMonthSwitcher)
                monthClass = " class='caleran-month-switch'";
            if (this.config.enableYearSwitcher)
                yearClass = " class='caleran-year-switch'";

            calendarOutput +=
                "<div class='caleran-title'><b" +
                monthClass +
                '>' +
                _month.locale(this.config.locale).format('MMMM') +
                '</b>&nbsp;<span' +
                yearClass +
                '>' +
                this.localizeNumbers(_month.year()) +
                '</span></div>';
            calendarOutput += "<div class='caleran-days-container'>";
            var localeWeekdays = moment
                .localeData(this.config.locale)
                .weekdaysShort();

            if (this.config.showWeekNumbers)
                calendarOutput += "<div class='caleran-dayofweek'>&nbsp;</div>";
            for (var days = startOfWeek; days < startOfWeek + 7; days++) {
                calendarOutput +=
                    "<div class='caleran-dayofweek'>" +
                    localeWeekdays[days % 7] +
                    '</div>';
            }
            this.globals.disabledDays = Object.assign(
                this.globals.disabledDays,
                this.getDisabledDaysInCalendar(calendarStart)
            );
            while (boxCount < 42) {
                var cellDate = calendarStart.middleOfDay().unix();
                var cellStyle =
                    _month.month() == calendarStart.month()
                        ? 'caleran-day'
                        : 'caleran-disabled';
                if (boxCount % 7 === 0 && this.config.showWeekNumbers) {
                    calendarOutput +=
                        "<div class='caleran-weeknumber'><span>" +
                        calendarStart.format('ww') +
                        '</span></div>';
                }
                calendarOutput +=
                    "<div class='" +
                    cellStyle +
                    "' data-value='" +
                    cellDate +
                    "'><span>" +
                    this.localizeNumbers(calendarStart.date()) +
                    '</span>' +
                    this.getDayText(cellDate) +
                    '</div>';
                calendarStart.add(moment.duration({ days: 1 }));
                boxCount++;
            }
            calendarOutput += '</div>';
            calendarOutput += '</div>';
            this.calendars.append(calendarOutput);
        },
        getDayText: function (cellDate) {
            if (cellDate in this.config.dayText) {
                return (
                    "<span class='caleran-cell-text'>" +
                    this.config.dayText[cellDate] +
                    '</span>'
                );
            }
            return '';
        },
        getDisabledDaysInCalendar: function (startDate) {
            var output = {};
            // Step #1: Collect all disabled days
            for (var d = -7; d < 49; d++) {
                var day = moment(startDate).add(d, 'days').middleOfDay();
                var cellKey = day.unix();
                output[cellKey] = 0;
                if (this.config.disableDays(day) === true) {
                    output[cellKey] = 2;
                }
                for (
                    var rangeIndex = 0;
                    rangeIndex < this.config.disabledRanges.length;
                    rangeIndex++
                ) {
                    var range = this.config.disabledRanges[rangeIndex];
                    if (day.isBetween(range.start, range.end, 'day', '[]')) {
                        output[cellKey] = 2;
                    }
                }
            }
            // Step #2: Set range boundaries
            if (this.config.isHotelBooking) {
                var prev = null;
                for (var curr in output) {
                    if (prev !== null) {
                        switch (output[curr]) {
                            case 2:
                                switch (output[prev]) {
                                    case 0:
                                    case 3:
                                        output[curr] = 1;
                                        break;
                                }
                                break;
                            case 0:
                                switch (output[prev]) {
                                    case 1:
                                    case 2:
                                        output[curr] = 3;
                                        break;
                                }
                                break;
                        }
                    }
                    prev = curr;
                }
            }
            /*console.log(Object.entries(output).reduce((a,[k,v]) => {
                a[moment.unix(k).inspect()] = v;
                return a;
            },{}));*/
            return output;
        },
        /**
         * Draws the footer of the plugin, which contains range selector links
         * @return void
         */
        drawFooter: function () {
            if (
                this.config.singleDate === false &&
                this.config.showFooter === true
            ) {
                if (
                    this.config.rangeOrientation === 'horizontal' ||
                    this.globals.isMobile
                ) {
                    this.input.append("<div class='caleran-ranges'></div>");
                } else {
                    this.input.addClass('caleran-input-vertical-range');
                    this.input.wrapInner("<div class='caleran-left'></div>");
                    $(
                        "<div class='caleran-right' style='max-width: " +
                            this.config.verticalRangeWidth +
                            'px; min-width: ' +
                            this.config.verticalRangeWidth +
                            "px'><div class='caleran-ranges'></div></div>"
                    ).insertAfter(this.input.find('.caleran-left'));
                }
                var ranges = this.input.parent().find('.caleran-ranges');
                ranges.append(
                    "<span class='caleran-range-header-container'>" +
                        this.config.rangeIcon +
                        "<div class='caleran-range-header'>" +
                        this.config.rangeLabel +
                        '</div></span>'
                );
                for (
                    var range_id = 0;
                    range_id < this.config.ranges.length;
                    range_id++
                ) {
                    ranges.append(
                        "<div class='caleran-range" +
                            (this.config.ranges[range_id].selected
                                ? ' caleran-range-selected'
                                : '') +
                            "' data-id='" +
                            range_id +
                            "'>" +
                            this.config.ranges[range_id].title +
                            '</div>'
                    );
                }
            }
            if (this.globals.isMobile && !this.config.inline) {
                if (
                    this.config.singleDate === true ||
                    this.config.showFooter === false
                ) {
                    this.input.append("<div class='caleran-filler'></div>");
                }
            }
            if (
                (this.globals.isMobile && !this.config.inline) ||
                (!this.globals.isMobile &&
                    !this.config.inline &&
                    this.config.showButtons)
            ) {
                if (
                    this.config.rangeOrientation === 'horizontal' ||
                    this.globals.isMobile
                ) {
                    this.input.append("<div class='caleran-footer'></div>");
                } else {
                    this.input
                        .find('.caleran-right')
                        .append("<div class='caleran-footer'></div>");
                }
                this.footer = this.input.find('.caleran-footer');
                this.footer.append(
                    "<button type='button' class='caleran-cancel'>" +
                        this.config.cancelLabel +
                        '</button>'
                );
                this.footer.append(
                    "<button type='button' class='caleran-apply'>" +
                        this.config.applyLabel +
                        '</button>'
                );
                if (
                    this.globals.firstValueSelected === false &&
                    this.config.startEmpty == true
                ) {
                    this.footer
                        .find('.caleran-apply')
                        .attr('disabled', 'disabled');
                }
                if (
                    this.globals.isMobile &&
                    this.globals.endSelected === false
                ) {
                    this.footer
                        .find('.caleran-apply')
                        .attr('disabled', 'disabled');
                }
            }
        },
        /**
         * Draws next month's calendar (just calls this.reDrawCalendars with an 1 month incremented currentDate)
         * Used in the next arrow click event
         *
         * @return void
         */
        drawNextMonth: function (event) {
            event = event || window.event;

            if (
                this.config.hideOutOfRange == true &&
                this.config.maxDate &&
                this.globals.currentDate
                    .clone()
                    .add(this.config.calendarCount, 'month')
                    .isAfter(this.config.maxDate, 'month')
            ) {
                return false;
            }

            if (this.globals.swipeTimeout === null) {
                var that = this;
                this.globals.swipeTimeout = setTimeout(function () {
                    if (
                        that.config.onbeforemonthchange(
                            that,
                            that.globals.currentDate.clone().startOfMonth(),
                            'next'
                        ) === true
                    ) {
                        var buffer = that.calendars.get(0).scrollTop;
                        that.globals.currentDate.middleOfDay().add(1, 'month');
                        that.reDrawCalendars();
                        that.calendars.get(0).scrollTop = buffer;
                        that.config.onaftermonthchange(
                            that,
                            that.globals.currentDate.clone().startOfMonth()
                        );
                    }
                    that.globals.swipeTimeout = null;
                }, 100);
            }
            this.stopBubbling(event);
        },
        /**
         * Draws previous month's calendar (just calls this.reDrawCalendars with an 1 month decremented currentDate)
         * Used in the prev arrow click event
         *
         * @return void
         */
        drawPrevMonth: function (event) {
            event = event || window.event;

            if (
                this.config.hideOutOfRange == true &&
                this.config.minDate &&
                this.globals.currentDate
                    .clone()
                    .add(-1, 'month')
                    .isBefore(this.config.minDate, 'month')
            ) {
                return false;
            }

            if (this.globals.swipeTimeout === null) {
                var that = this;
                this.globals.swipeTimeout = setTimeout(function () {
                    if (
                        that.config.onbeforemonthchange(
                            that,
                            that.globals.currentDate.clone().startOfMonth(),
                            'prev'
                        ) === true
                    ) {
                        var buffer = that.calendars.get(0).scrollTop;
                        that.globals.currentDate
                            .middleOfDay()
                            .subtract(1, 'month');
                        that.reDrawCalendars();
                        that.calendars.get(0).scrollTop = buffer;
                        that.config.onaftermonthchange(
                            that,
                            that.globals.currentDate.clone().startOfMonth()
                        );
                    }
                    that.globals.swipeTimeout = null;
                }, 100);
            }
            this.stopBubbling(event);
        },
        /**
         * Day cell click event handler
         * @param  [eventobject] e : The event object which contains the clicked cell in e.target property, which enables us to select dates
         * @return void
         */
        cellClicked: function (e) {
            e = e || window.event;
            e.target = e.target || e.srcElement;

            if ($(e.target).hasClass('caleran-day') === false)
                e.target = $(e.target).closest('.caleran-day').get(0);
            var cell = $(e.target).data('value');
            var selectedMoment = moment.unix(cell).middleOfDay();
            if (this.config.validateClick(selectedMoment) == false)
                return false;
            if (this.config.singleDate === false) {
                if (this.globals.startSelected === false) {
                    if (this.config.startDate !== null)
                        this.globals.startDateBackup =
                            this.config.startDate.clone();
                    this.config.startDate = selectedMoment;
                    this.config.endDate = null;
                    this.globals.startSelected = true;
                    this.globals.endSelected = false;
                    var applyButton =
                        typeof this.footer == 'undefined'
                            ? []
                            : this.footer.find('.caleran-apply');
                    if (applyButton.length > 0)
                        applyButton.attr('disabled', 'disabled');
                    this.config.onfirstselect(
                        this,
                        this.config.startDate.clone()
                    );
                } else {
                    if (selectedMoment.isBefore(this.config.startDate)) {
                        var start = this.config.startDate.clone();
                        this.config.startDate = selectedMoment.clone();
                        selectedMoment = start;
                    }
                    if (
                        selectedMoment.diff(this.config.startDate, 'day') <
                        this.config.minSelectedDays
                    ) {
                        this.globals.startSelected = false;
                        this.fetchInputs();
                    } else {
                        this.globals.startDateBackup = null;
                        this.config.endDate = selectedMoment;
                        this.globals.endSelected = true;
                        this.globals.startSelected = false;
                        this.globals.hoverDate = null;
                        if (
                            this.config.onbeforeselect(
                                this,
                                this.config.startDate.clone(),
                                this.config.endDate.clone()
                            ) === true &&
                            this.checkRangeContinuity() === true
                        ) {
                            this.globals.firstValueSelected = true;
                            this.clearRangeSelection();
                            this.updateInput(true);
                        } else this.fetchInputs();
                        if (
                            this.config.autoCloseOnSelect &&
                            this.config.inline === false
                        ) {
                            this.hideDropdown(e);
                        } else {
                            if (
                                typeof this.footer != 'undefined' &&
                                this.config.endDate != null
                            ) {
                                this.footer
                                    .find('.caleran-apply')
                                    .removeAttr('disabled');
                            }
                        }
                    }
                }
            } else {
                this.config.startDate = selectedMoment;
                this.config.endDate = selectedMoment;
                this.globals.endSelected = true;
                this.globals.startSelected = false;
                this.globals.hoverDate = null;
                if (
                    this.config.onbeforeselect(
                        this,
                        this.config.startDate.clone(),
                        this.config.endDate.clone()
                    ) === true
                ) {
                    this.globals.firstValueSelected = true;
                    this.clearRangeSelection();
                    this.updateInput(true);
                } else {
                    this.fetchInputs();
                }
                if (
                    this.config.autoCloseOnSelect &&
                    this.config.inline === false
                ) {
                    this.hideDropdown(e);
                } else {
                    if (
                        typeof this.footer != 'undefined' &&
                        this.config.endDate != null
                    ) {
                        this.footer
                            .find('.caleran-apply')
                            .removeAttr('disabled');
                    }
                }
            }
            this.reDrawCells();
            this.updateHeader();
            this.stopBubbling(e);
            return false;
        },
        /**
         * Checks if the defined range is continous (doesn't include disabled ranges or disabled days by callback)
         * @return boolean is continuous or not
         */
        checkRangeContinuity: function () {
            var daysInRange = this.config.endDate.diff(
                this.config.startDate,
                'days'
            );
            var startDate = moment(this.config.startDate).middleOfDay();
            
            // Calculate uninitialized disabled days object
            if(Object.keys(this.config.disableDays).length === 0 ){
                this.globals.disabledDays = Object.assign(
                    this.globals.disabledDays,
                    this.getDisabledDaysInCalendar(startDate)
                );
            }
            
            /**
             * variables affecting this:
             * -------------------------
             * config.isHotelBooking
             * config.continuous
             * config.disableOnlyStart
             * config.disableOnlyEnd
             *
             * if disableOnlyStart or disableOnlyEnd is active, skip continuousity.
             * else check continuousity
             *
             */
            if (this.config.disableOnlyStart == true) {
                // if disabling only start is active, just start date check will be sufficient
                // false means continuousity is preserved, true means the date is disabled
                return (
                    this.isDisabled(
                        this.config.startDate,
                        this.config.isHotelBooking
                    ) === false
                );
            } else if (this.config.disableOnlyEnd == true) {
                // if disabling only end is active, just end date check will be sufficient
                // false means continuousity is preserved, true means the date is disabled
                return (
                    this.isDisabled(
                        this.config.endDate,
                        this.config.isHotelBooking
                    ) === false
                );
            } else {
                if (this.config.continuous) {
                    var startDateUnix = startDate.middleOfDay().unix();
                    //check if startdate can be a start date
                    if (
                        // hotel style active, get only disabled.
                        // hotel style passing, get disabled.
                        this.isDisabled(startDateUnix, false) == true &&
                        // if hotel style is active check if start date is a range's start date
                        (this.config.isHotelBooking
                            ? this.getDisabledLevel(startDateUnix) === 1
                            : true)
                    ) {
                        return false;
                    }

                    if (startDate.isSame(this.config.endDate, 'day') == false) {
                        startDate.middleOfDay().add(1, 'days');
                        for (var i = 0; i <= daysInRange - 2; i++) {
                            startDateUnix = startDate.middleOfDay().unix();
                            if (this.getDisabledLevel(startDateUnix) !== 0)
                                return false;
                            startDate.add(1, 'days');
                        }
                    }

                    // check if enddate can be an end date
                    startDateUnix = startDate.middleOfDay().unix();
                    if (
                        // hotel style active, get only disabled.
                        // hotel style passing, get disabled.
                        this.isDisabled(startDateUnix, false) == true &&
                        // if hotel style is active check if end date is a range's end date
                        (this.config.isHotelBooking
                            ? this.getDisabledLevel(startDateUnix) === 3
                            : true)
                    ) {
                        return false;
                    }
                }
            }
            return true;
        },
        /**
         * Checks if given moment value is disabled for that calendar on first draw
         * @param [moment] day : The day to be checked
         * @return {boolean} If the day is disabled or not
         */
        isDisabledOnDraw: function (day) {
            var mday = moment(day).middleOfDay();
            if (this.config.disableDays(mday) === true) {
                return true;
            }
            for (
                var rangeIndex = 0;
                rangeIndex < this.config.disabledRanges.length;
                rangeIndex++
            ) {
                var range = this.config.disabledRanges[rangeIndex];
                if (mday.isBetween(range.start, range.end, 'day', '[]')) {
                    return true;
                }
            }
        },
        /**
         * Checks if given moment value is disabled for that calendar from the disabled array
         * @param [moment] day : The day to be checked
         * @return {boolean} If the day is disabled or not
         */
        isDisabled: function (day, hotelStyle) {
            if (undefined === hotelStyle) hotelStyle = false;
            if (
                this.config.disableOnlyStart == true &&
                this.globals.startSelected == true
            ) {
                return false;
            } else if (
                this.config.disableOnlyEnd == true &&
                this.globals.startSelected == false
            ) {
                return false;
            } else {
                if (typeof day == 'object' && day !== null) {
                    day = day.middleOfDay().unix();
                }
                if (hotelStyle && this.config.isHotelBooking) {
                    return this.globals.disabledDays[day] === 2;
                }
                return this.globals.disabledDays[day] !== 0;
            }
        },
        /**
         * Gets the disabledDays record for the given day
         * @param integer|object day
         */
        getDisabledLevel: function (day) {
            if (typeof day == 'object' && day !== null) {
                day = day.middleOfDay().unix();
            }
            return this.globals.disabledDays[day];
        },
        /**
         * Event triggered when a day is hovered
         * @param  [eventobject] e : The event object which contains the hovered cell in e.target property, which enables us to style hovered dates
         * @return void
         */
        cellHovered: function (e) {
            e = e || window.event;
            e.target = e.target || e.srcElement;
            if ($(e.target).hasClass('caleran-day') === false)
                e.target = $(e.target).closest('.caleran-day').get(0);
            var cell = $(e.target).data('value');
            this.globals.hoverDate = moment.unix(cell).middleOfDay();
            this.globals.keyboardHoverDate = null;
            if (this.globals.startSelected === true) this.reDrawCells();
            this.stopBubbling(e);
        },
        /**
         * Just a calendar refresher to be used with the new variables, updating the calendar view
         * @return void
         */
        reDrawCalendars: function () {
            //this.requestAnimFrame($.proxy(function(){
            this.input.empty();
            this.drawUserInterface();
            this.container.focus();
            /*if (this.globals.lastScrollY !== 0) {
                window.scrollTo(this.globals.lastScrollX, this.globals.lastScrollY);
            }*/
            //}, this));
        },
        /**
         * month switcher builder and processor method
         * @return void
         */
        monthSwitchClicked: function () {
            if (this.calendars.find('.caleran-month-selector').length > 0)
                return;
            var that = this;
            this.calendars.get(0).scrollTop = 0;
            var monthSelector = $(
                "<div class='caleran-month-selector'></div>"
            ).appendTo(this.calendars);
            var currentMonth = this.globals.currentDate.get('month');
            var currentDate = this.globals.currentDate.clone();
            for (var m = 0; m < 12; m++) {
                currentDate.month(m);
                if (this.config.hideOutOfRange) {
                    if (
                        currentDate.isBefore(this.config.minDate, 'month') ||
                        currentDate.isAfter(this.config.maxDate, 'month')
                    ) {
                        monthSelector.append(
                            "<div class='caleran-ms-month-disabled'>&nbsp;</div>"
                        );
                    } else {
                        monthSelector.append(
                            "<div class='caleran-ms-month" +
                                (currentMonth == m ? ' current' : '') +
                                "' data-month='" +
                                m +
                                "'>" +
                                (this.config.numericMonthSelector
                                    ? m + 1
                                    : moment({ day: 15, hour: 12, month: m })
                                          .locale(this.config.locale)
                                          .format(
                                              this.config.monthSwitcherFormat
                                          )) +
                                '</div>'
                        );
                    }
                } else {
                    monthSelector.append(
                        "<div class='caleran-ms-month" +
                            (currentMonth == m ? ' current' : '') +
                            "' data-month='" +
                            m +
                            "'>" +
                            (this.config.numericMonthSelector
                                ? m + 1
                                : moment({ day: 15, hour: 12, month: m })
                                      .locale(this.config.locale)
                                      .format(
                                          this.config.monthSwitcherFormat
                                      )) +
                            '</div>'
                    );
                }
            }
            monthSelector.css('display', 'block');
            this.optimizeFontSize(monthSelector.find('.caleran-ms-month'));
            monthSelector
                .find('.caleran-ms-month')
                .off('click')
                .on('click', function (event) {
                    that.globals.currentDate.month($(this).data('month'));
                    that.config.onaftermonthchange(
                        that,
                        that.globals.currentDate.clone().startOfMonth()
                    );
                    that.reDrawCalendars();
                    that.calendars.find('.caleran-month-selector').remove();
                    that.stopBubbling(event);
                });
        },
        /**
         * Draws the year switch on year switcher page change or on year switch open
         *
         * @param container the container instance to append on
         * @param currentYear the current displayed year
         *
         * @return void
         */
        drawYearSwitch: function (container, currentYear) {
            container.data('year', currentYear);
            container.empty();
            var prevYear = currentYear - 6;
            var nextYear = currentYear + 6;
            if (this.config.hideOutOfRange) {
                if (
                    moment(prevYear + '-01-01').isBefore(
                        this.config.minDate,
                        'year'
                    ) ||
                    moment(prevYear + '-12-31').isAfter(
                        this.config.maxDate,
                        'year'
                    )
                ) {
                    container.append(
                        "<div class='caleran-ys-year-disabled'>&nbsp;</div>"
                    );
                } else {
                    container.append(
                        "<div class='caleran-ys-year-prev'><i class='fa fa-angle-double-left'></i></div>"
                    );
                }
            } else {
                container.append(
                    "<div class='caleran-ys-year-prev'><i class='fa fa-angle-double-left'></i></div>"
                );
            }

            for (var year = currentYear - 6; year < currentYear + 7; year++) {
                if (this.config.hideOutOfRange) {
                    if (
                        moment(year + '-06-01').isBefore(
                            this.config.minDate,
                            'year'
                        ) ||
                        moment(year + '-06-01').isAfter(
                            this.config.maxDate,
                            'year'
                        )
                    ) {
                        container.append(
                            "<div class='caleran-ys-year-disabled'>&nbsp;</div>"
                        );
                    } else {
                        container.append(
                            "<div class='caleran-ys-year" +
                                (currentYear == year ? ' current' : '') +
                                "' data-year='" +
                                year +
                                "'>" +
                                this.localizeNumbers(year) +
                                '</div>'
                        );
                    }
                } else {
                    container.append(
                        "<div class='caleran-ys-year" +
                            (currentYear == year ? ' current' : '') +
                            "' data-year='" +
                            year +
                            "'>" +
                            this.localizeNumbers(year) +
                            '</div>'
                    );
                }
            }

            if (this.config.hideOutOfRange) {
                if (
                    moment(nextYear + '-01-01').isBefore(
                        this.config.minDate,
                        'year'
                    ) ||
                    moment(nextYear + '-12-31').isAfter(
                        this.config.maxDate,
                        'year'
                    )
                ) {
                    container.append(
                        "<div class='caleran-ys-year-disabled'>&nbsp;</div>"
                    );
                } else {
                    container.append(
                        "<div class='caleran-ys-year-next'><i class='fa fa-angle-double-right'></i></div>"
                    );
                }
            } else {
                container.append(
                    "<div class='caleran-ys-year-next'><i class='fa fa-angle-double-right'></i></div>"
                );
            }
        },
        /**
         * year switcher builder and processor method
         * @return void
         */
        yearSwitchClicked: function () {
            if (this.calendars.find('.caleran-year-selector').length > 0)
                return;
            var that = this;
            this.calendars.get(0).scrollTop = 0;
            var yearSelector = $(
                "<div class='caleran-year-selector'></div>"
            ).appendTo(this.calendars);
            var currentYear = this.globals.currentDate.get('year');
            this.drawYearSwitch(yearSelector, currentYear);
            yearSelector.css('display', 'block');
            this.optimizeFontSize(yearSelector.find('.caleran-ys-year'));
            $(document)
                .off('click.caleranys')
                .on('click.caleranys', '.caleran-ys-year', function (event) {
                    that.globals.currentDate.year($(this).data('year'));
                    that.config.onafteryearchange(
                        that,
                        that.globals.currentDate.clone().startOf('year')
                    );
                    that.reDrawCalendars();
                    that.calendars.find('.caleran-year-selector').remove();
                    if (that.config.DOBCalendar == true) {
                        that.calendars
                            .find('.caleran-calendar')
                            .first()
                            .find('.caleran-month-switch')
                            .click();
                    }
                    that.stopBubbling(event);
                });
            $(document)
                .off('click.caleranysprev')
                .on(
                    'click.caleranysprev',
                    '.caleran-ys-year-prev',
                    function (event) {
                        var currentYear = yearSelector.data('year') - 13;
                        yearSelector.data('year', currentYear);
                        that.drawYearSwitch(yearSelector, currentYear);
                        that.stopBubbling(event);
                    }
                );
            $(document)
                .off('click.caleranysnext')
                .on(
                    'click.caleranysnext',
                    '.caleran-ys-year-next',
                    function (event) {
                        var currentYear = yearSelector.data('year') + 13;
                        yearSelector.data('year', currentYear);
                        that.drawYearSwitch(yearSelector, currentYear);
                        that.stopBubbling(event);
                    }
                );
        },
        /**
         * Lowers the font size until all the text fits in the element
         */
        optimizeFontSize: function (element) {
            element.each(function (index, elem) {
                elem = $(elem);
                elem.wrapInner("<span class='adjust-subject'></span>").prepend(
                    "<span class='font-adjuster'>i</span>"
                );
                var adjustSubject = elem.find('.adjust-subject');
                var fontAdjuster = elem.find('.font-adjuster');
                if (
                    adjustSubject.innerHeight() === fontAdjuster.innerHeight()
                ) {
                    fontAdjuster.remove();
                    adjustSubject.contents().unwrap();
                } else {
                    var loopCount = 0;
                    while (
                        adjustSubject.innerHeight() !==
                            fontAdjuster.innerHeight() &&
                        loopCount < 16
                    ) {
                        var startSize = 0;
                        if (typeof window.getComputedStyle !== 'undefined') {
                            startSize = parseFloat(
                                window
                                    .getComputedStyle(fontAdjuster.get(0), null)
                                    .getPropertyValue('font-size')
                            );
                        } else {
                            startSize = parseFloat(
                                fontAdjuster.css('font-size')
                            );
                        }
                        adjustSubject
                            .parent()
                            .css('font-size', startSize - 1 + 'px');
                        fontAdjuster.css('font-size', startSize - 1 + 'px');
                        if (startSize < 2) break;
                        loopCount++;
                    }
                    fontAdjuster.remove();
                    adjustSubject.contents().unwrap();
                }
            });
        },
        /**
         * Shows the caleran dropdown
         * @return void
         */
        showDropdown: function (e) {
            var event =
                e ||
                window.event ||
                jQuery.Event('click', { target: this.elem });
            var eventTarget = event.target || event.srcElement;
            if (
                (!this.globals.isMobile &&
                    this.container.css('display') == 'none') ||
                (this.globals.isMobile && this.input.css('display') == 'none')
            ) {
                if (eventTarget !== this.elem) {
                    this.globals.dontHideOnce = true;
                    this.globals.initiator = eventTarget;
                }
                this.fetchInputs();
                this.reDrawCalendars();
                this.globals.startDateInitial = this.config.startDate;
                this.globals.endDateInitial = this.config.endDate;
                this.config.onbeforeshow(this);
                if (this.globals.isMobile) {
                    this.input.css({
                        display: 'flex',
                    });
                    this.overlay.show();
                    $('body').addClass('caleran-open');
                } else {
                    this.container.css({
                        display: 'block',
                    });
                }
                this.setViewport();
                if (this.config.DOBCalendar == true) {
                    this.calendars
                        .find('.caleran-calendar')
                        .first()
                        .find('.caleran-year-switch')
                        .click();
                }
                this.config.onaftershow(this);
            }
        },
        /**
         * Hides the caleran dropdown
         * @return void
         */
        hideDropdown: function (e) {
            var event =
                e || window.event || jQuery.Event('click', { target: 'body' });
            var eventTarget = event.target || event.srcElement;
            if (this.globals.initiator === eventTarget) return;
            if (
                this.config.inline === false &&
                ((!this.globals.isMobile &&
                    this.container.css('display') !== 'none') ||
                    (this.globals.isMobile &&
                        this.input.css('display') !== 'none'))
            ) {
                this.config.onbeforehide(this);
                if (this.globals.isMobile) {
                    this.input.css({
                        display: 'none',
                    });
                    this.overlay.hide();
                    $('body').removeClass('caleran-open');
                } else {
                    this.container.css({
                        display: 'none',
                    });
                }
                this.globals.hoverDate = null;
                if (this.globals.startDateBackup !== null) {
                    this.config.startDate = this.globals.startDateBackup;
                    this.globals.startSelected = false;
                }
                this.config.onafterhide(this);
            }
        },
        /**
         * When only a cell style update is needed, this function is used. This gives us the possibility to change styles without re-drawing the calendars.
         * @return void
         */
        reDrawCells: function () {
            var that = this;
            var startDateUnix =
                this.config.startDate != null
                    ? this.config.startDate.middleOfDay().unix()
                    : null;
            var endDateUnix =
                this.config.endDate != null
                    ? this.config.endDate.middleOfDay().unix()
                    : null;
            var minDateUnix =
                this.config.minDate != null
                    ? this.config.minDate.middleOfDay().unix()
                    : null;
            var maxDateUnix =
                this.config.maxDate != null
                    ? this.config.maxDate.middleOfDay().unix()
                    : null;
            var hoverDateUnix =
                this.globals.hoverDate != null
                    ? this.globals.hoverDate.middleOfDay().unix()
                    : null;
            var keyboardHoverDateUnix =
                this.globals.keyboardHoverDate != null
                    ? this.globals.keyboardHoverDate.middleOfDay().unix()
                    : null;
            var currentDateUnix = moment().middleOfDay().unix();
            this.lastHoverStatus = false;
            for (var c = 0; c < this.config.calendarCount; c++) {
                var calendar = this.calendars.find('.caleran-calendar').eq(c);
                var cells = calendar
                    .find('.caleran-days-container > div')
                    .not('.caleran-dayofweek, .caleran-weeknumber');
                var currentMonth = calendar.data('month');
                for (var i = 0; i < cells.length; i++) {
                    var cell = $(cells[i]);
                    var cellDate = parseInt(cell.attr('data-value'));
                    var cellMoment = moment
                        .unix(cellDate)
                        .middleOfDay()
                        .locale(that.config.locale);
                    var cellStyle = 'caleran-day';
                    var cellDay = cellMoment.day();
                    // is weekend day (saturday or sunday) check
                    if (cellDay == 6 || cellDay === 0)
                        cellStyle += ' caleran-weekend';
                    // is today check
                    if (cellDate === currentDateUnix)
                        cellStyle += ' caleran-today';
                    cellStyle = this.addDisabledStyles(
                        cell,
                        cellMoment,
                        cellDate,
                        cellStyle,
                        minDateUnix,
                        maxDateUnix,
                        currentMonth
                    );
                    cellStyle = this.addSelectedStyles(
                        cellDate,
                        cellStyle,
                        startDateUnix,
                        endDateUnix,
                        minDateUnix,
                        maxDateUnix
                    );
                    cellStyle = this.addHoverStyles(
                        cell,
                        cellDate,
                        cellStyle,
                        this,
                        startDateUnix,
                        hoverDateUnix,
                        keyboardHoverDateUnix
                    );
                    cell.attr('class', cellStyle);
                }
            }
            this.attachEvents();
            this.config.ondraw(this);
        },
        /**
         * returns calculated selected state style of the cell
         * @param cellMoment current cell's day
         * @param cellStyle current cell's already calculated style
         * @return appended style of the cell
         */
        addSelectedStyles: function (
            cellDateUnix,
            cellStyle,
            startDateUnix,
            endDateUnix,
            minDateUnix,
            maxDateUnix
        ) {
            var that = this;
            var cellStyleOriginal = cellStyle;

            if (
                that.config.startEmpty === false ||
                that.globals.firstValueSelected
            ) {
                // is the start of the range check
                if (
                    that.config.singleDate === false &&
                    startDateUnix !== null &&
                    startDateUnix === cellDateUnix
                )
                    cellStyle += ' caleran-start';
                // is the end of the range check
                if (
                    that.config.singleDate === false &&
                    endDateUnix !== null &&
                    endDateUnix === cellDateUnix
                )
                    cellStyle += ' caleran-end';
                // is between the start and the end range check
                if (
                    that.config.singleDate === false &&
                    startDateUnix !== null &&
                    endDateUnix !== null &&
                    cellDateUnix <= endDateUnix &&
                    cellDateUnix >= startDateUnix
                )
                    cellStyle += ' caleran-selected';
                // is the selected date of single date picker
                if (
                    that.config.singleDate === true &&
                    startDateUnix !== null &&
                    startDateUnix === cellDateUnix
                )
                    cellStyle += ' caleran-selected caleran-start caleran-end';
            }

            //if (cellStyleOriginal != cellStyle) { cellStyle = cellStyle.replace("caleran-disabled", "caleran-day"); }

            return cellStyle;
        },
        /**
         * returns calculated hovered state style of the cell
         * @param cellMoment current cell's day
         * @param cellStyle current cell's already calculated style
         * @return appended style of the cell
         */
        addHoverStyles: function (
            cell,
            cellDateUnix,
            cellStyle,
            ref,
            startDateUnix,
            hoverDateUnix,
            keyboardHoverDateUnix
        ) {
            // hovered date check
            var that = this;
            cellStyle
                .replace('caleran-hovered', '')
                .replace('caleran-hovered-last', '')
                .replace('caleran-hovered-first', '');
            if (
                that.globals.startSelected === true &&
                that.globals.endSelected === false &&
                hoverDateUnix !== null
            ) {
                if (
                    (cellDateUnix >= hoverDateUnix &&
                        cellDateUnix <= startDateUnix) ||
                    (cellDateUnix <= hoverDateUnix &&
                        cellDateUnix >= startDateUnix)
                ) {
                    cellStyle += ' caleran-hovered';
                }
            }
            if (
                that.config.enableKeyboard == true &&
                keyboardHoverDateUnix !== null
            ) {
                if (that.globals.startSelected === false) {
                    if (keyboardHoverDateUnix == cellDateUnix) {
                        cellStyle += ' caleran-hovered';
                    }
                } else {
                    if (
                        (cellDateUnix <= startDateUnix &&
                            cellDateUnix >= keyboardHoverDateUnix) ||
                        (cellDateUnix >= startDateUnix &&
                            cellDateUnix <= keyboardHoverDateUnix)
                    ) {
                        cellStyle += ' caleran-hovered';
                    }
                }
            }
            if (
                this.lastHoverStatus === false &&
                cellStyle.indexOf('caleran-hovered') > 0
            ) {
                this.lastHoverStatus = true;
                cellStyle += ' caleran-hovered-first';
            }
            if (
                this.lastHoverStatus === true &&
                cellStyle.indexOf('caleran-hovered') < 0
            ) {
                cell.prev('.caleran-day').addClass('caleran-hovered-last');
                this.lastHoverStatus = false;
            }
            return cellStyle;
        },
        /**
         * returns calculated disabled state style of the cell
         * @param {object}  cell current cell jquery object
         * @param {object}  cellMoment current cell's moment
         * @param {integer} cellDateUnix current call's moment unix timestamp
         * @param {string}  cellStyle current cell's already calculated style
         * @param {integer} minDateUnix minDate config moment unix timestamp
         * @param {integer} maxDateUnix maxDate config moment unix timestamp
         * @returns {string} appended style of the cell
         */
        addDisabledStyles: function (
            cell,
            cellMoment,
            cellDateUnix,
            cellStyle,
            minDateUnix,
            maxDateUnix,
            currentMonth
        ) {
            if (this.isDisabled(cellDateUnix)) {
                if (this.config.isHotelBooking == false) {
                    cellStyle = cellStyle.replace(
                        'caleran-day',
                        'caleran-disabled caleran-disabled-range'
                    );
                } else {
                    switch (this.globals.disabledDays[cellDateUnix]) {
                        case 1:
                            cellStyle = cellStyle.replace(
                                'caleran-day',
                                'caleran-day caleran-disabled-range caleran-disabled-range-start'
                            );
                            break;
                        case 2:
                            cellStyle = cellStyle.replace(
                                'caleran-day',
                                'caleran-disabled caleran-disabled-range'
                            );
                            break;
                        case 3:
                            cellStyle = cellStyle.replace(
                                'caleran-day',
                                'caleran-day caleran-disabled-range caleran-disabled-range-end'
                            );
                            break;
                    }
                }
            } else if (
                (maxDateUnix != null && cellDateUnix > maxDateUnix) ||
                (minDateUnix != null && cellDateUnix < minDateUnix)
            ) {
                cellStyle = cellStyle = cellStyle.replace(
                    'caleran-day',
                    'caleran-disabled'
                );
            }
            if (cellMoment.month() != currentMonth) {
                cellStyle += ' caleran-not-in-month';
            }
            return cellStyle;
        },
        /**
         * Localizes the given number to the current locale
         * @param {string} input The number to be localized
         * @returns {string} The localized number string
         */
        localizeNumbers: function (input) {
            return moment.localeData(this.config.locale).postformat('' + input);
        },
        /**
         * Event triggered when a range link is clicked in the footer of the plugin
         * @param   {object} e the clicked range link
         * @returns void
         */
        rangeClicked: function (e) {
            e = e || window.event;
            e.target = e.target || e.srcElement;
            if (!e.target.hasAttribute('data-id')) return;
            var range_id = $(e.target).attr('data-id');
            this.globals.currentDate = this.config.ranges[range_id].startDate
                .startOf('day')
                .clone()
                .middleOfDay();
            this.config.startDate = this.config.ranges[range_id].startDate
                .startOf('day')
                .clone()
                .middleOfDay();
            this.config.endDate = this.config.ranges[range_id].endDate
                .startOf('day')
                .clone()
                .middleOfDay();
            this.globals.firstValueSelected = true;
            if (this.checkRangeContinuity() === false) {
                this.fetchInputs();
            } else {
                this.clearRangeSelection();
                this.config.ranges[range_id].selected = true;
                this.config.onrangeselect(this, this.config.ranges[range_id]);
                this.reDrawCalendars();
                this.setViewport();
                if (this.config.autoCloseOnSelect) this.hideDropdown();
            }
            this.stopBubbling(e);
            return false;
        },
        /**
         * Fixes the view positions of dropdown calendar plugin
         * @returns void
         */
        setViewport: function () {
            if (this.globals.isMobile === true) {
                if (this.input.css('display') !== 'none') {
                    this.container.trigger('caleran:resize');
                }
            } else {
                if (
                    this.container.css('display') !== 'none' &&
                    this.globals.initComplete &&
                    this.globals.isMobile === false &&
                    this.config.inline === false
                ) {
                    var viewport = this.getViewport();
                    var result = false;
                    switch (this.config.showOn) {
                        case 'top':
                            result = this.config.autoAlign
                                ? this.positionOnTopAlign(viewport)
                                : this.positionOnTop(false, viewport);
                            result = this.horizontalAlign(viewport);
                            break;
                        case 'left':
                            result = this.config.autoAlign
                                ? this.positionOnLeftAlign(viewport)
                                : this.positionOnLeft(false, viewport);
                            result = this.verticalAlign(viewport);
                            break;
                        case 'right':
                            result = this.config.autoAlign
                                ? this.positionOnRightAlign(viewport)
                                : this.positionOnRight(false, viewport);
                            result = this.verticalAlign(viewport);
                            break;
                        case 'bottom':
                            result = this.config.autoAlign
                                ? this.positionOnBottomAlign(viewport)
                                : this.positionOnBottom(false, viewport);
                            result = this.horizontalAlign(viewport);
                            break;
                        case 'center':
                            result = this.positionOnCenter(viewport);
                            break;
                        default:
                            result = this.config.autoAlign
                                ? this.positionOnBottomAlign(viewport)
                                : this.positionOnBottom(false, viewport);
                            result = this.horizontalAlign(viewport);
                            break;
                    }
                    if (this.config.rangeOrientation !== 'horizontal') {
                        var height =
                            this.input.find('.caleran-header').outerHeight() +
                            this.input
                                .find('.caleran-calendars')
                                .outerHeight() +
                            (this.input.find('.caleran-footer').length > 0
                                ? this.input
                                      .find('.caleran-footer')
                                      .outerHeight()
                                : 0);
                        this.input
                            .find('.caleran-right')
                            .css('max-height', height);
                    }
                }
            }
        },
        /**
         * Vertically aligns the dropdown when the viewport changes
         * @param {object} viewport An object containing the current viewport dimensions
         */
        verticalAlign: function (viewport) {
            var dropdown = this.getDimensions(this.container, true),
                start_difference = viewport.top - dropdown.offsetTop,
                end_difference =
                    dropdown.offsetTop + dropdown.height - viewport.bottom;

            if (
                start_difference > 0 &&
                Math.abs(start_difference) < dropdown.height
            ) {
                this.container.css({
                    top: function () {
                        return (
                            parseFloat($(this).css('top').replace(/px$/, '')) +
                            start_difference
                        );
                    },
                });
                this.container.find("div[class*='caleran-box-arrow-']").css({
                    top: function () {
                        return (
                            parseFloat($(this).css('top').replace(/px$/, '')) -
                            start_difference
                        );
                    },
                });
            } else if (
                end_difference > 0 &&
                Math.abs(end_difference) < dropdown.height
            ) {
                this.container.css({
                    top: function () {
                        return (
                            parseFloat($(this).css('top').replace(/px$/, '')) -
                            end_difference
                        );
                    },
                });
                this.container.find("div[class*='caleran-box-arrow-']").css({
                    top: function () {
                        return (
                            parseFloat($(this).css('top').replace(/px$/, '')) +
                            end_difference
                        );
                    },
                });
            }
        },
        /**
         * Horizontally aligns the dropdown when the viewport changes
         * @param {object} viewport An object containing the current viewport dimensions
         */
        horizontalAlign: function (viewport) {
            var dropdown = this.getDimensions(this.container, true),
                difference =
                    dropdown.offsetLeft + dropdown.width - viewport.right;
            if (difference > 0 && Math.abs(difference) < dropdown.width) {
                this.container.css({
                    left: function () {
                        return (
                            parseFloat($(this).css('left').replace(/px$/, '')) -
                            difference
                        );
                    },
                });
                this.container.find("div[class*='caleran-box-arrow-']").css({
                    left: function () {
                        return (
                            parseFloat($(this).css('left').replace(/px$/, '')) +
                            difference
                        );
                    },
                });
            }
        },
        /**
         * Gets the coordinates of the dropdown relative to the position
         * @param string position
         */
        getDropdownPos: function (position) {
            var input = this.getDimensions(this.$elem, true);
            var dropdown = this.getDimensions(this.container, true);
            var margin = parseInt(this.input.css('margin-left'), 10);
            var arrow = parseFloat(
                this.container
                    .find("div[class*='caleran-box-arrow']")
                    .first()
                    .outerHeight() / 2
            );
            switch (position) {
                case 'left':
                    switch (this.config.arrowOn) {
                        case 'top':
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    arrow -
                                    input.height / 2,
                                left:
                                    input.offsetLeft - dropdown.width - margin,
                                arrow: 0,
                            };
                        case 'center':
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    dropdown.height / 2,
                                left:
                                    input.offsetLeft - dropdown.width - margin,
                                arrow:
                                    (dropdown.height - arrow * 2) / 2 -
                                    input.height / 2,
                            };
                        case 'bottom':
                            return {
                                top:
                                    input.offsetTop -
                                    dropdown.height +
                                    input.height +
                                    2 * margin +
                                    arrow,
                                left:
                                    input.offsetLeft - dropdown.width - margin,
                                arrow:
                                    dropdown.height -
                                    arrow * 4 -
                                    3 * margin -
                                    input.height / 2,
                            };
                        default:
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    arrow -
                                    input.height / 2,
                                left:
                                    input.offsetLeft - dropdown.width - margin,
                                arrow: 0,
                            };
                    }
                    break;
                case 'right':
                    switch (this.config.arrowOn) {
                        case 'top':
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    arrow -
                                    input.height / 2,
                                left: input.offsetLeft + input.width + margin,
                                arrow: 0,
                            };
                        case 'center':
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    dropdown.height / 2,
                                left: input.offsetLeft + input.width + margin,
                                arrow:
                                    (dropdown.height - arrow * 2) / 2 -
                                    input.height / 2,
                            };
                        case 'bottom':
                            return {
                                top:
                                    input.offsetTop -
                                    dropdown.height +
                                    input.height +
                                    2 * margin +
                                    arrow,
                                left: input.offsetLeft + input.width + margin,
                                arrow:
                                    dropdown.height -
                                    arrow * 4 -
                                    3 * margin -
                                    input.height / 2,
                            };
                        default:
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    arrow -
                                    input.height / 2,
                                left: input.offsetLeft + input.width + margin,
                                arrow: 0,
                            };
                    }
                    break;
                case 'top':
                    switch (this.config.arrowOn) {
                        case 'left':
                            return {
                                top: input.offsetTop - dropdown.height - margin,
                                left: input.offsetLeft - margin,
                                arrow: 0,
                            };
                        case 'center':
                            return {
                                top: input.offsetTop - dropdown.height - margin,
                                left:
                                    input.offsetLeft -
                                    (dropdown.width -
                                        margin * 2 -
                                        input.width) /
                                        2,
                                arrow: (dropdown.width - arrow * 5) / 2,
                            };
                        case 'right':
                            return {
                                top: input.offsetTop - dropdown.height - margin,
                                left:
                                    input.offsetLeft -
                                    (dropdown.width - input.width) +
                                    margin,
                                arrow: dropdown.width - 5 * arrow + margin,
                            };
                        default:
                            return {
                                top: input.offsetTop - dropdown.height - margin,
                                left: input.offsetLeft - margin,
                                arrow: 0,
                            };
                    }
                    break;
                case 'bottom':
                    switch (this.config.arrowOn) {
                        case 'left':
                            return {
                                top:
                                    input.offsetTop +
                                    input.height -
                                    margin +
                                    arrow,
                                left: input.offsetLeft - margin,
                                arrow: 0,
                            };
                        case 'center':
                            return {
                                top:
                                    input.offsetTop +
                                    input.height -
                                    margin +
                                    arrow,
                                left:
                                    input.offsetLeft -
                                    (dropdown.width -
                                        margin * 2 -
                                        input.width) /
                                        2,
                                arrow: (dropdown.width - arrow * 5) / 2,
                            };
                        case 'right':
                            return {
                                top:
                                    input.offsetTop +
                                    input.height -
                                    margin +
                                    arrow,
                                left:
                                    input.offsetLeft -
                                    (dropdown.width - input.width) +
                                    margin,
                                arrow: dropdown.width - 5 * arrow + margin,
                            };
                        default:
                            return {
                                top:
                                    input.offsetTop +
                                    input.height -
                                    margin +
                                    arrow,
                                left: input.offsetLeft - margin,
                                arrow: 0,
                            };
                    }
                    break;
                case 'center':
                    switch (this.config.arrowOn) {
                        case 'center':
                            return {
                                top:
                                    input.offsetTop -
                                    margin -
                                    dropdown.height / 2,
                                left:
                                    input.offsetLeft -
                                    (dropdown.width -
                                        margin * 2 -
                                        input.width) /
                                        2,
                            };
                    }
                    break;
            }
        },
        /**
         * Moves the plugin dropdown relative to the input or return the calculated areas
         * @param               {boolean}    returnValues whether the method should apply the CSS or return the calculated values
         * @param               {object}     viewport the current viewport boundaries, top, right, bottom, left in pixels
         * @returns             {object}     if returnValues is set to true, it returns the calculated positions
         */
        positionOnTop: function (returnValues, viewport) {
            var setting = this.getDropdownPos('top');
            if (!returnValues) {
                this.container.css({ left: setting.left, top: setting.top });
                this.container
                    .find("div[class*='caleran-box-arrow-']")
                    .attr('class', 'caleran-box-arrow-bottom')
                    .css({ left: setting.arrow });
            } else {
                return setting;
            }
        },
        /**
         * Moves the plugin dropdown relative to the input or return the calculated areas
         * @param               {boolean}    returnValues whether the method should apply the CSS or return the calculated values
         * @param               {object}     viewport the current viewport boundaries, top, right, bottom, left in pixels
         * @returns             {object}     if returnValues is set to true, it returns the calculated positions
         */
        positionOnBottom: function (returnValues, viewport) {
            var setting = this.getDropdownPos('bottom');
            if (!returnValues) {
                this.container.css({ left: setting.left, top: setting.top });
                this.container
                    .find("div[class*='caleran-box-arrow-']")
                    .attr('class', 'caleran-box-arrow-top')
                    .css({ left: setting.arrow });
            } else {
                return setting;
            }
        },
        /**
         * Moves the plugin dropdown relative to the input or return the calculated areas
         * @param               {boolean}    returnValues whether the method should apply the CSS or return the calculated values
         * @param               {object}     viewport the current viewport boundaries, top, right, bottom, left in pixels
         * @returns             {object}     if returnValues is set to true, it returns the calculated positions
         */
        positionOnLeft: function (returnValues, viewport) {
            var setting = this.getDropdownPos('left');
            if (!returnValues) {
                this.container.css({ left: setting.left, top: setting.top });
                this.container
                    .children("div[class*='caleran-box-arrow-']")
                    .attr('class', 'caleran-box-arrow-right')
                    .css({ top: setting.arrow });
            } else {
                return setting;
            }
        },
        /**
         * Moves the plugin dropdown relative to the input or return the calculated areas
         * @param               {boolean}    returnValues whether the method should apply the CSS or return the calculated values
         * @param               {object}     viewport the current viewport boundaries, top, right, bottom, left in pixels
         * @returns             {object}     if returnValues is set to true, it returns the calculated positions
         */
        positionOnRight: function (returnValues, viewport) {
            var setting = this.getDropdownPos('right');
            if (!returnValues) {
                this.container.css({ left: setting.left, top: setting.top });
                this.container
                    .children("div[class*='caleran-box-arrow-']")
                    .attr('class', 'caleran-box-arrow-left')
                    .css({ top: setting.arrow });
            } else {
                return setting;
            }
        },
        /**
         * Moves the plugin dropdown relative to the input
         * @param               {object}     viewport the current viewport boundaries, top, right, bottom, left in pixels
         * @returns             {object}
         */
        positionOnCenter: function (viewport) {
            var setting = this.getDropdownPos('center');
            var offsetX = Math.max(
                setting.left +
                    this.container[0].clientWidth -
                    (viewport.right - 30),
                0
            );
            var offsetY = Math.max(
                setting.top +
                    this.container[0].clientHeight -
                    (viewport.bottom - 30),
                0
            );
            if (!this.config.autoAlign) {
                offsetX = 0;
                offsetY = 0;
            }
            setting.left -= offsetX;
            setting.top -= offsetY;
            this.container.css({ left: setting.left, top: setting.top });
            this.container.find("div[class*='caleran-box-arrow-']").remove();
        },
        /**
         * Position the plugin dropdown relative to the input or return the calculated areas, and fixes if any overflow occurs
         */
        positionOnBottomAlign: function (viewport) {
            var standardPosition = this.positionOnBottom(true, viewport);
            var dropdown = this.getDimensions(this.container);
            if (standardPosition.top + dropdown.height < viewport.bottom) {
                this.positionOnBottom(false, viewport);
            } else {
                this.positionOnTop(false, viewport);
            }
        },
        /**
         * Position the plugin dropdown relative to the input or return the calculated areas, and fixes if any overflow occurs
         */
        positionOnLeftAlign: function (viewport) {
            var standardPosition = this.positionOnLeft(true, viewport);
            if (standardPosition.left > viewport.left - 50) {
                this.positionOnLeft(false, viewport);
            } else {
                this.positionOnRight(false, viewport);
            }
        },
        /**
         * Position the plugin dropdown relative to the input or return the calculated areas, and fixes if any overflow occurs
         */
        positionOnRightAlign: function (viewport) {
            var standardPosition = this.positionOnRight(true, viewport);
            var dropdown = this.getDimensions(this.container);
            if (standardPosition.left + dropdown.width < viewport.right + 50) {
                this.positionOnRight(false, viewport);
            } else {
                this.positionOnLeft(false, viewport);
            }
        },
        /**
         * Position the plugin dropdown relative to the input or return the calculated areas, and fixes if any overflow occurs
         */
        positionOnTopAlign: function (viewport) {
            var standardPosition = this.positionOnTop(true, viewport);
            if (standardPosition.top > viewport.top) {
                this.positionOnTop(false, viewport);
            } else {
                this.positionOnBottom(false, viewport);
            }
        },
        /**
         * Helper method for getting an element's inner/outer dimensions and positions
         * @param  [DOMelement] elem  The element whose dimensions and position are wanted
         * @param  {boolean}     outer true/false variable which tells the method to return inner(false) or outer(true) dimensions
         * @return {object}      an user defined object which contains the width and height of the element and top and left positions relative to the viewport
         */
        getDimensions: function (element, outer) {
            var doc = document,
                win = window,
                body = doc.body,
                elem = element[0],
                that = this,
                off = element.offset();
            if (
                element === this.$elem &&
                this.globals.parentScrollEventsAttached == false
            ) {
                // give each scroll parent a callback when they'll run on scrolling
                var scrollParent = $.proxy(function () {
                    // dont run while it's already running
                    if (!this.globals.isTicking) {
                        this.globals.isTicking = true;
                        // rAF technique implementation
                        this.globals.rafID = this.requestAnimFrame(
                            $.proxy(function () {
                                this.setViewport();
                                this.cancelAnimFrame(this.globals.rafID);
                                this.globals.isTicking = false;
                            }, this)
                        );
                    }
                }, this);

                // find the scrollable parents
                if (elem !== body) {
                    var parent = elem.parentNode;
                    while (parent !== body && parent !== null) {
                        // if the parent is scrollable
                        if (parent.scrollHeight > parent.offsetHeight) {
                            // attach the scroll event
                            $(parent)
                                .off('scroll.caleran')
                                .on('scroll.caleran', scrollParent);
                        }
                        // switch to next parent
                        parent = parent.parentNode;
                    }
                }
                // don't attach again once attached.
                this.globals.parentScrollEventsAttached = true;
            }

            // now return the dimensions
            return {
                width: elem.offsetWidth,
                height: elem.offsetHeight,
                offsetLeft: off.left,
                offsetTop: off.top,
            };
        },
        /**
         * Helper method for getting the window viewport
         * @return {object}     an user defined object which contains the rectangular position and dimensions of the viewport
         */
        getViewport: function () {
            var top = this.globals.lastScrollY,
                left = this.globals.lastScrollX,
                bottom = top + window.innerHeight,
                right = left + window.innerWidth;
            return { top: top, left: left, right: right, bottom: bottom };
        },
        /**
         * Attaches the related events on the elements after render/update
         * @return void
         */
        attachEvents: function () {
            var clickNextEvent = $.proxy(this.drawNextMonth, this);
            var clickPrevEvent = $.proxy(this.drawPrevMonth, this);
            var clickCellEvent = $.proxy(this.cellClicked, this);
            var hoverCellEvent = $.proxy(this.cellHovered, this);
            var rangeClickedEvent = $.proxy(this.rangeClicked, this);
            var monthSwitchClickEvent = $.proxy(this.monthSwitchClicked, this);
            var yearSwitchClickEvent = $.proxy(this.yearSwitchClicked, this);
            var clickEvent = 'click.caleran';
            this.container
                .find('.caleran-next')
                .off(clickEvent)
                .one(
                    clickEvent,
                    !this.config.isRTL ? clickNextEvent : clickPrevEvent
                );
            this.container
                .find('.caleran-prev')
                .off(clickEvent)
                .one(
                    clickEvent,
                    !this.config.isRTL ? clickPrevEvent : clickNextEvent
                );
            this.container
                .find('.caleran-day')
                .off(clickEvent)
                .on(clickEvent, clickCellEvent);
            this.container
                .find('.caleran-day')
                .off('mouseover.caleran')
                .on('mouseover.caleran', hoverCellEvent);
            this.container
                .find('.caleran-disabled')
                .not('.caleran-day')
                .off(clickEvent);
            this.container
                .find('.caleran-range')
                .off(clickEvent)
                .on(clickEvent, rangeClickedEvent);
            this.container
                .find('.caleran-month-switch ')
                .off(clickEvent)
                .on(clickEvent, monthSwitchClickEvent);
            this.container
                .find('.caleran-year-switch ')
                .off(clickEvent)
                .on(clickEvent, yearSwitchClickEvent);

            if (
                this.globals.isMobile === true &&
                this.config.enableSwipe == true
            ) {
                // check if jQuery Mobile is loaded
                if (typeof $.fn.swiperight === 'function') {
                    this.input
                        .find('.caleran-calendars')
                        .css('touch-action', 'none');
                    this.input
                        .find('.caleran-calendars')
                        .on(
                            'swipeleft',
                            this.config.isRTL ? clickNextEvent : clickPrevEvent
                        );
                    this.input
                        .find('.caleran-calendars')
                        .on(
                            'swiperight',
                            this.config.isRTL ? clickPrevEvent : clickNextEvent
                        );
                } else {
                    var hammer = new Hammer(
                        this.input.find('.caleran-calendars').get(0)
                    );
                    hammer
                        .off('swipeleft')
                        .on(
                            'swipeleft',
                            this.config.isRTL ? clickNextEvent : clickPrevEvent
                        );
                    hammer
                        .off('swiperight')
                        .on(
                            'swiperight',
                            this.config.isRTL ? clickPrevEvent : clickNextEvent
                        );
                }
            }
            if (
                (this.globals.isMobile || this.config.showButtons) &&
                !this.config.inline
            ) {
                this.input
                    .find('.caleran-cancel')
                    .off('click.caleran')
                    .on(
                        'click.caleran',
                        $.proxy(function (event) {
                            if (
                                this.config.onCancel(
                                    this,
                                    this.config.startDate,
                                    this.config.endDate
                                ) == true
                            ) {
                                if (this.globals.startDateInitial)
                                    this.config.startDate =
                                        this.globals.startDateInitial.clone();
                                if (this.globals.endDateInitial)
                                    this.config.endDate =
                                        this.globals.endDateInitial.clone();
                                this.hideDropdown(event);
                            }
                        }, this)
                    );

                this.input
                    .find('.caleran-apply')
                    .off('click.caleran')
                    .on(
                        'click.caleran',
                        $.proxy(function (event) {
                            if (
                                this.config.onbeforeselect(
                                    this,
                                    this.config.startDate.clone(),
                                    this.config.endDate.clone()
                                ) === true &&
                                this.checkRangeContinuity() === true
                            ) {
                                this.globals.firstValueSelected = true;
                                if (this.globals.delayInputUpdate) {
                                    this.globals.delayInputUpdate = false;
                                    this.updateInput(true);
                                    this.globals.delayInputUpdate = true;
                                } else {
                                    this.updateInput(true);
                                }
                            } else {
                                this.fetchInputs();
                            }
                            this.hideDropdown(event);
                        }, this)
                    );
            }
        },
        /**
         * Events per instance
         */
        addInitialEvents: function () {
            // buffer current scope
            var that = this;

            // create namespaced event name
            var eventClick = 'click.caleran';

            // create a unique document event name because multiple instances may overlap each other
            this.globals.documentEvent =
                eventClick +
                '_' +
                Math.round(new Date().getTime() + Math.random() * 100);

            // create a instance specific unique document click event, which will be used to hide the dropdown
            $(document).on(
                this.globals.documentEvent,
                $.proxy(function (e) {
                    if (
                        this.globals.isMobile === false &&
                        this.config.inline === false
                    ) {
                        var event =
                            e ||
                            window.event ||
                            jQuery.Event('click', { target: 'body' });
                        var eventTarget = event.target || event.srcElement;
                        if (
                            $(this.container).find($(eventTarget)).length ===
                                0 &&
                            this.elem !== eventTarget &&
                            this.container.is(':visible') > 0
                        ) {
                            this.hideDropdown(event);
                        }
                    }
                }, this)
            );

            // if the keyboard interaction is enabled, redefine the click event
            if (this.config.enableKeyboard)
                eventClick = 'click.caleran focus.caleran';

            // prepare the input click + focus event which will open the dropdown
            this.$elem.off(eventClick).on(
                eventClick,
                $.proxy(
                    this.debounce(
                        function (e) {
                            var event =
                                e ||
                                window.event ||
                                jQuery.Event('click', { target: 'body' });
                            var eventTarget = event.target || event.srcElement;
                            if (
                                this.input.get(0).clientHeight > 0 &&
                                this.config.target.get(0) !== eventTarget
                            ) {
                                this.hideDropdown(event);
                            } else {
                                $(document).trigger('click');
                                this.showDropdown(event);
                            }
                        },
                        200,
                        true
                    ),
                    this
                )
            );

            // on mobile screens, add an event to restyle the modal when a layout change occurs
            if (this.globals.isMobile) {
                $(window).on(
                    'resize.caleran',
                    $.proxy(function () {
                        this.container.trigger('caleran:resize');
                    }, this)
                );
            }

            // define the mobile resize event
            this.container.on('caleran:resize', function () {
                that.globals.rafID = that.requestAnimFrame(function () {
                    if (that.input.css('display') !== 'none') {
                        var oneCalendarHeight = that.input
                            .find('.caleran-calendar:visible:first')
                            .innerHeight();
                        that.input
                            .find('.caleran-calendars')
                            .css('height', oneCalendarHeight);
                        if (that.input.position().top < 0)
                            that.input.addClass('caleran-input-top-reset');
                        if (window.innerWidth > window.innerHeight) {
                            // landscape mode
                            that.input.css('height', oneCalendarHeight + 'px');
                        } else {
                            // portrait mode
                            that.input.css('height', 'auto');
                        }
                        that.cancelAnimFrame(that.globals.rafID);
                    }
                });
            });

            // run the event once if the environment is mobile and (possibly) display is inline
            if (this.input.css('display') !== 'none' && this.globals.isMobile)
                this.container.trigger('caleran:resize');

            // append the desktop layout refreshing event
            if (this.globals.isMobile === false) {
                $(window).on('resize.caleran scroll.caleran', function () {
                    // dont run while it's already running
                    if (!that.globals.isTicking) {
                        that.globals.isTicking = true;
                        // rAF technique implementation
                        that.globals.lastScrollX =
                            window.scrollX ||
                            window.pageXOffset ||
                            document.documentElement.scrollLeft;
                        that.globals.lastScrollY =
                            window.scrollY ||
                            window.pageYOffset ||
                            document.documentElement.scrollTop;
                        that.globals.rafID = that.requestAnimFrame(
                            $.proxy(function () {
                                this.setViewport();
                                this.globals.isTicking = false;
                                this.cancelAnimFrame(this.globals.rafID);
                            }, that)
                        );
                    }
                });
            }
        },
        /**
         * cross browser event bubbling (propagation) prevention handler
         * @return void
         */
        stopBubbling: function (e) {
            if (typeof e.stopPropagation !== 'undefined') {
                e.stopPropagation();
            } else if (typeof e.cancelBubble !== 'undefined') {
                e.cancelBubble = true;
            }
            if (typeof e.preventDefault !== 'undefined') {
                e.preventDefault();
            }
            e.returnValue = false;
        },
        /**
         * Delays a multiple triggered method execution after the last one has been triggered
         * @return [function] given callback execution promise/result
         */
        debounce: function (func, wait, immediate) {
            return function () {
                var context = this,
                    args = arguments;
                var later = function () {
                    context.globals.throttleTimeout = null;
                    if (!immediate) func.apply(context, args);
                };
                var callNow = immediate && !context.globals.throttleTimeout;
                clearTimeout(context.globals.throttleTimeout);
                context.globals.throttleTimeout = setTimeout(later, wait);
                if (callNow) func.apply(context, args);
            };
        },
        /**
         * javascript rAF implementation for cross browser compatibility
         */
        requestAnimFrame: function (callback) {
            if (typeof window.requestAnimationFrame === 'function')
                return requestAnimationFrame(callback);
            if (typeof window.webkitRequestAnimationFrame === 'function')
                return webkitRequestAnimationFrame(callback);
            if (typeof window.mozRequestAnimationFrame === 'function')
                return mozRequestAnimationFrame(callback);
            return setTimeout(callback, 100 / 6);
        },
        /**
         * javascript cAF implementation for cross browser compatibility
         */
        cancelAnimFrame: function (id) {
            if (typeof window.cancelAnimationFrame === 'function')
                return cancelAnimationFrame(id);
            if (typeof window.webkitCancelAnimationFrame === 'function')
                return webkitCancelAnimationFrame(id);
            if (typeof window.mozCancelAnimationFrame === 'function')
                return mozCancelAnimationFrame(id);
            return clearTimeout(id);
        },
        /**
         * Attaches keyboard events if enabled
         * @return void
         */
        addKeyboardEvents: function () {
            if (this.config.enableKeyboard) {
                var keyDownEvent = $.proxy(function (event) {
                    var keycode = event.which ? event.which : event.keyCode;
                    if (this.globals.keyboardHoverDate === null) {
                        if (this.config.startDate === null) {
                            this.globals.keyboardHoverDate = moment({
                                day: 1,
                                month: this.calendars.first().data('month'),
                            }).middleOfDay();
                        } else {
                            this.globals.keyboardHoverDate =
                                this.config.startDate.clone().middleOfDay();
                        }
                    } else {
                        this.globals.keyboardHoverDate.middleOfDay();
                    }
                    var shouldReDraw = false,
                        shouldPrevent = false;
                    switch (keycode) {
                        case 37: // left
                            this.globals.keyboardHoverDate.add(-1, 'day');
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 38: // top
                            this.globals.keyboardHoverDate.add(-1, 'week');
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 39: // right
                            this.globals.keyboardHoverDate.add(1, 'day');
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 40: // bottom
                            this.globals.keyboardHoverDate.add(1, 'week');
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 32: // space
                            this.input
                                .find(
                                    ".caleran-day[data-value='" +
                                        this.globals.keyboardHoverDate
                                            .middleOfDay()
                                            .unix() +
                                        "']"
                                )
                                .first()
                                .trigger('click.caleran');
                            shouldReDraw = false;
                            shouldPrevent = true;
                            break;
                        case 33: // page up
                            if (event.shiftKey) {
                                this.globals.keyboardHoverDate.add(-1, 'years');
                            } else {
                                this.globals.keyboardHoverDate.add(
                                    -1,
                                    'months'
                                );
                            }
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 34: // page down
                            if (event.shiftKey) {
                                this.globals.keyboardHoverDate.add(1, 'years');
                            } else {
                                this.globals.keyboardHoverDate.add(1, 'months');
                            }
                            shouldReDraw = true;
                            shouldPrevent = true;
                            break;
                        case 27: // esc
                        case 9: // tab
                            this.hideDropdown(event);
                            break;
                        case 36: // home
                            if (event.shiftKey) {
                                this.globals.keyboardHoverDate =
                                    moment().middleOfDay();
                                shouldReDraw = true;
                                shouldPrevent = true;
                            }
                            break;
                    }
                    if (shouldReDraw || shouldPrevent) {
                        this.globals.keyboardHoverDate =
                            this.globals.keyboardHoverDate.middleOfDay();
                        if (
                            this.globals.keyboardHoverDate.isBefore(
                                moment.unix(
                                    this.input
                                        .find('.caleran-day:first')
                                        .attr('data-value')
                                ),
                                'day'
                            ) ||
                            this.globals.keyboardHoverDate.isAfter(
                                moment.unix(
                                    this.input
                                        .find('.caleran-day:last')
                                        .attr('data-value')
                                ),
                                'day'
                            )
                        ) {
                            this.globals.currentDate =
                                this.globals.keyboardHoverDate
                                    .clone()
                                    .startOfMonth();
                            this.reDrawCalendars();
                            shouldReDraw = false;
                        }
                        if (shouldReDraw) {
                            this.globals.hoverDate = null;
                            this.reDrawCells();
                        }
                        if (shouldPrevent) this.stopBubbling(event);
                        return false;
                    }
                }, this);
                this.$elem
                    .off('keydown.caleran')
                    .on('keydown.caleran', keyDownEvent);
                this.container
                    .off('keydown.caleran')
                    .on('keydown.caleran', keyDownEvent);
            }
        },
        /**
         * Destroys the instance
         */
        destroy: function () {
            if (this.config.onBeforeDestroy(this)) {
                if (this.config.inline) {
                    this.input.remove();
                    if (this.globals.isMobile)
                        this.$elem.unwrap('.caleran-container-mobile');
                    else this.$elem.unwrap('.caleran-container');
                    this.elem.type = 'text';
                } else {
                    this.container.remove();
                }
                $(document).off(this.globals.documentEvent);
                this.$elem.removeData('caleran');
                this.config.ondestroy(this);
            }
        },
        /**
         * Detects if the browser is mobile
         * @return {boolean} if the browser is mobile or not
         */
        checkMobile: function () {
            return window.matchMedia(
                'only screen and (max-width: ' +
                    this.config.mobileBreakpoint +
                    'px)'
            ).matches;
        },
        /**
         * Converts the given datetime variable to momentjs object
         * @param {mixed} datetime the datetime variable to convert to moment object
         * @return {momentobject} the converted variable
         */
        fixDateTime: function (datetime) {
            if (datetime != null && moment.isMoment(datetime) == false) {
                if (typeof datetime === 'string') {
                    datetime = moment(datetime, this.config.format).locale(
                        this.config.locale
                    );
                } else {
                    datetime = moment(datetime).locale(this.config.locale);
                }
            }
            return datetime;
        },
        /**
         * set start date & time programmatically
         * @param {moment object | js Date object | ISO Datetime string} datetime the value to be set
         */
        setStart: function (datetime) {
            this.setDisplayDate(datetime);
            var datetimeConverted = this.fixDateTime(datetime);
            if (
                this.isDisabled(datetimeConverted) === false &&
                moment(datetimeConverted).isValid()
            ) {
                this.config.startDate = moment(datetimeConverted);
                this.refreshValues();
            }
        },
        /**
         * set end date & time programmatically
         * @param {moment object | js Date object | ISO Datetime string} datetime the value to be set
         */
        setEnd: function (datetime) {
            this.setDisplayDate(datetime);
            var datetimeConverted = this.fixDateTime(datetime);
            if (
                this.isDisabled(datetimeConverted) === false &&
                moment(datetimeConverted).isValid()
            ) {
                this.config.endDate = moment(datetimeConverted);
                this.refreshValues();
            }
        },
        /**
         * set min date & time programmatically
         * @param {moment object | js Date object | ISO Datetime string} datetime the value to be set
         */
        setMinDate: function (datetime) {
            var datetimeConverted = this.fixDateTime(datetime);
            if (moment(datetimeConverted).isValid()) {
                this.config.minDate = moment(datetimeConverted);
                this.refreshValues();
            }
        },
        /**
         * set max date & time programmatically
         * @param {moment object | js Date object | ISO Datetime string} datetime the value to be set
         */
        setMaxDate: function (datetime) {
            var datetimeConverted = this.fixDateTime(datetime);
            if (moment(datetimeConverted).isValid()) {
                this.config.maxDate = moment(datetimeConverted);
                this.refreshValues();
            }
        },
        /**
         * Sets the displayed month and year
         * @param {moment object | js Date object | ISO Datetime string} datetime the value to be set
         */
        setDisplayDate: function (datetime) {
            var datetimeConverted = this.fixDateTime(datetime);
            if (moment(datetimeConverted).isValid()) {
                this.globals.currentDate = moment(datetimeConverted);
                this.reDrawCalendars();
            }
        },
        /**
         * private method to reset the startdate and enddate to the input
         * @returns void
         */
        refreshValues: function () {
            var backup = this.globals.delayInputUpdate;
            this.globals.delayInputUpdate = false;
            this.validateDates();
            this.updateInput();
            this.globals.delayInputUpdate = backup;
            this.reDrawCells();
        },
    };
    caleran.defaults = caleran.prototype.defaults;
    /**
     * The main handler of caleran plugin
     * @param  {object} options javascript object which contains element specific or range specific options
     * @return {caleran} plugin reference
     */
    $.fn.caleran = function (options) {
        return this.each(function () {
            new caleran(this, options).init();
        });
    };

    /**
     * add middleOfDay method to moment.js to set 12:00:00 for the current moment
     * @return {object} modified momentjs instance.
     */
    if (typeof moment.fn.middleOfDay !== 'function') {
        moment.fn.middleOfDay = function () {
            this.hours(12).minutes(0).seconds(0);
            return this;
        };
        moment.fn.startOfMonth = function () {
            this.middleOfDay().date(1);
            return this;
        };
    }
})(jQuery, window, document);
