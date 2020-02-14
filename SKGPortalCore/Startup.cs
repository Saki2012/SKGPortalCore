using System;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore
{
    public class Startup
    {
        #region Property
        public IConfiguration Configuration { get; }
        #endregion

        #region Construct
        public Startup(IConfiguration configuration) { Configuration = configuration; }
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
                    //p.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    //p.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }).
            SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDistributedRedisCache(p => p.Configuration = "127.0.0.1:6379");
            services.AddSession(options =>
             {
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                 options.Cookie.Name = "YouKnowDaWaeOfDevil";
                 options.IdleTimeout = TimeSpan.FromMinutes(20);
             });
#if BackEnd
            services.AddSingleton<ISessionWapper, SessionWapper<BackendUserModel>>();
#else
            services.AddSingleton<ISessionWapper, SessionWapper<CustUserModel>>();
#endif
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
               // app.UseDatabaseErrorPage();
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
#if DEBUG
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
#endif
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
        /// 注入Repository
        /// </summary>
        /// <param name="services"></param>
        private void InjectionRepository(ref IServiceCollection services)
        {
            Assembly assembly = Assembly.Load("SKGPortalCore.Repository");
            Type[] types = assembly.ExportedTypes.Where(p => !p.Namespace.Contains("SKGPortalCore.Business") && p.Namespace.CompareTo("SKGPortalCore.Repository") != 0).ToArray();
            foreach (Type t in types)
            {
                services.AddTransient(t);
            }
        }
        /// <summary>
        /// 注入GraphSchema
        /// </summary>
        /// <param name="services"></param>
        private void InjectionGraphSchema(ref IServiceCollection services)
        {
            Type[] assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            Type[] fieldTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryFieldGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputFieldGraphType`1") == 0).ToArray();
            foreach (Type t in fieldTypes)
            {
                services.AddTransient(t);
            }

            Type[] setTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputSetGraphType`1") == 0).ToArray();
            foreach (Type t in setTypes)
            {
                services.AddTransient(t);
            }

            Type[] operateTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryType`2") == 0 || t.BaseType.Name.CompareTo("BaseMutationType`3") == 0).ToArray();
            foreach (Type t in operateTypes)
            {
                services.AddTransient(t);
            }

            ServiceProvider sp = services.BuildServiceProvider();
            Type[] schemaTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseSchema`1") == 0 || t.BaseType.Name.CompareTo("BaseSchema`2") == 0 || t.BaseType.Name.CompareTo("BaseSchema`3") == 0).ToArray();
            foreach (Type t in schemaTypes)
            {
                services.AddSingleton(t, Activator.CreateInstance(t, new FuncDependencyResolver(type => sp.GetService(type))));
            }

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
        }
        #endregion
    }
}
