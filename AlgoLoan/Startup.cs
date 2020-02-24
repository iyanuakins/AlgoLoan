using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlgoLoan.Startup))]
namespace AlgoLoan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
