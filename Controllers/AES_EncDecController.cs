using InformationSecurity.DTO;
using InformationSecurity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,User")]
public class AES_EncDecController : ControllerBase
{
    private readonly IAES_EncDecService _encryptionService;

    public AES_EncDecController(IAES_EncDecService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    [HttpPost("encrypt")]
    public async Task<IActionResult> EncryptFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Save the uploaded file temporarily before encryption in TempFiles
        string tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TempFiles", file.FileName);
        using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // Encrypt the file
        string uniqueId = await _encryptionService.EncryptFileAsync(tempFilePath);

        // Clean up the temporary file after encryption
        System.IO.File.Delete(tempFilePath);

        return Ok(new { Message = "File encrypted successfully.", FileId = uniqueId });
    }

    [HttpPost("decrypt")]
    public async Task<IActionResult> DecryptFile([FromBody] decrypt_File fileId)
    {
        if (fileId == null || string.IsNullOrEmpty(fileId.FileId))
            return BadRequest("File ID is required.");
        try
        {
            // Decrypt the file using the fileId
            string decryptedFilePath = await _encryptionService.DecryptFileAsync(fileId.FileId);

            // Return the decrypted file for download
            string fileName = Path.GetFileName(decryptedFilePath);
            string mimeType = "application/pdf"; // Adjust this based on the actual file type

            return PhysicalFile(decryptedFilePath, mimeType, fileName);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (CryptographicException ex)
        {
            return BadRequest(new { Message = "Decryption failed. The file might be corrupted or have invalid padding.", Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
        }
    }

}