using FacilitaDiario.DataGateway;
using System.Globalization;
using Microsoft.Playwright;
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
            Servants connection = new();
            _ = await connection.GetServants();

            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.iomat.mt.gov.br/");

            try
            {
                await page.Locator("xpath=//*[@id='downloadPdf']").ClickAsync();
                await page.Locator("xpath=//*[@id='html-interna']/a").ClickAsync();

                await page.WaitForTimeoutAsync(3000);

                var element = page.Locator("li.expandable", new()
                {
                    HasText = "DEFENSORIA PÚBLICA"
                });
                await element.ClickAsync();

                Console.WriteLine(element);

                //var listItems = element.Locator("ul");

                //if (await listItems.IsVisibleAsync())
                //{
                //    Console.WriteLine($"Locator encontrado: {listItems}");
                //}
                //else
                //{
                //    Console.WriteLine("Não foi possivel achar o Locator");
                //}

                //var itemCount = await listItems.CountAsync();
                
                //Console.WriteLine($"Count: {itemCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await browser.CloseAsync();
            }
        }
    }
}
