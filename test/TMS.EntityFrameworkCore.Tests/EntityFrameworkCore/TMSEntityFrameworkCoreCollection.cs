using Xunit;

namespace TMS.EntityFrameworkCore;

[CollectionDefinition(TMSTestConsts.CollectionDefinitionName)]
public class TMSEntityFrameworkCoreCollection : ICollectionFixture<TMSEntityFrameworkCoreFixture>
{

}
