namespace MeBot.Response
{
    public class HuggingFaceResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public string Text { get; set; }
    }

}
