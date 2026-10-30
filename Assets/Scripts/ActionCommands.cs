using System;

public class ActionCommands : ICommand
{
    private readonly Action action;

    public ActionCommands(Action action)
    {
        this.action = action;
    }

    public void Execute()
    {
        action?.Invoke();
    }
}
