using UnityEngine;

public class Rope2 : MonoBehaviour
{
    [SerializeField] Transform[] ropeTransforms;
    [SerializeField] private float ropeLength = 1;

    private float[] x;
    private float[] y;

    private void Start()
    {
        x = new float[ropeTransforms.Length];
        y = new float[ropeTransforms.Length];
    }

    private void Update()
    {
        DragRope(0, Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        DragRope(1, x[0], y[0]);
    }

    private void DragRope(int index, float x, float y)
    {
        float dx = x - this.x[index];
        float dy = y - this.y[index];
        float angle = Mathf.Atan2(dy, dx);
        this.x[index] = x - Mathf.Cos(angle) * ropeLength;
        this.y[index] = y - Mathf.Sin(angle) * ropeLength;

        float deg = angle * Mathf.Rad2Deg;
        ropeTransforms[index].position = new Vector2(this.x[index], this.y[index]);
        ropeTransforms[index].eulerAngles = new Vector3(0, 0, deg);
    }
}