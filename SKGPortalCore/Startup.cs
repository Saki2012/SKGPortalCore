using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using SKGPortalCore.Controllers.BillData;
using SKGPortalCore.Controllers.Func;
using SKGPortalCore.Controllers.MasterData;
using SKGPortalCore.Data;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Text;

namespace SKGPortalCore
{
    public class Startup
    {
        #region Property & Construct

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        #endregion
        #region Public
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"))
               );

            services.AddMvc().
                AddJsonOptions(p =>
                {
                    p.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    p.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }).
            SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "YouKnowDaWaeOfDevil";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            //if(BackEnd)
            //services.AddSingleton<ISessionWapper, SessionWapper<BackendUserModel>>();
            //if(FrontEnd
            services.AddSingleton<ISessionWapper, SessionWapper<CustUserModel>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            SetGraphQLService(ref services);
            SetRepository(ref services);
            SetGraphQLType(ref services);
            SetGraphQLSchema(ref services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #endregion
        #region Private
        /// <summary>
        /// 設置Repository
        /// </summary>
        /// <param name="services"></param>
        private void SetRepository(ref IServiceCollection services)
        {
            services.AddTransient<BillRepository>();
            services.AddTransient<AccountRepository>();
            services.AddTransient<RoleRepository>();
            services.AddTransient<CustomerRepository>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        private void SetGraphQLType(ref IServiceCollection services)
        {
            services.AddTransient<BillQuery>();
            services.AddTransient<BillMutation>();
            services.AddTransient<BillType>();
            services.AddTransient<BillInputType>();

            services.AddTransient<RoleQuery>();
            services.AddTransient<RoleMutation>();
            services.AddTransient<RoleType>();
            services.AddTransient<RoleInputType>();
            services.AddTransient<RolePermissionInputType>();
            services.AddTransient<RolePermissionType>();
            services.AddTransient<RoleSetType>();

            services.AddTransient<EnumerationGraphType<PayStatus>>();
            services.AddTransient<EnumerationGraphType<FuncAction>>();
            services.AddTransient<EnumerationGraphType<EndType>>();

            services.AddTransient<CustomerQuery>();
            services.AddTransient<CustomerMutation>();
            services.AddTransient<CustomerInputType>();
            services.AddTransient<CustomerSetType>();
            services.AddTransient<CustomerType>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        private void SetGraphQLSchema(ref IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            services.AddSingleton(new BillSchema(new FuncDependencyResolver(type => sp.GetService(type))));
            services.AddSingleton(new RoleSchema(new FuncDependencyResolver(type => sp.GetService(type))));
            services.AddSingleton(new CustomerSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }
        /// <summary>
        /// 設置GraphQLService
        /// </summary>
        /// <param name="services"></param>
        private void SetGraphQLService(ref IServiceCollection services)
        {
            //services.AddSingleton<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
        }
        #endregion
    }
}
