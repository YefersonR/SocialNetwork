using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class 
    {
        private readonly SocialMediaContext _mediaContext;
        public GenericRepository(SocialMediaContext mediaContext)
        {
            _mediaContext = mediaContext;
        }
        public virtual async Task Add(T type)
        {
            await _mediaContext.Set<T>().AddAsync(type);
            await _mediaContext.SaveChangesAsync();
        }
        public virtual async Task Update(T Type, int id)
        {
            var entry = _mediaContext.Set<T>().FindAsync(id);
            _mediaContext.Entry(entry).CurrentValues.SetValues(Type);
            await _mediaContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _mediaContext.Set<T>().ToListAsync();            
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _mediaContext.Set<T>().FindAsync(id);
        }
        public virtual async Task Delete(T type)
        {
            _mediaContext.Set<T>().Remove(type);
            await _mediaContext.SaveChangesAsync();
        }

    }
}
