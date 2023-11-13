using UnityEngine;

public class ControlLogic : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    private IInputHandler inputHandler;
    private Vector3 startSwipePosition = Vector3.zero;
    private Vector3 endSwipePosition = Vector3.zero;
    private Vector3 offset = Vector3.zero;
    private float swipeZone = 0.5f;
    private bool isSwiping = false;

    private void Start()
    {
        inputHandler = InputFactory.CreateInputHandler();
        inputHandler.OnSwipeStart += OnSwipeStart;
        inputHandler.OnSwipeMove += OnSwipeMove;
        inputHandler.OnSwipeEnd += OnSwipeEnd;
    }
    private void OnSwipeStart(Vector3 startPosition)
    {
        isSwiping = true;
        startSwipePosition = startPosition;
        endSwipePosition = startPosition;
    }
    private void OnSwipeMove(Vector3 currentPosition)
    {
        endSwipePosition = currentPosition;
        offset = endSwipePosition - startSwipePosition;
        CheckSwipe();
    }
    private void OnSwipeEnd(Vector3 endPosition)
    {
        isSwiping = false;
        endSwipePosition = endPosition;
    }
    private void Update()
    {
        inputHandler.Update();
    }
    private void CheckSwipe()
    {
        if (isSwiping)
        {
            if (Mathf.Abs(offset.y) > swipeZone)
            {
                modelManager.Rotate(startSwipePosition, offset);
                modelManager.CheckIntersection();
                startSwipePosition = endSwipePosition;
            }
        }
    }
}
