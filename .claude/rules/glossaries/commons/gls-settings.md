# 設定・パラメータ用語集

## 設定・パラメータ接尾辞

「設定」とくくられがちな用語について以下のように使い分ける。
C#/.NETの慣例（Settings/Options が主流）とはやや異なるが、英語としての語義の正確さを優先して採用している。
| 英語 | 日本語 | 説明 | 使用例 |
|------|--------|------|------|
| Preference | ユーザー設定 | ユーザーが変更するアプリ設定。ランタイム永続化対象 | `HecPreferenceBase`（音量、描画品質、入力マッピング） |
| Config | 構成パラメータ | コード内部の非永続構成パラメータ | `ManualDiConfig`, `DialogUiBase.ConfigBase`（DI引数、ダイアログ初期化引数） |
| Settings | 開発ツール設定 | Editor・開発ツールの設定 | `HecBuildEnvironmentSettings`（ビルド環境設定、プロジェクト設定） |
| Tuning | ゲームバランス調整パラメータ | プランナーが微調整するゲームバランスパラメータ。アセット永続化対象 | `DuelistPieceBrainTuning`（使用回数上限、リロード硬直秒数） |
| Cosmetic | 演出調整パラメータ | TA・UIデザイナーが微調整する演出・UI調整パラメータ。アセット永続化対象 | `CardDeckSlotCosmetic`, `CardDeckScrollCosmetic`（シェイク強度、スクロール速度、イージング） |

## 設定項目

| 英語 | 日本語 | 説明 | 使用例 |
|------|--------|------|------|
| Graphic | 描画設定 | 描画品質の設定項目 | — |
| Audio | 音響設定 | BGM/効果音の設定項目 | — |
| InputGamepad | ゲームパッド操作設定 | ゲームパッドの設定項目 | — |
| InputKeyboard | キーボード操作設定 | キーボードの設定項目 | — |
