using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Cotacao
    {
        public Cotacao()
        {
            CotacaoItems = new HashSet<CotacaoItem>();
        }

        public int Id { get; set; }
        public string Cnpjcomprador { get; set; }
        public string Cnpjfornecedor { get; set; }
        public long NumeroCotacao { get; set; }
        public DateTime DataCotacao { get; set; }
        public DateTime DataEntregaCotacao { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<CotacaoItem> CotacaoItems { get; set; }
    }
}
