using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;

public class GetAvisosRequestValidator : AbstractValidator<GetAvisosRequest>
{
    public GetAvisosRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber deve ser maior que zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .LessThanOrEqualTo(100).WithMessage("PageSize deve ser no máximo 100.");

        RuleFor(x => x.Titulo)
            .MaximumLength(200).WithMessage("Titulo deve ter no máximo 200 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Titulo));

        RuleFor(x => x.Mensagem)
            .MaximumLength(1000).WithMessage("Mensagem deve ter no máximo 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Mensagem));

        RuleFor(x => x.DataCriacaoInicio)
            .LessThanOrEqualTo(x => x.DataCriacaoFim)
            .WithMessage("DataCriacaoInicio deve ser menor ou igual a DataCriacaoFim.")
            .When(x => x.DataCriacaoInicio.HasValue && x.DataCriacaoFim.HasValue);

        RuleFor(x => x.OrderBy)
            .Must(x => string.IsNullOrWhiteSpace(x) || new[] { "DataCriacao", "Titulo", "Id" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderBy deve ser 'DataCriacao', 'Titulo' ou 'Id'.")
            .When(x => !string.IsNullOrWhiteSpace(x.OrderBy));
    }
}
