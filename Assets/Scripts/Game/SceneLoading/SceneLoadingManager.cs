using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Game.SceneLoading
{
    public class SceneLoadingManager
    {
        private AsyncOperation loadingSceneOperation;
        
        public void LoadLocationScene(ELocationType location)
        {
            loadingSceneOperation = SceneManager.LoadSceneAsync(location.ToString());
            loadingSceneOperation.allowSceneActivation = false;
        }

        private IEnumerator PreloadSceneProcess()
        {
            
            
            yield return null;
        }
    }
}