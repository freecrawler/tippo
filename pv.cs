namespace client
{
    using System;

    [Serializable]
    public class pv
    {
        public textID p = new textID();
        public textID v = new textID();

        public string toString()
        {
            return (this.p.text + ":" + this.v.text);
        }
    }
}

