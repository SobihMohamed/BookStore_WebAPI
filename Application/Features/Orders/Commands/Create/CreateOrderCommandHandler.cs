using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            var customerRepo = _unitOfWork.GetRepository<Customer, int>();
            var orderRepo = _unitOfWork.GetRepository<Order, int>();

            var customer = await customerRepo.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {request.CustomerId} was not found.");
            }

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var itemRequest in request.Items)
            {
                var book = await bookRepo.GetByIdAsync(itemRequest.BookId);

                if (book == null || book.IsDeleted)
                {
                    throw new BadRequestException($"Book with ID {itemRequest.BookId} is not available.");
                }

                if (book.Stock < itemRequest.Quantity)
                {
                    throw new BadRequestException($"Not enough stock for book '{book.Title}'. Available: {book.Stock}, Requested: {itemRequest.Quantity}");
                }

                book.Stock -= itemRequest.Quantity;
                bookRepo.Update(book);

                var orderItem = new OrderItem
                {
                    BookId = book.Id,
                    Quantity = itemRequest.Quantity,
                    UnitPriceAtPurchase = book.Price
                };

                orderItems.Add(orderItem);
                totalAmount += (itemRequest.Quantity * book.Price);
            }

            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            await orderRepo.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return order.Id;
        }
    }
}