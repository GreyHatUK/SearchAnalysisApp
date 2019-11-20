using SearchSiteManager.Dto;
using SearchSiteManager.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchSiteManager.Classes
{
    public class GoogleSearch : ISearchEngine
    {
        //Items normally in configeration 
        private readonly string GoogleSearchURL = "https://www.google.com/search?q={0}&safe=off&start={1}";

        //
        private bool Searching = false;


        public GoogleSearch()
        {
           

        }

        public void Dispose()
        {
            StopSearch();
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
                    var pageDoc =  await helper.GetPageAsync(string.Format(GoogleSearchURL, searchTerm, resultCount));
                    var resultColumn = pageDoc.GetElementbyId(@"main");
                    var nodesList = resultColumn.SelectNodes("*/div");
                    if (nodesList.Count == 0) Searching = false;
                    foreach (var searchelement in nodesList)
                    {
                        if(searchelement.GetAttributeValue("class","").Contains("ZINbbc") && !searchelement.GetAttributeValue("class", "").Contains("Pg70bf") && !searchelement.InnerText.StartsWith("Related searches"))
                        {
                            var resultItem = new SearchResultViewDto()
                            {
                                ResultId = Guid.NewGuid(),
                                ResultTitle = searchelement.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText,
                                LinkURL = HttpUtility.HtmlDecode(@"https://google.co.uk" + searchelement.ChildNodes[0].ChildNodes[0].Attributes["href"].Value),
                                ResultText = searchelement.ChildNodes[2].InnerText
                            };
                            
                            resultList.Add(resultItem);
                            resultCount++;
                        }

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
