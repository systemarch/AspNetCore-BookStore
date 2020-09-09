-- Drop the database 'BookStore'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Uncomment the ALTER DATABASE statement below to set the database to SINGLE_USER mode if the drop database command fails because the database is in use.
ALTER DATABASE BookStore SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- Drop the database if it exists
IF EXISTS (
  SELECT name
   FROM sys.databases
   WHERE name = N'BookStore'
)
DROP DATABASE BookStore
GO

-- Create a new database called 'BookStore'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'BookStore'
)
CREATE DATABASE BookStore
GO