using Api.Context;
using Api.Models;

namespace Api.Repositories
{
    public class CotacaoRepository : Repository<Cotacao>, ICotacaoRepository
    {
        public CotacaoRepository(ApiContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
