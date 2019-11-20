using SearchSiteManager.Dto;
using SearchSiteManager.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchSiteManager.Classes
{
    public class BingSearch : ISearchEngine
    {

        //Items normally in configeration 
        private readonly string BingSearchURL = "https://www.bing.com/search?q={0}&first={1}&FORM=PERE";

        //
        private bool Searching = false;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<SearchResultViewDto>> GetSearchResultListAsync(string searchTerm, int maxResultCount)
        {
            var resultList = new List<SearchResultViewDto>();
            try
            {
                Searching = true;
                var resultCount = 0;

                var helper = new WebScraperHelper();

                while (resultCount < maxResultCount && Searching)
                {
                    var pageDoc = await helper.GetPageAsync(string.Format(BingSearchURL, searchTerm, resultCount));
                    var nodesList = pageDoc.DocumentNode.SelectNodes("//li[contains(@class, 'b_algo')]");
                    foreach (var searchelement in nodesList)
                    {
                        var resultItem = new SearchResultViewDto()
                        {
                            ResultId = Guid.NewGuid(),
                            ResultTitle = searchelement.ChildNodes[0].ChildNodes[0].InnerText,
                            LinkURL = searchelement.ChildNodes[0].ChildNodes[0].Attributes["href"].Value,
                            ResultText = searchelement.ChildNodes[1].ChildNodes[1].InnerText
                        };

                        resultList.Add(resultItem);
                        resultCount++;

                        if (resultCount >= maxResultCount)
                        {
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return resultList;
         }

        public void StopSearch()
        {
            Searching = false;
        }
    }
}
