using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CnpjController : ControllerBase
{
    private readonly BrasilApiService _brasilApiService;
    private readonly CnpjValidator _cnpjValidator;

    public CnpjController(BrasilApiService brasilApiService, CnpjValidator cnpjValidator)
    {
        _brasilApiService = brasilApiService;
        _cnpjValidator = cnpjValidator;
    }

    [HttpGet("{cnpj}")]
    public async Task<IActionResult> Consultar(string cnpj)
    {
        var cnpjLimpo = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpjLimpo.Length != 14)
        {
            return BadRequest(new { message = "O CNPJ deve conter exatamente 14 digitos." });
        }

        if (!_cnpjValidator.Validar(cnpjLimpo))
        {
            return BadRequest(new { message = "O CNPJ informado e invalido." });
        }

        var resultado = await _brasilApiService.ConsultarCnpjAsync(cnpjLimpo);

        if (!resultado.Sucesso || resultado.Dados == null)
        {
            if (resultado.StatusCode == 404)
            {
                return NotFound(new { message = "CNPJ nao encontrado na BrasilAPI." });
            }

            if (resultado.StatusCode == 429)
            {
                return StatusCode(429, new { message = "Limite de consultas da BrasilAPI atingido. Tente novamente mais tarde." });
            }

            if (resultado.StatusCode == 503)
            {
                return StatusCode(503, new { message = "BrasilAPI indisponivel no momento. Tente novamente mais tarde." });
            }

            if (resultado.StatusCode == 504)
            {
                return StatusCode(504, new { message = "Tempo limite excedido ao consultar a BrasilAPI." });
            }

            return StatusCode(502, new { message = "Erro ao consultar servico externo de CNPJ." });
        }

        var dados = resultado.Dados;

        var resposta = new
        {
            cnpj = dados.Cnpj,
            razaoSocial = dados.RazaoSocial,
            nomeFantasia = dados.NomeFantasia,
            situacaoCadastral = dados.SituacaoCadastral,
            atividadePrincipal = dados.AtividadePrincipal,
            endereco = FormatarEndereco(dados),
            cidade = dados.Municipio,
            uf = dados.Uf,
            cep = dados.Cep,
            telefone = dados.Telefone,
            email = dados.Email
        };

        return Ok(resposta);
    }

    private static string FormatarEndereco(BrasilApiCnpjResponse dados)
    {
        var partes = new List<string>();

        if (!string.IsNullOrWhiteSpace(dados.Logradouro))
        {
            partes.Add(dados.Logradouro);
        }

        if (!string.IsNullOrWhiteSpace(dados.Numero))
        {
            partes.Add(dados.Numero);
        }

        if (!string.IsNullOrWhiteSpace(dados.Complemento))
        {
            partes.Add(dados.Complemento);
        }

        if (!string.IsNullOrWhiteSpace(dados.Bairro))
        {
            partes.Add(dados.Bairro);
        }

        return string.Join(", ", partes);
    }
}