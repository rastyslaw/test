using System.Threading;
using commands;
using UnityEngine;

namespace Assets.Scripts.libs.test
{
    class ReadPlayerPrefs: AsyncCommand
    {
        public override void Execute()
        {
            Debug.Log(GetType() + " execute");
            LoadPlayerPrefs();
        }

        void LoadPlayerPrefs()
        {
            int stage = 1; 
            if (PlayerPrefs.HasKey(Names.STAGE))
            {
                stage = PlayerPrefs.GetInt(Names.STAGE); 
            }
            DataModel.SetValue(Names.STAGE, stage);
            
            DispatchComplete(true);
            /*
            if (PlayerPrefs.HasKey("saveFloat")) loadFloat = PlayerPrefs.GetFloat("saveFloat");
            if (PlayerPrefs.HasKey("saveInt")) loadInt = PlayerPrefs.GetInt("saveInt");
            if (PlayerPrefs.HasKey("saveBool")) loadBool = bool.Parse(PlayerPrefs.GetString("saveBool"));

            int j = 0;
            List<string> tmp = new List<string>();
            while (PlayerPrefs.HasKey("elementArray_" + j))
            {
                tmp.Add(PlayerPrefs.GetString("elementArray_" + j));
                j++;
            }

            loadArray = new string[tmp.Count];
            for (int i = 0; i < tmp.Count; i++)
            {
                loadArray[i] = tmp[i];
            }
            */
        }

        void SavePlayerPrefs()
        {
            /*
            PlayerPrefs.SetFloat("saveFloat", saveFloat);
            PlayerPrefs.SetInt("saveInt", saveInt);
            PlayerPrefs.SetString("saveBool", saveBool.ToString());

            for (int i = 0; i < saveArray.Length; i++)
            {
                PlayerPrefs.SetString("elementArray_" + i, saveArray[i]);
            }
            */
        }
    }
}
