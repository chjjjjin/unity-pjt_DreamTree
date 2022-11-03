using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBound : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌");
        ExcecuteReBounding(collision);
        StartCoroutine(CtrlOnf());
        Debug.Log("함수 실행");
    }

    IEnumerator CtrlOnf()
    {
        GetComponent<BoingKit.UFOController>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        this.GetComponent<BoingKit.UFOController>().enabled = true;
    }

    void ExcecuteReBounding(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        Vector3 dir = transform.position - cp.point;  // 접촉지점에서부터 바니위치의 방향
        GetComponent<Rigidbody>().AddForce((dir).normalized * 300f);
    }
}
