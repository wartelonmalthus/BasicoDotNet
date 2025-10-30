using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class UpdateAvisoRequestValidator : AbstractValidator<UpdateAvisoRequest>
{
    public UpdateAvisoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Titulo é obrigatório.")
            .MaximumLength(200).WithMessage("Titulo deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("Mensagem é obrigatória.")
            .MaximumLength(1000).WithMessage("Mensagem deve ter no máximo 1000 caracteres.");
    }
}
