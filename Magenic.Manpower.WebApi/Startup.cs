using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Magenic.Manpower.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;
using Magenic.Manpower.WebApi.ServiceLogic;
using Magenic.Manpower.WebApi.Services.Repository;
using AutoMapper;
using Magenic.Manpower.WebApi.Email;

namespace Magenic.Manpower.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            CurrentEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        private IHostingEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //swagger comments
            string swaggerCommentXmlPath = string.Empty;
            if (CurrentEnvironment.IsDevelopment()) //while development
                swaggerCommentXmlPath = $@"{CurrentEnvironment.ContentRootPath}\bin\Debug\netcoreapp1.0\Magenic.Manpower.WebApi.xml";
            else //while production
                swaggerCommentXmlPath = $@"{CurrentEnvironment.ContentRootPath}\Magenic.Manpower.WebApi.xml";


            services.AddCors(o => { o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
            // Add framework services.
            services.AddMvc().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            // Add automapper services
            services.AddAutoMapper();
            services.AddDbContext<MagenicManpowerDBContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Magenic Manpower API",
                    Description = "These are the API endpoints of Magenic Manpower",
                    TermsOfService = "NA",
                    Contact = new Contact() { Name = "Magenic Manila Inc.", Email = "myemail@test.com", Url = "https://magenic.com/" }
                });
                options.IncludeXmlComments(swaggerCommentXmlPath); //Includes XML comment file
            });

            #region Email-Related
            // for getting email settings from config
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailTemplateFactory, EmailTemplateFactory>();
            services.AddScoped<IEmailComposer, EmailMessageComposer>();
            services.AddScoped<IEmailClient, SmtpClient>();
            #endregion

            //Domain Specific
            services.AddSingleton<IAuthenticationSvc, AuthenticationCustomSvc>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<ILookupRepository, LookupRepository>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserContextRepository, UserContextRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRequestSvc, RequestSvc>();
            services.AddTransient<IRequestContextRepository, RequestContextRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IReferenceNumberRepository, ReferenceNumberRepository>();
            services.AddTransient<IRequestTechnologyRepository, RequestTechnologyRepository>();
            services.AddScoped<IPrimarySkillRepository, PrimarySkillRepository>();
            services.AddScoped<IPrimarySkillService, PrimarySkillService>();
            services.AddScoped<ITechnologyDetailRepository, TechnologyDetailRepository>();
            services.AddScoped<ITechnologyDetailService, TechnologyDetailService>();
            services.AddScoped<IApplicantLevelService, ApplicantLevelService>(); 
            services.AddScoped<IApplicantLevelRepository, ApplicantLevelRepository>(); 
            services.AddScoped<IProjectManagementRepository, ProjectManagementRepository>();
            services.AddScoped<IProjectManagementService, ProjectManagementService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHrDashboardRepository, HrDashboardRepository>();
            services.AddScoped<IHrDashboardSvc, HrDashboardSvc>();
            services.AddScoped<IApplicantsRepository, ApplicantsRepository>();
            services.AddScoped<IApplicantsService, ApplicantsService>();
            services.AddTransient<ITaggableRepository, TaggableRepository>();
            services.AddTransient<ITaggedApplicantRepository, TaggedApplicantRepository>();
            services.AddScoped<IApplicantPoolSvc, ApplicantPoolSvc>();
            services.AddScoped<ICmDashboardRepository, CmDashboardRepository>();
            services.AddScoped<ICmDashboardService, CMDashboardService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
