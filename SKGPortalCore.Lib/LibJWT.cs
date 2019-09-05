using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SKGPortalCore.Lib
{
    public class LibJWT
    {
        /// <summary>
        /// 產生JWT Token
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static string GenerateToken(string secret, string claimType, string claimValue)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var payload = new Dictionary<string, object>
                        {
                            {"ClaimType", claimType},
                            {"ClaimValue",claimValue }
                        };
            var token = encoder.Encode(payload, secret);
            return token;
        }
        /// <summary>
        /// 解析 JWT Token
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool TryValidateToken(string secret, string token, out ClaimsPrincipal principal)
        {
            principal = null;
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var payload = decoder.DecodeToObject(token, secret, verify: true);
                List<Claim> claims = new List<Claim>();

                foreach (var item in payload)
                {
                    if (item.Value == null)
                    {
                        continue;
                    }

                    var key = item.Key;
                    var value = item.Value.ToString();
                    claims.Add(new Claim(key, value));
                }
                var identity = new ClaimsIdentity(claims, "JWT");
                principal = new ClaimsPrincipal(identity);
                return true;
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            return false;
        }
    }
}
