using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace designing_chart_api.Controllers
{
    public class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}