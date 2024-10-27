using HomeAssignment.IntegrationTests;

public class HomeAssignmentApiIntegrationTests
{
    [Test]
    public void FirstName_WhenNull_ShouldFailValidation()
    {

        var webFactory = new ApiWebFactory();

        var httpclient = webFactory.Server.CreateClient();
        // httpclient.BaseAddress = new Uri("http://localhost:5010");
        var result = httpclient.GetAsync("/api/V1/contact");


    }
}
