using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");

    SqlCommand command = connection.CreateCommand();

    /*

    command.CommandText = "SELECT COUNT(*) FROM products";
    object? count = await command.ExecuteScalarAsync();

    command.CommandText = "SELECT SUM(price) FROM products";
    object? amount = await command.ExecuteScalarAsync();

    command.CommandText = "SELECT MIN(price) FROM products";
    object? min = await command.ExecuteScalarAsync();

    command.CommandText = "SELECT MAX(price) FROM products";
    object? max = await command.ExecuteScalarAsync();

    command.CommandText = "SELECT AVG(price) FROM products";
    object? avg = await command.ExecuteScalarAsync();

    Console.WriteLine($"full count records: {count}");
    Console.WriteLine($"full sum prices: {amount}");
    Console.WriteLine($"min price: {min}");
    Console.WriteLine($"max price: {max}");
    Console.WriteLine($"avg price: {avg}");

    */
    string last_name, first_name, birth_date;
    

    Console.Write("Input last name: ");
    last_name = Console.ReadLine();

    Console.Write("Input first name: ");
    first_name = Console.ReadLine();

    Console.Write("Input birth date: ");
    birth_date = Console.ReadLine();

    string sqlCommand = $@"INSERT INTO authors
                            (last_name, first_name, birth_date)
                            VALUES(@last_name, @first_name, @birth_date);
                            SET @id=SCOPE_IDENTITY()";
    command.CommandText = sqlCommand;

    SqlParameter firstNameParameter 
        = new SqlParameter("@first_name", first_name);
    SqlParameter lastNameParameter 
        = new SqlParameter("@last_name", last_name);
    SqlParameter birthDateParameter 
        = new SqlParameter("@birth_date", DateTime.Parse(birth_date!));

    SqlParameter idParameter = new SqlParameter()
    {
        ParameterName = "@id",
        SqlDbType = System.Data.SqlDbType.Int,
        Direction = System.Data.ParameterDirection.Output
    };

    //command.Parameters.Add(firstNameParameter);
    //command.Parameters.Add(lastNameParameter);
    command.Parameters.AddRange(new[] { firstNameParameter, 
                                        lastNameParameter, 
                                        birthDateParameter,
                                        idParameter});

    await command.ExecuteNonQueryAsync();

    Console.WriteLine($"Add record with id {idParameter.Value}");

}