using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{	
	public interface ILogDbWrapper<LogEntity>
	{
        Task<IEnumerable<LogEntity>> FindAllAsync();
        Task<bool> InsertAsync(LogEntity data);   
        Task<bool> DeleteAllAsync();
    }
}
