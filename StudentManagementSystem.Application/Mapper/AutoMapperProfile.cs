using AutoMapper;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Domain.Entity;

namespace StudentManagementSystem.Application.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddStudentDetails, StudentDetails>()
            .ForMember(dest => dest.StudentImage, opt => opt.Ignore()).ForMember(dest=>dest.DateOfBirth,opt=>opt.Ignore())
            .ReverseMap();

        CreateMap<UpdateStudentDetails, StudentDetails>().ForMember(dest => dest.DateOfBirth, opt => opt.Ignore()).ReverseMap();

        CreateMap<StudentDetailsResponce, StudentDetails>().ReverseMap();

        CreateMap<AddStudentFeeReceipt,StudentFeeReceipts>()
            .ForMember(dest=>dest.DateOfFee,opt=>opt.Ignore()).ReverseMap();

        CreateMap<UpdateStudentFeeReceipt, StudentFeeReceipts>().ReverseMap();

        CreateMap<StudentFeeReceipts, StudentFeeReceiptsResponce>()
            .ForMember(dest=>dest.Status,opt=>opt.ToString()).ReverseMap();
    }
}
