using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {

        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private IMoviesService moviesService;
        private ShoppingCart shoppingCart;

        public OrdersController(IMoviesService _moviesService, ShoppingCart _shoppingCart) 
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items= _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems=items;


            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }
    }
}
