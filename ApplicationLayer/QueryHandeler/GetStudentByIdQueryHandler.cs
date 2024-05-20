using ApplicationLayer.Queries;
using AutoClassLibrary.Context;
using AutoClassLibrary.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.QueryHandeler
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
    {
        private readonly AppDbContext _context;

        public GetStudentByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.FindAsync(request.Id);
        }
    }
}
