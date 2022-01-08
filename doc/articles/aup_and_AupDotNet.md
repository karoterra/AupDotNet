# プロジェクトファイルと AupDotNet

## プロジェクトファイル
プロジェクトファイル全体を表すのは [AviUtlProject](xref:Karoterra.AupDotNet.AviUtlProject) です。
プロジェクトファイルの中身は以下の様になっています。

<table>
    <tr>
        <th>セクション</th>
        <th>AupDotNet</th>
        <th>説明</th>
    </tr>
    <tr>
        <td>ヘッダー</td>
        <td>-</td>
        <td><code>AviUtl ProjectFile version 0.18\0</code></td>
    </tr>
    <tr>
        <td>EditHandle</td>
        <td><a href="/api/Karoterra.AupDotNet.EditHandle.html">EditHandle</a></td>
        <td>編集中のファイルに関する基本データなどが入っている。</td>
    </tr>
    <tr>
        <td>フッター</td>
        <td>-</td>
        <td><code>AviUtl ProjectFile version 0.18\0</code></td>
    </tr>
    <tr>
        <td>FilterProject</td>
        <td><a href="/api/Karoterra.AupDotNet.FilterProject.html">FilterProject</a></td>
        <td>
            フィルタプラグインが <code>func_project_save</code> で保存するデータ。
            拡張編集のタイムラインの情報などはここ。
        </td>
    </tr>
    <tr>
        <td colspan=3>以降ファイル終端まで FilterProject の繰り返し</td>
    </tr>
</table>

## 参考文献
- ePi, [aupファイルの構造](https://scrapbox.io/ePi5131/aupファイルの構造)
