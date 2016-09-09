using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
	public class ProfileResults
	{
		public double[][] Matrix { get; set; }
		public Sample User { get; set; }

		public ProfileResults(double[][] matrix, Sample user)
		{
			Matrix = matrix;
			User = user;
		}
	}
}