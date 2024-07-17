using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfse = new Nfse(config);

string xmlFile = Util.ReadFile("caminho_do_arquivo.xml");

string xmlFileBase64 = Util.Encode(xmlFile);

var payload = new Dictionary<string, object>
{
    { "chave", "50000000000000000000000000000000000000000000" },
    { "xml", xmlFileBase64 }, 
};

try
{
    var resp = Task.Run(async () => await nfse.Conflito(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
