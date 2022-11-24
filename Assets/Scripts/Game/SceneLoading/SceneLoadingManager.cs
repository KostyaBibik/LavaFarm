using System.Collections;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneLoading
{
    public class SceneLoadingManager
    {
        private AsyncOperation _loadingSceneOperation;
        private readonly float? _delayBeforeActive;
        
        public void LoadLocationScene(ELocationType location, float? delayBeforeActive = null)
        {
            _loadingSceneOperation = SceneManager.LoadSceneAsync(location.ToString());
            _loadingSceneOperation.allowSceneActivation = false;

            if (delayBeforeActive.HasValue)
            {
                Observable.FromCoroutine(() => WaitSceneLoading(_loadingSceneOperation, delayBeforeActive))
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
            _loadingSceneOperation.allowSceneActivation = true;
        }
    }
}