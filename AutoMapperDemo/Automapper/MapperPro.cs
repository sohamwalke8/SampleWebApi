using AutoClassLibrary.DTO;
using AutoClassLibrary.Model;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutoMapperDemo.Automapper
{
    public class MapperPro : Profile
    {

        public MapperPro()
        {
            CreateMap<Student, StudentDto>().ReverseMap();

        }
    }
}
