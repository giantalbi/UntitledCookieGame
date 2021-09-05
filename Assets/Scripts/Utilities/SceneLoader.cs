using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GranCook.Utilities
{
    public class SceneLoader: Singleton<SceneLoader>
    {
        public delegate void SceneLoaded();
        public event SceneLoaded OnSceneLoaded;

        public float Progress { get; set; }

        private string _sceneName;
        private bool _loadNextFrame = false;

        public SceneLoader() { }


        public void Load(string name)
        {
            //GameManager.Instance.MouseEnabled(true);

            _sceneName = name;
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            var async = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

            while (!async.isDone)
            {
                Progress = async.progress;

                if(Progress >= .9f)
                {
                    async.allowSceneActivation = true;

                    //Dispose();
                }

                yield return null;
            }

            OnSceneLoaded?.Invoke();
        }
    }
}
