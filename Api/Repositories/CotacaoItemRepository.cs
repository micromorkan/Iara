using Api.Context;
using Api.Models;

namespace Api.Repositories
{
    public class CotacaoItemRepository : Repository<CotacaoItem>, ICotacaoItemRepository
    {
        public CotacaoItemRepository(ApiContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
