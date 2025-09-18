namespace EV_Station.Application.Common.Responses.Gemini
{
    public class GeminiResponse
    {
        public List<Candidate> candidates { get; set; } = default!;
    }

    public class Candidate
    {
        public CandidateContent content { get; set; } = default!;
    }

    public class CandidateContent
    {
        public List<GeminiPart> parts { get; set; } = default!;
    }
}
