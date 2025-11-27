import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VacinaService {
  private api = 'http://localhost:5011/api/Vacina'

  constructor(private http: HttpClient) {}

  criar(nome: string): Observable<any> {
    return this.http.post<any>(this.api, { nome });
  }

  listar(): Observable<any[]> {
    return this.http.get<any[]>(this.api);
  }

  remover(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
