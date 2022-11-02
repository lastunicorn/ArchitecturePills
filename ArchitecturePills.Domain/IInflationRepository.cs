using System.Collections.Generic;
using DustInTheWind.ArchitecturePills.Domain;

namespace DustInTheWind.ArchitecturePills.DataAccess
{
    public interface IInflationRepository
    {
        List<Inflation> GetAll();
    }
}