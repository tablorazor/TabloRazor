using NUnit.Framework;
using Tabler.Docs.Components.Modals;
using TabloRazor;

namespace TabloRazor.Tests
{
    public class RenderComponentTests
    {
        [Test]
        public void RenderComponent_can_render_when_setting_parameters()
        {
            var component = new RenderComponent<TestModalContent>()
                .Set(p => p.ReportName, "TestReport");
            Assert.IsNotNull(component);
        }
    }
}