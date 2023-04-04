using Newtonsoft.Json;
using OpenAISharp;

string InitializationVectorBase64String = EncryptionUtils.GetNewInitailizationVectorBase64String();
TripleDesEncryption tripleDesc = new TripleDesEncryption(EncryptionUtils.GetMacAddress16BytesFormat(), InitializationVectorBase64String);

var openAIConfiguration =new
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
Console.ReadLine(); 
