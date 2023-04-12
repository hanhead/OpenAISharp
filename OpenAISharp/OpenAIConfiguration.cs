using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAISharp.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T e) where T : Enum
        {
            var attribute = e.GetType().GetMember(e.ToString())
                .FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? e.ToString();
        }
    }
    public class OpenAIConfiguration
    {
        TripleDesEncryption tripleDesc;
        private string _initializationVectorBase64String;
        public OpenAIConfiguration() { }
        public static void Load(string configFile = "appsettings.json")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(configFile, optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            OpenAIConfiguration openAIConfig = configuration.GetSection("OpenAI").Get<OpenAIConfiguration>();
            OpenAISettings.UrlPrefix = openAIConfig.UrlPrefix;
            OpenAISettings.OrganizationID = openAIConfig.OrganizationID;
            OpenAISettings.ApiKey = openAIConfig.ApiKey;
        }
        public static string CreateConfig(string organizationID, string apiKey)
        {
            string InitializationVectorBase64String = EncryptionUtils.GetNewInitailizationVectorBase64String();
            TripleDesEncryption tripleDesc = new TripleDesEncryption(EncryptionUtils.GetMacAddress16BytesFormat(), InitializationVectorBase64String);

            var openAIConfiguration = new
            {
                OpenAI = new OpenAIConfiguration()
                {
                    UrlPrefix = "https://api.openai.com/v1",
                    InitializationVectorBase64String = InitializationVectorBase64String,
                    EncryptedOrgID = tripleDesc.Encrypt(organizationID),
                    EncryptedApiKey = tripleDesc.Encrypt(apiKey)
                }
            };
            string _json = JsonConvert.SerializeObject(openAIConfiguration, Formatting.Indented);
            return _json;
        }
        public static void CreateConfigFile(string organizationID, string apiKey, string appSettingsJsonFilePath  = "appsettings.json")
        {
            System.IO.File.WriteAllText(appSettingsJsonFilePath, CreateConfig(organizationID, apiKey));
        }
        public string InitializationVectorBase64String {
            get
            {
                return _initializationVectorBase64String;
            }
            set 
            {
                if (tripleDesc == null)
                {
                    tripleDesc = new TripleDesEncryption(EncryptionUtils.GetMacAddress16BytesFormat(), value);
                }
                _initializationVectorBase64String = value;
            } 
        }
        public string UrlPrefix { get; set; }
        public string EncryptedOrgID { get; set; }
        public string EncryptedApiKey { get; set; }

        string decryptedOrgID;
        string decryptedApiKey;
        [JsonIgnore]
        public string OrganizationID
        {
            get
            {
                if (decryptedOrgID==null && tripleDesc != null)
                {
                    if (decryptedOrgID == null)
                        decryptedOrgID = tripleDesc.Decrypt(this.EncryptedOrgID);
                    return decryptedOrgID;
                }
                else
                {
                    return decryptedOrgID;
                }
            }
        }
        [JsonIgnore]
        public string ApiKey
        {
            get
            {
                if (decryptedApiKey == null && tripleDesc != null)
                {
                    if (decryptedApiKey == null)
                        decryptedApiKey = tripleDesc.Decrypt(this.EncryptedApiKey);
                    return decryptedApiKey;
                }
                else
                {
                    return decryptedApiKey;
                }
            }
        }
    }
}
