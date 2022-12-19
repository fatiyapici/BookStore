using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors;
            List<AuthorsViewModel> authorsList = _mapper.Map<List<AuthorsViewModel>>(authors);
            return authorsList;
        }

    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}