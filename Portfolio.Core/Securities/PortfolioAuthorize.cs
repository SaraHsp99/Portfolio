
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Core.Interfaces.Services.AuthenticateManagerInterfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace Portfolio.Core.Securities
{
    public class PortfolioAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private bool _roleCheck;

        private string[] _authoeizeNames;

        public PortfolioAuthorize(bool roleCheck = false, params string[] authoeizeNames)
        {
            _authoeizeNames = authoeizeNames;
            _roleCheck = roleCheck;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            try
            {
                bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                     .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
                if (!hasAllowAnonymous)
                {
                    var isLogin = context.HttpContext.User.Identity.IsAuthenticated;
                    if (isLogin)
                    {
                        if (context.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "SuperAdmin").Value != "SuperAdmin@Rahya.com")
                        {
                            var authorizeService = (IAuthorizeService)context.HttpContext.RequestServices.GetService(typeof(IAuthorizeService));
                            if (_roleCheck == false)
                            {

                                if (_authoeizeNames.Count() == 1)
                                {
                                    var isAuth = authorizeService.IsAuthorizeByPermissionName(_authoeizeNames.FirstOrDefault());
                                    if (isAuth == false)
                                    {
                                        context.HttpContext.SignOutAsync();
                                        context.Result = new RedirectResult("/user/login");
                                    }
                                }
                                else
                                {
                                    var isAuth = authorizeService.IsAuthorizeByPermissionNames(_authoeizeNames);
                                    if (isAuth == false)
                                    {
                                        context.HttpContext.SignOutAsync();
										context.Result = new RedirectResult("/account/login?permission=false");

									}
								}
                            }
                            else
                            {
                                if (_authoeizeNames.Count() == 1)
                                {
                                    var isAuth = authorizeService.IsAuthorizeByRoleName(_authoeizeNames.FirstOrDefault());
                                    if (isAuth == false)
                                    {
                                        context.HttpContext.SignOutAsync();
                                        context.Result = new RedirectResult("/account/login?permission=false");
                                    }
                                }
                                else
                                {
                                    var isAuth = authorizeService.IsAuthorizeByRoleNames(_authoeizeNames);
                                    if (isAuth == false)
                                    {
                                        context.HttpContext.SignOutAsync();
                                        context.Result = new RedirectResult("/account/login?permission=false");
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
