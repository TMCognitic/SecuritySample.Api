using System.Data;
using System.Data.SqlClient;
using Tools.Cryptography;
using System.Text.Json;

namespace SecuritySample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CryptoAes cryptoAes = new CryptoAes(Properties.Resources.Vector, Properties.Resources.Key);
            string connectionString = cryptoAes.Decrypt(Properties.Resources.Secret);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient(sp => new SqlConnection(connectionString));
            builder.Services.AddSingleton(sp => new CryptoRSA(Properties.Resources.RSA_Keys));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}