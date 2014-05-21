namespace client
{
    using System;

    public class groupP : enumP
    {
        public groupP(fatherObj fatherObj_0)
        {
            base.key = "group";
            base.name = "产品组";
            base.dataBaseFieldForSave = "f33";
            base.dataBaseFieldForList = "f34";
            base.dataBaseFieldForSearch = "f35";
            base.haveBaseData = true;
            base.baseDataIsOwn = true;
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void setValueFromMap()
        {
            string str = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
            base.value = text.mapTextId(base.oldPro.group.value, base.baseData, base.isInput, 80, base.fatherObj.defaultLanguage);
            if ((base.value.id == "") && (base.fatherObj.cate.value.Count > 0))
            {
                base.value.text[base.fatherObj.defaultLanguage] = base.fatherObj.cate.value[base.fatherObj.cate.value.Count - 1].value.text[str];
                base.value.id = "other";
            }
        }
    }
}

