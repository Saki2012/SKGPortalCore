using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SKGPortalCore.Data;
using SKGPortalCore.Graph.BillData;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Controllers.BillData
{
    //[Route("[Controller]")]
    public class BillController : BaseController
    {
        private readonly IDistributedCache _cache;

        public BillController(IDocumentExecuter documentExecuter, BillSchema schema, ISessionWapper sessionWapper, IDistributedCache cache) : base(documentExecuter, schema, sessionWapper)
        {
            _cache = cache;
            for (int i = 0; i < 10; i++)
            {
                BackendUserModel backend = new BackendUserModel() { KeyId = i.ToString(), UserName = "人員" + i };
                byte[] sessionByte = backend.ObjectToByteArray();
                _cache.Set("session" + i, sessionByte);
                byte[] result = _cache.Get("session" + i);
                _ = result.ByteArrayToObject<BackendUserModel>();
            }
        }


    }
}
