namespace Application.Dtos.Storage;

public class PreSignedUrlResponseDto
{
    public required string SignedUrl { get; set; }
    public required string FilePath { get; set; }
}