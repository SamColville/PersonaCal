using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaCal
{
    public class FusionResult
    {
        public PersonasContainers DB { get; set; } = new PersonasContainers();
        //public Arcana[] Arcanas { get; set; }
        public Persona Parent1 { get; set; }
        public Persona Parent2 { get; set; }
        public Persona Result { get; set; }
        private readonly int[,] ResultKeys = MainWindow.ResultKeys;

        public FusionResult(Persona parent1, Persona parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
        }

        public Persona CheckPersonaResult()
        {
            //Check arcana of child
            Arcana ResultArc = CheckArcana();

            //if 'empty' arcana is returnes, no personas will be selected
            //otherwise, select only personas of child arcana
            var arcanaQuery = DB.Personas.Where(p => p.Arcana.Id == ResultArc.Id);

            //child persona's level will be the 'next up'
            //ie if average is 35, the child persona will have 
            //a level of 35 of greater.
            //Note: Math.Ceiling used as average level is rounded up in game
            double levelAverage = Math.Ceiling((double)(Parent1.Level + Parent2.Level) / 2);
            
            //find all personas of levels >= average, and select the first
            //Personas are sorted by arcana then level in db
            var Result = arcanaQuery.Where(p => p.Level >= levelAverage).First();
            
            return Result as Persona;
        }

        public Arcana CheckArcana()
        {
            //use arcana id of parents to obtain key of child arcana
            int resultKey = ResultKeys[(Parent1.Arcana.Id - 1), (Parent2.Arcana.Id - 1)];
            if (resultKey == 0)
            {
                //some fusions are not possible, this returns an 'empty' arcana
                return DB.Arcanas.Where(k => k.Id == 1002).First();
            }
            else
            {
                return DB.Arcanas.Where(k => k.Id == resultKey).First();
            }
        }
    }
}
