using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void AddTransfer(Transfer transfer)
        {
            try // need to add the transfer
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) // opens a connection
                {
                    conn.Open();

                    string commandText = "INSERT INTO transfers(transfer_id, transfer_type_id, transfer_status_id, account_from_account_to, amount &&" +
                        "VALUES(@transfer_id, @transfer_type_id, @transfer_status_id, @account_from_account_to, @amount)";

                    SqlCommand cmd = new SqlCommand(commandText, conn);

                    cmd.Parameters.AddWithValue("@transfer_id", transfer.ID);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transfer.Type);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transfer.ToUserId);
                    cmd.Parameters.AddWithValue("@account_from_account_to", transfer.FromUser);
                    cmd.Parameters.AddWithValue("@amount", transfer.ToUser);

                    cmd.ExecuteNonQuery(); // added the transfer

                    cmd = new SqlCommand("SELECT MAX(ID) AS lastinsertedrow FROM transfer;", conn);
                    int newID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
