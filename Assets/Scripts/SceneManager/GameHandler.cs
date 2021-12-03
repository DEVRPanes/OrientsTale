using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject PlayerOrbGameObject;
    [SerializeField] private GameObject PlayerGameObject;
    [SerializeField] private GameObject Heart1;
    [SerializeField] private GameObject Heart2;
    [SerializeField] private GameObject Heart3;
    [SerializeField] private GameObject Heart4;
    [SerializeField] private GameObject Heart5;

    private PlayerController player;

    private void Awake()
    {
        player = PlayerGameObject.GetComponent<PlayerController>();

        // Check if folder exist (if not, then create)
        if (!Directory.Exists(SaveSystem.SAVE_FOLDER))
        {
            Directory.CreateDirectory(SaveSystem.SAVE_FOLDER);
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        if (File.Exists(SaveSystem.SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SaveSystem.SAVE_FOLDER + "/save.txt");
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            
            if (saveObject.save_Scene == SceneManager.GetActiveScene().buildIndex)
            {
                Load();
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
    }

    public void Save()
    {
        // What do I Save?
        SaveObject saveObject = new SaveObject {
            player_Position = player.transform.position,
            player_Orb_Position = PlayerOrbGameObject.transform.position,
            save_Scene = SceneManager.GetActiveScene().buildIndex,
            
            player_Health = player.currentHealth,
            Hp_1 = Heart1.activeSelf,
            Hp_2 = Heart2.activeSelf,
            Hp_3 = Heart3.activeSelf,
            Hp_4 = Heart4.activeSelf,
            Hp_5 = Heart5.activeSelf,
        };
        string json = JsonUtility.ToJson(saveObject);

        // Save the Data
        File.WriteAllText(SaveSystem.SAVE_FOLDER + "/save.txt", json);
        // Debug.Log("Saved");
    }

    public void LoadScene()
    {
        if (File.Exists(SaveSystem.SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SaveSystem.SAVE_FOLDER + "/save.txt");
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            int loadSavedScene = saveObject.save_Scene;

            SceneManager.LoadScene(loadSavedScene);
        }
        else
        {
            return;
        }
    }

    public void Load()
    {
        // What do I Load?
        if (File.Exists(SaveSystem.SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SaveSystem.SAVE_FOLDER + "/save.txt");
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            // Load Player Data
            player.transform.position = new Vector3(
                saveObject.player_Position.x,
                saveObject.player_Position.y,
                saveObject.player_Position.z);

            PlayerOrbGameObject.transform.position = new Vector3(
                saveObject.player_Orb_Position.x,
                saveObject.player_Orb_Position.y,
                saveObject.player_Orb_Position.z);

            player.currentHealth = saveObject.player_Health;
            Heart1.SetActive(saveObject.Hp_1);
            Heart2.SetActive(saveObject.Hp_2);
            Heart3.SetActive(saveObject.Hp_3);
            Heart4.SetActive(saveObject.Hp_4);
            Heart5.SetActive(saveObject.Hp_5);

            // test
            Debug.Log(saveString);
        }
        else
        {
            return;
        }
    }

    public void DeleteSaved()
    {
        // used when entering New Game
        if (File.Exists(SaveSystem.SAVE_FOLDER + "/save.txt"))
        {
            File.Delete(SaveSystem.SAVE_FOLDER + "/save.txt");

            Debug.Log("new game started");
        }
        else
        {
            return;
        }
    }

    private class SaveObject
    {
        public Vector3 player_Position;
        public Vector3 player_Orb_Position;
        public int save_Scene;
        
        public int player_Health;
        public bool Hp_1;
        public bool Hp_2;
        public bool Hp_3;
        public bool Hp_4;
        public bool Hp_5;
    }
}
