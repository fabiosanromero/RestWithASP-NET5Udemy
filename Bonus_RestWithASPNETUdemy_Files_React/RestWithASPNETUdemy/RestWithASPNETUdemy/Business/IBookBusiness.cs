using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IBookBusiness
    {
        BookDTO Create(BookDTO book);
        BookDTO FindById(long id);
        PagedSearchDTO<BookDTO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page);
        List<BookDTO> FindAll();
        BookDTO Update(BookDTO book);
        void Delete(long id);
    }
}
