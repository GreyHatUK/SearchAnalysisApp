using NUnit.Framework;
using SearchSiteManager.Classes;
using SearchSiteManager.interfaces;

namespace tests.SearchSiteManager
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task GoggleNoResultsReturnedAsync()
        {
            ISearchEngine google = new GoogleSearch();
            var results =  await google.GetSearchResultListAsync("asdasdsadadfdwefvtrvrtbwcvwregerge", 1);

            Assert.IsTrue(results.Count == 0);
        }

        [Test]
        public async System.Threading.Tasks.Task GoggleFixedResultReturnedAsync()
        {
            ISearchEngine google = new GoogleSearch();
            var results = await google.GetSearchResultListAsync("test", 1);

            Assert.IsTrue(results.Count == 1);
        }
    }
}