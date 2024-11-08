using System;
using System.Collections.Generic;

namespace UserFeedbackService
{
    //Composite
    public class FeedbackGroup : FeedbackComponent
    {
        private readonly List<FeedbackComponent> _feedbackComponents = new List<FeedbackComponent>();
        private readonly string _groupName;

        public FeedbackGroup(string groupName)
        {
            _groupName = groupName;
        }

        public override void Add(FeedbackComponent component)
        {
            _feedbackComponents.Add(component);
        }

        public override void Remove(FeedbackComponent component)
        {
            _feedbackComponents.Remove(component);
        }

        public override void DisplayFeedback()
        {
            Console.WriteLine($"Feedback Group: {_groupName}");
            foreach (var component in _feedbackComponents)
            {
                component.DisplayFeedback();
            }
        }
    }
}
