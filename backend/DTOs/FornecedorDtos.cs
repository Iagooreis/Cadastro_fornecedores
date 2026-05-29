using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class LoginRequest
{
    [Required(ErrorMessage = "O CNPJ e obrigatorio.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha e obrigatoria.")]
    public string Senha { get; set; } = string.Empty;
}
public class FornecedorCreateRequest
{
    [Required(ErrorMessage = "O CNPJ e obrigatorio.")]
    [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre 14 e 18 caracteres.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "A razao social e obrigatoria.")]
    [StringLength(200)]
    public string RazaoSocial { get; set; } = string.Empty;

    [StringLength(200)]
    public string? NomeFantasia { get; set; }

    [Required(ErrorMessage = "O e-mail e obrigatorio.")]
    [EmailAddress(ErrorMessage = "O e-mail informado nao e valido.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(300)]
    public string? Endereco { get; set; }

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Uf { get; set; }

    [StringLength(300)]
    public string? AtividadePrincipal { get; set; }

    [Required(ErrorMessage = "A senha e obrigatoria.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    [StringLength(50)]
    public string? SituacaoCadastral { get; set; }
}

public class FornecedorResponse
{
    public int Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string? NomeFantasia { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
    public string? AtividadePrincipal { get; set; }
    public string? SituacaoCadastral { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}

public class FornecedorUpdateRequest
{
    [Required(ErrorMessage = "A razao social e obrigatoria.")]
    [StringLength(200)]
    public string RazaoSocial { get; set; } = string.Empty;

    [StringLength(200)]
    public string? NomeFantasia { get; set; }

    [Required(ErrorMessage = "O e-mail e obrigatorio.")]
    [EmailAddress(ErrorMessage = "O e-mail informado nao e valido.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(300)]
    public string? Endereco { get; set; }

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Uf { get; set; }

    [StringLength(300)]
    public string? AtividadePrincipal { get; set; }

    
}