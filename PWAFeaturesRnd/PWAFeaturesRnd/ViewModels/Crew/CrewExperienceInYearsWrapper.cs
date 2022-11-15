using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Models.Report.Crew;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    /// Crew Experience Wrapper for converting experienceInDays to Years
    /// </summary>
    public class CrewExperienceInYearsWrapper
    {
        /// <summary>
        /// Gets or sets the experience code.
        /// </summary>
        /// <value>
        /// The experience code.
        /// </value>
        public string ExperienceCode { get; set; }

        /// <summary>
        /// Gets or sets the experience code description.
        /// </summary>
        /// <value>
        /// The experience code description.
        /// </value>
        public string ExperienceCodeDescription { get; set; }

        /// <summary>
        /// Gets the experience in years.
        /// </summary>
        /// <value>
        /// The experience in years.
        /// </value>
        public double ExperienceInYears { get; internal set; }

        /// <summary>
        /// Gets the experience in months.
        /// </summary>
        /// <value>
        /// The experience in months.
        /// </value>
        public int ExperienceInMonths { get; internal set; }

        /// <summary>
        /// Gets the experience remaining days.
        /// </summary>
        /// <value>
        /// The experience remaining days.
        /// </value>
        public int ExperienceRemainingDays { get; internal set; }


        /// <summary>
        /// Gets or sets the Experience type.
        /// </summary>
        /// <value>
        /// The Experience type.
        /// </value>
        public string ExperienceType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewExperienceInYearsWrapper"/> class.
        /// </summary>
        /// <param name="x">The CrewExperienceDetail.</param>
        public CrewExperienceInYearsWrapper(CrewExperienceDetail x)
        {
            ExperienceCode = x.ExperienceCode;
            ExperienceCodeDescription = x.ExperienceCodeDescription;
            ExperienceType = x.ExperienceType;
            var years = (double)x.ExperienceInDays / Constants.DaysToMonth / Constants.MonthToYears;
            ExperienceInYears = Math.Round(years * 100.0) / 100.0;
            ExperienceInMonths = x.ExperienceInDays > 0 ? x.ExperienceInDays / Constants.DaysToMonth : 0;
            ExperienceRemainingDays = x.ExperienceInDays > 0 ? x.ExperienceInDays % Constants.DaysToMonth : 0;
        }

        
    }
}
