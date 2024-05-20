using ApplicationLayer.Commands;
using AutoClassLibrary.Context;
using AutoClassLibrary.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.CommandHandelers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly AppDbContext _context;
        public CreateStudentCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Name = request.Name,
                Address = request.Address
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }

}
