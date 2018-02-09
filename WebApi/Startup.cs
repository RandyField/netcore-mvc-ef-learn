using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

//EF
using Microsoft.EntityFrameworkCore;

//使用IConfiguration接口,提供运行时配置设置
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using MODEL;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //连接字符串传给数据库上下文的DbContextOptionsBuilder
            services.AddDbContext<SchoolDbContext>(option => 
                    option.UseMySQL(
                            //读取配置文件appsetting.json获取连接字符串
                            Configuration.GetConnectionString("MysqlDbConnectionString")
                            ));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
