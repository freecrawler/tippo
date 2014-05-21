namespace client
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Threading;

    [ComVisible(true), PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    public class handleWebbrowserBase
    {
        public string Value = "";

        public event myEventEventHandler handelUserChange;

        public event myEventEventHandler handelValueChange;

        public virtual string getValue(string string_0)
        {
            this.Value = "";
            myEventArgs e = new myEventArgs();
            e.id = string_0;
            this.myEventEventHandler_1(this, e);
            return this.Value;
        }

        public virtual void userChange(string string_0)
        {
            myEventArgs e = new myEventArgs();
            e.id = string_0;
            this.myEventEventHandler_1(this, e);
        }

        public virtual void valueChange(string string_0)
        {
            myEventArgs e = new myEventArgs();
            e.id = string_0;
            this.myEventEventHandler_0(this, e);
        }
    }
}

