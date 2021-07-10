/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
//var xhttp = new XMLHttpRequest();
//if (window.XMLHttpRequest) {
//    // code for modern browsers

//    xmlhttp = new XMLHttpRequest();
//} else {
//    // code for old IE browsers
//    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
//}
var currentDate = new Date();
var lastPickedDate = null;
var setEventInfoDate = document.getElementById("DateDet");
var ReadMore = document.getElementById("EventInfo")
if (lastPickedDate == null) {
    setEventInfoDate.innerHTML = currentDate.toDateString();
}


var vanillaCalendar = {


    month: document.querySelectorAll('[data-calendar-area="month"]')[0],
    next: document.querySelectorAll('[data-calendar-toggle="next"]')[0],
    previous: document.querySelectorAll('[data-calendar-toggle="previous"]')[0],
    label: document.querySelectorAll('[data-calendar-label="month"]')[0],
    activeDates: null,
    activeEvents: null,
    date: new Date(),
    todaysDate: new Date(),



    createListeners: function () {
        var _this = this
        this.next.addEventListener('click', function () {
            _this.clearCalendar()
            var nextMonth = _this.date.getMonth() + 1
            _this.date.setMonth(nextMonth)
            _this.createMonth()
        })
        // Clears the calendar and shows the previous month
        this.previous.addEventListener('click', function () {
            _this.clearCalendar()
            var prevMonth = _this.date.getMonth() - 1
            _this.date.setMonth(prevMonth)
            _this.createMonth()
        })
    },

    init: function (options) {
        this.options = options
        this.date.setDate(1)
        this.createMonth()
        this.createListeners()
    },


    createDay: function (num, day, year) {
        var newDay = document.createElement('div')
        var dateEl = document.createElement('span')
        dateEl.innerHTML = num


        newDay.className = 'vcal-date'
        newDay.setAttribute('data-calendar-date', this.date)


        // if it's the first day of the month
        if (num === 1) {
            if (day === 0) {
                newDay.style.marginLeft = (6 * 14.28) + '%'
            } else {
                newDay.style.marginLeft = ((day - 1) * 14.28) + '%'
            }
        }


        if (num == 25) {
            if (this.options.disablePastDays && this.date.getTime() <= this.todaysDate.getTime() - 1) {
                newDay.classList.add('vcal-date--disabled')
            } else {
                newDay.classList.add('vcal-date--active')
                newDay.setAttribute('data-calendar-status', 'active')
            }
            newDay.classList.add('vcal-date--today')
        }
        if (num == 14) {
            if (this.options.disablePastDays && this.date.getTime() <= this.todaysDate.getTime() - 1) {
                newDay.classList.add('vcal-date--disabled')
            } else {
                newDay.classList.add('vcal-date--active')
                newDay.setAttribute('data-calendar-status', 'active')
            }
            newDay.classList.add('vcal-date--today')
        }
        if (num == 2) {
            if (this.options.disablePastDays && this.date.getTime() <= this.todaysDate.getTime() - 1) {
                newDay.classList.add('vcal-date--disabled')
            } else {
                newDay.classList.add('vcal-date--active')
                newDay.setAttribute('data-calendar-status', 'active')
            }
            newDay.classList.add('vcal-date--today')
        }
        if (num == 31) {
            if (this.options.disablePastDays && this.date.getTime() <= this.todaysDate.getTime() - 1) {
                newDay.classList.add('vcal-date--disabled')
            } else {
                newDay.classList.add('vcal-date--active')
                newDay.setAttribute('data-calendar-status', 'active')
            }
            newDay.classList.add('vcal-date--today')
        }
        newDay.appendChild(dateEl)
        this.month.appendChild(newDay)
    },

    eventClicked: function () {

        //}
    },

    createMonth: function () {
        var currentMonth = this.date.getMonth()
        while (this.date.getMonth() === currentMonth) {
            this.createDay(
              this.date.getDate(),
              this.date.getDay(),
              this.date.getFullYear()
            )
            this.date.setDate(this.date.getDate() + 1)
        }
        // while loop trips over and day is at 30/31, bring it back
        this.date.setDate(1)
        this.date.setMonth(this.date.getMonth() - 1)

        this.label.innerHTML =
          this.monthsAsString(this.date.getMonth()) + ' ' + this.date.getFullYear()
        this.dateClicked()
    },
    dateClicked: function () {

        var _this = this
        this.activeDates = document.querySelectorAll(
          '[data-calendar-status="active"]'
        )
        for (var i = 0; i < this.activeDates.length; i++) {
            this.activeDates[i].addEventListener('click', function (event) {
                var picked = document.querySelectorAll(
                  '[data-calendar-label="org"]'
                )[0]
                lastPickedDate = this.dataset.calendarDate.substring(0, 15);

                setEventInfoDate.innerHTML = lastPickedDate.toString();

                if (lastPickedDate.includes("31")) {
                    ReadMore.innerHTML = "<a href='' target='_blank'></a>";
                    picked.innerHTML = "Fish Hunters Trophy";
                    var pickedDet = document.querySelectorAll(
                     '[data-calendar-label="OrgName"]'
                   )[0]
                    pickedDet.innerHTML = "Mabatho";
                    var picked2 = document.querySelectorAll(
                  '[data-calendar-label="OrgLoc"]'
                )[0]
                    picked2.innerHTML = "Boskop Dam";
                    var picked3 = document.querySelectorAll(
                  '[data-calendar-label="OrgDetails"]'
                )[0]
                    picked3.innerHTML = "mmabathom@dws.gov.za";
                    var picked4 = document.querySelectorAll(
    '[data-calendar-label="OrgCellNumber"]'
  )[0]
                    picked4.innerHTML = "012-546-7523";
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                }
                if (lastPickedDate.includes("02")) {
                    ReadMore.innerHTML = "<a href='' target='_blank'></a>";
                    var pickedDet = document.querySelectorAll(
                       '[data-calendar-label="OrgName"]'
                     )[0]
                    pickedDet.innerHTML = "Francois";
                    var picked2 = document.querySelectorAll(
                  '[data-calendar-label="OrgLoc"]'
                )[0]
                    picked2.innerHTML = "Vaal Dam";
                    var picked3 = document.querySelectorAll(
                  '[data-calendar-label="OrgDetails"]'
                )[0]
                    picked3.innerHTML = "vanaswegenfm@dws.gov.za";

                    //this.classList.add('vcal-date--selected')
                    picked.innerHTML = "Public Education Programme"
                    var picked4 = document.querySelectorAll(
    '[data-calendar-label="OrgCellNumber"]'
  )[0]
                    picked4.innerHTML = "065-456-9874";
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                }
                if (lastPickedDate.includes("14")) {
                    ReadMore.innerHTML = "<a href='' target='_blank'></a>";
                    picked.innerHTML = "Dam Diving competition"
                    var pickedDet = document.querySelectorAll(
                '[data-calendar-label="OrgName"]'
              )[0]
                    pickedDet.innerHTML = "Kampion";
                    var picked2 = document.querySelectorAll(
                  '[data-calendar-label="OrgLoc"]'
                )[0]
                    picked2.innerHTML = "Vaal Dam";
                    var picked3 = document.querySelectorAll(
                  '[data-calendar-label="OrgDetails"]'
                )[0]
                    picked3.innerHTML = "kampion@dws.gov.za";
                    var picked4 = document.querySelectorAll(
         '[data-calendar-label="OrgCellNumber"]'
       )[0]
                    picked4.innerHTML = "012-116-9999";
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                }
                if (lastPickedDate.includes("25")) {
                    ReadMore.innerHTML = "<a href='' target='_blank'></a>";
                    picked.innerHTML = "NATIONAL WATER WEEK 2018"

                    ReadMore.innerHTML = "<a href='http://www.dwa.gov.za/events/NationalWaterWeek/default.aspx' target='_blank'>[Read More]</a>";
                    var pickedDet = document.querySelectorAll(
                      '[data-calendar-label="OrgName"]'
                    )[0]
                    pickedDet.innerHTML = "Jan";
                    var picked2 = document.querySelectorAll(
                  '[data-calendar-label="OrgLoc"]'
                )[0]
                    picked2.innerHTML = "Katse Dam";
                    var picked3 = document.querySelectorAll(
                  '[data-calendar-label="OrgDetails"]'
                )[0]
                    picked3.innerHTML = "jan@dws.gov.za";
                    _this.removeActiveClass()
                    var picked4 = document.querySelectorAll(
               '[data-calendar-label="OrgCellNumber"]'
             )[0]
                    picked4.innerHTML = "012-115-4678";
                    _this.removeActiveClass()
                    //this.classList.add('vcal-date--selected')
                }

                //var activeEvent = document.getElementById("EventInfo");
                //for (var i = 0; i < this.activeDates.length; i++) {
                //activeEvent.addEventListener('click', function (event) {
                //    var pickedDate = lastPickedDate;
                //    setEventInfoDate.innerHTML = pickedDate.toString();
                //    if (pickedDate.includes("14")) {
                //          var picked = document.querySelectorAll(
                //          '[data-calendar-label="OrgName"]'
                //        )[0]                
                //          picked.innerHTML = "Kampion";
                //        var picked2 = document.querySelectorAll(
                //      '[data-calendar-label="OrgLoc"]'
                //    )[0]
                //        picked2.innerHTML = "Vaal Dam";
                //        var picked3 = document.querySelectorAll(
                //      '[data-calendar-label="OrgDetails"]'
                //    )[0]
                //        picked3.innerHTML = "kampion@dws.gov.za";
                //        _this.removeActiveClass()
                //              //this.classList.add('vcal-date--selected')
                //    }
                //    if (pickedDate.includes("31")) {
                //        var picked = document.querySelectorAll(
                //        '[data-calendar-label="OrgName"]'
                //      )[0]
                //        picked.innerHTML = "Mabatho";
                //        var picked2 = document.querySelectorAll(
                //      '[data-calendar-label="OrgLoc"]'
                //    )[0]
                //        picked2.innerHTML = "Boskop Dam";
                //        var picked3 = document.querySelectorAll(
                //      '[data-calendar-label="OrgDetails"]'
                //    )[0]
                //        picked3.innerHTML = "mmabathom@dws.gov.za";
                //        _this.removeActiveClass()
                //        //this.classList.add('vcal-date--selected')
                //    }
                //    if (pickedDate.includes("02")) {
                //        var picked = document.querySelectorAll(
                //        '[data-calendar-label="OrgName"]'
                //      )[0]
                //        picked.innerHTML = "Francois";
                //        var picked2 = document.querySelectorAll(
                //      '[data-calendar-label="OrgLoc"]'
                //    )[0]
                //        picked2.innerHTML = "Vaal Dam";
                //        var picked3 = document.querySelectorAll(
                //      '[data-calendar-label="OrgDetails"]'
                //    )[0]
                //        picked3.innerHTML = "vanaswegenfm@dws.gov.za";
                //        _this.removeActiveClass()
                //        //this.classList.add('vcal-date--selected')
                //    }
                //    if (pickedDate.includes("25")) {
                //        var picked = document.querySelectorAll(
                //        '[data-calendar-label="OrgName"]'
                //      )[0]
                //        picked.innerHTML = "Jan";
                //        var picked2 = document.querySelectorAll(
                //      '[data-calendar-label="OrgLoc"]'
                //    )[0]
                //        picked2.innerHTML = "Katse Dam";
                //        var picked3 = document.querySelectorAll(
                //      '[data-calendar-label="OrgDetails"]'
                //    )[0]
                //        picked3.innerHTML = "jan@dws.gov.za";
                //        _this.removeActiveClass()
                //        //this.classList.add('vcal-date--selected')
                //    }
                //    lastPickedDate = null;
                //    })

                _this.removeActiveClass()
                //this.classList.add('vcal-date--selected')
            })
        }
        this.eventClicked();
    },
    monthsAsString: function (monthIndex) {
        return [
          'January',
          'Febuary',
          'March',
          'April',
          'May',
          'June',
          'July',
          'August',
          'September',
          'October',
          'November',
          'December'
        ][monthIndex]
    },

    clearCalendar: function () {
        vanillaCalendar.month.innerHTML = ''
    },

    removeActiveClass: function () {
        for (var i = 0; i < this.activeDates.length; i++) {
            this.activeDates[i].classList.remove('vcal-date--selected')
        }
    }
}
