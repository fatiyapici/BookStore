using WebApi.DbOperations;

namespace WebApi.Applications.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public UpdateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is null)
                throw new InvalidOperationException("Guncellenecek kullanici bulunamadi.");

            user.Email = Model.Email != default ? Model.Email : user.Email;
            user.Password = Model.Password != default ? Model.Password : user.Password;

            _context.SaveChanges();
        }
        public class UpdateUserModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}