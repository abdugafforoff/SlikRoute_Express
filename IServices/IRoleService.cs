using BIS_project.Models;

namespace BIS_project.IServices;

public interface IRoleService
{
    public Task<List<Role>> GetRoles();
    public Task<bool> CreateRole(string role);
}