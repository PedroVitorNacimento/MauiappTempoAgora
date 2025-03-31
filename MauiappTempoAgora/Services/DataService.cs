// Importa os modelos da aplicação
using MauiappTempoAgora.Models;

// Importa a biblioteca Newtonsoft.Json.Linq para manipular JSON
using Newtonsoft.Json.Linq;

namespace MauiappTempoAgora.Services
{
    // Define a classe DataService, responsável por buscar os dados da previsão do tempo
    public class DataService
    {
        // Método assíncrono que busca os dados da previsão do tempo com base no nome da cidade
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            // Se a cidade estiver vazia, retorna null imediatamente
            if (string.IsNullOrWhiteSpace(cidade))
            {
                return null;
            }

            // Chave de API usada para acessar o OpenWeatherMap
            string chave = "f87fb1c9c9ec73daf0cdc836e6b4603b";

            // Monta a URL da requisição à API de previsão do tempo
            string url = $"http://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            try
            {
                // Verifica se há conexão com a Internet
                if (!Connectivity.NetworkAccess.HasFlag(NetworkAccess.Internet))
                {
                    throw new Exception("Sem conexão com a Internet. Verifique sua conexão e tente novamente.");
                }

                // Cria um cliente HTTP para fazer a requisição
                using (HttpClient client = new HttpClient())
                {
                    // Faz a requisição GET para a API usando a URL montada
                    HttpResponseMessage resp = await client.GetAsync(url);

                    // Se a cidade não existir, a API retorna código 404 (Not Found)
                    if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null; // Retorna null para indicar que a cidade não foi encontrada
                    }

                    // Se a requisição foi bem-sucedida (código 200 OK)
                    if (resp.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo da resposta e armazena na variável json
                        string json = await resp.Content.ReadAsStringAsync();

                        // Converte a string JSON em um objeto JObject para facilitar a manipulação dos dados
                        var rascunho = JObject.Parse(json);

                        // Obtém o horário do nascer do sol e converte para o horário local
                        DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds((long)rascunho["sys"]["sunrise"]).ToLocalTime().DateTime;

                        // Obtém o horário do pôr do sol e converte para o horário local
                        DateTime sunset = DateTimeOffset.FromUnixTimeSeconds((long)rascunho["sys"]["sunset"]).ToLocalTime().DateTime;

                        // Cria um novo objeto Tempo e preenche com os dados obtidos da API
                        return new Tempo
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int)rascunho["visibility"],
                            sunrise = sunrise.ToString("HH:mm:ss"),
                            sunset = sunset.ToString("HH:mm:ss"),
                        };
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Se houver erro de conexão com a Internet
                throw new Exception("Não foi possível conectar ao servidor. Verifique sua conexão com a Internet.");
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro inesperado, exibe a mensagem no console (pode ser alterado para um log)
                Console.WriteLine($"Erro ao buscar previsão: {ex.Message}");

                // Repassa o erro para ser tratado na interface do usuário
                throw;
            }

            return null;
        }
    }
}
