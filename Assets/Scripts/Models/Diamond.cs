using DG.Tweening;
using UnityEngine;

namespace Diamonds
{
    public class Diamond:MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;

        public void MoveIn(Vector3 position, float duration)
        {
            transform.DOLocalMove(position, duration);
        }

        public void SetConfig(DiamondConfig diamondConfig)
        {
            renderer.material.color = diamondConfig.color;
        }
    }
}
