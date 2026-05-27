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

        if (resultado == null)
        {
            return NotFound(new { message = "Nao foi possivel consultar o CNPJ informado." });
        }

        var resposta = new
        {
            cnpj = resultado.Cnpj,
            razaoSocial = resultado.RazaoSocial,
            nomeFantasia = resultado.NomeFantasia,
            situacaoCadastral = resultado.SituacaoCadastral,
            atividadePrincipal = resultado.AtividadePrincipal,
            endereco = FormatarEndereco(resultado),
            cidade = resultado.Municipio,
            uf = resultado.Uf,
            cep = resultado.Cep,
            telefone = resultado.Telefone,
            email = resultado.Email
        };

        return Ok(resposta);
    }

    private static string FormatarEndereco(BrasilApiCnpjResponse dados)
    {
        var partes = new List<string>();

        if (!string.IsNullOrWhiteSpace(dados.Logradouro))
            partes.Add(dados.Logradouro);

        if (!string.IsNullOrWhiteSpace(dados.Numero))
            partes.Add(dados.Numero);

        if (!string.IsNullOrWhiteSpace(dados.Complemento))
            partes.Add(dados.Complemento);

        if (!string.IsNullOrWhiteSpace(dados.Bairro))
            partes.Add(dados.Bairro);

        return string.Join(", ", partes);
    }
}