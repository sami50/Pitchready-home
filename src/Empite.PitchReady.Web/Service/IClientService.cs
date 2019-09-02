using System.Collections.Generic;
using System.Threading.Tasks;
using Empite.PitchReady.Entity;

namespace Empite.PitchReady.Web.Service
{
    public interface IClientService
    {
        Task<List<Client>> GetClient();
        Task<Client> SaveClient(string firstName, string lastName);
    }
}