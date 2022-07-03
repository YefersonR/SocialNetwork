using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces
{
    public interface IModifyRepository<T,Vm> where T:class where Vm : class 
    {
        Task Add(T type);
        Task task(T Type, Vm ViewModel);

    }
}
