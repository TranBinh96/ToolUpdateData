using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolUpdateData.Jobs
{
    class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            this._logger = logger;
        }
        public static bool Webpage(string url,string mgs)
        {
            using (var client = new WebClient())
            {
                Console.Write("Update "+mgs+"\t");
                client.DownloadString(url);
                Thread.Sleep(1000);
                return true;
            }
        }

        public void UpdateJob()
        {
            string urlSo = "http://localhost:55988/SO/GetSAPOrder";
            string urlCus = "http://localhost:55988/Custormer/GetCustormerOrder";
            string urlStyle = "http://localhost:55988/Style/GetStyleOrder";

            bool check1 = Webpage(urlCus,"Customer");
            bool check2 = Webpage(urlStyle,"Style");
            bool check = Webpage(urlSo,"SO");
            if (check == true && check1 == true && check2 == true)
            {
                Console.WriteLine("Update Successs");
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            UpdateJob();
            _logger.LogInformation($"Data update at {DateTime.Now}");
            
            return Task.CompletedTask;
        }
    }
}
