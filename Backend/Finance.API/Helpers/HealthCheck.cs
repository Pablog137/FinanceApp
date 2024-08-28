using Finance.API.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace Finance.API.Helpers
{
    public class HealthCheck : IHealthCheck
    {

        private readonly AppDbContext _context;

        public HealthCheck(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                if (canConnect)
                {
                    return HealthCheckResult.Healthy("API is working and DB connection is successful.");
                }
                else
                {
                    return HealthCheckResult.Unhealthy("API is working but DB connection failed.");
                }

            }
            catch (Exception e)
            {
                Log.Error(e, "Error checking health");
                return HealthCheckResult.Unhealthy("API is not working correctly.", e);
            }
        }
    }
}
