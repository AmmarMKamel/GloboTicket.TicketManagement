using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(
            IMapper mapper,
            IAsyncRepository<Event> eventRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _eventRepository = eventRepository ?? throw new ArgumentException(nameof(eventRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
        }

        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            var eventDetailsDto = _mapper.Map<EventDetailVm>(@event);

            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);
            eventDetailsDto.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailsDto;
        }
    }
}
