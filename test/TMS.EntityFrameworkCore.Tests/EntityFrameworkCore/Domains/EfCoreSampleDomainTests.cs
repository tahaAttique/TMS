using TMS.Samples;
using Xunit;

namespace TMS.EntityFrameworkCore.Domains;

[Collection(TMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TMSEntityFrameworkCoreTestModule>
{

}
