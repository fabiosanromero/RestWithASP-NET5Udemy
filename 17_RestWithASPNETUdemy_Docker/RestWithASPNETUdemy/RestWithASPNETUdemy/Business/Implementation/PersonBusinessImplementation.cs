using RestWithASPNETUdemy.Data.Converter.Implementation;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonDTO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc")? "asc":"desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1=1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_Name like '%{name}%'";
            query += $"order by p.first_Name {sort} limit {size} offset {offset}";
            
            string countQuery = @"select count(*) from person p where 1=1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_Name like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonDTO> {
                CurrentPage=page,
                List=_converter.Parse(persons),
                PageSize=size,
                SortDirections=sort,
                TotalResults=totalResults,
                TotalPage=_repository.GetTotalPageCount(totalResults,size)
            };
        }

        public PersonDTO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonDTO Create(PersonDTO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonDTO Update(PersonDTO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonDTO Disable(long Id)
        {
            var personEntity = _repository.Disable(Id);
            return _converter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonDTO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName,lastName));
        }
    }
}
