using System.Collections.Generic;
using System.Threading.Tasks;
using TariffComparison.Domain.Models;

namespace TariffComparison.Domain.Interfaces
{
    public interface ITariffRepository
    {
        Task<IEnumerable<Tariff>> GetAllTariffs();
        Task<Tariff> GetTariffById(int id);
    }
}
