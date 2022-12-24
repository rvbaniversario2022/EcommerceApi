using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public Guid OrderId { get; set; }
        public Status Status { get; set; }
    }
}
