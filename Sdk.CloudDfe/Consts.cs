using System.Collections.Generic;

namespace Sdk.CloudDfe
{
    public static class Consts
    {
        public const string AMBIENTE_PRODUCAO = "1";
        public const string AMBIENTE_HOMOLOGACAO = "2";

        public static readonly Dictionary<string, Dictionary<string, string>> URI = new Dictionary<string, Dictionary<string, string>>()
        {
            { "api", new Dictionary<string, string>()
                {
                    { AMBIENTE_PRODUCAO, "https://api.integranotas.com.br/v1" },
                    { AMBIENTE_HOMOLOGACAO, "https://hom-api.integranotas.com.br/v1" }
                }
            }
        };
    }

}