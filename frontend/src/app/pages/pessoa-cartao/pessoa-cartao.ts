import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PessoaService } from '../../services/pessoa';
import { VacinaService } from '../../services/vacina';
import { VacinacaoService } from '../../services/vacinacao';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pessoa-cartao',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pessoa-cartao.html',
  styleUrl: './pessoa-cartao.css',
})

export class PessoaCartao implements OnInit {
  pessoaId!: number;
  vacinacoes: any[] = [];
  vacinas: any[] = [];

  vacinaSelecionada!: number;
  dose!: number;
  data!: string;

  constructor(
    private route: ActivatedRoute,
    private pessoaService: PessoaService,
    private vacinaService: VacinaService,
    private vacinacaoService: VacinacaoService,
    private router: Router
  ) {}

  carregarCartao() {
    this.pessoaService.obterCartao(this.pessoaId).subscribe(res => {
      this.vacinacoes = res;
    });
  }

  carregarVacinas() {
    this.vacinaService.listar().subscribe(res => this.vacinas = res);
  }

  ngOnInit(): void {
    this.pessoaId = Number(this.route.snapshot.paramMap.get('id'));
    this.carregarCartao();
    this.carregarVacinas();
  }

  registrar() {
    const dto = {
      pessoaId: this.pessoaId,
      vacinaId: this.vacinaSelecionada,
      dose: Number(this.dose),
      dataAplicacao: this.data
    };

    this.vacinacaoService.registrar(dto).subscribe({
      next: () => this.carregarCartao(),
      error: (err) => alert(err.error.erro)
    });
  }

  abrirPessoaList() {
    this.router.navigate(['']);
  }
}
