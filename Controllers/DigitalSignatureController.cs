using InformationSecurity.DTO;
using InformationSecurity.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InformationSecurity.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace InformationSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class DigitalSignatureController : ControllerBase
    {
        private readonly IDigitalSignatureService _digitalSignatureService;

        public DigitalSignatureController(IDigitalSignatureService digitalSignatureService)
        {
            _digitalSignatureService = digitalSignatureService;
        }

        [HttpPost("sign")]
        public IActionResult SignData(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file provided for signing.");
            }

            try
            {
                RSA rsa = _digitalSignatureService.GenerateRSAKeyPair();
                var response = _digitalSignatureService.SignData(rsa, file);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifySignature([FromForm] VerifyRequestDto verifyRequest)
        {
            if (verifyRequest == null || verifyRequest.File == null || string.IsNullOrEmpty(verifyRequest.Signature) || string.IsNullOrEmpty(verifyRequest.PublicKey))
            {
                return BadRequest("Invalid data or signature.");
            }

            try
            {
                // In a real implementation, the public key should be retrieved from the sender or stored securely
                //RSA rsa = _digitalSignatureService.GenerateRSAKeyPair(); // Use the public key for verification
                RSA rsa = _digitalSignatureService.LoadPublicKey(verifyRequest.PublicKey);  // Assuming this method loads the public key into RSA

                // Verify the signature
                bool isVerified = _digitalSignatureService.VerifySignature(rsa, verifyRequest.File, verifyRequest.Signature);
                Console.WriteLine("here");
                return Ok(new { IsVerified = isVerified });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
