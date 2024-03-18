using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApp.CommonValidation;

namespace WebApp.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static WebApplication MapUpdateContact(this WebApplication app)
    {
        app.MapPut("/api/contacts", UpdateContact);
        return app;
    }

    public static async Task<IResult> UpdateContact(
        IUpdateContactSession session,
        UpdateContactDtoValidator validator,
        UpdateContactDto dto,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out var errors))
        {
            return Results.BadRequest(errors);
        }

        var existingContact = await session.GetContactAsync(dto.Id, cancellationToken);
        if (existingContact is null)
        {
            return Results.NotFound();
        }

        existingContact.FirstName = dto.FirstName;
        existingContact.LastName = dto.LastName;
        existingContact.Phone = dto.Phone;
        existingContact.Email = dto.Email;
        await session.UpdateContactAsync(existingContact, cancellationToken);
        await session.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}