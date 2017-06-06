if (!nezaci.services.calendar) {
    nezaci.services.calendar = {};
}

nezaci.services.calendar.getTodaysCalendarList = function (date, onSuccess, onError) {
    var url = "api/Calendar/" + date;

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: onSuccess,
        error: onError,
        type: 'Get'
    }

    $.ajax(url, settings);
};