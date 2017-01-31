using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;


public class SceneObjectData
{
	[XmlAttribute("name")]
	public string name;
	[XmlAttribute("posX")]
	public float posX;
	[XmlAttribute("posY")]
	public float posY;
	[XmlAttribute("posZ")]
	public float posZ;
}

[XmlRoot("SceneData")]
public class SceneData
{
	[XmlArray("SceneObjectDatas")]
	[XmlArrayItem("SceneObjectData")]
	public List<SceneObjectData> sceneObjectDatas = new List<SceneObjectData>();
}


public class SaveEngine : MonoBehaviour 
{
	void Load()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(SceneData));
		FileStream stream = new FileStream(Application.dataPath + "/saveFile.xml", FileMode.Open);
		SceneData sceneData = serializer.Deserialize(stream) as SceneData;
		stream.Close();

		for (int i = 0; i < sceneData.sceneObjectDatas.Count; ++i) 
		{			
			GameObject.Find (sceneData.sceneObjectDatas [i].name).transform.position = new Vector3(sceneData.sceneObjectDatas [i].posX, sceneData.sceneObjectDatas [i].posY, sceneData.sceneObjectDatas [i].posZ);
		}
	}

	void Save()
	{
		GameObject[] allGameObject = GameObject.FindObjectsOfType<GameObject> ();
		SceneData sceneData = new SceneData ();
		for (int i = 0; i < allGameObject.Length; ++i) 
		{
			SceneObjectData sceneObjectData = new SceneObjectData ();
			sceneObjectData.name = allGameObject[i].name;
			sceneObjectData.posX = allGameObject[i].transform.position.x;
			sceneObjectData.posY = allGameObject[i].transform.position.y;
			sceneObjectData.posZ = allGameObject[i].transform.position.z;
			sceneData.sceneObjectDatas.Add (sceneObjectData);
		}

		XmlSerializer serializer = new XmlSerializer(typeof(SceneData));
		FileStream stream = new FileStream(Application.dataPath + "/saveFile.xml", FileMode.Create);
		serializer.Serialize(stream, sceneData);
		stream.Close();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.L))
			Load ();
		if (Input.GetKeyDown (KeyCode.S))
			Save ();		
	}
}
