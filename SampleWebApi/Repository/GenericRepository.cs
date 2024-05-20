using SampleWebApi.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Application.Implementation
{
    public class GenericRepository : IGenericInterface
    {
        private readonly IConfiguration _configuration;

        public GenericRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetValueFromEnvironment()
        {
            // Read value from environment variable


            return _configuration["Connection"];
        }
    }
}

