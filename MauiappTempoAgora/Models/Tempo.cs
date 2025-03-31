// Define o namespace da aplicação, que organiza as classes do projeto
namespace MauiappTempoAgora.Models
{
    // Define a classe Tempo, que será usada para armazenar os dados da previsão do tempo
    public class Tempo
    {
        // Propriedade interna (não acessível fora da classe) para armazenar a condição principal do clima
        internal string? main;

        // Propriedade que armazena a longitude da localização consultada
        public double? lon { get; set; }

        // Propriedade que armazena a latitude da localização consultada
        public double? lat { get; set; }

        // Propriedade que armazena a visibilidade em metros (pode ser nula caso a API não forneça o dado)
        public int? visibility { get; set; }

        // Propriedade que armazena a temperatura mínima prevista
        public double? temp_min { get; set; }

        // Propriedade que armazena a temperatura máxima prevista
        public double? temp_max { get; set; }

        // Propriedade que armazena o horário do nascer do sol em formato de string
        public string? sunrise { get; set; }

        // Propriedade que armazena o horário do pôr do sol em formato de string
        public string? sunset { get; set; }

        // Propriedade que armazena a descrição do clima (exemplo: "céu limpo", "nublado", etc.)
        public string? description { get; set; }

        // Propriedade que armazena a velocidade do vento em metros por segundo
        public double? speed { get; set; }
    }
}
