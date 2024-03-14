using FluentValidation;

namespace WebApp.Contacts.UpdateContact;

public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
{
    public UpdateContactDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(200);
        RuleFor(x => x.Phone).MaximumLength(50);
    }
}