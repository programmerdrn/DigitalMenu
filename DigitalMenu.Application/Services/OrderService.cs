using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMenu.Application.DTOs.OrderDTOs;
using DigitalMenu.Application.Interfaces;
using DigitalMenu.Core.Entities;
using DigitalMenu.Core.Enums;
using DigitalMenu.Core.Interfaces;

namespace DigitalMenu.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(int tableNumber, List<OrderItemDto> orderItems)
        {
            // محاسبه جمع کل
            decimal totalAmount = orderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

            // ایجاد سفارش
            var order = new Order
            {
                TableNumber = tableNumber,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending
            };

            // اضافه کردن آیتم‌ها
            foreach (var itemDto in orderItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice
                };
                order.OrderItems.Add(orderItem);
            }

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetPendingOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatus.Pending);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return false;

            order.Status = status;
            _orderRepository.Update(order);
            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetOrdersByTableAsync(int tableNumber)
        {
            var orders = await _orderRepository.GetOrdersByTableAsync(tableNumber);
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}