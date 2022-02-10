using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private float ropeLength = 1;

    private float x = 0;
    private float y = 0;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //首先计算出鼠标位置与线段原点之间的弧度
        float dx = mousePosition.x - x;
        float dy = mousePosition.y - y;
        float rag = Mathf.Atan2(dy, dx); 

        //每帧都在做什么的关键所在：
        //最本质的原理就是：每帧根据鼠标位置和线段位置的Atan2反正切的弧度，用此弧度的正弦和余弦偏移出下一帧线段应该出现的位置。
        x = mousePosition.x - (Mathf.Cos(rag) * ropeLength); //比如鼠标x位置在2，那么 2 - (1 * 1) = 1，所以线段的x位置应该来到1，Cos的最大值永远为1。
        y = mousePosition.y - (Mathf.Sin(rag) * ropeLength); //比如鼠标y位置在2，那么 2 - (1 * 1) = 1，Sin的最大值也为1，这时Cos的值为0，所以x轴没有任何偏移。

        //线段的位置与旋转
        float deg = rag * Mathf.Rad2Deg;
        transform.position = new Vector2(x, y);
        transform.eulerAngles = new Vector3(0, 0, deg);
    }
}