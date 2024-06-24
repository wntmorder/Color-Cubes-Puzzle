using System;
using UnityEngine;

public interface IInputHandler
{
    event Action<Vector3> OnSwipeStart;
    event Action<Vector3> OnSwipeMove;
    event Action<Vector3> OnSwipeEnd;
    void Update();
}

public class MouseInputHandler : IInputHandler
{
    private Vector3 currentMousePosition;

    public event Action<Vector3> OnSwipeStart;
    public event Action<Vector3> OnSwipeMove;
    public event Action<Vector3> OnSwipeEnd;

    public void Update()
    {
        Vector2 screenSize = new(Screen.width, Screen.height);
        currentMousePosition = Input.mousePosition / screenSize;
        if (Input.GetMouseButtonDown(0))
        {
            OnSwipeStart?.Invoke(currentMousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnSwipeEnd?.Invoke(currentMousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            OnSwipeMove?.Invoke(currentMousePosition);
        }
    }
}

public class TouchInputHandler : IInputHandler
{
    private Vector3 currentTouchPosition;

    public event Action<Vector3> OnSwipeStart;
    public event Action<Vector3> OnSwipeMove;
    public event Action<Vector3> OnSwipeEnd;

    public void Update()
    {
        Vector2 screenSize = new(Screen.width, Screen.height);
        currentTouchPosition = Input.GetTouch(0).position / screenSize;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                OnSwipeStart?.Invoke(currentTouchPosition);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                OnSwipeMove?.Invoke(currentTouchPosition);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                OnSwipeEnd?.Invoke(currentTouchPosition);
            }
        }
    }
}

public class InputFactory
{
    public static IInputHandler CreateInputHandler()
    {
        if (Application.isMobilePlatform)
        {
            return new TouchInputHandler();
        }
        else
        {
            return new MouseInputHandler();
        }
    }
}
