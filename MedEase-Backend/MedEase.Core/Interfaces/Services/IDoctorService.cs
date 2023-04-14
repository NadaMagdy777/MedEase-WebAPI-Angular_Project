using MedEase.Core.Dtos;

namespace MedEase.EF.Services
{
    public interface IDoctorService
    {
        public  Task<List<DoctorInfoGetDto>> GetAll();
    }
}