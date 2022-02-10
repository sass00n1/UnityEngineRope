using UnityEngine;

public class Rope3 : MonoBehaviour
{
    [SerializeField] private Transform[] ropeTransforms;
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

        for (int i = 0; i < ropeTransforms.Length - 1; i++)
        {
            DragRope(i + 1, x[i], y[i]);
        }
    }

    private void DragRope(int index, float x, float y)
    {
        //使用反正切，计算出鼠标位置与绳子原点的弧度
        float dx = x - this.x[index];
        float dy = y - this.y[index];
        float rad = Mathf.Atan2(dy, dx);

        //关键的每帧原理：
        //每帧根据此弧度的正弦和余弦偏移出绳子应该出现的位置。
        this.x[index] = x - Mathf.Cos(rad) * ropeLength;
        this.y[index] = y - Mathf.Sin(rad) * ropeLength;

        //将弧度转换为角度，为整条绳子的各个子线段赋值位置与旋转
        float deg = rad * Mathf.Rad2Deg;
        ropeTransforms[index].position = new Vector2(this.x[index], this.y[index]);
        ropeTransforms[index].eulerAngles = new Vector3(0, 0, deg);
    }
}