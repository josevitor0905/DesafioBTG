import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PessoaService } from '../../services/pessoa';
import { VacinaService } from '../../services/vacina';
import { VacinacaoService } from '../../services/vacinacao';
import { FormsModule } from '@angular/forms';
import { Pessoa } from '../../interface/pessoa-interface';
import { ActivatedRoute } from '@angular/router';
import { Observable, mergeMap, forkJoin, catchError, throwError, map, of } from 'rxjs';

@Component({
  selector: 'app-pessoa-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pessoa-list.html',
  styleUrl: './pessoa-list.css',
})

export class PessoaList implements OnInit {
  pessoas: Pessoa[] = [];
  pessoaId!: number;

  
  pessoaSelecionada: number | null = null;
  pessoaSelecionadaVacinacaoDel: number | null = null;
  vacinaSelecionada: number | null = null;
  dose: number | null = null;
  data: string | null = null;

  novoNome: string | null = null; 
  novaIdade: number | null = null;
  novoSexo: string | null = null;
  novaVacina: string | null = null;

 
  pessoaDetalhe: Pessoa | null = null;
  vacinas: any[] = [];
  vacinacoes: any[] = [];


  //novoNome: string = '';
  //novaIdade: number = 0;
  //novoSexo: string = '';
  //novaVacina: string = '';

  //pessoaSelecionada: number | null = null;
  //vacinaSelecionada!: number;
  //dose!: number;
  //data!: string;

  doses: { numero: number, nome: string }[] = [
        { numero: 1, nome: '1ª Dose' },
        { numero: 2, nome: '2ª Dose' },
        { numero: 3, nome: '3ª Dose' },
        { numero: 4, nome: '1º Reforço' },
        { numero: 5, nome: '2º Reforço' },
    ];
  
  modoAtual: 'consulta' | 'cadastro' = 'consulta';

  constructor(
    private route: ActivatedRoute,
    private pessoaService: PessoaService,
    private vacinaService: VacinaService,
    private vacinacaoService: VacinacaoService,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.carregarPessoas();
    this.carregarVacinas();
    this.cd.detectChanges();
    console.log('Pessoas:', this.pessoas);
  }

  trocarModo(novoModo: 'consulta' | 'cadastro'): void {
      this.modoAtual = novoModo;
      
      if (novoModo === 'cadastro') {
            this.pessoaDetalhe = null;
            this.pessoaSelecionada = null;
      }
    this.cd.detectChanges(); 
  }

  verificarStatus(vacinaId: number, dose: number): any | null {
    const vac = this.vacinas.find(a =>
      a.id === vacinaId
    );
    const registro = this.vacinacoes.find(v => 
        String(v.nomeVacina) === vac.nome && 
        Number(v.dose) === dose
    );
    return registro || null;
  }

  carregarCartao(id: number) {
    this.pessoaService.obterCartao(id).subscribe(res => {
      this.vacinacoes = res;
      this.cd.detectChanges();
      console.log('Vacinações carregadas:', this.vacinacoes);
    });
  }

  carregarVacinas() {
      this.vacinaService.listar().subscribe({
          next: (res) => {
              this.vacinas = res;
              this.cd.detectChanges();
              console.log('Vacinas carregadas:', this.vacinas);
          },
          error: (err) => console.error('Erro ao carregar vacinas:', err)
      });
      this.cd.detectChanges();
    }

  onSelectionChange() {
    if (this.pessoaSelecionada) 
      {
        const pessoaEncontrada = this.pessoas!.find(p => p.id === this.pessoaSelecionada);      
        this.pessoaDetalhe = pessoaEncontrada || null;
        console.log('Pessoa Selecionada:', this.pessoaSelecionada);
        this.carregarCartao(this.pessoaSelecionada);
      } 
      else 
      {
        this.pessoaDetalhe = null;
        this.vacinacoes = [];
      }
      this.cd.detectChanges();
  }

  carregarPessoas() {
    this.pessoaService.listar().subscribe({
      next: (res) => {
        this.pessoas = res;
        this.cd.detectChanges();
        console.log('Pessoas carregadas:', this.pessoas);
      }        
    });
    
  }

  criarPessoa() {
    if (!this.novoNome?.trim()) {
        alert('O nome da pessoa é obrigatório.');
        return;
    }
    
    if (!this.novaIdade || this.novaIdade <= 0) {
        alert('A idade deve ser informada e maior que zero.');
        return;
    }

    if (!this.novoSexo?.trim()) {
        alert('O sexo da pessoa é obrigatório.');
        return;
    }

    this.pessoaService.criar(this.novoNome, this.novaIdade, this.novoSexo).subscribe({
      next: () => {
        alert('Pessoa criada com sucesso!');
        this.novoNome = '';
        this.novaIdade = null;
        this.novoSexo = '';
        this.carregarPessoas();
      },
      error: (err) => {
        if (err.status === 400) 
        {
          const validationErrors = err.error;
          let mensagens = '';

          for (const campo in validationErrors) 
          {
            mensagens += `${validationErrors[campo].join(', ')}\n`;
          }

          alert('Erros de validação:\n' + mensagens);
        } 
        else 
        {
          alert('Ocorreu um erro inesperado no servidor.');
        }
      }
    });
  }

