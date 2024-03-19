INSERT INTO "Contacts" ("Id", "FirstName", "LastName", "Email", "Phone")
VALUES ($1, $2, $3, $4, $5)
ON CONFLICT ("Id") DO UPDATE
SET "FirstName" = excluded."FirstName",
    "LastName" = excluded."LastName",
    "Email" = excluded."Email",
    "Phone" = excluded."Phone";