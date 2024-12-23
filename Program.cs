using MeBot.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace MeBot
{
    internal class Program
    {
        private const string PdfFilePath = "Resume Daniel Soares.pdf";

        private static async Task Main(string[] args)
        {
            string apiKey = args.FirstOrDefault(arg => arg.StartsWith("Phi3ApiKey="))?.Split('=')[1];
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Environment variables 'Phi3ApiKey' is not set.");
                return;
            }

            var resumeText = PdfTextExtractor.ExtractTextFromPdf(PdfFilePath);
            if (string.IsNullOrEmpty(resumeText))
            {
                Console.WriteLine("Error extracting text from PDF.");
                return;
            }

            Console.WriteLine("Welcome to the 'Me' Bot! Ask me anything about my experiences, and I'll answer as if I were you.");
            Console.WriteLine("Type '/bye' to end the conversation.\n");

            while (true)
            {
                Console.Write("Please ask me a question!");

                var question = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(question))
                {
                    continue;
                }

                if (question.ToLower() == "/bye")
                {
                    break;
                }

                var answer = await GetAnswerFromModelAsync(apiKey, question, resumeText);
                Console.WriteLine($"Daniel's answer: {answer}\n");
            }
        }

        public static async Task<string> GetAnswerFromModelAsync(string apiKey, string question, string context)
        {
            using var client = new HttpClient();

            var requestData = new
            {
                model = "phi-3",  // Hypothetical model name for Phi-3
                prompt = $"{context}\nQuestion: {question}",
                temperature = 0.7,
                max_tokens = 150
            };

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.anthropic.com/v1/completions", content);

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);

            return jsonResponse?.choices?[0]?.text?.ToString() ?? "No response from API.";
        }
    }
}
