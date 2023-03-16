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
        public async Task<IActionResult> GetOrderByRecieverID([FromQuery] PaggingVM pagging)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<Order> listOrder = await orderDAO.GetOrderByRecieverID(uid);



                return Ok(listOrder);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/sharer")]
        public async Task<IActionResult> GetOrderBySharerID([FromQuery] PaggingVM pagging)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<Order> listOrder = await orderDAO.GetOrderBySharerID(uid);



                return Ok(listOrder);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/useraccount/order/sharer")]
        public async Task<IActionResult> UpdateOrder([FromQuery] PaggingVM pagging, int orderID, int status)
        {
            try
            {
                Order? currentOrder = await orderDAO.GetOrderByOrderID(orderID);
                if (currentOrder == null)
                {
                    return BadRequest(ReturnMessage.create("oc cac"));
                }
                if (await orderDAO.UpdateStatusOrder(currentOrder, status))
                {
                    return Ok(ReturnMessage.create("success"));
                }

                return BadRequest(ReturnMessage.create("oc cac x2"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
