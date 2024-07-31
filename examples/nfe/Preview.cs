using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfe = new Nfe(config);

var lista_itens = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        {"numero_item", "1"},
        {"codigo_produto", "000297"},
        {"descricao", "SAL GROSSO 50KGS"},
        {"codigo_ncm", "84159020"},
        {"cfop", "5102"},
        {"unidade_comercial", "SC"},
        {"quantidade_comercial", 10},
        {"valor_unitario_comercial", "22.45"},
        {"valor_bruto", "224.50"},
        {"unidade_tributavel", "SC"},
        {"quantidade_tributavel", "10.00"},
        {"valor_unitario_tributavel", "22.45"},
        {"origem", "0"},
        {"inclui_no_total", "1"},
        {"imposto", new Dictionary<string, object>
            {
                {"valor_aproximado_tributos", 9.43},
                {"icms", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "102"},
                        {"aliquota_credito_simples", "0"},
                        {"valor_credito_simples", "0"},
                        {"modalidade_base_calculo", "3"},
                        {"valor_base_calculo", "0.00"},
                        {"modalidade_base_calculo_st", "4"},
                        {"aliquota_reducao_base_calculo", "0.00"},
                        {"aliquota", "0.00"},
                        {"aliquota_final", "0.00"},
                        {"valor", "0.00"},
                        {"aliquota_margem_valor_adicionado_st", "0.00"},
                        {"aliquota_reducao_base_calculo_st", "0.00"},
                        {"valor_base_calculo_st", "0.00"},
                        {"aliquota_st", "0.00"},
                        {"valor_st", "0.00"}
                    }
                },
                {"fcp", new Dictionary<string, object>
                    {
                        {"aliquota", "1.65"}
                    }
                },
                {"pis", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "01"},
                        {"valor_base_calculo", 224.5},
                        {"aliquota", "1.65"},
                        {"valor", "3.70"}
                    }
                },
                {"cofins", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "01"},
                        {"valor_base_calculo", 224.5},
                        {"aliquota", "7.60"},
                        {"valor", "17.06"}
                    }
                }
            }
        },
        {"valor_desconto", 0},
        {"valor_frete", 0},
        {"valor_seguro", 0},
        {"valor_outras_despesas", 0},
        {"informacoes_adicionais", "Valor aproximado tributos R$: 9,43 (4,20%) Fonte: IBPT"}
    }
};

var payload = new Dictionary<string, object>
{
    {"natureza_operacao", "VENDA DENTRO DO ESTADO"},
    {"serie", "1"},
    {"numero", "101007"},
    {"data_emissao", "2021-06-26T13:00:00-03:00"},
    {"data_entrada_saida", "2021-06-26T13:00:00-03:00"},
    {"tipo_operacao", "1"},
    {"finalidade_emissao", "1"},
    {"consumidor_final", "1"},
    {"presenca_comprador", "9"},
    {"intermediario", new Dictionary<string, object>
        {
            {"indicador", "0"}
        }
    },
    {"notas_referenciadas", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"nfe", new Dictionary<string, object>
                    {
                        {"chave", "50000000000000000000000000000000000000000000"}
                    }
                }
            }
        }
    },
    {"destinatario", new Dictionary<string, object>
        {
            {"cpf", "01234567890"},
            {"nome", "EMPRESA MODELO"},
            {"indicador_inscricao_estadual", "9"},
            {"inscricao_estadual", null},
            {"endereco", new Dictionary<string, object>
                {
                    {"logradouro", "AVENIDA TESTE"},
                    {"numero", "444"},
                    {"bairro", "CENTRO"},
                    {"codigo_municipio", "4108403"},
                    {"nome_municipio", "Mossoro"},
                    {"uf", "PR"},
                    {"cep", "59653120"},
                    {"codigo_pais", "1058"},
                    {"nome_pais", "BRASIL"},
                    {"telefone", "8499995555"}
                }
            }
        }
    },
    {"itens", lista_itens},
    {"frete", new Dictionary<string, object>
        {
            {"modalidade_frete", "0"},
            {"volumes", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        {"quantidade", "10"},
                        {"especie", null},
                        {"marca", "TESTE"},
                        {"numero", null},
                        {"peso_liquido", 500},
                        {"peso_bruto", 500}
                    }
                }
            }
        }
    },
    {"cobranca", new Dictionary<string, object>
        {
            {"fatura", new Dictionary<string, object>
                {
                    {"numero", "1035.00"},
                    {"valor_original", "224.50"},
                    {"valor_desconto", "0.00"},
                    {"valor_liquido", "224.50"}
                }
            }
        }
    },
    {"pagamento", new Dictionary<string, object>
        {
            {"formas_pagamento", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        {"meio_pagamento", "01"},
                        {"valor", "224.50"}
                    }
                }
            }
        }
    },
    {"informacoes_adicionais", "PV: 3325 * Rep: DIRETO * Motorista:  * Forma Pagto: 04 DIAS * teste de observação para a nota fiscal * Valor aproximado tributos R$9,43 (4,20%) Fonte: IBPT"},
    {"pessoas_autorizadas", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"cnpj", "96256273000170"}
            },
            new Dictionary<string, object>
            {
                {"cnpj", "80681257000195"}
            }
        }
    }
};

try
{

    var resp = Task.Run(async () => await nfe.Preview(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
