using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;
using StackExchange.Redis;
using NRedisStack;

#region archived
//CreateConfig();
//await GetAllModels();
//await CompletionsExample();
//await ChatExampleRaw();
//await ChatExample()
//MarkdownUtils();
//await ModerationsExample();
//await EditsExample1();
//await EditExample2();
//await EmbeddingsExample();
//await CosineSimilaritySearchExample();
#endregion

SetAndGetEmbeddingsToRedisAI();
Console.ReadLine();

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
    Models models = await Models.List(true);
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

    List<double[]> queryEmbeddingVectors = await Embeddings.Request(myTexts.Select(t => t.Text).ToArray());
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
    double[] queryEmbeddingVector = await Embeddings.Request(query);

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
    List<MyEmbeddingVectorData> preparedTexts = JsonConvert.DeserializeObject<List<MyEmbeddingVectorData>>(System.IO.File.ReadAllText("myTexts.json"));
    double[] embedding = Embeddings.Request("The quick brown fox jumps over the lazy dog.", Embeddings.AvailableModel.text_embedding_ada_002).Result;
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
class MyEmbeddingVectorData
{
    public string Text { get; set; }
    public double[] EmbeddingVector { get; set; }
    public double CosineSimilarity { get; set; }
}
#endregion


