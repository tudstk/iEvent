using System.Text.RegularExpressions;
using System.Diagnostics;

public class Monitor
{
    // Monitorizare FSM
    // se asigura ca actiunile respecta un anumit flux de stare: start -> finding -> interacting -> navigating -> finding
    public void MonitorFSM(List<string> actions)
    {
        string state = "start";

        foreach (var action in actions)
        {
            switch (state)
            {
                case "start":
                    if (action == "FindElement") state = "finding";
                    else if (action == "Click") state = "error";
                    break;

                case "finding":
                    if (action == "Click") state = "interacting";
                    else if (action == "FindElement") state = "finding";
                    break;

                case "interacting":
                    if (action == "Navigate") state = "navigating";
                    break;

                case "navigating":
                    if (action == "FindElement") state = "finding";
                    break;

                case "error":
                    Console.WriteLine("Violation: Click action without prior FindElement.");
                    break;

                default:
                    throw new InvalidOperationException("Unknown state!");
            }
        }
    }

    // Monitorizare ERE
    // expresie regulata care verifica daca actiunile respecta un anumit pattern: navigare -> gasire element -> click
    public void MonitorERE(List<string> actions)
    {
        string pattern = "Navigate.*FindElement.*Click";
        string actionSequence = string.Join(" ", actions);

        if (!Regex.IsMatch(actionSequence, pattern))
        {
            Console.WriteLine("Violation: Action sequence does not match ERE.");
        }
    }

    // Monitorizare LTL
    // conditii temporale, daca se navigheaza, atunci trebuie sa se gaseasca un element si sa se faca click
    public void MonitorLTL(List<string> actions)
    {
        bool navigateSeen = false;
        bool findElementAfterNavigate = false;
        bool clickAfterFindElement = false;

        foreach (var action in actions)
        {
            if (action == "Navigate")
            {
                navigateSeen = true;
            }

            if (navigateSeen && action == "FindElement")
            {
                findElementAfterNavigate = true;
            }

            if (findElementAfterNavigate && action == "Click")
            {
                clickAfterFindElement = true;
            }
        }

        if (!clickAfterFindElement)
        {
            Console.WriteLine("Violation: LTL property G(Navigate -> F(FindElement & F Click)) violated.");
        }
    }

}
public class PerformanceMonitor
{
    private Stopwatch stopwatch;

    public PerformanceMonitor()
    {
        stopwatch = new Stopwatch();
    }

    public void Start()
    {
        stopwatch.Start();
    }

    public long Stop()
    {
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public long Measure(Action action)
    {
        Start();
        action();
        return Stop();
    }
}