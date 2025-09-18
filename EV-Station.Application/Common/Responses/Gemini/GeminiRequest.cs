namespace EV_Station.Application.Common.Responses.Gemini
{
    public class GeminiRequest
    {
        public List<GeminiContent> contents { get; set; } = default!;
    }

    public class GeminiContent
    {
        public List<GeminiPart> parts { get; set; } = default!;
    }

    public class GeminiPart
    {
        public string text { get; set; } = default!;
    }
}
