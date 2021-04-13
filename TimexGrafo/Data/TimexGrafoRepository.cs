using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexGrafo.Models;

namespace TimexGrafo.Data
{
    public interface ITimexGrafoRepository
    {
        void AddEntity(object model);
        Cart GetCartById(int iD);
        IList<CartItem> GetCartItemByCartId(int id);
        CartItem GetCartItemById(int cartItemId);
        CartItem GetCartItemByProductId(int productId);
        ShopItem GetProductByCartItemId(int cartItemId);
        Item GetProductById(int id);

        ShopItem GetShopProductById(int id);
        IEnumerable<Item> GetProducts();
        bool IsThereSameProductInTheCart(int id, int productId);
        void RemoveCart(Cart cart);
        void RemoveCartItem(CartItem cartItem);
        List<CartItem> getCartItems();
        void RemoveCartItems(IList<CartItem> cartItem);
        void RemoveEntity(object model);
        bool SaveAll();
        void UpdateCartWithNewCartItem(Cart model);
        void UpdateEntity(object model);

        Cart GetCartByUserId(string Id);
    }

    public class TimexGrafoRepository : ITimexGrafoRepository
    {

        private readonly ApplicationDbContext _context;

        public TimexGrafoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetProducts()
        {
            return _context.Items
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Item GetProductById(int id)
        {
            return _context.Items.SingleOrDefault(p => p.Id == id);
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public void RemoveEntity(object model)
        {
            _context.Remove(model);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        //public Cart GetCartByUserId(string id)
        //{
        //    if (_context.Carts.SingleOrDefault(u => u.User.Id == id) == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {

        //        var cart = _context.Carts.SingleOrDefault(u => u.User.Id == id);
        //        //var cart = _userManager.FindByIdAsync(id).Result.Cart;
        //        var cartItem = _context.CartItems.Where(c => c.CartId == cart.Id).ToList();
        //        //var cartItem = _userManager.FindByIdAsync(id).Result.Cart.CartItem;
        //        cart.CartItem = cartItem;
        //        return cart;
        //    }
        //}

        public void UpdateEntity(object model)
        {
            _context.Update(model);
        }

        public void UpdateCartWithNewCartItem(Cart model)
        {

            _context.Update(model);
        }

        public bool IsThereSameProductInTheCart(int id, int productId)
        {
            if (_context.CartItems.Where(c => c.CartId == id).Any())
            {

                if (_context.CartItems.Where(c => c.CartId == id).Where(p => p.Item.Id == productId).Any())
                {
                    if (_context.CartItems.Where(c => c.CartId == id).Where(p => p.Item.Id == productId).SingleOrDefault(o => o.Item.Id == productId).Item.Id == productId)
                        return true;

                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public IList<CartItem> GetCartItemByCartId(int id)
        {

            //var cartItem = _context.CartItems.Where(c => c.CartId == id).Include(p=>p.Product).Include(s=>s.Size).ToList();

            var cartItem = _context.CartItems.Where(c => c.CartId == id)
                .Include(p => p.Item)
                .ToList();

            return cartItem;
        }

        public void RemoveCartItems(IList<CartItem> cartItem)
        {
            _context.RemoveRange(cartItem);
        }

        public void RemoveCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public CartItem GetCartItemByProductId(int productId)
        {
            return _context.CartItems.SingleOrDefault(p => p.Item.Id == productId);
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }

        public CartItem GetCartItemById(int cartItemId)
        {
            return _context.CartItems
                .Include(p => p.Item)
                .SingleOrDefault(ca => ca.Id == cartItemId);
        }

        public ShopItem GetProductByCartItemId(int cartItemId)
        {
            return _context.CartItems
                .Include(p => p.Item)
                .SingleOrDefault(c => c.Id == cartItemId).Item;
        }

        public Cart GetCartById(int iD)
        {
            return _context.Carts.SingleOrDefault(c => c.Id == iD);
        }

        public Cart GetCartByUserId(string Id)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.UserId == Id);

            return cart;
        }

        public List<CartItem> getCartItems()
        {
            var cartItems = _context.CartItems.ToList();
            return cartItems;
        }
        public ShopItem GetShopProductById(int id)
        {
            return _context.ShopItems.SingleOrDefault(p => p.Id == id);
        }
    }


}
