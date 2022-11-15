namespace PWAFeaturesRnd.ViewModels.Login
{
	public class LoginViewModel
	{
		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		public string ClientId { get; set; }

		/// <summary>
		/// Gets or sets the authentication error.
		/// </summary>
		/// <value>
		/// The authentication error.
		/// </value>
		public string AuthenticationError { get; set; }
	}
}
