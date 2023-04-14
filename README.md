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

Finally, it serializes the configuration object to a JSON string and create an appsettings.json

Use this as a starting point for setting up your own OpenAI API configuration in a C# application.

``` csharp
using OpenAISharp;

Console.WriteLine("Input your Organization ID:");
string orgid = Console.ReadLine();
Console.WriteLine("Input your API Key:");
string apikey = Console.ReadLine();
OpenAIConfiguration.CreateConfigFile(orgid, apikey);
Console.WriteLine("appsettings.json file is created.");
Console.ReadLine();
```

![new configuration console screen](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/configconsole.png)
![New appsettings.json file](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/newconfig.png)

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

OpenAIConfiguration.Load();
string chatResponse = await Chat.Request("I want you to act as a travel guide. I will write you my location and you will suggest a place to visit near my location. In some cases, I will also give you the type of places I will visit. You will also suggest me places of similar type that are close to my first location. My first suggestion request is \"I am in Los angeles and I want to visit only museums.\"");

Console.WriteLine(chatResponse);
Console.ReadLine();
```

![Response of Chat.Request](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/response.png)

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

### Edits example codes

``` csharp
using OpenAISharp.API;
using OpenAISharp;

OpenAIConfiguration.Load();
EditsResponse response = await Edits.Request(new Edits() { SelectedModel = Edits.AvailableModel.text_davinci_edit_001, input = "Hello! My name is ChatGPT. It is a pleasure to have the opportunity to communicate with you today. Is there anything I can assist you with?", instruction = "change tone of Snoop Dogg", temperature = 0.7M });
Console.WriteLine(JsonConvert.SerializeObject(response));
Console.ReadLine();
```

![Response of Edits.Request 2](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/edits_response_2.png)

``` csharp
using OpenAISharp.API;
using OpenAISharp;
using Newtonsoft.Json;

OpenAIConfiguration.Load();
EditsResponse response = await Edits.Request(new Edits() { SelectedModel = Edits.AvailableModel.text_davinci_edit_001, input = "Hello! My name is ChatGPT. It is a pleasure to have the opportunity to communicate with you today. Is there anything I can assist you with?", instruction = "change tone of Snoop Dogg", temperature = 0.7M });
Console.WriteLine(JsonConvert.SerializeObject(response));
Console.ReadLine();
```

![Response of Edits.Request 1](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/edits_response_1.png)


### Moderations

The moderation feature of OpenAI API is free, but a quota must be available to use it. This is an excellent tool for checking user-generated content and comments for violations.

![Free moderation API](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/free_moderaion_api.png)

``` csharp
using OpenAISharp.API;
using OpenAISharp;

OpenAIConfiguration.Load();
Console.WriteLine("Input text:");
string input = Console.ReadLine();
bool result = await Moderations.isViolated(input);
Console.WriteLine(result ? "Violated" : "Pass");
Console.ReadLine();
```

### OpenAI's GPT Embedding Vector

OpenAI's GPT embedding vector is a numerical representation of words and phrases in a 768-dimensional space. It is trained on a large and diverse corpus of text data, making it exceptional in its ability to encode the meaning of language. The GPT embedding vector is used in a wide range of natural language processing tasks, as well as recommendation systems and anomaly detection.

#### Usage Examples

Here are some examples of how to use OpenAI's GPT embedding vector in your projects:

##### Create GPT Embedding Vector

``` csharp
using OpenAISharp.API;
using OpenAISharp;
using Newtonsoft.Json;

OpenAIConfiguration.Load();
var response = await Embeddings.Request(new Embeddings() { SelectedModel = Embeddings.AvailableModel.text_embedding_ada_002, input = new string[] { "The food was delicious and the waiter." } });
Console.WriteLine(JsonConvert.SerializeObject(response));
Console.ReadLine();
```

![Response of Creating embedding vector](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/embeddings_response1.png)

##### Search with Cosine Similarity

``` csharp
using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;

OpenAIConfiguration.Load();

//await PrepareMyEmbeddingVectorDatabase();
List<MyEmbeddingVectorData> preparedTexts = JsonConvert.DeserializeObject<List<MyEmbeddingVectorData>>(System.IO.File.ReadAllText("myTexts.json"));

string query = "The quick brown fox";
double[] queryEmbeddingVector = await Embeddings.Request(query);

foreach (MyEmbeddingVectorData t in preparedTexts)
{
    t.CosineSimilarity = CosineSimilarity.Calculate(t.EmbeddingVector, queryEmbeddingVector);
    t.EuclideanDistance = EuclideanDistance.Calculate(t.EmbeddingVector, queryEmbeddingVector);
}
Console.WriteLine("Most similar text is:");
Console.WriteLine(preparedTexts.OrderByDescending(t=>t.CosineSimilarity).First().Text);
Console.ReadLine();

static async Task PrepareMyEmbeddingVectorDatabase()
{
    List<MyEmbeddingVectorData> myTexts = new List<MyEmbeddingVectorData>
    {
        new MyEmbeddingVectorData() { Text = "The quick brown fox jumps over the lazy dog." },
        new MyEmbeddingVectorData() { Text = "The quick brown fox jumps over the lazy cat." },
        new MyEmbeddingVectorData() { Text = "The lazy dog jumps over the quick brown fox." },
        new MyEmbeddingVectorData() { Text = "The quick brown fox runs fast." },
        new MyEmbeddingVectorData() { Text = "The lazy cat sleeps all day." },
        new MyEmbeddingVectorData() { Text = "The quick brown dog barks at the lazy cat." }
    };

    List<double[]> queryEmbeddingVectors = await Embeddings.Request(myTexts.Select(t => t.Text).ToArray());
    for (int i = 0; i < queryEmbeddingVectors.Count; i++)
    {
        myTexts[i].EmbeddingVector = queryEmbeddingVectors[i];
    }
    System.IO.File.WriteAllText("myTexts.json", JsonConvert.SerializeObject(myTexts));
}
class MyEmbeddingVectorData
{
    public string Text { get; set; }
    public double[] EmbeddingVector { get; set; }
    public double CosineSimilarity { get; set; }
    public double EuclideanDistance { get; set; }
}
```

![search with open ai embedding and cosine similarity](https://raw.githubusercontent.com/hanhead/OpenAISharp/master/screenshots/search_with_open_ai_embedding_and_cosine_similarity.png)

##### Save to Vector Database (RedisAI)

``` csharp
// Not implemented
```

##### Recommendation with Vector Database

``` csharp
// Not implemented
```

### Markdown utils example

``` csharp
string markdowncontent = System.IO.File.ReadAllText("readme.md");
Console.WriteLine(markdowncontent.MarkdownToText());
Console.WriteLine(markdowncontent.MarkdownToHtml());
```

## Some ideas for future features:

* Easy-to-understand examples for using embeddings
* Case study examples for fine-tuning models
* Prompt templates for generating customized text
* Dataset pipeline features for processing and cleaning data



## Contributing

Contributions are welcome! Whether you've found a bug, want to add a new feature, or just have a suggestion, feel free to open an issue or pull request.
