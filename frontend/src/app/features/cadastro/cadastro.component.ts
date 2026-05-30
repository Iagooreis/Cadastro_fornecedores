import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CnpjService } from '../../core/services/cnpj.service';
import { FornecedorCreateRequest, FornecedorService } from '../../core/services/fornecedor.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  carregandoCnpj = false;
  enviando = false;
  mensagemSucesso = '';
  mensagemErro = '';

  fornecedor: FornecedorCreateRequest = {
    cnpj: '',
    razaoSocial: '',
    nomeFantasia: '',
    email: '',
    telefone: '',
    endereco: '',
    cidade: '',
    uf: '',
    atividadePrincipal: '',
    senha: '',
    situacaoCadastral: 'Ativa'
  };

  constructor(
    private cnpjService: CnpjService,
    private fornecedorService: FornecedorService
  ) {}

  consultarCnpj(): void {
    this.limparMensagens();

    if (!this.fornecedor.cnpj || this.fornecedor.cnpj.trim().length < 14) {
      this.mensagemErro = 'Informe um CNPJ valido para consultar.';
      return;
    }

    this.carregandoCnpj = true;

    this.cnpjService.consultar(this.fornecedor.cnpj).subscribe({
      next: (dados) => {
        this.fornecedor.razaoSocial = dados.razaoSocial ?? '';
        this.fornecedor.nomeFantasia = dados.nomeFantasia ?? '';
        this.fornecedor.email = dados.email ?? this.fornecedor.email;
        this.fornecedor.telefone = dados.telefone ?? '';
        this.fornecedor.endereco = dados.endereco ?? '';
        this.fornecedor.cidade = dados.cidade ?? '';
        this.fornecedor.uf = dados.uf ?? '';
        this.fornecedor.atividadePrincipal = dados.atividadePrincipal ?? '';
        this.fornecedor.situacaoCadastral = dados.situacaoCadastral ?? 'Ativa';
        this.carregandoCnpj = false;
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel consultar o CNPJ.';
        this.carregandoCnpj = false;
      }
    });
  }

  cadastrar(): void {
    this.limparMensagens();
    this.enviando = true;

    this.fornecedorService.criarFornecedor(this.fornecedor).subscribe({
      next: () => {
        this.mensagemSucesso = 'Fornecedor cadastrado com sucesso.';
        this.enviando = false;
        this.limparFormulario();
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel cadastrar o fornecedor.';
        this.enviando = false;
      }
    });
  }
  private limparFormulario(): void {
  this.fornecedor = {
    cnpj: '',
    razaoSocial: '',
    nomeFantasia: '',
    email: '',
    telefone: '',
    endereco: '',
    cidade: '',
    uf: '',
    atividadePrincipal: '',
    senha: '',
    situacaoCadastral: 'Ativa'
  };
}

  private limparMensagens(): void {
    this.mensagemSucesso = '';
    this.mensagemErro = '';
  }
}