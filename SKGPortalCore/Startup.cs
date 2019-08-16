using System;
using System.Linq;
using System.Reflection;
using System.Text;
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
using SKGPortalCore.Data;
using SKGPortalCore.Graph.BillData;
using SKGPortalCore.Graph.MasterData;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.BillData;
using SKGPortalCore.Repository.Func;
using SKGPortalCore.Repository.MasterData;

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
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Namespace))
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

            InjectionRepository(ref services);
            InjectionGraphSchema(ref services);
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
        private void InjectionRepository(ref IServiceCollection services)
        {
            var assembly = Assembly.Load("SKGPortalCore.Repository");
            var types = assembly.ExportedTypes.Where(p => p.Namespace.CompareTo("SKGPortalCore.Repository") != 0).ToArray();
            foreach (var t in types)
                services.AddTransient(t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        private void InjectionGraphSchema(ref IServiceCollection services)
        {
            var assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            var fieldTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryFieldGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputFieldGraphType`1") == 0).ToArray();
            foreach (var t in fieldTypes)
                services.AddTransient(t);
            var setTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputSetGraphType`1") == 0).ToArray();
            foreach (var t in setTypes)
                services.AddTransient(t);
            var operateTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryType`2") == 0 || t.BaseType.Name.CompareTo("BaseMutationType`3") == 0).ToArray();
            foreach (var t in operateTypes)
                services.AddTransient(t);
            var sp = services.BuildServiceProvider();
            var schemaTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseSchema`1") == 0 || t.BaseType.Name.CompareTo("BaseSchema`2") == 0 || t.BaseType.Name.CompareTo("BaseSchema`3") == 0).ToArray();
            foreach (var t in schemaTypes)
                services.AddSingleton(t, Activator.CreateInstance(t, new FuncDependencyResolver(type => sp.GetService(type))));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
        }
        #endregion
    }
}
