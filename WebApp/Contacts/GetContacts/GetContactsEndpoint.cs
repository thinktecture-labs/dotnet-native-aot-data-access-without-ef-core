﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApp.CommonValidation;

namespace WebApp.Contacts.GetContacts;

public static class GetContactsEndpoint
{
    public static WebApplication MapGetContacts(this WebApplication app)
    {
        app.MapGet("/api/contacts", GetContacts);
        return app;
    }

    public static async Task<IResult> GetContacts(
        IGetContactsSession session,
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

        var dtoList = await session.GetContactsAsync(skip, take, cancellationToken);
        return Results.Ok(dtoList);
    }
}