using System;
using System.IO;

namespace BINANCE888 {
    static class Data {
        /* static private string path = "..\\..\\";*/   //dev
        private static string path1 = "..\\..\\res\\Data1.txt";
        private static string path2 = "..\\..\\res\\Data2.txt";
        private static string path3 = "..\\..\\res\\Data3.txt";
        private static string path4 = "..\\..\\res\\Data4.txt";
        private static string path5 = "..\\..\\res\\Data5.txt";

        //private static string path1 = "res\\Data1.txt";
        //private static string path2 = "res\\Data2.txt";
        //private static string path3 = "res\\Data3.txt";
        //private static string path4 = "res\\Data4.txt";
        //private static string path5 = "res\\Data5.txt";
        public static void push(int idx, string data) {
            if (idx == 0) {
                File.WriteAllText(path1, String.Empty);
                File.WriteAllText(path1, data);
            } else if (idx == 1) {
                File.WriteAllText(path2, String.Empty);
                File.WriteAllText(path2, data);
            } else if (idx == 2) {
                File.WriteAllText(path3, String.Empty);
                File.WriteAllText(path3, data);
            } else if (idx == 3) {
                File.WriteAllText(path4, String.Empty);
                File.WriteAllText(path4, data);
            }
            else if (idx == 4) {
                File.WriteAllText(path5, String.Empty);
                File.WriteAllText(path5, data);
            }
            else {

            }
        }

        public static string[] pull(int idx) {
            string str = "";
            string[] arr = {};
            if (idx == 0 && File.ReadAllText(path1) != "") {
                str = File.ReadAllText(path1);
                arr = str.Split(' ');
            }
            if (idx == 1 && File.ReadAllText(path2) != "") {
                str = File.ReadAllText(path2);
                arr = str.Split(' ');
            }
            if (idx == 2 && File.ReadAllText(path3) != "") {
                str = File.ReadAllText(path3);
                arr = str.Split(' ');
            }
            if (idx == 3 && File.ReadAllText(path4) != "") {
                str = File.ReadAllText(path4);
                arr = str.Split(' ');
            }
            if (idx == 4 && File.ReadAllText(path5) != "") {
                str = File.ReadAllText(path5);
                arr = str.Split(' ');
            }
            return arr;
        }

        public static void delData(int idx) {
            if (idx == 0) File.WriteAllText(path1, String.Empty);
            if (idx == 1) File.WriteAllText(path2, String.Empty);
            if (idx == 2) File.WriteAllText(path3, String.Empty);
            if (idx == 3) File.WriteAllText(path4, String.Empty);
            if (idx == 4) File.WriteAllText(path5, String.Empty);
        }
    }
}
