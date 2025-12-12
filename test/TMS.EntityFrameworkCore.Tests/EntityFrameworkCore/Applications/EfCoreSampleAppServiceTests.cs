using TMS.Samples;
using Xunit;

namespace TMS.EntityFrameworkCore.Applications;

[Collection(TMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TMSEntityFrameworkCoreTestModule>
{

}
