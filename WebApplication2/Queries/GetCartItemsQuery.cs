using MediatR;
using WebApplication2.Models;

namespace WebApplication2.Queries
{
    public record GetCartItemsQuery : IRequest<IEnumerable<CartItem>>;

}






























//public class GetCartItemsQuery : IRequest<IEnumerable<CartItem>>
//{
//    public Guid Id { get; set; }
//    public class GetAllCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, IEnumerable<CartItem>>
//    {
//        private readonly IAppDataContext _context;
//        public GetAllCartItemsQueryHandler(IAppDataContext context)
//        {
//            _context = context;
//        }


//        public async Task<IEnumerable<CartItem>> Handle(GetCartItemsQuery request, CancellationToken cancellation)
//        {
//            var query = "SELECT * FROM CartItems";

//            using var connection = _context.CreateConnection();
//            var items = await connection.QueryAsync<CartItem>(query);
//            return items.ToList();
//        }
//    }
//}