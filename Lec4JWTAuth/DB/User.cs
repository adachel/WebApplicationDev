namespace Lec4JWTAuth.DB
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[] Password {  get; set; }  // для того, чтобы не хранить пароль в чистом виде
        public byte[] Salt {  get; set; }   // для того, чтобы не хранить пароль в чистом виде
        public RoleId RoleId { get; set; }

        public virtual Role Role {  get; set; }
    }
}
