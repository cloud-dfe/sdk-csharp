using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var ciot = new Ciot(config);

var payload = new Dictionary<string, object>
{
    {"numero", "1001"},
    {"identificacao", "PEDIDO-77"},
    {"tipo_operacao", "1"},
    {"data_inicio", "2026-11-10"},
    {"data_fim", "2026-11-12"},
    {"valor_frete", "4550.92"},
    {"contratante", new Dictionary<string, object>
    {
        {"cnpj", "11222333000181"},
        {"nome", "TRANSPORTADORA EXEMPLO LTDA"},
        {"rntrc", "12345678"}
    }},
    {"contratado", new Dictionary<string, object>
    {
        {"cnpj", "22333444000181"},
        {"nome", "TRANSPORTADOR CONTRATADO LTDA"},
        {"rntrc", "87654321"}
    }},
    {"destinatario", new Dictionary<string, object>
    {
        {"cnpj", "44555666000199"},
        {"nome", "DESTINATARIO EXEMPLO LTDA"}
    }},
    {"motorista", new Dictionary<string, object>
    {
        {"cpf", "12345678909"},
        {"nome", "MOTORISTA EXEMPLO"}
    }},
    {"veiculos", new List<object>
    {
        new Dictionary<string, object>
        {
            {"placa", "BCD2E34"},
            {"eixos", "2"},
            {"rntrc", "87654321"}
        }
    }},
    {"carga", new Dictionary<string, object>
    {
        {"tipo", "05"},
        {"natureza", "8414"},
        {"peso", "1520"}
    }},
    {"viagens", new List<object>
    {
        new Dictionary<string, object>
        {
            {"quantidade", "1"},
            {"distancia", "560"},
            {"origem", new Dictionary<string, object>
            {
                {"cep", "85000000"},
                {"codigo_municipio", "4121604"},
                {"municipio", "Renascença"},
                {"uf", "PR"}
            }},
            {"destino", new Dictionary<string, object>
            {
                {"cep", "88300000"},
                {"codigo_municipio", "4208203"},
                {"municipio", "Itajaí"},
                {"uf", "SC"}
            }}
        }
    }},
    {"pagamentos", new List<object>
    {
        new Dictionary<string, object>
        {
            {"forma", "1"},
            {"tipo", "5"},
            {"cnpj", "22333444000181"},
            {"chave_pix", "22333444000181"},
            {"vencimento", "2026-11-12"},
            {"valor", "4550.92"}
        }
    }}
};

try
{
    var resp = Task.Run(async () => await ciot.Cria(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
