SELECT "Id", "FirstName", "LastName", "Email", "Phone"
FROM "Contacts"
ORDER BY "LastName"
OFFSET @Skip
LIMIT @Take;