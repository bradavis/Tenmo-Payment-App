using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;

        public AccountSqlDAO(string dbConnection)
        {

            connectionString = dbConnection;
        }

        public decimal GetBalance(int id)
        {
            decimal returnUserBalance = 0M;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT accounts.balance FROM users JOIN accounts ON accounts.user_id = users.user_id WHERE users.user_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            returnUserBalance = reader.GetDecimal(0);

                            return returnUserBalance;
                        }



                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUserBalance;
        }

    }
}
/* SELECT transfer_id, account_from, account_to, amount, users.username
FROM transfers
JOIN transfer_statuses ON transfer_statuses.transfer_status_id = transfers.transfer_status_id
JOIN transfer_types ON transfer_types.transfer_type_id = transfers.transfer_type_id
jOIN accounts ON accounts.account_id = transfers.account_from OR accounts.account_id = transfers.account_to
JOIN users ON users.user_id = accounts.user_id
WHERE users.user_id = 3
*/