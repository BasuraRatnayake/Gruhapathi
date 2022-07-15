CREATE DATABASE IF NOT EXISTS gruhapathi_v1;
USE gruhapathi_v1;

DROP TABLE IF EXISTS tbl_users;
DROP TABLE IF EXISTS tbl_loginDetails;
DROP TABLE IF EXISTS tbl_DeviceVersions;
DROP TABLE IF EXISTS tbl_userDescendents;
DROP TABLE IF EXISTS tbl_hubs;
DROP TABLE IF EXISTS tbl_houses;
DROP TABLE IF EXISTS tbl_altLog;
DROP TABLE IF EXISTS tbl_SoftwareVersions;
DROP TABLE IF EXISTS tbl_userDevice;
DROP TABLE IF EXISTS tbl_floors;
DROP TABLE IF EXISTS tbl_rooms;
DROP TABLE IF EXISTS tbl_devices;
DROP TABLE IF EXISTS tbl_powerPrices;
DROP TABLE IF EXISTS tbl_usageData;
DROP TABLE IF EXISTS tbl_tokens;
DROP TABLE IF EXISTS tbl_deviceAuths;
DROP TABLE IF EXISTS tbl_commands;
DROP TABLE IF EXISTS tbl_cmdRequests;

CREATE TABLE IF NOT EXISTS tbl_users(
	userId 			INT(8) 			NOT NULL 	AUTO_INCREMENT,
	firstname		VARCHAR(20)		NOT NULL,
	lastname		VARCHAR(50) 	NOT NULL,
	title			VARCHAR(7)		NOT NULL 	DEFAULT 'Mr',
	dateJoined		DATETIME 		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	
	CONSTRAINT PK_user 	PRIMARY KEY (userId)
);

