

namespace StudentManagementSystem.Application.Responce;

public class StudentDetailsResponce
{
    public int Id { get; set; }
    public string AdmissionNo { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string StudentImage { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedDate { get; set; }
}
