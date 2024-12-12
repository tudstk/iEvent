using System;
using System.Collections.Generic;

namespace EventRecommendationService
{

    public class MOPMonitor
    {
        private enum EngineState { Uninitialized, Initialized, Recommending }

        private EngineState _state = EngineState.Uninitialized;

        private string _eventSequence = "";

        // FSM Event Registration
        public void RegisterSetRecommendationEngine()
        {
            if (_state == EngineState.Recommending)
            {
                throw new InvalidOperationException("Cannot set recommendation engine while recommending events.");
            }

            _state = EngineState.Initialized;
            _eventSequence += "SetEngine ";
        }

        // FSM Event Registration
        public void RegisterGetRecommendations()
        {
            if (_state != EngineState.Initialized)
            {
                throw new InvalidOperationException("Recommendation engine must be initialized before getting recommendations.");
            }

            _state = EngineState.Recommending;
            _eventSequence += "GetRecommendations ";
        }

        // FSM Monitoring Logic
        // This method checks if the event sequence is valid according to the FSM rules
        // Rules: the engine must be set before recommendations are requested and the engine 
        // must be reset before requesting recommendations again
        public void VerifyFSM()
        {
            var events = _eventSequence.Trim().Split(' ');

            string state = "Start";
            foreach (var evt in events)
            {
                switch (state)
                {
                    case "Start":
                        if (evt == "SetEngine")
                        {
                            state = "EngineSet";
                        }
                        else
                        {
                            throw new InvalidOperationException("FSM Violation: Must set engine before recommendations.");
                        }
                        break;
                    case "EngineSet":
                        if (evt == "GetRecommendations")
                        {
                            state = "Recommending";
                        }
                        else if (evt == "SetEngine")
                        {
                            state = "EngineSet";
                        }
                        else
                        {
                            throw new InvalidOperationException("FSM Violation: Invalid event after engine set.");
                        }
                        break;
                    case "Recommending":
                        if (evt == "SetEngine")
                        {
                            state = "EngineSet";
                        }
                        else
                        {
                            throw new InvalidOperationException("FSM Violation: Cannot recommend without resetting engine.");
                        }
                        break;
                }
            }
        }

        // LTL-like Property Check
        // This method checks if the event sequence satisfies a specific property
        // Property: Every recommendation must be preceded by engine setup
        public void VerifyLTL()
        {
            if (!_eventSequence.Contains("SetEngine GetRecommendations"))
            {
                throw new InvalidOperationException("LTL Violation: Every recommendation must be preceded by engine setup.");
            }
        }
    }
}

public class IntegrityMonitor
    {
        // Verifies that all events contain the necessary fields
        public void VerifyEventIntegrity(List<Event> events)
        {
            foreach (var evt in events)
            {
                if (string.IsNullOrEmpty(evt.Name))
                {
                    throw new InvalidOperationException($"Event integrity violation: Event {evt.Name} is missing a name.");
                }
                if (string.IsNullOrEmpty(evt.Location))
                {
                    throw new InvalidOperationException($"Event integrity violation: Event {evt.Name} is missing a location.");
                }
                if (string.IsNullOrEmpty(evt.Genre))
                {
                    throw new InvalidOperationException($"Event integrity violation: Event {evt.Name} is missing a genre.");
                }
                if (string.IsNullOrEmpty(evt.Type))
                {
                    throw new InvalidOperationException($"Event integrity violation: Event {evt.Name} is missing a type.");
                }
            }
        }
    }

    public class CodeConsistencyMonitor
    {
        // Verifies that the recommendation engine has been properly set
        public void VerifyRecommendationEngineConsistency(IRecommendationEngine engine)
        {
            if (engine == null)
            {
                throw new InvalidOperationException("Recommendation engine is not set.");
            }

            if (engine.GetType() != typeof(MachineLearningEngine) && engine.GetType() != typeof(RuleBasedEngine))
            {
                throw new InvalidOperationException("Recommendation engine type is inconsistent with the expected types.");
            }

            Console.WriteLine("Recommendation engine is consistent.");
        }
    }