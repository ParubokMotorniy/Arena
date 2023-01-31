using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.StatesLogic
{
    public class InputReader : MonoBehaviour,Controls.IPlayerActions
    {
        public Vector2 movementValue { get; private set; }
        public event Action jumpEvent;
        public event Action dodgeEvent;
        public event Action lockTarget;

        private Controls controls;
        public bool isAttacking { get; private set; }
        public bool isBlocking { get; private set; }

        private void Awake()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }
        private void OnDestroy()
        {
            controls.Player.Disable();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                jumpEvent?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                dodgeEvent?.Invoke(); 
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnLockOnTarget(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                lockTarget?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                isAttacking = true;
            }
            if(context.canceled)
            {
                isAttacking = false;
            }
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                isBlocking = true;
            }
            if (context.canceled)
            {
                isBlocking = false;
            }
        }
    }
}

