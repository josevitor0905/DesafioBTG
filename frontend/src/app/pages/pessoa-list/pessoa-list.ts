import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PessoaService } from '../../services/pessoa';
import { VacinaService } from '../../services/vacina';
import { VacinacaoService } from '../../services/vacinacao';
import { FormsModule } from '@angular/forms';
import { Pessoa } from '../../interface/pessoa-interface';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-pessoa-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pessoa-list.html',
  styleUrl: './pessoa-list.css',
})

export class PessoaList implements OnInit {
  pessoas!: Pessoa[];
  pessoaId!: number;
  novoNome: string = '';
  pessoaSelecionada: number | null = null;
  pessoaDetalhe: Pessoa | null = null;

  vacinas: any[] = [];
  vacinacoes: any[] = [];
  dose!: number;
  data!: string;


  constructor(
    private route: ActivatedRoute,
    private pessoaService: PessoaService,
    private vacinaService: VacinaService,
    private vacinacaoService: VacinacaoService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.pessoaId = Number(this.route.snapshot.paramMap.get('id'));
    this.carregarVacinas();
    this.carregar();
  }

  carregarCartao() {
    this.pessoaService.obterCartao(this.pessoaId).subscribe(res => {
      this.vacinacoes = res;
    });
  }

  carregarVacinas() {
        // Assume-se que você tem o VacinaService injetado
        this.vacinaService.listar().subscribe({
            next: (res) => {
                this.vacinas = res;
            },
            error: (err) => console.error('Erro ao carregar vacinas:', err)
        });
    }

  onSelectionChange() {
    if (this.pessoaSelecionada) 
      {
        const pessoaEncontrada = this.pessoas.find(p => p.id === this.pessoaSelecionada);      
        this.pessoaDetalhe = pessoaEncontrada || null;
        this.carregarCartao();
      } 
      else 
      {
        this.pessoaDetalhe = null;
        this.vacinacoes = [];
      }
  }

  carregar() {
    this.pessoaService.listar().subscribe({
      next: (res) => this.pessoas = res
    });
  }

  criar() {
    if (!this.novoNome.trim()) 
    {
      return;
    }

    this.pessoaService.criar(this.novoNome).subscribe(() => {
      this.novoNome = ''; 
      this.carregar();
    });
  }

  excluir(id: number) {
    if (confirm('Deseja excluir esta pessoa?'))
    {
      this.pessoaService.remover(id).subscribe(() => this.carregar());
    }
  }

  abrirCartao(id: number) {
    this.router.navigate(['/pessoa', id]);
  }
}
