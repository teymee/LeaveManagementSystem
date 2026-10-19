using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveType);
        Task Create(LeaveTypeCreateVM model);
        Task Delete(int id);
        Task Edit(LeaveTypeEditVM model);
        Task<List<LeaveTypeReadOnlyVM>> GetAll();
        Task<T?> GetDetails<T>(int id) where T : class;
        bool LeaveTypeExists(int? id);
    }
}