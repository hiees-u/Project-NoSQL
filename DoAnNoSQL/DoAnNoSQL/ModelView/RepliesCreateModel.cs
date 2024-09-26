namespace DoAnNoSQL.ModelView
{
    public class RepliesCreateModel
    {
        public string? review_id { get; set; }
        public string? user_id { get; set; }
        public string? content { get; set; }
        
        public bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(review_id))
                return false;
            if (string.IsNullOrWhiteSpace(user_id))
                return false;
            if (string.IsNullOrWhiteSpace(content))
                return false;
            return true;
        }
    }
}
