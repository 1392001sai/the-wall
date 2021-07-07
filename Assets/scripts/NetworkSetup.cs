using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


    [System.Obsolete]
    public class NetworkSetup : MonoBehaviour
    {
        public NetworkManager networkManager;
        public GameObject ClientsReadyButton;
        public GameObject WaitingForServerText;
        public GameObject SetUpHolder;
        public Button[] Selectors;
        public FindHost findHost;
        public string HostAddress;
        bool Client = false;
        public PlayerSpawn localPlayer;
        public int Team0No = 0;
        public int Team1No = 0;
        //public int team;
        public GameObject TeamFullText;
        public GameObject ErrorText;
        public GameObject TimeSelection;
        public int GameTime;
        public bool HostFail = false;
        public bool StartedClient = false;

        public void OnHost()
        {

            TimeSelection.SetActive(true);
            SetUpHolder.SetActive(false);
        }

        public void StartHost()
        {
        if (HostFail != true)
        {
            findHost.Initialize();
            findHost.StartAsServer();
            networkManager.StartHost();
        }
            TimeSelectionComplete();           
        }

        public void OnClient()
        {

        //team = FindObjectOfType<TeamSelectScript>().team;
        if (StartedClient != true)
        {
            SetUpHolder.SetActive(false);
            StartedClient = true;
            findHost.Initialize();
            findHost.StartAsClient();
            Client = true;
        }

        }

        private void Update()
        {
            if(Client==true)
            {
                
                if (HostAddress != "none")
                {
                    
                    //WaitingForServerText.SetActive(true);
                    HostAddress.Replace("::ffff:", string.Empty);
                    networkManager.networkAddress = HostAddress;
                    networkManager.StartClient();
                    
                    Client = false;
                    HostAddress = "none";
                }
            }
        }
        public bool ConditionCheck()
        {
        if (Team0No == 0 || Team1No == 0 || Team0No > 3 || Team1No > 3)
        {
            Instantiate(ErrorText, transform);
            return false;
        }
        else
        {
            return true;
        }
        }
        void TimeSelectionComplete()
        {
            
           
                Destroy(TimeSelection);

                ClientsReadyButton.SetActive(true);
        }
        
        public void min2()
        {
            GameTime = 120;
        }
        public void min4()
        {
            GameTime = 240;
        }
        public void min6()
        {
            GameTime = 360;
        }
        /*public void SetUpFailed()
        {
        Instantiate(TeamFullText, transform);
        StartedClient = false;
            networkManager.StopClient();
            SetUpHolder.SetActive(true);
        }*/

    public void SetUpSuccess()
    {
        WaitingForServerText.SetActive(true);
        
    }

}


