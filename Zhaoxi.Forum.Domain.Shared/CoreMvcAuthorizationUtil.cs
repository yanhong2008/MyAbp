using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Forum.Domain.Shared
{
    /// <summary>
    /// **获取用户**
    /// </summary>
    public static class CoreMvcAuthorizationUtil
    {
        public static string? GetUserId(HttpContext httpContext)
        {
            var userIdentifie = httpContext.User.Claims.SingleOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
            return userIdentifie != null ?userIdentifie.Value: null;
        }
    }
}
