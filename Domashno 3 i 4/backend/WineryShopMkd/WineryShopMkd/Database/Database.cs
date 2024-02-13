using Newtonsoft.Json;
using WineryShopMkd.Models;

namespace WineryShopMkd.Database
{
    public class Database1
    {

        public List<Winery> wineryShops;
        public Database1()
        {
            string path = "Database/data.json";

            // Read the JSON file contents
            string json = File.ReadAllText(path);

            // Deserialize the JSON data into list of WineryData objects
            List<Winery> wineryDataList = JsonConvert.DeserializeObject<List<Winery>>(json);
            this.wineryShops = wineryDataList;
        }
    }
}
