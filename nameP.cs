namespace client
{
    using System;

    public class nameP : inputText
    {
        public nameP(fatherObj fatherObj_0)
        {
            base.key = "name";
            base.name = "产品名称";
            base.dataBaseFieldForSave = "f32";
            base.dataBaseFieldForList = "f32";
            base.dataBaseFieldForSearch = "f32";
            base.listWidth = 200;
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void setValueFromMap()
        {
            base.value = base.oldPro.name.value;
        }
    }
}

