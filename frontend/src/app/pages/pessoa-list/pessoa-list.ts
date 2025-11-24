import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PessoaService } from '../../services/pessoa';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pessoa-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pessoa-list.html',
  styleUrl: './pessoa-list.css',
})

export class PessoaList implements OnInit {
  pessoas: any[] = [];
  novoNome: string = '';

  constructor(
    private PessoaService: PessoaService,
    private router: Router
  ) {}

  carregar() {
    this.PessoaService.listar().subscribe({
      next: (res) => this.pessoas = res
    });
  }

  ngOnInit(): void {
    this.carregar();
  }

  criar() {
    if (!this.novoNome.trim()) 
    {
      return;
    }

    this.PessoaService.criar(this.novoNome).subscribe(() => {
      this.novoNome = ''; 
      this.carregar();
    });
  }

  excluir(id: number) {
    if (confirm('Deseja excluir esta pessoa?'))
    {
      this.PessoaService.remover(id).subscribe(() => this.carregar());
    }
  }

  abrirCartao(id: number) {
    this.router.navigate(['/pessoa', id]);
  }
}
