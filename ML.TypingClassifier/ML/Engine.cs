using Accord.MachineLearning;
using ML.TypingClassifier.Models;
using System;
using System.Linq;
using System.Threading;

namespace ML.TypingClassifier.ML
{
    public sealed class Engine
    {
		private const int CLUSTERS = 3;
        private static readonly Lazy<Engine> Thunk =
            new Lazy<Engine>(() => new Engine());

        public static Engine Instance { get { return Thunk.Value; } }

        private readonly DataAccess _data;
        private ReaderWriterLock _rwlock = new ReaderWriterLock();
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
            _matrix = history.Select(FeatureExtractor.Default).ToArray();
        }

        public void Refresh()
        {
            try
            {
                _rwlock.AcquireWriterLock(60000);
                Initialize();
            }
            finally
            {
                _rwlock.ReleaseWriterLock();
            }
        }

        public double[][] Matrix()
        {
            try
            {
                _rwlock.AcquireReaderLock(60000);
                return _matrix;
            }
            finally
            {
                _rwlock.ReleaseReaderLock();
            }
        }

        public Classification RunKMeans(int clusters)
        {
            try
            {
                _rwlock.AcquireReaderLock(60000);
                var kmeans = new KMeans(clusters);
                var clusterCollection = kmeans.Learn(_matrix);
                return new Classification
                {
                    Clusters = clusterCollection
                };
            }
            finally
            {
                _rwlock.ReleaseReaderLock();
            }
        }
    }
}