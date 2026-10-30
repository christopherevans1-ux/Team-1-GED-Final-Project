using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Singleton
{
    public class URCommandManager : Singleton<URCommandManager>
    {
        private Stack<URCommand> undoStack = new Stack<URCommand>();

        public void AddAndExecuteCommand(URCommand command)
        {
            undoStack.Push(command);
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                URCommand activeCommand = undoStack.Pop();
                activeCommand.Undo();
            }
        }
    }

}