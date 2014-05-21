namespace client
{
    using System;
    using System.Collections.Generic;

    public class rule
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public double defaluD;
        public string defaluT = "";
        public bool isMust;
        public int maxLength;
        public double maxValue;
        public int minLength;
        public double minValue;
        public int num = 10;
    }
}

