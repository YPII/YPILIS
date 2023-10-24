using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class Icd9CodeCollectionView : ObservableCollection<Icd9CodeView>
    {
        public Icd9CodeCollectionView()
        {
            this.Add(new Icd9CodeView("V76.2", "Cytology"));
            this.Add(new Icd9CodeView("V72.31", "Cytology"));
            this.Add(new Icd9CodeView("V76.47", "Cytology"));
            this.Add(new Icd9CodeView("V15.89", "Cytology"));
            this.Add(new Icd9CodeView("V76.49", "Cytology"));

            this.Add(new Icd9CodeView("V22.0", "Cytology/NGCT"));
            this.Add(new Icd9CodeView("V22.1", "Cytology/NGCT"));
            this.Add(new Icd9CodeView("V22.2", "Cytology/NGCT"));
            this.Add(new Icd9CodeView("V24.1", "Cytology/NGCT"));
            this.Add(new Icd9CodeView("V24.2", "Cytology/NGCT"));

            this.Add(new Icd9CodeView("V73.81", "HPV"));

            this.Add(new Icd9CodeView("V74.5", "NGCT"));
            this.Add(new Icd9CodeView("V01.6", "NGCT"));
            this.Add(new Icd9CodeView("V73.98", "NGCT"));
            this.Add(new Icd9CodeView("V73.88", "NGCT"));
            this.Add(new Icd9CodeView("V73.89", "NGCT"));  
        }
    }
}
