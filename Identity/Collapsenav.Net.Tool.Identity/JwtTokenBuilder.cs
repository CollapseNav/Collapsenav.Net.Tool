using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Collapsenav.Net.Tool.Identity;
public class JwtTokenBuilder
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public Dictionary<string, string> Claims { get; set; }
    public DateTime Expires { get => expires ?? NotBefore.AddMinutes(5); set => expires = value; }
    private DateTime? expires;
    public string Secret { get; set; }
    public string Algorithm { get; set; }
    public DateTime NotBefore { get => notBefore ?? DateTime.Now; set => notBefore = value; }
    private DateTime? notBefore;
    public JwtSecurityTokenHandler Handler { get; }

    public JwtTokenBuilder AddClaim(string key, string value)
    {
        Claims.Add(key, value);
        return this;
    }
    public JwtTokenBuilder AddClaims(IEnumerable<KeyValuePair<string, string>> kvs)
    {
        Claims.AddRange(kvs);
        return this;
    }

    public JwtTokenBuilder(string simple = null)
    {
        Issuer = simple ?? "Collapsenav.Net.Tool";
        Audience = Issuer;
        Secret = Issuer;
        Algorithm = SecurityAlgorithms.HmacSha256;
        Handler = new();
        Claims = new();
    }
    public JwtSecurityToken Build()
    {
        return new JwtSecurityToken(
            Issuer, Audience, Claims.Select(item => new Claim(item.Key, item.Value)), NotBefore, Expires, new SigningCredentials(new SymmetricSecurityKey(Secret.ToBytes()), Algorithm)
        );
    }

    public string BuildTokenString()
    {
        return Handler.WriteToken(Build());
    }
}