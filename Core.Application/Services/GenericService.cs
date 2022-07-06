using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class GenericService<T, SaveViewModel, ViewModel> : IGenericService<SaveViewModel, ViewModel>
        where T : class
        where SaveViewModel : class
        where ViewModel : class

    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<T> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public virtual async Task<SaveViewModel> Add(SaveViewModel type)
        {

            T entity = _mapper.Map<T>(type);

            await _genericRepository.Add(entity);

            SaveViewModel viewModel = _mapper.Map<SaveViewModel>(entity);
            return viewModel;
        }

        public virtual async Task Delete(int id)
        {
            T entity = await _genericRepository.GetById(id);
            await _genericRepository.Delete(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModels()
        {
            List<T> entity = await _genericRepository.GetAll();
            List<ViewModel> viewModels = _mapper.Map<List<ViewModel>>(entity);
            return viewModels;
        }

        public virtual async Task<SaveViewModel> GetById(int id)
        {
            T entity = await _genericRepository.GetById(id);
            SaveViewModel viewModel = _mapper.Map<SaveViewModel>(entity);
            return viewModel;
        }

        public virtual async Task Update(SaveViewModel type, int id)
        {
            T entity = _mapper.Map<T>(type);
            await _genericRepository.Update(entity,id);
        }
    }
}
