using Microsoft.AspNetCore.Mvc;

namespace BWHazel.Api.Web.Controllers
{
    /// <summary>
    /// The main site controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the home page view.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the about page view.
        /// </summary>
        /// <returns>The about page view.</returns>
        public IActionResult About()
        {
            return this.View();
        }

        /// <summary>
        /// Redirects to the Swagger API documentation.
        /// </summary>
        /// <returns>A redirection to the Swagger API documentation.</returns>
        public IActionResult Documentation()
        {
            return this.Redirect("/swagger/index.html");
        }
    }
}
