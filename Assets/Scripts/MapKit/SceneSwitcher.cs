using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomerAssistant.MapKit
{
    public class SceneSwitcher: MonoBehaviour
    {
        public void SwitchScene()
        {
            SwitchSceneAsync().Forget();
        }

        private async UniTaskVoid SwitchSceneAsync()
        {
            await SceneManager.LoadSceneAsync("Scenes/Dummy_demo");
        }
    }
}