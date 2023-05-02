using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces.Services
{
    public interface IGeneralService
    {
        Task<ApiResponse> GetAddresses();
        Task<ApiResponse> GetInsurances();
        Task<ApiResponse> GetSpecialities();
        Task<ApiResponse> GetSubSpecialities();
    }
}
