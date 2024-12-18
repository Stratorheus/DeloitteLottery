namespace Lottery.Domain.Models.Database.Entities
{
    public abstract class DbEntityBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <remarks>
        /// In case of auditing data collected by many users (only one in this case), I'd rather choose GUID for this entity
        /// </remarks>
        public int Id { get; set; }
    }
}
