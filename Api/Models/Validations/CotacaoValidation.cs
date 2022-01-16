using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Validations
{
    public class CotacaoValidation : AbstractValidator<Cotacao>
    {
        public CotacaoValidation()
        {
            RuleFor(x => x.Cnpjcomprador).NotEmpty().WithMessage("Informe o CNPJ do comprador").Length(14, 18).WithMessage("O CNPJ do comprador deve ter no mínimo 14 caracteres e no máximo 18 caracteres");
            RuleFor(x => x.Cnpjfornecedor).NotEmpty().WithMessage("Informe o CNPJ do fornecedor").Length(14, 18).WithMessage("O CNPJ do fornecedor deve ter no mínimo 14 caracteres e no máximo 18 caracteres");
            RuleFor(x => x.NumeroCotacao).NotEmpty().WithMessage("Informe o Número da Cotação");
            RuleFor(x => x.DataCotacao).NotEmpty().WithMessage("Informe a Data da Cotação");
            RuleFor(x => x.DataEntregaCotacao).NotEmpty().WithMessage("Informe a Data da Entrega da Cotação");
            RuleFor(x => x.Cep).NotEmpty().WithMessage("Informe o CEP").Length(8, 10).WithMessage("O CEP deve ter no mínimo 8 caracteres e no máximo 10 caracteres");
            RuleForEach(x => x.CotacaoItems).SetValidator(new CotacaoItemValidation());
        }
    }
}
