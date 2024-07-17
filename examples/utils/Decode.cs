using Sdk.CloudDfe;

string encodedString = "SGVsbG8sIHdvcmxkIQ==";
string decodedString = Util.Decode(encodedString);
Console.WriteLine("Decoded string: " + decodedString);
