using System.Threading.Tasks;
using UnityEngine;

namespace RPGFramework.Battle
{
    public interface IBattleArenaPresentation
    {
        /// <summary>
        /// Takes an asset to load and returns an instantiated copy of the prefab
        /// </summary>
        /// <param name="asset">The arena asset to load</param>
        /// <returns></returns>
        Task<GameObject> LoadAsync(BattleArenaDefinition asset);

        /// <summary>
        /// Unloads the asset and destroy instantiated prefab
        /// </summary>
        Task UnloadAsync();
    }

    public sealed class BattleArenaPresentation : IBattleArenaPresentation
    {
        private GameObject  m_Instance;
        private AssetBundle m_AssetBundle;

        async Task<GameObject> IBattleArenaPresentation.LoadAsync(BattleArenaDefinition asset)
        {
            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(asset.AssetPath);
            await bundleRequest;

            AssetBundleRequest prefabRequest = bundleRequest.assetBundle.LoadAssetWithSubAssetsAsync<GameObject>(asset.AssetName);
            await prefabRequest;

            m_AssetBundle = bundleRequest.assetBundle;

            GameObject prefab = (GameObject)prefabRequest.asset;

            GameObject[] op = await Object.InstantiateAsync(prefab);
            m_Instance = op[0];

            return m_Instance;
        }

        async Task IBattleArenaPresentation.UnloadAsync()
        {
            Object.Destroy(m_Instance);
            AssetBundleUnloadOperation op = m_AssetBundle.UnloadAsync(true);

            await op;
        }
    }
}