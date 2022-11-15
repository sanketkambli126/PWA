using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class JSACrewDetailViewModel
    {
        public string CrewId { get; set; }

        public string CRW_ID_TP { get; set; }

        public string CrewFullName { get; set; }

        public string Rank { get; set; }

        public string Department { get; set; }

        public bool? HasMeetingAttended { get; set; }

        public bool? HasWorkInstructionUnderstood { get; set; }

        public bool? HasSatisfiedWithPrecaution { get; set; }

        public bool IsNotesVisible { get; set; }

        public string Notes { get; set; }

        public string OtherCompany { get; set; }

        public string OtherCrewName { get; set; }

        public string OtherIdentityNo { get; set; }

        public string OtherPosition { get; set; }
    }
}
