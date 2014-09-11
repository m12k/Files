using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Criterion;

using FluentNHibernateTesting.Model;


namespace FluentNHibernateTesting
{
    class Program
    { 
         
        static void Main(string[] args)
        {
           
            sessionFactory = BuildSessionFactory();
             
            using (ISession session = sessionFactory.OpenSession()) 
                { 
                    using (ITransaction transaction = session.BeginTransaction()) 
                    { 
                        //Get post with id == 2 
                        int id_post = 2; 
                        Post p1 = session.Get<Post>(id_post); 
                        //Get employee with id == 1 
                        int id_employee = 1; 
                        Employee emp = session.Get<Employee>(id_employee); 
                        //Get a list of all posts 
                        IList<Post> posts = session 
                            .CreateCriteria(typeof(Post)) 
                            .List<Post>(); 

                        ////Get a post by given name 
                        //string post_name = "Maribor"; 
                        //Post p2 = session.CreateCriteria(typeof(Post)) 
                        //    .Add(Expression<b></b>session.Equals("PostName", post_name)) 
                        //    .UniqueResult<Post>(); 

                         
                        var post = session.CreateCriteria<Post>()
                            .Add(Restrictions.Eq("PostName", "Test1")).List<Post>();
                          

                        string pName;
                        foreach (Post p in post)
                        {
                            pName = p.PostName;
                        }
                    } 
                } 
        }

        private static ISessionFactory sessionFactory;


        private static ISessionFactory BuildSessionFactory()
        {

            //This method build our session factory -

            //the most important part of our ORM application
            AutoPersistenceModel model = CreateMappings();
 
            return Fluently.Configure() 
                .Database(MsSqlConfiguration.MsSql2005 
                .ConnectionString(c => c 
                    .Server(@"VMANNMBPS1\SQLEXPRESS") 
                    .Database("Hibernate")  
                    .TrustedConnection()))
                .Mappings(m => m 
                    .AutoMappings.Add(model)) 
                .ExposeConfiguration(BuildSchema) 
                .BuildSessionFactory(); 
        }
        
        private static AutoPersistenceModel CreateMappings()
        {
            //This method will create our auto-mappings model 
            return AutoMap 
                    .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                    .Where(t => t.Namespace == "FluentNHibernateTesting.Model"); 
        }
         
        private static void BuildSchema(Configuration config)
        { 
            //This method will create/recreate our database 
            //This method should be called only once when 
            //we want to create our database

            new SchemaExport(config).Create(false, false); 
        }
         
    }
}
