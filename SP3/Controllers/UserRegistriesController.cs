using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP3.Model;
using Microsoft.Extensions.Options;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SP3;
using Microsoft.AspNetCore.Authorization;

namespace SP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistriesController : ControllerBase
    {
        private readonly SocialWelfareContext _context;
        private readonly IConfiguration _configuration;


        public UserRegistriesController(SocialWelfareContext context, IConfiguration configuration)
        {
            this._context = context;
            _configuration = configuration;
        }

        //checking if email already exist in the database
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmailExistence(string email)
        {
            var existingEmail = await _context.UserRegistries
                .Where(u => u.Email == email)
                .AnyAsync();

            return Ok(new { exists = existingEmail });
        }

        //Checking if CNIC already exists
        [HttpGet("check-cnic")]
        public async Task<IActionResult> CheckCnicExistence(long cnic)
        {
            var existingCnic = await _context.UserRegistries
                .Where(u => u.Cnic == cnic)
                .AnyAsync();

            return Ok(new { exists = existingCnic });
        }



        // GET: api/UserRegistries
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<UserRegistry>>> GetUserRegistries()
        {
          if (_context.UserRegistries == null)
          {
              return NotFound();
          }
            return await _context.UserRegistries.ToListAsync();
        }

        // GET: api/UserRegistries/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserRegistry>> GetUserRegistry(long id)
        {
          if (_context.UserRegistries == null)
          {
              return NotFound();
          }
            var userRegistry = await _context.UserRegistries.FindAsync(id);

            if (userRegistry == null)
            {
                return NotFound();
            }

            return userRegistry;
        }

        // PUT: api/UserRegistries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUserRegistry(long id, UserRegistry userRegistry)
        {
            if (id != userRegistry.Cnic)
            {
                return BadRequest();
            }

            var existingUserRegistry = await _context.UserRegistries.FindAsync(id);

            if (existingUserRegistry == null)
            {
                return NotFound();
            }

            // Update non-password related fields of the userRegistry object
            existingUserRegistry.Name = userRegistry.Name;
            // Update other fields here

            _context.Entry(existingUserRegistry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegistryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new {message="success"});
        }

        // PUT: api/UserRegistries/5/ChangePassword
        [HttpPut("{id}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(long id, ChangePassword changePassword)
        {
            // Validate the input, perform authentication checks, etc.

            var userRegistry = await _context.UserRegistries.FindAsync(id);

            if (userRegistry == null)
            {
                return NotFound();
            }

            // Update the password hash
            userRegistry.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);

            _context.Entry(userRegistry).Property(u => u.Password).IsModified = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/UserRegistries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegistry>> PostUserRegistry(UserRegistry userRegistry)
        {
          if (_context.UserRegistries == null)
          {
              return Problem("Entity set 'SocialWelfareContext.UserRegistries'  is null.");
          }
            string hashedpassword = BCrypt.Net.BCrypt.HashPassword(userRegistry.Password);
            userRegistry.Password=hashedpassword;
            _context.UserRegistries.Add(userRegistry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRegistryExists(userRegistry.Cnic))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRegistry", new { id = userRegistry.Cnic }, userRegistry);
        }

        //User Login Function Goes here


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            // Find the user in the database based on the provided username
            var user = await _context.UserRegistries.FirstOrDefaultAsync(u => u.Cnic == model.Cnic);
           
            
            // Check if the user exists and the provided password matches the stored password
            
            if (user != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, hash: user.Password);
                if (isPasswordValid)
                {
                    var token = GenerateJwtToken(model.Cnic.ToString());
                    return Ok(new {token});
                }
            }


            return Unauthorized(new { message = "Login Unsuccessful."});

        }
        private string GenerateJwtToken(string username)
        {
            string secretKey = _configuration.GetSection("Jwt:Key").Value;
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                Expires = DateTime.UtcNow.AddMinutes(20), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // DELETE: api/UserRegistries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserRegistry(long id)
        {
            if (_context.UserRegistries == null)
            {
                return NotFound();
            }
            var userRegistry = await _context.UserRegistries.FindAsync(id);
            if (userRegistry == null)
            {
                return NotFound();
            }

            _context.UserRegistries.Remove(userRegistry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRegistryExists(long id)
        {
            return (_context.UserRegistries?.Any(e => e.Cnic == id)).GetValueOrDefault();
        }


        
    }
}



