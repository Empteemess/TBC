using Application.Dtos.Storage;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{
    private readonly IAwsService _awsService;


    public StorageController(IAwsService awsService)
    {
        _awsService = awsService;
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> DeletePreSignedUrlAsync(string url)
    {
        var preSignedUrl = await _awsService.DeletePreSignedUrlAsync(url);

        return Ok(new { DeletePreSignedUrl = preSignedUrl });
    }

    [HttpPut]
    public async Task<IActionResult> PutPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto)
    {
        var preSignedUrl = await _awsService.PutPreSignedUrlAsync(preSignedUrlRequestDto);

        return Ok(new { PreSignedUrl = preSignedUrl });
    }
}
