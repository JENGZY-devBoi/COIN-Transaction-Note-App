using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BINANCE888 {
    static class app {
        public static List<TextBox> lsTxt = new List<TextBox>();
        private static List<PictureBox> lsDel = new List<PictureBox>();
        private static List<PictureBox> lsEdit = new List<PictureBox>();
        private static List<Label> lsName = new List<Label>();
        private static List<Label> lsPrice = new List<Label>();
        private static List<Label> lsVol = new List<Label>();
        private static List<Label> lsUSD = new List<Label>();
        private static List<Label> lsDate = new List<Label>();
        private static List<String> lsTxtHr = new List<String>();
        private static List<String> lsTxtMin = new List<String>();
        private static string _data = "";

        public static int sizeOfList = lsName.Count;

        public static void setListName(params Label[] arr) { lsName.AddRange(arr); }
        public static void setListPrice(params Label[] arr) { lsPrice.AddRange(arr); }
        public static void setListVolume(params Label[] arr) { lsVol.AddRange(arr); }
        public static void setListUSD(params Label[] arr) { lsUSD.AddRange(arr); }
        public static void setListDate(params Label[] arr) { lsDate.AddRange(arr); }
        public static void setListTxt(params TextBox[] arr) { lsTxt.AddRange(arr); }
        public static void setListDel(params PictureBox[] arr) { lsDel.AddRange(arr); }
        public static void setListEdit(params PictureBox[] arr) { lsEdit.AddRange(arr); }

        // DISPLAY METHOD
        private static void showStr(Label lbl, TextBox txt) {
            _data += lbl.Text = $"{txt.Text}";
            _data += " ";
        }

        private static void showDbl(Label lbl, TextBox txt) { 
            _data += lbl.Text = $"{Convert.ToDouble(txt.Text).ToString("0.00")}";
            _data += " ";
        }

        private static void showDate(Label lblDur, TextBox txtHr, TextBox txtMin, int i) {
            _data += lblDur.Text = $"{DateTime.Now.ToString("dd/MM/yyyy")} " +
                          $"{(txtHr.Text.Length < 2 ? "0" + txtHr.Text : txtHr.Text)}:" +
                          $"{(txtMin.Text.Length < 2 ? "0" + txtMin.Text : txtMin.Text)}";
        }


        // Load app
        public static void showBoard(int i ,params string[] arr) {
            for (int idx = 0; idx < arr.Length; idx++) {
                if (arr.Length > 0 && arr[idx] != "") {
                    lsName[i].Text = arr[0];
                    lsPrice[i].Text = arr[1];
                    lsVol[i].Text = arr[2];
                    lsUSD[i].Text = arr[3];
                    lsDate[i].Text = arr[4] + " " + arr[5];
                }
            }
        }
        ///////

        public static void checkMovement() {
            lsTxtHr.Add(lsTxt[4].Text);
            lsTxtMin.Add(lsTxt[5].Text);

            for (int i = 0; i < lsName.Count(); i++) {
                if (lsName[i].Text == "") {
                    showStr(lsName[i], lsTxt[0]);
                    showDbl(lsPrice[i], lsTxt[1]);
                    showDbl(lsVol[i], lsTxt[2]);
                    showDbl(lsUSD[i], lsTxt[3]);
                    showDate(lsDate[i], lsTxt[4], lsTxt[5], i);
                    
                    // save to txt file
                    Data.push(i, _data);
                    _data = "";
                    break;
                }
            }
        }
        public static void reload() {
            for (int i = 1; i < lsName.Count(); i++) {
                _data = "";
                if (lsName[i - 1].Text == "") {
                    _data += lsName[i - 1].Text = lsName[i].Text;
                    _data += " ";
                    _data += lsPrice[i - 1].Text = lsPrice[i].Text;
                    _data += " ";
                    _data += lsVol[i - 1].Text = lsVol[i].Text;
                    _data += " ";
                    _data += lsUSD[i - 1].Text = lsUSD[i].Text;
                    _data += " ";
                    _data += lsDate[i - 1].Text = lsDate[i].Text;
                    lsName[i].Text = lsPrice[i].Text = lsVol[i].Text = lsUSD[i].Text = lsDate[i].Text = "";
                    Data.push(i-1, _data);
                    Data.delData(i);


                    for (int k = i + 1; k < lsName.Count(); k++) {
                        _data = "";
                        if (lsName[k - 1].Text == "") {
                            _data += lsName[k - 1].Text = lsName[k].Text;
                            _data += " ";
                            _data += lsPrice[k - 1].Text = lsPrice[k].Text;
                            _data += " ";
                            _data += lsVol[k - 1].Text = lsVol[k].Text;
                            _data += " ";
                            _data += lsUSD[k - 1].Text = lsUSD[k].Text;
                            _data += " ";
                            _data += lsDate[k - 1].Text = lsDate[k].Text;
                            _data += lsName[k].Text = lsPrice[k].Text = lsVol[k].Text = lsUSD[k].Text = lsDate[k].Text = "";
                            Data.push(k - 1, _data);
                            Data.delData(k);
                        }
                    }
                }
            }
        }

        public static void checkEditTool() {
            int i = 0;
            foreach (var itm in lsName) {
                lsDel[i].Visible = lsEdit[i].Visible = (itm.Text == "") ? false : true;
                i++;
            }
        }

        private static string checkEmptyData() {
            string mode = "";
            for (int i = lsName.Count; i > 0; i--) mode = (lsDel[i - 1].Visible == false) ?  "Edit" : "X";
            return mode;
        }

        public static string Slice(this string source, int start, int end) {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }

        public static void loadHrMIn() {
            foreach (var itm in lsDate) {
                if (itm.Text != "") {
                    Console.WriteLine(itm.Text.Length);
                    lsTxtHr.Add(itm.Text.Slice(11, 13));
                    lsTxtMin.Add(itm.Text.Slice(14, 16));
                    Console.WriteLine(lsTxtHr);
                    Console.WriteLine(lsTxtMin);
                }
            }
        }

        public static string delRow(int idx) {
            Data.delData(idx);
            lsName[idx].Text = lsPrice[idx].Text = lsVol[idx].Text = lsUSD[idx].Text = lsDate[idx].Text = "";
            foreach (var itm in lsTxt) itm.Text = "";
            reload();
            checkEditTool();
            string mode = checkEmptyData();
            return mode;
        }

        // EDIT ZONE
        private static int indexEdit = 0;
        private static void editData (int idx) {
            lsTxt[0].Text = lsName[idx].Text;
            lsTxt[1].Text = lsPrice[idx].Text;
            lsTxt[2].Text = lsVol[idx].Text;
            lsTxt[3].Text = lsUSD[idx].Text;
            //lsTxt[4].Text = lsTxtHr[idx].Length < 2 ? "0" + lsTxtHr[idx] : lsTxtHr[idx];
            //lsTxt[5].Text = lsTxtMin[idx].Length < 2 ? "0" + lsTxtMin[idx] : lsTxtMin[idx];
            lsTxt[4].Text = "--";
            lsTxt[5].Text = "--";
        }

        public static void editRow(int idx) {
            indexEdit = idx;
            editData(idx);
        }

        public static void save() {
            showStr(lsName[indexEdit], lsTxt[0]);
            showDbl(lsPrice[indexEdit], lsTxt[1]);
            showDbl(lsVol[indexEdit], lsTxt[2]);
            showDbl(lsUSD[indexEdit], lsTxt[3]);
            showDate(lsDate[indexEdit], lsTxt[4], lsTxt[5], indexEdit);
            Data.push(indexEdit, _data);
            _data = "";
        }

    }
}
