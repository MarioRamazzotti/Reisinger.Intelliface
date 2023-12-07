using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.FaceRecognition
{
    public class Thumbnail
    {
        public string id { get; set; }
        public string thumbnail { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class Collection
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public DateTime create_date { get; set; }
        public DateTime modified_date { get; set; }
    }

    public class RecognizeResult
    {

        public string name { get; set; }
        public string id { get; set; }
        public string gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public string nationality { get; set; }
        public string notes { get; set; }
        public DateTime create_date { get; set; }
        public DateTime modified_date { get; set; }
        public double score { get; set; }
        public List<Collection> collections { get; set; }
    }
}
