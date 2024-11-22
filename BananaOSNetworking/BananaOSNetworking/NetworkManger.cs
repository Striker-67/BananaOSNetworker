using BananaOS.Pages;
using BananaOSNetworking;
using BepInEx.Configuration;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static OVRHaptics;

namespace BananaOS.Networking
{

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public Dictionary<Player, GameObject> players = new Dictionary<Player, GameObject>();
        string pathforwatch = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/Watch";

        public override void OnJoinedRoom()
        {
            foreach (var player in PhotonNetwork.PlayerListOthers)
            {
                if (player.CustomProperties.ContainsKey("PanelOpen"))
                {
                    Debug.Log("=====================================PLAYER HAS THE MOD=====================================");
                        var rig = GorillaGameManager.StaticFindRigForPlayer(player);
                    var CloneWatch = GameObject.Instantiate(GameObject.Find(pathforwatch));
                    var clonescreen = GameObject.Instantiate(MonkeWatch.Instance.background);
                    CloneWatch.transform.SetParent(rig.leftHandTransform.parent, false);
                    clonescreen.transform.SetParent(CloneWatch.transform, false);
                    CloneWatch.transform.localPosition = MonkeWatch.watch.transform.localPosition;
                    CloneWatch.transform.localEulerAngles = MonkeWatch.watch.transform.localEulerAngles;
                    CloneWatch.transform.localScale = MonkeWatch.watch.transform.localScale;
                    clonescreen.transform.localPosition = new Vector3(0.6438f, 0.826f, 4.9895f);
                    clonescreen.transform.localEulerAngles = new Vector3(6.5946f, 96.9386f, 84.0126f);
                    CloneWatch.GetComponent<MeshRenderer>().material.mainTexture = BananaOSNetworking.Plugin.Background.mainTexture;
                    clonescreen.transform.GetChild(0).gameObject.SetActive(true);
                    clonescreen.transform.GetChild(1).transform.GetComponentInChildren<TextMeshProUGUI>().text = "<align=\"center\">\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n <color=\"yellow\">B A N A N A   <color=\"white\">O S</align>";
                    Destroy(CloneWatch.transform.GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(5).GetComponent<WatchButton>());

                    players.Add(player, CloneWatch);
                    if ((bool)player.CustomProperties["PanelOpen"])
                    {
                        players[player].transform.Find("Background(Clone)").transform.localScale = new Vector3(2, 2, 2);
                    }
                    else
                    {
                        players[player].transform.Find("Background(Clone)").transform.localScale = Vector3.zero;

                    }
                    
                }
            }
        }
        public override void OnLeftRoom()
        {
            foreach (GameObject Watches in players.Values)
            {
                Destroy(Watches);
            }
            players.Clear();
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {

           Destroy(players[otherPlayer].gameObject);
            players.Remove(otherPlayer);
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.ContainsKey("PanelOpen"))
            {
                if (newPlayer != PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("=====================================PLAYER HAS THE MOD=====================================");
                    var rig = GorillaGameManager.StaticFindRigForPlayer(newPlayer);
                    var clonescreen = GameObject.Instantiate(MonkeWatch.Instance.background);
                    var CloneWatch = GameObject.Instantiate(GameObject.Find(pathforwatch));
                    CloneWatch.transform.SetParent(rig.leftHandTransform.parent, false);
                    clonescreen.transform.SetParent(CloneWatch.transform, false);
                    CloneWatch.transform.localPosition = MonkeWatch.watch.transform.localPosition;
                    CloneWatch.transform.localEulerAngles = MonkeWatch.watch.transform.localEulerAngles;
                    CloneWatch.transform.localScale = MonkeWatch.watch.transform.localScale;
                    clonescreen.transform.localPosition = new Vector3(0.6438f, 0.826f, 4.9895f);
                    clonescreen.transform.localEulerAngles = new Vector3(6.5946f, 96.9386f, 84.0126f);
                    CloneWatch.GetComponent<MeshRenderer>().material.mainTexture = BananaOSNetworking.Plugin.Background.mainTexture;
                    clonescreen.transform.GetChild(0).gameObject.SetActive(true);
                    clonescreen.transform.GetChild(1).transform.GetComponentInChildren<TextMeshProUGUI>().text = "<align=\"center\">\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n <color=\"yellow\">B A N A N A   <color=\"white\">O S</align>";
                    Destroy(CloneWatch.transform.GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).GetComponent<WatchButton>());
                    Destroy(clonescreen.transform.GetChild(1).transform.GetChild(1).transform.GetChild(5).GetComponent<WatchButton>());
                    players.Add(newPlayer, CloneWatch);
                    if ((bool)newPlayer.CustomProperties["PanelOpen"])
                    {
                        players[newPlayer].transform.Find("Background(Clone)").transform.localScale = new Vector3(2, 2, 2);
                    }
                    else
                    {
                        players[newPlayer].transform.Find("Background(Clone)").transform.localScale = Vector3.zero;
                        
                    }
                }
                   
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if(targetPlayer != PhotonNetwork.LocalPlayer)
            {
                if ((bool)changedProps["PanelOpen"])
                {
                    players[targetPlayer].transform.Find("Background(Clone)").transform.localScale = new Vector3(2, 2, 2);

                }
                else
                {
                    players[targetPlayer].transform.Find("Background(Clone)").transform.localScale = Vector3.zero;
                }
            }
           
        }
    }
}
