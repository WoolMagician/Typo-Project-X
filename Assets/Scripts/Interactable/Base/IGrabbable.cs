using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    bool CanGrab();
    void EnterGrabState();
    void ExitGrabState();
}