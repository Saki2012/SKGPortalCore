using SKGPortalCore.Core;
using SKGPortalCore.Core.GraphQL;
using SKGPortalCore.Interface.IGraphQL.MasterData;
using SKGPortalCore.Interface.IRepository.MasterData;
using SKGPortalCore.Model.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class ChannelSchema : BaseSchema<ChannelSet, ChannelQuery, ChannelMutation>, IChannelSchema
    {
        public ChannelSchema(IChannelRepository repo, ISessionWrapper session) : base(repo, session) { }
    }
    //Operate
    public class ChannelQuery : BaseQueryType<ChannelSet, ChannelSetType, ChannelType>
    {
        public ChannelQuery(IChannelRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class ChannelMutation : BaseMutationType<ChannelSet, ChannelSetType, ChannelSetInputType>
    {
        public ChannelMutation(IChannelRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    //Input
    public class ChannelSetInputType : BaseInputSetGraphType<ChannelSet> { }
    public class ChannelInputType : BaseInputFieldGraphType<ChannelModel> { }
    public class ChannelMapInputType : BaseInputFieldGraphType<ChannelMapModel> { }

    //Query
    public class ChannelSetType : BaseQuerySetGraphType<ChannelSet> { }
    public class ChannelType : BaseQueryFieldGraphType<ChannelModel> { }
    public class ChannelMapType : BaseQueryFieldGraphType<ChannelMapModel> { }

}
