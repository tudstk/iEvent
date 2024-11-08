namespace UserFeedbackService
{
    public abstract class FeedbackComponent
    {
        public virtual void Add(FeedbackComponent component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(FeedbackComponent component)
        {
            throw new NotImplementedException();
        }

        public virtual void DisplayFeedback()
        {
            throw new NotImplementedException();
        }
    }
}
