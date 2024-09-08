create database demo01db;

use demo01db;

create table Employee (
	IdEmployee int primary key auto_increment,
	DocumentNumber varchar(8) unique,
	CompleteName varchar(60),
	Salary int
);

DELIMITER $$
CREATE PROCEDURE sp_employee_list()
BEGIN
    SELECT IdEmployee, DocumentNumber, CompleteName, Salary 
    FROM Employee;
END $$
DELIMITER ;

CALL sp_employee_list();

DELIMITER $$

CREATE PROCEDURE sp_get_employee_by_id(IN emp_id INT)
BEGIN
    SELECT IdEmployee, DocumentNumber, CompleteName, Salary
    FROM Employee
    WHERE IdEmployee = emp_id;
END $$

DELIMITER ;

CALL sp_get_employee_by_id(1); 


DELIMITER $$

CREATE PROCEDURE sp_create_employee(
    IN p_DocumentNumber VARCHAR(8),
    IN p_CompleteName VARCHAR(60),
    IN p_Salary INT
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SELECT 'Error: Document Number Already Exists.' AS ErrorMessage;
    END;

    INSERT INTO Employee (DocumentNumber, CompleteName, Salary)
    VALUES (p_DocumentNumber, p_CompleteName, p_Salary);
    
END $$

DELIMITER ;

CALL sp_create_employee('12345678', 'John Doe', 50000);




