using Application.Abstraction.Authentication;
using Application.DTOs.Auth;
using Application.Specifications.CustomerSpec;
using Domain.Contracts.UnitOfWorkPattern; 
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork; 

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IJwtProvider jwtProvider,
            IUnitOfWork unitOfWork) 
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedException();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtProvider.GenerateToken(user, roles);

            var customerRepo = _unitOfWork.GetRepository<Customer, int>();

            var spec = new CustomerByApplicationUserIdSpecification(user.Id);
            var customer = await customerRepo.GetByIdWithSpecAsync(spec);

            return new AuthResponse
            {
                Email = user.Email!,
                Token = token,
                FullName = customer?.FullName ?? user.UserName ?? "System User"
            };
        }
    }
}