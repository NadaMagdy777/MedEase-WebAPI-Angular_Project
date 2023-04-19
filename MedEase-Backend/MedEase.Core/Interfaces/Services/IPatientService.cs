using MedEase.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces.Services
{
    public interface IPatientService
    {
        Task<bool> EditPatient(PatientEditDto patient, int id);
        Task<PatientInfoGetDto> GetPatient(int ID);
        Task<bool> AddPatientInsurance(int PatientID, int InsuranceID);
        Task<bool> AddMedicalHistory(PatientMedicalHistoryDto medicalHistoryDto, int PatientID);

    }
}
