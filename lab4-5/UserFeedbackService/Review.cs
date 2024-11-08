using System;

namespace UserFeedbackService
{
    public class Review : FeedbackComponent
    {
        private readonly string _user;
        private readonly string _text;
        private readonly int _rating;

        public Review(string user, string text, int rating)
        {
            _user = user;
            _text = text;
            _rating = rating;
        }

        public override void DisplayFeedback()
        {
            Console.WriteLine($"User: {_user}, Rating: {_rating}, Feedback: {_text}");
        }
    }
}
