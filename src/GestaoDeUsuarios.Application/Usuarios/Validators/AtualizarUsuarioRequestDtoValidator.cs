using FluentValidation;
using GestaoDeUsuarios.Application.Usuarios.Dtos;

namespace GestaoDeUsuarios.Application.Usuarios.Validators;

public class AtualizarUsuarioRequestDtoValidator : AbstractValidator<AtualizarUsuarioRequestDto>
{
    public AtualizarUsuarioRequestDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
