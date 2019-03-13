-- --------------------------------------------------
-- Entity Designer DDL Script for Oracle database
-- --------------------------------------------------
-- Date Created: 03/06/2016 4:09:24 PM
-- Generated from EDMX file: E:\TVC_SGR\06.DEPLOYMENT\02.Sourcecode\01.SourceCode\Framework.Data\AuditLogContext.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- DROP TABLE "AuditLogModelStoreContainer"."SE_ACCESS_LOG";

-- DROP TABLE "AuditLogModelStoreContainer"."SE_ACTION_LOG";

-- DROP TABLE "AuditLogModelStoreContainer"."SE_CONFIG";

-- DROP TABLE "AuditLogModelStoreContainer"."SE_REMINDER";

-- DROP TABLE "AuditLogModelStoreContainer"."SE_LOG_DTL";

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SE_ACCESS_LOG'
CREATE TABLE "dbo"."SE_ACCESS_LOG" (
   "ID" NUMBER(38,0) NOT NULL,
   "USERNAME" NVARCHAR2(255) NULL,
   "FULLNAME" NVARCHAR2(255) NULL,
   "MOBILE" NVARCHAR2(255) NULL,
   "EMAIL" NVARCHAR2(255) NULL,
   "GROUP_NAMES" NVARCHAR2(255) NULL,
   "LOGIN_DATE" DATE NULL,
   "LOGOUT_DATE" DATE NULL,
   "LOGOUT_STATUS" NVARCHAR2(255) NULL,
   "ACCESS_FUNCTIONS" NVARCHAR2(2000) NULL,
   "IP" NVARCHAR2(255) NULL,
   "COMPUTER_NAME" NVARCHAR2(255) NULL
);

-- Creating table 'SE_ACTION_LOG'
CREATE TABLE "dbo"."SE_ACTION_LOG" (
   "ID" NUMBER(38,0) NOT NULL,
   "USERNAME" NVARCHAR2(255) NULL,
   "FULLNAME" NVARCHAR2(255) NULL,
   "MOBILE" NVARCHAR2(255) NULL,
   "EMAIL" NVARCHAR2(255) NULL,
   "ACTION_NAME" NVARCHAR2(255) NULL,
   "ACTION_DATE" DATE NULL,
   "OBJECT_ID" NUMBER(38,0) NULL,
   "OBJECT_NAME" NVARCHAR2(255) NULL,
   "IP" NVARCHAR2(255) NULL,
   "COMPUTER_NAME" NVARCHAR2(255) NULL,
   "VIEW_NAME" NVARCHAR2(255) NULL,
   "VIEW_DESCRIPTION" NVARCHAR2(255) NULL,
   "VIEW_GROUP" NVARCHAR2(255) NULL,
   "ACTION_TYPE" NVARCHAR2(255) NULL
);

-- Creating table 'SE_CONFIG'
CREATE TABLE "dbo"."SE_CONFIG" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(255) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "VALUE" NVARCHAR2(255) NULL,
   "MODULE" NUMBER(10,0) NULL
);

-- Creating table 'SE_REMINDER'
CREATE TABLE "dbo"."SE_REMINDER" (
   "USERNAME" NVARCHAR2(255) NULL,
   "TYPE" NUMBER(5,0) NULL,
   "VALUE" NVARCHAR2(255) NULL,
   "ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_LOG_DTL'
CREATE TABLE "dbo"."SE_LOG_DTL" (
   "ID" NUMBER(38,0) NOT NULL,
   "COL_NAME" NVARCHAR2(255) NULL,
   "OLD_VALUE" NVARCHAR2(1023) NULL,
   "NEW_VALUE" NVARCHAR2(1023) NULL,
   "SE_ACTION_LOG_ID" NUMBER(38,0) NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on "ID"in table 'SE_ACCESS_LOG'
ALTER TABLE "dbo"."SE_ACCESS_LOG"
ADD CONSTRAINT "PK_SE_ACCESS_LOG"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_ACTION_LOG'
ALTER TABLE "dbo"."SE_ACTION_LOG"
ADD CONSTRAINT "PK_SE_ACTION_LOG"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_CONFIG'
ALTER TABLE "dbo"."SE_CONFIG"
ADD CONSTRAINT "PK_SE_CONFIG"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_REMINDER'
ALTER TABLE "dbo"."SE_REMINDER"
ADD CONSTRAINT "PK_SE_REMINDER"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_LOG_DTL'
ALTER TABLE "dbo"."SE_LOG_DTL"
ADD CONSTRAINT "PK_SE_LOG_DTL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
