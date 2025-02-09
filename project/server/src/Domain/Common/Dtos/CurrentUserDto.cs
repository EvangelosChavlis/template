namespace server.src.Domain.Common.Dtos;

public record CurrentUserDto(Guid Id, 
    string UserName, bool UserFound);

