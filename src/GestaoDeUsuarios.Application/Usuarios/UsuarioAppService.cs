using AutoMapper;
using FluentValidation;
using GestaoDeUsuarios.Application.Usuarios.Dtos;
using GestaoDeUsuarios.Application.Usuarios.Interfaces;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Exceptions;
using GestaoDeUsuarios.Domain.Interfaces;

namespace GestaoDeUsuarios.Application.Usuarios;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IValidator<CriarUsuarioRequestDto> _criarValidator;
    private readonly IValidator<AtualizarUsuarioRequestDto> _atualizarValidator;

    public UsuarioAppService(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasherService passwordHasher,
        IMapper mapper,
        IValidator<CriarUsuarioRequestDto> criarValidator,
        IValidator<AtualizarUsuarioRequestDto> atualizarValidator)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _criarValidator = criarValidator;
        _atualizarValidator = atualizarValidator;
    }

    public async Task<UsuarioResponseDto> CriarAsync(CriarUsuarioRequestDto dto)
    {
        await _criarValidator.ValidateAndThrowAsync(dto);

        if (await _usuarioRepository.ExisteEmailAsync(dto.Email))
            throw new DomainException($"O e-mail '{dto.Email}' já está em uso.");

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = _passwordHasher.Hash(dto.Senha),
            Cargo = dto.Cargo,
            Ativo = true,
            CriadoEm = DateTime.UtcNow
        };

        await _usuarioRepository.AddAsync(usuario);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UsuarioResponseDto>(usuario);
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ListarAtivosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAtivosAsync();
        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ListarInativosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllInativosAsync();
        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
    }

    public async Task<IEnumerable<UsuarioResponseDto>> PesquisarPorNomeAsync(string nome)
    {
        var usuarios = await _usuarioRepository.GetByNameAsync(nome);
        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
    }

    public async Task<IEnumerable<UsuarioResponseDto>> PesquisarPorEmailAsync(string email)
    {
        var usuarios = await _usuarioRepository.GetByEmailAsync(email);
        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
    }

    public async Task<UsuarioResponseDto> BuscarPorIdAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Usuário com id '{id}' não encontrado.");

        return _mapper.Map<UsuarioResponseDto>(usuario);
    }

    public async Task<UsuarioResponseDto> AtualizarAsync(Guid id, AtualizarUsuarioRequestDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Usuário com id '{id}' não encontrado.");

        await _atualizarValidator.ValidateAndThrowAsync(dto);

        if (await _usuarioRepository.ExisteEmailAsync(dto.Email, ignorarId: id))
            throw new DomainException($"O e-mail '{dto.Email}' já está em uso.");

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Cargo = dto.Cargo;
        usuario.AtualizadoEm = DateTime.UtcNow;

        _usuarioRepository.Update(usuario);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UsuarioResponseDto>(usuario);
    }

    public async Task DesativarAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Usuário com id '{id}' não encontrado.");

        usuario.Ativo = false;
        _usuarioRepository.Update(usuario);
        await _unitOfWork.SaveAsync();
    }
}
