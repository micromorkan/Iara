using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Validations
{
    public class CotacaoItemValidation : AbstractValidator<CotacaoItem>
    {
        public CotacaoItemValidation()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Informe a Descrição");
            RuleFor(x => x.NumeroItem).NotEmpty().WithMessage("Informe o Nº do Item");
            RuleFor(x => x.Quantidade).NotEmpty().WithMessage("Informe a Quantidade");
        }
    }
}
