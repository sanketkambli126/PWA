using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Dashboard;

namespace PWAFeaturesRnd.Common
{
	/// <summary>
	/// Common util
	/// </summary>
	public static class CommonUtil
	{
		/// <summary>
		/// Transforms the paged request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pageRequest">The page request.</param>
		/// <returns></returns>
		public static PagedRequest TransformPagedRequest<T>(DataTablePageRequest<T> pageRequest)
		{
			PagedRequest pagedRequest = new PagedRequest();

			pagedRequest.PageNumber = (pageRequest.Start / pageRequest.Length) + 1;
			pagedRequest.PageSize = pageRequest.Length;
			pagedRequest.SearchFilter = (pageRequest.Search == null ? "" : pageRequest.Search.Value);
			if (pageRequest.Order != null)
			{
				pagedRequest.ServerSorting = new SortRequest();
				pagedRequest.ServerSorting.ColumnName = pageRequest.Columns[pageRequest.Order.FirstOrDefault().Column].Name;
				pagedRequest.ServerSorting.SortDirection = pageRequest.Order.FirstOrDefault().Dir == "asc" ? SortDirection.Ascending : SortDirection.Descending;
			}
			return pagedRequest;
		}

		/// <summary>
		/// Calculates the vessel age.
		/// </summary>
		/// <param name="vesselBuiltDate">The vessel built date.</param>
		/// <returns></returns>
		public static int CalculateVesselAge(DateTime? vesselBuiltDate)
		{
			return vesselBuiltDate != null ? Convert.ToInt32(Math.Floor(Convert.ToDouble(((DateTime.Now - Convert.ToDateTime(vesselBuiltDate)).Days / 365.2425)))) : 0;
		}

