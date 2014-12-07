//$("").echarts({ query: { id: 1, name: 'zxh' }, settings: { onclick: function () { } } });

//data 中会有:query,settings,echarts,labels,options
(function ($) {
    $.fn.echarts = function (arg) {
        var $this = $(this);
        var method = arguments[0];

        if (typeof (method) == 'string') {

            return $this.data(method);
        } else if (typeof arg == 'object') {
            var init = !$this.data('query');
            if (arg.query)
                $this.data('query', arg.query);
            var settings = $.extend({}, $.fn.echarts.defaults, arg.settings);
            $this.data('settings', settings);

            if (init) {//chart初始化  
                echartsInit.apply(this); //不应用设置
                return $this;
            }
            if (!init && arg.query) {//重新请求data数据
                echartsReload.apply(this);
                return $this;
            }
            if (arg.settings) {
                echartsApply.apply(this);
            }
        }

        return $this;
    };
    function echartsApply() {
        var $this = $(this);

        var echarts = $this.data('echarts');
        if (echarts && echarts.chart && echarts.config) {
            if ($this.data('settings').onclick)
                echarts.chart.on(echarts.config.EVENT.CLICK, $this.data('settings').onclick);
            window.onresize = echarts.chart.resize;
        }
    }
    function echartsInit() {
        var $this = $(this);

        var settings = $this.data('settings');
        require.config({
            packages: [{
                name: 'echarts',
                location: settings.echarts_path,
                main: 'echarts'
            }, {
                name: 'zrender',
                location: settings.zrender_path,
                main: 'zrender'
            }]
        });

        var chartOptions = _getOption.apply(this);
        //$this.data('options', chartOptions);
        require(
              ['echarts', 'echarts/chart/' + settings.chartType],
              function (ec) {
                  var echarts = { echarts: ec, chart: null, config: null };
                  echarts.chart = ec.init($this[0]);
                  echarts.chart.setOption(chartOptions);
                  echarts.config = require('echarts/config');
                  $this.data('echarts', echarts);
                  echartsApply.apply($this);
              });
        return $this;
    }
    function _getOption() {
        var $this = $(this);
        var option = {};
        var parameters = $.extend({}, $this.data('query'), { chartType: $this.data('settings').chartType });
        $.ajax({
            type: "post",
            dataType: "text",
            data: parameters,
            async: false,
            url: $this.data('settings').service,
            success: function (data) {
                //{ title: { text: '测试分析', x: 'right', y: 'bottom' }, legend: { data: ['测试2', '测试1', '测试3', '测试44'] }, series: [{ name: '测试2', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: [2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 14, 4, 2, 2, 2, 2, 2, 2, 2, 2] }, { name: '测试1', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: [1, 1, 1, 1, 1, 1, 1, 1, 7, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1] }, { name: '测试3', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: ['-', 2, 1, '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'] }, { name: '测试44', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: ['-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', 2, 1] }], xAxis: [{ type: 'category', data: ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23'] }], yAxis: [{ type: 'value' }] }
                var datajs = data; //"{   tooltip : { show: true,   axisPointer : {type : 'shadow'  },trigger: 'item'          },  title: { text: '测试分析', x: 'right', y: 'bottom' }, legend: { data: ['测试2', '测试1', '测试3', '测试44'] }, series: [{ name: '测试2', type: 'bar', itemStyle: {  barWidth: 40,    normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: [2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 14, 4, 2, 2, 2, 2, 2, 2, 2, 2] }, { name: '测试1', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: [1, 1, 1, 1, 1, 1, 1, 1, 7, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1] }, { name: '测试3', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: ['-', 2, 1, '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'] }, { name: '测试44', type: 'bar', itemStyle: { normal: { label: { show: true, position: 'inside' } } }, stack: '总数', data: ['-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', 2, 1] }], xAxis: [{ type: 'category', data: ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23'] }], yAxis: [{ type: 'value' }] }";
                if (data) {
                    option = eval('(' + datajs + ')');
                    if (option.labels)
                        $this.data('labels', option.labels);
                    option = option.options;
                    $this.data('options', option);

                }
            }
        });
        return option;
    };
    function echartsReload() {
        var $this = $(this);

        var chartOptions = _getOption.apply(this);
        var echarts = $this.data('echarts');
        if (echarts) {
            if (echarts.chart && echarts.chart.dispose) {
                echarts.chart.dispose();
                echarts.chart = echarts.echarts.init($this[0]);
                echarts.chart.setOption(chartOptions);
                echartsApply.apply($this);
            }
        }
    }
    //arguments
    $.fn.echarts.defaults = {
        service: 'MobileService.ashx',
        echarts_path: 'Component/echarts-2.1.9/src',
        zrender_path: 'Component/zrender-master/src',
        chartType: 'bar'
    };

})(jQuery);
 