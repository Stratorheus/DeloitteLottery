namespace Lottery.Domain.Models.Database.Entities
{
    public abstract class DbEntityBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
