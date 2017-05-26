﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MoonTrees.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonTrees.Data {
    public class MoonTrees {
        private static readonly string MoonTreeStorageString = Environment.GetEnvironmentVariable("MoonTreeStorage");

        CloudTable table;

        public MoonTrees() {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(MoonTreeStorageString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference("Trees");
            table.CreateIfNotExists();
        }
        public async Task<IEnumerable<Tree>> Get() {
            var trees = new List<Tree>();

            TableQuery<Tree> query = new TableQuery<Tree>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Tree.Pkey));
            foreach (var tree in table.ExecuteQuery(query)) {
                trees.Add(tree);
            }

            return trees;
        }
        public async Task<Tree> Get(string key) {
            var tree = new Tree();
            TableOperation retrieveOperation = TableOperation.Retrieve<Tree>(Tree.Pkey, key);
            
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            if (retrievedResult.Result == null) {
                throw new ArgumentException($"The record, {key}, could not be found");
            } else {
                tree = retrievedResult.Result as Tree;
            }

            return tree;
        }
        public async Task Insert(Tree tree) {
            TableOperation insertOperation = TableOperation.Insert(tree);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
        }
    }
}
