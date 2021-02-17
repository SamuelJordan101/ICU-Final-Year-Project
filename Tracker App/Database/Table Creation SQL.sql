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

CREATE TABLE Steps (
    ID INT IDENTITY(1,1) NOT NULL UNIQUE,
    Step TEXT NOT NULL,
    Image INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (Image) REFERENCES Image(ID)
);

CREATE TABLE Exercises (
    ID INT IDENTITY(1,1) NOT NULL UNIQUE,
    ExerciseName TEXT NOT NULL,
    Category TEXT,
    StepID INT NOT NULL,
    Image INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (StepID) REFERENCES Steps(ID),
    FOREIGN KEY (Image) REFERENCES Image(ID)
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

CREATE TABLE ExercisePlanSchedule (
    ID INT IDENTITY(1,1) UNIQUE NOT NULL,
    ExercisePlanID INT NOT NULL,
    DayOfWeek INT NOT NULL,
    HourOfDay INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (ExercisePlanID) REFERENCES ExercisePlan(ID)
);

INSERT INTO CPAX (PatientID, CPAXDate, Grip,Respiratory,Cough,BedMovement,DynamicSitting,StandingBalance,SitToStand,BedToChair,Stepping,Transfer)
VALUES (111111,'2021/01/01',1,1,1,1,1,1,1,1,1,1);

INSERT INTO Patient 
VALUES (111111,'Sam Jordan', '2021/01/01', 'ICU', 'Derriford Hospital', '1');

INSERT INTO Image (ImageData)
VALUES ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Sam\Documents\GitHub\ICU-Final-Year-Project\Documentation\Storyboard\Images\ExercisePlaceholder.png', SINGLE_BLOB) as T1));

INSERT INTO Image (ImageData, PatientID)
VALUES ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Sam\Documents\GitHub\ICU-Final-Year-Project\Documentation\Storyboard\Images\Derriford Hospital.jpg', SINGLE_BLOB) as T1),111111);