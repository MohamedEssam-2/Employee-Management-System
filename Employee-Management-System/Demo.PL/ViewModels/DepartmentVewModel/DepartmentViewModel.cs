namespace Demo.PL.ViewModels.DepartmentVewModel
{
    public class DepartmentViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
        public string? Description { get; set; }
    }
}
