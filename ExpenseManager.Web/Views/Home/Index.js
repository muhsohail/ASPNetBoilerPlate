$(function () {
    var myResult;
    //Widgets count
    $('.count-to').countTo();

    //Sales count to
    $('.sales-count-to').countTo({
        formatter: function (value, options) {
            return '$' + value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ' ').replace('.', ',');
        }
    });
    _getExpenseDetailByMonth();


});

var realtime = 'off';
function initRealTimeChart(myResult) {
    //Real time ==========================================================================================
    var plot = $.plot('#real_time_chart', [myResult], {
        series: {
            shadowSize: 0,
            color: 'rgb(0, 188, 212)'
        },
        grid: {
            borderColor: '#f3f3f3',
            borderWidth: 1,
            tickColor: '#f3f3f3'
        },
        lines: {
            fill: true
        },
        yaxis: {
            min: 0,
            max: 50000
        },
        xaxis: {
            min: 0,
            max: 12
        }
    });

    function updateRealTime() {
        getRandomData();
       
    }

    updateRealTime();

    $('#realtime').on('change', function () {
        realtime = this.checked ? 'on' : 'off';
        updateRealTime();
    });
    //====================================================================================================
}

function initSparkline() {
    $(".sparkline").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}

function initDonutChart() {
    var items = [];
    $.ajax({
        url: abp.appPath + 'Home/GetPensionDetails',
        type: "Get",
        dataType: "json",
        success: function (result) {
            for (var i = 0; i < result.result.length; ++i) {
                items.push({ label: result.result[i].stakeholderName, value: result.result[i].amountUsed})
            }

            Morris.Donut({
                element: 'donut_chart',
                data: items,
                colors: ['rgb(233, 30, 99)', 'rgb(0, 188, 212)', 'rgb(255, 152, 0)', 'rgb(0, 150, 136)', 'rgb(96, 125, 139)'],
                formatter: function (y) {
                    return y //return y + '%'
                }
            });
        },
        error: function (err) {
            showToastrError(err.statusText, " Error");
        }
    });
}

var data = [], totalPoints = 12;

function getRandomData() {


    if (data.length > 0) data = data.slice(1);

    while (data.length < totalPoints) {
        var prev = data.length > 0 ? data[data.length - 1] : 50,
            y = prev + Math.random() * 10 - 5;
        if (y < 0) {
            y = 0;
        }
        else if (y > 12) {
            y = 12;
        }

        data.push(y);
    }
    var res = [];

    var res = [];

    $.ajax({
        url: abp.appPath + 'Home/GetExpenseDetailByMonth',
        type: "Get",
        dataType: "json",
        success: function (result) {
            for (var i = 0; i < result.result.length; ++i) {
                res.push([i, result.result[i].amount]);
            }
            return res;
        },
        error: function (err) {
            showToastrError(err.statusText, " Error");
        }
    });

    //for (var i = 0; i < data.length; ++i) {
    //    res.push([i, data[i]]);
    //}

    //return res;
}

function _getExpenseDetailByMonth(value, Create) {

    var myResult = [];

    $.ajax({
        url: abp.appPath + 'Home/GetExpenseDetailByMonth',
        type: "Get",
        dataType: "json",
        success: function (result) {
            for (var i = 0; i < result.result.length; ++i) {
                myResult.push([i, result.result[i].amount]);
            }
            initRealTimeChart(myResult);
            initDonutChart();
            initSparkline();
        },
        error: function (err) {
            showToastrError(err.statusText, " Error");
        }
    });
}