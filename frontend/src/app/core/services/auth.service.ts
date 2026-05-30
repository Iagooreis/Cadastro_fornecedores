import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

export interface LoginRequest {
  cnpj: string;
  senha: string;
}

export interface LoginResponse {
  token: string;
  nome: string;
  role: 'Fornecedor' | 'Admin';
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = '/api/auth';
  private readonly tokenKey = 'fornecedor_token';
  private readonly roleKey = 'fornecedor_role';
  private readonly nomeKey = 'fornecedor_nome';

  constructor(private http: HttpClient) {}

  login(dados: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, dados).pipe(
      tap((response) => {
        localStorage.setItem(this.tokenKey, response.token);
        localStorage.setItem(this.roleKey, response.role);
        localStorage.setItem(this.nomeKey, response.nome);
      })
    );
  }

  obterToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  obterRole(): string | null {
    return localStorage.getItem(this.roleKey);
  }

  obterNome(): string | null {
    return localStorage.getItem(this.nomeKey);
  }

  estaAutenticado(): boolean {
    return !!this.obterToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.roleKey);
    localStorage.removeItem(this.nomeKey);
  }
}