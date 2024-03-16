SELECT "Id", "FirstName", "LastName", "Email", "Phone"
FROM "Contacts"
ORDER BY "LastName"
OFFSET $1
LIMIT $2;