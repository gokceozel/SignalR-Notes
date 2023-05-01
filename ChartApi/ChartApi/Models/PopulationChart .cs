namespace ChartApi.Models
{
    public class PopulationChart
    {
        public PopulationChart()
        {
            Counts = new List<int>();
        }
        public string ImmigrationDate { get; set; }
        public List<int> Counts { get; set; }
    }
}
