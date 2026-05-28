# 実装後検証（uloop）

コンパイルチェック通過後、レビュー前に実施する。実装内容に応じて該当する検証項目をAIが判断し、自発的に実行する。

## 判断と実行の原則

- 実装内容から該当する検証項目を判断し、該当するものをすべて実行する
- 該当しない項目はスキップしてよい（スキップ理由の報告は不要）
- 検証で問題を検出した場合、自力で修正を試みる。修正不能な場合のみユーザーに報告する
- ユーザーが明示的に指示しなくても、該当する検証は自発的に実行する

## 検証項目

### 1. コンソール警告チェック

該当条件: 全実装
コマンド: `uloop get-logs --log-type Warning`
確認観点:
- 実装に起因する新規警告が出ていないか
- 非推奨APIの使用警告がないか

### 2. EditorWindow / カスタムInspector の表示確認

該当条件: EditorWindow・カスタムInspector・PropertyDrawer等のエディタ拡張UIを実装・変更した場合
手順:
1. 対象ウィンドウを開く（`uloop execute-menu-item` または `uloop execute-dynamic-code`）
2. スクリーンショットを撮影する（`uloop screenshot --window-name <対象ウィンドウ名>`）
3. スクリーンショットを目視確認し、以下を検証する
   - UIレイアウトが意図通りか（要素の配置・間隔・整列）
   - ラベルや表示テキストが正しいか
   - 要素の切れ・はみ出し・重なりがないか
   - 既存ウィンドウへのUI注入・変更の場合、元のウィンドウが持つ機能やUI要素が損なわれていないか（ボタン・入力欄の消失、操作不能化、表示の遮蔽等）

### 3. Prefab / シーンのヒエラルキー検証

該当条件: Prefabやシーン上のGameObject構成を変更した場合
コマンド: `uloop get-hierarchy --root-path <対象のルートパス>`
確認観点:
- 追加・変更したGameObjectが意図した階層に存在するか
- コンポーネントの付与漏れがないか

### 4. EditMode テスト実行

該当条件: 変更対象に対応するEditModeテストが存在する場合
コマンド: `uloop run-tests --test-mode EditMode --filter-type exact --filter-value <テストクラス名>`
確認観点:
- 既存テストが全件パスするか
- 新規追加したテストがパスするか

### 5. PlayMode 動作確認

該当条件: ランタイム挙動の変更で、PlayModeで簡易確認が有効な場合（ユーザーから動作確認の指示があった場合のみ）
手順:
1. `uloop control-play-mode --action Play` でプレイモード開始
2. `uloop screenshot --window-name Game` でゲーム画面を撮影
3. `uloop get-logs --log-type Error` でランタイムエラーを確認
4. `uloop control-play-mode --action Stop` でプレイモード終了
注意: PlayMode検証はユーザーの明示的指示がある場合のみ実行する。自発的には実行しない
