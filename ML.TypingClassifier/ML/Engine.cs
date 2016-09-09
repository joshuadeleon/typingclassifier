﻿using Accord.MachineLearning;
using ML.TypingClassifier.Models;
using System;
using System.Linq;

namespace ML.TypingClassifier.ML
{
    public sealed class Engine
    {
        private static readonly Lazy<Engine> Thunk =
            new Lazy<Engine>(() => new Engine());

        public static Engine Instance { get { return Thunk.Value; } }

        private readonly DataAccess _data;
        private int _size;
        private double[][] _matrix;
        
        private Engine()
        {
            _data = new DataAccess(Constants.ConnectionString);
            Initialize();
        }

        private void Initialize()
        {
            var history = _data.All();
            _size = history.Count;
            _matrix = new double[_size + 1][];
            for (int i = 0; i < _matrix.Length; i++)
            {
                _matrix[i] = FeatureExtractor.Default(history[i]);
            }
        }

        public Classification RunKMeans(int clusters)
        {
            var kmeans = new KMeans(clusters);
            var clusterCollection = kmeans.Learn(_matrix);
            return new Classification
            {
                Clusters = clusterCollection
            };
        }
    }
}