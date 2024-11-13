using FacilitaDiario.Config;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FacilitaDiario
{
    internal class App
    {
        private static string Formatted(string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            string normalizedText = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new();

            foreach (char c in normalizedText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            string noAccents = stringBuilder.ToString();

            string formattedText = Regex.Replace(noAccents, @"([a-z])([A-Z])", "$1 $2");
            formattedText = Regex.Replace(formattedText, @"(\d)([A-Z])", "$1 $2");
            formattedText = Regex.Replace(formattedText, @"([A-Z])([A-Z][a-z])", "$1 $2");
            formattedText = Regex.Replace(formattedText, @"\s+", " ").Trim();

            return formattedText.ToUpper();
        }

        static async Task Main(string[] args)
        {
            Connection connection = new();
            var servants = await connection.GetServants();

            ChromeDriver driver = new();
            driver.Navigate().GoToUrl("https://www.iomat.mt.gov.br/");
            driver.Quit();
        }
    }
}
