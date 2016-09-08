using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ML.TypingClassifier.Startup))]
namespace ML.TypingClassifier
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
