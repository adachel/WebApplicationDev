
namespace Lec3ApiClientsBook.Client
{
    public class LibraryUsersClient : ILibraryUsersClient
    {
        readonly HttpClient client = new HttpClient();
        public async Task<bool> Exists(Guid? id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://localhost:7088/User/ExistsId?id={id.ToString()}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody == "true")
            {
                return true;
            }
            if (responseBody == "false")
            {
                return false;
            }

            throw new Exception("Unknow response");
        }
    }
}
