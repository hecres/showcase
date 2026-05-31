# 画面遷移図

アプリケーション全体の画面遷移フローを図示します。

---

## 全体遷移フロー

```mermaid
graph TD
    Launch["アプリ起動"] --> Title["タイトル画面"]
    Title -->|"コンティニュー"| Home["ホーム画面"]
    Title -->|"ニューゲーム"| Tutorial["初回チュートリアル<br>クエスト"]
    Tutorial -->|"クリア"| Home

    Home -->|"クエスト選択"| QSelect["クエスト選択画面"]
    Home -->|"カードデッキ編集"| CDEdit["カードデッキ編集画面"]
    Home -->|"装具セット編集"| GSEdit["装具セット編集画面"]
    Home -->|"モール"| Mall["モール画面"]
    Home -->|"メッセージ"| MBox["メッセージ閲覧画面"]
    Home -->|"アプリ設定"| Pref["アプリ設定<br>ダイアログ"]

    QSelect -->|"クエスト選択"| QDetail["クエスト詳細画面"]
    QSelect -->|"戻る"| Home

    QDetail -->|"受注"| QExec["クエスト実行画面<br>(バトル)"]
    QDetail -->|"戻る"| QSelect

    QExec -->|"成功 / 失敗 / 放棄"| QResult["クエスト結果画面"]
    QResult -->|"ホームへ戻る"| Home

    CDEdit -->|"プレビュー"| BSim["戦闘シミュレーション画面"]
    CDEdit -->|"戻る"| Home

    BSim -->|"終了"| CDEdit

    GSEdit -->|"戻る"| Home
    Mall -->|"戻る"| Home
    MBox -->|"戻る"| Home
    Pref -->|"閉じる"| Home

    style Title fill:#4a90d9,color:#fff
    style Home fill:#e8a838,color:#fff
    style QSelect fill:#5cb85c,color:#fff
    style QDetail fill:#5cb85c,color:#fff
    style QExec fill:#d9534f,color:#fff
    style QResult fill:#d9534f,color:#fff
    style CDEdit fill:#9b59b6,color:#fff
    style BSim fill:#d9534f,color:#fff
    style GSEdit fill:#9b59b6,color:#fff
    style Mall fill:#3498db,color:#fff
    style MBox fill:#3498db,color:#fff
    style Pref fill:#95a5a6,color:#fff
```

> **仕様補足: 凡例**<br/>
> - 実線矢印: ユーザー操作による遷移

> **仕様補足: シミュレーション→カードデッキ編集の遷移**<br/>
> 戦闘シミュレーション → カードデッキ編集の遷移は、内部的には Home 画面を経由し自動遷移で復帰する。

> **技術補足: シミュレーション→カードデッキ編集の遷移**<br/>
> `HomeManagerArgs(CardDeckEditManagerArgs)` のネスト引数パターンで実現する。

---

## クエスト攻略フロー（詳細）

```mermaid
graph TD
    H["ホーム画面"] --> QS["クエスト選択画面"]
    QS --> QD["クエスト詳細画面"]

    subgraph "クエスト詳細画面"
        QD_Info["クエスト情報確認"]
        QD_Brief["ブリーフィング<br>会話再生"]
        QD_Equip["装備編集へ"]
        QD_Take["クエスト受注"]
    end

    QD --> QD_Info
    QD_Info --> QD_Brief
    QD_Info --> QD_Equip
    QD_Info --> QD_Take

    QD_Take --> QE["クエスト実行画面"]

    subgraph "クエスト実行（バトル）"
        QE_Start["クエスト開始<br>アナウンス"]
        QE_Wave["ウェーブ開始<br>アナウンス"]
        QE_Battle["戦闘"]
        QE_Progress["進行<br>アナウンス"]
        QE_End["クエスト終了<br>アナウンス"]
    end

    QE --> QE_Start --> QE_Wave --> QE_Battle
    QE_Battle --> QE_Progress --> QE_Wave
    QE_Battle --> QE_End

    QE_End --> QR["クエスト結果画面"]
    QR --> H

    style H fill:#e8a838,color:#fff
    style QS fill:#5cb85c,color:#fff
    style QD fill:#5cb85c,color:#fff
    style QE fill:#d9534f,color:#fff
    style QR fill:#d9534f,color:#fff
```

---

## カードデッキ編集・プレビューフロー

