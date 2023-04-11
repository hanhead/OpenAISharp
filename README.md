


## Introduction
This C# library provides easy access to Open AI's powerful API for natural language processing and text generation. With just a few lines of code, you can use state-of-the-art deep learning models like GPT-3 and GPT-4 to generate human-like text, complete tasks, and more.

## Features
* Simple and intuitive API for text generation and natural language processing
* Supports various Open AI models, including GPT-3 and GPT-3.5
* Flexible configuration options to customize model behavior
* Seamless integration with C# projects

## Configuration

This code will help you get started with configuring your OpenAI API in a C# console application. First, it generates a new initialization vector in Base64 format using the EncryptionUtils.GetNewInitailizationVectorBase64String() method.

Then, it creates a new instance of the TripleDesEncryption class, passing in the 16-byte MAC address of the local machine and the generated initialization vector.

Next, it creates an anonymous object to hold the configuration settings for the OpenAI API. It sets the UrlPrefix to the base URL for the API, and sets the InitializationVectorBase64String and encrypted values of your Organization ID and API Key using the tripleDesc.Encrypt() method.

Finally, it serializes the configuration object to a JSON string and prints it to the console.

Use this as a starting point for setting up your own OpenAI API configuration in a C# application.

``` csharp
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
```


## Getting Started
To use this library, you'll need to sign up for an Open AI API key. Then, simply initialize the OpenAISharp class with your API key and start generating text!
### Completion example code
``` csharp
using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;

OpenAIConfiguration.Load();

CompletionsReponse _result = await Completions.Request("what is the best foods for a red wine?");
Console.WriteLine(_result.error != null ? _result.error.message : _result.choices[0].text);
```
### Chat example code
``` csharp
using OpenAISharp.API;
using OpenAISharp;
using Newtonsoft.Json;

OpenAIConfiguration.Load();
ChatResponse chatResponse = await Chat.Request(new Chat()
{
    messages = new chatformat[] {
        new chatformat() {  role = chatformat.roles.system, content = "You are a pet behaviorist." },
        new chatformat() {  role = chatformat.roles.user, content = "I have an aggressive German Shepherd who needs help managing its aggression." }
    }
});

Console.WriteLine(chatResponse.error != null ? chatResponse.error.message : JsonConvert.SerializeObject(chatResponse.choices));
```
``` csharp
using OpenAISharp.API;
using OpenAISharp;

OpenAIConfiguration.Load();
string chatResponse = await Chat.Request("I want you to act as a travel guide. I will write you my location and you will suggest a place to visit near my location. In some cases, I will also give you the type of places I will visit. You will also suggest me places of similar type that are close to my first location. My first suggestion request is \"I am in Los angeles and I want to visit only museums.\"");

Console.WriteLine(chatResponse);
Console.ReadLine();
```
![Response of Chat.Request](screenshots/response.png)
## Contributing
Contributions are welcome! Whether you've found a bug, want to add a new feature, or just have a suggestion, feel free to open an issue or pull request.