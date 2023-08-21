using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace GptConsole;

public class Configuration
{
    /// <summary>
    /// TODO: change and save your own key on secure place
    /// </summary>
    private const string Key = "B613679A0814D9EC772F95D778C35UA2";

    public string OrganisationId { get; set; }
    public string ApiKey { get; set; }

    public static void Clear()
    {
        var file = GetConfigurationFilePath();
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    public static Configuration Resolve()
    {
        var file = GetConfigurationFilePath();

        if (!File.Exists(file))
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"Vítejte v konfigurátoru. Před prvním použitím je nutné uvést údaje pro připojení k OpenAI API. Návod najdete na stránce https://www.holec.ai/aisummit/bonus");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine();
            Console.WriteLine("Zadejte Organization ID:");
            string orgId = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Zadejte API Key:");
            string apiKey = Console.ReadLine();
            Console.WriteLine();

            Configuration configuration = new()
            {
                OrganisationId = orgId,
                ApiKey = apiKey
            };

            string json = JsonSerializer.Serialize(configuration);
            File.WriteAllText(file, Encrypt(json), Encoding.UTF8);
            return configuration;
        }

        Console.ResetColor();

        string txt = File.ReadAllText(file);
        return JsonSerializer.Deserialize<Configuration>(Decrypt(txt));
    }

    private static string GetConfigurationFilePath()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var file = Path.Combine(path, "gptconfigk.txt");
        return file;
    }

    private static string Encrypt(string text)
    {
        byte[] key = Encoding.UTF8.GetBytes(Key);
        byte[] input = Encoding.UTF8.GetBytes(text);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        byte[] output = encryptor.TransformFinalBlock(input, 0, input.Length);

        return Convert.ToBase64String(output);
    }

    private static string Decrypt(string text)
    {
        byte[] key = Encoding.UTF8.GetBytes(Key);
        byte[] input = Convert.FromBase64String(text);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();
        byte[] output = decryptor.TransformFinalBlock(input, 0, input.Length);

        return Encoding.UTF8.GetString(output);
    }
}