		/// <summary>
		/// ConvertStreamToByte is static method for convert stream to byte.
		/// </summary>
		/// <param name="stream">The stream object.</param>
		/// <returns>
		/// Array byte
		/// </returns>
		public static byte[] ConvertStreamToByte(System.IO.Stream stream)
		{
			long originalPosition = 0;

			if (stream.CanSeek)
			{
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try
			{
				byte[] readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
				{
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length)
					{
						int nextByte = stream.ReadByte();
						if (nextByte != -1)
						{
							byte[] temp = new byte[readBuffer.Length * 2];
							Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
							Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				byte[] buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead)
				{
					buffer = new byte[totalBytesRead];
					Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			}
			finally
			{
				if (stream.CanSeek)
				{
					stream.Position = originalPosition;
				}
			}
		}

		/// <summary>
		/// Sets the session object.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static void SetSessionObject(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		/// <summary>
		/// Gets the session object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session">The session.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static T GetSessionObject<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}

        /// <summary>
        /// Determines whether [is key exist in session] [the specified key].
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key exist in session] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKeyExistInSession(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value != null;
		}

		/// <summary>
		/// Gets the executable web address.
		/// </summary>
		/// <param name="webAddress">The web address.</param>
		/// <returns></returns>
		public static string GetExecutableWebAddress(string webAddress)
		{
			if (!string.IsNullOrWhiteSpace(webAddress) && !webAddress.StartsWith("http") && !webAddress.StartsWith("\\"))
			{
				return "http://" + webAddress;
			}
			return webAddress;
		}

		/// <summary>
		/// Converts to kmb
		/// string format numbers Thousands 123K, Millions 123M, Billions 123B
		/// </summary>
		/// <param name="num">The number.</param>
		/// <returns>
		/// the string format numbers
		/// </returns>
		public static string ToKMB(this decimal num)
		{
			if (num > 999999999 || num < -999999999)
			{
				return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
			}
			else
			if (num > 999999 || num < -999999)
			{
				return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
			}
			else
			if (num > 999 || num < -999)
			{
				return num.ToString("0,.#K", CultureInfo.InvariantCulture);
			}
			else
			{
				return num.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Decrypts the string from bytes.
		/// </summary>
		/// <param name="cipherText">The cipher text.</param>
		/// <param name="key">The key.</param>
		/// <param name="iv">The iv.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">cipherText
		/// or
		/// key
		/// or
		/// key</exception>
		private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
		{
			// Check arguments.  
			if (cipherText == null || cipherText.Length <= 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (key == null || key.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}
			if (iv == null || iv.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}

			// Declare the string used to hold  
			// the decrypted text.  
			string plaintext = null;

			// Create an RijndaelManaged object  
			// with the specified key and IV.  
			using (var rijAlg = new RijndaelManaged())
			{
				//Settings  
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.PKCS7;
				rijAlg.FeedbackSize = 128;

				rijAlg.Key = key;
				rijAlg.IV = iv;

				// Create a decrytor to perform the stream transform.  
				var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				try
				{
					// Create the streams used for decryption.  
					using (var msDecrypt = new MemoryStream(cipherText))
					{
						using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
						{

							using (var srDecrypt = new StreamReader(csDecrypt))
							{
								// Read the decrypted bytes from the decrypting stream  
								// and place them in a string.  
								plaintext = srDecrypt.ReadToEnd();

							}
						}
					}
				}
				catch
				{
					plaintext = "keyError";
				}
			}

			return plaintext;
		}

		/// <summary>
		/// Decrypts the string aes.
		/// </summary>
		/// <param name="cipherText">The cipher text.</param>
		/// <returns></returns>
		public static string DecryptStringAES(string cipherText)
		{
			var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
			var iv = Encoding.UTF8.GetBytes("8080808080808080");

			var encrypted = Convert.FromBase64String(cipherText);
			var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
			return decriptedFromJavascript;
		}

		/// <summary>
		/// Gets the parameter from dictionary.
		/// To Be Used for complex objects
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input">The input.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		/// <exception cref="BusinessException"></exception>
		public static T GetParameterFromDictionary<T>(Dictionary<string, object> input, string key)
		{
			object obj;
			input.TryGetValue(key, out obj);
			if (obj != null)
			{
				T result = JsonConvert.DeserializeObject<T>(obj.ToString());
				return result;
			}
			return default(T);

		}

		/// <summary>
		/// Gets the encrypted URL.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_provider">The provider.</param>
		/// <param name="encryptionText">The encryption text.</param>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		public static string GetEncryptedURL<T>(IDataProtectionProvider _provider, string encryptionText, T request)
		{
			if (request != null)
			{
				string response = _provider.CreateProtector(encryptionText).Protect(JsonConvert.SerializeObject(request));
				return response;
			}
			return String.Empty;
		}

		/// <summary>
		/// Gets the decrypted request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_provider">The provider.</param>
		/// <param name="encryptionText">The encryption text.</param>
		/// <param name="encryptedText">The encrypted text.</param>
		/// <returns></returns>
		public static T GetDecryptedRequest<T>(IDataProtectionProvider _provider, string encryptionText, string encryptedText)
		{
			if (!string.IsNullOrWhiteSpace(encryptedText) && !string.IsNullOrWhiteSpace(encryptionText))
			{
				string response = _provider.CreateProtector(encryptionText).Unprotect(encryptedText);
				T objectRequest = JsonConvert.DeserializeObject<T>(response);
				return objectRequest;
			}
			return default(T);
		}

		/// <summary>
		/// Gets the encrypted vessel.
		/// </summary>
		/// <param name="_provider">The provider.</param>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="vesselName">Name of the vessel.</param>
		/// <param name="coyId">The coy identifier.</param>
		/// <returns></returns>
		public static string GetEncryptedVessel(IDataProtectionProvider _provider, string vesselId, string vesselName, string coyId)
		{
			string encryptedVessel = _provider.CreateProtector(Constants.VesselEncryptionText).Protect(vesselId + Constants.Separator + vesselName + " - " + coyId + Constants.Separator + coyId);
			return encryptedVessel;
		}

		/// <summary>
		/// Gets the decrypted vessel.
		/// </summary>
		/// <param name="_provider">The provider.</param>
		/// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
		/// <returns></returns>
		public static string GetDecryptedVessel(IDataProtectionProvider _provider, string encryptedVesselId)
		{
			if (!string.IsNullOrWhiteSpace(encryptedVesselId))
			{
				string decryptedVesselDetails = _provider.CreateProtector(Constants.VesselEncryptionText).Unprotect(encryptedVesselId);
				return decryptedVesselDetails;
			}
			return String.Empty;
		}

		/// <summary>
		/// Gets the decrypted vessel identifier.
		/// </summary>
		/// <param name="_provider">The provider.</param>
		/// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
		/// <returns></returns>
		public static string GetDecryptedVesselId(IDataProtectionProvider _provider, string encryptedVesselDetail)
		{
			if (!String.IsNullOrWhiteSpace(encryptedVesselDetail))
			{
				string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
				return decryptedString.Split(Constants.Separator)[0];
			}
			return String.Empty;
		}

		/// <summary>
		/// Get Encrypted Fleet Request
		/// </summary>
		/// <param name="_provider"></param>
		/// <param name="fleetId"></param>
		/// <param name="menuType"></param>
		/// <param name="vesselId"></param>
		/// <param name="title"></param>
		/// <param name="isFleetSelection"></param>
		/// <returns></returns>
		public static string GetEncryptedFleetRequest(IDataProtectionProvider _provider, DashboardParameter parameter)
		{
			string encryptedFleetRequest = GetEncryptedURL(_provider, Constants.FleetRequestEncryptionText, parameter);
			return encryptedFleetRequest;
		}

		/// <summary>
		/// Get Decrypted Fleet Request
		/// </summary>
		/// <param name="_provider"></param>
		/// <param name="encryptedFleetRequest"></param>
		/// <returns></returns>
		public static DashboardParameter GetDecryptedFleetRequest(IDataProtectionProvider _provider, string encryptedFleetRequest)
		{
			if (!string.IsNullOrWhiteSpace(encryptedFleetRequest))
			{
				DashboardParameter dashboardParameter = GetDecryptedRequest<DashboardParameter>(_provider, Constants.FleetRequestEncryptionText, encryptedFleetRequest);
				return dashboardParameter;
			}
			return null;
		}

		/// <summary>
		/// Gets the fleet tracker URL.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="userId">The user identifier.</param>
		/// <param name="parameter">The parameter.</param>
		/// <returns></returns>
		public static string GetFleetTrackerURL(IDataProtectionProvider provider, string userId, DashboardParameter parameter)
		{
			string fleetTrackerURL = AppSettings.FleetTrackerURL;
			if (fleetTrackerURL.EndsWith("/"))
			{
				fleetTrackerURL = fleetTrackerURL.Substring(0, fleetTrackerURL.Length - 1);
			}
			if (parameter != null)
			{
				if (!string.IsNullOrWhiteSpace(parameter.VesselId))
				{
					string decryptedString = CommonUtil.GetDecryptedVessel(provider, parameter.VesselId);
					fleetTrackerURL += "/Home/Index?searchType=vesselId&imoList=" + decryptedString.Split(Constants.Separator)[0];
				}
				else
				{
					fleetTrackerURL += "/Home/FleetTracker?client=PWA&menuType=" + parameter.MenuType + "&userId=" + userId;
					fleetTrackerURL += !string.IsNullOrWhiteSpace(parameter.FleetId) ? "&fleetId=" + parameter.FleetId : "";
				}
			}
			return fleetTrackerURL;
		}

		/// <summary>
		/// Gets the short name of the user.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns></returns>
		public static string GetUserShortName(string userName)
		{
			string result = "";
			if (!string.IsNullOrWhiteSpace(userName))
			{
				var splitName = userName.Trim().Split(" ");
				if (splitName != null && splitName.Count() > 1)
				{
					result += splitName[0].Substring(0, 1);
					result += splitName[1].Substring(0, 1);
				}
				else
				{
					result = splitName[0].Substring(0, 2).ToUpper();
				}
			}
			return result;
		}

		/// <summary>
		/// Converts the UTC date to local.
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <returns></returns>
		public static DateTime ConvertUTCDateToLocal(DateTime? dt, int timeZoneOffSet)
		{
			DateTime localDate = dt.Value - new TimeSpan(timeZoneOffSet / 60, timeZoneOffSet % 60, 0);
			return localDate;
		}

		/// <summary>
		/// Gets the local converted specific time format.
		/// </summary>
		/// <param name="modifiedDate">The modified date.</param>
		/// <param name="timeZoneOffSet">The time zone off set.</param>
		/// <param name="dateFormat">The date format.</param>
		/// <returns></returns>
		public static string GetLocalConvertedSpecificTimeFormat(DateTime? modifiedDate, int timeZoneOffSet, string dateFormat)
		{
			string recievedDate = String.Empty;
			if (modifiedDate != null && modifiedDate.GetValueOrDefault() != DateTime.MinValue)
			{
				DateTime localdate = ConvertUTCDateToLocal(modifiedDate, timeZoneOffSet);
				string convertedDateString = localdate.ToString(dateFormat);
				return convertedDateString;
			}
			return recievedDate;
		}

		/// <summary>
		/// Convertstrings to key value pair.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static KeyValuePair<string, string>? ConvertStringToKeyValuePair(string input)
		{
			if (!String.IsNullOrWhiteSpace(input))
			{
				string[] keyValueArray = input.Split(':');

				string key = keyValueArray[0];
				string value = keyValueArray[1];

				return new KeyValuePair<string, string>(key, value);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the display name of the vessel.
		/// Format: [VesselName] - [CoyId] 
		/// </summary>
		/// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
		/// <returns></returns>
		public static string GetVesselDisplayName(IDataProtectionProvider _provider, string encryptedVesselDetail)
		{
			if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
			{
				string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
				return decryptedString.Split(Constants.Separator)[1];
			}
			return null;
		}

		/// <summary>
		/// Gets the vessel name from encrypted details.
		/// </summary>
		/// <param name="_provider">The provider.</param>
		/// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
		/// <returns></returns>
		public static string GetVesselNameFromEncryptedDetails(IDataProtectionProvider _provider, string encryptedVesselDetail)
		{
			string vesselDisplayName = GetVesselDisplayName(_provider, encryptedVesselDetail);
			return GetVesselNameFromDisplayName(vesselDisplayName);
		}

		/// <summary>
		/// Gets the display name of the vessel name from.
		/// Input Format: [VesselName] - [CoyId] 
		/// Output Value: [VesselName]
		/// </summary>
		/// <param name="vesselDisplayName">Display name of the vessel.</param>
		/// <returns></returns>
		public static string GetVesselNameFromDisplayName(string vesselDisplayName)
		{
			string vesselName = null;

			if (!string.IsNullOrWhiteSpace(vesselDisplayName) && vesselDisplayName.Contains("-"))
			{
				int separatorIndex = vesselDisplayName.IndexOf("-");
				var vesselNameWithSpaces = vesselDisplayName.Substring(0, separatorIndex);
				vesselName = vesselNameWithSpaces.Trim();
			}
			return vesselName;
		}

		/// <summary>
		/// Gets the encrypted vessel details.
		/// </summary>
		/// <param name="vesselId">The vessel identifier.</param>
		/// <param name="vesselName">Name of the vessel.</param>
		/// <param name="coyId">The coy identifier.</param>
		/// <returns></returns>
		public static string GetEncryptedVesselDetails(IDataProtectionProvider _provider, string vesselId, string vesselName, string coyId)
		{
			string encryptedVessel = _provider.CreateProtector("Vessel").Protect(vesselId + Constants.Separator + vesselName + " - " + coyId + Constants.Separator + coyId);
			return encryptedVessel;
		}

        /// <summary>
        /// Sets the decimal default value.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string SetDecimalDefaultValue(this decimal? input)
        {
			return input == null ? "-" : (input??0).ToString("0.00");
        }

		/// <summary>
		/// Get session storage filter
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="provider"></param>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public static T GetSessionStorageFilter<T>(IDataProtectionProvider provider, string sessionDetails)
		{
			if (!string.IsNullOrWhiteSpace(sessionDetails))
			{
				Dictionary<string, object> sessionDetail = CommonUtil.GetDecryptedRequest<Dictionary<string, object>>(provider, "SessionStorage", sessionDetails);

				if (sessionDetail != null && sessionDetail.ContainsKey("filter") && sessionDetail["filter"] != null)
				{
					return CommonUtil.GetParameterFromDictionary<T>(sessionDetail, "filter");
				}
			}
			return default(T);
		}

		/// <summary>
		/// Set session storage filter
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="sessionDetails"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static string SetSessionStorageFilter(IDataProtectionProvider provider, string sessionDetails, object filter)
		{
			Dictionary<string, object> sessionDetail = CommonUtil.GetDecryptedRequest<Dictionary<string, object>>(provider, "SessionStorage", sessionDetails);

			if (sessionDetail != null && sessionDetail.ContainsKey("filter") && sessionDetail["filter"] != null)
			{
				sessionDetail["filter"] = filter;
				return CommonUtil.GetEncryptedURL<Dictionary<string, object>>(provider, "SessionStorage", sessionDetail);
			}
			return null;
		}

		/// <summary>
		/// Get session storage source URL
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="sessionDetails"></param>
		/// <returns></returns>
		public static string GetSessionStorageSourceURL(IDataProtectionProvider provider, string sessionDetails)
		{
			Dictionary<string, object> sessionDetail = CommonUtil.GetDecryptedRequest<Dictionary<string, object>>(provider, "SessionStorage", sessionDetails);

			if (sessionDetail != null && sessionDetail.ContainsKey("src") && sessionDetail["src"] != null)
			{
				return sessionDetail["src"].ToString();
			}
			return null;
		}

		/// <summary>
		/// Set session storage source URL
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="sessionDetails"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string SetSessionStorageSourceURL(IDataProtectionProvider provider, string sessionDetails, string url)
		{
			Dictionary<string, object> sessionDetail = CommonUtil.GetDecryptedRequest<Dictionary<string, object>>(provider, "SessionStorage", sessionDetails);

			if (sessionDetail != null && sessionDetail.ContainsKey("src") && sessionDetail["src"] != null)
			{
				sessionDetail["src"] = url;
				return CommonUtil.GetEncryptedURL<Dictionary<string, object>>(provider, "SessionStorage", sessionDetail);
			}
			return null;
		}

		/// <summary>
		/// Gets the object details.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		public static string GetObjDetails<T>(T obj)  where T : class
        {
			if(obj == null)
            {
				return null;
            }

			StringBuilder objDetails = new StringBuilder();
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				objDetails.Append(string.Format("{0}={1}, ", name, value));
			}

			return objDetails.ToString();
		}

		/// <summary>
		/// Replace Demo Data
		/// </summary>
		/// <param name="json"></param>
		/// <param name="demoData"></param>
		/// <returns></returns>
		public static string ReplaceDemoData(string json, Dictionary<string, string> demoData)
		{
			//find and replace demo data in loop
			foreach (KeyValuePair<string, string> keyVal in demoData)
			{
				json = Regex.Replace(json,keyVal.Key, keyVal.Value, RegexOptions.IgnoreCase);
			}
			//convert and return updated json into T type
			return json;
		}
	}
}
