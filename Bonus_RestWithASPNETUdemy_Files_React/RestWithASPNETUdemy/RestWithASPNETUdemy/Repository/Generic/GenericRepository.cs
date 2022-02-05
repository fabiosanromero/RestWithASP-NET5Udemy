using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected MySQLContext _context;

        private DbSet<T> _dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.SingleOrDefault(p => p.id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public void Delete(long id)
        {
            var result = _dataset.SingleOrDefault(p => p.id.Equals(id));

            if (result != null)
            {
                try
                {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public T Update(T item)
        {
            var result = _dataset.SingleOrDefault(p => p.id.Equals(item.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }           
        }

        public bool Exists(long id)
        {
            return _dataset.Any(p => p.id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result ="";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand()){
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return int.Parse(result);
        }

        public int GetTotalPageCount(int totalRow, int pageSize)
        {
            int totalPageCount = 0;
            if (pageSize == 0) pageSize = 1;            
            if (totalRow > 0)
            {
                Decimal totalPageCountIndefinido = Decimal.Round((totalRow / pageSize),2);
                
                int resultInteiro = 0;
                Decimal resultDecimal = 0;
                if (int.TryParse(totalPageCountIndefinido.ToString(), out resultInteiro))
                {
                    totalPageCount = resultInteiro;
                }
                else if (Decimal.TryParse(totalPageCountIndefinido.ToString(), out resultDecimal))
                {
                    resultDecimal = Decimal.Truncate(resultDecimal)+1;
                    totalPageCount = (int)resultDecimal;
                }
            }
            return totalPageCount;
        }
    }
}
