using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empite.PitchReady.Entity;
using Microsoft.EntityFrameworkCore;

namespace Empite.PitchReady.Service
{
    public class ClientService : IClientService
    {
        private ApplicationDbContext _applicationDbContext;

        public ClientService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<List<Client>> GetClient()
        {
            return await _applicationDbContext.Clients.Include(x=>x.ApplicationUser).AsNoTracking().ToListAsync();
        }

        public async Task<Client> SaveClient(string firstName, string lastName, ApplicationUser user)
        {
            Client client = new Client() {FirstName = firstName, LastName = lastName, ApplicationUser = user};
            await _applicationDbContext.Clients.AddAsync(client);
            await _applicationDbContext.SaveChangesAsync();

            return client;
        }
    }
}
