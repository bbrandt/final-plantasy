using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Planning;

// Ripped from https://stackoverflow.com/questions/69146423/date-only-cannot-be-mapped-sql-server-2019
/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
    { 
    }
}