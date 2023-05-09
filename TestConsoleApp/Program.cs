using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;
using StackExchange.Redis;
using NRedisStack;
using OpenAISharp.API.RedisUtils;
using Newtonsoft.Json.Schema;
using System.Runtime.Intrinsics.X86;

#region archived
//CreateConfig();
//await GetAllModels();
//await CompletionsExample();
//await ChatExampleRaw();
//await ChatExample();
//MarkdownUtils();
//await ModerationsExample();
//await EditsExample1();
//await EditExample2();
//await EmbeddingsExample();
//await CosineSimilaritySearchExample();
//await KNNwithRedisExample();
//await GenerateImageWithPrompt();
//await EditsImageExample();
#endregion


OpenAIConfiguration.Load();
List<byte[]> images = await Images.variationsImage("cute_magical_flying_dog_for_edit_with_magic_hat.png", 2, Images.ImageSize.x512);
int _count = 1;
foreach (byte[] image in images)
{
    System.IO.File.WriteAllBytes($"cute_magical_flying_dog_for_edit_with_magic_hat_variant_{_count}.png", image);
    _count++;
}


#region archived
static void CreateConfig()
{
    Console.WriteLine("Input your Organization ID:");
    string orgid = Console.ReadLine();
    Console.WriteLine("Input your API Key:");
    string apikey = Console.ReadLine();
    OpenAIConfiguration.CreateConfigFile(orgid, apikey);
    Console.WriteLine("appsettings.json file is created.");
    Console.ReadLine();
}
static async Task GetAllModels()
{
    OpenAIConfiguration.Load();
    Models models = await Models.List();
    foreach (var model in models.data)
    {
        Console.WriteLine(model.id);
        Console.WriteLine(model.@object);
        Console.WriteLine("");
    }
    Console.ReadLine();
}
static async Task CompletionsExample()
{
    OpenAIConfiguration.Load();
    CompletionsReponse _result = await Completions.Request("what is the best foods for a red wine?");
    Console.WriteLine(_result.error != null ? _result.error.message : _result.choices[0].text);
}
static async Task ChatExampleRaw()
{
    OpenAIConfiguration.Load();
    ChatResponse chatResponse = await Chat.Request(new Chat()
    {
        messages = new chatformat[] {
        new chatformat() {  role = chatformat.roles.system, content = "You are a pet behaviorist." },
        new chatformat() {  role = chatformat.roles.user, content = "I have an aggressive German Shepherd who needs help managing its aggression." }
    }
    });

    Console.WriteLine(chatResponse.error != null ? chatResponse.error.message : JsonConvert.SerializeObject(chatResponse.choices));
    Console.ReadLine();
}
static async Task ChatExample()
{
    OpenAIConfiguration.Load();
    string chatResponse = await Chat.Request("I want you to act as a travel guide. I will write you my location and you will suggest a place to visit near my location. In some cases, I will also give you the type of places I will visit. You will also suggest me places of similar type that are close to my first location. My first suggestion request is \"I am in Los angeles and I want to visit only museums.\"");

    Console.WriteLine(chatResponse);
    Console.ReadLine();
}
static void MarkdownUtils()
{
    string markdowncontent = System.IO.File.ReadAllText("readme.md");
    Console.WriteLine(markdowncontent.MarkdownToText());
    Console.WriteLine(markdowncontent.MarkdownToHtml());
}
static async Task ModerationsExample()
{
    OpenAIConfiguration.Load();
    Console.WriteLine("Input text:");
    string input = Console.ReadLine();
    bool result = await Moderations.isViolated(input, Moderations.AvailableModel.text_moderation_latest);
    Console.WriteLine(result ? "Violated" : "Pass");
    Console.ReadLine();
}
static async Task EditsExample1()
{
    OpenAIConfiguration.Load();
    EditsResponse response = await Edits.Request(new Edits() { SelectedModel = Edits.AvailableModel.text_davinci_edit_001, input = "Hello! My name is ChatGPT. It is a pleasure to have the opportunity to communicate with you today. Is there anything I can assist you with?", instruction = "change tone of Snoop Dogg", temperature = 0.7M });
    Console.WriteLine(JsonConvert.SerializeObject(response));
    Console.ReadLine();
}
static async Task EditExample2()
{
    OpenAIConfiguration.Load();
    Console.Write(await Edits.Request("What day of the wek is it?", "Fix the spelling mistakes"));
    Console.ReadLine();
}
static async Task EmbeddingsExample()
{
    OpenAIConfiguration.Load();
    var response = await Embeddings.Request(new Embeddings() { SelectedModel = Embeddings.AvailableModel.text_embedding_ada_002, input = new string[] { "The food was delicious and the waiter." } });
    Console.WriteLine(JsonConvert.SerializeObject(response));
    Console.ReadLine();
}
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

    List<float[]> queryEmbeddingVectors = await Embeddings.Request(myTexts.Select(t => t.Text).ToArray());
    for (int i = 0; i < queryEmbeddingVectors.Count; i++)
    {
        myTexts[i].EmbeddingVector = queryEmbeddingVectors[i];
    }
    System.IO.File.WriteAllText("myTexts.json", JsonConvert.SerializeObject(myTexts));
}
static async Task CosineSimilaritySearchExample()
{
    OpenAIConfiguration.Load();

    //await PrepareMyEmbeddingVectorDatabase();
    List<MyEmbeddingVectorData> preparedTexts = JsonConvert.DeserializeObject<List<MyEmbeddingVectorData>>(System.IO.File.ReadAllText("myTexts.json"));

    string query = "The quick brown fox";
    float[] queryEmbeddingVector = await Embeddings.Request(query);

    foreach (MyEmbeddingVectorData t in preparedTexts)
    {
        t.CosineSimilarity = CosineSimilarity.Calculate(t.EmbeddingVector, queryEmbeddingVector);
    }
    Console.WriteLine("Most similar text is:");
    Console.WriteLine(preparedTexts.OrderByDescending(t => t.CosineSimilarity).First().Text);
    Console.ReadLine();
}
static void SetAndGetEmbeddingsToRedisAI()
{
    OpenAIConfiguration.Load();
    float[] embedding = Embeddings.Request("The quick brown fox jumps over the lazy dog.", Embeddings.AvailableModel.text_embedding_ada_002).Result;
    string tensorName = "embedding:" + Guid.NewGuid().ToString();

    // RedisAI: https://oss.redis.com/redisai/
    // 1. To use RedisAI, you install Docker.
    // 2. After installing Docker, run the command "docker run -d --name redisai -p 6379:6379 redislabs/redisai:edge-cpu-bionic" to start the RedisAI container.
    // 3. Finally, you need to install the necessary NuGet packages: StackExchange.Redis and NRedisStack, to use RedisAI in your .NET Core C# project.

    string server = "localhost";
    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(server);
    IDatabase db = redis.GetDatabase();
    RedisAIUtils.TensorSet(db, tensorName, embedding.Select(v => (object)v).ToList(), RedisAIUtils.TensorInputDataType.DOUBLE, RedisAIUtils.TensorInputType.VALUES);
    Console.WriteLine(tensorName);
    Console.WriteLine(JsonConvert.SerializeObject(RedisAIUtils.TensorGet<double>(db, tensorName, RedisAIUtils.TensorOutputType.VALUES)));
    redis.Close();
}

