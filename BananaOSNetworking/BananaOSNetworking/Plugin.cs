using BananaOS;
using BananaOS.Networking;
using BepInEx;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace BananaOSNetworking
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        bool changed;
        public static Texture2D texture;
        public static Material Background;
        public static List<string> Backgrounds = new List<string>();
        string filespath = Path.Combine(Path.GetDirectoryName(typeof(Plugin).Assembly.Location), "CustomDefaults");

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }

        void OnGameInitialized()
        {
            if (!Directory.Exists(filespath))
            {
                Directory.CreateDirectory(filespath);

            }
            var Hash = new ExitGames.Client.Photon.Hashtable();
            Hash.Add("PanelOpen", false);

            PhotonNetwork.SetPlayerCustomProperties(Hash);

            gameObject.AddComponent<NetworkManager>();

            Background = new Material(Shader.Find("GorillaTag/UberShader"));
            string AppPath = Application.dataPath;
            AppPath = AppPath.Replace("/Gorilla Tag_Data", "");
            string path = AppPath + @"/BepInEx/plugins/CustomDefaults/";

            Byte[] bytes = File.ReadAllBytes(Directory.GetFiles(path, "*.png")[0]);


            Debug.Log(Directory.GetFiles(path, "*.png")[0]);
            Texture2D loadTexture = new Texture2D(1, 1);
            ImageConversion.LoadImage(loadTexture, bytes);
            loadTexture.Apply();

            Background.mainTexture = loadTexture;

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] info = dir.GetFiles("*.png");

            foreach (var image in info)
            {
                Backgrounds.Add(image.Name + " Image\n");
                Debug.Log(image);
            }

        }
        public void Update()
        {
            if(MonkeWatch.Instance.watchActive)
            {
                if(!changed)
                {
                    var Hash = new ExitGames.Client.Photon.Hashtable();
                    Hash.Add("PanelOpen", true);

                    PhotonNetwork.SetPlayerCustomProperties(Hash);
                    changed = true;
                    Debug.Log(changed);
                }
            }
            else
            {
                if(changed)
                {
                    var Hash = new ExitGames.Client.Photon.Hashtable();
                    Hash.Add("PanelOpen", false);

                    PhotonNetwork.SetPlayerCustomProperties(Hash);
                    changed = false;
                    Debug.Log(changed);
                }
            }
        }
    }
}
