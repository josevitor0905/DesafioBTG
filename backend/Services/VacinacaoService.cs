using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class VacinacaoService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly IVacinaRepository _vacinaRepo;
    private readonly IVacinacaoRepository _vacinacaoRepo;

    public VacinacaoService(
        IPessoaRepository pessoaRepo,
        IVacinaRepository vacinaRepo,
        IVacinacaoRepository vacinacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _vacinaRepo = vacinaRepo;
        _vacinacaoRepo = vacinacaoRepo;
    }

    public async Task<VacinacaoDTO?> RegistrarAsync(VacinacaoCreateDTO dto)
    {
        var pessoa = await _pessoaRepo.GetByIdAsync(dto.PessoaId);
        
        if (pessoa == null)
        {
            return null;
        }

        var vacina = await _vacinaRepo.GetByIdAsync(dto.VacinaId);

        if (vacina == null)
        {
            return null;
        }

        var aplicacoes = await _vacinacaoRepo.GetByPessoaAsync(dto.PessoaId);

        var aplicacoesDaVacina = aplicacoes
            .Where(v => v.VacinaId == dto.VacinaId)
            .OrderBy(v => v.Dose)
            .ToList();

        if (aplicacoesDaVacina.Count > 0)
        {
            int proximaDose = aplicacoesDaVacina.Last().Dose + 1;

            if (dto.Dose != proximaDose)
            {
                throw new Exception($"A próxima dose esperada é a dose {proximaDose}.");
            }
        }    
        else
        {
              if (dto.Dose != 1)
            {
                throw new Exception("A primeira dose precisa ser a dose 1.");
            }
        }  

        var vacinacao = new Vacinacao
        {
            PessoaId = dto.PessoaId,
            VacinaId = dto.VacinaId,
            Dose = dto.Dose,
            DataAplicacao = dto.DataAplicacao
        };

        await _vacinacaoRepo.AddAsync(vacinacao);

        return new VacinacaoDTO
        {
            Id = vacinacao.Id,
            NomeVacina = vacina.Nome,
            Dose = vacinacao.Dose,
            DataAplicacao = vacinacao.DataAplicacao
        };
    }

    public async Task<List<VacinacaoDTO>> ObterPorPessoaAsync(int pessoaId)
    {
        var vacs = await _vacinacaoRepo.GetByPessoaAsync(pessoaId);

        return vacs.Select(v => new VacinacaoDTO
        {
            Id = v.Id,
            NomeVacina = v.Vacina!.Nome,
            Dose = v.Dose,
            DataAplicacao = v.DataAplicacao
        }).ToList();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        return await _vacinacaoRepo.DeleteAsync(id);
    }
    
}