static void RedisCreateNDropIdex()
{
    // Redis: https://redis.io/docs/about/
    // 1. To use RedisAI, you install Docker.
    // 2. After installing Docker, run the command "$ docker run -d --name redis-stack-server -p 6379:6379 redis/redis-stack-server:latest" to start the Redis stack server container.
    // 3. Finally, you need to install the necessary NuGet packages: StackExchange.Redis and NRedisStack, to use Redis in your .NET Core C# project.
    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    IDatabase db = redis.GetDatabase();
    FT.CreateFLATVector(db, "myVectorIdx", new List<FlatVectorDocumentField>() {
        new FlatVectorDocumentField() {
            FieldID = "vec", VectorDataType = FT.VectorDataType.FLOAT32, Dimension = 128, Vector_Distance_Metric = FT.Vector_Distance_Metric.L2
        }
    });
    FT.DropIndex(db, "myVectorIdx", true);
    redis.Close();
    Console.ReadLine();
}

static void HashSetNGetNSearch()
{
    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    IDatabase db = redis.GetDatabase();
    FT.Create(db, "myIdx", new List<DocumentField>() {
    new DocumentField() { FieldID = "title", FieldType = FT.FieldType.TEXT, FieldOptions = new List<string>() { "WEIGHT", "5.0" } },
    new DocumentField() { FieldID = "body", FieldType = FT.FieldType.TEXT },
    new DocumentField() { FieldID = "url", FieldType = FT.FieldType.TEXT }
}, FT.IndexDataType.HASH, new List<object>() { "PREFIX", "1", "doc:" });

    HashStored.Set(db, "doc:1", new List<KeyValuePair<string, object>>() {
    new KeyValuePair<string, object>("title", "hello world"),
    new KeyValuePair<string, object>("body", "lorem ipsum"),
    new KeyValuePair<string, object>("url", "http://redis.io")
});
    object result = HashStored.Get(db, "doc:1", "title").First();
    Console.WriteLine(result);
    List<object> response = FT.Search(db, "myIdx", "hello");
    Console.WriteLine(JsonConvert.SerializeObject(response));
    redis.Close();
    Console.ReadLine();
}

