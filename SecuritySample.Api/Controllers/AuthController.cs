using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecuritySample.Api.Models.Forms;
using System.Data;
using System.Data.SqlClient;
using Tools.Cryptography;

namespace SecuritySample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CryptoRSA _crypto;
        private readonly SqlConnection _dbConnection;

        public AuthController(CryptoRSA crypto, SqlConnection dbConnection)
        {
            _crypto = crypto;
            _dbConnection = dbConnection;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            form.Passwd = _crypto.Decrypt(Convert.FromBase64String(form.Passwd!));

            using(SqlCommand dbCommand = _dbConnection.CreateCommand())
            {
                dbCommand.CommandText = "AppUserSchema.CSP_Register";
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("Email", form.Email);
                dbCommand.Parameters.AddWithValue("Passwd", form.Passwd);
                _dbConnection.Open();
                int rows = dbCommand.ExecuteNonQuery();

                return rows == 1 ? NoContent() : BadRequest();
            }

            
        }
    }
}
