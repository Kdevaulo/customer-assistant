using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;

using Object = UnityEngine.Object;


namespace TestActivity
{
    public class GameplayController
    {
        private readonly GameObject _movingObject;

        private CancellationTokenSource _tokenSource;

        public GameplayController(GameObject movingObject)
        {
            _movingObject = movingObject;

            Start();

            WaitDelay().Forget();
        }

        private void Start()
        {
            var t = Object.Instantiate(_movingObject);

            _ = MoveTransform(t.transform);
        }

        private async UniTask MoveTransform(Transform target)
        {
            _tokenSource = new CancellationTokenSource();

            await UniTask.Delay(200);

            await DOTween.Sequence()
                .Append(target.DOLocalMove(Vector3.down, 1f))
                .Append(target.DOLocalMove(Vector3.left, 1f))
                .Append(target.DOLocalMove(Vector3.up, 1f))
                .Append(target.DOLocalMove(Vector3.right, 1f))
                .Append(target.DOLocalMove(Vector3.down, 1f))
                .Append(target.DOLocalMove(Vector3.left, 1f))
                .Append(target.DOLocalMove(Vector3.up, 1f))
                .Append(target.DOLocalMove(Vector3.right, 1f))
                .WithCancellation(_tokenSource.Token);
        }

        private async UniTask WaitDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));

            CancelToken();
        }

        private void CancelToken()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}