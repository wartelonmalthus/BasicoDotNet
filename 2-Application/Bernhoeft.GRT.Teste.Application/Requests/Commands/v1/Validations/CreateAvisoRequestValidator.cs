using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class CreateAvisoRequestValidator : AbstractValidator<CreateAvisoRequest>
{
    public CreateAvisoRequestValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Titulo é obrigatório.")
            .MaximumLength(200).WithMessage("Titulo deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("Mensagem é obrigatória.")
            .MaximumLength(1000).WithMessage("Mensagem deve ter no máximo 1000 caracteres.");
    }
}