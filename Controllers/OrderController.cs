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
        public OrderController(IOrderRepository orderDAO, IUserRepository userDAO)
        {
            this.orderDAO = orderDAO;
            this.userDAO = userDAO;
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/reciever")]
        public async Task<IActionResult> GetOrderByRecieverID([FromQuery] PaggingVM pagging, [FromQuery] OrderStatusVM orderStatusVM)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/sharer/check")]
        public async Task<IActionResult> ChecktOrderBySharerID()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
        }

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
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                Order? currentOrder = await orderDAO.GetOrderByOrderID(updateOrderVM.OrderId);
                if (currentOrder == null)
                {
                    return BadRequest(ReturnMessage.Create("order not found"));
                }


                if (currentOrder.UserId == uid && updateOrderVM.Status == 1)
                {
                    return BadRequest(ReturnMessage.Create("Package status must be from Sharer"));
                }

                if (currentOrder.OrderDetails.First().Item.UserId == uid && currentOrder.Status == 2)
                {
                    return BadRequest(ReturnMessage.Create("Recieve status must be from Reciever"));
                }



                if (await orderDAO.UpdateStatusOrder(currentOrder, updateOrderVM.Status))
                {
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