  criarVacina() {
    if (!this.novaVacina?.trim() || !this.novaVacina) 
    {
      return;
    }

    this.vacinaService.criar(this.novaVacina).subscribe({
      next: () => {
      alert('Vacina criada com sucesso!');
      this.novaVacina = '';
      this.carregarVacinas();
      },
      error: (err) => {
        if (err.status === 400) 
        {
          const validationErrors = err.error;
          let mensagens = '';

          for (const campo in validationErrors) 
          {
            mensagens += `${validationErrors[campo].join(', ')}\n`;
          }

          alert('Erros de validação:\n' + mensagens);
        } 
        else 
        {
          alert('Ocorreu um erro inesperado no servidor.');
        }
      }
    });
  }

  excluirPessoa(id: number) {
    if (confirm('Deseja excluir esta pessoa e TODAS as suas vacinações?'))
    {
        this.excluirTodasVacinacoesPorPessoa(id).pipe(
            mergeMap(() => {
                return this.pessoaService.remover(id);
            })
        ).subscribe({
            next: () => {
                alert('Pessoa e vacinações associadas excluídas com sucesso!');
                this.carregarPessoas();
            },
            error: (err) => alert(err.message || 'Erro ao excluir pessoa e vacinações.')
        });
    }
  }

  excluirVacina(id: number) {
    if (confirm('Deseja mesmo excluir esta vacina?'))
    {
      this.vacinaService.remover(id).subscribe(() => this.carregarVacinas());
      alert('Vacina excluída com sucesso!');
      this.carregarVacinas();
    }
  }

  registrarVacinacao() {
    if (!this.pessoaSelecionada || !this.vacinaSelecionada || !this.dose || !this.data) {
        alert('Por favor, preencha todos os campos (Pessoa, Vacina, Dose e Data) para registrar a vacinação.');
        return; 
    }

    const dto = {
        pessoaId: this.pessoaSelecionada!, 
        vacinaId: this.vacinaSelecionada!, 
        dose: this.dose!,
        dataAplicacao: this.data! 
    };
 
    this.vacinacaoService.registrar(dto).subscribe({
        next: () => {
            alert('Vacinação registrada com sucesso!');
            this.pessoaSelecionada = null;
            this.vacinaSelecionada = null;
            this.dose = null;
            this.data = null;
            this.carregarVacinas(); 
        },
        error: (err) => alert(err.error.erro || 'Erro ao registrar vacinação.')
    });
  }

  excluirVacinacao(id: number) {
    if (!this.pessoaSelecionadaVacinacaoDel) {
        alert('Pessoa nao existe');
        return; 
    }

    if (confirm('Deseja mesmo excluir esta vacinacao?'))
    {
      this.vacinacaoService.remover(id).subscribe(() => this.carregarCartao(this.pessoaSelecionadaVacinacaoDel!));
      this.pessoaSelecionadaVacinacaoDel = null;
    }
  }

  excluirTodasVacinacoesPorPessoa(pessoaId: number): Observable<any> {
    // 1. Obtém o cartão de vacinação (lista de vacinações)
    return this.pessoaService.obterCartao(pessoaId).pipe(
      
        // 2. Transforma a lista de vacinações em uma série de chamadas de exclusão
        mergeMap((vacinacoes: any[]) => {
            if (vacinacoes.length === 0) {
                  return of(null); 
              }
            // Cria um array de Observables (uma chamada de remoção para cada vacinação)
            const exclusoes = vacinacoes.map(vac => 
                this.vacinacaoService.remover(vac.id)
            );
            // Combina todos os Observables de remoção. Espera que todas terminem.
            return forkJoin(exclusoes).pipe(
                // Retorna um Observable vazio/completo para continuar o pipe principal
                catchError(err => {
                    console.error('Erro ao excluir uma ou mais vacinações:', err);
                    // Decide se o processo deve parar ou continuar. 
                    // Se falhar a exclusão de vacinas, é melhor parar
                    return throwError(() => new Error('Falha ao excluir vacinações associadas.'));
                })
            );
        }),
        // Retorna um Observable vazio para sinalizar a conclusão desta etapa
        map(() => {})
    );
  }
}
