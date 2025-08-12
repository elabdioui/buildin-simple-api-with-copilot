// Exemple Controller minimal
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpGet]
    public IActionResult GetAll() => Ok(users);

    [HttpGet("{id}")]
    public IActionResult Get(int id) 
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user) 
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        users.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User updatedUser) 
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        // update properties...
        user.Name = updatedUser.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) 
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        users.Remove(user);
        return NoContent();
    }
}

// Exemple User avec validation
public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }
}
