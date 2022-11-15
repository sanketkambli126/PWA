describe('hotel mode tests -', function () {
    beforeEach(function () {
        this.initcalled = false;
        if ($('.caleran-test-input').length > 0) {
            $('.caleran-test-input').data('caleran').destroy();
            $('.caleran-test-input').remove();
        }
        this.input = $(
            "<input type='text' class='caleran-test-input' />"
        ).appendTo('body');
        var that = this;
        var disabled = {
            '2019-02-13': 0,
            '2019-03-13': 0,
            '2019-03-14': 0,
            '2019-03-15': 0,
            '2019-03-24': 0,
            '2019-03-27': 0,
            '2019-03-28': 0,
        };
        this.input.caleran({
            oninit: function (instance) {
                instance.setDisplayDate(moment('01-02-2019', 'DD-MM-YYYY'));
                that.initcalled = true;
            },
            disabledRanges: [
                {
                    start: moment('07-02-2019', 'DD-MM-YYYY'), // single day unavailable
                    end: moment('07-02-2019', 'DD-MM-YYYY'),
                },
                {
                    start: moment('14-02-2019', 'DD-MM-YYYY'),
                    end: moment('15-02-2019', 'DD-MM-YYYY'),
                },
                {
                    start: moment('21-02-2019', 'DD-MM-YYYY'),
                    end: moment('23-02-2019', 'DD-MM-YYYY'),
                },
            ],
            disableDays: function (day) {
                return disabled[day.format('YYYY-MM-DD')] == 0 ? true : false;
            },
            isHotelBooking: true,
            startEmpty: true,
            continuous: true,
            inline: true,
            minSelectedDays: 1,
        });
        this.caleran = this.input.data('caleran');
        this.element = this.caleran.input;
    });
    afterEach(function () {
        this.input = $('.caleran-test-input');
        this.input.data('caleran').destroy();
        this.caleran = null;
        this.input.remove();
    });
    // zero base test

    it('should select any range', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('01-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('05-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('01-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('05-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });

    // first base test array (7.2.2019)
    it('should select disabled range end as start date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('08-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('10-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('08-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('10-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
    it("shouldn't select single disabled range as end date", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('07-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('08-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("shouldn't select over single disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('06-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('09-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });

    // second base test array (13,14,15.02)
    it("shouldn't select over two days disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('11-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it('should fill the consecutive disabled ranges with disabled callbacks', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        expect(
            this.caleran.calendars
                .find(
                    "[data-value='" +
                        moment('14-02-2019', 'DD-MM-YYYY')
                            .middleOfDay()
                            .unix() +
                        "']"
                )
                .hasClass('caleran-disabled')
        ).toBe(true);
    });
    it("shouldn't select over two days disabled range", function () {
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('13-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it('should select after two days disabled range', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('17-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('16-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('17-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });

    // third base test array (21,22,23.02)
    it("shouldn't select over multiple days disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('21-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('24-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("shouldn't select over multiple days disabled range #2", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('20-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('25-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it('should select disabled start as end date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('19-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('21-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('19-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('21-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
    it('should select disabled end as start date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('24-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('26-02-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('24-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('26-02-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });

    // first base test callback (24.3.2019)
    it('should select disabled range end as start date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('25-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('26-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('25-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('26-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
    it("shouldn't select single disabled range as end date", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('24-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('25-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("shouldn't select over single disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('24-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('26-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });

    // second base test callback (24,27,28.03)
    it('should select between two disabled ranges', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('25-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('27-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('25-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('27-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
    it("shouldn't select over two days disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('27-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('29-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("shouldn't select over two days disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('26-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('29-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it('should select after two days disabled range', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('29-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('30-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('29-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('30-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });

    // third base test callback (21,22,23.02)
    it("shouldn't select over multiple days disabled range", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('13-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it("shouldn't select over multiple days disabled range #2", function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('12-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate).toBe(null);
        expect(this.caleran.config.endDate).toBe(null);
    });
    it('should select disabled start as end date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('11-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('13-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('11-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('13-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
    it('should select disabled end as start date', function () {
        this.caleran = this.input.data('caleran');
        expect(this.caleran.input.is(':visible')).toBe(true);
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('16-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        this.caleran.calendars
            .find(
                "[data-value='" +
                    moment('18-03-2019', 'DD-MM-YYYY').middleOfDay().unix() +
                    "']"
            )
            .first()
            .click();
        expect(this.caleran.config.startDate?.inspect()).toBe(
            moment('16-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
        expect(this.caleran.config.endDate?.inspect()).toBe(
            moment('18-03-2019', 'DD-MM-YYYY').middleOfDay()?.inspect()
        );
    });
});
