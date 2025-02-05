DELETE FROM ProjectEntityServiceEntity;
DELETE FROM Projects;
DELETE FROM Employees;
DELETE FROM "Services";
DELETE FROM UnitTypes;
DELETE FROM Roles;
DELETE FROM Customers;

INSERT INTO Customers (CustomerName) VALUES 
('EC utbildning'), 
('Affär AB'), 
('Kund AB');

INSERT INTO Roles (RoleName) VALUES 
('Projektledare'), 
('Webbutvecklare'), 
('Systemutvecklare');

INSERT INTO Employees (FirstName, LastName, RoleId) VALUES 
('Test', 'Testson', 3), 
('Joakim', 'Test', 2), 
('Hans', 'Exempel', 1);

INSERT INTO UnitTypes (UnitType) VALUES 
('Timmar'), 
('Styck'), 
('Mil');

INSERT INTO "Services" (ServiceName, Price, UnitId) VALUES 
('Projektledning', 2000, 1), 
('Webbutveckling', 1500, 1), 
('Milersättning', 50, 3);

INSERT INTO Projects (Title, Description, StartDate, EndDate, StatusId, CustomerId, ProjectManagerId) VALUES 
('Utveckla en ny web store', 'Exempel', '2024-01-01', '2024-06-01', 1, 1, 1), 
('Interntprojekt', 'Byte av glödlampa på personal toaletten', '2024-02-01', NULL, 2, 2, 2), 
('Pengar ska in', NULL, '2024-03-01', '2024-08-01', 3, 3, 3);

INSERT INTO ProjectEntityServiceEntity (ProjectsId, ServicesId) VALUES 
(100, 1), 
(100, 2), 
(102, 3);
