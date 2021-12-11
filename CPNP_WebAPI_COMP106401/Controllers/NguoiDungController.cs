using CPNP_WebAPI_COMP106401.EF;
using CPNP_WebAPI_COMP106401.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CPNP_WebAPI_COMP106401.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly Appsetting _appSetting;

        public NguoiDungController(MyDbContext context, IOptionsMonitor<Appsetting> optionsMonitor)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetAll(string tennguoidung)
        {
            try
            {
                var dsNguoiDung = _context.NguoiDungs.AsQueryable();
                if (!string.IsNullOrEmpty(tennguoidung))
                {
                    dsNguoiDung = dsNguoiDung.Where(hh => hh.HoTen.Contains(tennguoidung));
                }
                return Ok(dsNguoiDung);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var nd = _context.NguoiDungs.SingleOrDefault(x => x.id == id);
                if (nd == null)
                {
                    return NotFound();
                }
                return Ok(nd);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateNguoiDung(NguoiDung nd)
        {
            var _nd = new NguoiDung
            {
               id = nd.id,
               UserName = nd.UserName,
               Password = nd.Password,
               HoTen = nd.HoTen,
               Email = nd.Email,
            };
            _context.NguoiDungs.Add(_nd);
            _context.SaveChanges();
            return Ok(new { Success = true, Data = _nd });
        }


        [HttpPut("{id}")]
        public IActionResult EditNguoiDung(int id, NguoiDung nd)
        {
            var _nd = _context.NguoiDungs.SingleOrDefault(x => x.id == id);
            if (_nd != null)
            {
                _nd.UserName = nd.UserName;
                _nd.Password = nd.Password;
                _nd.HoTen = nd.HoTen;
                _nd.Email = nd.Email;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNguoiDung(int id)
        {
            try
            {
                var nd = _context.NguoiDungs.SingleOrDefault(x => x.id == id);
                if (nd == null)
                {
                    return NotFound();
                }
                _context.NguoiDungs.Remove(nd);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel model)
        {
            var user = _context.NguoiDungs.SingleOrDefault(nd => nd.UserName == model.UserName && nd.Password == model.Password);
            if(user == null)
            {
                return Ok(new ApiRespone
                { 
                    Success = false,
                    Message = "Invalid username or password"
                });
            }

            //cấp token
            var token = await GeneratorToken(user);
            return Ok(new ApiRespone { 
                Success = true,
                Message = "Authenticate Success",
                Data = token
            });
        }
        private async Task<Token> GeneratorToken(NguoiDung nd)
        {
            var JwtToken = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var tokenGenerator = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, nd.Email),
                    new Claim(JwtRegisteredClaimNames.Email, nd.HoTen),
                    new Claim(JwtRegisteredClaimNames.Sub, nd.HoTen),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", nd.UserName),
                    new Claim("id", nd.id.ToString()),
                    //roles
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new 
                    SymmetricSecurityKey(secretKey), 
                    SecurityAlgorithms.HmacSha512Signature)
                
            };
            var token = JwtToken.CreateToken(tokenGenerator);
            var accessToken =  JwtToken.WriteToken(token);
            var refreshToken = GeneratorRefreshToken();

            //Lưu vào Database
            var refreshTokenEntity = new RefreshToken
            {
                id = Guid.NewGuid(),
                JwtId = token.Id,
                idUser = nd.id,
                Token = refreshToken,
                isUse = false,
                isRevoke = false,
                isUseAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GeneratorRefreshToken()
        {
            var random = new byte[32];
            using(var data = RandomNumberGenerator.Create())
            {
                data.GetBytes(random);
            };
            return Convert.ToBase64String(random);
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(Token model)
        {
            var JwtToken = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var tokenValidParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //Ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, //không kiểm tra token hết hạn
            };
            try
            {
                //check 1: AccessToken valid Format
                var tokenVerification = JwtToken.ValidateToken(model.AccessToken, 
                        tokenValidParam, out var validated);

                //check 2: kiểm tra thuật toán
                if(validated is JwtSecurityToken jwt)
                {
                    var result = jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result) //false
                    {
                        return Ok(new ApiRespone
                        {
                            Success = false,
                            Message = "Invalid Token"
                        });
                    }
                }

                //check 3: Check token đã expried hay chưa
                
                var utcExpriedDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expriedDate = ConvertUnixTimeToDateTime(utcExpriedDate);
                if(expriedDate > DateTime.UtcNow)
                {
                    return Ok(new ApiRespone
                    {
                        Success = false,
                        Message = "Access Token has not yet expried"
                    });
                }

                //check 4: check refresh token exits in Database
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if(storedToken == null)
                {
                    return Ok(new ApiRespone
                    {
                        Success = false,
                        Message = "Access Token dose not exits"
                    });
                }

                //check 5: check refresh token đã sử dụng\thu hồi hay chưa
                if (storedToken.isUse)
                {
                    return Ok(new ApiRespone
                    {
                        Success = false,
                        Message = "Access Token had been used"
                    });
                }
                if (storedToken.isRevoke)
                {
                    return Ok(new ApiRespone
                    {
                        Success = false,
                        Message = "Access Token had been revoke"
                    });
                }

                //check 6: Kiểm tra id của token === Jwt trong refresh token hay không
                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if(storedToken.JwtId != jti)
                {
                    return Ok(new ApiRespone
                    {
                        Success = false,
                        Message = "Access Token does't match"
                    });
                }

                //Update token is used
                storedToken.isRevoke = true;
                storedToken.isUse = true;
                await _context.SaveChangesAsync();

                //create new token
                var user = _context.NguoiDungs.SingleOrDefault(nd => nd.id == storedToken.idUser);
                var token = await GeneratorToken (user);
                return Ok(new ApiRespone
                {
                    Success = true,
                    Message = "Renew token success",
                    Data = token
                });

            }
            catch(Exception ex)
            {
                return BadRequest(new ApiRespone
                {
                    Success = false,
                    Message = "Something went wrong"
                });
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpriedDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpriedDate).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}
