-- --------------------------------------------------
-- Entity Designer DDL Script for Oracle database
-- --------------------------------------------------
-- Date Created: 04/12/2019 12:00:46 PM
-- Generated from EDMX file: D:\MyProject\histaffHCM\histaffhcm\CommonDAL\CommonContext.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- ALTER TABLE "CommonModelStoreContainer"."SE_GRP_SE_USR" DROP CONSTRAINT "FK_SE_GRP_SE_USR_SE_GROUP" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_GRP_SE_USR" DROP CONSTRAINT "FK_SE_GRP_SE_USR_SE_USER" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_FUNCTION" DROP CONSTRAINT "FK_SM_SF" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_GROUP_PERMISSION" DROP CONSTRAINT "FK_SF_SGP" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_GROUP_PERMISSION" DROP CONSTRAINT "FK_SG_SGP" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."OT_OTHER_LIST_TYPE" DROP CONSTRAINT "FK_OOLG_OOLT" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."OT_OTHER_LIST" DROP CONSTRAINT "FK_OOLT_OOL" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_REPORT" DROP CONSTRAINT "FK_SM_SR" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_GROUP_REPORT" DROP CONSTRAINT "FK_SE_GROUP_REPORT_SE_GROUP" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_GROUP_REPORT" DROP CONSTRAINT "FK_SE_GROUP_REPORT_SE_REPORT" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."HU_ORGANIZATION" DROP CONSTRAINT "FK_HU_ORG_HU_ORG" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_FUNCTION" DROP CONSTRAINT "FK_SFG_SF" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."HU_EMPLOYEE" DROP CONSTRAINT "FK_HU_ORG_HU_EMP" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."HU_EMPLOYEE" DROP CONSTRAINT "FK_HU_TITLEHU_EMPLOYEE" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_USER_REPORT" DROP CONSTRAINT "FK_SE_USER_REPORT_SE_REPORT" CASCADE;

-- ALTER TABLE "CommonModelStoreContainer"."SE_USER_REPORT" DROP CONSTRAINT "FK_SE_USER_REPORT_SE_USER" CASCADE;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- DROP TABLE "CommonModelStoreContainer"."SE_GROUP";

-- DROP TABLE "CommonModelStoreContainer"."SE_FUNCTION";

-- DROP TABLE "CommonModelStoreContainer"."SE_USER";

-- DROP TABLE "CommonModelStoreContainer"."SE_MODULE";

-- DROP TABLE "CommonModelStoreContainer"."SE_GROUP_PERMISSION";

-- DROP TABLE "CommonModelStoreContainer"."OT_OTHER_LIST";

-- DROP TABLE "CommonModelStoreContainer"."OT_OTHER_LIST_GROUP";

-- DROP TABLE "CommonModelStoreContainer"."OT_OTHER_LIST_TYPE";

-- DROP TABLE "CommonModelStoreContainer"."SE_REPORT";

-- DROP TABLE "CommonModelStoreContainer"."HU_ORGANIZATION";

-- DROP TABLE "CommonModelStoreContainer"."SE_FUNCTION_GROUP";

-- DROP TABLE "CommonModelStoreContainer"."HU_EMPLOYEE";

-- DROP TABLE "CommonModelStoreContainer"."HU_TERMINATE";

-- DROP TABLE "CommonModelStoreContainer"."SE_MAIL";

-- DROP TABLE "CommonModelStoreContainer"."SE_REMINDER";

-- DROP TABLE "CommonModelStoreContainer"."HU_TITLE";

-- DROP TABLE "CommonModelStoreContainer"."SE_APP_PROCESS";

-- DROP TABLE "CommonModelStoreContainer"."SE_APP_SETUP";

-- DROP TABLE "CommonModelStoreContainer"."SE_APP_SETUPEXT";

-- DROP TABLE "CommonModelStoreContainer"."SE_APP_TEMPLATE";

-- DROP TABLE "CommonModelStoreContainer"."SE_APP_TEMPLATE_DTL";

-- DROP TABLE "CommonModelStoreContainer"."HU_WORKING";

-- DROP TABLE "CommonModelStoreContainer"."SE_CHOSEN_ORG";

-- DROP TABLE "CommonModelStoreContainer"."HU_EMPLOYEE_CV";

-- DROP TABLE "CommonModelStoreContainer"."HU_STAFF_RANK";

-- DROP TABLE "CommonModelStoreContainer"."SE_LDAP";

-- DROP TABLE "CommonModelStoreContainer"."SE_USER_ORG_ACCESS";

-- DROP TABLE "CommonModelStoreContainer"."SE_USER_PERMISSION";

-- DROP TABLE "CommonModelStoreContainer"."HUV_SE_GRP_SE_USR";

-- DROP TABLE "CommonModelStoreContainer"."SE_VIEW_CONFIG";

-- DROP TABLE "CommonModelStoreContainer"."SE_GRP_SE_USR";

-- DROP TABLE "CommonModelStoreContainer"."SE_GROUP_REPORT";

