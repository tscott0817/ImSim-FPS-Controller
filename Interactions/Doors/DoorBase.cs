using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorBase : MonoBehaviour
{
    public bool IsLocked { get; protected set; }

    public abstract void Open();
    public abstract void Close();
    public abstract void Interact();
}
