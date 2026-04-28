using AutoMapper;
using GestaoDeUsuarios.Application.Usuarios.Dtos;
using GestaoDeUsuarios.Domain.Entities;

namespace GestaoDeUsuarios.Application.Usuarios.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioResponseDto>();
    }
}
