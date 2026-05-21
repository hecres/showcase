using System;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Interfaces;
using R3;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Supporters
{
    /// <summary>
    /// UI遷移を別コンポーネントに連動させるクラス
    /// </summary>
    [RequireComponent(typeof(IUiTransitioner))]
    public class UiTransitionLinker : HecUnityMonoBehaviourBase
    {
        [SerializeField] private UiTransitionerBase linkedTransitioner;

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        private void Start()
        {
            if (linkedTransitioner == null) throw new InvalidOperationException();

            var transitioner = this.GetComponentSafely<IUiTransitioner>();
            linkedTransitioner.Hidden.Subscribe(_ => transitioner.HideAsync(destroyCancellationToken).Forget()).AddTo(this);
            linkedTransitioner.Shown.Subscribe(_ => transitioner.ShowAsync(destroyCancellationToken).Forget()).AddTo(this);
        }
    }
}
