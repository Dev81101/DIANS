using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace WineApp.Models
{
    public class Winery
    {
        [Key]
        public int Id { get; set; }

        public double? lon { get; set; }

        public double? lat { get; set; }

        public String? name { get; set; }

        public String? img { get; set; }
        public String? text { get; set; }

        public static Winery FromJson(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<Winery>(jsonString);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log or throw)
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
                return null;
            }
        }


    }
}
