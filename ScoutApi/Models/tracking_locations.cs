namespace ScoutApi.Models
{
    public class tracking_locations
    {
        public int TeamID { get; set; }
        public string teamname { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string lasttracked { get; set; }
        public int staff { get; set; }
        public bool? onsite { get; set; }
        public int? score { get; set; }
        public int? members { get; set; }
    }
}