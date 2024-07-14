function drawChart(dispositivoId) {

    $.ajax(
        {
            url: '/Home/DeviceDay',
            dataType: "json",
            data: { id: dispositivoId },
            type: "GET",
            success: function (jsonData) {
                var data = new google.visualization.DataTable(jsonData);
                var options = { chart: { title: 'Most recent 24 hours of measurements' } };
                var chart = new google.charts.Line(document.getElementById('chart_div'));
                chart.draw(data, options);
            }
        });

    return false;
}


