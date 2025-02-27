using UnityEngine;

namespace BeatBox.Util
{
    public class CircleRenderer : MonoBehaviour
    {

        [SerializeField] private int polygonPoints = 32;
        [SerializeField] public float radius = 2f;
        private float Lradius;

        private Vector2 Lpos;

        public LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.loop = true;
            Draw();
        }

        private void FixedUpdate()
        {
            if (radius != Lradius || transform.position.x != Lpos.x || transform.position.y != Lpos.y)
            {
                Draw();
                Lradius = radius;
                Lpos = transform.position;
            }
        }

        void Draw()
        {
            lineRenderer.positionCount = polygonPoints;
            float anglePerStep = 2 * Mathf.PI / polygonPoints;

            for (int i = 0; i < polygonPoints; ++i)
            {
                Vector2 point = Vector2.zero;
                float angle = anglePerStep * i;

                point.x = Mathf.Cos(angle) * radius + transform.position.x;
                point.y = Mathf.Sin(angle) * radius + transform.position.y;

                lineRenderer.SetPosition(i, point);
            }
        }
    }
}
