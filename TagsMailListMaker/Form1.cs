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
    public partial class frm_TagsMailListMaker : Form
    {
        /* 定数 */
        const string CONST_STR_INCLUDE = "に該当する";
        const string CONST_STR_NOTINC =  "に該当しない";
        const string CONST_STR_ALLAND = "全ての条件を満たす";
        const string CONST_STR_ALLOR  = "いずれかの条件を満たす";
        const string CONST_STR_NEWREGISTER = "<新規登録>";
        const string CONST_STR_ALLTAG = "<全タグ>";
        const string CONST_STR_ALLMEMBER = "<全メンバー>";
        /* グローバル変数 定義 */
        string strExePath;
        string strXmlPath;
        DataSet dsDataBase;

        /* データベース更新フラグ データベースに変更が生じたかを格納する */
        bool flg_DataBaseUpdate;

        /// <summary>
        ///
        /// </summary>
        public frm_TagsMailListMaker()
        {
            InitializeComponent();

            //フォームの最大化ボタンを非表示にする
            this.MaximizeBox = false;

            // フォームのサイズを固定する
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            strExePath = System.Windows.Forms.Application.StartupPath;
            strXmlPath = strExePath + @"\MailDataBase";
            lib_XmlLINQ.importXmlFiles(out dsDataBase, strXmlPath);
            // メインタブを初期化
            initializeMainTab();
            // メンバータブを初期化
            InitializeMemberTab();
            // タグタブを初期化
            InitializeTagTab();
            guiCob_MembersMain.SelectedIndex = 0;

            /* データベース更新フラグをOFFで初期化 */
            flg_DataBaseUpdate = false;

        }

        /// <summary>
        /// コンボボックス全タグ追加
        /// </summary>
        /// <param name="argCB">全タグを設定したいコンボボックス</param>
        private void make_TagPullDown(ComboBox argCB)
        {
            DataRow[] drAllTags;   // 全てのTagsテーブルのレコードを格納する
            argCB.Items.Clear();   // 引数で受け取ったコンボボックスの項目を初期化

            // データセットからTagsテーブルを抽出
            drAllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "", "Sort");

            // レコードからタグ名称を抽出してコンボボックスの項目に追加
            for (int i = 0; i < drAllTags.Length; i++)
            {
                argCB.Items.Add(drAllTags[i]["Tag"].ToString());
            }

        }

        /// <summary>
        /// メインタブ デフォルト状態作成
        /// </summary>
        private void initializeMainTab()
        {
            // 条件1~5にまつわるコンボボックスの項目を再設定する
            make_TagPullDown(guiCmb_TagCondition1);
            make_TagPullDown(guiCmb_TagCondition2);
            make_TagPullDown(guiCmb_TagCondition3);
            make_TagPullDown(guiCmb_TagCondition4);
            make_TagPullDown(guiCmb_TagCondition5);


            // 条件1~5にまつわるコントロールをデフォルト状態に戻す
            guiChb_UseCon1.Checked = false;
            guiCmb_TagCondition1.SelectedIndex = -1;
            guiCmb_TagCondition1.Enabled = false;
            guiBtn_TagYesNo1.Text = CONST_STR_INCLUDE;

            guiChb_UseCon2.Checked = false;
            guiCmb_TagCondition2.SelectedIndex = -1;
            guiCmb_TagCondition2.Enabled = false;
            guiBtn_TagYesNo2.Text = CONST_STR_INCLUDE;

            guiChb_UseCon3.Checked = false;
            guiCmb_TagCondition3.SelectedIndex = -1;
            guiCmb_TagCondition3.Enabled = false;
            guiBtn_TagYesNo3.Text = CONST_STR_INCLUDE;

            guiChb_UseCon4.Checked = false;
            guiCmb_TagCondition4.SelectedIndex = -1;
            guiCmb_TagCondition4.Enabled = false;
            guiBtn_TagYesNo4.Text = CONST_STR_INCLUDE;

            guiChb_UseCon5.Checked = false;
            guiCmb_TagCondition5.SelectedIndex = -1;
            guiCmb_TagCondition5.Enabled = false;
            guiBtn_TagYesNo5.Text = CONST_STR_INCLUDE;

            guiChb_UseConStr.Enabled = false;
            guiChb_UseConStr.Checked = false;
            guiTxB_StrCondition.Enabled = false;
            guiTxB_StrCondition.Text = "未実装";
            guiBtn_MotoListByString.Enabled = false;

            guiList_MotoList.Items.Clear();
            guiList_ToMember.Items.Clear();
            guiList_CcMember.Items.Clear();
        }

        /// <summary>
        /// メール送信先候補リスト作成処理
        /// </summary>
        /// <remarks>
        /// メインタブにおける一番左下のリストボックスを作成する処理。
        /// 条件1~5について，条件を満たすメンバーリストを作成し，
        /// それらのリストを照らし合わせることでリストボックスに表示する名称を選択する
        /// </remarks>
        private void make_MotoList()
        {

            List<string> strListCon1, strListCon2, strListCon3, strListCon4, strListCon5;
            bool bl_con1, bl_con2, bl_con3, bl_con4, bl_con5;
            DataRow[] AllName;
            string workName;
            AllName = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "", "Sort");

            // メインタブにおける一番左下のリストボックスを初期化
            guiList_MotoList.Items.Clear();

            // 各条件ごとに，条件に合致するメンバーの文字列リストを作成する。
            strListCon1 = make_MatchConditionNameList(1);
            strListCon2 = make_MatchConditionNameList(2);
            strListCon3 = make_MatchConditionNameList(3);
            strListCon4 = make_MatchConditionNameList(4);
            strListCon5 = make_MatchConditionNameList(5);

            // Namesテーブルに含まれる名称を全てチェックする
            for (int i = 0; i < AllName.Length; i++)
            {
                // 名称を抽出
                workName = AllName[i]["Name"].ToString();
                // 「全ての条件を満たす」or「いずれかの条件を満たす」で分岐
                if (guiBtn_AllAndOr.Text == CONST_STR_ALLAND)
                {
                    // 全ての条件を満たす が選択されている場合の処理
                    // 全ての条件を満たす の場合，最終的には，「 && 」で連結する処理なので，
                    // 使用しない条件については，true側に倒す。これにより，未使用の条件が
                    // 最終的な結果に影響を与えないようにする。
                    bl_con1 = (strListCon1.Contains(workName) || !guiChb_UseCon1.Checked);
                    bl_con2 = (strListCon2.Contains(workName) || !guiChb_UseCon2.Checked);
                    bl_con3 = (strListCon3.Contains(workName) || !guiChb_UseCon3.Checked);
                    bl_con4 = (strListCon4.Contains(workName) || !guiChb_UseCon4.Checked);
                    bl_con5 = (strListCon5.Contains(workName) || !guiChb_UseCon5.Checked);

                    // 条件1~5の全てのリストに含まれている場合，候補リストに追加する
                    if (bl_con1 && bl_con2 && bl_con3 && bl_con4 && bl_con5)
                    {
                        guiList_MotoList.Items.Add(workName);
                    }
                }
                else
                {
                    // いずれかの条件を満たす が選択されている場合の処理
                    // いずれかの条件を満たす の場合，最終的には「 || 」で連結する処理なので，
                    // 使用しない条件については，false側に倒す。これにより，未使用の条件が
                    // 最終的な結果に影響を与えないようにする
                    bl_con1 = (strListCon1.Contains(workName) && guiChb_UseCon1.Checked);
                    bl_con2 = (strListCon2.Contains(workName) && guiChb_UseCon2.Checked);
                    bl_con3 = (strListCon3.Contains(workName) && guiChb_UseCon3.Checked);
                    bl_con4 = (strListCon4.Contains(workName) && guiChb_UseCon4.Checked);
                    bl_con5 = (strListCon5.Contains(workName) && guiChb_UseCon5.Checked);

                    // 条件1~5のいずれかのリストに含まれている場合，候補リストに追加する
                    if (bl_con1 || bl_con2 || bl_con3 || bl_con4 || bl_con5)
                    {
                        guiList_MotoList.Items.Add(workName);
                    }


                }


            }

            // 処理の結果，条件に合致する項目がない(メインタブにおける一番左下のリストボックスが空)
            // の場合，警告文を表示する
            if (guiList_MotoList.Items.Count == 0)
            {
                MessageBox.Show("条件に該当する項目が存在しません。","条件一致項目なし");
            }

            return;
        }

        /// <summary>
        /// 単一条件合致メンバーリスト作成
        /// </summary>
        /// <remarks>
        /// GUI上で指定されたひとつの条件に合致するメンバーの名称リストを作成する
        /// </remarks>
        /// <param name="argNum">条件1~5のいずれかを指定 指定値 1~5</param>
        /// <returns></returns>
        private List<string> make_MatchConditionNameList(int argNum)
        {
            List<string> rtnList;
            List<string> workList;
            DataRow[] drCondition;
            string strTag;
            string workName;

            CheckBox chb_Active = new CheckBox();
            ComboBox cob_selectTag = new ComboBox();
            Button bt_InclueOrNo = new Button();
            DataRow[] AllName;

            rtnList = new List<string>();
            workList = new List<string>();

            // 引数 異常判定
            if( (argNum < 1) || (5 < argNum))
            {
               // 不正引数の場合，処理を終了
               return rtnList;
            }

            // 引数に応じて，処理に用いるコントロールを変更する
            switch (argNum)
            {
                case 1:
                    {
                        chb_Active = guiChb_UseCon1;
                        cob_selectTag = guiCmb_TagCondition1;
                        bt_InclueOrNo = guiBtn_TagYesNo1;
                        break;
                    }
                case 2:
                    {
                        chb_Active = guiChb_UseCon2;
                        cob_selectTag = guiCmb_TagCondition2;
                        bt_InclueOrNo = guiBtn_TagYesNo2;
                        break;
                    }
                case 3:
                    {
                        chb_Active = guiChb_UseCon3;
                        cob_selectTag = guiCmb_TagCondition3;
                        bt_InclueOrNo = guiBtn_TagYesNo3;
                        break;
                    }
                case 4:
                    {
                        chb_Active = guiChb_UseCon4;
                        cob_selectTag = guiCmb_TagCondition4;
                        bt_InclueOrNo = guiBtn_TagYesNo4;
                        break;
                    }
                case 5:
                    {
                        chb_Active = guiChb_UseCon5;
                        cob_selectTag = guiCmb_TagCondition5;
                        bt_InclueOrNo = guiBtn_TagYesNo5;
                        break;
                    }
                default:
                    {
                        // この処理が実施されることはない
                        return rtnList;
                    }

            }

            // Namesテーブルに含まれる全ての名称を取得する
            AllName = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "", "Sort");

            // 条件を「使用する」のチェックボックスがONか？
            if (chb_Active.Checked == true)
            {
                // 「に該当する」or「に該当しない」を取得する
                strTag = cob_selectTag.Text;
                // 処理で使用するタグを持つ全名称を取得する
                drCondition = lib_XmlLINQ.seleceLINQ(dsDataBase, "Groups", "Tag = '" + strTag + "'");

                // 条件が「に該当する」or「に該当しない」かで分岐する
                if (bt_InclueOrNo.Text == CONST_STR_INCLUDE)
                {
                    // 条件が「該当する」の時の処理
                    // 対象タグを持つ全ての名称を戻り値用リストに追加
                    for (int i = 0; i < drCondition.Length; i++)
                    {
                        rtnList.Add(drCondition[i]["Name"].ToString());
                    }
                }
                else
                {
                    // 条件が「該当しない」の時の処理
                    // Namesテーブルに含まれる全名称を取得する
                    for (int i = 0; i < drCondition.Length; i++)
                    {
                        // 当該タグを持つ名称を集めた「当該タグ持ちリスト」を作成
                        workList.Add(drCondition[i]["Name"].ToString());
                    }
                    // 全名称を一つずつチェックする
                    for (int i = 0; i < AllName.Length; i++)
                    {
                        // 名称を一つずつ取り出す。
                        workName = AllName[i]["Name"].ToString();
                        // 当該タグ持ちリストに含まれていないことをチェック
                        if (workList.Contains(workName) == false)
                        {
                            // 含まれていなければ戻り値用リストに追加
                            rtnList.Add(workName);
                        }

                    }

                }
            }

            return rtnList;
        }

        /// <summary>
        /// 該当する/しない 切り替え処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchConditionYesNo(object sender, EventArgs e)
        {
            Button eventObject = (Button)sender;
            // 押されたボタンの表示文字列で分岐
            if (eventObject.Text == CONST_STR_INCLUDE)
            {
                //「該当する」の場合，該当しないに変更
                eventObject.Text = CONST_STR_NOTINC;
            }
            else
            {
                //「該当しない」の場合，該当するに変更
                eventObject.Text = CONST_STR_INCLUDE;
            }
        }



        /// <summary>
        /// メールアドレスリスト文字列作成
        /// </summary>
        /// <param name="argLB">メール対象者名を格納しているリストボックス</param>
        private void copyAdressToClipboard(ListBox argLB)
        {
            string strCopyStr;
            string workName;
            DataRow[] nameRow;

            strCopyStr = string.Empty;
            // メール対象リストが空かを判定
            if (argLB.Items.Count == 0)
            {
                // 空の場合，警告文を表示して処理を終了
                MessageBox.Show("リストが空です。リストにメンバーを追加してください。");
                return;
            }

            // リストに含まれるメール送信対象者を一つずつ処理
            for (int iLoop = 0; iLoop < argLB.Items.Count; iLoop++)
            {
                // 先頭以外の処理
                if(iLoop != 0)
                {
                  // アドレスとアドレスの間にスペースを入れる
                  strCopyStr += " ";
                }
                // リストから送信対象者の名称を取得
                workName = argLB.Items[iLoop].ToString();
                // Namesテーブルから送信対象者のメールアドレスを取得
                nameRow = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "Name = '" + workName + "'");
                // 作成する文字列の末尾に追加
                strCopyStr += nameRow[0]["Address"].ToString() + ";";
            }
            //アプリケーション終了後もクリップボードにデータを残しておく
            Clipboard.SetDataObject(strCopyStr, true);
            MessageBox.Show("クリップボードにアドレス一覧を格納しました。\r\nOutLookのTo/CC欄に張り付けてください。" +
                            "\r\nHint : 貼付け後にTabキーを押すと日本語名称に切り替わります。");
        }

        /// <summary>
        /// 名称リスト作成処理
        /// </summary>
        /// <param name="argLB">メール対象者名を格納しているリストボックス</param>
        private void copyNameToClipboard(ListBox argLB)
        {
            string strCopyStr;
            string workName;

            strCopyStr = string.Empty;

            // 対象のリストボックスが空かを判定する
            if (argLB.Items.Count == 0)
            {
                // 空の場合，警告文を表示して処理を終了
                MessageBox.Show("リストが空です。リストにメンバーを追加してください。");
                return;
            }

            for (int iLoop = 0; iLoop < argLB.Items.Count; iLoop++)
            {
                workName = argLB.Items[iLoop].ToString();
                strCopyStr += workName + System.Environment.NewLine;
            }
            //アプリケーション終了後もクリップボードにデータを残しておく
            Clipboard.SetDataObject(strCopyStr, true);
            MessageBox.Show("クリップボードに名称一覧を格納しました。");

        }


        /// <summary>
        /// Tag条件のアクティブ/ノンアクティブを切り替える
        /// </summary>
        private void SwitchActiveCondition()
        {
            // 条件1に関する処理
            if (guiChb_UseCon1.Checked == true)
            {
                // チェックがオンの場合、各オブジェクトをアクティブ化
                guiCmb_TagCondition1.Enabled = true;
                guiBtn_TagYesNo1.Enabled = true;
                // 未選択の場合、最初の要素を選択状態にする
                if (guiCmb_TagCondition1.SelectedIndex == -1)
                {
                    guiCmb_TagCondition1.SelectedIndex = 0;
                }

            }
            else
            {
                // チェックがオンの場合、各オブジェクトをノンアクティブ化
                guiCmb_TagCondition1.Enabled = false;
                guiBtn_TagYesNo1.Enabled = false;

            }

            // 条件2に関する処理
            if (guiChb_UseCon2.Checked == true)
            {
                // チェックがオンの場合、各オブジェクトをアクティブ化
                guiCmb_TagCondition2.Enabled = true;
                guiBtn_TagYesNo2.Enabled = true;
                // 未選択の場合、最初の要素を選択状態にする
                if (guiCmb_TagCondition2.SelectedIndex == -1)
                {
                    guiCmb_TagCondition2.SelectedIndex = 0;
                }

            }
            else
            {
                // チェックがオンの場合、各オブジェクトをノンアクティブ化
                guiCmb_TagCondition2.Enabled = false;
                guiBtn_TagYesNo2.Enabled = false;

            }

            // 条件3に関する処理
            if (guiChb_UseCon3.Checked == true)
            {
                // チェックがオンの場合、各オブジェクトをアクティブ化
                guiCmb_TagCondition3.Enabled = true;
                guiBtn_TagYesNo3.Enabled = true;
                // 未選択の場合、最初の要素を選択状態にする
                if (guiCmb_TagCondition3.SelectedIndex == -1)
                {
                    guiCmb_TagCondition3.SelectedIndex = 0;
                }

            }
            else
            {
                // チェックがオンの場合、各オブジェクトをノンアクティブ化
                guiCmb_TagCondition3.Enabled = false;
                guiBtn_TagYesNo3.Enabled = false;

            }

            // 条件4に関する処理
            if (guiChb_UseCon4.Checked == true)
            {
                // チェックがオンの場合、各オブジェクトをアクティブ化
                guiCmb_TagCondition4.Enabled = true;
                guiBtn_TagYesNo4.Enabled = true;
                // 未選択の場合、最初の要素を選択状態にする
                if (guiCmb_TagCondition4.SelectedIndex == -1)
                {
                    guiCmb_TagCondition4.SelectedIndex = 0;
                }

            }
            else
            {
                // チェックがオンの場合、各オブジェクトをノンアクティブ化
                guiCmb_TagCondition4.Enabled = false;
                guiBtn_TagYesNo4.Enabled = false;

            }

            // 条件5に関する処理
            if (guiChb_UseCon5.Checked == true)
            {
                // チェックがオンの場合、各オブジェクトをアクティブ化
                guiCmb_TagCondition5.Enabled = true;
                guiBtn_TagYesNo5.Enabled = true;
                // 未選択の場合、最初の要素を選択状態にする
                if (guiCmb_TagCondition5.SelectedIndex == -1)
                {
                    guiCmb_TagCondition5.SelectedIndex = 0;
                }

            }
            else
            {
                // チェックがオンの場合、各オブジェクトをノンアクティブ化
                guiCmb_TagCondition5.Enabled = false;
                guiBtn_TagYesNo5.Enabled = false;

            }


            //guiChb_UseConStr
            if (guiChb_UseConStr.Checked == true)
            {
                guiTxB_StrCondition.Enabled = true;
            }
            else
            {
                guiTxB_StrCondition.Enabled = false;
            }

        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MotoListCreate_Click(object sender, EventArgs e)
        {
            make_MotoList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagYesNo_Click(object sender, EventArgs e)
        {
            SwitchConditionYesNo(sender, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiChb_UseCon_CheckedChanged(object sender, EventArgs e)
        {
            SwitchActiveCondition();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_ConditionInitial_Click(object sender, EventArgs e)
        {
            DialogResult rtn;
            rtn = MessageBox.Show("条件を初期化しますか?", "初期化問い合わせ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rtn == System.Windows.Forms.DialogResult.OK)
            {
                initializeMainTab();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_AllAndOr_Click(object sender, EventArgs e)
        {
            if (guiBtn_AllAndOr.Text == CONST_STR_ALLAND)
            {
                guiBtn_AllAndOr.Text = CONST_STR_ALLOR;
            }
            else
            {
                guiBtn_AllAndOr.Text = CONST_STR_ALLAND;
            }
        }


        /// <summary>
        /// メンバータブにおける各コントロールを初期化する
        /// </summary>
        private void InitializeMemberTab()
        {
            DataRow[] dr_AllTags;
            // 呼び出し左のコンボボックスを初期化
            guiCob_MembersMain.Items.Clear();
            guiCob_MembersMain.Items.Add(CONST_STR_NEWREGISTER);
            guiCob_MembersMotoMem.Items.Clear();
            guiCob_MembersMotoMem.Items.Add(CONST_STR_ALLTAG);

            dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names","", "Sort");
            for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
            {
                guiCob_MembersMain.Items.Add(dr_AllTags[iLoop]["Name"].ToString());
                guiCob_MembersMotoMem.Items.Add(dr_AllTags[iLoop]["Name"].ToString());
            }

            guiList_CopyMotoTag.Items.Clear();
            dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "", "Sort");
            for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
            {
                guiList_CopyMotoTag.Items.Add(dr_AllTags[iLoop]["Tag"].ToString());

            }

            guiTeb_EditName.Text = string.Empty;
            guiTeb_EditAddress.Text = string.Empty;
            guiList_NameRegistTag.Items.Clear();
            guiCob_MembersMain.SelectedIndex = 0;
            guiCob_MembersMotoMem.SelectedIndex = 0;
            SwitchMenberControlEnabled(true);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="argActive"></param>
        private void SwitchMenberControlEnabled(bool argActive)
        {

            guiCob_MembersMain.Enabled = argActive;
            guiBtn_MemberCall.Enabled = argActive;
            guiBtn_MemberChangeSort.Enabled = argActive;
            guiBtn_MemberCancel.Enabled = !argActive;
            guiBtn_MemberRegister.Enabled = !argActive;
            guiBtn_MemberDelete.Enabled = !argActive;
            guiBtn_MemberSleep.Enabled = false;
            guiTeb_EditName.Enabled = !argActive;
            guiTeb_EditAddress.Enabled = !argActive;
            guiList_NameRegistTag.Enabled = !argActive;
            guiBtn_MmberAddAllTag.Enabled = !argActive;
            guiBtn_MmberAddOneTag.Enabled = !argActive;
        }

        /// <summary>
        /// タグタブにおける各コントロールを初期化する
        /// </summary>
        private void InitializeTagTab()
        {
            DataRow[] dr_AllTags;
            // 呼び出し左のコンボボックスを初期化
            guiCob_TagsMain.Items.Clear();
            guiCob_TagsMain.Items.Add(CONST_STR_NEWREGISTER);
            guiCob_TagMotoMem.Items.Clear();
            guiCob_TagMotoMem.Items.Add(CONST_STR_ALLTAG);

            dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "", "Sort");
            for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
            {
                guiCob_TagsMain.Items.Add(dr_AllTags[iLoop]["Tag"].ToString());
                guiCob_TagMotoMem.Items.Add(dr_AllTags[iLoop]["Tag"].ToString());
            }

            guiList_TagRegistMoto.Items.Clear();
            dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "", "Sort");
            for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
            {
                guiList_TagRegistMoto.Items.Add(dr_AllTags[iLoop]["Name"].ToString());

            }

            guiTeb_EditTag.Text = string.Empty;
            guiList_TagRegistSaki.Items.Clear();
            guiCob_TagsMain.SelectedIndex = 0;
            guiCob_TagMotoMem.SelectedIndex = 0;
            SwitchTagTabControlEnabled(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argActive"></param>
        private void SwitchTagTabControlEnabled(bool argActive)
        {
            guiCob_TagsMain.Enabled = argActive;
            guiBtn_TagsCall.Enabled = argActive;
            guiBtn_TagChangeSort.Enabled = argActive;
            guiBtn_TagsRegister.Enabled = !argActive;
            guiBtn_TagsCancel.Enabled = !argActive;
            guiBtn_TagsCallSleep.Enabled = false;
            guiBtn_TagsDelete.Enabled = !argActive;
            guiBtn_TagDeleteOneMember.Enabled = !argActive;
            guiBtn_TagAddAllMember.Enabled = !argActive;
            guiBtn_TagAddOneMember.Enabled = !argActive;
            guiTeb_EditTag.Enabled = !argActive;
        }

        /// <summary>
        /// メンバー新規登録処理
        /// </summary>
        private void registerMember_New()
        {
            DataRow newNamesRow;
            DataRow newGroupRow;
            newNamesRow = dsDataBase.Tables["Names"].NewRow();
            newNamesRow["Name"] = guiTeb_EditName.Text;
            newNamesRow["Address"] = guiTeb_EditAddress.Text;
            newNamesRow["Sort"] = dsDataBase.Tables["Names"].Rows.Count - 1;
            dsDataBase.Tables["Names"].Rows.Add(newNamesRow);

            for (int iLoop = 0; iLoop < guiList_NameRegistTag.Items.Count; iLoop++)
            {
                newGroupRow = dsDataBase.Tables["Groups"].NewRow();
                newGroupRow["Name"] = guiTeb_EditName.Text;
                newGroupRow["Tag"] = guiList_NameRegistTag.Items[iLoop].ToString();
                dsDataBase.Tables["Groups"].Rows.Add(newGroupRow);
            }

        }

        /// <summary>
        /// タグ新規登録処理
        /// </summary>
        private void registerTag_New()
        {
            DataRow newTagRow;
            DataRow newGroupRow;
            newTagRow = dsDataBase.Tables["Tags"].NewRow();
            newTagRow["Tag"] = guiTeb_EditTag.Text;
            newTagRow["Sort"] = dsDataBase.Tables["Tags"].Rows.Count;
            dsDataBase.Tables["Tags"].Rows.Add(newTagRow);

            for (int iLoop = 0; iLoop < guiList_TagRegistSaki.Items.Count; iLoop++)
            {
                newGroupRow = dsDataBase.Tables["Groups"].NewRow();
                newGroupRow["Tag"] = guiTeb_EditTag.Text;
                newGroupRow["Name"] = guiList_TagRegistSaki.Items[iLoop].ToString();
                dsDataBase.Tables["Groups"].Rows.Add(newGroupRow);
            }
        }


        /// <summary>
        /// メンバー更新処理
        /// </summary>
        private void registerMember_Update()
        {
            DataRow[] updateNamesRow;
            DataRow updateGroupRow;
            string strTargetName;
            strTargetName = guiCob_MembersMain.SelectedItem.ToString();

            updateNamesRow = lib_XmlLINQ.seleceLINQ(dsDataBase,"Names", "Name = '"+ strTargetName+"'"); 
            updateNamesRow[0]["Name"] = guiTeb_EditName.Text;
            updateNamesRow[0]["Address"] = guiTeb_EditAddress.Text;

            lib_XmlLINQ.deleteLINQ(dsDataBase, "Groups", "Name = '" + strTargetName + "'");
            for (int iLoop = 0; iLoop < guiList_NameRegistTag.Items.Count; iLoop++)
            {
                updateGroupRow = dsDataBase.Tables["Groups"].NewRow();
                updateGroupRow["Name"] = guiTeb_EditName.Text;
                updateGroupRow["Tag"] = guiList_NameRegistTag.Items[iLoop].ToString();
                dsDataBase.Tables["Groups"].Rows.Add(updateGroupRow);
            }


        }

        /// <summary>
        ///
        /// </summary>
        private void registerTag_Update()
        {
            DataRow[] updateTagsRow;
            DataRow updateGroupRow;
            string strTargetName;
            strTargetName = guiCob_TagsMain.SelectedItem.ToString();

            updateTagsRow = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "Tag = '" + strTargetName + "'"); 
            updateTagsRow[0]["Tag"] = guiTeb_EditTag.Text;

            lib_XmlLINQ.deleteLINQ(dsDataBase, "Groups", "Tag = '" + strTargetName + "'");
            for (int iLoop = 0; iLoop < guiList_TagRegistSaki.Items.Count; iLoop++)
            {
                updateGroupRow = dsDataBase.Tables["Groups"].NewRow();
                updateGroupRow["Tag"] = guiTeb_EditTag.Text;
                updateGroupRow["Name"] = guiList_TagRegistSaki.Items[iLoop].ToString();
                dsDataBase.Tables["Groups"].Rows.Add(updateGroupRow);
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_AddAllTo_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.allItemCopyListBox2ListBox(guiList_MotoList, guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_AddOneCc_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.allItemCopyListBox2ListBox(guiList_MotoList, guiList_CcMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_AddOneTo_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_MotoList, guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_AddAllCc_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_MotoList, guiList_CcMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_getToAddress_Click(object sender, EventArgs e)
        {
            copyAdressToClipboard(guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_getCcAddress_Click(object sender, EventArgs e)
        {
            copyAdressToClipboard(guiList_CcMember);
        }

        private void guiBtn_getToName_Click(object sender, EventArgs e)
        {
            copyNameToClipboard(guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_getCcName_Click(object sender, EventArgs e)
        {
            copyNameToClipboard(guiList_CcMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_DeleteAllTo_Click(object sender, EventArgs e)
        {
            DialogResult rtn;

            if (guiList_ToMember.Items.Count == 0)
            {
                return;
            }

            rtn = MessageBox.Show("リストの要素を全削除しますか?", "リスト初期化", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rtn == System.Windows.Forms.DialogResult.OK)
            {
                guiList_ToMember.Items.Clear();
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_DeleteAllCc_Click(object sender, EventArgs e)
        {
            DialogResult rtn;

            if (guiList_CcMember.Items.Count == 0)
            {
                return;
            }

            rtn = MessageBox.Show("リストの要素を全削除しますか?", "リスト初期化", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rtn == System.Windows.Forms.DialogResult.OK)
            {
                guiList_CcMember.Items.Clear();
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_DeleteOneTo_Click(object sender, EventArgs e)
        {
            while (guiList_ToMember.SelectedItems.Count != 0)
            {
                guiList_ToMember.Items.RemoveAt(guiList_ToMember.SelectedIndices[0]);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_DeleteOneCc_Click(object sender, EventArgs e)
        {
            while (guiList_CcMember.SelectedItems.Count != 0)
            {
                guiList_CcMember.Items.RemoveAt(guiList_CcMember.SelectedIndices[0]);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_ToMoveUp_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveUpListItem(guiList_ToMember);
        }

        private void guiBtn_CcMoveUp_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveUpListItem(guiList_CcMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_ToMoveDown_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveDownListItem(guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_CcMoveDown_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.moveDownListItem(guiList_CcMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiList_MotoList_DoubleClick(object sender, EventArgs e)
        {
            lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_MotoList, guiList_ToMember);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MemberCall_Click(object sender, EventArgs e)
        {
            DataRow[] drCalled;
            string strSelected;
            SwitchMenberControlEnabled(false);

            strSelected = guiCob_MembersMain.SelectedItem.ToString();
            if (strSelected != CONST_STR_NEWREGISTER)
            {
                drCalled = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "Name = '" + strSelected + "'");
                guiTeb_EditName.Text = drCalled[0]["Name"].ToString();
                guiTeb_EditAddress.Text = drCalled[0]["Address"].ToString();

                drCalled = lib_XmlLINQ.seleceLINQ(dsDataBase, "Groups", "Name = '" + strSelected + "'");
                for (int iLoop = 0; iLoop < drCalled.Length; iLoop++)
                {
                    guiList_NameRegistTag.Items.Add(drCalled[iLoop]["Tag"].ToString());
                }

            }

        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagsCall_Click(object sender, EventArgs e)
        {
            DataRow[] drCalled;
            string strSelected;
            SwitchTagTabControlEnabled(false);

            strSelected = guiCob_TagsMain.SelectedItem.ToString();
            if (strSelected != CONST_STR_NEWREGISTER)
            {
                drCalled = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "Tag = '" + strSelected + "'");
                guiTeb_EditTag.Text = drCalled[0]["Tag"].ToString();


                drCalled = lib_XmlLINQ.seleceLINQ(dsDataBase, "Groups", "Tag = '" + strSelected + "'");
                for (int iLoop = 0; iLoop < drCalled.Length; iLoop++)
                {
                    guiList_TagRegistSaki.Items.Add(drCalled[iLoop]["Name"].ToString());
                }

            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MemberCancel_Click(object sender, EventArgs e)
        {
            DialogResult rtn;
            rtn = MessageBox.Show("入力内容を破棄します。よろしいですか?\r\n呼出前の状態に戻ります。", "入力情報破棄確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rtn == System.Windows.Forms.DialogResult.OK)
            {

                SwitchMenberControlEnabled(true);
                guiTeb_EditName.Text = string.Empty;
                guiTeb_EditAddress.Text = string.Empty;
                guiList_NameRegistTag.Items.Clear();
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagsCancel_Click(object sender, EventArgs e)
        {
            DialogResult rtn;
            rtn = MessageBox.Show("入力内容を破棄します。よろしいですか?\r\n呼出前の状態に戻ります。", "入力情報破棄確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rtn == System.Windows.Forms.DialogResult.OK)
            {

                SwitchTagTabControlEnabled(true);
                guiTeb_EditTag.Text = string.Empty;
                guiList_TagRegistSaki.Items.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MmberAddOneTag_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_CopyMotoTag, guiList_NameRegistTag);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MmberAddAllTag_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.allItemCopyListBox2ListBox(guiList_CopyMotoTag, guiList_NameRegistTag);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagAddOneMember_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_TagRegistMoto, guiList_TagRegistSaki);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagAddAllMember_Click(object sender, EventArgs e)
        {
            lib_HandleGUIControl.allItemCopyListBox2ListBox(guiList_TagRegistMoto, guiList_TagRegistSaki);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiList_CopyMotoTag_DoubleClick(object sender, EventArgs e)
        {
            if (guiBtn_MmberAddOneTag.Enabled == true)
            {
                lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_CopyMotoTag, guiList_NameRegistTag);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiList_TagRegistMoto_DoubleClick(object sender, EventArgs e)
        {
            if (guiBtn_TagAddOneMember.Enabled == true)
            {
                lib_HandleGUIControl.selecetedItemCopyListBox2ListBox(guiList_TagRegistMoto, guiList_TagRegistSaki);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiCob_MembersMotoMem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectedName;
            DataRow[] dr_AllTags;

            if (guiCob_MembersMotoMem.SelectedIndex == -1)
            {
                return;
            }

            strSelectedName = guiCob_MembersMotoMem.SelectedItem.ToString();
            guiList_CopyMotoTag.Items.Clear();
            if (strSelectedName == CONST_STR_ALLTAG)
            {
                dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "", "Sort");
                for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
                {
                    guiList_CopyMotoTag.Items.Add(dr_AllTags[iLoop]["Tag"].ToString());
                }

            }
            else
            {
                dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Groups", "Name = '" + strSelectedName + "'");

                for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
                {
                    guiList_CopyMotoTag.Items.Add(dr_AllTags[iLoop]["Tag"].ToString());
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiCob_TagMotoMem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectedName;
            DataRow[] dr_AllTags;

            if (guiCob_TagMotoMem.SelectedIndex == -1)
            {
                return;
            }

            strSelectedName = guiCob_TagMotoMem.SelectedItem.ToString();
            guiList_TagRegistMoto.Items.Clear();
            if (strSelectedName == CONST_STR_ALLTAG)
            {
                dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names");
                for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
                {
                    guiList_TagRegistMoto.Items.Add(dr_AllTags[iLoop]["Name"].ToString());
                }

            }
            else
            {
                dr_AllTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Groups", "Tag = '" + strSelectedName + "'");

                for (int iLoop = 0; iLoop < dr_AllTags.Length; iLoop++)
                {
                    guiList_TagRegistMoto.Items.Add(dr_AllTags[iLoop]["Name"].ToString());
                }
            }
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiTabPage_Tab_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// メンバー登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MemberRegister_Click(object sender, EventArgs e)
        {
            DataRow[] drRows;
            // 登録内容に不備がないかチェック
            // メンバー名称に名前の記入があるかをチェック
            if (guiTeb_EditName.Text == string.Empty)
            {
                MessageBox.Show("「メンバー名称」欄が空白です。メンバー名称を記入してください。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (guiTeb_EditName.Text == CONST_STR_NEWREGISTER)
            {
                MessageBox.Show(CONST_STR_NEWREGISTER+"という名称は登録できません。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            // メールアドレスに不備がないかをチェック
            if (!guiTeb_EditAddress.Text.Contains("@"))
            {
                MessageBox.Show("「メールアドレス」欄が不正です。\r\n文字列に「@」が含まれていません。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 名称の重複が発生していないかをチェック
            if (guiCob_MembersMain.SelectedItem.ToString() != guiTeb_EditName.Text)
            {
                drRows = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "Name = '" + guiTeb_EditName.Text + "'");
                if (0 < drRows.Length)
                {
                    MessageBox.Show(guiTeb_EditName.Text+ "は既に登録されています。\r\n同姓同名を登録することはできません。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 新規登録か更新かで分岐
            if (guiCob_MembersMain.SelectedItem.ToString() == CONST_STR_NEWREGISTER)
            {
                // 新規登録処理
                registerMember_New();
            }
            else
            {
                // 登録内容更新処理
                registerMember_Update();
            }

            flg_DataBaseUpdate = true;
            // メンバータグを初期化
            InitializeMemberTab();
            initializeMainTab();

        }

        /// <summary>
        /// タグ登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagsRegister_Click(object sender, EventArgs e)
        {
            DataRow[] drRows;
            // 登録内容に不備がないかチェック
            // タグ名称に名前の記入があるかをチェック
            if (guiTeb_EditTag.Text == string.Empty)
            {
                MessageBox.Show("「タグ名称」欄が空白です。タグ名称を記入してください。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (guiTeb_EditTag.Text == CONST_STR_NEWREGISTER)
            {
                MessageBox.Show(CONST_STR_NEWREGISTER + "という名称は登録できません。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            // 名称の重複が発生していないかをチェック
            if (guiCob_TagsMain.SelectedItem.ToString() != guiTeb_EditTag.Text)
            {
                drRows = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "Name = '" + guiTeb_EditTag.Text + "'");
                if (0 < drRows.Length)
                {
                    MessageBox.Show(guiTeb_EditTag.Text + "は既に登録されています。\r\n同じ名称のタグを複数登録することはできません。", "名称指定エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 新規登録か更新かで分岐
            if (guiCob_TagsMain.SelectedItem.ToString() == CONST_STR_NEWREGISTER)
            {
                // 新規登録処理
                registerTag_New();
            }
            else
            {
                // 登録内容更新処理
                registerTag_Update();
            }

            flg_DataBaseUpdate = true;
            // タグTabを初期化
            InitializeTagTab();
            // メインtabを初期化
            initializeMainTab();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MmberDeleteTagItem_Click(object sender, EventArgs e)
        {
            while (guiList_NameRegistTag.SelectedItems.Count != 0)
            {
                guiList_NameRegistTag.Items.RemoveAt(guiList_NameRegistTag.SelectedIndices[0]);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagDeleteOneMember_Click(object sender, EventArgs e)
        {
            while (guiList_TagRegistSaki.SelectedItems.Count != 0)
            {
                guiList_TagRegistSaki.Items.RemoveAt(guiList_TagRegistSaki.SelectedIndices[0]);
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiCob_MembersMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MemberDelete_Click(object sender, EventArgs e)
        {
            DialogResult rtn;
            string strDeleteName;
            strDeleteName = guiCob_MembersMain.SelectedItem.ToString();
            if (strDeleteName == CONST_STR_NEWREGISTER)
            {
                rtn = MessageBox.Show("入力内容を破棄します。よろしいですか?\r\n注意：破棄した情報は復元できません。", "削除確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rtn == System.Windows.Forms.DialogResult.OK)
                {
                    SwitchMenberControlEnabled(true);
                    guiTeb_EditName.Text = string.Empty;
                    guiTeb_EditAddress.Text = string.Empty;
                    guiList_NameRegistTag.Items.Clear();
                    InitializeMemberTab();
                    InitializeTagTab();
                    flg_DataBaseUpdate = true;
                }
            }
            else
            {

                rtn = MessageBox.Show(strDeleteName + "を削除します。よろしいですか?\r\n注意：削除した情報は復元できません。", "削除確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rtn == System.Windows.Forms.DialogResult.OK)
                {
                    lib_XmlLINQ.deleteLINQ(dsDataBase, "Names", "Name = '" + strDeleteName + "'");
                    lib_XmlLINQ.deleteLINQ(dsDataBase, "Groups", "Name = '" + strDeleteName + "'");
                    InitializeMemberTab();
                    flg_DataBaseUpdate = true;

                    InitializeTagTab();
                    guiCob_TagMotoMem_SelectedIndexChanged(sender, e);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagsDelete_Click(object sender, EventArgs e)
        {
            DialogResult rtn;
            string strDeleteName;
            strDeleteName = guiCob_TagsMain.SelectedItem.ToString();
            if (strDeleteName == CONST_STR_NEWREGISTER)
            {
                rtn = MessageBox.Show("入力内容を破棄します。よろしいですか?\r\n注意：破棄した情報は復元できません。", "削除確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rtn == System.Windows.Forms.DialogResult.OK)
                {
                    SwitchTagTabControlEnabled(true);
                    guiTeb_EditTag.Text = string.Empty;
                    guiList_TagRegistSaki.Items.Clear();

                    // メインtabを初期化
                    initializeMainTab();
                    InitializeMemberTab();
                    InitializeTagTab();
                    flg_DataBaseUpdate = true;
                    guiCob_MembersMotoMem_SelectedIndexChanged(sender,e);
                }
            }
            else
            {

                rtn = MessageBox.Show(strDeleteName + "を削除します。よろしいですか?\r\n注意：削除した情報は復元できません。", "削除確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rtn == System.Windows.Forms.DialogResult.OK)
                {
                    lib_XmlLINQ.deleteLINQ(dsDataBase, "Tags", "Tag = '" + strDeleteName + "'");
                    lib_XmlLINQ.deleteLINQ(dsDataBase, "Groups", "Tag = '" + strDeleteName + "'");
                    InitializeTagTab();
                    // メインtabを初期化
                    initializeMainTab();
                    flg_DataBaseUpdate = true;
                    // メインtabを初期化
                    InitializeMemberTab();
                    InitializeTagTab();
                    flg_DataBaseUpdate = true;
                    guiCob_MembersMotoMem_SelectedIndexChanged(sender, e);
                }
            }


        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_TagsMailListMaker_FormClosed(object sender, FormClosedEventArgs e)
        {
            // データベースへの登録処理が行われている場合、Xmlファイルを保存する
            if (flg_DataBaseUpdate == true)
            {
                lib_XmlLINQ.exportXmlFiles(dsDataBase, strXmlPath);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_MemberChangeSort_Click(object sender, EventArgs e)
        {
            DataRow[] drMembers;
            List<string> orginMembers;
            List<string> sortedMembers;
            string tmpName;
            SortItemsForm sortForm = new SortItemsForm();
            orginMembers = new List<string>();
            drMembers = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "", "Sort");
            
            
            for (int i = 0; i < drMembers.Length; i++)
            {
                orginMembers.Add( drMembers[i]["Name"].ToString() );
            }
            sortForm.SetOriginalItemList(orginMembers);


            sortForm.ShowDialog();

            if (sortForm.ResultRetrun == true)
            {
                sortedMembers = sortForm.getResult();
                for (int iNewSort = 0; iNewSort < sortedMembers.Count; iNewSort++)
                {
                    tmpName = sortedMembers[iNewSort];
                    drMembers = lib_XmlLINQ.seleceLINQ(dsDataBase, "Names", "[Name] = '" + tmpName + "'");
                    drMembers[0]["Sort"] = iNewSort;
                }
                flg_DataBaseUpdate = true;
                InitializeMemberTab();
                initializeMainTab();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiBtn_TagChangeSort_Click(object sender, EventArgs e)
        {
            DataRow[] drTags;
            List<string> originTags;
            List<string> sortedTags;
            string tmpName;
            SortItemsForm sortForm = new SortItemsForm();
            originTags = new List<string>();
            drTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "", "Sort");


            for (int i = 0; i < drTags.Length; i++)
            {
                originTags.Add(drTags[i]["Tag"].ToString());
            }
            sortForm.SetOriginalItemList(originTags);


            sortForm.ShowDialog();

            if (sortForm.ResultRetrun == true)
            {
                sortedTags = sortForm.getResult();
                for (int iNewSort = 0; iNewSort < sortedTags.Count; iNewSort++)
                {
                    tmpName = sortedTags[iNewSort];
                    drTags = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags", "[Tag] = '" + tmpName + "'");
                    drTags[0]["Sort"] = iNewSort;
                }
                flg_DataBaseUpdate = true;
                InitializeTagTab();
                initializeMainTab();
            }
        }

        /// <summary>
        /// 最前面表示切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiChck_AlwayFront_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = guiChck_AlwayFront.Checked;
        }

        private void registerNewTag_OnMainTag(ListBox argLB)
        {
            CreateTagGroup ctg_form;
            List<string> lst_kizon;
            DataRow[] drKizonTag;

            if (argLB.Items.Count == 0)
            {
                MessageBox.Show("リストにメンバーが格納されていません。\r\nメンバーを追加してください。", "空リストエラー");
                return;
            }

            lst_kizon = new List<string>();
            drKizonTag = lib_XmlLINQ.seleceLINQ(dsDataBase, "Tags");
            for (int i = 0; i < drKizonTag.Length; i++)
            {
                lst_kizon.Add( drKizonTag[i]["Tag"].ToString() );
            }
            lst_kizon.Add(CONST_STR_NEWREGISTER);
            ctg_form = new CreateTagGroup();

            ctg_form.setKizonTag(lst_kizon);
            ctg_form.ShowDialog();

            if (ctg_form.result == true)
            {
                DataRow newTagRow;
                DataRow newGroupRow;
                string strNewTagName = ctg_form.strNewTag;
                newTagRow = dsDataBase.Tables["Tags"].NewRow();
                newTagRow["Tag"] = strNewTagName;
                newTagRow["Sort"] = dsDataBase.Tables["Tags"].Rows.Count;
                dsDataBase.Tables["Tags"].Rows.Add(newTagRow);

                for (int iLoop = 0; iLoop < argLB.Items.Count; iLoop++)
                {
                    newGroupRow = dsDataBase.Tables["Groups"].NewRow();
                    newGroupRow["Tag"] = strNewTagName;
                    newGroupRow["Name"] = argLB.Items[iLoop].ToString();
                    dsDataBase.Tables["Groups"].Rows.Add(newGroupRow);
                }

                // 条件1~5にまつわるコンボボックスの項目を再設定する
                make_TagPullDown(guiCmb_TagCondition1);
                make_TagPullDown(guiCmb_TagCondition2);
                make_TagPullDown(guiCmb_TagCondition3);
                make_TagPullDown(guiCmb_TagCondition4);
                make_TagPullDown(guiCmb_TagCondition5);


                // 条件1~5にまつわるコントロールをデフォルト状態に戻す
                guiChb_UseCon1.Checked = false;
                guiCmb_TagCondition1.SelectedIndex = -1;
                guiCmb_TagCondition1.Enabled = false;
                guiBtn_TagYesNo1.Text = CONST_STR_INCLUDE;

                guiChb_UseCon2.Checked = false;
                guiCmb_TagCondition2.SelectedIndex = -1;
                guiCmb_TagCondition2.Enabled = false;
                guiBtn_TagYesNo2.Text = CONST_STR_INCLUDE;

                guiChb_UseCon3.Checked = false;
                guiCmb_TagCondition3.SelectedIndex = -1;
                guiCmb_TagCondition3.Enabled = false;
                guiBtn_TagYesNo3.Text = CONST_STR_INCLUDE;

                guiChb_UseCon4.Checked = false;
                guiCmb_TagCondition4.SelectedIndex = -1;
                guiCmb_TagCondition4.Enabled = false;
                guiBtn_TagYesNo4.Text = CONST_STR_INCLUDE;

                guiChb_UseCon5.Checked = false;
                guiCmb_TagCondition5.SelectedIndex = -1;
                guiCmb_TagCondition5.Enabled = false;
                guiBtn_TagYesNo5.Text = CONST_STR_INCLUDE;

                flg_DataBaseUpdate = true;
                // メンバータブを初期化
                InitializeMemberTab();
                // タグタブを初期化
                InitializeTagTab();
            }

        }

        private void guiBut_MainTagMake_Moto_Click(object sender, EventArgs e)
        {
            registerNewTag_OnMainTag(guiList_MotoList);
        }

        private void guiBut_MainTagMake_To_Click(object sender, EventArgs e)
        {
            registerNewTag_OnMainTag(guiList_ToMember);
        }

        private void guiBut_MainTagMake_Cc_Click(object sender, EventArgs e)
        {
            registerNewTag_OnMainTag(guiList_CcMember);
        }

        private void guiBtn_MemberSleep_Click(object sender, EventArgs e)
        {

        }

        private void guiBtn_TagDeleteTagItem_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if ((guiBtn_MemberRegister.Enabled == true) || (guiBtn_TagsRegister.Enabled == true) )
            {
                MessageBox.Show("項目編集中はタブを切り替えできません。\r\n編集内容を登録/更新で確定するか、\r\nキャンセルで取り消してください。",
                    "タブ切り替え不可能",MessageBoxButtons.OK,MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

    }
}
