using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApp.CommonValidation;
using WebApp.DatabaseAccess;

namespace WebApp.Contacts.GetContacts;

public static class GetContactsEndpoint
{
    public static WebApplication MapGetContacts(this WebApplication app)
    {
        app.MapGet("/api/contacts", GetContacts);
        return app;
    }

    public static async Task<IResult> GetContacts(
        DatabaseContext dbContext,
        PagingParametersValidator validator,
        int skip = 0,
        int take = 20,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(new PagingParameters(skip, take), out var errors))
        {
            return Results.BadRequest(errors);
        }

        var dtoList = await dbContext
           .Contacts
           .OrderBy(c => c.LastName)
           .Skip(skip)
           .Take(take)
           .Select(c => new ContactListDto(c.Id, c.LastName, c.FirstName, c.Email, c.Phone))
           .ToListAsync(cancellationToken);

        return Results.Ok(dtoList);
    }
}