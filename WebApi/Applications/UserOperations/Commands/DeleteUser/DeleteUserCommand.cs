using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public DeleteUserModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is null)
                throw new InvalidOperationException("Silinecek kullanici bulunamadi.");
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
    public class DeleteUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}