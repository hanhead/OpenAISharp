using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;

//CreateConfig();
OpenAIConfiguration.Load();

CompletionsReponse _result = await Completions.Request("what is the best foods for a red wine?");
Console.WriteLine(_result.error != null ? _result.error.message : _result.choices[0].text);

Console.ReadLine();

static void CreateConfig()
{
    string InitializationVectorBase64String = EncryptionUtils.GetNewInitailizationVectorBase64String();
    TripleDesEncryption tripleDesc = new TripleDesEncryption(EncryptionUtils.GetMacAddress16BytesFormat(), InitializationVectorBase64String);

    var openAIConfiguration = new
    {
        OpenAI = new OpenAIConfiguration()
        {
            UrlPrefix = "https://api.openai.com/v1",
            InitializationVectorBase64String = InitializationVectorBase64String,
            EncryptedOrgID = tripleDesc.Encrypt("YOUR_ORGANIZATION_ID"),
            EncryptedApiKey = tripleDesc.Encrypt("YOUR_API_KEY")
        }
    };
    string _json = JsonConvert.SerializeObject(openAIConfiguration, Formatting.Indented);
    Console.WriteLine(_json);
}