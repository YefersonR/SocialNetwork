using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IGenericService<T,Vm> where T:class where Vm:class
    {
        Task Add(T type);
        Task Update(T type, int id);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<List<Vm>> GetAllViewModels();
    }
}
