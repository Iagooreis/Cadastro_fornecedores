using backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("fornecedores")]
    public async Task<IActionResult> ListarFornecedores()
    {
        var fornecedores = await _context.Fornecedores
            .OrderBy(f => f.RazaoSocial)
            .Select(f => new
            {
                f.Id,
                f.Cnpj,
                f.RazaoSocial,
                f.NomeFantasia,
                f.Email,
                f.Telefone,
                f.Cidade,
                f.Uf,
                f.SituacaoCadastral,
                f.DataCadastro,
                f.DataAtualizacao
            })
            .ToListAsync();

        return Ok(fornecedores);
    }
}