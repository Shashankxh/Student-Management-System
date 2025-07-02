using AutoMapper;
using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentDetail.Query;

public class GetByIdHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetStudentDetailsById, Result<StudentDetailsResponce>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<StudentDetailsResponce>> Handle(GetStudentDetailsById request, CancellationToken cancellationToken)
    {
        var studentDetails = await _unitOfWork.StudentDetail.GetByIdAsync(request.Id);

        if (studentDetails == null)
        {
            return Result<StudentDetailsResponce>.Failure("Student not found");
        }

        var studentDetailsResponce = _mapper.Map<StudentDetailsResponce>(studentDetails);
        return Result<StudentDetailsResponce>.Success(studentDetailsResponce);
    }
}
