using System.ComponentModel;

namespace Domain.Models;

[Description("Users model")]
public class User
{
    [Description("Users unicure id")]
    public Guid Id { get; set; }
    
    [Description("Users e-mail")]
    public string Email { get; set; } = string.Empty;
    
    [Description("Users first name")]
    public string FirstName { get; set; } = string.Empty;
    
    [Description("Users last name")]
    public string LastName { get; set; } = string.Empty;
    
    [Description("User date of birth")]
    public DateTime DateOfBirth { get; set; }
    
    [Description("Users registration date")]
    public DateTime RegistrationDate { get; set; }
    
    [Description("User activity flag")]
    public bool IsActive { get; set; }
}