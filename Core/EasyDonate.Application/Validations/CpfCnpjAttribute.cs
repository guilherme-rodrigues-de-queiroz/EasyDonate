using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EasyDonate.Application.Validations;

public class CpfCnpjAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var document = Regex.Replace(value.ToString()!, @"\D", "");

        if (document.Length == 11 && IsValidCpf(document))
            return ValidationResult.Success;

        if (document.Length == 14 && IsValidCnpj(document))
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage ?? "Documento inválido.");
    }

    private static bool IsValidCpf(string cpf)
    {
        if (new string(cpf[0], cpf.Length) == cpf) return false;

        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (tempCpf[i] - '0') * multiplier1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (tempCpf[i] - '0') * multiplier2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (new string(cnpj[0], cnpj.Length) == cnpj) return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj[..12];
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += (tempCnpj[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += (tempCnpj[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
