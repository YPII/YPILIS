using System;
using System.Collections.ObjectModel;


namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlideViewCollection : ObservableCollection<VantageSlideView>
    {
        private string m_MasterAccessionNo;

        public VantageSlideViewCollection(string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.Load();
        }

        public void HandleSlideScan(string vantageSlideId)
        {
            VantageSlideView view = null;

            YellowstonePathology.Business.Facility.Model.Facility thisFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string thisLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            if (this.Exists(vantageSlideId) == false)
            {
                VantageSlide slide = new VantageSlide();
                slide.MasterAccessionNo = this.m_MasterAccessionNo;
                slide.VantageSlideId = vantageSlideId;
                slide.CurrentLocation = thisFacility.FacilityId;

                view = new VantageSlideView(slide, System.Windows.Media.Brushes.LightGreen);
                this.Add(view);
            }
            else
            {
                view = this.Get(vantageSlideId);
                view.ScanStatusColor = System.Windows.Media.Brushes.LightGreen;            }

            VantageSlideScan slideScan = new VantageSlideScan();
            slideScan.Location = thisLocation;
            slideScan.ScanDate = DateTime.Now;
            slideScan.SlideId = vantageSlideId;
            slideScan.ScannedBy = Business.User.SystemIdentity.Instance.User.UserName;
            view.VantageSlide.SlideScans.Add(slideScan);

            string jsonSlide = view.VantageSlide.ToJson();

            Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).DataBase.Execute("json.set", new string[] { view.VantageSlide.GetRedisKey(), ".", jsonSlide });

        }

        public VantageSlideView Get(string vantageSlideId)
        {
            VantageSlideView result = null; ;
            foreach (VantageSlideView view in this)
            {
                if (view.VantageSlide.VantageSlideId == vantageSlideId)
                {
                    result = view;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string vantageSlideId)
        {
            bool result = false;
            foreach (VantageSlideView view in this)
            {
                if (view.VantageSlide.VantageSlideId == vantageSlideId)
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
            foreach (string result in results)
            {
                VantageSlide vantageSlide = VantageSlide.FromJson(result);
                VantageSlideView view = new Model.VantageSlideView(vantageSlide, System.Windows.Media.Brushes.White);
                this.Add(view);
            }
        }

        public void SetLocation(string location)
        {
            foreach (VantageSlideView view in this)
            {
                view.VantageSlide.CurrentLocation = location;
                string jsonSlide = view.VantageSlide.ToJson();
                Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).DataBase.Execute("json.set", new string[] { view.VantageSlide.GetRedisKey(), ".", jsonSlide });
            }
        }
    }
}
