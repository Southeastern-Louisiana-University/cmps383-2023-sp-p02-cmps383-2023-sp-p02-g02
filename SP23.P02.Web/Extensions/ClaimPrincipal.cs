﻿using System.Security.Claims;

namespace SP23.P02.Web.Extensions
{
    public static class ClaimPrincipal
    {


        public static string? GetCurrentUserName(this ClaimsPrincipal claimsPrincipal)
        {

            return claimsPrincipal.Identity?.Name;
        }
        public static int? GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaimValue = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaimValue == null)
            {
                return null;
            }
            return int.Parse(userIdClaimValue);
        }     
    }
}

