using Application.Abstraction.Authentication;
using Application.DTOs.Auth;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider; 

        public RegisterCustomerCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider) 
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }

            var roleName = "Customer";
            await _userManager.AddToRoleAsync(user, roleName);

            var customer = new Customer
            {
                FullName = request.FullName,
                ApplicationUserId = user.Id
            };

            var customerRepo = _unitOfWork.GetRepository<Customer, int>();
            await customerRepo.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtProvider.GenerateToken(user, new List<string> { roleName });

            return new AuthResponse
            {
                FullName = customer.FullName,
                Email = user.Email,
                Token = token 
            };
        }
    }
}