-- DROP TABLE "CommonModelStoreContainer"."SE_USER_REPORT";

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SE_GROUP'
CREATE TABLE "dbo"."SE_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "IS_ADMIN" NUMBER(5,0) NULL,
   "CODE" VARCHAR2(35) NOT NULL,
   "EFFECT_DATE" DATE NOT NULL,
   "EXPIRE_DATE" DATE NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL
);

-- Creating table 'SE_FUNCTION'
CREATE TABLE "dbo"."SE_FUNCTION" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "MODULE_ID" NUMBER(38,0) NULL,
   "FID" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "GROUP_ID" NUMBER(38,0) NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL
);

-- Creating table 'SE_USER'
CREATE TABLE "dbo"."SE_USER" (
   "ID" NUMBER(38,0) NOT NULL,
   "USERNAME" NVARCHAR2(255) NOT NULL,
   "PASSWORD" NVARCHAR2(255) NULL,
   "FULLNAME" NVARCHAR2(255) NOT NULL,
   "EMAIL" NVARCHAR2(255) NULL,
   "TELEPHONE" NVARCHAR2(255) NULL,
   "LOCATION" NVARCHAR2(255) NULL,
   "IS_APP" NUMBER(5,0) NOT NULL,
   "IS_PORTAL" NUMBER(5,0) NOT NULL,
   "EMPLOYEE_CODE" VARCHAR2(15) NULL,
   "IS_AD" NUMBER(5,0) NULL,
   "EFFECT_DATE" DATE NULL,
   "EXPIRE_DATE" DATE NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "IS_CHANGE_PASS" NUMBER(5,0) NULL,
   "CHANGE_PASS_DATE" DATE NULL,
   "REMINDER_COUNT" NUMBER(5,0) NULL,
   "MODULE_ADMIN" NVARCHAR2(255) NULL
);

-- Creating table 'SE_MODULE'
CREATE TABLE "dbo"."SE_MODULE" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "MID" NVARCHAR2(255) NOT NULL
);

-- Creating table 'SE_GROUP_PERMISSION'
CREATE TABLE "dbo"."SE_GROUP_PERMISSION" (
   "ID" NUMBER(38,0) NOT NULL,
   "GROUP_ID" NUMBER(38,0) NOT NULL,
   "ALLOW_CREATE" NUMBER(5,0) NOT NULL,
   "ALLOW_MODIFY" NUMBER(5,0) NOT NULL,
   "ALLOW_DELETE" NUMBER(5,0) NOT NULL,
   "ALLOW_PRINT" NUMBER(5,0) NOT NULL,
   "ALLOW_IMPORT" NUMBER(5,0) NOT NULL,
   "ALLOW_EXPORT" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL1" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL2" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL3" NUMBER(5,0) NOT NULL,
   "FUNCTION_ID" NUMBER(38,0) NOT NULL,
   "ALLOW_SPECIAL4" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL5" NUMBER(5,0) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL
);

-- Creating table 'OT_OTHER_LIST'
CREATE TABLE "dbo"."OT_OTHER_LIST" (
   "ID" NUMBER(38,0) NOT NULL,
   "TYPE_ID" NUMBER(38,0) NOT NULL,
   "NAME_VN" NVARCHAR2(255) NOT NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "ATTRIBUTE1" NVARCHAR2(255) NULL,
   "ATTRIBUTE2" NVARCHAR2(255) NULL,
   "ATTRIBUTE3" NVARCHAR2(255) NULL,
   "ATTRIBUTE4" NVARCHAR2(255) NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "TYPE_CODE" NVARCHAR2(255) NULL,
   "REMARK" NVARCHAR2(1023) NULL
);

-- Creating table 'OT_OTHER_LIST_GROUP'
CREATE TABLE "dbo"."OT_OTHER_LIST_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL
);

-- Creating table 'OT_OTHER_LIST_TYPE'
CREATE TABLE "dbo"."OT_OTHER_LIST_TYPE" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(255) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "GROUP_ID" NUMBER(38,0) NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "IS_SYSTEM" NUMBER(5,0) NULL
);

-- Creating table 'SE_REPORT'
CREATE TABLE "dbo"."SE_REPORT" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "CODE" NVARCHAR2(255) NOT NULL,
   "MODULE_ID" NUMBER(38,0) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL
);

-- Creating table 'HU_ORGANIZATION'
CREATE TABLE "dbo"."HU_ORGANIZATION" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NOT NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "NAME_VN" NVARCHAR2(255) NOT NULL,
   "PARENT_ID" NUMBER(38,0) NULL,
   "DISSOLVE_DATE" DATE NULL,
   "FOUNDATION_DATE" DATE NOT NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "HIERARCHICAL_PATH" VARCHAR2(1023) NULL,
   "DESCRIPTION_PATH" NVARCHAR2(1023) NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "ADDRESS" NVARCHAR2(1023) NULL,
   "FAX" NVARCHAR2(255) NULL,
   "REPRESENTATIVE_ID" NUMBER(38,0) NOT NULL,
   "ORD_NO" NUMBER(38,0) NULL
);

