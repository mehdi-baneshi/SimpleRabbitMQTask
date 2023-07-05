using FluentMigrator;
using SimpleRabbit.Subscriber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.DapperDataAccess.Migrations
{
    [Migration(202306300001)]
    public class InitialTables_202306300001:Migration
    {
        public override void Down()
        {
            Delete.Table(nameof(Person)+"s");
        }
        public override void Up()
        {
            Create.Table(nameof(Person) + "s")
                .WithColumn(nameof(Person.Id)).AsString().NotNullable().PrimaryKey()
                .WithColumn(nameof(Person.FirstName)).AsString(50).NotNullable()
                .WithColumn(nameof(Person.LastName)).AsString(50).NotNullable()
                .WithColumn(nameof(Person.Age)).AsInt32().NotNullable();
        }
    }
}
