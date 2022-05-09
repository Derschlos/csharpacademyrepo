using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class Languages
    {
        public Languages(int id, string name)
        {   
            _id = id;
            _name = name;
            correct = 0;
        }
        private int _id;
        public int Id { get { return _id; } }
        private string _name;
        public string Name { get { return _name; } /*set { _name = value; }*/ }
        private Dictionary<int, Card> _cardStack;
        public Dictionary<int, Card> CardStack { get { return _cardStack; } }
        public int correct {get;set;}
        private void loadCards()
        {

        }
        
    }
    internal class Card
    {
        public Card(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        
    }

}
