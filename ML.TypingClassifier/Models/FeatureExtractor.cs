using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.TypingClassifier.Models
{
    public class FeatureExtractor
    {
        private readonly IList<Func<Timeline, double>> _extractors;

        public FeatureExtractor(IList<Func<Timeline, double>> extractors)
        {
            _extractors = extractors;
        }

        public double[] Run(Timeline timeline)
        {
            var features = _extractors.Select(f => f(timeline)).ToArray();
            return features;
        }

        public static double[] Default(Timeline timeline)
        {
            var def = new List<Func<Timeline, double>>
            {
                Extractors.WPM, Extractors.BackspaceCount, Extractors.DeleteCount,
                Extractors.AverageKeyPressDuration,
                Extractors.AverageTimeBetweenKeyStrokes
            };

            var fe = new FeatureExtractor(def);
            return fe.Run(timeline);
        }
    }

    public static class Extractors
    {
        public static double WPM(Timeline timeline)
        {
            var spaces = timeline.Events.Where(e => e.Key == " ").Count();
            var totalTime = timeline.Events.Last().Timestamp;
            return (spaces + 1) / totalTime;
        }

        public static double BackspaceCount(Timeline timeline)
        {
            return KeyCounter(timeline, "Backspace");
        }

        public static double DeleteCount(Timeline timeline)
        {
            return KeyCounter(timeline, "Delete");
        }

        public static double AverageKeyPressDuration(Timeline timeline)
        {
            return Average(timeline.Events.Select(e => e.Duration));
        }

        public static double AverageTimeBetweenKeyStrokes(Timeline timeline)
        {
            var totalKeyTime = timeline.Events.Sum(e => e.Duration);
            var totalTime = timeline.Events.Last().Timestamp;
            return (totalTime - totalKeyTime) / (timeline.Events.Count());
        }

        private static int KeyCounter(Timeline timeline, string key)
        {
            return timeline.Events.Where(e => e.Key == key).Count();
        }

        private static double Average(IEnumerable<double> values)
        {
            int n = values.Count();
            if (n == 0) return 0.0;
            return ((double)values.Sum()) / n;
        }
        
    }
}