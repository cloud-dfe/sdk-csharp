using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

public class Webhook
{
    public static bool IsValid(string token, string payload)
    {
        var std = JObject.Parse(payload);

        if (!std.TryGetValue("signature", out JToken signatureToken))
        {
            throw new Exception("Payload incorreto: não contém a assinatura.");
        }

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Token vazio.");
        }

        string ConvertKey(string innerToken)
        {
            return token.Length > 16 ? token.Substring(0, 16) : token.PadRight(16, '0');
        }

        double DecryptTime(byte[] innerCiphertextRaw, byte[] innerTokenBytes, byte[] innerIv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = innerTokenBytes;
                aes.IV = innerIv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(innerCiphertextRaw, 0, innerCiphertextRaw.Length);
                    var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

                    return double.Parse(decryptedString);
                }
            }
        }

        var key = ConvertKey(token);
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var signatureString = signatureToken.ToString();
        var signature = !string.IsNullOrEmpty(signatureString) ? Convert.FromBase64String(signatureString) : throw new Exception("Assinatura nula ou vazia.");

        const int ivlen = 16;

        var iv = new byte[ivlen];
        var hmac = new byte[32];
        var ciphertextRaw = new byte[signature.Length - ivlen - hmac.Length];

        Array.Copy(signature, 0, iv, 0, ivlen);
        Array.Copy(signature, ivlen, hmac, 0, hmac.Length);
        Array.Copy(signature, ivlen + hmac.Length, ciphertextRaw, 0, ciphertextRaw.Length);

        var originalTime = DecryptTime(ciphertextRaw, keyBytes, iv);

        using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(token)))
        {
            var calcmac = hmacsha256.ComputeHash(ciphertextRaw);

            if (FixedTimeEquals(hmac, calcmac))
            {
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var dif = currentTime - originalTime;

                if (dif < 300)
                {
                    return true;
                }
                throw new Exception("Assinatura expirou.");
            }
        }

        throw new Exception("Token ou assinatura incorreta.");
    }

    private static bool FixedTimeEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
        {
            diff |= a[i] ^ b[i];
        }
        return diff == 0;
    }
}
