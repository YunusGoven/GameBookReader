using System.Collections.Generic;

namespace GameBook.Domains
{
    public interface IBook
    {
        string Title { get; }
        Paragraph GetThisParagraph(int num);
        bool IsEmpty { get; }
    }


    /// <summary>
    /// Classe livre
    /// </summary>
    public class Book : IBook
    {

        /// <summary>
        /// Paragraphes du livre
        /// </summary>
        private readonly IDictionary<int, Paragraph> _paragraphs;
        /// <summary>
        /// Titre du livre
        /// </summary>
        public string Title { get; }


        /// <summary>
        /// Constructeur dun livre
        /// </summary>
        /// <param name="title">titre du livre</param>
        /// <param name="paragraphs">paragraphe du livre</param>
        public Book(string title, IDictionary<int, Paragraph> paragraphs)
        {
            this.Title = title ?? "Pas de titre";
            this._paragraphs = paragraphs ?? new Dictionary<int, Paragraph>();
        }




        /// <summary>
        /// Methode qui permet de recuper le paragraphe numero num
        /// </summary>
        /// <param name="num">numero du paragrapge a recuperer</param>
        /// <returns>un paragraphe </returns>
        public Paragraph GetThisParagraph(int num)
        {
            try
            {
                var paragraph = _paragraphs[num];
                return paragraph;
            }
            catch (KeyNotFoundException)
            {
                return new Paragraph(-1, "");
            }
        }

        /// <summary>
        /// Methode qui permet de savoir si le livre est vie
        /// </summary>
        /// <returns>vrai si le paragrape est vide sinon false</returns>
        public bool IsEmpty => _paragraphs.Count == 0;


    }
}
