using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces
{
    public interface IGenericRepository<T,VM> : IGetRepository<VM>,IModifyRepository<T,VM>,IRemoveRepository<T> where T:class where VM:class 
    {
         
    }
}
