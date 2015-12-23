using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Demo.Service;
using DemoCore;

namespace Demo.Server
{
    public class Global : HttpApplication
    {
        RefreshConfigThread _refreshConfig = new RefreshConfigThread();
        protected void Application_Start(object sender, EventArgs e)
        {
            var config = WindsorHelper.ResolveAll<IRefreshConfig>();
            _refreshConfig.Appends(config).Start();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("", new WebServiceHostFactory(), typeof(CommonService)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            throw new Exception("Error");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            _refreshConfig.Dispose();
            _refreshConfig = null;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}