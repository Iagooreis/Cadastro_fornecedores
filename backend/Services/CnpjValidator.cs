namespace backend.Services;

public class CnpjValidator
{
    public bool Validar(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)
            return false;

        if (cnpj.All(digito => digito == cnpj[0]))
            return false;

        int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var soma = 0;

        for (var i = 0; i < 12; i++)
        {
            soma += (cnpj[i] - '0') * multiplicadoresPrimeiroDigito[i];
        }

        var resto = soma % 11;
        var primeiroDigito = resto < 2 ? 0 : 11 - resto;

        if ((cnpj[12] - '0') != primeiroDigito)
            return false;

        int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        soma = 0;

        for (var i = 0; i < 13; i++)
        {
            soma += (cnpj[i] - '0') * multiplicadoresSegundoDigito[i];
        }

        resto = soma % 11;
        var segundoDigito = resto < 2 ? 0 : 11 - resto;

        return (cnpj[13] - '0') == segundoDigito;
    }
}