using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Tool.hbm2ddl;
using NHibernate;

namespace NHibernateTesting
{
    class Program
    { 

        static void Main(string[] args)
        {

            var configuration = new Configuration();
            configuration.SessionFactoryName("BuildIt");

            configuration.DataBaseIntegration(db => {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                db.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=dbtest;Integrated Security=True";
                db.Timeout = 10;

                db.LogFormattedSql =true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
                 
            });

            ModelMapper mapper = new ModelMapper();
            mapper.AddMapping<PersonMap>();
            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Person) });

            configuration.AddDeserializedMapping(mapping, "dbTest");
            SchemaMetadataUpdater.QuoteTableAndColumns(configuration);

            ISessionFactory SessionFactory;
            SessionFactory = configuration.BuildSessionFactory();

            using (ISession session = SessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                Person p = new Person() { Code = "12312-1023-12", LastName = "Cajandig", FirstName = "Michael", BirthDate = DateTime.Today.AddYears(-27) };

                session.SaveOrUpdate(p);
                transaction.Commit();
            }
              
            System.Collections.Generic.Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(1, "");
             
            
            
            
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add(1, "");
            ht.Add("test","");



            //using (ISession session = SessionFactory.OpenSession())
            //using (ITransaction transaction = session.BeginTransaction())
            //{  
            //    List<Person> pList = session.CreateCriteria<Person>().List<Person>().ToList();
               
            //    foreach(Person p in pList)
            //    {
            //        Console.WriteLine("Last Name: " + p.LastName + " First Name: " + p.FirstName + "\n");  
            //    } 

            //}
             
            Console.WriteLine("Press a key...");
            Console.Read();
        }
    }

    public class Person
    { 
        public virtual int Id { get; set; }
        public virtual string Code{ get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual DateTime BirthDate { get; set; }         
    
    }


    public class PersonMap:ClassMapping<Person>
    {
        public PersonMap()
        { 
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));            
            Property<string>(x => x.Code);
            Property<string>(x => x.LastName);
            Property<string>(x => x.FirstName);
            Property<DateTime>(x => x.BirthDate); 
        }
         
    }
}
