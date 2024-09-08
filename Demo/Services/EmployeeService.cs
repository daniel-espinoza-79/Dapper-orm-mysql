using Demo.Entities;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Demo.Services;

public class EmployeeService
{
    private readonly IConfiguration _configuration;
    private readonly string _sqlString;
    public EmployeeService(IConfiguration configuration)
    {
        _configuration = configuration;
        _sqlString = _configuration.GetConnectionString("DbConnectionString")!;
    }


    public async Task<List<Employee>> GetEmployees()
    {

        string sql = "sp_employee_list";
        using var conn = new MySqlConnection(_sqlString);
        var employeesList = await conn.QueryAsync<Employee>(sql, commandType: CommandType.StoredProcedure);
        return employeesList.ToList();
    }



    public async Task<Employee?> GetById(int id)
    {

        string sql = "sp_get_employee_by_id";
        var parameters = new DynamicParameters();
        parameters.Add("@emp_id", id, dbType: DbType.Int32);

        using var conn = new MySqlConnection(_sqlString);
        var employee = await conn.QueryFirstOrDefaultAsync<Employee>(sql, parameters, commandType: CommandType.StoredProcedure);
        return employee;
    }


    public async Task<int> AddEmployee(Employee employee)
    {
        string sql = "sp_create_employee";
        using var conn = new MySqlConnection(_sqlString);
     
        var parameters = new DynamicParameters();
        parameters.Add("@p_DocumentNumber", employee.DocumentNumber, dbType: DbType.Decimal);
        parameters.Add("@p_CompleteName", employee.CompleteName, dbType: DbType.String);
        parameters.Add("@p_Salary", employee.Salary, dbType: DbType.Int32);
     
        var result = await conn.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

        return result;
    }






}