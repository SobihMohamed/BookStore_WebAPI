using Application.DTOs.Author;
using Application.Specifications.AuthrSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;

namespace Application.Features.Authors.Queries.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();
            var spec = new ActiveAuthorsSpecification();
            var authors = await authorRepo.GetAllWithSpecAsync(spec);

            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio
            });
        }
    }
}