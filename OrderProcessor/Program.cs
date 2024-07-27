using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderProcessor;

var host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		string connstring = Environment.GetEnvironmentVariable("sqlconn");
		services.AddDbContext<OrderContext>(options =>
		{
			options.UseSqlServer(connstring);
		});
	})
	.Build();

host.Run();
