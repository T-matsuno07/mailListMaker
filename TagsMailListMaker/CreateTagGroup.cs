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
    public partial class CreateTagGroup : Form
    {
        public bool result;
        public string strNewTag;
        private List<string> listKizon;
        public CreateTagGroup()
        {
            
            InitializeComponent();
            result = false;
            strNewTag = string.Empty;
        }

        public void setKizonTag(List<string> argLisTag)
        {
            listKizon = argLisTag;
        }

        private void guiBut_EnterOK_Click(object sender, EventArgs e)
        {
            strNewTag = guiTexB_StrNewTagName.Text;
            if (listKizon.IndexOf(strNewTag) != -1 )
            {
                MessageBox.Show( strNewTag +"は既に登録されています。\r\n重複する名称は登録できません。"
                    ,"名称指定エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            result = true;
            this.Close();
        }

        private void guiBut_Cancel_Click(object sender, EventArgs e)
        {
            strNewTag = string.Empty;
            result = false;
            this.Close();
        }
    }
}
