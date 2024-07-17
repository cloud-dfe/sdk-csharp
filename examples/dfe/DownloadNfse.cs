using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var dfe = new Dfe(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"chave", "50000000000000000000000000000000000000000000"}
    };

    var resp = Task.Run(async () => await dfe.DownloadNfse(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

    if (resp.ContainsKey("sucesso") && Convert.ToBoolean(resp["sucesso"]))
    {
        var doc = resp.ContainsKey("doc") ? resp["doc"] as Dictionary<string, object> : null;

        string xmlBase64 = doc.ContainsKey("xml") ? doc["xml"].ToString() : string.Empty;
        string pdfBase64 = doc.ContainsKey("pdf") ? doc["pdf"].ToString() : string.Empty;

        byte[] xmlBytes = Convert.FromBase64String(xmlBase64);
        byte[] pdfBytes = Convert.FromBase64String(pdfBase64);

        await File.WriteAllBytesAsync("document.xml", xmlBytes);
        await File.WriteAllBytesAsync("document.pdf", pdfBytes);
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
