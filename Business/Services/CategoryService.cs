using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IExerciseUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IExerciseUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCategoryViewModel>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();

            var categoriesViewModels = _mapper.Map<IEnumerable<CategoryType>, IEnumerable<GetCategoryViewModel>>(categories);

            return categoriesViewModels;
        }
    }
}
