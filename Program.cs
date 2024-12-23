using MeBot.Response;

namespace MeBot;

using Newtonsoft.Json;
using System.Text;

internal class Program
{
    private const string ApiKey = "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";

    private static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.BackgroundColor = ConsoleColor.Black;

        var context = await File.ReadAllTextAsync("context.json");
        if (context == null)
        {
            Console.WriteLine("Error: Context file could not be read.");
            return;
        }

        Console.WriteLine("Welcome to the 'Me' Bot! Ask me anything about my experiences, and I'll answer as if I were you.");
        Console.WriteLine("Type '/bye' to end the conversation.\n");

        while (true)
        {
            Console.WriteLine("Please ask me a question!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> ");

            var question = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(question))
            {
                continue;
            }

            if (question.ToLower() == "/bye")
            {
                break;
            }

            var answer = await GetAnswerFromModelAsync(question, context);
            BotReply($"Daniel's answer: {answer}\n");
        }
    }

    private static async Task<string> GetAnswerFromModelAsync(string question, string context)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

        var payload = new
        {
            inputs = new { question, context }
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://api-inference.huggingface.co/models/deepset/roberta-base-squad2", content);
        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<HuggingFaceResponse>(responseString);
        return result?.Choices?.FirstOrDefault()?.Text ?? "Sorry, I couldn't answer that.";

    }

    private static void BotReply(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}