-- Creating table 'SE_FUNCTION_GROUP'
CREATE TABLE "dbo"."SE_FUNCTION_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(255) NOT NULL,
   "CODE" NVARCHAR2(255) NULL
);

-- Creating table 'HU_EMPLOYEE'
CREATE TABLE "dbo"."HU_EMPLOYEE" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_CODE" NVARCHAR2(15) NOT NULL,
   "FIRST_NAME_VN" NVARCHAR2(255) NOT NULL,
   "LAST_NAME_VN" NVARCHAR2(255) NOT NULL,
   "FULLNAME_VN" NVARCHAR2(255) NOT NULL,
   "FIRST_NAME_EN" VARCHAR2(255) NULL,
   "LAST_NAME_EN" NVARCHAR2(255) NULL,
   "FULLNAME_EN" NVARCHAR2(255) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "WORK_STATUS" NUMBER(38,0) NULL,
   "CONTRACT_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NOT NULL,
   "JOIN_DATE" DATE NULL,
   "DIRECT_MANAGER" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "LAST_WORKING_ID" NUMBER(38,0) NULL,
   "TER_EFFECT_DATE" DATE NULL,
   "TER_LAST_DATE" DATE NULL,
   "STAFF_RANK_ID" NUMBER(38,0) NULL,
   "ITIME_ID" NUMBER(38,0) NULL,
   "PA_OBJECT_SALARY_ID" NUMBER(38,0) NULL,
   "IS_3B" NUMBER(5,0) NULL
);

-- Creating table 'HU_TERMINATE'
CREATE TABLE "dbo"."HU_TERMINATE" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "IS_NOHIRE" NUMBER(5,0) NULL,
   "SEND_DATE" DATE NULL,
   "TER_REASON_DETAIL" NVARCHAR2(1023) NULL,
   "LAST_DATE" DATE NULL,
   "STATUS_ID" NUMBER(38,0) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "EMP_SENIORITY" NVARCHAR2(255) NULL,
   "LAST_WORKING_ID" NUMBER(38,0) NULL,
   "APPROVAL_DATE" DATE NULL,
   "IDENTIFI_CARD" NUMBER(38,0) NULL,
   "IDENTIFI_RDATE" DATE NULL,
   "IDENTIFI_STATUS" NVARCHAR2(255) NULL,
   "SUN_CARD" NUMBER(38,0) NULL,
   "SUN_RDATE" DATE NULL,
   "SUN_STATUS" NVARCHAR2(255) NULL,
   "INSURANCE_CARD" NUMBER(38,0) NULL,
   "INSURANCE_RDATE" DATE NULL,
   "INSURANCE_STATUS" NVARCHAR2(255) NULL,
   "REMAINING_LEAVE" NUMBER(38,0) NULL,
   "PAYMENT_LEAVE" NUMBER(38,0) NULL,
   "AMOUNT_VIOLATIONS" NUMBER(38,0) NULL,
   "AMOUNT_WRONGFUL" NUMBER(38,0) NULL,
   "ALLOWANCE_TERMINATE" NUMBER(38,0) NULL,
   "TRAINING_COSTS" NUMBER(38,0) NULL,
   "OTHER_COMPENSATION" NUMBER(38,0) NULL,
   "COMPENSATORY_LEAVE" NUMBER(38,0) NULL,
   "COMPENSATORY_PAYMENT" NUMBER(38,0) NULL,
   "DECISION_NO" NVARCHAR2(255) NULL,
   "SIGN_DATE" DATE NULL,
   "SIGN_ID" NUMBER(38,0) NULL,
   "SIGN_NAME" NVARCHAR2(255) NULL,
   "SIGN_TITLE" NVARCHAR2(255) NULL,
   "EFFECT_DATE" DATE NULL,
   "IDENTIFI_MONEY" NUMBER(38,0) NULL,
   "SUN_MONEY" NUMBER(38,0) NULL,
   "INSURANCE_MONEY" NUMBER(38,0) NULL,
   "IS_3B" NUMBER(5,0) NULL
);

-- Creating table 'SE_MAIL'
CREATE TABLE "dbo"."SE_MAIL" (
   "ID" NUMBER(38,0) NOT NULL,
   "SUBJECT" NVARCHAR2(255) NOT NULL,
   "MAIL_FROM" NVARCHAR2(255) NOT NULL,
   "MAIL_TO" NVARCHAR2(1024) NOT NULL,
   "MAIL_CC" NVARCHAR2(1024) NULL,
   "MAIL_BCC" NVARCHAR2(1024) NULL,
   "CONTENT" NCLOB NOT NULL,
   "VIEW_NAME" NVARCHAR2(255) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "ATTACHMENT" NCLOB NULL
);

