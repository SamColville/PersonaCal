using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaCal
{
    class SearchResult
    {
        public PersonasContainers db = new PersonasContainers();

        public Persona Child { get; set; }
        public List<int[]> PossibleCombos { get; set; }
        public List<Persona[]> ResultList { get; set; }
        private readonly int[,] ResultTab = MainWindow.ResultKeys;
        Persona CheckResult;

        public SearchResult(Persona child)
        {
            Child = child;
            PossibleCombos = new List<int[]>();
            ResultList = new List<Persona[]>();
            ResultTab = MainWindow.ResultKeys;
    }

        public void CheckPossible()
        {
            //Loop checks each value in the ResultsTable, and saves index 
            //of co-ordinates that produce this result.
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Child.Arcana.Id == MainWindow.ResultKeys[i, j])
                    {
                        PossibleCombos.Add(new int[] { i + 1, j + 1 });
                    }
                }
            }
        }

        public void VerifyResult()
        {
            int maxLevel = Child.Level;
            int parentOneLevel, parentTwoLevel;
            Persona parentOne = new Persona();
            Persona parentTwo = new Persona();
            ResultList = new List<Persona[]>();

            if (PossibleCombos.Count != 0)
            {
                foreach (var parent in PossibleCombos)
                {
                    //for the average level to be less than child level, 
                    //both parent's levels must be less than, or equal to, the child level
                    //Note that 2d array indicies will be 'out by one'.
                    parentOneLevel = parent[0] + 1;
                    parentTwoLevel = parent[1] + 1;
                    var parentOneList = db.Personas.Where(p => p.Level <= maxLevel && p.Arcana.Id == parentOneLevel);
                    parentOne = parentOneList.AsEnumerable().Last() as Persona;
                    var parentTwoList = db.Personas.Where(p => p.Level <= maxLevel && p.Arcana.Id == parentTwoLevel);
                    parentTwo = parentTwoList.AsEnumerable().Last() as Persona;

                    //Verify that the fusion results in the child persona
                    //else add empty personas
                    CheckResult = new FusionResult(parentOne, parentTwo).CheckPersonaResult();
                    if (CheckResult.Id == Child.Id)
                        ResultList.Add(new Persona[] { parentOne, parentTwo });
                    else
                    {
                        ResultList.Add(new Persona[]
                        {
                            new Persona() { Arcana_Id = 1002 }, new Persona() { Arcana_Id = 1002 }
                        });

                    }
                }
            }
        }
    }
}
