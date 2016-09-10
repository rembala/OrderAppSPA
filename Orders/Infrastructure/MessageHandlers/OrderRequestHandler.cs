using System;
using Orders.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Threading;
using System.Net;
using System.Threading.Tasks;

namespace Orders.Infrastructure.MessageHandlers
{
    public class OrderRequestHandler : DelegatingHandler
    {
        private IEnumerable<string> authHeaderValues = null;
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out this.authHeaderValues);
                if (this.authHeaderValues == null)
                    return base.SendAsync(request, cancellationToken);
                var tokens = this.authHeaderValues.FirstOrDefault();
                tokens = tokens.Replace("Basic", string.Empty).Trim();
                if (!string.IsNullOrEmpty(tokens))
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValue = decodedString.Split(':');
                    var membershipService = request.GetMembershipService();
                    var memberCtx = membershipService.ValidateUser(tokensValue[0], tokensValue[1]);
                    //System.Web.HttpContext.Current.Session["UserId"] = Convert.ToInt32(tokens[2]);                   
                    //Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(memberCtx.User);
                    if (memberCtx != null)
                    {
                        IPrincipal principal = memberCtx.Principal;
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }
    }
}