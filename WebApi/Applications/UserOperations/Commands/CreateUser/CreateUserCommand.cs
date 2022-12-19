using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.UserOperations.Commands
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, Model.Email));
            if (user is not null)
                throw new InvalidOperationException("Kullanici adi zaten mevcut.");
            user = _mapper.Map<User>(Model);
            user.RefreshToken = "";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}