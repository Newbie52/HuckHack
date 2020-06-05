using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HuckHack.Domain.Contracts.Services
{
    public interface ICommunicationService
    {
        Task SendInviteEmail(string fromName, string fromUserProfileLink, string toEmail, string coverLetter);
    }
}
