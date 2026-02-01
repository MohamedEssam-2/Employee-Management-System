namespace Demo_DAL.Models.Shared
{
    public class BaseEntity//include comnmon properties
    {
        public int Id { get; set; }
        public int Created_By { get; set; }//user id

        public DateTime Created_On { get; set; }
        public int Modified_By { get; set; }
        public DateTime Modified_On { get; set; }
        public DateTime Last_Mdifed_On { get; set; }
        public int Last_Modified_By { get; set; }
        public bool IsDeleted { get; set; }
    }
}
