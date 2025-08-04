
namespace Application.IServices;

public interface IAwsService
{
    Task<string> DeletePreSignedUrlAsync(string imagePath);
    Task<PreSignedUrlResponseDto> PutPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto);
}