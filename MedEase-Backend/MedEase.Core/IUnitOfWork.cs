using MedEase.Core.Interfaces;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}