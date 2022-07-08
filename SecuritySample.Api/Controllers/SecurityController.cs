using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools.Cryptography;

namespace SecuritySample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly CryptoRSA _cryptoRSA;

        public SecurityController(CryptoRSA cryptoRSA)
        {
            _cryptoRSA = cryptoRSA;
        }

        [HttpGet("GetPublicKey/")]
        public IActionResult Get()
        {
            return Ok(new { PublicKey = Convert.ToBase64String(_cryptoRSA.ToByteArray(false)) });
        }
    }
}
