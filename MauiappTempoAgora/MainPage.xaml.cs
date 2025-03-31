// Importa os modelos e serviços da aplicação
using MauiappTempoAgora.Models;
using MauiappTempoAgora.Services;

// Define o namespace da aplicação, organizando as classes do projeto
namespace MauiappTempoAgora
{
    // Define a classe MainPage, que representa a página principal da aplicação
    public partial class MainPage : ContentPage
    {
        // Declara uma variável inteira chamada "count", usada para contagem (não está sendo usada no código atual)
        int count = 0;

        // Construtor da classe MainPage, chamado ao inicializar a tela
        public MainPage()
        {
            InitializeComponent(); // Inicializa os componentes da interface definidos no XAML
        }

        // Método assíncrono que é chamado quando o botão de busca é pressionado
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            try
            {
                // Verifica se o campo de texto não está vazio
                if (!string.IsNullOrEmpty(txt_cidade.Text)) // Corrigido "text" para "Text"
                {
                    // Chama o serviço de dados para obter a previsão do tempo da cidade digitada
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    // Se os dados forem retornados com sucesso
                    if (t != null)
                    {
                        // Formata os dados da previsão para exibição
                        string dados_previsao =
                            $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nascer do Sol: {t.sunrise}\n" +
                            $"Pôr do Sol: {t.sunset}\n" +
                            $"Temp Máx: {t.temp_max}\n" +
                            $"Temp Mín: {t.temp_min}\n" +
                            $"Visibilidade: {t.visibility}\n" +
                            $"Vento: {t.speed} \n"+
                            $"Descrição: {t.description} \n"
                            ;

                        // Exibe os dados formatados na label lbl_res
                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        // Caso não haja retorno da API, exibe uma mensagem na tela
                        lbl_res.Text = "Sem dados de previsão";
                    }
                }
                else
                {
                    // Exibe uma mensagem caso o campo de cidade esteja vazio
                    lbl_res.Text = "Preencha a cidade";
                }
            }
            catch (Exception ex)
            {
                // Exibe um alerta caso ocorra algum erro durante a execução do código
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
