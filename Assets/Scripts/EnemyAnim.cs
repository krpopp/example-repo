using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{

    Animator myAnim;

    [SerializeField]
    float blinkMin, blinkMax;

    float blinkTime;

    // Start is called before the first frame update
    void Start()
    {
        blinkTime = Random.Range(blinkMin, blinkMax + 100);
        myAnim = GetComponent<Animator>();
    }
    // hiiiii
    // Update is called once per frame
    void Update()
    {
        blinkTime -= Time.deltaTime;
        if(blinkTime <= 0)
        {
            myAnim.SetTrigger("doBlink");
            blinkTime = Random.Range(blinkMin, blinkMax);
        }
    }
}
