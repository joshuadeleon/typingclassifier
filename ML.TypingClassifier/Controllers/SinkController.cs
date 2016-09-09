using ML.TypingClassifier.ML;
using ML.TypingClassifier.Models;
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
			Task.Run(() => PeriodicRefesh(CancellationToken.None));
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_dataAccess.All());
        }

        [Route("{email}")]
        public IHttpActionResult GetByEmail(string email)
        {
            var sample = _dataAccess.Single(email);
            if (sample == null)
                return NotFound();
            return Ok(sample);
        }

        [Route("")]
        public IHttpActionResult Post(Sample data)
        {
            _dataAccess.Add(data);
			Interlocked.Increment(ref _currentSubmissionCount);					
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

		/// <summary>
		/// Periodically re-calculates K-means
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private async Task PeriodicRefesh(CancellationToken cancellationToken)
		{
			while (_currentSubmissionCount > REFRESH_CLUSTERS)
			{
				await Task.Run(() => Engine.Instance.Refresh());
				Interlocked.Exchange(ref _currentSubmissionCount, 0);
			}
		}
	}
}
