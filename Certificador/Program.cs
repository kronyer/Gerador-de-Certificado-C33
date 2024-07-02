using Reading;
using Spectre.Console;
using Writing;

namespace Certificador
{
    public class Program
    {
        static void Main(string[] args)
        {
            string pastaCertificadosRelativa = @"Certificados";
            string pastaExcelRelativa = @"Excel";

            string diretorioAtual = Directory.GetCurrentDirectory();

            string pastaCertificadosCompleta = Path.Combine(diretorioAtual, pastaCertificadosRelativa);
            string pastaExcelCompleta = Path.Combine(diretorioAtual, pastaExcelRelativa);

            string[] certificados = Directory.GetFiles(pastaCertificadosCompleta);
            string[] planilhas = Directory.GetFiles(pastaExcelCompleta);


            if (planilhas.Length == 0)
            {
                Console.WriteLine("Nao há planilhas disponíveis");
            }

            var listaPlanilhas = new SelectionPrompt<string>().Title("Escolha a Planilha").PageSize(10);

            foreach (var planilha in planilhas)
            {
                listaPlanilhas.AddChoice(Path.GetFileName(planilha)); // Adiciona apenas o nome do arquivo, sem o caminho completo
            }

            string nomePlanilhaSelecionada = AnsiConsole.Prompt(listaPlanilhas);
            string caminhoPlanilhaSelecionada = Path.Combine(pastaExcelCompleta,nomePlanilhaSelecionada);


            if (certificados.Length == 0)
            {
                Console.WriteLine($"Nao há arquivos na pasta {pastaCertificadosCompleta}");
            }

            var listaCertificados = new SelectionPrompt<string>().Title("Escolha o certificado").PageSize(10);

            foreach (var certificado in certificados)
            {
                listaCertificados.AddChoice(Path.GetFileName(certificado)); // Adiciona apenas o nome do arquivo, sem o caminho completo
            }

            var certificadoSelecionado = AnsiConsole.Prompt(listaCertificados);

            var certificadoPath = Path.Combine(pastaCertificadosCompleta,certificadoSelecionado);



            List<string> names = ExcelReader.ReadUserNameFromExcel(caminhoPlanilhaSelecionada, "Buyer's info");
            //pensar num appsettings.json

            foreach (string name in names)
            {
                PdfWriter.ReplaceText(name, pastaCertificadosCompleta, certificadoPath);
            }
        }
    }
}
