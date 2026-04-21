using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<CategoryListVm>> Handle(
            GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = (await _categoryRepository.ListAllAsync()).OrderBy(c => c.Name);
            return _mapper.Map<List<CategoryListVm>>(allCategories);
        }
    }
}
