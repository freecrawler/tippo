namespace client
{
    using System;

    public class summaryP : inputText
    {
        public summaryP(fatherObj fatherObj_0)
        {
            base.key = "summary";
            base.name = "简要描述";
            base.dataBaseFieldForSave = "f36";
            base.dataBaseFieldForList = "f36";
            base.dataBaseFieldForSearch = "f36";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void setValueFromMap()
        {
            base.value = base.oldPro.summary.value;
            if (base.value.Length == 0)
            {
                base.value = base.fatherObj.name.value;
            }
        }
    }
}

