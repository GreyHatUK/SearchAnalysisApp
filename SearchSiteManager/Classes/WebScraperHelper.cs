using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace SearchSiteManager.Classes
{
    public class WebScraperHelper 
    {
        

        public async System.Threading.Tasks.Task<HtmlDocument> GetPageAsync(string pageURL)
        {           
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(pageURL);               
                var page = await response.Content.ReadAsStringAsync();
                var HTMLDocumnet = new HtmlDocument();
                HTMLDocumnet.LoadHtml(page);
                return HTMLDocumnet;

            }
        }
       
    }
}