-- Creating table 'SE_REMINDER'
CREATE TABLE "dbo"."SE_REMINDER" (
   "USERNAME" NVARCHAR2(255) NOT NULL,
   "TYPE" NUMBER(5,0) NOT NULL,
   "VALUE" NVARCHAR2(255) NULL,
   "ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'HU_TITLE'
CREATE TABLE "dbo"."HU_TITLE" (
   "ID" NUMBER(38,0) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "REMARK" NVARCHAR2(255) NULL,
   "CODE" NVARCHAR2(255) NOT NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "NAME_VN" NVARCHAR2(255) NOT NULL,
   "ACTFLG" NVARCHAR2(1) NOT NULL,
   "TITLE_GROUP_ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_APP_PROCESS'
CREATE TABLE "dbo"."SE_APP_PROCESS" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME" NVARCHAR2(150) NULL,
   "ACTFLG" VARCHAR2(1) NULL,
   "PROCESS_CODE" VARCHAR2(50) NULL,
   "NUMREQUEST" NUMBER(38,0) NULL,
   "EMAIL" NVARCHAR2(200) NULL
);

-- Creating table 'SE_APP_SETUP'
CREATE TABLE "dbo"."SE_APP_SETUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "PROCESS_ID" NUMBER(38,0) NULL,
   "TEMPLATE_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "FROM_DATE" DATE NULL,
   "TO_DATE" DATE NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'SE_APP_SETUPEXT'
CREATE TABLE "dbo"."SE_APP_SETUPEXT" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "SUB_EMPLOYEE_ID" NUMBER(38,0) NULL,
   "PROCESS_ID" NUMBER(38,0) NULL,
   "FROM_DATE" DATE NULL,
   "TO_DATE" DATE NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "REPLACEALL" NUMBER(38,0) NULL
);

-- Creating table 'SE_APP_TEMPLATE'
CREATE TABLE "dbo"."SE_APP_TEMPLATE" (
   "ID" NUMBER(38,0) NOT NULL,
   "TEMPLATE_NAME" NVARCHAR2(100) NULL,
   "TEMPLATE_TYPE" NUMBER(38,0) NULL,
   "TEMPLATE_ORDER" NUMBER(38,0) NULL,
   "ACTFLG" VARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'SE_APP_TEMPLATE_DTL'
CREATE TABLE "dbo"."SE_APP_TEMPLATE_DTL" (
   "ID" NUMBER(38,0) NOT NULL,
   "TEMPLATE_ID" NUMBER(38,0) NULL,
   "APP_LEVEL" NUMBER(38,0) NULL,
   "APP_TYPE" NUMBER(38,0) NULL,
   "APP_ID" NUMBER(38,0) NULL,
   "INFORM_DATE" NUMBER(38,0) NULL,
   "INFORM_EMAIL" VARCHAR2(150) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'HU_WORKING'
CREATE TABLE "dbo"."HU_WORKING" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "TRANSFER_TYPE" NUMBER(38,0) NULL,
   "TRANSFER_REASON" NUMBER(38,0) NULL,
   "INDIRECT_MANAGER" NUMBER(38,0) NULL,
   "DIRECT_MANAGER" NUMBER(38,0) NULL,
   "IS_DECISION" NUMBER(5,0) NULL,
   "DECISION_ID" NUMBER(38,0) NULL,
   "STATUS" NUMBER(38,0) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "EFFECT_DATE" DATE NULL,
   "EXPIRE_DATE" DATE NULL,
   "WORKING_TYPE" NUMBER(5,0) NULL,
   "COST_CENTER" NUMBER(38,0) NULL,
   "BASIC_SAL" NUMBER(38,0) NULL,
   "SOFT_SAL" NUMBER(38,0) NULL,
   "OTHER_SAL" NUMBER(38,0) NULL,
   "COEFFICIENT" NUMBER(38,0) NULL,
   "CONTRACTUAL_SAL" NUMBER(38,0) NULL,
   "SALARY_ENTERPRISE_ID" NUMBER(38,0) NULL,
   "ALLOWANCE_IDS" NVARCHAR2(255) NULL,
   "ALLOWANCE_SAL" NUMBER(38,0) NULL,
   "ALLOWANCE_TOTAL" NUMBER(38,0) NULL
);

-- Creating table 'SE_CHOSEN_ORG'
CREATE TABLE "dbo"."SE_CHOSEN_ORG" (
   "ORG_ID" NUMBER(38,0) NOT NULL,
   "USERNAME" NVARCHAR2(255) NOT NULL
);

-- Creating table 'HU_EMPLOYEE_CV'
CREATE TABLE "dbo"."HU_EMPLOYEE_CV" (
   "EMPLOYEE_ID" NUMBER(38,0) NOT NULL,
   "GENDER" NUMBER(38,0) NULL,
   "IMAGE" NVARCHAR2(255) NULL,
   "BIRTH_DATE" DATE NULL,
   "BIRTH_PLACE" NVARCHAR2(255) NULL,
   "MARITAL_STATUS" NUMBER(38,0) NULL,
   "RELIGION" NUMBER(38,0) NULL,
   "NATIVE" NUMBER(38,0) NULL,
   "NATIONALITY" NUMBER(38,0) NULL,
   "PER_ADDRESS" NVARCHAR2(255) NULL,
   "PER_PROVINCE" NUMBER(38,0) NULL,
   "HOME_PHONE" NVARCHAR2(255) NULL,
   "MOBILE_PHONE" NVARCHAR2(255) NULL,
   "ID_NO" NVARCHAR2(15) NULL,
   "ID_DATE" DATE NULL,
   "PASS_NO" NVARCHAR2(255) NULL,
   "PASS_DATE" DATE NULL,
   "PASS_EXPIRE" DATE NULL,
   "PASS_PLACE" NVARCHAR2(255) NULL,
   "VISA" NVARCHAR2(255) NULL,
   "VISA_DATE" DATE NULL,
   "VISA_EXPIRE" DATE NULL,
   "VISA_PLACE" NVARCHAR2(255) NULL,
   "WORK_PERMIT" NVARCHAR2(255) NULL,
   "WORK_PERMIT_DATE" DATE NULL,
   "WORK_PERMIT_EXPIRE" DATE NULL,
   "WORK_EMAIL" NVARCHAR2(255) NULL,
   "NAV_ADDRESS" NVARCHAR2(255) NULL,
   "NAV_PROVINCE" NUMBER(38,0) NULL,
   "PIT_CODE" VARCHAR2(255) NULL,
   "PER_EMAIL" NVARCHAR2(255) NULL,
   "CONTACT_PER" NVARCHAR2(255) NULL,
   "CONTACT_PER_PHONE" NVARCHAR2(15) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "ID_PLACE" NVARCHAR2(255) NULL,
   "WORK_PERMIT_PLACE" NVARCHAR2(255) NULL,
   "BANK_ID" NUMBER(38,0) NULL,
   "BANK_BRANCH_ID" NUMBER(38,0) NULL,
   "NGAY_VAO_DOAN" DATE NULL,
   "NOI_VAO_DOAN" NVARCHAR2(255) NULL,
   "CHUC_VU_DOAN" NVARCHAR2(255) NULL,
   "DOAN_PHI" NUMBER(5,0) NULL,
   "NGAY_VAO_DANG" DATE NULL,
   "NOI_VAO_DANG" NVARCHAR2(255) NULL,
   "CHUC_VU_DANG" NVARCHAR2(255) NULL,
   "DANG_PHI" NUMBER(5,0) NULL,
   "BANK_NO" NVARCHAR2(255) NULL,
   "CAREER" NVARCHAR2(255) NULL
);

-- Creating table 'HU_STAFF_RANK'
CREATE TABLE "dbo"."HU_STAFF_RANK" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL
);

