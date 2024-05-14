using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    public interface IInteractable
    {
        EntityType EntityType { get; }
        int Value { get; set; }
        void Interact(IInteractable entity);
    }
}
