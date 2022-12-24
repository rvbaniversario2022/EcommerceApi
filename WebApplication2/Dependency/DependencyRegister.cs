using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Dependency
{
    public class DependencyRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartItemRepository>().As<ICartItemRepository>().SingleInstance();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
        
            builder.RegisterMediatR(typeof(CartItemRepository).Assembly);
            builder.RegisterMediatR(typeof(OrderRepository).Assembly);
            builder.RegisterMediatR(typeof(UserRepository).Assembly);
        }
    }
}
