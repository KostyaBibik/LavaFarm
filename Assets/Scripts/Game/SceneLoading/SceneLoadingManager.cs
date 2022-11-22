using System.Collections;
using DG.Tweening;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Game.SceneLoading
{
    public class SceneLoadingManager
    {
        private AsyncOperation loadingSceneOperation;
        private readonly float? _delayBeforeActive;

        public SceneLoadingManager()
        {
            Debug.Log("SceneLoadingManager");
        }
        
        public void LoadLocationScene(ELocationType location, float? delayBeforeActive = null)
        {
            Debug.Log("LoadLocationScene");
            loadingSceneOperation = SceneManager.LoadSceneAsync(location.ToString());
            loadingSceneOperation.allowSceneActivation = false;

            if (delayBeforeActive.HasValue)
            {
                //loadingSceneOperation.completed += _ => OnLoadOver();
                Observable.FromCoroutine(() => WaitSceneLoading(loadingSceneOperation, delayBeforeActive))
                    .DoOnCompleted(OnLoadOver)
                    .Subscribe();
            }
            else
            {
                OnLoadOver();
            }
        }

        private IEnumerator WaitSceneLoading(AsyncOperation asyncOperation, float? delayBeforeActive = null)
        {
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    if (delayBeforeActive != null) 
                        yield return new WaitForSeconds(delayBeforeActive.Value);
                    OnLoadOver();
                }

                yield return null;
            }
        }

        private void OnLoadOver()
        {
            loadingSceneOperation.allowSceneActivation = true;
        }
    }
}