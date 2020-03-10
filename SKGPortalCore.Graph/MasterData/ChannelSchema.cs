using GraphQL;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Repository.MasterData;

namespace SKGPortalCore.Graph.MasterData
{
    //Schema
    public class ChannelSchema : BaseSchema<ChannelQuery, ChannelMutation>
    {
        public ChannelSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class ChannelQuery : BaseQueryType<ChannelSet, ChannelSetType, ChannelType>
    {
        public ChannelQuery(ChannelRepository repository) : base(repository) { }
    }
    public class ChannelMutation : BaseMutationType<ChannelSet, ChannelSetType, ChannelSetInputType>
    {
        public ChannelMutation(ChannelRepository repository) : base(repository) { }
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
