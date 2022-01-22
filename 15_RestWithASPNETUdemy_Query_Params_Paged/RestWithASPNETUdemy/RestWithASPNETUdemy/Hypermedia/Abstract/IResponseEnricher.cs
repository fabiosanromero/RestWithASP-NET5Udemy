using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnricher(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
