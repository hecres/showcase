using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecUI.Inputs.InputActions;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.HierarchySystems.Interfaces;
using UnityEngine.InputSystem;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Bases
{
    public abstract partial class MainSequenceManagerBase : HecUiInputActions.IAppHierarchyActions
    {
        private HecUiInputActions hecUiInputActions;

        /// <summary>
        /// 入力処理を初期化します。
        /// </summary>
        private void InitializeInputActions()
        {
            hecUiInputActions = new HecUiInputActions();
            hecUiInputActions.AppHierarchy.SetCallbacks(this);
        }

        /// <summary>
        /// 入力処理を有効化します。
        /// </summary>
        private void EnableInputActions()
        {
            hecUiInputActions.Enable();
        }

        /// <summary>
        /// 入力処理を無効化します。
        /// </summary>
        private void DisableInputActions()
        {
            hecUiInputActions.Disable();
        }

        /// <summary>
        /// 入力処理のリソースを破棄します。
        /// </summary>
        private void DisposeInputActions()
        {
            hecUiInputActions.Dispose();
        }

        /// <summary>
        /// 戻る入力を処理します。
        /// </summary>
        /// <param name="context">入力のコールバックコンテキスト</param>
        /// <remarks>
        /// Performed段階の場合にのみ処理します。
        /// </remarks>
        public void OnBackInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            var currentFocusTarget = UiFocusSystem.CurrentTarget;
            if (currentFocusTarget == null) return;

            var hierarchicalUi = currentFocusTarget.gameObject.GetComponentInParent<IHierarchicalUi>();
            hierarchicalUi.BackAsync(destroyCancellationToken).Forget();
        }
    }
}