```mermaid
graph TD
    H["ホーム画面"] --> CDE["カードデッキ編集画面"]

    subgraph "カードデッキ編集"
        CDE_Storage["カードストレージ<br>（所持カード一覧）"]
        CDE_Deck["カードデッキスロット<br>（編成）"]
        CDE_Move["カード移動<br>ストレージ ⇔ カードデッキ"]
        CDE_Preview["プレビュー"]
    end

    CDE --> CDE_Storage
    CDE --> CDE_Deck
    CDE_Storage --> CDE_Move --> CDE_Deck
    CDE_Deck --> CDE_Move --> CDE_Storage
    CDE --> CDE_Preview

    CDE_Preview --> BS["戦闘シミュレーション画面"]

    subgraph "戦闘シミュレーション"
        BS_Battle["仮想戦闘"]
        BS_Menu["ポーズメニュー<br>(ダイアログ)"]
        BS_End["シミュレーション終了"]
    end

    BS --> BS_Battle
    BS_Battle -.-> BS_Menu
    BS_Menu --> BS_End

    BS_End -->|"終了"| CDE

    style H fill:#e8a838,color:#fff
    style CDE fill:#9b59b6,color:#fff
    style BS fill:#d9534f,color:#fff
```

> **仕様補足: シミュレーション終了時の遷移**<br/>
> シミュレーション終了 → カードデッキ編集への遷移は、内部的には Home 画面を経由した自動遷移で実現される。<br/>
> ユーザーからは直接カードデッキ編集画面に戻るように見える。

---

## UIレイヤー構造

画面は複数のキャンバスレイヤーで構成されています。数値が小さいほど前面に表示されます。

```mermaid
graph TB
    subgraph "描画順序（上が前面）"
        L2["画面遷移演出 (200-201)<br>フェード / フラッシュ"]
        L3["ダイアログ (300-304)<br>設定画面、ポーズメニュー"]
        L4["トースト (400-402)<br>通知メッセージ"]
        L5["常駐UI (1000-1003)<br>アプリ全体で共有"]
        L6["シーンUI (2000-6004)<br>各画面固有のUI"]
    end

    L2 --- L3 --- L4 --- L5 --- L6

    style L2 fill:#e67e22,color:#fff
    style L3 fill:#f39c12,color:#fff
    style L4 fill:#2ecc71,color:#fff
    style L5 fill:#3498db,color:#fff
    style L6 fill:#9b59b6,color:#fff
```

---

## 各画面の仕様書へのリンク

| 画面名 | 説明 | 仕様書 |
|--------|------|--------|
| タイトル画面 | アプリ起動後の最初の画面。ロゴ表示後にニューゲーム/コンティニューを選択 | [タイトル画面（非公開資料）](private-notice.md) |
| ホーム画面 | メインハブ画面 | [ホーム画面（非公開資料）](private-notice.md) |
| クエスト選択画面 | チャプター/クエスト一覧 | [クエスト選択（非公開資料）](private-notice.md) |
| クエスト詳細画面 | クエスト情報の詳細確認・受注 | [クエスト詳細（非公開資料）](private-notice.md) |
| クエスト実行画面 | バトルプレイ画面 | [クエスト実行（非公開資料）](private-notice.md) |
| インゲーム画面 | バトル画面の全体構成 | [インゲーム画面](ui/screens/spec-ingame.md) |
| クエスト結果画面 | クリア報酬の確認 | [クエスト結果（非公開資料）](private-notice.md) |
| カードデッキ編集画面 | カードデッキの構築 | [カードデッキ編集](ui/screens/spec-card-deck-edit.md) |
| 装具セット編集画面 | 装具の編成 | [装具セット編集（非公開資料）](private-notice.md) |
| モール画面 | ゲーム内ショップ | [モール（非公開資料）](private-notice.md) |
| メッセージ閲覧画面 | 受信メッセージの閲覧 | [メッセージ閲覧（非公開資料）](private-notice.md) |
| 会話画面 | 会話データのオーバーレイ再生（HecTalk） | [会話画面（非公開資料）](private-notice.md) |
| 戦闘シミュレーション画面 | カードデッキのプレビュー戦闘 | [戦闘シミュレーション（非公開資料）](private-notice.md) |
| アプリ設定ダイアログ | 各種設定の変更 | [アプリ設定（非公開資料）](private-notice.md) |
