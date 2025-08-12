public class UserValidationTests
{
    [Fact]
    public void RegisterUser_WithInvalidEmail_ShouldFailValidation()
    {
        var user = new User { Email = "invalidEmail", Password = "password123" };
        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(user, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Email"));
    }
}
