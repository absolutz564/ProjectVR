using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int numberOfQuest = 0;

    public static GameController _instance;

    public Text QuestText;

    public Animator GhostAnim;
    public Animator SlenderAnim;

    public Image FillImage;

    public bool coolingDown;
    public int waitTime;

    public bool slenderIsRunning = false;

    public int speed;
    public Transform target;
    public GameObject Slender;

    public Light MyLight;
    public List<Light> LightsToTurnOn = new List<Light>();

    public List<GameObject> objectsToDestroy = new List<GameObject>();


    public void EndGame()
    {
        MyLight.color = Color.white;
        foreach (Light lt in LightsToTurnOn)
        {
            lt.intensity = 2;
        }

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
    private void Awake()
    {
        _instance = this;
        QuestText.text = "Tasks Completed 0/5";
    }

    void Update()
    {
        if (numberOfQuest == 5)
        {
            numberOfQuest = 0;
            EndGame();
        }

        if (coolingDown == true)
        {
            FillImage.fillAmount += 1.0f / waitTime * Time.deltaTime;
        }

        if(slenderIsRunning)
        {
            float step = speed * Time.deltaTime;
            Slender.GetComponent<Transform>().position += (target.position - Slender.GetComponent<Transform>().position).normalized * speed * Time.deltaTime;           
        }
    }

    public IEnumerator WaitToDestroySlender(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        slenderIsRunning = false;
        Destroy(obj);
    }
    public void QuestCompleted()
    {
        numberOfQuest += 1;
        QuestText.text = "Tasks Completed " + numberOfQuest + "/5";
    }

    public void TriggerColision(Collider col)
    {
        if (col.tag == "triggerghost")
        {
            GhostAnim.SetTrigger("ghostrun");
            GhostAnim.GetComponent<AudioSource>().Play();
        }
        if (col.tag == "triggerslenderrun")
        {
            SlenderAnim.SetTrigger("run");
            StartCoroutine(WaitToDestroySlender(Slender));
            slenderIsRunning = true;
        }
    }
}
