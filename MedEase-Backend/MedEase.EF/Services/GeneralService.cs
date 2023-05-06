using AutoMapper;
using MedEase.Core;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GeneralService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ApiResponse> GetAddresses()
        {
            IEnumerable<Address> addresses = await _unitOfWork.Addresses.GetAllAsync();
            return new ApiResponse(200, true, addresses);
        }

        public async Task<ApiResponse> GetInsurances()
        {
            return new(200, true, _mapper.Map<IEnumerable<InsuranceDto>>(await _unitOfWork.Insurance.GetAllAsync()));
        }

        public async Task<ApiResponse> GetSpecialities()
        {
            return new(200, true, _mapper.Map<IEnumerable<SpecialityDto>>(await _unitOfWork.Speciality.GetAllAsync()));
        }

        public async Task<ApiResponse> GetSubSpecialities()
        {
            return new(200, true, _mapper.Map<IEnumerable<SubspecialityDto>>(await _unitOfWork.SubSpeciality.GetAllAsync()));
        }

        public async Task<ApiResponse> GetSubSpecialitiesBySpecialityId(int SpecialityId)
        {
          
           return new(200, true, _mapper.Map<IEnumerable<SubspecialityDto>>(await _unitOfWork.SubSpeciality.FindAllAsync(s=>s.specialityID== SpecialityId)));
        }
        public async Task<ApiResponse> BasicInformation(int SpecialityId)
        {
            if (SpecialityId > 0)
            {
                BasicInformationDto basicInfo = new BasicInformationDto()
                {
                    specialityName= (string)await _unitOfWork.Speciality.FindWithSelectAsync(s=>s.ID== SpecialityId,s=>s.Name),
                    DoctorCount= await _unitOfWork.Doctors.CountAsync(d => d.SpecialityID == SpecialityId && d.IsConfirmed == true)
                    
                };
                return new(200, true, basicInfo);

            }
            else
            {
                BasicInformationDto basicInfo = new BasicInformationDto()
                {
                    specialityName = "All",
                    DoctorCount = await _unitOfWork.Doctors.CountAsync(D=>D.IsConfirmed==true)

                };
                return new(200, true, basicInfo);

            }

        }
    }
}
