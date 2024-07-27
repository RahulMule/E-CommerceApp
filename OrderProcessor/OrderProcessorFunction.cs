using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProcessor.Models;

namespace OrderProcessor
{
    public class OrderProcessorFunction
    {
        private readonly ILogger<OrderProcessorFunction> _logger;
        readonly OrderContext _orderContext;

        public OrderProcessorFunction(ILogger<OrderProcessorFunction> logger, OrderContext context)
		{
			_logger = logger;
            _orderContext = context;

		}

        [Function(nameof(OrderProcessorFunction))]
        public async Task Run(
            [ServiceBusTrigger("OrderProcessorQueue", Connection = "asb")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
			string messageBody = message.Body.ToString();
			
			try
            {
				Order order = JsonConvert.DeserializeObject<Order>(messageBody);
				await _orderContext.AddAsync(order);
				_orderContext.SaveChanges();
				// Complete the message
				await messageActions.CompleteMessageAsync(message);

			}
			catch (Exception ex)
            {
                await messageActions.DeadLetterMessageAsync(
                    message,
                    deadLetterReason:"Processing Error",
                    deadLetterErrorDescription: $"Processing failed due to :{ex.Message}");
            }

          
        }
    }
}
