using Cysharp.Threading.Tasks;

using Mapbox.Json;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomerAssistant.MapKit
{
    public class FinishButtonView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _warningPanel;

        public void OnButtonClick()
        {
            var json = PlayerPrefs.GetString("Shops");

            var shops = JsonConvert.DeserializeObject<ShopJson>(json);

            if (shops.Shops.Count > 0)
            {
                async UniTaskVoid SwitchAsync()
                {
                    await SceneManager.LoadSceneAsync("Scenes/Dummy_demo");
                }

                SwitchAsync().Forget();
            }
            else
            {
                _warningPanel.SetActive(true);
            }
        }
    }
}