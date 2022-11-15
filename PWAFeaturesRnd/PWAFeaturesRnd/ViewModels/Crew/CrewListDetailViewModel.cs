using System;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Report.Crew;

namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew List Detail ViewModel
	/// </summary>
	public class CrewListDetailViewModel
	{
		#region Properties

		/// <summary>
		/// Gets or sets the serial number.
		/// </summary>
		/// <value>
		/// The serial number.
		/// </value>
		public int SerialNumber { get; set; }

		/// <summary>
		/// Gets or sets the SVL identifier.
		/// </summary>
		/// <value>
		/// The SVL identifier.
		/// </value>
		public string SvlId { get; set; }

		/// <summary>
		/// Gets or sets the full name of the crew.
		/// </summary>
		/// <value>
		/// The full name of the crew.
		/// </value>
		public string CrewFullName { get; set; }

		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>
		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the short name of the department.
		/// </summary>
		/// <value>
		/// The short name of the department.
		/// </value>
		public string DepartmentShortName { get; set; }

		/// <summary>
		/// Gets or sets the name of the department.
		/// </summary>
		/// <value>
		/// The name of the department.
		/// </value>
		public string DepartmentName { get; set; }

		/// <summary>
		/// Gets or sets the nationality.
		/// </summary>
		/// <value>
		/// The nationality.
		/// </value>
		public string Nationality { get; set; }

		/// <summary>
		/// Gets or sets the sign on.
		/// </summary>
		/// <value>
		/// The sign on.
		/// </value>
		public DateTime? SignOn { get; set; }

		/// <summary>
		/// Gets or sets the due relief.
		/// </summary>
		/// <value>
		/// The due relief.
		/// </value>
		public DateTime? DueRelief { get; set; }

		/// <summary>
		/// Gets or sets the extension.
		/// </summary>
		/// <value>
		/// The extension.
		/// </value>
		public string Extension { get; set; }

		/// <summary>
		/// Gets or sets the left.
		/// </summary>
		/// <value>
		/// The left.
		/// </value>
		public int? Left { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the name of the reliever.
		/// </summary>
		/// <value>
		/// The name of the reliever.
		/// </value>
		public string RelieverName { get; set; }

		/// <summary>
		/// Gets or sets the crew identifier.
		/// </summary>
		/// <value>
		/// The crew identifier.
		/// </value>
		public string CrewId { get; set; }

		/// <summary>
		/// Gets or sets the reliever identifier.
		/// </summary>
		/// <value>
		/// The reliever identifier.
		/// </value>
		public string RelieverId { get; set; }

		/// <summary>
		/// Gets or sets the planning status description.
		/// </summary>
		/// <value>
		/// The planning status description.
		/// </value>
		public string PlanningStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the planning status colour.
		/// </summary>
		/// <value>
		/// The planning status colour.
		/// </value>
		public string PlanningStatusColour { get; set; }

		/// <summary>
		/// Gets or sets the planning status short code.
		/// </summary>
		/// <value>
		/// The planning status short code.
		/// </value>
		public string PlanningStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the length of contract.
		/// </summary>
		/// <value>
		/// The length of contract.
		/// </value>
		public string LengthOfContract { get; set; }

		/// <summary>
		/// Gets or sets the encrypted crew identifier.
		/// </summary>
		/// <value>
		/// The encrypted crew identifier.
		/// </value>
		public string EncryptedCrewId { get; set; }

		/// <summary>
		/// Gets or sets the encrypted reliver identifier.
		/// </summary>
		/// <value>
		/// The encrypted reliver identifier.
		/// </value>
		public string EncryptedReliverId { get; set; }

		/// <summary>
		/// Gets or sets the set identifier.
		/// </summary>
		/// <value>
		/// The set identifier.
		/// </value>
		public string SetId { get; set; }

		/// <summary>
		/// Gets or sets the service active status identifier.
		/// </summary>
		/// <value>
		/// The service active status identifier.
		/// </value>
		public int ServiceActiveStatusId { get; set; }

		/// <summary>
		/// Gets or sets the planning status identifier.
		/// </summary>
		/// <value>
		/// The planning status identifier.
		/// </value>
		public string PlanningStatusId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is crew name visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is crew name visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsCrewNameVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is reliever name visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is reliever name visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsRelieverNameVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is unplanned berth.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is unplanned berth; otherwise, <c>false</c>.
		/// </value>
		public bool IsUnplannedBerth { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is crew signed off.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is crew signed off; otherwise, <c>false</c>.
		/// </value>
		public bool IsCrewSignedOff { get; set; }

		/// <summary>
		/// Gets or sets the berth type short code.
		/// </summary>
		/// <value>
		/// The berth type short code.
		/// </value>
		public string BerthTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the berth type description.
		/// </summary>
		/// <value>
		/// The berth type description.
		/// </value>
		public string BerthTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the berth color code.
		/// </summary>
		/// <value>
		/// The berth color code.
		/// </value>
		public string BerthColorCode { get; set; }

		/// <summary>
		/// Gets or sets the channel count.
		/// </summary>
		/// <value>
		/// The channel count.
		/// </value>
		public int ChannelCount { get; set; }

		/// <summary>
		/// Gets or sets the notes count.
		/// </summary>
		/// <value>
		/// The notes count.
		/// </value>
		public int NotesCount { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="CrewListDetailViewModel" /> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="serialNumber">The serial number.</param>
		public CrewListDetailViewModel(OBCrewListDetail item, int serialNumber)
		{
			SerialNumber = serialNumber;
			RankDescription = item.RankDescription ?? "";
			DepartmentShortName = item.DepartmentShortName ?? "";
			DepartmentName = item.DepartmentName ?? "";
			Nationality = item.Nationality ?? "";
			CrewFullName = SetCrewFullName(item);
			IsCrewNameVisible = !string.IsNullOrWhiteSpace(CrewFullName) ? true : false;
			SignOn = item.CrewSignOn;
			DueRelief = item.CrewDueRelief;
			Left = item.CrewDueRelief.HasValue ? (item.CrewDueRelief.Value.Date - DateTime.Today.Date).Days : default(int?);
			Extension = item.Extension.GetValueOrDefault() + " " + item.ExtensionUnit;
			RelieverName = SetRelieverName(item);
			IsRelieverNameVisible = !string.IsNullOrWhiteSpace(RelieverName) ? true : false;
			Tuple<string, string, string> PlanningStatus = SetStatusShortCode(item.PlanningStatusId);
			PlanningStatusDescription = PlanningStatus.Item1;
			PlanningStatusShortCode = PlanningStatus.Item3;
			PlanningStatusColour = PlanningStatus.Item2;
			LengthOfContract = item.ContractLength.HasValue ? (item.ContractLength.GetValueOrDefault() + " " + item.ContractLengthDescription ?? "") : "";
			ServiceActiveStatusId = item.ServiceActiveStatusId;
			PlanningStatusId = item.PlanningStatusId;
			SvlId = item.SvlId;
			CrewId = item.CrewId;
			RelieverId = item.RelieverId;
			IsCrewSignedOff = !item.IsActive && (!string.IsNullOrWhiteSpace(item.CrwIdTp) || !string.IsNullOrWhiteSpace(item.CrewId));
			BerthTypeShortCode = item.BerthTypeShortCode;
			BerthTypeDescription = item.BerthTypeDescription;
			if (item.BerthToDate != null && item.BerthToDate.Value.Date < DateTime.Now.Date)
			{
				BerthTypeShortCode = EnumsHelper.GetKeyValue(CrewStatus.Expired);
				BerthTypeDescription = EnumsHelper.GetDescription(CrewStatus.Expired);
			}
			GetBerthColorCode();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sets the full name of the crew.
		/// </summary>
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public string SetCrewFullName(OBCrewListDetail crew)
		{
			string CrewFullName = "";

			//Set crew full name.
			if (string.IsNullOrWhiteSpace(crew.CrewLastName))
			{
				CrewFullName = crew.CrewFirstName ?? "";
			}
			else
			{
				CrewFullName = crew.CrewLastName + ", " + (crew.CrewFirstName ?? "");
			}
			return CrewFullName;
		}

		/// <summary>
		/// Sets the name of the reliever.
		/// </summary>
		/// <param name="crew">The crew.</param>
		/// <returns></returns>
		public string SetRelieverName(OBCrewListDetail crew)
		{
			string RelieverFullName = "";
			//Set reliever full name.
			if (string.IsNullOrWhiteSpace(crew.RelieverLastName))
			{
				RelieverFullName = crew.RelieverFirstName ?? "";
			}
			else
			{
				RelieverFullName = crew.RelieverLastName + ", " + (crew.RelieverFirstName ?? "");
			}
			return RelieverFullName;
		}

		/// <summary>
		/// Sets the status short code.
		/// </summary>
		/// <param name="PlanningStatusId">The planning status identifier.</param>
		/// <returns></returns>
		public Tuple<string, string, string> SetStatusShortCode(string PlanningStatusId)
		{
			string PlanningStatusShortCode = string.Empty;
			string PlanningStatusDescription = string.Empty;
			string PlanningStatusColour = "";

			if (PlanningStatusId != null)
			{
				if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Approved))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Approved);
					PlanningStatusColour = "#52A829"; //EnumsHelper.GetDescription(KPI.Better);
					PlanningStatusDescription = CrewPlanningStatus.Approved.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Ready))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Ready);
					PlanningStatusColour = "#606093"; //EnumsHelper.GetDescription(KPI.Best);
					PlanningStatusDescription = CrewPlanningStatus.Ready.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Planned))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Planned);
					PlanningStatusColour = "#ffca2b";//EnumsHelper.GetDescription(KPI.Normal);
					PlanningStatusDescription = CrewPlanningStatus.Planned.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Proposed))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Proposed);
					PlanningStatusColour = "#FF8D1F"; // EnumsHelper.GetDescription(KPI.Good);
					PlanningStatusDescription = CrewPlanningStatus.Proposed.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Rejected))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Rejected);
					PlanningStatusColour = "#FF2905";// EnumsHelper.GetDescription(KPI.Critical);
					PlanningStatusDescription = CrewPlanningStatus.Rejected.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Released))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Released);
					PlanningStatusColour = "#d3406d"; // EnumsHelper.GetDescription(KPI.PreWarning);
					PlanningStatusDescription = CrewPlanningStatus.Released.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Joined))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Joined);
					PlanningStatusColour = "#9D4F9B"; //EnumsHelper.GetDescription(KPI.Excellent);
					PlanningStatusDescription = CrewPlanningStatus.Joined.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.Cancelled))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.Cancelled);
					PlanningStatusColour = "#808080"; // EnumsHelper.GetDescription(KPI.Warning);
					PlanningStatusDescription = CrewPlanningStatus.Cancelled.ToString();
				}
				else if (PlanningStatusId == EnumsHelper.GetKeyValue(CrewPlanningStatus.PlanProposed))
				{
					PlanningStatusShortCode = EnumsHelper.GetDescription(CrewPlanningStatus.PlanProposed);
					PlanningStatusColour = "#FF8D1F"; // EnumsHelper.GetDescription(KPI.Good);
					PlanningStatusDescription = CrewPlanningStatus.PlanProposed.ToString();
				}
				else if (PlanningStatusId.Equals(EnumsHelper.GetKeyValue(CrewStatus.Overlap)))
				{
					PlanningStatusShortCode = EnumsHelper.GetKeyValue(CrewStatus.Overlap);
					PlanningStatusColour = "#FF2905";// EnumsHelper.GetDescription(KPI.Critical);
					PlanningStatusDescription = EnumsHelper.GetDescription(CrewStatus.Overlap);
				}
			}
			Tuple<string, string, string> planningStatus = new Tuple<string, string, string>(PlanningStatusShortCode, PlanningStatusColour, PlanningStatusDescription);
			return planningStatus;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the berth color code.
		/// </summary>
		// Referred to CrewStatusToBrushConverter of Crew Module >> StartDataTemplate
		private void GetBerthColorCode()
		{
			if (BerthTypeShortCode != null)
			{
				if (BerthTypeShortCode == EnumsHelper.GetKeyValue(BerthType.BudgetedBerth))
				{
					BerthColorCode = Constants.ShipsureBrushGreen;
				}
				else if (BerthTypeShortCode == EnumsHelper.GetKeyValue(BerthType.ExtraBerthTech))
				{
					BerthColorCode = Constants.ShipsureBrushDarkBlue;
				}
				else if (BerthTypeShortCode == EnumsHelper.GetKeyValue(BerthType.ExtraBerthOwner))
				{
					BerthColorCode = Constants.ShipsureBrushGraphLineCyan;
				}
				else if (BerthTypeShortCode == EnumsHelper.GetKeyValue(BerthType.TrainingBerth))
				{
					BerthColorCode = Constants.ShipsureBrushYellow;
				}
				else if (BerthTypeShortCode == EnumsHelper.GetKeyValue(BerthType.ExpiredBerth))
				{
					BerthColorCode = Constants.ShipsureBrushRed;
				}
			}

		}

		#endregion

	}
}
