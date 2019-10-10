namespace GoogleSearch.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleSearch.Models;
    using GoogleSearch.Services;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The _google search service.
        /// </summary>
        private readonly IGoogleSearchService _googleSearchService;

        /// <summary>
        /// The _data service.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="googleSearchService">
        /// The google search service.
        /// </param>
        /// <param name="dataService">The data service</param>
        public HomeController(IGoogleSearchService googleSearchService, IDataService dataService)
        {
            _googleSearchService = googleSearchService;
            _dataService = dataService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public IActionResult Index()
        {
            var request = new SearchRequest
            {
                Keywords = "online title search",
                BaseAddress = "https://www.google.com.au",
                PageSize = 100,
                Page = 0,
                Type = ResultType.Normal,
                TargetUrl = "www.infotrack.com.au"
            };
            return View(request);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult Index([Bind("Keywords, BaseAddress, PageSize, Page, TargetUrl")] SearchRequest request)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Result", request);
            }

            return View(request);
        }

        /// <summary>
        /// The search.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IActionResult> Search()
        {
            var request = new SearchRequest
                              {
                                  Keywords = "online title search",
                                  BaseAddress = "https://www.google.com.au",
                                  PageSize = 100,
                                  Page = 0,
                                  Type = ResultType.Normal,
                                  TargetUrl = "www.infotrack.com.au"
                              };

            var allResults = await _googleSearchService.SearchAsync(request);

            await _dataService.SaveAllAsync(allResults);

            var results = allResults.Where(x => x.Host.Contains(request.TargetUrl) || request.TargetUrl.Contains(x.Host)).ToList();

            await _dataService.SaveAsync(results);

            return View();
        }

        /// <summary>
        /// The history.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public async Task<IActionResult> Result(SearchRequest request)
        {
            var allResults = await _googleSearchService.SearchAsync(request);

            await _dataService.SaveAllAsync(allResults);

            var results = allResults.Where(x => x.Host == request.TargetUrl).ToList();

            await _dataService.SaveAsync(results);

            return View(results.ToList());
        }

        /// <summary>
        /// The history.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public async Task<IActionResult> History()
        {
            var results = await _dataService.GetAsync();
            return View(results.OrderByDescending(x => x.SearchDateTime).Take(100).ToList());
        }

        /// <summary>
        /// All history.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public async Task<IActionResult> AllHistory()
        {
            var results = await _dataService.GetAllAsync();
            return View(results.OrderByDescending(x => x.SearchDateTime).Take(100).ToList());
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
