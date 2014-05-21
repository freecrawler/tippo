namespace client
{
    using System;

    public class shipTempP : enumP
    {
        public shipTempP(fatherObj fatherObj_0)
        {
            base.key = "shipTemp";
            base.name = "运费模板";
            base.dataBaseFieldForSave = "f46";
            base.dataBaseFieldForList = "f47";
            base.dataBaseFieldForSearch = "f48";
            base.haveBaseData = true;
            base.baseDataIsOwn = true;
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void setValueFromMap()
        {
            base.value = text.mapTextId(base.oldPro.shipTemp.value, base.baseData, base.isInput, 80, base.fatherObj.defaultLanguage);
        }
    }
}

