﻿using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CartVM
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; } = null!;
        public UserVM? UserVM { get; set; }
        public List<CartDetailVM>? CartDetailList { get; set; }


        public static CartVM CartToVewModel(Cart cart)
        {
            var cartvm = new CartVM
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Address = cart.Address,
            };
            var user = cart.User;
            cartvm.UserVM = UserVM.UserAccountToVewModel(user);
            var ListCartDetail = cart.CartDetails.Select(cd => CartDetailVM.RequestDetailToVewModel(cd)).ToList();
            cartvm.CartDetailList = ListCartDetail;
            return cartvm;
        }

    }
}