static async Task KNNwithRedisExample()
{
    List<Product> Products = new List<Product>()
{
    new Product() { Name = "Bose QuietComfort Earbuds", Description="True wireless earbuds with noise cancelling technology and up to 6 hours of battery life, ideal for music and calls on the go." },
    new Product() { Name = "LG CX Series 65\" OLED TV", Description="4K Ultra HD Smart OLED TV with AI ThinQ, perfect for watching movies, TV shows, and gaming." },
    new Product() { Name = "Dyson V11 Torque Drive Cordless Vacuum Cleaner", Description="Lightweight and powerful cordless vacuum cleaner with up to 60 minutes of run time and LCD screen displaying real-time battery life and performance data." },
    new Product() { Name = "Apple MacBook Pro 13-inch", Description="Powerful and sleek laptop with Retina display, up to 10 hours of battery life, and the latest Apple M1 chip for exceptional performance." },
    new Product() { Name = "Sony WH-1000XM4 Wireless Noise Cancelling Headphones", Description="Premium noise cancelling headphones with dual noise sensor technology, touch sensor controls, and up to 30 hours of battery life." },
};

    OpenAIConfiguration.Load();
    List<float[]> queryEmbeddingVectors = await Embeddings.Request(Products.Select(t => t.Description).ToArray());
    for (int i = 0; i < queryEmbeddingVectors.Count; i++)
    {
        Products[i].DescriptionEmbedding = queryEmbeddingVectors[i];
    }

    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    IDatabase db = redis.GetDatabase();
    FT.Create(db, "ProductIndex", new List<DocumentField>() {
    new DocumentField() { FieldID="$.Name", FieldIDAlias="Name", FieldType= FT.FieldType.TEXT },
    new DocumentField() { FieldID="$.Description", FieldIDAlias="Description", FieldType= FT.FieldType.TEXT },
    new FlatVectorDocumentField() { FieldID="$.DescriptionEmbedding", FieldIDAlias="DescriptionEmbedding", VectorDataType = FT.VectorDataType.FLOAT32, Dimension = 1536, Vector_Distance_Metric = FT.Vector_Distance_Metric.L2 }.ToDocumentField(),
}, FT.IndexDataType.JSON, new List<object>() { "PREFIX", "1", "product:" });

    for (int i = 0; i < Products.Count; i++)
    {
        Json.Set(db, $"product:{i}", JsonConvert.SerializeObject(Products[i]));
    }
    // Redis query syntax: https://redis.io/docs/stack/search/reference/query_syntax/
    var response = FT.Search(db, "ProductIndex", "@Name:(EarBuds)");
    Console.WriteLine(JsonConvert.DeserializeObject<Product>(((List<object>)response[2])[1].ToString()).Name);
    // Redis vector similarity: https://redis.io/docs/stack/search/reference/vectors/
    string exampleSearchTerm = "headphone";
    float[] searchEmbedding = await Embeddings.Request(exampleSearchTerm);
    var response2 = FT.SearchKNN(db, "ProductIndex", "*=>[KNN 3 @DescriptionEmbedding $DescriptionEmbedding]", new List<KeyValuePair<string, float[]>>() { new KeyValuePair<string, float[]>("DescriptionEmbedding", searchEmbedding) });
    Console.WriteLine(JsonConvert.DeserializeObject<Product>(((List<object>)response2[2])[3].ToString()).Name);
    Console.WriteLine(JsonConvert.DeserializeObject<Product>(((List<object>)response2[4])[3].ToString()).Name);
    Console.WriteLine(JsonConvert.DeserializeObject<Product>(((List<object>)response2[6])[3].ToString()).Name);

    redis.Close();
    Console.ReadLine();
}

static async Task GenerateImageWithPrompt()
{
    OpenAIConfiguration.Load();
    string Sample_Image_Generation_Prompt = "a cute magical flying dog, fantasy art drawn by disney concept artists, golden colour, high quality, highly detailed, elegant, sharp focus, concept art, character concepts, digital painting, mystery, adventure";
    List<byte[]> images = await Images.Generate(Sample_Image_Generation_Prompt, 1, Images.ImageSize.x512);
    if (images.Count > 0)
    {
        System.IO.File.WriteAllBytes("cute_magical_flying_dog.png", images[0]);
    }
}

static async Task EditsImageExample()
{
    string edit_prompt = "Cute puppy wearing a magic hat";

    OpenAIConfiguration.Load();
    // https://www.online-image-editor.com/
    List<byte[]> images = await Images.editsImage("cute_magical_flying_dog_for_edit.png", edit_prompt, null, 1, Images.ImageSize.x512);
    if (images.Count > 0)
    {
        System.IO.File.WriteAllBytes("cute_magical_flying_dog_for_edit_with_magic_hat.png", images[0]);
    }
}

class MyEmbeddingVectorData
{
    public string Text { get; set; }
    public float[] EmbeddingVector { get; set; }
    public double CosineSimilarity { get; set; }
}

class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float[] DescriptionEmbedding { get; set; }
}
#endregion


