using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(GloboTicketDbContext dbContext) : base(dbContext)
        { }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePastEvents)
        {
            var allCategories = await _dbContext.Categories
                .Include(c => c.Events)
                .ToListAsync();

            if (!includePastEvents)
            {
                allCategories.ForEach(c => c.Events.ToList().RemoveAll(e => e.Date < DateTime.Today));
            }

            return allCategories;
        }
    }
}
