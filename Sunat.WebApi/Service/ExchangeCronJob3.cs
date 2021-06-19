using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sunat.WebApi.Service
{
    public class ExchangeCronJob3: CronJobService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ExchangeCronJob3(IScheduleConfig<ExchangeCronJob3> config)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Trace("CronJob 3 starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            Logger.Trace($"{DateTime.Now:hh:mm:ss} CronJob 3 is working.");
            try
            {

                Logger.Trace("Final task::: completed");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
            await Task.CompletedTask;
            Logger.Trace("Final task::: completed");
        }



        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.Trace("CronJob 3 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
