using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Responses;

namespace API.TariffComparison.Controllers
{
    [Produces("application/json")]
    [Route("api/tariffs")]
    public class TariffComparisonController : Controller
    {
        private readonly IMediator _mediator;
        public TariffComparisonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// List all Electricity Tariffs
        /// </summary>
        [HttpGet(Name="ListAllTariffs")]
        [ProducesResponseType(typeof(IEnumerable<TariffResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ListTariffsEvent());
            if (!response.Any())
            {
                return BadRequest();
            }

            return Ok(response);
        }

        /// <summary>
        /// Get an Electricity Tariff by Id
        /// </summary>
        [HttpGet("{id}", Name = "GetTariffById")]
        [ProducesResponseType(typeof(TariffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByCode([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetTariffByIdEvent() { Id = id });
            if (response==null)
            {
                return BadRequest();

            }
            return Ok(response);
        }
        /// <summary>
        /// Calculate electricity total cost based in your consume vs tariffs database and show the cheapest price.
        /// </summary>
        [HttpPost("compare", Name = "CompareCosts")]
        [ProducesResponseType(typeof(IEnumerable<CalculatedCostResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody]ListCostsByGivenKwhConsumptionEvent command)
        {
            if (command.AnnualKwhConsumption < 1)
            {
                return BadRequest("<AnnualKwhConsumption> Must Be Greater Than Zero");
            }

            var response = await _mediator.Send(command);
            if (!response.Any())
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
