using System.Security.Cryptography;
using System.Text;

public class Webhook
{
    public static bool IsValid(string token, string payload)
    {
        var std = System.Text.Json.JsonDocument.Parse(payload).RootElement;

        if (!std.TryGetProperty("signature", out var signatureElement))
        {
            throw new Exception("Payload incorreto não contém a assinatura.");
        }

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Token vazio.");
        }

        string convertKey(string token)
        {
            return token.Length > 16 ? token.Substring(0, 16) : token.PadRight(16, '0');
        }

        double decryptTime(byte[] ciphertextRaw, byte[] tokenBytes, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = tokenBytes;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var decryptedBytes = decryptor.TransformFinalBlock(ciphertextRaw, 0, ciphertextRaw.Length);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            return double.Parse(decryptedString);
        }

        var key = convertKey(token);
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var signatureString = signatureElement.GetString();
        var signature = !string.IsNullOrEmpty(signatureString) ? Convert.FromBase64String(signatureString) : throw new Exception("Signature is null or empty.");

        const int ivlen = 16;

        var iv = new byte[ivlen];
        var hmac = new byte[32];
        var ciphertextRaw = new byte[signature.Length - ivlen - hmac.Length];

        Array.Copy(signature, 0, iv, 0, ivlen);
        Array.Copy(signature, ivlen, hmac, 0, hmac.Length);
        Array.Copy(signature, ivlen + hmac.Length, ciphertextRaw, 0, ciphertextRaw.Length);

        var originalTime = decryptTime(ciphertextRaw, keyBytes, iv);

        using var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(token));
        var calcmac = hmacsha256.ComputeHash(ciphertextRaw);

        if (hmac.Length == calcmac.Length && CryptographicOperations.FixedTimeEquals(hmac, calcmac))
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var dif = currentTime - originalTime;

            if (dif < 300)
            {
                return true;
            }
            throw new Exception("Assinatura expirou.");
        }

        throw new Exception("Token ou assinatura incorreta.");
    }
}
