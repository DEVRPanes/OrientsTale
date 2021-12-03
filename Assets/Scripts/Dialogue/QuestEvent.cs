using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent : MonoBehaviour
{

    public GameObject quest;
    public GameObject finishQuestDialogue;

    public GameObject questProgressUI;
    public GameObject questFinishedUI;
    public GameObject portal;

    public GameObject questSpawnGameObjReq;

    public string questName = "Quest Name";
    public string questReq = "Dungan Essence";
    public int questQuantityReq = 4;
    public int currentQuantityReq = 0;

    public bool startQuestEvent = false;
    public bool FinishQuestEvent = false;

    public void Update()
    {
        if (startQuestEvent == true)
        {
            StartQuest();
        }

        if (currentQuantityReq >= 4)
        {
            FinishQuestEvent = true;
        }

        if (FinishQuestEvent == true)
        {
            quest.SetActive(false);
            questProgressUI.SetActive(false);
            questFinishedUI.SetActive(true);
            finishQuestDialogue.SetActive(true);
            portal.SetActive(true);
        }
    }

    public void StartQuest()
    {
        questProgressUI.SetActive(true);
        questSpawnGameObjReq.SetActive(true);
    }

    public void GotItem()
    {
        currentQuantityReq += 1;
    }
}
