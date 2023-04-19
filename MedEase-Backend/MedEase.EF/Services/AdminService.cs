using MedEase.Core.Dtos;
using MedEase.Core.Models;
using MedEase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedEase.Core.Interfaces.Services;

namespace MedEase.EF.Services
{
    public class AdminService:IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<int> AddInsurance(InsuranceDto InsuranceDto)
        {
            Insurance NewInsurance = new Insurance();
            NewInsurance = _mapper.Map<Insurance>(InsuranceDto);

             await _unitOfWork.Insurance.AddAsync(NewInsurance);
            return _unitOfWork.Complete();

        }
    }
}
