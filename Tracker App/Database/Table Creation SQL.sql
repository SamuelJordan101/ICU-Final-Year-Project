DECLARE @sql NVARCHAR(max)=''

SELECT @sql += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql

CREATE TABLE Image (
    ID INT IDENTITY(1,1) NOT NULL UNIQUE,
    ImageData IMAGE NOT NULL,
    Category TEXT,
    PatientID INT,
    PRIMARY KEY (ID)
);

CREATE TABLE Exercises (
    ID INT IDENTITY(1,1) NOT NULL UNIQUE,
    ExerciseName TEXT NOT NULL,
    Category TEXT,
    Image INT NOT NULL,
    Gif INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (Image) REFERENCES Image(ID)
);

CREATE TABLE Steps (
    ID INT IDENTITY(1,1) NOT NULL UNIQUE,
    Step TEXT NOT NULL,
    Image INT NOT NULL,
    ExerciseID INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (Image) REFERENCES Image(ID),
	FOREIGN KEY (ExerciseID) REFERENCES Exercises(ID)
);

CREATE TABLE CPAX (
    ID INT IDENTITY(1,1) UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    CPAXDate DATETIME NOT NULL,
    Grip INT NOT NULL,
    Respiratory INT NOT NULL,
    Cough INT NOT NULL,
    BedMovement INT NOT NULL,
    DynamicSitting INT NOT NULL,
    StandingBalance INT NOT NULL,
    SitToStand INT NOT NULL,
    BedToChair INT NOT NULL,
    Stepping INT NOT NULL,
    Transfer INT NOT NULL,
    PRIMARY KEY (ID),
);

CREATE TABLE Patient (
    PatientID INT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    Admission DATE NOT NULL,
    Ward TEXT NOT NULL,
    Hospital TEXT NOT NULL,
    GoalCPAX INT,
    PRIMARY KEY (PatientID),
    FOREIGN KEY (GoalCPAX) REFERENCES CPAX(ID)
);

CREATE TABLE Achievement (
    ID INT IDENTITY(1,1) UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    Achievement TEXT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE Goals (
    ID INT IDENTITY(1,1) UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    Goal TEXT NOT NULL,
    Assigned BIT NOT NULL,
    Done BIT,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE ExercisePlan (
    ID INT IDENTITY(1,1) UNIQUE NOT NULL,
    ExerciseID INT NOT NULL,
    PatientID INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (ExerciseID) REFERENCES Exercises(ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

INSERT INTO CPAX (PatientID, CPAXDate, Grip,Respiratory,Cough,BedMovement,DynamicSitting,StandingBalance,SitToStand,BedToChair,Stepping,Transfer)
VALUES (111111,'2021/01/01',1,1,1,1,1,1,1,1,1,1);

INSERT INTO Patient 
VALUES (111111,'Sam Jordan', '2021/01/01', 'ICU', 'Derriford Hospital', '1');

INSERT INTO Image (ImageData)
VALUES ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Sam\Documents\GitHub\ICU-Final-Year-Project\Documentation\Storyboard\Images\ExercisePlaceholder.png', SINGLE_BLOB) as T1));

INSERT INTO Image (ImageData)
VALUES ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Sam\Documents\GitHub\ICU-Final-Year-Project\Documentation\Storyboard\Images\Exercise Gif Placeholder.gif', SINGLE_BLOB) as T1));

INSERT INTO Image (ImageData, PatientID) 
VALUES ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Sam\Documents\GitHub\ICU-Final-Year-Project\Documentation\Storyboard\Images\Derriford Hospital.jpg', SINGLE_BLOB) as T1),111111);

INSERT INTO Achievement 
VALUES (111111,'Walked 1K');

INSERT INTO Goals (PatientID, Goal, Assigned, Done) 
VALUES (111111,'Get Out Of Bed', 'FALSE', 'FALSE');

INSERT INTO Goals (PatientID, Goal, Assigned, Done) 
VALUES (111111,'Have 3 Meals', 'FALSE', 'FALSE');

INSERT INTO Goals (PatientID, Goal, Assigned, Done) 
VALUES (111111,'Eat Breakfast', 'TRUE', 'FALSE');

INSERT INTO Exercises
VALUES ('Arm Curls','Arm','1','2');

INSERT INTO Steps 
VALUES ('Pick up Weight','1','1');

INSERT INTO Steps 
VALUES ('Curl Weight','1','1');

INSERT INTO Exercises
VALUES ('Leg Stretches','Leg','2','2');

INSERT INTO Steps 
VALUES ('Stand Up','1','2');

INSERT INTO Steps 
VALUES ('Put your leg infront of you','1','2');

INSERT INTO Steps 
VALUES ('Pull your leg up','1','2');