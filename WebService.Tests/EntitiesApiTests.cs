
using Microsoft.AspNetCore.Mvc.Testing;
using Core;

using Newtonsoft.Json;
using System.Text;

using System.Text.RegularExpressions;

//
public class EntitiesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EntitiesApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }



    [Fact]
    public async Task InsertEntity()
    {

        await InsertEntityInternal();

    }

    [Theory]
    [InlineData(true)]//entity mast bee find
    [InlineData(false)]//entity mast bee not find
    public async Task GetEntity_ReturnsExpectedResult(bool shouldFindEntity)
    {
        // Arrange
        string id;
        if (shouldFindEntity)
        {
            id = await InsertEntityInternal();
        }
        else
        {
            id = Guid.NewGuid().ToString();
        }

        // Act
        var getResponse = await _client.GetAsync($"Entities/get/{id}");

        // Assert
        if (shouldFindEntity)
        {
            getResponse.EnsureSuccessStatusCode();
            var getResponseString = await getResponse.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<Entity>(getResponseString);
            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id.ToString());
            Assert.Equal(100.00m, entity.Amount);
        }
        else
        {
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
            var getResponseString = await getResponse.Content.ReadAsStringAsync();
            Assert.Contains(id.ToString(), getResponseString);
        }
    }

    private async Task<string> InsertEntityInternal()
    {
        var newEntity = new { Amount = 100.00m };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newEntity), Encoding.UTF8, "application/json");
        try
        {

            // Act
            var insertResponse = await _client.PostAsync("Entities/insert", jsonContent);
            // Assert Insert
            insertResponse.EnsureSuccessStatusCode();
            var insertResponseString = await insertResponse.Content.ReadAsStringAsync();
            var match = Regex.Match(insertResponseString, @"Entity with ID ""(?<id>.+)"" added.");
            Assert.True(match.Success, "Response did not match expected pattern.");
            var id = match.Groups["id"].Value;
            Assert.NotEmpty(id);
            return id;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}


