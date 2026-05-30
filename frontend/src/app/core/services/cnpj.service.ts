import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface CnpjResponse {
  cnpj: string;
  razaoSocial: string;
  nomeFantasia?: string | null;
  situacaoCadastral?: string | null;
  atividadePrincipal?: string | null;
  endereco?: string | null;
  cidade?: string | null;
  uf?: string | null;
  cep?: string | null;
  telefone?: string | null;
  email?: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class CnpjService {
  private readonly apiUrl = '/api/cnpj';

  constructor(private http: HttpClient) {}

  consultar(cnpj: string): Observable<CnpjResponse> {
  const cnpjLimpo = cnpj.replace(/\D/g, '');
  return this.http.get<CnpjResponse>(`${this.apiUrl}/${cnpjLimpo}`);
  }
}