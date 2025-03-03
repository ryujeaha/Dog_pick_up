using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour
{
    private MeshRenderer renderer;//ofset값을 바꿔줄 렌더러 변수.

    private float offset;//오프셋값을 직접적으로 바꿔줄 변수

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();//MeshRenderer를 찾아서 변수에 넣어줌.
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;//프레임과 스피드 값을 곱한만큼 더해줌
        renderer.material.mainTextureOffset = new Vector2(offset,0);//적용
    }
}
