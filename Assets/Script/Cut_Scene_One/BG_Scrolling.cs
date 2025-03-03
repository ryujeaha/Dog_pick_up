using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour
{
    private MeshRenderer renderer;//ofset���� �ٲ��� ������ ����.

    private float offset;//�����°��� ���������� �ٲ��� ����

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();//MeshRenderer�� ã�Ƽ� ������ �־���.
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;//�����Ӱ� ���ǵ� ���� ���Ѹ�ŭ ������
        renderer.material.mainTextureOffset = new Vector2(offset,0);//����
    }
}
