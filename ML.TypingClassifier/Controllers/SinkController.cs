using ML.TypingClassifier.ML;
using ML.TypingClassifier.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ML.TypingClassifier.Controllers
{
    [RoutePrefix("sink")]
    public class SinkController : ApiController
    {
        private readonly DataAccess _dataAccess;
		private static int _currentSubmissionCount = 0;
		private const int REFRESH_CLUSTERS = 10;

		public SinkController()
        {
            _dataAccess = new DataAccess(Constants.ConnectionString);
        }

        [Route("{email?}")]
        public IHttpActionResult GetByEmail(string email = "")
        {
            if (email == "") return Ok(_dataAccess.All());
            var sample = _dataAccess.Single(email);
            var features = FeatureExtractor.Default(sample);
            if (sample == null)
                return NotFound();
            return Ok(new
            {
                Sample = sample,
                Features = features
            });
        }

        [Route("")]
        public IHttpActionResult Post(Sample data)
        {
            _dataAccess.Add(data);
			
			if (_currentSubmissionCount > REFRESH_CLUSTERS) {
				Interlocked.Exchange(ref _currentSubmissionCount,0);
				Task.Run(() => Engine.Instance.Refresh());
			}
			else {
				Interlocked.Increment(ref _currentSubmissionCount);
			}
							
            return Ok(data);
        }

        [Route("kmeans/{clusters}")]
        public IHttpActionResult Get(int clusters)
        {
            var classification = Engine.Instance.RunKMeans(clusters);
            return Ok(classification);
        }

        [Route("kmeans")]
        public IHttpActionResult Post(int clusters)
        {
            var classification = Engine.Instance.RunKMeans(clusters);
            return Ok(classification);
        }

        [Route("invalidate")]
        public IHttpActionResult Post()
        {
            Engine.Instance.Refresh();
            return Ok();
        }

        [Route("points")]
        public IHttpActionResult GetPoints()
        {
            var points = Engine.Instance.Matrix();
            var features = new List<Feature>(5);
            for (int i = 0; i < 5; ++i)
            {
                var values = new double[points.Length];
                for (int j = 0; j < points.Length; ++j)
                {
                    values[j] = System.Math.Round(points[j][i], 2);
                }
                features.Add(new Feature
                {
                    Label = FeatureNames[i],
                    Values = values
                });
            }
            return Ok(features);
        }

        private static readonly string[] FeatureNames = new[]
        {
            "WPM", "Backspaces", "Deletes", "AverageKeyPressDuration", "AverageTimeBetweenKeystrokes"
        };
	}
}