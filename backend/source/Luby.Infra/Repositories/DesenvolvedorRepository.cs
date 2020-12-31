
using System.Collections.Generic;
using System.Linq;
using Luby.Domain.Models;
using Luby.Infra.Context;
namespace Luby.Infra.Repositories
{
    public class DesenvolvedorRepository:Repository<Luby.Domain.Models.Desenvolvedor>
    {
       public DesenvolvedorRepository(LubyContext context):base (context)
       {}

       public override Luby.Domain.Models.Desenvolvedor GetById(int id){
           var query =_context.Set<Luby.Domain.Models.Desenvolvedor>().Where(e=>e.Id==id);

           if(query.Any()){
               return query.First();               
           }
           return null;
       }

       public override IEnumerable<Luby.Domain.Models.Desenvolvedor> GetAll(){
           var query =_context.Set<Luby.Domain.Models.Desenvolvedor>();
           return query.Any()?query.ToList():new List<Luby.Domain.Models.Desenvolvedor>();
       }
    }
}