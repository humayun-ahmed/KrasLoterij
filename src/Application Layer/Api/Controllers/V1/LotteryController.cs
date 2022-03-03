using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NederlandseLoterij.KrasLoterij.Service.Contracts;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Api.Controllers.V1
{
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryQueryService m_lotteryQueryService;
        private readonly ILotteryService m_lotteryService;
        private readonly IValidator<ScratchCommand> m_validator;

        public LotteryController(ILotteryQueryService lotteryQueryService, ILotteryService lotteryService, IValidator<ScratchCommand> validator)
        {
            m_lotteryQueryService = lotteryQueryService;
            m_lotteryService = lotteryService;
            m_validator = validator;
        }

        [HttpGet]
        [Route(ApiRoutes.Get)]
        public async Task<IActionResult> Get()
        {
            var lotteries =await m_lotteryQueryService.GetAllLotteriesAsync();
            return Ok(lotteries);
        }

        [HttpGet]
        [Route(ApiRoutes.IsScratchedByUser)]
        public async Task<IActionResult> IsScratchedByUserAsync(Guid userId)
        {
            var isScratchedByUser = await m_lotteryQueryService.IsScratchedByUserAsync(userId);
            return Ok(isScratchedByUser);
        }

        [HttpPut]
        [Route(ApiRoutes.Scratch)]
        public async Task<IActionResult> Put([FromBody] ScratchCommand lotteryCommand)
        {
            //ToDo idempotency need to implement for concurrency
            try
            {
                var validationResult = await m_validator.ValidateAsync(lotteryCommand);

                if (validationResult.IsValid)
                {
                    await m_lotteryService.ScratchLotteryAsync(lotteryCommand);
                    return this.Ok();
                }
                else
                {
                    return BadRequest(validationResult.Errors);
                }
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(new ValidationResult(  new List<ValidationFailure>{ new ( @"DataValidation", ex.Message)}));
            }
        }
    }
}