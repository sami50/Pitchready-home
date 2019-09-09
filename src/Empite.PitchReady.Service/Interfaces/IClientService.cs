using Empite.PitchReady.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Empite.PitchReady.Service
{
    public interface IClientService
    {
        Task<List<Client>> GetClient();
        Task<Client> SaveClient(string firstName, string lastName, ApplicationUser user);

    }
}