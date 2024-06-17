namespace Sds.DentistChair.Api.Dtos;

public class CustomResponse
{
    public bool Success { get; set; }

    public int Status { get; set; }

    public DateTimeOffset DateUtc { get; private set; } = DateTimeOffset.UtcNow;

    public object Data { get; set; }

    public string[] Messages { get; set; }
}
