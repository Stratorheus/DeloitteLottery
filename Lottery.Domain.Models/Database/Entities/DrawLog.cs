namespace Lottery.Domain.Models.Database.Entities
{
    public sealed class DrawLog : DbEntityBase
    {
        /// <summary>
        /// When the log was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Stores the drawn numbers in JSON format.
        /// </summary>
        /// <remarks>
        /// For this simple case study, the numbers are stored as JSON. 
        /// In a more complex scenario, an N:M relation might be considered.
        /// Using five fixed columns for the numbers is avoided due to its lack of flexibility.
        /// </remarks>
        public required string Numbers { get; set; }
    }
}
