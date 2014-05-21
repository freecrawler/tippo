namespace client
{
    using csExWB;
    using IfacesEnumsStructsClasses;
    using System;
    using System.Collections.Generic;

    public class baseP
    {
        public bool baseDataIsOwn;
        public string baseDataLinkId = "";
        public string dataBaseFieldForList = "";
        public string dataBaseFieldForSave = "";
        public string dataBaseFieldForSearch = "";
        public client.fatherObj fatherObj;
        public bool haveBaseData;
        public bool isBatch;
        public bool isMust;
        public string key;
        public int listWidth = 120;
        public mapping mapObj;
        public string name = "";
        public product oldPro;
        public Dictionary<string, rule> rules = new Dictionary<string, rule>();
        public string searchName = "";
        public string tipTextForBatch = "";
        public string tipTextForEdit = "";
        public string tipTextForSearch = "";
        public cEXWB webbrowser;

        public virtual void batchEdit()
        {
        }

        public IHTMLElement elem(string string_0)
        {
            return this.webbrowser.GetElementByID(true, this.key + ">" + string_0);
        }

        public IHTMLElement elem2(string string_0)
        {
            return this.webbrowser.GetElementByID(true, string_0);
        }

        public virtual void forceEdit()
        {
        }

        public virtual void getBaseDataFromDataBase()
        {
        }

        public virtual void getBaseDataFromWeb()
        {
        }

        public virtual string getDataBaseFromValue()
        {
            return "";
        }

        public virtual string getListCacheFromValue()
        {
            return "";
        }

        public virtual string getSearchCacheFromValue()
        {
            return "";
        }

        public virtual string getSearchCondition()
        {
            return "";
        }

        public virtual void getTranslate()
        {
        }

        public virtual string getUploadDataFromValue()
        {
            return "";
        }

        public virtual object getValueFromBatchC()
        {
            return null;
        }

        public virtual void getValueFromDataBase(string string_0)
        {
        }

        public virtual void getValueFromEditC()
        {
        }

        public virtual object getValueFromSearchC()
        {
            return null;
        }

        public virtual void getValueFromWeb()
        {
        }

        public virtual void handleBatchUserChange(string string_0)
        {
        }

        public virtual void handleBatchValueChange(string string_0)
        {
        }

        public virtual void handleEditUserChange(string string_0)
        {
        }

        public virtual void handleEditValueChange(string string_0)
        {
            this.getValueFromEditC();
            string str = this.verifyEdit();
            if (str != "")
            {
                this.elem("err").innerHTML = str;
                this.elem("err").style.display = "block";
            }
            else
            {
                this.elem("err").style.display = "none";
            }
        }

        public virtual void handleSearchUserChange(string string_0)
        {
        }

        public virtual void handleSearchValueChange(string string_0)
        {
        }

        public virtual void initValue()
        {
        }

        public virtual void saveBaseData()
        {
        }

        public virtual void setBatchControl()
        {
        }

        public virtual void setEditControl()
        {
        }

        public virtual void setSearchControl()
        {
        }

        public virtual void setTranslate()
        {
        }

        public virtual void setValueFromMap()
        {
        }

        public virtual void uploadBaseData()
        {
        }

        public virtual string verifyBatch(object object_0)
        {
            return "";
        }

        public virtual string verifyEdit()
        {
            return "";
        }

        public virtual string verifySearch(object object_0)
        {
            return "";
        }

        public IHTMLElement control
        {
            get
            {
                return this.webbrowser.GetElementByID(true, this.key + ">control");
            }
        }

        public string KeyId
        {
            get
            {
                return (this.key + ">");
            }
        }
    }
}

