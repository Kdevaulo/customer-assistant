using Cysharp.Threading.Tasks;

using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public interface IStaticMap
    {
        UniTask<Texture> Build(Vector2 ll, Vector2Int size);
    }
}