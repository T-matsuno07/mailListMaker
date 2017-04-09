using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TagsMailListMaker
{
    static class lib_HandleGUIControl
    {

        /// <summary>
        /// リストボックス内アイテム移動(上)
        /// </summary>
        /// <returns>
        /// 引数で受け取ったリストボックスで選択中の項目を一つ上へ移動する
        ///</remarks>
        /// <param name="argLB">アイテムの並び替えを実施するリストボックス</param>
        static public void moveUpListItem(ListBox argLB)
        {
            List<int> lstMoveTargets; // 選択中の項目のインデックスリスト
            int iLimitIndex = 0;      // 上へ移動可能なインデックスの境界
            int iMotoIndex;
            // 選択中の項目数をチェック
            if (argLB.SelectedItems.Count == 0)
            {
                // 選択中の項目が存在しない場合，処理を終了
                return;
            }

            // 選択中のインデックスを格納するリストを初期化
            lstMoveTargets = new List<int>();

            // 選択中のインデックスをリストに退避する
            // 選択中の項目が複数ある場合，初期状態を退避してから移動を実施しないと
            // 移動する行がずれる(処理が複雑化する)
            for (int iLoop = 0; iLoop < argLB.SelectedItems.Count; iLoop++)
            {
                // 選択中の項目のインデックスを取得
                iMotoIndex = argLB.SelectedIndices[iLoop];
                // 対象のインデックスが移動可能(上の行が存在する)かをチェックする
                if (iLimitIndex < iMotoIndex)
                {
                    // 移動させるインデックスの値をリストに追加する
                    lstMoveTargets.Add(iMotoIndex);
                }else{
                    // iLimitIndexは基本的には0。つまり，最初の要素は移動できない。
                    // [0,1,2]と選択された状態で，全てを移動させないようにするため，
                    // 0行目が移動できなかったことを保存しておくことにより，1行目も移動させない
                    iLimitIndex = iMotoIndex +1;
                }
            }

            // 移動対象インデックスを格納しているリストの要素を一つずつ処理
            for (int iLoop = 0; iLoop < lstMoveTargets.Count; iLoop++)
            {
                // 移動元のインデックスを取得
                iMotoIndex = lstMoveTargets[iLoop];
                // 移動する文字列を退避
                string tmp = argLB.Items[iMotoIndex].ToString();
                // 元の場所のアイテムを削除
                argLB.Items.RemoveAt(iMotoIndex);
                // 移動先にアイテムを挿入
                argLB.Items.Insert(iMotoIndex - 1, tmp);
            }

            // 選択されていた項目を再度選択状態にする
            // (移動すると選択状態が解除されてしまうので，選択しなおす)
            for (int iLoop = 0; iLoop < lstMoveTargets.Count; iLoop++)
            {
                // 最初に退避したインデックスのそれぞれ一つ上の行を選択状態にする
                argLB.SetSelected(lstMoveTargets[iLoop] - 1, true);
            }


        }

        /// <summary>
        /// リストボックス内アイテム移動(下)
        /// </summary>
        /// <returns>
        /// 引数で受け取ったリストボックスで選択中の項目を一つ下へ移動する
        ///</remarks>
        /// <param name="argLB">アイテムの並び替えを実施するリストボックス</param>
        static public  void moveDownListItem(ListBox argLB)
        {
            List<int> lstMoveTargets; // 選択中の項目のインデックスリスト
            int iLimitIndex = 0;      // 下へ移動可能なインデックスの境界

            int iMotoIndex;
            // 選択中の項目数をチェック
            if (argLB.SelectedItems.Count == 0)
            {
                // 選択中の項目が存在しない場合，処理終了
                return;
            }

            // 移動可能な末尾インデックスを初期化
            // この状態で一番最後の要素は下へ移動できないことを意味している
            iLimitIndex  = argLB.Items.Count - 1;
            lstMoveTargets = new List<int>();

            // 選択中のインデックスをリストに退避する
            // 選択中の項目が複数ある場合，初期状態を退避してから移動を実施しないと
            // 移動する行がずれる(処理が複雑化する)
            for (int iLoop = argLB.SelectedItems.Count -1  ; 0 <= iLoop ; iLoop--)
            {
                iMotoIndex = argLB.SelectedIndices[iLoop];
                if (iMotoIndex < iLimitIndex)
                {
                    // 移動対象インデックスの値をリストに追加する
                    lstMoveTargets.Add(iMotoIndex);
                }else
                {
                    iLimitIndex = iMotoIndex - 1;
                }
            }

            for (int iLoop = 0; iLoop < lstMoveTargets.Count; iLoop++)
            {
                iMotoIndex = lstMoveTargets[iLoop];
                string tmp = argLB.Items[iMotoIndex].ToString();
                argLB.Items.Insert(iMotoIndex + 2, tmp);
                argLB.Items.RemoveAt(iMotoIndex);

            }

            for (int iLoop = 0; iLoop < lstMoveTargets.Count; iLoop++)
            {
                argLB.SetSelected(lstMoveTargets[iLoop] + 1, true);
            }
        }

        static public void moveTargetLeneListItem(ListBox argLB, int argLine){
            List<int> lstMoveTargets; // 選択中の項目のインデックスリスト

            int iMotoIndex;
            // 選択中の項目数をチェック
            if (argLB.SelectedItems.Count == 0)
            {
                // 選択中の項目が存在しない場合，処理終了
                return;
            }

            // 移動可能な末尾インデックスを初期化
            // この状態で一番最後の要素は下へ移動できないことを意味している
            lstMoveTargets = new List<int>();

            // 選択中のインデックスをリストに退避する
            // 選択中の項目が複数ある場合，初期状態を退避してから移動を実施しないと
            // 移動する行がずれる(処理が複雑化する)
            for (int iLoop = argLB.SelectedItems.Count - 1; 0 <= iLoop; iLoop--)
            {
                iMotoIndex = argLB.SelectedIndices[iLoop];
                // 移動対象インデックスの値をリストに追加する
                lstMoveTargets.Add(iMotoIndex);
            }

            for (int iLoop = 0; iLoop < lstMoveTargets.Count; iLoop++)
            {
                iMotoIndex = lstMoveTargets[iLoop];
                string tmp = argLB.Items[iMotoIndex].ToString();
                argLB.Items.Insert(argLine, tmp);
                if (argLine < iMotoIndex)
                {
                    argLB.Items.RemoveAt(iMotoIndex+1);
                }
                else
                {
                    argLB.Items.RemoveAt(iMotoIndex);
                }

            }

        }

        /// <summary>
        /// リストボックス内アイテム削除
        /// </summary>
        /// <returns>
        /// 引数で受け取ったリストボックスで選択中の項目を削除する
        ///</remarks>
        /// <param name="argLB">アイテムの削除を実施するリストボックス</param>
        static public void deleteSelectedItem(ListBox argLB)
        {
            // 選択中の項目がなくなるまでループ
            while (argLB.SelectedItems.Count != 0)
            {
                // 選択中の項目の中で最初の項目を削除
                // 次ループ時は，次点の選択中の項目が先頭要素となる
                argLB.Items.RemoveAt(argLB.SelectedIndices[0]);
            }
        }


        /// <summary>
        /// リストボックス選択中アイテムを他リストへコピー
        /// </summary>
        /// <param name="argLBMoto">コピー元のリストボックス</param>
        /// <param name="argLBTarget">コピー先のリストボックス</param>
        /// <param name="argIndex"></param>
        static public void selecetedItemCopyListBox2ListBox(ListBox argLBMoto, ListBox argLBTarget, bool argArrowOverLap = false)
        {
            string workAddName;
            

            // コピー元のリストボックスにおいて，選択中のアイテムが存在するか？
            if (argLBMoto.SelectedItems.Count == 0)
            {
                // 選択中のアイテムが存在しない場合，処理終了
                return;
            }

            // 選択中の項目を1つずつ処理するループ処理
            for (int i = 0; i < argLBMoto.SelectedItems.Count; i++)
            {
                workAddName = argLBMoto.SelectedItems[i].ToString();
                // コピーの際に，アイテムの重複を許可するか否かで分岐
                if (argArrowOverLap == true)
                {
                    // 重複を許可する場合の処理
                    // コピー元で選択中の1項目をコピー先のリストへ追加
                    argLBTarget.Items.Add(workAddName);
                }
                else
                {
                    // 重複を許可しない場合の処理
                    // コピー先のリストボックスに既に含まれているかを判定
                    if (argLBTarget.Items.IndexOf(workAddName) == -1)
                    {
                        // 含まれていない場合，コピー元で選択中の項目をコピー先へコピー
                        argLBTarget.Items.Add(workAddName);
                    }
                }
            }

        }

        static public void allItemCopyListBox2ListBox(ListBox argLBMoto, ListBox argLBTarget, bool argArrowOverLap = false)
        {
            string workAddName;
            for (int iMotoLoop = 0; iMotoLoop < argLBMoto.Items.Count; iMotoLoop++)
            {
                workAddName = argLBMoto.Items[iMotoLoop].ToString();
                // コピーの際に，アイテムの重複を許可するか否かで分岐
                if (argArrowOverLap == true)
                {
                    // 重複を許可する場合の処理
                    // コピー元で選択中の1項目をコピー先のリストへ追加
                    argLBTarget.Items.Add(workAddName);
                }
                else
                {
                    // 重複を許可しない場合の処理
                    // コピー先のリストボックスに既に含まれているかを判定
                    if (argLBTarget.Items.IndexOf(workAddName) == -1)
                    {
                        // 含まれていない場合，コピー元で選択中の項目をコピー先へコピー
                        argLBTarget.Items.Add(workAddName);
                    }
                }
            }

            return;
        }

    }
}
