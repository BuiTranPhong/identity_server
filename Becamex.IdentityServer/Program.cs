using Becamex.IdentityServer.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RSACryptoServiceProvider GetRSACryptoServiceProvider()
{
    return new RSACryptoServiceProvider(2048);
}
SecurityKey GetSecurityKey()
{
    return new RsaSecurityKey(GetRSACryptoServiceProvider()) { KeyId = "8E326BF5420CBC8ABF92FA5BA0AAE11F" };
}
//var newKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abc@123")) { KeyId = "8E326BF5420CBC8ABF92FA5BA0AAE11F" };

var key = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.RsaSha256);
IdentityServerConstants.RsaSigningAlgorithm signingAlgorithm = IdentityServerConstants.RsaSigningAlgorithm.RS256;
//var rsa = CryptoHelper.CreateRsaSecurityKey(rsaParameters, "8E326BF5420CBC8ABF92FA5BA0AAE11F");
var signinKey = new RsaSecurityKey(RSA.Create()) { KeyId = "8E326BF5420CBC8ABF92FA5BA0AAE11F" };

try
{
    builder.Services.AddIdentityServer()
      .AddInMemoryClients(Clients.Get())
      .AddInMemoryIdentityResources(Becamex.IdentityServer.Models.Resources.GetIdentityResources())
      .AddInMemoryApiResources(Becamex.IdentityServer.Models.Resources.GetApiResources())
      .AddInMemoryApiScopes(Scopes.GetApiScopes())
      .AddTestUsers(Users.Get())
      .AddSigningCredential(signinKey, signingAlgorithm);
}
catch (Exception ex)
{

    throw;
}


builder.Services.AddMvc();
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



  //.AddTemporarySigningCredential();

app.UseIdentityServer();
app.UseStatusCodePages();

app.Run();
