namespace GoogleSearchTest.Services
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GoogleSearch.Models;
    using GoogleSearch.Services;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// The google search service test.
    /// </summary>
    public class GoogleSearchServiceTest
    {
        /// <summary>
        /// The _google search service.
        /// </summary>
        private readonly IGoogleSearchService _googleSearchService;

        /// <summary>
        /// The _http client service.
        /// </summary>
        private readonly IHttpClientService _httpClientService;

        /// <summary>
        /// The _analyse service.
        /// </summary>
        private readonly IAnalyseService _analyseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSearchServiceTest"/> class.
        /// </summary>
        public GoogleSearchServiceTest()
        {
            _httpClientService = Substitute.For<IHttpClientService>();
            _analyseService = Substitute.For<IAnalyseService>();

            _googleSearchService = new GoogleSearchService(_httpClientService, _analyseService);
        }

        /// <summary>
        /// The when keywords is empty search async return empty.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task WhenKeywordsIsEmptySearchAsyncReturnEmpty()
        {
            var request = new SearchRequest { Keywords = string.Empty, BaseAddress = "something" };

            var actual = await _googleSearchService.SearchAsync(request);

            Assert.Equal(0, actual.Count());
        }

        /// <summary>
        /// The when base address is empty search async return empty.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task WhenBaseAddressIsEmptySearchAsyncReturnEmpty()
        {
            var request = new SearchRequest { Keywords = "something", BaseAddress = string.Empty };

            var actual = await _googleSearchService.SearchAsync(request);

            Assert.Equal(0, actual.Count());
        }

        /// <summary>
        /// The when correct request search async return expected result.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task WhenCorrectRequestSearchAsyncReturnExpectedResult()
        {
            var request = new SearchRequest
                              {
                                  Keywords = "something",
                                  BaseAddress = "something",
                                  PageSize = 100,
                                  Page = 0,
                                  TargetUrl = "something",
                                  Type = ResultType.Ad
                              };

            var content = Substitute.For<HttpContent>();

            _httpClientService.GetAsync(
                request.BaseAddress,
                $"/search?q={request.Keywords}&num={request.PageSize}&start={request.Page}")
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            var actual = await _googleSearchService.SearchAsync(request);

            _httpClientService
                .Received()
                .GetAsync(request.BaseAddress, $"/search?q={request.Keywords}&num={request.PageSize}&start={request.Page}");

            _analyseService.Received().FindAdResults(
                await content.ReadAsStringAsync(),
                request.Keywords);

            _analyseService.DidNotReceive().FindNormalResults(Arg.Any<string>(), Arg.Any<string>());

            Assert.Equal(0, actual.Count());
        }
    }
}
