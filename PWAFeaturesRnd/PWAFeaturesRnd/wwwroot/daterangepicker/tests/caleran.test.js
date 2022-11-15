describe("initial testing dropdown", function () {
    beforeEach(function () {
        this.initcalled = false;
        if ($(".caleran-test-input").length > 0) {
            $(".caleran-test-input").data("caleran").destroy();
            $(".caleran-test-input").remove();
        }
        this.input = $(
            "<input type='text' class='caleran-test-input' />"
        ).appendTo("body");
        var that = this;
        this.input.caleran({
            oninit: function () {
                that.initcalled = true;
            }
        });
        this.caleran = this.input.data("caleran");
        this.element = this.caleran.input;
    });
    afterEach(function () {
        this.input = $(".caleran-test-input");
        this.input.data("caleran").destroy();
        this.caleran = null;
        this.input.remove();
    });

    it("should have appended an this.input to the body", function () {
        expect($(".caleran-test-input").length).toEqual(1);
    });

    it("should have given an caleran object", function () {
        expect(this.caleran).toEqual(jasmine.any(Object));
    });

    it("should have oninit event called", function () {
        expect(this.initcalled).toBe(true);
    });
    it("should have initcompleted flag set", function () {
        expect(this.caleran.globals.initComplete).toBe(true);
    });

    it("should have the date set on the this.input", function () {
        expect(this.input.val()).toEqual(this.caleran.config.startDate.format(this.caleran.config.format) + this.caleran.config.dateSeparator + this.caleran.config.endDate.format(this.caleran.config.format));
    });

    it("should have the dropdown hidden", function () {
        expect($(".caleran-input").is(":visible")).toBe(false);
    });

    it("should have the dropdown visible", function () {
        this.input.click();
        expect($(".caleran-input").is(":visible")).toBe(true);
    });

    it("should have the custom date set on the this.input", function () {
        this.input.val(moment({
            year: 2016,
            month: 2,
            day: 13,
            hour: 14,
            minute: 37
        }).format(this.caleran.config.format) + this.caleran.config.dateSeparator + moment({
            year: 2016,
            month: 4,
            day: 24,
            hour: 11,
            minute: 35
        }).format(this.caleran.config.format)).click();
        expect(this.input.val()).toEqual(this.caleran.config.startDate.clone().format(this.caleran.config.format) + this.caleran.config.dateSeparator + this.caleran.config.endDate.clone().format(this.caleran.config.format));
    });

    it("should have the correct months visible and days selected on the calendar", function () {
        this.input.click();
        expect(this.element.find(".caleran-calendar").length).toEqual(this.caleran.config.calendarCount);
        expect(this.element.find(".caleran-calendar:first").data("month")).toEqual(this.caleran.config.startDate.month());
        expect(this.element.find(".caleran-calendar:last").data("month")).toEqual(this.caleran.config.startDate.clone().add(this.caleran.config.calendarCount - 1, "months").month());
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-selected")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-selected")).toBe(true);
        var days = this.element.find(".caleran-day");
        for (var d = 0; d < days.length; d++) {
            var day = days.eq(d);
            var mday = moment.unix(day.data("value"));
            var month = day.parents(".caleran-calendar").first().data("month");
            var daystr = day.text();
            var year = day.parents(".caleran-calendar").first().find(".caleran-year-switch").text();
            expect(mday.hours()).toEqual(12);
            expect(mday.minutes()).toEqual(0);
            expect(mday.seconds()).toEqual(0);
            if (day.hasClass("caleran-not-in-month")) {
                expect((mday.month()).toString()).not.toEqual(month.toString());
            } else {
                expect((mday.month()).toString()).toEqual(month.toString());
            }
            expect(mday.date().toString()).toEqual(daystr);
            if (mday.month().toString() === month.toString())
                expect(mday.year().toString()).toEqual(year);
        }
    });

    it("should move to the next month with arrow click", function () {
        jasmine.clock().install();
        this.input.click();
        for (var i = 0; i < 12; i++) {
            var currentMonth = this.element.find(".caleran-calendar:first").data("month");
            this.element.find(".caleran-next").click();
            jasmine.clock().tick(101);
            var modifiedMonth = this.element.find(".caleran-calendar:first").data("month");
            expect(modifiedMonth).toEqual(currentMonth == 11 ? 0 : currentMonth + 1);
        }
        jasmine.clock().uninstall();
    });

    it("should move to the previous month with arrow click", function () {
        jasmine.clock().install();
        this.input.click();
        for (var i = 0; i < 12; i++) {
            var currentMonth = this.element.find(".caleran-calendar:first").data("month");
            this.element.find(".caleran-prev").click();
            jasmine.clock().tick(101);
            var modifiedMonth = this.element.find(".caleran-calendar:first").data("month");
            expect(modifiedMonth === (currentMonth == 0 ? 11 : currentMonth - 1)).toBe(true);
        }
        jasmine.clock().uninstall();
    });

    it("should set the range", function () {
        var days = this.caleran.input.find(".caleran-day");
        var startDay = $(days[Math.floor(Math.random() * days.length)]).first();
        var endDay = $(days[Math.floor(Math.random() * days.length)]).first();
        var startDate = moment.unix(startDay.attr("data-value"));
        var endDate = moment.unix(endDay.attr("data-value"));
        if (startDate.isAfter(endDate)) {
            var swapper = startDate.clone();
            startDate = endDate.clone();
            endDate = swapper.clone();
        }
        startDay.click();
        endDay.click();
        expect(this.caleran.config.startDate.isSame(startDate, "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(endDate, "day")).toBe(true);
    });

    describe("keyboard tests", function () {

        it("should move to the previous day", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 37,
                keyCode: 37
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 14 }).format('L'));
        });

        it("should move to the next day", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 39,
                keyCode: 39
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 16 }).format('L'));
        });

        it("should move to the previous week", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 38,
                keyCode: 38
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 8 }).format('L'));
        });

        it("should move to the next week", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 40,
                keyCode: 40
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 22 }).format('L'));
        });

        it("should move to the previous month", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 33,
                keyCode: 33
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 15 }).middleOfDay().add(-1, "month").format('L'));
            this.caleran.globals.keyboardHoverDate = moment({ day: 1 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 38,
                keyCode: 38
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.month()).toEqual(moment({ day: 1 }).middleOfDay().add(-1, "month").month());
        });

        it("should move to the next month", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 34,
                keyCode: 34
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 15 }).middleOfDay().add(1, "month").format('L'));
            this.caleran.globals.keyboardHoverDate = moment({ day: 28 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                which: 40,
                keyCode: 40
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.month()).toEqual(moment({ day: 1 }).middleOfDay().add(1, "month").month());
        });

        it("should move to the previous year", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 }).middleOfDay();
            this.input.click();
            var keyPressEvent = {
                shiftKey: true,
                type: 'keydown',
                which: 33,
                keyCode: 33
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 15 }).middleOfDay().add(-1, "year").format('L'));
        });

        it("should move to the next year", function () {
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 });
            this.input.click();
            var keyPressEvent = {
                type: 'keydown',
                shiftKey: true,
                which: 34,
                keyCode: 34
            };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            expect(this.caleran.globals.keyboardHoverDate.format('L')).toEqual(moment({ day: 15 }).middleOfDay().add(1, "year").format('L'));
        });
        it("should select the days with space", function () {
            var startDate = moment({ day: 15 }).middleOfDay();
            var endDate = moment({ day: 17 }).middleOfDay();
            this.caleran.globals.keyboardHoverDate = startDate;
            this.input.focus();
            var keyPressEvent = { type: 'keydown', shiftKey: true, which: 32, keyCode: 32 };
            this.input.trigger(keyPressEvent);
            this.caleran.globals.keyboardHoverDate = endDate;
            var keyPressEvent = { type: 'keydown', shiftKey: true, which: 32, keyCode: 32 };
            this.input.trigger(keyPressEvent);
            expect(this.caleran.config.startDate.clone().middleOfDay().format()).toEqual(startDate.format());
            expect(this.caleran.config.endDate.clone().middleOfDay().format()).toEqual(endDate.format());
        });
        it("should select the singleDate day with space", function () {
            this.caleran.config.singleDate = true;
            var startDate = moment({ day: 15 }).middleOfDay();
            this.caleran.globals.keyboardHoverDate = startDate;
            this.input.focus();
            var keyPressEvent = { type: 'keydown', shiftKey: true, which: 32, keyCode: 32 };
            this.input.trigger(keyPressEvent);
            expect(this.caleran.config.startDate.clone().middleOfDay().format()).toEqual(startDate.format());
        });
        it("should close the dropdown with esc key", function () {
            this.input.focus();
            var keyPressEvent = { type: 'keydown', shiftKey: true, which: 27, keyCode: 27 };
            this.input.trigger(keyPressEvent);
            expect(this.caleran.input.is(":visible")).toBe(false);
        });
        it("should close the dropdown with tab key", function () {
            this.input.focus();
            var keyPressEvent = { type: 'keydown', shiftKey: true, which: 9, keyCode: 9 };
            this.input.trigger(keyPressEvent);
            expect(this.caleran.input.is(":visible")).toBe(false);
        });
        it("should go to the current month with home key", function () {
            jasmine.clock().install();
            this.caleran.globals.keyboardHoverDate = moment({ day: 15 });
            this.input.click();
            var keyPressEvent = { shiftKey: true, type: 'keydown', which: 33, keyCode: 33 };
            this.input.focus();
            this.input.trigger(keyPressEvent);
            jasmine.clock().tick(100);
            expect(this.caleran.globals.keyboardHoverDate.format()).toEqual(moment({ day: 15 }).add(-1, "year").middleOfDay().format());
            var keyPressEvent = { shiftKey: false, type: 'keydown', which: 36, keyCode: 36 };
            this.input.trigger(keyPressEvent);
            jasmine.clock().tick(100);
            expect(this.caleran.globals.currentDate.year()).not.toEqual(moment().year());
            var keyPressEvent = { shiftKey: true, type: 'keydown', which: 36, keyCode: 36 };
            this.input.trigger(keyPressEvent);
            jasmine.clock().tick(100);
            expect(this.caleran.globals.currentDate.year()).toEqual(moment().year());
            jasmine.clock().uninstall();
        });
    });
});
describe("config tests", function () {
    beforeEach(function () {
        this.input = $("<input type='text' class='caleran-test-input' />").appendTo("body");
    });
    afterEach(function () {
        this.input = $(".caleran-test-input");
        this.input.data("caleran").destroy();
        this.caleran = null;
        this.input.remove();
    });
    it("should have the defined count of calendars", function () {
        this.input.caleran({
            calendarCount: 4
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.find(".caleran-calendar").length).toBe(4);
    });
    it("should be visible when inline is defined", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should have mindate effective", function () {
        this.input.caleran({
            minDate: moment()
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.calendars.find("div[data-value='" + moment().subtract(1, "days").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should have maxdate effective", function () {
        this.input.caleran({
            maxDate: moment()
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.calendars.find("div[data-value='" + moment().add(1, "days").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should have the start date and end date effective", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        var endDate = moment("25/05/1984", "DD/MM/YYYY");
        this.input.caleran({ startDate: startDate, endDate: endDate });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("L")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("L"));
        expect(this.caleran.config.endDate.format("L")).toEqual(moment("25/05/1984", "DD/MM/YYYY").format("L"));
        this.input.click();
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toEqual(true);
    });
    it("should respect the mindate on start date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            minDate: moment("24/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss")
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.minDate.inspect());
    });
    it("should respect the mindate on end date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            minDate: moment("20/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss")
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.minDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.minDate.inspect());
    });
    it("should respect the maxdate on end date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            maxDate: moment("24/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss")
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
    });
    it("should respect the maxdate on start date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss"),
            maxDate: moment("20/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss")
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
    });
    it("should swap the start and end date", function () {
        var startDate = moment("20/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss");
        var endDate = moment("15/11/2017 12:00:00", "dd/MM/YYYY HH:mm:ss");
        this.input.caleran({
            startDate: startDate,
            endDate: endDate
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(endDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(startDate.inspect());
    });
    it("should hide the calendar header", function () {
        this.input.caleran({
            showHeader: false
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.find(".caleran-header").length).toBe(1);
        expect(this.caleran.input.find(".caleran-header").is(":visible")).toBe(false);
    });
    it("should not add the calendar footer", function () {
        this.input.caleran({
            showFooter: false
        });
        this.caleran = this.input.data("caleran");
        if (this.caleran.globals.isMobile === false)
            expect(this.caleran.input.find(".caleran-footer").length).toBe(0);
        else
            expect(this.caleran.input.find(".caleran-footer").length).toBe(1);
    });
    it("should start on Monday", function () {
        this.input.caleran({
            startOnMonday: true
        });
        this.caleran = this.input.data("caleran");
        var calendarStart = this.caleran.input.find(".caleran-calendar:first").find(".caleran-disabled, .caleran-day").first().attr("data-value");
        var calendarStartMoment = moment.unix(calendarStart);
        expect(calendarStartMoment.day()).toBe(1);
    });
    it("should start on Sunday", function () {
        this.input.caleran({
            startOnMonday: false
        });
        this.caleran = this.input.data("caleran");
        var calendarStart = this.caleran.input.find(".caleran-calendar:first").find(".caleran-disabled, .caleran-day").first().attr("data-value");
        var calendarStartMoment = moment.unix(calendarStart);
        expect(calendarStartMoment.day()).toBe(0);
    });
    it("should start empty", function () {
        this.input.caleran({
            startEmpty: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.input.val()).toBe("");
        this.input.click();
        $("body").click();
        expect(this.input.val()).toBe("");
        this.input.click();
        expect(this.caleran.calendars.find(".caleran-selected").length).toBe(0);
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:first").click();
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:last").click();
        expect(this.caleran.calendars.find(".caleran-selected").length).not.toBe(0);
        if (this.caleran.globals.isMobile) $(".caleran-apply").click();
        else $("body").click();
        expect(this.input.val()).not.toBe("");
    });
    it("should close when selection occurs (autocloseonselect)", function () {
        this.input.caleran({
            autoCloseOnSelect: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.input.is(":visible")).toBe(true);
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:first").click();
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:last").click();
        expect(this.input.val()).not.toBe("");
        if (this.caleran.globals.isMobile)
            expect(this.caleran.input.is(":visible")).toBe(false);
        else
            expect(this.caleran.container.is(":visible")).toBe(false);
    });
    it("should not close when inline view selection occurs (autocloseonselect)", function () {
        this.input.caleran({
            autoCloseOnSelect: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:first").click();
        this.caleran.calendars.find(".caleran-calendar:first .caleran-day:last").click();
        expect(this.input.val()).not.toBe("");
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should disable days by function", function () {
        this.input.caleran({
            disableDays: function (day) {
                return day.isSame(moment(), "day");
            }
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        expect(this.caleran.calendars.find("[data-value='" + moment().middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should disable days by array", function () {
        this.input.caleran({
            disabledRanges: [{
                start: moment().add(1, "day"),
                end: moment().add(3, "day")
            }]
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        expect(this.caleran.calendars.find("[data-value='" + moment().add(1, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().add(2, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().add(3, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should preserve continuousity when disabling days by function  (first value selected state)", function () {
        this.input.caleran({
            disableDays: function (day) {
                return day.isSame(moment(), "day");
            },
            continuous: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-2, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(1, "day").unix() + "']").first().click();
        if (this.caleran.globals.delayInputUpdate) {
            expect(this.caleran.config.startDate).toBe(null);
            expect(this.caleran.config.endDate).toBe(null);
        } else {
            this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
            this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-2, "day").unix() + "']").first().click();
        }
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should preserve continuousity when disabling days by function  (first value not selected state)", function () {
        this.input.caleran({
            disableDays: function (day) {
                return day.isSame(moment(), "day");
            },
            continuous: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(1, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });

    it("should preserve continuousity when disabling days by array (first value selected state)", function () {
        this.input.caleran({
            disabledRanges: [
                {
                    start: moment(),
                    end: moment().add(1, "day")
                }
            ],
            continuous: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-2, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(3, "day").unix() + "']").first().click();
        if (this.caleran.globals.delayInputUpdate) {
            expect(this.caleran.config.startDate).toBe(null);
            expect(this.caleran.config.endDate).toBe(null);
        } else {
            expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
            expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        }
        expect(this.caleran.input.is(":visible")).toBe(true);
    });

    it("should preserve continuousity when disabling days by array (first value not selected state)", function () {
        this.input.caleran({
            disabledRanges: [
                {
                    start: moment(),
                    end: moment().add(1, "day")
                }
            ],
            continuous: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(3, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });

    it("shouldn't change the datetime on the input until apply button is clicked", function () {
        this.input.caleran({
            showButtons: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(false);
        this.input.click();
        expect(this.caleran.input.is(":visible")).toBe(true);
        expect(this.caleran.config.startDate.format("L")).toBe(moment().format("L"));
        expect(this.caleran.config.endDate.format("L")).toBe(moment().format("L"));
        this.caleran.calendars.find("[data-value='" + moment({ day: 14 }).middleOfDay().unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment({ day: 16 }).middleOfDay().unix() + "']").first().click();
        expect(this.caleran.config.startDate.format("L")).toBe(moment({ day: 14 }).format("L"));
        expect(this.caleran.config.endDate.format("L")).toBe(moment({ day: 16 }).format("L"));
        expect(this.caleran.$elem.val()).not.toBe(this.caleran.config.startDate.format("L") + this.caleran.config.dateSeparator + this.caleran.config.endDate.format("L"));
        expect(this.caleran.$elem.val()).toBe(moment().format("L") + this.caleran.config.dateSeparator + moment().format("L"));
        expect(this.caleran.input.find(".caleran-apply").length).toEqual(1);
        this.caleran.input.find(".caleran-apply").click();
        expect(this.caleran.$elem.val()).toBe(this.caleran.config.startDate.format("L") + this.caleran.config.dateSeparator + this.caleran.config.endDate.format("L"));
        if (!this.caleran.globals.isMobile) {
            expect(this.caleran.container.is(":visible")).toBe(false);
        } else {
            expect(this.caleran.input.is(":visible")).toBe(false);
        }
    });
});
