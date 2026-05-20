# Third-Party Notices

本 Unity プロジェクト (ArchitectureSample) は下記の第三者ソフトウェアに依存しています。

**これらのソースコード・バイナリは本リポジトリには同梱されておらず**、Unity Package Manager (UPM) または NuGet を介して、利用時に各自の環境へ取り込まれます。各依存物は本リポジトリのライセンスとは独立に、それぞれのライセンスに従って配布されます。

各ライブラリのライセンス全文は [`licenses/`](licenses/) 配下に同梱しています。

---

## Unity Package Manager (UPM) 経由

| パッケージ | バージョン | ライセンス | 著作権表示 | 配布元 |
|---|---|---|---|---|
| uLoopMCP | 2.1.3 | [MIT](licenses/uloopmcp/LICENSE) | Copyright (c) 2025 Masamichi Hatayama | https://github.com/hatayama/uLoopMCP |
| VContainer | 1.18.0 | [MIT](licenses/vcontainer/LICENSE) | Copyright (c) 2020 hadashiA | https://github.com/hadashiA/VContainer |
| UniTask | git HEAD | [MIT](licenses/unitask/LICENSE) | Copyright (c) 2019 Yoshifumi Kawai / Cysharp, Inc. | https://github.com/Cysharp/UniTask |
| R3 (Unity 統合) | git HEAD | [MIT](licenses/r3/LICENSE) | Copyright (c) 2024 Cysharp, Inc. | https://github.com/Cysharp/R3 |
| NuGetForUnity | git HEAD | [MIT](licenses/nugetforunity/LICENSE) | Copyright (c) 2018 Patrick McCarthy | https://github.com/GlitchEnzo/NuGetForUnity |

正確に固定されたコミットハッシュは [`Packages/packages-lock.json`](Packages/packages-lock.json) を参照してください。

## NuGet 経由（NuGetForUnity が `Assets/Packages/` に解決、`.gitignore` 除外済み）

| パッケージ | バージョン | ライセンス | 著作権表示 | 配布元 |
|---|---|---|---|---|
| R3 | 1.3.1 | [MIT](licenses/r3/LICENSE) | Copyright (c) 2024 Cysharp, Inc. | https://www.nuget.org/packages/R3 |
| Microsoft.Bcl.AsyncInterfaces | 6.0.0 | [MIT](licenses/dotnet-runtime/LICENSE) | Copyright (c) .NET Foundation and Contributors | https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces |
| Microsoft.Bcl.TimeProvider | 8.0.0 | [MIT](licenses/dotnet-runtime/LICENSE) | Copyright (c) .NET Foundation and Contributors | https://www.nuget.org/packages/Microsoft.Bcl.TimeProvider |
| System.ComponentModel.Annotations | 5.0.0 | [MIT](licenses/dotnet-runtime/LICENSE) | Copyright (c) .NET Foundation and Contributors | https://www.nuget.org/packages/System.ComponentModel.Annotations |
| System.Threading.Channels | 8.0.0 | [MIT](licenses/dotnet-runtime/LICENSE) | Copyright (c) .NET Foundation and Contributors | https://www.nuget.org/packages/System.Threading.Channels |

パッケージ一覧は [`Assets/packages.config`](Assets/packages.config) を参照してください。

> Microsoft.Bcl.* および System.* は .NET Foundation が dotnet/runtime monorepo にてメンテナンスしており、同一の MIT ライセンスが適用されます。
