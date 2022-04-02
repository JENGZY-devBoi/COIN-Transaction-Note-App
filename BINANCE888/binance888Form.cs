using System;
using System.Windows.Forms;

namespace BINANCE888 {
    public partial class binance888Form : Form {
        #region Fields
         bool edit = false;
        #endregion

        public binance888Form() {
            InitializeComponent();

            // init textbox control to list 
            TextBox[] arrTxt = 
                { txtToken, txtPrice, txtVolume, txtUsdVal, txtTimeHr, txtTimeMin};
            Label[] arrLblName = 
                { lblNameRow1, lblNameRow2, lblNameRow3, lblNameRow4, lblNameRow5 };
            Label[] arrLblPrice =
                { lblPriceRow1, lblPriceRow2, lblPriceRow3, lblPriceRow4, lblPriceRow5 };
            Label[] arrLblVol =
                { lblVolRow1, lblVolRow2, lblVolRow3, lblVolRow4, lblVolRow5};
            Label[] arrLblUSD =
                { lblUsdRow1, lblUsdRow2, lblUsdRow3, lblUsdRow4, lblUsdRow5 };
            Label[] arrDate =
                { lblDurRow1, lblDurRow2, lblDurRow3, lblDurRow4, lblDurRow5};
            PictureBox[] arrDel =
                {btnDelRow1, btnDelRow2, btnDelRow3, btnDelRow4, btnDelRow5};
            PictureBox[] arrEdit =
                {btnEditRow1, btnEditRow2, btnEditRow3, btnEditRow4, btnEditRow5};

            app.setListName(arrLblName);
            app.setListPrice(arrLblPrice);
            app.setListVolume(arrLblVol);
            app.setListUSD(arrLblUSD);
            app.setListDate(arrDate);
            app.setListTxt(arrTxt);
            app.setListDel(arrDel);
            app.setListEdit(arrEdit);
            app.loadHrMIn();
            
            String[] row1 = Data.pull(0);
            String[] row2 = Data.pull(1);
            String[] row3 = Data.pull(2);
            String[] row4 = Data.pull(3);
            String[] row5 = Data.pull(4);

            app.showBoard(0, row1);
            app.showBoard(1, row2);
            app.showBoard(2, row3);
            app.showBoard(3, row4);
            app.showBoard(4, row5);

            if (lblNameRow1.Text == "") btnEditMode.Enabled = false;
            foreach (var itm in app.lsTxt) checkFill(itm);
        }

        private void binance888Form_Load(object sender, EventArgs e) {  }

        #region Add and Save BUTTON
        #region fields
        private int lineSpeedHover = 10;
        private bool addHover = false;
        private bool addLeave = false;
        #endregion 
        private void btnADD_MouseHover(object sender, EventArgs e) {
            addHover = true;
            addLeave = false;
        }
        private void btnAddAndSave_MouseLeave(object sender, EventArgs e) {
            addHover = false;
            addLeave = true;
        }
        private void btnAddAndSave_Click(object sender, EventArgs e) {
            if (btnAddAndSave.Text == "Add"){
                app.checkMovement();
                clearTxt();
                if (edit) app.checkEditTool();
            }else {
                app.save();
                clearTxt();
            }

            checkEditEnabled();
        }
        #endregion

        #region TextBox 
        #region fields of animate
        private int lineSpeed = 10;
        private bool tokenFocus = false;
        private bool priceFocus = false;
        private bool volumeFocus = false;
        private bool usdFocus = false;
        private bool timeFocus = false;
        #endregion
        private void txtToken_MouseClick(object sender, MouseEventArgs e) {
            tokenFocus = true;
        }
        private void txtPrice_MouseClick(object sender, MouseEventArgs e) {
            priceFocus = true;
        }
        private void txtVolume_MouseClick(object sender, MouseEventArgs e) {
            volumeFocus = true;
        }
        private void txtUsdVal_MouseClick(object sender, MouseEventArgs e) {
            usdFocus = true;
        }
        private void txtTimeHr_MouseClick(object sender, MouseEventArgs e) {
            timeFocus = true;
        }
        private void txtTimeMin_MouseClick(object sender, MouseEventArgs e) {
        }
        #endregion

