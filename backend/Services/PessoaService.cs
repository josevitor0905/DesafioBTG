using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class PessoaService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly IVacinacaoRepository _vacinacaoRepo;

    public PessoaService(IPessoaRepository pessoaRepo, IVacinacaoRepository vacinacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _vacinacaoRepo = vacinacaoRepo;
    }

    public async Task<PessoaDTO> CriarPessoaAsync(PessoaCreateDTO dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome
        };

        await _pessoaRepo.AddAsync(pessoa);

        return new PessoaDTO
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome
        };
    }

    public async Task<bool> RemoverPessoaAsync(int id)
    {
        var vacinacoes = await _vacinacaoRepo.GetByPessoaAsync(id);
        
        foreach (var v in vacinacoes)
        {
            await _vacinacaoRepo.DeleteAsync(v.Id);
        }

        return await _pessoaRepo.DeleteAsync(id);
    }

    public async Task<List<PessoaDTO>> ListarAsync()
    {
        var pessoas = await _pessoaRepo.GetAllAsync();

        return pessoas.Select(p => new PessoaDTO{Id = p.Id, Nome = p.Nome}).ToList();
    }
}