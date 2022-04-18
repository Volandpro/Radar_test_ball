using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader
    {
        public void Load(int sceneId, Action onLoaded = null) =>
            LoadScene(sceneId, onLoaded);

        private async void LoadScene(int sceneId, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneId);

            while (!waitNextScene.isDone)
                await Task.Yield();
      
            onLoaded?.Invoke();
        }
    }
}