        #region Animate
        private void timerAnimate_Tick(object sender, EventArgs e) {
            if (line1.Width <= txtToken.Width && tokenFocus) line1.Width += lineSpeed;
            if (line2.Width <= txtPrice.Width && priceFocus) line2.Width += lineSpeed;
            if (line3.Width <= txtVolume.Width && volumeFocus) line3.Width += lineSpeed;
            if (line4.Width <= txtUsdVal.Width && usdFocus) line4.Width += lineSpeed;
            if (line5.Width <= lblTimeAll.Width && timeFocus) line5.Width += lineSpeed;

            if (line6.Width < btnAddAndSave.Width && addHover) line6.Width += lineSpeedHover;
            else if (line6.Width > 0 && addLeave) line6.Width -= lineSpeedHover;
        }
        #endregion

        #region Validate fill form
        private void checkFill(TextBox txt) {
            if (txt.Text == "") btnAddAndSave.Enabled = false;
            else btnAddAndSave.Enabled =  true;
        }
        private void checkFillFull() { foreach (var itm in app.lsTxt) checkFill(itm); }
        #endregion
        private void clearTxt() {
            txtToken.Text = txtPrice.Text =
                txtVolume.Text = txtUsdVal.Text =
                txtTimeHr.Text = txtTimeMin.Text = "";
        }

        #region EDIT MODE
        private void editMode() {
            clearTxt();

            if (btnEditMode.Text == "Edit") {
                btnEditMode.Text = "X";
                btnAddAndSave.Text = "Add";
                edit = true;
            }
            else {
                btnEditMode.Text = "Edit";
                btnAddAndSave.Text = "Add";
                edit = false;
            }
            btnDelRow1.Visible = btnDelRow2.Visible = 
                btnDelRow3.Visible = btnDelRow4.Visible = btnDelRow5.Visible = edit;
            btnEditRow1.Visible = btnEditRow2.Visible = 
                btnEditRow3.Visible = btnEditRow4.Visible = btnEditRow5.Visible = edit;
            
            if (edit) {
                app.reload();
                app.checkEditTool();
            } 
        }
        private void checkEditEnabled() {
            if (lblNameRow1.Text == "") btnEditMode.Enabled = false;
            else btnEditMode.Enabled = true;
        }
        private void btnEditMode_Click(object sender, EventArgs e) { editMode(); }
        private void btnEditMode_MouseDoubleClick(object sender, MouseEventArgs e) { editMode(); }
        #endregion

        #region Textchange event
        private void txtToken_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        private void txtPrice_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        private void txtVolume_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        private void txtUsdVal_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        private void txtTimeHr_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        private void txtTimeMin_TextChanged(object sender, EventArgs e) { checkFillFull(); }
        #endregion

        #region BUTTON DEL AND EDIT
        // del
        private void btnDelRow1_Click(object sender, EventArgs e) {
            btnEditMode.Text =  app.delRow(0);
            btnAddAndSave.Text = "Add";
            checkEditEnabled();
            clearTxt();
        }

        private void btnDelRow2_Click(object sender, EventArgs e) {
            btnEditMode.Text = app.delRow(1);
            btnAddAndSave.Text = "Add";
            checkEditEnabled();
            clearTxt();
        }

        private void btnDelRow3_Click(object sender, EventArgs e) {
            btnEditMode.Text = app.delRow(2);
            clearTxt();
            checkEditEnabled();
            btnAddAndSave.Text = "Add";
        }

        private void btnDelRow4_Click(object sender, EventArgs e) {
            btnEditMode.Text = app.delRow(3);
            clearTxt();
            checkEditEnabled();
            btnAddAndSave.Text = "Add";
        }

        private void btnDelRow5_Click(object sender, EventArgs e) {
            btnEditMode.Text = app.delRow(4);
            clearTxt();
            checkEditEnabled();
            btnAddAndSave.Text = "Add";
        }

        private void btnEditRow1_Click(object sender, EventArgs e) {
            app.editRow(0);
            checkEditEnabled();
            btnAddAndSave.Text = "Save";
        }

        // Edit 
        private void btnEditRow2_Click(object sender, EventArgs e) {
            app.editRow(1);
            checkEditEnabled();
            btnAddAndSave.Text = "Save";
        }

        private void btnEditRow3_Click(object sender, EventArgs e) {
            app.editRow(2);
            checkEditEnabled();
            btnAddAndSave.Text = "Save";
        }

        private void btnEditRow4_Click(object sender, EventArgs e) {
            app.editRow(3);
            checkEditEnabled();
            btnAddAndSave.Text = "Save";
        }

        private void btnEditRow5_Click(object sender, EventArgs e) {
            app.editRow(4);
            checkEditEnabled();
            btnAddAndSave.Text = "Save";
        }
        #endregion
    }
}
