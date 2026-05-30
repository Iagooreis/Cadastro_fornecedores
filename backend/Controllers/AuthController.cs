using backend.Data;
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(
        AppDbContext context,
        TokenService tokenService,
        IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var adminUser = _configuration["Admin:User"] ?? "admin";
        var adminPassword = _configuration["Admin:Password"] ?? "admin123";

        if (request.Cnpj.Equals(adminUser, StringComparison.OrdinalIgnoreCase))
        {
            if (request.Senha != adminPassword)
            {
                return Unauthorized(new { message = "CNPJ ou senha invalidos." });
            }

            var adminToken = _tokenService.GerarToken(adminUser, "Administrador", "Admin");

            return Ok(new
            {
                token = adminToken,
                nome = "Administrador",
                role = "Admin"
            });
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

        var token = _tokenService.GerarToken(
    fornecedor.Cnpj,
    fornecedor.RazaoSocial,
    "Fornecedor"
);

        return Ok(new
        {
            token,
            nome = fornecedor.RazaoSocial,
            role = "Fornecedor"
        });
    }
}