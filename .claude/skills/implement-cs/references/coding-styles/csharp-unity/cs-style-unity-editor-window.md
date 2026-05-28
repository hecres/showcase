---
paths:
  - "**/*.cs"
---

# Unity EditorWindow 構成規約

EditorWindow サブクラスの配置・構成に関するルールを定める。

---

## 1. ディレクトリ構成

EditorWindow サブクラスは、機能ディレクトリ直下ではなく `EditorWindows/` サブディレクトリに配置する。

```
<機能ディレクトリ>/
├── EditorWindows/
│   └── XxxEditorWindow.cs
└── XxxEditorMenu.cs
```

### 正例・誤例

```
// ✅ 良い例 — EditorWindows/ サブディレクトリに配置
AnimationClipSelectors/
├── EditorWindows/
│   └── AnimationClipHierarchyEditorWindow.cs
└── AnimationClipSelectorEditorMenu.cs

// ❌ 悪い例 — 機能ディレクトリ直下に配置
AnimationClipSelectors/
└── AnimationClipHierarchyEditorWindow.cs
```

### EditorWindows/ に配置するもの

- EditorWindow サブクラス（`.cs`）
- UXML レイアウトファイル（`.uxml`）
- USS スタイルシートファイル（`.uss`）

---

## 2. MenuItem の分離

EditorWindow を開く `[MenuItem]` は、EditorWindow クラス内に定義せず、専用の `*EditorMenu.cs` 静的クラスに分離する。

### EditorMenu クラスの配置

`EditorWindows/` の**親ディレクトリ**（機能ディレクトリ直下）に配置する。

### EditorMenu クラスの命名

`<機能名>EditorMenu` とする。

### 正例

```csharp
// AnimationClipSelectorEditorMenu.cs（機能ディレクトリ直下）
internal static class AnimationClipSelectorEditorMenu
{
    [MenuItem("Hecres/Animation/Clip Hierarchy")]
    private static void OpenAnimationClipHierarchyEditorWindow()
    {
        var window = EditorWindow.GetWindow<AnimationClipHierarchyEditorWindow>();
        window.titleContent = new GUIContent("Clip Hierarchy");
        window.minSize = new Vector2(300, 200);
        window.Show();
    }
}
```

### 誤例

```csharp
// ❌ EditorWindow クラス内に MenuItem を定義
internal sealed class AnimationClipHierarchyEditorWindow : EditorWindow
{
    [MenuItem("Hecres/Animation/Clip Hierarchy")]
    private static void Open() { ... }
}
```

---

## 3. 既存ウィンドウ拡張との区別

| カテゴリ | 配置先 | 説明 |
|---------|--------|------|
| 独立 EditorWindow | `EditorTools/<機能名>/EditorWindows/` | EditorWindow サブクラスとして独自ウィンドウを作成 |
| 既存ウィンドウ拡張 | `EditorWindowExtensions/<機能名>/` | 既存の Unity EditorWindow に UI を注入・拡張 |

同じ機能領域で両方が存在する場合でも、それぞれのカテゴリに分けて配置する。
