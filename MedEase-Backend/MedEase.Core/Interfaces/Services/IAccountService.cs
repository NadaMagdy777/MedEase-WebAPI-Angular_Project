using MedEase.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ApiResponse> RegisterDoctor(DoctorRegisterDto docDto);
    }
}
