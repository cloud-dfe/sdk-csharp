using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var cte = new Cte(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"cfop", "5932"},
        {"natureza_operacao", "PRESTACAO DE SERVIÇO"},
        {"numero", ""},
        {"serie", ""},
        {"data_emissao", ""},
        {"tipo_operacao", ""},
        {"codigo_municipio_inicio", ""},
        {"nome_municipio_inicio", ""},
        {"uf_envio", ""},
        {"tipo_servico", ""},
        {"codigo_municipio_inicio", ""},
        {"nome_municipio_inicio", ""},
        {"uf_inicio", ""},
        {"codigo_municipio_fim", ""},
        {"nome_municipio_fim", ""},
        {"uf_fim": "RN"},
        {"retirar_mercadoria", "1"},
        {"detalhes_retirar", null},
        {"tipo_programacao_entrega", "0"},
        {"sem_hora_tipo_hora_programada", "0"},
        {"remetente": new Dictionary<string, object>
            {
                {"cpf": "01234567890"},
                {"inscricao_estadual": None},
                {"nome": "EMPRESA MODELO"},
                {"razao_social": "MODELO LTDA"},
                {"telefone": "8433163070"},
            }
        }

    };

    //Não terminado

    var resp = Task.Run(async () => await cte.Consulta(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
