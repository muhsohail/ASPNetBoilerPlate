using System.Web.Mvc;

namespace ExpenseManager.Web.Controllers
{
    public class AboutController : ExpenseManagerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}