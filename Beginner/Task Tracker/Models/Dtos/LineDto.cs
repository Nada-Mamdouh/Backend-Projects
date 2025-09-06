using Task_Tracker.Models.Enums;

namespace Task_Tracker.Models.Dtos
{
    internal class LineDto
    {
        public Commands CommandType { get; set; }
        public int Id { get; set; }
        public string DESC { get; set; } = string.Empty;
    }
}