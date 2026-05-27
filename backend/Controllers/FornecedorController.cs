using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CnpjValidator _cnpjValidator;

    public FornecedorController(AppDbContext context, CnpjValidator cnpjValidator)
    {
        _context = context;
        _cnpjValidator = cnpjValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] FornecedorCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cnpjLimpo = new string(request.Cnpj.Where(char.IsDigit).ToArray());

        if (!_cnpjValidator.Validar(cnpjLimpo))
        {
            return BadRequest(new { message = "O CNPJ informado e invalido." });
        }

        var cnpjJaExiste = await _context.Fornecedores
            .AnyAsync(f => f.Cnpj == cnpjLimpo);

        if (cnpjJaExiste)
        {
            return Conflict(new { message = "Ja existe um fornecedor cadastrado com este CNPJ." });
        }

        var fornecedor = new Fornecedor
        {
            Cnpj = cnpjLimpo,
            RazaoSocial = request.RazaoSocial,
            NomeFantasia = request.NomeFantasia,
            Email = request.Email,
            Telefone = request.Telefone,
            Endereco = request.Endereco,
            Cidade = request.Cidade,
            Uf = request.Uf,
            AtividadePrincipal = request.AtividadePrincipal,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            SituacaoCadastral = request.SituacaoCadastral ?? "Ativa",
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();

        var response = MapearParaResponse(fornecedor);

        return CreatedAtAction(nameof(Criar), new { id = fornecedor.Id }, response);
    }

    private static FornecedorResponse MapearParaResponse(Fornecedor fornecedor)
    {
        return new FornecedorResponse
        {
            Id = fornecedor.Id,
            Cnpj = fornecedor.Cnpj,
            RazaoSocial = fornecedor.RazaoSocial,
            NomeFantasia = fornecedor.NomeFantasia,
            Email = fornecedor.Email,
            Telefone = fornecedor.Telefone,
            Endereco = fornecedor.Endereco,
            Cidade = fornecedor.Cidade,
            Uf = fornecedor.Uf,
            AtividadePrincipal = fornecedor.AtividadePrincipal,
            SituacaoCadastral = fornecedor.SituacaoCadastral,
            DataCadastro = fornecedor.DataCadastro,
            DataAtualizacao = fornecedor.DataAtualizacao
        };
    }
}