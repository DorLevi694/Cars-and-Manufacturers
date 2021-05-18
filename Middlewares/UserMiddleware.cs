using Cars_and_Manufacturers.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Middlewares
{
    public class UserMiddleware
    {

        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context,ICurrentUserService currentUserService)
        {
            var userName = context.Request.Headers["username"].ToString();
            if (!string.IsNullOrWhiteSpace(userName))
                await currentUserService.setCurrentUserName(userName);
            await _next.Invoke(context);
        }
    }

    public static class UserMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserMiddleware>();
            return app;
        }
    }







}
