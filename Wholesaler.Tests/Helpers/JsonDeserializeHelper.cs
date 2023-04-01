using Newtonsoft.Json;

namespace Wholesaler.Tests.Helpers
{
    public static class JsonDeserializeHelper
    {
        public async static Task<TResult> DeserializeAsync<TResult>(HttpResponseMessage response)
        {
            var resultContent = await response.Content.ReadAsStringAsync();
            var objectFromResponse = JsonConvert.DeserializeObject<TResult>(resultContent);

            return objectFromResponse;
        }
    }
}
