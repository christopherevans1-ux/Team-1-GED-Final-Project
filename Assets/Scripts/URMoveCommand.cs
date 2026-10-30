using UnityEngine;

public class URMoveCommand : URCommand
{
    private Transform target;
    private Vector3 prevPosition;
    public float timer = 0f;

    public URMoveCommand(Transform target, Vector3 prevPlayerPos)
    {
        this.target = target;
        prevPosition = prevPlayerPos;
    }

    public void Undo()
    {
        if (prevPosition != null) { target.position = prevPosition; }
    }
}
