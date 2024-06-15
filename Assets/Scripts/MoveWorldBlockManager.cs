using System.Collections;
using UnityEngine;

public class MoveWorldBlockManager : MonoBehaviour
{
    //�Զ�ִ�еĺ�����Ҫ��ǰ�ű���������ײ����������Ӧ��갴��
    private IEnumerator OnMouseDown()
    {
        //��ǰ������Ļ����(���ڻ�ȡZֵ)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //������Ļ����
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPos.z);

        //������������
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        //��������ϵ�£���������Ĺ̶�ƫ��
        Vector3 offset = transform.position - mouseWorldPos;

        var wait = new WaitForFixedUpdate();

        while(Input.GetMouseButton(0))
        {
            //���¼��������������ĵ�ǰ��Ļ����
            mouseScreenPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPos.z);

            //��ǰ�����Ļ����ת��Ϊ��������
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            //���Ϲ̶�ƫ��õ��������������
            Vector3 pos = mouseWorldPos + offset;

            //�����º����������������µ�����
            transform.position = pos;

            yield return wait;
        }
    }
}