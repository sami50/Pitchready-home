using Empite.PitchReady.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Empite.PitchReady.Service
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(string email, string subject, Dictionary<string, string> properties, string templateName);
    }
}