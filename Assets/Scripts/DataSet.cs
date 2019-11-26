using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSet : MonoBehaviour {

	// Use this for initialization
	public string jarak;
	public string ammo;
	public string health;
	public string healthEnemy;

	public List<SaveD> dataset;
	public SaveD DataS;
	private Player P;
	private NPC n;
	void Start () {
		P = GetComponent<Player>();
		n = GetComponent<NPC>();
		// Load();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z)){
			DataS.aksi="nembak";
			ambildataZ();
		}
		if(Input.GetKeyDown(KeyCode.X)){
			DataS.aksi="jurus";
			ambildataX();
		}
		if(Input.GetKeyDown(KeyCode.L)){
			Load();
		}
		if(Input.GetKeyDown(KeyCode.A)){
			Save();
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			Reset();
		}
	}
		public void ambildataZ(){

		// Debug.Log(P.ammo+P.health+P.jarak);
		if(P.jarak >=0.1f && P.jarak <= 4f)
			{
				DataS.jarak = "dekat";
			}
			else if(P.jarak > 4f && P.jarak <= 6f)
			{
				DataS.jarak = "sedang";
			}
			else if(P.jarak > 6f)
			{
				DataS.jarak = "jauh";
			}
			if(P.ammo == 0)
			{
				DataS.ammo = "habis";
			}
			else if(P.ammo >= 1 && P.ammo <= 10)
			{
				DataS.ammo = "sedikit";
			}
			// else if(P.ammo > 5 && P.jarak <= 9)
			// {
			// 	DataS.ammo = "sedang";
			// }
			else if(P.ammo >10)
			{
				DataS.ammo = "banyak";
			}
			if(P.health >= 10 && P.health <= 50)
			{
				DataS.health = "lemah";
			}
			else if(P.health > 50 && P.health <= 150)
			{
				DataS.health = "sedang";
			}
			else if(P.health > 150)
			{
				DataS.health = "kuat";
			}
			// Health NPC
			if(n.healthEnemy >= 10 && n.healthEnemy <= 50)
			{
				DataS.healthEnemy = "lemah";
			}
			else if(n.healthEnemy > 50 && n.healthEnemy <= 150)
			{
				DataS.healthEnemy = "sedang";
			}
			else if(n.healthEnemy > 150)
			{
				DataS.healthEnemy = "kuat";
			}
			DataS.aksi = "nembak";
		dataset.Add(DataS);

	}
	public void ambildataX(){

		// Debug.Log(P.ammo+P.health+P.jarak);
			if(P.jarak >=0.1f && P.jarak <= 4f)
			{
				DataS.jarak = "dekat";
			}
			else if(P.jarak > 4f && P.jarak <= 8f)
			{
				DataS.jarak = "sedang";
			}
			else if(P.jarak > 8f)
			{
				DataS.jarak = "jauh";
			}
			if(P.ammo == 0)
			{
				DataS.ammo = "habis";
			}
			else if(P.ammo >= 1 && P.ammo <= 10)
			{
				DataS.ammo = "sedikit";
			}
			// else if(P.ammo > 5 && P.jarak <= 9)
			// {
			// 	DataS.ammo = "sedang";
			// }
			else if(P.ammo > 10)
			{
				DataS.ammo = "banyak";
			}
			if(P.health >= 10 && P.health <= 50)
			{
				DataS.health = "lemah";
			}
			else if(P.health > 50 && P.health <= 150)
			{
				DataS.health = "sedang";
			}
			else if(P.health > 150)
			{
				DataS.health = "kuat";
			}
			// ammo
			// Health NPC
			// if(npc.healthEnemy >= 10 && npc.healthEnemy <= 50)
			// {
			// 	DataS.healthEnemy = "lemah";
			// }
			// else if(npc.healthEnemy > 50 && npc.healthEnemy <= 150)
			// {
			// 	DataS.healthEnemy = "sedang";
			// }
			// else if(npc.healthEnemy > 150)
			// {
			// 	DataS.healthEnemy = "kuat";
			// }
			Debug.Log(healthEnemy);
			DataS.aksi = "jurus";
		dataset.Add(DataS);
	}
	public void Save(){
		List<SaveD> datakosong;
		datakosong = LoadA();

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Open(Application.dataPath + "/save.txt", FileMode.OpenOrCreate);
		SimpanData data = new SimpanData();

		data.simpanD = datakosong;
		int i = 0;
		while (i < dataset.Count){
			data.simpanD.Add(dataset[i]);
			i++;
		}

		formatter.Serialize(file, data);
		file.Close();

		Debug.Log("Berhasil disimpan");
	}
	public List<SaveD> LoadA(){
		List<SaveD> D = new List<SaveD>();

		if(File.Exists(Application.dataPath + "/save.txt")){
			using(FileStream file = File.Open(Application.dataPath + "/save.txt", FileMode.Open)){
				BinaryFormatter formatter = new BinaryFormatter();
				SimpanData data = (SimpanData)formatter.Deserialize(file);

				int i = 0; 
				// dataset.Clear();
				while (i < data.simpanD.Count)
				{
					D.Add(data.simpanD[i]);
					i++;
				}

				Debug.Log("Load data berhasil");
			}
		}else{
			Debug.Log("Load data tidak berhasil");
		}
		return D;
	}
	public void Load(){
		if(File.Exists(Application.dataPath + "/save.txt")){
			using(FileStream file = File.Open(Application.dataPath + "/save.txt", FileMode.Open)){
				BinaryFormatter formatter = new BinaryFormatter();
				SimpanData data = (SimpanData)formatter.Deserialize(file);
				// name = data.name;
				// level = data.level;
				int i = 0; 
				dataset.Clear();
				while (i < data.simpanD.Count)
				{
					dataset.Add(data.simpanD[i]);
					i++;
				}

				Debug.Log("load data berhasil");
			}
		}else{
			Debug.Log("load data tidak berhasil");
		}
	}
	public void Reset(){
		List<SaveD> datakosong = new List<SaveD>();

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Open(Application.dataPath + "/save.txt", FileMode.OpenOrCreate);
		SimpanData data = new SimpanData();
	
		data.simpanD = datakosong;


		formatter.Serialize(file, data);
		file.Close();

		Debug.Log("reset data");
	}
}
[Serializable]
public struct SaveD{
	public string jarak;
	public string ammo;
	public string health;
	public string healthEnemy;
	public string aksi;
}

[Serializable]
public struct SimpanData{
	public List<SaveD> simpanD;
}
