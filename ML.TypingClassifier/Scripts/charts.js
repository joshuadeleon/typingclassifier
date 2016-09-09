$(function () {
	var points
	  , features
	  , plot
	  , xaxis
	  , yaxis
	  , oldxaxis
	  , oldyaxis

	$.getJSON('/sink/points', function (data) {
		points = data;
		features = {
			wpm: ['WPM'].concat(points[0].Values),
			backspaces: ['Backspaces'].concat(points[1].Values),
			deletes: ['Deletes'].concat(points[2].Values),
			averageKeyPressDuration: ['AverageKeyPressDuration'].concat(points[3].Values),
			averageTimeBetweenKeystrokes: ['AverageTimeBetweenKeystrokes'].concat(points[4].Values)
		};
		console.log(data);
		console.log(features);
		xaxis = features.wpm;
		yaxis = features.averageKeyPressDuration
		plot = render(xaxis, yaxis);
	});

	function render(x, y) {
		var xlabel = x[0];
		var ylabel = y[0];

		return c3.generate({
			bindto: '#chart',
			data: {
				columns: [x, y],
				type: 'scatter',
				x: xlabel,
			},
			axis: {
				x: {
					label: xlabel,
					tick: {
						fit: true
					}
				},
				y: {
					label: ylabel
				}
			}
		});
	}

	//	Redraw the scatter plot
	function redraw(xaxis, yaxis, oldxaxis, oldyaxis) {
		var idsToRemove = [];
		if (oldxaxis !== null || oldxaxis !== undefined) {
			idsToRemove.push(oldxaxis[0]);
		}

		if (oldyaxis != null || oldyaxis !== undefined) {
			idsToRemove.push(oldyaxis[0]);
		}

		plot.axis.labels({ x: xaxis[0] });
		plot.axis.labels({ y: yaxis[0] });
		

		plot.unload({ ids: idsToRemove });
		plot.load({ columns: [xaxis, yaxis], x: xaxis[0] });
		
	}

	//	Redraw when x-axis changes
	$('#chart-x-axis').on('change', function (event) {
		var valueSelected = this.value;
		
		oldxaxis = xaxis;
		xaxis = features[valueSelected];

		redraw(xaxis, yaxis, oldxaxis, oldyaxis);
	});

	//	Redraw when y-axis changes
	$('#chart-y-axis').on('change', function (event, selector, data) {
		var valueSelected = this.value;

		oldyaxis = yaxis;
		yaxis = features[valueSelected];

		redraw(xaxis, yaxis, oldxaxis, oldyaxis);
	});
});

