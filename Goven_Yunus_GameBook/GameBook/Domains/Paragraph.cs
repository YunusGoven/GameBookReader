using System;
using System.Collections.Generic;

namespace GameBook.Domains
{
    public class Paragraph
    {
        /// <summary>
        /// Contenu du paragraphe
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Destination du paragraphe
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Liste des choix possible
        /// </summary>
        public ISet<Action> Actions { get; }


        /// <summary>
        /// Constructeur d'un paragraphe
        /// </summary>
        /// <param name="description"> contenu du paragraphe</param>
        /// <param name="number">numero du paragraphe</param>
        /// <param name="choicePossible"> liste de choix possible</param>
        public Paragraph(int number, string description, ISet<Action> choicePossible)
        {
            this.Number = number;
            this.Content = description ?? "----";
            this.Actions = choicePossible ?? new HashSet<Action>();

        }
        public Paragraph(int number, string content)
        {
            this.Number = number;
            this.Content = content ?? "----";
            this.Actions = new HashSet<Action>();
        }

        /// <summary>
        /// Methode qui permet de savoir si un paragraphe contient des actions et donc determine si il est le dernier
        /// </summary>
        /// <returns>true si les choix du paragraphe sont vides sinon false</returns>
        public bool ChoiceIsEmpty() => Actions.Count == 0;

        public override bool Equals(object obj)
        {
            if (!(obj is Paragraph)) return false;
            Paragraph p = (Paragraph)obj;
            return Equals(p);
        }

        public string GetContentInShort()
        {
            var split = Content.Split(" ");
            if (split.Length < 4) return $"{Number} - {Content}";
            var txt = "";
            for (var i = 0; i < 4; i++)
            {
                txt += $"{split[i]} ";
            }

            return $"{Number} - {txt}";
        }

        protected bool Equals(Paragraph other) => Content == other.Content && Number == other.Number;


        public override int GetHashCode() => HashCode.Combine(Content, Number);

    }
}
