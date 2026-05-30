import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  FornecedorResponse,
  FornecedorService,
  FornecedorUpdateRequest
} from '../../core/services/fornecedor.service';

@Component({
  selector: 'app-area-restrita',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './area-restrita.component.html',
  styleUrl: './area-restrita.component.scss'
})
export class AreaRestritaComponent implements OnInit {
  fornecedor: FornecedorResponse | null = null;
  form: FornecedorUpdateRequest = {
    razaoSocial: '',
    nomeFantasia: '',
    email: '',
    telefone: '',
    endereco: '',
    cidade: '',
    uf: '',
    atividadePrincipal: ''
  };

  carregando = false;
  salvando = false;
  mensagemSucesso = '';
  mensagemErro = '';

  constructor(
    private fornecedorService: FornecedorService,
 
  ) {}

  ngOnInit(): void {
    this.carregarDados();
  }

  carregarDados(): void {
    this.carregando = true;
    this.mensagemErro = '';

    this.fornecedorService.obterMeusDados().subscribe({
      next: (fornecedor) => {
        this.fornecedor = fornecedor;
        this.form = {
          razaoSocial: fornecedor.razaoSocial,
          nomeFantasia: fornecedor.nomeFantasia,
          email: fornecedor.email,
          telefone: fornecedor.telefone,
          endereco: fornecedor.endereco,
          cidade: fornecedor.cidade,
          uf: fornecedor.uf,
          atividadePrincipal: fornecedor.atividadePrincipal
        };
        this.carregando = false;
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel carregar seus dados.';
        this.carregando = false;
      }
    });
  }

  salvar(): void {
    this.mensagemErro = '';
    this.mensagemSucesso = '';
    this.salvando = true;

    this.fornecedorService.atualizarMeusDados(this.form).subscribe({
      next: (fornecedor) => {
        this.fornecedor = fornecedor;
        this.mensagemSucesso = 'Dados atualizados com sucesso.';
        this.salvando = false;
      },
      error: (erro) => {
        this.mensagemErro = erro.error?.message ?? 'Nao foi possivel atualizar seus dados.';
        this.salvando = false;
      }
    });
  }

  
}