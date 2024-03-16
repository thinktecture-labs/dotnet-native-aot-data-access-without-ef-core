UPDATE "Contacts"
SET "FirstName" = $1, "LastName" = $2, "Email" = $3, "Phone" = $4
WHERE "Id" = $5;