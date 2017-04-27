using System.IO;
using System.Xml;
using System.Collections;
using System.Security.Cryptography;

using UnityEngine;

//MD5 For File Hashing

public class SavingLoading : MonoBehaviour
{
    private string myFileName = "MyFile.xml";

    public Enemy saveableEnemy;
    public Enemy loadableEnemy;
    public EnemyProtection enemyProtection;

    public GameObject enemyObject;

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.S))
       {
            SaveFileXML();
       }

       if (Input.GetKeyDown(KeyCode.L))
       {
            LoadFileXML();
       }
    }
    private void SaveFileXML()
    {
        Enemy myNewEnemy = new Enemy();

        if (System.IO.File.Exists(Application.dataPath + "//" + myFileName))
        {
            System.IO.File.Delete(Application.dataPath + "//" + myFileName);

            SaveFileXML();
        }
        else
        {
            myNewEnemy.enemyHealth = 100;
            myNewEnemy.enemyLevel = 5;
            myNewEnemy.enemyName = enemyObject.name;

            myNewEnemy.xPos = enemyObject.transform.position.x;
            myNewEnemy.yPos = enemyObject.transform.position.y;
            myNewEnemy.zPos = enemyObject.transform.position.z;

            saveableEnemy = myNewEnemy;
        }


        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof (Enemy));

        StreamWriter fileWriter = new StreamWriter(Application.dataPath + "//" + myFileName);

        xmlSerializer.Serialize(fileWriter, saveableEnemy);

        fileWriter.Close();
    }

    private void LoadFileXML()
    {
        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Enemy));

        StreamReader fileReader = new StreamReader(Application.dataPath + "//" + myFileName);

        loadableEnemy = (Enemy)xmlSerializer.Deserialize(fileReader);

        fileReader.Close();

        Debug.Log("My Loaded Enemy's health is : " + loadableEnemy.enemyHealth);
        Debug.Log("My Loaded Enemy's Level is : " + loadableEnemy.enemyLevel);
        Debug.Log("My Loaded Enemy's Name is : " + loadableEnemy.enemyName);

        enemyObject.transform.position = new Vector3(loadableEnemy.xPos, loadableEnemy.yPos, loadableEnemy.zPos);
    }
}

public class Enemy
{
    [Header("Enemy Attributes")]
    public string enemyName = "Hello, I am an enemy!";
    public int enemyLevel = 5;
    public float enemyHealth = 100f;

    public float xPos = -5;
    public float yPos = 0;
    public float zPos = 5;
}

public class EnemyProtection
{
    public string hash;
}
