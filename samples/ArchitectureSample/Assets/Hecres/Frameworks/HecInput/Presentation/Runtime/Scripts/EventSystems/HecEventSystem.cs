using System;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.UIBehaviours.EventSystems.Extensions;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using VContainer;

namespace Hecres.Frameworks.HecInput.Presentation.EventSystems
{
    /// <summary>
    /// 独自にカスタマイズしたEventSystemクラス
    /// </summary>
    public class HecEventSystem : IHecEventSystem
    {
        /// <summary>
        /// 選択前の通知
        /// </summary>
        public Observable<GameObject> PreSelect => preSelectStream.Share();

        /// <summary>
        /// 選択後の通知
        /// </summary>
        public Observable<GameObject> PostSelect => postSelectStream.Share();

        /// <summary>
        /// 現在選択中のゲームオブジェクト
        /// </summary>
        public GameObject CurrentSelectedGameObject => EventSystem.current.currentSelectedGameObject;

        private InputSystemUIInputModule UiModule => uiModuleCache ?? (InputSystemUIInputModule)EventSystem.current.currentInputModule;

        private readonly Subject<GameObject> preSelectStream = new();
        private readonly Subject<GameObject> postSelectStream = new();
        private InputSystemUIInputModule uiModuleCache;

        /// <summary>
        /// 選択を解除します。
        /// </summary>
        public void Deselect()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        /// <summary>
        /// 選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        public void SetSelectedGameObject(GameObject selected)
        {
            OnPreProcess(selected);
            EventSystem.current.SetSelectedGameObject(selected);
            OnPostProcess(selected);
        }

        /// <summary>
        /// 選択対象を設定します。
        /// </summary>
        /// <remarks>
        /// 現在と同一オブジェクトが指定された場合は無視します。
        /// </remarks>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        public void SetSelectedGameObjectIgnoreSame(GameObject selected)
        {
            OnPreProcess(selected);
            EventSystem.current.SetSelectedGameObjectIgnoreSame(selected);
            OnPostProcess(selected);
        }

        /// <summary>
        /// 子階層を考慮して選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        public void SetSelectedGameObjectInChildren(GameObject selected)
        {
            OnPreProcess(selected);
            EventSystem.current.SetSelectedGameObjectInChildren(selected);
            OnPostProcess(selected);
        }

        /// <summary>
        /// 子階層を考慮して選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のコンポーネント</param>
        public void SetSelectedGameObjectInChildren(Component selected)
        {
            if (selected == null) throw new ArgumentNullException(nameof(selected));

            OnPreProcess(selected.gameObject);
            EventSystem.current.SetSelectedGameObjectInChildren(selected);
            OnPostProcess(selected.gameObject);
        }

        /// <summary>
        /// 指定したTransformの子孫が選択されているか確認します。
        /// </summary>
        /// <param name="parent">親のTransform</param>
        /// <returns>true: 子孫に選択中のオブジェクトが含まれている / false: 含まれていない</returns>
        public bool IsSelectedChildOf(Transform parent) => EventSystem.current.IsSelectedChildOf(parent);

        /// <summary>
        /// 選択前の処理を行います。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        private void OnPreProcess(GameObject selected)
        {
            preSelectStream.OnNext(selected);
        }

        /// <summary>
        /// 選択後の処理を行います。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        private void OnPostProcess(GameObject selected)
        {
            postSelectStream.OnNext(selected);
        }

        /// <summary>
        /// このクラスのインスタンスを生成し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>生成されたインスタンス</returns>
        public static HecEventSystem InstantiateAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = new HecEventSystem();
            builder.RegisterInstance(instance).As<IHecEventSystem>();

            return instance;
        }

        /// <summary>
        /// UIナビゲーションにおけるリピートの開始判定時間を取得します。
        /// </summary>
        /// <remarks>
        /// リピートとは、十字キーなどを押し続けた時に自動でカーソル移動を行なう挙動のことを指します。
        /// </remarks>
        /// <returns>リピート開始判定時間（秒）</returns>
        public float GetMoveRepeatDelaySeconds() => UiModule.moveRepeatDelay;

        /// <summary>
        /// UIナビゲーションにおけるリピートの間隔を取得します。
        /// </summary>
        /// <remarks>
        /// リピートとは、十字キーなどを押し続けた時に自動でカーソル移動を行なう挙動のことを指します。
        /// </remarks>
        /// <returns>リピート間隔（秒）</returns>
        public float GetMoveRepeatRateSeconds() => UiModule.moveRepeatRate;
    }
}
