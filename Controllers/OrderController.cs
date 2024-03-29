﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderDAO;
        private readonly IUserService userDAO;
        private readonly IEmailService emailDAO;
        private readonly Logger4Net _logger4Net;
        public OrderController(IOrderService orderDAO, IUserService userDAO, IEmailService emailDAO)
        {
            this.orderDAO = orderDAO;
            this.userDAO = userDAO;
            this.emailDAO = emailDAO;
            _logger4Net = new Logger4Net();
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/order/reciever")]
        public async Task<IActionResult> GetOrderByRecieverID([FromQuery] PaggingVM pagging, [FromQuery] OrderStatusVM orderStatusVM)
        {
            try
            {
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                List<OrderBriefVM> listOrder = await orderDAO.GetOrderByRecieverID(uid, pagging, orderStatusVM);
                int total = await orderDAO.GetOrderByRecieverIDCount(uid, orderStatusVM);
                PaggingReturnVM<OrderBriefVM> result = new PaggingReturnVM<OrderBriefVM>(listOrder, pagging, total);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                await orderDAO.CheckOrderReceivedDate(uid);
                List<OrderBriefVM> listOrder = await orderDAO.GetOrderBySharerID(uid, pagging, orderStatusVM);
                int total = await orderDAO.GetOrderBySharerIDCount(uid, orderStatusVM);
                PaggingReturnVM<OrderBriefVM> result = new PaggingReturnVM<OrderBriefVM>(listOrder, pagging, total);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                int uid = await userService.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
                int uid = await userService.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
                _logger4Net.Loggers(ex);
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
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
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
                        Email newEmail = new()
                        {
                            To = currentOrder.User.UserGmail,
                            UserName = currentOrder.User.UserName,
                            Subject = "Đơn hàng của bạn đang được giao",
                            Obj = "đơn hàng của bạn đang được giao",
                            Link = "https://moby-customer.vercel.app/account/order/order/" + currentOrder.OrderId + ""
                        };
                        await emailDAO.SendEmai(newEmail);
                    }
                    else
                    if (updateOrderVM.Status == 2)
                    {
                        Email newEmail = new()
                        {
                            To = currentOrder.Item.User.UserGmail,
                            UserName = currentOrder.Item.User.UserName,
                            Subject = "Đơn hàng của bạn đang được nhận thành công",
                            Obj = "đơn hàng của bạn đang được nhận thành công",
                            Link = "https://moby-customer.vercel.app/account/order/order/" + currentOrder.OrderId + ""
                        };
                        await emailDAO.SendEmai(newEmail);
                    }
                    return Ok(ReturnMessage.Create("success"));
                }

                return BadRequest(ReturnMessage.Create("error at UpdateOrder"));


            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/order/cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderVM cancelOrdervm)
        {
            try
            {
                if (cancelOrdervm.ReasonCancel == null || cancelOrdervm.ReasonCancel.Equals(""))
                {
                    return BadRequest(ReturnMessage.Create("must have reseaon to cancel"));
                }
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                Order? currentOrder = await orderDAO.GetOrderByOrderID(cancelOrdervm.OrderId);
                if (currentOrder == null)
                {
                    return BadRequest(ReturnMessage.Create("order not found"));
                }
                bool pernament = true;
                TimeSpan totalDays = DateTime.Now - currentOrder.DateCreate;
                int totalDaysint = Convert.ToInt32(totalDays.TotalDays);
                if (totalDaysint >= 7)
                {
                    pernament = false;
                }
                if (await orderDAO.CancelOrder(currentOrder, cancelOrdervm.ReasonCancel, uid, pernament))
                {
                    Email newEmail = new()
                    {
                        To = currentOrder.Item.User.UserGmail,
                        UserName = currentOrder.Item.User.UserName,
                        Subject = "Đơn hàng đã bị hủy",
                        Obj = "đơn hàng của bạn đã bị hủy",
                        Link = "https://moby-customer.vercel.app/account/order/order/" + currentOrder.OrderId + ""
                    };
                    await emailDAO.SendEmai(newEmail);
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at cancelOrder"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
