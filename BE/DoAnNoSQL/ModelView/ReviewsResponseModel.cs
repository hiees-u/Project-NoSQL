namespace DoAnNoSQL.ModelView
{
    public class ReviewsResponseModel
    {
        public string? _id { get; set; }
                
        public string? user_name { get; set; }

        public int? rating { get; set; }

        public string? content { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
