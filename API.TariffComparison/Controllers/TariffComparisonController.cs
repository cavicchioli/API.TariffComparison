using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Notification;
using TariffComparison.Application.Responses;

namespace API.TariffComparison.Controllers
{
    [Produces("application/json")]
    [Route("api/tariffs")]
    public class TariffComparisonController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotificationContext _notificationContext;
        public TariffComparisonController(IMediator mediator, INotificationContext notificationContext)
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
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

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
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

            var response = await _mediator.Send(command);
            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
            }

            return Ok(response);
        }
    }
}
