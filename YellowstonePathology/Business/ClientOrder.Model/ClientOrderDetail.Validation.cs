using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrderDetail
	{
        private Dictionary<string, string> m_ValidationErrors;

        private string m_CollectionDateBinding;
        private string m_FixationStartTimeBinding;        

        public void ValidateObject()
        {
            this.ValidateCollectionDate();
            this.ValidateFixationComment();
            this.ValidateContainerId();
            this.ValidateDescriptionToAccession();            
            this.ValidateFixationStartTime();
            this.ValidateClientFixation();
            this.ValidateLabFixation();
        }

        public System.Windows.Media.SolidColorBrush CollectionDateBackgroundColorBinding
        {
            get
            {
                System.Windows.Media.SolidColorBrush brush = System.Windows.Media.Brushes.White;

                if (this.m_ValidationErrors.ContainsKey("CollectionDateBinding") == false)
                {
                    System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
                    if (this.m_CollectionDate.HasValue == true)
                    {
                        if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_CollectionDate) == true)
                        {
                            brush = brushConverter.ConvertFromString("#5CF353") as System.Windows.Media.SolidColorBrush;
                        }
                        else
                        {
                            brush = brushConverter.ConvertFromString("#EAF353") as System.Windows.Media.SolidColorBrush;
                        }
                    }
                    else
                    {
                        brush = brushConverter.ConvertFromString("#F35364") as System.Windows.Media.SolidColorBrush;
                    }
                }                
                
                return brush;			
            }
        }

        public string FixationCommentBinding
        {
            get { return this.m_FixationComment; }
            set
            {
                this.m_FixationComment = value;
                this.ValidateFixationComment();                   
            }
        }

        public void ValidateFixationComment()
        {
            if (this.m_ValidationErrors.ContainsKey("FixationCommentBinding") == true) this.m_ValidationErrors.Remove("FixationCommentBinding");
            if (this.m_FixationStartTimeManuallyEntered == true && string.IsNullOrEmpty(this.m_FixationComment) == true)
            {
                this.m_ValidationErrors.Add("FixationCommentBinding", "A comment is required when the Fixation Start Time has been manually set.");
            }
            this.NotifyPropertyChanged("FixationCommentBinding");
        }

        public string ContainerIdBinding
        {
            get { return this.m_ContainerId; }
            set
            {
                this.m_ContainerId = value;
                this.ValidateContainerId();                
            }
        }

        public void ValidateContainerId()
        {
            if (this.m_ValidationErrors.ContainsKey("ContainerIdBinding") == true) this.m_ValidationErrors.Remove("ContainerIdBinding");
            if (string.IsNullOrEmpty(this.m_ContainerId) == true)
            {
                this.m_ValidationErrors.Add("ContainerIdBinding", "The container id should not be blank.");
            }
            this.NotifyPropertyChanged("ContainerIdBinding");
        }

        public string DescriptionToAccessionBinding
        {
            get { return this.m_DescriptionToAccession; }
            set
            {
                this.m_DescriptionToAccession = value;                
            }
        }

        public void ValidateDescriptionToAccession()
        {
            if (this.m_ValidationErrors.ContainsKey("DescriptionToAccessionBinding") == true) this.m_ValidationErrors.Remove("DescriptionToAccessionBinding");
            if (string.IsNullOrEmpty(this.m_DescriptionToAccession) == true)
            {
                this.m_ValidationErrors.Add("DescriptionToAccessionBinding", "The Accessioned As field cannot be blank.");
            }
            this.NotifyPropertyChanged("DescriptionToAccessionBinding");
        }

        public string ClientFixationBinding
        {
            get { return this.m_ClientFixation; }
            set
            {
                this.m_ClientFixation = value;
                this.ValidateClientFixation();
            }
        }

        public void ValidateClientFixation()
        {
            if (this.m_ValidationErrors.ContainsKey("ClientFixationBinding") == true) this.m_ValidationErrors.Remove("ClientFixationBinding");
            if (string.IsNullOrEmpty(this.m_ClientFixation) == true)
            {
                this.m_ValidationErrors.Add("ClientFixationBinding", "The Received In field cannot be blank.");
            }
            this.NotifyPropertyChanged("ClientFixationBinding");
        }

        public string LabFixationBinding
        {
            get { return this.m_LabFixation; }
            set
            {
                this.m_LabFixation = value;
                this.ValidateLabFixation();
            }
        }

        public void ValidateLabFixation()
        {
            if (this.m_ValidationErrors.ContainsKey("LabFixationBinding") == true) this.m_ValidationErrors.Remove("LabFixationBinding");
            if (string.IsNullOrEmpty(this.m_LabFixation) == true)
            {
                this.m_ValidationErrors.Add("LabFixationBinding", "The Processed In field cannot be blank.");
            }
            this.NotifyPropertyChanged("LabFixationBinding");
        }

        public string CollectionDateBinding
        {
            get
            {
                string result = null;

                if (this.m_ValidationErrors.ContainsKey("CollectionDateBinding") == false)
                {
                    if (this.m_CollectionDate.HasValue == true)
                    {
                        this.m_CollectionDateBinding = this.m_CollectionDate.Value.ToString("MM/dd/yyy HH:mm");
                        result = this.m_CollectionDateBinding;
                    }
                    else
                    {
                        this.m_CollectionDateBinding = null;
                        result = this.m_CollectionDateBinding;
                    }
                }
                else
                {
                    return this.m_CollectionDateBinding;
                }
                return result;
            }
            set
            {                
                string formattedString = YellowstonePathology.Business.Helper.DateTimeExtensions.AddSlashesSpacesAndColon(value);
                this.m_CollectionDateBinding = formattedString;

                DateTime collectionDate;
                bool isGoodDate = DateTime.TryParse(formattedString, out collectionDate);
                if (isGoodDate == true)
                {
                    this.m_CollectionDate = collectionDate;
                    this.SetFixationStartTime();
                }
                else
                {
                    this.m_CollectionDateBinding = value;
                }
                
                this.ValidateCollectionDate();                
            }
        }

        public void ValidateCollectionDate()
        {
            if (this.m_ValidationErrors.ContainsKey("CollectionDateBinding") == true) this.m_ValidationErrors.Remove("CollectionDateBinding");

            DateTime collectionDate;
            bool isGoodDate = DateTime.TryParse(this.m_CollectionDateBinding, out collectionDate);
            if (isGoodDate == true)
            {
                if (this.m_ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.PreservCyt)
                {
                    if (this.m_FixationStartTime.HasValue == true && this.m_CollectionDate.HasValue == true && this.m_FixationStartTime < this.m_CollectionDate)
                    {
                        this.AddError("CollectionDateBinding", "The Collection Time cannot be after the Fixation Start Time.");                        
                    }
                }                
            }
            else
            {
                this.m_ValidationErrors.Add("CollectionDateBinding", "The Collection Date is invalid");                
            }

            this.NotifyPropertyChanged("CollectionDateBinding");            
            this.NotifyPropertyChanged("CollectionDate");
            this.NotifyPropertyChanged("CollectionDateBackgroundColorBinding");
        }

        public string FixationStartTimeBinding
        {
            get
            {
                string result = null;
                if (this.m_ValidationErrors.ContainsKey("FixationStartTimeBinding") == false)
                {
                    if (this.m_FixationStartTime.HasValue == true)
                    {
                        this.m_FixationStartTimeBinding = this.m_FixationStartTime.Value.ToString("MM/dd/yyy HH:mm");
                        result = this.m_FixationStartTimeBinding;
                    }
                    else
                    {
                        this.m_FixationStartTimeBinding = null;
                        result = this.m_FixationStartTimeBinding;
                    }
                }
                else
                {
                    return this.m_FixationStartTimeBinding;
                }
                return result;
            }
            set
            {
                string formattedString = YellowstonePathology.Business.Helper.DateTimeExtensions.AddSlashesSpacesAndColon(value);
                this.m_FixationStartTimeBinding = formattedString;

                DateTime fixationStartTime;
                bool isGoodDate = DateTime.TryParse(formattedString, out fixationStartTime);
                if (isGoodDate == true)
                {
                    this.m_FixationStartTime = fixationStartTime;                    
                }
                else
                {
                    this.m_FixationStartTimeBinding = value;
                }

                this.ValidateFixationStartTime();                
            }
        }

        public void ValidateFixationStartTime()
        {
            if (this.m_ValidationErrors.ContainsKey("FixationStartTimeBinding") == true) this.m_ValidationErrors.Remove("FixationStartTimeBinding");

            DateTime fixationStartTime;
            bool isGoodDate = DateTime.TryParse(this.m_FixationStartTimeBinding, out fixationStartTime);
            if (isGoodDate == true)
            {
                if (this.m_CollectionDate.HasValue == true)
                {
                    if (fixationStartTime < this.m_CollectionDate)
                    {
                        this.m_ValidationErrors.Add("FixationStartTimeBinding", "The Fixation Start Time cannot be before the Collection Date.");
                    }
                }

                if (this.m_DateReceived.HasValue == true)
                {
                    if (fixationStartTime > this.m_DateReceived)
                    {
                        this.m_ValidationErrors.Add("FixationStartTimeBinding", "The Fixation Start Time cannot be after the Date Received.");
                    }
                }
            }
            else
            {
                if (this.m_ClientAccessioned == false)
                {
                    if (this.m_ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable)
                    {
                        if (string.IsNullOrEmpty(this.m_FixationStartTimeBinding) == true)
                        {
                            if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_CollectionDate) == true)
                            {
                                this.m_ValidationErrors.Add("FixationStartTimeBinding", "The Fixation Start Time should not be blank.");
                            }
                        }
                        else
                        {
                            this.m_ValidationErrors.Add("FixationStartTimeBinding", "The Fixation Start Time is invalid.");
                        }
                    }
                }
            }

            this.NotifyPropertyChanged("FixationStartTimeBinding");
            this.NotifyPropertyChanged("FixationStartTime");
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (this.m_ValidationErrors.ContainsKey(columnName) == true)
                {
                    result = this.m_ValidationErrors[columnName];
                }
                return result;                
            }
        }

        public string Error
        {
            get
            {
                string result = null;
                if (this.m_ValidationErrors.Count > 0)
                {
                    result = "There are errors that need to handled.";
                }
                return result;
            }
        }

        public void AddError(string proteryName, string message)
        {
            if (this.m_ValidationErrors.ContainsKey(proteryName) == false)
            {
                this.m_ValidationErrors.Add(proteryName, message);
            }
        }

        private void RemoveError(string propertyName)
        {
            if (this.m_ValidationErrors.ContainsKey(propertyName) == true)
            {
                this.m_ValidationErrors.Remove(propertyName);
            }
        }

        public Dictionary<string, string> ValidationErrors
        {
            get { return this.m_ValidationErrors; }
        }
	}
}
