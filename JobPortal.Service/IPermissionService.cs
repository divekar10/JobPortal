
namespace JobPortal.Service
{
    public interface IPermissionService
    {
        bool Authorize(string permission);
    }
}
