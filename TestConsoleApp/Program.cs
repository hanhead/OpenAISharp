using Newtonsoft.Json;
using OpenAISharp;
using OpenAISharp.API;
//CreateConfig();
//await GetAllModels();
//await CompletionsExample();
//await ChatExampleRaw();
//await ChatExample()
//MarkdownUtils();
//await ModerationsExample();

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
    bool result = await Moderations.isViolated(input);
    Console.WriteLine(result ? "Violated" : "Pass");
    Console.ReadLine();
}
