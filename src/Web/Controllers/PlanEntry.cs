namespace TRS.FinalPlantasy.Web.Controllers
{
    public class PlanEntry
    { 
        public int Id { get; set; }

        public DateOnly EventDate { get; set; }

        public double Amount { get; set; }
    }
}