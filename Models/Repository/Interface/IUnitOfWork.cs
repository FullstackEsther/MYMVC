using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Repository.Interface
{
    public interface IUnitOfWork
    {
        public Task SaveAsync();
    }
}