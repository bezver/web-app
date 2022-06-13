using Core.DTO;
using Core.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IFilmRepository: IRepository<Film, Guid>
    {
        Task<ServiceResult<Film>> ListWidthCount(Expression<Func<Film, bool>> filterQuery, int skip, int take);
    }
}
