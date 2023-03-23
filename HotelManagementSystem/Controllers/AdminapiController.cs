using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminapiController : ControllerBase
    {
        public AdminService aservice;
        private IConfiguration _config;
        public AdminapiController(IConfiguration cs, AdminService a)
        {
            _config = cs;
            aservice = a;
        }
        [HttpGet]
        public IActionResult GetAdmin()
        {
            IEnumerable<Admin>? alist;
            try
            {
                alist = aservice.GetAdmin();
                return Ok(alist);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string GenerateToken(Admin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.AdminName.ToString()),
                new Claim(ClaimTypes.Name,user.Password.ToString()),
                new Claim(ClaimTypes.Role,user.AdminType.ToString())

            };
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        [HttpGet("{AdminId}")]
        public async Task<IActionResult>GetAdminById([FromRoute] int AdminId)
        {
            Admin c;
            try
            {
                c = aservice.GetAdminById(AdminId);
                return  Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("UpdateAdmin")]
        public IActionResult UpdateAdmin(Admin? UpdAdmin)
        {
            Admin? UpdAdmins;
            try
            {
                UpdAdmin = aservice.UpdateAdmin(UpdAdmin);
                return Ok(UpdAdmin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult addAdmin([FromQuery] Admin a)
        {
            List<Admin> _adminlist = aservice.GetAdmin().ToList();
            foreach(Admin l in _adminlist)
            {
                if(l.AdminName.Equals(a.AdminName) && l.Password.Equals(a.Password) && a.AdminType.Equals(l.AdminType))
                {
                    var token = GenerateToken(a);
                    return Ok(token);
                }
            }
            return BadRequest("invalid request");
            
        }
        [HttpDelete("{AdminId}")]
        public async Task<IActionResult> Delete([FromRoute] int AdminId)
        {
            var admid = aservice.RemoveAdmin(AdminId);
            return Ok();
        }

    }
}
