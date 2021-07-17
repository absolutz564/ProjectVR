using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public bool trigger = false;

    Coroutine co;
    public void TriggerExit()
    {
        StopCoroutine(co);
        GameController._instance.coolingDown = false;
        GameController._instance.FillImage.fillAmount = 0;
        trigger = false;
        //GetComponent<Renderer>().material.color = Color.red;
    }
    public void TriggerEnter()
    {
        Debug.Log("Colidiu");
        trigger = true;
        //GetComponent<Renderer>().material.color = Color.blue;
        co = StartCoroutine(WaitToDestroy());
        GameController._instance.coolingDown = true;
    }
    IEnumerator WaitToDestroy()
    {
        if (trigger)
        {
            yield return new WaitForSeconds(2.0f);
            GameController._instance.QuestCompleted();
            Destroy(this.gameObject);
            GameController._instance.coolingDown = false;
            GameController._instance.FillImage.fillAmount = 0;

        }
    }
}
