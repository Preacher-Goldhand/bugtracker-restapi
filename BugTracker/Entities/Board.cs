using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual List<Quest> BoardTasks { get; set; } = new List<Quest>();
    }
}