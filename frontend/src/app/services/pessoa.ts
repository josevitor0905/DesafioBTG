import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pessoa } from '../interface/pessoa-interface';

@Injectable({
  providedIn: 'root',
})

export class PessoaService {
  private api = 'http://localhost:5011/api/Pessoa'

  constructor(private http: HttpClient) {}

  listar(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(this.api);
  }

  criar(nome: string, idade: number, sexo: string): Observable<any> {
    return this.http.post<any>(this.api, { nome, idade, sexo });
  }

  remover(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }

  obterCartao(id: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/${id}/cartao`);
  }

}
