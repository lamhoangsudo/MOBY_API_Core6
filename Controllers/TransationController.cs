using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class TransationController : ControllerBase
    {
        private readonly ITransationRepository transationRepository;
        private readonly IUserRepository userRepository;
        public TransationController(ITransationRepository transationRepository, IUserRepository userRepository)
        {

            this.transationRepository = transationRepository;
            this.userRepository = userRepository;
        }


        [Authorize]
        [HttpPut]
        [Route("api/transaction")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionIDVM transactionIDVM)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userRepository.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser != null)
                {
                    if (await transationRepository.UpdateTransationLog(transactionIDVM.TransactionId))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at UpdateTransaction"));
                }

                return BadRequest(ReturnMessage.Create("No User Found"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/transaction/list")]
        public async Task<IActionResult> GetTransaction([FromQuery] TransactionStatusVM transactionStatusVM, [FromQuery] PaggingVM pagging)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userRepository.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser == null)
                {
                    return BadRequest(ReturnMessage.Create("No User Found"));
                }

                List<TransationLogVM> listTransaction = await transationRepository.GetTransationLog(transactionStatusVM.TransactionStatus, pagging);
                int count = await transationRepository.GetTransationLogCount(transactionStatusVM.TransactionStatus);
                PaggingReturnVM<TransationLogVM> result = new PaggingReturnVM<TransationLogVM>(listTransaction, pagging, count);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/transaction")]
        public async Task<IActionResult> GetTransactionByTransactionID([FromQuery] TransactionIDVM transactionIDVM)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userRepository.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser == null)
                {
                    return BadRequest(ReturnMessage.Create("No User Found"));
                }

                TransationLogVM? transaction = await transationRepository.GetTransationLogByID(transactionIDVM.TransactionId);

                return Ok(transaction);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/transaction")]
        public async Task<IActionResult> GetTransactionByUserID([FromQuery] PaggingVM pagging)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userRepository.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser == null)
                {
                    return BadRequest(ReturnMessage.Create("No User Found"));
                }

                List<TransationLogVM> listTransaction = await transationRepository.GetTransationLogByUserID(currentUser.UserId, pagging);
                int count = await transationRepository.GetTransationLogByUserIDCount(currentUser.UserId);
                PaggingReturnVM<TransationLogVM> result = new PaggingReturnVM<TransationLogVM>(listTransaction, pagging, count);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
