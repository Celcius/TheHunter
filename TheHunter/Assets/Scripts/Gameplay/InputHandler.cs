using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputHandler : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    private float triggerSensitivity = 0.2f;

    private InputActions inputActions;

    [SerializeField]
    private Transform anchor;

    public class InputActions : PlayerActionSet
    {
        public PlayerAction lockOn;
        public PlayerAction lockNext;
        public PlayerAction lockPrev;

        public PlayerOneAxisAction cycleLock;

        public InputActions()
        {
            lockOn = CreatePlayerAction( "Lock On" );
            lockNext = CreatePlayerAction( "Lock Next" );
            lockPrev = CreatePlayerAction( "Lock Prev" );
            cycleLock = CreateOneAxisPlayerAction( lockPrev, lockNext );
        }
    }

    private void Start()
    {
        inputActions = new InputActions();

        inputActions.lockOn.AddDefaultBinding( Key.Tab );
        inputActions.lockOn.AddDefaultBinding( InputControlType.LeftBumper );
        inputActions.lockOn.AddDefaultBinding( InputControlType.RightBumper );

    
        inputActions.lockNext.AddDefaultBinding( InputControlType.RightTrigger );
        inputActions.lockPrev.AddDefaultBinding( InputControlType.LeftTrigger );

        inputActions.lockNext.AddDefaultBinding( Key.E );
        inputActions.lockPrev.AddDefaultBinding( Key.Q );
    }

    private void Update()
    {
        if (inputActions.lockOn.WasPressed)
        {
            LockableSystem.Instance.CycleLock(anchor.position);
        }

        if( inputActions.cycleLock.WasPressed)
        {
            CycleLock( inputActions.cycleLock.Value );
        }
        
    }

    private void CycleLock(float offset)
    {
        if(Mathf.Abs(offset) < triggerSensitivity)
        {
            return;
        }

        if(offset < 0)
        {
            LockableSystem.Instance.LockOnPrevious(anchor.position);
        } 
        else if(offset > 0)
        {
            LockableSystem.Instance.LockOnNext(anchor.position);
        }
    }
}