-- Creating table 'SE_LDAP'
CREATE TABLE "dbo"."SE_LDAP" (
   "ID" NUMBER(38,0) NOT NULL,
   "LDAP_NAME" NVARCHAR2(255) NOT NULL,
   "DOMAIN_NAME" NVARCHAR2(255) NOT NULL,
   "BASE_DN" NVARCHAR2(255) NOT NULL
);

-- Creating table 'SE_USER_ORG_ACCESS'
CREATE TABLE "dbo"."SE_USER_ORG_ACCESS" (
   "ID" NUMBER(38,0) NOT NULL,
   "USER_ID" NUMBER(38,0) NOT NULL,
   "ORG_ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_USER_PERMISSION'
CREATE TABLE "dbo"."SE_USER_PERMISSION" (
   "ID" NUMBER(38,0) NOT NULL,
   "USER_ID" NUMBER(38,0) NOT NULL,
   "ALLOW_CREATE" NUMBER(5,0) NOT NULL,
   "ALLOW_MODIFY" NUMBER(5,0) NOT NULL,
   "ALLOW_DELETE" NUMBER(5,0) NOT NULL,
   "ALLOW_PRINT" NUMBER(5,0) NOT NULL,
   "ALLOW_IMPORT" NUMBER(5,0) NOT NULL,
   "ALLOW_EXPORT" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL1" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL2" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL3" NUMBER(5,0) NOT NULL,
   "FUNCTION_ID" NUMBER(38,0) NOT NULL,
   "ALLOW_SPECIAL4" NUMBER(5,0) NOT NULL,
   "ALLOW_SPECIAL5" NUMBER(5,0) NOT NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL,
   "GROUP_ID" NUMBER(38,0) NULL
);

-- Creating table 'HUV_SE_GRP_SE_USR'
CREATE TABLE "dbo"."HUV_SE_GRP_SE_USR" (
   "SE_USERS_ID" NUMBER(38,0) NOT NULL,
   "SE_GROUPS_ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_VIEW_CONFIG'
CREATE TABLE "dbo"."SE_VIEW_CONFIG" (
   "CODE_NAME" NCLOB NOT NULL,
   "CONFIG_DATA" NCLOB NULL
);

-- Creating table 'SE_GRP_SE_USR'
CREATE TABLE "dbo"."SE_GRP_SE_USR" (
   "SE_GROUPS_ID" NUMBER(38,0) NOT NULL,
   "SE_USERS_ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_GROUP_REPORT'
CREATE TABLE "dbo"."SE_GROUP_REPORT" (
   "SE_GROUPS_ID" NUMBER(38,0) NOT NULL,
   "SE_REPORTS_ID" NUMBER(38,0) NOT NULL
);

-- Creating table 'SE_USER_REPORT'
CREATE TABLE "dbo"."SE_USER_REPORT" (
   "SE_REPORT_ID" NUMBER(38,0) NOT NULL,
   "SE_USER_ID" NUMBER(38,0) NOT NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on "ID"in table 'SE_GROUP'
ALTER TABLE "dbo"."SE_GROUP"
ADD CONSTRAINT "PK_SE_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_FUNCTION'
ALTER TABLE "dbo"."SE_FUNCTION"
ADD CONSTRAINT "PK_SE_FUNCTION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_USER'
ALTER TABLE "dbo"."SE_USER"
ADD CONSTRAINT "PK_SE_USER"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_MODULE'
ALTER TABLE "dbo"."SE_MODULE"
ADD CONSTRAINT "PK_SE_MODULE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_GROUP_PERMISSION'
ALTER TABLE "dbo"."SE_GROUP_PERMISSION"
ADD CONSTRAINT "PK_SE_GROUP_PERMISSION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'OT_OTHER_LIST'
ALTER TABLE "dbo"."OT_OTHER_LIST"
ADD CONSTRAINT "PK_OT_OTHER_LIST"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'OT_OTHER_LIST_GROUP'
ALTER TABLE "dbo"."OT_OTHER_LIST_GROUP"
ADD CONSTRAINT "PK_OT_OTHER_LIST_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'OT_OTHER_LIST_TYPE'
ALTER TABLE "dbo"."OT_OTHER_LIST_TYPE"
ADD CONSTRAINT "PK_OT_OTHER_LIST_TYPE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_REPORT'
ALTER TABLE "dbo"."SE_REPORT"
ADD CONSTRAINT "PK_SE_REPORT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_ORGANIZATION'
ALTER TABLE "dbo"."HU_ORGANIZATION"
ADD CONSTRAINT "PK_HU_ORGANIZATION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_FUNCTION_GROUP'
ALTER TABLE "dbo"."SE_FUNCTION_GROUP"
ADD CONSTRAINT "PK_SE_FUNCTION_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "PK_HU_EMPLOYEE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_TERMINATE'
ALTER TABLE "dbo"."HU_TERMINATE"
ADD CONSTRAINT "PK_HU_TERMINATE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_MAIL'
ALTER TABLE "dbo"."SE_MAIL"
ADD CONSTRAINT "PK_SE_MAIL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_REMINDER'
ALTER TABLE "dbo"."SE_REMINDER"
ADD CONSTRAINT "PK_SE_REMINDER"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_TITLE'
ALTER TABLE "dbo"."HU_TITLE"
ADD CONSTRAINT "PK_HU_TITLE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_APP_PROCESS'
ALTER TABLE "dbo"."SE_APP_PROCESS"
ADD CONSTRAINT "PK_SE_APP_PROCESS"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_APP_SETUP'
ALTER TABLE "dbo"."SE_APP_SETUP"
ADD CONSTRAINT "PK_SE_APP_SETUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_APP_SETUPEXT'
ALTER TABLE "dbo"."SE_APP_SETUPEXT"
ADD CONSTRAINT "PK_SE_APP_SETUPEXT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_APP_TEMPLATE'
ALTER TABLE "dbo"."SE_APP_TEMPLATE"
ADD CONSTRAINT "PK_SE_APP_TEMPLATE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_APP_TEMPLATE_DTL'
ALTER TABLE "dbo"."SE_APP_TEMPLATE_DTL"
ADD CONSTRAINT "PK_SE_APP_TEMPLATE_DTL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_WORKING'
ALTER TABLE "dbo"."HU_WORKING"
ADD CONSTRAINT "PK_HU_WORKING"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ORG_ID", "USERNAME"in table 'SE_CHOSEN_ORG'
ALTER TABLE "dbo"."SE_CHOSEN_ORG"
ADD CONSTRAINT "PK_SE_CHOSEN_ORG"
   PRIMARY KEY ("ORG_ID", "USERNAME" )
   ENABLE
   VALIDATE;


-- Creating primary key on "EMPLOYEE_ID"in table 'HU_EMPLOYEE_CV'
ALTER TABLE "dbo"."HU_EMPLOYEE_CV"
ADD CONSTRAINT "PK_HU_EMPLOYEE_CV"
   PRIMARY KEY ("EMPLOYEE_ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_STAFF_RANK'
ALTER TABLE "dbo"."HU_STAFF_RANK"
ADD CONSTRAINT "PK_HU_STAFF_RANK"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_LDAP'
ALTER TABLE "dbo"."SE_LDAP"
ADD CONSTRAINT "PK_SE_LDAP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_USER_ORG_ACCESS'
ALTER TABLE "dbo"."SE_USER_ORG_ACCESS"
ADD CONSTRAINT "PK_SE_USER_ORG_ACCESS"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'SE_USER_PERMISSION'
ALTER TABLE "dbo"."SE_USER_PERMISSION"
ADD CONSTRAINT "PK_SE_USER_PERMISSION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "SE_USERS_ID", "SE_GROUPS_ID"in table 'HUV_SE_GRP_SE_USR'
ALTER TABLE "dbo"."HUV_SE_GRP_SE_USR"
ADD CONSTRAINT "PK_HUV_SE_GRP_SE_USR"
   PRIMARY KEY ("SE_USERS_ID", "SE_GROUPS_ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "CODE_NAME"in table 'SE_VIEW_CONFIG'
ALTER TABLE "dbo"."SE_VIEW_CONFIG"
ADD CONSTRAINT "PK_SE_VIEW_CONFIG"
   PRIMARY KEY ("CODE_NAME" )
   ENABLE
   VALIDATE;


-- Creating primary key on "SE_GROUPS_ID", "SE_USERS_ID"in table 'SE_GRP_SE_USR'
ALTER TABLE "dbo"."SE_GRP_SE_USR"
ADD CONSTRAINT "PK_SE_GRP_SE_USR"
   PRIMARY KEY ("SE_GROUPS_ID", "SE_USERS_ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "SE_GROUPS_ID", "SE_REPORTS_ID"in table 'SE_GROUP_REPORT'
ALTER TABLE "dbo"."SE_GROUP_REPORT"
ADD CONSTRAINT "PK_SE_GROUP_REPORT"
   PRIMARY KEY ("SE_GROUPS_ID", "SE_REPORTS_ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "SE_REPORT_ID", "SE_USER_ID"in table 'SE_USER_REPORT'
ALTER TABLE "dbo"."SE_USER_REPORT"
ADD CONSTRAINT "PK_SE_USER_REPORT"
   PRIMARY KEY ("SE_REPORT_ID", "SE_USER_ID" )
   ENABLE
   VALIDATE;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on "SE_GROUPS_ID" in table 'SE_GRP_SE_USR'
ALTER TABLE "dbo"."SE_GRP_SE_USR"
ADD CONSTRAINT "FK_SE_GRP_SE_USR_SE_GROUP"
   FOREIGN KEY ("SE_GROUPS_ID")
   REFERENCES "dbo"."SE_GROUP"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_GRP_SE_USR_SE_GROUP'
CREATE INDEX "IX_FK_SE_GRP_SE_USR_SE_GROUP"
ON "dbo"."SE_GRP_SE_USR"
   ("SE_GROUPS_ID");

-- Creating foreign key on "SE_USERS_ID" in table 'SE_GRP_SE_USR'
ALTER TABLE "dbo"."SE_GRP_SE_USR"
ADD CONSTRAINT "FK_SE_GRP_SE_USR_SE_USER"
   FOREIGN KEY ("SE_USERS_ID")
   REFERENCES "dbo"."SE_USER"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_GRP_SE_USR_SE_USER'
CREATE INDEX "IX_FK_SE_GRP_SE_USR_SE_USER"
ON "dbo"."SE_GRP_SE_USR"
   ("SE_USERS_ID");

-- Creating foreign key on "MODULE_ID" in table 'SE_FUNCTION'
ALTER TABLE "dbo"."SE_FUNCTION"
ADD CONSTRAINT "FK_SM_SF"
   FOREIGN KEY ("MODULE_ID")
   REFERENCES "dbo"."SE_MODULE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SM_SF'
CREATE INDEX "IX_FK_SM_SF"
ON "dbo"."SE_FUNCTION"
   ("MODULE_ID");

-- Creating foreign key on "FUNCTION_ID" in table 'SE_GROUP_PERMISSION'
ALTER TABLE "dbo"."SE_GROUP_PERMISSION"
ADD CONSTRAINT "FK_SF_SGP"
   FOREIGN KEY ("FUNCTION_ID")
   REFERENCES "dbo"."SE_FUNCTION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SF_SGP'
CREATE INDEX "IX_FK_SF_SGP"
ON "dbo"."SE_GROUP_PERMISSION"
   ("FUNCTION_ID");

-- Creating foreign key on "GROUP_ID" in table 'SE_GROUP_PERMISSION'
ALTER TABLE "dbo"."SE_GROUP_PERMISSION"
ADD CONSTRAINT "FK_SG_SGP"
   FOREIGN KEY ("GROUP_ID")
   REFERENCES "dbo"."SE_GROUP"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SG_SGP'
CREATE INDEX "IX_FK_SG_SGP"
ON "dbo"."SE_GROUP_PERMISSION"
   ("GROUP_ID");

-- Creating foreign key on "GROUP_ID" in table 'OT_OTHER_LIST_TYPE'
ALTER TABLE "dbo"."OT_OTHER_LIST_TYPE"
ADD CONSTRAINT "FK_OOLG_OOLT"
   FOREIGN KEY ("GROUP_ID")
   REFERENCES "dbo"."OT_OTHER_LIST_GROUP"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OOLG_OOLT'
CREATE INDEX "IX_FK_OOLG_OOLT"
ON "dbo"."OT_OTHER_LIST_TYPE"
   ("GROUP_ID");

-- Creating foreign key on "TYPE_ID" in table 'OT_OTHER_LIST'
ALTER TABLE "dbo"."OT_OTHER_LIST"
ADD CONSTRAINT "FK_OOLT_OOL"
   FOREIGN KEY ("TYPE_ID")
   REFERENCES "dbo"."OT_OTHER_LIST_TYPE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OOLT_OOL'
CREATE INDEX "IX_FK_OOLT_OOL"
ON "dbo"."OT_OTHER_LIST"
   ("TYPE_ID");

-- Creating foreign key on "MODULE_ID" in table 'SE_REPORT'
ALTER TABLE "dbo"."SE_REPORT"
ADD CONSTRAINT "FK_SM_SR"
   FOREIGN KEY ("MODULE_ID")
   REFERENCES "dbo"."SE_MODULE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SM_SR'
CREATE INDEX "IX_FK_SM_SR"
ON "dbo"."SE_REPORT"
   ("MODULE_ID");

-- Creating foreign key on "SE_GROUPS_ID" in table 'SE_GROUP_REPORT'
ALTER TABLE "dbo"."SE_GROUP_REPORT"
ADD CONSTRAINT "FK_SE_GROUP_REPORT_SE_GROUP"
   FOREIGN KEY ("SE_GROUPS_ID")
   REFERENCES "dbo"."SE_GROUP"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_GROUP_REPORT_SE_GROUP'
CREATE INDEX "IX_FK_SE_GROUP_REPORT_SE_GROUP"
ON "dbo"."SE_GROUP_REPORT"
   ("SE_GROUPS_ID");

-- Creating foreign key on "SE_REPORTS_ID" in table 'SE_GROUP_REPORT'
ALTER TABLE "dbo"."SE_GROUP_REPORT"
ADD CONSTRAINT "FK_SE_GROUP_REPORT_SE_REPORT"
   FOREIGN KEY ("SE_REPORTS_ID")
   REFERENCES "dbo"."SE_REPORT"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_GROUP_REPORT_SE_REPORT'
CREATE INDEX "IX_FK_SE_GROUP_REPORT_SE_REPORT"
ON "dbo"."SE_GROUP_REPORT"
   ("SE_REPORTS_ID");

-- Creating foreign key on "PARENT_ID" in table 'HU_ORGANIZATION'
ALTER TABLE "dbo"."HU_ORGANIZATION"
ADD CONSTRAINT "FK_HU_ORG_HU_ORG"
   FOREIGN KEY ("PARENT_ID")
   REFERENCES "dbo"."HU_ORGANIZATION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HU_ORG_HU_ORG'
CREATE INDEX "IX_FK_HU_ORG_HU_ORG"
ON "dbo"."HU_ORGANIZATION"
   ("PARENT_ID");

-- Creating foreign key on "GROUP_ID" in table 'SE_FUNCTION'
ALTER TABLE "dbo"."SE_FUNCTION"
ADD CONSTRAINT "FK_SFG_SF"
   FOREIGN KEY ("GROUP_ID")
   REFERENCES "dbo"."SE_FUNCTION_GROUP"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SFG_SF'
CREATE INDEX "IX_FK_SFG_SF"
ON "dbo"."SE_FUNCTION"
   ("GROUP_ID");

-- Creating foreign key on "ORG_ID" in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "FK_HU_ORG_HU_EMP"
   FOREIGN KEY ("ORG_ID")
   REFERENCES "dbo"."HU_ORGANIZATION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HU_ORG_HU_EMP'
CREATE INDEX "IX_FK_HU_ORG_HU_EMP"
ON "dbo"."HU_EMPLOYEE"
   ("ORG_ID");

-- Creating foreign key on "TITLE_ID" in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "FK_HU_TITLEHU_EMPLOYEE"
   FOREIGN KEY ("TITLE_ID")
   REFERENCES "dbo"."HU_TITLE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HU_TITLEHU_EMPLOYEE'
CREATE INDEX "IX_FK_HU_TITLEHU_EMPLOYEE"
ON "dbo"."HU_EMPLOYEE"
   ("TITLE_ID");

-- Creating foreign key on "SE_REPORT_ID" in table 'SE_USER_REPORT'
ALTER TABLE "dbo"."SE_USER_REPORT"
ADD CONSTRAINT "FK_SE_USER_REPORT_SE_REPORT"
   FOREIGN KEY ("SE_REPORT_ID")
   REFERENCES "dbo"."SE_REPORT"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_USER_REPORT_SE_REPORT'
CREATE INDEX "IX_FK_SE_USER_REPORT_SE_REPORT"
ON "dbo"."SE_USER_REPORT"
   ("SE_REPORT_ID");

-- Creating foreign key on "SE_USER_ID" in table 'SE_USER_REPORT'
ALTER TABLE "dbo"."SE_USER_REPORT"
ADD CONSTRAINT "FK_SE_USER_REPORT_SE_USER"
   FOREIGN KEY ("SE_USER_ID")
   REFERENCES "dbo"."SE_USER"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_SE_USER_REPORT_SE_USER'
CREATE INDEX "IX_FK_SE_USER_REPORT_SE_USER"
ON "dbo"."SE_USER_REPORT"
   ("SE_USER_ID");

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
