using System.Text.Json;
using System.Text.Json.Serialization;

namespace backend.Services;

public class BrasilApiService
{
    private readonly HttpClient _httpClient;

    public BrasilApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BrasilApiCnpjResponse?> ConsultarCnpjAsync(string cnpj)
    {
        try
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            var url = $"https://brasilapi.com.br/api/cnpj/v1/{cnpj}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"BrasilAPI erro: {(int)response.StatusCode} - {erro}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<BrasilApiCnpjResponse>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar BrasilAPI: {ex.Message}");
            return null;
        }
    }
}

public class BrasilApiCnpjResponse
{
    [JsonPropertyName("cnpj")]
    public string? Cnpj { get; set; }

    [JsonPropertyName("razao_social")]
    public string? RazaoSocial { get; set; }

    [JsonPropertyName("nome_fantasia")]
    public string? NomeFantasia { get; set; }

    [JsonPropertyName("descricao_situacao_cadastral")]
    public string? SituacaoCadastral { get; set; }

    [JsonPropertyName("cnae_fiscal_descricao")]
    public string? AtividadePrincipal { get; set; }

    [JsonPropertyName("logradouro")]
    public string? Logradouro { get; set; }

    [JsonPropertyName("numero")]
    public string? Numero { get; set; }

    [JsonPropertyName("complemento")]
    public string? Complemento { get; set; }

    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }

    [JsonPropertyName("municipio")]
    public string? Municipio { get; set; }

    [JsonPropertyName("uf")]
    public string? Uf { get; set; }

    [JsonPropertyName("cep")]
    public string? Cep { get; set; }

    [JsonPropertyName("ddd_telefone_1")]
    public string? Telefone { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}