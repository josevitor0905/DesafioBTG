import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VacinacaoService {
  private api = 'https://localhost:5011/api/vacinacao'

  constructor(private http: HttpClient) {}

  registrar(dto: any): Observable<any> {
    return this.http.post<any>(this.api, dto);
  }

  remover(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
