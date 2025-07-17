namespace CodeVrikATS.UI.Helpers
{
    public class JwtRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Session.GetString("authToken");
            var path = context.Request.Path.Value?.ToLower() ?? "";

            //  Define all public/unauthenticated routes
            var allowedAnonymousPaths = new[]
                {
                "/account/index",
                "/account/login",
                "/account/forgotpassword",
                "/account/sendresetemail",
                "/resetpassword"
            };

            var isStaticResource = path.StartsWith("/css") || path.StartsWith("/js") ||
                                   path.StartsWith("/lib") || path.StartsWith("/images");

            //  Allow public paths
            if (allowedAnonymousPaths.Any(p => path.StartsWith(p)) || isStaticResource)
            {
                await _next(context);
                return;
            }

            //  If token is missing, redirect
            if (string.IsNullOrEmpty(token))
            {
                context.Response.Redirect("/Account/Index");
                return;
            }

            await _next(context);
        }
    }
}
