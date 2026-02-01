 using Demo_BLL.DTOs.DepartmentDto;

namespace Demo_BLL.Services.Interfaces
{
    public interface IDepartment_Service
    {
        int AddDepartment(Add_Department_Dto dept);
        bool DeleteDepartment(int id);
        IEnumerable<Department_Dto> GetAllDepartmnets(string? DepartmenSearchName = null);
        Department_Details_Dto? GetDepartmentById(int id);
        int UpdateDepartment(Update_Department_Dto dept);
    }
}