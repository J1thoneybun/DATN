
var chartDom = document.getElementById('main');
var myChart = echarts.init(chartDom);
var option;

option = {
    title: {
        text: 'Thống kê số lịch đặt khám theo thời gian'
    },
    tooltip: {},
    xAxis: {
        type: 'category',
        data: ['NgayDat', 'DangXuLi', 'DaXacNhan', 'DaTuVan', 'DaHuy', 'HoanThanh', 'TongSo']
    },
    yAxis: {
        type: 'value'
    },
    series: [
        {
            data: [2, 4, 6, 8, 10, 12, 14],
            type: 'line'
        }
    ]
};

option && myChart.setOption(option);

