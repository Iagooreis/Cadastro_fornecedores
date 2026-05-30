import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { FornecedorResponse, FornecedorService } from '../../core/services/fornecedor.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {
  fornecedores: FornecedorResponse[] = [];
  carregando = false;
  mensagemErro = '';

  constructor(
    private fornecedorService: FornecedorService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarFornecedores();
  }

  carregarFornecedores(): void {
    this.carregando = true;
    this.mensagemErro = '';

    this.fornecedorService.listarFornecedores().subscribe({
      next: (fornecedores) => {
        this.fornecedores = fornecedores;
        this.carregando = false;
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel carregar fornecedores.';
        this.carregando = false;
      }
    });
  }

  sair(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}