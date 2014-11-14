using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageSnippets.Table
{
    class CustomerEntity : TableEntity
    {
        public CustomerEntity (string familyName, string GivenName)
        {
            this.PartitionKey = familyName;
            this.RowKey = GivenName;
        }

        public CustomerEntity() { }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
