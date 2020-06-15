﻿IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GameLogs')
	DROP TABLE GameLogs
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Agents')
	DROP TABLE Agents
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AgentType')
	DROP TABLE AgentType
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Maps')
	DROP TABLE Maps
GO

CREATE TABLE "Maps"(
	MapID int IDENTITY(1,1),
	MapName varchar(20) NOT NULL
	PRIMARY KEY(MapID)
);

CREATE TABLE "AgentType"(
	TypeID int IDENTITY(1,1),
	TypeName varchar(20) NOT NULL
	PRIMARY KEY(TypeID)
);

CREATE TABLE "Agents"(
	AgentID int IDENTITY(1,1),
	AgentName varchar(20) NOT NULL,
	AgentTypeID int NOT NULL,
	SignatureAbilityName varchar(30),
	SignatureAbilityDiscription varchar(300),
	UltamateAbilityName varchar(30),
	UltamateAbilityDiscription varchar(300),
	AbilityOneName varchar(30),
	AbilityOneDiscription varchar(300),
	AbilityTwoName varchar(30),
	AbilityTwoDiscription varchar(300),
	Bio varchar(400),
	PRIMARY KEY (AgentID),
	FOREIGN KEY (AgentTypeID) REFERENCES AgentType(TypeID)
);

CREATE TABLE "GameLogs"(
	GameID int IDENTITY(1,1) NOT NULL,
	MapID int NOT NULL,
	AgentID int NOT NULL,
	TeamScore int NOT NULL,
	OpponentScore int NOT NULL,
	Kills int,
	Deaths int,
	Assits int,
	ADR int,
	DateLogged datetime,
	PRIMARY KEY (GameID),
	FOREIGN KEY (MapID) REFERENCES Maps(MapID),
	FOREIGN KEY (AgentID) REFERENCES Agents(AgentID)
)