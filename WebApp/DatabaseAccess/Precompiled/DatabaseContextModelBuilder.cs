﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace WebApp.DatabaseAccess.Precompiled
{
    public partial class DatabaseContextModel
    {
        partial void Initialize()
        {
            var address = AddressEntityType.Create(this);
            var contact = ContactEntityType.Create(this);

            AddressEntityType.CreateForeignKey1(address, contact);

            AddressEntityType.CreateAnnotations(address);
            ContactEntityType.CreateAnnotations(contact);

            AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            AddAnnotation("ProductVersion", "8.0.3");
            AddAnnotation("Relational:MaxIdentifierLength", 63);
            AddRuntimeAnnotation("Relational:RelationalModel", CreateRelationalModel());
        }

        private IRelationalModel CreateRelationalModel()
        {
            var relationalModel = new RelationalModel(this);

            var address = FindEntityType("WebApp.DatabaseAccess.Model.Address")!;

            var defaultTableMappings = new List<TableMappingBase<ColumnMappingBase>>();
            address.SetRuntimeAnnotation("Relational:DefaultMappings", defaultTableMappings);
            var webAppDatabaseAccessModelAddressTableBase = new TableBase("WebApp.DatabaseAccess.Model.Address", null, relationalModel);
            var cityColumnBase = new ColumnBase<ColumnMappingBase>("City", "character varying(200)", webAppDatabaseAccessModelAddressTableBase);
            webAppDatabaseAccessModelAddressTableBase.Columns.Add("City", cityColumnBase);
            var contactIdColumnBase = new ColumnBase<ColumnMappingBase>("ContactId", "uuid", webAppDatabaseAccessModelAddressTableBase);
            webAppDatabaseAccessModelAddressTableBase.Columns.Add("ContactId", contactIdColumnBase);
            var idColumnBase = new ColumnBase<ColumnMappingBase>("Id", "uuid", webAppDatabaseAccessModelAddressTableBase);
            webAppDatabaseAccessModelAddressTableBase.Columns.Add("Id", idColumnBase);
            var streetColumnBase = new ColumnBase<ColumnMappingBase>("Street", "character varying(200)", webAppDatabaseAccessModelAddressTableBase);
            webAppDatabaseAccessModelAddressTableBase.Columns.Add("Street", streetColumnBase);
            var zipCodeColumnBase = new ColumnBase<ColumnMappingBase>("ZipCode", "character varying(5)", webAppDatabaseAccessModelAddressTableBase);
            webAppDatabaseAccessModelAddressTableBase.Columns.Add("ZipCode", zipCodeColumnBase);
            relationalModel.DefaultTables.Add("WebApp.DatabaseAccess.Model.Address", webAppDatabaseAccessModelAddressTableBase);
            var webAppDatabaseAccessModelAddressMappingBase = new TableMappingBase<ColumnMappingBase>(address, webAppDatabaseAccessModelAddressTableBase, true);
            webAppDatabaseAccessModelAddressTableBase.AddTypeMapping(webAppDatabaseAccessModelAddressMappingBase, false);
            defaultTableMappings.Add(webAppDatabaseAccessModelAddressMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)idColumnBase, address.FindProperty("Id")!, webAppDatabaseAccessModelAddressMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)cityColumnBase, address.FindProperty("City")!, webAppDatabaseAccessModelAddressMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)contactIdColumnBase, address.FindProperty("ContactId")!, webAppDatabaseAccessModelAddressMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)streetColumnBase, address.FindProperty("Street")!, webAppDatabaseAccessModelAddressMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)zipCodeColumnBase, address.FindProperty("ZipCode")!, webAppDatabaseAccessModelAddressMappingBase);

            var tableMappings = new List<TableMapping>();
            address.SetRuntimeAnnotation("Relational:TableMappings", tableMappings);
            var addressesTable = new Table("Addresses", null, relationalModel);
            var idColumn = new Column("Id", "uuid", addressesTable);
            addressesTable.Columns.Add("Id", idColumn);
            var cityColumn = new Column("City", "character varying(200)", addressesTable);
            addressesTable.Columns.Add("City", cityColumn);
            var contactIdColumn = new Column("ContactId", "uuid", addressesTable);
            addressesTable.Columns.Add("ContactId", contactIdColumn);
            var streetColumn = new Column("Street", "character varying(200)", addressesTable);
            addressesTable.Columns.Add("Street", streetColumn);
            var zipCodeColumn = new Column("ZipCode", "character varying(5)", addressesTable);
            addressesTable.Columns.Add("ZipCode", zipCodeColumn);
            var pK_Addresses = new UniqueConstraint("PK_Addresses", addressesTable, new[] { idColumn });
            addressesTable.PrimaryKey = pK_Addresses;
            var pK_AddressesUc = RelationalModel.GetKey(this,
                "WebApp.DatabaseAccess.Model.Address",
                new[] { "Id" });
            pK_Addresses.MappedKeys.Add(pK_AddressesUc);
            RelationalModel.GetOrCreateUniqueConstraints(pK_AddressesUc).Add(pK_Addresses);
            addressesTable.UniqueConstraints.Add("PK_Addresses", pK_Addresses);
            var iX_Addresses_ContactId = new TableIndex(
            "IX_Addresses_ContactId", addressesTable, new[] { contactIdColumn }, false);
            var iX_Addresses_ContactIdIx = RelationalModel.GetIndex(this,
                "WebApp.DatabaseAccess.Model.Address",
                new[] { "ContactId" });
            iX_Addresses_ContactId.MappedIndexes.Add(iX_Addresses_ContactIdIx);
            RelationalModel.GetOrCreateTableIndexes(iX_Addresses_ContactIdIx).Add(iX_Addresses_ContactId);
            addressesTable.Indexes.Add("IX_Addresses_ContactId", iX_Addresses_ContactId);
            relationalModel.Tables.Add(("Addresses", null), addressesTable);
            var addressesTableMapping = new TableMapping(address, addressesTable, true);
            addressesTable.AddTypeMapping(addressesTableMapping, false);
            tableMappings.Add(addressesTableMapping);
            RelationalModel.CreateColumnMapping(idColumn, address.FindProperty("Id")!, addressesTableMapping);
            RelationalModel.CreateColumnMapping(cityColumn, address.FindProperty("City")!, addressesTableMapping);
            RelationalModel.CreateColumnMapping(contactIdColumn, address.FindProperty("ContactId")!, addressesTableMapping);
            RelationalModel.CreateColumnMapping(streetColumn, address.FindProperty("Street")!, addressesTableMapping);
            RelationalModel.CreateColumnMapping(zipCodeColumn, address.FindProperty("ZipCode")!, addressesTableMapping);

            var contact = FindEntityType("WebApp.DatabaseAccess.Model.Contact")!;

            var defaultTableMappings0 = new List<TableMappingBase<ColumnMappingBase>>();
            contact.SetRuntimeAnnotation("Relational:DefaultMappings", defaultTableMappings0);
            var webAppDatabaseAccessModelContactTableBase = new TableBase("WebApp.DatabaseAccess.Model.Contact", null, relationalModel);
            var emailColumnBase = new ColumnBase<ColumnMappingBase>("Email", "character varying(200)", webAppDatabaseAccessModelContactTableBase)
            {
                IsNullable = true
            };
            webAppDatabaseAccessModelContactTableBase.Columns.Add("Email", emailColumnBase);
            var firstNameColumnBase = new ColumnBase<ColumnMappingBase>("FirstName", "character varying(200)", webAppDatabaseAccessModelContactTableBase);
            webAppDatabaseAccessModelContactTableBase.Columns.Add("FirstName", firstNameColumnBase);
            var idColumnBase0 = new ColumnBase<ColumnMappingBase>("Id", "uuid", webAppDatabaseAccessModelContactTableBase);
            webAppDatabaseAccessModelContactTableBase.Columns.Add("Id", idColumnBase0);
            var lastNameColumnBase = new ColumnBase<ColumnMappingBase>("LastName", "character varying(200)", webAppDatabaseAccessModelContactTableBase);
            webAppDatabaseAccessModelContactTableBase.Columns.Add("LastName", lastNameColumnBase);
            var phoneColumnBase = new ColumnBase<ColumnMappingBase>("Phone", "character varying(50)", webAppDatabaseAccessModelContactTableBase)
            {
                IsNullable = true
            };
            webAppDatabaseAccessModelContactTableBase.Columns.Add("Phone", phoneColumnBase);
            relationalModel.DefaultTables.Add("WebApp.DatabaseAccess.Model.Contact", webAppDatabaseAccessModelContactTableBase);
            var webAppDatabaseAccessModelContactMappingBase = new TableMappingBase<ColumnMappingBase>(contact, webAppDatabaseAccessModelContactTableBase, true);
            webAppDatabaseAccessModelContactTableBase.AddTypeMapping(webAppDatabaseAccessModelContactMappingBase, false);
            defaultTableMappings0.Add(webAppDatabaseAccessModelContactMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)idColumnBase0, contact.FindProperty("Id")!, webAppDatabaseAccessModelContactMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)emailColumnBase, contact.FindProperty("Email")!, webAppDatabaseAccessModelContactMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)firstNameColumnBase, contact.FindProperty("FirstName")!, webAppDatabaseAccessModelContactMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)lastNameColumnBase, contact.FindProperty("LastName")!, webAppDatabaseAccessModelContactMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)phoneColumnBase, contact.FindProperty("Phone")!, webAppDatabaseAccessModelContactMappingBase);

            var tableMappings0 = new List<TableMapping>();
            contact.SetRuntimeAnnotation("Relational:TableMappings", tableMappings0);
            var contactsTable = new Table("Contacts", null, relationalModel);
            var idColumn0 = new Column("Id", "uuid", contactsTable);
            contactsTable.Columns.Add("Id", idColumn0);
            var emailColumn = new Column("Email", "character varying(200)", contactsTable)
            {
                IsNullable = true
            };
            contactsTable.Columns.Add("Email", emailColumn);
            var firstNameColumn = new Column("FirstName", "character varying(200)", contactsTable);
            contactsTable.Columns.Add("FirstName", firstNameColumn);
            var lastNameColumn = new Column("LastName", "character varying(200)", contactsTable);
            contactsTable.Columns.Add("LastName", lastNameColumn);
            var phoneColumn = new Column("Phone", "character varying(50)", contactsTable)
            {
                IsNullable = true
            };
            contactsTable.Columns.Add("Phone", phoneColumn);
            var pK_Contacts = new UniqueConstraint("PK_Contacts", contactsTable, new[] { idColumn0 });
            contactsTable.PrimaryKey = pK_Contacts;
            var pK_ContactsUc = RelationalModel.GetKey(this,
                "WebApp.DatabaseAccess.Model.Contact",
                new[] { "Id" });
            pK_Contacts.MappedKeys.Add(pK_ContactsUc);
            RelationalModel.GetOrCreateUniqueConstraints(pK_ContactsUc).Add(pK_Contacts);
            contactsTable.UniqueConstraints.Add("PK_Contacts", pK_Contacts);
            relationalModel.Tables.Add(("Contacts", null), contactsTable);
            var contactsTableMapping = new TableMapping(contact, contactsTable, true);
            contactsTable.AddTypeMapping(contactsTableMapping, false);
            tableMappings0.Add(contactsTableMapping);
            RelationalModel.CreateColumnMapping(idColumn0, contact.FindProperty("Id")!, contactsTableMapping);
            RelationalModel.CreateColumnMapping(emailColumn, contact.FindProperty("Email")!, contactsTableMapping);
            RelationalModel.CreateColumnMapping(firstNameColumn, contact.FindProperty("FirstName")!, contactsTableMapping);
            RelationalModel.CreateColumnMapping(lastNameColumn, contact.FindProperty("LastName")!, contactsTableMapping);
            RelationalModel.CreateColumnMapping(phoneColumn, contact.FindProperty("Phone")!, contactsTableMapping);
            var fK_Addresses_Contacts_ContactId = new ForeignKeyConstraint(
                "FK_Addresses_Contacts_ContactId", addressesTable, contactsTable,
                new[] { contactIdColumn },
                contactsTable.FindUniqueConstraint("PK_Contacts")!, ReferentialAction.Cascade);
            var fK_Addresses_Contacts_ContactIdFk = RelationalModel.GetForeignKey(this,
                "WebApp.DatabaseAccess.Model.Address",
                new[] { "ContactId" },
                "WebApp.DatabaseAccess.Model.Contact",
                new[] { "Id" });
            fK_Addresses_Contacts_ContactId.MappedForeignKeys.Add(fK_Addresses_Contacts_ContactIdFk);
            RelationalModel.GetOrCreateForeignKeyConstraints(fK_Addresses_Contacts_ContactIdFk).Add(fK_Addresses_Contacts_ContactId);
            addressesTable.ForeignKeyConstraints.Add(fK_Addresses_Contacts_ContactId);
            contactsTable.ReferencingForeignKeyConstraints.Add(fK_Addresses_Contacts_ContactId);
            return relationalModel.MakeReadOnly();
        }
    }
}