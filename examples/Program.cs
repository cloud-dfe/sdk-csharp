using Sdk.CloudDfe;

string filePath = "caminho_do_arquivo.xml";
string fileContent = Util.ReadFile(filePath);
Console.WriteLine("File content: " + fileContent);
