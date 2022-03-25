using System.Threading;

using UnityEngine;

namespace TestActivity
{
	public class StartupLifeTimeScope : MonoBehaviour
	{
		[SerializeField] private Model _model;

		private void Start()
		{
			_ = new GameplayController(_model.MovingObjectPrefab);
		}
	}
}