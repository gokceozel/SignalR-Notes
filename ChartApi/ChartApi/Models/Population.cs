namespace ChartApi.Models
{
    public enum ECity
    {
        Istanbul = 1,
        Ankara = 2,
        Izmir = 3,
        Konya = 4,
        Bursa = 5
    }
    public class Population
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime ImmigrationDate { get; set; }
    }
}
