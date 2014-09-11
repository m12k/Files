using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces
{
    public interface IPreferences
    {

        int Id { get; set; }
        string Tag { get; set; }
        string Value { get; set; }
        BTSS.Common.Core.Operation Operation { get; set; }
        void Save();
        object GetValue(BTSS.Common.Core.Preferences preferences);

    }
}
