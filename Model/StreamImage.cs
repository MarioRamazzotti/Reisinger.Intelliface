using Reisinger_Intelliface_1_0.Domain;

namespace Reisinger_Intelliface_1_0.Model
{
    public class StreamImage : IStorableObject
    {
        public Guid ID { get; set; }
        public string Collection_id { get; set; }
        public List<string> Images { get; set; }

        public int max_results = 10;
        public double min_score = 0.7;
        public string search_mode = "Accurate";

        public StreamImage()
        {
            ID = Guid.NewGuid();

            Images = new List<string>();


        }
    }
}
