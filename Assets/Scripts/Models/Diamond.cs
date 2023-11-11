using UnityEngine;

namespace Diamonds
{
    public class Diamond:MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;

        public void MoveIn(Vector3 position, float moveTime = 0f)
        {
            transform.localPosition = position;
        }

        public void SetConfig(DiamondConfig diamondConfig)
        {
            renderer.material.color = diamondConfig.color;
        }
    }
}
