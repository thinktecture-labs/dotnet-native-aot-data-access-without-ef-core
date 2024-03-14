using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApp.CommonValidation;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static WebApplication MapUpdateContact(this WebApplication app)
    {
        app.MapPut("/api/contacts", UpdateContact);
        return app;
    }

    public static async Task<IResult> UpdateContact(
        DatabaseContext dbContext,
        UpdateContactDtoValidator validator,
        UpdateContactDto dto,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out var errors))
        {
            return Results.BadRequest(errors);
        }

        var existingContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);
        if (existingContact is null)
        {
            return Results.NotFound();
        }

        existingContact.FirstName = dto.FirstName;
        existingContact.LastName = dto.LastName;
        existingContact.Email = dto.Email;
        existingContact.Phone = dto.Phone;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}