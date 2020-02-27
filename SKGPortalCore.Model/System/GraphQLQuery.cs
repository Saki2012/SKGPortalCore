﻿using Newtonsoft.Json.Linq;

namespace SKGPortalCore.Model.System
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}