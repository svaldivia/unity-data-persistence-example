using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// -----------------------------------------------------
// Singleton that stores player health and experience
// -----------------------------------------------------

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float health;
    public float experience;

    void Awake () {
        if (control == null) {
            // This prevents the current gameObject this script is attached to be destroyed when changing scenes
            DontDestroyOnLoad(gameObject);
            control = this;
        } else if (control != this) {
            // We don't want more than one!
            Destroy(gameObject);
        }
    }

    void OnGUI () {
        GUI.Label(new Rect(10, 10, 100,30), "Health: "+ health);
        GUI.Label(new Rect(10, 40, 150,30), "Experience: "+ experience);
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.health = health;
        data.experience = experience;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = bf.Deserialize(file) as PlayerData;
            file.Close();

            health = data.health;
            experience = data.experience;
        }
    }
}

// Data model class that is going to be serialized for saving
[Serializable]
class PlayerData {
    public float health;
    public float experience;

    // public PlayerData() {
    //     health = GameControl.control.health;
    //     experience = GameControl.control.experience;
    // }
}
