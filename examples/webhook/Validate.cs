using System;

public class Program
{
    public static void Main(string[] args)
    {
        Validate();
    }

    public static void Validate()
    {
        // * Este exemplo de verificação da assinatura dos dados enviados via webhook
        // *
        // * Este método indica se os dados passados pelo webhook são válidos
        // *
        // * NOTA: Será retornado uma Exception nos seguintes casos:
        // * 1 - o payload não é um JSON válido
        // * 2 - o payload não contém a assinatura (campo signature)
        // * 3 - o token está vazio (não foi passado um token)
        // * 4 - Token ou assinatura incorreta (não foi possível decriptar a assinatura)
        // * 5 - a assinatura expirou (quando a assinatura tiver sido feita a mais de 5 minutos atrás)

        try
        {
            // Token da softhouse do ambiente sendo usado (lembre-se existem dois tokens, um para homologação e outro para produção, e são diferentes)
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ";

            // Payload do webhook em JSON (https://doc.cloud-dfe.com.br/webhook)
            string payload = "{\"origem\": \"TESTE\", \"cnpj_cpf\": \"12345678000123\", \"signature\": \"tBQrTEui9FxaU7AdFbqPaveg3tBPZ1RjKj3Ytn15fm10/AYIztE6ST+YvLuLu6ea8PUrefX4SpxcT1K8LK40fQ==\"}";

            bool isValid = Webhook.IsValid(token, payload);

            if (isValid)
            {
                Console.WriteLine("O payload é válido.");
            }
            else
            {
                Console.WriteLine("O payload é inválido.");
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
        }
    }
}
