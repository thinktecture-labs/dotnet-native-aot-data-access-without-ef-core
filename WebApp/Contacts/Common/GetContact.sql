SELECT c."Id" AS "ContactId",
       c."FirstName",
       c."LastName",
       c."Email",
       c."Phone",
       a."Id" AS "AddressId",
       a."Street",
       a."ZipCode",
       a."City"
FROM "Contacts" c
     LEFT JOIN "Addresses" a ON c."Id" = a."ContactID"
WHERE c."Id" = @Id;