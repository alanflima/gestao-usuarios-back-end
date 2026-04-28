using FluentValidation;
using GestaoDeUsuarios.Application.Usuarios.Dtos;

namespace GestaoDeUsuarios.Application.Usuarios.Validators;

public class CriarUsuarioRequestDtoValidator : AbstractValidator<CriarUsuarioRequestDto>
{
    public CriarUsuarioRequestDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Senha).NotEmpty();
    }
}
