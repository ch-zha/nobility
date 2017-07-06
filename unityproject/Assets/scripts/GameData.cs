using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class GameData: MonoBehaviour {

	public TeamStatus currentTeam;

	public static GameData GAMEDATA;

	void Awake() {
		if (GAMEDATA == null) {
			DontDestroyOnLoad (gameObject);
			GAMEDATA = this;
		} else if (GAMEDATA != this) {
			Destroy (gameObject);
		}

		createDefaultTeam ();
	}

	private void createDefaultTeam() {
		currentTeam = new TeamStatus (new Participant[] 
			{new Participant(new Xenon (Character.BONDSTATE.ONE)),
			new Participant(new Xenon (Character.BONDSTATE.TWO)),
			new Participant (new Helium (Character.BONDSTATE.TWO))});
	}

	public void Save() {
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/savedata.dat", FileMode.Create);

		SaveData save = new SaveData ();
		save.currentTeam = currentTeam;

		formatter.Serialize (file, save);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/savedata.dat")) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/savedata.dat", FileMode.Open);
			SaveData save = (SaveData) formatter.Deserialize (file);

			currentTeam = save.currentTeam;

			file.Close ();
		}
	}

	[Serializable]
	class SaveData {

		public TeamStatus currentTeam;

	}
}
