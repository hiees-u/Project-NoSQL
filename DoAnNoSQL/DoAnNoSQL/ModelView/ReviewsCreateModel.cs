namespace DoAnNoSQL.ModelView
{
    public class ReviewsCreateModel
    {
        public string? product_id { get; set; }
        public string? user_id { get; set; }
        public int? rating { get; set; }
        public string? content { get; set; }

        public bool isValid()
        {
            if(string.IsNullOrWhiteSpace(product_id) || string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(content)) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product_id))
            {
                return false;
            }

            if (string.IsNullOrEmpty(content))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user_id))
            {
                return false;
            }

            if(rating < 0) {
                return false;
            }

            return true;
        }
    }
}
