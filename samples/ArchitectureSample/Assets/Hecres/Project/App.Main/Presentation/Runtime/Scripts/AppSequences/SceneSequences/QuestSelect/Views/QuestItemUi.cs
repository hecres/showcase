using System;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestSelect.Views
{
    /// <summary>
    /// クエスト選択一覧で使用するクエスト項目UIクラス
    /// </summary>
    public class QuestItemUi : HecUnityMonoBehaviourBase
    {
        /// <summary>
        /// 選択要求時に通知
        /// </summary>
        /// <remarks>
        /// 通知値は表示中のクエストデータです。
        /// </remarks>
        public Observable<QuestData> SelectRequested => selectButton.OnClickAsObservable().Select(_ => questData).Share();

        [SerializeField] private Text displayNameLabel;
        [SerializeField] private Button selectButton;

        private QuestData questData;

        /// <summary>
        /// 表示するクエストデータを設定します。
        /// </summary>
        /// <param name="quest">設定するクエストデータ</param>
        public void Setup(QuestData quest)
        {
            if (quest == null) throw new ArgumentNullException(nameof(quest));

            questData = quest;
            displayNameLabel.text = quest.DisplayName;
        }
    }
}
