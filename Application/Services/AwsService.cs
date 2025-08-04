namespace Application.Services;

public class AwsService : IAwsService
{
    private readonly string _bucketName;
    private readonly AmazonS3Client _s3Client;

    public AwsService(IConfiguration configuration)
    {
        _bucketName = configuration["AWS_STORAGE_BUCKET_NAME"]!;
        var awsAccessKey = configuration["AWS_ACCESS_KEY_ID"]!;
        var awsSecretKey = configuration["AWS_SECRET_ACCESS_KEY"]!;

        var awsConfig = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUCentral1,
        };

        var basicAwsCredentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);

        _s3Client = new AmazonS3Client(basicAwsCredentials, awsConfig);
    }

    public async Task<string> DeletePreSignedUrlAsync(string imagePath)
    {
        imagePath = imagePath.Replace("%2F", "/");

        var preSignedUrlObj = new GetPreSignedUrlRequest()
        {
            BucketName = _bucketName,
            Key = imagePath,
            Expires = DateTime.UtcNow.AddSeconds(30),
            Verb = HttpVerb.DELETE,
            Protocol = Protocol.HTTPS
        };

        var preSignedUrl = await _s3Client.GetPreSignedURLAsync(preSignedUrlObj);

        return preSignedUrl;
    }

    public async Task<PreSignedUrlResponseDto> PutPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto)
    {
        var filePath = $"{preSignedUrlRequestDto.FolderName}/{Guid.NewGuid()}{preSignedUrlRequestDto.FileExtension}";

        var preSignedUrlObj = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = filePath,
            Expires = DateTime.UtcNow.AddSeconds(30),
            Verb = HttpVerb.PUT,
        };

        var preSignedUrl = await _s3Client.GetPreSignedURLAsync(preSignedUrlObj);

        return new PreSignedUrlResponseDto
        {
            SignedUrl = preSignedUrl,
            FilePath = filePath
        };
    }
}