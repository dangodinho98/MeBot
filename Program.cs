using MeBot.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace MeBot
{
    internal class Program
    {
        private const string Phi3ApiKey = "your-phi3-api-key";
        private const string Phi3ApiUrl = "https://api.anthropic.com/v1/completions"; // Hypothetical URL
        private const string PdfFilePath = "path_to_your_resume.pdf";

        private static async Task Main(string[] args)
        {
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

                var answer = await GetAnswerFromModelAsync(question, resumeText);
                Console.WriteLine($"Daniel's answer: {answer}\n");
            }
        }

        // This function retrieves the answer from Phi-3 (Hypothetical example)
        public static async Task<string> GetAnswerFromModelAsync(string question, string context)
        {
            using var client = new HttpClient();
            
            // Prepare the API request body for Phi-3 (Hypothetical)
            var requestData = new
            {
                model = "phi-3",  // Hypothetical model name for Phi-3
                prompt = $"{context}\nQuestion: {question}",
                temperature = 0.7,  // Adjust temperature for creativity of answers
                max_tokens = 150    // You can adjust this based on your needs
            };

            // Set the authorization header with your Phi-3 API key
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Phi3ApiKey}");

            // Serialize the request data and send the POST request
            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Phi3ApiUrl, content);

            // Read the response
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);

            // Return the answer from the model
            return jsonResponse.choices[0].text.ToString();
        }
    }
}
