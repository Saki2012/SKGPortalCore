using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SKGPortalCore.Interface
{
    public static class ITF
    {
        public static List<Type> IGraphQL => typeof(ITF).Assembly.GetTypes().Where(p => p.IsInterface && p.Namespace.Contains("IGraphQL", StringComparison.Ordinal)).ToList();
        public static List<Type> IRepository => typeof(ITF).Assembly.GetTypes().Where(p => p.IsInterface && p.Namespace.Contains("IRepository", StringComparison.Ordinal)).ToList();
    }
}
