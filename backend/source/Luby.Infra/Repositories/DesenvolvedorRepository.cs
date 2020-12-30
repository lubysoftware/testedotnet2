
using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;
namespace Luby.Infra.Repositories
{
    public class DesenvolvedorRepository:Repository<Desenvolvedor>
    {
       public DesenvolvedorRepository(LubyContext context):base (context)
       {}

       public override Desenvolvedor GetById(int id){
           var query =_context.Set<Desenvolvedor>().Where(e=>e.Id==id);

           if(query.Any()){
               return query.First();               
           }
           return null;
       }

       public override IEnumerable<Desenvolvedor> GetAll(){
           var query =_context.Set<Desenvolvedor>();
           return query.Any()?query.ToList():new List<Desenvolvedor>();
       }
    }
}