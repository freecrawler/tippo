namespace client
{
    using csExWB;
    using System;

    public class batchEditPic : fatherObj
    {
        public batchEditPic(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "batch";
            base.pics = new picsP(this);
            base.pics.isBatch = true;
            base.des = new desP(this);
            base.des.isBatch = true;
            base.saleAttr = new saleAttrP(this);
            base.saleAttr.isBatch = true;
        }

        public void saveEditPic()
        {
            base.pics.replacePics(base.pics.allBatchPics);
            base.des.getPics();
            base.des.replacePics(base.pics.allBatchPics);
            base.saleAttr.replacePics(base.pics.allBatchPics);
        }
    }
}

