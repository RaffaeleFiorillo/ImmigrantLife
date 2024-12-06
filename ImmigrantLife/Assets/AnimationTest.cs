using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationTest : MonoBehaviour
{

    [SerializeField] GameObject animT;

    [SerializeField]
    private float timeToJump;
   [SerializeField] private float timeOfJump;
    private float timeWait { get;set; }
    Vector3 endV;

   [SerializeField] Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {endV= animT.transform.position; ;

    }

    // Update is called once per frame
    void Update()
    { 



        timeWait += Time.deltaTime;

       
        if(timeWait >= timeToJump)
        {
            timeWait = 0;
         


//        animT.transform.DOMoveY()
                //(endV, 10f, 1, timeOfJump,true);



        }
    }
}
