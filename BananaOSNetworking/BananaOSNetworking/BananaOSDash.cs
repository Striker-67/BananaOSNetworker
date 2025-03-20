using System;
using UnityEngine.XR;
using UnityEngine;
using BananaOS;
using System.Text;
using BananaOS.Pages;
using System.Collections;
using Photon.Pun;
using GorillaNetworking;
using BananaOS.Networking;
using Photon.Realtime;

namespace DashMonkebananaos
{
    public class DashManager : WatchPage
    {
        public override string Title => "BananaOSNetworker";

        public override bool DisplayOnMainMenu => true;
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 3;
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Players with the mod");
            stringBuilder.AppendLine("====================");

            foreach(Player players in NetworkManager.players.Keys)
            {
                stringBuilder.AppendLine(players.NickName);
            }
             return stringBuilder.ToString();
        }
        public override void OnButtonPressed(WatchButtonType buttonType)
        {

            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Left:

                    break;
                case WatchButtonType.Right:

                    break;
                case WatchButtonType.Enter:

                    break;

                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
        

    }
       
}
public class Moddedcheck : MonoBehaviourPunCallbacks
{
    public static Moddedcheck modcheck;
    public object gameMode;

    public void Start()
    {
        modcheck = this;
    }
public bool IsModded()
{
        return NetworkSystem.Instance.GameModeString.Contains("MODDED");
}

    public override void OnLeftRoom()
    {
        BananaOS.MonkeWatch.Instance.UpdateScreen();
       
    }
}