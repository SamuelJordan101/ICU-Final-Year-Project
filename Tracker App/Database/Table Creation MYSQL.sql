DROP DATABASE tracker_app; 

CREATE DATABASE tracker_app;

USE tracker_app;

CREATE TABLE Image (
    ID INT AUTO_INCREMENT NOT NULL UNIQUE,
    ImageData BLOB NOT NULL,
    PRIMARY KEY (ID)
);

CREATE TABLE Steps (
    ID INT AUTO_INCREMENT NOT NULL UNIQUE,
    Step TEXT NOT NULL,
    Image INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (Image) REFERENCES Image(ID)
);

CREATE TABLE Exercises (
    ID INT AUTO_INCREMENT NOT NULL UNIQUE,
    ExerciseName TEXT NOT NULL,
    Category TEXT,
    StepID INT NOT NULL,
    Image INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (StepID) REFERENCES Steps(ID),
    FOREIGN KEY (Image) REFERENCES Image(ID)
);

CREATE TABLE CPAX (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
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
    PRIMARY KEY (ID)
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

CREATE TABLE PatientCPAXMap (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    CPAXID INT NOT NULL,
    CPAXDate DATETIME NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (CPAXID) REFERENCES CPAX(ID)
);

CREATE TABLE ImageCategory (
    ID INT NOT NULL AUTO_INCREMENT UNIQUE,
    Category TEXT NOT NULL,
    PatientID INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE Achievement (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    Achievement TEXT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE Goals (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    PatientID INT NOT NULL,
    Goal TEXT NOT NULL,
    Assigned BOOLEAN NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE PatientImageMap (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    CategoryID INT NOT NULL,
    ImageID INT NOT NULL,
    PatientID INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (CategoryID) REFERENCES ImageCategory(ID),
    FOREIGN KEY (ImageID) REFERENCES Image(ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE ExercisePlan (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    ExerciseID INT NOT NULL,
    PatientID INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (ExerciseID) REFERENCES Exercises(ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

CREATE TABLE ExercisePlanSchedule (
    ID INT AUTO_INCREMENT UNIQUE NOT NULL,
    ExercisePlanID INT NOT NULL,
    DayOfWeek INT NOT NULL,
    HourOfDay INT NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (ExercisePlanID) REFERENCES ExercisePlan(ID)
);

INSERT INTO patient VALUES (111111, "Samuel Jordan", "2021-01-01", "ICU", "Derriford Hospital", null);
