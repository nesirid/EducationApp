using Domain.Common;

namespace Domain.Models
{
    public class Education:BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Group> Groups { get; set; }
    }
}
