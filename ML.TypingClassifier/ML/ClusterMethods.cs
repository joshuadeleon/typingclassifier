using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accord.MachineLearning;
using ML.TypingClassifier.Models;

namespace ML.TypingClassifier.ML
{
	/// <summary>
	/// K-means clustering methods
	/// </summary>
	public class ClusterMethods
	{
		public static KMeans TrainByFeature(int clusterNumber, double[][] features)
		{
			// Create a new K-Means algorithm with 3 clusters 
			var kmeans = new KMeans(clusterNumber);
			kmeans.Learn(features);
			return kmeans;
		}

		public static IEnumerable<ClusterResult<double[]>> GetFeatureClusters<T>(KMeans kmeans, double[][] features)
		{
			return features.Select(
				x => { return new ClusterResult<double[]>(kmeans.Clusters.Decide(x), x); }
			);			
		}
	}
}