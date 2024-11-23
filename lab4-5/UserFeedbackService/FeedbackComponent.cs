namespace UserFeedbackService
{
    public abstract class FeedbackComponent
    {   
        [FeedbackAspect]
        public virtual void Add(FeedbackComponent component)
        {
            throw new NotImplementedException();
        }

        [FeedbackAspect]
        public virtual void Remove(FeedbackComponent component)
        {
            throw new NotImplementedException();
        }

        [FeedbackAspect]
        public virtual void DisplayFeedback()
        {
            throw new NotImplementedException();
        }
    }
}
