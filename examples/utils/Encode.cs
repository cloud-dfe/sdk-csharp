using Sdk.CloudDfe;

string originalString = "Hello, world!";
string encodedString = Util.Encode(originalString);
Console.WriteLine("Encoded string: " + encodedString);
