namespace Application.Dto;

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    DateTime RegistrationDate,
    bool IsActive
);