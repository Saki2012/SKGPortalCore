using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SKGPortalCore.Data;
using SKGPortalCore.Graph;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SKGPortalCore
{
    public class Startup
    {
        #region Property
        IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        #endregion

        #region Construct
        public Startup(IConfiguration configuration, IWebHostEnvironment env) { Configuration = configuration; Env = env; }
        #endregion

        #region Public
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Namespace))
      );
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            InjectionRepository(ref services);
            InjectionGraphSchema(ref services);
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddDistributedRedisCache(p => p.Configuration = "127.0.0.1:6379");
            services.AddSession(options =>
             {
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                 options.Cookie.Name = "YouKnowDaWaeOfDevil";
                 options.IdleTimeout = TimeSpan.FromMinutes(20);
             });
#if BackEnd
            services.AddSingleton<ISessionWrapper, SessionWapper<BackendUserModel>>();
#else
            services.AddSingleton<ISessionWapper, SessionWapper<CustUserModel>>();
#endif
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets();

            Type[] assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            Type[] schemaTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseSchema`1") == 0 || t.BaseType.Name.CompareTo("BaseSchema`2") == 0 || t.BaseType.Name.CompareTo("BaseSchema`3") == 0).ToArray();

            foreach (var type in schemaTypes)
            {
                typeof(GraphQLWebSocketsExtensions).GetMethods()[0].MakeGenericMethod(type).Invoke(null, new object[] { app, $@"/{type.Name.Replace("Schema", "")}" });
                typeof(GraphQL.Server.ApplicationBuilderExtensions).GetMethods()[0].MakeGenericMethod(type).Invoke(null, new object[] { app, $@"/{type.Name.Replace("Schema", "")}" });
            }


            app.UseStaticFiles();
#if DEBUG
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/", GraphQLEndPoint = "/Bill" });
#endif
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
            Type[] types = assembly.ExportedTypes.Where(p => !p.Namespace.Contains("SKGPortalCore.Business") && p.Namespace.Contains("SKGPortalCore.Repository")).ToArray();
            foreach (Type t in types) services.AddScoped(t);
        }
        /// <summary>
        /// 注入GraphSchema
        /// </summary>
        /// <param name="services"></param>
        private void InjectionGraphSchema(ref IServiceCollection services)
        {
            Type[] assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            //Field
            Type[] fieldTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryFieldGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputFieldGraphType`1") == 0).ToArray();
            foreach (Type t in fieldTypes) services.AddScoped(t);
            //Set
            Type[] setTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0 || t.BaseType.Name.CompareTo("BaseInputSetGraphType`1") == 0).ToArray();
            foreach (Type t in setTypes) services.AddScoped(t);
            //Operate
            Type[] operateTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQueryType`3") == 0 || t.BaseType.Name.CompareTo("BaseMutationType`3") == 0 || t.Name.Contains("Subscription")).ToArray();
            foreach (Type t in operateTypes) services.AddScoped(t);
            //Schema
            Type[] schemaTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseSchema`1") == 0 || t.BaseType.Name.CompareTo("BaseSchema`2") == 0 || t.BaseType.Name.CompareTo("BaseSchema`3") == 0).ToArray();
            foreach (Type t in schemaTypes) services.AddScoped(t);
            //添加Enum
            Type[] enumType = Assembly.Load("SKGPortalCore.Model").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Model.System") == 0 && p.IsEnum).ToArray();
            foreach (Type t in enumType) services.AddScoped(typeof(BaseEnumerationGraphType<>).MakeGenericType(new[] { t }));

            services.AddScoped<Permission>();

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = Env.IsDevelopment(); // expose detailed exceptions in JSON response
                })
               .AddGraphTypes(ServiceLifetime.Scoped)
               .AddUserContextBuilder(httpContext => httpContext.User)
               .AddDataLoader()
               .AddWebSockets();
            services.AddCors();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }
        #endregion
    }
}
