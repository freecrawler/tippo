namespace client
{
    using System;
    using System.Collections.Generic;

    public class unitP : enumP
    {
        public unitP(fatherObj fatherObj_0)
        {
            base.key = "unit";
            base.name = "计量单位";
            base.dataBaseFieldForSave = "f40";
            base.dataBaseFieldForList = "f41";
            base.dataBaseFieldForSearch = "f42";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public virtual Dictionary<string, string> getMapData(string string_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (textID tid in base.baseData)
            {
                if (string_0 == "")
                {
                    dictionary[tid.GetFullText] = tid.id;
                }
                else
                {
                    dictionary[func.正则获取(tid.text[string_0], @"([^\/]*)")] = tid.id;
                }
            }
            return dictionary;
        }

        public override void setValueFromMap()
        {
            if (base.fatherObj.Store.isOwn)
            {
                base.value = base.oldPro.unit.value;
            }
            else
            {
                using (List<textID>.Enumerator enumerator = base.baseData.GetEnumerator())
                {
                    textID current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.id == base.mapObj.units[base.oldPro.unit.value.text[base.oldPro.defaultLanguage]])
                        {
                            goto Label_008D;
                        }
                    }
                    return;
                Label_008D:
                    base.value = current;
                }
            }
        }
    }
}

