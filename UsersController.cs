[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = new();

    // POST api/users/register
    [HttpPost("register")]
    public IActionResult Register([FromBody] User newUser)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        // Simuler hashing du mot de passe
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        users.Add(newUser);
        return Ok("User registered successfully");
    }

    // POST api/users/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        var user = users.SingleOrDefault(u => u.Email == loginUser.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            return Unauthorized("Invalid credentials");

        var token = JwtHelper.GenerateToken(user);
        return Ok(new { token });
    }

    // GET api/users/admin
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Access granted to Admin!");
    }
}
