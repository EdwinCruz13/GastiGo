--
-- PostgreSQL database dump
--

\restrict IZ31n9THp2LJWXM61asVwPAx2WAYh8wchcYJLDH9ne14dc9Zk1z1MAsG86Mfbks

-- Dumped from database version 18.2
-- Dumped by pg_dump version 18.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: auth; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA auth;


ALTER SCHEMA auth OWNER TO postgres;

--
-- Name: finances; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA finances;


ALTER SCHEMA finances OWNER TO postgres;

--
-- Name: users; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA users;


ALTER SCHEMA users OWNER TO postgres;

--
-- Name: recalcular(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.recalcular()
    LANGUAGE plpgsql
    AS $$
BEGIN

    RAISE NOTICE 'Recalculando balances...';

    UPDATE finances."Transactions" t
    SET 
        "PreviousBalance" = sub."PreviousBalance",
        "Balance" = sub."Balance"
    FROM (
        SELECT 
            t."TransactionId",

            COALESCE(
                SUM(
                    CASE 
                        WHEN t."EntryType" = 'IN' THEN t."Amount"
                        WHEN t."EntryType" = 'OUT' THEN -t."Amount"
                        WHEN ty."Code" = 'FM' THEN t."Amount"
                        ELSE 0
                    END
                ) OVER (
                    PARTITION BY t."UserId", t."AccountId"
                    ORDER BY t."TransactionDate"
                    ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                ), 0
            ) AS "PreviousBalance",

            SUM(
                CASE 
                    WHEN t."EntryType" = 'IN' THEN t."Amount"
                    WHEN t."EntryType" = 'OUT' THEN -t."Amount"
                    WHEN ty."Code" = 'FM' THEN t."Amount"
                    ELSE 0
                END
            ) OVER (
                PARTITION BY t."UserId", t."AccountId"
                ORDER BY t."TransactionDate"
            ) AS "Balance"

        FROM finances."Transactions" t
        INNER JOIN finances."TransactionTypes" ty
            ON t."TransactionTypeId" = ty."TransactionTypeId"

    ) sub
    WHERE t."TransactionId" = sub."TransactionId";

    RAISE NOTICE 'Proceso finalizado';

END;
$$;


ALTER PROCEDURE public.recalcular() OWNER TO postgres;

--
-- Name: recalcular(uuid, uuid); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.recalcular(IN userid uuid, IN accountid uuid)
    LANGUAGE plpgsql
    AS $$
BEGIN

    RAISE NOTICE 'Recalculando balances...';

    UPDATE finances."Transactions" t
    SET 
        "PreviousBalance" = sub."PreviousBalance",
        "Balance" = sub."Balance"
    FROM (
        SELECT 
            x."TransactionId",

            COALESCE(
                SUM(x."Movimiento") OVER (
                    PARTITION BY x."UserId", x."AccountId"
                    ORDER BY x."TransactionDate", x."CreatedAt", x."TransactionId"
                    ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                ), 0
            ) AS "PreviousBalance",

            SUM(x."Movimiento") OVER (
                PARTITION BY x."UserId", x."AccountId"
                ORDER BY x."TransactionDate", x."CreatedAt", x."TransactionId"
                ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
            ) AS "Balance"

        FROM (
            SELECT 
                t."TransactionId",
                t."UserId",
                t."AccountId",
                t."TransactionDate",
                t."CreatedAt",

                CASE 
                    WHEN ty."Code" = 'FM' THEN t."Amount"
                    WHEN t."EntryType" = 'IN' THEN t."Amount"
                    WHEN t."EntryType" = 'OUT' THEN -t."Amount"
                    ELSE 0
                END AS "Movimiento"

            FROM finances."Transactions" t
            INNER JOIN finances."TransactionTypes" ty 
                ON t."TransactionTypeId" = ty."TransactionTypeId"
            WHERE t."UserId" = userid 
              AND t."AccountId" = accountid
        ) x
    ) sub
    WHERE t."TransactionId" = sub."TransactionId";

    RAISE NOTICE 'Proceso finalizado';

END;
$$;


ALTER PROCEDURE public.recalcular(IN userid uuid, IN accountid uuid) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: RefreshTokens; Type: TABLE; Schema: auth; Owner: postgres
--

CREATE TABLE auth."RefreshTokens" (
    "RefreshTokenId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Token" text NOT NULL,
    "ExpiresAt" timestamp with time zone NOT NULL,
    "Revoked" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE auth."RefreshTokens" OWNER TO postgres;

--
-- Name: TwoFactorCodes; Type: TABLE; Schema: auth; Owner: postgres
--

CREATE TABLE auth."TwoFactorCodes" (
    "TwoFactorCodeId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Code" text NOT NULL,
    "ExpiresAt" timestamp with time zone NOT NULL,
    "TwoFactorStatusId" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE auth."TwoFactorCodes" OWNER TO postgres;

--
-- Name: TwoFactorStatus; Type: TABLE; Schema: auth; Owner: postgres
--

CREATE TABLE auth."TwoFactorStatus" (
    "TwoFactorStatusId" integer NOT NULL,
    "Status" character varying(50) NOT NULL
);


ALTER TABLE auth."TwoFactorStatus" OWNER TO postgres;

--
-- Name: TwoFactorStatus_TwoFactorStatusId_seq; Type: SEQUENCE; Schema: auth; Owner: postgres
--

ALTER TABLE auth."TwoFactorStatus" ALTER COLUMN "TwoFactorStatusId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME auth."TwoFactorStatus_TwoFactorStatusId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: AccountTypes; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."AccountTypes" (
    "AccountTypeId" uuid NOT NULL,
    "Name" character varying(25) NOT NULL,
    "Abbre" character varying(9) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."AccountTypes" OWNER TO postgres;

--
-- Name: Accounts; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Accounts" (
    "AccountId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "AccountTypeId" uuid NOT NULL,
    "CurrencyId" uuid CONSTRAINT "Accounts_CurrecyId_not_null" NOT NULL,
    "BankId" uuid,
    "Name" text NOT NULL,
    "Description" character varying(150) NOT NULL,
    "Balance" double precision NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "InitialBalanceDate" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL
);


ALTER TABLE finances."Accounts" OWNER TO postgres;

--
-- Name: Banks; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Banks" (
    "BankId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Abbre" character varying(10) NOT NULL,
    "TransferFee" double precision NOT NULL,
    "ImgURL" text,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."Banks" OWNER TO postgres;

--
-- Name: Categories; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Categories" (
    "CategoryId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "ParentId" uuid,
    "NatureId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" character varying(150) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "isActive" boolean DEFAULT false CONSTRAINT "Categories_IsDeleted_not_null" NOT NULL,
    "isSalary" boolean DEFAULT false NOT NULL
);


ALTER TABLE finances."Categories" OWNER TO postgres;

--
-- Name: CategoryParams; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."CategoryParams" (
    "ParamId" uuid NOT NULL,
    "CategoryId" uuid NOT NULL,
    "ApplySalary" boolean NOT NULL,
    "ApplyPercentage" boolean NOT NULL,
    "ApplyAmount" boolean NOT NULL,
    "Value" numeric NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."CategoryParams" OWNER TO postgres;

--
-- Name: Currencies; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Currencies" (
    "CurrencyId" uuid CONSTRAINT "Currencies_CurrecyId_not_null" NOT NULL,
    "Name" text NOT NULL,
    "Code" character varying(3) NOT NULL,
    "Symbol" character varying(3) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."Currencies" OWNER TO postgres;

--
-- Name: Natures; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Natures" (
    "NatureId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Abbre" character varying(5) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."Natures" OWNER TO postgres;

--
-- Name: TransactionTypes; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."TransactionTypes" (
    "TransactionTypeId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "CurrentValue" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."TransactionTypes" OWNER TO postgres;

--
-- Name: Transactions; Type: TABLE; Schema: finances; Owner: postgres
--

CREATE TABLE finances."Transactions" (
    "TransactionId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "TransactionTypeId" uuid NOT NULL,
    "CategoryId" uuid,
    "AccountId" uuid,
    "Description" character varying(500) NOT NULL,
    "TransactionDate" timestamp with time zone NOT NULL,
    "EntryType" character varying(3) DEFAULT ''::character varying NOT NULL,
    "PreviousBalance" numeric(18,2) DEFAULT 0.0 NOT NULL,
    "Amount" numeric(18,2) DEFAULT 0.0 NOT NULL,
    "Balance" numeric(18,2) DEFAULT 0.0 NOT NULL,
    "TransferGroupID" uuid,
    "Reference" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE finances."Transactions" OWNER TO postgres;

--
-- Name: ExchangeRates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ExchangeRates" (
    "ExchangeId" uuid NOT NULL,
    "Date" timestamp with time zone NOT NULL,
    "Value" numeric(18,4) NOT NULL,
    "CurrencyFromId" uuid NOT NULL,
    "CurrencyToId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."ExchangeRates" OWNER TO postgres;

--
-- Name: IncomeTax; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."IncomeTax" (
    "Id" integer NOT NULL,
    "Min" double precision NOT NULL,
    "Max" double precision NOT NULL,
    "Percentage" double precision NOT NULL,
    "Base" double precision NOT NULL,
    "Excess" double precision NOT NULL
);


ALTER TABLE public."IncomeTax" OWNER TO postgres;

--
-- Name: IncomeTax_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."IncomeTax" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."IncomeTax_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: users; Owner: postgres
--

CREATE TABLE users."Users" (
    "UserId" uuid CONSTRAINT "Users_UserID_not_null" NOT NULL,
    "Email" character varying(150) NOT NULL,
    "Username" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "FullName" text NOT NULL,
    "IsActive" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "HiresDate" timestamp with time zone
);


ALTER TABLE users."Users" OWNER TO postgres;

--
-- Data for Name: RefreshTokens; Type: TABLE DATA; Schema: auth; Owner: postgres
--

COPY auth."RefreshTokens" ("RefreshTokenId", "UserId", "Token", "ExpiresAt", "Revoked", "CreatedAt") FROM stdin;
41136790-6390-4ef6-a7f3-363f8e507a1e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	RqOVnSPQ3R/mWw33khvH8+eDboipZCCunb5OIV+YO1wdfMcdJGZoEfrrNIJ3N4T2pmdIzpuc9mLVtGPO862Ylg==	2026-03-23 15:06:41.700616-06	f	2026-03-16 15:06:41.700742-06
d58fa966-f033-489b-aee7-cc1be9ea8c56	c502b17e-a322-4df7-ad25-75a11bc2ac2d	CWOLNEYSgbTxVDUljh7J0F/IWpRiLoBkhuwNpM89vvQ5e8/3tewo3pp+8YYk8KioCte+yo1sNkWucnK0uUVkbQ==	2026-03-23 15:38:54.153962-06	f	2026-03-16 15:38:54.154061-06
6182c11c-84cd-4cce-b162-0991666c4d09	c502b17e-a322-4df7-ad25-75a11bc2ac2d	szchci1EXCF76RXslwN+GMEWGc/SJhp6qjm5fKU4l4Bg7vfMXOo50btuM12m/Vtd2RCzdp6McUtvOfj0HE9xoA==	2026-03-23 20:48:54.537684-06	f	2026-03-16 20:48:54.537789-06
900e97b6-ce39-41cf-9ea7-5cc4a371c1a7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	6Tz6mjmv/PHhOPVKXAYbNF6ph8kd7sYcu8M1TtiqZp9TrG6mopiyIgwf5OvKdZ/sa6QcbNA8l/H4GyMSp67+tA==	2026-03-28 08:49:53.042575-06	f	2026-03-21 08:49:53.042699-06
586c3648-3aa3-4545-a74f-fcb17bcbfaa7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	p23Kz4yUNxu+N1hQ3zCGEz/Sbq8JaI8XE83z/MdCwartDiBr7bC+JDLMojG9vG9TdOfgZ8xGnE8sLaR8fMNzeg==	2026-03-30 10:59:40.486641-06	f	2026-03-23 10:59:40.486743-06
fd09eef6-27c7-4c33-beb5-61776a8cad70	c502b17e-a322-4df7-ad25-75a11bc2ac2d	aCjM/tO3TAiN1RHaXH2g+CdG+fUoNDavUEovT2p7aPFMt7Vl6Sqb5hcpA8B9DAmNNcymFpKeleVFa5/OFhkGOA==	2026-04-06 08:35:23.505285-06	f	2026-03-30 08:35:23.505384-06
8664f959-661e-4530-9449-ea69bb00efe7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	SWkD0nK2AIDbzOGAyriBYngV/VgAbJkcbHVxIhDiU/JHDxsC5/NEcSeZkOjhLZLmz5ogsbrykkVlNotm23eXXg==	2026-04-08 14:05:44.521167-06	f	2026-04-01 14:05:44.521241-06
ba0394b1-3e51-4643-a223-dcafeff51366	c502b17e-a322-4df7-ad25-75a11bc2ac2d	nEkGHnT3P11FmYjXBf4GGScH5WHoONobdWUiBUZ381UwhYjAk989piEDaQrGGa6L3ba3pFYCSw7Rvi6d7ccQ6A==	2026-04-27 12:59:25.943596-06	f	2026-04-20 12:59:25.943692-06
\.


--
-- Data for Name: TwoFactorCodes; Type: TABLE DATA; Schema: auth; Owner: postgres
--

COPY auth."TwoFactorCodes" ("TwoFactorCodeId", "UserId", "Code", "ExpiresAt", "TwoFactorStatusId", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: TwoFactorStatus; Type: TABLE DATA; Schema: auth; Owner: postgres
--

COPY auth."TwoFactorStatus" ("TwoFactorStatusId", "Status") FROM stdin;
1	Active
2	Used
3	Expired
4	Replaced
\.


--
-- Data for Name: AccountTypes; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."AccountTypes" ("AccountTypeId", "Name", "Abbre", "CreatedAt") FROM stdin;
39910334-952d-47df-8660-aaebda6d8ab2	Investment	TYPE-INVS	2026-03-16 15:38:06.072517-06
4712c41c-b0be-4bc3-837c-764510b97344	Cash	TYPE-CASH	2026-03-16 15:38:06.072516-06
a7206696-4fc5-41b3-8964-ff8dd9526ac5	Debit	TYPE-DEBT	2026-03-16 15:38:06.072517-06
cc7b3f5f-0241-4541-b881-d525ef0d3bb6	Savings	TYPE-SAVS	2026-03-16 15:38:06.072517-06
\.


--
-- Data for Name: Accounts; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Accounts" ("AccountId", "UserId", "AccountTypeId", "CurrencyId", "BankId", "Name", "Description", "Balance", "CreatedAt", "InitialBalanceDate") FROM stdin;
fac07685-5147-4172-a02c-3f6a1834d765	c502b17e-a322-4df7-ad25-75a11bc2ac2d	cc7b3f5f-0241-4541-b881-d525ef0d3bb6	691ce7b8-cf85-43e5-a878-185c8bbb50f7	f8413551-d55b-4455-8556-0e2aceddbe5f	Cuenta Ahorro Bac	Cuenta de ahorro BAC en dolares	7840	2026-03-20 15:09:21.35672-06	2026-03-31 23:59:59.35672-06
f93559ad-545e-48ad-bce3-c10d150f398e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a7206696-4fc5-41b3-8964-ff8dd9526ac5	385e35f0-87c3-4182-b514-12dcd0eb3b1a	bdcabf75-693b-4f9e-ac67-e363d8f38230	Cuenta Débito Banpro	cuenta de debito banpro donde recibo el pago	1500	2026-03-20 15:11:33.051787-06	2026-03-31 23:59:59.35672-06
c3cbdd17-55c9-4962-98da-ed267746fdfc	c502b17e-a322-4df7-ad25-75a11bc2ac2d	39910334-952d-47df-8660-aaebda6d8ab2	691ce7b8-cf85-43e5-a878-185c8bbb50f7	f8413551-d55b-4455-8556-0e2aceddbe5f	Cuenta Inversión SAFI	Safi instrumento de inversión de INVERCASA	1200	2026-03-21 08:44:17.061105-06	2026-03-31 23:59:59.35672-06
b04beaee-598a-4236-97f9-d8fedd6ba06e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	4712c41c-b0be-4bc3-837c-764510b97344	385e35f0-87c3-4182-b514-12dcd0eb3b1a	\N	Efectivo	Guarda el efectivo en cordobas	0	2026-03-21 08:21:38.856101-06	2026-03-31 23:59:59.35672-06
\.


--
-- Data for Name: Banks; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Banks" ("BankId", "Name", "Abbre", "TransferFee", "ImgURL", "CreatedAt") FROM stdin;
f8413551-d55b-4455-8556-0e2aceddbe5f	BANCO DE AMERICA	BAC	2	https://cdn.brandfetch.io/idtShdffQm/theme/dark/symbol.svg?c=1dxbfHSJFAPEGdCLU4o5B	2026-03-16 15:38:06.072458-06
bdcabf75-693b-4f9e-ac67-e363d8f38230	BANCO DE LA PRODUCCION	BANPRO	2	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHwWMwWrTnldUDb-xandtCLT03TNPi4Fps5Q&s	2026-03-16 15:38:06.072459-06
e40808d2-1f58-4c8f-81fd-ce02c36ed3e8	BANCO DE FINANZAS	BDF	2	https://is1-ssl.mzstatic.com/image/thumb/Purple211/v4/2c/5d/32/2c5d324c-a23c-8ab5-b08c-2086926e2d07/TailoredAppIcon-0-0-1x_U007emarketing-0-11-0-0-85-220.png/200x200ia-75.webp	2026-03-17 16:01:00.514101-06
7080a9b5-467b-4908-8347-51eed81a6705	BANCO LAFISE BANCENTRO	LAFISE	2	https://virtualbanking.lafise.com/api/image?_file=image&_id=5f873d4f1d8cf738e1d36b81	2026-03-19 11:21:51.121063-06
\.


--
-- Data for Name: Categories; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Categories" ("CategoryId", "UserId", "ParentId", "NatureId", "Name", "Description", "CreatedAt", "isActive", "isSalary") FROM stdin;
20bc2efe-cc23-4f02-b39e-f3ed28b38d85	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Salario	Salario recibido por ISSDHU, incluye todas las percepciones recibida	2026-03-27 13:14:28.997381-06	t	f
8104ef3f-b6b7-4ddd-a500-6f7084c8deb7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Inversiones	Inversiones asociadas	2026-03-27 13:15:14.242358-06	t	f
51508bb9-4653-4cc6-8403-3d7a39dcd721	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Otros ingreso	otros ingresos asociadas	2026-03-27 13:15:39.516863-06	t	f
93d03768-d0fc-4ae9-b08a-9db9e78a7df7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Gasto Esenciales	gastos esenciales 	2026-03-27 13:16:09.319478-06	t	f
5d30c170-848d-49de-953f-e2e40dabcddb	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Gastos Fijos	Gastos que siempre debo de pagar	2026-03-27 13:16:37.367796-06	t	f
7a9db0d0-88cb-4226-9f91-1d70e9d965b3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	\N	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Gastos Discrecional	Gastos menos oportunos, fácil de vivir sin ellos	2026-03-27 13:17:05.022458-06	t	f
e14c7f31-ce54-47d1-a32d-33f1165b6856	c502b17e-a322-4df7-ad25-75a11bc2ac2d	20bc2efe-cc23-4f02-b39e-f3ed28b38d85	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Salario Básico	Salario Recibido Mes a mes 	2026-03-27 13:19:55.577904-06	t	t
0d42b486-18da-471c-8c7b-692f74f30555	c502b17e-a322-4df7-ad25-75a11bc2ac2d	20bc2efe-cc23-4f02-b39e-f3ed28b38d85	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Años de servicios	Antiguedad aplicada al salario, 1% cada mes	2026-03-27 13:21:45.3623-06	t	f
9c39577e-2f6b-486c-9812-d706a78bf0d0	c502b17e-a322-4df7-ad25-75a11bc2ac2d	20bc2efe-cc23-4f02-b39e-f3ed28b38d85	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Titulo	Porcentaje aplicado por titulo	2026-03-27 13:29:00.146428-06	t	f
a00474f6-47d9-41f6-9e25-a0a1f84c915a	c502b17e-a322-4df7-ad25-75a11bc2ac2d	20bc2efe-cc23-4f02-b39e-f3ed28b38d85	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Viático de alimentación	Viatico de alimentación como beneficio 	2026-03-27 13:29:58.474027-06	t	f
5ba1a410-d8ce-4efd-bf33-6df2a198954e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	8104ef3f-b6b7-4ddd-a500-6f7084c8deb7	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Dividendos Invercasa	Dividendo del 6%-7% de invercasa	2026-03-27 13:30:36.813414-06	t	f
bffa3bb4-4d75-4a65-aea3-0559820881c8	c502b17e-a322-4df7-ad25-75a11bc2ac2d	51508bb9-4653-4cc6-8403-3d7a39dcd721	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Freelance	Otros trabajos como servicios profesionales	2026-03-27 13:31:43.152667-06	t	f
320e457e-8ee5-4abc-8c77-d0b8b3f163d2	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Alimentación	Ayuda económica del hogar	2026-03-27 13:32:50.708397-06	t	f
5316985c-02f8-4d57-82e1-75a62792eb0b	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Servicio eléctrico	Pago de factura de luz	2026-03-27 13:33:15.818565-06	t	f
fcbccc97-0f6d-4a18-86f7-fb749367c18f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Tigo Hogar	Pago de servicio de internet	2026-03-27 13:33:30.730677-06	t	f
663c2f92-d66a-4d81-a7b3-2897b000f986	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Tigo pospago	Servicio de internet para el telefono	2026-03-27 13:33:55.472458-06	t	f
d55df5ab-1ccb-49b9-83d3-1e8861bf3cea	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Servicio de agua	Pago de factura de agua	2026-03-27 13:34:35.536797-06	t	f
ec7047a8-4fd7-43a1-8aef-d33c231494d9	c502b17e-a322-4df7-ad25-75a11bc2ac2d	93d03768-d0fc-4ae9-b08a-9db9e78a7df7	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Servicio Bus	pago de c$2.5 de transporte por cada bus que tome	2026-03-27 13:35:06.447948-06	t	f
36a815b9-8d2c-42ae-8d1c-8735526b2b4b	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Medicación	Pagos de medicamentos para en caso de enfermedad, protector solar	2026-03-27 13:35:35.25129-06	t	f
1b381bab-8598-4181-b16f-aab8717f1012	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Taxi	Pago de transporte de taxi	2026-03-27 13:36:02.634232-06	t	f
de6c64da-e8b5-4f5f-a49e-450a3bee0c82	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Mantenimiento	Pago de trabajos por reparación o servicios en el hogar	2026-03-27 13:36:42.156065-06	t	f
07cdc60a-eff8-41e4-948a-bb0788454c68	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	INSS	Pago de seguro médico	2026-03-27 13:37:07.032459-06	t	f
df61de18-71ce-4821-bfa9-fc9a1e8b20bb	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	IR	Pago de impuesto sobre la renta	2026-03-27 13:37:37.199433-06	t	f
12087df0-a82a-4c93-8a31-f262315f967f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Seguro colectivo	Pago de seguro médico de la empresa	2026-03-27 13:38:01.513433-06	t	f
39a2f36a-f411-4bfe-bbb5-7a853b1e1790	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Seguro de tarjeta de débito banpro	pago de seguro contra robo, \nmas bien el mismo banco me roba por pagarle	2026-03-27 13:38:47.905609-06	t	f
c9165469-07f1-4a1c-bb88-b25c64579bc6	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Seguro tarjeta de débito bac	pago de seguro contra fraudes	2026-03-27 13:39:10.753289-06	t	f
618cb7dc-cd33-4d49-9748-bd796224616d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Ayuda económica	pago de ayuda económica por si alguien me pide	2026-03-27 13:39:44.852006-06	t	f
f25b9310-d3f3-4fab-a818-cc5b45620209	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Préstamos	pagos de deudas 	2026-03-27 13:40:04.761916-06	t	f
03f90928-98d3-4f3a-82dd-58fa13e9b1f3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	5d30c170-848d-49de-953f-e2e40dabcddb	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Pago de lotes	pagos de créditos por lote	2026-03-27 13:40:29.144996-06	t	f
ea29bfdc-f19f-444e-8017-3ad953d71400	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Tarifa por transferencia	cada vez que hago transferencia interbancaria hay una tarija del 2%	2026-03-27 13:41:19.521049-06	t	f
ba94b2f3-1c56-49c1-9715-fbbdae850a58	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Provisiones	compras de provisiones personales	2026-03-27 13:41:44.251846-06	t	f
70e86cdc-48cf-4a36-a624-6d6847b3231f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Salida al cine	pago por salida y snack en el cine	2026-03-27 13:42:07.658388-06	t	f
81a8c0a3-0819-431a-a93c-1b95219192c2	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Salida a comer	cada vez que compro comida	2026-03-27 13:42:22.231883-06	t	f
579e6898-f9c1-4904-b998-053a2a4fdc56	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Apuesta	si, soy un ludopata	2026-03-27 13:42:34.387297-06	t	f
ea973850-0518-452e-9481-3c49e56df637	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Ropa	compra de ropa y calzado	2026-03-27 13:42:45.872185-06	t	f
fc96aadd-efe9-4a17-a815-28bcdc89bf1d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Libros	compra de libros	2026-03-27 13:42:54.951133-06	t	f
3cb9acb7-acd8-471d-82bf-0fdbb5727644	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Celebración	celebraciones en la que soy participe	2026-03-27 13:43:09.459175-06	t	f
fdc13cef-efe3-487f-9afd-e968e41f35ce	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Belleza	compra de cremas faciales, cosmeticos y belleza	2026-03-27 13:43:38.622088-06	t	f
0c309997-6de4-40db-9dbe-c6cf8e892b24	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Corte de pelo	servicio de corte de pelo	2026-03-27 13:43:52.236754-06	t	f
0fd85d8b-53b2-4e62-9029-6e6e2063a410	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Viajes	pagos de viajes y los gastos que hay en ello	2026-03-27 13:44:07.818332-06	t	f
8ce55b86-3e09-4801-8cfe-9d7f7667b94a	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Cursos	Pago por educación	2026-03-27 13:44:19.024961-06	t	f
d7140b13-0c25-48cb-87e2-3a8e6d668334	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Juegos	Compras de video juegos	2026-03-27 13:44:30.286336-06	t	f
75b2e938-e29a-4151-a2ff-cfeed518a767	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Suscripciones	Pago de suscripciones, como chatgpt, github copilot	2026-03-27 13:44:55.215825-06	t	f
4d5f5c0b-3559-4cde-8113-4c3f25d81326	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Muebles	Compra de muebles del hogar y oficina	2026-03-27 13:45:06.873734-06	t	f
6c84daf5-2e21-40a6-968b-72200b0014b7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Tecnologia	Compras de productos electrónicos	2026-03-27 13:45:22.515669-06	t	f
93dfa21a-0e50-485c-8ad8-edd996460ad0	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Zanganaderia	Prueba de sisema	2026-03-27 13:45:36.258712-06	t	f
902154d3-671f-4baa-9e3a-55630632ee48	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Otros servicios	otros servicios no contemplados	2026-03-27 13:45:59.624036-06	t	f
05ae7e67-2ecf-4364-b96d-8a93dd265daa	c502b17e-a322-4df7-ad25-75a11bc2ac2d	7a9db0d0-88cb-4226-9f91-1d70e9d965b3	129a7622-4437-42d1-8e16-0e9ce1a65e2e	Snacks	compra de chiverias 	2026-03-27 13:46:11.411103-06	t	f
c00ed0ed-2c5b-44a2-8b32-020016d586e6	c502b17e-a322-4df7-ad25-75a11bc2ac2d	8104ef3f-b6b7-4ddd-a500-6f7084c8deb7	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Interés Banpro	Dividendo miserable de banpro, del 2% anual	2026-03-27 13:32:20.204173-06	t	f
ec9cdb78-8009-4a72-a455-2e79dd9957d1	c502b17e-a322-4df7-ad25-75a11bc2ac2d	51508bb9-4653-4cc6-8403-3d7a39dcd721	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Viático alimentacion/transporte	Viático de alimentacion y transporte	2026-03-27 13:31:22.559145-06	t	f
e23fbe98-a09e-4684-a181-0cd56bbc581e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	51508bb9-4653-4cc6-8403-3d7a39dcd721	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Regalos	regalos monetarios	2026-03-27 13:47:33.41875-06	t	f
399faa1c-0530-48a8-8c25-1eb0640f4d87	c502b17e-a322-4df7-ad25-75a11bc2ac2d	8104ef3f-b6b7-4ddd-a500-6f7084c8deb7	f1e9c692-e70c-4e5c-92a2-6908fe750f39	Interés Bac	Dividendo miserable que te da bac del 2% anual	2026-03-27 13:31:03.924327-06	t	f
\.


--
-- Data for Name: CategoryParams; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."CategoryParams" ("ParamId", "CategoryId", "ApplySalary", "ApplyPercentage", "ApplyAmount", "Value", "CreatedAt") FROM stdin;
78aef122-7fab-4dd5-8d5e-04e0bad8adba	0d42b486-18da-471c-8c7b-692f74f30555	t	t	f	1	2026-03-27 13:21:45.362507-06
39150b5b-ebb6-4538-9701-371ec03168f5	a00474f6-47d9-41f6-9e25-a0a1f84c915a	t	f	t	2850	2026-03-27 13:29:58.474028-06
76e20c64-5a37-427b-b965-af23d9e3e21d	07cdc60a-eff8-41e4-948a-bb0788454c68	t	t	f	7	2026-03-27 13:37:07.032461-06
9babcfb4-3522-448b-a813-8b27b1e959bd	df61de18-71ce-4821-bfa9-fc9a1e8b20bb	t	f	f	0	2026-03-27 13:37:37.199434-06
baf9aded-5b1c-42f1-8e46-36251a842e9b	12087df0-a82a-4c93-8a31-f262315f967f	t	f	t	70	2026-03-27 13:38:01.513435-06
8fedae94-3832-4fee-910b-cbf74de767d5	9c39577e-2f6b-486c-9812-d706a78bf0d0	t	t	f	13	2026-03-27 13:29:00.147067-06
\.


--
-- Data for Name: Currencies; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Currencies" ("CurrencyId", "Name", "Code", "Symbol", "CreatedAt") FROM stdin;
11fafee3-280c-418b-8bc1-8387d49b1bd6	Euro	EUR	€	2026-03-16 15:38:06.072477-06
385e35f0-87c3-4182-b514-12dcd0eb3b1a	Cordoba Nicaraguense	NIO	C$	2026-03-16 15:38:06.072477-06
691ce7b8-cf85-43e5-a878-185c8bbb50f7	Dolar Estadounidense	USD	$	2026-03-16 15:38:06.072477-06
\.


--
-- Data for Name: Natures; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Natures" ("NatureId", "Name", "Abbre", "CreatedAt") FROM stdin;
129a7622-4437-42d1-8e16-0e9ce1a65e2e	Expenses	E	2026-03-16 15:38:06.072433-06
f1e9c692-e70c-4e5c-92a2-6908fe750f39	Income	I	2026-03-16 15:38:06.072432-06
\.


--
-- Data for Name: TransactionTypes; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."TransactionTypes" ("TransactionTypeId", "Name", "Code", "CurrentValue", "CreatedAt") FROM stdin;
00381474-72a5-4407-bdbb-1ee93ab80609	Transfers	TRF	7	2026-03-16 15:38:06.0725-06
a219b6f9-51ae-441a-a0ad-d98430da6991	First Move	FM	3	2026-03-16 15:38:06.072499-06
a819b6f9-51ae-441a-a0ad-d98430da6990	Income	INC	28	2026-03-16 15:38:06.072499-06
12ff4410-0c87-48c2-8c6e-005ab96ee155	Expenses	EXP	91	2026-03-16 15:38:06.072499-06
\.


--
-- Data for Name: Transactions; Type: TABLE DATA; Schema: finances; Owner: postgres
--

COPY finances."Transactions" ("TransactionId", "UserId", "TransactionTypeId", "CategoryId", "AccountId", "Description", "TransactionDate", "EntryType", "PreviousBalance", "Amount", "Balance", "TransferGroupID", "Reference", "CreatedAt") FROM stdin;
55871847-2743-47ba-b30f-d46fad1b1908	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	e14c7f31-ce54-47d1-a32d-33f1165b6856	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SALARIO MARZO 2026	2026-03-08 18:00:00-06	IN	-22690.70	34578.77	11888.07	\N	INC-000017	2026-04-25 20:23:10.77207-06
38a03d57-5104-4433-92df-fc46d1fb5b17	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	0d42b486-18da-471c-8c7b-692f74f30555	f93559ad-545e-48ad-bce3-c10d150f398e	BONIFICACION POR AÑO(9)	2026-03-08 18:00:00-06	IN	11888.07	3112.09	15000.16	\N	INC-000018	2026-04-25 20:23:10.77941-06
2d455407-a68a-4eda-886f-902e7bf31750	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	9c39577e-2f6b-486c-9812-d706a78bf0d0	f93559ad-545e-48ad-bce3-c10d150f398e	TITULO	2026-03-08 18:00:00-06	IN	15000.16	4495.24	19495.40	\N	INC-000019	2026-04-25 20:23:10.779425-06
bab181f3-258d-44bb-a930-1e402c4813be	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	a00474f6-47d9-41f6-9e25-a0a1f84c915a	f93559ad-545e-48ad-bce3-c10d150f398e	VIÁTICO DE ALIMENTACIÓN	2026-03-08 18:00:00-06	IN	19495.40	2850.00	22345.40	\N	INC-000020	2026-04-25 20:23:10.779428-06
67c1947d-d67d-4c8e-b125-b285a4fa1d55	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	07cdc60a-eff8-41e4-948a-bb0788454c68	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO INSS	2026-03-08 18:00:00-06	OUT	22345.40	2953.03	19392.37	\N	EXP-000051	2026-04-25 20:23:10.779638-06
43173da9-292f-465e-890f-485b602d4c6d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	93dfa21a-0e50-485c-8ad8-edd996460ad0	f93559ad-545e-48ad-bce3-c10d150f398e	ZANGANADERIA DIAMONT BLUE Y SGG	2026-03-12 18:00:00-06	OUT	11406.37	16110.00	-4703.63	\N	EXP-000059	2026-04-25 20:28:22.119483-06
dabbc268-e97c-4931-86a9-78b7e2b4df39	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	4d5f5c0b-3559-4cde-8113-4c3f25d81326	f93559ad-545e-48ad-bce3-c10d150f398e	NO RECUERDO QUE COMPRE	2026-03-26 18:00:00-06	OUT	-13283.63	285.00	-13568.63	\N	EXP-000064	2026-04-25 20:31:00.867173-06
448831be-80ed-46de-960b-aeeec2238662	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	75b2e938-e29a-4151-a2ff-cfeed518a767	f93559ad-545e-48ad-bce3-c10d150f398e	SUSCRPCIONES CHATGTP	2026-03-27 18:00:00-06	OUT	-13568.63	740.00	-14308.63	\N	EXP-000063	2026-04-25 20:30:29.008812-06
7b59d533-3dd7-4788-a085-f4d9e967ead7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	e14c7f31-ce54-47d1-a32d-33f1165b6856	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SALARIO ABRIL 2026	2026-04-08 18:00:00-06	IN	-15350.85	34576.70	19225.85	\N	INC-000023	2026-04-25 20:41:15.86512-06
a928777e-db73-492b-a08b-b0974eed9768	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	0d42b486-18da-471c-8c7b-692f74f30555	f93559ad-545e-48ad-bce3-c10d150f398e	BONIFICACION POR AÑO(9)	2026-04-08 18:00:00-06	IN	19225.85	3111.90	22337.75	\N	INC-000024	2026-04-25 20:41:15.873567-06
8bdcf9d0-4a3c-485f-84f1-bdebadbe3120	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	07cdc60a-eff8-41e4-948a-bb0788454c68	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO INSS	2026-04-08 18:00:00-06	OUT	29682.72	2952.85	26729.87	\N	EXP-000075	2026-04-25 20:41:15.873581-06
09886d7a-56b9-4c7a-b190-3e89712474e7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	36a815b9-8d2c-42ae-8d1c-8735526b2b4b	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO TE, PROTECTOR SOLAR, UNGUENTO	2026-04-10 18:00:00-06	OUT	17094.46	748.68	16345.78	\N	EXP-000079	2026-04-25 20:43:02.77453-06
91481b79-62db-4722-970e-6262cf8da067	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	de6c64da-e8b5-4f5f-a49e-450a3bee0c82	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO MANTENIMIENTO AC, CUERTO Y CUARTO DE MAMA	2026-04-11 18:00:00-06	OUT	16345.78	5809.00	10536.78	\N	EXP-000080	2026-04-25 20:43:34.277326-06
8693a1bc-deeb-4c9c-a8c9-b4bc1acfd3e4	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	5ba1a410-d8ce-4efd-bf33-6df2a198954e	fac07685-5147-4172-a02c-3f6a1834d765	DIVIDENDO INVERCASA MARZO 2026	2026-03-14 18:00:00-06	IN	8242.29	6.99	8249.28	\N	INC-000021	2026-04-25 20:32:19.723851-06
dbc25ba2-edf3-4842-9bac-3bee9fdeec39	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ec7047a8-4fd7-43a1-8aef-d33c231494d9	f93559ad-545e-48ad-bce3-c10d150f398e	BUS	2026-01-02 18:00:00-06	OUT	5370.76	150.00	5220.76	\N	EXP-000012	2026-04-25 19:23:52.600327-06
2b7687ef-db15-492f-ab61-d277452adbfc	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	0d42b486-18da-471c-8c7b-692f74f30555	f93559ad-545e-48ad-bce3-c10d150f398e	BONIFICACION POR AÑO(9)	2026-01-08 18:00:00-06	IN	39797.53	3111.91	42909.44	\N	INC-000006	2026-04-25 19:03:17.443372-06
482dd8a8-3727-4523-9172-5a4992282a1c	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	9c39577e-2f6b-486c-9812-d706a78bf0d0	f93559ad-545e-48ad-bce3-c10d150f398e	TITULO	2026-01-08 18:00:00-06	IN	42909.44	4494.98	47404.42	\N	INC-000007	2026-04-25 19:03:25.268997-06
0c32f2c5-aef8-4d97-aefa-681d13e1deee	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	df61de18-71ce-4821-bfa9-fc9a1e8b20bb	f93559ad-545e-48ad-bce3-c10d150f398e	IMPUESTO SOBRE LA RENTA	2026-01-08 18:00:00-06	OUT	47301.56	6266.03	41035.53	\N	EXP-000005	2026-04-25 19:03:59.556666-06
fa150449-914d-436c-b779-cea3aabe236f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ea973850-0518-452e-9481-3c49e56df637	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE CAMISAS BLANCAS	2026-01-12 18:00:00-06	OUT	28655.53	420.00	28235.53	\N	EXP-000021	2026-04-25 19:34:30.208985-06
e595b121-df86-443c-801c-5bf3fc54e91e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	ec9cdb78-8009-4a72-a455-2e79dd9957d1	f93559ad-545e-48ad-bce3-c10d150f398e	VIATICO DE TRANSPORTE ENERO 2026	2026-01-14 18:00:00-06	IN	28235.53	220.00	28455.53	\N	INC-000010	2026-04-25 19:18:27.356543-06
22564daf-af9a-4140-bdcc-7e647cc8c445	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ba94b2f3-1c56-49c1-9715-fbbdae850a58	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE PROVICIONES PARA USO PERSONAL	2026-01-14 18:00:00-06	OUT	28455.53	1054.00	27401.53	\N	EXP-000017	2026-04-25 19:31:50.759732-06
ff3ac976-44a0-46be-a48f-1dfe6441cd2e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	12087df0-a82a-4c93-8a31-f262315f967f	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO COLECTIVO	2026-04-08 18:00:00-06	OUT	20463.86	70.00	20393.86	\N	EXP-000077	2026-04-25 20:41:15.876328-06
ff0251b4-0b2a-4cd4-9b9b-8b2fecb301a2	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fcbccc97-0f6d-4a18-86f7-fb749367c18f	f93559ad-545e-48ad-bce3-c10d150f398e	TIGO HOGAR ABRIL 2026	2026-04-09 18:00:00-06	OUT	20393.86	1649.70	18744.16	\N	EXP-000067	2026-04-25 20:37:15.673152-06
77c70b78-4f2a-42bd-a0cc-dfd2f4fcd0e7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fcbccc97-0f6d-4a18-86f7-fb749367c18f	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO INTERNET ABRIL 2026	2026-04-09 18:00:00-06	OUT	18744.16	1649.70	17094.46	\N	EXP-000078	2026-04-25 20:42:16.931293-06
751bce5d-0248-468f-b223-560b9e973403	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	4d5f5c0b-3559-4cde-8113-4c3f25d81326	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE NO SE QUE	2026-04-17 18:00:00-06	OUT	1686.78	1300.00	386.78	\N	EXP-000085	2026-04-25 20:46:24.64778-06
017b689c-b305-4a5b-a8a1-e09804a0bc7f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	75b2e938-e29a-4151-a2ff-cfeed518a767	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SUSCRIPCION CHAT GTP	2026-04-17 18:00:00-06	OUT	386.78	740.00	-353.22	\N	EXP-000086	2026-04-25 20:46:58.622831-06
5fe4e6c9-9e74-4207-acc2-2577285543db	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ba94b2f3-1c56-49c1-9715-fbbdae850a58	b04beaee-598a-4236-97f9-d8fedd6ba06e	COMPRA DE PROVICIONES USO PERSONAL	2026-04-14 18:00:00-06	OUT	-11051.69	635.00	-11686.69	\N	EXP-000070	2026-04-25 20:38:38.172484-06
f1ede256-8767-4a6f-aadd-63aaf64a9b28	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	1b381bab-8598-4181-b16f-aab8717f1012	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE TAXI	2026-02-24 18:00:00-06	OUT	859.81	1600.00	-740.19	\N	EXP-000037	2026-04-25 20:10:41.682328-06
3279265b-5ef5-43ea-8879-a8b3782a5a0f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	81a8c0a3-0819-431a-a93c-1b95219192c2	b04beaee-598a-4236-97f9-d8fedd6ba06e	ALMUERZIS	2026-02-24 18:00:00-06	OUT	-740.19	680.00	-1420.19	\N	EXP-000039	2026-04-25 20:11:57.302947-06
02a4ed87-7039-44ff-bcec-c7170828080d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	320e457e-8ee5-4abc-8c77-d0b8b3f163d2	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE ALIMENTACION FEBRERO 2026	2026-02-05 18:00:00-06	OUT	9739.81	5200.00	4539.81	\N	EXP-000026	2026-04-25 20:01:17.861971-06
9c3c20df-111c-4b49-a0dd-763e723e4c25	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	81a8c0a3-0819-431a-a93c-1b95219192c2	f93559ad-545e-48ad-bce3-c10d150f398e	ALMUERZO	2026-01-12 18:00:00-06	OUT	29495.53	840.00	28655.53	\N	EXP-000018	2026-04-25 19:32:38.61753-06
b9734c95-fb4c-45f7-a66b-68658e5e0ddd	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	36a815b9-8d2c-42ae-8d1c-8735526b2b4b	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE PROTECTOR SOLAR	2026-02-10 18:00:00-06	OUT	19028.57	1000.00	18028.57	\N	EXP-000033	2026-04-25 20:08:25.899793-06
2d221c66-bd49-450b-ae16-d5fb29b4ce0c	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	de6c64da-e8b5-4f5f-a49e-450a3bee0c82	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE LIMPIEZA	2026-03-22 18:00:00-06	OUT	-11803.63	1480.00	-13283.63	\N	EXP-000056	2026-04-25 20:25:43.350864-06
d669830b-c2ce-45a3-80f3-1bad48871c24	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	SAQUE EN EFECTIVO PARA PODER PAGAR ALGO EN ABRIL 2026	2026-03-31 20:34:47.171387-06	OUT	-14308.63	1000.00	-15308.63	d513546f-a43e-427a-874a-288b008eb393	TRF-000004	2026-04-25 20:34:47.171387-06
bfc66a9b-09ca-4f08-a314-69ad1bea46c4	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	39a2f36a-f411-4bfe-bbb5-7a853b1e1790	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE SERVICIO CONTRA ROBO	2026-04-03 18:00:00-06	OUT	-15308.63	42.22	-15350.85	\N	EXP-000082	2026-04-25 20:44:27.50421-06
749707fb-471e-4910-81c4-d4b7b6398c80	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	9c39577e-2f6b-486c-9812-d706a78bf0d0	f93559ad-545e-48ad-bce3-c10d150f398e	TITULO	2026-04-08 18:00:00-06	IN	22337.75	4494.97	26832.72	\N	INC-000025	2026-04-25 20:41:15.873576-06
03e1a2c9-41c6-4a72-90fc-28219069ce46	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	a00474f6-47d9-41f6-9e25-a0a1f84c915a	f93559ad-545e-48ad-bce3-c10d150f398e	VIÁTICO DE ALIMENTACIÓN	2026-04-08 18:00:00-06	IN	26832.72	2850.00	29682.72	\N	INC-000026	2026-04-25 20:41:15.873577-06
d5d16290-0bfa-4480-a996-f3028de2acc3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	618cb7dc-cd33-4d49-9748-bd796224616d	f93559ad-545e-48ad-bce3-c10d150f398e	PRESTAMOS A NINETH, $150	2026-01-15 18:00:00-06	OUT	27401.53	5550.00	21851.53	\N	EXP-000013	2026-04-25 19:24:57.440869-06
16100a5b-2385-4727-8f92-7c5812572ee3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	93dfa21a-0e50-485c-8ad8-edd996460ad0	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SERVICIO DE "CABLE"	2026-01-15 18:00:00-06	OUT	21851.53	11700.00	10151.53	\N	EXP-000020	2026-04-25 19:33:48.871671-06
e280671f-68b6-4985-874d-5b502ef12ce7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fdc13cef-efe3-487f-9afd-e968e41f35ce	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE JABONES AXEXIA	2026-01-22 18:00:00-06	OUT	9786.53	1550.00	8236.53	\N	EXP-000022	2026-04-25 19:35:03.835209-06
7432231a-7d3a-41fa-aeb4-a2d84f411d3d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	902154d3-671f-4baa-9e3a-55630632ee48	f93559ad-545e-48ad-bce3-c10d150f398e	NO RECUERDO QUE FUE	2026-01-29 18:00:00-06	OUT	4483.53	1370.00	3113.53	\N	EXP-000025	2026-04-25 19:54:15.322129-06
a5bd4208-3800-458a-944f-bc8bdc417ca8	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	GUARDAR EL EFECTIVO PARA PAGOS DEL MES DE FEBEROR	2026-01-31 19:56:51.569124-06	OUT	3113.53	8649.54	-5536.01	93c2b290-64f7-45d0-bc73-a66c38cf04b7	TRF-000001	2026-04-25 19:56:51.569125-06
92437815-d189-4e91-b9c1-e3c5e137ad30	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ea29bfdc-f19f-444e-8017-3ad953d71400	f93559ad-545e-48ad-bce3-c10d150f398e	TRANSFERI A OTRA CUENTA BAC PARA PAGO DE MANTENIMIENTO	2026-02-18 18:00:00-06	OUT	9683.71	74.00	9609.71	\N	EXP-000058	2026-04-25 20:26:58.27792-06
498084af-a9fe-4be8-be65-6424a278cacd	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	RETIRO PARA PAGOS DEL MES DE MARZO	2026-02-28 20:17:17.98775-06	OUT	-13927.70	8649.00	-22576.70	e927b180-817c-41be-b378-173c554286e4	TRF-000003	2026-04-25 20:17:17.987751-06
20c3f92b-b848-41da-b95b-56ae199dfdf4	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	39a2f36a-f411-4bfe-bbb5-7a853b1e1790	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SEGURO CONTRA ROBO	2026-03-01 18:00:00-06	OUT	-22576.70	114.00	-22690.70	\N	EXP-000057	2026-04-25 20:26:13.523431-06
e58d0d30-e7ab-43ce-b03e-1f844d333076	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	12087df0-a82a-4c93-8a31-f262315f967f	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO COLECTIVO	2026-03-08 18:00:00-06	OUT	13125.77	70.00	13055.77	\N	EXP-000053	2026-04-25 20:23:10.782937-06
2dbc4408-7831-4328-9f0b-9eaa832e458c	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	df61de18-71ce-4821-bfa9-fc9a1e8b20bb	f93559ad-545e-48ad-bce3-c10d150f398e	IMPUESTO SOBRE LA RENTA	2026-04-08 18:00:00-06	OUT	26729.87	6266.01	20463.86	\N	EXP-000076	2026-04-25 20:41:15.876324-06
1a4d7472-88c4-454c-8e55-f627b3a269f7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	5316985c-02f8-4d57-82e1-75a62792eb0b	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE SERVICIO ELECTRICO FEBRERO 2026	2026-02-05 18:00:00-06	OUT	4539.81	2000.00	2539.81	\N	EXP-000027	2026-04-25 20:01:55.533427-06
699f1261-8a3a-4228-b51c-f1ad71f0ec18	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	663c2f92-d66a-4d81-a7b3-2897b000f986	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO POSPAGO	2026-04-19 18:00:00-06	OUT	-11686.69	840.00	-12526.69	\N	EXP-000068	2026-04-25 20:37:44.282064-06
82e8885e-2e77-42d7-9be9-904225dbdce1	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ec7047a8-4fd7-43a1-8aef-d33c231494d9	b04beaee-598a-4236-97f9-d8fedd6ba06e	BUS	2026-03-30 18:00:00-06	OUT	-3661.69	150.00	-3811.69	\N	EXP-000047	2026-04-25 20:20:36.433602-06
59f1a872-4ed8-42f6-bfe0-1216ab779b90	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	c9165469-07f1-4a1c-bb88-b25c64579bc6	fac07685-5147-4172-a02c-3f6a1834d765	SEGURO TARJETA BAC	2026-04-03 18:00:00-06	OUT	8254.74	1.15	8253.59	\N	EXP-000088	2026-04-26 08:31:16.184089-06
215e6f1b-c420-4272-a95a-4767e9318d1d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	5ba1a410-d8ce-4efd-bf33-6df2a198954e	fac07685-5147-4172-a02c-3f6a1834d765	DIVIDENDO INVERCASA	2026-01-01 18:00:00-06	IN	0.00	6.60	6.60	\N	INC-000009	2026-04-25 19:17:39.914336-06
5b6c3f6a-a6db-4a05-9a5c-c396b524e2b1	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	5ba1a410-d8ce-4efd-bf33-6df2a198954e	fac07685-5147-4172-a02c-3f6a1834d765	PAGO DE INVERCASA	2026-04-13 18:00:00-06	IN	8253.59	6.99	8260.58	\N	INC-000027	2026-04-25 20:55:35.400371-06
458bb7b4-070b-48e4-9fc9-f61e5fc0a80d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	5ba1a410-d8ce-4efd-bf33-6df2a198954e	fac07685-5147-4172-a02c-3f6a1834d765	DIVIDENDO INVERCASA	2026-02-03 18:00:00-06	IN	8236.29	6.00	8242.29	\N	INC-000016	2026-04-25 20:15:27.079483-06
962b4bc6-916f-41ab-8be2-8fae21ce677d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	8ce55b86-3e09-4801-8cfe-9d7f7667b94a	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE CURSO DE CRIANZA DE TILAPIA BASICO	2026-03-13 18:00:00-06	OUT	-4703.63	1096.00	-5799.63	\N	EXP-000062	2026-04-25 20:29:59.188665-06
a585fccd-26a0-4907-8a9c-23e574cb1ec9	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	663c2f92-d66a-4d81-a7b3-2897b000f986	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO INTERNET TELEFONO MARZO 2026	2026-03-19 18:00:00-06	OUT	-9369.63	840.00	-10209.63	\N	EXP-000055	2026-04-25 20:25:00.062532-06
47381a0e-52d8-4e47-a02a-3f621591b866	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	0fd85d8b-53b2-4e62-9029-6e6e2063a410	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO PARA EL VIAJE A MOMBACHO EN VCN	2026-03-20 18:00:00-06	OUT	-10209.63	1594.00	-11803.63	\N	EXP-000061	2026-04-25 20:29:27.87728-06
651ac873-ad5e-4932-af24-b5345ed38500	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	399faa1c-0530-48a8-8c25-1eb0640f4d87	fac07685-5147-4172-a02c-3f6a1834d765	INTERES BAC	2026-03-14 18:00:00-06	IN	8249.28	5.46	8254.74	\N	INC-000022	2026-04-25 20:32:44.120431-06
4f565fc1-dcf9-49e9-aa70-31a7b7350b6d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	05ae7e67-2ecf-4364-b96d-8a93dd265daa	f93559ad-545e-48ad-bce3-c10d150f398e	ANTOJITOS	2026-01-20 18:00:00-06	OUT	10151.53	365.00	9786.53	\N	EXP-000019	2026-04-25 19:33:13.581252-06
d37642ad-06dc-46a1-a6e6-df30f98668e7	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	a00474f6-47d9-41f6-9e25-a0a1f84c915a	f93559ad-545e-48ad-bce3-c10d150f398e	VIÁTICO DE ALIMENTACIÓN	2026-02-08 18:00:00-06	IN	27956.83	2850.00	30806.83	\N	INC-000015	2026-04-25 20:07:28.571126-06
9dce9516-953b-4f86-8b58-49439eaf2b61	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	df61de18-71ce-4821-bfa9-fc9a1e8b20bb	f93559ad-545e-48ad-bce3-c10d150f398e	IMPUESTO SOBRE LA RENTA	2026-02-08 18:00:00-06	OUT	27853.89	6266.32	21587.57	\N	EXP-000031	2026-04-25 20:07:28.574231-06
c892afd4-4405-4202-93ec-3d86f6ce040f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	12087df0-a82a-4c93-8a31-f262315f967f	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO COLECTIVO	2026-02-08 18:00:00-06	OUT	21587.57	70.00	21517.57	\N	EXP-000032	2026-04-25 20:07:28.574239-06
aeee9c22-c476-4676-ad94-02e24b8fd7b5	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fcbccc97-0f6d-4a18-86f7-fb749367c18f	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SERVICIO DE INTERNET FEBRERO 2026	2026-02-10 18:00:00-06	OUT	21517.57	1649.00	19868.57	\N	EXP-000028	2026-04-25 20:04:17.605882-06
1bbdb73d-f823-4ffc-ba24-7d9a954950ed	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ba94b2f3-1c56-49c1-9715-fbbdae850a58	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE PROVICIONES PARA USO PERSONAL	2026-02-18 18:00:00-06	OUT	13588.57	2100.00	11488.57	\N	EXP-000038	2026-04-25 20:11:13.967598-06
65e5c84f-9aff-4fa6-97ae-afe5e30f7e96	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	4d5f5c0b-3559-4cde-8113-4c3f25d81326	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE MUEBLE PARA MI CUARTO	2026-02-18 18:00:00-06	OUT	11488.57	1804.86	9683.71	\N	EXP-000042	2026-04-25 20:13:43.512981-06
f02786c7-adcb-4151-ae5a-475505c86d89	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fcbccc97-0f6d-4a18-86f7-fb749367c18f	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO TIGO HOGAR MARZO 2026	2026-03-10 18:00:00-06	OUT	13055.77	1649.40	11406.37	\N	EXP-000054	2026-04-25 20:24:38.645105-06
bcc7659b-b999-499a-a916-cc625513b4cc	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	b04beaee-598a-4236-97f9-d8fedd6ba06e	PARA PAGOS DEL MES DE FEBRERO 2026	2026-01-31 20:00:27.136139-06	IN	0.00	9739.81	9739.81	ecf536e7-e3cf-4a27-82f8-737068cecdb8	TRF-000002	2026-04-25 20:00:27.13614-06
4ae84a65-13b9-4b6c-bd00-c9b75d0e9d95	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	1b381bab-8598-4181-b16f-aab8717f1012	b04beaee-598a-4236-97f9-d8fedd6ba06e	TAXI	2026-03-30 18:00:00-06	OUT	-3811.69	990.00	-4801.69	\N	EXP-000048	2026-04-25 20:21:09.017514-06
f70f007a-1db1-4180-bbc7-fadd9c1e0f24	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	b04beaee-598a-4236-97f9-d8fedd6ba06e	TRANSFERENCIA PARA TENER EFECTIVO PAGO MAYO 2026	2026-04-25 20:48:50.180148-06	IN	-17676.69	4100.00	-13576.69	6b5848d4-7fd3-491e-922e-a20530bd952d	TRF-000005	2026-04-25 20:48:50.180149-06
63fd24f6-c847-4356-a5c3-7b1208846b9a	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	6c84daf5-2e21-40a6-968b-72200b0014b7	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE LAPTOP DELL, PARA ADRIANA	2026-04-22 18:00:00-06	OUT	-14846.69	1500.00	-16346.69	\N	EXP-000087	2026-04-25 20:49:57.906186-06
066eecf6-dacd-4329-a66d-fe4dd0c28b3a	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	81a8c0a3-0819-431a-a93c-1b95219192c2	b04beaee-598a-4236-97f9-d8fedd6ba06e	ALMUERZOS	2026-04-19 18:00:00-06	OUT	-12526.69	2170.00	-14696.69	\N	EXP-000072	2026-04-25 20:39:33.128195-06
1b2f7b56-acf2-4300-9dff-1dd1b1ec2356	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	05ae7e67-2ecf-4364-b96d-8a93dd265daa	b04beaee-598a-4236-97f9-d8fedd6ba06e	ANTOJITOS	2026-04-24 18:00:00-06	OUT	-17236.69	440.00	-17676.69	\N	EXP-000073	2026-04-25 20:40:09.922286-06
87acd09e-1123-4d1b-b0d4-a24a0ed3deed	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	05ae7e67-2ecf-4364-b96d-8a93dd265daa	b04beaee-598a-4236-97f9-d8fedd6ba06e	ANTOJITOS	2026-02-17 18:00:00-06	OUT	2539.81	1680.00	859.81	\N	EXP-000040	2026-04-25 20:12:26.881684-06
ab71f0d7-38ac-4b22-b6f9-03c10b7855d9	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	320e457e-8ee5-4abc-8c77-d0b8b3f163d2	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE COMIDA MARZO 2026, INCLUYE EL TANQUE DE GAS Y TANQUE DE AGUA	2026-03-09 18:00:00-06	OUT	7078.81	6299.00	779.81	\N	EXP-000045	2026-04-25 20:19:38.619089-06
9d6073d9-7df2-41a3-84a1-348305cac659	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	b04beaee-598a-4236-97f9-d8fedd6ba06e	SAQUE EN EFECTIVO PARA PODER PAGAR ALGO EN ABRIL 2026	2026-03-31 20:34:47.171387-06	IN	-4801.69	1000.00	-3801.69	d513546f-a43e-427a-874a-288b008eb393	TRF-000004	2026-04-25 20:34:47.17139-06
ddcefc83-95ff-4693-a9f0-4ae7cb8e65ff	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	75b2e938-e29a-4151-a2ff-cfeed518a767	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE CHATGTP ENERO 2026	2026-01-28 18:00:00-06	OUT	6756.53	803.00	5953.53	\N	EXP-000024	2026-04-25 19:36:14.463236-06
7ae5a151-aebc-4205-b746-822f92393d2d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	1b381bab-8598-4181-b16f-aab8717f1012	f93559ad-545e-48ad-bce3-c10d150f398e	SERVICIO DE TAXI	2026-01-29 18:00:00-06	OUT	5953.53	1470.00	4483.53	\N	EXP-000016	2026-04-25 19:31:04.246693-06
4bcf7ee1-b902-44a5-8c13-9be3363af91f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	07cdc60a-eff8-41e4-948a-bb0788454c68	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO INSS	2026-02-08 18:00:00-06	OUT	30806.83	2952.94	27853.89	\N	EXP-000030	2026-04-25 20:07:28.571131-06
1970d5bd-a1ce-4327-bf63-84504ed11399	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	03f90928-98d3-4f3a-82dd-58fa13e9b1f3	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE LOTE FEBRERO 2026	2026-02-13 18:00:00-06	OUT	18028.57	4440.00	13588.57	\N	EXP-000035	2026-04-25 20:09:29.472066-06
3aa14c2f-17b4-49ac-8158-b4baef3079fe	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	df61de18-71ce-4821-bfa9-fc9a1e8b20bb	f93559ad-545e-48ad-bce3-c10d150f398e	IMPUESTO SOBRE LA RENTA	2026-03-08 18:00:00-06	OUT	19392.37	6266.60	13125.77	\N	EXP-000052	2026-04-25 20:23:10.782933-06
9ed92e89-9280-41b4-9390-3b7ea6e23032	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ea973850-0518-452e-9481-3c49e56df637	f93559ad-545e-48ad-bce3-c10d150f398e	ROPA A VALERIA	2026-03-18 18:00:00-06	OUT	-5799.63	3570.00	-9369.63	\N	EXP-000060	2026-04-25 20:28:58.84037-06
3ad39392-be49-4eb5-93ab-36f1c6ba8f3e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a219b6f9-51ae-441a-a0ad-d98430da6991	\N	c3cbdd17-55c9-4962-98da-ed267746fdfc	PRIMER MOVIMIENTO CON SECUENCIA: FM-000002	2026-01-01 18:38:54.975-06	IN	0.00	1200.00	1200.00	\N	FM-000002	2026-04-25 18:42:00.46103-06
e1bd0b64-2a6d-46a9-8dd3-9656910be7be	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a219b6f9-51ae-441a-a0ad-d98430da6991	\N	fac07685-5147-4172-a02c-3f6a1834d765	PRIMER MOVIMIENTO CON SECUENCIA: FM-000001	2026-01-01 18:38:54.975-06	IN	12.05	8224.24	8236.29	\N	FM-000001	2026-04-25 18:41:11.387659-06
11a9fc55-4c1b-4ac7-8aad-f8a1ee0b5d82	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	fcbccc97-0f6d-4a18-86f7-fb749367c18f	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO TIGO HOGAR ENERO 2026	2026-01-01 18:00:00-06	OUT	7903.00	1649.70	6253.30	\N	EXP-000009	2026-04-25 19:20:49.285863-06
73169035-79ab-436e-9464-90284e13c73e	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	0fd85d8b-53b2-4e62-9029-6e6e2063a410	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO ANTICIPADO PARA VIAJE	2026-01-26 18:00:00-06	OUT	8236.53	1480.00	6756.53	\N	EXP-000023	2026-04-25 19:35:46.039317-06
96f5c646-ffe2-4aaf-83d7-02735adccd4c	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	663c2f92-d66a-4d81-a7b3-2897b000f986	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SERVICIO DE INTERNET TELEFONO	2026-02-10 18:00:00-06	OUT	19868.57	840.00	19028.57	\N	EXP-000029	2026-04-25 20:04:50.926113-06
41802432-bcf4-4bad-a821-cf919bcade4f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	93dfa21a-0e50-485c-8ad8-edd996460ad0	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE SERVICIO DE SSG	2026-02-19 18:00:00-06	OUT	9609.71	21924.41	-12314.70	\N	EXP-000041	2026-04-25 20:13:06.687416-06
fd799b70-028e-4cb1-b560-faf18186aa33	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	d7140b13-0c25-48cb-87e2-3a8e6d668334	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE JUEGO WHITOUT SURVIVAL	2026-02-19 18:00:00-06	OUT	-12314.70	80.00	-12394.70	\N	EXP-000043	2026-04-25 20:14:07.827314-06
6cebf732-9592-440b-9059-9c3892edcae3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	6c84daf5-2e21-40a6-968b-72200b0014b7	f93559ad-545e-48ad-bce3-c10d150f398e	COMPRA DE MOUSE Y ADAPTADOR DE HDMI/TYPEC	2026-02-21 18:00:00-06	OUT	-12394.70	1533.00	-13927.70	\N	EXP-000044	2026-04-25 20:14:47.705422-06
74f328c5-8464-4126-ab11-c89199c5833c	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	93dfa21a-0e50-485c-8ad8-edd996460ad0	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO LITIH DIAMONT	2026-04-15 18:00:00-06	OUT	10536.78	4400.00	6136.78	\N	EXP-000084	2026-04-25 20:46:00.190175-06
c3423243-b81d-47df-8d0d-76454354d5f3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	03f90928-98d3-4f3a-82dd-58fa13e9b1f3	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE LOTE ABRIL 2026	2026-04-17 18:00:00-06	OUT	6136.78	4450.00	1686.78	\N	EXP-000081	2026-04-25 20:44:00.290926-06
d372307f-12bf-45d2-9582-70928108f2e4	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	320e457e-8ee5-4abc-8c77-d0b8b3f163d2	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO COMIDA ABRIL 2026	2026-04-09 18:00:00-06	OUT	-3801.69	5000.00	-8801.69	\N	EXP-000065	2026-04-25 20:36:28.102584-06
7cfb41c0-6db1-4b65-a104-dfa236ded3e2	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ec7047a8-4fd7-43a1-8aef-d33c231494d9	b04beaee-598a-4236-97f9-d8fedd6ba06e	BUS	2026-04-20 18:00:00-06	OUT	-14696.69	150.00	-14846.69	\N	EXP-000069	2026-04-20 20:38:02.050017-06
7bef3612-f74e-4f09-b38a-1f86252bd2e4	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	5316985c-02f8-4d57-82e1-75a62792eb0b	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO LUZ ABRIL 2026	2026-04-09 18:00:00-06	OUT	-8801.69	2000.00	-10801.69	\N	EXP-000066	2026-04-25 20:36:48.217182-06
6c4604f5-4aac-4658-afff-65445500ec29	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	0c309997-6de4-40db-9dbe-c6cf8e892b24	b04beaee-598a-4236-97f9-d8fedd6ba06e	CORTE DE PEZO	2026-04-10 18:00:00-06	OUT	-10801.69	250.00	-11051.69	\N	EXP-000074	2026-04-25 20:40:34.600107-06
cef00a8a-5f62-4848-a07e-fea7198dd192	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	fac07685-5147-4172-a02c-3f6a1834d765	TRANSFERENCIA. PARA NIVELAR LA CUENTA DE BANPRO, POR MIGRACION	2026-04-26 08:53:52.332453-06	OUT	8265.58	125.22	8140.36	ed6028c3-8d48-43f0-9f50-09ded6ebf74e	TRF-000006	2026-04-26 08:53:52.347627-06
bcad1169-63a1-4909-b036-4a117f3bd74f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	399faa1c-0530-48a8-8c25-1eb0640f4d87	fac07685-5147-4172-a02c-3f6a1834d765	PAGO INTENRES	2026-01-01 18:00:00-06	IN	6.60	5.45	12.05	\N	INC-000011	2026-04-25 19:52:08.694477-06
3663e8ee-2f71-4481-8786-48f1d6fea15b	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	fac07685-5147-4172-a02c-3f6a1834d765	DESEMBOLSO PARA NIVELAR LAS CUENTAS POR MIGRACION	2026-04-26 09:09:00.879803-06	OUT	8140.36	488.11	7652.25	de2e346e-bfa1-441c-acd2-c49dfc47fcb6	TRF-000007	2026-04-26 09:09:00.879813-06
6e4974c4-4f41-48bc-ae0d-74334c32bf6b	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	399faa1c-0530-48a8-8c25-1eb0640f4d87	fac07685-5147-4172-a02c-3f6a1834d765	PAGO DE INTEREES BAC	2026-04-14 18:00:00-06	IN	8260.58	5.00	8265.58	\N	INC-000028	2026-04-25 20:56:00.702192-06
4461a816-ef04-4eab-a6ac-5e51f1de4600	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	05ae7e67-2ecf-4364-b96d-8a93dd265daa	b04beaee-598a-4236-97f9-d8fedd6ba06e	ANTOJITOS 26/04/2026	2026-04-26 00:00:00-06	OUT	-13576.69	105.00	-13681.69	\N	EXP-000091	2026-04-27 09:49:48.381715-06
655e656d-1e99-4d55-a414-6956e998a9f3	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	b04beaee-598a-4236-97f9-d8fedd6ba06e	DESEMBOLSO PARA NIVELAR LAS CUENTAS POR MIGRACION	2026-04-29 09:09:00.879828-06	IN	-13681.69	17876.69	4195.00	de2e346e-bfa1-441c-acd2-c49dfc47fcb6	TRF-000007	2026-04-29 09:09:00.879828-06
1403e715-a53d-48e8-8cce-83a0813e72a5	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	1b381bab-8598-4181-b16f-aab8717f1012	b04beaee-598a-4236-97f9-d8fedd6ba06e	SERVICIO DE TAXI E INDRIVE	2026-04-23 18:50:00-06	OUT	-16346.69	890.00	-17236.69	\N	EXP-000071	2026-04-25 20:39:02.529609-06
ab77b2e7-7aa8-4854-ba70-37fae9b22e8a	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ec7047a8-4fd7-43a1-8aef-d33c231494d9	b04beaee-598a-4236-97f9-d8fedd6ba06e	SERVICIO DE BUS	2026-02-27 18:00:00-06	OUT	-1420.19	150.00	-1570.19	\N	EXP-000034	2026-04-25 20:08:56.503129-06
972bcf4d-2366-4c3f-b462-77cb3e9e2b72	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a219b6f9-51ae-441a-a0ad-d98430da6991	\N	f93559ad-545e-48ad-bce3-c10d150f398e	PRIMER MOVIMIENTO CON SECUENCIA: FM-000003	2025-12-31 18:38:54.975-06	IN	0.00	7903.00	7903.00	\N	FM-000003	2026-04-25 18:43:03.933922-06
a5de1d39-053b-47ab-8158-b8712608abc1	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	663c2f92-d66a-4d81-a7b3-2897b000f986	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO INTERNET TELEFONO ENERO 2026	2026-01-01 18:00:00-06	OUT	6253.30	839.99	5413.31	\N	EXP-000010	2026-04-25 19:21:54.743336-06
0c78b44b-47e5-4ea3-9b87-944b41b65d64	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	39a2f36a-f411-4bfe-bbb5-7a853b1e1790	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO CONTRA ROBO	2026-01-01 18:00:00-06	OUT	5413.31	42.55	5370.76	\N	EXP-000015	2026-04-25 19:29:53.150499-06
f7f37d35-231d-43a6-bcab-e0f3c3272e26	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	5316985c-02f8-4d57-82e1-75a62792eb0b	b04beaee-598a-4236-97f9-d8fedd6ba06e	PAGO DE SERVICIO ELECTRICO MARZO 2026	2026-03-09 18:00:00-06	OUT	779.81	2000.00	-1220.19	\N	EXP-000046	2026-04-25 20:20:06.206579-06
2d4998ea-7c10-4724-a8fa-6e8338c6fbf0	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	05ae7e67-2ecf-4364-b96d-8a93dd265daa	b04beaee-598a-4236-97f9-d8fedd6ba06e	ANTOJITOS	2026-03-14 18:00:00-06	OUT	-1220.19	1371.50	-2591.69	\N	EXP-000049	2026-04-25 20:21:56.009928-06
1f82668c-0f2a-401b-892a-155f57a599f0	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	81a8c0a3-0819-431a-a93c-1b95219192c2	b04beaee-598a-4236-97f9-d8fedd6ba06e	ALMUERZOS	2026-03-26 18:00:00-06	OUT	-2591.69	1070.00	-3661.69	\N	EXP-000050	2026-04-25 20:22:21.436836-06
eac7e879-7af1-4e08-9d8c-9bb8550615d0	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	PARA PAGOS DEL MES DE FEBRERO 2026	2026-01-31 20:00:27.136058-06	OUT	-5536.01	8649.54	-14185.55	ecf536e7-e3cf-4a27-82f8-737068cecdb8	TRF-000002	2026-04-25 20:00:27.136059-06
bdf64da7-e2f8-4fe0-b54d-3192df33b08d	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	39a2f36a-f411-4bfe-bbb5-7a853b1e1790	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO CONTRA FRAUDE TARJETA	2026-02-01 18:00:00-06	OUT	-14185.55	42.50	-14228.05	\N	EXP-000036	2026-04-25 20:10:11.24037-06
c75f8c1a-f9af-4c90-8727-e3cb9b0d0e80	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	e14c7f31-ce54-47d1-a32d-33f1165b6856	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO SALARIO FEBRERO 2026	2026-02-08 18:00:00-06	IN	-14228.05	34577.77	20349.72	\N	INC-000012	2026-04-25 20:07:28.562877-06
43380dc9-895e-4785-acc5-b4302aa866bd	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	0d42b486-18da-471c-8c7b-692f74f30555	f93559ad-545e-48ad-bce3-c10d150f398e	BONIFICACION POR AÑO(9)	2026-02-08 18:00:00-06	IN	20349.72	3112.00	23461.72	\N	INC-000013	2026-04-25 20:07:28.571113-06
5256e573-d9ba-45e9-a3a8-3bb30d384c44	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	9c39577e-2f6b-486c-9812-d706a78bf0d0	f93559ad-545e-48ad-bce3-c10d150f398e	TITULO	2026-02-08 18:00:00-06	IN	23461.72	4495.11	27956.83	\N	INC-000014	2026-04-25 20:07:28.571125-06
262b8926-7976-4fa8-82bc-f6316d6c20a6	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	b04beaee-598a-4236-97f9-d8fedd6ba06e	RETIRO PARA PAGOS DEL MES DE MARZO	2026-02-28 20:17:17.98775-06	IN	-1570.19	8649.00	7078.81	e927b180-817c-41be-b378-173c554286e4	TRF-000003	2026-04-25 20:17:17.987755-06
091602e7-1dea-4665-b231-4da50db7b4db	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	e14c7f31-ce54-47d1-a32d-33f1165b6856	f93559ad-545e-48ad-bce3-c10d150f398e	SALARIO ENERO 2026	2026-01-08 18:00:00-06	IN	5220.76	34576.77	39797.53	\N	INC-000005	2026-04-25 19:03:00.946557-06
5b68ea8b-aca0-4f26-a90a-195d27d82b96	c502b17e-a322-4df7-ad25-75a11bc2ac2d	a819b6f9-51ae-441a-a0ad-d98430da6990	a00474f6-47d9-41f6-9e25-a0a1f84c915a	f93559ad-545e-48ad-bce3-c10d150f398e	VIÁTICO DE ALIMENTACIÓN	2026-01-08 18:00:00-06	IN	47404.42	2850.00	50254.42	\N	INC-000008	2026-04-25 19:03:30.700012-06
691d7f62-1627-443d-816b-f7a37d13f623	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	07cdc60a-eff8-41e4-948a-bb0788454c68	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO INSS	2026-01-08 18:00:00-06	OUT	50254.42	2952.86	47301.56	\N	EXP-000004	2026-04-25 19:03:53.381168-06
d8297d0a-9409-478c-b434-06d409c0677f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	12087df0-a82a-4c93-8a31-f262315f967f	f93559ad-545e-48ad-bce3-c10d150f398e	SEGURO COLECTIVO	2026-01-08 18:00:00-06	OUT	41035.53	70.00	40965.53	\N	EXP-000006	2026-04-25 19:04:05.914899-06
86310eab-0fe9-4a4d-994f-bd3ca7252674	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	5316985c-02f8-4d57-82e1-75a62792eb0b	f93559ad-545e-48ad-bce3-c10d150f398e	SERVICIO ELECTRICO ENERO 2026	2026-01-08 18:00:00-06	OUT	40965.53	2000.00	38965.53	\N	EXP-000008	2026-04-25 19:19:57.384048-06
413e1e38-aafe-4ad6-a491-343dd6c0d55f	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	320e457e-8ee5-4abc-8c77-d0b8b3f163d2	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO DE ALIMENTACION ENERO 2026	2026-01-09 18:00:00-06	OUT	38965.53	5000.00	33965.53	\N	EXP-000007	2026-04-25 19:18:59.807728-06
546f08b4-eb8e-477f-aee7-65d8598e902b	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	03f90928-98d3-4f3a-82dd-58fa13e9b1f3	f93559ad-545e-48ad-bce3-c10d150f398e	PAGO LOTE ENERO 2026	2026-01-11 18:00:00-06	OUT	33965.53	4440.00	29525.53	\N	EXP-000014	2026-04-25 19:29:02.059847-06
f82c4fe6-9438-42cd-b5b0-3fe4945047f9	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	36a815b9-8d2c-42ae-8d1c-8735526b2b4b	f93559ad-545e-48ad-bce3-c10d150f398e	PASTILLA PARA LA GRIPE	2026-01-12 18:00:00-06	OUT	29525.53	30.00	29495.53	\N	EXP-000011	2026-04-25 19:23:21.571629-06
88bf97f5-3241-413a-93d6-2c65e1efee58	c502b17e-a322-4df7-ad25-75a11bc2ac2d	12ff4410-0c87-48c2-8c6e-005ab96ee155	ea29bfdc-f19f-444e-8017-3ad953d71400	f93559ad-545e-48ad-bce3-c10d150f398e	TARIFA POR TRANSFERENCIA, PAGO DE MANTENIMIENTO DE AC	2026-04-18 18:00:00-06	OUT	-353.22	77.00	-430.22	\N	EXP-000083	2026-04-25 20:45:13.16196-06
901a1be2-ca3d-4d4f-9bf0-8bb687aa6121	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	TRANSFERENCIA PARA TENER EFECTIVO PAGO MAYO 2026	2026-04-25 20:48:50.180142-06	OUT	-430.22	4100.00	-4530.22	6b5848d4-7fd3-491e-922e-a20530bd952d	TRF-000005	2026-04-25 20:48:50.180143-06
25536af1-c4e8-453a-879d-b0447b92a847	c502b17e-a322-4df7-ad25-75a11bc2ac2d	00381474-72a5-4407-bdbb-1ee93ab80609	\N	f93559ad-545e-48ad-bce3-c10d150f398e	TRANSFERENCIA. PARA NIVELAR LA CUENTA DE BANPRO, POR MIGRACION	2026-04-26 08:53:53.13217-06	IN	-4530.22	4586.02	55.80	ed6028c3-8d48-43f0-9f50-09ded6ebf74e	TRF-000006	2026-04-26 08:53:53.146444-06
\.


--
-- Data for Name: ExchangeRates; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ExchangeRates" ("ExchangeId", "Date", "Value", "CurrencyFromId", "CurrencyToId", "CreatedAt") FROM stdin;
571e8f91-9edd-4dc8-99c6-f23f813b44b6	2025-12-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:11.422111-06
f464b03f-2b0d-4906-abc0-4d2c2a84b76e	2026-01-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:14.351532-06
553496a3-057c-4676-88e1-a9a46c3910d7	2026-01-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:15.492054-06
1ae7d953-0b4f-4bcf-bcac-0f4185dff8bc	2026-01-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:16.806013-06
f4e750ae-5387-4da7-9a01-5ae71c4f8944	2026-01-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.795557-06
dcb51ea6-3d60-4e39-99b8-ffc61d2d9520	2026-01-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.801319-06
a5ea9d40-388f-473c-a673-64a19c97716e	2026-01-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.804952-06
a0994b8a-b182-4666-8ae9-7803b3be59dd	2026-01-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.809012-06
66fb92ba-85dc-4c33-b78d-86aa57865be5	2026-01-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.812709-06
da006bfe-9b22-4aef-b4d0-13910c11e396	2026-01-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.815711-06
51506d97-a72e-40ad-b22e-4a13eccfb86f	2026-01-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.818684-06
0e8dcb33-a77c-4c26-ad82-d26078f1eefd	2026-01-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.821498-06
9764cb00-0632-4d13-92c1-27b248259cfd	2026-01-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.824785-06
125942c6-a78f-47fd-af92-a06a57911603	2026-01-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.82897-06
2e408860-9641-4923-aa23-825d596b1613	2026-01-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.83161-06
5c7dc1db-6d11-48e5-9e12-eda3f24937aa	2026-01-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.834724-06
1f06cdc5-6d1b-4df7-bfc4-cdbc8522f44b	2026-01-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.838373-06
ce9a7835-5b2d-4d6c-993f-7bf1c422dcc8	2026-01-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.84248-06
79250dcb-add3-4a63-adf4-ad6479724bc0	2026-01-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.845304-06
84cbfa1f-bb15-4f0d-ae5f-07164bb3706e	2026-01-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.848692-06
d38f9e15-551f-4e21-83b1-00da769f9d24	2026-01-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.852912-06
0ae2ce00-127b-4a5a-ace7-08f6c767eb4f	2026-01-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.855484-06
c24caff6-0dd9-40a3-8dab-2f0b494200af	2026-01-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.85827-06
e08afb04-cb08-40d8-be5b-a90b214b4d55	2026-01-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.861109-06
31aa15ae-0315-4709-beb6-31c49c363226	2026-01-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.864097-06
152f9f13-f36c-4b6f-aca4-3eb2488d02f8	2026-01-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.867095-06
9bd11e71-b512-4a10-8a49-5c73df594bb9	2026-01-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.87179-06
523f8088-b81f-4718-8faf-c2a3de2f4c31	2026-01-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.877022-06
957c4952-2343-41b6-bd33-a280eb9d9a25	2026-01-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.879943-06
66db0f3f-11a8-4e6d-b79c-c654d399a6a9	2026-01-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.882585-06
de666432-376d-4e59-9688-502f77850907	2026-01-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.885321-06
e2adbf33-e034-4713-b097-67f940ad0b93	2026-01-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.888888-06
340fca65-993a-4bbb-8352-1147cf394260	2026-02-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.891802-06
d164e307-c06d-4fbc-b374-6db4cbc5b634	2026-02-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.894709-06
b8e672bb-97b4-4a69-a0c4-7359d39be8b8	2026-02-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.897336-06
e493f4eb-79c0-4dab-992b-4bc58fe4a598	2026-02-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.900395-06
806a47c4-ea64-4b40-8de1-5baad0e4589d	2026-02-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.905778-06
19adb7a1-8e64-4d31-8500-073d3fc18eef	2026-02-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.909242-06
b63312e5-a06e-4a74-8ccf-bca3f649b86a	2026-02-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.911809-06
3b57e947-0648-48ed-a04b-98125efd5a6f	2026-02-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.915251-06
92cf79f4-31a7-4e3c-9e99-bdc97639ee73	2026-02-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.917903-06
6de6dba7-254a-4e5f-8fb8-6390c7e0fa1c	2026-02-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.920547-06
e363d8f9-191f-40e3-ae7d-e35f26014c98	2026-02-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.92318-06
96812530-1ac1-4233-9439-be47303ac5c9	2026-02-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.926403-06
7c4dc1ba-24d6-4a1d-863d-ff5d41f79038	2026-02-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.929226-06
06b775fc-a923-4877-8e15-0dd0f5edb9ad	2026-02-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.932908-06
93671fcd-d54d-4b5d-8a8f-dec3122489a0	2026-02-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.938847-06
56f9e3c4-fb1c-47a4-afef-921e6d04e802	2026-02-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.941951-06
a6564aee-a42d-44eb-b832-8a2c9aff3006	2026-02-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.944689-06
67258fdb-290d-4627-97dd-0f7f87a82d2e	2026-02-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.947424-06
c81e82f3-d92c-4013-aa13-4b725bffea9a	2026-02-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.950209-06
c103bb7c-03f4-4a53-82dc-e8a88dd2a49d	2026-02-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.953178-06
c1756a84-8445-4793-8f2f-6181001235bc	2026-02-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.957285-06
d0b516f7-88dc-4d9d-81d8-0711d9a08493	2026-02-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.960089-06
f3f31949-406f-4f64-8fe4-ff9ab6555e6f	2026-02-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.963029-06
46a94ebd-9b8d-4621-b8c0-5ed42695f443	2026-02-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.966588-06
83bea61b-c934-4ed0-bf9b-9431bf6990d0	2026-02-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.971152-06
26763690-0d78-423a-b4d4-c4ad64bb10f9	2026-02-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.976833-06
d23cf578-0972-4895-831b-e5abc7b00ade	2026-02-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.979719-06
1cd282a8-3dea-457b-8bc9-d061f39fda2d	2026-02-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.982748-06
e9a935e5-c923-4a40-8f47-7d34627be936	2026-03-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.985787-06
28d01847-e6ee-48b1-9979-1d35363a9207	2026-03-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.988682-06
c7359de3-49ca-437e-96ee-095d66354290	2026-03-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.991259-06
8cb03430-6cda-4572-8d46-ff242a8ea5ea	2026-03-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.994134-06
aae2956b-d8f1-42a1-9545-4967f5f9a056	2026-03-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:19.997185-06
ec934a1d-c568-46fa-8b9d-2e85bfd636d6	2026-03-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.000057-06
b4663848-b4d2-4933-ab75-2afea7cb1abe	2026-03-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.006601-06
171e1d02-af2b-448e-800c-e15db5f36e66	2026-03-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.013354-06
8933aef0-8d67-4852-96d9-0d141c0148d5	2026-03-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.016954-06
7d0cf740-1253-4c05-873f-6c55ebb3dcfc	2026-03-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.019826-06
176b4b68-07a0-4cce-b03e-1a73335665c0	2026-03-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.022783-06
af4ab4ee-5f61-4cba-80e3-0943fb4dde36	2026-03-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.025718-06
82acdf8c-77d8-4f27-80a6-354444940d96	2026-03-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.028558-06
3afce136-c3d4-436f-947a-dfdc51d828d2	2026-03-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.031417-06
7126ff1a-dbd2-4781-ba51-aac49642dfb7	2026-03-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.03463-06
4459bf32-5154-46ce-bf37-68479c5a244a	2026-03-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.03764-06
3e662acf-5aad-48db-ba54-9d878c344582	2026-03-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.040802-06
afe2a916-2dbf-45f9-a907-1cc981a4ec93	2026-03-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.045621-06
94001468-49fd-4b85-8b50-ead1ed30af37	2026-03-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.049268-06
bf1b08a3-19dc-4d42-bc69-3b53988fafaa	2026-03-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.052208-06
a6afb7e4-3d76-4dc9-8cfe-e70206226468	2026-03-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.055221-06
f3c2703f-9dc6-44c3-be34-883542adde72	2026-03-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.058188-06
42c3c466-2c8b-4e7a-abc3-b764bca83f4e	2026-03-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.061524-06
8466006b-cc95-43d9-952b-3c71297de8ac	2026-03-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.064376-06
960eceaa-c1a2-4d1f-b60f-46c5b0dec911	2026-03-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.067302-06
2ef9a1cb-f120-4a52-ad5e-277ec53dd611	2026-03-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.07014-06
33ae6f02-87ef-48d1-9922-6fb1903909fa	2026-03-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.077619-06
a0c45479-a647-423f-b8ca-8b3c2d1dee61	2026-03-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.080828-06
5c9ace25-dde5-449b-90ec-b24eebf01c9e	2026-03-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.08374-06
641f124f-1926-4f64-a716-fe8f602d4c70	2026-03-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.086413-06
5937f5bc-d942-4bc0-829c-fdba1a7eb74f	2026-03-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.08947-06
c5ab6b01-c087-4b74-b4af-2b63910eacdd	2026-04-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.092497-06
5c0b9bc5-2895-457a-b27b-4d5b78b4c098	2026-04-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.09549-06
39924e47-5d22-4002-bbe5-f50c272dd53e	2026-04-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.09861-06
81c1b7e7-a288-4c04-a395-9287aefa9c0d	2026-04-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.102241-06
063abeaf-7337-43c0-82a2-372ee08de2d2	2026-04-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.109128-06
682d54ed-b4d7-42ff-bfdf-9a222f066236	2026-04-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.112447-06
c73df508-5209-4f52-a423-5593a795350e	2026-04-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.115945-06
5622394a-2d48-497b-82ae-6d105d71f11b	2026-04-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.119031-06
e63f3fbe-507d-43ce-a930-3c0837e05081	2026-04-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.12205-06
311e3572-0659-49cf-afca-eedc8a9a0b01	2026-04-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.125093-06
d814e14b-9dd4-4c8b-814d-13a1d70e3a26	2026-04-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.128956-06
f173eaa3-0f51-4d00-a039-c8e2e9bc9bd8	2026-04-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.132059-06
31409661-af81-44c5-a300-80825ecbfbcf	2026-04-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.135044-06
1af4deb4-1244-486a-a086-b0ee00628d23	2026-04-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.138553-06
d29e8202-1fd2-4698-a647-4281ded9ca32	2026-04-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.144291-06
73e0fd30-7093-4f7a-bcbe-0b945bec097a	2026-04-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.147339-06
181bd994-feef-41cd-9448-7c1b4adfac69	2026-04-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.153375-06
f3fb4e26-9b9a-4dd7-8500-81307db6efb6	2026-04-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.157826-06
ddc13fd4-145d-4d23-91a5-1a6f5e36362f	2026-04-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.160964-06
0a110c93-abea-4d3c-bf7a-7af9b6b4e11e	2026-04-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.164139-06
9ee80ed0-f900-4029-95cd-9eb70c598d68	2026-04-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.167364-06
25e20db0-4da4-4402-a2c7-bd756e9ceca8	2026-04-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.170913-06
b45871d1-4abb-4c8d-9db9-6ecff0a6f941	2026-04-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.176123-06
bde44600-bafe-436e-be75-274a0e1f6894	2026-04-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.179214-06
6708eb65-72f1-4020-b7a0-b08609f91f11	2026-04-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.182558-06
96d4fd45-cc70-473b-9897-d06a233a08eb	2026-04-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.185685-06
e051b3d0-bf2a-49c4-a827-a2f3b285f4be	2026-04-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.191694-06
814978f6-d7ba-4ff5-8f6f-d441cabf6117	2026-04-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.195477-06
bfd82069-cd3f-4c60-9842-baf2a677567a	2026-04-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.199155-06
31a22f53-caf1-4563-bd06-43447372adad	2026-04-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.2124-06
f6350300-4043-4d08-917d-879c722db75a	2026-05-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.276328-06
fa6dfac1-9ce9-4ed1-8a9f-8a8cfce2dfe3	2026-05-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.315707-06
0dc136cf-a960-4015-9155-5750d2d7925c	2026-05-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.319803-06
4fc55286-639f-43b5-b142-a818fca07d72	2026-05-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.32311-06
b7f79830-7bfc-4ec9-abc6-596b505bc4f7	2026-05-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.329545-06
1b729ae6-bdd2-4bb0-958d-6e5242d393b1	2026-05-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.332992-06
79f162b5-6e5a-40af-9c58-d8daa5c23995	2026-05-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.33708-06
32cd02a0-92ae-44d0-a46d-63960b12ac08	2026-05-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.343666-06
8f1a6efe-1b82-4e36-ab40-25c1ef857a86	2026-05-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.346847-06
e3a91e39-4023-48c5-81f9-85965285dd4c	2026-05-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.349998-06
9551be81-fb07-4d05-a3fb-d7e8b95c39ee	2026-05-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.353113-06
47d9b33d-faf8-4a42-b5cd-ed8b6b412d83	2026-05-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.356498-06
d2d50570-37f9-41bd-a11a-014929dc39d8	2026-05-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.359626-06
848694a7-5afc-48ec-a1f5-1365c4e4651e	2026-05-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.362806-06
d98f4da0-43af-49f9-a70d-7013f61b4a4f	2026-05-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.367854-06
6a13951e-7027-4ccf-a5c8-8f9758277678	2026-05-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.371529-06
c9c6005d-144b-41de-9575-984c59c8d37c	2026-05-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.377367-06
3e89a218-8e36-4cc6-bf5d-4c57b69227fe	2026-05-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.381006-06
ef5bbe9e-a995-4dd2-acfc-16618ad0e1c8	2026-05-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.384202-06
1458e8fd-e6e0-4661-a93a-bab4b81f9d2e	2026-05-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.387683-06
743b5c8c-d89f-45b3-b978-9c2d6f31f73c	2026-05-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.391183-06
db2ccd72-1072-46b5-bfb4-19988890527a	2026-05-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.395181-06
d02a433b-233c-4c5a-a85a-5bd95d37a9f4	2026-05-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.401648-06
c4de4a8c-f068-4f6c-8ba6-e2cf3dee9f56	2026-05-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.40508-06
aa8ab65d-b3df-4b5c-b042-4cda6cfade34	2026-05-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.412364-06
ed39155f-e2cd-48c8-8c41-4054cccc1f5d	2026-05-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.419373-06
e0b1c294-5620-4760-a307-e43fcedcabd3	2026-05-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.423052-06
b7d2c035-6afa-4f08-9669-2e0ae038e507	2026-05-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.426733-06
96469f2d-3c78-4079-a302-c89d08fb25fb	2026-05-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.43327-06
5c25c837-04f5-4b7c-af17-0c45f18422e2	2026-05-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.436851-06
14aa9b23-b23a-4f6b-83bf-e6a562f58238	2026-05-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.440368-06
d42e4b2b-53c5-4a7f-aac4-9624be1b1347	2026-06-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.445399-06
330a6511-c34a-461c-af2f-755bcba51c6f	2026-06-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.45212-06
ca11026c-d9a6-4df8-b270-21f9391626ee	2026-06-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.455617-06
eac5ecb8-620c-4a08-bf25-4189fc9a478c	2026-06-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.459266-06
673e6457-299f-4323-ba8f-60e126487fd4	2026-06-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.462895-06
2046ffbe-fe75-4e14-afbf-797a6c1f178a	2026-06-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.466377-06
cbd4987b-ba9b-48d3-8021-dab6674add48	2026-06-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.46994-06
ceadf474-1208-46aa-88b6-783a4b9049bf	2026-06-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.474442-06
0b1b0777-269b-4ca6-8add-db8e9812bc89	2026-06-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.480883-06
d8efb92e-b310-461e-bf4a-844bd8ddf665	2026-06-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.48522-06
d44aa134-b9f4-41f8-a5f6-f1254f8da779	2026-06-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.492116-06
8e2618d5-7a3a-4091-a665-6d5763a1c813	2026-06-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.49747-06
f7343a7e-31b7-43c2-90ed-ed096229065a	2026-06-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.501139-06
19ae2308-cfa3-42c9-9f4c-3b304bd5b11e	2026-06-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.504648-06
3ba7df15-6bf0-41d4-a8f4-ee6459a2f2a9	2026-06-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.508657-06
903d5376-82b0-4959-92ed-92fbad344aed	2026-06-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.512467-06
3753097e-0b1b-4b07-9cd1-0da0530819af	2026-06-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.51731-06
94bf4ff5-acdd-48ef-aa18-c20987c0600f	2026-06-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.521086-06
0fd14b31-d6cd-4ee0-9562-7a0ed3976bf8	2026-06-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.526476-06
99bd3df1-2192-479f-b4e0-25d17604d2b3	2026-06-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.530091-06
0cfd223c-b3f6-4b00-85e1-459f72e8a148	2026-06-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.533567-06
a57f3684-8482-4d04-8789-2d860e84aba8	2026-06-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.537249-06
88dbf902-96af-4470-87a6-f283aabab002	2026-06-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.541071-06
f9826127-0cfa-4a59-966c-821b54ec4ba8	2026-06-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.545768-06
0c96f67d-0499-4bf8-95fa-5d6393ed96e2	2026-06-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.549315-06
b7b2de9b-340f-40e1-94cb-3cd1abd485e2	2026-06-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.553083-06
8b414b45-cf69-4cf0-a3f9-6de1c36fa74f	2026-06-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.556798-06
a7c63589-b1d6-4ef0-867a-4d126b353143	2026-06-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.560003-06
4c5f55b7-b4d1-46a1-a6cd-2402d48ce2d8	2026-06-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.564989-06
2c678a8b-a4e1-434e-bad5-896fda08a897	2026-06-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.572108-06
2a3c6493-abde-4faa-b90c-e67d447c3e75	2026-07-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.575649-06
ce11888b-2540-429d-82bb-076087aa269c	2026-07-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.579303-06
d35dba96-cd1b-4413-97fa-d5b580505095	2026-07-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.582941-06
3663a1c7-3b73-486a-9ee1-c7afa70e2916	2026-07-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.586554-06
af22aad4-9021-45e5-a3be-967784c71f57	2026-07-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.590065-06
1e397942-3b65-461d-af6b-9ec42d5810ac	2026-07-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.594138-06
ebcd3c95-b071-4b25-ab28-291952501fd1	2026-07-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.597731-06
a0c6211e-7901-446a-b305-f4edf414c540	2026-07-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.601518-06
56350413-4df6-4a37-8fca-4cb18836b07a	2026-07-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.605341-06
de248469-333b-4936-acd7-30863ec3860f	2026-07-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.613649-06
6abb5bda-5329-4e23-95a5-b587a004b0f9	2026-07-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.61736-06
05cb37b3-d16c-41f9-a54f-741a2517f0f2	2026-07-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.62104-06
0efaa5a1-7afa-42bb-916e-a3669ad07063	2026-07-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.624593-06
f833c1b3-6528-44ca-97cd-16458e1f5d54	2026-07-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.628249-06
a4c5bb2a-ad4a-4227-94ab-7869fd825781	2026-07-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.632898-06
474bef84-529c-42fa-b12e-65fcb0d33dd8	2026-07-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.639451-06
e9468a8f-9aca-4094-9007-4d7e8582bea8	2026-07-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.643156-06
05dfd345-d6fa-4862-8bba-a333f15a0811	2026-07-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.647257-06
0f879d37-5028-48c1-8818-a2321d57b0c4	2026-07-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.653249-06
00e6af9e-39b7-40c3-b86c-c806662823b5	2026-07-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.657553-06
93d71641-0d17-4253-bf0b-94b494909036	2026-07-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.665575-06
bfe06e4b-29a2-4e2a-b930-0b9a85edcc97	2026-07-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.669045-06
fc6aaaa8-e215-4997-b402-61ab8efe4ea8	2026-07-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.672703-06
91b60df6-3f82-4659-8b1a-713ea19bd7c6	2026-07-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.676389-06
d9b2a9ef-3469-41e9-8d09-e94a39f7838b	2026-07-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.681524-06
510b07fd-fa5f-4da9-af20-10d76f328229	2026-07-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.685098-06
76247630-ed6a-4982-8497-0ad1e59e03c0	2026-07-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.68892-06
bbcee5fc-3874-4286-a043-d20f2e799d56	2026-07-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.692551-06
e718c4d2-1b93-4edd-81b3-143fc98d9062	2026-07-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.696242-06
29489442-71fb-4237-a4f6-e3ecc8a74c79	2026-07-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.700112-06
aab72ec5-aa3f-4c3c-b9a5-814e5cdbc270	2026-07-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.70733-06
2a83616c-4fb7-4d7a-8bcd-94ec1799a808	2026-08-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.711018-06
65c5483c-5e2b-439a-a0da-ebfd5e03d356	2026-08-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.716489-06
55295bba-466d-4c44-91b5-49803d624e22	2026-08-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.720113-06
c3ad1d5c-3bd3-46c1-8ca5-9ce1b05ce830	2026-08-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.723722-06
b62960b8-34b7-4595-8659-02e4c9b959b2	2026-08-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.727589-06
f458443d-12d4-4de2-8bed-8b94603c03e7	2026-08-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.733061-06
99bb20a1-01ab-4057-9178-34cdf7b6475d	2026-08-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.737452-06
114644fe-3aab-452a-88ac-4ec74bd09cc6	2026-08-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.742059-06
fde261b2-d7e0-4586-9ffe-8d2dc9fdb7ae	2026-08-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.747184-06
db2d6427-7598-4898-b41c-da9414e98bf7	2026-08-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.75129-06
5283101b-0332-449d-b0aa-5385c918174c	2026-08-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.756868-06
276774aa-667f-499d-b487-2b0cd0e6c62c	2026-08-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.762179-06
cb7cedab-7f62-44a8-99bf-615c8ab97418	2026-08-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.766028-06
29986b43-92a9-4981-83fe-77dd32e0d453	2026-08-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.770139-06
f02e085f-d022-460e-ab60-2dc35835c873	2026-08-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.77556-06
9f7ea996-c845-495d-9da5-92bbbc7a626a	2026-08-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.784489-06
6e1aa94e-34a1-47d5-90aa-64528d78c7a5	2026-08-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.791059-06
dd2fa0c2-c647-4810-a1cc-f92a91a9ec47	2026-08-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.797335-06
742aada1-1be0-431d-abf9-d9a85b4f39d4	2026-08-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.803161-06
f4522297-5cf1-4676-a594-e6f79dda402b	2026-08-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.810783-06
90cd02c5-ed26-4673-bdd5-eb553028a466	2026-08-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.82068-06
20cbc1dd-0e66-474c-a9d1-08a49e25c426	2026-08-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.827281-06
6545b930-128c-430d-b557-0673aeedbeb8	2026-08-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.833407-06
c9c17283-1618-4742-a87a-d71129866c24	2026-08-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.839539-06
47e8626f-4f5e-4ff6-a91b-7770e56c0c8a	2026-08-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.845054-06
e296d99d-ad67-4b82-9f9a-e87a5de43608	2026-08-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.851456-06
970807be-18e0-4836-903b-c416b49e36b9	2026-08-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.857127-06
48119d37-9b9d-4fe2-807c-750347fae5e9	2026-08-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.863588-06
9f40217d-0d3a-4b08-8efd-0726c649f5fd	2026-08-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.870192-06
917764f2-25bf-4b05-9103-db8fc1dc56e7	2026-08-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.876879-06
ae23c6ce-cad4-427d-8f07-11bc06eeaad8	2026-08-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.883157-06
6df17bd9-ff54-40e3-9436-0c9bc59d8192	2026-09-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.890122-06
2a989b45-99c1-49b4-8e15-08b3dc6e8c4d	2026-09-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.896735-06
74bac7e7-ab3a-4a5c-a590-8f7fed58abce	2026-09-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.903554-06
281e15cc-3f90-44ae-8b19-4ae8d8e24b03	2026-09-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.909369-06
92be8fa9-5314-48d5-8e75-27c759ae76bd	2026-09-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.913307-06
8f9e3664-10f5-4b2d-9ad7-7c25fa8aa720	2026-09-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.917613-06
8f4b80ef-ea6a-4200-8b0b-b36965fe15f1	2026-09-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.921591-06
a6038c42-96e6-46f9-8ae6-916ba425f7f8	2026-09-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.927702-06
85c3afcd-d999-457b-b4e0-284c5733e022	2026-09-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.933549-06
ef53bb16-6f57-42e6-9554-4965b900cdcc	2026-09-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.937831-06
4abe3ca8-4091-4c5f-a7ae-e4d05b7aec76	2026-09-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.942785-06
e7177c8a-abac-49db-92e4-7f42f37392cf	2026-09-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.951577-06
0d24bbcf-bcd1-44bd-be42-6feba418f4cc	2026-09-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.957482-06
86f8f865-6b2f-443a-a476-ac8032312909	2026-09-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.961557-06
439dcbdd-66c5-41e3-b63b-296197cc80f3	2026-09-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.965612-06
5bf15148-589b-4d86-80b1-cfe47119d475	2026-09-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.970155-06
ca33cdc0-6d9f-4005-ad0c-67172ee5c47e	2026-09-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.974471-06
1b4f5788-eb11-42d2-a0d5-367ad2923c0f	2026-09-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.978509-06
cf3165ca-b10a-4194-91d6-a35b3b4fbaf9	2026-09-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.98306-06
a87c9f8e-82be-4887-ace0-8c124dc07726	2026-09-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.989132-06
9756d196-0b5c-48fb-a7eb-583be42f823e	2026-09-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:20.993744-06
b06f7925-2374-41fa-bd0f-e8618d3b3c2d	2026-09-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.002522-06
bd92ef72-eaf9-434d-afab-1486a4748181	2026-09-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.006577-06
bd8caa6a-da27-4793-9bbf-147183bb59d1	2026-09-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.010933-06
928c4237-e05b-42c5-a2e2-13aab34ed053	2026-09-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.015176-06
ec25a7cd-a4ee-4d21-9c13-16ac996c227c	2026-09-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.020964-06
8291e8fe-c876-46f8-a1d6-138148ff26c5	2026-09-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.025349-06
88303d6d-6112-41c6-b50d-d8560c9de278	2026-09-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.029438-06
00e4ca59-afb0-490e-8e84-e5ded0d8b29e	2026-09-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.033611-06
66e74606-2a48-464b-81f9-5727fc496698	2026-09-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.03969-06
d9c95f8d-a5b5-4d7f-a75b-b1853a2832cc	2026-10-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.043876-06
ba36132e-aec1-4062-9d97-5337bd2265c9	2026-10-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.049303-06
df9daf06-fbe6-4d3c-a813-fba56d359adf	2026-10-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.055865-06
738d896b-6f77-445d-a596-8ea5ae80c77c	2026-10-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.060184-06
472c62e2-200c-42df-a1d0-4c82525006f6	2026-10-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.064402-06
991c73a1-d7a1-4457-b599-3005df868219	2026-10-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.068494-06
9f3fbce9-2637-424f-be82-a6c4163fa3d6	2026-10-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.07253-06
2f65596d-6cb2-4187-8cde-534b733ed5e7	2026-10-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.077582-06
3307a10b-07da-4b02-b88f-20a1b6cdaefa	2026-10-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.082045-06
cc160f43-61de-40d5-9235-f281321a56e5	2026-10-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.090995-06
dd0569e1-621e-45db-85ed-800d393344c4	2026-10-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.095823-06
a846fb89-c8f0-437c-8602-b6be3ce66ee2	2026-10-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.10296-06
398976d9-7c4c-4152-a71e-1b5ad0c78817	2026-10-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.110945-06
09fb41e7-b0a7-49a5-90df-baebd17361d5	2026-10-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.117175-06
e36d4c5a-b870-4e32-96fa-a2e8c8f19459	2026-10-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.123051-06
e96e0322-3d72-4db0-a3d8-d05ef38137cb	2026-10-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.127575-06
5413cfb1-2846-4d97-8abd-99b87e9a9e91	2026-10-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.133757-06
a37966e6-f49a-4fbd-adab-53d806c8f1f0	2026-10-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.140005-06
6931a53c-9db9-4a03-9d92-18b9b8930f1a	2026-10-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.144586-06
3fcd5913-b0dd-454f-a094-817ef3903e01	2026-10-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.151791-06
51ba0fef-715e-46eb-affa-2b6de21d22d7	2026-10-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.158786-06
1c5ef6f8-fc06-45bb-ad18-c1651a33bb73	2026-10-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.16437-06
050368ae-3ef2-4b5e-b3f6-6510e257f866	2026-10-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.172535-06
39035f43-7f7c-48d9-8dd5-72c5863b5f56	2026-10-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.183404-06
06fc1b98-c372-4f75-9e51-899615bee859	2026-10-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.187858-06
d30210b2-134f-4e9f-a5ed-fd74dd890527	2026-10-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.192346-06
b6b7cf75-cb99-4a4a-bc75-86b3db4186b8	2026-10-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.197171-06
3f5b0d10-340c-4ab7-abc6-7f9f548e3cda	2026-10-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.201882-06
395f777c-2e7b-4e60-9005-3983aedae4ce	2026-10-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.208441-06
ee1a8f24-685c-408a-8501-e917dfe285cf	2026-10-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.212963-06
40a5dd7c-9303-445c-972c-607a7aa4b49b	2026-10-31 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.219723-06
7249f049-6ea7-4e60-8245-506d015804e1	2026-11-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.227774-06
92c28f1f-afce-4556-bb92-47cab74b2a64	2026-11-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.236446-06
169ae459-cf5d-45f1-89a9-055a0a16062c	2026-11-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.24478-06
3605676b-ece1-48a1-8223-9af55fc4ce86	2026-11-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.252768-06
dbdf62b4-7c1f-4176-a008-08b03550304c	2026-11-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.257956-06
7f673616-913c-4f91-bf00-3d35f5a7c245	2026-11-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.263358-06
e856bcd2-57f9-4218-afa3-442841d97116	2026-11-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.272027-06
bf872222-6494-4e11-91e3-d5a2b9255942	2026-11-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.277742-06
b1a0861e-f296-4d0c-b4c5-a99fa4a62ba7	2026-11-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.282033-06
c393ad93-3d03-4faa-aa26-b909e1916968	2026-11-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.287134-06
b93610ad-b89b-4955-b1f0-abd89d2dc70b	2026-11-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.294183-06
6620fe61-af66-43d3-bd59-4da9d9b59863	2026-11-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.298914-06
c7b33ae9-b8fb-4f1c-aef0-969261d32283	2026-11-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.304363-06
611cfc2c-b435-41c5-94a9-ac6f8bbec017	2026-11-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.307882-06
fe80b1d4-aac3-4b89-b813-696e726bf524	2026-11-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.311369-06
944f7ee4-2cff-47e4-a262-838991c4a690	2026-11-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.31469-06
54536ab6-4ba4-4ee2-9a07-9575c0792862	2026-11-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.318325-06
b51d98c0-3125-42bf-8222-b23d8a43cf90	2026-11-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.321636-06
7621a5ca-97b7-406d-b31a-b44ab828d847	2026-11-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.324856-06
dce74fba-9907-4f2f-9dca-93dee02f9cde	2026-11-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.329916-06
71f2d9c6-151a-4618-a389-00a4aa25a2fb	2026-11-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.335199-06
b20f979f-51c3-4ee2-b1c5-7ba385969835	2026-11-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.338277-06
d854dda6-15d8-436a-b41c-f3ea38b58da4	2026-11-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.341806-06
29d5965b-3927-47cf-9027-0309ac4f6335	2026-11-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.345152-06
b36476e9-d76f-4bda-896e-81a6481a6d6a	2026-11-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.348316-06
f396436f-13e3-488f-9727-95aad2daba3f	2026-11-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.351699-06
261bb2d1-af98-4279-9a97-09a246b3b795	2026-11-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.355003-06
0422df4d-5262-4e1a-858b-e44f66808981	2026-11-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.35851-06
271436e4-cd31-4709-ae42-71ffccebee3e	2026-11-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.361599-06
2a1b2f20-5fa7-4c8f-b416-1a8ddbc239e3	2026-11-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.364632-06
a4bd4d5c-50c4-4366-9984-1fe4d1d543bb	2026-12-01 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.368938-06
17d698b6-080a-4203-9e81-2ef3c5040486	2026-12-02 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.375768-06
c29d921c-ef57-437d-9c38-cdbb88a06b32	2026-12-03 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.38074-06
b11004c8-4d5f-4a32-a223-d2d7741a3c31	2026-12-04 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.385758-06
8738194e-fc0d-49cc-9762-7ed66cc02b56	2026-12-05 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.389674-06
36fe826d-9109-4115-bda1-a69d31fdd688	2026-12-06 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.392719-06
c14ff730-f7eb-482e-83b2-7682291e8d90	2026-12-07 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.396455-06
94550c59-76e1-4121-b5ba-41a64120dbe0	2026-12-08 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.399679-06
bf03d7b8-7632-4403-bbe2-20474d579b79	2026-12-09 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.402541-06
1111b663-10a7-4ad4-85f3-b01514b6f1a6	2026-12-10 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.405379-06
e8747b91-0588-4df5-ab41-fce7a8699253	2026-12-11 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.408169-06
15a46ba3-e076-47e6-a93d-a0ea94745826	2026-12-12 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.412779-06
f89e8f76-1d1b-4e7f-a8ec-3b6b9818da62	2026-12-13 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.417057-06
64bd20eb-be2a-493f-ae8a-10dbd3601cae	2026-12-14 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.420094-06
353b7009-8f82-4829-8a6d-30370c8573f6	2026-12-15 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.424315-06
4e033936-71ae-4d91-94fa-5ff853716a5e	2026-12-16 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.42732-06
7651112c-c4c6-4f53-a2c3-6810114a9160	2026-12-17 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.430112-06
d1075f21-fdc2-4071-bd86-2eabc42d2968	2026-12-18 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.432896-06
49d46188-983a-4790-b3e2-aac3a17157e7	2026-12-19 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.436798-06
32e8e2fb-a44a-4eee-acb6-a876af2897d2	2026-12-20 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.43977-06
5a4c43c7-6a96-4040-8900-903c61502bdb	2026-12-21 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.442614-06
8f01a957-c0fb-46ad-8d92-2d8b59d88fdc	2026-12-22 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.445674-06
9e16fdc5-4dcf-4489-82d3-4cbb1900c69c	2026-12-23 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.450055-06
69d6c7a7-cba0-4dd2-ad85-fa0e89856df9	2026-12-24 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.453067-06
4c128564-547d-455f-8767-d9c74f8b66e8	2026-12-25 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.456001-06
cf085ea3-a8fe-48e2-9fd3-8f87df138fd4	2026-12-26 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.458879-06
559af2b7-3374-4254-a640-d79a3f2d7886	2026-12-27 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.462575-06
de43bcd6-29ce-41bc-b882-e424bc1a2caf	2026-12-28 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.46553-06
9a487b70-ffeb-4c80-9845-403a47ad250a	2026-12-29 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.468396-06
59da9a7a-f255-4e02-9574-2fb1cfaf0f66	2026-12-30 18:00:00-06	36.6243	691ce7b8-cf85-43e5-a878-185c8bbb50f7	385e35f0-87c3-4182-b514-12dcd0eb3b1a	2026-04-22 16:20:21.471148-06
\.


--
-- Data for Name: IncomeTax; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."IncomeTax" ("Id", "Min", "Max", "Percentage", "Base", "Excess") FROM stdin;
1	0.01	100000	0	0	0
2	10000.01	200000	15	0	100000
3	200000.01	350000	20	15000	200000
4	350000.01	500000	25	45000	350000
5	500000.01	1000000	30	82500	500000
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260319154340_adding_bankIMGURL	8.0.24
20260319202619_changing_properties_on_Account	8.0.24
20260319203004_changin_properties_on_accounts	8.0.24
20260319203524_addingchanginsoncurrencyaccounttable	8.0.24
20260319203853_addingchanginsoncurrencyaccounttable2	8.0.24
20260321141532_adding changings on account	8.0.24
20260323163504_adding changes on transactions2	8.0.24
20260324202155_adding changes on transactions detail	8.0.24
20260324220717_adding the right FK relation	8.0.24
20260325213145_adding categories params	8.0.24
20260325214145_adding IR as Income Tax	8.0.24
20260325220033_adding categories params 2	8.0.24
20260327161818_adding for salary and params	8.0.24
20260327162355_adding categories params 3	8.0.24
20260327171715_adding for salary and params 4	8.0.24
20260401205014_migrating transfers module	8.0.24
20260407224604_adding initial balance	8.0.24
20260410192146_restructuring_transaction	8.0.24
20260414155813_changing_double_decimal	8.0.24
20260422212441_adding_exchangeRate	8.0.24
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: users; Owner: postgres
--

COPY users."Users" ("UserId", "Email", "Username", "PasswordHash", "FullName", "IsActive", "TwoFactorEnabled", "CreatedAt", "HiresDate") FROM stdin;
c502b17e-a322-4df7-ad25-75a11bc2ac2d	edwincruz130691@gmail.com	Egeminis13	$2a$11$pn2e6BnsjXE4ZPgynA/UhedqSY.PkOvgKutt0FVbgX0VRwjpStZTG	Edwin Cruz	t	f	2026-03-16 15:03:31.381309-06	2016-08-01 00:00:00-06
\.


--
-- Name: TwoFactorStatus_TwoFactorStatusId_seq; Type: SEQUENCE SET; Schema: auth; Owner: postgres
--

SELECT pg_catalog.setval('auth."TwoFactorStatus_TwoFactorStatusId_seq"', 5, false);


--
-- Name: IncomeTax_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."IncomeTax_Id_seq"', 1, false);


--
-- Name: RefreshTokens PK_RefreshTokens; Type: CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."RefreshTokens"
    ADD CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("RefreshTokenId");


--
-- Name: TwoFactorCodes PK_TwoFactorCodes; Type: CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."TwoFactorCodes"
    ADD CONSTRAINT "PK_TwoFactorCodes" PRIMARY KEY ("TwoFactorCodeId");


--
-- Name: TwoFactorStatus PK_TwoFactorStatus; Type: CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."TwoFactorStatus"
    ADD CONSTRAINT "PK_TwoFactorStatus" PRIMARY KEY ("TwoFactorStatusId");


--
-- Name: AccountTypes PK_AccountTypes; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."AccountTypes"
    ADD CONSTRAINT "PK_AccountTypes" PRIMARY KEY ("AccountTypeId");


--
-- Name: Accounts PK_Accounts; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Accounts"
    ADD CONSTRAINT "PK_Accounts" PRIMARY KEY ("AccountId");


--
-- Name: Banks PK_Banks; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Banks"
    ADD CONSTRAINT "PK_Banks" PRIMARY KEY ("BankId");


--
-- Name: Categories PK_Categories; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Categories"
    ADD CONSTRAINT "PK_Categories" PRIMARY KEY ("CategoryId");


--
-- Name: CategoryParams PK_CategoryParams; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."CategoryParams"
    ADD CONSTRAINT "PK_CategoryParams" PRIMARY KEY ("ParamId");


--
-- Name: Currencies PK_Currencies; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Currencies"
    ADD CONSTRAINT "PK_Currencies" PRIMARY KEY ("CurrencyId");


--
-- Name: Natures PK_Natures; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Natures"
    ADD CONSTRAINT "PK_Natures" PRIMARY KEY ("NatureId");


--
-- Name: TransactionTypes PK_TransactionTypes; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."TransactionTypes"
    ADD CONSTRAINT "PK_TransactionTypes" PRIMARY KEY ("TransactionTypeId");


--
-- Name: Transactions PK_Transactions; Type: CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Transactions"
    ADD CONSTRAINT "PK_Transactions" PRIMARY KEY ("TransactionId");


--
-- Name: ExchangeRates PK_ExchangeRates; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ExchangeRates"
    ADD CONSTRAINT "PK_ExchangeRates" PRIMARY KEY ("ExchangeId");


--
-- Name: IncomeTax PK_IncomeTax; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."IncomeTax"
    ADD CONSTRAINT "PK_IncomeTax" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: users; Owner: postgres
--

ALTER TABLE ONLY users."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("UserId");


--
-- Name: IX_RefreshTokens_Token; Type: INDEX; Schema: auth; Owner: postgres
--

CREATE UNIQUE INDEX "IX_RefreshTokens_Token" ON auth."RefreshTokens" USING btree ("Token");


--
-- Name: IX_RefreshTokens_UserId; Type: INDEX; Schema: auth; Owner: postgres
--

CREATE INDEX "IX_RefreshTokens_UserId" ON auth."RefreshTokens" USING btree ("UserId");


--
-- Name: IX_TwoFactorCodes_TwoFactorStatusId; Type: INDEX; Schema: auth; Owner: postgres
--

CREATE INDEX "IX_TwoFactorCodes_TwoFactorStatusId" ON auth."TwoFactorCodes" USING btree ("TwoFactorStatusId");


--
-- Name: IX_TwoFactorCodes_UserId_Code; Type: INDEX; Schema: auth; Owner: postgres
--

CREATE INDEX "IX_TwoFactorCodes_UserId_Code" ON auth."TwoFactorCodes" USING btree ("UserId", "Code");


--
-- Name: IX_Accounts_AccountTypeId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Accounts_AccountTypeId" ON finances."Accounts" USING btree ("AccountTypeId");


--
-- Name: IX_Accounts_BankId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Accounts_BankId" ON finances."Accounts" USING btree ("BankId");


--
-- Name: IX_Accounts_CurrecyId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Accounts_CurrecyId" ON finances."Accounts" USING btree ("CurrencyId");


--
-- Name: IX_Accounts_UserId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Accounts_UserId" ON finances."Accounts" USING btree ("UserId");


--
-- Name: IX_Categories_NatureId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Categories_NatureId" ON finances."Categories" USING btree ("NatureId");


--
-- Name: IX_Categories_ParentId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Categories_ParentId" ON finances."Categories" USING btree ("ParentId");


--
-- Name: IX_Categories_UserId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Categories_UserId" ON finances."Categories" USING btree ("UserId");


--
-- Name: IX_CategoryParams_CategoryId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_CategoryParams_CategoryId" ON finances."CategoryParams" USING btree ("CategoryId");


--
-- Name: IX_TransactionTypes_Code; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE UNIQUE INDEX "IX_TransactionTypes_Code" ON finances."TransactionTypes" USING btree ("Code");


--
-- Name: IX_Transactions_AccountId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Transactions_AccountId" ON finances."Transactions" USING btree ("AccountId") WITH (fillfactor='100', deduplicate_items='true');


--
-- Name: IX_Transactions_CategoryId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Transactions_CategoryId" ON finances."Transactions" USING btree ("CategoryId") WITH (fillfactor='100', deduplicate_items='true');


--
-- Name: IX_Transactions_TransactionTypeId; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Transactions_TransactionTypeId" ON finances."Transactions" USING btree ("TransactionTypeId") WITH (fillfactor='100', deduplicate_items='true');


--
-- Name: IX_Transactions_UserId_TransactionDate; Type: INDEX; Schema: finances; Owner: postgres
--

CREATE INDEX "IX_Transactions_UserId_TransactionDate" ON finances."Transactions" USING btree ("UserId", "TransactionDate") WITH (fillfactor='100', deduplicate_items='true');


--
-- Name: IX_ExchangeRates_CurrencyFromId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ExchangeRates_CurrencyFromId" ON public."ExchangeRates" USING btree ("CurrencyFromId");


--
-- Name: IX_ExchangeRates_CurrencyToId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ExchangeRates_CurrencyToId" ON public."ExchangeRates" USING btree ("CurrencyToId");


--
-- Name: IX_ExchangeRates_Date; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_ExchangeRates_Date" ON public."ExchangeRates" USING btree ("Date");


--
-- Name: IX_Users_Email; Type: INDEX; Schema: users; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Users_Email" ON users."Users" USING btree ("Email");


--
-- Name: RefreshTokens FK_RefreshTokens_Users_UserId; Type: FK CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."RefreshTokens"
    ADD CONSTRAINT "FK_RefreshTokens_Users_UserId" FOREIGN KEY ("UserId") REFERENCES users."Users"("UserId") ON DELETE CASCADE;


--
-- Name: TwoFactorCodes FK_TwoFactorCodes_TwoFactorStatus_TwoFactorStatusId; Type: FK CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."TwoFactorCodes"
    ADD CONSTRAINT "FK_TwoFactorCodes_TwoFactorStatus_TwoFactorStatusId" FOREIGN KEY ("TwoFactorStatusId") REFERENCES auth."TwoFactorStatus"("TwoFactorStatusId") ON DELETE CASCADE;


--
-- Name: TwoFactorCodes FK_TwoFactorCodes_Users_UserId; Type: FK CONSTRAINT; Schema: auth; Owner: postgres
--

ALTER TABLE ONLY auth."TwoFactorCodes"
    ADD CONSTRAINT "FK_TwoFactorCodes_Users_UserId" FOREIGN KEY ("UserId") REFERENCES users."Users"("UserId") ON DELETE CASCADE;


--
-- Name: Accounts FK_Accounts_AccountTypes_AccountTypeId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Accounts"
    ADD CONSTRAINT "FK_Accounts_AccountTypes_AccountTypeId" FOREIGN KEY ("AccountTypeId") REFERENCES finances."AccountTypes"("AccountTypeId") ON DELETE CASCADE;


--
-- Name: Accounts FK_Accounts_Banks_BankId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Accounts"
    ADD CONSTRAINT "FK_Accounts_Banks_BankId" FOREIGN KEY ("BankId") REFERENCES finances."Banks"("BankId");


--
-- Name: Accounts FK_Accounts_Currencies_CurrecyId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Accounts"
    ADD CONSTRAINT "FK_Accounts_Currencies_CurrecyId" FOREIGN KEY ("CurrencyId") REFERENCES finances."Currencies"("CurrencyId") ON DELETE CASCADE;


--
-- Name: Accounts FK_Accounts_Users_UserId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Accounts"
    ADD CONSTRAINT "FK_Accounts_Users_UserId" FOREIGN KEY ("UserId") REFERENCES users."Users"("UserId") ON DELETE CASCADE;


--
-- Name: Categories FK_Categories_Categories_ParentId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Categories"
    ADD CONSTRAINT "FK_Categories_Categories_ParentId" FOREIGN KEY ("ParentId") REFERENCES finances."Categories"("CategoryId") ON DELETE RESTRICT;


--
-- Name: Categories FK_Categories_Natures_NatureId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Categories"
    ADD CONSTRAINT "FK_Categories_Natures_NatureId" FOREIGN KEY ("NatureId") REFERENCES finances."Natures"("NatureId") ON DELETE CASCADE;


--
-- Name: Categories FK_Categories_Users_UserId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Categories"
    ADD CONSTRAINT "FK_Categories_Users_UserId" FOREIGN KEY ("UserId") REFERENCES users."Users"("UserId") ON DELETE CASCADE;


--
-- Name: CategoryParams FK_CategoryParams_Categories_CategoryId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."CategoryParams"
    ADD CONSTRAINT "FK_CategoryParams_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES finances."Categories"("CategoryId") ON DELETE CASCADE;


--
-- Name: Transactions FK_Transactions_Accounts_AccountId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Transactions"
    ADD CONSTRAINT "FK_Transactions_Accounts_AccountId" FOREIGN KEY ("AccountId") REFERENCES finances."Accounts"("AccountId");


--
-- Name: Transactions FK_Transactions_Categories_CategoryId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Transactions"
    ADD CONSTRAINT "FK_Transactions_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES finances."Categories"("CategoryId");


--
-- Name: Transactions FK_Transactions_TransactionTypes_TransactionTypeId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Transactions"
    ADD CONSTRAINT "FK_Transactions_TransactionTypes_TransactionTypeId" FOREIGN KEY ("TransactionTypeId") REFERENCES finances."TransactionTypes"("TransactionTypeId") ON DELETE CASCADE;


--
-- Name: Transactions FK_Transactions_Users_UserId; Type: FK CONSTRAINT; Schema: finances; Owner: postgres
--

ALTER TABLE ONLY finances."Transactions"
    ADD CONSTRAINT "FK_Transactions_Users_UserId" FOREIGN KEY ("UserId") REFERENCES users."Users"("UserId") ON DELETE CASCADE;


--
-- Name: ExchangeRates FK_ExchangeRates_Currencies_CurrencyFromId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ExchangeRates"
    ADD CONSTRAINT "FK_ExchangeRates_Currencies_CurrencyFromId" FOREIGN KEY ("CurrencyFromId") REFERENCES finances."Currencies"("CurrencyId") ON DELETE CASCADE;


--
-- Name: ExchangeRates FK_ExchangeRates_Currencies_CurrencyToId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ExchangeRates"
    ADD CONSTRAINT "FK_ExchangeRates_Currencies_CurrencyToId" FOREIGN KEY ("CurrencyToId") REFERENCES finances."Currencies"("CurrencyId") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict IZ31n9THp2LJWXM61asVwPAx2WAYh8wchcYJLDH9ne14dc9Zk1z1MAsG86Mfbks

