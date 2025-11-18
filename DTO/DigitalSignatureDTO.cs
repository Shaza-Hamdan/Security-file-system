
namespace InformationSecurity.DTO
{
    public record SignRequestDto(
         IFormFile File,
        string Signature
    );
    public record SignResponseDto
    (
        // The digital signature in Base64 format
        string Signature,
        string rsa
    );
    public record VerifyRequestDto
(
     IFormFile File,  // File to be verified
     string Signature,
     string PublicKey
);
}
