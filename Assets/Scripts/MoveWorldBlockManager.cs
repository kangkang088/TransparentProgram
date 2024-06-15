using System.Collections;
using UnityEngine;

public class MoveWorldBlockManager : MonoBehaviour
{
    //自动执行的函数，要求当前脚本身上有碰撞器，用于响应鼠标按下
    private IEnumerator OnMouseDown()
    {
        //当前物体屏幕坐标(用于获取Z值)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //鼠标的屏幕坐标
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPos.z);

        //鼠标的世界坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //世家坐标系下，鼠标和物体的固定偏差
        Vector3 offset = transform.position - mouseWorldPos;

        var wait = new WaitForFixedUpdate();

        while(Input.GetMouseButton(0))
        {
            //按下键，持续更新鼠标的当前屏幕坐标
            mouseScreenPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPos.z);

            //当前鼠标屏幕坐标转化为世界坐标
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            //加上固定偏差，得到物体的世界坐标
            Vector3 pos = mouseWorldPos + offset;

            //将更新后的物体世界坐标更新到物体
            transform.position = pos;

            yield return wait;
        }
    }
}