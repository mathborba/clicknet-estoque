using Engefibra.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engefibra.Web.Controllers
{
    [Access(false)]
    public class BaseController : Controller
    {
	}
}