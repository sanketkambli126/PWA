describe("initial testing inline", function () {
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
            },
            inline: true
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

    it("should have given an this.caleran object", function () {
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

    it("should have the dropdown visible", function () {
        expect($(".caleran-input").is(":visible")).toBe(true);
    });

    it("should have the custom date set on the this.input", function () {
        this.input.val(moment({ year: 2016, month: 2, day: 13, hour: 14, minute: 37 }).format(this.caleran.config.format) +
            this.caleran.config.dateSeparator +
            moment({ year: 2016, month: 4, day: 24, hour: 11, minute: 35 }).format(this.caleran.config.format));
        this.caleran.reDrawCalendars();
        expect(this.input.val()).toEqual(this.caleran.config.startDate.clone().format(this.caleran.config.format) + this.caleran.config.dateSeparator + this.caleran.config.endDate.clone().format(this.caleran.config.format));
    });

    it("should have the correct months visible and days selected on the calendar", function () {
        expect(this.element.find(".caleran-calendar").length).toEqual(this.caleran.config.calendarCount);
        expect(this.element.find(".caleran-calendar:first").data("month")).toEqual(this.caleran.config.startDate.month());
        expect(this.element.find(".caleran-calendar:last").data("month")).toEqual(this.caleran.config.startDate.clone().add(this.caleran.config.calendarCount - 1, "months").month());
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-selected")).toBe(true);
        expect(this.element.find(".caleran-day[data-value='" + this.caleran.config.endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-selected")).toBe(true);
    });

    it("should have the dropdown visible", function () {
        this.input.click();
        expect($(".caleran-input").is(":visible")).toBe(true);
    });

    it("should move to the next month with arrow click", function () {
        jasmine.clock().install();
        this.input.click();
        for (var i = 0; i < 12; i++) {
            var currentMonth = this.element.find(".caleran-calendar:first").data("month");
            this.element.find(".caleran-next").click();
            jasmine.clock().tick(101);
            var modifiedMonth = this.element.find(".caleran-calendar:first").data("month");
            expect(modifiedMonth === (currentMonth == 11 ? 0 : currentMonth + 1)).toBe(true);
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
        var days = $(".caleran-day");
        var startDay = $(days[Math.floor(Math.random() * days.length)]);
        var endDay = $(days[Math.floor(Math.random() * days.length)]);
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
});

describe("config tests inline", function () {
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
            calendarCount: 4,
            inline: true
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
            minDate: moment(),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.calendars.find("div[data-value='" + moment().subtract(1, "days").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should have maxdate effective", function () {
        this.input.caleran({
            maxDate: moment(),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.calendars.find("div[data-value='" + moment().add(1, "days").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should change the mindate with function", function () {
        this.input.caleran({
            minDate: moment(),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.setMinDate(moment().add(1, "days"));
        expect($(".caleran-input").is(":visible")).toBe(true);
        expect(this.caleran.calendars.find("div[data-value='" + moment().middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should change the maxdate with function", function () {
        this.input.caleran({
            maxDate: moment(),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.setMaxDate(moment().add(-1, "days"));
        expect($(".caleran-input").is(":visible")).toBe(true);
        expect(this.caleran.calendars.find("div[data-value='" + moment().middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
    });
    it("should have the start date and end date effective", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        var endDate = moment("25/05/1984", "DD/MM/YYYY");
        this.input.caleran({ startDate: startDate, endDate: endDate, inline: true });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.config.endDate.format("DD/MM/YYYY")).toEqual(moment("25/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toEqual(true);
    });
    it("should read the start date and end date from input", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        var endDate = moment("25/05/1984", "DD/MM/YYYY");
        this.input.val(startDate.format("DD/MM/YYYY") + " - " + endDate.format("DD/MM/YYYY"));
        this.input.caleran({
            format: "DD/MM/YYYY",
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.config.endDate.format("DD/MM/YYYY")).toEqual(moment("25/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toEqual(true);
    });
    it("should set the start date and end date via function", function () {
        var startDate = moment("24/05/1994", "DD/MM/YYYY");
        var endDate = moment("25/05/1995", "DD/MM/YYYY");
        var startDate2 = moment("13/03/1989 18:28", "DD/MM/YYYY");
        var endDate2 = moment("25/04/1989 07:27", "DD/MM/YYYY");
        this.input.val(startDate.format("DD/MM/YYYY") + " - " + endDate.format("DD/MM/YYYY"));
        this.input.caleran({
            format: "DD/MM/YYYY",
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.setStart(startDate2);
        this.caleran.setEnd(endDate2);
        this.caleran.setDisplayDate(startDate2);
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(startDate2.format("DD/MM/YYYY"));
        expect(this.caleran.config.endDate.format("DD/MM/YYYY")).toEqual(endDate2.format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate2.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate2.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + endDate2.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toEqual(true);
    });
    it("should read the start date and end date from div", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        var endDate = moment("25/05/1984", "DD/MM/YYYY");
        this.input.remove();
        this.input = $("<div class='caleran-test-input'></div>").appendTo("body");
        this.input.text(startDate.format("DD/MM/YYYY") + " - " + endDate.format("DD/MM/YYYY"));
        this.input.caleran({
            format: "DD/MM/YYYY",
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.config.endDate.format("DD/MM/YYYY")).toEqual(moment("25/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + endDate.clone().middleOfDay().unix() + "']").hasClass("caleran-end")).toEqual(true);
    });
    it("should have the start date effective on singledate", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        this.input.caleran({ startDate: startDate, singleDate: true, inline: true });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
    });
    it("should read the start date effective on singledate", function () {
        var startDate = moment("24/05/1984", "DD/MM/YYYY");
        this.input.val(startDate.format("DD/MM/YYYY"));
        this.input.caleran({ format: "DD/MM/YYYY", singleDate: true, inline: true });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.startDate.format("DD/MM/YYYY")).toEqual(moment("24/05/1984", "DD/MM/YYYY").format("DD/MM/YYYY"));
        expect(this.caleran.input.find(".caleran-calendar:first").data("month").toString()).toEqual(startDate.month().toString());
        expect(this.caleran.input.find(".caleran-day[data-value='" + startDate.clone().middleOfDay().unix() + "']").hasClass("caleran-start")).toEqual(true);
    });
    it("should respect the mindate on start date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            minDate: moment("24/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.minDate.inspect());
    });
    it("should respect the mindate on end date", function () {
        this.input.caleran({
            startDate: moment("20/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            minDate: moment("30/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.minDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.minDate.inspect());
    });
    it("should respect the maxdate on end date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            maxDate: moment("24/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
    });
    it("should respect the maxdate on start date", function () {
        this.input.caleran({
            startDate: moment("22/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            endDate: moment("28/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            maxDate: moment("20/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss"),
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(this.caleran.config.maxDate.inspect());
    });
    it("should swap the start and end date", function () {
        var startDate = moment("20/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss");
        var endDate = moment("15/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss");
        this.input.caleran({
            startDate: startDate,
            endDate: endDate,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.inspect()).toBe(endDate.inspect());
        expect(this.caleran.config.endDate.inspect()).toBe(startDate.inspect());
    });
    it("should swap the min and max date", function () {
        var startDate = moment("20/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss");
        var endDate = moment("15/11/2017 12:00:00", "DD/MM/YYYY HH:mm:ss");
        this.input.caleran({
            minDate: startDate,
            maxDate: endDate,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.minDate.inspect()).toBe(endDate.inspect());
        expect(this.caleran.config.maxDate.inspect()).toBe(startDate.inspect());
    });
    it("should ignore invalid start and end date", function () {
        this.input.val("testing");
        var startDate = moment.invalid();
        var endDate = moment.invalid();
        this.input.caleran({
            startDate: startDate,
            endDate: endDate,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.isSame(moment(), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment(), "day")).toBe(true);
    });
    it("should ignore invalid start date on single input", function () {
        this.input.val("testing");
        var startDate = moment.invalid();
        this.input.caleran({
            startDate: startDate,
            singleDate: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.input.click();
        expect(this.caleran.config.startDate.isSame(moment(), "day")).toBe(true);
    });
    it("should hide the calendar header", function () {
        this.input.caleran({
            showHeader: false,
            inline: true
        });
        this.caleran = this.input.data("caleran");

        expect($(".caleran-input").is(":visible")).toBe(true);
        expect(this.caleran.input.find(".caleran-header").length).toBe(1);
        expect(this.caleran.input.find(".caleran-header").is(":visible")).toBe(false);
    });
    it("should not add the calendar footer", function () {
        this.input.caleran({
            showFooter: false,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.find(".caleran-footer").length).toBe(0);
    });
    it("should start on Monday", function () {
        this.input.caleran({
            startOnMonday: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        var calendarStart = this.caleran.input.find(".caleran-calendar:first").find(".caleran-disabled, .caleran-day").first().attr("data-value");
        var calendarStartMoment = moment.unix(calendarStart);
        expect(calendarStartMoment.day()).toBe(1);
    });
    it("should start on Sunday", function () {
        this.input.caleran({
            startOnMonday: false,
            locale: 'tr',
            inline: true
        });
        this.caleran = this.input.data("caleran");
        var calendarStart = this.caleran.input.find(".caleran-calendar:first").find(".caleran-disabled, .caleran-day").first().attr("data-value");
        var calendarStartMoment = moment.unix(calendarStart);
        expect(calendarStartMoment.day()).toBe(0);
    });
    it("should start empty", function () {
        this.input.caleran({
            startEmpty: true,
            inline: true
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
    it("should open the month selector and select a month", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.input.find(".caleran-month-switch").click();
        expect(this.caleran.input.find(".caleran-month-selector").length).toEqual(1);
        expect(this.caleran.input.find(".caleran-month-selector").css("display")).toEqual("block");
        expect(this.caleran.input.find(".caleran-month-selector .caleran-ms-month:visible").length).toEqual(12);
        expect(this.caleran.input.find(".caleran-month-selector .caleran-ms-month.current").length).toEqual(1);
        this.caleran.input.find(".caleran-month-selector .caleran-ms-month:eq(3)").click();
        expect(this.caleran.input.find(".caleran-month-selector").length).toEqual(0);
        expect(this.caleran.input.find(".caleran-calendars .caleran-calendar:first").data("month")).toEqual(3);
    });
    it("shouldn't open the month selector when monthselector is disabled", function () {
        this.input.caleran({
            inline: true,
            enableMonthSwitcher: false
        });
        this.caleran = this.input.data("caleran");
        expect(this.input.find(".caleran-month-selector").length).toEqual(0);
    });
    it("should open the year selector and select a year", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.input.find(".caleran-year-switch").click();
        expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(1);
        expect(this.caleran.input.find(".caleran-year-selector").css("display")).toEqual("block");
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year:visible").length).toEqual(13); // 5 x 3 - 2 arrows
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year.current").length).toEqual(1);
        var year = this.caleran.input.find(".caleran-year-selector .caleran-ys-year:eq(3)");
        var yearNumber = year.data("year");
        year.click();
        expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(0);
        expect(this.caleran.globals.currentDate.year()).toEqual(yearNumber);
    });
    it("shouldn't open the year selector when year selector is disabled", function () {
        this.input.caleran({
            inline: true,
            enableYearSwitcher: false
        });
        this.caleran = this.input.data("caleran");
        expect(this.input.find(".caleran-year-selector").length).toEqual(0);
    });
    it("should open the year selector and change the pages backwards", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.input.find(".caleran-year-switch").click();
        expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(1);
        expect(this.caleran.input.find(".caleran-year-selector").css("display")).toEqual("block");
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year:visible").length).toEqual(13); // 5 x 3 - 2 arrows
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year.current").length).toEqual(1);
        var year = this.caleran.input.find(".caleran-year-selector .caleran-ys-year:first");
        var yearNumber = year.data("year");
        for (var i = 0; i < 5; i++) {
            this.caleran.input.find(".caleran-year-selector .caleran-ys-year-prev").click();
            expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(1);
            year = this.caleran.input.find(".caleran-year-selector .caleran-ys-year:first");
            expect(yearNumber).toEqual(+year.data("year") + 13);
            yearNumber = year.data("year");
        }
    });
    it("should open the year selector and change the pages forward", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        this.caleran.input.find(".caleran-year-switch").click();
        expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(1);
        expect(this.caleran.input.find(".caleran-year-selector").css("display")).toEqual("block");
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year:visible").length).toEqual(13); // 5 x 3 - 2 arrows
        expect(this.caleran.input.find(".caleran-year-selector .caleran-ys-year.current").length).toEqual(1);
        var year = this.caleran.input.find(".caleran-year-selector .caleran-ys-year:first");
        var yearNumber = year.data("year");
        for (var i = 0; i < 5; i++) {
            this.caleran.input.find(".caleran-year-selector .caleran-ys-year-next").click();
            expect(this.caleran.input.find(".caleran-year-selector").length).toEqual(1);
            year = this.caleran.input.find(".caleran-year-selector .caleran-ys-year:first");
            expect(yearNumber).toEqual(+year.data("year") - 13);
            yearNumber = year.data("year");
        }
    });
    it("should apply the clicked range", function () {
        this.input.caleran({
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.config.ranges.length > 0).toEqual(true);
        this.caleran.input.find(".caleran-range:eq(1)").click();
        expect(this.caleran.input.find(".caleran-day[data-value='" + this.caleran.config.ranges[1].startDate.middleOfDay().unix() + "']").hasClass("caleran-start")).toBe(true);
        expect(this.caleran.input.find(".caleran-day[data-value='" + this.caleran.config.ranges[1].endDate.middleOfDay().unix() + "']").hasClass("caleran-end")).toBe(true);
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
            },
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should disable days by array", function () {
        this.input.caleran({
            disabledRanges: [
                {
                    start: moment().add(1, "day"),
                    end: moment().add(3, "day")
                }
            ],
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().add(1, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().add(2, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.calendars.find("[data-value='" + moment().add(3, "day").middleOfDay().unix() + "']").hasClass("caleran-disabled")).toBe(true);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should preserve continuousity when disabling days by function", function () {
        this.input.caleran({
            disableDays: function (day) {
                return day.isSame(moment(), "day");
            },
            continuous: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-2, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(1, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        expect(this.caleran.input.is(":visible")).toBe(true);
    });
    it("should preserve continuousity when disabling days by function  (first value not selected state)", function () {
        this.input.caleran({
            disableDays: function (day) {
                return day.isSame(moment(), "day");
            },
            continuous: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(1, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("should preserve continuousity when disabling days by array (first value selected state)", function () {
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
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-2, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.isSame(moment().middleOfDay().add(-2, "day"), "day")).toBe(true);
        expect(this.caleran.config.endDate.isSame(moment().middleOfDay().add(-1, "day"), "day")).toBe(true);
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(-1, "day").unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment().middleOfDay().add(3, "day").unix() + "']").first().click();
        expect(this.caleran.config.startDate.format('L')).toBe(moment().middleOfDay().add(-2, "day").format('L'));
        expect(this.caleran.config.endDate.format('L')).toBe(moment().middleOfDay().add(-1, "day").format('L'));
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
    it("shouldn't show the buttons on inline", function () {
        this.input.caleran({
            showButtons: true,
            inline: true
        });
        this.caleran = this.input.data("caleran");
        expect(this.caleran.input.is(":visible")).toBe(true);
        expect(this.caleran.config.startDate.format("L")).toBe(moment().format("L"));
        expect(this.caleran.config.endDate.format("L")).toBe(moment().format("L"));
        this.caleran.calendars.find("[data-value='" + moment({ day: 14 }).middleOfDay().unix() + "']").first().click();
        this.caleran.calendars.find("[data-value='" + moment({ day: 16 }).middleOfDay().unix() + "']").first().click();
        expect(this.caleran.config.startDate.format("L")).toBe(moment().date(14).format("L"));
        expect(this.caleran.config.endDate.format("L")).toBe(moment().date(16).format("L"));
        expect(this.caleran.input.find(".caleran-apply").length).toEqual(0);
        expect(this.caleran.$elem.val()).toBe(this.caleran.config.startDate.format("L") + this.caleran.config.dateSeparator + this.caleran.config.endDate.format("L"));
        expect(this.caleran.input.is(":visible")).toBe(true);
    })
});
