using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public partial class CotacaoItem
    {
        public int Id { get; set; }
        public int IdCotacao { get; set; }
        public string Descricao { get; set; }
        public long NumeroItem { get; set; }
        public decimal? Preco { get; set; }
        public int Quantidade { get; set; }
        public string Marca { get; set; }
        public string Unidade { get; set; }
        [JsonIgnore]
        public Cotacao Cotacao { get; set; }
    }
}
