using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RJCP.IO.Ports;
using System.Threading;
using RestSharp;

namespace SmartLock.IoTClient
{
    public class Startup
    {
        public static string ApiUrl { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiUrl = configuration.GetValue<string>("ApiUrl");
            Thread readThread = new Thread(Read);
            Thread writeThread = new Thread(Write);
            Program._serialPort = new SerialPortStream("COM3");
            Program._serialPort.BaudRate = 9600;
            Program._serialPort.ReadTimeout = 1000;
            Program._serialPort.WriteTimeout = 1000;
            Program._serialPort.Open();
            readThread.Start();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public static void Read()
        {
            Console.WriteLine("Read");
            while (true)
            {
                try
                {
                    string command = Program._serialPort.ReadLine();
                    var comArray = command.Split(' ');
                    int id = Convert.ToInt32(comArray[0]);
                    string action = comArray[1];
                    var client = new RestClient(ApiUrl);
                    RestRequest request;
                    if (action == "opened\r")
                    {
                        request = new RestRequest($"api/Locks/{id}/opened", Method.POST);
                    }
                    else if (action == "closed\r")
                    {
                        request = new RestRequest($"api/Locks/{id}/closed", Method.POST);
                    }
                    else
                    {
                        continue;
                    }
                    Program._serialPort.Flush();
                    IRestResponse response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        Console.WriteLine("Api notification successful!");
                    }

                }
                catch (TimeoutException) { }
            }
        }

        public static void Write()
        {
            Console.WriteLine("Write");
            while (true)
            {
                try
                {
                    Program._serialPort.Write("1");
                }
                catch (TimeoutException) { }
            }
        }
    }
}
