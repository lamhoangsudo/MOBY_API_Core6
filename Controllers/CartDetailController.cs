﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly IUserRepository userDAO;

        private readonly ICartDetailRepository cartDetailDAO;

        private readonly IEmailRepository emailDAO;
        public CartDetailController(ICartDetailRepository cartDetailDAO, IUserRepository userDAO, IEmailRepository emailDAO)
        {
            this.userDAO = userDAO;
            this.cartDetailDAO = cartDetailDAO;


            this.emailDAO = emailDAO;
        }

        [Authorize]
        [HttpGet]
        [Route("api/cartdetail")]
        public async Task<IActionResult> GetListCartDetailByListCartDetailid([FromQuery] int[] listOfIds)
        {

            if (listOfIds == null)
            {
                return BadRequest(ReturnMessage.Create("there no CartDetailid"));
            }
            try
            {

                List<CartDetailVM> result = await cartDetailDAO.GetListCartDetailByListID(listOfIds);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(ReturnMessage.Create("error at GetListCartDetailByListCartDetailid"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/cartdetail/create")]
        public async Task<IActionResult> CreateCartDetail([FromBody] CreateCartDetailVM createdRequestDetail)
        {

            try
            {
                var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (currentUid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                String check = await cartDetailDAO.CheclExistCartDetail(createdRequestDetail);
                if (check.Equals("succes"))
                {
                    return Ok(ReturnMessage.Create("Success"));
                }
                else if (check.Equals("cant not add more than share ammout"))
                {
                    return BadRequest(ReturnMessage.Create("cant not add more than share ammout"));
                }


                String result = await cartDetailDAO.CreateCartDetail(createdRequestDetail, currentUid);
                if (result.Equals("success"))
                {
                    return Ok(ReturnMessage.Create(result));
                }
                return BadRequest(ReturnMessage.Create(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("api/cartdetail")]
        public async Task<IActionResult> UpdateCartDetail([FromBody] UpdateCartDetailVM updatedCartDetail)
        {
            try
            {

                CartDetail? currentCartDetail = await cartDetailDAO.GetCartDetailByCartDetailID(updatedCartDetail.CartDetailId);
                if (currentCartDetail != null)
                {
                    if (updatedCartDetail.ItemQuantity <= 0)
                    {
                        if (await cartDetailDAO.DeleteCartDetail(currentCartDetail))
                        {
                            return Ok(ReturnMessage.Create("Success"));
                        }
                    }
                    String result = await cartDetailDAO.UpdateCartDetail(currentCartDetail, updatedCartDetail);
                    if (result.Equals("success"))
                    {
                        return Ok(ReturnMessage.Create("Success"));
                    }
                    else if (result.Contains("item available ammout"))
                    {
                        return Ok(ReturnMessage.Create(result));
                    }
                    else
                    {
                        return BadRequest(ReturnMessage.Create("error at UpdateRequestDetail"));
                    }

                }
                return BadRequest(ReturnMessage.Create("error at UpdateRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/cartdetail")]
        public async Task<IActionResult> DeleteCartDetail([FromQuery] CartDetailIdVM cartDetailid)
        {
            try
            {
                CartDetail? requestDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailid.CartDetailid);
                if (requestDetail != null)
                {
                    if (await cartDetailDAO.DeleteCartDetail(requestDetail))
                    {
                        return Ok(ReturnMessage.Create("Success"));
                    }
                }
                return BadRequest(ReturnMessage.Create("error at DeleteRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/cartdetail/checking")]
        public async Task<IActionResult> CheckingCartDetail([FromQuery] int[] listCartDetailID)
        {

            try
            {
                var uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (listCartDetailID == null || listCartDetailID.Length == 0)
                {
                    return BadRequest(ReturnMessage.Create("there no cart Detail to confirm"));
                }
                String result = await cartDetailDAO.CheckCartDetail(listCartDetailID, uid);

                if (result.Equals("success"))
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/cartdetail/confirm")]
        public async Task<IActionResult> ConfirmCartDetail([FromBody] ListCartDetailidToConfirm listCartDetailID)
        {

            try
            {
                var user = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (user == null)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (listCartDetailID.listCartDetailID == null || listCartDetailID.listCartDetailID.Count == 0)
                {
                    return BadRequest(ReturnMessage.Create("there no cart Detail to confirm"));
                }

                if (await cartDetailDAO.ConfirmCartDetail(listCartDetailID, user.UserId))
                {
                    Email newEmail = new Email();
                    newEmail.To = user.UserGmail;
                    newEmail.Subject = "your order has been shipping";
                    newEmail.Body = "your order has been shipping";
                    await emailDAO.SendEmai(newEmail);
                    return Ok(ReturnMessage.Create("Success"));
                }
                return BadRequest(ReturnMessage.Create("error at ConfirmCartDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
