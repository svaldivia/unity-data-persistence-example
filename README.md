# Data Persistence in Unity - Examples

## Purpose
This project explores 3 different methods of data serialization (Saving) in Unity: binary serialization, PlayerPrefs and JSON serialization. I based this project on the Unity Serialization Live Cast and a great [tutorial](http://naplandgames.com/blog/2016/11/27/saving-data-in-unity-3d-serialization-for-beginners/) from Napland Games.  

## Method Examples

### Binary Serialization

This example explores the serialization of a class containing player's "health" and "experience" using C#'s binary formatter. The save and load functions are handled by a singleton called `GameControl` that saves/loads data from a `.dat` file in Unity's pre-defined `persistentDataPath`.

The user can modify the "health" and "experience" numbers and save. This data persists accross the 2 scenes in this example.

**How to run**

* Open "Level 1" scene and press play.
* If you saved data before, press the "Load" button to show the saved data.
* **Note** Every time you save you overwrite the previous save.


**Relevant files**

* "Level 1" and "Level 2" scenes
* Scripts in `Binary Save Example Folder`

### Player Prefs

This example explores using PlayerPrefs to control the volume and mute an audio source. It mainly consists on a `PlayerPrefHandler` class that handles all functions regarding PlayerPrefs and a singleton `DataService` that exposes an instance of the `PlayerPrefHandler` for other classes to use.

The saved data is loaded every time the scene is loaded.

**How to run**

* Make sure `MuteTogleHandler` and `VolumeSliderHandler` use `EX1.DataService`.
* Open "Settings" and press play.
* You can make changes and press "Reload Scene." Your changes will persist, even if you stop and play again the scene.
* The main menu will not work when this example is activated.
* To delete all PlayerPrefs you can use Edit/Clear All PlayerPrefs on the Editor's menu.

**Relevant files**

* "Settings" scene
* Scripts in `Player Prefs Example`

**Note** `DataService.cs` is a singleton that is shared between this example and the JSON example, they are both namespaced `EX1` and `EX2` respectively. `EX2` is an extension of this class so it build on top of the behavior in this example. I left `EX1` here because it depicts the only code needed to make PlayerPrefs work.

### JSON Serialization

This example explores the serialization of the `SaveData` class into a JSON object using Unity's `JsonUtility` that is then saved in a text file. Also, this supports the saving into 3 game slots that contain different data.

The user can modify their stats and change the level they are currently in.

**How to run**

* * Make sure `MuteTogleHandler` and `VolumeSliderHandler` use `EX2.DataService`.
* Open "Main Menu" scene and press play.
* You can go to settings and modify the audio (saved in PlayerPrefs).
* Click in the slots and modify the stats using the buttons. You can explicity save or let it auto-save when you return to the main menu.

**Relevant files**

* "Main Menu", "Game", "Game 2", "Game 3" and "Settings" scenes
* Scripts in `JSON Serialization Example`

## Troubleshooting

**I see a warning and script error related to `OnLevelWasLoaded`**

This is OK, the code is going to work as expected. If REALLY you don't like this you can replace the `OnLevelWasLoaded` callback to use a lamda function instead. There is a commented example of this in `EX1.DataService`.

**It doesn't go to the right scenes...or there is a build settings error**

Make sure that the build settings have the scene in this order:
* Level 1
* Level 2
* Main Menu
* Settings
* Game
* Game 2
* Game 3
