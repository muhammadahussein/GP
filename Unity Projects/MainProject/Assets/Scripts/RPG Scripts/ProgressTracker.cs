using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ProgressTracker: MonoBehaviour
{
    public static ProgressTracker Instance;

    private QuestGiver currentQuestGiver;

    public GameObject QuestsList;


    #region PRIVATE API
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void loadProgress()
    {
        loadFromFile();
        
    }

    QuestGiver questGiver;
    public Save savegame;
 

    void loadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "/save.sky"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.sky", FileMode.Open);
            savegame = (Save)bf.Deserialize(file);
            file.Close();

            if (savegame.questSave!=null)
            {
                GameObject questParent = QuestsList.transform.GetChild(savegame.questSave.questOrder - 1).gameObject;
                questParent.SetActive(true);
                Debug.Log(savegame.questSave.questOrder);
                /*if(savegame.questSave.questOrder==0)
                {
                    QuestsList.transform.GetChild(0).gameObject.SetActive(true);
                }*/
                questGiver = questParent.GetComponentInChildren<QuestGiver>();

                questGiver.AssignQuest(true);
                questGiver.Quest.QuestCompleted = !savegame.questSave.questState;
                questGiver.CheckQuest(true);

                simpleControl.Instance.transform.position = questGiver.SpawnPoint;
            }
            else
            {
                QuestsList.transform.GetChild(0).gameObject.SetActive(true);
            }
            //InventoryHandler.Instance.AddItemsToInventory(savegame.savedInventory);
            Invoke("addInvItems", 0.1f);

            //SetActive(true);
            //Debug.Log(savegame.savedInventory.Count);
        }
        else
        {
            GameObject questParent = QuestsList.transform.GetChild(0).gameObject;
            questParent.SetActive(true);
        }


    }
    void addInvItems()
    {
        InventoryHandler.Instance.AddItemsToInventory(savegame.savedInventory);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Save savegame = new Save();
            //savegame.questGiver = new QuestGiver();
            savegame.savedInventory = InventoryHandler.Instance.invItems;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/save.sky");
            bf.Serialize(file, savegame);
            file.Close();
            Debug.Log(savegame.savedInventory.Count);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            loadProgress();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            File.Delete(Application.persistentDataPath + "/save.sky");
        }
    }
    public void deleteSave()
    {
        File.Delete(Application.persistentDataPath + "/save.sky");
    }

    public void UpdateProgress(QuestSave qs)
    {
        Save savegame = new Save();
        savegame.questSave = qs;
        savegame.savedInventory = InventoryHandler.Instance.invItems;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.sky");
        bf.Serialize(file, savegame);
        file.Close();
    }

    #endregion
}




[System.Serializable]
public class Save
{
    public List<Item> savedInventory = new List<Item>();
    public QuestSave questSave;
}

