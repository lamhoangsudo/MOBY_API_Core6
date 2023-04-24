using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderDAO;
        private readonly IUserRepository userDAO;
        private readonly IEmailRepository emailDAO;
        public OrderController(IOrderRepository orderDAO, IUserRepository userDAO, IEmailRepository emailDAO)
        {
            this.orderDAO = orderDAO;
            this.userDAO = userDAO;
            this.emailDAO = emailDAO;
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/reciever")]
        public async Task<IActionResult> GetOrderByRecieverID([FromQuery] PaggingVM pagging, [FromQuery] OrderStatusVM orderStatusVM)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<OrderBriefVM> listOrder = await orderDAO.GetOrderByRecieverID(uid, pagging, orderStatusVM);
                int total = await orderDAO.GetOrderByRecieverIDCount(uid, orderStatusVM);
                PaggingReturnVM<OrderBriefVM> result = new PaggingReturnVM<OrderBriefVM>(listOrder, pagging, total);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/sharer")]
        public async Task<IActionResult> GetOrderBySharerID([FromQuery] PaggingVM pagging, [FromQuery] OrderStatusVM orderStatusVM)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<OrderBriefVM> listOrder = await orderDAO.GetOrderBySharerID(uid, pagging, orderStatusVM);
                int total = await orderDAO.GetOrderBySharerIDCount(uid, orderStatusVM);
                PaggingReturnVM<OrderBriefVM> result = new PaggingReturnVM<OrderBriefVM>(listOrder, pagging, total);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*[Authorize]
        [HttpGet]
        [Route("api/useraccount/order/sharer/check")]
        public async Task<IActionResult> ChecktOrderBySharerID()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                bool checking = await orderDAO.checkOrderSharer(uid);

                if (checking)
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("there no record change"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/reciever/check")]
        public async Task<IActionResult> ChecktOrderByRecieverID()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                bool checking = await orderDAO.checkOrderReciever(uid);

                if (checking)
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("there no record change"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }*/

        [Authorize]
        [HttpGet]
        [Route("api/order")]
        public async Task<IActionResult> GetOrderByOrderID([FromQuery] OrderidVM orderidVM)
        {
            try
            {

                OrderVM? Order = await orderDAO.GetOrderVMByOrderID(orderidVM.OrderId);

                if (Order == null)
                {
                    return BadRequest(ReturnMessage.Create("order not found"));
                }
                return Ok(Order);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("api/order")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderVM updateOrderVM)
        {
            try
            {
                if (updateOrderVM.Status < 1 && updateOrderVM.Status > 2)
                {
                    return BadRequest(ReturnMessage.Create("unknown function"));
                }
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                Order? currentOrder = await orderDAO.GetOrderByOrderID(updateOrderVM.OrderId);
                if (currentOrder == null)
                {
                    return BadRequest(ReturnMessage.Create("order not found"));
                }


                if (currentOrder.UserId == uid && updateOrderVM.Status == 1)
                {
                    return BadRequest(ReturnMessage.Create("Package status must be from Sharer"));
                }

                if (currentOrder.Item.UserId == uid && currentOrder.Status == 2)
                {
                    return BadRequest(ReturnMessage.Create("Recieve status must be from Reciever"));
                }



                if (await orderDAO.UpdateStatusOrder(currentOrder, updateOrderVM.Status))
                {
                    if (updateOrderVM.Status == 1)
                    {
                        Email newEmail = new Email();
                        newEmail.To = currentOrder.User.UserGmail;
                        newEmail.Subject = "your order has been shipping";
                        newEmail.Body = "your order has been shipping";
                        await emailDAO.SendEmai(newEmail);
                    }
                    else
                    if (updateOrderVM.Status == 2)
                    {
                        Email newEmail = new Email();
                        newEmail.To = currentOrder.Item.User.UserGmail;
                        newEmail.Subject = "your order has been recievied";
                        newEmail.Body = "your order has been recievied";
                        await emailDAO.SendEmai(newEmail);
                    }
                    return Ok(ReturnMessage.Create("success"));
                }

                return BadRequest(ReturnMessage.Create("error at UpdateOrder"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
