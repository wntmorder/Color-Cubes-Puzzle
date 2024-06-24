using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ModelObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    private readonly List<MoveStep> moveBuffer = new();
    private ModelObjectConfig modelObjectConfig;
    public ModelObjectConfig ModelObjectConfig
    {
        get => modelObjectConfig;
        set
        {
            modelObjectConfig = value;
            _renderer.material.color = value.color;
        }
    }

    private void Start()
    {
        Movement();
    }

    public void MoveIn(Vector3 position, float duration)
    {
        moveBuffer.Add(new MoveStep
        {
            position = position,
            duration = duration
        });
    }

    private async void Movement()
    {
        if (moveBuffer.Count < 1)
        {
            await Task.Yield();
            Movement();
            return;
        }

        MoveStep step = moveBuffer[0];
        moveBuffer.RemoveAt(0);

        await transform.DOLocalMove(step.position, step.duration).AsyncWaitForCompletion();
        Movement();
    }

    private void OnDestroy()
    {
        if (transform != null)
        {
            DOTween.Kill(transform);
        }
    }
}
