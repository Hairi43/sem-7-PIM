using UnityEngine;
using System.Collections;

public class ARCameraDistance : MonoBehaviour
{
    public Transform cube;
    float dist;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CalcDistance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator CalcDistance()
    {
        while(true) {
            dist = Vector3.Distance(cube.position, transform.position);
            Debug.Log("Distance is: " + dist);
            yield return new WaitForSeconds(3);
        }
    }
}
