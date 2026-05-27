using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cnpjLimpo = new string(request.Cnpj.Where(char.IsDigit).ToArray());

        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.Cnpj == cnpjLimpo);

        if (fornecedor == null)
        {
            return Unauthorized(new { message = "CNPJ ou senha invalidos." });
        }

        var senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, fornecedor.SenhaHash);

        if (!senhaValida)
        {
            return Unauthorized(new { message = "CNPJ ou senha invalidos." });
        }

        return Ok(new
        {
            message = "Login realizado com sucesso.",
            fornecedor = new
            {
                fornecedor.Id,
                fornecedor.Cnpj,
                fornecedor.RazaoSocial,
                fornecedor.Email
            }
        });
    }
}