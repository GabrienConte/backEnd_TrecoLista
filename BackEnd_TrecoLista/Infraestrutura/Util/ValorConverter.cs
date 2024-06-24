using System.Globalization;

namespace BackEnd_TrecoLista.Infraestrutura.Util
{
    public static class ValorConverter
    {
        public static double ConvertPrice(string preco)
        {
            // Remove todos os caracteres não numéricos, exceto o ponto e a vírgula
            var cleanedPrice = new string(preco.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());

            // Substituir vírgula por ponto se necessário
            cleanedPrice = cleanedPrice.Replace(',', '.');

            // Converter para double
            if (double.TryParse(cleanedPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            throw new FormatException($"Formato de preço inválido: {preco}");
        }
    }
}
