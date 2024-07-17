# SDK em C# para API Integra Notas

Este SDK visa simplificar a integração do seu sistema com a nossa API, oferecendo classes com funções pré-definidas para acessar as rotas da API. Isso elimina a necessidade de desenvolver uma aplicação para se comunicar diretamente com a nossa API, tornando o processo mais eficiente e direto.

## Forma de instalação de nosso SDK:

```
dotnet add package SDK-CloudDfe
```

## Forma de uso:

```cs
using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfe = new Nfe(config);

try
{
    var resp = Task.Run(async () => await nfe.Status()).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
```

### Sobre dados de envio e retornos

Para saber os detalhes referente ao dados de envio e os retornos consulte nossa documentação [IntegraNotas Documentação](https://integranotas.com.br/doc).

### Veja alguns exemplos de consumi de nossa API nos link abaixo:

[Pasta de Exemplos](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples)

[Utilitários](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/utils)

[Averbação](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/averbacao)

[Certificado Digital](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/certificado)

[CT-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/cte)

[CT-e OS](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/cteos)

[DF-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/dfe)

[Emitente](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/emitente)

[GNR-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/gnre)

[MDF-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/mdfe)

[NFC-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/nfce)

[NFCom](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/nfcom)

[NF-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/nfe)

[NFS-e](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/nfse)

[Softhouse](https://github.com/cloud-dfe/sdk-csharp/tree/master/examples/softhouse)