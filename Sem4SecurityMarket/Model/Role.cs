namespace Sem4SecurityMarket.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string NameRole { get; set; }


        public virtual List<User> Users { get; set; }
    }
}
