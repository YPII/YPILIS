﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlideCollection : ObservableCollection<VantageSlide>
    {
        private string m_MasterAccessionNo;

        public VantageSlideCollection(string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.Load();
        }

        public void HandleSlideScan(string vantageSlideId, string scanType, string location)
        {
            VantageSlide slide = null;
            if (this.Exists(vantageSlideId) == false)
            {
                slide = new VantageSlide();
                slide.MasterAccessionNo = this.m_MasterAccessionNo;
                slide.VantageSlideId = vantageSlideId;
                if (scanType == "Receiving") slide.CurrentLocation = "YPIBLGS";
                if (scanType == "Shipping") slide.CurrentLocation = "YPBZM";
                this.Add(slide);
            }
            else
            {
                slide = this.Get(vantageSlideId);
            }

            VantageSlideScan slideScan = new VantageSlideScan();
            slideScan.Location = location;
            slideScan.ScanDate = DateTime.Now;
            slideScan.SlideId = vantageSlideId;
            slide.SlideScans.Add(slideScan);

            string jsonSlide = slide.ToJson();

            Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).DataBase.Execute("json.set", new string[] { slide.GetRedisKey(), ".", jsonSlide });
            
        }

        public VantageSlide Get(string vantageSlideId)
        {
            VantageSlide result = null; ;
            foreach (VantageSlide slide in this)
            {
                if (slide.VantageSlideId == vantageSlideId)
                {
                    result = slide;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string vantageSlideId)
        {
            bool result = false;
            foreach(VantageSlide slide in this)
            {
                if(slide.VantageSlideId == vantageSlideId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void Load()
        {
            string[] results = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).GetAllJSONKeys(this.m_MasterAccessionNo);
            foreach(string result in results)
            {
                VantageSlide vantageSlide = VantageSlide.FromJson(result);
                this.Add(vantageSlide);
            }
        }
    }
}