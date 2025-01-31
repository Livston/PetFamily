namespace PetFamily.API.Response
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    public record Envelope
    {
        public Envelope(object? result, IEnumerable<ResponseError> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        public object? Result { get; }
        public List<ResponseError> Errors { get; }
        public DateTime TimeGenerated { get; }

        public static Envelope ok(object? result = null)
            => new Envelope(result, []);

        public static Envelope Error(IEnumerable<ResponseError> errors)
            => new Envelope(null, errors);
    }
}
