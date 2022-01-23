using RestWithASPNETUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Utils
{
    public class PagedSearchDTO<T> where T:ISupportHypermedia
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalResults { get; set; }

        public string SortFields { get; set; }

        public int TotalPage { get; set; }

        public string SortDirections { get; set; }
        
        public Dictionary<string,object> Filters { get; set; }
        public List<T> List { get; set; }

        public PagedSearchDTO()
        {
        }
        public PagedSearchDTO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }
        public PagedSearchDTO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters) : this(currentPage, pageSize, sortFields, sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }
        public PagedSearchDTO(int currentPage, string sortFields, string sortDirections) 
            : this(currentPage, 10, sortFields,sortDirections)
        {}

        public PagedSearchDTO(int currentPage, int pageSize, int totalResults, int totalPage, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalResults = totalResults;
            TotalPage = totalPage;
            SortDirections = sortDirections;
        }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
