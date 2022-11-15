using FluentValidation;
using PWAFeaturesRnd.ViewModels.Login;

namespace PWAFeaturesRnd.Validators.Login
{
	/// <summary>
	/// LoginValidator class
	/// </summary>
	/// <seealso cref="FluentValidation.AbstractValidator{PWAFeaturesRnd.ViewModels.Login.LoginViewModel}" />
	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoginValidator"/> class.
		/// </summary>
		public LoginValidator()
		{
			RuleFor(x => x.Username).NotEmpty().WithMessage("Please enter username......");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter password......");
		}
	}
}
