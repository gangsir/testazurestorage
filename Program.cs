using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using AzureStorageSnippets.Table;

namespace AzureStorageSnippets
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a storage account object
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=pshstgacc;AccountKey=EUE6tZNg4NjoZCcP004TkXHR3lN2KtsOrkR4T2frb7c6YbZUVWNXmFvHnac6KRh51t2bbKFuxepdFFJE8yGSHQ==");

            // create a table client which represent the table service
            var tableClient = storageAccount.CreateCloudTableClient();
            // create the table if it doesnt exist
            var peopleTable = tableClient.GetTableReference("people");
            peopleTable.CreateIfNotExists();

            //customer entity instance is the row data of table
            // batch operation
            var batchOperation = new TableBatchOperation();

            //var customer1 = new CustomerEntity("liu", "chungang");
            //customer1.Email = "sdfsd@outlook.com";
            //customer1.PhoneNumber = "2345523";
            //var customer2 = new CustomerEntity("wang", "sdfs");
            //customer2.Email = "erses@outlook.com";
            //customer2.PhoneNumber = "1561832342386639";
            //var customer3 = new CustomerEntity("wang", "ewedf");
            //customer3.Email = "gsdg@outlook.com";
            //customer3.PhoneNumber = "3242";
            //var customer4 = new CustomerEntity("wang", "gsdf");
            //customer4.Email = "gstds@outlook.com";
            //customer4.PhoneNumber = "23423";

            //create the TableOperation that inserts the customer entity;
            //TableOperation updateOperation = TableOperation.Replace(customer1);

            //batchOperation.Replace(customer2);
            //batchOperation.Replace(customer3);
            //batchOperation.Replace(customer4);

            // create a TableQuery object and set the filters
            var query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition
                ("PartitionKey", QueryComparisons.Equal, "wang"));

            var query1 = new TableQuery<CustomerEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "wang"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, "z")));

            // execute the query
            var queryResults = peopleTable.ExecuteQuery(query1); 

            //print the fields for each customer
            foreach (var entity in queryResults)
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey, entity.Email, entity.PhoneNumber);
            }

            //Execute the operation
            //peopleTable.Execute(updateOperation);
            //peopleTable.ExecuteBatch(batchOperation);

            Console.ReadKey();
        }
    }
}
