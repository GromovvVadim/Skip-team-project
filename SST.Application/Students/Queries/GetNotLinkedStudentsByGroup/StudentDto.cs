using AutoMapper;
using SST.Application.Common.Mapping;
using SST.Domain.Entities;

namespace SST.Application.Students.Queries.GetNotLinkedStudentsByGroup
{
    public class StudentDto : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.LastName + " " + z.FirstName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.User.Email ?? string.Empty));
        }
    }
}
