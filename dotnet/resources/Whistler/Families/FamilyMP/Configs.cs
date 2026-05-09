using System;
using System.Collections.Generic;
using System.Text;
using Whistler.Families.FamilyMP.Models;

namespace Whistler.Families.FamilyMP
{
    class Configs
    {
        public static readonly Dictionary<FamilyMPType, ConfigMP> ConfigMPList = new Dictionary<FamilyMPType, ConfigMP>
        {
            { FamilyMPType.IslandCapture, new ConfigMP("Fight for the island", "$ 2000 To every surviving member of the profit family on the island and on weapons", "Capture of the island")},
            { FamilyMPType.BusinessWar, new ConfigMP("Battle for business", "Business for the family", "Recording of the businessss")},
        };
    }
}
