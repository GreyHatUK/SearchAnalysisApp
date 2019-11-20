using NUnit.Framework;
using SearchSiteManager.Classes;
using SearchSiteManager.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace tests.SearchSiteManager
{
    public class BingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task BingNoResultsReturnedAsync()
        {
            ISearchEngine bing = new BingSearch();
            var results = await bing.GetSearchResultListAsync("asdasdsadadfdwefvtrvrtbwohbtygfvycvwregerge", 1);

            Assert.IsTrue(results.Count == 0);
        }

        [Test]
        public async System.Threading.Tasks.Task BingFixedResultReturnedAsync()
        {
            ISearchEngine bing = new BingSearch();
            var results = await bing.GetSearchResultListAsync("test", 1);

            Assert.IsTrue(results.Count == 1);
        }
    }
}
