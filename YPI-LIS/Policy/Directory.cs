using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YellowstonePathology.Policy
{
    public class Directory
    {        
        private string m_Name;
        private string m_Path;        
        private List<Directory> m_Subdirectories;

        private bool m_IsNew;
        private bool m_IsModified;

        public Directory(string dirName, string path)
        {
            this.m_Name = dirName;
            this.m_Path = path;
            this.m_Subdirectories = new List<Directory>();
        }        

        public List<Directory> Subdirectories
        {
            get { return this.m_Subdirectories; }
        }        

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }    
        
        public string Path
        {
            get { return this.m_Path; }
            set { this.m_Path = value; }
        }                
        
        public bool IsNew
        {
            get { return this.m_IsNew; }
            set { this.m_IsNew = value; }
        }

        public bool IsModified
        {
            get { return this.m_IsModified; }
            set { this.m_IsModified = value; }
        }
    }
}
