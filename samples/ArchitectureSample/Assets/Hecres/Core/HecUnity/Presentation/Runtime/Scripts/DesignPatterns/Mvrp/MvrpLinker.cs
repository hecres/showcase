using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp
{
    /// <summary>
    /// MVRPパターンの紐づけクラス
    /// </summary>
    public static class MvrpLinker
    {
        /// <summary>
        /// 指定のModelとPresenterを紐づけます。
        /// </summary>
        /// <param name="model">紐づけるModel</param>
        /// <param name="presenterGameObject">Presenterを保持するGameObject</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <typeparam name="TModel">Model型</typeparam>
        /// <typeparam name="TPresenter">Presenter型</typeparam>
        /// <returns>非同期処理のタスク</returns>
        public static async UniTask LinkAsync<TModel, TPresenter>(TModel model, GameObject presenterGameObject, CancellationToken token) where TPresenter : IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            var presenter = presenterGameObject.GetComponentSafely<TPresenter>();
            await LinkAsync(model, presenter, token);
        }

        /// <summary>
        /// 指定のModelとPresenterを紐づけます。
        /// </summary>
        /// <param name="model">紐づけるModel</param>
        /// <param name="presenter">紐づけるPresenter</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <typeparam name="TModel">Model型</typeparam>
        /// <typeparam name="TPresenter">Presenter型</typeparam>
        /// <returns>非同期処理のタスク</returns>
        public static async UniTask LinkAsync<TModel, TPresenter>(TModel model, TPresenter presenter, CancellationToken token) where TPresenter : IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (presenter == null) throw new ArgumentNullException(nameof(presenter));

            await presenter.InitializeModelLinkAsync(model, token);
            await presenter.LinkMvrpRxAsync(token);
        }
    }
}
