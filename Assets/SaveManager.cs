using UnityEngine;
using System.IO;
using System.Collections;
using System;

public class SaveManager : MonoBehaviour {
    Player player;
    int level;
    int strength;
    int agility;
    int intelligence;
    string savePath;
    //Turn into array once allowing multiple saves
    string saveCode;
    string loadCode;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        savePath = Application.persistentDataPath + "/SaveGame.txt";
	}

    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            writeFile();
            deAlphatizeString("iaf");
        }
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("f down");
            readFile();
            processCode(loadCode);
        }
    }
	
	// Update is called once per frame
	string createCode () {
        string code = "";
        strength = player.GetStrength();

        alphatizeInt(strength);
        agility = player.GetAgility();
        intelligence = player.GetIntelligence();
        
        return (strength + " " + agility + " " + intelligence);
	}

    void processCode(string code)
    {
        strength = -1;
        intelligence = -1;
        agility = -1;
        Debug.Log(code);
        //Reset values in case of previous load
        string temp = "";

        for(int i = 0; i < code.Length; i++)
        {
            //Read each part of code until a space is found
            if(code[i] != ' ')
            {
                temp += code[i];
            }
            if (code[i] == ' ' || i == code.Length - 1)
            {   //Once space is reached, if value hasnt been set, fill it in with the current temp string
                if(strength == -1)
                {
                    strength = Int32.Parse(temp);
                }else if(agility == -1)
                {
                    agility = Int32.Parse(temp);
                }else if(intelligence == -1)
                {
                    intelligence = Int32.Parse(temp);
                }
                temp = "";
            }
     

        }
        player.SetStrength(strength);
        player.SetAgility(agility);
        player.SetIntelligence(intelligence);
    }

    void writeFile()
    {
        
        Debug.Log(savePath);
        using (StreamWriter outputFile = new StreamWriter(savePath))
        {
            Debug.Log("Should be working");
            saveCode = createCode();
            outputFile.WriteLine(saveCode);
        }
        strength = player.GetStrength();
        agility = player.GetAgility();
        intelligence = player.GetIntelligence();
    }

    void readFile()
    {
        using (StreamReader inputFile = new StreamReader(savePath))
        {
            loadCode = inputFile.ReadLine();
        }
    }

    string alphatizeInt(int i)
    {
        
        char first;
        char second;
        char third;
        first = (char)('a' + UnityEngine.Random.Range(1, 26));
        second = (char)('a' + (i / 26));
        third = (char)('a' + (i % 26));
        string temp = first.ToString() + second.ToString() + third.ToString();
        Debug.Log(first);
        return temp;
    }
    int deAlphatizeString(string s)
    {
        int num = 0;
        for(int i = s.Length - 1; i > 0 ; i--)
        {
            num += (s[i] - 'a') * (s.Length - i) * 26 ;
        }
        Debug.Log("num is " + num);
        return num;
    }
}
