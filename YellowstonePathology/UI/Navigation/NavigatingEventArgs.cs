using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Navigation
{
    public class NavigatingEventArgs : EventArgs
    {
        private ContentControl m_NavigatingFromPage;
        private ContentControl m_NavigatingToPage;

        public NavigatingEventArgs(ContentControl navigatingFromPage, ContentControl navigatingToPage)
        {
            this.m_NavigatingFromPage = navigatingFromPage;
            this.m_NavigatingToPage = navigatingToPage;
        }

        public ContentControl NavigatingFromPage
        {
            get { return this.m_NavigatingFromPage; }
        }

        public ContentControl NavigatingToPage
        {
            get { return this.m_NavigatingToPage; }
        }
    }
}
