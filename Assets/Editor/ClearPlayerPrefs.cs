using UnityEngine;
using UnityEditor;

// Clears the PlayerPrefs file/registry entries
public class ClearPlayerPrefs : Editor {

	[MenuItem("Edit/Clear All PlayerPrefs")]
    static void ClearAll() {
        PlayerPrefs.DeleteAll();
    }
}
