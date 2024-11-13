using System.Text.Json;
using FacilitaDiario.Types;
using DotNetEnv;

namespace FacilitaDiario.Config
{
    internal class Connection
    {
        private static readonly HttpClient _httpClient = new();

        public Connection()
        {
            Env.TraversePath().Load();
        }

        public async Task<List<IServant>> GetServants()
        {
            try
            {
                string? backend_url = Environment.GetEnvironmentVariable("BACKEND_URL");
                string url = backend_url + "/api/get_servants";

                if (string.IsNullOrEmpty(backend_url))
                {
                    throw new InvalidOperationException("A variável de ambiente 'BACKEND_URL' não está definida.");
                }

                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    throw new HttpRequestException("A resposta do servidor está vazia.");
                }

                var servant = JsonSerializer.Deserialize<List<IServant>>(response);

                if (servant == null)
                {
                    throw new JsonException("Erro ao desserializar a resposta para o tipo esperado.");
                }

                return servant;
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter dados dos servos: {ex.Message}");
                return [];
            }
        }
    }
}
