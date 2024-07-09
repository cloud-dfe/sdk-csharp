using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Programs
{
    public static void Main()
    {
        var config = new Dictionary<string, object>
        {
            { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
            { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
            { "timeout", 60 },
            { "debug", false }
        };

        var nfe = new sdk_cloud_dfe.Nfe(config);

        try
        {
            var resp = Task.Run(async () => await nfe.Status()).GetAwaiter().GetResult();
            
            string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
            Console.WriteLine(jsonOutput);

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao obter o status da NF-e: {ex.Message}");
        }
    }
}
