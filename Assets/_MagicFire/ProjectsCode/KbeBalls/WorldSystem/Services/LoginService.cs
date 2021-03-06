namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using UniRx;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using KBEngine;
    
    
    public class LoginService : LoginServiceBase {
        
        /// <summary>
        /// This method is invoked whenever the kernel is loading
        /// Since the kernel lives throughout the entire lifecycle  of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            // Use the line below to subscribe to events
            // this.OnEvent<MyEvent>().Subscribe(myEventInstance => { TODO });
        }

        public static LoginService inst;

        public int ui_state = 0;
        private string stringAccount = "";
        private string stringPasswd = "";
        private string labelMsg = "";
        private Color labelColor = Color.green;

        // ��ɫ���
        private int mass = 0;
        private int level = 0;

        public bool showReliveGUI = false;

        protected override void Start()
        {
            inst = this;
            InstallEvents();
        }

        void InstallEvents()
        {
            this.OnEvent<OnLoginSuccessfullyEvent>().ObserveOnMainThread().Subscribe(OnLoginSuccessfully);

            // common
            KBEngine.Event.registerOut("onKicked", this, "onKicked");
            KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
            KBEngine.Event.registerOut("onConnectionState", this, "onConnectionState");
            // login
            KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
            KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
            KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
            KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
            KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
            KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
            KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");
        }

        protected override void OnDestroy()
        {
            KBEngine.Event.deregisterOut(this);
            base.OnDestroy();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("KeyCode.Space");
            }
        }

        void onLoginUI()
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 30, 200, 30), "Login(��½)"))
            {
                Debug.Log("stringAccount:" + stringAccount);
                Debug.Log("stringPasswd:" + stringPasswd);

                if (stringAccount.Length > 0 && stringPasswd.Length > 5)
                {
                    login();
                }
                else
                {
                    err("account or password is error, length < 6!(�˺Ż���������󣬳��ȱ������5!)");
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 70, 200, 30), "CreateAccount(ע���˺�)"))
            {
                Debug.Log("stringAccount:" + stringAccount);
                Debug.Log("stringPasswd:" + stringPasswd);

                if (stringAccount.Length > 0 && stringPasswd.Length > 5)
                {
                    createAccount();
                }
                else
                {
                    err("account or password is error, length < 6!(�˺Ż���������󣬳��ȱ������5!)");
                }
            }

            stringAccount = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 30), stringAccount, 20);
            stringPasswd = GUI.PasswordField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 30), stringPasswd, '*');
        }

        void onWorldUI()
        {
            labelMsg = "";

            if (showReliveGUI)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 30), "Relive(����)"))
                {
                    KBEngine.Event.fireIn("relive", (Byte)1);
                }
            }

            if (level == 1)
                GUI.Label(new Rect(0, 75, 400, 100), "Mass: " + mass + "mg");
            else if (level == 2)
                GUI.Label(new Rect(0, 75, 400, 100), "Mass: " + mass + "g");
            else if (level == 3)
                GUI.Label(new Rect(0, 75, 400, 100), "Mass: " + mass + "kg");
            else if (level == 4)
                GUI.Label(new Rect(0, 75, 400, 100), "Mass: " + mass + "t");
            else if (level >= 5)
                GUI.Label(new Rect(0, 75, 400, 100), "Mass: " + mass + "kt");
        }

        void OnGUI()
        {
            if (ui_state == 1)
            {
                onWorldUI();
            }
            else
            {
                onLoginUI();
            }

            if (KBEngineApp.app != null && KBEngineApp.app.serverVersion != ""
                && KBEngineApp.app.serverVersion != KBEngineApp.app.clientVersion)
            {
                labelColor = Color.red;
                labelMsg = "version not match(curr=" + KBEngineApp.app.clientVersion + ", srv=" + KBEngineApp.app.serverVersion + " )(�汾��ƥ��)";
            }
            else if (KBEngineApp.app != null && KBEngineApp.app.serverScriptVersion != ""
                && KBEngineApp.app.serverScriptVersion != KBEngineApp.app.clientScriptVersion)
            {
                labelColor = Color.red;
                labelMsg = "scriptVersion not match(curr=" + KBEngineApp.app.clientScriptVersion + ", srv=" + KBEngineApp.app.serverScriptVersion + " )(�ű��汾��ƥ��)";
            }

            GUI.contentColor = labelColor;
            GUI.Label(new Rect((Screen.width / 2) - 100, 40, 400, 100), labelMsg);

            GUI.Label(new Rect(0, 5, 400, 100), "client version: " + KBEngine.KBEngineApp.app.clientVersion);
            GUI.Label(new Rect(0, 20, 400, 100), "client script version: " + KBEngine.KBEngineApp.app.clientScriptVersion);
            GUI.Label(new Rect(0, 35, 400, 100), "server version: " + KBEngine.KBEngineApp.app.serverVersion);
            GUI.Label(new Rect(0, 50, 400, 100), "server script version: " + KBEngine.KBEngineApp.app.serverScriptVersion);
        }

        public void err(string s)
        {
            labelColor = Color.red;
            labelMsg = s;
        }

        public void info(string s)
        {
            labelColor = Color.green;
            labelMsg = s;
        }

        public void login()
        {
            //SceneManager.LoadScene("world");
            info("connect to server...(���ӵ������...)");
            KBEngine.Event.fireIn("login", stringAccount, stringPasswd, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_balls"));
        }

        public void createAccount()
        {
            info("connect to server...(���ӵ������...)");

            KBEngine.Event.fireIn("createAccount", stringAccount, stringPasswd, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_balls"));
        }

        public void onCreateAccountResult(UInt16 retcode, byte[] datas)
        {
            if (retcode != 0)
            {
                err("createAccount is error(ע���˺Ŵ���)! err=" + KBEngineApp.app.serverErr(retcode));
                return;
            }

            if (KBEngineApp.validEmail(stringAccount))
            {
                info("createAccount is successfully, Please activate your Email!(ע���˺ųɹ����뼤��Email!)");
            }
            else
            {
                info("createAccount is successfully!(ע���˺ųɹ�!)");
            }
        }

        public void onConnectionState(bool success)
        {
            if (!success)
                err("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! (���Ӵ���)");
            else
                info("connect successfully, please wait...(���ӳɹ�����Ⱥ�...)");
        }

        public void onLoginFailed(UInt16 failedcode)
        {
            if (failedcode == 20)
            {
                err("login is failed(��½ʧ��), err=" + KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
            }
            else
            {
                err("login is failed(��½ʧ��), err=" + KBEngineApp.app.serverErr(failedcode));
            }
        }

        public void onVersionNotMatch(string verInfo, string serVerInfo)
        {
            err("");
        }

        public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
        {
            err("");
        }

        public void onLoginBaseappFailed(UInt16 failedcode)
        {
            err("loginBaseapp is failed(��½����ʧ��), err=" + KBEngineApp.app.serverErr(failedcode));
        }

        public void onLoginBaseapp()
        {
            info("connect to loginBaseapp, please wait...(���ӵ����أ� ���Ժ�...)");
        }

        public void OnLoginSuccessfully(OnLoginSuccessfullyEvent evt)
        {
            Debug.Log("login is successfully!");
            info("login is successfully!(��½�ɹ�!)");
            ui_state = 1;
            //this.Publish(new UnloadSceneCommand() { SceneName = "LoginScene" });
            this.Publish(new LoadSceneCommand() { SceneName = "WorldScene" });
        }

        public void onKicked(UInt16 failedcode)
        {
            err("kick, disconnect!, reason=" + KBEngineApp.app.serverErr(failedcode));
            SceneManager.LoadScene("login");
            ui_state = 0;
        }

        public void Loginapp_importClientMessages()
        {
            info("Loginapp_importClientMessages ...");
        }

        public void Baseapp_importClientMessages()
        {
            info("Baseapp_importClientMessages ...");
        }

        public void Baseapp_importClientEntityDef()
        {
            info("importClientEntityDef ...");
        }

        public void onDisconnected()
        {
            SceneManager.LoadScene("login");
            ui_state = 0;
        }
    }
}
