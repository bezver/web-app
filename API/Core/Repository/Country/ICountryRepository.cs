using Core.Models;
using System;

namespace Core.Repository
{
    public interface ICountryRepository: IRepository<Country, Guid>
    {
    }
}
