using Microsoft.EntityFrameworkCore;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

public interface IPlanningContextConfiguration
{
    void Configure(DbContextOptionsBuilder optionsBuilder);
}
