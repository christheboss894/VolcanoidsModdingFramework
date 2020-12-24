using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VolcanoidsModdingFramework
{
    public class FrameworkMod : GameMod
    {
        public override void Load()
        {
            System.Version version = typeof(FrameworkMod).Assembly.GetName().Version;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log(string.Format("GenericMod loaded: {0}, build time: {1}", version, File.GetLastWriteTime(typeof(FrameworkMod).Assembly.Location).ToShortTimeString()));
            Loaded = true;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000217C File Offset: 0x0000037C
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "Island")
            {
                scene = arg0;
                new GameObject("Framework", typeof(Framework));
            }
        }
        public override void Unload()
        {
            Debug.Log("Mod unloaded");
            Loaded = false;
        }
        public bool Loaded;
        public static Scene scene;
    }
}
