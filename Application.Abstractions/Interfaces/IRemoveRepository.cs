using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces
{
    public interface IRemoveRepository<T> where T : class
    {
        Task Delete(int id);
    }
}
