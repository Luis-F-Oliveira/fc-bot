using FacilitaDiario.Config;
using OpenQA.Selenium.Chrome;

namespace FacilitaDiario
{
    internal class App
    {
        static async Task Main(string[] args)
        {
            Connection connection = new();
            Connection.GetServants();

            //ChromeDriver driver = new();
            //driver.Navigate().GoToUrl("http://selenium.dev");
            //driver.Quit();
        }
    }
}
