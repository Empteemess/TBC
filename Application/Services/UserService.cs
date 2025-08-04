namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<GetApplicationUserDto> FilterApplicationUser(FilterDto filterDto)
    {
        var users = _unitOfWork.UserRepository.GetAllApplicationUsers();

        var filteredUsers = FilterUsers(filterDto, users);

        var pagingResult = filteredUsers
            .Skip((filterDto.CurrentPage - 1) * filterDto.UserQuantity)
            .Take(filterDto.UserQuantity)
            .Select(x => x.ToGetApplicationUser())
            .ToList();

        return pagingResult;
    }

    private IQueryable<ApplicationUser> FilterUsers(FilterDto filterDto, IQueryable<ApplicationUser> users)
    {
        if (filterDto.FirstName is not null)
        {
            users = users.Where(x => x.FirstName.Contains(filterDto.FirstName));
        }

        if (filterDto.LastName is not null)
        {
            users = users.Where(x => x.LastName.Contains(filterDto.LastName));
        }

        if (filterDto.PersonalId is not null)
        {
            users = users.Where(x => x.PersonalId.Contains(filterDto.PersonalId));
        }

        return users;
    }

    public async Task<GetApplicationUserDto> GetApplicationUserByIdAsync(int userId)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

        if (user is null) throw new NotFoundException("User not found");

        var allRelationships = GetRelationshipDtos(user);

        var mappedApplicationUser = user.ToGetApplicationUserDto(allRelationships);

        return mappedApplicationUser;
    }

    private IEnumerable<FullRelationshipDto> GetRelationshipDtos(ApplicationUser user)
    {
        var connections = user.Connections ?? [];
        var connectedBy = user.ConnectedBy ?? [];

        var allRelationships = new List<FullRelationshipDto>();

        foreach (var relationship in connections)
        {
            var relationDto = new FullRelationshipDto
            {
                UserId = relationship.TargetUserId,
                FirstName = relationship.TargetUser?.FirstName ?? String.Empty,
                LastName = relationship.TargetUser?.LastName ?? String.Empty,
            };
            allRelationships.Add(relationDto);
        }

        foreach (var relationship in connectedBy)
        {
            var relationDto = new FullRelationshipDto
            {
                UserId = relationship.SourceUserId,
                FirstName = relationship.SourceUser?.FirstName ?? String.Empty,
                LastName = relationship.SourceUser?.LastName ?? String.Empty,
            };
            allRelationships.Add(relationDto);
        }

        return allRelationships;
    }

    public async Task RemoveApplicationUser(int userId)
    {
        var applicationUser = await _unitOfWork.UserManager.FindByIdAsync($"{userId}");

        if (applicationUser is null)
            throw new IdentityException($"Application User with id - ({userId})", (int)HttpStatusCode.NotFound);

        _unitOfWork.UserRelationshipsRepository.RemoveRelationshipBySourceId(applicationUser.Id);

        await _unitOfWork.UserManager.DeleteAsync(applicationUser);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddApplicationUserAsync(ApplicationUserDto applicationUserDto)
    {
        var applicationUser = applicationUserDto.ToApplicationUser();

        if (applicationUser is null) throw new IdentityException("Application user is null");

        await _unitOfWork.UserManager.CreateAsync(applicationUser);
    }

    public async Task EditApplicationUserAsync(EditApplicationUserDto editApplicationUserDto)
    {
        if (editApplicationUserDto is null) throw new IdentityException("You must update minimum One Field");

        var applicationUser = await _unitOfWork.UserRepository.GetUserByIdAsync(editApplicationUserDto.UserId);

        if (applicationUser is null) throw new IdentityException("Application", (int)HttpStatusCode.NotFound);

        var mappedApplicationUser = applicationUser.ToUpdateApplicationUser(editApplicationUserDto);

        await _unitOfWork.UserManager.UpdateAsync(mappedApplicationUser);
    }
}

