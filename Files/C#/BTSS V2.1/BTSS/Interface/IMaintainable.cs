using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biztech.Interface
{
    interface IMaintainable
    {

        #region"Methods"
        
        bool New();
        bool Edit();
        bool Delete();
        bool Save();
        bool Cancel();
        void Initialize();
        void InitializeGrid();
        void InitializeGridLayout();
        void Lock(bool isControlLock);
        void ClearControls();
        void ControlToClass();
        void RefreshListing();

        #endregion
    }
}
