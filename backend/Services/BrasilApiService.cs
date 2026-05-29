using System.Text.Json;
using System.Text.Json.Serialization;

namespace backend.Services;

public class BrasilApiResult
{
    public bool Sucesso { get; set; }
    public int StatusCode { get; set; }
    public string? MensagemErro { get; set; }
    public BrasilApiCnpjResponse? Dados { get; set; }
}

public class BrasilApiService
{
    private readonly HttpClient _httpClient;

    public BrasilApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BrasilApiResult> ConsultarCnpjAsync(string cnpj)
    {
        try
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            var response = await _httpClient.GetAsync($"api/cnpj/v1/{cnpj}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new BrasilApiResult
                {
                    Sucesso = false,
                    StatusCode = (int)response.StatusCode,
                    MensagemErro = content
                };
            }

            var dados = JsonSerializer.Deserialize<BrasilApiCnpjResponse>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return new BrasilApiResult
            {
                Sucesso = true,
                StatusCode = (int)response.StatusCode,
                Dados = dados
            };
        }
        catch (TaskCanceledException)
        {
            return new BrasilApiResult
            {
                Sucesso = false,
                StatusCode = 504,
                MensagemErro = "Tempo limite excedido ao consultar a BrasilAPI."
            };
        }
        catch (HttpRequestException ex)
        {
            return new BrasilApiResult
            {
                Sucesso = false,
                StatusCode = 503,
                MensagemErro = ex.Message
            };
        }
        catch (Exception ex)
        {
            return new BrasilApiResult
            {
                Sucesso = false,
                StatusCode = 500,
                MensagemErro = ex.Message
            };
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