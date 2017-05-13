using System.Collections.Generic;
using UnityEngine;
using System.IO;

// -----------------------------------------------------------------------------------------
// Class that will serialize and write to a file
// Stores: coins, health, power ups and last level the player was in.
// -----------------------------------------------------------------------------------------

public enum PowerUp {
    Fireballs,
    DoubleJump
}
public class SaveData {

	#region Defaults
    public const string DEFAULT_LEVEL = "Game";
    private const int DEFAULT_COINS = 0;
    private const int DEFAULT_HEALTH = 100;
    private const int DEFAULT_LIVES = 3;
    #endregion

    // Initialize stats with default
    public int coins = DEFAULT_COINS;
    public int health = DEFAULT_HEALTH;
    public int lives = DEFAULT_LIVES;
    public List<PowerUp> powerUps = new List<PowerUp>();
    public string lastLevel = DEFAULT_LEVEL;

    const bool DEBUG_ON = true;

    public void WriteToFile(string filePath) {

        string json = JsonUtility.ToJson(this, true);

        File.WriteAllText(filePath, json);

        if (DEBUG_ON) {
            Debug.LogFormat("WriteToFile({0}) -- data:\n {1}", filePath, json);
        }
    }

    public static SaveData ReadFromFile(string filePath) {
        if (!File.Exists(filePath)) {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new SaveData();
        } else {

            string contents = File.ReadAllText(filePath);

            if (DEBUG_ON) {
                Debug.LogFormat("ReadFromFile({0})\ncontents:\n{1}", filePath, contents);
            }

            if (string.IsNullOrEmpty(contents)){
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new SaveData();
            }

            return JsonUtility.FromJson<SaveData>(contents);
        }
    }

    public bool isDefault() {
        return (
            coins == DEFAULT_COINS &&
            health == DEFAULT_HEALTH &&
            lives == DEFAULT_LIVES && 
            lastLevel == DEFAULT_LEVEL && 
            powerUps.Count == 0
        );
    }

    // Set up the string representation of this object
    public override string ToString() {

        return string.Format(
            "coins: {0}\nhealth: {1}\nlives: {2}\npowerUps: {3}\nlastLeve: {4}",
            coins,
            health,
            lives,
            GetPowerUpsString(),
            lastLevel
        );
    }

    public string GetPowerUpsString() {
        string[] powerUpsStrings = new string[powerUps.Count];
        for(int i = 0; i < powerUps.Count; i++) {
            powerUpsStrings[i] = powerUps[i].ToString();
        }

        return "[" +string.Join(",", powerUpsStrings) + "]";
    }

}
