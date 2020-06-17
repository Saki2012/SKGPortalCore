using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SKGPortalCore.Core;
using SKGPortalCore.Core.DB;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Interface;

namespace SKGPortalCore
{
    public class Startup
    {
        #region Property
        IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        /// <summary>
        /// Session過期時間
        /// </summary>
        private const int IdleTimeout = 20;
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
      options.UseSqlServer(Configuration.GetConnectionString(SystemCP.SqlConnection), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Namespace))
      );
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            DependencyInjection(ref services);

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });


            services.AddSession(options =>
             {
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                 options.Cookie.Name = SystemCP.CookieName;
                 options.IdleTimeout = TimeSpan.FromMinutes(IdleTimeout);
             });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
#if BackEnd
            services.AddSingleton<ISessionWrapper, SessionWapper<BackendUserModel>>();
#else
            //services.AddSingleton<ISessionWapper, SessionWapper<CustUserModel>>();
#endif
            //services.AddDistributedRedisCache(p => p.Configuration = Configuration.GetConnectionString(SystemCP.RedisConnection));
            services.AddDistributedMemoryCache();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app)
        {
            //app.UseForwardedHeaders();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets();
            List<Type> assembly = ITF.IGraphQL;
            foreach (var type in assembly)
            {
                typeof(GraphQLWebSocketsExtensions).GetMethods().FirstOrDefault().MakeGenericMethod(type).Invoke(null, new object[] { app, $@"/{type.Name.Substring(1).Replace("Schema", "", StringComparison.Ordinal)}" });
                typeof(GraphQL.Server.ApplicationBuilderExtensions).GetMethods().FirstOrDefault().MakeGenericMethod(type).Invoke(null, new object[] { app, $@"/{type.Name.Substring(1).Replace("Schema", "", StringComparison.Ordinal)}" });
            }
            app.UseStaticFiles();
#if DEBUG
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/", GraphQLEndPoint = "/Bill" });
#endif
        }
        #endregion

        #region Private
        /// <summary>
        /// 注入相依性注入服務
        /// </summary>
        /// <param name="services"></param>
        private void DependencyInjection(ref IServiceCollection services)
        {
            List<Type> repo = Assembly.LoadFrom(Configuration.GetSection(SystemCP.RepositoryDLL).Value).GetTypes().ToList();
            List<Type> graph = Assembly.LoadFrom(Configuration.GetSection(SystemCP.GraphDLL).Value).GetTypes().ToList();
            Injectionimplement(ref services, ITF.IRepository, repo);
            Injectionimplement(ref services, ITF.IGraphQL, graph);
            //添加Enum
            Type[] enumType = Assembly.Load("SKGPortalCore.Core").GetTypes().Where(p => p.IsEnum).ToArray();
            foreach (Type t in enumType) services.AddSingleton(typeof(BaseEnumerationGraphType<>).MakeGenericType(new[] { t }));

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = Env.IsDevelopment(); // expose detailed exceptions in JSON response
                })
               .AddGraphTypes(ServiceLifetime.Scoped)
               .AddUserContextBuilder(httpContext => httpContext.User)
               .AddDataLoader()
               .AddWebSockets();
            services.AddCors();
        }
        /// <summary>
        /// 動態注入介面/實體服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaces"></param>
        /// <param name="implements"></param>
        private void Injectionimplement(ref IServiceCollection services, List<Type> interfaces, List<Type> implements)
        {
            foreach (Type t in interfaces)
            {
                Type implement = implements.FirstOrDefault(p => null != p.GetInterface(t.Name));
                if (null != implement)
                {
                    //#if BackEnd
                    //                    if (implement.IsDefined(typeof(EndPointAttribute)) && implement.GetCustomAttribute<EndPointAttribute>().EndType != Core.LibEnum.EndType.Backend) continue;
                    //#endif
                    services.AddScoped(t, implement);
                }
            }
        }
        #endregion
    }
}

