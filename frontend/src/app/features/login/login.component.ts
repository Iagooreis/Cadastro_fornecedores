import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  cnpj = '';
  senha = '';
  carregando = false;
  mensagemErro = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  entrar(): void {
    this.mensagemErro = '';
    this.carregando = true;

    this.authService.login({
      cnpj: this.cnpj,
      senha: this.senha
    }).subscribe({
      next: (response) => {
        this.carregando = false;

        if (response.role === 'Admin') {
          this.router.navigate(['/admin']);
          return;
        }

        this.router.navigate(['/area-restrita']);
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel realizar login.';
        this.carregando = false;
      }
    });
  }
}