using MediatR;
using WebApplication2.Dto;
using WebApplication2.Enums;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class UpdateCartItemCommand : IRequest<CartItem>
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
    }
}






































////public class UpdateCartItemCommand : IRequest<Guid>
////{
////    public Guid Id { get; set; }
////    public string? Name { get; set; }
////    public double Price { get; set; }
////    //public Guid UserId { get; set; }

////    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Guid>
////    {
////        private readonly IAppDataContext _context;
////        public UpdateCartItemCommandHandler(IAppDataContext context)
////        {
////            _context = context;
////        }
////        public async Task<Guid> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
////        {
////            var item = _context.CartItems.Where(x => x.Id == command.Id).FirstOrDefault();

////            if (item == null)
////            {
////                return default;
////            }
////            else
////            {
////                item.Id = command.Id;
////                item.Name = command.Name;
////                item.Price = command.Price;
////                //item.UserId = command.UserId;

////                await _context.SaveChangesAsync();

////                return item.Id;
////            }
////        }
////    }
////}