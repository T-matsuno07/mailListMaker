using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TagsMailListMaker
{
    static class lib_XmlLINQ
    {
		/// <summary>
		/// Xml情報フォルダ読み込み処理
		/// </summary>
		/// <param name="argDS">読み込み情報格納先データセット</param>
		/// <param name="importFolderPath">読み込み対象フォルダ</param>
        static public void importXmlFiles(out DataSet argDS, string importFolderPath)
        {
            DataSet   oneXmlLoader;  // Xml 読み込み作業用DataSet
            DataTable oneXmlTable;   // work用DataTable
            string strSchemaFileName; // Xsdスキーマファイルフルパス
            string strRecordFileName; // Xmlレコードファイルフルパス
            string[] fileNameList;   // ファイルパスリスト

            argDS = new DataSet();  // 読込先Datasetを初期化

            // xsdファイルにはテーブルにどのような列が存在するのかを格納している
            // 第2引数で指定されたフォルダに格納されているxsdファイルのリストを取得
            fileNameList = System.IO.Directory.GetFiles(importFolderPath, "*.xsd", System.IO.SearchOption.AllDirectories);

            // フォルダ内のスキーマファイル分ループ
            for (int iLoop = 0; iLoop < fileNameList.Length; iLoop++)
            {
                // xsdファイル名をリストから取得
                strSchemaFileName = fileNameList[iLoop];
                // xmlファイル名を取得
                strRecordFileName = strSchemaFileName.Replace(".xsd", ".xml");
                // 作業用Datasetを初期化
                oneXmlLoader = new DataSet();
                // 作業用DataTabaleを初期化
                oneXmlTable = new DataTable();
                // スキーマ情報を読み込み
                oneXmlLoader.ReadXmlSchema(strSchemaFileName);
                // レコードファイルが存在するかをチェック
                if (System.IO.File.Exists(strRecordFileName) == true)
                {
                    // レコードファイルを読み込み
                    oneXmlLoader.ReadXml(strRecordFileName);
                }
                // 作業用DataTableに退避
                oneXmlTable = oneXmlLoader.Tables[0].Copy();
                // 戻り値用DataSetへ格納
                argDS.Tables.Add(oneXmlTable);
            }
        }

        /// <summary>
        /// Xml情報フォルダ書き込み処理
        /// </summary>
        /// <param name="argDS">書き込み対象データセット</param>
        /// <param name="exportFolderPath">書き込み先フォルダのパスs</param>
        static public void exportXmlFiles(DataSet argDS, string exportFolderPath)
        {
            DataTable oneXmlTable;   // work用DataTable
            string strTableName; // Table名称
            string[] fileNameList;   // ファイルパスリスト
            try
            {
                // 第2引数で指定されたフォルダに格納されているxsdファイルのリストを取得
                fileNameList = System.IO.Directory.GetFiles(exportFolderPath, "*", System.IO.SearchOption.AllDirectories);
                // 保存先フォルダに存在する既存のファイルを削除する
                for (int iLoop = 0; iLoop < fileNameList.Length; iLoop++)
                {
                    // 1ファイルずつ削除していく
                    System.IO.File.Delete(fileNameList[iLoop] );
                }

                // データセットに含まれるテーブル単位でファイルへの書き込みを実施する
                for (int iTableLoop = 0; iTableLoop < argDS.Tables.Count; iTableLoop++)
                {
                    // データセットからテーブル情報を抽出
                    oneXmlTable = argDS.Tables[iTableLoop];
                    // データテーブルの名称を抽出
                    strTableName = oneXmlTable.TableName;
                    // スキーマ(カラム)情報をファイル(.xsd)に書き込み
                    oneXmlTable.WriteXmlSchema(exportFolderPath + "\\" + strTableName + ".xsd");
                    // レコード情報をファイル(.xml)に書き込み
                    oneXmlTable.WriteXml(exportFolderPath + "\\" + strTableName + ".xml");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xmlファイル書き出し中に例外処理が発生しました。" + ex.ToString());
            }
        }

        /// <summary>
        /// LINQ SQL Select実行処理(DataSet引数)
        /// </summary>
        /// <remarks>
        /// 第一引数で受け取ったDataSet(データベース)に対してSQLのSelect文を実行する
        /// 第三引数のWhere句や第四引数のorderBy句は省略可能。
        /// 第三引数を省略した場合，全てのレコードを抽出する
        /// 第四引数を省略した場合，テーブルに格納されているレコードの順番に抽出する
        /// where句にて，テーブルのカラム名称を指定する場合には，カラム名称を[]で囲むことを推奨する。
        /// Where句にて，条件に文字列を指定する場合，シングルクォーテーション''で囲むこと(必須)。
        /// Where句にて，条件に日付/時刻を指定する場合，シャープ##で囲むこと(必須)。
        ///  Where句記載例 : "[分類] = '哺乳類' And [サイズ] = 100"
        /// orderBy句を連結(複数のカラムを用いてソート)したい場合は，カンマ,を用いる。
        /// レコードを昇順に並び替える場合は ASC と記載  例 : "[寿命] ASC"
        /// レコードを降順に並び替える場合は DESC と記載  例 : "[寿命] DESC"
        /// 昇順・降順の記載を省略した場合は，昇順となる。
        /// </remarks>
        /// <param name="argDS">SQL実行対象のデータベース/LINQを発行するデータセット</param>
        /// <param name="argFrom">[from句]テーブル名称</param>
        /// <param name="argWhere">[where句]抽出レコード条件</param>
        /// <param name="argSortBy">[sortby句]レコード並び替え条件</param>
        /// <returns>Select文実行結果を格納したレコード配列</returns>
        static public DataRow[] seleceLINQ(DataSet argDS, string argFrom, string argWhere = "", string argSortBy = "")
        {
            DataRow[] rtnQuery;  // SQL Select 実施結果格納用変数
            // from区で指定されたテーブルが存在するか否かをチェック
            if ( argDS.Tables.Contains(argFrom) == false ){
                // テーブルが存在しない場合，例外をthrowしてメソッド終了
                throw new ApplicationException("seleceLINQメソッドで例外処理が発生しました。\r\n " +
                    "第2引数で指定した名称のテーブルを含みません。\r\n" +
                    "argFrom = " + argFrom);

            }

            // SQL文実行
            rtnQuery = argDS.Tables[argFrom].Select(argWhere, argSortBy);
            return rtnQuery;
        }

        /// <summary>
        /// LINQ SQL Delete実行処理(DataSet引数)
        /// </summary>
        /// <remarks>
        /// 第一引数で受け取ったDataSet(データベース)に対してSQLのDelete文を実行する。
        /// 第三引数で指定した条件に一致する行を削除する。
        /// 第三引数のWhereは省略可能。第三引数を省略した場合，全てのレコードを抽出する
        /// where句にて，テーブルのカラム名称を指定する場合には，カラム名称を[]で囲むことを推奨する。
        /// Where句にて，条件に文字列を指定する場合，シングルクォーテーション''で囲むこと(必須)。
        /// Where句にて，条件に日付/時刻を指定する場合，シャープ##で囲むこと(必須)。
        ///  Where句記載例 : "[分類] = '哺乳類' And [サイズ] = 100"
        /// </remarks>
        /// <param name="argForm">[from句]テーブル名称</param>
        /// <param name="argWhere">[where句]削除レコード条件</param>
        static public void deleteLINQ(DataSet argDS, string argFrom, string argWhere = "")
        {
            DataRow[] deleteQuery;  // SQL文実行結果格納変数

            // from句で指定されたテーブルが存在するかをチェック
            if (argDS.Tables.Contains(argFrom) == false)
            {
                // 指定されたテーブルが存在しないため，例外をthrowしてメソッド終了
                throw new ApplicationException("deleteLINQメソッドで例外処理が発生しました。\r\n " +
                    "第2引数で指定した名称のテーブルを含みません。\r\n" +
                    "argFrom = " + argFrom);

            }

            // SQL文を用いて条件に一致するレコードを抽出
            deleteQuery = argDS.Tables[argFrom].Select(argWhere);

            // 1レコードずつ削除するループ処理
            for (int iLoop = 0; iLoop < deleteQuery.Length; iLoop++)
            {
                // 1レコードを削除
                deleteQuery[iLoop].Delete();
            }

        }


    }
}
