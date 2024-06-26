using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    private IInputHandler inputHandler;
    private Vector3 startSwipePosition = Vector3.zero;
    private Vector3 endSwipePosition = Vector3.zero;
    private Vector3 offset = Vector3.zero;
    private readonly float swipeZone = 0.05f;
    private bool isSwiping = false;
    private float lastSwipeStartTime;

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
        modelManager.UpdateSwipeState(isSwiping);
        lastSwipeStartTime = Time.time;
        startSwipePosition = startPosition;
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
        modelManager.UpdateSwipeState(isSwiping);
        endSwipePosition = endPosition;
    }

    private void Update()
    {
        inputHandler.Update();
    }

    private void CheckSwipe()
    {
        if (isSwiping && Mathf.Abs(offset.y) > swipeZone)
        {
            modelManager.Rotate(startSwipePosition, offset, Time.time - lastSwipeStartTime);
            lastSwipeStartTime = Time.time;
            startSwipePosition = endSwipePosition;
        }
    }
}
