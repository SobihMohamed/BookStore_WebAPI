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

        public RegisterCustomerCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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
            // middleware will handle the error, so we don't need to handle it here anymore
            //if (!result.Succeeded)
            //{
            //    // هنرمي إيرور دلوقتي، ولما نوصل لـ Task 12 (Global Exception) هنظبط شكل الإيرور ده
            //    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            //    throw new Exception($"Registration failed: {errors}");
            //}

            // 2. إعطاء صلاحية "Customer" للحساب ده
            await _userManager.AddToRoleAsync(user, "Customer");

            // 3. إنشاء ملف العميل (Customer Profile) وربطه بالحساب
            var customer = new Customer
            {
                FullName = request.FullName,
                ApplicationUserId = user.Id
            };

            var customerRepo = _unitOfWork.GetRepository<Customer, int>();
            await customerRepo.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            // 4. token generation logic will be added here in the future (Task 11)
            return new AuthResponse
            {
                FullName = customer.FullName,
                Email = user.Email,
                Token = "Token_Will_Be_Generated_Here_Soon"
            };
        }
    }
}