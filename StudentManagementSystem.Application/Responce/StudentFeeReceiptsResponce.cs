﻿namespace StudentManagementSystem.Application.Responce;

public class StudentFeeReceiptsResponce
{
    public int Id { get; set; }
    public string AdmissionNo { get; set; } = string.Empty;
    public string ReceiptNo { get; set; } = string.Empty;
    public DateTime DateOfFee { get; set; }
    public double Fee { get; set; }
    public double Fine { get; set; }
    public string Month { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedDate { get; set; }
}
