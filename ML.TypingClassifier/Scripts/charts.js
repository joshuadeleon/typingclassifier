

var chart = c3.generate({
    bindto: '#chart',
    data: {
        xs: {
            'Total Time': 'Total Time X',
            'Spaces': 'Spaces X',
            'Deletion Key Frequency': 'Deletion Key Frequency X',
            'Keypress Duration': 'Keypress Duration X',
            'Words Per Minute': 'Words Per Minute X',
        },
        // iris data from R
        columns: [
            ["Total Time", 3.5],
            ["Total Time X", 1.5],
            ["Spaces", 3.2],
            ["Spaces X", 5.7],
            ["Deletion Key Frequency", 0.2],
            ["Deletion Key Frequency X", 4.2],
            ["Keypress Duration", 1.4],
            ["Keypress Duration X", 0.4],
            ["Words Per Minute", 3.1],
            ["Words Per Minute X", 2.7],
        ],
        type: 'scatter'
    },
    axis: {
        x: {
            label: 'totalTime.Width',
            tick: {
                fit: false
            }
        },
        y: {
            label: 'spaces.Width'
        }
    }
});
