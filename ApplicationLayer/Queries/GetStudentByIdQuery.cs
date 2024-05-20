using AutoClassLibrary.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Queries
{
    public class GetStudentByIdQuery : IRequest<Student>
    {
        public int Id { get; set; }
    }
}
