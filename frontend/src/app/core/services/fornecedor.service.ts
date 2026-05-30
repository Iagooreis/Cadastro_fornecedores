import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface FornecedorCreateRequest {
  cnpj: string;
  razaoSocial: string;
  nomeFantasia?: string | null;
  email: string;
  telefone?: string | null;
  endereco?: string | null;
  cidade?: string | null;
  uf?: string | null;
  atividadePrincipal?: string | null;
  senha: string;
  situacaoCadastral?: string | null;
}

export interface FornecedorResponse {
  id: number;
  cnpj: string;
  razaoSocial: string;
  nomeFantasia?: string | null;
  email: string;
  telefone?: string | null;
  endereco?: string | null;
  cidade?: string | null;
  uf?: string | null;
  atividadePrincipal?: string | null;
  situacaoCadastral?: string | null;
  dataCadastro: string;
  dataAtualizacao?: string | null;
}

export interface FornecedorUpdateRequest {
  razaoSocial: string;
  nomeFantasia?: string | null;
  email: string;
  telefone?: string | null;
  endereco?: string | null;
  cidade?: string | null;
  uf?: string | null;
  atividadePrincipal?: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {
  private readonly apiUrl = 'http://localhost:5260/api/fornecedor';

  constructor(private http: HttpClient) {}

  criarFornecedor(dados: FornecedorCreateRequest): Observable<FornecedorResponse> {
    return this.http.post<FornecedorResponse>(this.apiUrl, dados);
  }

  obterMeusDados(): Observable<FornecedorResponse> {
    return this.http.get<FornecedorResponse>(`${this.apiUrl}/me`);
  }

  atualizarMeusDados(dados: FornecedorUpdateRequest): Observable<FornecedorResponse> {
    return this.http.put<FornecedorResponse>(`${this.apiUrl}/me`, dados);
  }
}