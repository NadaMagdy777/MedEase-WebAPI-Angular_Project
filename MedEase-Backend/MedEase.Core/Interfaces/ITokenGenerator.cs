using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user, int Id);
    }
}
