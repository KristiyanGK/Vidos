using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.DataServices.Extensions;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.DataServices
{
    public class SessionCartService : CartService
    {
        public static ICartService GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext.Session;

            SessionCartService cart = 
                session.GetJson<SessionCartService>(Constants.SessionCartKey)
                                      ?? new SessionCartService();

            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override CartItem AddItem(AirConditioner product, int quantity)
        {
            var item = base.AddItem(product, quantity);
            Session.SetJson(Constants.SessionCartKey, this);
            return item;
        }
        public override void RemoveById(string productId)
        {
            base.RemoveById(productId);
            Session.SetJson(Constants.SessionCartKey, this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove(Constants.SessionCartKey);
        }
    }
}
