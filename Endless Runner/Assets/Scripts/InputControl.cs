using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    public void OnSlideRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IControls>().SlideRight();
        }
    }

    public void OnSlideLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IControls>().SlideLeft();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IControls>().Jump();
        }
    }

    public void OnDuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IControls>().Duck();
        }
        if (context.canceled)
        {

        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IControls>().Pause();
        }
    }

    public void OnSendChat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<IChatControls>().Send();
        }
    }
}
