using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TorteLand.WebAPI2.Auth;

internal sealed class Auth : IAuth
{
    private readonly byte[] _salt;
    private readonly byte[] _hash;
    private readonly SigningCredentials _credentials;
    private readonly Encoding _encoding;

    public Auth(string token, Encoding encoding)
    {
        _encoding = encoding;

        _credentials = new SigningCredentials(
            new SymmetricSecurityKey(_encoding.GetBytes(token)),
            SecurityAlgorithms.HmacSha512Signature);

        // TODO : google it and fix
        _salt = ReadFile("salt");
        _hash = ReadFile("hash");
    }

    public void Register(string password)
    {
        using var hmac = new HMACSHA512();
        var hash = hmac.ComputeHash(_encoding.GetBytes(password));
        File.WriteAllBytes("salt", hmac.Key);
        File.WriteAllBytes("hash", hash);
    }

    public string Login(string password)
    {
        using var hmac = new HMACSHA512(_salt);
        var hash = hmac.ComputeHash(_encoding.GetBytes(password));

        if (!hash.SequenceEqual(_hash))
            throw new Exception("wrong password");

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(1),
            signingCredentials: _credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static byte[] ReadFile(string name)
        => File.Exists(name) ? File.ReadAllBytes(name) : Array.Empty<byte>();
}