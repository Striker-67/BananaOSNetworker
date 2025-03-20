using BananaOS;
using BananaOS.Networking;
using BepInEx;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace BananaOSNetworking
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        bool changed;
        public static Texture2D texture;
        public static Material Background;
        public static List<string> Backgrounds = new List<string>();
        string filespath = Path.Combine(BepInEx.Paths.PluginPath.ToString(), "CustomDefaults");

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }

        void OnGameInitialized()
        {
            Debug.Log(filespath);
            if (!Directory.Exists(filespath))
            {
                Directory.CreateDirectory(filespath);

            }
            var Hash = new ExitGames.Client.Photon.Hashtable();
            Hash.Add("PanelOpen", false);

            PhotonNetwork.SetPlayerCustomProperties(Hash);

            gameObject.AddComponent<NetworkManager>();
            try
            {
                Background = new Material(Shader.Find("GorillaTag/UberShader"));
                string path = filespath;

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
            catch (Exception e)
            {
                Debug.Log("):");
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
