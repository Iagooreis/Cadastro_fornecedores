using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Fornecedor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O CNPJ e obrigatorio.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter exatamente 14 digitos.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "A razao social e obrigatoria.")]
    public string RazaoSocial { get; set; } = string.Empty;

    public string? NomeFantasia { get; set; }

    [Required(ErrorMessage = "O e-mail e obrigatorio.")]
    [EmailAddress(ErrorMessage = "O e-mail informado nao e valido.")]
    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public string? Endereco { get; set; }

    public string? Cidade { get; set; }

    public string? Uf { get; set; }

    public string? AtividadePrincipal { get; set; }

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public string? SituacaoCadastral { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}