namespace Lottery.Domain.Models.Database.Entities
{
    public sealed class DrawLog : DbEntityBase
    {
        public DateTime Created { get; set; }

        public IEnumerable<DrawNumber> DrawNumbers { get; set; }
    }
}
