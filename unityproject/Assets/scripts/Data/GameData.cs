using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameData: MonoBehaviour {

	public string PLAYERNAME;
	public TeamStatus currentTeam;
	public string currentScene;

	public static GameData GAMEDATA;

	void Awake() {
		if (GAMEDATA == null) {
			DontDestroyOnLoad (gameObject);
			GAMEDATA = this;
		} else if (GAMEDATA != this) {
			Destroy (gameObject);
		}

		createDefaultTeam ();
		PLAYERNAME = "banana";
	}

	private void createDefaultTeam() {
		currentTeam = new TeamStatus (new Participant[] 
			{
				new Participant (new Xenon (Character.BONDSTATE.ONE)),
				new Participant (new Xenon (Character.BONDSTATE.ONE)),
				new Participant (new Xenon (Character.BONDSTATE.ONE))
			});
		
		foreach (Participant teammate in currentTeam.TEAMMATES) {
			if (teammate != null) {
				teammate.setTeam (currentTeam);
			}
		}
	}

	public void Save() {
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/savedata.dat", FileMode.Create);

		SaveData save = new SaveData ();
		save.PLAYERNAME = PLAYERNAME;
		save.currentTeam = currentTeam;
		save.currentScene = currentScene;

		formatter.Serialize (file, save);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/savedata.dat")) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/savedata.dat", FileMode.Open);
			SaveData save = (SaveData) formatter.Deserialize (file);

			currentTeam = save.currentTeam;
			PLAYERNAME = save.PLAYERNAME;
			currentScene = save.currentScene;

			file.Close ();
		}
	}

	[Serializable]
	class SaveData {

		public string PLAYERNAME;
		public TeamStatus currentTeam;
		public string currentScene;

	}
}
