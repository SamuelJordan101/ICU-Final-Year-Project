PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Achievement
CREATE TABLE Achievement (
    ID          INTEGER PRIMARY KEY AUTOINCREMENT
                        UNIQUE
                        NOT NULL,
    PatientID   INT     REFERENCES Patient (PatientID) 
                        NOT NULL,
    Achievement TEXT    NOT NULL
);


-- Table: CPAX
CREATE TABLE CPAX (
    ID              INTEGER PRIMARY KEY AUTOINCREMENT
                            UNIQUE
                            NOT NULL,
    Grip            INT     NOT NULL,
    Respiratory     INT     NOT NULL,
    Cough           INT     NOT NULL,
    BedMovement     INT     NOT NULL,
    DynamicSitting  INT     NOT NULL,
    StandingBalance INT     NOT NULL,
    SitToStand      INT     NOT NULL,
    BedToChair      INT     NOT NULL,
    Stepping        INT     NOT NULL,
    Transfer        INT     NOT NULL
);


-- Table: ExercisePlan
CREATE TABLE ExercisePlan (
    ID         INTEGER  PRIMARY KEY AUTOINCREMENT
                        UNIQUE
                        NOT NULL,
    ExerciseID INT      REFERENCES Exercises (ID) 
                        NOT NULL,
    PatientID  INT      REFERENCES Patient (PatientID) 
                        NOT NULL,
    StartDate  DATETIME NOT NULL,
    EndDate    INT      NOT NULL
);


-- Table: ExercisePlanSchedule
CREATE TABLE ExercisePlanSchedule (
    ID             INTEGER PRIMARY KEY AUTOINCREMENT
                           UNIQUE
                           NOT NULL,
    ExercisePlanID INT     NOT NULL,
    DayOfWeek      INT     NOT NULL,
    Hour           INT     NOT NULL
);


-- Table: Exercises
CREATE TABLE Exercises (
    ID       INTEGER PRIMARY KEY AUTOINCREMENT
                     NOT NULL
                     UNIQUE,
    Name     TEXT    NOT NULL,
    Category TEXT,
    StepID   INT     REFERENCES Steps (ID) 
                     NOT NULL,
    Image    INT     REFERENCES Image (ID) 
                     NOT NULL
);


-- Table: Goals
CREATE TABLE Goals (
    ID        INTEGER PRIMARY KEY AUTOINCREMENT
                      UNIQUE
                      NOT NULL,
    PatientID INT     REFERENCES Patient (PatientID) 
                      NOT NULL,
    Goal      TEXT    NOT NULL,
    Type      BOOLEAN NOT NULL
);


-- Table: Image
CREATE TABLE Image (
    ID   INTEGER PRIMARY KEY AUTOINCREMENT
                 NOT NULL
                 UNIQUE,
    Data BLOB    NOT NULL
);


-- Table: ImageCategory
CREATE TABLE ImageCategory (
    ID   INTEGER   NOT NULL
                   PRIMARY KEY AUTOINCREMENT
                   UNIQUE,
    Name TEXT (20) NOT NULL
);


-- Table: Patient
CREATE TABLE Patient (
    PatientID   INT      PRIMARY KEY
                         NOT NULL
                         UNIQUE,
    Name        TEXT     NOT NULL,
    Admission   DATETIME NOT NULL,
    Ward        TEXT     NOT NULL,
    Hospital    TEXT     NOT NULL,
    Images      TEXT,
    CurrentCPAX TEXT     NOT NULL,
    GoalCPAX    TEXT
);


-- Table: PatientCPAXMap
CREATE TABLE PatientCPAXMap (
    ID        INTEGER  PRIMARY KEY AUTOINCREMENT
                       UNIQUE
                       NOT NULL,
    PatientID INT      REFERENCES Patient (PatientID) 
                       NOT NULL,
    CPAXID    INT      NOT NULL
                       REFERENCES CPAX (ID),
    Date      DATETIME NOT NULL
);


-- Table: PatientImageMap
CREATE TABLE PatientImageMap (
    ID         INTEGER PRIMARY KEY AUTOINCREMENT
                       UNIQUE
                       NOT NULL,
    CategoryID INT     REFERENCES ImageCategory (ID) 
                       NOT NULL,
    ImageID            REFERENCES Image (ID) 
                       NOT NULL,
    PatientID  INT     REFERENCES Patient (PatientID) 
                       NOT NULL
);


-- Table: Steps
CREATE TABLE Steps (
    ID    INTEGER PRIMARY KEY AUTOINCREMENT
                  NOT NULL
                  UNIQUE,
    Step  TEXT    NOT NULL,
    Image INT     REFERENCES Image (ID) 
                  NOT NULL
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
