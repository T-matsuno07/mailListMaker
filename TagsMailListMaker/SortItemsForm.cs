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
    /// <summary>
    ///
    /// </summary>
    public partial class SortItemsForm : Form
    {
        /// <summary>
        ///
        /// </summary>
        public bool ResultRetrun;

        /// <summary>
        ///
        /// </summary>
        private List<string> list_Orginal;

        /// <summary>
        ///
        /// </summary>
        private List<string> list_Return;
        public List<string> getResult() { return this.list_Return; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SortItemsForm()
        {
            InitializeComponent();
            list_Orginal = new List<string>();
            list_Return  = new List<string>();

            guiNumUD_MoveNum.Value = 5;
            ResultRetrun = false;
        }



        /// <summary>
        /// 並び替え対象データリスト設定
        /// </summary>
        /// <param name="argList">並び替えを実施する文字列リスト</param>
        public void SetOriginalItemList(List<string> argList)
        {
            // 本フォーム上のリストボックスにおける項目を初期化する
            guiList_SortItemsList.Items.Clear();
            // 本クラスで使用する文字列リストを初期化
            list_Orginal = new List<string>();

            // 引数で受け取ったリストに含まれる文字列を1つずつ処理するループ
            for (int i = 0; i < argList.Count; i++)
            {
                // 本クラスで使用する文字列リストを初期化
                list_Orginal.Add(argList[i]);
                // リストボックスの項目に文字列を追加
                guiList_SortItemsList.Items.Add(argList[i]);
            }

            // フォームにおける数値コントロールの上限値を設定する。

            // 一回のボタン押下で何行移動させるかの上限値を設定
            guiNumUD_MoveNum.Maximum = (list_Orginal.Count - 1);
            // 指定行に移動させる機能における最終行を指定する
            guiNumUD_DirectLine.Maximum = (list_Orginal.Count );
            // ユーザーに上限値を知らせるためのラベルに値を設定
            guiLbl_finallineNum.Text = (list_Orginal.Count ).ToString();
        }

        /// <summary>
        /// フォーム終了(決定)
        /// </summary>
        /// <remarks>
        /// リストボックスで実施した並び替え結果を戻り値用リスト「list_Return」へ
        /// 格納して，本フォームを閉じる
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_SortEnter_Click(object sender, EventArgs e)
        {
            list_Return = new List<string>();
            for (int i = 0; i < guiList_SortItemsList.Items.Count; i++)
            {
                list_Return.Add(guiList_SortItemsList.Items[i].ToString() );
            }
            ResultRetrun = true;
            this.Close();
        }



        /// <summary>
        /// フォーム終了(キャンセル)
        /// </summary>
        /// <remarks>
        /// 本フォームを終了する。
        /// 本フォームで実施した編集内容は全て破棄する
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_SortCancel_Click(object sender, EventArgs e)
        {
            ResultRetrun = false;
            this.Close();
        }

        private void guiBtn_OneMoveUp_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveUpListItem(guiList_SortItemsList);
        }

        private void guiBtn_OneMoveDown_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveDownListItem(guiList_SortItemsList);
        }

        private void guiBtn_LoopMoveUp_Click(object sender, EventArgs e)
        {
            int iloopEnd = (int)guiNumUD_MoveNum.Value;
            for (int i = 0; i < iloopEnd; i++)
            {
                lib_HandleGUIControl.moveUpListItem(guiList_SortItemsList);
            }
        }

        private void guiBtn_LoopMoveDown_Click(object sender, EventArgs e)
        {
            int iloopEnd = (int)guiNumUD_MoveNum.Value;
            for (int i = 0; i < iloopEnd; i++)
            {
                lib_HandleGUIControl.moveDownListItem(guiList_SortItemsList);
            }
        }

        private void guiBtn_MoveDirectLine_Click(object sender, EventArgs e)
        {
            int iTargetLine = (int)guiNumUD_DirectLine.Value;
            lib_HandleGUIControl.moveTargetLeneListItem(guiList_SortItemsList, iTargetLine);
        }
    }
}
