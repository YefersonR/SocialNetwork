using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces
{
    public interface IGetRepository<Vm> where Vm : class
    {
        Task<List<Vm>> GetAll();
        Task<Vm> GetById();

    }
}
