using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindById(long id);
        List<PersonDTO> FindByName(string firstName, string lastName);
        PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize,int page);
        List<PersonDTO> FindAll();
        PersonDTO Update(PersonDTO person);
        PersonDTO Disable(long Id);
        void Delete(long id);

        
    }
}
