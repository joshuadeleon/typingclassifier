using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
	public class ClusterResult<T>
	{
		public int ClusterId { get; set; }
		public T Features { get; set; }

		public ClusterResult(int clusterId, T features)
		{
			ClusterId = clusterId;
			Features = features;
		}
	}
}