CREATE TABLE IF NOT EXISTS tbl_loginDetails(	
	userId 			INT(8) 			NOT NULL,
	username		VARCHAR(10)		NOT NULL,
	password		VARCHAR(50)		NOT NULL,
	email			VARCHAR(50)		NOT NULL,
	mobileNumber	INT(10)			NOT NULL,
	
	CONSTRAINT PK_login_user 	PRIMARY KEY (userId, username),
	UNIQUE KEY user_email (username,email, mobileNumber),
	CONSTRAINT FK_login_user 	FOREIGN KEY (userId) 	REFERENCES tbl_users(userId) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_userDescendents(
	descent_id		INT(8) 			NOT NULL 	AUTO_INCREMENT,
	username		VARCHAR(10)		NOT NULL,
	descent_user	VARCHAR(10)		NOT NULL,
	
	CONSTRAINT PK_descentUser 		PRIMARY KEY (descent_id),	
	CONSTRAINT FK_descent_parent 	FOREIGN KEY (username) REFERENCES tbl_loginDetails (username),
	CONSTRAINT FK_descent_child 	FOREIGN KEY (descent_user) REFERENCES tbl_loginDetails (username)
);

CREATE TABLE IF NOT EXISTS tbl_tokens(
	username		VARCHAR(10)		NOT NULL,
	timeCreated		DATETIME 		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	token			VARCHAR(32)		NOT NULL,
	hashSalt		VARCHAR(5)		NOT NULL,
	refreshToken	VARCHAR(32)		NOT NULL,

	CONSTRAINT PK_tokens 		PRIMARY KEY(username, token),
	CONSTRAINT FK_tokens_user 	FOREIGN KEY (username) 	REFERENCES tbl_loginDetails (username) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_altLog(
	qr_id 			INT(8) 			NOT NULL 	AUTO_INCREMENT,
	qr_code			VARCHAR(32)		NOT NULL,
	created			DATETIME 		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	auth_token		VARCHAR(32)		NOT NULL 	DEFAULT '0',
	ip_address		VARCHAR(16)		NOT NULL,	
	
	CONSTRAINT PK_altLog 		PRIMARY KEY (qr_id)
); 

CREATE TABLE IF NOT EXISTS tbl_DeviceVersions(
	versionId 		INT(8) 			NOT NULL 	AUTO_INCREMENT,
	dateMan			DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	devVersion		VARCHAR(11)		NOT NULL,
	devType			VARCHAR(20)		NOT NULL,
	
	CONSTRAINT PK_deviceVersions 	PRIMARY KEY (versionId)
);

CREATE TABLE IF NOT EXISTS tbl_SoftwareVersions(
	softwareId		INT(8)			NOT NULL	AUTO_INCREMENT,
	dateMan			DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	softVersion		VARCHAR(11)		NOT NULL,
	softType		VARCHAR(20)		NOT NULL,
	
	CONSTRAINT PK_softwareVersions 	PRIMARY KEY (softwareId)
);

CREATE TABLE IF NOT EXISTS tbl_devices(
	deviceId		INT(8) 			NOT NULL 	AUTO_INCREMENT,
	versionId 		INT(8) 			NOT NULL, 
	serialNumber	VARCHAR(5)		NOT NULL,
	assigned		VARCHAR(3)		NOT NULL 	DEFAULT 'NO',
	username		VARCHAR(10)		NOT NULL,
		
	UNIQUE KEY (serialNumber),
	CONSTRAINT PK_devices 	PRIMARY KEY (deviceId),
	CONSTRAINT FK_dev_user 	FOREIGN KEY (username) 		REFERENCES tbl_loginDetails(username),
	CONSTRAINT FK_dev_id 	FOREIGN KEY (versionId)		REFERENCES tbl_DeviceVersions (versionId) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS tbl_deviceAuths(
	auth_id			INT(9) 			NOT NULL 	AUTO_INCREMENT,
	deviceId		INT(8) 			NOT NULL,
	auth_code		VARCHAR(20)		NOT NULL,
	dateRecored		DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	
	UNIQUE KEY (auth_code),
	CONSTRAINT PK_deviceAuths			PRIMARY KEY (auth_id, deviceId),
	CONSTRAINT FK_deviceAuths_device 	FOREIGN KEY (deviceId) 		REFERENCES tbl_devices (deviceId)
);

CREATE TABLE IF NOT EXISTS tbl_houses(
	houseId			INT(8) 			NOT NULL 	AUTO_INCREMENT,
	deviceId		INT(8) 			NOT NULL,
	username		VARCHAR(10)		NOT NULL,
	houseName		VARCHAR(20),
	postal_lane		VARCHAR(7)		NOT NULL,
	town			VARCHAR(30)		NOT NULL,
	city			VARCHAR(30)		NOT NULL,	
	landNumber		INT(10),
	powerState		INT(1)			NOT NULL 	DEFAULT 0,
	
	UNIQUE KEY (username, landNumber),
	CONSTRAINT PK_house 		PRIMARY KEY (houseId, deviceId),
	CONSTRAINT FK_house_hub 	FOREIGN KEY (deviceId) 	REFERENCES tbl_devices(deviceId) ON DELETE CASCADE,
	CONSTRAINT FK_house_user 	FOREIGN KEY (username) 	REFERENCES tbl_loginDetails(username) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_floors(
	floorId			INT(8) 			NOT NULL 	AUTO_INCREMENT,
	houseId			INT(8) 			NOT NULL,
	floorname		VARCHAR(20)		NOT NULL,	
	powerState		INT(1)			NOT NULL 	DEFAULT 0,
	
	CONSTRAINT PK_floor 		PRIMARY KEY (floorId, houseId),
	CONSTRAINT FK_house_floor 	FOREIGN KEY (houseId) 	REFERENCES tbl_houses (houseId) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_rooms(
	roomId			INT(8) 			NOT NULL 	AUTO_INCREMENT,
	floorId			INT(8) 			NOT NULL,
	roomName		VARCHAR(20)		NOT NULL,	
	roomDesc		VARCHAR(50)		NOT NULL 	DEFAULT 'Room Description',
	loc_xy			VARCHAR(7) 		NOT NULL	DEFAULT '0,0',
	size_hw			VARCHAR(7)		NOT NULL	DEFAULT '100,100',
	rotateState		INT(1)			NOT NULL	DEFAULT 1,
	powerState		INT(1)			NOT NULL 	DEFAULT 0,
	
	CONSTRAINT PK_rooms 		PRIMARY KEY (roomId, floorId),
	CONSTRAINT FK_floor_rooms 	FOREIGN KEY (floorId) 	REFERENCES tbl_floors (floorId) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_userDevice(
	userDevices		INT(8) 			NOT NULL 	AUTO_INCREMENT,
	deviceId		INT(8) 			NOT NULL,
	floorId			INT(8) 			NOT NULL,
	roomId			INT(8),
	devName			VARCHAR(20)		NOT NULL 	DEFAULT 'Device Name',
	devDes			VARCHAR(50)		NOT NULL 	DEFAULT 'Device Description',
	loc_xy			VARCHAR(7) 		NOT NULL	DEFAULT '0,0',
	powerState		INT(1)			NOT NULL 	DEFAULT 0,
	isDoor 			INT(1) 			NOT NULL	DEFAULT 0,
	rotateState		INT(1)			NOT NULL	DEFAULT 1,
	
	CONSTRAINT PK_userdevices 		PRIMARY KEY (userDevices),
	CONSTRAINT FK_userdev_device 	FOREIGN KEY (deviceId) 	REFERENCES tbl_devices (deviceId) ON DELETE CASCADE,
	CONSTRAINT FK_userdev_room 		FOREIGN KEY (roomId) 	REFERENCES tbl_rooms (roomId) ON DELETE CASCADE,
	CONSTRAINT FK_userdev_floor 	FOREIGN KEY (floorId) 	REFERENCES tbl_floors (floorId) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_powerPrices(
	priceId			INT(4) 			NOT NULL 	AUTO_INCREMENT,
	powerType		ENUM('E','W') 	NOT NULL,
	powerPrice		INT(3)			NOT NULL,
	dateIntro		DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	
	CONSTRAINT PK_prices 	PRIMARY KEY (priceId, dateIntro)
);

CREATE TABLE IF NOT EXISTS tbl_usageData(
	usageId			INT(9) 			NOT NULL 	AUTO_INCREMENT,
	priceId			INT(4) 			NOT NULL,
	userDevices		INT(8) 			NOT NULL,
	dateRecored		DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	usageData		INT(5)			NOT NULL,
	
	CONSTRAINT PK_usageData 	PRIMARY KEY (usageId, dateRecored),
	CONSTRAINT FK_usage_price 	FOREIGN KEY (priceId) 		REFERENCES tbl_powerPrices (priceId) ON DELETE CASCADE,
	CONSTRAINT FK_usage_device 	FOREIGN KEY (userDevices) 	REFERENCES tbl_userDevice (userDevices) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tbl_commands(
	commandId		INT(9) 			NOT NULL 	AUTO_INCREMENT,
	username		VARCHAR(10)		NOT NULL,
	deviceId		INT(8) 			NOT NULL,
	dateRecored		DATETIME		NOT NULL 	DEFAULT CURRENT_TIMESTAMP,
	
	CONSTRAINT PK_commands			PRIMARY KEY (commandId, username),
	CONSTRAINT FK_commands_user 	FOREIGN KEY (username) 		REFERENCES tbl_loginDetails (username),
	CONSTRAINT FK_commands_device	FOREIGN KEY (deviceId) 		REFERENCES tbl_devices (deviceId)
);
CREATE TABLE IF NOT EXISTS tbl_cmdRequests(
	reqId			INT(9) 			NOT NULL 	AUTO_INCREMENT,
	commandId		INT(9) 			NOT NULL,
	completed		VARCHAR(1)		NOT NULL 	DEFAULT '0',
	
	CONSTRAINT PK_cmdReq			PRIMARY KEY (reqId, commandId),
	CONSTRAINT FK_cmdReq_cmd 		FOREIGN KEY (commandId) 		REFERENCES tbl_commands (commandId)
);