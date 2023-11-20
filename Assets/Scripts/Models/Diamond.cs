using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Diamonds
{
    public partial class Diamond:MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        private readonly List<MoveStep> moveBuffer = new();
        private void Start()
        {
            Movement();
        }
        public void MoveIn(Vector3 position, float duration)
        {
            moveBuffer.Add(new MoveStep { 
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
        public void SetConfig(DiamondConfig diamondConfig)
        {
            renderer.material.color = diamondConfig.color;
        }
    }
}
