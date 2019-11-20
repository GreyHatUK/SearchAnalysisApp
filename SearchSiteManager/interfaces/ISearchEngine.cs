using SearchSiteManager.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchSiteManager.interfaces
{
    public interface ISearchEngine :IDisposable
    {

        Task<List<SearchResultViewDto>> GetSearchResultListAsync(string searchTerm, int maxResultCount);

        void StopSearch();

    }
}
