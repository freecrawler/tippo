namespace client
{
    using System;

    public class validityDayP : enumP
    {
        public validityDayP(fatherObj fatherObj_0)
        {
            base.key = "validityDay";
            base.name = "有效期";
            base.dataBaseFieldForSave = "f43";
            base.dataBaseFieldForList = "f44";
            base.dataBaseFieldForSearch = "f45";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void setValueFromMap()
        {
            base.value = text.mapTextId(base.oldPro.validityDay.value, base.baseData, base.isInput, 80, base.fatherObj.defaultLanguage);
        }
    }
}

