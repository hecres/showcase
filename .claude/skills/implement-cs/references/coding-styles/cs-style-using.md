---
paths:
  - "**/*.cs"
---

# C# 名前空間・using

名前空間と using ディレクティブに関するルールを定める。

---

## 1. 名前空間

**ブロック形式 namespace** を使用する（`namespace Xxx { }`）。

---

## 2. using ディレクティブの並び順

`System.*` の using を先頭に配置し、以降はアルファベット順に並べる。using グループ間に空行を入れない。

```csharp
// ✅ 良い例
using System;
using System.Collections.Generic;
using System.Threading;
using ThirdParty.Library;

namespace MyProject.App.Main.UseCase
{
    // ...
}
```