/* 日記
 ************************************************
 *  2020/04/24                   Kevin Wu       *
 * ----------------------------------------------
 * 要優先把前端頁面給搞定了...進度拖太久了。    *
 * 但還是想繼續把後端整理好弄好看點，ㄏㄏ       *
 * 哎...一個人把後端寫成這樣應該還不錯了吧？    *
 * ----------------------------------------------
 *  2020/04/27                   Kevin Wu       *
 * ----------------------------------------------
 *  舊的破系統又再次出問題了，PM只會當復答機，  *
 *  大家都說沒時間去更動，是有沒有人想要正視這  *
 *  個系統整體結構問題？                        *
 *  查到最後是資料問題，但本身這樣設計程式業務  *
 *  邏輯就有問題了好不。                        *
 *  PM自己提的流程都不知道為什麼哪裡有異常，幾  *
 *  次了                                        *
 *  算了，至少前端現在有動靜了，要在加緊腳步把  *
 *  Demo的東西快點丟給經理去提案吧。            * 
 * ----------------------------------------------
 *  2020/04/28                   Kevin Wu       *
 * ----------------------------------------------
 *  今天看起來暫時沒狀況，先來試Docker吧。      *
 *  要幫公司省錢，想哪些東西不用錢，當個免費仔  *
 *  希望這幾天能先有個成果可以在Docker架好Server*
 *  弄完之後再來把通路的資訊流做一個測試產生頁  *
 *  面，預計是做成Grid表版的功能吧              *
 * ----------------------------------------------
 *  2020/04/29                   Kevin Wu       *
 * ----------------------------------------------
 *  今天繼續弄Docker，昨天找了一整天用Docker架  *
 *  Service都不行，原因好像出在Proxy要架設的問  *
 *  題，跟馬哥確認先用Nginx架設在研究看看吧，   *
 *  反正前端也是用Nginx去代理Server的。         *
 *  好像發現NetCore不用Nginx代理Service的樣子。 *
 * ----------------------------------------------
 *  2020/04/30                   Kevin Wu       *
 * ----------------------------------------------
 * 今天被追一些工作上的事情都沒回報。也是啦，   *
 * 我一直都沒有把重心擺在現有破系統的問題上。   *
 * 有點迷茫了，說要啟新專案的事情一直沒消息，   *
 * 一直覺得明知道自己對現有破系統的維運不負責   *
 * ，但又被講成不是我的錯，然後自己專心又迷茫   *
 * 的開發新平台，好像感覺都不太對，這樣的情形   *
 * 大概已經圍繞了我一年多了吧？                 *
 * 有點壓抑不住這個迷茫感了，中午就直接跟經理   *
 * 談這件事情，但好像沒有太多進展，大概就起個   *
 * 頭這樣的感覺？                               *
 * 從今天開始，每周末都一定要發個進度報告列表   *
 * 跟週報回報了。既然沒有起頭，那只能先這樣推   *
 * 推看進度了。                                 *
 * 希望至少在轉職前能夠看到苗頭...但好像又有    *
 * 變化了。                                     *  
 * 在四點被抓去說要其他系統的開發，說是總經理   *
 * 要的。媽R今天怎麼事情這麼多？而且看了一下好  *
 * 像也沒有所謂的開發平台，好吧，過去展現一下看 *
 * 看吧。希望能盡早搞定然後再回來繼續推動這個   *
 * 平台了。                                     *
 * Oh my...我還是想早點轉行去做能發大財的工作。 *
 * ----------------------------------------------
 *  2020/05/04                   Kevin Wu       *
 * ----------------------------------------------
 * 今天開始聽了一下專案狀況，又是一個又趕著要   *
 * 但初步的底框架構都沒好的狀況。看來接下來的   *
 * 日子不好過囉....這邊的開發比率要變得很低了   *
 * 先優先把文檔整理好吧，唉。                   *
 * ----------------------------------------------
 *  2020/05/05                   Kevin Wu       *
 * ----------------------------------------------
 * 看起來整個Team的感覺好像也是處在瞎忙的狀態   *
 * 還是繼續有空就抽時間過來調整開發平台吧，不   *
 * 管怎麼說，我都要把這個開發平台完成，然後完   *
 * 成實際業務的系統出來演示。看前端的狀況大概   *
 * 這個月底以前能有的結果。時間阿...            *
 * ----------------------------------------------
 *  2020/05/12                   Kevin Wu       *
 * ----------------------------------------------
 * 真的是慘不忍睹。OU那邊沒一個專門理整體需求   *
 * 跟流程的人，每個都在那邊自己說：「喔有一個   *
 * 小的功能要做。」呵呵，馬上做到一半又再說怎   *
 * 麼又變了，當然阿，一開始就沒搞清楚ＰＭ要什   *
 * 麼，達成甚麼，想幹甚麼，就在那邊胡亂搞。     *
 * 不得不說，有禍有福啦。至少學到一點ＩｏＣ，   *
 * 洋蔥式架構的一些觀念，這幾天抽空就來大改架   *
 * 構試試看。                                   *
 * ----------------------------------------------
 *  2020/05/14                   Kevin Wu       *
 * ----------------------------------------------
 * 才沒過多久，事情又變回原點了，好不容易先說   *
 * 服動一個人的思考架構，馬上就被經理打回原形   *
 * 我還是把時間多花在這邊開發吧，開發完好離職   *
 * 去做健康管理事業比較實際。                   *
 * ----------------------------------------------
 *  2020/06/15                   Kevin Wu       *
 * ----------------------------------------------
 * 一個月過去了，的確又浪費掉了...              *
 * ----------------------------------------------
 *  2020/06/16                   Kevin Wu       *
 * ----------------------------------------------
 * 不能再浪費時間了。早上公司的事情，下午處理   *
 * 系統開發的事情，文件的事情優先吧。           *
 * 我真不想待在這家公司等死。                   *
 * ----------------------------------------------
 *  2020/06/17                   Kevin Wu       *
 * ----------------------------------------------
 *  早上專心弄公司的事情，下午專心弄新平台      *
 *  資料變更等等的問題看看吧。                  *
 *  希望能稍微找回以前開發的感覺了。才兩個月    *
 *  就整個狀態不對                              *
 * **********************************************
 */
